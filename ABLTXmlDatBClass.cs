// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ＡＢｅＬＴＡＸ受信ＸＭＬマスタ(ABLTXmlDatBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付             2009/07/15
// *
// * 作成者           比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2010/04/16   000001     VS2008対応（比嘉）
// * 2011/08/30   000002     eLTAX利用届出連携の削除機能追加に伴う改修（比嘉）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{

    public class ABLTXmlDatBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス

        private DataSet m_csDataSchma;                        // スキーマ保管用データセット
        private string m_strInsertSQL;
        private string m_strUpDateSQL;
        private string m_strUpDateSQL_ConvertFG;
        private string m_strUpDateSQL_SakujoFG;
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateConvertFGUFParameterCollectionClass;  // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateSakujoFGUFParameterCollectionClass;  // UPDATE用パラメータコレクション
                                                                                          // *履歴番号 000002 2011/08/30 追加開始
        private string m_strDeleteSQL;
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;  // DELETE用パラメータコレクション
                                                                                  // *履歴番号 000002 2011/08/30 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABLTXmlDatBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード

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
        public ABLTXmlDatBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // SQL文の作成
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTXMLDatEntity.TABLE_NAME, ABLTXMLDatEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド

        #region eLTAX受信XMLデータ取得メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XML届出・申告データ取得
        // * 
        // * 構文         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        // * 
        // * 機能　　     eLTAX受信XMLマスタより該当データを取得する。
        // * 
        // * 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
        // * 
        // * 戻り値       取得したｅＬＴＡＸ受信ＸＭＬマスタの該当データ（DataSet）
        // *                 構造：csLtXMLDatEntity    
        // ************************************************************************************************
        public DataSet GetLTXmlDat(ABLTXmlDatParaXClass csABLTXmlDatParaX)
        {
            const string THIS_METHOD_NAME = "GetLTXmlDat";

            // * corresponds to VS2008 Start 2010/04/16 000001
            // Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000001
            DataSet csLtXMLDatEntity;                                 // 利用届出受信マスタ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL.Append("SELECT * ");
                strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME);

                // WHERE句
                strSQL.Append(" WHERE ");

                // 必須条件
                // * SHINKOKUSHINSEIKB = "R0" AND 
                strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ");
                strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB);
                strSQL.Append(" AND ");
                strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ");
                strSQL.Append("'1'");


                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB;
                cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 税目区分
                if (csABLTXmlDatParaX.p_strTaxKB != ABEnumDefine.ZeimokuCDType.Empty)
                {
                    strSQL.Append(" AND ");

                    // 税目区分が設定されている場合、抽出条件にする
                    strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ");
                    strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)csABLTXmlDatParaX.p_strTaxKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }

                // コンバートフラグ
                strSQL.Append(" AND ");
                if (csABLTXmlDatParaX.p_blnConvertFG == true)
                {
                    // コンバートフラグがTrueの場合、"1"を取得する
                    strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ");
                    strSQL.Append("'1'");
                }

                else
                {
                    // コンバートフラグがFalseの場合、"1"以外を取得する
                    strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ");
                    strSQL.Append("'1'");

                }

                // 最大取得件数セット
                if (csABLTXmlDatParaX.p_intMaxCount != 0)
                {
                    m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount;
                }
                else
                {
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // 届出・申告データ取得
                csLtXMLDatEntity = m_csDataSchma.Clone();
                csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, false);


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

            return csLtXMLDatEntity;

        }
        #endregion

        #region eLTAX受信XMLデータ取得メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XML届出・申告データ取得
        // * 
        // * 構文         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass, _
        // *                                          ByRef intAllCount As Integer) As DataSet
        // * 
        // * 機能　　     eLTAX受信XMLマスタより該当データを取得する。
        // * 
        // * 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
        // *              intAllCount As Integer                      : 全データ件数
        // * 
        // * 戻り値       取得したｅＬＴＡＸ受信ＸＭＬマスタの該当データ（DataSet）
        // *                 構造：csLtXMLDatEntity    
        // ************************************************************************************************
        public DataSet GetLTXmlDat(ABLTXmlDatParaXClass csABLTXmlDatParaX, ref int intAllCount)
        {
            const string THIS_METHOD_NAME = "GetLTXmlDat";
            const string COL_COUNT = "COUNT";
            // * corresponds to VS2008 Start 2010/04/16 000001
            // Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000001
            DataSet csLtXMLDatEntity;                                 // 利用届出受信マスタ
            DataSet csLtXmlDat_All;                                   // 利用届出受信全件データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            var strSQL_ALL = new StringBuilder();                             // SQL文全件取得文字列
            var strWhere = new StringBuilder();                               // WHERE文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL.Append("SELECT * ");
                strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME);

                strSQL_ALL.Append("SELECT COUNT(*) AS ").Append(COL_COUNT);
                strSQL_ALL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME);

                // WHERE句
                strWhere.Append(" WHERE ");

                // 必須条件
                // * SHINKOKUSHINSEIKB = "R0" AND 
                strWhere.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB);
                strWhere.Append(" AND ");

                // *履歴番号 000002 2011/08/30 修正開始
                if (csABLTXmlDatParaX.p_blnSakuJoFG == false)
                {
                    // eLTAX受信XMLパラメータクラス:削除フラグ="False"の場合、削除データ以外を抽出
                    strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ");
                    strWhere.Append("'1'");
                }
                else
                {
                    // eLTAX受信XMLパラメータクラス:削除フラグ="True"の場合、削除データを抽出
                    strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = ");
                    strWhere.Append("'1'");
                }
                // strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
                // strWhere.Append("'1'")
                // *履歴番号 000002 2011/08/30 修正終了


                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB;
                cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 税目区分
                if (csABLTXmlDatParaX.p_strTaxKB != ABEnumDefine.ZeimokuCDType.Empty)
                {
                    strWhere.Append(" AND ");

                    // 税目区分が設定されている場合、抽出条件にする
                    strWhere.Append(ABLTXMLDatEntity.TAXKB).Append(" = ");
                    strWhere.Append(ABLTXMLDatEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)csABLTXmlDatParaX.p_strTaxKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }

                // コンバートフラグ
                strWhere.Append(" AND ");
                if (csABLTXmlDatParaX.p_blnConvertFG == true)
                {
                    // コンバートフラグがTrueの場合、"1"を取得する
                    strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ");
                    strWhere.Append("'1'");
                }

                else
                {
                    // コンバートフラグがFalseの場合、"1"以外を取得する
                    strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ");
                    strWhere.Append("'1'");

                }

                // 最大取得件数セット
                if (csABLTXmlDatParaX.p_intMaxCount != 0)
                {
                    m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount;
                }
                else
                {
                }

                // SQL文結合 
                strSQL.Append(strWhere.ToString());
                strSQL_ALL.Append(strWhere.ToString());

                // 全件取得処理
                csLtXmlDat_All = m_cfRdbClass.GetDataSet(strSQL_ALL.ToString(), cfUFParameterCollectionClass);

                intAllCount = (int)csLtXmlDat_All.Tables(0).Rows(0)(COL_COUNT);


                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // 届出・申告データ取得
                csLtXMLDatEntity = m_csDataSchma.Clone();
                csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, false);


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

            return csLtXMLDatEntity;

        }
        #endregion

        #region eLTAX受信XML届出・申告データ件数取得メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XML届出・申告データ件数取得
        // * 
        // * 構文         Public Function GetLTXmlCount(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        // * 
        // * 機能　　     eLTAX受信XMLマスタより該当データの件数を取得する。
        // * 
        // * 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
        // * 
        // * 戻り値       取得したeLTAX受信データ件数データ（DataSet）
        // *                 構造：csLtXMLDatCountDS    
        // ************************************************************************************************
        public DataSet GetLTXmlCount(ABLTXmlDatParaXClass csABLTXmlDatParaX)
        {
            const string THIS_METHOD_NAME = "GetLTXmlCount";

            // * corresponds to VS2008 Start 2010/04/16 000001
            // Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000001
            DataSet csLtXMLDatCountDS;                                // ABeLTAX受信DAT件数データセット
            DataSet csDataSet;
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            DataRow csDataRow;
            DataRow csNewRow;
            int intCount = 0;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL.Append("SELECT ");
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ");
                strSQL.Append(ABLTXMLDatEntity.PROCID).Append(", ");
                strSQL.Append("COUNT(*) AS COUNT");
                strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME);

                // WHERE句
                strSQL.Append(" WHERE ");

                // 必須条件
                // * SHINKOKUSHINSEIKB = "T0" AND 
                strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ");
                strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB);
                strSQL.Append(" AND ");
                strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ");
                strSQL.Append("'1'");

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB;
                cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 税目区分
                if (csABLTXmlDatParaX.p_strTaxKB != ABEnumDefine.ZeimokuCDType.Empty)
                {
                    strSQL.Append(" AND ");

                    // 税目区分が設定されている場合、抽出条件にする
                    strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ");
                    strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)csABLTXmlDatParaX.p_strTaxKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }

                // コンバートフラグ
                strSQL.Append(" AND ");
                if (csABLTXmlDatParaX.p_blnConvertFG == true)
                {
                    // コンバートフラグがTrueの場合、"1"を取得する
                    strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ");
                    strSQL.Append("'1'");
                }

                else
                {
                    // コンバートフラグがFalseの場合、"1"以外を取得する
                    strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ");
                    strSQL.Append("'1'");

                }

                // GROUP BY句
                strSQL.Append(" GROUP BY ");
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ");
                strSQL.Append(ABLTXMLDatEntity.PROCID);

                // ORDER BY句
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ");
                strSQL.Append(ABLTXMLDatEntity.PROCID);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // データ取得
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, false);


                // eLTAX受信DAT件数データテーブル作成
                csLtXMLDatCountDS = CreateDataSet();


                // ｅＬＴＡＸ受信ＸＭＬ届出・申告データ件数データセットにセット
                foreach (DataRow currentCsDataRow in csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows)
                {
                    csDataRow = currentCsDataRow;

                    csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow;

                    csNewRow(ABLTXmlDatCountData.TAXKB) = csDataRow(ABLTXMLDatEntity.TAXKB);
                    csNewRow(ABLTXmlDatCountData.PROCID) = csDataRow(ABLTXMLDatEntity.PROCID);
                    csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = GetProcRyakumei((string)csDataRow(ABLTXMLDatEntity.PROCID));
                    csNewRow(ABLTXmlDatCountData.COUNT) = csDataRow("COUNT");

                    csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow);

                }
                // ----------------------------------------------------------------------------
                // 合計行追加
                csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow;

                // 税目区分
                if (csABLTXmlDatParaX.p_strTaxKB != ABEnumDefine.ZeimokuCDType.Empty)
                {
                    // 空白以外
                    csNewRow(ABLTXmlDatCountData.TAXKB) = (string)csABLTXmlDatParaX.p_strTaxKB;
                }
                else
                {
                    // 空白の場合
                    csNewRow(ABLTXmlDatCountData.TAXKB) = string.Empty;
                }

                // 手続ID
                csNewRow(ABLTXmlDatCountData.PROCID) = string.Empty;

                // 手続名
                csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = string.Empty;

                // 件数
                foreach (DataRow currentCsDataRow1 in csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows)
                {
                    csDataRow = currentCsDataRow1;
                    intCount += (int)csDataRow("COUNT");
                }
                csNewRow(ABLTXmlDatCountData.COUNT) = intCount.ToString();

                csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow);
                // ----------------------------------------------------------------------------


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

            return csLtXMLDatCountDS;

        }
        #endregion

        #region eLTAX受信XMLデータ追加メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ追加メソッド
        // * 
        // * 構文         Public Function InsertLTXMLDat(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタに新規データを追加する
        // * 
        // * 引数         csDataRow As DataRow   : 追加データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       追加件数(Integer)
        // ************************************************************************************************
        public int InsertLTXMLDat(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertLTXMLDat";                                 // パラメータクラス
                                                                                              // * corresponds to VS2008 Start 2010/04/16 000001
                                                                                              // Dim csDataColumn As DataColumn                                  ' データカラム
                                                                                              // * corresponds to VS2008 End 2010/04/16 000001
            int intInsCnt;                                        // 追加件数
            string strUpdateDateTime;                                 // システム日付

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");        // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;              // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.SAKUJOFG) = "0";                                          // 削除フラグ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = decimal.Zero;                            // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.SAKUSEINICHIJI) = strUpdateDateTime;                      // 作成日時
                csDataRow(ABLTXMLDatEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;               // 作成ユーザー
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = strUpdateDateTime;                       // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                // 更新ユーザー


                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName == ABLTXMLDatEntity.KEY_XMLDAT)
                    {
                        // 項目:XMLDatの場合は、byte型のままセットする
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength));
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength)).ToString();
                    }
                }

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

        #region eLTAX受信XMLデータ更新メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ更新メソッド
        // * 
        // * 構文         Public Function UpdateLTXMLDat(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateLTXMLDat(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateLTXmlDat";                         // パラメータクラス
                                                                                      // * corresponds to VS2008 Start 2010/04/16 000001
                                                                                      // Dim csDataColumn As DataColumn                          ' データカラム
                                                                                      // * corresponds to VS2008 End 2010/04/16 000001
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL is null | string.IsNullOrEmpty(m_strUpDateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) + 1m;         // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) == ABLTXMLDatEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else if (cfParam.ParameterName == ABLTXMLDatEntity.KEY_XMLDAT)
                    {
                        // 項目:XMLDatの場合は、byte型のままセットする
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current);
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass);

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

        #region eLTAX受信XMLデータ:コンバートフラグ更新メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:コンバートフラグ更新メソッド
        // * 
        // * 構文         Public Function UpdateLTXMLDat_ConvertFG(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateLTXMLDat_ConvertFG(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateLTXMLDat_ConvertFG";                         // パラメータクラス
                                                                                                // * corresponds to VS2008 Start 2010/04/16 000001
                                                                                                // Dim csDataColumn As DataColumn                          ' データカラム
                                                                                                // * corresponds to VS2008 End 2010/04/16 000001
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL_ConvertFG is null | string.IsNullOrEmpty(m_strUpDateSQL_ConvertFG) | m_cfUpdateConvertFGUFParameterCollectionClass is null)
                {
                    CreateSQL_UpDateConvertFG();
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) + 1m;         // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateConvertFGUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) == ABLTXMLDatEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current);
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_ConvertFG, m_cfUpdateConvertFGUFParameterCollectionClass);

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

        #region eLTAX受信XMLデータ:削除フラグ更新メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:削除フラグ更新メソッド
        // * 
        // * 構文         Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateLTXMLDat_SakujoFG(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateLTXMLDat_SakujoFG";                         // パラメータクラス
                                                                                               // * corresponds to VS2008 Start 2010/04/16 000001
                                                                                               // Dim csDataColumn As DataColumn                          ' データカラム
                                                                                               // * corresponds to VS2008 End 2010/04/16 000001
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL_SakujoFG is null | string.IsNullOrEmpty(m_strUpDateSQL_SakujoFG) | m_cfUpdateSakujoFGUFParameterCollectionClass is null)
                {
                    CreateSQL_UpDateSakujoFG();
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) + 1m;         // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateSakujoFGUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) == ABLTXMLDatEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString;
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass);

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

        #region eLTAX受信XMLデータ:削除フラグ更新メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:削除フラグ更新メソッド
        // * 
        // * 構文         Public Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow, _
        // *                                                      ByVal blnKoshinCounter As Boolean) As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow    : 利用届データ(ABeLTAXRiyoTdk)
        // *              blnKoshinCounter        : 更新カウンタ(True:条件に含む、False:含まない)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateLTXMLDat_SakujoFG(DataRow csDataRow, bool blnKoshinCounter)
        {
            const string THIS_METHOD_NAME = "UpdateLTXMLDat_SakujoFG";                         // パラメータクラス
                                                                                               // * corresponds to VS2008 Start 2010/04/16 000001
                                                                                               // Dim csDataColumn As DataColumn                          ' データカラム
                                                                                               // * corresponds to VS2008 End 2010/04/16 000001
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL_SakujoFG is null | string.IsNullOrEmpty(m_strUpDateSQL_SakujoFG) | m_cfUpdateSakujoFGUFParameterCollectionClass is null)
                {
                    CreateSQL_UpDateSakujoFG(blnKoshinCounter);
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) + 1m;         // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateSakujoFGUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) == ABLTXMLDatEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString;
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass);

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

        // *履歴番号 000002 2011/08/30 追加開始
        #region eLTAX受信XMLデータ:削除(物理)メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:削除(物理)メソッド
        // * 
        // * 構文         Public Overloads Function DeleteLTXMLDat() As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタの該当データを物理削除する
        // * 
        // * 引数         なし
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int DeleteLTXMLDat(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteLTXMLDat";                         // パラメータクラス
            int intUpdCnt;                                // 更新件数
            bool blnKoshinCounter = false;                 // 更新カウンター

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL_SakujoFG is null || string.IsNullOrEmpty(m_strUpDateSQL_SakujoFG) || m_cfUpdateSakujoFGUFParameterCollectionClass is null)
                {
                    CreateSQL_UpDateSakujoFG(blnKoshinCounter);
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                  // 端末ＩＤ
                csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) + 1m;         // 更新カウンタ
                csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                    // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    if (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) == ABLTXMLDatEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString;
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass);

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

        #region eLTAX受信XMLデータ:削除データ一括削除(物理)メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:削除データ一括削除(物理)メソッド
        // * 
        // * 構文         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータの削除フラグ="1"のデータを一括削除する
        // * 
        // * 引数         なし
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int DeleteLTXMLDat_Sakujo()
        {
            const string THIS_METHOD_NAME = "DeleteLTXMLDat_Sakujo";
            var csSQL = new StringBuilder();
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文作成
                csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME);
                csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> '1' ");
                csSQL.Append("AND ").Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = '1'");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString()) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString());

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

        #region eLTAX受信XMLデータ:コンバート済み一括削除(物理)メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX受信XMLデータ:コンバート済み一括削除(物理)メソッド
        // * 
        // * 構文         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
        // * 
        // * 機能　　     eLTAX受信XMLマスタのデータのコンバートフラグ="1"のデータを一括削除する
        // * 
        // * 引数         なし
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int DeleteLTXMLDat_Convert()
        {
            const string THIS_METHOD_NAME = "DeleteLTXMLDat_Convert";
            var csSQL = new StringBuilder();
            int intUpdCnt;                    // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文作成
                csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME);
                csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" = '1'");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString()) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString());

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
        // *履歴番号 000002 2011/08/30 追加終了

        #region SQL文の作成
        // ************************************************************************************************
        // * メソッド名   SQL文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数         csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            string strInsertColumn;                               // 追加SQL文項目文字列
            string strInsertParam;                                // 追加SQL文パラメータ文字列
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABLTXMLDatEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // UPDATE SQL文の作成
                m_strUpDateSQL = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET ";

                // UPDATE Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER);

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // UPDATE SQL文の作成
                    m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");
                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // UPDATE SQL文のトリミング
                m_strUpDateSQL = m_strUpDateSQL.Trim();
                m_strUpDateSQL = m_strUpDateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpDateSQL += strWhere.ToString();

                // UPDATE コレクションにキー情報を追加
                // 受信日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // ＸＭＬ連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 申告受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

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

        #region SQL文の作成(コンバートフラグ用)
        // ************************************************************************************************
        // * メソッド名   SQL文の作成(コンバートフラグ用)
        // * 
        // * 構文         Private Sub CreateSQL_UpDateConvertFG()
        // * 
        // * 機能　　     UPDATEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数         csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private void CreateSQL_UpDateConvertFG()
        {
            const string THIS_METHOD_NAME = "CreateSQL_UpDateConvertFG";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            var strWhere = new StringBuilder();                           // 更新SQL文Where文文字列
            var strSet = new StringBuilder();                             // 更新SQL文Set文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpDateSQL_ConvertFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET ";

                // UPDATE Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateConvertFGUFParameterCollectionClass = new UFParameterCollectionClass();

                // コンバートフラグ用UPDATE SQL文の作成
                m_strUpDateSQL_ConvertFG += ABLTXMLDatEntity.CONVERTFG + " = " + ABLTXMLDatEntity.KEY_CONVERTFG + ",";

                // 共通Set文
                strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER);

                // UPDATE SQL文にWHERE句の追加
                m_strUpDateSQL_ConvertFG += strSet.ToString() + strWhere.ToString();

                // *-------------------------------------------------------------------------*
                // コンバートフラグ用UPDATE コレクションにパラメータを追加
                // コンバートフラグ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_CONVERTFG;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // *-------------------------------------------------------------------------*
                // UPDATE コレクションにキー情報を追加
                // 端末ＩＤ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新ユーザ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受信日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // ＸＭＬ連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 申告受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER;
                m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // *-------------------------------------------------------------------------*

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

        #region SQL文の作成(削除フラグ用)
        // ************************************************************************************************
        // * メソッド名   SQL文の作成(削除フラグ用)
        // * 
        // * 構文         Private Sub CreateSQL_UpDateSakujoFG()
        // * 
        // * 機能　　     UPDATEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数         csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private void CreateSQL_UpDateSakujoFG()
        {

            CreateSQL_UpDateSakujoFG(true);

        }
        private void CreateSQL_UpDateSakujoFG(bool blnKoshinCounter)
        {
            const string THIS_METHOD_NAME = "CreateSQL_UpDateSakujoFG";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            var strWhere = new StringBuilder();                           // 更新SQL文Where文文字列
            var strSet = new StringBuilder();                             // 更新SQL文Set文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpDateSQL_SakujoFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET ";

                // *履歴番号 000002 2011/08/30 追加開始
                m_strDeleteSQL = "DELETE " + ABLTXMLDatEntity.TABLE_NAME;
                // *履歴番号 000002 2011/08/30 追加終了

                // UPDATE Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN);
                strWhere.Append(" AND ");
                strWhere.Append(ABLTXMLDatEntity.RCPTYMD);
                strWhere.Append(" = ");
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD);

                if (blnKoshinCounter == true)
                {
                    strWhere.Append(" AND ");
                    strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER);
                    strWhere.Append(" = ");
                    strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER);
                }
                else
                {
                }

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateSakujoFGUFParameterCollectionClass = new UFParameterCollectionClass();

                // 削除フラグ用UPDATE SQL文の作成
                m_strUpDateSQL_SakujoFG += ABLTXMLDatEntity.SAKUJOFG + " = " + ABLTXMLDatEntity.KEY_SAKUJOFG + ",";

                // 共通Set文
                strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",");
                strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER);

                // UPDATE SQL文にWHERE句の追加
                m_strUpDateSQL_SakujoFG += strSet.ToString() + strWhere.ToString();

                // *-------------------------------------------------------------------------*
                // 削除フラグ用UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SAKUJOFG;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // *-------------------------------------------------------------------------*
                // UPDATE コレクションにキー情報を追加
                // 端末ＩＤ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新ユーザ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受信日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // ＸＭＬ連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                // 申告受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD;
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                if (blnKoshinCounter == true)
                {
                    // 更新カウンタ
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER;
                    m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }
                // *-------------------------------------------------------------------------*

                // *履歴番号 000002 2011/08/30 追加開始
                // DELETE パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE SQL文にWHERE句の追加
                m_strDeleteSQL += strWhere.ToString();

                // *-------------------------------------------------------------------------*
                // DELETE コレクションにキー情報を追加
                // 受信日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // ＸＭＬ連番
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 申告受付番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                if (blnKoshinCounter == true)
                {
                    // 更新カウンタ
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER;
                    m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }
                // *-------------------------------------------------------------------------*
                // *履歴番号 000002 2011/08/30 追加終了

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

        #region データセット作成
        // ************************************************************************************************
        // * メソッド名   ｅＬＴＡＸ受信ＤＡＴ件数データセット作成
        // * 
        // * 構文         Private Function CreateDataSet() As DataSet
        // * 
        // * 機能　　     ｅＬＴＡＸ受信ＤＡＴ件数データセットを作成する
        // * 
        // * 引数         なし
        // * 
        // * 戻り値       作成したｅＬＴＡＸ受信ＤＡＴデータセット(DataSet)
        // ************************************************************************************************
        private DataSet CreateDataSet()
        {
            const string THIS_METHOD_NAME = "CreateDataSet";
            DataSet csDataSet;                        // データセット
            DataTable csDataTable;                    // テーブル
            DataColumn csDataColumn;                  // カラム

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // DataSetのインスタンス作成
                csDataSet = new DataSet();

                // データテーブル作成
                csDataTable = csDataSet.Tables.Add(ABLTXmlDatCountData.TABLE_NAME);

                // カラム定義の作成
                // 税目区分
                csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.TAXKB, Type.GetType("System.String"));
                // 手続ID
                csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCID, Type.GetType("System.String"));
                // 手続名(略)
                csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCRYAKUMEI, Type.GetType("System.String"));
                // 件数
                csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.COUNT, Type.GetType("System.String"));

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
        #endregion

        #region 手続名称(略)取得処理
        // ************************************************************************************************
        // * メソッド名   手続名称(略)取得処理
        // * 
        // * 構文         Private Function GetProcRyakumei(ByVal strProcId As String) As String
        // * 
        // * 機能　　     手続名称(略)を取得する
        // * 
        // * 引数         ByVal strProcId As String   ：手続ＩＤ
        // * 
        // * 戻り値       
        // ************************************************************************************************
        private string GetProcRyakumei(string strProcId)
        {
            const string THIS_METHOD_NAME = "GetProcRyakumei";
            string strProcRyakumei = string.Empty;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strProcId ?? "")
                {
                    case var @case when @case == ABConstClass.ELTAX_PROCID_SHINKI:
                        {
                            // 手続ＩＤ:T0999910，手続略称:届出新規
                            strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHINKI;
                            break;
                        }

                    case var case1 when case1 == ABConstClass.ELTAX_PROCID_HENKO_RIYOSHAJOHO:
                        {
                            // 手続ＩＤ:T0999920，手続略称:変更(利)
                            strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_RIYOSHAJOHO;
                            break;
                        }

                    case var case2 when case2 == ABConstClass.ELTAX_PROCID_HENKO_SHINKOKUSAKITAXKB:
                        {
                            // 手続ＩＤ:T0999910，手続略称:変更(申)
                            strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_SHINKOKUSAKITAXKB;
                            break;
                        }

                    case var case3 when case3 == ABConstClass.ELTAX_PROCID_HAISHI:
                        {
                            // 手続ＩＤ:T0999910，手続略称:廃止
                            strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HAISHI;
                            break;
                        }

                    case var case4 when case4 == ABConstClass.ELTAX_PROCID_SHOMEISHOSASIKAE:
                        {
                            // 手続ＩＤ:T0999910，手続略称:証明差替
                            strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHOMEISHOSASIKAE;
                            break;
                        }

                    default:
                        {
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
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return strProcRyakumei;

        }
        #endregion

        #endregion

    }
}