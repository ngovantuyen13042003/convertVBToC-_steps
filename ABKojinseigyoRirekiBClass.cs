// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         個人制御履歴DA
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付             2023/10/13
// *
// * 作成者　　　     下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABKojinseigyoRirekiBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private ABLogXClass m_cABLogX;                        // ABログ出力Xクラス
        private DataSet m_csDataSchma;                        // スキーマ保管用データセット:全項目用
        private string m_strInsertSQL;
        private string m_strUpDateSQL;
        // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;
        private StringBuilder m_CsSb;
        #endregion

        #region コンスタント定義
        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKojinseigyoRirekiBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
        private const string STR_SELECT_ALL = "SELECT *";
        private const string STR_SELECT_FROM = " FROM ";
        private const string STR_SELECT_WHERE = " WHERE ";
        private const string STR_SELECT_AND = " And ";
        private const string STR_SELECT_LEFTKAKKO = "(";
        private const string STR_SELECT_RIGHTKAKKO = ")";
        private const string STR_SELECT_EQUAL = " = ";
        private const string STR_SELECT_NOTEQUAL = " <> ";
        private const string STR_SQL_INSERT = "INSERT INTO ";
        private const string STR_SQL_SET = " SET ";
        private const string STR_SQL_VALUES = " VALUES (";
        private const string STR_SQL_KANMA = ", ";
        private const string STR_SQL_KUHAKU = " ";
        private const string STR_SELECT_ORDERBY = " ORDER BY ";

        private const string SAKUJOFG_1 = "'1'";
        private const string SAKUJOFG_0 = "0";

        private const string STR_DATEFORMATE = "yyyyMMddHHmmssfff";
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
        public ABKojinseigyoRirekiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ABログ出力クラスのインスタンス化
            m_cABLogX = new ABLogXClass(m_cfControlData, m_cfConfigDataClass, THIS_CLASS_NAME);

            m_CsSb = new StringBuilder();
            {
                ref var withBlock = ref m_CsSb;
                withBlock.RRemove(0, withBlock.RLength());
                withBlock.Append(STR_SELECT_ALL);
                withBlock.Append(STR_SELECT_FROM);
                withBlock.Append(ABKojinseigyoRirekiEntity.TABLE_NAME);
            }
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(m_CsSb.ToString(), ABKojinseigyoRirekiEntity.TABLE_NAME, false);
        }
        #endregion

        #region メソッド

        #region 個人制御履歴取得メソッド
        // ************************************************************************************************
        // * メソッド名   個人制御履歴取得メソッド
        // * 
        // * 構文         Public Function GetKojinseigyoRireki(
        // ByVal strJuminCd As String) As DataSet
        // * 
        // * 機能　　     個人制御履歴より該当データを取得する。
        // * 
        // * 引数         strJuminCd As String   : 住民コード
        // * 
        // * 戻り値       取得した個人制御履歴の該当データ（DataSet）
        // *                   
        // ************************************************************************************************
        public DataSet GetKojinseigyoRireki(string strJuminCd)
        {

            const string THIS_METHOD_NAME = "GetKojinseigyoRireki";
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            DataSet csABKojinSeigyoRirekiEntity;                      // 個人制御履歴DataSet
            StringBuilder strSQL;

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                if (string.IsNullOrEmpty(strJuminCd))
                {
                    return default;
                }

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL = new StringBuilder();
                // SELECT句
                strSQL.Append(STR_SELECT_ALL);
                strSQL.Append(STR_SELECT_FROM).Append(ABKojinseigyoRirekiEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABKojinseigyoRirekiEntity.TABLE_NAME, false);
                }

                // WHERE句
                strSQL.Append(STR_SELECT_WHERE);
                // 住民コード
                strSQL.Append(ABKojinseigyoRirekiEntity.JUMINCD);
                strSQL.Append(STR_SELECT_EQUAL);
                strSQL.Append(ABKojinseigyoRirekiEntity.KEY_JUMINCD);
                strSQL.Append(STR_SELECT_AND);
                strSQL.Append(ABKojinseigyoRirekiEntity.SAKUJOFG);
                strSQL.Append(STR_SELECT_NOTEQUAL);
                strSQL.Append(SAKUJOFG_1);
                strSQL.Append(STR_SELECT_ORDERBY);
                strSQL.Append(ABKojinseigyoRirekiEntity.RIREKINO);
                strSQL.Append(STR_SQL_KANMA);
                strSQL.Append(ABKojinseigyoRirekiEntity.RIREKIEDABAN);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABKojinseigyoRirekiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCd;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass));

                // SQLの実行 DataSetの取得
                csABKojinSeigyoRirekiEntity = m_csDataSchma.Clone();
                csABKojinSeigyoRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABKojinseigyoRirekiEntity.TABLE_NAME, cfUFParameterCollectionClass);

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

            return csABKojinSeigyoRirekiEntity;

        }
        #endregion

        #region 個人制御履歴データ追加メソッド
        // ************************************************************************************************
        // * メソッド名   個人制御履歴データ追加メソッド
        // * 
        // * 構文         Public Function InsertKojinseigyoRireki(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     個人制御履歴に新規データを追加する。
        // * 
        // * 引数         csDataRow As DataRow   : 個人制御履歴データ(Kojinseigyomst)
        // * 
        // * 戻り値       追加件数(Integer)
        // ************************************************************************************************
        public int InsertKojinseigyoRireki(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertKojinseigyoRireki";                                 // パラメータクラス
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
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(STR_DATEFORMATE);        // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABKojinseigyoRirekiEntity.TANMATSUID] = m_cfControlData.m_strClientId;      // 端末ＩＤ
                csDataRow[ABKojinseigyoRirekiEntity.SAKUJOFG] = SAKUJOFG_0;                           // 削除フラグ
                csDataRow[ABKojinseigyoRirekiEntity.KOSHINCOUNTER] = decimal.Zero;                    // 更新カウンタ
                csDataRow[ABKojinseigyoRirekiEntity.SAKUSEINICHIJI] = strUpdateDateTime;              // 作成日時
                csDataRow[ABKojinseigyoRirekiEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;       // 作成ユーザー
                csDataRow[ABKojinseigyoRirekiEntity.KOSHINNICHIJI] = strUpdateDateTime;               // 更新日時
                csDataRow[ABKojinseigyoRirekiEntity.KOSHINUSER] = m_cfControlData.m_strUserId;        // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass));

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
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

            return intInsCnt;

        }
        #endregion

        #region  SQL文の作成
        // ************************************************************************************************
        // * メソッド名   SQL文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　     INSERTSQLを作成、パラメータコレクションを作成する
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
            StringBuilder strInsertSQLSB;
            string strInsertColumn = string.Empty;
            string strInsertParam = string.Empty;

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                strInsertSQLSB = new StringBuilder();
                strInsertColumnSB = new StringBuilder();
                strInsertParamSB = new StringBuilder();

                // INSERT SQL文の作成
                strInsertSQLSB.Append(STR_SQL_INSERT).Append(ABKojinseigyoRirekiEntity.TABLE_NAME);
                strInsertSQLSB.Append(STR_SQL_KUHAKU);

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumnSB.Append(csDataColumn.ColumnName).Append(STR_SQL_KANMA);

                    strInsertParamSB.Append(ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER);
                    strInsertParamSB.Append(csDataColumn.ColumnName);
                    strInsertParamSB.Append(STR_SQL_KANMA);

                    // INSERT コレクションにパラメータを追加
                    {
                        ref var withBlock = ref m_CsSb;
                        withBlock.RRemove(0, withBlock.RLength());
                        withBlock.Append(ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER);
                        withBlock.Append(csDataColumn.ColumnName);
                    }
                    cfUFParameterClass.ParameterName = m_CsSb.ToString();

                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

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

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
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
        }
        #endregion

        #endregion

    }
}
