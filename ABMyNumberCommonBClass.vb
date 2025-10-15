'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        共通番号マスタ共通処理ビジネスクラス(ABMyNumberCommonBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2014/04/30　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2016/01/21   000001      公表の同意取得対応（岩下）
'************************************************************************************************

Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text
'*履歴番号 000001 2016/01/21 追加開始
Imports System.Collections.Generic
'*履歴番号 000001 2016/01/21 追加終了

''' <summary>
''' 共通番号マスタ共通処理ビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABMyNumberCommonBClass

#Region "メンバー変数"

    Private m_cfControlData As UFControlData                    ' コントロールデータ
    Private m_cfConfig As UFConfigClass                         ' コンフィグクラス
    Private m_cfConfigDataClass As UFConfigDataClass            ' コンフィグデータクラス
    Private m_cfLogClass As UFLogClass                          ' ログ出力クラス
    Private m_cfRdbClass As UFRdbClass                          ' ＲＤＢクラス
    '*履歴番号 000001 2016/01/21 追加開始
    Private m_strSelectSQL As String
    '*履歴番号 000001 2016/01/21 追加開始

    Private m_cABMyNumberB As ABMyNumberBClass                  ' 共通番号ビジネスクラス

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABMyNumberCommonBClass"

#End Region

#Region "コンストラクター"

    ''' <summary>
    ''' コンストラクター
    ''' </summary>
    ''' <param name="cfControlData">コントロールデータ</param>
    ''' <param name="cfConfigDataClass">コンフィグデータ</param>
    ''' <param name="cfRdbClass">ＲＤＢクラス</param>
    ''' <remarks></remarks>
    Public Sub New( _
        ByVal cfControlData As UFControlData, _
        ByVal cfConfigDataClass As UFConfigDataClass, _
        ByVal cfRdbClass As UFRdbClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' 共通番号ビジネスクラスのインスタンス化
        m_cABMyNumberB = New ABMyNumberBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

    End Sub

#End Region

#Region "メソッド"

#Region "GetMyNumber"

    ''' <summary>
    ''' 共通番号取得
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <returns>共通番号</returns>
    ''' <remarks>
    ''' 引数の住民コードに対応する直近の共通番号を取得し返信します。
    ''' </remarks>
    Public Function GetMyNumber(ByVal strJuminCd As String) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csDataSet As DataSet
        Dim strMyNumber As String = String.Empty

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 共通番号マスタ取得
            csDataSet = m_cABMyNumberB.SelectByJuminCd(strJuminCd)

            ' 返信オブジェクトの整備
            If (csDataSet IsNot Nothing _
                AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0 _
                AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength > 0) Then
                strMyNumber = csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.MYNUMBER).ToString.Trim
            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
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

        Return strMyNumber

    End Function

#End Region

