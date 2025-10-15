// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名介護マスタ更新(ABAtenaNenkinupBClas)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/11/12　吉澤　行宣
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/03/26 000001     ビジネスIDの変更修正
// * 2005/12/01 000002     住基の個別事項更新結果を評価するかしないかの処理を追加
// * 2008/05/13 000003     ホスト連携処理を起動するワークフロー起動処理を追加（比嘉）
// * 2008/09/30 000004     住基の個別事項マスタ更新の制御機能を追加（吉澤）
// * 2022/12/16 000005    【AB-8010】住民コード世帯コード15桁対応(下村)
// * 2024/02/19 000006    【AB-9001_1】個別記載事項対応(下村)
// ************************************************************************************************
using System;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAtenaKaigoupBClass
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
        private const string THIS_CLASS_NAME = "ABAtenaKaigoupBClass";
        private const string AA_BUSSINESS_ID = "AA";                            // 業務コード
                                                                                // *履歴番号 000003 2008/05/13 追加開始
        private const string WORK_FLOW_NAME = "宛名介護個別事項";         // ワークフロー名
        private const string DATA_NAME = "介護個別";                      // データ名
                                                                      // *履歴番号 000003 2008/05/13 追加終了
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
        public ABAtenaKaigoupBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
        // * メソッド名     宛名介護マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaKaigo(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty) As Integer
        // * 
        // * 機能　　    　  宛名介護マスタのデータを更新する。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaKaigo(ABKobetsuKaigoProperty[] cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaKaigo";
            var intUpdCnt = default(int);
            ABAtenaKaigoBClass cABAtenaKaigoBClass;
            localhost.AAKOBETSUKAIGOParamClass[] cAAKOBETSUKAIGOParamClass;
            localhost.AACommonBSClass cAACommonBSClass;
            DataSet csABAtenaKaigoEntity;
            DataRow cDatRow;
            string strControlData;
            var cUSSCItyInfo = new USSCityInfoClass();
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFAppException csAppExp;
            int intcnt;
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

                // 宛名介護ＤＡクラスのインスタンス化
                cABAtenaKaigoBClass = new ABAtenaKaigoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);

                var loopTo = cABKobetsuProperty.Length - 1;
                for (intcnt = 0; intcnt <= loopTo; intcnt++)
                {

                    // 宛名介護マスタ抽出呼び出し
                    csABAtenaKaigoEntity = cABAtenaKaigoBClass.GetAtenaKaigo(cABKobetsuProperty[intcnt].p_strJUMINCD);

                    // 追加・更新の判定
                    if (csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Count == 0)
                    {

                        cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).NewRow();
                        // 各項目をプロパティから取得
                        cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow.Item(ABAtenaKaigoEntity.HIHOKENSHAGAITOKB) = string.Empty;
                        cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty[intcnt].p_strHIHKNSHANO;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty[intcnt].p_strSKAKSHUTKYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty[intcnt].p_strSKAKSSHTSYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty[intcnt].p_strSKAKHIHOKENSHAKB;
                        cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty[intcnt].p_strJUSHOCHITKRIKB;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty[intcnt].p_strJUKYUSHAKB;
                        cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty[intcnt].p_strYOKAIGJOTAIKBCD;
                        cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty[intcnt].p_strKAIGSKAKKB;
                        cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty[intcnt].p_strNINTEIKAISHIYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty[intcnt].p_strNINTEISHURYOYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty[intcnt].p_strJUKYUNINTEIYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty[intcnt].p_strJUKYUNINTEITORIKESHIYMD;

                        // 市町村コード
                        cDatRow.Item(ABAtenaKaigoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);
                        // 旧市町村コード
                        cDatRow.Item(ABAtenaKaigoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);

                        // データの追加
                        // csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Add(cDatRow)

                        // 宛名介護マスタ追加メソッド呼び出し
                        intUpdCnt = cABAtenaKaigoBClass.InsertAtenaKaigo(cDatRow);
                    }
                    else
                    {

                        cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows(0);
                        // 各項目をプロパティから取得
                        cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty[intcnt].p_strHIHKNSHANO;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty[intcnt].p_strSKAKSHUTKYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty[intcnt].p_strSKAKSSHTSYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty[intcnt].p_strSKAKHIHOKENSHAKB;
                        cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty[intcnt].p_strJUSHOCHITKRIKB;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty[intcnt].p_strJUKYUSHAKB;
                        cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty[intcnt].p_strYOKAIGJOTAIKBCD;
                        cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty[intcnt].p_strKAIGSKAKKB;
                        cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty[intcnt].p_strNINTEIKAISHIYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty[intcnt].p_strNINTEISHURYOYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty[intcnt].p_strJUKYUNINTEIYMD;
                        cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty[intcnt].p_strJUKYUNINTEITORIKESHIYMD;
                        // 市町村コード
                        cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);
                        // 旧市町村コード
                        cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0);

                        // 宛名介護マスタ更新メソッド呼び出し
                        intUpdCnt = cABAtenaKaigoBClass.UpdateAtenaKaigo(cDatRow);
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

                // *履歴番号 000004 2008/09/30 修正開始
                // 宛名管理情報Ｂクラスのインスタンス作成
                cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // 住基個別事項マスタ更新制御情報の取得
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "17");

                // 管理情報のレコード存在し、パラメータが "1" の場合のみ更新を行なわない。
                if (!(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count == 0) && (string)csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER) == "1")
                {
                }
                // 住基個別事項マスタの更新は行わない。
                else
                {

                    // *履歴番号 000002 2005/12/01 追加開始
                    // *履歴番号 000004 2008/09/30 削除開始
                    // ' 宛名管理情報Ｂクラスのインスタンス作成
                    // cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
                    // *履歴番号 000004 2008/09/30 削除終了

                    // 宛名管理情報の種別04識別キー25のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                    csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "25");
                    // 管理情報にレコードが存在し、パラメータが"1"の時はチェックしない
                    if (!(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count == 0))
                    {
                        if ((string)csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER) == "1")
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
                    cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx";
                    // cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                    cAAKOBETSUKAIGOParamClass = new localhost.AAKOBETSUKAIGOParamClass[cABKobetsuProperty.Length];

                    var loopTo1 = cABKobetsuProperty.Length - 1;
                    for (intcnt = 0; intcnt <= loopTo1; intcnt++)
                    {

                        // 個別介護パラメータのインスタンス化
                        cAAKOBETSUKAIGOParamClass[intcnt] = new localhost.AAKOBETSUKAIGOParamClass();

                        // 更新・追加した項目を取得
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strJUMINCD = (string)cABKobetsuProperty[intcnt].p_strJUMINCD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strHIHKNSHANO = (string)cABKobetsuProperty[intcnt].p_strHIHKNSHANO;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strSKAKSHUTKYMD = (string)cABKobetsuProperty[intcnt].p_strSKAKSHUTKYMD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strSKAKSSHTSYMD = (string)cABKobetsuProperty[intcnt].p_strSKAKSSHTSYMD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strSKAKHIHOKENSHAKB = (string)cABKobetsuProperty[intcnt].p_strSKAKHIHOKENSHAKB;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strJUSHOCHITKRIKB = (string)cABKobetsuProperty[intcnt].p_strJUSHOCHITKRIKB;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strJUKYUSHAKB = (string)cABKobetsuProperty[intcnt].p_strJUKYUSHAKB;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strYOKAIGJOTAIKBCD = (string)cABKobetsuProperty[intcnt].p_strYOKAIGJOTAIKBCD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strKAIGSKAKKB = (string)cABKobetsuProperty[intcnt].p_strKAIGSKAKKB;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strNINTEIKAISHIYMD = (string)cABKobetsuProperty[intcnt].p_strNINTEIKAISHIYMD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strNINTEISHURYOYMD = (string)cABKobetsuProperty[intcnt].p_strNINTEISHURYOYMD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strJUKYUNINTEIYMD = (string)cABKobetsuProperty[intcnt].p_strJUKYUNINTEIYMD;
                        cAAKOBETSUKAIGOParamClass[intcnt].m_strJUKYUNINTEITORIKESHIYMD = (string)cABKobetsuProperty[intcnt].p_strJUKYUNINTEITORIKESHIYMD;
                    }

                    // 住基個別介護更新メソッドを実行する
                    strControlData = UFControlToolClass.ControlGetStr(m_cfControlData);
                    intUpdCnt = cAACommonBSClass.UpdateKBKAIGO(strControlData, cAAKOBETSUKAIGOParamClass);

                    // *履歴番号 000002 2005/12/01 修正開始
                    // ''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                    // '''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                    // '''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    // '''    'エラー定義を取得
                    // '''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    // '''    '例外を生成
                    // '''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // '''    Throw csAppExp

                    // '''End If
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
                    // *履歴番号 000002 2005/12/01 修正終了

                }
                // *履歴番号 000004 2008/09/30 修正終了



                // *履歴番号 000003 2008/05/13 追加開始
                // 宛名管理情報の種別04識別キー26のデータを取得する(上田市ﾎｽﾄとの連携をするかどうかの判定)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "26");

                // 管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
                if (!(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count == 0))
                {
                    if ((string)csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER) == "1")
                    {
                        // ワークフロー処理メソッドを呼ぶ
                        WorkFlowSet(cABKobetsuProperty);
                    }
                }
            }
            // *履歴番号 000003 2008/05/13 追加終了

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

        // *履歴番号 000003 2008/05/13 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名介護ワークフロー
        // * 
        // * 構文           Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty)
        // * 
        // * 機能　　    　 宛名介護データをワークフローへ渡す。
        // * 
        // * 引数           ByVal cDatRow As DataRow  :更新データ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void WorkFlowSet(ABKobetsuKaigoProperty[] cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "WorkFlowSet";
            var csABKaigoEntity = new DataSet();                  // 個別事項介護データセット
            DataTable csABKaigoTable;                     // 個別事項介護データテーブル
            DataRow csABKaigoRow;                         // 個別事項介護データロウ
            string strNen;                                // 作成日時
            int intRecCnt;                            // 連番用カウンター
            var cuCityInfoClass = new USSCityInfoClass();         // 市町村管理情報クラス
            string strCityCD;                             // 市町村コード
            ABAtenaCnvBClass cABAtenaCnvBClass;
            int intIdx;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 市町村管理情報の取得
                cuCityInfoClass.GetCityInfo(m_cfControlData);
                // 市町村コードの取得
                strCityCD = cuCityInfoClass.p_strShichosonCD(0);
                // 作成日時(14桁)の取得
                strNen = DateTime.Now.ToString("yyyyMMddHHmmss");
                // 連番用カウンターの初期設定
                intRecCnt = 1;

                // テーブルセットの取得
                csABKaigoTable = CreateColumnsData();
                csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME;
                // データセットにテーブルセットの追加
                csABKaigoEntity.Tables.Add(csABKaigoTable);

                // *****
                // *　１行目～の編集
                // *
                // *****
                var loopTo = cABKobetsuProperty.Length - 1;
                for (intIdx = 0; intIdx <= loopTo; intIdx++)
                {
                    // 新規レコードの作成
                    csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow;
                    // 各項目にデータをセット
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65";
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = "";
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = intRecCnt.ToString().RPadLeft(7, '0');
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = cABKobetsuProperty[intIdx].p_strJUMINCD.RSubstring(3, 12);
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = strCityCD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = string.Empty;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = cABKobetsuProperty[intIdx].p_strHIHKNSHANO;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty[intIdx].p_strSKAKSHUTKYMD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty[intIdx].p_strSKAKSSHTSYMD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty[intIdx].p_strSKAKHIHOKENSHAKB;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty[intIdx].p_strJUSHOCHITKRIKB;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty[intIdx].p_strJUKYUSHAKB;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty[intIdx].p_strYOKAIGJOTAIKBCD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty[intIdx].p_strKAIGSKAKKB;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty[intIdx].p_strNINTEIKAISHIYMD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty[intIdx].p_strNINTEISHURYOYMD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty[intIdx].p_strJUKYUNINTEIYMD;
                    csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty[intIdx].p_strJUKYUNINTEITORIKESHIYMD;

                    // データセットにレコードを追加
                    csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow);
                    // 連番用カウントアップ
                    intRecCnt += 1;
                }

                // *****
                // *　最終行の編集
                // *
                // *****
                // 新規レコードの作成
                csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow;
                // 各項目にデータをセット
                csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65";
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = "E";
                csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = intRecCnt.ToString().RPadLeft(7, '0');
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = string.Empty;
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = string.Empty;
                // データセットにレコードを追加
                csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow);

                // *****
                // *　ワークフロー送信
                // *
                // *****
                // データセット取得クラスのインスタンス化
                cABAtenaCnvBClass = new ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);
                // ワークフロー送信処理呼び出し
                cABAtenaCnvBClass.WorkFlowExec(csABKaigoEntity, WORK_FLOW_NAME, DATA_NAME);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppExp)                   // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + exAppExp.Message + "】");



                throw;
            }
            catch (Exception exExp)                           // Exceptionをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exExp.Message + "】");


                throw;

            }

        }

        // ************************************************************************************************
        // * メソッド名      データカラム作成
        // * 
        // * 構文            Private Function CreateColumnsData() As DataTable
        // * 
        // * 機能　　        レプリカＤＢのカラム定義を作成する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          DataTable() 代納情報テーブル
        // ************************************************************************************************
        private DataTable CreateColumnsData()
        {
            const string THIS_METHOD_NAME = "CreateColumnsData";
            DataTable csABKaigoTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 個別介護情報カラム定義
                csABKaigoTable = new DataTable();
                csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME;
                // 市町村コード
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.CITYCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                // 識別ID
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHIKIBETSUID, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                // 処理日時
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SAKUSEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 14;
                // 最終行区分
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.LASTRECKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                // 連番
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.RENBAN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                // 住民コード
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                // 市町村コード
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                // 旧市町村コード
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KYUSHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                // 被保険者番号
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.HIHKNSHANO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                // 資格取得日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSHUTKYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // 資格喪失日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSSHTSYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // 資格被保険者区分
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                // 住所地特例者区分
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUSHOCHITKRIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                // 受給者区分
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUSHAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                // 要介護状態区分コード
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                // 要介護状態区分
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KAIGSKAKKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                // 認定有効開始年月日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEIKAISHIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // 認定有効終了年月日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEISHURYOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // 受給認定年月日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // 受給認定取消年月日
                csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw;
            }

            return csABKaigoTable;

        }
        // *履歴番号 000003 2008/05/13 追加終了

    }
}