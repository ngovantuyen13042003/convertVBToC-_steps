// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名履歴_標準マスタＤＡ(ABAtenaRireki_HyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/08/14 早崎  雄矢
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/12/11  000001     【AB-9000-1】住基更新連携標準化対応(下村)
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    // ************************************************************************************************
    // *
    // * 宛名履歴_標準マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRireki_HyojunBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                                          // エラー処理クラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDelRonriSQL;                                              // 論理削除用SQL
        private string m_strDelButuriSQL;                                             // 物理削除用SQL
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
        private DataSet m_csDataSchma;                                                // スキーマ保管用データセット
        private string m_strUpdateDatetime;                                           // 更新日時

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRireki_HyojunBClass";          // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード
        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;
        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";
        private const string ERR_JUMINCD = "住民コード";

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
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaRireki_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDelRonriSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;

        }
        #endregion

        #region メソッド
        #region 宛名履歴_標準マスタ抽出
        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRirekiHyojunBHoshu(ByVal strJuminCD As String, _
        // *                                                           ByVal strRirekiNO As String, _
        // *                                                           ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 宛名履歴_標準マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRirekiHyojunBHoshu(string strJuminCD, string strRirekiNO, bool blnSakujoFG)
        {

            return GetAtenaRirekiHyojunBHoshu(strJuminCD, strRirekiNO, string.Empty, blnSakujoFG);
        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRirekiHyojunBHoshu(ByVal strJuminCD As String, _
        // *                                                           ByVal strRirekiNO As String, _
        // *                                                           ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 宛名履歴_標準マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRirekiHyojunBHoshu(string strJuminCD, string strRirekiNO, string strJuminJutogaiKB, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetAtenaRirekiHyojunBHoshu";
            UFErrorStruct cfErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 住民コードが指定されていないときエラー
                if (strJuminCD == null || strJuminCD.Trim().RLength() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(cfErrorStruct.m_strErrorMessage + ERR_JUMINCD, cfErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiHyojunEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiHyojunEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strRirekiNO, strJuminJutogaiKB, blnSakujoFG));

                // ORDER BY句の作成
                strSQL.Append(" ORDER BY " + ABAtenaRirekiHyojunEntity.RIREKINO + " DESC");

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABAtenaRirekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csAtenaEntity;

        }
        #endregion

        #region SELECT句の作成
        // ************************************************************************************************
        // * メソッド名     SELECT句の作成
        // * 
        // * 構文           Private Sub CreateSelect() As String
        // * 
        // * 機能　　    　 SELECT句を生成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String    :   SELECT句
        // ************************************************************************************************
        private string CreateSelect()
        {
            const string THIS_METHOD_NAME = "CreateSelect";
            var csSELECT = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の作成
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiHyojunEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.EDANO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHIMEIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.UMAREBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOUMAREBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JIJITSUSTAINUSMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SEARCHJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.BANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEICODE);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEITO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUSHO_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HON_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HON_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HON_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HON_SHIKUGUNCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HON_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CKINIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CKINIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TOROKUIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOTOROKUIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HYOJUNKISAIJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KISAIYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KISAIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOKISAIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUTEIIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOJUTEIIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HYOJUNSHOJOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KOKUSEKISOSHITSUBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHOJOIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHOJOIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_YUBINNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUTJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAISHUJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KAISEIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOKAISEIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KAISEISHOJOYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KAISEISHOJOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOKAISEISHOJOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.CHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TOKUBETSUYOSHIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.IDOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.NYURYOKUBASHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.NYURYOKUBASHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KYUUJIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TDKDSHIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.HYOJUNIDOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.NICHIJOSEIKATSUKENIKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TOROKUBUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TANKITAIZAISHAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KYOYUNINZU);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHIZEIJIMUSHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ST);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ED);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.IDOSHURUI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SHOKANKUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TOGOATENAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOUMAREBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKIKANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKICHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.JUKIBANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiHyojunEntity.KOSHINUSER);

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

            return csSELECT.ToString();

        }
        #endregion

        #region WHERE文の作成
        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Function CreateWhere(ByVal strJuminCD As String, _
        // /                                              ByVal strRirekiNO As String, _
        // *                                             ByVal strJuminJutogaiKB As String,
        // ByVal blnSakujoFG As Boolean) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                strJuminJutogaiKB  : 住民住登外区分,
        // *                blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strRirekiNO, string strJuminJutogaiKB, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "CreateWhere";
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 住民コード
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiHyojunEntity.JUMINCD, ABAtenaRirekiHyojunEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                if (!strRirekiNO.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiHyojunEntity.RIREKINO, ABAtenaRirekiHyojunEntity.KEY_RIREKINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_RIREKINO;
                    cfUFParameterClass.Value = strRirekiNO;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 住民住登外区分
                if (!strJuminJutogaiKB.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB, ABAtenaRirekiHyojunEntity.PARAM_JUMINJUTOGAIKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_JUMINJUTOGAIKB;
                    cfUFParameterClass.Value = strJuminJutogaiKB;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiHyojunEntity.SAKUJOFG, SAKUJOFG_ON);
                }
                else
                {
                    // 処理なし
                }

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

            return csWHERE.ToString();

        }

        #region 宛名履歴_標準マスタ追加
        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRirekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名履歴_標準マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRirekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRirekiHyojunB";
            int intInsCnt;                            // 追加件数

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL) || m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateInsertSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow[ABAtenaRirekiHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow[ABAtenaRirekiHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow[ABAtenaRirekiHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow[ABAtenaRirekiHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow[ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI];
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow[ABAtenaRirekiHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

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

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     Insert用SQL文の作成
        // * 
        // * 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           INSERT用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateInsertSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateInsertSQL";
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義
            UFParameterClass cfUFParameterClass;
            string strParamName;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();
                    strParamName = string.Format("{0}{1}", ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    if (csDataColumn.ColumnName == "RRKNO")
                    {
                        csInsertColumn.AppendFormat("{0},", "RIREKINO");
                    }
                    else
                    {
                        csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    }

                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaRirekiHyojunEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

                // デバッグ終了ログ出力
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

        }
        #endregion

        #region 宛名履歴_標準マスタ更新
        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRirekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名履歴_標準マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRirekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaRirekiHyojunB";                     // パラメータクラス
            int intUpdCnt;                            // 更新件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null || string.IsNullOrEmpty(m_strUpdateSQL) || m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateUpdateSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaRirekiHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;         // 端末ＩＤ

                // 更新カウンタ
                if (m_cfControlData.m_strMenuId == ABMenuIdCNST.MENU_ATENATOKUSHU_UPDATE || m_cfControlData.m_strMenuId == ABMenuIdCNST.MENU_ATENATOKUSHU_RIREKI_UPDATE)
                {
                    // 特殊修正または特殊履歴修正の場合
                    csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER] = "0";
                }
                else
                {
                    // 上記以外はカウントアップ
                    csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER]) + 1m;
                }

                csDataRow[ABAtenaRirekiHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;           // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRirekiHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiHyojunEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength())].ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

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

            return intUpdCnt;

        }

        // ************************************************************************************************
        // * メソッド名     Update用SQL文の作成
        // * 
        // * 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csUpdateParam;                  // UPDATE用SQL定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaRirekiHyojunEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_RIREKINO);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・履歴番号・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaRirekiHyojunEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaRirekiHyojunEntity.RIREKINO) && !(csDataColumn.ColumnName == ABAtenaRirekiHyojunEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(",".ToCharArray());

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += csWhere.ToString();

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_RIREKINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
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

        }
        #endregion

        #region 宛名履歴_標準マスタ削除
        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaRirekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名履歴_標準マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRirekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaRirekiHyojunB";  // パラメータクラス
            int intDelCnt;        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null || string.IsNullOrEmpty(m_strDelRonriSQL) || m_cfDelRonriUFParameterCollectionClass is null)
                {
                    CreateDeleteRonriSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaRirekiHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABAtenaRirekiHyojunEntity.SAKUJOFG] = SAKUJOFG_ON;                                                       // 削除フラグ
                csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRirekiHyojunEntity.KOSHINCOUNTER]) + 1m; // 更新カウンタ
                csDataRow[ABAtenaRirekiHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRirekiHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);

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

            return intDelCnt;

        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴_標準マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaHyojunRB(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 宛名履歴_標準マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaHyojunRB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaHyojunRB";
            UFErrorStruct cfErrorStruct;                 // エラー定義構造体
                                                         // パラメータクラス
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除区分のチェックを行う
                if (!(strSakujoKB == "D"))
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    cfErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null || string.IsNullOrEmpty(m_strDelButuriSQL) || m_cfDelButuriUFParameterCollectionClass == null)
                {
                    CreateDeleteButsuriSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // 処理なし
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL,
                // m_cfDelButuriUFParameterCollectionClass) + "】")

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass);

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

            return intDelCnt;

        }

        // ************************************************************************************************
        // * メソッド名     論理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteRonriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteRonriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csDelRonriParam;                // 論理削除パラメータ定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiHyojunEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_RIREKINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
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

        }

        // ************************************************************************************************
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteButsuriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiHyojunEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiHyojunEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_RIREKINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
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

        }
        #endregion

        #region 更新日時設定
        // ************************************************************************************************
        // * メソッド名     更新日時設定
        // * 
        // * 構文           Private Sub SetUpdateDatetime(ByRef csDate As Object)
        // * 
        // * 機能           未設定のとき更新日時を設定する
        // * 
        // * 引数           csDate As Object : 更新日時の項目
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetUpdateDatetime(ref object csDate)
        {
            try
            {
                // 未設定のとき
                if (csDate is DBNull || UFVBAPI.ToString(csDate).Trim().Equals(string.Empty))
                {
                    csDate = m_strUpdateDatetime;
                }
                else
                {
                    // 処理なし
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
