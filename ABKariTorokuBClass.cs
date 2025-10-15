// ************************************************************************************************
// * 業務名　　　　   宛名管理システム
// * 
// * クラス名　　　   仮登録Bクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 作成日付　　     2024/01/10
// *
// * 作成者　　　　   掛川　翔太
// * 
// * 著作権　　　　   （株）電算
// * 
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2024/01/10             【AB-0120-1】住民データ異動中の排他制御(掛川)
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;

#region 参照名前空間

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{
    #endregion

    public class ABKariTorokuBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                        // コントロールデータ
        private UFConfigDataClass m_cfConfigData;                     // コンフィグデータ
        private UFRdbClass m_cfRdb;                                   // ＲＤＢクラス
        private UFErrorClass m_cfError;                               // エラー処理クラス
        private ABLogXClass m_cABLogX;                                // ABログ出力Xクラス
        private string m_strKTorokuKBN;                               // 仮登録中区分
        private ABKojinSeigyoBClass m_cABKojinSeigyo;                 // 個人制御情報DA
        private ABKojinseigyoRirekiBClass m_cABKojinSeigyoRireki;     // 個人制御情報履歴DA
        private string m_strShichosonCD;                              // 市町村コード
        private string m_strKTorokuMsg;                               // 仮登録中メッセージ
        private string m_strMsg;                                      // メッセージ
        private string m_strSystemYMD;                                // システム日付

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKariTorokuBClass";
        private const string ERR_MSG_SHORIKBN = "処理区分";               // エラーメッセージ_処理区分
        private const string ERR_MSG_JUMINCD = "住民コード";              // エラーメッセージ_住民コード
        private const string ERR_MSG_KOJINSEIGYO = "個人制御情報";        // エラーメッセージ_個人制御情報
        private const string ERR_MSG_KOJINSEIGYORIREKI = "個人制御情報";  // エラーメッセージ_個人制御情報
        private const string KTOROKU_MSG_TOROKUCHU = "仮登録中です。";    // メッセージ_仮登録中
        private const string KTOROKU_MSG_KOSHIN = "入力・更新中です。";   // メッセージ_更新中
        private const string SHUBETSU_KEY_20 = "20";                      // 種別キー
        private const string SHIKIBETSU_KEY_85 = "85";                    // 識別キー
        private const string ALL9_YMD = "99999999";                       // 年月日オール９
        #endregion

        #region メソッド

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdb As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdb as UFRdb                          : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABKariTorokuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)

        {

            // 変数の初期化
            m_cfControlData = new UFControlData();
            m_cfConfigData = new UFConfigDataClass();
            m_cfRdb = new UFRdbClass(ABConstClass.THIS_BUSINESSID);
            m_cfError = new UFErrorClass();
            m_cABLogX = new ABLogXClass(cfControlData, cfConfigData, THIS_CLASS_NAME);
            m_strKTorokuKBN = string.Empty;
            m_cABKojinSeigyo = new ABKojinSeigyoBClass(cfControlData, cfConfigData, cfRdb);
            m_cABKojinSeigyoRireki = new ABKojinseigyoRirekiBClass(cfControlData, cfConfigData, cfRdb);
            m_strShichosonCD = string.Empty;
            m_strKTorokuMsg = string.Empty;
            m_strMsg = string.Empty;
            m_strSystemYMD = string.Empty;

            // メンバ変数セット
            m_cfConfigData = cfConfigData;
            m_cfControlData = cfControlData;
            m_cfRdb = cfRdb;

        }
        #endregion

        #region 個人制御情報更新
        // ************************************************************************************************
        // * メソッド名     個人制御情報更新
        // * 
        // * 構文           Public Function KojinSeigyoKoshin(ByVal cABKariTorokuPrm As ABKariTorokuParamXClass) As Integer
        // * 
        // * 機能　　    　 個人制御情報更新を更新する
        // * 
        // * 引数           cABKariTorokuPrm：仮登録パラメータ
        // * 
        // * 戻り値         更新件数：Integer
        // ************************************************************************************************
        public int KojinSeigyoKoshin(ABKariTorokuParamXClass cABKariTorokuPrm)
        {
            const string THIS_METHOD_NAME = "KojinSeigyoKoshin";          // メソッド名
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            USSCityInfoClass cUssCityInfo;                // 市町村情報
            ABKANRIJOHOCacheBClass cABKanriInfo;          // AB管理情報
            ABCommonClass cCommonClass;
            DataRow csKojinSeigyoRow;
            DataRow csKojinSeigyoRirekiRow;
            DataSet csKTourokuDS;
            DataSet csKojinSeigyoDS;
            DataSet csKojinSeigyoRirekiDS;
            DataRow[] csSortDataRow;
            int intRirekiNo;
            bool blnInsertFlg;
            int intKojinSeigyoCnt;
            int intKojinSeigyoRirekiCnt;

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // パラメータチェック
                // 仮登録パラメータ・処理区分が「1」「2」以外の場合
                if (!(cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI || cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO))
                {

                    // エラー定義を取得
                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_SHORIKBN, objErrorStruct.m_strErrorCode);
                }

                // 仮登録パラメータ・住民コードが空白の場合
                if (cABKariTorokuPrm.p_strJuminCD.Trim() == string.Empty)
                {

                    // エラー定義を取得
                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_JUMINCD, objErrorStruct.m_strErrorCode);
                }

                // メンバ変数.仮登録中区分の設定
                m_strKTorokuKBN = cABKariTorokuPrm.p_strKariTorokuKb.Trim();
                // 仮登録パラメータ・処理区分＝「1」(排他開始)　AND　仮登録パラメータ・仮登録中区分＝空白の場合
                if (cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI && cABKariTorokuPrm.p_strKariTorokuKb.Trim() == string.Empty)
                {
                    m_strKTorokuKBN = ABKariTorokuParamXClass.KTOROKUKBN_KTOROKUCHU;
                }

                // メンバ変数のセット
                // メンバ変数・個人制御情報DAがnothingの場合
                if (m_cABKojinSeigyo is null)
                {
                    m_cABKojinSeigyo = new ABKojinSeigyoBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                }

                // メンバ変数・個人制御情報履歴DAがnothingの場合
                if (m_cABKojinSeigyoRireki is null)
                {
                    m_cABKojinSeigyoRireki = new ABKojinseigyoRirekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                }

                // メンバ変数・市町村コード＝空白の場合
                if (string.IsNullOrEmpty(m_strShichosonCD.Trim()))
                {
                    cUssCityInfo = new USSCityInfoClass();
                    cUssCityInfo.GetCityInfo(m_cfControlData);
                    m_strShichosonCD = cUssCityInfo.p_strShichosonCD[0];
                }

                // メンバ変数・仮登録中メッセージ＝空白の場合
                if (string.IsNullOrEmpty(m_strKTorokuMsg.Trim()))
                {
                    cABKanriInfo = new ABKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                    csKTourokuDS = cABKanriInfo.GetKanriJohoHoshu(SHUBETSU_KEY_20, SHIKIBETSU_KEY_85);

                    // 取得できた場合
                    if (csKTourokuDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                    {
                        m_strKTorokuMsg = (string)csKTourokuDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                    }
                }

                // メンバ変数・仮登録中区分＝「1」(仮登録中)の場合
                if (m_strKTorokuKBN.Trim() == ABKariTorokuParamXClass.KTOROKUKBN_KTOROKUCHU)
                {
                    // メンバ変数・仮登録中メッセージ≠空白の場合
                    if (!string.IsNullOrEmpty(m_strKTorokuMsg.Trim()))
                    {
                        m_strMsg = m_strKTorokuMsg;
                    }
                    else
                    {
                        m_strMsg = KTOROKU_MSG_TOROKUCHU;
                    }
                }
                else
                {
                    m_strMsg = KTOROKU_MSG_KOSHIN;
                }

                // 個人制御情報を取得する。
                csKojinSeigyoDS = m_cABKojinSeigyo.GetABKojinSeigyo(cABKariTorokuPrm.p_strJuminCD);
                // 個人制御情報が取得できなかった場合
                if (csKojinSeigyoDS.Tables[ABKojinseigyomstEntity.TABLE_NAME].Rows.Count == 0)
                {

                    blnInsertFlg = true;

                    // 仮登録パラメータ・処理区分＝2(排他解除)の場合
                    if (cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO)
                    {
                        cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001039);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                    }
                }
                else
                {
                    blnInsertFlg = false;
                }

                // システム日付(YYYYMMDD)を取得し、メンバ変数にセットする。
                m_strSystemYMD = m_cfRdb.GetSystemDate().ToString("yyyyMMdd");


                // 個人制御情報を編集する。
                cCommonClass = new ABCommonClass();
                if (blnInsertFlg == true)
                {
                    csKojinSeigyoRow = csKojinSeigyoDS.Tables[ABKojinseigyomstEntity.TABLE_NAME].NewRow();
                    csKojinSeigyoRow.BeginEdit();
                    cCommonClass.InitColumnValue(csKojinSeigyoRow);
                    csKojinSeigyoRow = EditKojinSeigyoInfo(csKojinSeigyoRow, cABKariTorokuPrm, blnInsertFlg);
                    csKojinSeigyoRow.EndEdit();
                }
                else
                {
                    csKojinSeigyoRow = csKojinSeigyoDS.Tables[ABKojinseigyomstEntity.TABLE_NAME].Rows[0];
                    csKojinSeigyoRow.BeginEdit();
                    csKojinSeigyoRow = EditKojinSeigyoInfo(csKojinSeigyoRow, cABKariTorokuPrm, blnInsertFlg);
                    csKojinSeigyoRow.EndEdit();
                }

                // 個人制御情報履歴を取得する。
                csKojinSeigyoRirekiDS = m_cABKojinSeigyoRireki.GetKojinseigyoRireki(cABKariTorokuPrm.p_strJuminCD);
                if (csKojinSeigyoRirekiDS.Tables[ABKojinseigyoRirekiEntity.TABLE_NAME].Rows.Count == 0)
                {
                    intRirekiNo = 1;
                }
                else
                {
                    csSortDataRow = csKojinSeigyoRirekiDS.Tables[ABKojinseigyoRirekiEntity.TABLE_NAME].Select(string.Empty, ABKojinseigyoRirekiEntity.RIREKINO + " DESC, " + ABKojinseigyoRirekiEntity.RIREKIEDABAN + " DESC ");


                    intRirekiNo = (int)csSortDataRow[0][ABKojinseigyoRirekiEntity.RIREKINO].ToString() + 1;
                }

                // 個人制御情報履歴を編集する
                csKojinSeigyoRirekiRow = csKojinSeigyoRirekiDS.Tables[ABKojinseigyoRirekiEntity.TABLE_NAME].NewRow();
                csKojinSeigyoRirekiRow.BeginEdit();
                cCommonClass.InitColumnValue(csKojinSeigyoRirekiRow);
                csKojinSeigyoRirekiRow = EditKojinSeigyoRirekiInfo(csKojinSeigyoRirekiRow, csKojinSeigyoRow, intRirekiNo);
                csKojinSeigyoRirekiRow.EndEdit();

                // 更新処理
                if (blnInsertFlg == true)
                {
                    intKojinSeigyoCnt = m_cABKojinSeigyo.InsertKojinSeigyo(csKojinSeigyoRow);
                    if (intKojinSeigyoCnt == 0)
                    {

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode);
                    }
                }
                else
                {
                    intKojinSeigyoCnt = m_cABKojinSeigyo.UpdateKojinSeigyo(csKojinSeigyoRow);
                    if (intKojinSeigyoCnt == 0)
                    {

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001048);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode);
                    }
                }

                // 個人制御情報履歴をInsertする
                intKojinSeigyoRirekiCnt = m_cABKojinSeigyoRireki.InsertKojinseigyoRireki(csKojinSeigyoRirekiRow);
                if (intKojinSeigyoRirekiCnt == 0)
                {

                    // エラー定義を取得
                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYORIREKI, objErrorStruct.m_strErrorCode);
                }

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return intKojinSeigyoCnt;

        }
        #endregion

        #region 個人制御情報編集
        // ************************************************************************************************
        // * メソッド名     個人制御情報編集
        // * 
        // * 構文           Public Function EditKojinSeigyoInfo(ByVal csKojinSeigyoRow As DataRow,
        // *                                                    ByVal cABKariTorokuPrm As ABKariTorokuParamXClass,
        // *                                                    ByVal blnInsertFlg As Boolean) As DataRow
        // * 
        // * 機能　　    　 個人制御情報を編集する
        // * 
        // * 引数           csKojinSeigyoRow：個人制御情報
        // *                cABKariTorokuPrm ：仮登録パラメータ
        // *                blnInsertFlg：挿入フラグ
        // * 
        // * 戻り値         個人制御情報(編集後)：DataRow
        // ************************************************************************************************
        public DataRow EditKojinSeigyoInfo(DataRow csKojinSeigyoRow, ABKariTorokuParamXClass cABKariTorokuPrm, bool blnInsertFlg)
        {
            const string THIS_METHOD_NAME = "EditKojinSeigyoInfo";          // メソッド名

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // 追加の場合
                if (blnInsertFlg == true)
                {
                    csKojinSeigyoRow[ABKojinseigyomstEntity.JUMINCD] = cABKariTorokuPrm.p_strJuminCD;        // 住民コード
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHICHOSONCD] = m_strShichosonCD;                 // 市町村コード
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KYUSHICHOSONCD] = m_strShichosonCD;              // 旧市町村コード
                    csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOKB] = string.Empty;                      // ＤＶ対象区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOMSG] = string.Empty;                     // ＤＶ対象メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD] = string.Empty;              // ＤＶ対象申請日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOKAISHIYMD] = string.Empty;               // ＤＶ対象開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOSHURYOYMD] = string.Empty;               // ＤＶ対象終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIKB] = string.Empty;                   // 発行停止区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIMSG] = string.Empty;                  // 発行停止メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD] = string.Empty;            // 発行停止開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD] = string.Empty;            // 発行停止終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAKB] = string.Empty;                   // 実態調査区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAMSG] = string.Empty;                  // 実態調査メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD] = string.Empty;            // 実態調査開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD] = string.Empty;            // 実態調査終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENKB] = string.Empty;                   // 成年後見区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENMSG] = string.Empty;                  // 成年後見メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD] = string.Empty;            // 成年後見開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD] = string.Empty;            // 成年後見終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD] = string.Empty;    // 成年被後見人の審判確定日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD] = string.Empty;         // 成年被後見人の登記日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD] = string.Empty;       // 成年被後見人である旨を知った日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKB] = m_strKTorokuKBN;                 // 仮登録中区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUMSG] = m_strMsg;                       // 仮登録中メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKAISHIYMD] = m_strSystemYMD;           // 仮登録中開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUSHURYOYMD] = ALL9_YMD;                 // 仮登録中終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIKB] = string.Empty;                // 特別養子区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG] = string.Empty;               // 特別養子メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD] = string.Empty;         // 特別養子開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD] = string.Empty;         // 特別養子終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOKB] = string.Empty;                 // 特別事情区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOMSG] = string.Empty;                // 特別事情メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD] = string.Empty;          // 特別事情開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD] = string.Empty;          // 特別事情終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1KB] = string.Empty;                    // 処理注意1区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1MSG] = string.Empty;                   // 処理注意1メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD] = string.Empty;             // 処理注意1開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD] = string.Empty;             // 処理注意1終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2KB] = string.Empty;                    // 処理注意2区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2MSG] = string.Empty;                   // 処理注意2メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD] = string.Empty;             // 処理注意2開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD] = string.Empty;             // 処理注意2終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUCD_CHUI] = string.Empty;                    // 業務コード注意
                    csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI] = string.Empty;              // 業務詳細（税目）コード注意
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3KB] = string.Empty;                    // 処理注意3区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3MSG] = string.Empty;                   // 処理注意3メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD] = string.Empty;             // 処理注意3開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD] = string.Empty;             // 処理注意3終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUKB] = string.Empty;                    // 処理保留区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUMSG] = string.Empty;                   // 処理保留メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD] = string.Empty;             // 処理保留開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD] = string.Empty;             // 処理保留終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUCD_HORYU] = string.Empty;                   // 業務コード保留
                    csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU] = string.Empty;             // 業務詳細（税目）コード保留
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAKB] = string.Empty;                    // 他業務不可区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAMSG] = string.Empty;                   // 他業務不可メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD] = string.Empty;             // 他業務不可開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD] = string.Empty;             // 他業務不可終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD] = string.Empty;         // 登録業務コード
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1KB] = string.Empty;                       // その他１区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1MSG] = string.Empty;                      // その他１メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1KAISHIYMD] = string.Empty;                // その他１開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1SHURYOYMD] = string.Empty;                // その他１終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2KB] = string.Empty;                       // その他２区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2MSG] = string.Empty;                      // その他２メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2KAISHIYMD] = string.Empty;                // その他２開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2SHURYOYMD] = string.Empty;                // その他２終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3KB] = string.Empty;                       // その他３区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3MSG] = string.Empty;                      // その他３メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3KAISHIYMD] = string.Empty;                // その他３開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3SHURYOYMD] = string.Empty;                // その他３終了日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KINSHIKAIJOKB] = string.Empty;                   // 禁止解除区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.SETAIYOKUSHIKB] = string.Empty;                  // 世帯抑止区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD] = string.Empty;                // 一時解除開始年月日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME] = string.Empty;               // 一時解除開始時刻
                    csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD] = string.Empty;                // 一時解除終了年月日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME] = string.Empty;               // 一時解除終了時刻
                    csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOUSER] = string.Empty;                 // 一時解除設定操作者ID
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KANRIKB] = string.Empty;                         // 管理区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.BIKO] = string.Empty;                            // 備考
                    csKojinSeigyoRow[ABKojinseigyomstEntity.RESERVE] = string.Empty;                         // リザーブ
                }

                // 更新の場合
                // 仮登録パラメータ・処理区分＝1(排他開始)の場合
                else if (cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI)
                {
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKB] = m_strKTorokuKBN;             // 仮登録中区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUMSG] = m_strMsg;                   // 仮登録中メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKAISHIYMD] = m_strSystemYMD;       // 仮登録中開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUSHURYOYMD] = ALL9_YMD;             // 仮登録中終了日
                }
                // 仮登録パラメータ・処理区分＝2(排他終了)の場合
                else if (cABKariTorokuPrm.p_strShoriKBN.Trim() == ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO)
                {
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKB] = string.Empty;                // 仮登録中区分
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUMSG] = string.Empty;               // 仮登録中メッセージ
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKAISHIYMD] = string.Empty;         // 仮登録中開始日
                    csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUSHURYOYMD] = string.Empty;         // 仮登録中終了日
                }

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return csKojinSeigyoRow;

        }
        #endregion

        #region 個人制御履歴情報編集
        // ************************************************************************************************
        // * メソッド名     個人制御履歴情報編集
        // * 
        // * 構文           Public Function EditKojinSeigyoRirekiInfo(ByVal csKojinSeigyoRirekiRow As DataRow,
        // *                                                          ByVal csKojinSeigyoRow As DataRow ,
        // *                                                          ByVal intRirekiNo As Integer) As DataRow
        // * 
        // * 機能　　    　 個人制御履歴情報編集を編集する
        // * 
        // * 引数           csKojinSeigyoRirekiRow：個人制御履歴情報
        // *                csKojinSeigyoRow：個人制御情報
        // *                intRirekiNo：履歴番号
        // * 
        // * 戻り値         個人制御履歴情報編集(編集後)：DataRow
        // ************************************************************************************************
        public DataRow EditKojinSeigyoRirekiInfo(DataRow csKojinSeigyoRirekiRow, DataRow csKojinSeigyoRow, int intRirekiNo)
        {
            const string THIS_METHOD_NAME = "EditKojinSeigyoRirekiInfo";          // メソッド名

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.JUMINCD] = csKojinSeigyoRow[ABKojinseigyomstEntity.JUMINCD];                                            // 住民コード
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHICHOSONCD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHICHOSONCD];                                    // 市町村コード
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KYUSHICHOSONCD] = csKojinSeigyoRow[ABKojinseigyomstEntity.KYUSHICHOSONCD];                              // 旧市町村コード
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.RIREKINO] = (decimal)intRirekiNo;                                                                          // 履歴番号
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.RIREKIEDABAN] = decimal.Zero;                                                                           // 履歴枝番
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.DVTAISHOKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOKB];                                      // ＤＶ対象区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.DVTAISHOMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOMSG];                                    // ＤＶ対象メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.DVTAISHOSHINSEIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD];                      // ＤＶ対象申請日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.DVTAISHOKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOKAISHIYMD];                        // ＤＶ対象開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.DVTAISHOSHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.DVTAISHOSHURYOYMD];                        // ＤＶ対象終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.HAKKOTEISHIKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIKB];                                // 発行停止区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.HAKKOTEISHIMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIMSG];                              // 発行停止メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.HAKKOTEISHIKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD];                  // 発行停止開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.HAKKOTEISHISHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD];                  // 発行停止終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.JITTAICHOSAKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAKB];                                // 実態調査区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.JITTAICHOSAMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAMSG];                              // 実態調査メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.JITTAICHOSAKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD];                  // 実態調査開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.JITTAICHOSASHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD];                  // 実態調査終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENKOKENKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENKB];                                // 成年後見区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENKOKENMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENMSG];                              // 成年後見メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENKOKENKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD];                  // 成年後見開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENKOKENSHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD];                  // 成年後見終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENKOKENSHIMPANKAKUTEIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD];  // 成年被後見人の審判確定日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENHIKOKENNINTOKIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD];            // 成年被後見人の登記日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SEINENHIKOKENNINSHITTAYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD];        // 成年被後見人である旨を知った日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KARITOROKUKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKB];                                  // 仮登録中区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KARITOROKUMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUMSG];                                // 仮登録中メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KARITOROKUKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUKAISHIYMD];                    // 仮登録中開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KARITOROKUSHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.KARITOROKUSHURYOYMD];                    // 仮登録中終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIKB];                          // 特別養子区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG];                        // 特別養子メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD];            // 特別養子開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUYOSHISHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD];            // 特別養子終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOKB];                            // 特別事情区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUJIJOMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOMSG];                          // 特別事情メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD];              // 特別事情開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.TOKUBETSUJIJOSHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD];              // 特別事情終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI1KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1KB];                                  // 処理注意1区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI1MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1MSG];                                // 処理注意1メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI1KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD];                    // 処理注意1開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI1SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD];                    // 処理注意1終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI2KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2KB];                                  // 処理注意2区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI2MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2MSG];                                // 処理注意2メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI2KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD];                    // 処理注意2開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI2SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD];                    // 処理注意2終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.GYOMUCD_CHUI] = csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUCD_CHUI];                                  // 業務コード注意
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_CHUI] = csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI];                      // 業務詳細（税目）コード注意
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI3KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3KB];                                  // 処理注意3区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI3MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3MSG];                                // 処理注意3メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI3KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD];                    // 処理注意3開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORICHUI3SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD];                    // 処理注意3終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORIHORYUKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUKB];                                  // 処理保留区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORIHORYUMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUMSG];                                // 処理保留メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORIHORYUKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD];                    // 処理保留開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SHORIHORYUSHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD];                    // 処理保留終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.GYOMUCD_HORYU] = csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUCD_HORYU];                                // 業務コード保留
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_HORYU] = csKojinSeigyoRow[ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU];                    // 業務詳細（税目）コード保留
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SANSHOFUKAKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAKB];                                  // 他業務不可区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SANSHOFUKAMSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAMSG];                                // 他業務不可メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SANSHOFUKAKAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD];                    // 他業務不可開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SANSHOFUKASHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD];                    // 他業務不可終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SANSHOFUKATOROKUGYOMUCD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD];            // 登録業務コード
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA1KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1KB];                                        // その他１区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA1MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1MSG];                                      // その他１メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA1KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1KAISHIYMD];                          // その他１開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA1SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA1SHURYOYMD];                          // その他１終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA2KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2KB];                                        // その他２区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA2MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2MSG];                                      // その他２メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA2KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2KAISHIYMD];                          // その他２開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA2SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA2SHURYOYMD];                          // その他２終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA3KB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3KB];                                        // その他３区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA3MSG] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3MSG];                                      // その他３メッセージ
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA3KAISHIYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3KAISHIYMD];                          // その他３開始日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SONOTA3SHURYOYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.SONOTA3SHURYOYMD];                          // その他３終了日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KINSHIKAIJOKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.KINSHIKAIJOKB];                                // 禁止解除区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.SETAIYOKUSHIKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.SETAIYOKUSHIKB];                              // 世帯抑止区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD];                          // 一時解除開始年月日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTTIME] = csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME];                        // 一時解除開始時刻
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDYMD] = csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD];                          // 一時解除終了年月日
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDTIME] = csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME];                        // 一時解除終了時刻
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.ICHIJIKAIJOUSER] = csKojinSeigyoRow[ABKojinseigyomstEntity.ICHIJIKAIJOUSER];                            // 一時解除設定操作者ID
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.KANRIKB] = csKojinSeigyoRow[ABKojinseigyomstEntity.KANRIKB];                                            // 管理区分
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.BIKO] = csKojinSeigyoRow[ABKojinseigyomstEntity.BIKO];                                                  // 備考
                csKojinSeigyoRirekiRow[ABKojinseigyoRirekiEntity.RESERVE] = csKojinSeigyoRow[ABKojinseigyomstEntity.RESERVE];                                            // リザーブ

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return csKojinSeigyoRirekiRow;

        }
        #endregion
        #endregion

    }
}
