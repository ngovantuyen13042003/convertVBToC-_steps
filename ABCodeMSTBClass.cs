// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         コードマスタ取得(ABCodeMSTBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付             2023/10/31
// *
// * 作成者           下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/10/31             【AB-0880-1】個人制御情報詳細管理項目追加
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABCodeMSTBClass
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
        private const string THIS_CLASS_NAME = "ABCodeMSTBClass";

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
        public ABCodeMSTBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // SQL文の作成
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABCodeMSTEntity.TABLE_NAME, ABCodeMSTEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     コードマスタ抽出
        // * 
        // * 構文           Public Overloads Function GetCodeMst() As DataSet
        // * 
        // * 機能　　    　 コードマスタより該当データを取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         コードマスタデータ(全件)（DataSet）
        // ************************************************************************************************
        public DataSet GetCodeMst()
        {
            const string THIS_METHOD_NAME = "GetCodeMst";             // このメソッド名
            DataSet csDataSet;                                    // コードマスタ
            var strSQL = new System.Text.StringBuilder();                 // SQL文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABCodeMSTEntity.TABLE_NAME);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABCodeMSTEntity.SHUBETSU);
                strSQL.Append(",");
                strSQL.Append(ABCodeMSTEntity.CODE);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");

                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABCodeMSTEntity.TABLE_NAME, false);

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

            return csDataSet;

        }

        // ************************************************************************************************
        // * メソッド名     コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetCodeMst(ByVal strShubetsu As String) As DataSet
        // * 
        // * 機能　　    　 種別コードより該当データを取得する。
        // * 
        // * 引数          strShubetsu As String     :種別
        // * 
        // * 戻り値         取得したコードマスタ（DataSet）
        // ************************************************************************************************
        public DataSet GetCodeMst(string strShubetsu)
        {
            return GetCodeMst(strShubetsu, false);
        }

        // ************************************************************************************************
        // * メソッド名     コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetCodeMst(ByVal strShubetsu As String, 
        // *                                                     ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　 種別コードより該当データを取得する
        // * 
        // * 引数           strShubetsu As String     :種別コード
        // *                blnSakujoFG As Boolean    :削除フラグ
        // * 
        // * 戻り値         取得したコードマスタ（DataSet）
        // ************************************************************************************************
        public DataSet GetCodeMst(string strShubetsu, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetCodeMst";             // このメソッド名
            DataSet csDataset;                                    // コードマスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABCodeMSTEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABCodeMSTEntity.SHUBETSU);
                strSQL.Append(" = ");
                strSQL.Append(ABCodeMSTEntity.KEY_SHUBETSU);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABCodeMSTEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABCodeMSTEntity.HYOJIJUN);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABCodeMSTEntity.KEY_SHUBETSU;
                cfUFParameterClass.Value = strShubetsu;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);


                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csDataset = m_csDataSchma.Clone();
                csDataset = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataset, ABCodeMSTEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csDataset;

        }
        #endregion

    }
}
