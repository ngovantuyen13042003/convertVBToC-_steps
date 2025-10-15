'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        施行日判定Ｂ(ABSekoYMDHanteiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2011/10/18　後藤　洋輔
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　 履歴番号　　    修正内容
'************************************************************************************************

Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABSekoYMDHanteiBClass

    '**
    '* クラスID定義
    '* 
    Private Const THIS_CLASS_NAME As String = "ABSekoYMDHanteiBCClass"

#Region "メンバ変数定義"
    Private m_cfControlData As UFControlData                            'コントロールデータ
    Private m_cfConfigData As UFConfigDataClass                         'コンフィグデータ
    Private m_cfLog As UFLogClass                                       'ログクラス
    Private m_cfRdb As UFRdbClass                                       'RDBクラス
    Private m_cfDate As UFDateClass                                     '日付クラス
    Private m_csAtenaKanriJohoB As ABAtenaKanriJohoBClass               '宛名管理情報Bクラス   
#End Region

#Region "コンストラクタ(New)"
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass, _
                   ByVal cfRdb As UFRdbClass)

        '●各メンバ変数のインスタンス化
        '  *コントロールデータ
        m_cfControlData = cfControlData

        '  *コンフィグデータ
        m_cfConfigData = cfConfigData

        '  *ログクラス
        m_cfLog = New UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId)

        '  *RDBクラス
        m_cfRdb = cfRdb

        '  *日付クラス
        m_cfDate = New UFDateClass(m_cfConfigData)

        '  *宛名管理情報クラス
        m_csAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

    End Sub
#End Region

#Region "施行日取得(呼び出し)(GetSekoYMD)"
    ''' <summary>
    ''' 施行日を取得します
    ''' </summary>
    ''' <returns>施行日</returns>
    ''' <remarks>施行日取得メソッドを実行し、施行日を取得します</remarks>
    Public Function GetSekoYMD() As String

        Const THIS_METHOD_NAME As String = "GetSekoYMD"         'メソッド名
        Dim strRetSekoYMD As String = String.Empty              '施行日(返却用)

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '●管理情報から施行日の取得を行う
            strRetSekoYMD = Me.GetSekoYMDFromKanriJoho

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
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
            ' エラーをそのままスローする
            Throw objExp

        End Try

        Return strRetSekoYMD

    End Function
#End Region

#Region "施行日取得(GetSekoYMDFromKanriJoho)"
    ''' <summary>
    ''' 施行日を取得します
    ''' </summary>
    ''' <returns>施行日</returns>
    ''' <remarks>管理情報で保持する住基法改正施行日を取得します</remarks>
    Private Function GetSekoYMDFromKanriJoho() As String

        Const THIS_METHOD_NAME As String = "GetSekoYMDFromKanriJoho"            'メソッド名
        Const CNS_SHUKEY25 As String = "25"                                     '主キー"25"
        Const CNS_SHIKIBETSUKEY01 As String = "01"                              '識別キー"01"
        Dim strRetSekoYMD As String = String.Empty                              '施行日(返却用)
        Dim csKanriJoho As DataSet = Nothing                                    '管理情報

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '●管理情報から施行日を取得(引数"25","01")
            csKanriJoho = m_csAtenaKanriJohoB.GetKanriJohoHoshu(CNS_SHUKEY25, CNS_SHIKIBETSUKEY01)

            '  *取得した管理情報からパラメータをワークに設定
            With csKanriJoho.Tables(ABAtenaKanriJohoEntity.TABLE_NAME)

                '●施行日の設定
                If (.Rows.Count > 0) Then
                    '**管理情報が取得された場合**

                    strRetSekoYMD = DirectCast(.Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER), String)
                    m_cfDate.p_strDateValue = strRetSekoYMD

                    If ((strRetSekoYMD.Trim.RLength = 8) AndAlso (m_cfDate.CheckDate())) Then
                        '**0行目のパラメータが8桁 かつ 日付として正しい場合**
                        '処理なし
                    Else
                        '  *戻り値に空白を設定
                        strRetSekoYMD = String.Empty
                    End If

                Else
                    '処理なし
                End If

            End With

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
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
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return strRetSekoYMD

    End Function

#End Region

#Region "施行日後チェック(CheckAfterSekoYMD)"
    ''' <summary>
    ''' 施行日後チェックメソッド
    ''' </summary>
    ''' <returns>施行日後チェック結果</returns>
    ''' <remarks>現在日が住基法改正施行日後かの判定をします</remarks>
    Public Function CheckAfterSekoYMD() As Boolean

        Const THIS_METHOD_NAME As String = "CheckAfterSekoYMD"              'メソッド名
        Dim blnCheckResult As Boolean = False                                    '施行日チェックの結果
        Dim strSekoYMD As String = String.Empty                             '施行日

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '●施行日を管理情報より取得
            strSekoYMD = Me.GetSekoYMDFromKanriJoho()

            '●取得した施行日から判定をする
            If (strSekoYMD.Trim.RLength > 0) Then
                '**施行日が空白でない場合**

                If (strSekoYMD <= m_cfRdb.GetSystemDate.ToString("yyyyMMdd")) Then
                    '**施行日が現在日以前の場合**

                    blnCheckResult = True
                Else
                    '処理なし
                End If
            Else
                '処理なし
            End If

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
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
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return blnCheckResult

    End Function
#End Region

End Class
