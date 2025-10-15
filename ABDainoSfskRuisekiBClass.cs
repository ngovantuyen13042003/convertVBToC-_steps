// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ＡＢ代納送付先異動累積マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け           2007/08/10
// *
// * 作成者           比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// *  修正履歴　 履歴番号　　修正内容
// * 2010/02/26   000001     送付先データ更新の場合、代納送付先累積マスタ:代納区分に｢40｣をセットするよう改修（比嘉）
// * 2010/04/16   000002     VS2008対応（比嘉）
// * 2023/10/25   000003    【AB-0840-1】送付先管理項目追加（見城）
// * 2023/12/05   000004    【AB-0840-1】送付先管理項目追加_追加修正（仲西）
// * 2024/03/07   000005    【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// * 2024/06/10   000006    【AB-9902-1】不具合対応 
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.reams.ur.publicmodule.library.business.ur002b;
using ndensan.reams.ur.publicmodule.library.businesscommon.ur002x;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{
    // *履歴番号 000003 2023/10/25 追加終了

    // ************************************************************************************************
    // *
    // * 代納送付先異動累積マスタ取得、更新時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABDainoSfskRuisekiBClass
    {

        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private DataSet m_csDataSchma;                        // スキーマ保管用データセット
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABDainoSfskRuisekiBClass";            // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード
        private const string STRING_D = "D";                                          // 代納
        private const string string_S = "S";                                          // 送付先
                                                                                      // *履歴番号 000003 2023/10/25 追加開始
        private const string ZENGOKB_ZEN = "1";                                       // 前後区分　前
        private const string ZENGOKB_GO = "2";                                        // 前後区分　後
        private const string SOUFU_TSUIKA = "S0";                                     // 処理区分　送付_追加
        private const string SOUFU_SHUSEI = "S1";                                     // 処理区分　送付_修正
        private const string SOUFU_SAKUJO = "S2";                                     // 処理区分　送付_削除
        private const string DAINO_TSUIKA = "D0";                                     // 処理区分　代納_追加
        private const string DAINO_SHUSEI = "D1";                                     // 処理区分　代納_修正
        private const string DAINO_SAKUJO = "D2";                                     // 処理区分　代納_削除
        private const string SAKUJO_ON = "1";                                         // 削除フラグ
                                                                                      // *履歴番号 000003 2023/10/25 追加終了
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
        // *                               ByVal cfUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABDainoSfskRuisekiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;

            // AB代納送付先累積マスタのスキーマ取得
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド

        #region 代納送付先異動累積マスタ抽出
        // 使用していないが、作ったので残しておく
        // ''''************************************************************************************************
        // ''''* メソッド名     代納送付先異動累積マスタ抽出
        // ''''* 
        // ''''* 構文           Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
        // ''''* 
        // ''''* 機能　　    　 代納送付先異動累積マスタよりデータを抽出する
        // ''''* 
        // ''''* 引数           strJuminCD        : 住民コード
        // ''''* 
        // ''''* 戻り値         DataSet : 取得した代納送付先異動累積マスタの該当データ
        // ''''************************************************************************************************
        // '''Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
        // '''    Const THIS_METHOD_NAME As String = "GetDainoSfsk"
        // '''    Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        // '''    Dim cfUFParameterClass As UFParameterClass          ' パラメータクラス
        // '''    Dim csDainoSfskEntity As DataSet                    ' 代納送付先累積DataSet
        // '''    Dim strSQL As StringBuilder
        // '''    Dim strWHERE As StringBuilder

        // '''    Try
        // '''        ' デバッグ開始ログ出力
        // '''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''        ' パラメータコレクションのインスタンス化
        // '''        m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

        // '''        ' SQL文の作成
        // '''        strSQL = New StringBuilder
        // '''        strSQL.Append("SELECT * FROM ")
        // '''        strSQL.Append(ABDainoSfskRuisekiEntity.TABLE_NAME)
        // '''        strSQL.Append(" WHERE ")

        // '''        'WHERE句の作成
        // '''        strWHERE = New StringBuilder
        // '''        '住民コード
        // '''        If Not (strJuminCD = String.Empty) Then
        // '''            strWHERE.Append(ABDainoSfskRuisekiEntity.JUMINCD)
        // '''            strWHERE.Append(" = ")
        // '''            strWHERE.Append(ABDainoSfskRuisekiEntity.KEY_JUMINCD)
        // '''            ' 検索条件のパラメータを作成
        // '''            cfUFParameterClass = New UFParameterClass
        // '''            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
        // '''            cfUFParameterClass.Value = strJuminCD
        // '''            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
        // '''            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
        // '''        End If

        // '''        'ORDER句を結合
        // '''        If (strWHERE.Length <> 0) Then
        // '''            strSQL.Append(strWHERE)
        // '''            strSQL.Append(" ORDER BY ")
        // '''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
        // '''            strSQL.Append(" , ")
        // '''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
        // '''        Else
        // '''            strSQL.Append(" ORDER BY ")
        // '''            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
        // '''            strSQL.Append(", ")
        // '''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
        // '''            strSQL.Append(", ")
        // '''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
        // '''        End If

        // '''        ' RDBアクセスログ出力
        // '''        m_cfLogClass.RdbWrite(m_cfControlData, _
        // '''                                    "【クラス名:" + Me.GetType.Name + "】" + _
        // '''                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        // '''                                    "【実行メソッド名:GetDataSet】" + _
        // '''                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

        // '''        ' SQLの実行 DataSetの取得
        // '''        csDainoSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)


        // '''        ' デバッグ終了ログ出力
        // '''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''    Catch objAppExp As UFAppException
        // '''        ' ワーニングログ出力
        // '''        m_cfLogClass.WarningWrite(m_cfControlData, _
        // '''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '''                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // '''                                    "【ワーニング内容:" + objAppExp.Message + "】")
        // '''        ' エラーをそのままスローする
        // '''        Throw

        // '''    Catch objExp As Exception
        // '''        ' エラーログ出力
        // '''        m_cfLogClass.ErrorWrite(m_cfControlData, _
        // '''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '''                                    "【エラー内容:" + objExp.Message + "】")
        // '''        ' エラーをそのままスローする
        // '''        Throw
        // '''    End Try

        // '''    Return csDainoSfskEntity

        // '''End Function
        #endregion

        #region 代納送付先異動累積マスタ追加
        // ************************************************************************************************
        // * メソッド名     代納送付先異動累積マスタ追加
        // * 
        // * 構文           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     　代納送付先異動累積マスタにデータを追加
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertDainoSfskB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertDainoSfskB";
            // * corresponds to VS2008 Start 2010/04/16 000002
            // Dim csInstRow As DataRow
            // Dim csDataColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000002
            int intInsCnt;                            // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABDainoSfskRuisekiEntity.TANMATSUID] = m_cfControlData.m_strClientId;  // 端末ＩＤ
                                                                                                 // csDataRow[ABDainoSfskRuisekiEntity.SAKUJOFG] = "0"                              ' 削除フラグ
                csDataRow[ABDainoSfskRuisekiEntity.KOSHINCOUNTER] = decimal.Zero;                // 更新カウンタ
                csDataRow[ABDainoSfskRuisekiEntity.SAKUSEINICHIJI] = strUpdateDateTime;          // 作成日時
                csDataRow[ABDainoSfskRuisekiEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;   // 作成ユーザー
                csDataRow[ABDainoSfskRuisekiEntity.KOSHINNICHIJI] = strUpdateDateTime;           // 更新日時
                csDataRow[ABDainoSfskRuisekiEntity.KOSHINUSER] = m_cfControlData.m_strUserId;    // 更新ユーザー

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER.RLength())].ToString();

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
        // * 機能　　    　 INSERTのSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義


            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiEntity.TABLE_NAME + " ";
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();


                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    csInsertColumn.Append(csDataColumn.ColumnName);
                    csInsertColumn.Append(", ");

                    csInsertParam.Append(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);


                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + csInsertColumn.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")" + " VALUES (" + csInsertParam.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")";

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

        #region 代納送付先累積データ作成
        // *履歴番号 000003 2023/10/25 修正開始
        // ************************************************************************************************
        // * メソッド名     代納送付先累積データ作成
        // * 
        // * 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strShoriKB As String) As Integer
        // * 
        // * 機能　　    　 代納送付先累積データを作成する
        // * 
        // * 引数           csDataRow As DataRow      : 代納送付先データ
        // *                strShoriKB As String      : 処理区分
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int CreateDainoSfskData(DataRow csDataRow, string strShoriKB)
        {
            string strShoriNichiji = string.Empty;

            return CreateDainoSfskData(csDataRow, strShoriKB, null, ref strShoriNichiji);

        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積データ作成
        // * 
        // * 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strShoriKB As String, _
        // ByRef strShoriNichiji As String) As Integer
        // * 
        // * 機能　　    　 代納送付先累積データを作成する
        // * 
        // * 引数           csDataRow As DataRow      : 代納送付先データ
        // *                strShoriKB As String      : 処理区分
        // *                strShoriNichiji As String : 処理日時
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int CreateDainoSfskData(DataRow csDataRow, string strShoriKB, ref string strShoriNichiji)
        {

            return CreateDainoSfskData(csDataRow, strShoriKB, null, ref strShoriNichiji);

        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積データ作成
        // * 
        // * 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
        // *                                                    ByVal strShoriKB As String, _
        // *                                                    ByVal csABSfskHyojunDataRow As DataRow, _
        // *                                                    ByRef strShoriNichiji As String) As Integer
        // * 
        // * 機能　　    　 代納送付先累積データを作成する
        // * 
        // * 引数           csDataRow As DataRow                : 代納送付先データ
        // *                strShoriKB As String                : 処理区分
        // *                csABSfskHyojunDataRow As DataRow    : AB送付先_標準データ（DataRow形式）
        // *                strShoriNichiji As String           : 処理日時
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************

        // Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String) As Integer
        public int CreateDainoSfskData(DataRow csDataRow, string strShoriKB, DataRow csABSfskHyojunDataRow, ref string strShorinichiji)
        {
            // *履歴番号 000003 2023/10/25 修正終了
            const string THIS_METHOD_NAME = "CreateDainoSfskData";
            DataSet csDataSet;
            DataRow csRuisekiDR;
            DataColumn csDataColumn;
            string strSystemDate;                 // システム日付
            int intInsCnt;
            // Dim csDainoSfskRows[] As DataRow
            // Dim csDainoSfskRow As DataRow
            // * corresponds to VS2008 Start 2010/04/16 000002
            // Dim csNewDainosfskRow As DataRow
            // * corresponds to VS2008 End 2010/04/16 000002
            DataRow csOriginalDR;
            // Dim csDainoSfskEntity As DataSet
            int intUpdataCount_zen;
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");
                strShorinichiji = strSystemDate;
                // スキーマを取得
                csDataSet = m_csDataSchma.Clone();

                // ***
                // * 代納送付先累積(前)編集処理
                // *
                if (strShoriKB != ABConstClass.DAINO_ADD && strShoriKB != ABConstClass.SFSK_ADD)
                {
                    // 処理区分が追加以外の場合
                    if (csDataRow.HasVersion(DataRowVersion.Original))
                    {
                        // 修正前情報が残っている場合

                        // 代納送付先累積データを作成
                        csOriginalDR = csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].NewRow();

                        foreach (DataColumn currentCsDataColumn in csDataRow.Table.Columns)
                        {
                            csDataColumn = currentCsDataColumn;
                            if (!(csDataColumn.ColumnName == ABDainoEntity.RESERVE) && !(csDataColumn.ColumnName == ABSfskDataEntity.SFSKDATAKB))
                            {
                                csOriginalDR[csDataColumn.ColumnName] = csDataRow[csDataColumn.ColumnName, DataRowVersion.Original];
                            }
                        }

                        csOriginalDR[ABDainoSfskRuisekiEntity.SHORINICHIJI] = strSystemDate;
                        csOriginalDR[ABDainoSfskRuisekiEntity.SHORIKB] = strShoriKB;               // 処理区分
                        csOriginalDR[ABDainoSfskRuisekiEntity.ZENGOKB] = "1";                      // 前後区分

                        // *履歴番号 000001 2010/02/26 修正開始
                        // -- コメント修正 --
                        // '''' 送付先データの場合、送付先区分を代納区分にセットする
                        // 送付先データの場合、代納区分に｢40｣をセットする。送付先データは｢40｣固定のため。
                        // -- コメント修正 --
                        if (strShoriKB.RSubstring(0, 1) == "S")
                        {
                            // csOriginalDR[ABDainoSfskRuisekiEntity.DAINOKB] = csDataRow[ABSfskEntity.SFSKDATAKB]
                            csOriginalDR[ABDainoSfskRuisekiEntity.DAINOKB] = "40";

                            // *履歴番号 000003 2023/10/25 追加開始
                            if (!(csABSfskHyojunDataRow == null) && csABSfskHyojunDataRow.HasVersion(DataRowVersion.Original))
                            {
                                // 送付先_標準がNothing以外でかつ、修正前情報が残っている場合
                                // 送付先番地コード１
                                csOriginalDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD1] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD1, DataRowVersion.Original];
                                // 送付先番地コード２
                                csOriginalDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD2] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD2, DataRowVersion.Original];
                                // 送付先番地コード３
                                csOriginalDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD3] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD3, DataRowVersion.Original];
                                // 送付先方書コード
                                csOriginalDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKKATAGAKICD, DataRowVersion.Original];
                            }
                        }
                        // *履歴番号 000003 2023/10/25 追加終了

                        else
                        {
                        }
                        // *履歴番号 000001 2010/02/26 修正終了

                        // データセットに修正前情報を追加
                        csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Rows.Add(csOriginalDR);

                        // 代納送付先累積(前)マスタ追加処理
                        intUpdataCount_zen = this.InsertDainoSfskB(csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Rows[0]);

                        // 更新件数が１件以外の場合、エラーを発生させる
                        if (!(intUpdataCount_zen == 1))
                        {
                            m_cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                            // エラー定義を取得（既に同一データが存在します。：代納送付先累積）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "代納送付先累積", objErrorStruct.m_strErrorCode);
                        }

                        // データセットのクリア
                        csDataSet.Clear();
                    }
                    else
                    {

                    }
                }
                else
                {

                }


                // ***
                // * 代納送付先累積(後)編集処理
                // *
                // 代納送付先累積データを作成
                csRuisekiDR = csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].NewRow();

                foreach (DataColumn currentCsDataColumn1 in csDataRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn1;
                    if (!(csDataColumn.ColumnName == ABDainoEntity.RESERVE) && !(csDataColumn.ColumnName == ABSfskDataEntity.SFSKDATAKB))
                    {
                        csRuisekiDR[csDataColumn.ColumnName] = csDataRow[csDataColumn.ColumnName];
                    }
                }

                // 共通項目のデータセット
                csRuisekiDR[ABDainoSfskRuisekiEntity.SHORINICHIJI] = strSystemDate;              // 処理日時
                csRuisekiDR[ABDainoSfskRuisekiEntity.SHORIKB] = strShoriKB;                      // 処理区分
                csRuisekiDR[ABDainoSfskRuisekiEntity.ZENGOKB] = "2";                             // 前後区分
                csRuisekiDR[ABDainoSfskRuisekiEntity.RESERVE1] = string.Empty;                   // リザーブ1
                csRuisekiDR[ABDainoSfskRuisekiEntity.RESERVE2] = string.Empty;                   // リザーブ2

                // *履歴番号 000003 2023/10/25 追加開始
                // 代納、送付先の処理区分が削除の場合、削除フラグを立てる
                if (strShoriKB == ABConstClass.DAINO_DELETE || strShoriKB == ABConstClass.SFSK_DELETE)
                {
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SAKUJOFG] = SAKUJO_ON;                  // 削除フラグ

                }
                // *履歴番号 000003 2023/10/25 追加終了

                // 代納データ、送付先データ別処理の場合
                // If (CStr(csDataRow[ABDainoSfskRuisekiEntity.DAINOKB]) <> "40") Then
                if (strShoriKB.RSubstring(0, 1) == "D")
                {
                    // 代納データの場合
                    // 代納区分が"40"以外の場合
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB] = string.Empty;     // 送付先管内管外区分
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKANAMEISHO] = string.Empty;         // 送付先カナ名称
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO] = string.Empty;        // 送付先漢字名称
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKYUBINNO] = string.Empty;            // 送付先郵便番号
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKZJUSHOCD] = string.Empty;           // 送付先住所コード
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKJUSHO] = string.Empty;              // 送付先住所
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD1] = string.Empty;          // 送付先番地コード1
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD2] = string.Empty;          // 送付先番地コード2
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD3] = string.Empty;          // 送付先番地コード3
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHI] = string.Empty;             // 送付先番地
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD] = string.Empty;         // 送付先方書コード
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKI] = string.Empty;           // 送付先方書
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1] = string.Empty;       // 送付先連絡先1
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2] = string.Empty;       // 送付先連絡先2
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD] = string.Empty;         // 送付先行政区コード
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI] = string.Empty;        // 送付先行政区名
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUCD1] = string.Empty;           // 送付先地区コード1
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1] = string.Empty;          // 送付先地区名1
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUCD2] = string.Empty;           // 送付先地区コード2
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2] = string.Empty;          // 送付先地区名2
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUCD3] = string.Empty;           // 送付先地区コード3
                    csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3] = string.Empty;          // 送付先地区名3
                }
                else
                {
                    // 送付先データの場合
                    // 代納区分が"40"の場合
                    // *履歴番号 000001 2010/02/26 修正開始
                    // **コメント ： 送付先データの場合、代納区分に｢40｣をセット。送付先データは｢40｣固定のため。
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.DAINOKB] = csDataRow[ABSfskEntity.SFSKDATAKB]
                    csRuisekiDR[ABDainoSfskRuisekiEntity.DAINOKB] = "40";
                    // *履歴番号 000001 2010/02/26 修正終了
                    // *履歴番号 000003 2023/10/25 修正開始
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.DAINOJUMINCD] = String.Empty
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD1] = String.Empty          ' 送付先番地コード1
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD2] = String.Empty          ' 送付先番地コード2
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD3] = String.Empty          ' 送付先番地コード3
                    // csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD] = String.Empty         ' 送付先方書コード
                    if (!(csABSfskHyojunDataRow == null))
                    {
                        // 送付先_標準がNothing以外の場合
                        // 送付先番地コード１
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD1] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD1];
                        // 送付先番地コード２
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD2] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD2];
                        // 送付先番地コード３
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD3] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKBANCHICD3];
                        // 送付先方書コード
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD] = csABSfskHyojunDataRow[ABSfskHyojunEntity.SFSKKATAGAKICD];
                    }
                    else
                    {
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD1] = string.Empty;          // 送付先番地コード1
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD2] = string.Empty;          // 送付先番地コード2
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKBANCHICD3] = string.Empty;          // 送付先番地コード3
                        csRuisekiDR[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD] = string.Empty;

                    }         // 送付先方書コード
                              // *履歴番号 000003 2023/10/25 修正終了
                }

                csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Rows.Add(csRuisekiDR);

                // ***
                // * 代納送付先累積(後)マスタ追加処理
                // *
                intInsCnt = InsertDainoSfskB(csDataSet.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Rows[0]);

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

        // *履歴番号 000003 2023/10/25 追加開始
        #region 代納送付先累積データ抽出
        // ************************************************************************************************
        // * メソッド名     代納送付先累積データ抽出
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
        public DataRow[] GetABDainoSfskRuisekiData(string strJuminCD, string strGyomuCD, string strGyomuNaiShubetsuCD, int intTorokuRenban, string strShoriKB)
        {

            const string THIS_METHOD_NAME = "GetABDainoSfskRuisekiData";
            DataSet csDainoSfskRuisekiEntity;
            DataRow[] csReturnDataRows;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban.ToString(), strShoriKB, THIS_METHOD_NAME));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csDainoSfskRuisekiEntity = m_csDataSchma.Clone();
                csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity, ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // 戻り値用にデータを格納
                strSQL.Clear();
                strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD);
                strSQL.Append(" = '");
                strSQL.Append(strJuminCD);
                strSQL.Append("'");
                csReturnDataRows = csDainoSfskRuisekiEntity.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Select(strSQL.ToString());

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

            return csReturnDataRows;

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
                strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiEntity.JUMINCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHICHOSONCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KYUSHICHOSONCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORINICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORIKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.ZENGOKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TOROKURENBAN);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.STYMD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.EDYMD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RRKNO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOJUMINCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANAMEISHO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKYUBINNO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKZJUSHOCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKJUSHO);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD3);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKICD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD3);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE1);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE2);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TANMATSUID);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUJOFG);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINCOUNTER);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEINICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEIUSER);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINNICHIJI);
                strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINUSER);

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
        // *                                             ByVal strTorokuRenban As String,
        // *                                             ByVal strShoriKB As String,
        // *                                             ByVal strMethodName As String) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD             : 住民コード 
        // *                strGyomuCD             : 業務コード
        // *                strGyomuNaiShubetsuCD  : 業務内種別コード
        // *                strTorokuRenban        : 登録連番
        // *                strShoriKB             : 処理区分　"D"：代納、"S"：送付
        // *                strMethodName          : 呼出し元関数名
        // *
        // * 戻り値         String    :   WHERE句
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strGyomuCD, string strGyomuNaiShubetsuCD, string strTorokuRenban, string strShoriKB, string strMethodName)
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
                if (!string.IsNullOrEmpty(strTorokuRenban))
                {
                    strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN;
                    cfUFParameterClass.Value = strTorokuRenban;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 処理区分
                switch (strShoriKB ?? "")
                {
                    case string_S:
                        {
                            // 送付
                            strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB, ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE);
                            break;
                        }

                    case STRING_D:
                        {
                            // 代納
                            // *履歴番号 000004 2023/12/05 修正開始
                            // strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                            // ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_SHUSEI)
                            strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB, ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_DELETE);
                            break;
                        }
                        // *履歴番号 000004 2023/12/05 修正終了

                }

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

        #region 登録連番最大値取得処理
        // ************************************************************************************************
        // * メソッド名     登録連番最大値取得処理
        // * 
        // * 構文           Public Function GetMaxTorokuRenban(ByVal strJuminCD As String,
        // *                                                    ByVal strGyomuCD As String,
        // *                                                    ByVal strGyomuNaiShubetsuCD As String,
        // *                                                    ByVal strShoriKB As String) As Integer
        // * 
        // * 機能　　    　 代納送付先累積マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD             : 住民コード 
        // *                strGyomuCD             : 業務コード
        // *                strGyomuNaiShubetsuCD  : 業務内種別コード
        // *                strShoriKB             : 処理区分　"D"：代納、"S"：送付
        // * 
        // * 戻り値         Integer : 取得した登録連番の最大
        // ************************************************************************************************
        public int GetMaxTorokuRenban(string strJuminCD, string strGyomuCD, string strGyomuNaiShubetsuCD, string strShoriKB)
        {

            const string THIS_METHOD_NAME = "GetMaxTorokuRenban";
            DataSet csDainoSfskRuisekiEntity;
            int intMaxTorokuRenban = 0;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の生成
                strSQL.AppendFormat("SELECT MAX({0}) AS MAXTOROKURENBAN ", ABDainoSfskRuisekiEntity.TOROKURENBAN);
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, string.Empty, strShoriKB, THIS_METHOD_NAME));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csDainoSfskRuisekiEntity = m_csDataSchma.Clone();
                csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity, default, m_cfSelectUFParameterCollectionClass, false);

                if (0 < csDainoSfskRuisekiEntity.Tables[ABDainoSfskRuisekiEntity.TABLE_NAME].Rows.Count)
                {
                    // データがある場合は戻り値に格納する
                    if (UFVBAPI.IsNumeric(csDainoSfskRuisekiEntity.Tables[0].Rows[0][0]))
                    {
                        intMaxTorokuRenban = UFVBAPI.ToInteger(csDainoSfskRuisekiEntity.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        // データが無い場合は0を戻り値にセット
                        intMaxTorokuRenban = 0;
                    }

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

            return intMaxTorokuRenban;

        }
        #endregion


        #region 代納送付先累積データをエンティティに格納する

        // ************************************************************************************************
        // * メソッド名     代納送付先累積と備考のデータをエンティティに格納する
        // * 
        // * 構文        Public Function SetDainoSfsfRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
        // *                                                    ByVal strShoriKB As String) As DataSet
        // * 
        // * 機能　　    　 代納送付先累積マスタより該当データを格納する
        // * 
        // * 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
        // *                strShoriKB As String                   ：処理区分　"D"：代納、"S"：送付先
        // * 
        // * 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
        // ************************************************************************************************
        public DataSet SetDainoSfsfRirekiData(DataSet csDainoSfskRuisekiDataset, string strShoriKB)
        {
            const string SHORIKB_SFSK = "S";
            const string SHORIKB_DAINO = "D";

            var csReturnDataset = default(DataSet);

            if ((strShoriKB ?? "") == SHORIKB_SFSK)
            {
                csReturnDataset = SetSfskRirekiData(csDainoSfskRuisekiDataset, strShoriKB);
            }
            else if ((strShoriKB ?? "") == SHORIKB_DAINO)
            {
                csReturnDataset = SetDainoRirekiData(csDainoSfskRuisekiDataset, strShoriKB);
            }

            return csReturnDataset;
        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積と備考のデータをエンティティに格納する
        // * 
        // * 構文           Public Function SetSfskRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
        // *                                                  ByVal strShoriKB As String) As DataSet
        // * 
        // * 機能　　    　 代納送付先累積マスタより該当データを格納する
        // * 
        // * 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
        // *                strShoriKB As String                   ：処理区分　"D"：代納、"S"：送付先
        // * 
        // * 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
        // ************************************************************************************************
        public DataSet SetSfskRirekiData(DataSet csDainoSfskRuisekiDataset, string strShoriKB)
        {
            // 定数
            const string ALL9_YMD = "99999999";               // 年月日オール９
            const string SFSK = "送付先";                      // 送付先文言

            DataSet csReturnDataset;
            DataRow csDataNewRow;

            bool blnIsDainoSfskBiko = false;
            DataSet csBikoDataSet;
            // *履歴番号 000004 2023/12/05 修正開始
            // Dim blnSakujoFG As Boolean = False
            bool blnSakujoFG = true;
            // *履歴番号 000004 2023/12/05 修正終了

            URGyomuCDMstBClass cGyomuCDMstB;              // 業務コードマスタＤＡ
            DataSet csGyomuCDMstEntity;                   // 業務コードマスタDataSet
            UFDateClass cfDate;                           // 日付クラス
            ABDainoKankeiBClass cDainoKankeiB;            // 代納関係取得クラス
            ABAtenaGetBClass cAtenaGetB;                  // 宛名取得クラス
            ABAtenaHenshuBClass cAtenaHenshuB;            // 宛名編集Ｂ
            ABJuminShubetsuBClass cJuminShubetsuB;        // 住民種別名称取得クラス
            ABKannaiKangaiKBBClass cKannaiKangaiKBB;      // 管内管外名称取得クラス
            ABBikoBClass cABBikoB;

            DataTable csDataTable;
            ABDainoSfskRuisekiBClass cDainoSfskRuisekiB;               // 代納送付先累積ＤＡビジネスクラス
            ABDainoSfskRuiseki_HyojunBClass cDainoSfskRuisekiHyojunB;  // 代納送付先累積_標準ＤＡビジネスクラス
            DataRow[] csSfskRirekiDataRows;
            DataRow csSfskRirekiHyojunDataRow;
            var csSfskRirekiHyojunDataTable = new DataTable();

            // データ抽出用変数
            string strJuminCd;
            string strGyomuCD;
            string strGyomuNaiShuCD;
            int intTorokuRenban;
            // *履歴番号 000004 2023/12/05 追加開始
            string strKannaiKangaiCD;
            string strKannaiKangaiMeisho;
            // *履歴番号 000004 2023/12/05 追加終了

            try
            {

                DataRow[] csDataRows;

                csDataRows = csDainoSfskRuisekiDataset.Tables[ABSfskDataEntity.TABLE_NAME].Select(string.Format("{0} = 'True'", ABSfskDataEntity.CHECK));

                strJuminCd = csDataRows[0][ABSfskDataEntity.JUMINCD].ToString();
                strGyomuCD = csDataRows[0][ABSfskDataEntity.GYOMUCD].ToString();
                strGyomuNaiShuCD = csDataRows[0][ABSfskDataEntity.GYOMUNAISHUCD].ToString();
                intTorokuRenban = UFVBAPI.ToInteger(csDataRows[0][ABSfskDataEntity.TOROKURENBAN]);

                // 代納送付先累積データの取得
                // 代納送付先累積ＤＡクラスのインスタンス化
                cDainoSfskRuisekiB = new ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                csSfskRirekiDataRows = cDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd, strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB);
                // 代納送付先累積_標準データの取得
                cDainoSfskRuisekiHyojunB = new ABDainoSfskRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // データセットインスタンス化
                csReturnDataset = new DataSet();

                // テーブルセットの取得
                csDataTable = CreateColumnsABSfskRirekiData();

                // データセットにテーブルセットの追加
                csReturnDataset.Tables.Add(csDataTable);

                // 日付クラスのインスタンス化
                cfDate = new UFDateClass(m_cfConfigDataClass);

                // 代納関係取得インスタンス化
                cDainoKankeiB = new ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 業務コードマスタＤＡのインスタンス作成
                cGyomuCDMstB = new URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 宛名編集Ｂのインスタンス作成
                cAtenaHenshuB = new ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 宛名取得クラスインスタンス化
                cAtenaGetB = new ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass);

                // 住民種別クラスインスタンス化
                cJuminShubetsuB = new ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass);

                // 管内管外クラスインスタンス化
                cKannaiKangaiKBB = new ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass);

                // 備考クラスのインスタンス化
                cABBikoB = new ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                foreach (var csDataRow in csSfskRirekiDataRows)
                {

                    csDataNewRow = csReturnDataset.Tables[ABSfskDataEntity.TABLE_NAME].NewRow();

                    // 初期値の設定
                    foreach (DataColumn csDataColumn in csDataNewRow.Table.Columns)
                    {
                        if (csDataColumn.ColumnName == ABSfskDataEntity.KOSHINCOUNTER)
                        {
                            csDataNewRow[csDataColumn.ColumnName] = decimal.Zero;
                        }
                        else
                        {
                            csDataNewRow[csDataColumn.ColumnName] = string.Empty;
                        }
                    }

                    // 住民コード
                    csDataNewRow[ABSfskDataEntity.JUMINCD] = csDataRow[ABDainoSfskRuisekiEntity.JUMINCD];
                    // 市町村コード
                    csDataNewRow[ABSfskDataEntity.SHICHOSONCD] = csDataRow[ABDainoSfskRuisekiEntity.SHICHOSONCD];
                    // 旧市町村コード
                    csDataNewRow[ABSfskDataEntity.KYUSHICHOSONCD] = csDataRow[ABDainoSfskRuisekiEntity.KYUSHICHOSONCD];
                    // 業務コード
                    csDataNewRow[ABSfskDataEntity.GYOMUCD] = csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD];

                    // 業務コードマスタより取得する
                    strGyomuCD = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD]);
                    csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD);

                    if (csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows.Count == 0)
                    {
                        // 業務名称
                        csDataNewRow[ABSfskDataEntity.GYOMUMEISHO] = string.Empty;
                        // 業務名称略
                        csDataNewRow[ABSfskDataEntity.GYOMUMEISHORYAKU] = string.Empty;
                    }
                    else
                    {
                        // 業務名称
                        csDataNewRow[ABSfskDataEntity.GYOMUMEISHO] = csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows[0](URGyomuCDMstEntity.GYOMUMEI);
                        // 業務名称略
                        csDataNewRow[ABSfskDataEntity.GYOMUMEISHORYAKU] = csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows[0](URGyomuCDMstEntity.GYOMURYAKUSHO);
                    }

                    // 業務内種別コード
                    csDataNewRow[ABSfskDataEntity.GYOMUNAISHUCD] = csDataRow[ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD];
                    // 代納住民コード
                    csDataNewRow[ABSfskDataEntity.DAINOJUMINCD] = csDataRow[ABDainoSfskRuisekiEntity.DAINOJUMINCD];
                    // 開始年月
                    csDataNewRow[ABSfskDataEntity.STYMD] = csDataRow[ABDainoSfskRuisekiEntity.STYMD];
                    // 終了年月
                    csDataNewRow[ABSfskDataEntity.EDYMD] = csDataRow[ABDainoSfskRuisekiEntity.EDYMD];

                    // 表示用開始年月
                    cfDate.p_strDateValue = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.STYMD]);
                    cfDate.p_enEraType = UFEraType.KanjiRyaku;
                    cfDate.p_enDateSeparator = UFDateSeparator.Period;
                    csDataNewRow[ABSfskDataEntity.DISP_STYMD] = cfDate.p_strWarekiYMD;

                    // 表示用終了年月（999999の時は、非表示）
                    if ((UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.EDYMD]) ?? "") == ALL9_YMD)
                    {
                        csDataNewRow[ABSfskDataEntity.DISP_EDYMD] = string.Empty;
                    }
                    else
                    {
                        cfDate.p_strDateValue = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.EDYMD]);
                        csDataNewRow[ABSfskDataEntity.DISP_EDYMD] = cfDate.p_strWarekiYMD;
                    }

                    // 送付先カナ名称
                    csDataNewRow[ABSfskDataEntity.SFSKKANAMEISHO] = csDataRow[ABDainoSfskRuisekiEntity.SFSKKANAMEISHO];
                    // 送付先漢字名称
                    csDataNewRow[ABSfskDataEntity.SFSKKANJIMEISHO] = csDataRow[ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO];

                    // 送付先管内管外区分
                    csDataNewRow[ABSfskDataEntity.SFSKKANNAiKANGAIKB] = csDataRow[ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB];
                    // *履歴番号 000004 2023/12/05 追加開始
                    // 管内管外名称キーセット
                    strKannaiKangaiCD = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB]);
                    // 管内管外名称取得メゾット実行
                    strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD);
                    // 管内管外名称
                    csDataNewRow[ABSfskDataEntity.SFSKKANNAIKANGAIMEI] = strKannaiKangaiMeisho;
                    // *履歴番号 000004 2023/12/05 追加終了
                    // 送付先郵便番号
                    csDataNewRow[ABSfskDataEntity.SFSKYUBINNO] = csDataRow[ABDainoSfskRuisekiEntity.SFSKYUBINNO];
                    // 送付先住所コード
                    csDataNewRow[ABSfskDataEntity.SFSKZJUSHOCD] = csDataRow[ABDainoSfskRuisekiEntity.SFSKZJUSHOCD];
                    // 送付先住所
                    csDataNewRow[ABSfskDataEntity.SFSKJUSHO] = csDataRow[ABDainoSfskRuisekiEntity.SFSKJUSHO];
                    // 送付先番地
                    csDataNewRow[ABSfskDataEntity.SFSKBANCHI] = csDataRow[ABDainoSfskRuisekiEntity.SFSKBANCHI];
                    // 送付先番地コード1
                    csDataNewRow[ABSfskDataEntity.BANCHICD1] = csDataRow[ABDainoSfskRuisekiEntity.SFSKBANCHICD1];
                    // 送付先番地コード2
                    csDataNewRow[ABSfskDataEntity.BANCHICD2] = csDataRow[ABDainoSfskRuisekiEntity.SFSKBANCHICD2];
                    // 送付先番地コード3
                    csDataNewRow[ABSfskDataEntity.BANCHICD3] = csDataRow[ABDainoSfskRuisekiEntity.SFSKBANCHICD3];
                    // 送付先方書
                    csDataNewRow[ABSfskDataEntity.SFSKKATAGAKI] = csDataRow[ABDainoSfskRuisekiEntity.SFSKKATAGAKI];
                    // 送付先連絡先１
                    csDataNewRow[ABSfskDataEntity.SFSKRENRAKUSAKI1] = csDataRow[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1];
                    // 送付先連絡先２
                    csDataNewRow[ABSfskDataEntity.SFSKRENRAKUSAKI2] = csDataRow[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2];
                    // 方書コード
                    csDataNewRow[ABSfskDataEntity.SFSKKATAGAKICD] = csDataRow[ABDainoSfskRuisekiEntity.SFSKKATAGAKICD];
                    // 送付先行政区コード
                    csDataNewRow[ABSfskDataEntity.SFSKGYOSEIKUCD] = csDataRow[ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD];
                    // 送付先行政区名
                    // 行政区ＣＤに数字以外のものが混入している場合はそのまま行政区名称をセット
                    csDataNewRow[ABSfskDataEntity.SFSKGYOSEIKUMEI] = csDataRow[ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI];
                    // 送付先地区コード１
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUCD1] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUCD1];
                    // 送付先地区名１
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUMEI1] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1];
                    // 送付先地区コード２
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUCD2] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUCD2];
                    // 送付先地区名２
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUMEI2] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2];
                    // 送付先地区コード３
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUCD3] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUCD3];
                    // 送付先地区名３
                    csDataNewRow[ABSfskDataEntity.SFSKCHIKUMEI3] = csDataRow[ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3];
                    // 送付先連絡先１
                    csDataNewRow[ABSfskDataEntity.SFSKRENRAKUSAKI1] = csDataRow[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1];
                    // 送付先連絡先２
                    csDataNewRow[ABSfskDataEntity.SFSKRENRAKUSAKI2] = csDataRow[ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2];


                    csSfskRirekiHyojunDataTable = cDainoSfskRuisekiHyojunB.GetABDainoSfskRuisekiData(strJuminCd, strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB);
                    csSfskRirekiHyojunDataRow = csSfskRirekiHyojunDataTable.Select(string.Format("{0}='{1}'", ABDainoSfskRuisekiHyojunEntity.RRKNO, csDataRow[ABDainoSfskRuisekiEntity.RRKNO].ToString()))[0];

                    // 備考マスタを取得
                    csBikoDataSet = cABBikoB.SelectByKey(ABBikoEntity.DEFAULT.BIKOKBN.SFSK, csDataRow[ABDainoSfskRuisekiEntity.JUMINCD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.TOROKURENBAN].ToString(), csDataRow[ABDainoSfskRuisekiEntity.RRKNO].ToString(), blnSakujoFG);

                    if (csBikoDataSet is not null && 0 < csBikoDataSet.Tables[ABBikoEntity.TABLE_NAME].Rows.Count)
                    {
                        // 住民コード
                        csDataNewRow[ABSfskDataEntity.DAINOJUMINCD] = csBikoDataSet.Tables[ABBikoEntity.TABLE_NAME].Rows[0](ABBikoEntity.RESERVE);
                        csDataNewRow[ABSfskDataEntity.BIKO] = csBikoDataSet.Tables[ABBikoEntity.TABLE_NAME].Rows[0](ABBikoEntity.BIKO);
                    }
                    else
                    {
                        csDataNewRow[ABSfskDataEntity.BIKO] = string.Empty;
                    }

                    csDataNewRow[ABSfskDataEntity.CHECK] = false;
                    csDataNewRow[ABSfskDataEntity.JOTAI] = ABDainoSfskShoriMode.Empty.GetHashCode().ToString();
                    csDataNewRow[ABSfskDataEntity.DISP_JOTAI] = string.Empty;
                    csDataNewRow[ABSfskDataEntity.SEIGYOKB] = string.Empty;

                    csDataNewRow[ABSfskDataEntity.TOROKURENBAN] = csDataRow[ABDainoSfskRuisekiEntity.TOROKURENBAN];     // 登録連番
                    csDataNewRow[ABSfskDataEntity.RRKNO] = csDataRow[ABDainoSfskRuisekiEntity.RRKNO];                   // 履歴番号
                    csDataNewRow[ABSfskDataEntity.SHIKUCHOSONCD] = string.Empty;                                        // 市区町村コート
                    csDataNewRow[ABSfskDataEntity.MACHIAZACD] = string.Empty;                                           // 町字コード
                    csDataNewRow[ABSfskDataEntity.TODOFUKEN] = string.Empty;                                            // 都道府県
                    csDataNewRow[ABSfskDataEntity.SHIKUCHOSON] = string.Empty;
                    csDataNewRow[ABSfskDataEntity.MACHIAZA] = string.Empty;

                    // 送付先区分
                    csDataNewRow[ABSfskDataEntity.SFSKKBN] = csSfskRirekiHyojunDataRow[ABDainoSfskRuisekiHyojunEntity.SFSKKBN].ToString();

                    csDataNewRow[ABSfskDataEntity.DISP_DAINOKB] = SFSK;

                    // 削除フラグ
                    csDataNewRow[ABSfskDataEntity.SAKUJOFG] = csDataRow[ABDainoSfskRuisekiEntity.SAKUJOFG];

                    // 更新ユーザ
                    csDataNewRow[ABSfskDataEntity.KOSHINUSER] = csDataRow[ABDainoSfskRuisekiEntity.KOSHINUSER];
                    // 更新カウンタ
                    csDataNewRow[ABSfskDataEntity.KOSHINCOUNTER] = csDataRow[ABDainoSfskRuisekiEntity.KOSHINCOUNTER];

                    csReturnDataset.Tables[ABSfskDataEntity.TABLE_NAME].Rows.Add(csDataNewRow);

                }
                csReturnDataset.AcceptChanges();
            }


            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            return csReturnDataset;

        }

        // ************************************************************************************************
        // * メソッド名     代納送付先累積と備考のデータをエンティティに格納する
        // * 
        // * 構文        Public Function SetDainoRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
        // *                                                    ByVal strShoriKB As String) As DataSet
        // * 
        // * 機能　　    　 代納送付先累積マスタより該当データを格納する
        // * 
        // * 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
        // *                strShoriKB As String                  : 処理区分　"D"：代納、"S"：送付先
        // * 
        // * 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
        // ************************************************************************************************
        public DataSet SetDainoRirekiData(DataSet csDainoSfskRuisekiDataset, string strShoriKB)
        {
            // 定数
            const string ALL9_YMD = "99999999";               // 年月日オール９
            const string JUSHOHENSHU1_PARA_ONE = "1";         // 情報編集1　パラメータ＝1
            const string GET_HONNINDATA = "1";                // 本人データ取得
            const string DATAKB_HOJIN = "20";                 // データ区分　法人
            const string DATASHU_FRN = "2";                   // データ種　外国人

            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            DataSet csReturnDataset;
            DataRow csDataNewRow;
            DataSet csDainoKankeiDataSet;
            DataSet csAtenaDataSet;
            DataRow csAtenaRow;

            string strDainoKB;
            int intRowCount;
            string strDataKB;
            string strDataShu;
            string strMeisho;
            string strKannaiKangaiCD;
            string strKannaiKangaiMeisho;
            string strKanjiShimei;                        // 漢字氏名
            string strKanaShimei;                         // カナ氏名
            string strYubinNO;                            // 郵便番号
            DataSet csBikoDataSet;
            // *履歴番号 000004 2023/12/05 修正開始
            // Dim blnSakujoFG As Boolean = False
            bool blnSakujoFG = true;
            // *履歴番号 000004 2023/12/05 修正終了

            URGyomuCDMstBClass cGyomuCDMstB;              // 業務コードマスタＤＡ
            DataSet csGyomuCDMstEntity;                   // 業務コードマスタDataSet
            UFDateClass cfDate;                           // 日付クラス
            ABDainoKankeiBClass cDainoKankeiB;            // 代納関係取得クラス
            ABAtenaGetBClass cAtenaGetB;                  // 宛名取得クラス
            ABAtenaGetPara1XClass cAtenaGetPara1X;        // 宛名取得パラメータクラス
            ABAtenaHenshuBClass cAtenaHenshuB;            // 宛名編集Ｂ
            DataSet csAtena1Entity;                       // 宛名データEntity
            ABJuminShubetsuBClass cJuminShubetsuB;        // 住民種別名称取得クラス
            ABKannaiKangaiKBBClass cKannaiKangaiKBB;      // 管内管外名称取得クラス
            ABBikoBClass cABBikoB;

            DataTable csDataTable;
            ABDainoSfskRuisekiBClass csDainoSfskRuisekiB; // 代納送付先累積ＤＡビジネスクラス
            DataRow[] csDainoRirekiDataRows;

            // データ抽出用変数
            string strJuminCd;
            string strGyomuCD;
            string strGyomuNaiShuCD;
            int intTorokuRenban;

            try
            {

                DataRow[] csDataRows;
                csDataRows = csDainoSfskRuisekiDataset.Tables[ABDainoDataEntity.TABLE_NAME].Select(string.Format("{0} = 'True'", ABDainoDataEntity.CHECK));

                strJuminCd = csDataRows[0][ABDainoDataEntity.JUMINCD].ToString();
                strGyomuCD = csDataRows[0][ABDainoDataEntity.GYOMUCD].ToString();
                strGyomuNaiShuCD = csDataRows[0][ABDainoDataEntity.GYOMUNAISHUCD].ToString();
                intTorokuRenban = UFVBAPI.ToInteger(csDataRows[0][ABDainoDataEntity.TOROKURENBAN]);


                // 代納送付先累積データの取得
                // 代納送付先累積ＤＡクラスのインスタンス化
                csDainoSfskRuisekiB = new ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                csDainoRirekiDataRows = csDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd, strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB);

                // データセットインスタンス化
                csReturnDataset = new DataSet();

                // テーブルセットの取得
                csDataTable = CreateColumnsABDainoRirekiData();

                // データセットにテーブルセットの追加
                csReturnDataset.Tables.Add(csDataTable);

                // 日付クラスのインスタンス化
                cfDate = new UFDateClass(m_cfConfigDataClass);

                // 代納関係取得インスタンス化
                cDainoKankeiB = new ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 業務コードマスタＤＡのインスタンス作成
                cGyomuCDMstB = new URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 宛名編集Ｂのインスタンス作成
                cAtenaHenshuB = new ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 宛名取得クラスインスタンス化
                cAtenaGetB = new ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass);

                // 住民種別クラスインスタンス化
                cJuminShubetsuB = new ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass);

                // 管内管外クラスインスタンス化
                cKannaiKangaiKBB = new ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass);

                // 備考クラスのインスタンス化
                cABBikoB = new ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                foreach (var csDataRow in csDainoRirekiDataRows)
                {
                    csDataNewRow = csReturnDataset.Tables[ABDainoDataEntity.TABLE_NAME].NewRow();

                    // 初期値の設定
                    foreach (DataColumn csDataColumn in csDataNewRow.Table.Columns)
                    {
                        if (csDataColumn.ColumnName == ABDainoDataEntity.KOSHINCOUNTER)
                        {
                            csDataNewRow[csDataColumn.ColumnName] = decimal.Zero;
                        }
                        else
                        {
                            csDataNewRow[csDataColumn.ColumnName] = string.Empty;
                        }
                    }

                    // 住民コード
                    csDataNewRow[ABDainoDataEntity.JUMINCD] = csDataRow[ABDainoSfskRuisekiEntity.JUMINCD];
                    // 市町村コード
                    csDataNewRow[ABDainoDataEntity.SHICHOSONCD] = csDataRow[ABDainoSfskRuisekiEntity.SHICHOSONCD];
                    // 旧市町村コード
                    csDataNewRow[ABDainoDataEntity.KYUSHICHOSONCD] = csDataRow[ABDainoSfskRuisekiEntity.KYUSHICHOSONCD];
                    // 業務コード
                    csDataNewRow[ABDainoDataEntity.GYOMUCD] = csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD];

                    // 業務コードマスタより取得する
                    strGyomuCD = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD]);
                    csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD);

                    if (csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows.Count == 0)
                    {
                        // 業務名称
                        csDataNewRow[ABHiDainoDataEntity.GYOMUMEISHO] = string.Empty;
                        // 業務名称略
                        csDataNewRow[ABHiDainoDataEntity.GYOMUMEISHORYAKU] = string.Empty;
                    }
                    else
                    {
                        // 業務名称
                        csDataNewRow[ABHiDainoDataEntity.GYOMUMEISHO] = csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows[0](URGyomuCDMstEntity.GYOMUMEI);
                        // 業務名称略
                        csDataNewRow[ABHiDainoDataEntity.GYOMUMEISHORYAKU] = csGyomuCDMstEntity.Tables[URGyomuCDMstEntity.TABLE_NAME].Rows[0](URGyomuCDMstEntity.GYOMURYAKUSHO);
                    }

                    // 業務内種別コード
                    csDataNewRow[ABDainoDataEntity.GYOMUNAISHUCD] = csDataRow[ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD];
                    // 代納住民コード
                    csDataNewRow[ABDainoDataEntity.DAINOJUMINCD] = csDataRow[ABDainoSfskRuisekiEntity.DAINOJUMINCD];
                    // 開始年月
                    csDataNewRow[ABDainoDataEntity.STYMD] = csDataRow[ABDainoSfskRuisekiEntity.STYMD];
                    // 終了年月
                    csDataNewRow[ABDainoDataEntity.EDYMD] = csDataRow[ABDainoSfskRuisekiEntity.EDYMD];

                    // 表示用開始年月
                    cfDate.p_strDateValue = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.STYMD]);
                    cfDate.p_enEraType = UFEraType.KanjiRyaku;
                    cfDate.p_enDateSeparator = UFDateSeparator.Period;
                    csDataNewRow[ABDainoDataEntity.DISP_STYMD] = cfDate.p_strWarekiYMD;

                    // 表示用終了年月（999999の時は、非表示）
                    if ((UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.EDYMD]) ?? "") == ALL9_YMD)
                    {
                        csDataNewRow[ABDainoDataEntity.DISP_EDYMD] = string.Empty;
                    }
                    else
                    {
                        cfDate.p_strDateValue = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.EDYMD]);
                        csDataNewRow[ABDainoDataEntity.DISP_EDYMD] = cfDate.p_strWarekiYMD;
                    }

                    // 代納区分
                    csDataNewRow[ABDainoDataEntity.DAINOKB] = csDataRow[ABDainoSfskRuisekiEntity.DAINOKB];
                    // 代納区分名称
                    strDainoKB = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.DAINOKB]);
                    csDainoKankeiDataSet = cDainoKankeiB.GetDainoKBHoshu(strDainoKB);
                    intRowCount = csDainoKankeiDataSet.Tables[ABDainoKankeiCDMSTEntity.TABLE_NAME].Rows.Count;
                    if (!(intRowCount == 0))
                    {
                        csDataNewRow[ABDainoDataEntity.DAINOKBMEISHO] = (string)csDainoKankeiDataSet.Tables[ABDainoKankeiCDMSTEntity.TABLE_NAME].Rows[0](ABDainoKankeiCDMSTEntity.DAINOKBMEISHO);
                        csDataNewRow[ABDainoDataEntity.DAINOKBRYAKUMEI] = (string)csDainoKankeiDataSet.Tables[ABDainoKankeiCDMSTEntity.TABLE_NAME].Rows[0](ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI);
                    }

                    // 宛名取得パラメータインスタンス化
                    cAtenaGetPara1X = new ABAtenaGetPara1XClass();

                    // 宛名抽出キーセット
                    cAtenaGetPara1X.p_strJuminCD = UFVBAPI.ToString(csDataRow[ABDainoSfskRuisekiEntity.DAINOJUMINCD]);
                    cAtenaGetPara1X.p_strJushoHenshu1 = JUSHOHENSHU1_PARA_ONE;
                    cAtenaGetPara1X.p_blnSakujoFG = true;
                    cAtenaGetPara1X.p_strDaihyoShaKB = GET_HONNINDATA;       // *本人データ取得
                                                                             // 個人番号取得パラメータを設定
                    cAtenaGetPara1X.p_strMyNumberKB = ABConstClass.MYNUMBER.MYNUMBERKB.ON;

                    try
                    {
                        // 「宛名取得Ｂ」クラスの「宛名取得２」メソッドを実行
                        csAtenaDataSet = cAtenaGetB.AtenaGet2(cAtenaGetPara1X);

                        intRowCount = csAtenaDataSet.Tables[ABAtenaEntity.TABLE_NAME].Rows.Count;
                        if (!(intRowCount == 1))
                        {
                            // エラークラスのインスタンス化
                            cfErrorClass = new UFErrorClass(m_cfControlData.m_strBusinessId);
                            // エラー定義を取得
                            objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003078);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        }

                        // 「宛名編集Ｂ」クラスの「宛名編集」メソッドを実行する
                        csAtena1Entity = cAtenaHenshuB.AtenaHenshu(cAtenaGetPara1X, csAtenaDataSet);

                        csAtenaRow = csAtenaDataSet.Tables[ABAtenaEntity.TABLE_NAME].Rows[0];

                        // 住民名称取得キーセット
                        strDataKB = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.ATENADATAKB]);
                        strDataShu = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.ATENADATASHU]);
                        // 住民名称取得メゾット実行
                        cJuminShubetsuB.GetJuminshubetsu(strDataKB, strDataShu);
                        // 住民種別名称
                        csDataNewRow[ABDainoDataEntity.JUMINSHUMEISHO] = cJuminShubetsuB.p_strHenshuShubetsu;

                        // カナ名
                        strMeisho = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.KANAMEISHO2]);
                        if (string.IsNullOrEmpty(strMeisho))
                        {
                            csDataNewRow[ABDainoDataEntity.KANASHIMEI] = csAtenaRow[ABAtenaEntity.KANAMEISHO1];
                        }
                        // ### 法人の時はカナ名称１とカナ名称２を半角スペースでくっつける
                        else if ((strDataKB ?? "") == DATAKB_HOJIN)
                        {
                            // 文字列を結合した場合，MaxLengthを超えないように切り詰め
                            strKanaShimei = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.KANAMEISHO1]) + " " + UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.KANAMEISHO2]);
                            if (strKanaShimei.RLength() > csDataNewRow.Table.Columns[ABDainoDataEntity.KANASHIMEI].MaxLength)
                            {
                                csDataNewRow[ABDainoDataEntity.KANASHIMEI] = strKanaShimei.RSubstring(0, csDataNewRow.Table.Columns[ABDainoDataEntity.KANASHIMEI].MaxLength);
                            }
                            else
                            {
                                csDataNewRow[ABDainoDataEntity.KANASHIMEI] = strKanaShimei;
                            }
                        }
                        else if (UFVBAPI.ToString(strDataShu[0]) == DATASHU_FRN)
                        {
                            // ### 外国人の時はカナ名称１
                            csDataNewRow[ABDainoDataEntity.KANASHIMEI] = csAtenaRow[ABAtenaEntity.KANAMEISHO1];
                        }
                        else
                        {
                            csDataNewRow[ABDainoDataEntity.KANASHIMEI] = csAtenaRow[ABAtenaEntity.KANAMEISHO2];
                        }

                        strKanjiShimei = (string)csAtena1Entity.Tables[ABAtena1Entity.TABLE_NAME].Rows[0](ABAtena1Entity.HENSHUKANJISHIMEI);
                        if (csDataNewRow.Table.Columns[ABDainoDataEntity.KANJISHIMEI].MaxLength < strKanjiShimei.RLength())
                        {
                            csDataNewRow[ABDainoDataEntity.KANJISHIMEI] = strKanjiShimei.RSubstring(0, csDataNewRow.Table.Columns[ABDainoDataEntity.KANJISHIMEI].MaxLength);
                        }
                        else
                        {
                            csDataNewRow[ABDainoDataEntity.KANJISHIMEI] = strKanjiShimei;
                        }

                        // 管内管外名称キーセット
                        strKannaiKangaiCD = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.KANNAIKANGAIKB]);
                        // 管内管外名称取得メゾット実行
                        strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD);
                        // 管内管外名称
                        csDataNewRow[ABDainoDataEntity.KANNAIKANGAIMEISHO] = strKannaiKangaiMeisho;
                        // 郵便番号
                        csDataNewRow[ABDainoDataEntity.YUBINNO] = csAtenaRow[ABAtenaEntity.YUBINNO];
                        // 住所コード
                        csDataNewRow[ABDainoDataEntity.JUSHOCD] = csAtenaRow[ABAtenaEntity.JUSHOCD];
                        // 住所名
                        csDataNewRow[ABDainoDataEntity.JUSHO] = csAtenaRow[ABAtenaEntity.JUSHO];
                        // 番地コード１
                        csDataNewRow[ABDainoDataEntity.BANCHICD1] = csAtenaRow[ABAtenaEntity.BANCHICD1];
                        // 番地コード２
                        csDataNewRow[ABDainoDataEntity.BANCHICD2] = csAtenaRow[ABAtenaEntity.BANCHICD2];
                        // 番地コード３
                        csDataNewRow[ABDainoDataEntity.BANCHICD3] = csAtenaRow[ABAtenaEntity.BANCHICD3];
                        // 番地
                        csDataNewRow[ABDainoDataEntity.BANCHI] = csAtenaRow[ABAtenaEntity.BANCHI];
                        // 方書フラグ
                        csDataNewRow[ABDainoDataEntity.KATAGAKIFG] = csAtenaRow[ABAtenaEntity.KATAGAKIFG];
                        // 方書コード
                        csDataNewRow[ABDainoDataEntity.KATAGAKICD] = csAtenaRow[ABAtenaEntity.KATAGAKICD];
                        // 方書
                        csDataNewRow[ABDainoDataEntity.KATAGAKI] = csAtenaRow[ABAtenaEntity.KATAGAKI];
                        // 連絡先１
                        csDataNewRow[ABDainoDataEntity.RENRAKUSAKI1] = csAtenaRow[ABAtenaEntity.RENRAKUSAKI1];
                        // 連絡先２
                        csDataNewRow[ABDainoDataEntity.RENRAKUSAKI2] = csAtenaRow[ABAtenaEntity.RENRAKUSAKI2];
                        // 行政区コード
                        csDataNewRow[ABDainoDataEntity.GYOSEIKUCD] = csAtenaRow[ABAtenaEntity.GYOSEIKUCD];
                        // 行政区名
                        csDataNewRow[ABDainoDataEntity.GYOSEIKUMEI] = csAtenaRow[ABAtenaEntity.GYOSEIKUMEI];
                        // 地区コード１
                        csDataNewRow[ABDainoDataEntity.CHIKUCD1] = csAtenaRow[ABAtenaEntity.CHIKUCD1];
                        // 地区名１
                        csDataNewRow[ABDainoDataEntity.CHIKUMEI1] = csAtenaRow[ABAtenaEntity.CHIKUMEI1];
                        // 地区コード２
                        csDataNewRow[ABDainoDataEntity.CHIKUCD2] = csAtenaRow[ABAtenaEntity.CHIKUCD2];
                        // 地区名２
                        csDataNewRow[ABDainoDataEntity.CHIKUMEI2] = csAtenaRow[ABAtenaEntity.CHIKUMEI2];
                        // 地区コード３
                        csDataNewRow[ABDainoDataEntity.CHIKUCD3] = csAtenaRow[ABAtenaEntity.CHIKUCD3];
                        // 地区名３
                        csDataNewRow[ABDainoDataEntity.CHIKUMEI3] = csAtenaRow[ABAtenaEntity.CHIKUMEI3];
                        // 郵便番号
                        strYubinNO = UFVBAPI.ToString(csAtenaRow[ABAtenaEntity.YUBINNO]).Trim();
                        if (3 < strYubinNO.RLength())
                        {
                            csDataNewRow[ABDainoDataEntity.DISP_YUBINNO] = strYubinNO.RSubstring(0, 3) + "-" + strYubinNO.RSubstring(3);
                        }
                        else
                        {
                            csDataNewRow[ABDainoDataEntity.DISP_YUBINNO] = strYubinNO;
                        }
                        // 表示用編集住所
                        csDataNewRow[ABDainoDataEntity.DISP_HENSHUJUSHO] = csAtena1Entity.Tables[ABAtena1Entity.TABLE_NAME].Rows[0](ABAtena1Entity.HENSHUJUSHO);
                        csDataNewRow[ABDainoDataEntity.KOSHINUSER] = csDataRow[ABAtenaEntity.KOSHINUSER];
                        csDataNewRow[ABDainoDataEntity.MYNUMBER] = csAtenaRow[ABMyNumberEntity.MYNUMBER];
                        csDataNewRow[ABDainoDataEntity.ATENADATAKB] = csAtenaRow[ABAtenaEntity.ATENADATAKB];

                        // 備考マスタを取得
                        csBikoDataSet = cABBikoB.SelectByKey(ABBikoEntity.DEFAULT.BIKOKBN.DAINO, csDataRow[ABDainoSfskRuisekiEntity.JUMINCD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.GYOMUCD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD].ToString(), csDataRow[ABDainoSfskRuisekiEntity.TOROKURENBAN].ToString(), csDataRow[ABDainoSfskRuisekiEntity.RRKNO].ToString(), blnSakujoFG);

                        if (csBikoDataSet is not null && 0 < csBikoDataSet.Tables[ABBikoEntity.TABLE_NAME].Rows.Count)
                        {
                            csDataNewRow[ABDainoDataEntity.BIKO] = csBikoDataSet.Tables[ABBikoEntity.TABLE_NAME].Rows[0](ABBikoEntity.BIKO);
                        }
                        else
                        {
                            csDataNewRow[ABDainoDataEntity.BIKO] = string.Empty;
                        }

                        csDataNewRow[ABDainoDataEntity.CHECK] = false;
                        csDataNewRow[ABDainoDataEntity.JOTAI] = ABDainoSfskShoriMode.Empty.GetHashCode().ToString();
                        csDataNewRow[ABDainoDataEntity.DISP_JOTAI] = string.Empty;
                        csDataNewRow[ABDainoDataEntity.SEIGYOKB] = string.Empty;

                        csDataNewRow[ABDainoDataEntity.TOROKURENBAN] = csDataRow[ABDainoSfskRuisekiEntity.TOROKURENBAN];     // 登録連番
                        csDataNewRow[ABDainoDataEntity.RRKNO] = csDataRow[ABDainoSfskRuisekiEntity.RRKNO];                   // 履歴番号
                        csDataNewRow[ABDainoDataEntity.SHIKUCHOSONCD] = string.Empty;                                        // 市区町村コート
                        csDataNewRow[ABDainoDataEntity.MACHIAZACD] = string.Empty;                                           // 町字コード
                        csDataNewRow[ABDainoDataEntity.TODOFUKEN] = string.Empty;                                            // 都道府県

                        csDataNewRow[ABDainoDataEntity.SHORINICHIJI] = csDataRow[ABDainoSfskRuisekiEntity.SHORINICHIJI];     // 処理日時
                        csDataNewRow[ABDainoDataEntity.ZENGOKB] = csDataRow[ABDainoSfskRuisekiEntity.ZENGOKB];               // 前後区分
                    }
                    catch
                    {
                        // そのままスローする
                        throw;
                    }


                    // 削除フラグ
                    csDataNewRow[ABDainoDataEntity.SAKUJOFG] = csDataRow[ABDainoSfskRuisekiEntity.SAKUJOFG];

                    // 更新カウンタ
                    csDataNewRow[ABDainoDataEntity.KOSHINCOUNTER] = csDataRow[ABDainoSfskRuisekiEntity.KOSHINCOUNTER];

                    csReturnDataset.Tables[ABDainoDataEntity.TABLE_NAME].Rows.Add(csDataNewRow);

                }
                csReturnDataset.AcceptChanges();
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            return csReturnDataset;

        }
        #endregion

        #region 代納送付先累積履歴データカラム作成

        // ************************************************************************************************
        // * メソッド名      データカラム作成
        // * 
        // * 構文            Private Function CreateColumnsABSfskRirekiData() As DataTable
        // * 
        // * 機能　　        送付先履歴情報セッションのカラム定義を作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataTable() 代納履歴情報テーブル
        // ************************************************************************************************
        private DataTable CreateColumnsABSfskRirekiData()
        {
            const string THIS_METHOD_NAME = "CreateColumnsABSfskRirekiData";
            DataTable csDataTable;
            DataColumn csDataColumn;
            var csDataPrimaryKey = new DataColumn[9];               // 主キー

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 送付先情報カラム定義
                csDataTable = new DataTable();
                csDataTable.TableName = ABSfskDataEntity.TABLE_NAME;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.AllowDBNull = false;
                csDataColumn.MaxLength = 15;
                csDataPrimaryKey[0] = csDataColumn;              // 主キー①　住民コード
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KYUSHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn.AllowDBNull = false;
                csDataPrimaryKey[1] = csDataColumn;              // 主キー②　業務コード
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHORYAKU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUNAISHUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn.AllowDBNull = false;
                csDataPrimaryKey[2] = csDataColumn;              // 主キー③　業務内種コード
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.STYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn.AllowDBNull = false;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.EDYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn.AllowDBNull = false;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKDATAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAiKANGAIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAIKANGAIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANAMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;        // 60
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANJIMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;        // 40
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKYUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKZJUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKBANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 20
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SAKUJOFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINCOUNTER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                // 更新ユーザー
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINUSER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 32;

                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_STYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_EDYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;

                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BIKO, typeof(string));
                csDataColumn.DefaultValue = string.Empty;

                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.CHECK, typeof(string));
                csDataColumn.DefaultValue = bool.FalseString;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JOTAI, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_JOTAI, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOKB, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_DAINOKB, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOJUMINCD, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataPrimaryKey[3] = csDataColumn;              // 主キー④　代納住民コード
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SEIGYOKB, typeof(string));
                csDataColumn.DefaultValue = string.Empty;

                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TOROKURENBAN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataPrimaryKey[4] = csDataColumn;              // 主キー⑤　登録連番
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.RRKNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataPrimaryKey[5] = csDataColumn;              // 主キー⑥　履歴番号
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSONCD, typeof(string));
                csDataColumn.DefaultValue = 6;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZACD, typeof(string));
                csDataColumn.DefaultValue = 7;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TODOFUKEN, typeof(string));
                csDataColumn.DefaultValue = 16;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSON, typeof(string));
                csDataColumn.DefaultValue = 48;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZA, typeof(string));
                csDataColumn.DefaultValue = 480;
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHORINICHIJI, typeof(string));
                csDataColumn.DefaultValue = 17;
                csDataPrimaryKey[6] = csDataColumn;              // 主キー⑦　処理日時
                csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.ZENGOKB, typeof(string));
                csDataColumn.DefaultValue = 1;
                csDataPrimaryKey[7] = csDataColumn;              // 主キー⑧　前後区分

                csDataTable.PrimaryKey = csDataPrimaryKey;       // 主キー

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

            return csDataTable;

        }

        // ************************************************************************************************
        // * メソッド名      データカラム作成
        // * 
        // * 構文            Private Function CreateColumnsABDainoRirekiData() As DataTable
        // * 
        // * 機能　　        代納履歴情報セッションのカラム定義を作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataTable() 代納履歴情報テーブル
        // ************************************************************************************************
        private DataTable CreateColumnsABDainoRirekiData()
        {
            const string THIS_METHOD_NAME = "CreateColumnsABDainoRirekiData";
            DataTable csDataTable;
            DataColumn csDataColumn;
            var csDataPrimaryKey = new DataColumn[9];               // 主キー

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 代納情報カラム定義
                csDataTable = new DataTable();
                csDataTable.TableName = ABDainoDataEntity.TABLE_NAME;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.AllowDBNull = false;
                csDataColumn.MaxLength = 15;
                csDataPrimaryKey[0] = csDataColumn;              // 主キー①　住民コード
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KYUSHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn.AllowDBNull = false;
                csDataPrimaryKey[1] = csDataColumn;              // 主キー②　業務コード
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHORYAKU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUNAISHUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn.AllowDBNull = false;
                csDataPrimaryKey[2] = csDataColumn;              // 主キー③　業務内種コード
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOJUMINCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn.AllowDBNull = false;
                csDataPrimaryKey[3] = csDataColumn;              // 主キー④　代納住民コード
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.STYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.EDYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBRYAKUMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINSHUMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANASHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANJISHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANNAIKANGAIMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKIFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SAKUJOFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINCOUNTER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_STYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_EDYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_HENSHUJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 160;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINUSER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 32;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MYNUMBER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ATENADATAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BIKO, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHECK, typeof(string));
                csDataColumn.DefaultValue = bool.FalseString;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JOTAI, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_JOTAI, typeof(string));
                csDataColumn.DefaultValue = string.Empty;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SEIGYOKB, typeof(string));
                csDataColumn.DefaultValue = string.Empty;

                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SFSKZJUSHOCD, typeof(string));
                csDataColumn.DefaultValue = 13;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TOROKURENBAN, typeof(string));
                csDataColumn.DefaultValue = 10;
                csDataPrimaryKey[4] = csDataColumn;              // 主キー⑤　登録連番
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RRKNO, typeof(string));
                csDataColumn.DefaultValue = 10;
                csDataPrimaryKey[5] = csDataColumn;              // 主キー⑥　履歴番号
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHIKUCHOSONCD, typeof(string));
                csDataColumn.DefaultValue = 10;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MACHIAZACD, typeof(string));
                csDataColumn.DefaultValue = 10;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TODOFUKEN, typeof(string));
                csDataColumn.DefaultValue = 10;
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHORINICHIJI, typeof(string));
                csDataColumn.DefaultValue = 17;
                csDataPrimaryKey[6] = csDataColumn;              // 主キー⑦　処理日時
                csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ZENGOKB, typeof(string));
                csDataColumn.DefaultValue = 1;
                csDataPrimaryKey[7] = csDataColumn;              // 主キー⑧　前後区分

                csDataTable.PrimaryKey = csDataPrimaryKey;       // 主キー

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

            return csDataTable;

        }
        #endregion
        // *履歴番号 000003 2023/10/25 追加終了
        #endregion

    }
}
