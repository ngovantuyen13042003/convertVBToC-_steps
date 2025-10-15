// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         表示名称制御マスタＤＡ(ABMeishoSeigyoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け           2011/04/13
// *
// * 作成者　　　     小池 可那子
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{

    public class ABMeishoSeigyoBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDeleteSQL;                                                // DELETE用SQL（物理）
        private string m_strDelRonriSQL;                                              // DELETE用SQL（論理）
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;      // DELETE用パラメータコレクション（物理）
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // DELETE用パラメータコレクション（論理）
        private DataSet m_csDataSchma;   // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMeishoSeigyoBClass";
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
        public ABMeishoSeigyoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_strDelRonriSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDeleteUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;

            // SQL文の作成
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABMeishoSeigyoEntity.TABLE_NAME, ABMeishoSeigyoEntity.TABLE_NAME, false);

        }
        #endregion

        #region 表示名称制御データ抽出
        // ************************************************************************************************
        // * メソッド名     表示名称制御データ抽出
        // * 
        // * 構文           Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　 引数の条件で表示名称制御マスタを抽出する
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // * 
        // * 戻り値         取得した表示名称制御マスタの該当データ（DataSet）
        // *                   構造：csMeishoSeigyoEntity    インテリセンス：ABMeishoSeigyoEntity
        // ************************************************************************************************
        public DataSet GetMeishoSeigyo(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetMeishoSeigyo";            // このメソッド名
            DataSet csMeishoSeigyoEntity = default;                   // 表示名称制御マスタデータ
            StringBuilder strSQL = null;                           // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ストリングビルダーのインスタンス化
                strSQL = new StringBuilder();

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABMeishoSeigyoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABMeishoSeigyoEntity.KEY_JUMINCD);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABMeishoSeigyoEntity.SHITEICD);
                strSQL.Append(" ASC");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csMeishoSeigyoEntity = m_csDataSchma.Clone();
                csMeishoSeigyoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csMeishoSeigyoEntity, ABMeishoSeigyoEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return csMeishoSeigyoEntity;

        }
        #endregion

        #region 表示名称制御データ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // ************************************************************************************************
        // * メソッド名     表示名称制御データ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String, _
        // *                                                          ByVal strGroupID As String) As DataSet 
        // *                                                        
        // * 機能　　    　 引数の条件で表示名称制御マスタを抽出する(オーバーロード処理）
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // *                strGroupID As String  :グループＩＤ(指定コード）
        // * 
        // * 戻り値         取得した表示名称制御マスタの該当データ（DataSet）
        // *                   構造：csMeishoSeigyoEntity    インテリセンス：ABMeishoSeigyoEntity
        // ************************************************************************************************
        public DataSet GetMeishoSeigyo(string strJuminCD, string strGroupID)
        {
            const string THIS_METHOD_NAME = "GetMeishoSeigyo";            // このメソッド名
            DataSet csMeishoSeigyoEntity = default;                   // 表示名称制御マスタデータ
            StringBuilder strSQL = null;                           // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ストリングビルダーのインスタンス化
                strSQL = new StringBuilder();

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABMeishoSeigyoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABMeishoSeigyoEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABMeishoSeigyoEntity.SHITEICD);
                strSQL.Append(" = ");
                strSQL.Append(ABMeishoSeigyoEntity.KEY_SHITEICD);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // グループＩＤ(指定コード）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD;
                cfUFParameterClass.Value = strGroupID;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csMeishoSeigyoEntity = m_csDataSchma.Clone();
                csMeishoSeigyoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csMeishoSeigyoEntity, ABMeishoSeigyoEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return csMeishoSeigyoEntity;

        }
        #endregion

        #region 表示名称制御マスタ追加
        // ************************************************************************************************
        // * メソッド名     表示名称制御マスタ追加
        // * 
        // * 構文           Public Function InsertMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 引数のデータを表示名称制御マスタに追加する
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertMeishoSeigyo(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertMeishoSeigyo";         // このメソッド名
                                                                          // パラメータクラス
            int intInsCnt;                                        // 追加件数
            string strUpdateDateTime = string.Empty;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL.Trim()) || m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");           // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId;               // 端末ＩＤ
                csDataRow(ABMeishoSeigyoEntity.SAKUJOFG) = "0";                                           // 削除フラグ
                csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = decimal.Zero;                             // 更新カウンタ
                csDataRow(ABMeishoSeigyoEntity.SAKUSEINICHIJI) = strUpdateDateTime;                       // 作成日時
                csDataRow(ABMeishoSeigyoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;                // 作成ユーザー
                csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = strUpdateDateTime;                        // 更新日時
                csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                 // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");




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
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return intInsCnt;

        }
        #endregion

        #region 表示名称制御マスタ更新
        // ************************************************************************************************
        // * メソッド名     表示名称制御マスタ更新
        // * 
        // * 構文           Public Function UpdateMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 引数のデータを表示名称制御マスタに更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateMeishoSeigyo(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateMeishoSeigyo";         // このメソッド名
                                                                          // パラメータクラス
            int intUpdCnt;                                        // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null || string.IsNullOrEmpty(m_strUpdateSQL.Trim()) || m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) + 1m;      // 更新カウンタ
                csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) == ABMeishoSeigyoEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim);
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




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
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw;

            }

            return intUpdCnt;
        }
        #endregion

        #region 表示名称制御マスタ削除(論理）
        // ************************************************************************************************
        // * メソッド名     表示名称制御マスタ削除（論理）
        // * 
        // * 構文           Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 引数のデータを表示名称制御マスタから削除（論理）する。
        // * 
        // * 引数           csDataRow As DataRow  :削除データ
        // * 
        // * 戻り値         削除（論理）件数(Integer)
        // ************************************************************************************************
        public int DeleteMeishoSeigyo(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteMeishoSeigyo（論理）";  // このメソッド名
                                                                       // パラメータクラス
            int intDelCnt;                                         // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null || string.IsNullOrEmpty(m_strDelRonriSQL.Trim()) || m_cfDelRonriUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                 // 端末ＩＤ
                csDataRow(ABMeishoSeigyoEntity.SAKUJOFG) = 1;                                                               // 削除フラグ
                csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) + 1m;    // 更新カウンタ
                csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff"); // 更新日時
                csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                   // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) == ABMeishoSeigyoEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】");




                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw;

            }

            return intDelCnt;

        }
        #endregion

        #region 表示名称制御マスタ削除（物理）
        // ************************************************************************************************
        // * メソッド名     表示名称制御マスタ削除（物理）
        // * 
        // * 構文           Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow, 
        // *                                                      ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　  引数のデータを表示名称制御マスタから削除（物理）する。
        // * 
        // * 引数           csDataRow As DataRow      :削除データ
        // *                strSakujoKB As String     :削除フラグ
        // * 
        // * 戻り値         削除（物理）件数(Integer)
        // ************************************************************************************************
        public int DeleteMeishoSeigyo(DataRow csDataRow, string strSakujoKB)
        {
            const string THIS_METHOD_NAME = "DeleteMeishoSeigyo（物理）";  // このメソッド名
            UFErrorStruct objErrorStruct;                              // エラー定義構造体
                                                                       // パラメータクラス
            int intDelCnt;                                         // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数の削除区分をチェック
                if (strSakujoKB.Trim() != "D")
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null || string.IsNullOrEmpty(m_strDeleteSQL.Trim()) || m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) == ABMeishoSeigyoEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");




                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw;

            }

            return intDelCnt;

        }
        #endregion

        #region SQL文作成
        // ************************************************************************************************
        // * メソッド名     SQL文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　 INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";              // このメソッド名
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            string strInsertColumn = string.Empty;                // 追加SQL文項目文字列
            string strInsertParam = string.Empty;                 // 追加SQL文パラメータ文字列
            StringBuilder strDelRonriSQL = null;               // 論理削除SQL文文字列
            StringBuilder strDeleteSQL = null;                 // 物理削除SQL文文字列
            StringBuilder strWhere = null;                     // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ストリングビルダーのインスタンス化
                strDelRonriSQL = new StringBuilder();
                strDeleteSQL = new StringBuilder();
                strWhere = new StringBuilder();

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABMeishoSeigyoEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABMeishoSeigyoEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABMeishoSeigyoEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABMeishoSeigyoEntity.SHITEICD);
                strWhere.Append(" = ");
                strWhere.Append(ABMeishoSeigyoEntity.KEY_SHITEICD);
                strWhere.Append(" AND ");
                strWhere.Append(ABMeishoSeigyoEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABMeishoSeigyoEntity.TABLE_NAME + " SET ";

                // DELETE（論理） SQL文の作成
                strDelRonriSQL.Append("UPDATE ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME);
                strDelRonriSQL.Append(" SET ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.TANMATSUID);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_TANMATSUID);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.SAKUJOFG);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_SAKUJOFG);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINCOUNTER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINCOUNTER);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINNICHIJI);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINNICHIJI);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINUSER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINUSER);
                strDelRonriSQL.Append(strWhere.ToString());
                m_strDelRonriSQL = strDelRonriSQL.ToString();

                // DELETE（物理） SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME);
                strDeleteSQL.Append(strWhere.ToString());
                m_strDeleteSQL = strDeleteSQL.ToString();

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（論理） パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（物理） パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                m_strUpdateSQL += strWhere.ToString();

                // UPDATE,DELETE(物理) コレクションにキー情報を追加
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 指定コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // DELETE（論理） コレクションにパラメータを追加
                // 端末ＩＤ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 削除フラグ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新ユーザ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 指定コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }
        }
        #endregion

        #region データ整合性チェック
        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       表示名称制御マスタのデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String   :項目名称
        // *                strValue As String        :値
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";       // このメソッド名
            UFErrorStruct objErrorStruct;                         // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABMeishoSeigyoEntity.JUMINCD:                        // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABMeishoSeigyoEntity.SHICHOSONCD:                    // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABMeishoSeigyoEntity.KYUSHICHOSONCD:                 // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABMeishoSeigyoEntity.SHITEICD:                       // 指定コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SHITEICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABMeishoSeigyoEntity.RIYOFG:                         // 利用名フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_RIYOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // TODO:【リサーブ1～リサーブ5】現時点(2011/04/15)では対応しないが、住基法改正時に対応が必要。
                    case var case5 when case5 == ABMeishoSeigyoEntity.RESERVE1:                       // リサーブ1
                        {
                            break;
                        }
                    // 何もしない
                    case var case6 when case6 == ABMeishoSeigyoEntity.RESERVE2:                       // リサーブ2
                        {
                            break;
                        }
                    // 何もしない
                    case var case7 when case7 == ABMeishoSeigyoEntity.RESERVE3:                       // リサーブ3
                        {
                            break;
                        }
                    // 何もしない
                    case var case8 when case8 == ABMeishoSeigyoEntity.RESERVE4:                       // リサーブ4
                        {
                            break;
                        }
                    // 何もしない
                    case var case9 when case9 == ABMeishoSeigyoEntity.RESERVE5:                       // リサーブ5
                        {
                            break;
                        }
                    // 何もしない

                    case var case10 when case10 == ABMeishoSeigyoEntity.TANMATSUID:                     // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case11 when case11 == ABMeishoSeigyoEntity.SAKUJOFG:                       // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case12 when case12 == ABMeishoSeigyoEntity.KOSHINCOUNTER:                  // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABMeishoSeigyoEntity.SAKUSEINICHIJI:                 // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABMeishoSeigyoEntity.SAKUSEIUSER:                    // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case15 when case15 == ABMeishoSeigyoEntity.KOSHINNICHIJI:                  // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case16 when case16 == ABMeishoSeigyoEntity.KOSHINUSER:                     // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINUSER);
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