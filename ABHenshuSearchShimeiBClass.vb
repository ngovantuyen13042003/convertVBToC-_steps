'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        編集検索氏名(ABHenshuSearchShimeiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2007/09/28　中沢　誠
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2007/10/10 000001     標準市町村の検索カナ項目がアルファベットの場合は大文字に変換（中沢）
'* 2023/08/14 000002    【AB-0820-1】住登外管理項目追加(早崎)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports Densan.Common

Public Class ABHenshuSearchShimeiBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLog As UFLogClass                       'ログ出力クラス
    Private m_cfConfigData As UFConfigDataClass         '環境情報データクラス
    Private m_cfControlData As UFControlData            'コントロールデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABHenshuSearchShimeiBClass"
    '*履歴番号 000002 2023/08/14 追加開始
    Private Const KANA_SEIMEI As Integer = 120
    Private Const KANA_SEI As Integer = 72
    Private Const KANA_MEI As Integer = 48
    '*履歴番号 000002 2023/08/14 追加終了
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal csUFControlData As UFControlData, 
    '*                               ByVal csUFConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            csUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 csUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)
        'メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigData

        'ログ出力クラスのインスタンス化
        m_cfLog = New UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     検索用カナ編集
    '* 
    '* 構文           Public Function GetSearchKana(ByVal strKanaMeisho As String, _
    '*                                              ByVal strKanaMeisho As String, _
    '*                                              ByVal enHommyKensakuKB As FrnHommyoKensakuType) As String()
    '* 
    '* 機能　　       検索用カナ名称を編集する
    '* 
    '* 引数           strKanaMeisho    As String                   : カナ名称１
    '*                strKanaMeisho2   As String                   : カナ名称２
    '*                enHommyKensakuKB As FrnHommyoKensakuType     : 本名優先検索区分
    '* 
    '* 戻り値         String()          : [0]検索用カナ姓名
    '*                                  : [1]検索用カナ姓
    '*                                  : [2]検索用カナ名
    '*                                  : [3]カナ姓
    '*                                  : [4]カナ名
    '************************************************************************************************
    Public Function GetSearchKana(ByVal strKanaMeisho As String, _
                                             ByVal strKanaMeisho2 As String, _
                                             ByVal enHommyKensakuKB As FrnHommyoKensakuType) As String()
        Const THIS_METHOD_NAME As String = "GetSearchKana"                      'メソッド名
        Dim strSearchKana(4) As String                      '検索用カナ
        Dim cuString As New USStringClass                   '文字列編集
        Dim intIndex As Integer                             '先頭からの空白位置

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '標準（Tsusho：標準　Tsusho_Seishiki：本名と通称名で検索可能なDB）
            If (enHommyKensakuKB = FrnHommyoKensakuType.Tsusho) Then

                ' カナ姓名 空白を詰めてから清音化する
                '* 履歴番号 0000001 2007/10/10 修正開始
                strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()
                'strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty))
                '* 履歴番号 0000001 2007/10/10 修正終了

                ' 先頭からの空白位置を調べる
                intIndex = strKanaMeisho.RIndexOf(" ")

                ' 空白が存在しない場合
                If (intIndex = -1) Then
                    ' カナ姓・名
                    strSearchKana(1) = strSearchKana(0)
                    strSearchKana(3) = strKanaMeisho
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    ' カナ姓・名
                    '* 履歴番号 0000001 2007/10/10 修正開始
                    strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex)).ToUpper()
                    'strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.Substring(0, intIndex))
                    '* 履歴番号 0000001 2007/10/10 修正終了
                    strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                    ' 先頭からの空白位置が文字列長と以上場合
                    If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                        strSearchKana(2) = String.Empty
                        strSearchKana(4) = String.Empty
                    Else
                        '* 履歴番号 0000001 2007/10/10 修正開始
                        strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper()
                        'strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.Substring(intIndex + 1))
                        '* 履歴番号 0000001 2007/10/10 修正終了
                        strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                    End If
                End If
            Else
                '本名と通称名で検索可能なDB

                ' カナ姓名 空白を詰めてから清音化する
                strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()

                ' 先頭からの空白位置を調べる
                intIndex = strKanaMeisho.RIndexOf(" ")

                ' 空白が存在しない場合カナ姓のみをセット
                If (intIndex = -1) Then
                    ' カナ姓
                    strSearchKana(1) = String.Empty
                    strSearchKana(3) = strKanaMeisho
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    ' カナ姓（法人のみ使用）
                    strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                    ' 先頭からの空白位置が文字列長以上の場合
                    If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                        strSearchKana(2) = String.Empty
                        strSearchKana(4) = String.Empty
                    Else
                        strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper()
                        ' カナ名（法人のみ使用）
                        strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                    End If
                End If

                '本名カナ姓名
                If (strKanaMeisho2.RLength > 0) Then
                    strSearchKana(1) = cuString.ToKanaKey((strKanaMeisho2).Replace(" ", String.Empty)).ToUpper()
                Else
                    strSearchKana(1) = String.Empty
                End If

            End If

            '*履歴番号 000002 2023/08/14 修正開始
            ''検索カナ姓名の桁チェック
            'If strSearchKana(0).RLength > 40 Then
            '    strSearchKana(0) = strSearchKana(0).RSubstring(0, 40)
            'End If
            If strSearchKana(0).RLength > KANA_SEIMEI Then
                strSearchKana(0) = strSearchKana(0).RSubstring(0, KANA_SEIMEI)
            End If
            '*履歴番号 000002 2023/08/14 修正終了

            '*履歴番号 000002 2023/08/14 修正開始
            ''検索カナ姓の桁チェック
            'If strSearchKana(1).RLength > 24 Then
            '    strSearchKana(1) = strSearchKana(1).RSubstring(0, 24)
            'End If
            If strSearchKana(1).RLength > KANA_SEI Then
                strSearchKana(1) = strSearchKana(1).RSubstring(0, KANA_SEI)
            End If
            '*履歴番号 000002 2023/08/14 修正終了

            '*履歴番号 000002 2023/08/14 修正開始
            ''検索カナ名の桁チェック
            'If strSearchKana(2).RLength > 16 Then
            '    strSearchKana(2) = strSearchKana(2).RSubstring(0, 16)
            'End If
            If strSearchKana(2).RLength > KANA_MEI Then
                strSearchKana(2) = strSearchKana(2).RSubstring(0, KANA_MEI)
            End If
            '*履歴番号 000002 2023/08/14 修正終了

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

        Return strSearchKana

    End Function
#End Region
End Class
