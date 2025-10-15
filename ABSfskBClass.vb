'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        送付先マスタＤＡ(ABSfskBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/08　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/02/25 000001     抽出条件から業務内種別コードをはずすとあるが、業務内種別コードを String.Emptyとして取得する
'* 2003/03/10 000002     住所ＣＤ等の整合性チェックに誤り
'* 2003/03/17 000003     追加時、共通項目を設定する
'* 2003/03/27 000004     エラー処理クラスの参照先を"AB"固定にする
'* 2003/04/23 000005     終了年月整合性チェックで"999999"を許す
'* 2003/05/06 000006     整合性チェック変更
'* 2003/05/21 000007     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000008     RDBアクセスログの修正
'* 2003/10/30 000009     仕様変更、カタカナチェックをANKチェックに変更
'* 2004/08/27 000010     速度改善：（宮沢）
'* 2005/01/25 000011     速度改善２：（宮沢）
'* 2005/06/05 000012     デバックログの一部をはずす
'* 2005/06/16 000013     SQL文をInsert,Update,Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
'* 2005/12/14 000014     仕様変更：行政区ＣＤのチェックANKに変更(マルゴ村山)
'* 2007/03/09 000015     送付先情報取得SQLのソート順を変更(高原)
'* 2010/03/04 000016     送付先マスタ抽出処理のオーバーロードを追加（比嘉）
'* 2010/04/16 000017     VS2008対応（比嘉）
'* 2020/08/21 000018     【AB32006】代納・送付先メンテナンス（石合）
'* 2023/03/10 000019     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
'* 2023/08/22 000020     【AB-0820-1】住登外管理項目追加（澤木）
'* 2023/10/20 000021     【AB-0840-1】送付先管理項目追加(早崎)
'* 2023/12/05 000022     【AB-0840-1】送付先管理項目追加_追加修正（仲西）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABSfskBClass
#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDeleteSQL As String                        ' DELETE用SQL（物理）
    Private m_strDelRonriSQL As String                      ' DELETE用SQL（論理）
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE用パラメータコレクション（物理）
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    'DELETE用パラメータコレクション（論理）

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABSfskBClass"
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード
    '*履歴番号 000021 2023/10/20 追加開始
    Private Const THIS_ONE As Integer = 1
    Private Const ALL0_YMD As String = "00000000"                                   ' 年月日オール０
    Private Const ALL9_YMD As String = "99999999"                                   ' 年月日オール９
    '*履歴番号 000021 2023/10/20 追加終了
    '* 履歴番号 000010 2004/08/27 追加開始（宮沢）
    Public m_blnBatch As Boolean = False               'バッチフラグ
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット
    '* 履歴番号 000010 2004/08/27 追加終了
    Private m_csDataSchma_Hyojun As DataSet   'スキーマ保管用データセット_標準版
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData,
    '* 　　                           ByVal cfConfigDataClass As UFConfigDataClass,
    '* 　　                           ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '* 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '* 　　            cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetSfskBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String    :住民コード
    '*                blnSakujoFG As Boolean  :削除フラグ
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              'このメソッド名
        Dim csSfskEntity As DataSet                                     '送付先マスタデータ
        Dim strSQL As New StringBuilder()                               'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            '* 履歴番号 000010 2004/08/27 追加開始（宮沢）
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            '* 履歴番号 000010 2004/08/27 追加終了

            'WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            'ORDER文結合
            '*履歴番号 000015 2007/03/09 修正開始
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABSfskEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC, ")
            '*履歴番号 000020 2023/08/22 修正開始
            strSQL.Append(ABSfskEntity.STYMD)
            strSQL.Append(" ASC;")
            'strSQL.Append(ABSfskEntity.STYM)
            'strSQL.Append(" ASC;")
            '*履歴番号 000020 2023/08/22 修正終了
            'strSQL.Append(" ORDER BY ")
            'strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            'strSQL.Append(" ASC")
            '*履歴番号 000015 2007/03/09 修正終了

            '検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            '検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000008 2003/08/28 修正開始
            ''RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000011 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                           "【クラス名:" + Me.GetType.Name + "】" + _
                                           "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                           "【実行メソッド名:GetDataSet】" + _
                                           "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000011 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000008 2003/08/28 修正終了

            'SQLの実行 DataSetの取得

            '* 履歴番号 000010 2004/08/27 更新開始（宮沢）
            'csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csSfskEntity = m_csDataSchma.Clone()
            'm_csDataSchma.Clear()
            'csSfskEntity = m_csDataSchma
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* 履歴番号 000010 2004/08/27 更新終了

            'デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String) As DataSet
    '* 
    '* 機能　　    　　送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                strKikanYMD As String         :期間年月日
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String) As DataSet
        Return GetSfskBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                strKikanYMD As String         :期間年月日
    '*                blnSakujoFG As Boolean        :削除フラグ
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String,
                                            ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              'このメソッド名
        Dim csSfskEntity As DataSet                                     '送付先マスタデータ
        Dim strSQL As String                                            'SQL文文字列
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean                                        '削除データ読み込み

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000011 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()
            '* 履歴番号 000011 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする

            'SQL,パラメータコレクションの作成
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiSHUCD, True, strKikanYMD, blnSakujo, cfUFParameterCollectionClass)

            '*履歴番号 000008 2003/08/28 修正開始
            ''RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000011 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000011 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000008 2003/08/28 修正終了

            'SQLの実行 DataSetの取得
            '* 履歴番号 000010 2004/08/27 更新開始（宮沢）
            'csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* 履歴番号 000010 2004/08/27 更新終了

            '* 履歴番号 000011 2005/01/25 追加開始（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する
            '上の番号で一度作成したが、必要なくなったので削除
            'If (strGyomuCD = "*1") Then
            '    If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 1) Then
            '        Dim csDataRow As DataRow
            '        Dim csDataTable As DataTable
            '        Dim intRowCount As Integer
            '        csDataTable = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME)
            '        csDataRow = csDataTable.Rows(0)
            '        For intRowCount = csDataTable.Rows.Count - 1 To 1 Step -1
            '            If (CType(csDataRow.Item(ABSfskEntity.GYOMUNAISHU_CD), String) <> CType(csDataTable.Rows(intRowCount).Item(ABSfskEntity.GYOMUNAISHU_CD), String)) Then
            '                csDataTable.Rows(intRowCount).Delete()
            '            End If
            '        Next
            '        csDataTable.AcceptChanges()
            '    End If
            'End If
            '* 履歴番号 000011 2005/01/25 追加終了（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する

            '* 履歴番号 000011 2005/01/25 追加終了（宮沢）１件だけ読み込む様にしたものを元に戻す
            m_cfRdbClass.p_intMaxRows = intWkKensu
            '* 履歴番号 000011 2005/01/25 追加終了（宮沢）１件だけ読み込む様にしたものを元に戻す

            '* 履歴番号 000011 2005/01/25 削除開始（宮沢）上で全部読み込む様にしたので削除
            ''取得件数
            'If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count() = 0) Then
            '    '取得件数が０件の時
            '    If (strGyomunaiSHUCD <> "") Then
            '        'SQL,パラメータコレクションの作成
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '        strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '        '*履歴番号 000008 2003/08/28 修正開始
            '        ''RDBアクセスログ出力
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '        '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '        '                    "【実行メソッド名:GetDataSet】" + _
            '        '                    "【SQL内容:" + strSQL + "】")

            '        ' RDBアクセスログ出力
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "【クラス名:" + Me.GetType.Name + "】" + _
            '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                                    "【実行メソッド名:GetDataSet】" + _
            '                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '        '*履歴番号 000008 2003/08/28 修正終了
            '        'SQLの実行 DataSetの取得
            '        csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        '取得件数
            '        If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count() = 0) Then
            '            '取得件数が０件の時
            '            'SQL,パラメータコレクションの作成
            '            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '            strSQL = Me.CreateSql_Param(strJuminCD, "00", strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '            '*履歴番号 000008 2003/08/28 修正開始
            '            ''RDBアクセスログ出力
            '            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '            '                "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '            '                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '            '                "【実行メソッド名:GetDataSet】" + _
            '            '                "【SQL内容:" + strSQL + "】")

            '            ' RDBアクセスログ出力
            '            m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                        "【クラス名:" + Me.GetType.Name + "】" + _
            '                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                                        "【実行メソッド名:GetDataSet】" + _
            '                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '            '*履歴番号 000008 2003/08/28 修正終了
            '            'SQLの実行 DataSetの取得
            '            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        End If
            '    ElseIf (strGyomuCD <> "00") Then
            '        'SQL,パラメータコレクションの作成
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '        strSQL = Me.CreateSql_Param(strJuminCD, "00", strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '        '*履歴番号 000008 2003/08/28 修正開始
            '        ''RDBアクセスログ出力
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '        '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '        '                    "【実行メソッド名:GetDataSet】" + _
            '        '                    "【SQL内容:" + strSQL + "】")

            '        ' RDBアクセスログ出力
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "【クラス名:" + Me.GetType.Name + "】" + _
            '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                                    "【実行メソッド名:GetDataSet】" + _
            '                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '        '*履歴番号 000008 2003/08/28 修正終了
            '        'SQLの実行 DataSetの取得
            '        csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '    End If
            'End If
            '* 履歴番号 000011 2005/01/25 削除終了（宮沢）上で全部読み込む様にしたので削除

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '*履歴番号 000016 2010/03/04 追加開始
    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal cABSfskGetParaX As ABSFSKGetParaXClass) As DataSet
    '* 
    '* 機能　　    　 送付先マスタより該当データを取得する。
    '* 
    '* 引数           cABSfskGetParaX   :   送付先情報パラメータクラス
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal cABSfskGetParaX As ABSFSKGetParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              ' メソッド名
        Dim csSfskEntity As DataSet                                     ' 送付先マスタデータ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim blnAndFg As Boolean = False                                 ' AND判定フラグ
        Dim strWork As String

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' インスタンス化
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' スキーマ取得処理
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            Else
            End If

            ' SQL文の作成
            ' SELECT句
            strSQL.Append("SELECT * ")

            strSQL.Append(" FROM ").Append(ABSfskEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' 住民コード
            If (cABSfskGetParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' 住民コードが設定されている場合

                strSQL.Append(ABSfskEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
                cfUFParameterClass.Value = CStr(cABSfskGetParaX.p_strJuminCD)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 業務コード
            If (cABSfskGetParaX.p_strGyomuCD.Trim <> String.Empty) Then
                ' 業務コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABSfskEntity.GYOMUCD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = cABSfskGetParaX.p_strGyomuCD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 業務内種別コード
            If (cABSfskGetParaX.p_strGyomuneiSHU_CD.Trim <> String.Empty) Then
                ' 業務内種別コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = cABSfskGetParaX.p_strGyomuneiSHU_CD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If

            '---------------------------------------------------------------------------------
            ' 期間
            If (cABSfskGetParaX.p_strKikanYM.Trim <> String.Empty) Then
                ' 期間が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append("(")
                '*履歴番号 000021 2023/10/20 修正開始
                'strSQL.Append(ABSfskEntity.STYM)                    '開始年月
                'strSQL.Append(" <= ")
                'strSQL.Append(ABSfskEntity.KEY_STYM)
                'strSQL.Append(" AND ")
                'strSQL.Append(ABSfskEntity.EDYM)                    '終了年月
                'strSQL.Append(" >= ")
                'strSQL.Append(ABSfskEntity.KEY_EDYM)
                strSQL.Append(ABSfskEntity.STYMD)                    '開始年月
                strSQL.Append(" <= ")
                strSQL.Append(ABSfskEntity.KEY_STYMD)
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.EDYMD)                    '終了年月
                strSQL.Append(" >= ")
                strSQL.Append(ABSfskEntity.KEY_EDYMD)
                '*履歴番号 000021 2023/10/20 修正終了
                strSQL.Append(")")

                ' 開始年月
                cfUFParameterClass = New UFParameterClass
                '*履歴番号 000021 2023/10/20 修正開始
                'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
                '*履歴番号 000021 2023/10/20 修正終了
                cfUFParameterClass.Value = cABSfskGetParaX.p_strKikanYM
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 終了年月
                cfUFParameterClass = New UFParameterClass
                '*履歴番号 000021 2023/10/20 修正開始
                'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
                '*履歴番号 000021 2023/10/20 修正終了
                cfUFParameterClass.Value = cABSfskGetParaX.p_strKikanYM
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 削除フラグ
            If (cABSfskGetParaX.p_strSakujoFG.Trim = String.Empty) Then
                ' 削除フラグ指定がない場合、削除データは抽出しない
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If
                strSQL.Append(ABSfskEntity.SAKUJOFG).Append(" <> '1'")

            Else
                ' 削除フラグ指定がある場合、削除データも抽出する
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、SQL文生成処理を終了
                Else
                    ' AND判定フラグが"False"の場合、SQL文から｢WHERE｣を削除
                    ' 削除したSQLを一時退避
                    strWork = strSQL.ToString.Replace("WHERE", String.Empty)

                    ' strSQLをクリアし、退避したSQLをセット
                    strSQL.Length = 0
                    strSQL.Append(strWork)
                End If
            End If
            '---------------------------------------------------------------------------------

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            'デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try

        Return csSfskEntity

    End Function
    '*履歴番号 000016 2010/03/04 追加終了

    '*履歴番号 000018 2020/08/21 追加開始
#Region "被送付先マスタ抽出"

    ''' <summary>
    ''' 被送付先マスタ抽出
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <returns>被送付先マスタ</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetHiSfskBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetHiSfskBHoshu(strJuminCD, False)
    End Function

    ''' <summary>
    ''' 被送付先マスタ抽出
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>被送付先マスタ</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetHiSfskBHoshu( _
        ByVal strJuminCD As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim cfParameterClass As UFParameterClass
        Dim cfParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' スキーマ取得処理
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(String.Empty, ABSfskEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' SQL文の作成    
            csSQL = New StringBuilder
            With csSQL

                .AppendFormat("SELECT A.* FROM {0} AS A", ABSfskEntity.TABLE_NAME)
                .AppendFormat(" LEFT JOIN {0} AS B", ABBikoEntity.TABLE_NAME)
                .AppendFormat(" ON A.{0} = B.{1}", ABSfskEntity.JUMINCD, ABBikoEntity.DATAKEY1)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.GYOMUCD, ABBikoEntity.DATAKEY2)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.GYOMUNAISHU_CD, ABBikoEntity.DATAKEY3)
                '*履歴番号 000021 2023/10/20 修正開始
                '* 履歴番号 000020 2023/08/22 修正開始
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.STYMD, ABBikoEntity.DATAKEY4)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.EDYMD, ABBikoEntity.DATAKEY5)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.STYM, ABBikoEntity.DATAKEY4)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.EDYM, ABBikoEntity.DATAKEY5)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.TOROKURENBAN, ABBikoEntity.DATAKEY4)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.RRKNO, ABBikoEntity.DATAKEY5)
                '* 履歴番号 000020 2023/08/22 修正終了
                '*履歴番号 000021 2023/10/20 修正終了
                .Append(" WHERE")
                .AppendFormat(" B.{0} = '{1}'", ABBikoEntity.BIKOKBN, ABBikoEntity.DEFAULT.BIKOKBN.SFSK)
                .AppendFormat(" AND B.{0} = {1} AND B.{0} IS NOT NULL AND RTRIM(LTRIM(B.{0})) <> ''", ABBikoEntity.RESERVE, ABBikoEntity.PARAM_RESERVE)
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat(" AND A.{0} <> '1'", ABSfskEntity.SAKUJOFG)
                End If
                .Append(" ORDER BY")
                .AppendFormat(" A.{0} ASC,", ABSfskEntity.GYOMUCD)
                .AppendFormat(" A.{0} ASC,", ABSfskEntity.GYOMUNAISHU_CD)
                '* 履歴番号 000020 2023/08/22 修正開始
                .AppendFormat(" A.{0} DESC", ABSfskEntity.STYMD)
                '.AppendFormat(" A.{0} DESC", ABSfskEntity.STYM)
                '* 履歴番号 000020 2023/08/22 修正終了
                .Append(";")

            End With

            ' 検索条件のパラメーターコレクションクラスのインスタンス化
            cfParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメーターを作成
            cfParameterClass = New UFParameterClass
            cfParameterClass.ParameterName = ABBikoEntity.PARAM_RESERVE
            cfParameterClass.Value = strJuminCD

            ' 検索条件のパラメーターコレクションクラスにパラメータークラスを追加
            cfParameterCollectionClass.Add(cfParameterClass)

            ' バッチ判定
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "【クラス名:" + Me.GetType.Name + "】" + _
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                            "【実行メソッド名:GetDataSet】" + _
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString, cfParameterCollectionClass) + "】")
            Else
                ' noop
            End If

            ' SQLの実行 DataSetの取得
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, csDataSet, ABSfskEntity.TABLE_NAME, cfParameterCollectionClass, True)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        Return csDataSet

    End Function

