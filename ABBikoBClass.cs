// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ備考マスタビジネスクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2018/05/07　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2018/05/07   000000      【AB27002】新規作成（石合）
// * 2023/10/20   000001      【AB-0840-1】送付先管理項目追加(早崎)
// ************************************************************************************************

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Densan.Reams.AB.AB000BB
{

    /// <summary>
/// ＡＢ備考マスタビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABBikoBClass
    {

        #region メンバー変数

        // メンバー変数
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス

        private string m_strSelectSQL;                                                // SELECT用SQL
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDeleteSQL;                                                // 物理削除用SQL
        private string m_strLogicalDeleteRecoverSQL;                                  // 論理削除・回復用SQL
        private UFParameterCollectionClass m_cfSelectParamCollection;                 // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertParamCollection;                 // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateParamCollection;                 // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteParamCollection;                 // 物理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfLogicalDeleteRecoverParamCollection;   // 論理削除・回復用パラメータコレクション

        private bool m_blnIsCreateSelectSQL;                                       // SELECT用SQL作成済みフラグ
        private bool m_blnIsCreateInsertSQL;                                       // INSERT用SQL作成済みフラグ
        private bool m_blnIsCreateUpdateSQL;                                       // UPDATE用SQL作成済みフラグ
        private bool m_blnIsCreateDeleteSQL;                                       // 物理削除用SQL作成済みフラグ
        private bool m_blnIsCreateLogicalDeleteRecoverSQL;                         // 論理削除・回復用SQL作成済みフラグ

        private DataSet m_csDataSchema;                                               // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABBikoBClass";                        // クラス名

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private static readonly string SQL_SAKUJOFG = string.Format("{0} = '0'", ABBikoEntity.SAKUJOFG);

        #endregion

        #region プロパティー

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        public ABBikoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメーター変数の初期化
            m_strSelectSQL = string.Empty;
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDeleteSQL = string.Empty;
            m_strLogicalDeleteRecoverSQL = string.Empty;
            m_cfSelectParamCollection = (object)null;
            m_cfInsertParamCollection = (object)null;
            m_cfUpdateParamCollection = (object)null;
            m_cfDeleteParamCollection = (object)null;
            m_cfLogicalDeleteRecoverParamCollection = (object)null;

            // SQL作成済みフラグの初期化
            m_blnIsCreateSelectSQL = false;
            m_blnIsCreateInsertSQL = false;
            m_blnIsCreateUpdateSQL = false;
            m_blnIsCreateDeleteSQL = false;
            m_blnIsCreateLogicalDeleteRecoverSQL = false;

            // スキーマ保管用データセットの初期化
            m_csDataSchema = null;

        }

        #endregion

        #region メソッド

        #region GetTableSchema

        /// <summary>
    /// GetTableSchema
    /// </summary>
    /// <returns>テーブルスキーマ</returns>
    /// <remarks></remarks>
        public DataSet GetTableSchema()
        {

            StringBuilder csSQL;
            DataSet csBikoEntity;

            try
            {

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // SELECT区の生成
                csSQL.Append(CreateSelect());

                // FROM区の生成
                csSQL.AppendFormat(" FROM {0}", ABBikoEntity.TABLE_NAME);

                // スキーマの取得
                csBikoEntity = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABBikoEntity.TABLE_NAME, false);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csBikoEntity;

        }

        #endregion

        #region Select

        /// <summary>
    /// Select
    /// </summary>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>全件抽出</remarks>
        public DataSet Select()
        {
            return Select(false);
        }

        /// <summary>
    /// Select
    /// </summary>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>全件抽出</remarks>
        public DataSet Select(bool blnSakujoFG)
        {
            if (blnSakujoFG == true)
            {
                return Select(string.Empty);
            }
            else
            {
                return Select(SQL_SAKUJOFG);
            }
        }

        /// <summary>
    /// Select
    /// </summary>
    /// <param name="strWhere">SQL文</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        private DataSet Select(string strWhere)
        {
            return Select(strWhere, new UFParameterCollectionClass());
        }

        /// <summary>
    /// Select
    /// </summary>
    /// <param name="strWhere">SQL文</param>
    /// <param name="cfParamCollection">パラメーターコレクション</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        private DataSet Select(string strWhere, UFParameterCollectionClass cfParamCollection)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            string strSQL;
            DataSet csBikoEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateSelectSQL == false)
                {

                    CreateSelectSQL();

                    m_blnIsCreateSelectSQL = true;
                }

                else
                {
                    // noop
                }

                // WHERE区の作成
                if (strWhere.Trim().RLength > 0)
                {
                    strSQL = string.Format(m_strSelectSQL, string.Concat(" WHERE ", strWhere));
                }
                else
                {
                    strSQL = string.Format(m_strSelectSQL, string.Empty);
                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】");

                // SQLの実行 DataSetの取得
                csBikoEntity = m_csDataSchema.Clone();
                csBikoEntity = m_cfRdbClass.GetDataSet(strSQL, csBikoEntity, ABBikoEntity.TABLE_NAME, cfParamCollection, false);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 抽出結果DataSetの返信
            return csBikoEntity;

        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strBikoKbn">備考区分</param>
    /// <param name="strDataKey1">データキー１</param>
    /// <param name="strDataKey2">データキー２</param>
    /// <param name="strDataKey3">データキー３</param>
    /// <param name="strDataKey4">データキー４</param>
    /// <param name="strDataKey5">データキー５</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strBikoKbn, string strDataKey1, string strDataKey2, string strDataKey3, string strDataKey4, string strDataKey5)
        {
            return SelectByKey(strBikoKbn, strDataKey1, strDataKey2, strDataKey3, strDataKey4, strDataKey5, false);
        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strBikoKbn">備考区分</param>
    /// <param name="strDataKey1">データキー１</param>
    /// <param name="strDataKey2">データキー２</param>
    /// <param name="strDataKey3">データキー３</param>
    /// <param name="strDataKey4">データキー４</param>
    /// <param name="strDataKey5">データキー５</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strBikoKbn, string strDataKey1, string strDataKey2, string strDataKey3, string strDataKey4, string strDataKey5, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csBikoEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 備考区分
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.PARAM_BIKOKBN);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_BIKOKBN;
                cfParam.Value = strBikoKbn;
                m_cfSelectParamCollection.Add(cfParam);

                // データキー１
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.PARAM_DATAKEY1);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY1;
                cfParam.Value = strDataKey1;
                m_cfSelectParamCollection.Add(cfParam);

                // データキー２
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.PARAM_DATAKEY2);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY2;
                cfParam.Value = strDataKey2;
                m_cfSelectParamCollection.Add(cfParam);

                // データキー３
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.PARAM_DATAKEY3);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY3;
                cfParam.Value = strDataKey3;
                m_cfSelectParamCollection.Add(cfParam);

                // データキー４
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.PARAM_DATAKEY4);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY4;
                cfParam.Value = strDataKey4;
                m_cfSelectParamCollection.Add(cfParam);

                // データキー５
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.PARAM_DATAKEY5);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY5;
                cfParam.Value = strDataKey5;
                m_cfSelectParamCollection.Add(cfParam);

                // 削除フラグ
                if (blnSakujoFG == true)
                {
                }
                // noop
                else
                {
                    csSQL.AppendFormat("AND {0}", SQL_SAKUJOFG);

                }

                // 抽出処理を実行
                csBikoEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 抽出結果DataSetの返信
            return csBikoEntity;

        }

        #endregion

        #region CreateSelectSQL

        /// <summary>
    /// CreateSelectSQL
    /// </summary>
    /// <remarks></remarks>
        private void CreateSelectSQL()
        {

            StringBuilder csSQL;

            try
            {

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // SELECT区の生成
                csSQL.Append(CreateSelect());

                // FROM区の生成
                csSQL.AppendFormat(" FROM {0}", ABBikoEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABBikoEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABBikoEntity.BIKOKBN);
                csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY1);
                csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY2);
                csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY3);
                csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY4);
                csSQL.AppendFormat(" {0} ", ABBikoEntity.DATAKEY5);

                // メンバー変数に設定
                m_strSelectSQL = csSQL.ToString();
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region CreateSelect

        /// <summary>
    /// CreateSelect
    /// </summary>
    /// <returns>SELECT区</returns>
    /// <remarks></remarks>
        private string CreateSelect()
        {

            StringBuilder csSQL;

            try
            {

                csSQL = new StringBuilder();


                csSQL.Append("SELECT ");
                csSQL.Append(ABBikoEntity.SHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABBikoEntity.KYUSHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABBikoEntity.BIKOKBN);
                csSQL.AppendFormat(", {0}", ABBikoEntity.DATAKEY1);
                csSQL.AppendFormat(", {0}", ABBikoEntity.DATAKEY2);
                csSQL.AppendFormat(", {0}", ABBikoEntity.DATAKEY3);
                csSQL.AppendFormat(", {0}", ABBikoEntity.DATAKEY4);
                csSQL.AppendFormat(", {0}", ABBikoEntity.DATAKEY5);
                csSQL.AppendFormat(", {0}", ABBikoEntity.BIKO);
                csSQL.AppendFormat(", {0}", ABBikoEntity.RESERVE);
                csSQL.AppendFormat(", {0}", ABBikoEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}", ABBikoEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}", ABBikoEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}", ABBikoEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}", ABBikoEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}", ABBikoEntity.KOSHINNICHIJI);

                csSQL.AppendFormat(", {0}", ABBikoEntity.KOSHINUSER);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csSQL.ToString();

        }

        #endregion

        #region Insert

        /// <summary>
    /// Insert
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <returns>更新件数</returns>
    /// <remarks></remarks>
        public int Insert(DataRow csDataRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            int intKoshinCount;
            string strUpdateDatetime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateInsertSQL == false)
                {

                    CreateInsertSQL(csDataRow);

                    m_blnIsCreateInsertSQL = true;
                }

                else
                {
                    // noop
                }

                // 更新日時を取得
                strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId;          // 端末ＩＤ
                                                                                             // *履歴番号 000001 2023/10/20 修正開始
                                                                                             // csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF                         
                if (new string(csDataRow(ABBikoEntity.SAKUJOFG).ToString ?? new char[0]) == "")          // 削除フラグ
                {
                    csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF;
                }
                // *履歴番号 000001 2023/10/20 修正終了
                csDataRow(ABBikoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;                   // 更新カウンター
                csDataRow(ABBikoEntity.SAKUSEINICHIJI) = strUpdateDatetime;                  // 作成日時
                csDataRow(ABBikoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;           // 作成ユーザー
                csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime;                   // 更新日時
                csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;            // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertParamCollection)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertParamCollection) + "】");

                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 更新件数の返信
            return intKoshinCount;

        }

        #endregion

        #region CreateInsertSQL

        /// <summary>
    /// CreateInsertSQL
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <remarks></remarks>
        private void CreateInsertSQL(DataRow csDataRow)
        {

            ArrayList csColumnList;
            ArrayList csParamList;
            UFParameterClass cfParam;
            string strParamName;

            try
            {

                csColumnList = new ArrayList();
                csParamList = new ArrayList();

                m_cfInsertParamCollection = new UFParameterCollectionClass();

                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {

                    strParamName = string.Concat(ABBikoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csColumnList.Add(csDataColumn.ColumnName);
                    csParamList.Add(strParamName);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfInsertParamCollection.Add(cfParam);

                }

                m_strInsertSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ABBikoEntity.TABLE_NAME, string.Join(',', (string[])csColumnList.ToArray(typeof(string))), string.Join(',', (string[])csParamList.ToArray(typeof(string))));
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region Update

        /// <summary>
    /// Update
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <returns>更新件数</returns>
    /// <remarks></remarks>
        public int Update(DataRow csDataRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            int intKoshinCount;
            string strUpdateDatetime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateUpdateSQL == false)
                {

                    CreateUpdateSQL(csDataRow);

                    m_blnIsCreateUpdateSQL = true;
                }

                else
                {
                    // noop
                }

                // 更新日時を取得
                strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                          // 端末ＩＤ
                csDataRow(ABBikoEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABBikoEntity.KOSHINCOUNTER)) + 1m;           // 更新カウンタ
                csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime;                                                   // 更新日時
                csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                            // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfUpdateParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();

                    }

                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateParamCollection) + "】");

                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 更新件数の返信
            return intKoshinCount;

        }

        #endregion

        #region CreateUpdateSQL

        /// <summary>
    /// CreateUpdateSQL
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <remarks></remarks>
        private void CreateUpdateSQL(DataRow csDataRow)
        {

            ArrayList csParamList;
            UFParameterClass cfParam;
            string strParamName;
            StringBuilder csWhere;

            try
            {

                csParamList = new ArrayList();

                m_cfUpdateParamCollection = new UFParameterCollectionClass();

                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {

                    strParamName = string.Concat(ABBikoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csParamList.Add(string.Format("{0} = {1}", csDataColumn.ColumnName, strParamName));

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfUpdateParamCollection.Add(cfParam);

                }

                m_strUpdateSQL = string.Format("UPDATE {0} SET {1} ", ABBikoEntity.TABLE_NAME, string.Join(',', (string[])csParamList.ToArray(typeof(string))));

                csWhere = new StringBuilder(256);
                csWhere.Append("WHERE ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER);
                m_strUpdateSQL += csWhere.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateParamCollection.Add(cfParam);
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region Delete

        /// <summary>
    /// Delete
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <returns>更新件数</returns>
    /// <remarks></remarks>
        public int Delete(DataRow csDataRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            int intKoshinCount;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateDeleteSQL == false)
                {

                    CreateDeleteSQL(csDataRow);

                    m_blnIsCreateDeleteSQL = true;
                }

                else
                {
                    // noop
                }

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfDeleteParamCollection)
                    // キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteParamCollection) + "】");

                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 更新件数の返信
            return intKoshinCount;

        }

        #endregion

        #region CreateDeleteSQL

        /// <summary>
    /// CreateDeleteSQL
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <remarks></remarks>
        private void CreateDeleteSQL(DataRow csDataRow)
        {

            UFParameterClass cfParam;
            StringBuilder csSQL;

            try
            {

                m_cfDeleteParamCollection = new UFParameterCollectionClass();

                csSQL = new StringBuilder(256);
                csSQL.AppendFormat("DELETE FROM {0} ", ABBikoEntity.TABLE_NAME);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER);
                m_strDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER;
                m_cfDeleteParamCollection.Add(cfParam);
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region LogicalDelete

        /// <summary>
    /// LogicalDelete
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <returns>更新件数</returns>
    /// <remarks></remarks>
        public int LogicalDelete(DataRow csDataRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            int intKoshinCount;
            string strUpdateDatetime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateLogicalDeleteRecoverSQL == false)
                {

                    CreateLogicalDeleteRecoverSQL(csDataRow);

                    m_blnIsCreateLogicalDeleteRecoverSQL = true;
                }

                else
                {
                    // noop
                }

                // 更新日時を取得
                strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                          // 端末ＩＤ
                csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_ON;                                                              // 削除フラグ
                                                                                                                             // *履歴番号 000001 2023/10/20 修正開始
                                                                                                                             // csDataRow(ABBikoEntity.KOSHINCOUNTER) = CType(csDataRow(ABBikoEntity.KOSHINCOUNTER), Decimal) + 1           ' 更新カウンタ
                csDataRow(ABBikoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;                                                  // 更新カウンタ 
                                                                                                                            // *履歴番号 000001 2023/10/20 修正終了
                csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime;                                                   // 更新日時
                csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                            // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteRecoverParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();

                    }

                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】");

                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 更新件数の返信
            return intKoshinCount;

        }

        #endregion

        #region Recover

        /// <summary>
    /// Recover
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <returns>更新件数</returns>
    /// <remarks></remarks>
        public int Recover(DataRow csDataRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            int intKoshinCount;
            string strUpdateDatetime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateLogicalDeleteRecoverSQL == false)
                {

                    CreateLogicalDeleteRecoverSQL(csDataRow);

                    m_blnIsCreateLogicalDeleteRecoverSQL = true;
                }

                else
                {
                    // noop
                }

                // 更新日時を取得
                strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                          // 端末ＩＤ
                csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF;                                                             // 削除フラグ
                csDataRow(ABBikoEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABBikoEntity.KOSHINCOUNTER)) + 1m;           // 更新カウンタ
                csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime;                                                   // 更新日時
                csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                            // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteRecoverParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();

                    }

                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】");

                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            // 更新件数の返信
            return intKoshinCount;

        }

        #endregion

        #region CreateLogicalDeleteRecoverSQL

        /// <summary>
    /// CreateLogicalDeleteRecoverSQL
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <remarks></remarks>
        private void CreateLogicalDeleteRecoverSQL(DataRow csDataRow)
        {

            UFParameterClass cfParam;
            StringBuilder csSQL;

            try
            {

                m_cfLogicalDeleteRecoverParamCollection = new UFParameterCollectionClass();

                csSQL = new StringBuilder(256);
                csSQL.AppendFormat("UPDATE {0} ", ABBikoEntity.TABLE_NAME);
                csSQL.Append("SET ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.TANMATSUID, ABBikoEntity.PARAM_TANMATSUID);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.SAKUJOFG, ABBikoEntity.PARAM_SAKUJOFG);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.PARAM_KOSHINCOUNTER);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINNICHIJI, ABBikoEntity.PARAM_KOSHINNICHIJI);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINUSER, ABBikoEntity.PARAM_KOSHINUSER);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER);
                m_strLogicalDeleteRecoverSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_TANMATSUID;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_SAKUJOFG;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINCOUNTER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINNICHIJI;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINUSER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #endregion

    }
}