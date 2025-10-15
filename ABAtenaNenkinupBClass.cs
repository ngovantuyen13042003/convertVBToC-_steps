// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名年金マスタ更新(ABAtenaNenkinupBClas)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/11/11　吉澤　行宣
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/03/26 000001     ビジネスIDの変更修正
// * 2005/12/01 000002     住基の個別事項更新結果を評価するかしないかの処理を追加
// * 2024/02/19 000003    【AB-9001_1】個別記載事項対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABAtenaNenkinupBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfABConfigDataClass;        // コンフィグデータAB
        private UFConfigDataClass m_cfAAConfigDataClass;       // コンフィグデータAA
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private string m_strRsBusiId;

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaNenkinupBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
        private const string AA_BUSSINESS_ID = "AA";            // 業務コード
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData,
        // * 　　                           ByVal cfConfigDataClass As UFConfigDataClass,
        // * 　　                           ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // * 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // * 　　            cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaNenkinupBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            UFConfigDataClass cfAAUFConfigData;
            UFConfigClass cfAAUFConfigClass;

            // ----------コンフィグデータの"AA"の環境情報を取得----------------------
            cfAAUFConfigClass = new UFConfigClass();
            cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID);
            m_cfAAConfigDataClass = cfAAUFConfigData;
            // ----------コンフィグデータの"AA"の環境情報を取得----------------------

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfABConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfABConfigDataClass, m_cfControlData.m_strBusinessId);

            // 受け取ったビジネスIDをメンバへ保存
            m_strRsBusiId = m_cfControlData.m_strBusinessId;

            // *履歴番号 000001 2004/03/26 削除開始
            // '業務IDを宛名(AB)に変更
            // m_cfControlData.m_strBusinessId = "AB"
            // *履歴番号 000001 2004/03/26 削除終了

        }

        #endregion

        // ************************************************************************************************
        // * メソッド名     宛名年金マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaNenkin(ByVal cABKobetsuProperty As ABKobetsuNenkinProperty) As Integer
        // * 
        // * 機能　　    　  宛名年金マスタのデータを更新する。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaNenkin(ABKobetsuNenkinProperty[] cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaNenkin";
            int intUpdCnt;
            ABAtenaNenkinBClass cABAtenaNenkinBClass;
            var cAAKOBETSUNENKINParamClass = new localhost.AAKOBETSUNENKINParamClass[1];
            localhost.AACommonBSClass cAACommonBSClass;
            DataSet csAtenaNenkinEntity;
            DataRow cDatRow;
            string strControlData;
            var cUSSCItyInfo = new USSCityInfoClass();
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFAppException csAppExp;
            // *履歴番号 000002 2005/12/01 追加開始
            ABAtenaKanriJohoBClass cAtenaKanriJohoB;      // 宛名管理情報ＤＡビジネスクラス
            DataSet csAtenaKanriEntity;                   // 宛名管理情報データセット
            string strJukiResult;                         // 住基の結果をチェックするかどうか(0:する 1:しない)
                                                          // *履歴番号 000002 2005/12/01 追加終了

            try
            {

                // *履歴番号 000001 2004/03/26 追加開始
                // 業務IDを宛名(AB)に変更
                m_cfControlData.m_strBusinessId = "AB";
                // *履歴番号 000001 2004/03/26 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 市町村情報取得（市町村コード)
                cUSSCItyInfo.GetCityInfo(m_cfControlData);

                // 宛名年金ＤＡクラスのインスタンス化
                cABAtenaNenkinBClass = new ABAtenaNenkinBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);

                int intcnt;
                var loopTo = cABKobetsuProperty.Length - 1;
                for (intcnt = 0; intcnt <= loopTo; intcnt++)
                {

                    // 宛名年金マスタ抽出呼び出し
                    csAtenaNenkinEntity = cABAtenaNenkinBClass.GetAtenaNenkin(Convert.ToString(cABKobetsuProperty[intcnt]).p_strJUMINCD);

                    // 追加・更新の判定
                    if (csAtenaNenkinEntity.Tables[ABAtenaNenkinEntity.TABLE_NAME].Rows.Count == 0)
                    {

                        cDatRow = csAtenaNenkinEntity.Tables[ABAtenaNenkinEntity.TABLE_NAME].NewRow();
                        // 各項目をプロパティから取得
                        cDatRow[ABAtenaNenkinEntity.JUMINCD] = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow[ABAtenaNenkinEntity.HIHOKENSHAGAITOKB] = string.Empty;
                        cDatRow[ABAtenaNenkinEntity.KSNENKNNO] = cABKobetsuProperty[intcnt].p_strKSNENKNNO;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKYMD] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKYMD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKSHU] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKSHU;
                        cDatRow[ABAtenaNenkinEntity.SHUBETSUHENKOYMD] = string.Empty;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKRIYUCD] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKRIYUCD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSSHTSYMD] = cABKobetsuProperty[intcnt].p_strSKAKSSHTSYMD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSSHTSRIYUCD] = cABKobetsuProperty[intcnt].p_strSKAKSSHTSRIYUCD;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO1] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO1] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU1] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN1] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB1] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO2] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO2] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU2] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN2] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB2] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO3] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO3] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU3] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN3] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB3] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB3;
                        // 市町村コード
                        cDatRow[ABAtenaNenkinEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                        // 旧市町村コード
                        cDatRow[ABAtenaNenkinEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                        // データの追加
                        // csAtenaNenkinEntity.Tables[ABAtenaNenkinEntity.TABLE_NAME].Rows.Add(cDatRow)

                        // 宛名年金マスタ追加メソッド呼び出し
                        intUpdCnt = cABAtenaNenkinBClass.InsertAtenaNenkin(cDatRow);
                    }
                    else
                    {

                        cDatRow = csAtenaNenkinEntity.Tables[ABAtenaNenkinEntity.TABLE_NAME].Rows[0];
                        // 各項目をプロパティから取得
                        cDatRow[ABAtenaNenkinEntity.JUMINCD] = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow[ABAtenaNenkinEntity.KSNENKNNO] = cABKobetsuProperty[intcnt].p_strKSNENKNNO;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKYMD] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKYMD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKSHU] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKSHU;
                        cDatRow[ABAtenaNenkinEntity.SKAKSHUTKRIYUCD] = cABKobetsuProperty[intcnt].p_strSKAKSHUTKRIYUCD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSSHTSYMD] = cABKobetsuProperty[intcnt].p_strSKAKSSHTSYMD;
                        cDatRow[ABAtenaNenkinEntity.SKAKSSHTSRIYUCD] = cABKobetsuProperty[intcnt].p_strSKAKSSHTSRIYUCD;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO1] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO1] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU1] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN1] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB1] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB1;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO2] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO2] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU2] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN2] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB2] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB2;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKIGO3] = cABKobetsuProperty[intcnt].p_strJKYNENKNKIGO3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNNO3] = cABKobetsuProperty[intcnt].p_strJKYNENKNNO3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNSHU3] = cABKobetsuProperty[intcnt].p_strJKYNENKNSHU3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNEDABAN3] = cABKobetsuProperty[intcnt].p_strJKYNENKNEDABAN3;
                        cDatRow[ABAtenaNenkinEntity.JKYNENKNKB3] = cABKobetsuProperty[intcnt].p_strJKYNENKNKB3;

                        // 市町村コード
                        cDatRow[ABAtenaNenkinEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                        // 旧市町村コード
                        cDatRow[ABAtenaNenkinEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                        // 宛名年金マスタ更新メソッド呼び出し
                        intUpdCnt = cABAtenaNenkinBClass.UpdateAtenaNenkin(cDatRow);
                    }

                    // 追加・更新件数が0件の時メッセージ"宛名の個別事項の更新は正常に行えませんでした"を返す
                    if (intUpdCnt == 0)
                    {
                        cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                        // エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004);
                        // 例外を生成
                        csAppExp = new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        throw csAppExp;
                    }

                }

                // *履歴番号 000002 2005/12/01 追加開始
                // 宛名管理情報Ｂクラスのインスタンス作成
                cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // 宛名管理情報の種別04識別キー23のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "23");
                // 管理情報にレコードが存在し、パラメータが"1"の時はチェックしない
                if (!(csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0))
                {
                    if ((string)csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == "1")
                    {
                        // ﾊﾟﾗﾒｰﾀが"1"のときはチェックしない
                        strJukiResult = "1";
                    }
                    else
                    {
                        // ﾊﾟﾗﾒｰﾀが"1"のときはチェックする
                        strJukiResult = "0";
                    }
                }
                else
                {
                    // レコードがないときはチェックする
                    strJukiResult = "0";
                }
                // *履歴番号 000002 2005/12/01 追加終了

                // WebserviceのURLをWebConfigから取得して設定する
                cAACommonBSClass = new localhost.AACommonBSClass();
                // m_cfLogClass.WarningWrite(m_cfControlData, m_cfABConfigDataClass.p_strWebServerDomain + "Densan/Reams/AA/AA001BS/AACommonBSClass.asmx")
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx";

                // cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                cAAKOBETSUNENKINParamClass = new localhost.AAKOBETSUNENKINParamClass[cABKobetsuProperty.Length];

                var loopTo1 = cABKobetsuProperty.Length - 1;
                for (intcnt = 0; intcnt <= loopTo1; intcnt++)
                {

                    // 個別年金パラメータのインスタンス化
                    cAAKOBETSUNENKINParamClass[intcnt] = new localhost.AAKOBETSUNENKINParamClass();

                    // 更新・追加した項目を取得
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJUMINCD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJUMINCD;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strKSNENKNNO = Convert.ToString(cABKobetsuProperty[intcnt]).p_strKSNENKNNO;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strSKAKSHUTKYMD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strSKAKSHUTKYMD;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strSKAKSHUTKSHU = Convert.ToString(cABKobetsuProperty[intcnt]).p_strSKAKSHUTKSHU;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strSKAKSHUTKRIYUCD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strSKAKSHUTKRIYUCD;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strSKAKSSHTSYMD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strSKAKSSHTSYMD;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strSKAKSSHTSRIYUCD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strSKAKSSHTSRIYUCD;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKIGO1 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKIGO1;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNNO1 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNNO1;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNSHU1 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNSHU1;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNEDABAN1 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNEDABAN1;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKB1 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKB1;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKIGO2 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKIGO2;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNNO2 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNNO2;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNSHU2 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNSHU2;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNEDABAN2 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNEDABAN2;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKB2 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKB2;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKIGO3 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKIGO3;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNNO3 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNNO3;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNSHU3 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNSHU3;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNEDABAN3 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNEDABAN3;
                    cAAKOBETSUNENKINParamClass[intcnt].m_strJKYNENKNKB3 = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJKYNENKNKB3;

                }

                // 住基個別年金更新メソッドを実行する
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData);
                intUpdCnt = cAACommonBSClass.UpdateKBNENKIN(strControlData, cAAKOBETSUNENKINParamClass);

                // *履歴番号 000002 2005/12/01 修正開始
                // '''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                // ''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                // ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                // ''''    'エラー定義を取得
                // ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                // ''''    '例外を生成
                // ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                // ''''    Throw csAppExp

                // ''''End If

                if (strJukiResult == "0")
                {
                    // 管理情報から取得した内容が"0"のときはチェックする
                    // 追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                    if (!(intUpdCnt == cABKobetsuProperty.Length))
                    {

                        cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                        // エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002);
                        // 例外を生成
                        csAppExp = new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        throw csAppExp;

                    }
                }
                else if (strJukiResult == "1")
                {
                }
                // チェックしない
                else
                {
                    // チェックしない
                }
            }
            // *履歴番号 000002 2005/12/01 修正終了

            catch (Global.System.Web.Services.Protocols.SoapException objSoapExp)             // SoapExceptionをキャッチ
            {
                // OuterXmlにエラー内容が格納してある。
                var objExpTool = new UFExceptionTool(objSoapExp.Detail.OuterXml);
                var objErr = default(UFErrorStruct);

                // アプリケーション例外かどうかの判定
                if (objExpTool.IsAppException == true)
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objExpTool.p_strErrorCode + "】" + "【ワーニング内容:" + objExpTool.p_strErrorMessage + "】");




                    // 付加メッセージを作成する
                    string strExtMsg = "<P>対象住民のリカバリ処理を行ってください。<BR>";

                    // アプリケーション例外を作成する
                    UFAppException objAppExp;
                    objAppExp = new UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode);

                    // 拡張領域のメッセージにも付加（実際にはここのメッセージが表示される）
                    UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt);
                    objErr.m_strErrorMessage += strExtMsg;
                    objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr);
                    // メッセージを付加しない場合は以下
                    // objAppExp.p_strExt = objExpTool.p_strExt

                    throw objAppExp;
                }
                else
                {
                    // システム例外の場合
                    // エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExpTool.p_strErrorMessage + "】");


                    throw objSoapExp;
                }
            }
            catch (UFAppException exAppExp)                   // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + exAppExp.Message + "】");



                throw exAppExp;
            }
            catch (Exception exExp)                           // Exceptionをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exExp.Message + "】");


                throw exExp;
            }
            finally
            {
                // 元のビジネスIDを入れる
                m_cfControlData.m_strBusinessId = m_strRsBusiId;
                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            return intUpdCnt;

        }

    }
}
