// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ共通番号累積_標準マスタＤＡ(ABMyNumberRuisekiHyojunBClass)
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
/// ＡＢ共通番号累積マスタＤＡ
/// </summary>
/// <remarks></remarks>
    public class ABMyNumberRuisekiHyojunBClass
    {

        #region メンバー変数

        // メンバー変数
        private UFLogClass m_cfLogClass;                                      // ログ出力クラス
        private UFControlData m_cfControlData;                                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                      // ＲＤＢクラス

        private string m_strSelectSQL;                                        // SELECT用SQL
        private string m_strInsertSQL;                                        // INSERT用SQL
        private UFParameterCollectionClass m_cfSelectParamCollection;         // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertParamCollection;         // INSERT用パラメータコレクション

        private bool m_blnIsCreateSelectSQL;                               // SELECT用SQL作成済みフラグ
        private bool m_blnIsCreateInsertSQL;                               // INSERT用SQL作成済みフラグ

        private DataSet m_csDataSchema;                                       // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMyNumberRuisekiHyojunBClass";     // クラス名

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

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
        public ABMyNumberRuisekiHyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


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
            m_cfSelectParamCollection = null;
            m_cfInsertParamCollection = null;

            // SQL作成済みフラグの初期化
            m_blnIsCreateSelectSQL = false;
            m_blnIsCreateInsertSQL = false;

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
            return Select(string.Empty);
        }

        // ''' <summary>
        // ''' Select
        // ''' </summary>
        // ''' <param name="strWhere">SQL文</param>
        // ''' <returns>抽出結果DataSet</returns>
        // ''' <remarks></remarks>
        // Private Overloads Function [Select](ByVal strWhere As String) As DataSet
        /// <summary>
    /// Select
    /// </summary>
    /// <param name="strWhere">SQL文</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet Select(string strWhere)
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
        public DataSet Select(string strWhere, UFParameterCollectionClass cfParamCollection)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            string strSQL;
            DataSet csMyNumberRuisekiHyojunEntity;

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
                csMyNumberRuisekiHyojunEntity = m_csDataSchema.Clone();
                csMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberRuisekiHyojunEntity, ABMyNumberRuisekiHyojunEntity.TABLE_NAME, cfParamCollection, false);

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
            return csMyNumberRuisekiHyojunEntity;

        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strShoriNichiji">処理日時</param>
    /// <param name="strZengoKB">前後区分</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strJuminCd, string strMyNumber, string strShoriNichiji, string strZengoKB)



        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberRuisekiHyojunEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.JUMINCD, ABMyNumberRuisekiHyojunEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
                m_cfSelectParamCollection.Add(cfParam);

                // 共通番号
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.MYNUMBER, ABMyNumberRuisekiHyojunEntity.PARAM_MYNUMBER);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_MYNUMBER;
                cfParam.Value = strMyNumber.RPadRight(13);
                m_cfSelectParamCollection.Add(cfParam);

                // 処理日時
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI, ABMyNumberRuisekiHyojunEntity.PARAM_SHORINICHIJI);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_SHORINICHIJI;
                cfParam.Value = strShoriNichiji;
                m_cfSelectParamCollection.Add(cfParam);

                // 前後区分
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.ZENGOKB, ABMyNumberRuisekiHyojunEntity.PARAM_ZENGOKB);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_ZENGOKB;
                cfParam.Value = strZengoKB;

                m_cfSelectParamCollection.Add(cfParam);

                // 抽出処理を実行
                csMyNumberRuisekiHyojunEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberRuisekiHyojunEntity;

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
                csSQL.AppendFormat(" FROM {0}", ABMyNumberRuisekiHyojunEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberRuisekiHyojunEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.JUMINCD);
                csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.MYNUMBER);
                csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI);
                csSQL.AppendFormat(" {0}", ABMyNumberRuisekiHyojunEntity.ZENGOKB);

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
                csSQL.Append(ABMyNumberRuisekiHyojunEntity.JUMINCD);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KYUSHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.MYNUMBER);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.ZENGOKB);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.BANGOHOKOSHINKB);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE1);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE2);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE3);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE4);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE5);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINNICHIJI);

                csSQL.AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINUSER);
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
                csDataRow[ABMyNumberRuisekiHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow[ABMyNumberRuisekiHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow[ABMyNumberRuisekiHyojunEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;              // 更新カウンター
                csDataRow[ABMyNumberRuisekiHyojunEntity.SAKUSEINICHIJI] = strUpdateDatetime;             // 作成日時
                csDataRow[ABMyNumberRuisekiHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow[ABMyNumberRuisekiHyojunEntity.KOSHINNICHIJI] = strUpdateDatetime;              // 更新日時
                csDataRow[ABMyNumberRuisekiHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;       // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertParamCollection)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABMyNumberRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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

                    strParamName = string.Concat(ABMyNumberRuisekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csColumnList.Add(csDataColumn.ColumnName);
                    csParamList.Add(strParamName);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfInsertParamCollection.Add(cfParam);

                }

                m_strInsertSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ABMyNumberRuisekiHyojunEntity.TABLE_NAME, string.Join(',', (string[])csColumnList.ToArray(typeof(string))), string.Join(',', (string[])csParamList.ToArray(typeof(string))));
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
