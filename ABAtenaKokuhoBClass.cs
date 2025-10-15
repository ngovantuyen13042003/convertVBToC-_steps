// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        宛名国保ＤＡ(ABAtenaKokuhoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/26　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/17 000001     追加時、共通項目を設定する
// * 2003/05/21 000002     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000003     RDBアクセスログの修正
// * 2003/09/11 000004     国保番号で取得するメソッドの仕様追加
// * 2003/10/31 000005     ファイルレイアウト変更に伴う修正
// * 2003/11/18 000006     仕様変更：項目名の変更
// * 2004/11/11 000007     データチェックを行なわない
// * 2005/02/16 000008     レスポンス改善：ＳＱＬ文作成の修正     
// * 2010/04/16 000009     VS2008対応（比嘉）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABAtenaKokuhoBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strInsertSQL;                                            // INSERT用SQL
        private string m_strUpdateSQL;                                            // UPDATE用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaKokuhoBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
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
        public ABAtenaKokuhoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名国保マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaKokuho(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　宛名国保マスタより該当データを取得する。。
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // * 
        // * 戻り値         取得した宛名国保マスタの該当データ（DataSet）
        // *                   構造：csAtenaKokuhoEntity    インテリセンス：ABAtenaKokuhoEntity
        // ************************************************************************************************
        public DataSet GetAtenaKokuho(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetAtenaKokuho";
            DataSet csAtenaKokuhoEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaKokuhoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKokuhoEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKokuhoEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csAtenaKokuhoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaKokuhoEntity.TABLE_NAME, cfUFParameterCollectionClass);

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

            return csAtenaKokuhoEntity;

        }

        // *履歴番号 000004 2003/09/11 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名国保マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaKokuhoBango(ByVal strKokuhoNO As String) As DataSet
        // * 
        // * 機能　　    　　宛名国保マスタより該当データを取得する。
        // * 
        // * 引数           strKokuhoNO As String  : 国保番号
        // * 
        // * 戻り値         取得した宛名国保マスタの該当データ（DataSet）
        // *                   構造：csAtenaKokuhoEntity    インテリセンス：ABAtenaKokuhoEntity
        // ************************************************************************************************
        public DataSet GetAtenaKokuhoBango(string strKokuhoNO)
        {
            DataSet csAtenaKokuhoEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaKokuhoEntity.KOKUHONO);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKokuhoEntity.PARAM_KOKUHONO);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKokuhoEntity.SAKUJOFG);
                strSQL.Append(" <> ");
                strSQL.Append(ABAtenaKokuhoEntity.PARAM_SAKUJOFG);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_KOKUHONO;
                cfUFParameterClass.Value = strKokuhoNO;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_SAKUJOFG;
                cfUFParameterClass.Value = "1";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csAtenaKokuhoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaKokuhoEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csAtenaKokuhoEntity;

        }
        // *履歴番号 000004 2003/09/11 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名国保マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaKokuho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名国保マスタにデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertAtenaKokuho(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertAtenaKokuho";
            // * corresponds to VS2008 Start 2010/04/16 000009
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000009
            int intInsCnt;
            string strUpdateDateTime;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    // *履歴番号 000008 2005/02/16 修正開始
                    CreateInsertSQL(csDataRow);
                    // Call CreateSQL(csDataRow)
                    // *履歴番号 000008 2005/02/16 修正終了
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABAtenaKokuhoEntity.TANMATSUID] = m_cfControlData.m_strClientId;               // 端末ＩＤ
                csDataRow[ABAtenaKokuhoEntity.SAKUJOFG] = "0";                                           // 削除フラグ
                csDataRow[ABAtenaKokuhoEntity.KOSHINCOUNTER] = decimal.Zero;                             // 更新カウンタ
                csDataRow[ABAtenaKokuhoEntity.SAKUSEINICHIJI] = strUpdateDateTime;                       // 作成日時
                csDataRow[ABAtenaKokuhoEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;                // 作成ユーザー
                csDataRow[ABAtenaKokuhoEntity.KOSHINNICHIJI] = strUpdateDateTime;                        // 更新日時
                csDataRow[ABAtenaKokuhoEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                 // 更新ユーザー

                // *履歴番号 000007 2004/11/11 修正開始
                // 当クラスのデータ整合性チェックを行う
                // For Each csDataColumn In csDataRow.Table.Columns
                // 'データ整合性チェック
                // CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim())
                // Next csDataColumn
                // *履歴番号 000007 2004/11/11 修正終了

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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
        // * メソッド名     宛名国保マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaKokuho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名国保マスタのデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateAtenaKokuho(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateAtenaKokuho";
            // * corresponds to VS2008 Start 2010/04/16 000009
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000009
            int intUpdCnt;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    // *履歴番号 000008 2005/02/16 修正開始
                    CreateUpdateSQL(csDataRow);
                    // Call CreateSQL(csDataRow)
                    // *履歴番号 000008 2005/02/16 修正終了
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaKokuhoEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABAtenaKokuhoEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABAtenaKokuhoEntity.KOSHINCOUNTER] + 1m;       // 更新カウンタ
                csDataRow[ABAtenaKokuhoEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow[ABAtenaKokuhoEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaKokuhoEntity.PREFIX_KEY.RLength()) == ABAtenaKokuhoEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // *履歴番号 000007 2004/11/11 修正開始
                        // データ整合性チェック
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.Length), csDataRow[cfParam.ParameterName.Substring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current].ToString().Trim())
                        // *履歴番号 000007 2004/11/11 修正終了
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
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
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateAtenaKokuho");
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

        // *履歴番号 000008 2005/02/16 追加開始
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
                m_strInsertSQL = "INSERT INTO " + ABAtenaKokuhoEntity.TABLE_NAME + " ";
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
                    strInsertParam += ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
        // * 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　 UPDATESQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            UFParameterClass cfUFParameterClass;
            string strUpdateWhere;
            string strUpdateParam;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaKokuhoEntity.TABLE_NAME + " SET ";
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
                        m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += " WHERE " + ABAtenaKokuhoEntity.JUMINCD + " = " + ABAtenaKokuhoEntity.KEY_JUMINCD + " AND " + ABAtenaKokuhoEntity.KOSHINCOUNTER + " = " + ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER;

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER;
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
        // *履歴番号 000008 2005/02/16 追加終了

        // *履歴番号 000008 2005/02/16 削除開始
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
        // m_strInsertSQL = "INSERT INTO " + ABAtenaKokuhoEntity.TABLE_NAME + " "
        // strInsertColumn = ""
        // strInsertParam = ""

        // ' UPDATE SQL文の作成
        // m_strUpdateSQL = "UPDATE " + ABAtenaKokuhoEntity.TABLE_NAME + " SET "
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
        // strInsertParam += ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // ' SQL文の作成
        // m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // ' INSERT コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
        // m_strUpdateSQL += " WHERE " + ABAtenaKokuhoEntity.JUMINCD + " = " + ABAtenaKokuhoEntity.KEY_JUMINCD + " AND " + _
        // ABAtenaKokuhoEntity.KOSHINCOUNTER + " = " + ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER

        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER
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
        // *履歴番号 000008 2005/02/16 追加終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       宛名国保マスタのデータ整合性チェックを行います。
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
                                                          // * corresponds to VS2008 Start 2010/04/16 000009
                                                          // '*履歴番号 000001 2003/10/31 追加開始
                                                          // Const THIS_DBTABLE_NAME As String = "ＡＢ宛名国保."
                                                          // '*履歴番号 000001 2003/10/31 追加終了
                                                          // * corresponds to VS2008 End 2010/04/16 000009

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABAtenaKokuhoEntity.JUMINCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABAtenaKokuhoEntity.SHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABAtenaKokuhoEntity.KYUSHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABAtenaKokuhoEntity.KOKUHONO:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHONO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKBMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKBRYAKUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABAtenaKokuhoEntity.KOKUHOGAKUENKB:
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKBMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case9 when case9 == ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKBRYAKUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHUTOKUYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case11 when case11 == ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSOSHITSUYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case12 when case12 == ABAtenaKokuhoEntity.KOKUHOTISHKKB:
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKBMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKBRYAKUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case15 when case15 == ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB:
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHONHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    // *履歴番号 000006 2003/11/18 修正開始
                    // Case ABAtenaKokuhoEntity.KOKUHOTIAHKHONHIKBMEISHO
                    case var case16 when case16 == ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO:       // 国保退職本被区分正式名称
                        {
                            // *履歴番号 000006 2003/11/18 修正終了
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTIAHKHONHIKBMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case17 when case17 == ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHONHIKBRYAKUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case18 when case18 == ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKGAITOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case19 when case19 == ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHIGAITOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    // *履歴番号 000005 2003/10/31 追加開始
                    case var case20 when case20 == ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO:
                        {
                            break;
                        }
                    // If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + THIS_DBTABLE_NAME + strColumnName, objErrorStruct.m_strErrorCode)
                    // End If
                    // *履歴番号 000005 2003/10/31 追加終了
                    case var case21 when case21 == ABAtenaKokuhoEntity.KOKUHOHOKENSHONO:
                        {
                            break;
                        }
                    // *履歴番号 000005 2003/10/31 修正開始
                    // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOHOKENSHONO)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // End If
                    // If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + THIS_DBTABLE_NAME + strColumnName, objErrorStruct.m_strErrorCode)
                    // End If
                    // *履歴番号 000005 2003/10/31 修正終了
                    case var case22 when case22 == ABAtenaKokuhoEntity.TANMATSUID:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case23 when case23 == ABAtenaKokuhoEntity.SAKUJOFG:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case24 when case24 == ABAtenaKokuhoEntity.KOSHINCOUNTER:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case25 when case25 == ABAtenaKokuhoEntity.SAKUSEINICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case26 when case26 == ABAtenaKokuhoEntity.SAKUSEIUSER:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case27 when case27 == ABAtenaKokuhoEntity.KOSHINNICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case28 when case28 == ABAtenaKokuhoEntity.KOSHINUSER:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINUSER);
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
