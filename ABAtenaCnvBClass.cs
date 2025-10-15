// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        レプリカＤＢデータセット作成(ABAtenaCnvBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2004/02/12　吉澤　行宣
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/03/08  000001      被代納人の情報全件取得処理を追加
// *                      　 (ほぼ全体の構成を修正したので修正箇所は示さない)
// * 2004/04/05  000002      本店コード追加に伴う修正
// * 2004/11/05  000003      速度向上改修：① USSCITYINFOクラスインスタンス位置を変更する。
// *                                       ② 業務情報のテーブルをメンバに変更する。
// * 2005/02/06  000004      ワークフロー呼び出し処理の修正（レプリカデータ作成処理をバッチへ移す）
// * 2005/10/13  000005      上田市ホスト連携（ワークフロー）処理を追加(マルゴ村山)
// * 2008/05/14  000006      上田市介護個別ホスト連携（ワークフロー）処理を追加（比嘉）
// * 2010/04/16  000007      VS2008対応（比嘉）
// * 2022/12/16  000008    【AB-8010】住民コード世帯コード15桁対応(下村)
// ************************************************************************************************
using System;
using System.Collections;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAtenaCnvBClass
    {

        // **
        // * クラスID定義
        // * 
        private const string THIS_CLASS_NAME = "ABAtenaCnvBClass";

        // **
        // * メンバ変数
        // *  
        private UFControlData m_cfControlData;                        // コントロールデータ
        private UFConfigDataClass m_cfConfigData;                     // 環境情報データクラス
        private UFLogClass m_cfLog;                                   // ログ出力クラス
        private UFRdbClass m_cfRdbClass;                              // RDBクラス
        private UFRdbClass m_cfSFSKRdbClass;                          // RDBクラス
        private UFRdbClass m_cfDainoRdbClass;                         // RDBクラス
        private UFErrorClass m_cfErrorClass;                          // エラー処理クラス
        private UFDataReaderClass m_cReader;                          // データリーダクラス

        private ArrayList m_aryABAtena;                               // 宛名抽出リスト配列
        private ArrayList m_aryABSfsk;                                // 送付先抽出リスト配列
        private ArrayList m_aryABDaino;                               // 代納抽出リスト配列
        private string m_strSQL;                                      // 宛名本人ＳＱＬ文
        private string m_strSFSKSQL;                                  // 送付先ＳＱＬ文
        private string m_strDAINOSQL;                                 // 代納ＳＱＬ文
                                                                      // *履歴番号 000001 2004/03/08 追加開始
        private string m_strHIDAINOSQL;                               // 被代納ＳＱＬ文
                                                                      // (暫定処理のため"50"の数字に意味はない)
        private string[] m_strHidainoJuminCD = new string[51];
        private int m_intHiDaiCnt = 0;                            // 被代納人カウンタ
                                                                  // *履歴番号 000001 2004/03/08 追加終了
                                                                  // *履歴番号 000003 2004/11/05 追加開始
        private string m_strCityCD;                                   // 市町村CD
        private DataTable m_csGyomuTable;                           // 業務情報テーブル
                                                                    // *履歴番号 000003 2004/11/05 追加終了

        private string m_JuminCDA;                                    // 宛名本人用住民コード
        private string m_JuminCDS;                                    // 宛名送付先用住民コード
        private string m_JuminCDD;                                    // 宛名代納用住民コード
        private int m_intRecCnt;                                  // 連番のカウタ
        private string m_strNen;                                        // 作成日時

        public const string STR_A = "A";
        public const string STR_B = "B";
        public const string STR_C = "C";
        public const string STR_D = "D";
        // *履歴番号 000002 2004/04/05 追加開始
        public const string STR_E = "E";
        public const string STR_E_ = "E_";
        // *履歴番号 000002 2004/04/05 追加終了
        public const string STR_A_ = "A_";
        public const string STR_B_ = "B_";
        public const string STR_C_ = "C_";
        public const string STR_D_ = "D_";
        private const string SEPARATOR = ",";                         // セパレータ
        public const string ATENA = "宛名";                           // ワークフロー名(宛名)
        public const string KOKUHO = "国保個別";                      // ワークフロー名(国保)
                                                                  // *履歴番号 000005 2005/10/17 追加開始
        public const string JITE = "児手個別";                        // ワークフロー名(児手)
                                                                  // *履歴番号 000005 2005/10/17 追加終了
                                                                  // *履歴番号 000006 2008/05/14 追加開始
        public const string KAIGO = "介護個別";                       // ワークフロー名(介護)
                                                                  // *履歴番号 000006 2008/05/14 追加終了

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass
        // * 　　                          ByVal csUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaCnvBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            USSCityInfoClass cuCityInfo;                      // 市町村情報クラス
                                                              // * corresponds to VS2008 Start 2010/04/16 000007
                                                              // Dim strCityCD As String                                 '市町村コード
                                                              // * corresponds to VS2008 End 2010/04/16 000007

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // *履歴番号 000003 2004/11/05 追加開始
            // '''インスタンス化
            cuCityInfo = new USSCityInfoClass();
            // 市町村情報の取得
            cuCityInfo.GetCityInfo(m_cfControlData);
            // 市町村ｺｰﾄﾞの取得
            m_strCityCD = cuCityInfo.p_strShichosonCD(0);
            // *履歴番号 000003 2004/11/05 追加終了

            // ログ出力クラスのインスタンス化
            m_cfLog = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

        }
        #endregion

        // *履歴番号 000004 2005/03/22 削除開始
        #region 宛名追加処理
        // '************************************************************************************************
        // '* メソッド名     宛名追加処理
        // '* 
        // '* 構文           Public Function AtenaCnv(ByVal cABToshoProperty() As ABToshoProperty,  
        // '* 　　                                      ByVal WORK_FLOW_NAME As String,
        // '*                                           ByVal DATA_NAME As String) As DataSet
        // '* 
        // '* 機能　　       個人データの追加を行なう。
        // '* 
        // '* 引数           cABToshoProperty()
        // '*                WORK_FLOW_NAME
        // '*                DATA_NAME
        // '* 
        // '* 戻り値         なし
        // '************************************************************************************************
        // Public Function AtenaCnv(ByVal cABToshoProperty() As ABToshoProperty, ByVal WORK_FLOW_NAME As String, ByVal DATA_NAME As String) As DataSet
        // Const THIS_METHOD_NAME As String = "AtenaCnv"
        // Dim csToshoEntity As New DataSet()                      '当初用データセット
        // Dim csToshoRow As DataRow                               '当初データロウ
        // Dim csToshoTable As DataTable                           '当初データテーブル
        // Dim intCnt As Integer
        // '*履歴番号 000003 2004/11/05 削除開始
        // '''Dim cuCityInfo As USSCityInfoClass                      '市町村情報クラス
        // '''Dim strCityCD As String                                 '市町村コード
        // '*履歴番号 000003 2004/11/05 削除終了
        // '*履歴番号 000001 2004/03/08 追加開始
        // Dim csHiDainoEntity As DataSet                          '被代納データセット
        // Dim csHiDainoRow As DataRow                             '被代納データロウ
        // Dim intHiDainoCnt As Integer
        // '*履歴番号 000001 2004/03/08 追加終了

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 作成日時(14桁)
        // m_strNen = DateTime.Now.ToString("yyyyMMddHHmmss")

        // ' テーブルセットの取得
        // csToshoTable = Me.CreateColumnsData()
        // csToshoTable.TableName = ABToshoTable.TABLE_NAME
        // ' データセットにテーブルセットの追加
        // csToshoEntity.Tables.Add(csToshoTable)

        // '物理削除とそれ以外の場合分け
        // If Not (cABToshoProperty(0).p_strKoshinKB = "D") Then

        // '被代納人カウンタを"0"にする
        // m_intHiDaiCnt = 0

        // 'プロバティがなくなるまで繰り返す
        // For intCnt = 0 To cABToshoProperty.Length - 1

        // '**
        // '*本人情報の全情報取得処理
        // '*
        // csToshoEntity = Me.AtenaHenshu(cABToshoProperty(intCnt).p_strJuminCD, cABToshoProperty(intCnt).p_strRonSakuFG, cABToshoProperty(intCnt).p_strKoshinKB, csToshoEntity)


        // '**
        // '*被代納人住民コード編集
        // '*
        // ' 被代納SQL実行
        // csHiDainoEntity = m_cfRdbClass.GetDataSet(m_strHIDAINOSQL, ABDainoEntity.TABLE_NAME)
        // '代納データの取得
        // For Each csHiDainoRow In csHiDainoEntity.Tables(ABDainoEntity.TABLE_NAME).Rows
        // m_strHidainoJuminCD(m_intHiDaiCnt) = CType(csHiDainoRow.Item(ABDainoEntity.JUMINCD), String)
        // '被代納人の数をカウント
        // m_intHiDaiCnt += 1
        // Next

        // Next

        // '**
        // '*被代納人の全情報取得処理
        // '*
        // For intHiDainoCnt = 0 To m_intHiDaiCnt - 1
        // '全件取得処理
        // csToshoEntity = Me.AtenaHenshu(m_strHidainoJuminCD(intHiDainoCnt), cABToshoProperty(0).p_strRonSakuFG, cABToshoProperty(0).p_strKoshinKB, csToshoEntity)
        // Next

        // Else

        // '**
        // '*物理削除の編集処理
        // '*
        // '*履歴番号 000003 2004/11/05 削除開始
        // ''''インスタンス化
        // '''cuCityInfo = New USSCityInfoClass()
        // ''''市町村情報の取得
        // '''cuCityInfo.GetCityInfo(m_cfControlData)
        // ''''市町村ｺｰﾄﾞの取得
        // '''strCityCD = cuCityInfo.p_strShichosonCD(0)
        // '*履歴番号 000003 2004/11/05 削除終了

        // '連番のカウントをとる
        // m_intRecCnt += 1
        // '新しいRowを追加
        // csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow

        // ' 市町村ｺｰﾄﾞ(6桁)
        // '*履歴番号 000003 2004/11/05 修正開始
        // '''csToshoRow.Item(ABToshoTable.SHICHOSONCD) = strCityCD
        // csToshoRow.Item(ABToshoTable.SHICHOSONCD) = m_strCityCD
        // '*履歴番号 000003 2004/11/05 修正終了
        // ' 識別ID(4桁)
        // csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
        // ' 作成日時(14桁)
        // csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
        // ' 最終行区分(1桁)
        // csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
        // ' 連番(7桁)
        // csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
        // ' 住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.JUMIN_CD) = cABToshoProperty(intCnt).p_strJuminCD.Substring(4, 8)
        // ' 更新区分(1桁)
        // csToshoRow.Item(ABToshoTable.UPDATE_KBN) = cABToshoProperty(intCnt).p_strKoshinKB

        // '編集したRowをデータセットに追加
        // csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)

        // End If

        // '**
        // '*最終行の編集処理
        // '*
        // '連番のカウントをとる
        // m_intRecCnt += 1
        // '最終行の取得
        // csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow
        // csToshoRow = Me.ReflectLastData(csToshoRow)
        // '編集したRowをデータセットに追加
        // csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)

        // '**
        // '*ワークフロー送信処理呼び出し
        // '*
        // Me.WorkFlowExec(csToshoEntity, WORK_FLOW_NAME, DATA_NAME)

        // ' RDBアクセスログ出力
        // m_cfLog.RdbWrite(m_cfControlData, _
        // "【クラス名:" + Me.GetType.Name + "】" + _
        // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        // "【実行メソッド名:ExecuteSQL】" + _
        // "【SQL内容:" + m_strSQL + m_strSFSKSQL + m_strDAINOSQL + "】")

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoEntity

        // End Function
        #endregion

        #region 宛名追加処理(MAIN)
        // '************************************************************************************************
        // '* メソッド名     宛名追加処理(繰返し内)
        // '* 
        // '* 構文           Public Function AtenaHenshu(ByVal strJuminCD As String,   
        // '* 　　                                      ByVal strRonSakuFG As String,
        // '*                                           ByVal strUpdataKB As String,
        // '*                                           ByVal csToshoEntity As DataSet) As DataSet
        // '* 
        // '* 機能　　       全件のデータを取得する。
        // '* 
        // '* 引数           strJuminCD        
        // '*                strRonSakuFG
        // '*                strUpdataKB
        // '*              　csToshoEntity
        // '* 
        // '* 戻り値         DataSet
        // '************************************************************************************************
        // Public Function AtenaHenshu(ByVal strJuminCD As String, ByVal strRonSakuFG As String, ByVal strUpdataKB As String, ByVal csToshoEntity As DataSet) As DataSet
        // Const THIS_METHOD_NAME As String = "AtenaHenshu"
        // Dim csToshoRow As DataRow                               '当初データロウ
        // Dim csAtenaEntity As DataSet                            '本人宛名情報用データセット
        // Dim csDainoEntity As DataSet                            '本人宛名+代納人情報用データセット
        // Dim csSfskEntity As New DataSet()                       '本人宛名+送f先情報用データセット
        // Dim csAtenaRow As DataRow                               '本人宛名情報用データロウ
        // Dim csSfskRow As DataRow                                '本人宛名+送付先情報用データロウ
        // Dim csDainoRow As DataRow                               '本人宛名+代納人情報用データロウ
        // Dim strKey(1) As String                                 'キー
        // Dim intED As Integer = 1                                '枝番カウンタ
        // '*履歴番号 000003 2004/11/29 削除開始
        // '''''Dim csGyomuTable As DataTable
        // '*履歴番号 000003 2004/11/29 削除終了
        // Dim csGyomuRow As DataRow                               '業務データロウ
        // Dim csGERows As DataRow()                               '業務・枝版用データロウ

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 作成日時(14桁)
        // m_strNen = DateTime.Now.ToString("yyyyMMddHHmmss")

        // 'SQL作成
        // Me.CreateSQL(strJuminCD, strRonSakuFG)

        // '**
        // '*本人宛名情報編集
        // '*
        // '本人SQL実行
        // csAtenaEntity = m_cfRdbClass.GetDataSet(m_strSQL, ABAtenaEntity.TABLE_NAME)
        // ' 本人宛名データの取得
        // For Each csAtenaRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
        // '連番のカウントをとる
        // m_intRecCnt += 1
        // '新しいRowを追加
        // csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow()

        // ' 宛名本人のデータを一行読み込みセットする
        // csToshoRow = Me.ReflectAtenaData(csAtenaRow, csToshoRow, strUpdataKB)
        // '編集したRowをデータセットに追加
        // csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
        // Next

        // '枝番のカウンタを初期化
        // intED = 0
        // '業務コードのキーを初期化
        // strKey(0) = String.Empty
        // strKey(1) = String.Empty
        // '業務コード・枝番のテーブル作成
        // '*履歴番号 000003 2004/11/29 修正開始
        // '業務ＣＤ・枝版テーブルの作成
        // If m_csGyomuTable Is Nothing Then
        // m_csGyomuTable = Me.CreateClmGyomuData
        // End If
        // ''''''csGyomuTable = Me.CreateClmGyomuData
        // '*履歴番号 000003 2004/11/29 修正終了

        // '**
        // '*本人宛名・送付先情報編集
        // '*
        // ' 送付先SQL実行
        // csSfskEntity = m_cfRdbClass.GetDataSet(m_strSFSKSQL, ABSfskEntity.TABLE_NAME)
        // '送付先データの取得
        // ' データ編集 & 出力
        // For Each csSfskRow In csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows
        // '連番のカウントをとる
        // m_intRecCnt += 1
        // ''新しいRowを追加
        // csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow
        // ' 宛名本人・送付先を一行読み込みセットする
        // csToshoRow = Me.ReflectSofusakiData(csSfskRow, csToshoRow, strUpdataKB)
        // '枝番の編集
        // If Not (CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String) = String.Empty) Then
        // 'ブレイクキーの設定(後キー)
        // strKey(0) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
        // '前キーと後キーが同じだったら枝番カウンタに+1して枝番にデータを追加
        // If (strKey(0) = strKey(1)) Then
        // intED += 1
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // Else
        // '業務コード・枝番テーブルに新規ロウを作成
        // csGyomuRow = m_csGyomuTable.NewRow()
        // csGyomuRow.Item(ABToshoTable.GYOMU_CD) = strKey(1)
        // csGyomuRow.Item(ABToshoTable.EDABAN) = CStr(intED)
        // '業務コード・枝番テーブルにロウを追加
        // m_csGyomuTable.Rows.Add(csGyomuRow)

        // intED = 1
        // '枝番に一番目のデータ(001)
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)

        // End If
        // Else
        // intED = 1
        // '枝番に一番目のデータ(001)
        // ' 枝番(3桁)
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // End If
        // 'ブレイクキーの設定(前キー)
        // strKey(1) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
        // '編集したRowをデータセットに追加
        // csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
        // Next

        // '業務コード・枝番テーブルに新規ロウを作成
        // csGyomuRow = m_csGyomuTable.NewRow()
        // csGyomuRow.Item(ABToshoTable.GYOMU_CD) = strKey(1)
        // csGyomuRow.Item(ABToshoTable.EDABAN) = CStr(intED)
        // '業務コード・枝番テーブルにロウを追加
        // m_csGyomuTable.Rows.Add(csGyomuRow)

        // '枝番のカウンタを初期化
        // intED = 0
        // '業務コードのキーを初期化
        // strKey(0) = String.Empty
        // strKey(1) = String.Empty


        // '**
        // '*本人宛名・代納人宛名情報編集
        // '*
        // ' 代納SQL実行
        // csDainoEntity = m_cfRdbClass.GetDataSet(m_strDAINOSQL, ABDainoEntity.TABLE_NAME)

        // '代納データの取得
        // For Each csDainoRow In csDainoEntity.Tables(ABDainoEntity.TABLE_NAME).Rows
        // '連番のカウントをとる
        // m_intRecCnt += 1
        // '新しいRowを追加
        // csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow

        // ' 宛名本人・代納のデータを一行読み込みセットする
        // csToshoRow = Me.ReflectDainoData(csDainoRow, csToshoRow, strUpdataKB)

        // '枝番の編集
        // If Not (CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String) = String.Empty) Then
        // 'ブレイクキーの設定(後キー)
        // strKey(0) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
        // '前キーと後キーが同じだったら枝番カウンタに+1して枝番にデータを追加
        // If (strKey(0) = strKey(1)) Then
        // intED += 1
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // Else
        // If Not (m_csGyomuTable.Rows.Count = 0) Then

        // '業務コードをキーを検索条件として存在するロウを取得
        // csGERows = m_csGyomuTable.Select(ABToshoTable.GYOMU_CD + " = " + "'" + strKey(0) + "'")

        // '業務ＣＤ・枝番テーブルにデータが存在するかどうか
        // If Not (csGERows.Length = 0) Then
        // intED = CType(csGERows(0).Item(ABToshoTable.EDABAN), Integer) + 1
        // csToshoRow.Item(ABToshoTable.EDABAN) = CType(intED, String).PadLeft(3, "0"c)
        // Else
        // intED = 1
        // '枝番に一番目のデータ(001)
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // End If
        // Else
        // intED = 1
        // '枝番に一番目のデータ(001)
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // End If
        // End If
        // Else
        // intED = 1
        // '枝番に一番目のデータ(001)
        // ' 枝番(3桁)
        // csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
        // End If
        // 'ブレイクキーの設定(前キー)
        // strKey(1) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)

        // '編集したRowをデータセットに追加
        // csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
        // Next


        // ' RDBアクセスログ出力
        // m_cfLog.RdbWrite(m_cfControlData, _
        // "【クラス名:" + Me.GetType.Name + "】" + _
        // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        // "【実行メソッド名:ExecuteSQL】" + _
        // "【SQL内容:" + m_strSQL + m_strSFSKSQL + m_strDAINOSQL + "】")

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoEntity

        // End Function
        #endregion

        #region 宛名データ編集
        // '**
        // '*	メソッド名	ReflectAtenaData
        // '*	概要			宛名データの反映 (本人宛名情報)
        // '*	引数			csRow		　　: データ取得
        // '*			    	csToshoRow		: データ格納
        // '*				    strUpDateKB		: 更新区分
        // '*	戻り値		なし
        // '*
        // Private Function ReflectAtenaData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
        // Const THIS_METHOD_NAME As String = "ReflectAtenaData"
        // Dim strPrefixA As String = CType((STR_A_), String)
        // '*履歴番号 000002 2004/04/058 追加開始
        // Dim strPrefixE As String = CType((STR_E_), String)
        // '*履歴番号 000002 2004/04/058 追加終了

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 市町村ｺｰﾄﾞ(6桁)
        // csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
        // ' 識別ID(4桁)
        // csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
        // ' 作成日時(14桁)
        // csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
        // ' 最終行区分(1桁)
        // csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
        // ' 連番(7桁)
        // csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
        // ' 住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
        // ' 枝番(3桁)
        // csToshoRow.Item(ABToshoTable.EDABAN) = "001"
        // ' 世帯コード(8桁)(.NET12桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
        // Else
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
        // End If
        // ' データ区分(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
        // Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
        // ' 住民基本台帳番号(14桁)
        // csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
        // ' データ種別(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
        // Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
        // ' 検索用カナ（姓）(24桁)
        // csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
        // ' 検索用カナ（名）(16桁)
        // csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
        // ' カナ名称１(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
        // ' 漢字名称１(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
        // ' カナ名称２(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
        // ' 漢字名称２(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
        // '生年月日(8桁)
        // csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
        // ' 生和暦年月日(7桁)
        // csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
        // '性別コード(1桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
        // ' 性別(2桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
        // ' 続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
        // ' 続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
        // ' 第２続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
        // ' 第２続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
        // ' 共有代表者住民コード(8桁)
        // csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
        // ' 法人代表者名（漢字）(60桁)
        // csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
        // ' 産業分類コード(4桁)
        // csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正開始
        // If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // Else
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
        // End If
        // 'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正終了
        // ' 汎用区分１(1桁)
        // '(データ区分が"11""12"の時、カナ名称２がある時の判定)
        // If (strDataKB = "11" Or strDataKB = "12") Then
        // If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
        // End If
        // ' 法人形態(20桁)
        // csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
        // ' 個人法人区分(1桁)
        // csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
        // ' 他人数(4桁)
        // csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
        // ' 汎用区分２(1桁)
        // '(データ区分が"18""28"の時、転出確定住所・転出予定住所がある時の判定)
        // If strDataSB = "18" Or strDataSB = "28" Then
        // If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
        // ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // ' 管内管外区分(1桁)
        // csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
        // ' 郵便番号(7桁)
        // csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
        // ' 住所コード(11桁)
        // csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
        // ' 住所名(60桁)
        // csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
        // ' 番地コード１(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
        // ' 番地コード２(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
        // ' 番地コード３(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
        // ' 番地(40桁)
        // csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
        // ' 方書フラグ(1桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
        // ' 方書コード(4桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
        // ' 方書(60桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
        // ' 連絡先１(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
        // ' 連絡先２(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
        // ' 行政区コード(7桁)(.NET9桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
        // Else
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
        // End If
        // ' 行政区名(60桁)
        // csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
        // ' 地区コード１(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区名１(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
        // ' 地区コード２(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
        // End If
        // ' 地区名２(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
        // ' 地区コード３(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
        // End If
        // ' 地区名３(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
        // ' 登録異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
        // ' 登録事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
        // ' 削除異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
        // ' 削除事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
        // ' 最終履歴番号(4桁)
        // csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
        // ' 異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
        // ' 異動事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
        // ' 登録年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
        // ' 更新区分(1桁)
        // csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
        // ' ユーザID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
        // End If
        // ' 端末ID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
        // End If
        // ' タイムスタンプ(14桁)
        // csToshoRow.Item(ABToshoTable.UP_DATE) = ""
        // ' 論理ロックキー(6桁)
        // csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoRow

        // End Function
        #endregion

        #region 送付先データ編集
        // '**
        // '*	メソッド名	ReflectSofusakiData
        // '*	概要			送付先データの反映
        // '*	引数			csRow		　　: データ取得
        // '*				    csToshoRow		: データ格納
        // '*				    strUpDateKB		: 更新区分
        // '*	戻り値		なし
        // '*
        // Private Function ReflectSofusakiData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
        // Const THIS_METHOD_NAME As String = "ReflectSofusakiData"
        // Dim strPrefixA As String = CType((STR_A_), String)
        // Dim strPrefixB As String = CType((STR_B_), String)
        // '*履歴番号 000002 2004/04/058 追加開始
        // Dim strPrefixE As String = CType((STR_E_), String)
        // '*履歴番号 000002 2004/04/058 追加終了

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 市町村ｺｰﾄﾞ(6桁)
        // csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
        // ' 識別ID(4桁)
        // csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
        // ' 作成日時(14桁)
        // csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
        // ' 最終行区分(1桁)
        // csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
        // ' 連番(7桁)
        // csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
        // ' 住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
        // ' 枝番(3桁)
        // 'csToshoRow.Item(ABToshoTable.EDABAN) = ""
        // ' 世帯コード(8桁)(.NET12桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
        // Else
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
        // End If
        // ' データ区分(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
        // Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
        // ' 住民基本台帳番号(14桁)
        // csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
        // ' データ種別(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
        // Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
        // ' 検索用カナ（姓）(24桁)
        // csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
        // ' 検索用カナ（名）(16桁)
        // csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
        // ' カナ名称１(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
        // ' 漢字名称１(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
        // ' カナ名称２(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
        // ' 漢字名称２(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
        // '生年月日(8桁)
        // csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
        // ' 生和暦年月日(7桁)
        // csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
        // '性別コード(1桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
        // ' 性別(2桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
        // ' 続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
        // ' 続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
        // ' 第２続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
        // ' 第２続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
        // ' 共有代表者住民コード(8桁)
        // csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
        // ' 法人代表者名（漢字）(60桁)
        // csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
        // ' 産業分類コード(4桁)
        // csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正開始
        // If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // Else
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
        // End If
        // 'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正終了
        // ' 汎用区分１(1桁)
        // '(データ区分が"11""12"の時、カナ名称２がある時の判定)
        // If (strDataKB = "11" Or strDataKB = "12") Then
        // If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
        // End If
        // ' 法人形態(20桁)
        // csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
        // ' 個人法人区分(1桁)
        // csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
        // ' 他人数(4桁)
        // csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
        // ' 汎用区分２(1桁)
        // '(データ区分が"18""28"の時、転出確定住所・転出予定住所がある時の判定)
        // If strDataSB = "18" Or strDataSB = "28" Then
        // If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
        // ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // ' 管内管外区分(1桁)
        // csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
        // ' 郵便番号(7桁)
        // csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
        // ' 住所コード(11桁)
        // csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
        // ' 住所名(60桁)
        // csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
        // ' 番地コード１(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
        // ' 番地コード２(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
        // ' 番地コード３(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
        // ' 番地(40桁)
        // csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
        // ' 方書フラグ(1桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
        // ' 方書コード(4桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
        // ' 方書(60桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
        // ' 連絡先１(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
        // ' 連絡先２(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
        // ' 行政区コード(7桁)(.NET9桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
        // Else
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
        // End If
        // ' 行政区名(60桁)
        // csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
        // ' 地区コード１(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区名１(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
        // ' 地区コード２(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
        // End If
        // ' 地区名２(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
        // ' 地区コード３(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
        // End If
        // ' 地区名３(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
        // ' 登録異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
        // ' 登録事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
        // ' 削除異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
        // ' 削除事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
        // ' 最終履歴番号(4桁)
        // csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
        // ' 異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
        // ' 異動事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
        // ' 登録年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
        // ' 更新区分(1桁)
        // csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
        // ' ユーザID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
        // End If
        // ' 端末ID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
        // End If
        // ' タイムスタンプ(14桁)
        // csToshoRow.Item(ABToshoTable.UP_DATE) = ""
        // ' 論理ロックキー(6桁)
        // csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""


        // '住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.D_JUMIN_CD) = CType(csRow.Item(strPrefixB + ABSfskEntity.JUMINCD), String).Substring(4, 8)
        // ' 業務コード(2桁)
        // csToshoRow.Item(ABToshoTable.GYOMU_CD) = csRow.Item(strPrefixB + ABSfskEntity.GYOMUCD)
        // ' 開始年月日(6桁)
        // csToshoRow.Item(ABToshoTable.ST_YM) = csRow.Item(strPrefixB + ABSfskEntity.STYM)
        // ' 終了年月日(6桁)
        // csToshoRow.Item(ABToshoTable.ED_YM) = csRow.Item(strPrefixB + ABSfskEntity.EDYM)
        // ' 代納区分(2桁)
        // csToshoRow.Item(ABToshoTable.D_DAINO_KBN) = "40"
        // ' カナ名称１(60桁)
        // csToshoRow.Item(ABToshoTable.D_KANAMEISHO1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANAMEISHO)
        // ' 漢字名称１(80桁)
        // csToshoRow.Item(ABToshoTable.D_MEISHO1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANJIMEISHO)
        // '管内管外区分(1桁)
        // csToshoRow.Item(ABToshoTable.D_NAIGAI_KBN) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANNAIKANGAIKB)
        // ' 郵便番号(7桁)
        // csToshoRow.Item(ABToshoTable.D_YUBIN_NO) = csRow.Item(strPrefixB + ABSfskEntity.SFSKYUBINNO)
        // ' 住所コード(11桁)
        // csToshoRow.Item(ABToshoTable.D_JUSHO_CD) = csRow.Item(strPrefixB + ABSfskEntity.SFSKZJUSHOCD)
        // '住所(60桁)
        // csToshoRow.Item(ABToshoTable.D_JUSHO) = csRow.Item(strPrefixB + ABSfskEntity.SFSKJUSHO)
        // '番地(40桁)
        // csToshoRow.Item(ABToshoTable.D_BANCHI) = csRow.Item(strPrefixB + ABSfskEntity.SFSKBANCHI)
        // ' 方書(60桁)
        // csToshoRow.Item(ABToshoTable.D_KATAGAKI) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKATAGAKI)
        // ' 連絡先1(14桁)
        // csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKRENRAKUSAKI1)
        // ' 連絡先2(14桁)
        // csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKRENRAKUSAKI2)
        // ' 行政区コード(7桁)(.NET9桁)
        // If csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD) Is String.Empty Or _
        // CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Length <= 7 Then
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD)
        // ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = "       "
        // Else
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Substring(2, 7)
        // End If
        // ' 行政区名(60桁)
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU) = csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUMEI)
        // ' 地区コード１(6桁)(.NET8桁)
        // If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1) Is String.Empty Or _
        // CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1)
        // ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区１(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI1)
        // ' 地区コード２(6桁)(.NET8桁)
        // If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2) Is String.Empty Or _
        // CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2)
        // ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Substring(2, 6)
        // End If
        // ' 地区２(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI2)
        // ' 地区コード３(6桁)(.NET8桁)
        // If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3) Is String.Empty Or _
        // CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3)
        // ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Substring(2, 6)
        // End If
        // ' 地区３(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU3) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI3)

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoRow

        // End Function
        #endregion

        #region 代納データ編集
        // '**
        // '*	メソッド名	ReflectDainoData
        // '*	概要			宛名データの反映 (代納宛名情報)
        // '*	引数			csRow		　　: データ取得
        // '*				    csToshoRow		: データ格納
        // '*			    	strUpDateKB		: 更新区分
        // '*	戻り値		なし
        // '*
        // Private Function ReflectDainoData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
        // Const THIS_METHOD_NAME As String = "ReflectDainoData"
        // Dim strPrefixA As String = CType((STR_A_), String)
        // Dim strPrefixC As String = CType((STR_C_), String)
        // Dim strPrefixD As String = CType((STR_D_), String)
        // '*履歴番号 000002 2004/04/058 追加開始
        // Dim strPrefixE As String = CType((STR_E_), String)
        // '*履歴番号 000002 2004/04/058 追加終了

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 市町村ｺｰﾄﾞ(6桁)
        // csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
        // ' 識別ID(4桁)
        // csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
        // ' 作成日時(14桁)
        // csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
        // ' 最終行区分(1桁)
        // csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
        // ' 連番(7桁)
        // csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
        // ' 住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
        // ' 枝番(3桁)
        // 'csToshoRow.Item(ABToshoTable.EDABAN) = ""
        // ' 世帯コード(8桁)(.NET12桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
        // Else
        // csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
        // End If
        // ' データ区分(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
        // Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
        // ' 住民基本台帳番号(14桁)
        // csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
        // ' データ種別(2桁)
        // csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
        // Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
        // ' 検索用カナ（姓）(24桁)
        // csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
        // ' 検索用カナ（名）(16桁)
        // csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
        // ' カナ名称１(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
        // ' 漢字名称１(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
        // ' カナ名称２(60桁)
        // csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
        // ' 漢字名称２(80桁)
        // csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
        // '生年月日(8桁)
        // csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
        // ' 生和暦年月日(7桁)
        // csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
        // '性別コード(1桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
        // ' 性別(2桁)
        // csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
        // ' 続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
        // ' 続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
        // ' 第２続柄コード(8桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
        // ' 第２続柄(30桁)
        // csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
        // ' 共有代表者住民コード(8桁)
        // csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
        // ' 法人代表者名（漢字）(60桁)
        // csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
        // ' 産業分類コード(4桁)
        // csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正開始
        // If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // Else
        // ' 本店コード(8桁)
        // csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
        // End If
        // 'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
        // '*履歴番号 000002 2004/04/058 修正終了
        // ' 汎用区分１(1桁)
        // '(データ区分が"11""12"の時、カナ名称２がある時の判定)
        // If (strDataKB = "11" Or strDataKB = "12") Then
        // If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
        // End If
        // ' 法人形態(20桁)
        // csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
        // ' 個人法人区分(1桁)
        // csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
        // ' 他人数(4桁)
        // csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
        // ' 汎用区分２(1桁)
        // '(データ区分が"18""28"の時、転出確定住所・転出予定住所がある時の判定)
        // If strDataSB = "18" Or strDataSB = "28" Then
        // If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
        // ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
        // End If
        // ' 管内管外区分(1桁)
        // csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
        // ' 郵便番号(7桁)
        // csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
        // ' 住所コード(11桁)
        // csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
        // ' 住所名(60桁)
        // csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
        // ' 番地コード１(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
        // ' 番地コード２(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
        // ' 番地コード３(5桁)
        // csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
        // ' 番地(40桁)
        // csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
        // ' 方書フラグ(1桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
        // ' 方書コード(4桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
        // ' 方書(60桁)
        // csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
        // ' 連絡先１(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
        // ' 連絡先２(14桁)
        // csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
        // ' 行政区コード(7桁)(.NET9桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
        // Else
        // csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
        // End If
        // ' 行政区名(60桁)
        // csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
        // ' 地区コード１(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区名１(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
        // ' 地区コード２(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
        // End If
        // ' 地区名２(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
        // ' 地区コード３(6桁)(.NET8桁)
        // If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
        // CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
        // ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
        // End If
        // ' 地区名３(60桁)
        // csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
        // ' 登録異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
        // ' 登録事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
        // ' 削除異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
        // ' 削除事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
        // ' 最終履歴番号(4桁)
        // csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
        // ' 異動年月日(8桁)
        // csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
        // ' 異動事由コード(2桁)
        // csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
        // ' 登録年月日(8桁)
        // csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
        // ' 更新区分(1桁)
        // csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
        // ' ユーザID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
        // End If
        // ' 端末ID(8桁)(.NET32桁)
        // If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
        // csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
        // Else
        // csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
        // End If
        // ' タイムスタンプ(14桁)
        // csToshoRow.Item(ABToshoTable.UP_DATE) = ""
        // ' 論理ロックキー(6桁)
        // csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""

        // ' 代納住民コード(8桁)(.NET12桁)
        // csToshoRow.Item(ABToshoTable.D_JUMIN_CD) = CType(csRow.Item(strPrefixD + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
        // ' 業務コード(2桁)
        // csToshoRow.Item(ABToshoTable.GYOMU_CD) = csRow.Item(strPrefixD + ABDainoEntity.GYOMUCD)
        // ' 開始年月日(6桁)
        // csToshoRow.Item(ABToshoTable.ST_YM) = csRow.Item(strPrefixD + ABDainoEntity.STYM)
        // ' 終了年月日(6桁)
        // csToshoRow.Item(ABToshoTable.ED_YM) = csRow.Item(strPrefixD + ABDainoEntity.EDYM)
        // ' 代納区分(2桁)
        // csToshoRow.Item(ABToshoTable.D_DAINO_KBN) = csRow.Item(strPrefixD + ABDainoEntity.DAINOKB)
        // ' 世帯コード(8桁)(.NET12桁)
        // If CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String) = String.Empty Then
        // csToshoRow.Item(ABToshoTable.D_SETAI_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String)
        // ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_SETAI_CD) = "        "
        // Else
        // csToshoRow.Item(ABToshoTable.D_SETAI_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String).Substring(4, 8)
        // End If
        // 'データ区分(2桁)
        // csToshoRow.Item(ABToshoTable.D_DATA_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.ATENADATAKB)
        // Dim strDataDKB As String = CStr(csToshoRow.Item(ABToshoTable.D_DATA_KBN))
        // '住民基本台帳番号(14桁)
        // csToshoRow.Item(ABToshoTable.D_DAICHO_NO) = ""
        // '個人法人区分(1桁)
        // csToshoRow.Item(ABToshoTable.D_KOJINHOJIN_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.KJNHJNKB)
        // ' データ種別(2桁)
        // csToshoRow.Item(ABToshoTable.D_DATA_SHU) = csRow.Item(strPrefixC + ABAtenaEntity.ATENADATASHU)
        // Dim strDataDSB As String = CStr(csToshoRow.Item(ABToshoTable.D_DATA_SHU))
        // ' カナ名称１(60桁)
        // csToshoRow.Item(ABToshoTable.D_KANAMEISHO1) = csRow.Item(strPrefixC + ABAtenaEntity.KANAMEISHO1)
        // ' 漢字名称１(80桁)
        // csToshoRow.Item(ABToshoTable.D_MEISHO1) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIMEISHO1)
        // ' カナ名称２(60桁)
        // csToshoRow.Item(ABToshoTable.D_KANAMEISHO2) = csRow.Item(strPrefixC + ABAtenaEntity.KANAMEISHO2)
        // ' 漢字名称２(80桁)
        // csToshoRow.Item(ABToshoTable.D_MEISHO2) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIMEISHO2)
        // ' 汎用区分１(1桁)
        // '(データ区分が"11""12"の時、カナ名称２がある時の判定)
        // If (strDataKB = "11" Or strDataKB = "12") Then
        // If Not (csToshoRow.Item(ABToshoTable.D_KANAMEISHO2) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = "T"
        // Else
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = "S"
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB1)
        // End If
        // ' 法人形態(20桁)
        // csToshoRow.Item(ABToshoTable.D_HOJINKEITAI) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIHJNKEITAI)
        // ' 汎用区分２(1桁)
        // '(データ区分が"18""28"の時、転出確定住所・転出予定住所がある時の判定)
        // If strDataSB = "18" Or strDataSB = "28" Then
        // If Not (csRow.Item(strPrefixC + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = "K"
        // ElseIf Not (CType(csRow.Item(strPrefixC + ABAtenaEntity.TENSHUTSUYOTEIJUSHO), String) = String.Empty) Then
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = "Y"
        // Else
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB2)
        // End If
        // Else
        // csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB2)
        // End If
        // '管内管外区分(1桁)
        // csToshoRow.Item(ABToshoTable.D_NAIGAI_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.KANNAIKANGAIKB)
        // ' 郵便番号(7桁)
        // csToshoRow.Item(ABToshoTable.D_YUBIN_NO) = csRow.Item(strPrefixC + ABAtenaEntity.YUBINNO)
        // ' 住所コード(11桁)
        // csToshoRow.Item(ABToshoTable.D_JUSHO_CD) = csRow.Item(strPrefixC + ABAtenaEntity.JUSHOCD)
        // '住所(60桁)
        // csToshoRow.Item(ABToshoTable.D_JUSHO) = csRow.Item(strPrefixC + ABAtenaEntity.JUSHO)
        // '番地コード１(5桁)
        // csToshoRow.Item(ABToshoTable.D_BANCHI_CD1) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD1)
        // '番地コード２(5桁)
        // csToshoRow.Item(ABToshoTable.D_BANCHI_CD2) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD2)
        // '番地コード３(5桁)
        // csToshoRow.Item(ABToshoTable.D_BANCHI_CD3) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD3)
        // '番地(40桁)
        // csToshoRow.Item(ABToshoTable.D_BANCHI) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHI)
        // ' 方書フラグ(1桁)
        // csToshoRow.Item(ABToshoTable.D_KATAGAKI_FLG) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKIFG)
        // ' 方書コード(4桁)
        // csToshoRow.Item(ABToshoTable.D_KATAGAKI_CD) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKICD)
        // ' 方書(60桁)
        // csToshoRow.Item(ABToshoTable.D_KATAGAKI) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKI)
        // ' 連絡先1(14桁)
        // csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI1) = csRow.Item(strPrefixC + ABAtenaEntity.RENRAKUSAKI1)
        // ' 連絡先2(14桁)
        // csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI2) = csRow.Item(strPrefixC + ABAtenaEntity.RENRAKUSAKI2)
        // ' 行政区コード(7桁)(.NET9桁)
        // If csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
        // CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD)
        // ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = "       "
        // Else
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
        // End If
        // ' 行政区名(60桁)
        // csToshoRow.Item(ABToshoTable.D_GYOSEIKU) = csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUMEI)
        // ' 地区コード１(6桁)(.NET8桁)
        // If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
        // CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1)
        // ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区名１(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU1) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI1)
        // ' 地区コード２(6桁)(.NET8桁)
        // If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
        // CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2)
        // ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区名２(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU2) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI2)
        // ' 地区コード３(6桁)(.NET8桁)
        // If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
        // CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3)
        // ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = "      "
        // Else
        // csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
        // End If
        // ' 地区３(60桁)
        // csToshoRow.Item(ABToshoTable.D_CHIKU3) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI3)
        // ' 別宛名数(3桁)
        // csToshoRow.Item(ABToshoTable.D_BETSUATENA) = "000"


        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoRow

        // End Function
        #endregion

        #region 最終データ編集
        // '**
        // '*	メソッド名	ReflectLastData
        // '*	概要			最終データの反映
        // '*	引数			csRow		: 取得データ
        // '*				    csToshoRow	: 格納データ
        // '*	戻り値		DataRow
        // '*
        // Public Function ReflectLastData(ByVal csToshoRow As DataRow) As DataRow
        // Const THIS_METHOD_NAME As String = "ReflectLastData"
        // '*履歴番号 000003 2004/11/05 削除開始
        // ''''Dim cuCityInfo As USSCityInfoClass
        // ''''Dim strCityCD As String
        // '*履歴番号 000003 2004/11/05 削除終了

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '*履歴番号 000003 2004/11/05 修正開始
        // ''''インスタンス化
        // '''cuCityInfo = New USSCityInfoClass()
        // ''''市町村情報の取得
        // '''cuCityInfo.GetCityInfo(m_cfControlData)
        // ''''市町村ｺｰﾄﾞの取得
        // '''strCityCD = cuCityInfo.p_strShichosonCD(0)
        // ' 市町村ｺｰﾄﾞ(6桁)
        // ''''csToshoRow.Item(ABToshoTable.SHICHOSONCD) = strCityCD
        // csToshoRow.Item(ABToshoTable.SHICHOSONCD) = m_strCityCD
        // '*履歴番号 000003 2004/11/05 修正終了
        // ' 識別ID(4桁)
        // csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
        // ' 作成日時(14桁)
        // csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
        // ' 最終行区分(1桁)
        // csToshoRow.Item(ABToshoTable.LASTRECKB) = "E"
        // ' 連番(7桁)
        // csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch exAppException As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + exAppException.Message + "】")
        // ' ワーニングをスローする
        // Throw exAppException

        // Catch exException As Exception ' システムエラーをキャッチ
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + exException.Message + "】")

        // ' システムエラーをスローする
        // Throw exException

        // End Try

        // Return csToshoRow
        // End Function
        #endregion

        #region SQL分の作成
        // '************************************************************************************************
        // '* メソッド名     SQL文の作成
        // '* 
        // '* 構文           Private Sub CreateSQL(ByVal strJuminCD As String)
        // '* 
        // '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // '* 
        // '* 引数           strJuminCD As String : 取得データの住民コード
        // '* 
        // '* 戻り値         なし
        // '************************************************************************************************
        // Private Sub CreateSQL(ByVal strJuminCD As String, ByVal strRonSakuFG As String)
        // Const THIS_METHOD_NAME As String = "CreateSQL"
        // Dim strSQL As New Text.StringBuilder()
        // Dim strSFSKSQL As New Text.StringBuilder()
        // Dim strDAINOSQL As New Text.StringBuilder()
        // '*履歴番号 000001 2004/03/08 追加開始
        // Dim strHIDAINOSQL As New Text.StringBuilder()
        // '*履歴番号 000001 2004/03/08 追加終了


        // '**
        // '*本人宛名
        // '*
        // strSQL.Append(" SELECT	")
        // strSQL.Append(getColumnList(True))
        // strSQL.Append(" FROM	ABATENA A")
        // '論理削除の判定
        // If strRonSakuFG = "1" Then
        // '*履歴番号 000002 2004/04/05 追加開始
        // strSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON A.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strSQL.Append(" WHERE	A.SAKUJOFG<>'0' AND A.JUTOGAIYUSENKB ='1' AND A.JUMINCD = '")
        // Else
        // '*履歴番号 000002 2004/04/05 追加開始
        // strSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON A.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strSQL.Append(" WHERE	A.SAKUJOFG<>'1' AND A.JUTOGAIYUSENKB ='1' AND A.JUMINCD = '")
        // End If
        // strSQL.Append(strJuminCD)
        // strSQL.Append("'")

        // '**
        // '*本人宛名＋本人送付先
        // '*
        // strSFSKSQL.Append(" SELECT	")
        // strSFSKSQL.Append(getSFSKColumnList(True))
        // strSFSKSQL.Append(" FROM	ABSFSK B")
        // '論理削除の判定
        // If strRonSakuFG = "1" Then
        // strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') A ON B.JUMINCD = A.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加開始
        // strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON B.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strSFSKSQL.Append(" WHERE	B.SAKUJOFG<>'0' AND B.JUMINCD = '")
        // Else
        // strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') A ON B.JUMINCD = A.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加開始
        // strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON B.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strSFSKSQL.Append(" WHERE	B.SAKUJOFG<>'1' AND B.JUMINCD = '")
        // End If
        // strSFSKSQL.Append(strJuminCD)
        // strSFSKSQL.Append("'")
        // strSFSKSQL.Append(" ORDER BY B.GYOMUCD ")

        // '**
        // '*本人宛名＋代納人宛名＋本人代納
        // '*
        // strDAINOSQL.Append(" SELECT	")
        // strDAINOSQL.Append(getDAINOColumnList(True))
        // strDAINOSQL.Append(" FROM	ABDAINO D")
        // '論理削除の判定
        // If strRonSakuFG = "1" Then
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') C ON D.DAINOJUMINCD = C.JUMINCD")
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') A ON D.JUMINCD = A.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加開始
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON D.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strDAINOSQL.Append(" WHERE	D.SAKUJOFG<>'0' AND D.GYOMUCD<>'05' AND D.GYOMUNAISHU_CD<>'9' AND D.JUMINCD = '")
        // Else
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') C ON D.DAINOJUMINCD = C.JUMINCD")
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') A ON D.JUMINCD = A.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加開始
        // strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON D.JUMINCD = E.JUMINCD")
        // '*履歴番号 000002 2004/04/05 追加終了
        // strDAINOSQL.Append(" WHERE	D.SAKUJOFG<>'1' AND D.GYOMUCD<>'05' AND D.GYOMUNAISHU_CD<>'9' AND D.JUMINCD = '")
        // End If
        // strDAINOSQL.Append(strJuminCD)
        // strDAINOSQL.Append("'")
        // strDAINOSQL.Append(" ORDER BY D.GYOMUCD ")

        // '*履歴番号 000001 2004/03/08 追加開始
        // '**
        // '*被代納人住民コード
        // '*
        // strHIDAINOSQL.Append(" SELECT  JUMINCD ")
        // strHIDAINOSQL.Append(" FROM    ABDAINO ")
        // strHIDAINOSQL.Append(" WHERE	SAKUJOFG<>'1' AND GYOMUCD<>'05' AND GYOMUNAISHU_CD<>'9' AND DAINOJUMINCD = '")
        // strHIDAINOSQL.Append(strJuminCD)
        // strHIDAINOSQL.Append("'")
        // '*履歴番号 000001 2004/03/08 追加終了

        // m_strSQL = strSQL.ToString()
        // m_strSFSKSQL = strSFSKSQL.ToString()
        // m_strDAINOSQL = strDAINOSQL.ToString()
        // '*履歴番号 000001 2004/03/08 追加開始
        // m_strHIDAINOSQL = strHIDAINOSQL.ToString()
        // '*履歴番号 000001 2004/03/08 追加終了
        // End Sub
        #endregion

        #region SQLパラメータ編集
        // '**
        // '* メソッド名
        // '*	GetColumnList_ABAtena
        // '* 
        // '* 概要
        // '*	ABAtenaで処理に必要な列のリストを返す。
        // '* 
        // '* 引数
        // '*	なし
        // '* 
        // '* 戻り値
        // '*	列リスト
        // Private Function GetColumnList_ABAtena() As ArrayList

        // If (m_aryABAtena Is Nothing) Then
        // m_aryABAtena = New ArrayList(56)
        // m_aryABAtena.Add(ABAtenaEntity.SHICHOSONCD)
        // m_aryABAtena.Add(ABAtenaEntity.JUMINCD)
        // m_aryABAtena.Add(ABAtenaEntity.STAICD)
        // m_aryABAtena.Add(ABAtenaEntity.ATENADATAKB)
        // m_aryABAtena.Add(ABAtenaEntity.ATENADATASHU)
        // m_aryABAtena.Add(ABAtenaEntity.SEARCHKANASEI)
        // m_aryABAtena.Add(ABAtenaEntity.SEARCHKANAMEI)
        // m_aryABAtena.Add(ABAtenaEntity.KANAMEISHO1)
        // m_aryABAtena.Add(ABAtenaEntity.KANJIMEISHO1)
        // m_aryABAtena.Add(ABAtenaEntity.KANAMEISHO2)
        // m_aryABAtena.Add(ABAtenaEntity.KANJIMEISHO2)
        // m_aryABAtena.Add(ABAtenaEntity.UMAREYMD)
        // m_aryABAtena.Add(ABAtenaEntity.UMAREWMD)
        // m_aryABAtena.Add(ABAtenaEntity.SEIBETSUCD)
        // m_aryABAtena.Add(ABAtenaEntity.SEIBETSU)
        // m_aryABAtena.Add(ABAtenaEntity.ZOKUGARACD)
        // m_aryABAtena.Add(ABAtenaEntity.ZOKUGARA)
        // m_aryABAtena.Add(ABAtenaEntity.DAI2ZOKUGARACD)
        // m_aryABAtena.Add(ABAtenaEntity.DAI2ZOKUGARA)
        // m_aryABAtena.Add(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
        // m_aryABAtena.Add(ABAtenaEntity.HANYOKB1)
        // m_aryABAtena.Add(ABAtenaEntity.KANJIHJNKEITAI)
        // m_aryABAtena.Add(ABAtenaEntity.KJNHJNKB)
        // m_aryABAtena.Add(ABAtenaEntity.HANYOKB2)
        // m_aryABAtena.Add(ABAtenaEntity.KANNAIKANGAIKB)
        // m_aryABAtena.Add(ABAtenaEntity.YUBINNO)
        // m_aryABAtena.Add(ABAtenaEntity.JUSHOCD)
        // m_aryABAtena.Add(ABAtenaEntity.JUSHO)
        // m_aryABAtena.Add(ABAtenaEntity.BANCHICD1)
        // m_aryABAtena.Add(ABAtenaEntity.BANCHICD2)
        // m_aryABAtena.Add(ABAtenaEntity.BANCHICD3)
        // m_aryABAtena.Add(ABAtenaEntity.BANCHI)
        // m_aryABAtena.Add(ABAtenaEntity.KATAGAKIFG)
        // m_aryABAtena.Add(ABAtenaEntity.KATAGAKICD)
        // m_aryABAtena.Add(ABAtenaEntity.KATAGAKI)
        // m_aryABAtena.Add(ABAtenaEntity.RENRAKUSAKI1)
        // m_aryABAtena.Add(ABAtenaEntity.RENRAKUSAKI2)
        // m_aryABAtena.Add(ABAtenaEntity.GYOSEIKUCD)
        // m_aryABAtena.Add(ABAtenaEntity.GYOSEIKUMEI)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUCD1)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI1)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUCD2)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI2)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUCD3)
        // m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI3)
        // m_aryABAtena.Add(ABAtenaEntity.TOROKUIDOYMD)
        // m_aryABAtena.Add(ABAtenaEntity.TOROKUJIYUCD)
        // m_aryABAtena.Add(ABAtenaEntity.SHOJOIDOYMD)
        // m_aryABAtena.Add(ABAtenaEntity.SHOJOJIYUCD)
        // m_aryABAtena.Add(ABAtenaEntity.CKINIDOYMD)
        // m_aryABAtena.Add(ABAtenaEntity.CKINJIYUCD)
        // m_aryABAtena.Add(ABAtenaEntity.CKINTDKDYMD)
        // m_aryABAtena.Add(ABAtenaEntity.SAKUSEIUSER)
        // m_aryABAtena.Add(ABAtenaEntity.TANMATSUID)
        // m_aryABAtena.Add(ABAtenaEntity.TENSHUTSUKKTIJUSHO)
        // m_aryABAtena.Add(ABAtenaEntity.TENSHUTSUYOTEIJUSHO)
        // m_aryABAtena.TrimToSize()
        // End If

        // Return m_aryABAtena
        // End Function

        // '**
        // '* メソッド名
        // '*	GetColumnList_ABSfsk
        // '* 
        // '* 概要
        // '*	ABSfskで処理に必要な列のリストを返す。
        // '* 
        // '* 引数
        // '*	なし
        // '* 
        // '* 戻り値
        // '*	列リスト
        // Private Function GetColumnList_ABSfsk() As ArrayList

        // If (m_aryABSfsk Is Nothing) Then
        // m_aryABSfsk = New ArrayList(23)
        // m_aryABSfsk.Add(ABSfskEntity.STYM)
        // m_aryABSfsk.Add(ABSfskEntity.EDYM)
        // m_aryABSfsk.Add(ABSfskEntity.JUMINCD)
        // m_aryABSfsk.Add(ABSfskEntity.GYOMUCD)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKDATAKB)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKKANAMEISHO)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKKANJIMEISHO)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKKANNAIKANGAIKB)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKYUBINNO)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKZJUSHOCD)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKJUSHO)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKBANCHI)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKKATAGAKI)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKRENRAKUSAKI1)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKRENRAKUSAKI2)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKGYOSEIKUCD)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKGYOSEIKUMEI)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD1)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI1)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD2)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI2)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD3)
        // m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI3)
        // m_aryABSfsk.TrimToSize()
        // End If

        // Return m_aryABSfsk
        // End Function
        // '**
        // '* メソッド名
        // '*	GetColumnList_ABDaino
        // '* 
        // '* 概要
        // '*	ABDainoで処理に必要な列のリストを返す。
        // '* 
        // '* 引数
        // '*	なし
        // '* 
        // '* 戻り値
        // '*	列リスト
        // Private Function GetColumnList_ABDaino() As ArrayList
        // If (m_aryABDaino Is Nothing) Then
        // m_aryABDaino = New ArrayList(5)
        // m_aryABDaino.Add(ABDainoEntity.STYM)
        // m_aryABDaino.Add(ABDainoEntity.EDYM)
        // m_aryABDaino.Add(ABDainoEntity.DAINOKB)
        // m_aryABDaino.Add(ABDainoEntity.DAINOJUMINCD)
        // m_aryABDaino.Add(ABDainoEntity.GYOMUCD)
        // m_aryABDaino.TrimToSize()
        // End If

        // Return m_aryABDaino
        // End Function

        // '**
        // '* メソッド名
        // '*	getColumnList
        // '* 
        // '* 概要
        // '*	SQLのSelect節の文字列を生成する。
        // '* 
        // '* 引数
        // '*	blnNeedAll		: 業務コードが指定され、全てのテーブルから
        // '*					  それぞれデータを取得する必要があるか？
        // '* 
        // '* 戻り値
        // '*	Select節文字列(但し、"Select" を除く)
        // Private Function getColumnList(ByVal blnNeedAll As Boolean) As String
        // Dim ary As ArrayList
        // Dim iEnum As IEnumerator
        // Dim strTmp As String
        // Dim strClmn As String
        // Dim strBldr1 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/058 追加開始
        // Dim strBldr2 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/058 追加開始

        // Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
        // Const CLMDEF_2 As String = " '' AS {0}_{1}"

        // Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

        // ' 本人宛名
        // ary = GetColumnList_ABAtena()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strBldr1.Length > 0) Then
        // strBldr1.Append(SEPARATOR)
        // End If
        // ' 本人宛名
        // strTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
        // strBldr1.Append(strTmp)

        // End While

        // '*履歴番号 000002 2004/04/05 追加開始
        // ' 代納
        // ary = GetColumnList_ABDaino()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strBldr2.Length > 0) Then
        // strBldr2.Append(SEPARATOR)
        // End If
        // strTmp = String.Format(strFormat, STR_E, iEnum.Current)
        // strBldr2.Append(strTmp)
        // End While
        // '*履歴番号 000002 2004/04/05 追加終了

        // '*履歴番号 000002 2004/04/05 修正開始
        // Return strBldr1.ToString() + SEPARATOR + strBldr2.ToString()
        // 'Return strBldr1.ToString()
        // '*履歴番号 000002 2004/04/05 修正開始
        // End Function

        // '**
        // '* メソッド名
        // '*	getSFSKColumnList
        // '* 
        // '* 概要
        // '*	SQLのSelect節の文字列を生成する。
        // '* 
        // '* 引数
        // '*	blnNeedAll		: 業務コードが指定され、全てのテーブルから
        // '*					  それぞれデータを取得する必要があるか？
        // '* 
        // '* 戻り値
        // '*	Select節文字列(但し、"Select" を除く)
        // Private Function getSFSKColumnList(ByVal blnNeedAll As Boolean) As String
        // Dim ary As ArrayList

        // Dim iEnum As IEnumerator
        // Dim strSFSKTmp As String
        // Dim strSFSKClmn As String
        // Dim strSFSKBldr1 As New Text.StringBuilder()
        // Dim strSFSKBldr2 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/05 追加開始
        // Dim strSFSKBldr3 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/05 追加開始

        // Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
        // Const CLMDEF_2 As String = " '' AS {0}_{1}"

        // Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

        // ' 本人宛名
        // ary = GetColumnList_ABAtena()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strSFSKBldr1.Length > 0) Then
        // strSFSKBldr1.Append(SEPARATOR)
        // End If
        // ' 本人宛名
        // strSFSKTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
        // strSFSKBldr1.Append(strSFSKTmp)

        // End While
        // '本人送付先()
        // ary = GetColumnList_ABSfsk()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strSFSKBldr2.Length > 0) Then
        // strSFSKBldr2.Append(SEPARATOR)
        // End If
        // ' 本人送付先
        // strSFSKTmp = String.Format(strFormat, STR_B, iEnum.Current)
        // strSFSKBldr2.Append(strSFSKTmp)
        // End While


        // '*履歴番号 000002 2004/04/05 追加開始
        // ' 代納
        // ary = GetColumnList_ABDaino()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strSFSKBldr3.Length > 0) Then
        // strSFSKBldr3.Append(SEPARATOR)
        // End If
        // strSFSKTmp = String.Format(strFormat, STR_E, iEnum.Current)
        // strSFSKBldr3.Append(strSFSKTmp)
        // End While
        // '*履歴番号 000002 2004/04/05 追加終了

        // '*履歴番号 000002 2004/04/05 修正開始
        // Return strSFSKBldr1.ToString() + SEPARATOR + strSFSKBldr2.ToString() + SEPARATOR + strSFSKBldr3.ToString()
        // 'Return strSFSKBldr1.ToString() + SEPARATOR + strSFSKBldr2.ToString()
        // '*履歴番号 000002 2004/04/05 修正終了
        // End Function

        // '**
        // '* メソッド名
        // '*	getDAINOColumnList
        // '* 
        // '* 概要
        // '*	SQLのSelect節の文字列を生成する。
        // '* 
        // '* 引数
        // '*	blnNeedAll		: 業務コードが指定され、全てのテーブルから
        // '*					  それぞれデータを取得する必要があるか？
        // '* 
        // '* 戻り値
        // '*	Select節文字列(但し、"Select" を除く)
        // Private Function getDAINOColumnList(ByVal blnNeedAll As Boolean) As String
        // Dim ary As ArrayList

        // Dim iEnum As IEnumerator
        // Dim strDAINOTmp As String
        // Dim strDAINOClmn As String
        // Dim strDAINOBldr1 As New Text.StringBuilder()
        // Dim strDAINOBldr2 As New Text.StringBuilder()
        // Dim strDAINOBldr3 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/05 追加開始
        // Dim strDAINOBldr4 As New Text.StringBuilder()
        // '*履歴番号 000002 2004/04/05 追加終了

        // Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
        // Const CLMDEF_2 As String = " '' AS {0}_{1}"

        // Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

        // ' 本人宛名
        // ary = GetColumnList_ABAtena()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strDAINOBldr1.Length > 0) Then
        // strDAINOBldr1.Append(SEPARATOR)
        // strDAINOBldr2.Append(SEPARATOR)
        // End If
        // ' 本人宛名
        // strDAINOTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
        // strDAINOBldr1.Append(strDAINOTmp)

        // ' 代納人宛名
        // strDAINOTmp = String.Format(strFormat, STR_C, iEnum.Current)
        // strDAINOBldr2.Append(strDAINOTmp)
        // End While

        // ' 代納
        // ary = GetColumnList_ABDaino()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strDAINOBldr3.Length > 0) Then
        // strDAINOBldr3.Append(SEPARATOR)
        // End If
        // strDAINOTmp = String.Format(strFormat, STR_D, iEnum.Current)
        // strDAINOBldr3.Append(strDAINOTmp)
        // End While

        // '*履歴番号 000002 2004/04/05 追加開始
        // ' 代納
        // ary = GetColumnList_ABDaino()
        // iEnum = ary.GetEnumerator()
        // While (iEnum.MoveNext())
        // If (strDAINOBldr4.Length > 0) Then
        // strDAINOBldr4.Append(SEPARATOR)
        // End If
        // strDAINOTmp = String.Format(strFormat, STR_E, iEnum.Current)
        // strDAINOBldr4.Append(strDAINOTmp)
        // End While
        // '*履歴番号 000002 2004/04/05 追加終了

        // '*履歴番号 000002 2004/04/05 修正開始
        // Return strDAINOBldr1.ToString() + SEPARATOR + strDAINOBldr2.ToString() + SEPARATOR + strDAINOBldr3.ToString() + SEPARATOR + strDAINOBldr4.ToString()
        // 'Return strDAINOBldr1.ToString() + SEPARATOR + strDAINOBldr2.ToString() + SEPARATOR + strDAINOBldr3.ToString()
        // '*履歴番号 000002 2004/04/05 修正終了
        // End Function
        #endregion

        #region データカラム作成
        // '************************************************************************************************
        // '* メソッド名      データカラム作成
        // '* 
        // '* 構文            Private Function CreateColumnsData() As DataTable
        // '* 
        // '* 機能　　        レプリカＤＢのカラム定義を作成する
        // '* 
        // '* 引数           なし
        // '* 
        // '* 戻り値         DataTable() 代納情報テーブル
        // '************************************************************************************************
        // Private Function CreateColumnsData() As DataTable
        // Const THIS_METHOD_NAME As String = "CreateColumnsData"
        // Dim csToshoTable As DataTable
        // Dim csDataColumn As DataColumn

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 代納情報カラム定義
        // csToshoTable = New DataTable()
        // csToshoTable.TableName = ABToshoTable.TABLE_NAME
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SHICHOSONCD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SHIKIBETSUID, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SAKUSEIYMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LASTRECKB, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENBAN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUMIN_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOMU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.EDABAN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 3
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ST_YM, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ED_YM, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SETAI_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DATA_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DAICHO_NO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DATA_SHU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANASEI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 24
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 16
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEISHO1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.MEISHO1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 80
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEISHO2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.MEISHO2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 80
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UMARE_YMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UMARE_WYMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SEIBETSU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SEIBETSU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 30
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA_CD2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 30
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.K_DAIHYOJUMIN_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.H_DAIHYOMEI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SANGYO_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HONTEN_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HANYO_KBN1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HOJINKEITAI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 20
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KOJINHOJIN_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HOKA_NINZU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HANYO_KBN2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.NAIGAI_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.YUBIN_NO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUSHO_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 11
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUSHO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 40
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI_FLG, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENRAKUSAKI1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENRAKUSAKI2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOSEIKU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOSEIKU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_IDO_YMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_JIYU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SJO_IDO_YMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SJO_JIYU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LAST_RIREKI_NO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DAINO_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUMIN_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_SETAI_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DATA_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DAICHO_NO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KOJINHOJIN_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DATA_SHU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KANAMEISHO1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_MEISHO1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 80
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KANAMEISHO2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_MEISHO2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 80
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HANYO_KBN1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HOJINKEITAI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 20
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HANYO_KBN2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_NAIGAI_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_YUBIN_NO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUSHO_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 11
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUSHO, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 5
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 40
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI_FLG, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 4
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_RENRAKUSAKI1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_RENRAKUSAKI2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_GYOSEIKU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 7
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_GYOSEIKU, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU1, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU2, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU3, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 60
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BETSUATENA, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 3
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.IDO_YMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JIYU_CD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 2
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_YMD, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UPDATE_KBN, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 1
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RSV, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 23
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.USER_ID, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.WS_ID, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 8
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UP_DATE, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 14
        // csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LOCK_KEY, System.Type.GetType("System.String"))
        // csDataColumn.MaxLength = 6


        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        // Catch objAppExp As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + objAppExp.Message + "】")
        // ' エラーをそのままスローする
        // Throw objAppExp

        // Catch objExp As Exception
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + objExp.Message + "】")
        // ' エラーをそのままスローする
        // Throw objExp
        // End Try

        // Return csToshoTable

        // End Function
        #endregion

        #region データカラム作成(業務・枝番)
        // '************************************************************************************************
        // '* メソッド名      データカラム作成
        // '* 
        // '* 構文            Private Function CreateClmGyomuData() As DataTable
        // '* 
        // '* 機能　　        レプリカＤＢのカラム定義を作成する
        // '* 
        // '* 引数           なし
        // '* 
        // '* 戻り値         DataTable() 代納情報テーブル
        // '************************************************************************************************
        // Private Function CreateClmGyomuData() As DataTable
        // Const THIS_METHOD_NAME As String = "CreateClmGyomuData"
        // Dim csGyomuTable As DataTable
        // Dim csGyomuColumn As DataColumn

        // Try
        // ' デバッグログ出力
        // m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 代納情報カラム定義
        // csGyomuTable = New DataTable()
        // csGyomuTable.TableName = ABToshoTable.TABLE_NAME
        // csGyomuColumn = csGyomuTable.Columns.Add(ABToshoTable.GYOMU_CD, System.Type.GetType("System.String"))
        // csGyomuColumn.MaxLength = 2
        // csGyomuColumn = csGyomuTable.Columns.Add(ABToshoTable.EDABAN, System.Type.GetType("System.String"))
        // csGyomuColumn.MaxLength = 3

        // ' デバッグログ出力
        // m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch objAppExp As UFAppException
        // ' ワーニングログ出力
        // m_cfLog.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + objAppExp.Message + "】")
        // ' エラーをそのままスローする
        // Throw objAppExp

        // Catch objExp As Exception
        // ' エラーログ出力
        // m_cfLog.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + objExp.Message + "】")
        // ' エラーをそのままスローする
        // Throw objExp
        // End Try

        // Return csGyomuTable

        // End Function
        #endregion
        // *履歴番号 000004 2005/03/22 削除終了

        #region ワークフロー送信
        // ************************************************************************************************
        // * メソッド名      ワークフロー送信
        // * 
        // * 構文            Private Sub WorkFlowExec()
        // * 
        // * 機能　　        レプリカＤＢのカラム定義を作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void WorkFlowExec(DataSet csToshoEntity, string WORK_FLOW_NAME, string DATA_NAME)
        {
            const string THIS_METHOD_NAME = "WorkFlowExec";
            UWMessageClass cwMessage;
            UWStartRetInfo cwStartRetInfo;
            var cwStartDataInfoForDataSet = new UWStartDataInfoForDataSet[1];
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            var cwSerialGroupId = new UWSerialGroupId[1];            // シリアルグループ
            var cwStartDataInfo = new UWStartDataInfo[1];
            var strHanteiFile = new string[1];

            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ワークフロー出力設定
                cwStartDataInfoForDataSet[0] = new UWStartDataInfoForDataSet();
                cwStartDataInfoForDataSet[0].p_blnColumnOn = false;                                          // カラム情報フラグ
                cwStartDataInfoForDataSet[0].p_strSep = ",";                                                 // 区切り文字
                cwStartDataInfoForDataSet[0].p_strDataName = DATA_NAME;                                      // データ名
                cwStartDataInfoForDataSet[0].p_strDataKbn = UWStartDataInfo.DATAKBN_DATA;                    // データ区分
                cwStartDataInfoForDataSet[0].p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE;    // 圧縮形式

                // ワークフロー起動用クラスのプロパティ設定
                cwMessage = new UWMessageClass(WORK_FLOW_NAME, m_cfControlData.m_strBusinessId);
                cwMessage.p_strWorkFlowName = WORK_FLOW_NAME;
                cwMessage.p_strBusinessCd = ABConstClass.THIS_BUSINESSID;
                cwMessage.p_strApplicationId = m_cfControlData.m_strMenuId;
                cwMessage.p_strUserId = m_cfControlData.m_strUserId;
                cwMessage.p_strClientId = m_cfControlData.m_strClientId;
                // データネームによってテーブル名の場合分けをする
                switch (DATA_NAME ?? "")
                {
                    case ATENA:
                        {
                            // *履歴番号 000004 2005/02/28 修正開始
                            cwStartDataInfoForDataSet[0].p_csData = csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME);
                            cwSerialGroupId[0] = new UWSerialGroupId();
                            cwSerialGroupId[0].p_strValue = (string)csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows(0).Item(ABToshoPrmEntity.STAICD);
                            cwMessage.p_arySerialGroupId = cwSerialGroupId;
                            // ワークフロー出力設定_２
                            cwStartDataInfo[0] = new UWStartDataInfo();
                            cwStartDataInfo[0].p_strDataName = DATA_NAME + "処理判定";                         // データ名
                            cwStartDataInfo[0].p_strDataKbn = UWStartDataInfo.DATAKBN_PARAM;                    // データ区分
                            cwStartDataInfo[0].p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE;    // 圧縮形式
                            cwStartDataInfo[0].p_strEncryptionType = UWStartDataInfo.ENCRYPTIONTYPE_NONE;
                            cwStartDataInfo[0].p_strDataType = UWStartDataInfo.DATATYPE_TXT;
                            cwStartDataInfo[0].p_strCharCode = UWStartDataInfo.CHARCODE_SJIS + UWStartDataInfo.CHAR_RENKETSU + UWStartDataInfo.GAIJI_DENSANUSER;
                            strHanteiFile[0] = "SET PRM_FG=0";
                            cwStartDataInfo[0].p_strData = strHanteiFile;
                            cwMessage.p_aryDataInfo = cwStartDataInfo;
                            break;
                        }
                    // -----------------------------------
                    // '''''cwStartDataInfoForDataSet(1) = New UWStartDataInfoForDataSet()
                    // '''''cwStartDataInfoForDataSet(1).p_blnColumnOn = False                                          'カラム情報フラグ
                    // '''''cwStartDataInfoForDataSet(1).p_strSep = ","                                                 '区切り文字
                    // '''''cwStartDataInfoForDataSet(1).p_strDataName = DATA_NAME + "処理判定"                         'データ名
                    // '''''cwStartDataInfoForDataSet(1).p_strDataKbn = UWStartDataInfo.DATAKBN_DATA                    'データ区分
                    // '''''cwStartDataInfoForDataSet(1).p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE    '圧縮形式
                    // '''''cwStartDataInfoForDataSet(1).p_strEncryptionType = UWStartDataInfo.ENCRYPTIONTYPE_NONE
                    // '''''cwStartDataInfoForDataSet(1).p_strCharCode = UWStartDataInfo.CHARCODE_SJIS + UWStartDataInfo.CHAR_RENKETSU + UWStartDataInfo.GAIJI_DENSANUSER
                    // '''''cwStartDataInfoForDataSet(1).p_strDataType = UWStartDataInfo.DATATYPE_TXT
                    // '''''cwStartDataInfoForDataSet(1).p_csData = csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME)
                    // -------------------------------------
                    // '''''cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABToshoTable.TABLE_NAME)
                    // *履歴番号 000004 2005/02/28 修正終了
                    case KOKUHO:
                        {
                            cwStartDataInfoForDataSet[0].p_csData = csToshoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME);
                            break;
                        }
                    // *履歴番号 000005 2005/10/17 追加開始
                    case JITE:
                        {
                            cwStartDataInfoForDataSet[0].p_csData = csToshoEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME);
                            break;
                        }
                    // *履歴番号 000005 2005/10/17 追加終了
                    // *履歴番号 000006 2008/05/14 追加開始
                    case KAIGO:
                        {
                            cwStartDataInfoForDataSet[0].p_csData = csToshoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME);
                            break;
                        }
                        // *履歴番号 000006 2008/05/14 追加終了
                }
                cwMessage.SetAryDataInfoFromDataSet(cwStartDataInfoForDataSet);

                try
                {
                    cwStartRetInfo = new UWStartRetInfo();
                    // *履歴番号 000005 2005/10/17 修正開始
                    // '''cwStartRetInfo = cwMessage.SendPreStartMsg()
                    try
                    {
                        cwStartRetInfo = cwMessage.SendPreStartMsg();
                    }
                    catch
                    {
                        cwStartRetInfo = cwMessage.SendPreStartCancel();
                        throw;
                    }
                    // *履歴番号 000005 2005/10/17 修正終了

                    if (cwStartRetInfo.p_enStatus == UWReturnCodeTyep.SUCCESS)
                    {
                        try
                        {
                        }
                        // ワークフロー起動ＯＫ
                        // 本来はここでコミットをしなければならない
                        catch (Exception objExp)
                        {
                            m_cfLog.DebugWrite(m_cfControlData, "ワークフロー起動・ステップ２／" + objExp.ToString());
                            cwStartRetInfo = cwMessage.SendPreStartCancel();
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            // エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070);
                            // 例外を生成
                            throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        }
                        try
                        {
                            cwStartRetInfo = cwMessage.SendStartMsg();
                            if (cwStartRetInfo.p_enStatus == UWReturnCodeTyep.ERROR)
                            {
                                m_cfLog.DebugWrite(m_cfControlData, "ワークフロー起動・ステップ３／失敗");
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }
                        }
                        catch (Exception objExp)
                        {
                            m_cfLog.DebugWrite(m_cfControlData, "ワークフロー起動・ステップ３／" + objExp.ToString());
                            // System.Diagnostics.Debug.WriteLine(ex.Message)
                            cwStartRetInfo = cwMessage.SendPreStartCancel();
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            // エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070);
                            // 例外を生成
                            throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        }
                    }
                    // *履歴番号 000005 2005/10/17 追加開始
                    else
                    {
                        m_cfLog.DebugWrite(m_cfControlData, "ワークフロー起動・ステップ１／失敗");
                        cwStartRetInfo = cwMessage.SendPreStartCancel();
                        m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                        // *履歴番号 000005 2005/10/17 追加終了
                    }
                }
                catch (Exception objExp)
                {
                    m_cfLog.DebugWrite(m_cfControlData, "ワークフロー起動・ステップ１／" + objExp.ToString());
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;
            }

        }
        #endregion

        // *履歴番号 000004 2005/02/28 追加開始
        #region レプリカデータ作成用パラメータデータカラム作成
        // ************************************************************************************************
        // * メソッド名      レプリカデータ作成用パラメータデータカラム作成
        // * 
        // * 構文            Private Function CreateColumnsData() As DataTable
        // * 
        // * 機能　　        レプリカＤＢのカラム定義を作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataTable() 代納情報テーブル
        // ************************************************************************************************
        public DataTable CreateColumnsToshoPrmData()
        {
            const string THIS_METHOD_NAME = "CreateColumnsToshoPrmData";
            DataTable csABToshoPrmTable;                       // レプリカ作成用パラメータデータテーブル
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // レプリカデータ作成用パラメータカラム定義
                csABToshoPrmTable = new DataTable();
                csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME;
                csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.STAICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.KOSHINKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;

                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;
            }
            return csABToshoPrmTable;

        }
        #endregion
        // *履歴番号 000004 2005/02/28 追加終了

    }
}