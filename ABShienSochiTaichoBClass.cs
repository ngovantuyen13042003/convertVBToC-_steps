// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ支援措置対象者マスタＤＡ(ABShienSochiTaishoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/10/13　下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
// * 2024/01/18   000001    【AB-0070-1】 支援措置通知書標準化対応
// * 2024/03/07   000002   【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABShienSochiTaishoBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
        private string m_strUpdateDatetime;                   // 更新日時

        public bool m_blnBatch = false;               // バッチフラグ
                                                      // コンスタント定義
        private const string THIS_CLASS_NAME = "ABShienSochiTaishoBClass";                     // クラス名
        private const string THIS_BUSINESSID = "AB";                                   // 業務コード

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private const string ERR_JUMINCD = "住民コード";
        private const string ERR_SHIENSOCHIKANRINO = "支援措置管理番号";

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
        public ABShienSochiTaishoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_strDelButuriSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;
            m_cfDelButuriUFParameterCollectionClass = (object)null;
        }
        #endregion

        #region メソッド
        #region 支援措置対象者マスタ抽出　[GetShienSochiTaisho]
        // ************************************************************************************************
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaisho As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号 
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaisho(string strShienSochiKanriNo)
        {

            return GetShienSochiTaisho(strShienSochiKanriNo, false);

        }
        // ************************************************************************************************
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaisho As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号
        // *               blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaisho(string strShienSochiKanriNo, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochiTaisho";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 支援措置管理番号が指定されていないときエラー
                if (strShienSochiKanriNo == null || strShienSochiKanriNo.Trim().RLength == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strShienSochiKanriNo, 0, blnSakujoFG));
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                strSQL.AppendFormat(", {0}", ABShienSochiTaishoEntity.RENBAN);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaisho As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号 
        // *               intRenban            : 連番
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaisho(string strShienSochiKanriNo, int intRenban)
        {

            return GetShienSochiTaisho(strShienSochiKanriNo, intRenban, false);

        }
        // ************************************************************************************************
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaisho As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号
        // *               intRenban            : 連番
        // *               blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaisho(string strShienSochiKanriNo, int intRenban, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochiTaisho";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 支援措置管理番号が指定されていないときエラー
                if (strShienSochiKanriNo == null || strShienSochiKanriNo.Trim().RLength == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strShienSochiKanriNo, intRenban, blnSakujoFG));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名    支援措置対象者抽出
        // * 
        // * 構文          Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo() As String) As DataSet
        // * 
        // * 機能　　    　支援措置管理番号より該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号の配列       
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者の該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaisho(string[] strShienSochiKanriNo)
        {

            const string THIS_METHOD_NAME = "GetShienSochiTaisho";
            DataSet csShienSochitaishoEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfParameter;
            string strParameterName;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                if (strShienSochiKanriNo.Length == 0)
                {
                    csShienSochitaishoEntity = m_csDataSchma.Clone();
                }
                else
                {
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                    strSQL.Append(" IN (");

                    for (int i = 0, loopTo = strShienSochiKanriNo.Length - 1; i <= loopTo; i++)
                    {
                        // -----------------------------------------------------------------------------
                        // 支援措置管理番号
                        strParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO + i.ToString();

                        if (i > 0)
                        {
                            strSQL.AppendFormat(", {0}", strParameterName);
                        }
                        else
                        {
                            strSQL.Append(strParameterName);
                        }

                        cfParameter = new UFParameterClass();
                        cfParameter.ParameterName = strParameterName;
                        cfParameter.Value = strShienSochiKanriNo[i];
                        m_cfSelectUFParameterCollectionClass.Add(cfParameter);
                        // -----------------------------------------------------------------------------
                    }

                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABShienSochiTaishoEntity.SAKUJOFG);

                    strSQL.Append(" <> '1'");

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                    // SQLの実行 DataSetの取得
                    csShienSochitaishoEntity = m_csDataSchma.Clone();
                    csShienSochitaishoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochitaishoEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csShienSochitaishoEntity;

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
                csSELECT.AppendFormat("SELECT {0}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RENBAN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MOSHIDEJOKYOKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TAISHOSHAKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TAISHOSHAKANKEI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.YUBINNO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_KANNAIKANGAIKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_JUSHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_JUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.BANCHI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KATAGAKICD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KANAKATAGAKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUKIDAICHOETSURAN_GENJUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUMINHYOUTSUSHIKOFU_GENJUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUMINHYOUTSUSHIKOFU_ZENJUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TENSHUTSUKAKUTEI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TENSHUTSUYOTEI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TOGOKISAIRAN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_JUSHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_JUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_SHIKUCHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_BANCHI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOSEKIFUHYOUTSUSHIKOFU_HONSEKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_JUSHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_JUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_SHIKUGUNCHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HONSEKIBANCHI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOSEKIFUHYOUTSUSHIKOFU_ZENHONSEKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_JUSHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_JUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_SHIKUCHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_SHIKUGUNCHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHONSEKIBANCHI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOTEISHISAN);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD1);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON1);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD2);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON2);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD3);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON3);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD4);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON4);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD5);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON5);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINUSER);

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
        // * メソッド名   WHERE文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能         WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数         strShienSochiKanriNo : 支援措置管理番号 
        // *              intRenban            : 連番
        // *              blnSakujoFG          : 削除フラグ
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private string CreateWhere(string strShienSochiKanriNo, int intRenban, bool blnSakujoFG)
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

                // 支援措置管理番号
                csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO, ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO;
                cfUFParameterClass.Value = strShienSochiKanriNo;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 連番
                if (!(intRenban == 0))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiTaishoEntity.RENBAN, ABShienSochiTaishoEntity.KEY_RENBAN);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN;
                    cfUFParameterClass.Value = intRenban.ToString();
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
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiTaishoEntity.SAKUJOFG, SAKUJOFG_ON);
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

        // ************************************************************************************************
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaishoByJuminCD() As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          a_strJuminCd()       : 住民コードの配列
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaishoByJuminCD(string[] a_strJuminCd)
        {

            const string THIS_METHOD_NAME = "GetShienSochiTaishoByJuminCD";
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();
            string strParameterName;
            UFParameterClass cfParameter;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();
                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(" WHERE ");
                strSQL.Append(ABShienSochiTaishoEntity.JUMINCD);
                strSQL.Append(" IN (");

                for (int i = 0, loopTo = a_strJuminCd.Length - 1; i <= loopTo; i++)
                {
                    // -----------------------------------------------------------------------------
                    // 住民コード
                    strParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD + i.ToString();

                    if (i > 0)
                    {
                        strSQL.AppendFormat(", {0}", strParameterName);
                    }
                    else
                    {
                        strSQL.Append(strParameterName);
                    }

                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = strParameterName;
                    cfParameter.Value = a_strJuminCd[i];
                    m_cfSelectUFParameterCollectionClass.Add(cfParameter);
                    // -----------------------------------------------------------------------------
                }

                strSQL.Append(")");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaishoByJuminCD As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strJuminCD : 住民コード 
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaishoByJuminCD(string strJuminCD)
        {

            return GetShienSochiTaishoByJuminCD(strJuminCD, string.Empty, false);

        }
        // ************************************************************************************************
        // * メソッド名    支援措置対象者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiTaishoByJuminCD() As DataSet
        // * 
        // * 機能　　    　支援措置対象者マスタより該当データを取得する
        // * 
        // * 引数          strJuminCD           : 住民コード
        // *               strShienSochiKanriNo : 支援措置管理番号
        // *               blnSakujoFG          : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiTaishoByJuminCD(string strJuminCd, string strShienSochiKanriNo, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochiTaishoByJuminCD";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 住民コードが指定されていないときエラー
                if (strJuminCd == null || strJuminCd.Trim().RLength == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhereJuminCD(strJuminCd, strShienSochiKanriNo, blnSakujoFG));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名   WHERE文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能         WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数         strJuminCD           : 住民コード 
        // *              strShienSochiKanriNo : 支援措置管理番号
        // *              blnSakujoFG          : 削除フラグ
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private string CreateWhereJuminCD(string strJuminCD, string strShienSochiKanriNo, bool blnSakujoFG)
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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiTaishoEntity.JUMINCD, ABShienSochiTaishoEntity.PARAM_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 支援措置管理番号
                if (!string.IsNullOrEmpty(strShienSochiKanriNo))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO, ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO;
                    cfUFParameterClass.Value = strShienSochiKanriNo;
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
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiTaishoEntity.SAKUJOFG, SAKUJOFG_ON);
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
        #endregion

        #region 支援措置対象者マスタ追加　[InsertShienSochiTaisho]
        // ************************************************************************************************
        // * メソッド名     支援措置対象者マスタ追加
        // * 
        // * 構文           Public Function InsertShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置対象者マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertShienSochiTaisho(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertShienSochiTaisho";
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
                csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow(ABShienSochiTaishoEntity.SAKUJOFG) = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow(ABShienSochiTaishoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow(ABShienSochiTaishoEntity.SAKUSEINICHIJI);
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");

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
                    strParamName = string.Format("{0}{1}", ABShienSochiTaishoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABShienSochiTaishoEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

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

        #region 支援措置対象者マスタ更新　[UpdateShienSochiTaisho]
        // ************************************************************************************************
        // * メソッド名     支援措置対象者マスタ更新
        // * 
        // * 構文           Public Function UpdateShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 支援措置対象者マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateShienSochiTaisho(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateShienSochiTaisho";                     // パラメータクラス
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
                csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) + 1m;  // 更新カウンタ
                csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI) = m_strUpdateDatetime;

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) == ABShienSochiTaishoEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");

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
                m_strUpdateSQL = "UPDATE " + ABShienSochiTaishoEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 支援措置管理番号・連番・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABShienSochiTaishoEntity.SHIENSOCHIKANRINO) && !(csDataColumn.ColumnName == ABShienSochiTaishoEntity.RENBAN) && !(csDataColumn.ColumnName == ABShienSochiTaishoEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABShienSochiTaishoEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER;
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

        #region 支援措置対象者マスタ削除　[DeleteShienSochiTaisho]
        // ************************************************************************************************
        // * メソッド名     支援措置対象者マスタ削除
        // * 
        // * 構文           Public Function DeleteShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置対象者マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteShienSochiTaisho(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteShienSochiTaisho";  // パラメータクラス
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
                csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABShienSochiTaishoEntity.SAKUJOFG) = SAKUJOFG_ON;                                                       // 削除フラグ
                csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) + 1m;  // 更新カウンタ
                csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) == ABShienSochiTaishoEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】");
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
        // * メソッド名     支援措置対象者物理削除
        // * 
        // * 構文           Public Function DeleteShiensochiTaisho(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　支援措置対象者マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteShiensochiTaisho(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteShiensochiTaisho";
            UFErrorStruct objErrorStruct; // エラー定義構造体
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
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
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
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) == ABShienSochiTaishoEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外の取得なし
                    else
                    {
                        // 処理なし
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】");
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
                csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER);


                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER;
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
        // * 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateButsuriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABShienSochiTaishoEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER;
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

        #region その他
        // ************************************************************************************************
        // * メソッド名     更新日時設定
        // * 
        // * 構文           Private Sub SetUpdateDatetime()
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
