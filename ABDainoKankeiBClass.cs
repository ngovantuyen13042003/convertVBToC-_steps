// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        代納関係ＤＡ(ABDainoKankeiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/19　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/14 000001     整合性チェックは、空白をとって処理する
// * 2003/03/27 000002     エラー処理クラスの参照先を"AB"固定にする
// * 2003/05/21 000003     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000004     RDBアクセスログの修正
// * 2005/01/25 000005     速度改善２：（宮沢）
// * 2010/04/16  000006      VS2008対応（比嘉）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABDainoKankeiBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDeleteSQL;                        // DELETE用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;  // DELETE用パラメータコレクション

        // * 履歴番号 000005 2005/01/25 追加開始（宮沢）
        private DataSet m_csDainoKankeiCDMSTEntity;
        // * 履歴番号 000005 2005/01/25 追加終了（宮沢）

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABDainoKankeiBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfUFControlData As UFControlData,
        // * 　　                           ByVal cfUFConfigDataClass As UFConfigDataClass,
        // * 　　                           ByVal cfUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // * 　　            cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // * 　　            cfUFRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABDainoKankeiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_strDeleteSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDeleteUFParameterCollectionClass = null;
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     代納関係コードマスタ抽出
        // * 
        // * 構文           Public Overloads Function GetDainoKBHoshu() As DataSet
        // * 
        // * 機能       　　代納関係コードマスタより該当データを全件取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         取得した代納関係コードマスタの該当データ（DataSet）
        // *                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
        // ************************************************************************************************
        public DataSet GetDainoKBHoshu()
        {
            const string THIS_METHOD_NAME = "GetDainoKBHoshu";
            DataSet csDainoKankeiCDMSTEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABDainoKankeiCDMSTEntity.TABLE_NAME);

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

            return csDainoKankeiCDMSTEntity;

        }

        // ************************************************************************************************
        // * メソッド名     代納関係コードマスタ抽出（オーバーロード）
        // * 
        // * 構文           Public Overloads Function GetDainoKBHoshu(ByVal strDainoKB As String) As DataSet
        // * 
        // * 機能　　    　　指定された代納区分を条件に代納関係コードマスタの該当データを取得する。
        // * 
        // * 引数           strDainoKB As String  :代納区分
        // * 
        // * 戻り値         取得した代納関係コードマスタの該当データ（DataSet）
        // *                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
        // ************************************************************************************************
        public DataSet GetDainoKBHoshu(string strDainoKB)
        {
            const string THIS_METHOD_NAME = "GetJutogaiBHoshu(String)";
            DataSet csDainoKankeiCDMSTEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB;
                cfUFParameterClass.Value = strDainoKB;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass);

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

            return csDainoKankeiCDMSTEntity;

        }

        // * 履歴番号 000005 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名     代納関係コードマスタ抽出
        // * 
        // * 構文           Public Overloads Function GetDainoKBHoshu2(ByVal strDainoKB As String) As DataSet
        // * 
        // * 機能　　    　　指定された代納区分を条件に代納関係コードマスタの該当データを取得する。
        // * 
        // * 引数           strDainoKB As String  :代納区分
        // * 
        // * 戻り値         取得した代納関係コードマスタの該当データ（DataRow）
        // *                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
        // ************************************************************************************************
        public DataRow[] GetDainoKBHoshu2(string strDainoKB)
        {
            const string THIS_METHOD_NAME = "GetJutogaiBHoshu2(String)";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim csDainoKankeiCDMSTEntity As DataSet
            // * corresponds to VS2008 End 2010/04/16 000006
            var strSQL = new StringBuilder();
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim cfUFParameterClass As UFParameterClass
            // * corresponds to VS2008 End 2010/04/16 000006
            var cfUFParameterCollectionClass = default(UFParameterCollectionClass);
            DataRow[] csDainoKankeiCDMSTDataRows;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (m_csDainoKankeiCDMSTEntity is null)
                {
                    // SQL文の作成
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                    // *履歴番号 000004 2003/08/28 修正終了

                    // SQLの実行 DataSetの取得

                    m_csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass);
                }

                strSQL.RRemove(0, strSQL.RLength());
                strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB);
                strSQL.Append(" = '");
                strSQL.Append(strDainoKB);
                strSQL.Append("'");
                csDainoKankeiCDMSTDataRows = m_csDainoKankeiCDMSTEntity.Tables[ABDainoKankeiCDMSTEntity.TABLE_NAME].Select(strSQL.ToString());

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

            return csDainoKankeiCDMSTDataRows;

        }
        // * 履歴番号 000005 2005/01/25 追加終了（宮沢）

        // ************************************************************************************************
        // * メソッド名     代納関係コードマスタ追加
        // * 
        // * 構文           Public Function InsertDainoKB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  代納関係コードマスタにデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertDainoKB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertDainoKB";
            // * corresponds to VS2008 Start 2010/04/16 000006
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
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABDainoKankeiCDMSTEntity.TANMATSUID] = m_cfControlData.m_strClientId;          // 端末ＩＤ
                csDataRow[ABDainoKankeiCDMSTEntity.SAKUJOFG] = "0";                                      // 削除フラグ
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINCOUNTER] = decimal.Zero;                        // 更新カウンタ
                csDataRow[ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI] = strUpdateDateTime;                  // 作成日時
                csDataRow[ABDainoKankeiCDMSTEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;           // 作成ユーザー
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINNICHIJI] = strUpdateDateTime;                   // 更新日時
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINUSER] = m_cfControlData.m_strUserId;            // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim());

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

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
        // * メソッド名     代納関係コードマスタ更新
        // * 
        // * 構文           Public Function UpdateDainoKB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  代納関係コードマスタのデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateDainoKB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateDainoKB";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000006
            int intUpdCnt;
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim strUpdateDateTime As String
            // * corresponds to VS2008 End 2010/04/16 000006

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow[ABDainoKankeiCDMSTEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                    // 端末ＩＤ
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABDainoKankeiCDMSTEntity.KOSHINCOUNTER] + 1m;     // 更新カウンタ
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow[ABDainoKankeiCDMSTEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                      // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength()) == ABDainoKankeiCDMSTEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength()), csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString().Trim());
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

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

        // ************************************************************************************************
        // * メソッド名     代納関係コードマスタ削除
        // * 
        // * 構文           Public Function DeleteDainoKB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  代納関係コードマスタのデータを削除する。
        // * 
        // * 引数           csDataRow As DataRow  :削除データ
        // * 
        // * 戻り値         削除件数(Integer)
        // ************************************************************************************************
        public int DeleteDainoKB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteDainoKB";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim csDataColumn As DataColumn
            // Dim intIndex As Integer
            // * corresponds to VS2008 End 2010/04/16 000006
            int intDelCnt;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null | string.IsNullOrEmpty(m_strDeleteSQL) | m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength()) == ABDainoKankeiCDMSTEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strDeleteSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "DeleteDainoKB");
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

            return intDelCnt;

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
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";
            string strInsertColumn;
            string strInsertParam;
            UFParameterClass cfUFParameterClass;
            var strDeleteSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " SET ";

                // DELETE SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME);
                strDeleteSQL.Append(" WHERE ");
                strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB);
                strDeleteSQL.Append(" = ");
                strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB);
                strDeleteSQL.Append(" AND ");
                strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER);
                strDeleteSQL.Append(" = ");
                strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER);
                m_strDeleteSQL = strDeleteSQL.ToString();

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");

                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += " WHERE " + ABDainoKankeiCDMSTEntity.DAINOKB + " = " + ABDainoKankeiCDMSTEntity.KEY_DAINOKB + " AND " + ABDainoKankeiCDMSTEntity.KOSHINCOUNTER + " = " + ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER;

                // UPDATE,DELETE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "CreateSQL");
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
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       代納関係コードマスタのデータ整合性チェックを行います。
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
                    case var @case when @case == ABDainoKankeiCDMSTEntity.SHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABDainoKankeiCDMSTEntity.KYUSHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABDainoKankeiCDMSTEntity.DAINOKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABDainoKankeiCDMSTEntity.DAINOKBMEISHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBRYAKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABDainoKankeiCDMSTEntity.TANMATSUID:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABDainoKankeiCDMSTEntity.SAKUJOFG:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABDainoKankeiCDMSTEntity.KOSHINCOUNTER:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case9 when case9 == ABDainoKankeiCDMSTEntity.SAKUSEIUSER:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABDainoKankeiCDMSTEntity.KOSHINNICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case11 when case11 == ABDainoKankeiCDMSTEntity.KOSHINUSER:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINUSER);
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