#End Region
    '*履歴番号 000018 2020/08/21 追加終了

    '************************************************************************************************
    '* メソッド名     送付先マスタ追加
    '* 
    '* 構文           Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  送付先マスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertSfskB"                'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '追加件数
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000013 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* 履歴番号 000013 2005/06/16 追加終了
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '作成日時

            '*履歴番号 000021 2023/10/20 追加開始
            ' 個別項目編集を行う
            csDataRow(ABSfskEntity.RRKNO) = THIS_ONE.ToString()                 '履歴番号
            '*履歴番号 000021 2023/10/20 追加終了

            ' 共通項目の編集を行う
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId  '端末ＩＤ
            csDataRow(ABSfskEntity.SAKUJOFG) = "0"                              '削除フラグ
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = Decimal.Zero                '更新カウンタ
            csDataRow(ABSfskEntity.SAKUSEINICHIJI) = strUpdateDateTime          '作成日時
            csDataRow(ABSfskEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   '作成ユーザー
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = strUpdateDateTime           '更新日時
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId    '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000008 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000008 2003/08/28 修正終了

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ更新
    '* 
    '* 構文           Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  送付先マスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateSfskB"                'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intUpdCnt As Integer                                        '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000013 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* 履歴番号 000013 2005/06/16 追加終了
            End If

            '*履歴番号 000022 2023/12/05 削除開始
            ''*履歴番号 000021 2023/10/20 追加開始
            ''履歴番号のカウントアップ
            'csDataRow(ABSfskEntity.RRKNO) = CDec(csDataRow(ABSfskEntity.RRKNO)) + 1                             '履歴番号
            ''*履歴番号 000021 2023/10/20 追加終了
            '*履歴番号 000022 2023/12/05 削除終了

            ' 共通項目の編集を行う
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '端末ＩＤ
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskEntity.KOSHINCOUNTER)) + 1             '更新カウンタ
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '更新日時
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000008 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            '*履歴番号 000008 2003/08/28 修正終了

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ削除（論理）
    '* 
    '* 構文           Public Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  送付先マスタのデータを削除（論理）する。
    '* 
    '* 引数           csDataRow As DataRow  :削除データ
    '* 
    '* 戻り値         削除（論理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB（論理）"                'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intDelCnt As Integer                                        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000013 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* 履歴番号 000013 2005/06/16 追加終了
            End If

            ' 共通項目の編集を行う
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '端末ＩＤ
            csDataRow(ABSfskEntity.SAKUJOFG) = 1                                                                '削除フラグ
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskEntity.KOSHINCOUNTER)) + 1             '更新カウンタ
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '更新日時
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '更新ユーザー

            '*履歴番号 000022 2023/12/05 削除開始
            ''* 履歴番号 000021 2023/10/20 追加開始
            'csDataRow(ABSfskEntity.RRKNO) = CDec(csDataRow(ABSfskEntity.RRKNO)) + 1                             '履歴番号
            ''* 履歴番号 000021 2023/10/20 追加終了
            '*履歴番号 000022 2023/12/05 削除終了

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000008 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strDelRonriSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
            '*履歴番号 000008 2003/08/28 修正終了

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ削除（物理）
    '* 
    '* 構文           Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　  送付先マスタのデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '*                strSakujoKB As String     :削除フラグ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB（物理）"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intDelCnt As Integer                            '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 引数の削除区分をチェック
            If (strSakujoKB <> "D") Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000013 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* 履歴番号 000013 2005/06/16 追加終了
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000008 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strDeleteSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")
            '*履歴番号 000008 2003/08/28 修正終了

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '* corresponds to VS2008 Start 2010/04/16 000017
    ''* 履歴番号 000013 2005/06/16 削除開始
    '''''************************************************************************************************
    '''''* メソッド名     SQL文の作成
    '''''* 
    '''''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '''''* 
    '''''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '''''* 
    '''''* 引数           csDataRow As DataRow : 更新対象の行
    '''''* 
    '''''* 戻り値         なし
    '''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"              'このメソッド名
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
    ''''    Dim strInsertColumn As String                               '追加SQL文項目文字列
    ''''    Dim strInsertParam As String                                '追加SQL文パラメータ文字列
    ''''    Dim strDelRonriSQL As New StringBuilder()                   '論理削除SQL文文字列
    ''''    Dim strDeleteSQL As New StringBuilder()                     '物理削除SQL文文字列
    ''''    Dim strWhere As New StringBuilder()                         '更新削除SQL文Where文文字列

    ''''    Try
    ''''        'デバッグログ出力
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        'SELECT SQL文の作成
    ''''        m_strInsertSQL = "INSERT INTO " + ABSfskEntity.TABLE_NAME + " "
    ''''        strInsertColumn = ""
    ''''        strInsertParam = ""

    ''''        '更新削除Where文作成
    ''''        strWhere.Append(" WHERE ")
    ''''        strWhere.Append(ABSfskEntity.JUMINCD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_JUMINCD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.GYOMUCD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.STYM)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_STYM)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.EDYM)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_EDYM)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

    ''''        'UPDATE SQL文の作成
    ''''        m_strUpdateSQL = "UPDATE " + ABSfskEntity.TABLE_NAME + " SET "

    ''''        'DELETE（論理） SQL文の作成
    ''''        strDelRonriSQL.Append("UPDATE ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.TABLE_NAME)
    ''''        strDelRonriSQL.Append(" SET ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.TANMATSUID)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_TANMATSUID)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.SAKUJOFG)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_SAKUJOFG)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINCOUNTER)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINCOUNTER)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINNICHIJI)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINNICHIJI)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINUSER)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINUSER)
    ''''        strDelRonriSQL.Append(strWhere.ToString)
    ''''        m_strDelRonriSQL = strDelRonriSQL.ToString

    ''''        'DELETE（物理） SQL文の作成
    ''''        strDeleteSQL.Append("DELETE FROM ")
    ''''        strDeleteSQL.Append(ABSfskEntity.TABLE_NAME)
    ''''        strDeleteSQL.Append(strWhere.ToString)
    ''''        m_strDeleteSQL = strDeleteSQL.ToString

    ''''        'SELECT パラメータコレクションクラスのインスタンス化
    ''''        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'UPDATE パラメータコレクションのインスタンス化
    ''''        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'DELETE（論理） パラメータコレクションのインスタンス化
    ''''        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'DELETE（物理） パラメータコレクションのインスタンス化
    ''''        m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'パラメータコレクションの作成
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            'INSERT SQL文の作成
    ''''            strInsertColumn += csDataColumn.ColumnName + ", "
    ''''            strInsertParam += ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            'SQL文の作成
    ''''            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            'INSERT コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''            'UPDATE コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        Next csDataColumn

    ''''        'INSERT SQL文のトリミング
    ''''        strInsertColumn = strInsertColumn.Trim()
    ''''        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    ''''        strInsertParam = strInsertParam.Trim()
    ''''        strInsertParam = strInsertParam.Trim(CType(",", Char))
    ''''        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    ''''        'UPDATE SQL文のトリミング
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim()
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    ''''        'UPDATE SQL文にWHERE句の追加
    ''''        m_strUpdateSQL += strWhere.ToString

    ''''        'UPDATE,DELETE(物理) コレクションにキー情報を追加
    ''''        '住民コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '業務コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '業務内種別コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '開始年月
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '終了年月
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '更新カウンタ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        'DELETE（論理） コレクションにパラメータを追加
    ''''        '端末ＩＤ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '削除フラグ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '更新カウンタ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '更新日時
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '更新ユーザ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '住民コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '業務コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '業務内種別コード
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '開始年月
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '終了年月
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '更新カウンタ
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch exAppException As UFAppException
    ''''        ' ワーニングログ出力
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
    ''''                                    "【ワーニング内容:" + exAppException.Message + "】")
    ''''        ' ワーニングをスローする
    ''''        Throw exAppException

    ''''    Catch exException As Exception 'システムエラーをキャッチ
    ''''        ' エラーログ出力
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【エラー内容:" + exException.Message + "】")
    ''''        ' システムエラーをスローする
    ''''        Throw exException

    ''''    End Try
    ''''End Sub
    ''* 履歴番号 000013 2005/06/16 削除終了
    '* corresponds to VS2008 Start 2010/04/16 000017

    '* 履歴番号 000013 2005/06/16 追加開始
    '************************************************************************************************
    '* メソッド名     Insert用SQL文の作成
    '* 
    '* 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           INSERT用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim csInsertColumn As StringBuilder                        '追加SQL文項目文字列
        Dim csInsertParam As StringBuilder                         '追加SQL文パラメータ文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABSfskEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            'INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            'パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABSfskEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                'INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            'INSERT SQL文のトリミング
            m_strInsertSQL += "(" + csInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + csInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     Update用SQL文の作成
    '* 
    '* 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*履歴番号 000021 2023/10/20 修正開始
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*履歴番号 000021 2023/10/20 修正終了
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABSfskEntity.TABLE_NAME + " SET "

            'UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            'パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABSfskEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABSfskEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABSfskEntity.SAKUSEINICHIJI) Then
                    cfUFParameterClass = New UFParameterClass

                    'SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    'UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            'UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            'UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += strWhere.ToString

            'UPDATE コレクションにキー情報を追加
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正開始
            ''開始年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ''終了年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正終了
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     論理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteRonriSQL"
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strDelRonriSQL As New StringBuilder                     '論理削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*履歴番号 000021 2023/10/20 修正開始
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*履歴番号 000021 2023/10/20 修正終了
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'DELETE（論理） SQL文の作成
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABSfskEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABSfskEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINCOUNTER)
            '*履歴番号 000021 2023/10/20 追加開始
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.RRKNO)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_RRKNO)
            '*履歴番号 000021 2023/10/20 追加終了
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            'DELETE（論理） パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE（論理） コレクションにパラメータを追加
            '端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '削除フラグ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 追加開始
            '履歴番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 追加終了
            '更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正開始
            ''開始年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ''終了年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正終了
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     物理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strDeleteSQL As New StringBuilder                       '物理削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*履歴番号 000021 2023/10/20 修正開始
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*履歴番号 000021 2023/10/20 修正終了
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABSfskEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            'DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE(物理) コレクションにキー情報を追加
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正開始
            ''開始年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ''終了年月
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000021 2023/10/20 修正終了
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub
    '* 履歴番号 000013 2005/06/16 追加終了

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       送付先マスタのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       'このメソッド名
        Dim objErrorStruct As UFErrorStruct                         'エラー定義構造体

        Try
            ' デバッグログ出力
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 日付クラスのインスタンス化
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABSfskEntity.JUMINCD                               '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SHICHOSONCD                           '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KYUSHICHOSONCD                        '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.GYOMUCD                               '業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.GYOMUNAISHU_CD                        '業務内種別コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                '*履歴番号 000021 2023/10/20 修正開始
                'Case ABSfskEntity.STYM                                  '開始年月
                '    If Not (strValue = String.Empty Or strValue = "000000") Then
                '        m_cfDateClass.p_strDateValue = strValue + "01"
                '        If (Not m_cfDateClass.CheckDate()) Then
                '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '            'エラー定義を取得
                '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_STYM)
                '            '例外を生成
                '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                '        End If
                '    End If
                'Case ABSfskEntity.EDYM                                  '終了年月
                '    If Not (strValue = String.Empty Or strValue = "000000" Or strValue = "999999") Then
                '        m_cfDateClass.p_strDateValue = strValue + "01"
                '        If (Not m_cfDateClass.CheckDate()) Then
                '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '            'エラー定義を取得
                '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_EDYM)
                '            '例外を生成
                '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                '        End If
                '    End If
                Case ABSfskEntity.STYMD                                  '開始年月日
                    If (Not (strValue = String.Empty Or strValue = ALL0_YMD)) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_STYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskEntity.EDYMD                                  '終了年月日
                    If (Not (strValue = String.Empty Or strValue = ALL0_YMD Or strValue = ALL9_YMD)) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_EDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                '*履歴番号 000021 2023/10/20 修正終了
                Case ABSfskEntity.SFSKDATAKB                            '送付先データ区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKDATAKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANNAIKANGAIKB                    '送付先管内管外区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANNAIKANGAIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANAMEISHO                        '送付先カナ名称
                    '*履歴番号 000009 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000009 2003/10/30 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANAMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANJIMEISHO                       '送付先漢字名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANJIMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKYUBINNO                           '送付先郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKYUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKZJUSHOCD                          '送付先住所コード
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKZJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKJUSHO                             '送付先住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKJUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKBANCHI                            '送付先番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKBANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKATAGAKI                          '送付先方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKRENRAKUSAKI1                      '送付先連絡先1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKRENRAKUSAKI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKRENRAKUSAKI2                      '送付先連絡先2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKRENRAKUSAKI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKGYOSEIKUCD                        '送付先行政区コード
                    '* 履歴番号 000014 2005/12/14 修正開始
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* 履歴番号 000014 2005/12/14 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKGYOSEIKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKGYOSEIKUMEI                       '送付先行政区名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKGYOSEIKUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD1                          '送付先地区コード1
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI1                         '送付先地区名1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD2                          '送付先地区コード2
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI2                         '送付先地区名2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD3                          '送付先地区コード3
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI3                         '送付先地区名3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.RESERVE                               'リザーブ
                    '何もしない
                Case ABSfskEntity.TANMATSUID                            '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUJOFG                              '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINCOUNTER                         '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUSEINICHIJI                        '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUSEIUSER                           '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINNICHIJI                         '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINUSER                            '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            '*履歴番号 000021 2023/10/20 追加開始
                Case ABSfskEntity.TOROKURENBAN                          '登録連番
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '*履歴番号 000022 2023/12/05 修正開始
                        ''例外を生成
                        'Throw New UFAppException("数字項目入力エラー：ＡＢ送付先　登録連番", UFAppException.ERR_EXCEPTION)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_TOROKURENBAN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        '*履歴番号 000022 2023/12/05 修正終了
                    End If

                Case ABSfskEntity.RRKNO                                 '履歴番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '*履歴番号 000022 2023/12/05 修正開始
                        ''例外を生成
                        'Throw New UFAppException("数字項目入力エラー：ＡＢ送付先　履歴番号", UFAppException.ERR_EXCEPTION)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_RRKNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        '*履歴番号 000022 2023/12/05 修正終了
                    End If
                    '*履歴番号 000021 2023/10/20 追加終了
            End Select

            ' デバッグログ出力
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     ＳＱＬ文・パラメータコレクション作成
    '* 
    '* 構文           Private Function CreateSql_Param(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strKikanYMD As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* 機能　　    　　ＳＱＬ文及びパラメータコレクションを作成し引き渡す。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                blnGyomunaiSHUCD As Boolean   :業務内種別コードの有無（True:有り,False:無し）
    '*                strKikanYMD As String         :期間年月日
    '*                blnSakujo As Boolean          :削除データの有無(True:有り,False:無し)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :パラメータコレクションクラス
    '* 
    '* 戻り値         ＳＱＬ文(String)
    '*                パラメータコレクションクラス(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strKikanYMD As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param"            'このメソッド名
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            '* 履歴番号 000010 2004/08/27 追加開始（宮沢）
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            '* 履歴番号 000010 2004/08/27 追加終了

            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)                 '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                '* 履歴番号 000011 2005/01/25 更新開始（宮沢）共通も一度に読み込む
                'strSQL.Append(" AND ")
                'strSQL.Append(ABSfskEntity.GYOMUCD)             '業務コード
                'strSQL.Append(" = ")
                'strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUCD)             '業務コード
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                '* 履歴番号 000011 2005/01/25 更新終了（宮沢）

                '* 履歴番号 000011 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
                m_cfRdbClass.p_intMaxRows = 1
                '* 履歴番号 000011 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする
            End If
            strSQL.Append(" AND ")
            '* 履歴番号 000011 2005/01/25 更新開始（宮沢）共通種別も一度に読み込む
            'strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)      '業務内種別コード
            'strSQL.Append(" = ")
            'strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If
            '* 履歴番号 000011 2005/01/25 更新終了（宮沢）共通種別も一度に読み込む

            strSQL.Append(" AND (")
            strSQL.Append(ABSfskEntity.STYMD)                    '開始年月日
            strSQL.Append(" <= ")
            strSQL.Append(ABSfskEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABSfskEntity.EDYMD)                    '終了年月日
            strSQL.Append(" >= ")
            strSQL.Append(ABSfskEntity.KEY_EDYMD)
            strSQL.Append(")")
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)            '削除フラグ
                strSQL.Append(" <> 1")
            End If

            '* 履歴番号 000011 2005/01/25 追加開始（宮沢）一度で読んだものをソートして先頭の１件を対象にする
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.Append(ABSfskEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If
            '* 履歴番号 000011 2005/01/25 追加終了（宮沢）一度で読んだものをソートして先頭の１件を対象にする

            '検索条件のパラメータを作成
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '* 履歴番号 000011 2005/01/25 追加開始（宮沢）
            ' 業務内種別コード
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                If (blnGyomunaiSHUCD) Then
                    cfUFParameterClass.Value = strGyomunaiSHUCD
                Else
                    cfUFParameterClass.Value = String.Empty
                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '* 履歴番号 000011 2005/01/25 追加開始（宮沢）

            ' 開始年月
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
            cfUFParameterClass.Value = strKikanYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 終了年月
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
            cfUFParameterClass.Value = strKikanYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return strSQL.ToString

    End Function
    '* 履歴番号 000011 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名     送付先マスタスキーマ取得
    '* 
    '* 構文           Public Function GetSfskSchemaBHoshu() As DataSet
    '* 
    '* 機能　　    　　送付先マスタよりスキーマ取得
    '* 
    '* 
    '* 戻り値         DataSet : 取得した送付先マスタのスキーマ
    '************************************************************************************************
    Public Overloads Function GetSfskSchemaBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskSchemaBHoshu"              'このメソッド名

        Try
            If (m_csDataSchma Is Nothing) Then
                Dim strSQL As New StringBuilder                                 'SQL文文字列
                'デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

                'SQL文の作成
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABSfskEntity.TABLE_NAME)

                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            Return (m_csDataSchma.Clone)
        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try
    End Function
    '* 履歴番号 000011 2005/01/25 追加終了（宮沢）

    '*履歴番号 000019 2023/03/10 追加開始
