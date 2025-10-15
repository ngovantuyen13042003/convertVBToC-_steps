// ************************************************************************************************
// * 業務名　　　　   宛名管理システム
// * 
// * クラス名　　　   ABKojinSeigyoBClass：宛名個人情報制御Bクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 作成日付　　     2011/01/18
// *
// * 作成者　　　　   2901 夘之原　和慶
// * 
// * 著作権　　　　   （株）電算
// * 
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2011/04/19  000001      宛名個人制御情報全件取得(戻り:UFDataReaderClass型)メソッド追加（夘之原）
// * 2011/05/10  000002      宛名個人制御情報全件取得時のソート設定追加（夘之原）
// * 2012/06/21  000003      【AB18010】八街市－異動分抽出対応（中嶋）
// * 2012/08/17  000004      【AB18010】異動分の並び順が住民コード順になっていない不具合修正（中嶋）
// * 2016/01/07  000005      【AB00163】個人制御の同一人対応（石合）
// * 2023/10/16  000006      【AB-0890-1】個人制御情報詳細管理項目追加_公開系(下村)
// * 2023/10/25  000007      【AB-1000-1】個人制御同一個人番号者対応(下村)
// * 2024/01/10  000008      【AB-0120-1】 住民データ異動中の排他制御
// ************************************************************************************************
using System;
// *履歴番号 000003 2012/06/21 追加開始
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region 参照名前空間

namespace Densan.Reams.AB.AB000BB
{
    // *履歴番号 000003 2012/06/21 追加終了
    #endregion

    public class ABKojinSeigyoBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigData;             // コンフィグデータ
        private UFRdbClass m_cfRdb;                           // ＲＤＢクラス
        private UFErrorClass m_cfError;                       // エラー処理クラス
        private ABLogXClass m_cABLogX;                       // ABログ出力Xクラス
                                                             // *履歴番号 000008 2024/01/10 追加開始
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private StringBuilder m_csSb;
        private string m_strInsertSQL;
        private string m_strUpDateSQL;
        // *履歴番号 000008 2024/01/10 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKojinSeigyoBClass";
        // *履歴番号 000003 2012/06/21 追加開始
        private const string PRM_CHUSHUTSU_ST_YMD = "@CHUSHUTSU_ST_YMD";
        private const string PRM_CHUSHUTSU_ED_YMD = "@CHUSHUTSU_ED_YMD";
        private const string PRM_CHUSHUTSU_ST_YMDHMS = "@CHUSHUTSU_ST_YMDHMS";
        private const string PRM_CHUSHUTSU_ED_YMDHMS = "@CHUSHUTSU_ED_YMDHMS";
        // *履歴番号 000003 2012/06/21 追加終了
        // *履歴番号 000008 2024/01/10 追加開始
        private const string STR_SELECT_WHERE = " WHERE ";
        private const string STR_SELECT_AND = " And ";
        private const string STR_SELECT_LEFTKAKKO = "(";
        private const string STR_SELECT_RIGHTKAKKO = ")";
        private const string STR_SELECT_EQUAL = " = ";
        private const string STR_SELECT_NOTEQUAL = " <> ";
        private const string STR_SQL_UPDATE = "UPDATE ";
        private const string STR_SQL_INSERT = "INSERT INTO ";
        private const string STR_SQL_SET = " SET ";
        private const string STR_SQL_VALUES = " VALUES (";
        private const string STR_SQL_KANMA = ", ";
        private const string STR_SQL_KUHAKU = " ";
        private const string SAKUJOFG_1 = "'1'";
        private const string SAKUJOFG_0 = "0";
        private const string STR_CLASSNAME = "【クラス名:";
        private const string STR_KAKKO = "】";
        private const string STR_UFAPPEXCEPTION_METHODNAME = "【メソッド名:";
        private const string STR_EXCEPTION_ERRORNAME = "【エラー内容:";
        private const string STR_JIKKOMETHODNAME_EXECUTESQL = "【実行メソッド名:ExecuteSQL】";
        private const string STR_SQLNAIYOU = "【SQL内容:";
        private const string STR_DATEFORMATE = "yyyyMMddHHmmssfff";
        // *履歴番号 000008 2024/01/10 追加終了
        #endregion

