// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名累積_標準マスタＤＡ(ABAtenaRuiseki_HyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/08/14 早崎  雄矢
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// *
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 宛名累積_標準マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRuiseki_HyojunBClass
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
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private DataSet m_csDataSchma;                                                // スキーマ保管用データセット
        private string m_strUpdateDatetime;                                           // 更新日時

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRuiseki_HyojunBClass";         // クラス名
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
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaRuiseki_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名累積_標準マスタ抽出
        // * 
        // * 構文           Public Function GetABAtenaRuisekiHyojunBClassBHoshu(ByVal strJuminCD As String, _
        // *                                                                    ByVal strRirekiNO As String, _
        // *                                                                    ByVal strShoriNichiji As String, _
        // *                                                                    ByVal strZengoKB As String) As DataSet
        // * 
        // * 機能　　    　 宛名累積_標準マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                strShoriNichiji    : 処理日時
        // *                strZengoKB         : 前後区分
        // * 
        // * 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
        // ************************************************************************************************
        public DataSet GetABAtenaRuisekiHyojunBClassBHoshu(string strJuminCD, string strRirekiNO, string strShoriNichiji, string strZengoKB)
        {

            const string THIS_METHOD_NAME = "GetABAtenaRuisekiHyojunBClassBHoshu";
            UFErrorStruct cfErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var csSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 住民コードが指定されていないときエラー
                if (strJuminCD == null || strJuminCD.Trim().RLength == 0)
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
                csSQL.Append(CreateSelect());
                // FROM句の生成
                csSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiHyojunEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaRuisekiHyojunEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                csSQL.Append(CreateWhere(strJuminCD, strRirekiNO, strShoriNichiji, strZengoKB));

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity, ABAtenaRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiHyojunEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.ZENGOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.EDANO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIMEIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.UMAREBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JIJITSUSTAINUSMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.BANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEICODE);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEITO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUGUNCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOTOROKUIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNKISAIJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKISAIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUTEIIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOJUTEIIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNSHOJOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOKUSEKISOSHITSUBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_YUBINNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEISHOJOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOKUBETSUYOSHIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANJIKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANAKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYUUJIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TDKDSHIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNIDOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NICHIJOSEIKATSUKENIKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUBUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANKITAIZAISHAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYOYUNINZU);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIZEIJIMUSHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ST);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ED);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOSHURUI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOKANKUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOGOATENAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIKANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIBANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINUSER);

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

        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Function CreateWhere(ByVal strJuminCD As String, _
        // *                                             ByVal strRirekiNO As String, _
        // *                                             ByVal strShoriNichiji As String, _
        // *                                             ByVal strZengoKB As String) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                strShoriNichiji    : 処理日時
        // *                strZengoKB         : 前後区分
        // *
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strRirekiNO, string strShoriNichiji, string strZengoKB)
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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiHyojunEntity.JUMINCD, ABAtenaRuisekiHyojunEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                if (!strRirekiNO.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.RIREKINO, ABAtenaRuisekiHyojunEntity.KEY_RIREKINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_RIREKINO;
                    cfUFParameterClass.Value = strRirekiNO;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 処理日時
                if (!strShoriNichiji.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI, ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI;
                    cfUFParameterClass.Value = strShoriNichiji;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 前後区分
                if (!strZengoKB.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.ZENGOKB, ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB;
                    cfUFParameterClass.Value = strZengoKB;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
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

        #region 宛名累積_標準マスタ追加
        // ************************************************************************************************
        // * メソッド名     宛名累積_標準マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積_標準マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRuisekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRuisekiHyojunB";
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
                csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI);
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString();

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
                    strParamName = string.Format("{0}{1}", ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaRuisekiHyojunEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

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

        #region 宛名累積_標準マスタ更新
        // ************************************************************************************************
        // * メソッド名     宛名累積_標準マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積_標準マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRuisekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaRuisekiHyojunB";                     // パラメータクラス
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
                csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1m;                  // 更新カウンタ
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) == ABAtenaRuisekiHyojunEntity.PREFIX_KEY)
                    {

                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
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
                m_strUpdateSQL = "UPDATE " + ABAtenaRuisekiHyojunEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・履歴番号・処理日時・前後区分・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.RIREKINO) && !(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.SHORINICHIJI) && !(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.ZENGOKB) && !(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER;
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

        #region 宛名累積_標準マスタ削除
        // ************************************************************************************************
        // * メソッド名     宛名累積_標準マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積_標準マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRuisekiHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaRuisekiHyojunB";  // パラメータクラス
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
                csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;      // 端末ＩＤ
                csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_ON;                          // 削除フラグ
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1m;                     // 更新カウンタ
                csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;        // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) == ABAtenaRuisekiHyojunEntity.PREFIX_KEY)
                    {

                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
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
                csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER;
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
                if (csDate is DBNull || Conversions.ToString(csDate).Trim().Equals(string.Empty))
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

    }
}
