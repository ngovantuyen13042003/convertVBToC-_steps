// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        宛名選挙ＤＡ(ABAtenaSenkyoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/07　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/17 000001     追加時、共通項目を設定する
// * 2003/05/21 000002     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000003     RDBアクセスログの修正
// * 2004/11/11 000004     データチェックを行なわない
// * 2005/02/17 000005     レスポンス改善：ＳＱＬ文作成の修正     
// * 2010/04/16 000006     VS2008対応（比嘉）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAtenaSenkyoBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                                            // INSERT用SQL
        private string m_strUpdateSQL;                                            // UPDATE用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaSenkyoBClass";
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
        public ABAtenaSenkyoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // メンバ変数の初期化
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名選挙マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaSenkyo(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　宛名選挙マスタより該当データを取得する。。
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // * 
        // * 戻り値         取得した宛名選挙マスタの該当データ（DataSet）
        // *                   構造：csAtenaSenkyoEntity    インテリセンス：ABAtenaSenkyoEntity
        // ************************************************************************************************
        public DataSet GetAtenaSenkyo(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetAtenaSenkyo";
            DataSet csAtenaSenkyoEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaSenkyoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaSenkyoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaSenkyoEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaSenkyoEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csAtenaSenkyoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaSenkyoEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csAtenaSenkyoEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名選挙マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaSenkyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名選挙マスタにデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertAtenaSenkyo(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertAtenaSenkyo";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000006
            int intInsCnt;
            string strUpdateDateTime;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    // *履歴番号 000005 2005/02/17 修正開始
                    CreateInsertSQL(csDataRow);
                    // Call CreateSQL(csDataRow)
                    // *履歴番号 000005 2005/02/17 修正終了
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABAtenaSenkyoEntity.TANMATSUID) = m_cfControlData.m_strClientId;               // 端末ＩＤ
                csDataRow(ABAtenaSenkyoEntity.SAKUJOFG) = "0";                                           // 削除フラグ
                csDataRow(ABAtenaSenkyoEntity.KOSHINCOUNTER) = decimal.Zero;                             // 更新カウンタ
                csDataRow(ABAtenaSenkyoEntity.SAKUSEINICHIJI) = strUpdateDateTime;                       // 作成日時
                csDataRow(ABAtenaSenkyoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;                // 作成ユーザー
                csDataRow(ABAtenaSenkyoEntity.KOSHINNICHIJI) = strUpdateDateTime;                        // 更新日時
                csDataRow(ABAtenaSenkyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                 // 更新ユーザー

                // *履歴番号 000004 2004/11/11 修正開始
                // 当クラスのデータ整合性チェックを行う
                // For Each csDataColumn In csDataRow.Table.Columns
                // ' データ整合性チェック
                // CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
                // Next csDataColumn
                // *履歴番号 000004 2004/11/11 修正終了

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaSenkyoEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     宛名選挙マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaSenkyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名選挙マスタのデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaSenkyo(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaSenkyo";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000006
            int intUpdCnt;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    // *履歴番号 000005 2005/02/17 修正開始
                    CreateUpdateSQL(csDataRow);
                    // Call CreateSQL(csDataRow)
                    // *履歴番号 000005 2005/02/17 修正終了
                }

                // 共通項目の編集を行う
                csDataRow(ABAtenaSenkyoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABAtenaSenkyoEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABAtenaSenkyoEntity.KOSHINCOUNTER) + 1m;       // 更新カウンタ
                csDataRow(ABAtenaSenkyoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABAtenaSenkyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaSenkyoEntity.PREFIX_KEY.RLength) == ABAtenaSenkyoEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaSenkyoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // *履歴番号 000004 2004/11/11 修正開始
                        // データ整合性チェック
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaSenkyoEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaSenkyoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
                        // *履歴番号 000004 2004/11/11 修正終了
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaSenkyoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw exException;

            }

            return intUpdCnt;

        }

        // *履歴番号 000005 2005/02/17 追加開始
        // ************************************************************************************************
        // * メソッド名     SQL文の作成
        // * 
        // * 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　 INSERTSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateInsertSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateInsertSQL";
            string strInsertColumn;
            string strInsertParam;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABAtenaSenkyoEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");

                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }
        }

        // ************************************************************************************************
        // * メソッド名     SQL文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;
            string strUpdateWhere;
            string strUpdateParam;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaSenkyoEntity.TABLE_NAME + " SET ";
                strUpdateParam = "";
                strUpdateWhere = "";

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {

                    // 住民ＣＤ（主キー）と作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaInkanEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaInkanEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaInkanEntity.SAKUSEINICHIJI))

                    {

                        cfUFParameterClass = new UFParameterClass();

                        // SQL文の作成
                        m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += " WHERE " + ABAtenaSenkyoEntity.JUMINCD + " = " + ABAtenaSenkyoEntity.KEY_JUMINCD + " AND " + ABAtenaSenkyoEntity.KOSHINCOUNTER + " = " + ABAtenaSenkyoEntity.KEY_KOSHINCOUNTER;

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }
        }

        // *履歴番号 000005 2005/02/17 追加終了

        // *履歴番号 000005 2005/02/17 削除開始
        // '************************************************************************************************
        // '* メソッド名     SQL文の作成
        // '* 
        // '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // '* 
        // '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // '* 
        // '* 引数           csDataRow As DataRow : 更新対象の行
        // '* 
        // '* 戻り値         なし
        // '************************************************************************************************
        // Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // Const THIS_METHOD_NAME As String = "CreateSQL"
        // Dim csDataColumn As DataColumn
        // Dim strInsertColumn As String
        // Dim strInsertParam As String
        // Dim cfUFParameterClass As UFParameterClass
        // Dim strUpdateWhere As String
        // Dim strUpdateParam As String

        // Try
        // ' デバッグログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' SELECT SQL文の作成
        // m_strInsertSQL = "INSERT INTO " + ABAtenaSenkyoEntity.TABLE_NAME + " "
        // strInsertColumn = ""
        // strInsertParam = ""

        // ' UPDATE SQL文の作成
        // m_strUpdateSQL = "UPDATE " + ABAtenaSenkyoEntity.TABLE_NAME + " SET "
        // strUpdateParam = ""
        // strUpdateWhere = ""

        // ' SELECT パラメータコレクションクラスのインスタンス化
        // m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

        // ' UPDATE パラメータコレクションのインスタンス化
        // m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

        // ' パラメータコレクションの作成
        // For Each csDataColumn In csDataRow.Table.Columns
        // cfUFParameterClass = New UFParameterClass()

        // ' INSERT SQL文の作成
        // strInsertColumn += csDataColumn.ColumnName + ", "
        // strInsertParam += ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // ' SQL文の作成
        // m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // ' INSERT コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // Next csDataColumn

        // ' INSERT SQL文のトリミング
        // strInsertColumn = strInsertColumn.Trim()
        // strInsertColumn = strInsertColumn.Trim(CType(",", Char))
        // strInsertParam = strInsertParam.Trim()
        // strInsertParam = strInsertParam.Trim(CType(",", Char))

        // m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

        // ' UPDATE SQL文のトリミング
        // m_strUpdateSQL = m_strUpdateSQL.Trim()
        // m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

        // ' UPDATE SQL文にWHERE句の追加
        // m_strUpdateSQL += " WHERE " + ABAtenaSenkyoEntity.JUMINCD + " = " + ABAtenaSenkyoEntity.KEY_JUMINCD + " AND " + _
        // ABAtenaSenkyoEntity.KOSHINCOUNTER + " = " + ABAtenaSenkyoEntity.KEY_KOSHINCOUNTER

        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.KEY_JUMINCD
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaSenkyoEntity.KEY_KOSHINCOUNTER
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'デバッグログ出力
        // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLogClass.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLogClass.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")
        // ' システムエラーをスローする
        // Throw exException

        // End Try
        // End Sub
        // *履歴番号 000005 2005/02/17 削除終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       宛名選挙マスタのデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String
        // *                strValue As String
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABAtenaSenkyoEntity.JUMINCD:                        // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABAtenaSenkyoEntity.SHICHOSONCD:                    // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABAtenaSenkyoEntity.KYUSHICHOSONCD:                 // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABAtenaSenkyoEntity.SENKYOSHIKAKUKB:                // 選挙資格区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_SENKYOSHIKAKUKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABAtenaSenkyoEntity.TANMATSUID:                     // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABAtenaSenkyoEntity.SAKUJOFG:                       // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABAtenaSenkyoEntity.KOSHINCOUNTER:                  // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABAtenaSenkyoEntity.SAKUSEINICHIJI:                 // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABAtenaSenkyoEntity.SAKUSEIUSER:                    // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case9 when case9 == ABAtenaSenkyoEntity.KOSHINNICHIJI:                  // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABAtenaSenkyoEntity.KOSHINUSER:                     // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENASENKYOB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }
            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;
            }
        }
        #endregion

    }
}
