// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名選挙マスタ更新(ABAtenaSenkyoupBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/11/11　吉澤　行宣
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/03/26 000001     ビジネスIDの変更修正
// * 2005/02/17 000002     レスポンス改善：UpdateAtenaSenkyoでAtenaマスタ更新修正
// * 2006/03/17 000003     投票区コードの更新判定を修正
// * 2010/02/09 000004     管理情報により住基個別事項の更新を制御する
// * 2024/02/19 000005    【AB-9001_1】個別記載事項対応(下村)
// ************************************************************************************************
using System;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAtenaSenkyoupBClass
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
        private const string THIS_CLASS_NAME = "ABAtenaSenkyoupBClass";
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
        public ABAtenaSenkyoupBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
        // * メソッド名     宛名選挙マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaSenkyo(ByVal cABKobetsuProperty As ABKobetsuSenkyoProperty) As Integer
        // * 
        // * 機能　　    　  宛名選挙マスタのデータを更新する。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaSenkyo(ABKobetsuSenkyoProperty[] cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaSenkyo";
            var intUpdCnt = default(int);
            ABAtenaSenkyoBClass cABAtenaSenkyoBClass;
            var cAAKOBETSUSENKYOParamClass = new localhost.AAKOBETSUSENKYOParamClass[1];
            localhost.AACommonBSClass cAACommonBSClass;
            DataSet csABAtenaSenkyoEntity;
            DataRow cDatRow;
            string strControlData;
            var cUSSCItyInfo = new USSCityInfoClass();

            ABAtenaBClass cABAtenaBClass;
            DataSet csABAtenaEntity;
            DataRow cDatRowt;
            var cSearchKey = new ABAtenaSearchKey();            // 宛名検索キー
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFAppException csAppExp;
            int intcnt;

            // *履歴番号 000004 2010/02/09 追加開始
            ABAtenaKanriJohoBClass cAtenaKanriJohoB;      // 宛名管理情報ＤＡビジネスクラス
            DataSet csAtenaKanriEntity;                   // 宛名管理情報データセット
                                                          // *履歴番号 000004 2010/02/09 追加終了

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

                // 宛名選挙ＤＡクラスのインスタンス化
                cABAtenaSenkyoBClass = new ABAtenaSenkyoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);

                // 宛名ＤＡクラスのインスタンス化
                cABAtenaBClass = new ABAtenaBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                cSearchKey.p_strJuminYuseniKB = "1";

                var loopTo = cABKobetsuProperty.Length - 1;
                for (intcnt = 0; intcnt <= loopTo; intcnt++)
                {

                    // 宛名選挙マスタ抽出呼び出し
                    csABAtenaSenkyoEntity = cABAtenaSenkyoBClass.GetAtenaSenkyo(cABKobetsuProperty[intcnt].p_strJUMINCD);

                    // 追加・更新の判定
                    if (csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Count == 0)
                    {

                        cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).NewRow();
                        // 各項目をプロパティから取得
                        cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty[intcnt].p_strSenkyoShikakuKB;
                        cDatRow.Item(ABAtenaSenkyoEntity.TOROKUJOTAIKBN) = string.Empty;

                        // 市町村コード
                        cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);
                        // 旧市町村コード
                        cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);

                        // データの追加
                        // csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Add(cDatRow)

                        // 宛名選挙マスタ追加メソッド呼び出し
                        intUpdCnt = cABAtenaSenkyoBClass.InsertAtenaSenkyo(cDatRow);
                    }

                    else
                    {

                        cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows(0);
                        // 各項目をプロパティから取得
                        cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty[intcnt].p_strSenkyoShikakuKB;

                        // 市町村コード
                        cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);
                        // 旧市町村コード
                        cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);

                        // 宛名選挙マスタ更新メソッド呼び出し
                        intUpdCnt = cABAtenaSenkyoBClass.UpdateAtenaSenkyo(cDatRow);
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

                    // 宛名検索キーの設定をする
                    cSearchKey.p_strJuminCD = cABKobetsuProperty[intcnt].p_strJUMINCD;

                    // 宛名データを取得する
                    csABAtenaEntity = cABAtenaBClass.GetAtenaBHoshu(1, cSearchKey);

                    // 追加・更新の判定
                    if (csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count == 0)
                    {
                        intUpdCnt = 0;
                    }
                    else
                    {
                        // *履歴番号 000002 2005/02/17 修正開始　000003 2006/03/17 修正開始
                        // Rowを取得
                        cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0);

                        // 宛名マスタの投票区コードと個別プロパティの投票区コードが同じだったら更新しない
                        if (!((string)cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) == cABKobetsuProperty[intcnt].p_strTohyokuCD))
                        {
                            // 投票区CDをプロパティから取得
                            cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty[intcnt].p_strTohyokuCD;

                            // 宛名マスタ追加メソッド呼び出し
                            intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt);
                        }

                        // cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
                        // '投票区CDをプロパティから取得
                        // cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty(intcnt).p_strTohyokuCD

                        // '宛名マスタ追加メソッド呼び出し
                        // intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt)
                        // *履歴番号 000002 2004/02/17 修正終了　000003 2006/03/17 修正開始
                    }

                    // 追加・更新件数が0件の時0を返す
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

                // *履歴番号 000004 2010/02/09 修正開始
                // 宛名管理情報Ｂクラスのインスタンス作成
                cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // 宛名管理情報の種別04識別キー01のデータを全件取得する
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "15");

                // 管理情報の住基更新レコードが存在しない、または、パラメータが"0"の時だけ住基更新処理を行う
                if (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count == 0 || (string)csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER) == "0")
                {

                    // WebserviceのURLをWebConfigから取得して設定する
                    cAACommonBSClass = new localhost.AACommonBSClass();
                    cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx";
                    // cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                    cAAKOBETSUSENKYOParamClass = new localhost.AAKOBETSUSENKYOParamClass[cABKobetsuProperty.Length];

                    var loopTo1 = cABKobetsuProperty.Length - 1;
                    for (intcnt = 0; intcnt <= loopTo1; intcnt++)
                    {

                        // 個別選挙パラメータのインスタンス化
                        cAAKOBETSUSENKYOParamClass[intcnt] = new localhost.AAKOBETSUSENKYOParamClass();

                        // 更新・追加した項目を取得
                        cAAKOBETSUSENKYOParamClass[intcnt].m_strJuminCD = (string)cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cAAKOBETSUSENKYOParamClass[intcnt].m_strSenkyoShikakuKB = (string)cABKobetsuProperty[intcnt].p_strSenkyoShikakuKB;
                        cAAKOBETSUSENKYOParamClass[intcnt].m_strTohyokuCD = (string)cABKobetsuProperty[intcnt].p_strTohyokuCD;

                    }

                    // 住基個別選挙更新メソッドを実行する
                    strControlData = UFControlToolClass.ControlGetStr(m_cfControlData);
                    intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass);

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
            }
            // 'WebserviceのURLをWebConfigから取得して設定する
            // cAACommonBSClass = New localhost.AACommonBSClass
            // cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
            // 'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            // ReDim cAAKOBETSUSENKYOParamClass(cABKobetsuProperty.Length - 1)

            // For intcnt = 0 To cABKobetsuProperty.Length - 1

            // '個別選挙パラメータのインスタンス化
            // cAAKOBETSUSENKYOParamClass(intcnt) = New localhost.AAKOBETSUSENKYOParamClass

            // '更新・追加した項目を取得
            // cAAKOBETSUSENKYOParamClass(intcnt).m_strJuminCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
            // cAAKOBETSUSENKYOParamClass(intcnt).m_strSenkyoShikakuKB = CStr(cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB)
            // cAAKOBETSUSENKYOParamClass(intcnt).m_strTohyokuCD = CStr(cABKobetsuProperty(intcnt).p_strTohyokuCD)

            // Next

            // ' 住基個別選挙更新メソッドを実行する
            // strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            // intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass)

            // '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
            // If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            // cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            // 'エラー定義を取得
            // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            // '例外を生成
            // csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            // Throw csAppExp

            // End If
            // *履歴番号 000004 2010/02/09 修正終了

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