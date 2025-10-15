// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名_標準マスタＤＡ(ABAtena_HyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/08/14 早崎  雄矢
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/12/11  000001     【AB-9000-1】住基更新連携標準化対応(下村)
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
    // * 宛名_標準マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtena_HyojunBClass
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
        private const string THIS_CLASS_NAME = "ABAtena_HyojunBClass";                // クラス名
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
        public ABAtena_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
        // * メソッド名     宛名_標準マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaHyojunBHoshu(ByVal strJuminCD As String, _
        // *                                                     ByVal strJuminJutogaiKB As String, _
        // *                                                     ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 宛名_標準マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strJuminJutogaiKB  : 住民住登外区分
        // *                blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaHyojunBHoshu(string strJuminCD, string strJuminJutogaiKB, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetAtenaHyojunBHoshu";
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
                csSQL.AppendFormat(" FROM {0} ", ABAtenaHyojunEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaHyojunEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                csSQL.Append(CreateWhere(strJuminCD, strJuminJutogaiKB, blnSakujoFG));

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
                // csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity, ABAtenaHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
                csSELECT.AppendFormat("SELECT {0}", ABAtenaHyojunEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RRKNO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.EDANO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.UMAREBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOUMAREBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JIJITSUSTAINUSMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.BANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KUNIMEICODE);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KUNIMEITO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CKINIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOCKINIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOTOROKUIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KISAIYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KISAIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKISAIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOJUTEIIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOJOIDOWMD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOSHOJOIDOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_YUBINNO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_BANCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEIBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKAISEIBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEISHOJOYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOKUBETSUYOSHIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.IDOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NYURYOKUBASHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NYURYOKUBASHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKANJIKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKANAKYUUJI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TDKDSHIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNIDOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOROKUBUSHO);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TANKITAIZAISHAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KYOYUNINZU);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIZEIJIMUSHOCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.IDOSHURUI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOKANKUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOGOATENAFG);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOUMAREBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKISHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIMACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKITODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKISHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIMACHIAZA);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIKANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD4);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD5);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD6);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD7);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD8);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD9);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD10);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINUSER);

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
        // ByVal strJuminJutogaiKB As String, _
        // ByVal blnSakujoFG As Boolean) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strJuminJutogaiKB  : 住民住登外区分
        // *                blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strJuminJutogaiKB, bool blnSakujoFG)
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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaHyojunEntity.JUMINCD, ABAtenaHyojunEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 住民住登外区分
                if (!strJuminJutogaiKB.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaHyojunEntity.JUMINJUTOGAIKB, ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB;
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
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaHyojunEntity.SAKUJOFG, SAKUJOFG_ON);
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

        #region 宛名_標準マスタ追加
        // ************************************************************************************************
        // * メソッド名     宛名_標準マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名_標準マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaHyojunB";
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
                csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow(ABAtenaHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow(ABAtenaHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow(ABAtenaHyojunEntity.SAKUSEINICHIJI);
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
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
                    strParamName = string.Format("{0}{1}", ABAtenaHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaHyojunEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

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

        #region 宛名_標準マスタ更新
        // ************************************************************************************************
        // * メソッド名     宛名_標準マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名_標準マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow 　　: 更新するデータの含まれるDataRowオブジェクト
        // *                strAtenaDataKB As String : 宛名データ区分
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaHyojunB(DataRow csDataRow, string strAtenaDataKB)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaHyojunB";                     // パラメータクラス
            int intUpdCnt;                            // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null || string.IsNullOrEmpty(m_strUpdateSQL) || m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateUpdateSQL(csDataRow, strAtenaDataKB);
                }
                else
                {
                    // 処理なし
                }

                // 共通項目の編集を行う
                csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1m;       // 更新カウンタ
                csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) == ABAtenaHyojunEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        if (cfParam.ParameterName != ABAtenaHyojunEntity.KOSHINCOUNTER)
                        {
                            this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                        }
                        else
                        {
                            this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original);
                        }
                    }
                    else
                    {
                        // キー項目以外は編集内容取得
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
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
        // * 引数           csDataRow As DataRow 　　: 更新対象の行
        // *                strAtenaDataKB As String : 宛名データ区分
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow, string strAtenaDataKB)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            const string strAtenaDataKbn_Hojin = "13";
            const string strAtenaDataKbn_Kyoyu = "14";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csUpdateParam;                  // UPDATE用SQL定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaHyojunEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 以下の項目だけ更新する
                    if (csDataColumn.ColumnName == ABAtenaHyojunEntity.JUMINJUTOGAIKB || csDataColumn.ColumnName == ABAtenaHyojunEntity.RRKNO || csDataColumn.ColumnName == ABAtenaHyojunEntity.UMAREBIFUSHOPTN || csDataColumn.ColumnName == ABAtenaHyojunEntity.FUSHOUMAREBI || csDataColumn.ColumnName == ABAtenaHyojunEntity.SHIKUCHOSONCD || csDataColumn.ColumnName == ABAtenaHyojunEntity.MACHIAZACD || csDataColumn.ColumnName == ABAtenaHyojunEntity.TODOFUKEN || csDataColumn.ColumnName == ABAtenaHyojunEntity.SHIKUCHOSON || csDataColumn.ColumnName == ABAtenaHyojunEntity.MACHIAZA || csDataColumn.ColumnName == ABAtenaHyojunEntity.SEARCHJUSHO || csDataColumn.ColumnName == ABAtenaHyojunEntity.SEARCHKATAGAKI || csDataColumn.ColumnName == ABAtenaHyojunEntity.BANCHIEDABANSUCHI || csDataColumn.ColumnName == ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN || csDataColumn.ColumnName == ABAtenaHyojunEntity.FUSHOSHOJOIDOBI || csDataColumn.ColumnName == ABAtenaHyojunEntity.NYURYOKUBASHOCD || csDataColumn.ColumnName == ABAtenaHyojunEntity.NYURYOKUBASHO || csDataColumn.ColumnName == ABAtenaHyojunEntity.FUSHOUMAREBI_DATE || csDataColumn.ColumnName == ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE || csDataColumn.ColumnName == ABAtenaHyojunEntity.TANMATSUID || csDataColumn.ColumnName == ABAtenaHyojunEntity.SAKUJOFG || csDataColumn.ColumnName == ABAtenaHyojunEntity.KOSHINCOUNTER || csDataColumn.ColumnName == ABAtenaHyojunEntity.KOSHINNICHIJI || csDataColumn.ColumnName == ABAtenaHyojunEntity.KOSHINUSER)
                    {

                        // 以下の条件の時はループの先頭に戻る
                        switch (csDataColumn.ColumnName ?? "")
                        {
                            case var @case when @case == ABAtenaHyojunEntity.RRKNO:
                                {
                                    // 履歴番号
                                    if (m_cfControlData.m_strMenuId == ABMenuIdCNST.MENU_ATENATOKUSHU_UPDATE)
                                    {
                                        // メニューID「AB09092」(特殊修正)の場合は更新しない
                                        continue;
                                    }

                                    break;
                                }
                            case var case1 when case1 == ABAtenaHyojunEntity.UMAREBIFUSHOPTN:
                            case var case2 when case2 == ABAtenaHyojunEntity.FUSHOUMAREBI:
                                {
                                    // 生年月日不詳パターン,不詳生年月日
                                    if (ReferenceEquals(strAtenaDataKB, strAtenaDataKbn_Hojin) || ReferenceEquals(strAtenaDataKB, strAtenaDataKbn_Kyoyu))
                                    {
                                        // 消除異動日不詳パターン
                                        continue;
                                    }

                                    break;
                                }
                        }

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaHyojunEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB;
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

        #region 宛名付随マスタ更新　[UpdateAtenaHyojunB]
        // ************************************************************************************************
        // * メソッド名     宛名付随マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaFZYB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名付随マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaHyojunB";                     // パラメータクラス
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
                csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1m;       // 更新カウンタ
                csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) == ABAtenaHyojunEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

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
                m_strUpdateSQL = "UPDATE " + ABAtenaHyojunEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・住民住登外区分・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaHyojunEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaHyojunEntity.JUMINJUTOGAIKB) && !(csDataColumn.ColumnName == ABAtenaHyojunEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaHyojunEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaHyojunEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER;
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

        #region 宛名_標準マスタ削除
        // ************************************************************************************************
        // * メソッド名     宛名_標準マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名_標準マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaHyojunB";  // パラメータクラス
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
                csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABAtenaHyojunEntity.SAKUJOFG) = SAKUJOFG_ON;                                                       // 削除フラグ
                csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1m;       // 更新カウンタ
                csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) == ABAtenaHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
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
        // * メソッド名     宛名_標準マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow, _
        // *                                                   ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 宛名_標準マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow  : 削除するデータの含まれるDataRowオブジェクト
        // *                strSakujoKB As String : 削除フラグ
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaHyojunB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaHyojunB";
            UFErrorStruct cfErrorStruct; // エラー定義構造体
                                         // パラメータクラス
            int intDelCnt;            // 削除件数

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
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) == ABAtenaHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外の取得なし
                    else
                    {
                        // 処理なし
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
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
                csWhere.Append(ABAtenaHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER;
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
                csWhere.Append(ABAtenaHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABAtenaHyojunEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER;
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
