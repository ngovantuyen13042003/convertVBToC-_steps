// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名印鑑マスタ更新(ABAtenaInkanupBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/11/11　吉澤　行宣
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/03/26 000001     ビジネスIDの変更修正 
// * 2007/03/16 000002     エラーを取得する個所の変更とABLOGへ書き込む処理の追加(高原)
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{


    public class ABAtenaInkanupBClass
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
        private const string THIS_CLASS_NAME = "ABAtenaInkanupBClass";
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
        public ABAtenaInkanupBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            UFConfigDataClass cfAAUFConfigData;
            UFConfigClass cfAAUFConfigClass;

            // コンフィグデータの"AA"の環境情報を取得
            cfAAUFConfigClass = new UFConfigClass();
            cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID);
            m_cfAAConfigDataClass = cfAAUFConfigData;

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
        // * メソッド名     宛名印鑑マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
        // * 
        // * 機能　　    　  宛名印鑑マスタのデータを更新する。
        // * 
        // * 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaInkan(ABKobetsuInkanProperty[] cABKobetsuProperty)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaInkan";
            int intUpdCnt;
            ABAtenaInkanBClass cABAtenaInkanBClass;
            localhost.AAKOBETSUINKANParamClass[] cAAKOBETSUINKANParamClass;
            localhost.AACommonBSClass cAACommonBSClass;
            DataSet csABAtenaInkanEntity;
            DataRow cDatRow;
            string strControlData;
            var cUSSCItyInfo = new USSCityInfoClass();
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFAppException csAppExp;
            var intcnt = default(int);

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

                // 宛名印鑑ＤＡクラスのインスタンス化
                cABAtenaInkanBClass = new ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass);

                try
                {
                    var loopTo = cABKobetsuProperty.Length - 1;
                    for (intcnt = 0; intcnt <= loopTo; intcnt++)
                    {

                        // 宛名印鑑マスタ抽出呼び出し
                        csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(Convert.ToString(cABKobetsuProperty[intcnt]).p_strJUMINCD);

                        // 追加・更新の判定
                        if (csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows.Count == 0)
                        {

                            cDatRow = csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].NewRow();
                            // 各項目をプロパティから取得
                            cDatRow[ABAtenaInkanEntity.JUMINCD] = cABKobetsuProperty[intcnt].p_strJUMINCD;
                            cDatRow[ABAtenaInkanEntity.INKANNO] = cABKobetsuProperty[intcnt].p_strINKANNO;
                            cDatRow[ABAtenaInkanEntity.INKANTOROKUKB] = cABKobetsuProperty[intcnt].p_strINKANTOROKUKB;

                            // 市町村コード
                            cDatRow[ABAtenaInkanEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                            // 旧市町村コード
                            cDatRow[ABAtenaInkanEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                            // データの追加
                            // csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows.Add(cDatRow)

                            // 宛名印鑑マスタ追加メソッド呼び出し
                            intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow);
                        }
                        else
                        {

                            cDatRow = csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows[0];
                            // 各項目をプロパティから取得
                            cDatRow[ABAtenaInkanEntity.JUMINCD] = cABKobetsuProperty[intcnt].p_strJUMINCD;
                            cDatRow[ABAtenaInkanEntity.INKANNO] = cABKobetsuProperty[intcnt].p_strINKANNO;
                            cDatRow[ABAtenaInkanEntity.INKANTOROKUKB] = cABKobetsuProperty[intcnt].p_strINKANTOROKUKB;

                            // 市町村コード
                            cDatRow[ABAtenaInkanEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];
                            // 旧市町村コード
                            cDatRow[ABAtenaInkanEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0];

                            // 宛名印鑑マスタ更新メソッド呼び出し
                            intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow);
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
                }

                // *履歴番号 000002 2007/03/16 追加開始
                catch (UFAppException exAppExp)                   // UFAppExceptionをキャッチ
                {
                    // ※通常のエラーをログファイルに書き込み
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + exAppExp.Message + "】");




                    // ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004);
                    // ※ABLOGへ書き込み
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[intcnt].p_strJUMINCD, objErrorStruct.m_strErrorMessage);

                    throw exAppExp;
                }
                catch (Exception exExp)                           // Exceptionをキャッチ
                {
                    // ※通常のエラーをログファイルに書き込み
                    // エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exExp.Message + "】");


                    // ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004);
                    // ※ABLOGへ書き込み
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[intcnt].p_strJUMINCD, objErrorStruct.m_strErrorMessage);

                    throw exExp;
                }
                // *履歴番号 000002 2007/03/16 追加終了


                try
                {
                    // WebserviceのURLをWebConfigから取得して設定する
                    cAACommonBSClass = new localhost.AACommonBSClass();
                    cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx";
                    // cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                    cAAKOBETSUINKANParamClass = new localhost.AAKOBETSUINKANParamClass[cABKobetsuProperty.Length];

                    var loopTo1 = cABKobetsuProperty.Length - 1;
                    for (intcnt = 0; intcnt <= loopTo1; intcnt++)
                    {

                        // 個別印鑑パラメータのインスタンス化
                        cAAKOBETSUINKANParamClass[intcnt] = new localhost.AAKOBETSUINKANParamClass();

                        // 更新・追加した項目を取得
                        cAAKOBETSUINKANParamClass[intcnt].m_strJUMINCD = Convert.ToString(cABKobetsuProperty[intcnt]).p_strJUMINCD;
                        cAAKOBETSUINKANParamClass[intcnt].m_strINKANNO = Convert.ToString(cABKobetsuProperty[intcnt]).p_strINKANNO;
                        cAAKOBETSUINKANParamClass[intcnt].m_strINKANTOROKUKB = Convert.ToString(cABKobetsuProperty[intcnt]).p_strINKANTOROKUKB;
                    }

                    // 住基個別印鑑更新メソッドを実行する
                    strControlData = UFControlToolClass.ControlGetStr(m_cfControlData);
                    intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass);

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
                        // *履歴番号 000002 2007/03/16 追加開始
                        // ※ログファイル書き込み後、連携エラー用メッセージを作成
                        cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                        // エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002);
                        // ※ABLOGへ書き込み
                        // ※　注　※　引数で住民コードを渡す個所ですが、cABKobetsuPropertyが複数であっても
                        // ＡＡから戻ってきたエラーでは何番目で落ちたか判断できないので、以下固定でIndex(0)を渡します。
                        SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[0].p_strJUMINCD, objErrorStruct.m_strErrorMessage);
                        // *履歴番号 000002 2007/03/16 追加終了

                        throw objAppExp;
                    }
                    else
                    {
                        // システム例外の場合
                        // エラーログ出力
                        m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExpTool.p_strErrorMessage + "】");



                        // *履歴番号 000002 2007/03/16 追加開始
                        // ※ログファイル書き込み後、連携エラー用メッセージを作成
                        cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                        // エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002);
                        // ※ABLOGへ書き込み
                        SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[0].p_strJUMINCD, objErrorStruct.m_strErrorMessage);
                        // *履歴番号 000002 2007/03/16 追加終了
                        throw objSoapExp;
                    }
                }
                catch (UFAppException exAppExp)                   // UFAppExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + exAppExp.Message + "】");




                    // *履歴番号 000002 2007/03/16 追加開始
                    // ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002);
                    // ※ABLOGへ書き込み
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[0].p_strJUMINCD, objErrorStruct.m_strErrorMessage);
                    // *履歴番号 000002 2007/03/16 追加終了

                    throw exAppExp;
                }
                catch (Exception exExp)                           // Exceptionをキャッチ
                {
                    // エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exExp.Message + "】");



                    // *履歴番号 000002 2007/03/16 追加開始
                    // ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002);
                    // ※ABLOGへ書き込み
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD[0], "ABKOB", "AC", "個別記載更新（印鑑）", cABKobetsuProperty[0].p_strJUMINCD, objErrorStruct.m_strErrorMessage);
                    // *履歴番号 000002 2007/03/16 追加終了

                    throw exExp;
                }
            }
            catch
            {
                throw;
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

        // *履歴番号 000002 2007/03/16 追加開始
        #region 宛名更新エラーログSET
        // ************************************************************************************************
        // * メソッド名     宛名更新エラーログSET処理
        // * 
        // * 構文           SetABLOG(ByVal strShichosonCD As String, _
        // * 　　                    ByVal strShoriID As String, _
        // * 　　                    ByVal strShoriShu As String, _
        // * 　　                    ByVal strBasho As String, _
        // * 　　                    ByVal strJuminCD As String, _
        // * 　　                    ByVal strErrMsg As String)
        // * 
        // * 機能           ABLOG用エラーメッセージをSETする
        // * 
        // * 引数           ByVal strShichosonCD As String : 市町村コード
        // * 　　           ByVal strShoriID as string     : 処理ＩＤ
        // * 　　           ByVal strShoriShu As String    : 処理種別
        // * 　　           ByVal strBasho As String       : エラー発生場所
        // * 　　           ByVal strJuminCD As String     : 該当住民コード
        // * 　　           ByVal strErrMsg As String      : エラーメッセージ
        // * 
        // * 戻り値         Dim intCnt As Integer          : エラー追加件数
        // ************************************************************************************************
        private int SetABLOG(string strShichosonCD, string strShoriID, string strShoriShu, string strBasho, string strJuminCD, string strErrMsg)




        {
            ABErrLogBClass cABErrLog;
            ABErrLogXClass cABErrLogPrm;
            int intCnt;

            cABErrLog = new ABErrLogBClass(m_cfControlData, m_cfABConfigDataClass);
            cABErrLogPrm = new ABErrLogXClass();

            // 各種項目をパラメータにセット
            cABErrLogPrm.p_strShichosonCD = strShichosonCD;
            cABErrLogPrm.p_strShoriID = strShoriID;
            cABErrLogPrm.p_strShoriShu = strShoriShu;
            cABErrLogPrm.p_strMsg5 = strBasho;
            cABErrLogPrm.p_strMsg6 = strJuminCD;
            cABErrLogPrm.p_strMsg7 = strErrMsg;

            intCnt = cABErrLog.InsertABErrLog(cABErrLogPrm);

            return intCnt;

        }

        #endregion
        // *履歴番号 000002 2007/03/16 追加終了

        // *履歴番号 000002 2007/03/16 削除開始
        // ※Try-Catchの作りを大幅に変えるので旧ソースをそのまま残しておきます。
        #region 旧ソース UpdateAtenaInkan
        // '************************************************************************************************
        // '* メソッド名     宛名印鑑マスタ更新
        // '* 
        // '* 構文           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
        // '* 
        // '* 機能　　    　  宛名印鑑マスタのデータを更新する。
        // '* 
        // '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
        // '* 
        // '* 戻り値         更新件数(Integer)
        // '************************************************************************************************
        // Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty() As ABKobetsuInkanProperty) As Integer
        // Const THIS_METHOD_NAME As String = "UpdateAtenaInkan"
        // Dim intUpdCnt As Integer
        // Dim cABAtenaInkanBClass As ABAtenaInkanBClass
        // Dim cAAKOBETSUINKANParamClass() As localhost.AAKOBETSUINKANParamClass
        // Dim cAACommonBSClass As localhost.AACommonBSClass
        // Dim csABAtenaInkanEntity As DataSet
        // Dim cDatRow As DataRow
        // Dim strControlData As String
        // Dim cUSSCItyInfo As New USSCityInfoClass()
        // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        // Dim csAppExp As UFAppException
        // Dim intcnt As Integer

        // Try

        // '*履歴番号 000001 2004/03/26 追加開始
        // '業務IDを宛名(AB)に変更
        // m_cfControlData.m_strBusinessId = "AB"
        // '*履歴番号 000001 2004/03/26 追加終了

        // ' デバッグログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '市町村情報取得（市町村コード)
        // cUSSCItyInfo.GetCityInfo(m_cfControlData)

        // '宛名印鑑ＤＡクラスのインスタンス化
        // cABAtenaInkanBClass = New ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

        // For intcnt = 0 To cABKobetsuProperty.Length - 1

        // '宛名印鑑マスタ抽出呼び出し
        // csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

        // '追加・更新の判定
        // If csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows.Count = 0 Then

        // cDatRow = csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].NewRow()
        // '各項目をプロパティから取得
        // cDatRow[ABAtenaInkanEntity.JUMINCD] = cABKobetsuProperty(intcnt).p_strJUMINCD
        // cDatRow[ABAtenaInkanEntity.INKANNO] = cABKobetsuProperty(intcnt).p_strINKANNO
        // cDatRow[ABAtenaInkanEntity.INKANTOROKUKB] = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

        // '市町村コード
        // cDatRow[ABAtenaInkanEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0]
        // '旧市町村コード
        // cDatRow[ABAtenaInkanEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0]

        // 'データの追加
        // 'csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows.Add(cDatRow)

        // '宛名印鑑マスタ追加メソッド呼び出し
        // intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow)
        // Else

        // cDatRow = csABAtenaInkanEntity.Tables[ABAtenaInkanEntity.TABLE_NAME].Rows[0]
        // '各項目をプロパティから取得
        // cDatRow[ABAtenaInkanEntity.JUMINCD] = cABKobetsuProperty(intcnt).p_strJUMINCD
        // cDatRow[ABAtenaInkanEntity.INKANNO] = cABKobetsuProperty(intcnt).p_strINKANNO
        // cDatRow[ABAtenaInkanEntity.INKANTOROKUKB] = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

        // '市町村コード
        // cDatRow[ABAtenaInkanEntity.SHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0]
        // '旧市町村コード
        // cDatRow[ABAtenaInkanEntity.KYUSHICHOSONCD] = cUSSCItyInfo.p_strShichosonCD[0]

        // '宛名印鑑マスタ更新メソッド呼び出し
        // intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow)
        // End If

        // '追加・更新件数が0件の時メッセージ"宛名の個別事項の更新は正常に行えませんでした"を返す
        // If intUpdCnt = 0 Then

        // cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
        // 'エラー定義を取得
        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
        // '例外を生成
        // csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
        // Throw csAppExp
        // End If

        // Next

        // 'WebserviceのURLをWebConfigから取得して設定する
        // cAACommonBSClass = New localhost.AACommonBSClass()
        // cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
        // 'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

        // ReDim cAAKOBETSUINKANParamClass(cABKobetsuProperty.Length - 1)

        // For intcnt = 0 To cABKobetsuProperty.Length - 1

        // '個別印鑑パラメータのインスタンス化
        // cAAKOBETSUINKANParamClass(intcnt) = New localhost.AAKOBETSUINKANParamClass()

        // '更新・追加した項目を取得
        // cAAKOBETSUINKANParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
        // cAAKOBETSUINKANParamClass(intcnt).m_strINKANNO = CStr(cABKobetsuProperty(intcnt).p_strINKANNO)
        // cAAKOBETSUINKANParamClass(intcnt).m_strINKANTOROKUKB = CStr(cABKobetsuProperty(intcnt).p_strINKANTOROKUKB)
        // Next

        // ' 住基個別印鑑更新メソッドを実行する
        // strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
        // intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass)

        // '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
        // If Not (intUpdCnt = cABKobetsuProperty.Length) Then

        // cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
        // 'エラー定義を取得
        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
        // '例外を生成
        // csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
        // Throw csAppExp

        // End If

        // Catch objSoapExp As Web.Services.Protocols.SoapException             ' SoapExceptionをキャッチ
        // ' OuterXmlにエラー内容が格納してある。
        // Dim objExpTool As UFExceptionTool = New UFExceptionTool(objSoapExp.Detail.OuterXml)
        // Dim objErr As UFErrorStruct

        // ' アプリケーション例外かどうかの判定
        // If (objExpTool.IsAppException = True) Then
        // ' ワーニングログ出力
        // m_cfLogClass.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + objExpTool.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + objExpTool.p_strErrorMessage + "】")

        // ' 付加メッセージを作成する
        // Dim strExtMsg As String = "<P>対象住民のリカバリ処理を行ってください。<BR>"

        // ' アプリケーション例外を作成する
        // Dim objAppExp As UFAppException
        // objAppExp = New UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode)

        // ' 拡張領域のメッセージにも付加（実際にはここのメッセージが表示される）
        // UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt)
        // objErr.m_strErrorMessage += strExtMsg
        // objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr)
        // ' メッセージを付加しない場合は以下
        // 'objAppExp.p_strExt = objExpTool.p_strExt

        // Throw objAppExp
        // Else
        // ' システム例外の場合
        // ' エラーログ出力
        // m_cfLogClass.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + objExpTool.p_strErrorMessage + "】")
        // Throw objSoapExp
        // End If
        // Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
        // ' ワーニングログ出力
        // m_cfLogClass.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppExp.Message + "】")
        // Throw exAppExp
        // Catch exExp As Exception                           ' Exceptionをキャッチ
        // ' エラーログ出力
        // m_cfLogClass.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exExp.Message + "】")
        // Throw exExp
        // Finally
        // '元のビジネスIDを入れる
        // m_cfControlData.m_strBusinessId = m_strRsBusiId
        // ' デバッグログ出力
        // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // End Try

        // Return intUpdCnt

        // End Function
        #endregion
        // *履歴番号 000002 2007/03/16 削除終了
    }
}
