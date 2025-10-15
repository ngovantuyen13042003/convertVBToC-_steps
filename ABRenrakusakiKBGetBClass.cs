// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         連絡先区分コードマスタ取得(ABRenrakusakiKBGetBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付             2007/07/26
// *
// * 作成者           比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{

    public class ABRenrakusakiKBGetBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス

        private DataSet m_csDataSchma;   // スキーマ保管用データセット

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABRenrakusakiKBGetBClass";

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData, 
        // *                              　ByVal cfConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABRenrakusakiKBGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // SQL文の作成
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiCDMstEntity.TABLE_NAME, ABRenrakusakiCDMstEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     連絡先区分コードマスタ抽出
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiCD() As DataSet
        // * 
        // * 機能　　    　 連絡先区分コードマスタより該当データを取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         連絡先区分コードマスタデータ(全件)（DataSet）
        // ************************************************************************************************
        public DataSet GetRenrakusakiCD()
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiCD";             // このメソッド名
            DataSet csRenrakusakiCDEntity;                                // 異動理由マスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABRenrakusakiCDMstEntity.TABLE_NAME, false);

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

            return csRenrakusakiCDEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先区分コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String) As DataSet
        // * 
        // * 機能　　    　 連絡先区分コードより該当データを取得する。
        // * 
        // * 引数           strRenrakusakiCD As String     :連絡先区分
        // * 
        // * 戻り値         取得した連絡先区分コードマスタの該当データ（DataSet）
        // ************************************************************************************************
        public DataSet GetRenrakusakiCD(string strRenrakusakiCD)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiCD";             // このメソッド名
            DataSet csRenrakusakiCDEntity;                                // 連絡先区分コードマスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB);
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB;
                cfUFParameterClass.Value = strRenrakusakiCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);


                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csRenrakusakiCDEntity = m_csDataSchma.Clone();
                csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csRenrakusakiCDEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先区分コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String, 
        // *                                                             ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 連絡先区分コードより該当データを取得する。
        // * 
        // * 引数           strRenrakusakiCD As String     :連絡先区分
        // *                blnSakujoFG As Boolean         :削除フラグ
        // * 
        // * 戻り値         取得した連絡先区分コードマスタの該当データ（DataSet）
        // ************************************************************************************************
        public DataSet GetRenrakusakiCD(string strRenrakusakiCD, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiCD";             // このメソッド名
            DataSet csRenrakusakiCDEntity;                                // 連絡先区分コードマスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB);
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABRenrakusakiCDMstEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABIdoRiyuEntity.KEY_RIYUCD;
                cfUFParameterClass.Value = strRenrakusakiCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);


                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csRenrakusakiCDEntity = m_csDataSchma.Clone();
                csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csRenrakusakiCDEntity;

        }
        #endregion

    }
}