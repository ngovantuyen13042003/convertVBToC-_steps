'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         番地コード編集Ｂクラス(ABBanchiEdabanSuchiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/08/14  早崎 雄矢
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Security

Public Class ABBanchiEdabanSuchiBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass                            ' ログ出力クラス
    Private m_cfUFControlData As UFControlData                      ' コントロールデータ
    Private m_cfUFConfigDataClass As UFConfigDataClass              ' コンフィグデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABBanchiEdabanSuchiBClass"

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass) 
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    <SecuritySafeCritical>
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)

        ' メンバ変数セット
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfConfigDataClass

        ' ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId)

    End Sub
#End Region

#Region "メソッド"
    '**********************************************************************************************************************
    '* メソッド名     番地コード編集
    '* 
    '* 構文           Public Function GetBanchiEdabanSuchi(ByVal strBanchiCD1 As String, ByVal strBanchiCD2 As String, _
    '*                                                     ByVal strBanchiCD3 As String) As String
    '* 
    '* 機能           番地から番地コード１～３を編集する
    '* 
    '* 引数           strBanchiCD1 As String : 番地コード１
    '*                strBanchiCD2 As String : 番地コード２
    '*                strBanchiCD3 As String : 番地コード３
    '*
    '* 戻り値         String      編集した番地コード
    '*
    '**********************************************************************************************************************
    <SecuritySafeCritical>
    Public Function GetBanchiEdabanSuchi(ByVal strBanchiCD1 As String, ByVal strBanchiCD2 As String,
                                         ByVal strBanchiCD3 As String) As String
        Dim THIS_METHOD_NAME As String = "GetBanchiEdabanSuchi"
        Dim strAfterBanchiCD1 As String
        Dim strAfterBanchiCD2 As String
        Dim strAfterBanchiCD3 As String

        Try

            strAfterBanchiCD1 = GetBanchiCDChange(strBanchiCD1)
            strAfterBanchiCD2 = GetBanchiCDChange(strBanchiCD2)
            strAfterBanchiCD3 = GetBanchiCDChange(strBanchiCD3)

            '連結して戻り値とする
            GetBanchiEdabanSuchi = strAfterBanchiCD1 & strAfterBanchiCD2 & strAfterBanchiCD3

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            Throw
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            Throw
        End Try

        Return GetBanchiEdabanSuchi

    End Function

    '**********************************************************************************************************************
    '* メソッド名     番地コード変換(5桁)
    '* 
    '* 構文           Public Function GetBanchiCDChange(ByVal strBanchiCD As String) As String
    '* 
    '* 機能           番地コードに数値以外が存在した場合、以降を０埋めする(5桁)
    '* 
    '* 引数           strBanchiCD As String : 番地コード
    '*
    '* 戻り値         String      編集した番地コード
    '*
    '**********************************************************************************************************************
    Public Function GetBanchiCDChange(ByVal strBanchiCD As String) As String
        Dim THIS_METHOD_NAME As String = "GetBanchiCDChange"
        Dim strBanchiData As String
        Dim strBanchiCDAfter As String = String.Empty

        '番地コード≠空白の場合
        If (strBanchiCD.Trim IsNot String.Empty) Then
            '番地コードに数値以外が含まれる場合
            If Not IsNumeric(strBanchiCD) Then
                '一文字づつチェックを行い、数値以外が存在する場合、以降0埋めする(5桁)
                For Each strBanchiData In strBanchiCD
                    If IsNumeric(strBanchiData) Then
                        strBanchiCDAfter = strBanchiCDAfter & strBanchiData

                    ElseIf strBanchiData = " " Then
                        strBanchiCDAfter = strBanchiCDAfter & "0"

                    Else
                        strBanchiCDAfter = strBanchiCDAfter.PadRight(5, "0"c)
                        Exit For
                    End If
                Next
            ElseIf (strBanchiCD.Trim.Length < 5) Then
                '数値のみ5桁以下の場合、前0で5桁埋める
                strBanchiCDAfter = strBanchiCD.Trim.PadLeft(5, "0"c)
            ElseIf (strBanchiCD.Trim.Length = 5) Then
                '数値のみ5桁の場合、そのまま返す
                strBanchiCDAfter = strBanchiCD
            End If
        Else
            strBanchiCDAfter = String.Empty.PadLeft(5, "0"c)
        End If

        Return strBanchiCDAfter

    End Function
#End Region

End Class
