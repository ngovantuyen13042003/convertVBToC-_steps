// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        送付先_標準マスタＤＡ(ABSfsk_HyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/10/20 早崎 雄矢
// *
// * 著作権          （株）電算 
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2024/6/11   000001    【AB-9901-1】不具合対応
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABSfsk_HyojunBClass
    {
        #region メンバ変数

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABSfsk_HyojunBClass";
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード
        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";
        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        public bool m_blnBatch = false;                                            // バッチフラグ
        private DataSet m_csDataSchma;                                                // スキーマ保管用データセット
        private DataSet m_csDataSchma_Hyojun;                                         // スキーマ保管用データセット_標準版

        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                                          // エラー処理クラス
        private UFDateClass m_cfDateClass;                                            // 日付クラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDeleteSQL;                                                // DELETE用SQL（物理）
        private string m_strDelRonriSQL;                                              // DELETE用SQL（論理）
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;      // DELETE用パラメータコレクション（物理）
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // DELETE用パラメータコレクション（論理）

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData,
        // *                                ByVal cfConfigDataClass As UFConfigDataClass,
        // *                                ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // *                 cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABSfsk_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDeleteUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     送付先マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　 送付先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // * 
        // * 戻り値         取得した送付先マスタの該当データ（DataSet）
        // *
        // ************************************************************************************************
        public DataSet GetSfskBHoshu(string strJuminCD)
        {

            return GetSfskBHoshu(strJuminCD, string.Empty, string.Empty, string.Empty, false);
        }

        // ************************************************************************************************
        // * メソッド名     送付先マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 送付先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String    :住民コード
        // *                blnSakujoFG As Boolean  :削除フラグ
        // * 
        // * 戻り値         取得した送付先マスタの該当データ（DataSet）
        // *
        // ************************************************************************************************
        public DataSet GetSfskBHoshu(string strJuminCD, bool blnSakujoFG)
        {

            return GetSfskBHoshu(strJuminCD, string.Empty, string.Empty, string.Empty, blnSakujoFG);

        }

        // ************************************************************************************************
        // * メソッド名     送付先マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
        // *                                                          ByVal strGyomuCD As String, _
        // *                                                          ByVal strGyomunaiShuCD As String, _
        // *                                                          ByVal strTorokurenban As String) As DataSet
        // * 
        // * 機能           送付先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String        :住民コード
        // *                strGyomuCD As String        :業務コード
        // *                strGyomunaiShuCD As String  :業務内種別コード
        // *                strTorokurenban As String   :登録連番
        // * 
        // * 戻り値         取得した送付先マスタの該当データ（DataSet）
        // *
        // ************************************************************************************************
        public DataSet GetSfskBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiShuCD, string strTorokurenban)
        {

            return GetSfskBHoshu(strJuminCD, strGyomuCD, strGyomunaiShuCD, strTorokurenban, true);

        }

        // ************************************************************************************************
        // * メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
        // *                                                          ByVal strGyomuCD As String, _
        // *                                                          ByVal strGyomunaiShuCD As String, _
        // *                                                          ByVal strTorokurenban As String, _
        // *                                                          ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 送付先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String        :住民コード
        // *                strGyomuCD As String        :業務コード
        // *                strGyomunaiShuCD As String  :業務内種別コード
        // *                strTorokurenban As String   :登録連番
        // *                blnSakujoFG As Boolean      :削除フラグ
        // * 
        // * 戻り値         取得した送付先マスタの該当データ（DataSet）
        // *
        // ************************************************************************************************
        public DataSet GetSfskBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiShuCD, string strTorokurenban, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetSfskBHoshu";            // このメソッド名
            DataSet csSfskEntity;                                     // 送付先マスタデータ
            string strSQL;                                            // SQL文文字列
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnSakujo;                                        // 削除データ読み込み

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                int intWkKensu;
                intWkKensu = m_cfRdbClass.p_intMaxRows();

                // SQL,パラメータコレクションの作成
                blnSakujo = blnSakujoFG;
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                strSQL = CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiShuCD, true, strTorokurenban, blnSakujo, cfUFParameterCollectionClass);

                // RDBアクセスログ出力
                if (m_blnBatch == false)
                {
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                }

                // SQLの実行 DataSetの取得
                csSfskEntity = m_csDataSchma.Clone();
                csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskHyojunEntity.TABLE_NAME, cfUFParameterCollectionClass, false);


                m_cfRdbClass.p_intMaxRows = intWkKensu;

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

            return csSfskEntity;

        }

        // ************************************************************************************************
        // * メソッド名     送付先_標準データ作成
        // * 
        // * 構文           Public Function CreateSfskHyojunData(ByVal csDataRow As DataRow, ByVal csSfskEntity As DataSet) As DataRow
        // *                                      
        // * 
        // * 機能　　    　 送付先_標準データを作成する
        // * 
        // * 引数           csDataRow As DataRow      : 送付先データ
        // *                csSfskEntity As DataSet   : 送付先エンティティ
        // * 
        // * 戻り値         DataRow
        // ************************************************************************************************
        public DataRow CreateSfskHyojunData(DataRow csDataRow, DataSet csSfskEntity)
        {
            const string THIS_METHOD_NAME = "CreateSfskHyojunData";
            DataRow[] csSfskHyojunRows;
            DataRow csSfskHyojunRow;
            StringBuilder strSelect;                                         // 抽出SQL

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 送付先_標準のDateRowを作成
                csSfskHyojunRow = csSfskEntity.Tables[ABSfskHyojunEntity.TABLE_NAME].NewRow();

                // レコードの特定
                strSelect = new StringBuilder();
                strSelect.Append(ABSfskHyojunEntity.GYOMUCD);
                strSelect.Append("='");
                strSelect.Append(Convert.ToString(csDataRow[ABSfskEntity.GYOMUCD]));
                strSelect.Append("' AND ");

                strSelect.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD);
                strSelect.Append("='");
                strSelect.Append(Convert.ToString(csDataRow[ABSfskEntity.GYOMUNAISHU_CD]));
                strSelect.Append("' AND ");

                strSelect.Append(ABSfskHyojunEntity.TOROKURENBAN);
                strSelect.Append("='");
                strSelect.Append(Convert.ToString(csDataRow[ABSfskEntity.TOROKURENBAN]));
                strSelect.Append("'");

                csSfskHyojunRows = csSfskEntity.Tables[ABSfskHyojunEntity.TABLE_NAME].Select(strSelect.ToString());
                csSfskHyojunRow = csSfskHyojunRows[0];

                // 送付先のデータを送付先_標準に変換
                foreach (DataColumn csDataHyojunColumn in csSfskHyojunRow.Table.Columns)
                {
                    foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    {
                        if (!(csDataColumn.ColumnName == ABSfskEntity.KOSHINCOUNTER))
                        {
                            // カラム名が一致するデータを代入
                            if (csDataColumn.ColumnName == csDataHyojunColumn.ColumnName)
                            {

                                csSfskHyojunRow[csDataHyojunColumn.ColumnName] = csDataRow[csDataColumn.ColumnName];

                                break;

                            }
                        }
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw;
            }

            return csSfskHyojunRow;
        }

        // ************************************************************************************************
        // * メソッド名     送付先マスタ追加
        // * 
        // * 構文           Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 送付先マスタにデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertSfskB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertSfskB";                // このメソッド名
                                                                          // パラメータクラス
            int intInsCnt;                                        // 追加件数
            string strUpdateDateTime;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL) || m_cfInsertUFParameterCollectionClass is null)
                {

                    CreateInsertSQL(csDataRow);

                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);          // 作成日時

                // 個別項目編集を行う
                csDataRow[ABSfskHyojunEntity.SFSKTOROKUYMD] = UFVBAPI.Left(strUpdateDateTime, 8);         // 送付先登録年月日

                // 共通項目の編集を行う
                csDataRow[ABSfskHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;  // 端末ＩＤ
                csDataRow[ABSfskHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                     // 削除フラグ
                csDataRow[ABSfskHyojunEntity.KOSHINCOUNTER] = decimal.Zero;                // 更新カウンタ
                csDataRow[ABSfskHyojunEntity.SAKUSEINICHIJI] = strUpdateDateTime;          // 作成日時
                csDataRow[ABSfskHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;   // 作成ユーザー
                csDataRow[ABSfskHyojunEntity.KOSHINNICHIJI] = strUpdateDateTime;           // 更新日時
                csDataRow[ABSfskHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;    // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim());

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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
        // * メソッド名     送付先マスタ更新
        // * 
        // * 構文           Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 送付先マスタのデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateSfskB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateSfskB";                // このメソッド名
                                                                          // パラメータクラス
            int intUpdCnt;                                        // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null || string.IsNullOrEmpty(m_strUpdateSQL) || m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateUpdateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow[ABSfskHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow[ABSfskHyojunEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABSfskHyojunEntity.KOSHINCOUNTER] + 1m;       // 更新カウンタ
                csDataRow[ABSfskHyojunEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);    // 更新日時
                csDataRow[ABSfskHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength()) == ABSfskHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength()), csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString().Trim());
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
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
        // * メソッド名     送付先マスタ削除（論理）
        // * 
        // * 構文           Public Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 送付先マスタのデータを削除（論理）する。
        // * 
        // * 引数           csDataRow As DataRow  :削除データ
        // * 
        // * 戻り値         削除（論理）件数(Integer)
        // ************************************************************************************************
        public int DeleteSfskB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteSfskB（論理）";        // このメソッド名
                                                                      // パラメータクラス
            int intDelCnt;                                        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null || string.IsNullOrEmpty(m_strDelRonriSQL) || m_cfDelRonriUFParameterCollectionClass is null)
                {
                    CreateDeleteRonriSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow[ABSfskHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow[ABSfskHyojunEntity.SAKUJOFG] = SAKUJOFG_ON;                                                      // 削除フラグ
                csDataRow[ABSfskHyojunEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABSfskHyojunEntity.KOSHINCOUNTER] + 1m;       // 更新カウンタ
                csDataRow[ABSfskHyojunEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);    // 更新日時
                csDataRow[ABSfskHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength()) == ABSfskHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
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
        // * メソッド名     送付先マスタ削除（物理）
        // * 
        // * 構文           Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, 
        // *                                                      ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 送付先マスタのデータを削除（物理）する。
        // * 
        // * 引数           csDataRow As DataRow      :削除データ
        // *                strSakujoKB As String     :削除フラグ
        // * 
        // * 戻り値         削除（物理）件数(Integer)
        // ************************************************************************************************
        public int DeleteSfskB(DataRow csDataRow, string strSakujoKB)
        {
            const string THIS_METHOD_NAME = "DeleteSfskB（物理）";
            const string SAKUJOKB_D = "D";                    // 削除区分
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数の削除区分をチェック
                if ((strSakujoKB ?? "") != SAKUJOKB_D)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null || string.IsNullOrEmpty(m_strDeleteSQL) || m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateDeleteButsuriSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength()) == ABSfskHyojunEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Current].ToString();
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
            UFParameterClass cfUFParameterClass;                 // パラメータクラス
            StringBuilder strInsertColumn;                       // 追加SQL文項目文字列
            StringBuilder strInsertParam;                        // 追加SQL文パラメータ文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABSfskHyojunEntity.TABLE_NAME + " ";
                strInsertColumn = new StringBuilder();
                strInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn.Append(csDataColumn.ColumnName);
                    strInsertColumn.Append(", ");
                    strInsertParam.Append(ABSfskHyojunEntity.PARAM_PLACEHOLDER);
                    strInsertParam.Append(csDataColumn.ColumnName);
                    strInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                m_strInsertSQL += "(" + strInsertColumn.ToString().Trim().Trim(",") + ")" + " VALUES (" + strInsertParam.ToString().Trim().TrimEnd(",") + ")";

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
        // * メソッド名     Update用SQL文の作成
        // * 
        // * 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABSfskHyojunEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_KOSHINCOUNTER);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABSfskHyojunEntity.TABLE_NAME + " SET ";

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABSfskHyojunEntity.JUMINCD) && !(csDataColumn.ColumnName == ABSfskHyojunEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABSfskHyojunEntity.SAKUSEINICHIJI))
                    {
                        cfUFParameterClass = new UFParameterClass();

                        // SQL文の作成
                        m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += strWhere.ToString();

                // UPDATE コレクションにキー情報を追加
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 登録連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_KOSHINCOUNTER;
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

        // ************************************************************************************************
        // * メソッド名     論理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteRonriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteRonriSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            var strDelRonriSQL = new StringBuilder();                     // 論理削除SQL文文字列
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABSfskHyojunEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN);

                // DELETE（論理） SQL文の作成
                strDelRonriSQL.Append("UPDATE ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.TABLE_NAME);
                strDelRonriSQL.Append(" SET ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.TANMATSUID);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_TANMATSUID);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.SAKUJOFG);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_SAKUJOFG);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINCOUNTER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINCOUNTER);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINNICHIJI);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINNICHIJI);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINUSER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINUSER);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.RRKNO);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_RRKNO);
                strDelRonriSQL.Append(strWhere.ToString());
                m_strDelRonriSQL = strDelRonriSQL.ToString();

                // DELETE（論理） パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（論理） コレクションにパラメータを追加
                // 端末ＩＤ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 削除フラグ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新ユーザ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 履歴番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_RRKNO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 登録連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteButsuriSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            var strDeleteSQL = new StringBuilder();                       // 物理削除SQL文文字列
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABSfskHyojunEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD);
                strWhere.Append(" AND ");
                strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN);

                // DELETE（物理） SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABSfskHyojunEntity.TABLE_NAME);
                strDeleteSQL.Append(strWhere.ToString());
                m_strDeleteSQL = strDeleteSQL.ToString();

                // DELETE（物理） パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE(物理) コレクションにキー情報を追加
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 登録連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       送付先_標準マスタのデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String
        // *                strValue As String
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";       // このメソッド名
            UFErrorStruct objErrorStruct;                         // エラー定義構造体

            try
            {

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABSfskHyojunEntity.JUMINCD:                               // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABSfskHyojunEntity.GYOMUCD:                               // 業務コード
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABSfskHyojunEntity.GYOMUNAISHU_CD:                        // 業務内種別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUNAISHU_CD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABSfskHyojunEntity.TOROKURENBAN:                          // 登録連番
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TOROKURENBAN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABSfskHyojunEntity.STYMD:                                  // 開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) || strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_STYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case5 when case5 == ABSfskHyojunEntity.EDYMD:                                   // 終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) || strValue == "00000000" || strValue == "99999999"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_EDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case6 when case6 == ABSfskHyojunEntity.RRKNO:                                   // 履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_RRKNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABSfskHyojunEntity.SFSKKANAKATAGAKI:                        // 送付先方書フリガナ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANAKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABSfskHyojunEntity.SFSKTSUSHO:                              // 送付先氏名_通称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTSUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case9 when case9 == ABSfskHyojunEntity.SFSKKANATSUSHO:                           // 送付先氏名_通称_フリガナ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANATSUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABSfskHyojunEntity.SFSKSHIMEIYUSENKB:                         // 送付先氏名_優先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIMEIYUSENKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case11 when case11 == ABSfskHyojunEntity.SFSKEIJISHIMEI:                            // 送付先氏名_外国人英字
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKEIJISHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case12 when case12 == ABSfskHyojunEntity.SFSKKANJISHIMEI:                           // 送付先氏名_外国人漢字
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANJISHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABSfskHyojunEntity.SFSKSHINSEISHAMEI:                          // 送付先申請者名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD:                     // 送付先申請者関係コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAKANKEICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case15 when case15 == ABSfskHyojunEntity.SFSKSHIKUCHOSONCD:                          // 送付先_市区町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case16 when case16 == ABSfskHyojunEntity.SFSKMACHIAZACD:                             // 送付先_町字コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case17 when case17 == ABSfskHyojunEntity.SFSKTODOFUKEN:                               // 送付先_都道府県
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTODOFUKEN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case18 when case18 == ABSfskHyojunEntity.SFSKSHIKUCHOSON:                             // 送付先_市区郡町村名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSON);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case19 when case19 == ABSfskHyojunEntity.SFSKMACHIAZA:                                // 送付先_町字
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case20 when case20 == ABSfskHyojunEntity.SFSKBANCHICD1:                                // 送付先番地コード１
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case21 when case21 == ABSfskHyojunEntity.SFSKBANCHICD2:                                 // 送付先番地コード２
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case22 when case22 == ABSfskHyojunEntity.SFSKBANCHICD3:                                 // 送付先番地コード３
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case23 when case23 == ABSfskHyojunEntity.SFSKKATAGAKICD:                                // 送付先方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case24 when case24 == ABSfskHyojunEntity.SFSKRENRAKUSAKIKB:                             // 連絡先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKRENRAKUSAKIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case25 when case25 == ABSfskHyojunEntity.SFSKKBN:                                       // 送付先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKBN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case26 when case26 == ABSfskHyojunEntity.SFSKTOROKUYMD:                                  // 送付先登録年月日
                        {
                            if (!string.IsNullOrEmpty(strValue))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTOROKUYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }
                    case var case27 when case27 == ABSfskEntity.RESERVE:                                               // リザーブ
                        {
                            break;
                        }
                    // 何もしない
                    case var case28 when case28 == ABSfskEntity.TANMATSUID:                                            // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case29 when case29 == ABSfskHyojunEntity.SAKUJOFG:                                        // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case30 when case30 == ABSfskHyojunEntity.KOSHINCOUNTER:                                   // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case31 when case31 == ABSfskHyojunEntity.SAKUSEINICHIJI:                                  // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case32 when case32 == ABSfskHyojunEntity.SAKUSEIUSER:                                     // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case33 when case33 == ABSfskHyojunEntity.KOSHINNICHIJI:                                   // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case34 when case34 == ABSfskHyojunEntity.KOSHINUSER:                                      // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                }
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
        // * メソッド名     ＳＱＬ文・パラメータコレクション作成
        // * 
        // * 構文           Private Function CreateSql_Param(ByVal strJuminCD As String, 
        // *                                                 ByVal strGyomuCD As String, 
        // *                                                 ByVal strGyomunaiSHUCD As String, 
        // *                                                 ByVal blnGyomunaiSHUCD As Boolean, 
        // *                                                 ByVal strTorokurenban As String, 
        // *                                                 ByVal blnSakujoFG As Boolean,
        // *                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
        // As String
        // * 
        // * 機能　　    　　ＳＱＬ文及びパラメータコレクションを作成し引き渡す。
        // * 
        // * 引数           strJuminCD As String          :住民コード
        // *                strGyomuCD As String          :業務コード
        // *                strGyomunaiSHUCD As String    :業務内種別コード
        // *                blnGyomunaiSHUCD As Boolean   :業務内種別コードの有無（True:有り,False:無し）
        // *                strTorokurenban As String     :登録番号
        // *                blnSakujoFG As Boolean        :削除データの有無(True:有り,False:無し)
        // *                cfUFParameterCollectionClass As UFParameterCollectionClass  :パラメータコレクションクラス
        // * 
        // * 戻り値         ＳＱＬ文(String)
        // *                パラメータコレクションクラス(UFParameterCollectionClass)
        // ************************************************************************************************
        private string CreateSql_Param(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, bool blnGyomunaiSHUCD, string strTorokurenban, bool blnSakujoFG, UFParameterCollectionClass cfUFParameterCollectionClass)
        {
            const string THIS_METHOD_NAME = "CreateSql_Param";            // このメソッド名
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABSfskHyojunEntity.TABLE_NAME);

                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABSfskHyojunEntity.TABLE_NAME, false);
                }

                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABSfskEntity.JUMINCD);                 // 住民コード
                strSQL.Append(" = ");
                strSQL.Append(ABSfskEntity.KEY_JUMINCD);

                // 業務コード
                if (!string.IsNullOrEmpty(strGyomuCD))
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABSfskEntity.GYOMUCD);
                    strSQL.Append(" IN(");
                    strSQL.Append(ABSfskEntity.KEY_GYOMUCD);
                    strSQL.Append(",'00')");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD);
                    strSQL.Append(" IN(");
                    strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD);
                    strSQL.Append(" ,'')");
                }

                if (!string.IsNullOrEmpty(strTorokurenban))
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABSfskEntity.TOROKURENBAN);
                    strSQL.Append(" = ");
                    strSQL.Append(ABSfskEntity.KEY_TOROKURENBAN);
                }

                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABSfskEntity.SAKUJOFG);            // 削除フラグ
                    strSQL.Append(" <> ");
                    strSQL.Append(SAKUJOFG_ON);
                }

                // ソート
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABSfskEntity.GYOMUCD);
                strSQL.Append(" DESC,");
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD);
                strSQL.Append(" DESC");

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = strGyomuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD;
                if (blnGyomunaiSHUCD)
                {
                    cfUFParameterClass.Value = strGyomunaiSHUCD;
                }
                else
                {
                    cfUFParameterClass.Value = string.Empty;
                }
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 登録連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN;
                cfUFParameterClass.Value = strTorokurenban;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

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

            return strSQL.ToString();

        }

        #endregion

    }
}
