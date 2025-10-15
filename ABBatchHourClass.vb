'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ更新系バッチ排他クラス(ABBatchHourClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2015/07/02　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* yyyy/MM/dd   000000      ＮＮＮＮＮ
'************************************************************************************************

Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common

''' <summary>
''' ＡＢ更新系バッチ排他クラス
''' </summary>
''' <remarks></remarks>
Public Class ABBatchHourClass
    Inherits USBBatchHourClass

    ''' <summary>
    ''' 共通クラスの戻り値定義
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AB_RESULT
        Public Const NIGHT As Integer = 1         ' 夜間バッチエラー
        Public Const UPDATE As Integer = 2        ' 更新系バッチエラー
    End Class

    ''' <summary>
    ''' 排他キー定義
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AB_HAITAKEY
        Public Const AB As String = "AB"
    End Class

    ''' <summary>
    ''' 更新系バッチ排他チェック
    ''' </summary>
    ''' <param name="strKey">排他キー</param>
    ''' <remarks></remarks>
    Public Overloads Sub CheckBatchHourForAB(ByVal strKey As String)
        Me.CheckBatchHourForAB(New String() {strKey})
    End Sub

    ''' <summary>
    ''' 更新系バッチ排他チェック
    ''' </summary>
    ''' <param name="a_strKey">排他キー配列</param>
    ''' <remarks></remarks>
    Public Overloads Sub CheckBatchHourForAB(ByVal a_strKey() As String)
        Dim intResult As Integer
        For Each strKey As String In a_strKey
            intResult = MyBase.ChkBatchHour(ABConstClass.THIS_BUSINESSID, strKey)
            Select Case intResult
                Case AB_RESULT.NIGHT, AB_RESULT.UPDATE
                    Throw New UFAppException(Me.p_strErrMsg, String.Empty)
                Case Else
                    ' noop
            End Select
        Next strKey
    End Sub

End Class