#Region "送付先マスタ抽出_標準版"
    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出_標準版
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu_Hyojun(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                strKikanYMD As String         :期間年月日
    '*                blnSakujoFG As Boolean        :削除フラグ
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*                   構造：csSfskEntity    インテリセンス：ABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu_Hyojun(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String,
                                            ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu_Hyojun"       'このメソッド名
        Dim csSfskEntity As DataSet                                     '送付先マスタデータ
        Dim strSQL As String                                            'SQL文文字列
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean                                        '削除データ読み込み

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()

            'SQL,パラメータコレクションの作成
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param_Hyojun(strJuminCD, strGyomuCD, strGyomunaiSHUCD, True, strKikanYMD, blnSakujo, cfUFParameterCollectionClass)

            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If

            'SQLの実行 DataSetの取得
            csSfskEntity = m_csDataSchma_Hyojun.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            m_cfRdbClass.p_intMaxRows = intWkKensu

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csSfskEntity

    End Function
#End Region

#Region "ＳＱＬ文・パラメータコレクション作成_標準版"
    '************************************************************************************************
    '* メソッド名     ＳＱＬ文・パラメータコレクション作成_標準版
    '* 
    '* 構文           Private Function CreateSql_Param_Hyojun(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strKikanYMD As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* 機能　　    　　ＳＱＬ文及びパラメータコレクションを作成し引き渡す。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                blnGyomunaiSHUCD As Boolean   :業務内種別コードの有無（True:有り,False:無し）
    '*                strKikanYMD As String         :期間年月日
    '*                blnSakujo As Boolean          :削除データの有無(True:有り,False:無し)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :パラメータコレクションクラス
    '* 
    '* 戻り値         ＳＱＬ文(String)
    '*                パラメータコレクションクラス(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param_Hyojun(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strKikanYMD As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param_Hyojun"     'このメソッド名
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT ")
            ' 送付先マスタの全項目セット
            strSQL.AppendFormat(" {0}.*", ABSfskEntity.TABLE_NAME)
            ' 送付先マスタ_標準の項目セット
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANAKATAGAKI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTSUSHO)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANATSUSHO)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKEIJISHIMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANJISHIMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZACD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTODOFUKEN)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSON)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZA)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD1)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD2)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD3)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKATAGAKICD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKBN)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTOROKUYMD)

            strSQL.Append(" FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            ' 送付先マスタ_標準を付加
            strSQL.AppendFormat(" LEFT OUTER JOIN {0} ", ABSfskHyojunEntity.TABLE_NAME)
            strSQL.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.JUMINCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.TOROKURENBAN,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.TOROKURENBAN)

            If (m_csDataSchma_Hyojun Is Nothing) Then
                m_csDataSchma_Hyojun = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If

            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD)               '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" AND ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD)           '業務コード
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")

                m_cfRdbClass.p_intMaxRows = 1
            End If
            strSQL.Append(" AND ")
            If Not (strGyomuCD = "*1") Then
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If

            strSQL.Append(" AND (")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.STYMD)                 '開始年月日
            strSQL.Append(" <= ")
            strSQL.Append(ABSfskEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.EDYMD)                 '終了年月日
            strSQL.Append(" >= ")
            strSQL.Append(ABSfskEntity.KEY_EDYMD)
            strSQL.Append(")")
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.SAKUJOFG)            '削除フラグ
                strSQL.Append(" <> 1")
            End If

            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If

            '検索条件のパラメータを作成
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 業務内種別コード
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                If (blnGyomunaiSHUCD) Then
                    cfUFParameterClass.Value = strGyomunaiSHUCD
                Else
                    cfUFParameterClass.Value = String.Empty
                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' 開始年月
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
            cfUFParameterClass.Value = strKikanYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 終了年月
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
            cfUFParameterClass.Value = strKikanYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return strSQL.ToString

    End Function
