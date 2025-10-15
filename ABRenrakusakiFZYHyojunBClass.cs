// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ連絡先付随標準マスタビジネスクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2024/01/10　原
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2024/01/11   000000     【AB-0860-1】連絡先管理項目追加
// * 2024/03/07   000001     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
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
/// ＡＢ連絡先付随標準マスタビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABRenrakusakiFZYHyojunBClass
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
        private const string THIS_CLASS_NAME = "ABRenrakusakiFZYHyojunBClass";              // クラス名

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private static readonly string SQL_SAKUJOFG = string.Format("{0} = '0'", ABRenrakusakiFZYHyojunEntity.SAKUJOFG);

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
        public ABRenrakusakiFZYHyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


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
            m_cfSelectParamCollection = null;
            m_cfInsertParamCollection = null;
            m_cfUpdateParamCollection = null;
            m_cfDeleteParamCollection = null;
            m_cfLogicalDeleteRecoverParamCollection = null;

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

            DataSet csRenrakusakiFZYHyojunEntity;

            try
            {

                // スキーマの取得
                csRenrakusakiFZYHyojunEntity = m_cfRdbClass.GetTableSchemaNoRestriction(string.Format("SELECT * FROM {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME), ABRenrakusakiFZYHyojunEntity.TABLE_NAME, false);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csRenrakusakiFZYHyojunEntity;

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
            DataSet csRenrakusakiFZYHyojunEntity;

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
                csRenrakusakiFZYHyojunEntity = m_csDataSchema.Clone();
                csRenrakusakiFZYHyojunEntity = m_cfRdbClass.GetDataSet(strSQL, csRenrakusakiFZYHyojunEntity, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, cfParamCollection, false);

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
            return csRenrakusakiFZYHyojunEntity;

        }

        /// <summary>
    /// SelectByKey
    /// </summary>
    /// <param name="strJuminCD">住民コード</param>
    /// <param name="strGyomuCD">業務コード</param>
    /// <param name="strGyomuNaiShuCD">業務内種別コード</param>
    /// <param name="strTorokuRenban">登録連番</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks></remarks>
        public DataSet SelectByKey(string strJuminCD, string strGyomuCD, string strGyomuNaiShuCD, string strTorokuRenban, bool blnSakujoFG)




        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csRenrakusakiFZYHyojunEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCD;
                m_cfSelectParamCollection.Add(cfParam);

                // 業務コード
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.PARAM_GYOMUCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_GYOMUCD;
                cfParam.Value = strGyomuCD;
                m_cfSelectParamCollection.Add(cfParam);

                // 業務内種別コード
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.PARAM_GYOMUNAISHU_CD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_GYOMUNAISHU_CD;
                cfParam.Value = strGyomuNaiShuCD;
                m_cfSelectParamCollection.Add(cfParam);

                // 登録連番
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.PARAM_TOROKURENBAN);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_TOROKURENBAN;
                cfParam.Value = strTorokuRenban;
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
                csRenrakusakiFZYHyojunEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

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
            return csRenrakusakiFZYHyojunEntity;

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
                csSQL.AppendFormat(" FROM {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABRenrakusakiFZYHyojunEntity.TABLE_NAME, false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0},", ABRenrakusakiFZYHyojunEntity.JUMINCD);
                csSQL.AppendFormat(" {0},", ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                csSQL.AppendFormat(" {0},", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                csSQL.AppendFormat(" {0} ", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);

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
                csSQL.Append(ABRenrakusakiFZYHyojunEntity.JUMINCD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.TOROKUYMD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.BIKO);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RESERVE1);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RESERVE2);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RESERVE3);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RESERVE4);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.RESERVE5);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI);

                csSQL.AppendFormat(", {0}", ABRenrakusakiFZYHyojunEntity.KOSHINUSER);
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
                csDataRow[ABRenrakusakiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                                            // 端末ＩＤ
                csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG], SAKUJOFG_OFF);                   // 削除フラグ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;                                                                     // 更新カウンター
                csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI], strUpdateDatetime);  // 作成日時
                csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;                                                             // 作成ユーザー
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI], strUpdateDatetime);    // 更新日時
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                                              // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertParamCollection)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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

                    strParamName = string.Concat(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csColumnList.Add(csDataColumn.ColumnName);
                    csParamList.Add(strParamName);

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfInsertParamCollection.Add(cfParam);

                }

                m_strInsertSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, string.Join(',', (string[])csColumnList.ToArray(typeof(string))), string.Join(',', (string[])csParamList.ToArray(typeof(string))));


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
                csDataRow[ABRenrakusakiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                                            // 端末ＩＤ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER]) + 1m;             // 更新カウンタ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI], strUpdateDatetime);    // 更新日時
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                                              // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfUpdateParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();

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

                    strParamName = string.Concat(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    csParamList.Add(string.Format("{0} = {1}", csDataColumn.ColumnName, strParamName));

                    cfParam = new UFParameterClass();
                    cfParam.ParameterName = strParamName;
                    m_cfUpdateParamCollection.Add(cfParam);

                }

                m_strUpdateSQL = string.Format("UPDATE {0} SET {1} ", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, string.Join(',', (string[])csParamList.ToArray(typeof(string))));


                csWhere = new StringBuilder(256);
                csWhere.Append("WHERE ");
                csWhere.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.KEY_TOROKURENBAN);
                csWhere.Append("AND ");
                csWhere.AppendFormat("{0} = {1}", ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER);
                m_strUpdateSQL += csWhere.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_TOROKURENBAN;
                m_cfUpdateParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER;
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
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();

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
                csSQL.AppendFormat("DELETE FROM {0} ", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.KEY_TOROKURENBAN);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER);
                m_strDeleteSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_TOROKURENBAN;
                m_cfDeleteParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER;
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
                csDataRow[ABRenrakusakiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                                            // 端末ＩＤ
                csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = SAKUJOFG_ON;                                                                                // 削除フラグ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER]) + 1m;             // 更新カウンタ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI], strUpdateDatetime);    // 更新日時
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                                              // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteRecoverParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();

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
                csDataRow[ABRenrakusakiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                                            // 端末ＩＤ
                csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                                                                               // 削除フラグ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER]) + 1m;             // 更新カウンタ
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = GetValue(csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI], strUpdateDatetime);    // 更新日時
                csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                                              // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfLogicalDeleteRecoverParamCollection)
                {

                    if (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY, StringComparison.CurrentCulture) == true)
                    {

                        // キー項目は更新前の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    else
                    {

                        // キー項目以外は更新後の値で設定
                        cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABRenrakusakiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();

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
                csSQL.AppendFormat("UPDATE {0} ", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);
                csSQL.Append("SET ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.TANMATSUID, ABRenrakusakiFZYHyojunEntity.PARAM_TANMATSUID);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.SAKUJOFG, ABRenrakusakiFZYHyojunEntity.PARAM_SAKUJOFG);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINCOUNTER);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINNICHIJI);
                csSQL.Append(", ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.KOSHINUSER, ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINUSER);
                csSQL.Append("WHERE ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1} ", ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.KEY_TOROKURENBAN);
                csSQL.Append("AND ");
                csSQL.AppendFormat("{0} = {1}", ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER);
                m_strLogicalDeleteRecoverSQL = csSQL.ToString();

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_TANMATSUID;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_SAKUJOFG;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.PARAM_KOSHINUSER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_TOROKURENBAN;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfLogicalDeleteRecoverParamCollection.Add(cfParam);
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region GetValue

        /// <summary>
    /// GetValue
    /// </summary>
    /// <param name="objValue">対象オブジェクト</param>
    /// <param name="strValue">代替値</param>
    /// <returns>編集後オブジェクト</returns>
    /// <remarks></remarks>
        private object GetValue(object objValue, string strValue)

        {

            object objResult;

            try
            {

                if (objValue is DBNull || objValue is null || objValue.ToString().Trim().RLength() == 0)

                {
                    objResult = strValue;
                }
                else
                {
                    objResult = objValue;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return objResult;

        }

        #endregion

        #endregion

    }
}