#Region "GetJuminCd"

    ''' <summary>
    ''' 住民コード取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <returns>共通番号文字列配列</returns>
    ''' <remarks>
    ''' 引数の共通番号に対応する住民コードを取得し返信します。
    ''' 共通番号を履歴を含めて検索します。
    ''' </remarks>
    Public Overloads Function GetJuminCd( _
        ByVal strMyNumber As String) As String()
        Return Me.GetJuminCd(strMyNumber, False)
    End Function

    ''' <summary>
    ''' 住民コード取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="blnCkinFG">直近フラグ（True：直近のみ検索、False：履歴を含めて検索）</param>
    ''' <returns>住民コード文字列配列</returns>
    ''' <remarks>
    ''' 引数の共通番号に対応する住民コードを取得し返信します。
    ''' 直近のみ検索、履歴を含めて検索の指定が可能です。
    ''' </remarks>
    Public Overloads Function GetJuminCd( _
        ByVal strMyNumber As String, _
        ByVal blnCkinFG As Boolean) As String()

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csDataSet As DataSet
        Dim a_strJuminCd() As String = Nothing
        Dim intIndex As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 共通番号マスタ取得
            If (blnCkinFG = True) Then
                csDataSet = m_cABMyNumberB.SelectByMyNumber(strMyNumber, ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
            Else
                csDataSet = m_cABMyNumberB.SelectByMyNumber(strMyNumber, String.Empty)
            End If

            ' 返信オブジェクトの整備
            If (csDataSet IsNot Nothing _
                AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then
                ReDim a_strJuminCd(csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count - 1)
                intIndex = 0
                For Each csDataRow As DataRow In csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows
                    a_strJuminCd(intIndex) = csDataRow.Item(ABMyNumberEntity.JUMINCD).ToString
                    intIndex += 1
                Next csDataRow
            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
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

        Return a_strJuminCd

    End Function

#End Region

    '*履歴番号 000001 2016/01/21 追加開始
#Region "GetConsent"
    ''' <summary>
    ''' 公表の同意取得
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <returns>公表の同意</returns>
    ''' <remarks>
    ''' 対象の住民コードの直近個人番号が法人番号を持っている場合、公表の同意を取得し返信します。
    ''' それ以外はNothinggを返却します。
    ''' 取得した公表の同意が不正な値の場合もNothingを返却します。
    ''' </remarks>
    Public Overloads Function GetConsent(ByVal strJuminCd As String) As String
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim strKohyounoDoui As String = Nothing
        Dim strJuminCdLst As List(Of String)
        Dim dicReturn As Dictionary(Of String, String)

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strJuminCdLst = New List(Of String)
            strJuminCdLst.Add(strJuminCd)

            '公表の同意リスト取得メソッドを呼出
            dicReturn = GetConsent(strJuminCdLst)

            If ((dicReturn IsNot Nothing) AndAlso _
                (dicReturn.Count > 0)) Then
                strKohyounoDoui = dicReturn(strJuminCd)
            Else
                strKohyounoDoui = Nothing
            End If

            ' デバッグ終了ログ出力
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

        Return strKohyounoDoui

    End Function

    ''' <summary>
    ''' 公表の同意取得
    ''' </summary>
    ''' <param name="strJuminCdLst">住民コードリスト</param>
    ''' <returns>Dictionary(住民コード, 公表の同意)</returns>
    ''' <remarks>
    ''' 対象の住民コードが法人番号を持っている場合、公表の同意を取得し返信します。その他はNothingを返却します。
    ''' それ以外はNothinggを返却します。
    ''' 取得した公表の同意が不正な値の場合もNothingを返却します。
    ''' </remarks>
    Public Overloads Function GetConsent(ByVal strJuminCdLst As List(Of String)) As Dictionary(Of String, String)

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csDataSet As DataSet
        Dim strKohyounoDoui As String = Nothing
        Dim strR As String
        Dim strJuminCD As String = String.Empty
        Dim dicReturn As Dictionary(Of String, String)
        Dim lstSortedJuminCD As List(Of String)

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '引数リストの整理
            lstSortedJuminCD = New List(Of String)
            If (strJuminCdLst IsNot Nothing) Then
                lstSortedJuminCD = strJuminCdLst.Distinct().ToList
            Else
                '住民コードリストが存在しない
                Return Nothing
            End If

            ' Dictionaryのインスタンス化
            dicReturn = New Dictionary(Of String, String)

            For Each strJuminCD In lstSortedJuminCD
                ' 共通番号マスタ取得(削除データ除く)
                csDataSet = m_cABMyNumberB.SelectConsentByJuminCd(strJuminCD, False)

                ' 返信オブジェクトの整備
                If (csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0 AndAlso
                     csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength = 13) Then
                    strR = csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.RESERVE).ToString.Trim

                    Select Case strR
                        Case ABConstClass.KOHYONODOUI.KOHYOZUMI_CODE,
                                ABConstClass.KOHYONODOUI.ARI_CODE,
                                ABConstClass.KOHYONODOUI.NASHI_CODE,
                                ABConstClass.KOHYONODOUI.HUYO_CODE

                            strKohyounoDoui = strR

                        Case Else
                            strKohyounoDoui = Nothing
                    End Select
                Else
                    ' noop
                    strKohyounoDoui = Nothing
                End If

                dicReturn.Add(strJuminCD, strKohyounoDoui)
            Next

            ' デバッグ終了ログ出力
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

        Return dicReturn

    End Function

#End Region
    '*履歴番号 000001 2016/01/21 追加終了

#End Region

End Class