#End Region

#Region "送付先マスタスキーマ取得_標準版"
    '************************************************************************************************
    '* メソッド名     送付先マスタスキーマ取得_標準版
    '* 
    '* 構文           Public Function GetSfskSchemaBHoshu_Hyojun() As DataSet
    '* 
    '* 機能　　    　　送付先マスタよりスキーマ取得
    '* 
    '* 
    '* 戻り値         DataSet : 取得した送付先マスタのスキーマ
    '************************************************************************************************
    Public Overloads Function GetSfskSchemaBHoshu_Hyojun() As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskSchemaBHoshu_Hyojun"         'このメソッド名

        Try
            If (m_csDataSchma_Hyojun Is Nothing) Then
                Dim strSQL As New StringBuilder                                 'SQL文文字列
                'デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

                ' SQL文の作成
                strSQL.Append("SELECT ")
                ' 送付先マスタの全項目セット
                strSQL.AppendFormat(" {0}.*", ABSfskEntity.TABLE_NAME)
                ' 送付先マスタ_標準の項目セット
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANAKATAGAKI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTSUSHO)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANATSUSHO)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKEIJISHIMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANJISHIMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZACD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTODOFUKEN)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSON)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZA)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD1)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD2)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD3)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKATAGAKICD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKBN)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTOROKUYMD)

                strSQL.Append(" FROM ")
                strSQL.Append(ABSfskEntity.TABLE_NAME)

                ' 送付先マスタ_標準を付加
                strSQL.AppendFormat(" LEFT OUTER JOIN {0} ", ABSfskHyojunEntity.TABLE_NAME)
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.JUMINCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUNAISHU_CD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.TOROKURENBAN,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.TOROKURENBAN)

                m_csDataSchma_Hyojun = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            Return (m_csDataSchma_Hyojun.Clone)
        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try
    End Function
