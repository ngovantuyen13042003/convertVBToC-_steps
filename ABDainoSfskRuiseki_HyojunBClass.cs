// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ＡＢ代納送付先異動累積_標準マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け           2023/10/25
// *
// * 作成者           見城　啓四郎
// *
// * 著作権          （株）電算
// ************************************************************************************************
// *  修正履歴　 履歴番号　　修正内容
// * 2024/06/10  000001     【AB-9902-1】不具合対応
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 代納送付先異動累積_標準マスタ取得、更新時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABDainoSfskRuiseki_HyojunBClass
    {

        #region メンバ変数

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABDainoSfskRuiseki_HyojunBClass";     // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード
        private const string ZENGOKB_ZEN = "1";                                       // 前後区分　前
        private const string ZENGOKB_GO = "2";                                        // 前後区分　後
        private const string SAKUJOFG_SAKUJO = "1";                                   // 削除フラグ　削除
        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private UFErrorClass m_cfErrorClass;                                          // エラー処理クラス
        private UFDateClass m_cfDateClass;                                            // 日付クラス
        private DataSet m_csDataSchma;                                                // スキーマ保管用データセット
        private DataSet m_csDataSchmaHyojun;                                          // スキーマ保管用データセット
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private USSCityInfoClass m_cUSSCityInfoClass;                                 // 市町村情報管理クラス

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // *                               ByVal cfConfigDataClass As UFConfigDataClass, 
        // *                               ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABDainoSfskRuiseki_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;

            // AB代納送付先累積マスタのスキーマ取得
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, false);

            // AB代納送付先累積_標準マスタのスキーマ取得
            m_csDataSchmaHyojun = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド

        #region 代納送付先異動累積マスタ追加
        // ************************************************************************************************
        // * メソッド名     代納送付先異動累積_標準マスタ追加
        // * 
        // * 構文           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能           代納送付先異動累積_標準マスタにデータを追加
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertDainoSfskB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertDainoSfskB";
            int intInsCnt;                            // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL) || m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);  // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;  // 端末ＩＤ
                csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = decimal.Zero;                // 更新カウンタ
                csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = strUpdateDateTime;          // 作成日時
                csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;   // 作成ユーザー
                csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = strUpdateDateTime;           // 更新日時
                csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;    // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

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

            return intInsCnt;

        }
        #endregion

        #region SQL文作成
        // ************************************************************************************************
        // * メソッド名     SQL文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           INSERTのSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder strInsertColumn;                 // INSERT用カラム定義
            StringBuilder strInsertParam;                  // INSERT用パラメータ定義


            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME + " ";
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

                    strInsertParam.Append(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER);
                    strInsertParam.Append(csDataColumn.ColumnName);
                    strInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);


                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + strInsertColumn.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")" + " VALUES (" + strInsertParam.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")";

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

        }
        #endregion

        #region 代納送付先累積_標準データ作成
        // ************************************************************************************************
        // * メソッド名     代納送付先累積_標準データ作成
        // * 
        // * 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strShoriKB As String, _
        // *                                                    ByVal strShoriNichiji As String) As Integer
        // * 
        // * 機能           代納送付先累積データを作成する
        // * 
        // * 引数           csDataRow As DataRow      : 代納送付先データ
        // *                strShoriKB As String      : 処理区分
        // *                strShoriNichiji As String : 処理日時
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int CreateDainoSfskData(DataRow csDataRow, string strShoriKB, string strShoriNichiji)
        {
            int intInsCnt;
            ABSfsk_HyojunBClass cSfskHyojunB;               // 送付先ＤＡクラス
            DataSet csSfskHyojun;                           // 送付先ＤＡクラス

            const string THIS_METHOD_NAME = "CreateDainoSfskData";

            try
            {

                // 送付先_標準ＤＡクラスのインスタンス化
                cSfskHyojunB = new ABSfsk_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 送付先_標準の取得
                csSfskHyojun = cSfskHyojunB.GetSfskBHoshu(csDataRow(ABSfskEntity.JUMINCD).ToString(), csDataRow(ABSfskEntity.GYOMUCD).ToString(), csDataRow(ABSfskEntity.GYOMUNAISHU_CD).ToString(), csDataRow(ABSfskEntity.TOROKURENBAN).ToString());

                intInsCnt = CreateDainoSfskData(csDataRow, strShoriKB, csSfskHyojun.Tables(ABSfskHyojunEntity.TABLE_NAME).Rows(0), strShoriNichiji);
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

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積_標準データ作成
        // * 
        // * 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strShoriKB As String, _
        // *                                                    ByVal csABSfskHyojunDataRow As DataRow, _
        // *                                                    ByVal strShoriNichiji As String) As Integer
        // * 
        // * 機能　　    　 代納送付先累積_標準データを作成する
        // * 
        // * 引数           csDataRow As DataRow              : 代納送付先データ
        // *                strShoriKB As String              : 処理区分
        // *                csABSfskHyojunDataRow As DataRow  : AB送付先_標準データ（DataRow形式）
        // *                strShoriNichiji As String         : 処理日時
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int CreateDainoSfskData(DataRow csDataRow, string strShoriKB, DataRow csABSfskHyojunDataRow, string strShoriNichiji)
        {
            const string THIS_METHOD_NAME = "CreateDainoSfskData";
            DataSet csDataSet;
            DataSet csDataSetHyojun;
            DataRow csRuisekiDR;
            DataColumn csDataColumn;
            // Dim strSystemDate As String                 ' システム日付
            int intInsCnt;
            DataRow csOriginalDR;
            DataRow csOriginalHyojunDR;
            DataRow csDainoSfskRuisekiHyojunDR;
            int intUpdataCount_zen;
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            var cuCityInfo = new USSCityInfoClass();            // 市町村情報管理クラス

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // strSystemDate = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

                // スキーマを取得
                csDataSet = m_csDataSchma.Clone();
                csDataSetHyojun = m_csDataSchmaHyojun.Clone();

                // 更新用データのDataRowを作成
                csDainoSfskRuisekiHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow;

                // ***
                // * 代納送付先累積_標準(前)編集処理
                // *

                if (strShoriKB != ABConstClass.SFSK_ADD)
                {

                    // 代納送付先累積データを作成
                    csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow;
                    // 代納送付先累積_標準データを作成
                    csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow;

                    // 処理区分が追加以外の場合
                    if (csDataRow.HasVersion(DataRowVersion.Original))
                    {

                        // 修正前情報が残っている場合、代納送付先累積データを作成
                        csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow;

                        foreach (DataColumn currentCsDataColumn in csDataRow.Table.Columns)
                        {
                            csDataColumn = currentCsDataColumn;
                            if (!(csDataColumn.ColumnName == ABDainoEntity.RESERVE) && !(csDataColumn.ColumnName == ABSfskDataEntity.SFSKDATAKB))
                            {
                                csOriginalDR[csDataColumn.ColumnName] = csDataRow[csDataColumn.ColumnName, DataRowVersion.Original];
                            }
                        }

                        // 代納送付先累積_標準データを作成
                        csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow;

                        foreach (DataColumn currentCsDataColumn1 in csABSfskHyojunDataRow.Table.Columns)
                        {
                            csDataColumn = currentCsDataColumn1;
                            if (!(csDataColumn.ColumnName == ABSfskHyojunEntity.SFSKBANCHICD1) && !(csDataColumn.ColumnName == ABSfskHyojunEntity.SFSKBANCHICD2) && !(csDataColumn.ColumnName == ABSfskHyojunEntity.SFSKBANCHICD3) && !(csDataColumn.ColumnName == ABSfskHyojunEntity.SFSKKATAGAKICD))
                            {

                                csOriginalHyojunDR[csDataColumn.ColumnName] = csABSfskHyojunDataRow[csDataColumn.ColumnName, DataRowVersion.Original];
                            }
                        }

                        // (前)データのセット
                        csOriginalHyojunDR = SetDainoSfskRuisekiHyojunData(csOriginalDR, csOriginalHyojunDR, csDainoSfskRuisekiHyojunDR);

                        // 共通項目のセット
                        csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji;                 // 処理日時
                        csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB;                           // 処理区分
                        csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_ZEN;                          // 前後区分

                        // 削除フラグの設定
                        csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG, DataRowVersion.Original);

                        // データセットに修正前情報を追加
                        csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csOriginalHyojunDR);

                        // 代納送付先累積(前)マスタ追加処理
                        intUpdataCount_zen = this.InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0));

                        // 更新件数が１件以外の場合、エラーを発生させる
                        if (!(intUpdataCount_zen == 1))
                        {
                            m_cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                            // エラー定義を取得（既に同一データが存在します。：代納送付先累積）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "代納送付先累積_標準", objErrorStruct.m_strErrorCode);
                        }

                        // データセットのクリア
                        csDataSetHyojun.Clear();
                    }
                    else
                    {

                    }
                }
                else
                {

                }

                // ***
                // * 代納送付先累積_標準(後)編集処理　追加の場合もこちら
                // *
                // 代納送付先累積_標準データを作成
                csRuisekiDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow;

                // 共通項目のセット
                csRuisekiDR = SetDainoSfskRuisekiHyojunData(csDataRow, csABSfskHyojunDataRow, csDainoSfskRuisekiHyojunDR);

                // データセット　　
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji;            // 処理日時
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB;                      // 処理区分
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_GO;                      // 前後区分
                                                                                                       // 削除フラグ
                if (strShoriKB == ABConstClass.SFSK_DELETE)
                {
                    // 削除の場合は"1"をセット
                    csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_SAKUJO;
                }
                else
                {
                    // それ以外の場合は送付先の値をそのままセット
                    csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG);
                }

                csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csRuisekiDR);

                // ***
                // * 代納送付先累積_標準(後)マスタ追加処理
                // *
                intInsCnt = InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0));

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

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積_標準データ編集処理
        // * 
        // * 構文           Private Function SetDainoSfskRuisekiHyojunData(ByVal csSfskDataRow As DataRow,
        // *                                                               ByVal csSfskHyojunDataRow As DataRow,
        // *                                                               ByVal csReturnDataRow As DataRow) As DataRow
        // * 
        // * 機能　　    　 代納送付先累積_標準データを編集する
        // * 
        // * 引数           csSfskDataRow As DataRow            : 送付先データ
        // *                csSfskHyojunDataRow As DataRow      : 送付先_標準データ
        // *                csReturnDataRow                     : 戻り値
        // * 
        // * 戻り値         DataRow : 編集したデータ
        // ************************************************************************************************
        private DataRow SetDainoSfskRuisekiHyojunData(DataRow csSfskDataRow, DataRow csSfskHyojunDataRow, DataRow csReturnDataRow)
        {
            const string THIS_METHOD_NAME = "SetDainoSfskRuisekiHyojunData";

            // 市町村情報管理クラスの設定
            m_cUSSCityInfoClass = new USSCityInfoClass();
            m_cUSSCityInfoClass.GetCityInfo(m_cfControlData);

            try
            {
                // 共通項目　※処理日時、処理区分、前後区分、削除フラグは呼出し元でセットする
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.JUMINCD) = csSfskDataRow(ABSfskEntity.JUMINCD);                                           // 住民コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0);                                   // 市町村コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0);                                // 旧市町村コード

                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUCD) = csSfskDataRow(ABSfskEntity.GYOMUCD);                                           // 業務コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD) = csSfskDataRow(ABSfskEntity.GYOMUNAISHU_CD);                             // 業務内種別コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN) = csSfskDataRow(ABSfskEntity.TOROKURENBAN);                                 // 登録連番
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.STYMD) = csSfskDataRow(ABSfskEntity.STYMD);                                               // 開始年月日
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.EDYMD) = csSfskDataRow(ABSfskEntity.EDYMD);                                               // 終了年月日
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RRKNO) = csSfskDataRow(ABSfskEntity.RRKNO);                                               // 履歴番号

                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANAKATAGAKI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANAKATAGAKI);             // 送付先方書フリガナ
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTSUSHO);                         // 送付先氏名_通称
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANATSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANATSUSHO);                 // 送付先氏名_通称_フリガナ
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIMEIYUSENKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIMEIYUSENKB);           // 送付先氏名_優先区分
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKEIJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKEIJISHIMEI);                 // 送付先氏名_外国人英字
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANJISHIMEI);               // 送付先氏名_外国人漢字
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAMEI);           // 送付先申請者名
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAKANKEICD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD); // 送付先申請者関係コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSONCD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSONCD);           // 送付先_市区町村コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZACD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZACD);                 // 送付先_町字コード
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTODOFUKEN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTODOFUKEN);                   // 送付先_都道府県
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSON) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSON);               // 送付先_市区郡町村名
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZA) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZA);                     // 送付先_町字
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKRENRAKUSAKIKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKRENRAKUSAKIKB);           // 連絡先区分
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKBN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKBN);                               // 送付先区分
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTOROKUYMD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTOROKUYMD);                   // 送付先登録年月日
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE1) = string.Empty;                                                                 // リザーブ１
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE2) = string.Empty;                                                                 // リザーブ２
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE3) = string.Empty;                                                                 // リザーブ３
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE4) = string.Empty;                                                                 // リザーブ４
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE5) = string.Empty;                                                                 // リザーブ５
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                              // 端末ＩＤ
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csSfskHyojunDataRow(ABSfskHyojunEntity.SAKUJOFG);                             // 削除フラグ
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = decimal.Zero;                                                            // 更新カウンタ
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI);           // 作成日時
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;                                               // 作成ユーザー
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI);            // 更新日時
                csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                                // 更新ユーザー
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

            return csReturnDataRow;

        }

        #endregion

        #region 代納送付先累積_標準データ抽出
        // ************************************************************************************************
        // * メソッド名     代納送付先累積_標準データ抽出
        // * 
        // * 構文           PPublic Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
        // *                                                           ByVal strGyomuCD As String,
        // *                                                           ByVal strGyomuNaiShubetsuCD As String,
        // *                                                           ByVal intTorokuRenban As Integer,
        // *                                                           ByVal strShoriKB As String) As DataRow()
        // * 
        // * 機能　　    　 代納送付先累積マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD             : 住民コード 
        // *                strGyomuCD             : 業務コード
        // *                strGyomuNaiShubetsuCD  : 業務内種別コード
        // *                intTorokuRenban        : 登録番号
        // *                strShoriKB             : 処理区分　"D"：代納、"S"：送付
        // * 
        // * 戻り値         DataSet : 取得した代納送付先累積マスタの該当データ(DataRow())
        // ************************************************************************************************
        public DataTable GetABDainoSfskRuisekiData(string strJuminCD, string strGyomuCD, string strGyomuNaiShubetsuCD, int intTorokuRenban, string strShoriKB)
        {

            const string THIS_METHOD_NAME = "GetABDainoSfskRuisekiData";
            DataSet csDainoSfskRuisekiHyojunEntity;
            DataRow[] csReturnDataRows;
            DataTable csReturnDatatable;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiHyojunEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban, strShoriKB, THIS_METHOD_NAME));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csDainoSfskRuisekiHyojunEntity = m_csDataSchma.Clone();
                csDainoSfskRuisekiHyojunEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiHyojunEntity, ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // 戻り値用にデータを格納
                csReturnDatatable = csDainoSfskRuisekiHyojunEntity.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return csReturnDatatable;

        }

        // ************************************************************************************************
        // * メソッド名     SELECT句の作成
        // * 
        // * 構文           Private Sub CreateSelect() As String
        // * 
        // * 機能           SELECT句を生成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String    :   SELECT句
        // ************************************************************************************************
        private string CreateSelect()
        {
            const string THIS_METHOD_NAME = "CreateSelect";
            var strSELECT = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の作成
                strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiHyojunEntity.JUMINCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORIKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.ZENGOKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.STYMD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.EDYMD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RRKNO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SFSKKBN);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TANMATSUID);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUJOFG);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINUSER);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strSELECT.ToString();

        }

        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Function CreateWhere(ByVal strJuminCD As String,
        // *                                             ByVal strGyomuCD As String,
        // *                                             ByVal strGyomuNaiShubetsuCD As String,
        // *                                             ByVal intTorokuRenban As Integer,
        // *                                             ByVal strShoriKB As String,
        // *                                             ByVal strMethodName As String) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD             : 住民コード 
        // *                strGyomuCD             : 業務コード
        // *                strGyomuNaiShubetsuCD  : 業務内種別コード
        // *                strShoriKB             : 処理区分　"D"：代納、"S"：送付
        // *                strMethodName          : 呼出し元関数名
        // *
        // * 戻り値         String    :   WHERE句
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strGyomuCD, string strGyomuNaiShubetsuCD, int intTorokuRenban, string strShoriKB, string strMethodName)
        {

            const string THIS_METHOD_NAME = "CreateWhere";
            const string GET_MAX_TOROKURENBAN = "GetMaxTorokuRenban";

            StringBuilder strWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // WHERE句の作成
                strWHERE = new StringBuilder(256);

                // 住民コード
                strWHERE.AppendFormat("WHERE {0} = {1}", ABDainoSfskRuisekiEntity.JUMINCD, ABDainoSfskRuisekiEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 業務コード
                strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUCD, ABDainoSfskRuisekiEntity.KEY_GYOMUCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = strGyomuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 業務内種別コード
                strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD, ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = strGyomuNaiShubetsuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 登録連番
                strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN;
                cfUFParameterClass.Value = intTorokuRenban;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 処理区分
                // 送付
                strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB, ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE);

                // 前後区分
                strWHERE.AppendFormat(" AND {0} = '{1}'", ABDainoSfskRuisekiEntity.ZENGOKB, ZENGOKB_GO);

                // 履歴番号　降番でソート　
                if ((strMethodName ?? "") != GET_MAX_TOROKURENBAN)
                {
                    strWHERE.AppendFormat(" ORDER BY {0} DESC", ABDainoSfskRuisekiEntity.RRKNO);
                }


                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strWHERE.ToString();

        }
        #endregion

        #endregion

    }
}
