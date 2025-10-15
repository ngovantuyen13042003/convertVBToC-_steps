// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名国保マスタ更新(ABAtenaKokuhoupBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/11/12　吉澤　行宣
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/02/26  000001     RⅢ連携（ワークフロー）処理を追加
// * 2004/03/08  000002     住基更新処理有無の判定を追加
// * 2004/03/26  000003     ビジネスIDの変更修正
// * 2005/12/01  000004     住基の個別事項更新結果を評価するかしないかの処理を追加
// * 2010/04/16  000005      VS2008対応（比嘉）
// * 2022/12/16  000006    【AB-8010】住民コード世帯コード15桁対応(下村)
// * 2024/02/19  000007    【AB-9001_1】個別記載事項対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.framework.us.publicmodule.library.businesscommon.uwfkokai;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABAtenaKokuhoupBClass
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
        private const string THIS_CLASS_NAME = "ABAtenaKokuhoupBClass";
        private const string AA_BUSSINESS_ID = "AA";          // 業務コード
                                                              // *履歴番号 000001 2004/02/26 追加開始
        private const string WORK_FLOW_NAME = "宛名国保個別事項";             // ワークフロー名
        private const string DATA_NAME = "国保個別";                      // データ名
                                                                      // *履歴番号 000001 2004/02/26 追加終了

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
        public ABAtenaKokuhoupBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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

            // *履歴番号 000003 2004/03/26 削除開始
            // '業務IDを宛名(AB)に変更
            // m_cfControlData.m_strBusinessId = "AB"
            // *履歴番号 000003 2004/03/26 削除終了

        }

        #endregion

        // ************************************************************************************************
        // * メソッド名     宛名国保マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
        // * 
        // * 機能　　    　  宛名国保マスタのデータを更新する。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaKokuho(ABKobetsuKokuhoProperty cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaKokuho";
            int intUpdCnt;
            ABAtenaKokuhoBClass cABAtenaKokuhoBClass;
            var cAAKOBETSUKOKUHOParamClass = new localhost.AAKOBETSUKOKUHOParamClass[1];
            localhost.AACommonBSClass cAACommonBSClass;
            DataSet csABAtenaKokuhoEntity;
            DataRow cDatRow;
            string strControlData;
            var cUSSCItyInfo = new USSCityInfoClass();
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFAppException csAppExp;
            // *履歴番号 000001 2004/02/26 追加開始
            ABAtenaKanriJohoBClass cAtenaKanriJohoB;      // 宛名管理情報ＤＡビジネスクラス
            DataSet csAtenaKanriEntity;                   // 宛名管理情報データセット
                                                          // *履歴番号 000001 2004/02/26 追加終了
                                                          // *履歴番号 000004 2005/12/01 追加開始
            string strJukiResult;                         // 住基の結果をチェックするかどうか(0:する 1:しない)
                                                          // *履歴番号 000004 2005/12/01 追加終了

            try
            {

                // *履歴番号 000003 2004/03/26 追加開始
                // 業務IDを宛名(AB)に変更
                m_cfControlData.m_strBusinessId = "AB";
                // *履歴番号 000003 2004/03/26 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 市町村情報取得（市町村コード)
                cUSSCItyInfo.GetCityInfo(m_cfControlData);

                // 宛名国保ＤＡクラスのインスタンス化
                cABAtenaKokuhoBClass = new ABAtenaKokuhoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);

                // 宛名国保マスタ抽出呼び出し
                csABAtenaKokuhoEntity = cABAtenaKokuhoBClass.GetAtenaKokuho(cABKobetsuProperty.p_strJUMINCD);

                // 追加・更新の判定
                if (csABAtenaKokuhoEntity.Tables[ABAtenaKokuhoEntity.TABLE_NAME].Rows.Count == 0)
                {

                    cDatRow = csABAtenaKokuhoEntity.Tables[ABAtenaKokuhoEntity.TABLE_NAME].NewRow();
                    // 各項目をプロパティから取得
                    cDatRow[ABAtenaKokuhoEntity.JUMINCD] = cABKobetsuProperty.p_strJUMINCD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHONO] = cABKobetsuProperty.p_strKOKUHONO;
                    cDatRow[ABAtenaKokuhoEntity.HIHOKENSHAGAITOKB] = string.Empty;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKB] = cABKobetsuProperty.p_strKOKUHOGAKUENKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD] = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD] = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKB] = cABKobetsuProperty.p_strKOKUHOTISHKKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO] = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO;   // *DB(ABATENAKOKUHO)に存在してない
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOHOKENSHONO] = cABKobetsuProperty.p_strKOKUHOHOKENSHONO;

                    // 市町村コード
                    cDatRow[ABAtenaKokuhoEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                    // 旧市町村コード
                    cDatRow[ABAtenaKokuhoEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                    // データの追加
                    // csABAtenaKokuhoEntity.Tables[ABAtenaKokuhoEntity.TABLE_NAME].Rows.Add(cDatRow)

                    // 宛名国保マスタ追加メソッド呼び出し
                    intUpdCnt = cABAtenaKokuhoBClass.InsertAtenaKokuho(cDatRow);
                }
                else
                {

                    cDatRow = csABAtenaKokuhoEntity.Tables[ABAtenaKokuhoEntity.TABLE_NAME].Rows[0];
                    // 各項目をプロパティから取得
                    cDatRow[ABAtenaKokuhoEntity.JUMINCD] = cABKobetsuProperty.p_strJUMINCD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHONO] = cABKobetsuProperty.p_strKOKUHONO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKB] = cABKobetsuProperty.p_strKOKUHOGAKUENKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD] = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD] = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKB] = cABKobetsuProperty.p_strKOKUHOTISHKKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD;
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO] = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO;  // *DB(ABATENAKOKUHO)に存在してない
                    cDatRow[ABAtenaKokuhoEntity.KOKUHOHOKENSHONO] = cABKobetsuProperty.p_strKOKUHOHOKENSHONO;

                    // 市町村コード
                    cDatRow[ABAtenaKokuhoEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                    // 旧市町村コード
                    cDatRow[ABAtenaKokuhoEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                    // 宛名国保マスタ更新メソッド呼び出し
                    intUpdCnt = cABAtenaKokuhoBClass.UpdateAtenaKokuho(cDatRow);
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


                // *履歴番号 000002 2004/03/08 追加開始
                // 宛名管理情報Ｂクラスのインスタンス作成
                cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // 宛名管理情報の種別04識別キー01のデータを全件取得する
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "11");

                // 管理情報の住基更新レコードが存在し、パラメータが"0"の時だけ住基更新処理を行う
                if (!(csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0))
                {
                    if ((string)csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == "0")
                    {

                        // WebserviceのURLをWebConfigから取得して設定する
                        cAACommonBSClass = new localhost.AACommonBSClass();
                        cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx";
                        // cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                        // 個別国保パラメータのインスタンス化
                        cAAKOBETSUKOKUHOParamClass[0] = new localhost.AAKOBETSUKOKUHOParamClass();

                        // 更新・追加した項目を取得
                        cAAKOBETSUKOKUHOParamClass[0].m_strJUMINCD = cABKobetsuProperty.p_strJUMINCD;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHONO = cABKobetsuProperty.p_strKOKUHONO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOSHIKAKUKB = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOSHIKAKUKBMEISHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOSHIKAKUKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOGAKUENKB = cABKobetsuProperty.p_strKOKUHOGAKUENKB;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOGAKUENKBMEISHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOGAKUENKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOSHUTOKUYMD = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOSOSHITSUYMD = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKKB = cABKobetsuProperty.p_strKOKUHOTISHKKB;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKHONHIKB = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKHONHIKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO; // ＊国保退職本被区分正式名称英字項目名に間違いあり＊
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKHONHIKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOTISHKHIGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOHOKENSHOKIGO = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO;
                        cAAKOBETSUKOKUHOParamClass[0].m_strKOKUHOHOKENSHONO = cABKobetsuProperty.p_strKOKUHOHOKENSHONO;

                        // 住基個別国保更新メソッドを実行する
                        strControlData = UFControlToolClass.ControlGetStr(m_cfControlData);
                        intUpdCnt = cAACommonBSClass.UpdateKBKOKUHO(strControlData, cAAKOBETSUKOKUHOParamClass);

                        // *履歴番号 000004 2005/12/01 追加開始
                        // 宛名管理情報の種別04識別キー22のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                        csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "22");
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
                        // *履歴番号 000004 2005/12/01 追加終了

                        // *履歴番号 000004 2005/12/01 修正開始
                        // * corresponds to VS2008 Start 2010/04/16 000005
                        // '''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                        // '''If intUpdCnt = 0 Then
                        // '''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        // '''    'エラー定義を取得
                        // '''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                        // '''    '例外を生成
                        // '''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        // '''    Throw csAppExp
                        // '''End If
                        // * corresponds to VS2008 End 2010/04/16 000005
                        if (strJukiResult == "0")
                        {
                            // 管理情報から取得した内容が"0"のときはチェックする
                            // 追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                            if (intUpdCnt == 0)
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
                        // *履歴番号 000004 2005/12/01 修正終了
                    }
                }
                // *履歴番号 000002 2004/03/08 追加開始

                // *履歴番号 000001 2004/02/26 追加開始
                // 宛名管理情報の種別04識別キー01のデータを全件取得する
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "02");

                // 管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
                if (!(csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0))
                {
                    if ((string)csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == "1")
                    {
                        // ワークフロー処理メソッドを呼ぶ
                        WorkFlowSet(cABKobetsuProperty);
                    }
                }
                // *履歴番号 000001 2004/02/26 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

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


        // *履歴番号 000001 2004/02/26 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名国保ワークフロー
        // * 
        // * 構文           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
        // * 
        // * 機能　　    　  宛名国保データをワークフローへ渡す。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public void WorkFlowSet(ABKobetsuKokuhoProperty cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "WorkFlowSet";
            // * corresponds to VS2008 Start 2010/04/16 000005
            // Dim cwMessage As UWMessageClass                     'ワークフロー起動クラス
            // Dim cwStartRetInfo As UWStartRetInfo                'ワークフロー開始クラス
            // * corresponds to VS2008 End 2010/04/16 000005
            // Dim cUWSerialGroupId(0) As UWSerialGroupId
            // Dim cUWSerialGroupIdTemp As UWSerialGroupId
            // Dim cwDataInfo As UWStartDataInfo                                              ' ワークフローデータ
            string strMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;       // ワークフローデータ
            var cUWStartDataInfoForDataSet = new UWStartDataInfoForDataSet[1];
            // * corresponds to VS2008 Start 2010/04/16 000005
            // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000005
            var csABKokuhoEntity = new DataSet();               // 個別事項国保データセット
            DataTable csABKokuhoTable;                    // 個別事項国保データテーブル
            DataRow csABKokuhoRow;                        // 個別事項国保データロウ
            string strNen;                                // 作成日時
            int intRecCnt;                            // 連番用カウンター
            var cuCityInfoClass = new USSCityInfoClass();       // 市町村管理情報クラス
            string strCityCD;                             // 市町村コード
            ABAtenaCnvBClass cABAtenaCnvBClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 市町村管理情報の取得
                cuCityInfoClass.GetCityInfo(m_cfControlData);
                // 市町村コードの取得
                strCityCD = cuCityInfoClass.p_strShichosonCD[0];
                // 作成日時(14桁)の取得
                strNen = DateTime.Now.ToString("yyyyMMddHHmmss");
                // 連番用カウンターの初期設定
                intRecCnt = 1;

                // テーブルセットの取得
                csABKokuhoTable = CreateColumnsData();
                csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME;
                // データセットにテーブルセットの追加
                csABKokuhoEntity.Tables.Add(csABKokuhoTable);

                // *****
                // *　１行目の編集
                // *
                // *****
                // 新規レコードの作成
                csABKokuhoRow = csABKokuhoEntity.Tables[ABKobetsuKokuhoEntity.TABLE_NAME].NewRow();
                // 各項目にデータをセット
                csABKokuhoRow[ABKobetsuKokuhoEntity.SHICHOSONCD] = strCityCD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.SHIKIBETSUID] = "AA60";
                csABKokuhoRow[ABKobetsuKokuhoEntity.LASTRECKB] = "";
                csABKokuhoRow[ABKobetsuKokuhoEntity.SAKUSEIYMD] = strNen;
                csABKokuhoRow[ABKobetsuKokuhoEntity.RENBAN] = intRecCnt.ToString().RPadLeft(7, '0');
                csABKokuhoRow[ABKobetsuKokuhoEntity.JUMINCD] = cABKobetsuProperty.p_strJUMINCD.RSubstring(3, 12);
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHONO] = cABKobetsuProperty.p_strKOKUHONO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOGAKUENKB] = cABKobetsuProperty.p_strKOKUHOGAKUENKB;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD] = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD] = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKKB] = cABKobetsuProperty.p_strKOKUHOTISHKKB;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO] = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD] = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO] = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO;
                csABKokuhoRow[ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO] = cABKobetsuProperty.p_strKOKUHOHOKENSHONO;
                // データセットにレコードを追加
                csABKokuhoEntity.Tables[ABKobetsuKokuhoEntity.TABLE_NAME].Rows.Add(csABKokuhoRow);

                // *****
                // *　最終行の編集
                // *
                // *****
                // 連番用カウンタに１を足す
                intRecCnt += 1;
                // 新規レコードの作成
                csABKokuhoRow = csABKokuhoEntity.Tables[ABKobetsuKokuhoEntity.TABLE_NAME].NewRow();
                // 各項目にデータをセット
                csABKokuhoRow[ABKobetsuKokuhoEntity.SHICHOSONCD] = strCityCD;
                csABKokuhoRow[ABKobetsuKokuhoEntity.SHIKIBETSUID] = "AA60";
                csABKokuhoRow[ABKobetsuKokuhoEntity.LASTRECKB] = "E";
                csABKokuhoRow[ABKobetsuKokuhoEntity.SAKUSEIYMD] = strNen;
                csABKokuhoRow[ABKobetsuKokuhoEntity.RENBAN] = intRecCnt.ToString().RPadLeft(7, '0');
                // データセットにレコードを追加
                csABKokuhoEntity.Tables[ABKobetsuKokuhoEntity.TABLE_NAME].Rows.Add(csABKokuhoRow);

                // *****
                // *　ワークフロー送信
                // *
                // *****
                // データセット取得クラスのインスタンス化
                cABAtenaCnvBClass = new ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // ワークフロー送信処理呼び出し
                cABAtenaCnvBClass.WorkFlowExec(csABKokuhoEntity, WORK_FLOW_NAME, DATA_NAME);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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

        }


        // ************************************************************************************************
        // * メソッド名      データカラム作成
        // * 
        // * 構文            Private Function CreateColumnsData() As DataTable
        // * 
        // * 機能　　        レプリカＤＢのカラム定義を作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataTable() 代納情報テーブル
        // ************************************************************************************************
        private DataTable CreateColumnsData()
        {
            const string THIS_METHOD_NAME = "CreateColumnsData";
            DataTable csABKokuhoTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 代納情報カラム定義
                csABKokuhoTable = new DataTable();
                csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHIKIBETSUID, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SAKUSEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 14;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.LASTRECKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.RENBAN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHONO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 14;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 24;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 24;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 24;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 24;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 32;
                csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 32;

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;
            }

            return csABKokuhoTable;

        }
        // *履歴番号 000001 2004/02/26 追加終了

    }
}
