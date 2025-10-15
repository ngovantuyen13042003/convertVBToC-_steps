// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ共通番号_標準マスタＤＡ(ABMyNumberHyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/10/04　下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// *
// ************************************************************************************************

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    /// <summary>
/// ＡＢ共通番号マスタＤＡ
/// </summary>
/// <remarks></remarks>
    public class ABMyNumberHyojunBClass
    {

        #region メンバー変数

        // メンバー変数
        private UFLogClass m_cfLogClass;                                      // ログ出力クラス
        private UFControlData m_cfControlData;                                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                      // ＲＤＢクラス

        private string m_strSelectSQL;                                        // SELECT用SQL
        private string m_strInsertSQL;                                        // INSERT用SQL
        private string m_strUpdateSQL;                                        // UPDATE用SQL
        private string m_strDeleteSQL;                                        // 物理削除用SQL
        private string m_strLogicalDeleteSQL;                                 // 論理削除用SQL
        private string m_strSelectConsentSQL;                                 // SELECTCONSENT用SQL
        private UFParameterCollectionClass m_cfSelectParamCollection;         // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertParamCollection;         // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateParamCollection;         // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteParamCollection;         // 物理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfLogicalDeleteParamCollection;  // 論理削除用パラメータコレクション

        private bool m_blnIsCreateSelectSQL;                               // SELECT用SQL作成済みフラグ
        private bool m_blnIsCreateInsertSQL;                               // INSERT用SQL作成済みフラグ
        private bool m_blnIsCreateUpdateSQL;                               // UPDATE用SQL作成済みフラグ
        private bool m_blnIsCreateDeleteSQL;                               // 物理削除用SQL作成済みフラグ
        private bool m_blnIsCreateLogicalDeleteSQL;                        // 論理削除用SQL作成済みフラグ

        private DataSet m_csDataSchema;                                       // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMyNumberHyojunBClass";            // クラス名

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;
        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";
        private static readonly string SQL_SAKUJOFG = string.Format("{0} = '0'", ABMyNumberHyojunEntity.SAKUJOFG);

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
        public ABMyNumberHyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


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
            m_strLogicalDeleteSQL = string.Empty;
            m_strSelectConsentSQL = string.Empty;
            m_cfSelectParamCollection = null;
            m_cfInsertParamCollection = null;
            m_cfUpdateParamCollection = null;
            m_cfDeleteParamCollection = null;
            m_cfLogicalDeleteParamCollection = null;

            // SQL作成済みフラグの初期化
            m_blnIsCreateSelectSQL = false;
            m_blnIsCreateInsertSQL = false;
            m_blnIsCreateUpdateSQL = false;
            m_blnIsCreateDeleteSQL = false;
            m_blnIsCreateLogicalDeleteSQL = false;

            // スキーマ保管用データセットの初期化
            m_csDataSchema = null;

        }

        #endregion

        #region メソッド

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
            DataSet csMyNumberHyojunEntity;

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
                if (strWhere.Trim().RLength() > 0)
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
                csMyNumberHyojunEntity = m_csDataSchema.Clone();
                csMyNumberHyojunEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberHyojunEntity, ABMyNumberHyojunEntity.TABLE_NAME, cfParamCollection, false);

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
            return csMyNumberHyojunEntity;

        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strJuminCd, string strMyNumber)

        {
            return SelectByKey(strJuminCd, strMyNumber, false);
        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strJuminCd, string strMyNumber, bool blnSakujoFG)


        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberHyojunEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.JUMINCD, ABMyNumberHyojunEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
                m_cfSelectParamCollection.Add(cfParam);

                // 共通番号
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.MYNUMBER, ABMyNumberHyojunEntity.PARAM_MYNUMBER);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_MYNUMBER;
                cfParam.Value = strMyNumber.RPadRight(13);
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
                csMyNumberHyojunEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberHyojunEntity;

        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <returns>抽出結果DataSet</returns>
        public DataSet SelectByJuminCd(string strJuminCd)
        {
            return SelectByJuminCd(strJuminCd, false);
        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
        public DataSet SelectByJuminCd(string strJuminCd, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberHyojunEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.JUMINCD, ABMyNumberHyojunEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
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
                csMyNumberHyojunEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberHyojunEntity;

        }

        /// <summary>
    /// SelectByMyNumber
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>抽出結果DataSet</returns>
        public DataSet SelectByMyNumber(string strMyNumber)
        {
            return SelectByMyNumber(strMyNumber, false);
        }

        /// <summary>
    /// SelectByMyNumber
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
        public DataSet SelectByMyNumber(string strMyNumber, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberHyojunEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 共通番号
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.MYNUMBER, ABMyNumberHyojunEntity.PARAM_MYNUMBER);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_MYNUMBER;
                cfParam.Value = strMyNumber.RPadRight(13);
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
                csMyNumberHyojunEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberHyojunEntity;

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
                csSQL.AppendFormat(" FROM {0}", ABMyNumberHyojunEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberHyojunEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABMyNumberHyojunEntity.JUMINCD);
                csSQL.AppendFormat(" {0}", ABMyNumberHyojunEntity.MYNUMBER);

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
                csSQL.Append(ABMyNumberHyojunEntity.JUMINCD);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.MYNUMBER);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.BANGOHOKOSHINKB);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.RESERVE1);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.RESERVE2);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.RESERVE3);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.RESERVE4);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.RESERVE5);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.KOSHINNICHIJI);

                csSQL.AppendFormat(", {0}", ABMyNumberHyojunEntity.KOSHINUSER);
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
                csDataRow[ABMyNumberHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow[ABMyNumberHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow[ABMyNumberHyojunEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;              // 更新カウンター
                csDataRow[ABMyNumberHyojunEntity.SAKUSEINICHIJI] = strUpdateDatetime;             // 作成日時
                csDataRow[ABMyNumberHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow[ABMyNumberHyojunEntity.KOSHINNICHIJI] = strUpdateDatetime;              // 更新日時
                csDataRow[ABMyNumberHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;       // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertParamCollection)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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

                    strParamName = string.Concat(ABMyNumberHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csColumnList.Add(csDataColumn.ColumnName);
                    csParamList.Add(strParamName);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfInsertParamCollection.Add(cfParam);

                }

                m_strInsertSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ABMyNumberHyojunEntity.TABLE_NAME, string.Join(',', (string[])csColumnList.ToArray(typeof(string))), string.Join(',', (string[])csParamList.ToArray(typeof(string))));
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
                csDataRow[ABMyNumberHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                      // 端末ＩＤ
                csDataRow[ABMyNumberHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABMyNumberHyojunEntity.KOSHINCOUNTER]) + 1m;   // 更新カウンタ
                csDataRow[ABMyNumberHyojunEntity.KOSHINNICHIJI] = strUpdateDatetime;                                               // 更新日時
                csDataRow[ABMyNumberHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfUpdateParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABMyNumberHyojunEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();

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

                    strParamName = string.Concat(ABMyNumberHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csParamList.Add(string.Format("{0} = {1}", csDataColumn.ColumnName, strParamName));

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfUpdateParamCollection.Add(cfParam);

                }

                m_strUpdateSQL = string.Format("UPDATE {0} SET {1} ", ABMyNumberHyojunEntity.TABLE_NAME, string.Join(',', (string[])csParamList.ToArray(typeof(string))));

                csWhere = new StringBuilder(256);
                csWhere.Append("WHERE ");
                csWhere.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.JUMINCD, ABMyNumberHyojunEntity.KEY_JUMINCD);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.MYNUMBER, ABMyNumberHyojunEntity.KEY_MYNUMBER);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1}", ABMyNumberHyojunEntity.KOSHINCOUNTER, ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER);
                m_strUpdateSQL += csWhere.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_JUMINCD;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_MYNUMBER;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER;
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
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();

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
                csSQL.AppendFormat("DELETE FROM {0} ", ABMyNumberHyojunEntity.TABLE_NAME);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.JUMINCD, ABMyNumberHyojunEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.MYNUMBER, ABMyNumberHyojunEntity.KEY_MYNUMBER);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABMyNumberHyojunEntity.KOSHINCOUNTER, ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER);
                m_strDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_JUMINCD;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_MYNUMBER;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER;
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
                if (m_blnIsCreateLogicalDeleteSQL == false)
                {

                    CreateLogicalDeleteSQL(csDataRow);

                    m_blnIsCreateLogicalDeleteSQL = true;
                }

                else
                {
                    // noop
                }

                // 更新日時を取得
                strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow[ABMyNumberHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                      // 端末ＩＤ
                csDataRow[ABMyNumberHyojunEntity.SAKUJOFG] = SAKUJOFG_ON;                                                          // 削除フラグ
                csDataRow[ABMyNumberHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABMyNumberHyojunEntity.KOSHINCOUNTER]) + 1m;   // 更新カウンタ
                csDataRow[ABMyNumberHyojunEntity.KOSHINNICHIJI] = strUpdateDatetime;                                               // 更新日時
                csDataRow[ABMyNumberHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABMyNumberHyojunEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();

                    }

                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteSQL, m_cfLogicalDeleteParamCollection) + "】");




                // SQLの実行
                intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteSQL, m_cfLogicalDeleteParamCollection);

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

        #region CreateLogicalDeleteSQL

        /// <summary>
    /// CreateLogicalDeleteSQL
    /// </summary>
    /// <param name="csDataRow">更新対象DataRow</param>
    /// <remarks></remarks>
        private void CreateLogicalDeleteSQL(DataRow csDataRow)
        {

            UFParameterClass cfParam;
            StringBuilder csSQL;

            try
            {

                m_cfLogicalDeleteParamCollection = new UFParameterCollectionClass();

                csSQL = new StringBuilder(256);
                csSQL.AppendFormat("UPDATE {0} ", ABMyNumberHyojunEntity.TABLE_NAME);
                csSQL.Append("SET ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.TANMATSUID, ABMyNumberHyojunEntity.PARAM_TANMATSUID);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.SAKUJOFG, ABMyNumberHyojunEntity.PARAM_SAKUJOFG);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.KOSHINCOUNTER, ABMyNumberHyojunEntity.PARAM_KOSHINCOUNTER);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.KOSHINNICHIJI, ABMyNumberHyojunEntity.PARAM_KOSHINNICHIJI);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.KOSHINUSER, ABMyNumberHyojunEntity.PARAM_KOSHINUSER);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.JUMINCD, ABMyNumberHyojunEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberHyojunEntity.MYNUMBER, ABMyNumberHyojunEntity.KEY_MYNUMBER);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABMyNumberHyojunEntity.KOSHINCOUNTER, ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER);
                m_strLogicalDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_TANMATSUID;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_SAKUJOFG;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.PARAM_KOSHINUSER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_JUMINCD;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_MYNUMBER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);
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