#End Region
    '*履歴番号 000019 2023/03/10 追加終了

    '*履歴番号 000021 2023/10/20 追加開始
    '************************************************************************************************
    '* メソッド名     AB代納送付先累積取得
    '* 
    '* 構文           Public Overloads Function GetABdainosfskruiseki(ByVal csDataRow As DataRow) As String
    '* 
    '* 機能           AB代納送付先累積より登録連番を取得
    '* 
    '* 引数           csDataRow As DataRow          :行データ
    '* 
    '* 戻り値         登録連番
    '************************************************************************************************
    Public Overloads Function GetABdainosfskruiseki(ByVal csDataRow As DataRow) As String
        Const THIS_METHOD_NAME As String = "GetABdainosfskruiseki"      ' このメソッド名
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim csSfskEntity As DataSet                                     ' 送付先マスタデータ
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim strTorokurenban As String                                   ' 登録連番

        Try

            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' スキーマ取得処理
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            Else
            End If

            'SQL文の作成
            strSQL.Append("SELECT ")
            strSQL.Append("MAX( ")
            strSQL.Append(ABDainoSfskRuisekiEntity.TOROKURENBAN)
            strSQL.Append(") ")
            strSQL.Append(" FROM ")
            strSQL.Append(ABDainoSfskRuisekiEntity.TABLE_NAME)

            strSQL.Append(" WHERE ")
            '住民コード++
            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.JUMINCD))
            '業務コード
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD))
            '業務内種別コード
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            If (csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString = String.Empty) Then
                strSQL.Append("''")
            Else
                strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD))
            End If
            '処理区分
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.SHORIKB)
            strSQL.Append(" IN ('")
            strSQL.Append(ABConstClass.SFSK_ADD)            ' 追加（送付先）
            strSQL.Append("','")
            strSQL.Append(ABConstClass.SFSK_SHUSEI)         ' 修正（送付先）
            strSQL.Append("','")
            strSQL.Append(ABConstClass.SFSK_DELETE)         ' 削除（送付先）
            strSQL.Append("')")

           ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            'デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '登録連番を取得する。
            If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 0) Then
                If (Not (IsDBNull(csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0).Item(0)))) Then
                    strTorokurenban = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0).Item(0).ToString()
                End If
            End If

            '登録連番が取得できない場合0をセットする
            If (strTorokurenban = String.Empty) Then
                strTorokurenban = "0"
            End If

            Return strTorokurenban

        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try
    End Function
    '*履歴番号 000021 2023/10/20 追加終了
#End Region

End Class
