// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ｅＬＴＡＸ税目区分マスタ(ABLTTaxKBBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付             2008/11/25
// *
// * 作成者           比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2009/07/16   000001     税目区分マスタ業務コード指定取得メソッドを追加（比嘉）
// * 2010/04/16   000002     VS2008対応（比嘉）
// ************************************************************************************************
using System;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{

    public class ABLTTaxKBBClass
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
        private const string THIS_CLASS_NAME = "ABLTTaxKBBClass";

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
        // *                 cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // *                 cfRdbClass As UFRdbClass               : ＲＤＢデータオブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABLTTaxKBBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // SQL文の作成
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTTaxKBEntity.TABLE_NAME, ABLTTaxKBEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     税目区分マスタ取得
        // * 
        // * 構文           Public Overloads Function GetLTTaxKB() As DataSet
        // * 
        // * 機能　　    　 税目区分マスタより全件データを取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         取得した税目区分マスタの該当データ（DataSet）
        // ************************************************************************************************
        public DataSet GetLTTaxKB()
        {
            const string THIS_METHOD_NAME = "GetLTTaxKB";
            DataSet csLTTaxKBEntity;                                      // 税目区分マスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000002
                                                                                  // Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
                                                                                  // * corresponds to VS2008 End 2010/04/16 000002
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABLTTaxKBEntity.TABLE_NAME);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABLTTaxKBEntity.TAXKB);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csLTTaxKBEntity = m_csDataSchma.Clone();
                csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csLTTaxKBEntity;

        }

        // *履歴番号 000001 2009/07/16 追加開始
        // ************************************************************************************************
        // * メソッド名     税目区分マスタ取得
        // * 
        // * 構文           Public Overloads Function GetLTTaxKB(ByVal strGyomuCD() As String) As DataSet
        // * 
        // * 機能　　    　 税目区分マスタより全件データを取得する。
        // * 
        // * 引数           strGyomuCD() As String        :業務コード配列
        // * 
        // * 戻り値         取得した税目区分マスタの該当データ（DataSet）
        // ************************************************************************************************
        public DataSet GetLTTaxKB(string[] strGyomuCD)
        {
            const string THIS_METHOD_NAME = "GetLTTaxKB";
            DataSet csLTTaxKBEntity;                                      // 税目区分マスタデータ
            var strSQL = new System.Text.StringBuilder();                         // SQL文文字列
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000002
                                                                                  // Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
                                                                                  // * corresponds to VS2008 End 2010/04/16 000002
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス
            int intI;
            var strWhere = new System.Text.StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABLTTaxKBEntity.TABLE_NAME);

                // WHERE句
                if (strGyomuCD.Length > 0)
                {
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABLTTaxKBEntity.GYOMUCD);
                    strSQL.Append(" IN(");

                    var loopTo = strGyomuCD.Length - 1;
                    for (intI = 0; intI <= loopTo; intI++)
                    {
                        strSQL.Append("'");
                        strSQL.Append(strGyomuCD[intI]);
                        strSQL.Append("',");
                    }
                    strSQL.RRemove(strSQL.RLength - 1, 1);
                    strSQL.Append(")");
                }

                else
                {
                }

                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABLTTaxKBEntity.TAXKB);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csLTTaxKBEntity = m_csDataSchma.Clone();
                csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csLTTaxKBEntity;

        }
        // *履歴番号 000001 2009/07/16 追加終了
        #endregion

    }
}