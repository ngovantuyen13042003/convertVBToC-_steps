// ************************************************************************************************
// * 業務名　　　　   宛名管理システム
// * 
// * クラス名　　　   ABKojinSeigyoTempBClass：宛名個人情報TempBクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 作成日付　　     2011/02/22
// *
// * 作成者　　　　   2901 夘之原　和慶
// * 
// * 著作権　　　　   （株）電算
// * 
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.reams.ab.publicmodule.library.businesscommon.ab001x;

#region 参照名前空間

namespace Densan.Reams.AB.AB000BB
{

    #endregion

    public class ABKojinSeigyoTempBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigData;             // コンフィグデータ
        private UFRdbClass m_cfRdb;                           // ＲＤＢクラス
        private UFErrorClass m_cfError;                       // エラー処理クラス
        private ABLogXClass m_cABLogX;                        // ABログ出力Xクラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollection;       // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollection;    // 物理削除用パラメータコレクション

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKojinSeigyoTempBClass";

        #endregion

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
        public ABKojinSeigyoTempBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigData;
            m_cfRdb = cfRdb;

            // ABログ出力クラスのインスタンス化
            m_cABLogX = new ABLogXClass(m_cfControlData, m_cfConfigData, THIS_CLASS_NAME);

        }
        #endregion

        #region 宛名個人制御Temp取得
        // ************************************************************************************************
        // * メソッド名     宛名個人制御Temp取得
        // * 
        // * 構文           Public Function GetABKojinSeigyoTemp(ByVal strKey As String) As DataSet
        // * 
        // * 機能　　    　 宛名個人制御TempからKeyに合うデータを取得する。
        // * 
        // * 引数           ByVal strKey As String : キー情報
        // * 
        // * 戻り値         取得した宛名個人制御Tempの該当データ（DataSet）
        // *                構造：csABKojinSeigyoTempEntity
        // ************************************************************************************************
        public DataSet GetABKojinSeigyoTemp(string strKey)
        {
            const string THIS_METHOD_NAME = "GetABKojinSeigyoTemp";       // メソッド名
            DataSet csABKojinSeigyoTempEntity;                        // 個人制御情報Tempデータ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABKojinSeigyoTempEntity.KEYCD);
                strSQL.Append(" = ");
                strSQL.Append(ABKojinSeigyoTempEntity.KEY_KEYCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABKojinSeigyoTempEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.KEY_KEYCD;
                cfUFParameterClass.Value = strKey;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass));

                // SQLの実行 DataSetの取得
                csABKojinSeigyoTempEntity = m_cfRdb.GetDataSet(strSQL.ToString(), ABKojinSeigyoTempEntity.TABLE_NAME, cfUFParameterCollectionClass);

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

            return csABKojinSeigyoTempEntity;

        }
        #endregion

        #region 宛名個人制御Temp追加
        // ************************************************************************************************
        // * メソッド名     宛名個人制御Temp追加
        // * 
        // * 構文           Public Function InsertABKojinSeigyoTemp(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名個人制御Tempにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertABKojinSeigyoTemp(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertABKojinSeigyoTemp";
            int intInsCnt;        // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollection is null)
                {
                    CreateInsertSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABKojinSeigyoTempEntity.TANMATSUID) = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow(ABKojinSeigyoTempEntity.SAKUJOFG) = "0";                               // 削除フラグ
                csDataRow(ABKojinSeigyoTempEntity.KOSHINCOUNTER) = decimal.Zero;                 // 更新カウンタ
                csDataRow(ABKojinSeigyoTempEntity.SAKUSEINICHIJI) = strUpdateDateTime;           // 作成日時
                csDataRow(ABKojinSeigyoTempEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;    // 作成ユーザー
                csDataRow(ABKojinSeigyoTempEntity.KOSHINNICHIJI) = strUpdateDateTime;            // 更新日時
                csDataRow(ABKojinSeigyoTempEntity.KOSHINUSER) = m_cfControlData.m_strUserId;     // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollection)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(THIS_METHOD_NAME, "ExecuteSQL", m_cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollection));

                // SQLの実行
                intInsCnt = m_cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollection);

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

            return intInsCnt;

        }
        #endregion

        #region 宛名個人制御Temp物理削除
        // ************************************************************************************************
        // * メソッド名     宛名個人制御Temp物理削除
        // * 
        // * 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 宛名個人制御Tempのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteABKojinSeigyoTemp(DataRow csDataRow, string strSakujoKB)
        {
            const string THIS_METHOD_NAME = "DeleteABKojinSeigyoTemp";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // 削除区分のチェックを行う
                if (!(strSakujoKB == "D"))
                {
                    // エラー定義を取得
                    m_cfError = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    objErrorStruct = m_cfError.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null | string.IsNullOrEmpty(m_strDelButuriSQL) | m_cfDelButuriUFParameterCollection == null)
                {

                    CreateDeleteButsuriSQL();

                }

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollection)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(THIS_METHOD_NAME, m_cfRdb.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection) + "】");

                // SQLの実行
                intDelCnt = m_cfRdb.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection);

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

            return intDelCnt;

        }
        #endregion

        #region InsertSQL文作成
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
            var csInsertColumn = new StringBuilder();                 // INSERT用カラム定義
            var csInsertParam = new StringBuilder();                  // INSERT用パラメータ定義
            var csInsertSQL = new StringBuilder();                // SQL文文字列
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // INSERT SQL文の作成
                csInsertSQL.Append("INSERT INTO ");
                csInsertSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME);
                csInsertSQL.Append(" (");

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollection = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    csInsertColumn.Append(csDataColumn.ColumnName);
                    csInsertColumn.Append(", ");
                    csInsertParam.Append(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollection.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                csInsertSQL.Append(csInsertColumn.ToString().Trim().Trim(","));
                csInsertSQL.Append(") VALUES (");
                csInsertSQL.Append(csInsertParam.ToString().Trim().TrimEnd(","));
                csInsertSQL.Append(")");

                // メンバ変数に設定する
                m_strInsertSQL = csInsertSQL.ToString();

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

        #region DeleteSQL文作成
        // ************************************************************************************************
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL()
        {
            const string THIS_METHOD_NAME = "CreateDeleteButsuriSQL";
            UFParameterClass cfUFParameterClass;
            var csDeleteButsuriSQL = new StringBuilder();                // SQL文文字列

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // 物理DELETE SQL文の作成
                csDeleteButsuriSQL.Append("DELETE FROM ");
                csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME);
                csDeleteButsuriSQL.Append(" WHERE ");
                csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.KEYCD);
                csDeleteButsuriSQL.Append(" = ");
                csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.KEY_KEYCD);

                // メンバ変数に設定する。
                m_strDelButuriSQL = csDeleteButsuriSQL.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollection = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.KEY_KEYCD;
                m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass);

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

        #region 宛名個人制御Tempスキーマ取得
        // ************************************************************************************************
        // * メソッド名     宛名個人制御Tempスキーマ取得
        // * 
        // * 構文           Public Function GetSchemaABKojinSeigyoTemp() As DataSet
        // * 
        // * 機能　　    　 宛名個人制御Tempのスキーマ情報を取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         宛名個人制御Tempのスキーマ（DataSet）
        // *                構造：csABKojinSeigyoTempEntity
        // ************************************************************************************************
        public DataSet GetSchemaABKojinSeigyoTemp()
        {
            const string THIS_METHOD_NAME = "GetSchemaABKojinSeigyoTemp";  // メソッド名
            DataSet csABKojinSeigyoTempEntity;                         // 個人制御情報Tempデータ

            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                // 宛名個人制御Tempのテーブルスキーマを取得する
                csABKojinSeigyoTempEntity = m_cfRdb.GetTableSchema(ABKojinSeigyoTempEntity.TABLE_NAME);

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

            return csABKojinSeigyoTempEntity;

        }
        #endregion

        #endregion

    }
}
