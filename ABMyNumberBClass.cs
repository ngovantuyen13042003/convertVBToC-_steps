// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ共通番号マスタＤＡ(ABMyNumberBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2014/04/30　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2015/09/24   000001      速度改善のため、削除フラグの指定方法を変更（石合）※規定値以外は設定されないことが大前提
// * 2016/01/27   000002      公表の同意取得用メソッド追加（岩下）
// * 2023/10/25   000003     【AB-1000-1】個人制御同一個人番号者対応(下村)
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
/// ＡＢ共通番号マスタＤＡ
/// </summary>
/// <remarks></remarks>
    public class ABMyNumberBClass
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
                                                                              // *履歴番号 000002 2016/01/27 追加開始
        private string m_strSelectConsentSQL;                                 // SELECTCONSENT用SQL
                                                                              // *履歴番号 000002 2016/01/27 追加終了
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
                                                                           // *履歴番号 000002 2016/01/27 追加開始
        private bool m_blnIsConsentSelectSQL;                              // 公表の同意SQL作成済みフラグ
                                                                           // *履歴番号 000002 2016/01/27 追加終了

        private DataSet m_csDataSchema;                                       // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMyNumberBClass";            // クラス名

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        // *履歴番号 000001 2015/09/24 修正開始
        // Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0} <> '1'", ABMyNumberEntity.SAKUJOFG)
        private static readonly string SQL_SAKUJOFG = string.Format("{0} = '0'", ABMyNumberEntity.SAKUJOFG);
        // *履歴番号 000001 2015/09/24 修正終了

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
        public ABMyNumberBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


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
            // *履歴番号 000002 2016/01/27 追加開始
            m_strSelectConsentSQL = string.Empty;
            // *履歴番号 000002 2016/01/27 追加開始
            m_cfSelectParamCollection = (object)null;
            m_cfInsertParamCollection = (object)null;
            m_cfUpdateParamCollection = (object)null;
            m_cfDeleteParamCollection = (object)null;
            m_cfLogicalDeleteParamCollection = (object)null;

            // SQL作成済みフラグの初期化
            m_blnIsCreateSelectSQL = false;
            m_blnIsCreateInsertSQL = false;
            m_blnIsCreateUpdateSQL = false;
            m_blnIsCreateDeleteSQL = false;
            m_blnIsCreateLogicalDeleteSQL = false;
            // *履歴番号 000002 2016/01/27 追加開始
            m_blnIsConsentSelectSQL = false;
            // *履歴番号 000002 2016/01/27 追加開始

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
            DataSet csMyNumberEntity;

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
                csMyNumberEntity = m_csDataSchema.Clone();
                csMyNumberEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberEntity, ABMyNumberEntity.TABLE_NAME, cfParamCollection, false);

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
            return csMyNumberEntity;

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
            DataSet csMyNumberEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
                m_cfSelectParamCollection.Add(cfParam);

                // 共通番号
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
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
                csMyNumberEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberEntity;

        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ</remarks>
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
    /// <remarks>直近のみ</remarks>
        public DataSet SelectByJuminCd(string strJuminCd, bool blnSakujoFG)

        {
            return this.SelectByJuminCd(strJuminCd, ABMyNumberEntity.DEFAULT.CKINKB.CKIN, blnSakujoFG);
        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ、履歴を含む指定が可能</remarks>
        public DataSet SelectByJuminCd(string strJuminCd, string strCkinKB)

        {
            return SelectByJuminCd(strJuminCd, strCkinKB, false);
        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ、履歴を含む指定が可能</remarks>
        public DataSet SelectByJuminCd(string strJuminCd, string strCkinKB, bool blnSakujoFG)


        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
                m_cfSelectParamCollection.Add(cfParam);

                // 直近区分
                if (strCkinKB is not null && strCkinKB == ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
                {

                    csSQL.Append("AND ");
                    csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB;
                    cfParam.Value = strCkinKB;
                    m_cfSelectParamCollection.Add(cfParam);
                }

                else
                {
                    // noop
                }

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
                csMyNumberEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberEntity;

        }

        /// <summary>
    /// SelectByMyNumber
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ</remarks>
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
    /// <remarks>直近のみ</remarks>
        public DataSet SelectByMyNumber(string strMyNumber, bool blnSakujoFG)

        {
            return this.SelectByMyNumber(strMyNumber, ABMyNumberEntity.DEFAULT.CKINKB.CKIN, blnSakujoFG);
        }

        /// <summary>
    /// SelectByMyNumber
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ、履歴を含む指定が可能</remarks>
        public DataSet SelectByMyNumber(string strMyNumber, string strCkinKB)

        {
            return SelectByMyNumber(strMyNumber, strCkinKB, false);
        }

        /// <summary>
    /// SelectByMyNumber
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ、履歴を含む指定が可能</remarks>
        public DataSet SelectByMyNumber(string strMyNumber, string strCkinKB, bool blnSakujoFG)


        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 共通番号
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
                cfParam.Value = strMyNumber.RPadRight(13);
                m_cfSelectParamCollection.Add(cfParam);

                // 直近区分
                if (strCkinKB is not null && strCkinKB == ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
                {

                    csSQL.Append("AND ");
                    csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB;
                    cfParam.Value = strCkinKB;
                    m_cfSelectParamCollection.Add(cfParam);
                }

                else
                {
                    // noop
                }

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
                csMyNumberEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberEntity;

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
                csSQL.AppendFormat(" FROM {0}", ABMyNumberEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABMyNumberEntity.JUMINCD);
                csSQL.AppendFormat(" {0}", ABMyNumberEntity.MYNUMBER);

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
                csSQL.Append(ABMyNumberEntity.JUMINCD);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.SHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.KYUSHICHOSONCD);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.MYNUMBER);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.CKINKB);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.IDOKB);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.IDOYMD);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.IDOSHA);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.RESERVE);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.KOSHINNICHIJI);

                csSQL.AppendFormat(", {0}", ABMyNumberEntity.KOSHINUSER);
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
                csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow(ABMyNumberEntity.SAKUJOFG) = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;              // 更新カウンター
                csDataRow(ABMyNumberEntity.SAKUSEINICHIJI) = strUpdateDatetime;             // 作成日時
                csDataRow(ABMyNumberEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime;              // 更新日時
                csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId;       // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertParamCollection)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength)).ToString();

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

                    strParamName = string.Concat(ABMyNumberEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csColumnList.Add(csDataColumn.ColumnName);
                    csParamList.Add(strParamName);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfInsertParamCollection.Add(cfParam);

                }

                m_strInsertSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ABMyNumberEntity.TABLE_NAME, string.Join(',', (string[])csColumnList.ToArray(typeof(string))), string.Join(',', (string[])csParamList.ToArray(typeof(string))));


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
                csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                      // 端末ＩＤ
                csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABMyNumberEntity.KOSHINCOUNTER)) + 1m;   // 更新カウンタ
                csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime;                                               // 更新日時
                csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfUpdateParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABMyNumberEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();

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

                    strParamName = string.Concat(ABMyNumberEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csParamList.Add(string.Format("{0} = {1}", csDataColumn.ColumnName, strParamName));

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfUpdateParamCollection.Add(cfParam);

                }

                m_strUpdateSQL = string.Format("UPDATE {0} SET {1} ", ABMyNumberEntity.TABLE_NAME, string.Join(',', (string[])csParamList.ToArray(typeof(string))));


                csWhere = new StringBuilder(256);
                csWhere.Append("WHERE ");
                csWhere.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER);
                m_strUpdateSQL += csWhere.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER;
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
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();

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
                csSQL.AppendFormat("DELETE FROM {0} ", ABMyNumberEntity.TABLE_NAME);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER);
                m_strDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER;
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
                csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                      // 端末ＩＤ
                csDataRow(ABMyNumberEntity.SAKUJOFG) = SAKUJOFG_ON;                                                          // 削除フラグ
                csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABMyNumberEntity.KOSHINCOUNTER)) + 1m;   // 更新カウンタ
                csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime;                                               // 更新日時
                csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABMyNumberEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();

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
                csSQL.AppendFormat("UPDATE {0} ", ABMyNumberEntity.TABLE_NAME);
                csSQL.Append("SET ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.TANMATSUID, ABMyNumberEntity.PARAM_TANMATSUID);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.SAKUJOFG, ABMyNumberEntity.PARAM_SAKUJOFG);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.PARAM_KOSHINCOUNTER);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINNICHIJI, ABMyNumberEntity.PARAM_KOSHINNICHIJI);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINUSER, ABMyNumberEntity.PARAM_KOSHINUSER);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER);
                m_strLogicalDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_TANMATSUID;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_SAKUJOFG;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINCOUNTER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINNICHIJI;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINUSER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER;
                m_cfLogicalDeleteParamCollection.Add(cfParam);
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        // *履歴番号 000002 2016/01/27 追加開始

        /// <summary>
    /// SelectConsentByJuminCd
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>直近のみ</remarks>
        public DataSet SelectConsentByJuminCd(string strJuminCd, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csMyNumberEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;
                m_cfSelectParamCollection.Add(cfParam);

                // 直近区分
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB;
                cfParam.Value = ABConstClass.MYNUMBER.CHOKKIN;
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
                csMyNumberEntity = SelectConsent(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csMyNumberEntity;

        }

        /// <summary>
    /// SelectConsent
    /// </summary>
    /// <param name="strWhere">SQL文</param>
    /// <param name="cfParamCollection">パラメーターコレクション</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        private DataSet SelectConsent(string strWhere, UFParameterCollectionClass cfParamCollection)

        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            string strSQL;
            DataSet csMyNumberEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsConsentSelectSQL == false)
                {

                    CreateSelectConcentSQL();

                    m_blnIsConsentSelectSQL = true;
                }

                else
                {
                    // noop
                }

                // WHERE区の作成
                if (strWhere.Trim().RLength > 0)
                {
                    strSQL = string.Format(m_strSelectConsentSQL, string.Concat(" WHERE ", strWhere));
                }
                else
                {
                    strSQL = string.Format(m_strSelectConsentSQL, string.Empty);
                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】");




                // SQLの実行 DataSetの取得
                csMyNumberEntity = m_csDataSchema.Clone();
                csMyNumberEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberEntity, ABMyNumberEntity.TABLE_NAME, cfParamCollection, false);

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
            return csMyNumberEntity;

        }

        /// <summary>
    /// CreateSelectConcentSQL
    /// </summary>
    /// <remarks></remarks>
        private void CreateSelectConcentSQL()
        {

            StringBuilder csSQL;

            try
            {

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // SELECT区の生成
                csSQL.Append("SELECT ");
                csSQL.Append(ABMyNumberEntity.JUMINCD);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.MYNUMBER);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.CKINKB);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.RESERVE);
                csSQL.AppendFormat(", {0}", ABMyNumberEntity.SAKUJOFG);

                // FROM区の生成
                csSQL.AppendFormat(" FROM {0}", ABMyNumberEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABMyNumberEntity.JUMINCD);
                csSQL.AppendFormat(" {0}", ABMyNumberEntity.MYNUMBER);

                // メンバー変数に設定
                m_strSelectConsentSQL = csSQL.ToString();
            }

            catch (Exception csExp)
            {
                throw;
            }

        }
        // *履歴番号 000002 2016/01/27 追加終了

        // ************************************************************************************************
        // * メソッド名      同一人取得
        // * 
        // * 構文            Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet
        // * 
        // * 機能　　        同一個人法人番号のデータを取得する
        // * 
        // * 引数            住民コード配列  : a_strJuminCD()
        // * 
        // * 戻り値          DataSet
        // ************************************************************************************************
        public DataSet GetDoitsunin(string[] a_strJuminCD)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            StringBuilder csSQL;
            UFParameterClass cfParameter;
            UFParameterCollectionClass cfParameterCollection;
            string strParameterName;
            string strSQL;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                csSQL = new StringBuilder();
                cfParameterCollection = new UFParameterCollectionClass();


                csSQL.Append("SELECT * FROM ");
                csSQL.Append(ABMyNumberEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.Append(ABMyNumberEntity.MYNUMBER);
                csSQL.Append(" IN (SELECT ");
                csSQL.Append(ABMyNumberEntity.MYNUMBER);
                csSQL.Append(" FROM ");
                csSQL.Append(ABMyNumberEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.Append(ABMyNumberEntity.JUMINCD);
                csSQL.Append(" IN (");

                for (int i = 0, loopTo = a_strJuminCD.Length - 1; i <= loopTo; i++)
                {

                    // -----------------------------------------------------------------------------
                    // 住民コード
                    strParameterName = ABMyNumberEntity.KEY_JUMINCD + i.ToString();

                    if (i > 0)
                    {
                        csSQL.AppendFormat(", {0}", strParameterName);
                    }
                    else
                    {
                        csSQL.Append(strParameterName);
                    }

                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = strParameterName;
                    cfParameter.Value = a_strJuminCD[i];
                    cfParameterCollection.Add(cfParameter);
                    // -----------------------------------------------------------------------------

                }

                csSQL.Append(")");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.SAKUJOFG);
                csSQL.Append(" <> '1'");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.CKINKB);
                csSQL.Append(" = '1')");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.SAKUJOFG);
                csSQL.Append(" <> '1'");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.CKINKB);

                csSQL.Append(" = '1'");
                strSQL = csSQL.ToString();
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParameterCollection) + "】");

                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(strSQL, ABMyNumberEntity.TABLE_NAME, cfParameterCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException csAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + csAppExp.Message + "】");
                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return csDataSet;

        }

        // ************************************************************************************************
        // * メソッド名      同一人取得
        // * 
        // * 構文            Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet
        // * 
        // * 機能　　        同一個人法人番号のデータを取得する
        // * 
        // * 引数            住民コード  : strJuminCD
        // * 
        // * 戻り値          DataSet
        // ************************************************************************************************
        public DataSet GetDoitsunin(string strJuminCD)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            StringBuilder csSQL;
            UFParameterClass cfParameter;
            UFParameterCollectionClass cfParameterCollection;
            string strSQL;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                csSQL = new StringBuilder();
                cfParameterCollection = new UFParameterCollectionClass();


                csSQL.Append("SELECT * FROM ");
                csSQL.Append(ABMyNumberEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.Append(ABMyNumberEntity.MYNUMBER);
                csSQL.Append(" IN (SELECT ");
                csSQL.Append(ABMyNumberEntity.MYNUMBER);
                csSQL.Append(" FROM ");
                csSQL.Append(ABMyNumberEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD);
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.SAKUJOFG);
                csSQL.Append(" <> '1'");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.CKINKB);
                csSQL.Append(" = '1')");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.SAKUJOFG);
                csSQL.Append(" <> '1'");
                csSQL.Append(" AND ");
                csSQL.Append(ABMyNumberEntity.CKINKB);

                csSQL.Append(" = '1'");
                strSQL = csSQL.ToString();

                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABMyNumberEntity.PARAM_JUMINCD;
                cfParameter.Value = strJuminCD;
                cfParameterCollection.Add(cfParameter);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParameterCollection) + "】");

                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(strSQL, ABMyNumberEntity.TABLE_NAME, cfParameterCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException csAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + csAppExp.Message + "】");
                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return csDataSet;

        }
        #endregion

    }
}