        // *履歴番号 000003 2012/06/21 追加開始
        #region 構造体
        public struct KikanColName
        {
            public string m_strSTCol;
            public string m_strEDCol;
        }
        #endregion
        // *履歴番号 000003 2012/06/21 追加終了

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
        public ABKojinSeigyoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigData;
            m_cfRdb = cfRdb;

            // ABログ出力クラスのインスタンス化
            m_cABLogX = new ABLogXClass(m_cfControlData, m_cfConfigData, THIS_CLASS_NAME);

            // *履歴番号 000008 2024/01/10 追加開始
            m_csSb = new StringBuilder();
            // *履歴番号 000008 2024/01/10 追加終了

        }
        #endregion

        #region 宛名個人制御情報取得
        // ************************************************************************************************
        // * メソッド名     宛名個人制御情報取得
        // * 
        // * 構文           Public Function GetABKojinSeigyo(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　 宛名個人制御情報マスタから条件に合うものを取得する。
        // * 
        // * 引数           ByVal strJuminCD As String  :　住民コード
        // * 
        // * 戻り値         取得した宛名個人制御情報の該当データ（DataSet）
        // *                構造：csABKojinSeigyoEntity
        // ************************************************************************************************
        public DataSet GetABKojinSeigyo(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetABKojinSeigyo";           // メソッド名
            DataSet csABKojinSeigyoEntity;                            // 個人制御情報データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABKojinseigyomstEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABKojinseigyomstEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass));

                // SQLの実行 DataSetの取得
                csABKojinSeigyoEntity = m_cfRdb.GetDataSet(strSQL.ToString(), ABKojinseigyomstEntity.TABLE_NAME, cfUFParameterCollectionClass);

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

            return csABKojinSeigyoEntity;

        }

        // *履歴番号 000005 2016/01/07 追加開始
        /// <summary>
    /// 個人制御データ取得
    /// </summary>
    /// <param name="a_strJuminCD">住民コード文字列配列</param>
    /// <returns>個人制御データ</returns>
    /// <remarks></remarks>
        public DataSet GetABKojinSeigyo(string[] a_strJuminCD)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            StringBuilder csSQL;
            UFParameterCollectionClass cfParameterCollection;
            UFParameterClass cfParameter;
            string strParameterName;

            try
            {

                // デバッグ開始ログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQL文の作成
                csSQL = new StringBuilder();
                cfParameterCollection = new UFParameterCollectionClass();


                csSQL.Append("SELECT * FROM ");
                csSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                csSQL.Append(" IN (");

                for (int i = 0, loopTo = a_strJuminCD.Length - 1; i <= loopTo; i++)
                {

                    // -----------------------------------------------------------------------------
                    // 住民コード
                    strParameterName = ABKojinseigyomstEntity.KEY_JUMINCD + i.ToString();

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
                csSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                csSQL.Append(" <> '1'");
                csSQL.Append(" ORDER BY ");

                csSQL.Append(ABKojinseigyomstEntity.JUMINCD);

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(THIS_METHOD_NAME, m_cfRdb.GetDevelopmentSQLString(csSQL.ToString(), cfParameterCollection));

                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdb.GetDataSet(csSQL.ToString(), ABKojinseigyomstEntity.TABLE_NAME, cfParameterCollection);

                // デバッグ終了ログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException cfRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {

                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, cfRdbTimeOutExp.p_strErrorCode, cfRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, cfAppExp.p_strErrorCode, cfAppExp.Message);
                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, csExp.Message);
                // システムエラーをスローする
                throw;

            }

            return csDataSet;

        }
        // *履歴番号 000005 2016/01/07 追加終了

        #endregion


        // *履歴番号 000001 2011/04/19 追加開始
        #region 宛名個人制御情報全件取得(戻り:UFDataReaderClass)
        // ************************************************************************************************
        // * メソッド名     宛名個人制御情報全件取得
        // * 
        // * 構文           Public Function GetABKojinSeigyo() As UFDataReaderClass
        // * 
        // * 機能　　    　 宛名個人制御情報マスタから全件取得する。(UFDataReaderClass型)
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         取得した宛名個人制御情報のDataReader（UFDataReaderClass）
        // *                構造：cfABKojinSeigyoDataReader
        // ************************************************************************************************
        public UFDataReaderClass GetABKojinSeigyo()
        {
            const string THIS_METHOD_NAME = "GetABKojinSeigyo";           // メソッド名
            UFDataReaderClass cfABKojinSeigyoDataReader;              // 個人制御情報DataReader
            var strSQL = new StringBuilder();                                 // SQL文文字列

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");
                // *履歴番号 000002 2011/05/10 追加開始
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                strSQL.Append(" ASC");
                // *履歴番号 000002 2011/05/10 追加終了

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString()));

                // SQLの実行 DataSetの取得
                cfABKojinSeigyoDataReader = m_cfRdb.GetDataReader(strSQL.ToString());

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

            return cfABKojinSeigyoDataReader;

        }
        #endregion
        // *履歴番号 000001 2011/04/19 追加終了

        // *履歴番号 000003 2012/06/21 追加開始
        #region 宛名個人制御情報取得(UFDataReaderClass型)（異動分）
        // ************************************************************************************************
        // * メソッド名     宛名個人制御情報取得(UFDataReaderClass型)（異動分）
        // * 
        // * 構文               Public Function GetABKojinSeigyo(ByVal strChushutsuST As String, ByVal strChushutsuED As String _
        // *                                 , ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As DataSet
        // * 
        // * 機能　　    　 宛名個人制御情報マスタから全件取得する。(UFDataReaderClass型)
        // * 
        // * 引数           strChushutsuST：抽出開始日時 strChushutsuED：抽出終了日時 
        // *                enSeigyoShiteiKBN：制御指定区分,strShuryoHanteiKBN：終了判定区分
        // * 
        // * 戻り値         取得した宛名個人制御情報のDataReader（UFDataReaderClass）
        // *                構造：cfABKojinSeigyoDataReader
        // ************************************************************************************************
        // *履歴番号 000005 2016/01/07 修正開始
        // Public Function GetABKojinSeigyo(ByVal strChushutsuST As String, ByVal strChushutsuED As String _
        // , ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As UFDataReaderClass
        public UFDataReaderClass GetABKojinSeigyo(string strChushutsuST, string strChushutsuED, ABSeigyoKbnType enSeigyoShiteiKBN, string strShuryoHanteiKBN, string strDoitsuninUmu)




        {
            // *履歴番号 000005 2016/01/07 修正終了
            const string THIS_METHOD_NAME = "GetABKojinSeigyo";           // メソッド名
            var strSQL = new StringBuilder();                                 // SQL文文字列
            var cfParameterCollection = default(UFParameterCollectionClass);         // パラメータクラス
            UFDataReaderClass cfABKojinSeigyoDataReader;              // 個人制御情報DataReader
                                                                      // *履歴番号 000005 2016/01/07 追加開始
            StringBuilder csIdobunKojinSeigyo;
            int intSelectLength = "SELECT *".RLength;
            // *履歴番号 000005 2016/01/07 追加終了

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE (");
                strSQL.Append(CreateWhere(ref cfParameterCollection, strChushutsuST, strChushutsuED, enSeigyoShiteiKBN, strShuryoHanteiKBN));
                strSQL.Append(") AND ");
                strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");
                // *履歴番号 000005 2016/01/07 追加開始
                if (strDoitsuninUmu == "1")
                {
                    // 同一人対応ありの場合、プラスして異動対象者の同一人の個人制御を取得する
                    csIdobunKojinSeigyo = new StringBuilder(strSQL.ToString(intSelectLength, strSQL.RLength - intSelectLength));
                    strSQL.Append(" UNION ");
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                    strSQL.Append(" FROM ");
                    strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" FROM ");
                    strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                    strSQL.Append(csIdobunKojinSeigyo);
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                    strSQL.Append(" UNION ");
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABMyNumberEntity.JUMINCD);
                    strSQL.Append(" FROM ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABMyNumberEntity.MYNUMBER);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABMyNumberEntity.MYNUMBER);
                    strSQL.Append(" FROM ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABMyNumberEntity.JUMINCD);
                    strSQL.Append(" IN (");
                    strSQL.Append("SELECT ");
                    strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                    strSQL.Append(csIdobunKojinSeigyo);
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.CKINKB);
                    strSQL.Append(" = '1'");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.CKINKB);
                    strSQL.Append(" = '1'");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                    strSQL.Append(")");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");
                }
                else
                {
                    // noop
                }
                // *履歴番号 000005 2016/01/07 追加終了
                // *履歴番号 000004 2012/08/17 追加開始
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABKojinseigyomstEntity.JUMINCD);
                strSQL.Append(" ASC");
                // *履歴番号 000004 2012/08/17 追加終了

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfParameterCollection));

                // SQLの実行 DataSetの取得
                cfABKojinSeigyoDataReader = m_cfRdb.GetDataReader(strSQL.ToString(), cfParameterCollection);

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

            return cfABKojinSeigyoDataReader;

        }
        #endregion

        #region Where句作成：異動分抽出
        // ************************************************************************************************
        // * メソッド名     Where句作成：異動分抽出
        // * 
        // * 構文           Private Function CreateWhere(ByVal cfParameterCollection As UFParameterCollectionClass, _
        // *                             ByVal strChushutsuST As String, ByVal strChushutsuED As String, _
        // *                             ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As String
        // * 
        // * 機能　　    　 異動分のWhere句を作成する
        // * 
        // * 引数           cfParameterCollection：パラメータコレクション strChushutsuST：抽出開始日時 strChushutsuED：抽出終了日時 
        // *                enSeigyoShiteiKBN：制御指定区分,strShuryoHanteiKBN：終了判定区分
        // * 
        // * 戻り値         Where句
        // ************************************************************************************************
        private string CreateWhere(ref UFParameterCollectionClass cfParameterCollection, string strChushutsuST, string strChushutsuED, ABSeigyoKbnType enSeigyoShiteiKBN, string strShuryoHanteiKBN)

        {
            List<KikanColName> csKikanList;
            KikanColName cKikanColName;
            StringBuilder csWhere;
            try
            {
                csKikanList = new List<KikanColName>();

                switch (enSeigyoShiteiKBN)
                {
                    case var @case when @case == ABSeigyoKbnType.SeigyoShinai:
                        {
                            // 指定なしの時、全ての期間をチェックする
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.DVTAISHOKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.DVTAISHOSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA1KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA1SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA2KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA2SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA3KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA3SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 仮登録の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.KARITOROKUKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.KARITOROKUSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 特別養子の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 処理注意１の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 処理注意２の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 処理注意３の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 処理保留の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            // 参照不可の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }

                    case var case1 when case1 == ABSeigyoKbnType.DVTaishoSha:
                        {
                            // DV対象の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.DVTAISHOKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.DVTAISHOSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case2 when case2 == ABSeigyoKbnType.HakkoTeishi:
                        {
                            // 発行停止の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case3 when case3 == ABSeigyoKbnType.JittaiChosa:
                        {
                            // 実態調査の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case4 when case4 == ABSeigyoKbnType.SeinenHiKokennin:
                        {
                            // 成年被後見人の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case5 when case5 == ABSeigyoKbnType.Sonota1:
                        {
                            // その他１の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA1KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA1SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case6 when case6 == ABSeigyoKbnType.Sonota2:
                        {
                            // その他２の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA2KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA2SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case7 when case7 == ABSeigyoKbnType.Sonota3:
                        {
                            // その他３の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA3KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA3SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case8 when case8 == ABSeigyoKbnType.KariToroku:
                        {
                            // 仮登録の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.KARITOROKUKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.KARITOROKUSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case9 when case9 == ABSeigyoKbnType.TokubetsuYoshi:
                        {
                            // 特別養子の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case10 when case10 == ABSeigyoKbnType.TokubetsuJijo:
                        {
                            // 特別事情の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case11 when case11 == ABSeigyoKbnType.ShoriChui1:
                        {
                            // 処理注意１の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case12 when case12 == ABSeigyoKbnType.ShoriChui2:
                        {
                            // 処理注意２の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case13 when case13 == ABSeigyoKbnType.ShoriChui3:
                        {
                            // 処理注意３の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case14 when case14 == ABSeigyoKbnType.ShoriHoryu:
                        {
                            // 処理保留の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                    case var case15 when case15 == ABSeigyoKbnType.SanshoFuka:
                        {
                            // 参照不可の時
                            cKikanColName = new KikanColName();
                            cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD;
                            cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD;
                            csKikanList.Add(cKikanColName);
                            break;
                        }
                }

                csWhere = new StringBuilder();
                // 更新日時での抽出条件
                csWhere.AppendFormat(" ({0} < {1} AND {1} <= {2}) OR ", PRM_CHUSHUTSU_ST_YMDHMS, ABKojinseigyomstEntity.KOSHINNICHIJI, PRM_CHUSHUTSU_ED_YMDHMS);
                if (strChushutsuST.RSubstring(0, 8) != strChushutsuED.RSubstring(0, 8))
                {
                    // 抽出開始日・終了日が違う時、開始・終了期間で抽出する
                    foreach (KikanColName csKikan in csKikanList)
                    {
                        // 期間リスト分ループ
                        csWhere.AppendFormat(" ({0} < {1} AND {1} <= {2}) OR ", PRM_CHUSHUTSU_ST_YMD, csKikan.m_strSTCol, PRM_CHUSHUTSU_ED_YMD);
                        if (strShuryoHanteiKBN == "1")
                        {
                            // 終了判定区分が"1"の時、終了日もチェックする
                            csWhere.AppendFormat(" ({0} <= {1} AND {1} < {2}) OR ", PRM_CHUSHUTSU_ST_YMD, csKikan.m_strEDCol, PRM_CHUSHUTSU_ED_YMD);
                        }
                        else
                        {
                            // 何もしない
                        }
                    }
                }
                else
                {
                    // 何もしない
                }

                // パラメータの作成
                if (cfParameterCollection is null)
                {
                    cfParameterCollection = new UFParameterCollectionClass();
                }
                else
                {
                    // 何もしない
                }

                // 抽出開始日時
                cfParameterCollection.Add(PRM_CHUSHUTSU_ST_YMDHMS, strChushutsuST);
                // 抽出終了日時
                cfParameterCollection.Add(PRM_CHUSHUTSU_ED_YMDHMS, strChushutsuED);
                // 抽出開始日
                cfParameterCollection.Add(PRM_CHUSHUTSU_ST_YMD, strChushutsuST.RSubstring(0, 8));
                // 抽出終了日
                cfParameterCollection.Add(PRM_CHUSHUTSU_ED_YMD, strChushutsuED.RSubstring(0, 8));
            }


            catch (Exception csException)
            {
                throw;
            }

            return csWhere.ToString().TrimEnd("OR ".ToCharArray());
        }
        #endregion
        // *履歴番号 000003 2012/06/21 終了開始

        // *履歴番号 000008 2024/01/10 追加開始
        #region 個人制御情報マスタデータ更新メソッド
        // ************************************************************************************************
        // * メソッド名   個人制御情報マスタデータ更新メソッド
        // * 
        // * 構文         Public Function UpdateKojinSeigyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     個人制御情報マスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow   :  個人制御情報データ(Kojinseigyomst)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateKojinSeigyo(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateKojinSeigyo";                         // パラメータクラス
            int intUpdCnt = 0;                            // 更新件数

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL is null | string.IsNullOrEmpty(m_strUpDateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABKojinseigyomstEntity.TANMATSUID) = m_cfControlData.m_strClientId;
                // 端末ＩＤ
                csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER) + 1m;
                // 更新カウンタ
                csDataRow(ABKojinseigyomstEntity.KOSHINNICHIJI) = m_cfRdb.GetSystemDate.ToString(STR_DATEFORMATE);
                // 更新日時
                csDataRow(ABKojinseigyomstEntity.KOSHINUSER) = m_cfControlData.m_strUserId;  // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABKojinseigyomstEntity.PREFIX_KEY.RLength) == ABKojinseigyomstEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinseigyomstEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinseigyomstEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                {
                    ref var withBlock = ref m_csSb;
                    withBlock.RRemove(0, withBlock.RLength);
                    withBlock.Append(STR_CLASSNAME);
                    withBlock.Append(GetType().Name);
                    withBlock.Append(STR_KAKKO);
                    withBlock.Append(STR_UFAPPEXCEPTION_METHODNAME);
                    withBlock.Append(System.Reflection.MethodBase.GetCurrentMethod().Name);
                    withBlock.Append(STR_KAKKO);
                    withBlock.Append(STR_JIKKOMETHODNAME_EXECUTESQL);
                    withBlock.Append(STR_SQLNAIYOU);
                    withBlock.Append(m_cfRdb.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass));
                    withBlock.Append(STR_KAKKO);
                }

                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(m_csSb.ToString()));

                // SQLの実行
                intUpdCnt = m_cfRdb.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass);

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw new UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException);
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return intUpdCnt;

        }
        #endregion

        #region 個人制御情報マスタデータ追加メソッド
        // ************************************************************************************************
        // * メソッド名   個人制御情報マスタデータ追加メソッド
        // * 
        // * 構文         Public Function InsertKojinSeigyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     個人制御情報マスタに新規データを追加する。
        // * 
        // * 引数         csDataRow As DataRow   : 個人制御情報データ(Kojinseigyomst)
        // * 
        // * 戻り値       追加件数(Integer)
        // ************************************************************************************************
        public int InsertKojinSeigyo(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertKojinSeigyo";                                 // パラメータクラス
            int intInsCnt = 0;                                    // 追加件数
            string strUpdateDateTime = string.Empty;                  // システム日付

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdb.GetSystemDate.ToString(STR_DATEFORMATE);               // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABKojinseigyomstEntity.TANMATSUID) = m_cfControlData.m_strClientId;      // 端末ＩＤ
                csDataRow(ABKojinseigyomstEntity.SAKUJOFG) = SAKUJOFG_0;                           // 削除フラグ
                csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER) = decimal.Zero;                    // 更新カウンタ
                csDataRow(ABKojinseigyomstEntity.SAKUSEINICHIJI) = strUpdateDateTime;              // 作成日時
                csDataRow(ABKojinseigyomstEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;       // 作成ユーザー
                csDataRow(ABKojinseigyomstEntity.KOSHINNICHIJI) = strUpdateDateTime;               // 更新日時
                csDataRow(ABKojinseigyomstEntity.KOSHINUSER) = m_cfControlData.m_strUserId;        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinseigyomstEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                {
                    ref var withBlock = ref m_csSb;
                    withBlock.RRemove(0, withBlock.RLength);
                    withBlock.Append(STR_CLASSNAME);
                    withBlock.Append(GetType().Name);
                    withBlock.Append(STR_KAKKO);
                    withBlock.Append(STR_UFAPPEXCEPTION_METHODNAME);
                    withBlock.Append(System.Reflection.MethodBase.GetCurrentMethod().Name);
                    withBlock.Append(STR_KAKKO);
                    withBlock.Append(STR_JIKKOMETHODNAME_EXECUTESQL);
                    withBlock.Append(STR_SQLNAIYOU);
                    withBlock.Append(m_cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfUpdateUFParameterCollectionClass));
                    withBlock.Append(STR_KAKKO);
                }

                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(m_csSb.ToString()));

                // SQLの実行
                intInsCnt = m_cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw new UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException);
            }


            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return intInsCnt;

        }
        #endregion

        #region  SQL文の作成
        // ************************************************************************************************
        // * メソッド名   SQL文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数         csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            StringBuilder strInsertColumnSB;                      // 追加SQL文項目文字列
            StringBuilder strInsertParamSB;                       // 追加SQL文パラメータ文字列
            StringBuilder strWhereSB;                             // 更新削除SQL文Where文文字列
            StringBuilder strInsertSQLSB;
            StringBuilder strUpDateSQLSB;
            string strInsertColumn = string.Empty;
            string strInsertParam = string.Empty;

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);
                strWhereSB = new StringBuilder();
                strInsertSQLSB = new StringBuilder();
                strUpDateSQLSB = new StringBuilder();
                strInsertColumnSB = new StringBuilder();
                strInsertParamSB = new StringBuilder();

                // INSERT SQL文の作成
                strInsertSQLSB.Append(STR_SQL_INSERT).Append(ABKojinseigyomstEntity.TABLE_NAME);
                strInsertSQLSB.Append(STR_SQL_KUHAKU);

                // UPDATE SQL文の作成
                strUpDateSQLSB.Append(STR_SQL_UPDATE).Append(ABKojinseigyomstEntity.TABLE_NAME);
                strUpDateSQLSB.Append(STR_SQL_SET);

                // UPDATE Where文作成
                strWhereSB.Append(STR_SELECT_WHERE);
                strWhereSB.Append(ABKojinseigyomstEntity.JUMINCD);
                strWhereSB.Append(STR_SELECT_EQUAL);
                strWhereSB.Append(ABKojinseigyomstEntity.PREFIX_KEY);
                strWhereSB.Append(ABKojinseigyomstEntity.JUMINCD);
                strWhereSB.Append(STR_SELECT_AND);
                strWhereSB.Append(ABKojinseigyomstEntity.SAKUJOFG);
                strWhereSB.Append(STR_SELECT_NOTEQUAL);
                strWhereSB.Append(SAKUJOFG_1);
                strWhereSB.Append(STR_SELECT_AND);
                strWhereSB.Append(ABKojinseigyomstEntity.KOSHINCOUNTER);
                strWhereSB.Append(STR_SELECT_EQUAL);
                strWhereSB.Append(ABKojinseigyomstEntity.PREFIX_KEY);
                strWhereSB.Append(ABKojinseigyomstEntity.KOSHINCOUNTER);

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumnSB.Append(csDataColumn.ColumnName).Append(STR_SQL_KANMA);

                    strInsertParamSB.Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER);
                    strInsertParamSB.Append(csDataColumn.ColumnName);
                    strInsertParamSB.Append(STR_SQL_KANMA);

                    // UPDATE SQL文の作成
                    strUpDateSQLSB.Append(csDataColumn.ColumnName).Append(STR_SELECT_EQUAL);
                    strUpDateSQLSB.Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER);
                    strUpDateSQLSB.Append(csDataColumn.ColumnName);
                    strUpDateSQLSB.Append(STR_SQL_KANMA);

                    // INSERT コレクションにパラメータを追加
                    {
                        ref var withBlock = ref m_csSb;
                        withBlock.RRemove(0, withBlock.RLength);
                        withBlock.Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER);
                        withBlock.Append(csDataColumn.ColumnName);
                    }
                    cfUFParameterClass.ParameterName = m_csSb.ToString();

                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    {
                        ref var withBlock1 = ref m_csSb;
                        withBlock1.RRemove(0, withBlock1.RLength);
                        withBlock1.Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER);
                        withBlock1.Append(csDataColumn.ColumnName);
                    }
                    cfUFParameterClass.ParameterName = m_csSb.ToString();
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumnSB.ToString();
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParamSB.ToString();
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");

                strInsertSQLSB.Append(STR_SELECT_LEFTKAKKO);
                strInsertSQLSB.Append(strInsertColumn);
                strInsertSQLSB.Append(STR_SELECT_RIGHTKAKKO);
                strInsertSQLSB.Append(STR_SQL_VALUES);
                strInsertSQLSB.Append(strInsertParam);
                strInsertSQLSB.Append(STR_SELECT_RIGHTKAKKO);
                m_strInsertSQL = strInsertSQLSB.ToString();

                // UPDATE SQL文のトリミング
                m_strUpDateSQL = strUpDateSQLSB.ToString().Trim();
                m_strUpDateSQL = m_strUpDateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                strUpDateSQLSB.RRemove(0, strUpDateSQLSB.RLength);
                strUpDateSQLSB.Append(m_strUpDateSQL);
                strUpDateSQLSB.Append(strWhereSB.ToString());
                m_strUpDateSQL = strUpDateSQLSB.ToString();

                // UPDATE コレクションにキー情報を追加
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                {
                    ref var withBlock2 = ref m_csSb;
                    withBlock2.RRemove(0, withBlock2.RLength);
                    withBlock2.Append(ABKojinseigyomstEntity.PREFIX_KEY);
                    withBlock2.Append(ABKojinseigyomstEntity.JUMINCD);
                }
                cfUFParameterClass.ParameterName = m_csSb.ToString();
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                {
                    ref var withBlock3 = ref m_csSb;
                    withBlock3.RRemove(0, withBlock3.RLength);
                    withBlock3.Append(ABKojinseigyomstEntity.PREFIX_KEY);
                    withBlock3.Append(ABKojinseigyomstEntity.KOSHINCOUNTER);
                }
                cfUFParameterClass.ParameterName = m_csSb.ToString();
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw new UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException);
            }


            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }
        }
        #endregion
        // *履歴番号 000008 2024/01/10 追加終了
        #endregion

    }
}