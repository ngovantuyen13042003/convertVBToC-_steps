'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ共通ビジネスクラス(ABCommonBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2014/07/14　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2015/01/05   000001      法人番号利用開始日対応（石合）
'* 2015/01/09   000002      権限管理機能実装（石合）
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
Imports Densan.Reams.UR.UR010BX
Imports Densan.Reams.UR.UR010BB
Imports System.Security

''' <summary>
''' ＡＢ共通ビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABCommonBClass

#Region "メンバー変数"

    ' メンバー変数
    Protected m_strClassName As String = THIS_CLASS_NAME                      ' クラス名
    Protected m_cfLogClass As UFLogClass                                      ' ログ出力クラス
    Protected m_cfControlData As UFControlData                                ' コントロールデータ
    Protected m_cfConfigDataClass As UFConfigDataClass                        ' コンフィグデータ
    Protected m_cfRdbClass As UFRdbClass                                      ' ＲＤＢクラス

    Protected m_crSekoYMDHanteiB As URSekoYMDHanteiBClass                     ' 番号制度施行日判定ビジネスクラス
    Protected m_crBangoMaskB As URBangoMaskBClass                             ' 番号マスク化ビジネスクラス
    Protected m_cuAccessLog As USLAccessLogKojinBangoClass                    ' アクセスログクラス
    Protected m_cMyNumberCommonB As ABMyNumberCommonBClass                    ' 共通番号共通ビジネスクラス

    '*履歴番号 000001 2015/01/05 追加開始
    Protected m_cAtenaKanriJohoB As ABAtenaKanriJohoBClass                    ' 宛名管理情報ビジネスクラス
    Protected m_blnIsAfterKojinBangoRiyoKaishiYMD As Boolean                  ' 個人番号利用開始日以降判定結果
    Protected m_blnIsAfterHojinBangoRiyoKaishiYMD As Boolean                  ' 法人番号利用開始日以降判定結果
    '*履歴番号 000001 2015/01/05 追加終了

    '*履歴番号 000002 2015/01/09 追加開始
    Protected m_cfUserInfo As UFUserInfoClass                                 ' ユーザー情報クラス
    '*履歴番号 000002 2015/01/09 追加終了

    ' コンスタント定義
    Protected Const THIS_CLASS_NAME As String = "ABCommonBClass"              ' クラス名

#End Region

#Region "コンストラクター"

    ''' <summary>
    ''' コンストラクター
    ''' </summary>
    ''' <param name="strClassName">クラス名</param>
    ''' <remarks></remarks>
    Protected Sub New(ByVal strClassName As String)
        m_strClassName = strClassName
    End Sub

    ''' <summary>
    ''' コンストラクター
    ''' </summary>
    ''' <param name="cfControlData">コントロールデータ</param>
    ''' <param name="cfConfigDataClass">コンフィグデータ</param>
    ''' <param name="cfRdbClass">ＲＤＢクラス</param>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Sub New(
        ByVal cfControlData As UFControlData,
        ByVal cfConfigDataClass As UFConfigDataClass,
        ByVal cfRdbClass As UFRdbClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' 施行日判定ビジネスクラスのインスタンス化
        Try
            m_crSekoYMDHanteiB = New URSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_cfControlData.m_strBusinessId)
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try

        '*履歴番号 000001 2015/01/05 追加開始
        ' 宛名管理情報ビジネスクラスのインスタンス化
        m_cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '*履歴番号 000001 2015/01/05 追加終了

        ' 番号マスク化ビジネスクラスのインスタンス化
        m_crBangoMaskB = New URBangoMaskBClass(m_cfControlData)

        ' アクセスログクラスのインスタンス化
        m_cuAccessLog = New USLAccessLogKojinBangoClass(m_cfControlData, m_cfControlData.m_strBusinessId)

        ' 共通番号共通ビジネスクラスのインスタンス化
        m_cMyNumberCommonB = New ABMyNumberCommonBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '*履歴番号 000002 2015/01/09 追加開始
        ' ユーザー情報クラスのインスタンス化
        m_cfUserInfo = New UFUserInfoClass(m_cfControlData.m_strBusinessId)
        '*履歴番号 000002 2015/01/09 追加終了

        '*履歴番号 000001 2015/01/05 追加開始
        ' 個人番号利用開始日以降判定結果を取得
        m_blnIsAfterKojinBangoRiyoKaishiYMD = CheckAfterBangoSeidoDai4SekoYMD()

        ' 法人番号利用開始日以降判定結果を取得
        m_blnIsAfterHojinBangoRiyoKaishiYMD = CheckAfterHojinBangoRiyoKaishiYMD()
        '*履歴番号 000001 2015/01/05 追加終了

    End Sub

#End Region

#Region "メソッド"

#Region "番号制度判定用システム日付取得"

    ''' <summary>
    ''' 番号制度判定用システム日付取得
    ''' </summary>
    ''' <returns>番号制度判定用システム日付</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function GetBangoSeidoHanteiyouSystemDate() As String
        Try
            Return m_crSekoYMDHanteiB.GetBangoSeidoHanteiyouSystemDate()
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "番号制度施行日取得"

    ''' <summary>
    ''' 番号制度施行日取得
    ''' </summary>
    ''' <returns>番号制度施行日</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function GetBangoSeidoSekoYMD() As String
        Try
            Return m_crSekoYMDHanteiB.GetBangoSeidoSekoYMD()
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "番号制度第４施行日取得"

    ''' <summary>
    ''' 番号制度第４施行日取得
    ''' </summary>
    ''' <returns>番号制度第４施行日</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function GetBangoSeidoDai4SekoYMD() As String
        Try
            Return m_crSekoYMDHanteiB.GetBangoSeidoDai4SekoYMD()
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "番号制度施行日以降判定"

    ''' <summary>
    ''' 番号制度施行日以降判定
    ''' </summary>
    ''' <returns>番号制度施行日以降判定結果</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function CheckAfterBangoSeidoSekoYMD() As Boolean
        Try
            Return m_crSekoYMDHanteiB.CheckAfterBangoSeidoSekoYMD()
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "番号制度第４施行日以降判定"

    ''' <summary>
    ''' 番号制度第４施行日以降判定
    ''' </summary>
    ''' <returns>番号制度第４施行日以降判定結果</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function CheckAfterBangoSeidoDai4SekoYMD() As Boolean
        Try
            Return m_crSekoYMDHanteiB.CheckAfterBangoSeidoDai4SekoYMD()
        Catch cfAppExp As UFAppException
            ' システムをダウンさせるため、ExceptionにてThrowする。
            Throw New Exception(cfAppExp.Message, cfAppExp)
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

    '*履歴番号 000001 2015/01/05 追加開始
#Region "法人番号利用開始日取得"

    ''' <summary>
    ''' 法人番号利用開始日取得
    ''' </summary>
    ''' <returns>法人番号利用開始日</returns>
    ''' <remarks></remarks>
    Public Function GetHojinBangoRiyoKaishiYMD() As String
        Try
            Return m_cAtenaKanriJohoB.GetHojinBangoRiyoKaishiYMD_Param()
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "法人番号利用開始日以降判定"

    ''' <summary>
    ''' 法人番号利用開始日以降判定
    ''' </summary>
    ''' <returns>法人番号利用開始日以降判定結果</returns>
    ''' <remarks></remarks>
    Public Function CheckAfterHojinBangoRiyoKaishiYMD() As Boolean

        Dim blnResult As Boolean

        Try

            If (m_cfRdbClass.GetSystemDate.ToString("yyyyMMdd") < GetHojinBangoRiyoKaishiYMD()) Then
                blnResult = False
            Else
                blnResult = True
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return blnResult

    End Function

#End Region
    '*履歴番号 000001 2015/01/05 追加終了

#Region "番号マスク編集後の文字列取得"

    ''' <summary>
    ''' 番号マスク編集後の文字列取得
    ''' </summary>
    ''' <param name="crBangoMaskPrm">番号編集パラメーター</param>
    ''' <returns>番号マスク編集後の文字列</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function URBangoMask(ByVal crBangoMaskPrm As URBangoMaskPrmClass) As String
        Return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm)
    End Function

#End Region

#Region "番号マスク編集後の文字列取得"

    ''' <summary>
    ''' 番号マスク編集後の文字列取得
    ''' </summary>
    ''' <param name="crBangoMaskPrm">番号編集パラメーター</param>
    ''' <param name="blnWriteAccessLog">アクセスログ出力有無</param>
    ''' <returns>番号マスク編集後の文字列</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function URBangoMask(
        ByVal crBangoMaskPrm As URBangoMaskPrmClass,
        ByVal blnWriteAccessLog As Boolean) As String
        Return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm, blnWriteAccessLog)
    End Function

#End Region

#Region "共通番号（表示用）取得"

    ''' <summary>
    ''' 共通番号（表示用）取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strAtenaDataKB">宛名データ区分</param>
    ''' <returns>共通番号（表示用）</returns>
    ''' <remarks>宛名データ区分を番号タイプに変換してマスク化を行います。</remarks>
    <SecuritySafeCritical>
    Public Function GetDispMyNumber(
        ByVal strMyNumber As String,
        ByVal strJuminCD As String,
        ByVal strAtenaDataKB As String) As String
        Return Me.GetDispMyNumber(strMyNumber, strJuminCD, CType(GetBangoTypeWithAtenaDataKB(strAtenaDataKB), URBangoMaskPrmClass.URKojinBangoType))
    End Function

#End Region

#Region "共通番号（表示用）取得"

    ''' <summary>
    ''' 共通番号（表示用）取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strUserKB">ユーザー区分</param>
    ''' <returns>共通番号（表示用）</returns>
    ''' <remarks>ユーザー区分を番号タイプに変換してマスク化を行います。</remarks>
    Public Function GetDispMyNumberWithUserKB(
        ByVal strMyNumber As String,
        ByVal strJuminCD As String,
        ByVal strUserKB As String) As String
        Return Me.GetDispMyNumber(strMyNumber, strJuminCD, CType(GetBangoTypeWithUserKB(strUserKB), URBangoMaskPrmClass.URKojinBangoType))
    End Function

#End Region

#Region "共通番号（表示用）取得"

    ''' <summary>
    ''' 共通番号（表示用）取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="enBangoType">番号タイプ</param>
    ''' <returns>共通番号（表示用）</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function GetDispMyNumber(
        ByVal strMyNumber As String,
        ByVal strJuminCD As String,
        ByVal enBangoType As URBangoMaskPrmClass.URKojinBangoType) As String

        Dim crBangoMaskPrm As URBangoMaskPrmClass

        crBangoMaskPrm = New URBangoMaskPrmClass
        With crBangoMaskPrm
            .p_strGyomuCD = ABConstClass.THIS_BUSINESSID
            .p_enBangoType = enBangoType
            .p_strMaskId = String.Empty
            .p_strMaskBango = strMyNumber
            .p_strJuminCd = strJuminCD
        End With
        Return GetDispMyNumber(crBangoMaskPrm)

    End Function

#End Region

#Region "共通番号（表示用）取得"

    ''' <summary>
    ''' 共通番号（表示用）取得
    ''' </summary>
    ''' <param name="crBangoMaskPrm">番号編集パラメーター</param>
    ''' <returns>共通番号（表示用）</returns>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Public Function GetDispMyNumber(
        ByVal crBangoMaskPrm As URBangoMaskPrmClass) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnWriteAccessLog As Boolean
        Dim cuPersonalData As USLPersonalDataKojinBango
        '*履歴番号 000001 2015/01/05 追加開始
        Dim enAuthLevel As UFAuthLevel
        '*履歴番号 000001 2015/01/05 追加終了

        ' 住民コードに値が存在しない場合、アクセスログを出力しない。
        ' ※住民コードと紐付けがされていない共通番号を処理する場合、アクセスログの出力は行わない方針。
        If (crBangoMaskPrm.p_strJuminCd IsNot Nothing _
            AndAlso crBangoMaskPrm.p_strJuminCd.Trim.RLength > 0) Then
            blnWriteAccessLog = True
        Else
            blnWriteAccessLog = False
        End If

        ' 共通番号を事前に整備する。
        ' マスク化桁数分の空白が存在する場合にエラーするので事前に空白を除去する。
        ' ※値なしと桁数分の空白を同様に扱うため。（業共側のエラー回避）
        If (crBangoMaskPrm.p_strMaskBango IsNot Nothing) Then
            crBangoMaskPrm.p_strMaskBango = crBangoMaskPrm.p_strMaskBango.Trim
        Else
            crBangoMaskPrm.p_strMaskBango = String.Empty
        End If

        ' 個人・法人区分を整備する（桁数による判定）
        crBangoMaskPrm.p_enBangoType = CType(GetBangoType(crBangoMaskPrm.p_strMaskBango, crBangoMaskPrm.p_enBangoType), URBangoMaskPrmClass.URKojinBangoType)

        '*履歴番号 000001 2015/01/05 追加開始
        ' 個人・法人区分判定
        Select Case crBangoMaskPrm.p_enBangoType

            Case URBangoMaskPrmClass.URKojinBangoType.KOJIN

                If (m_blnIsAfterKojinBangoRiyoKaishiYMD = False) Then
                    ' 個人番号利用開始日前の場合には、空文字を返信する。
                    Return String.Empty
                Else
                    ' 個人番号の場合、権限判定を行う。
                    enAuthLevel = Me.GetAuthLevel()
                End If

            Case URBangoMaskPrmClass.URKojinBangoType.HOJIN

                If (m_blnIsAfterHojinBangoRiyoKaishiYMD = False) Then
                    ' 法人番号利用開始日前の場合には、空文字を返信する。
                    Return String.Empty
                Else
                    ' 法人番号の場合、権限判定を行わない。
                    enAuthLevel = UFAuthLevel.W
                End If

            Case Else
                ' noop
        End Select
        '*履歴番号 000001 2015/01/05 追加終了

        '*履歴番号 000001 2015/01/05 修正開始
        'Select Case Me.GetAuthLevel()
        Select Case enAuthLevel
            '*履歴番号 000001 2015/01/05 修正終了
            Case UFAuthLevel.P
                ' P（プロテクト表示）の場合、業共側でアクセスログを出力する。
                ' ※ただしバッチから処理された場合、基盤の仕組みによりアクセスログは出力されない。
                Return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm, blnWriteAccessLog)
            Case UFAuthLevel.H
                ' H（非表示）の場合、アクセスログを出力しない。
                Return String.Empty
            Case Else

                ' 上記以外の場合、業務側でアクセスログを出力する。
                ' ※ただしバッチから処理された場合、基盤の仕組みによりアクセスログは出力されない。
                If (blnWriteAccessLog = True) Then

                    If (crBangoMaskPrm.p_strMaskBango.RLength > 0) Then

                        cuPersonalData = New USLPersonalDataKojinBango
                        With cuPersonalData
                            .p_strJuminCD = crBangoMaskPrm.p_strJuminCd
                            .p_strKojinBango = crBangoMaskPrm.p_strMaskBango
                            If (crBangoMaskPrm.p_enBangoType = URBangoMaskPrmClass.URKojinBangoType.HOJIN) Then
                                .p_enKojinBangoType = USLPersonalDataKojinBango.USLKojinBangoTypeEnum.HojinBango
                            Else
                                .p_enKojinBangoType = USLPersonalDataKojinBango.USLKojinBangoTypeEnum.KojinBango
                            End If
                        End With
                        m_cuAccessLog.ShokaiWrite(
                                        m_strClassName,
                                        THIS_METHOD_NAME,
                                        String.Empty,
                                        USLShokaiSubShubetsuEnum.SHOKAI,
                                        cuPersonalData)

                    Else
                        ' noop
                    End If

                Else
                    ' noop
                End If

                Return crBangoMaskPrm.p_strMaskBango

        End Select

    End Function

#End Region

#Region "住民コード⇒共通番号変換"

    ''' <summary>
    ''' 住民コード⇒共通番号変換
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <returns>共通番号</returns>
    ''' <remarks></remarks>
    Public Function GetMyNumber(ByVal strJuminCd As String) As String
        Return m_cMyNumberCommonB.GetMyNumber(strJuminCd)
    End Function

#End Region

#Region "共通番号⇒住民コード変換"

    ''' <summary>
    ''' 共通番号⇒住民コード変換
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <returns>住民コード配列</returns>
    ''' <remarks></remarks>
    Public Function GetJuminCd(ByVal strMyNumber As String) As String()
        Return m_cMyNumberCommonB.GetJuminCd(strMyNumber)
    End Function

    ''' <summary>
    ''' 共通番号⇒住民コード変換
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="blnCkinFG">直近フラグ</param>
    ''' <returns>住民コード配列</returns>
    ''' <remarks></remarks>
    Public Function GetJuminCd(ByVal strMyNumber As String, ByVal blnCkinFG As Boolean) As String()
        Return m_cMyNumberCommonB.GetJuminCd(strMyNumber, blnCkinFG)
    End Function

#End Region

#Region "ユーザー権限取得"

    ''' <summary>
    ''' ユーザー権限取得
    ''' </summary>
    ''' <returns>ユーザー権限</returns>
    ''' <remarks></remarks>
    Public Function GetAuthLevel() As UFAuthLevel
        '*履歴番号 000002 2015/01/09 追加開始
        Try
            Return m_cfUserInfo.GetBangoAuth(m_cfControlData.m_strUserId, m_cfControlData.m_strBusinessId)
        Catch csExp As Exception
            Throw
        End Try
        '*履歴番号 000002 2015/01/09 追加終了
    End Function

#End Region

#Region "番号タイプ取得"


    ''' <summary>
    ''' 番号タイプ取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strAtenaDataKB">宛名データ区分</param>
    ''' <returns>番号タイプ</returns>
    ''' <remarks>桁数により番号タイプを判定し、返信します。</remarks>
    Public Function GetBangoTypeWithAtenaDataKB(
        ByVal strMyNumber As String,
        ByVal strAtenaDataKB As String) As Integer
        Return GetBangoType(strMyNumber, GetBangoTypeWithAtenaDataKB(strAtenaDataKB))
    End Function

#End Region

#Region "番号タイプ取得"

    ''' <summary>
    ''' 番号タイプ取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strUserKB">ユーザー区分</param>
    ''' <returns>番号タイプ</returns>
    ''' <remarks>桁数により番号タイプを判定し、返信します。</remarks>
    Public Function GetBangoTypeWithUserKB(
        ByVal strMyNumber As String,
        ByVal strUserKB As String) As Integer
        Return GetBangoType(strMyNumber, GetBangoTypeWithUserKB(strUserKB))
    End Function

#End Region

#Region "番号タイプ取得"

    ''' <summary>
    ''' 番号タイプ取得
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="intBangoType">番号タイプ</param>
    ''' <returns>番号タイプ</returns>
    ''' <remarks>桁数により番号タイプを判定し、返信します。</remarks>
    Public Function GetBangoType(
        ByVal strMyNumber As String,
        ByVal intBangoType As Integer) As Integer

        Dim intResult As Integer
        Dim strMyNumberWork As String

        If (strMyNumber Is Nothing) Then
            strMyNumberWork = String.Empty
        Else
            strMyNumberWork = strMyNumber.Trim
        End If

        Select Case strMyNumberWork.RLength
            Case ABConstClass.MYNUMBER.LENGTH.KOJIN
                ' 12桁の場合、個人番号と判定
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN
            Case ABConstClass.MYNUMBER.LENGTH.HOJIN
                ' 13桁の場合、法人番号と判定
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN
            Case Else
                ' 上記以外の場合、指定された番号タイプに従う
                intResult = intBangoType
        End Select

        Return intResult

    End Function

#End Region

#Region "番号タイプ取得"

    ''' <summary>
    ''' 番号タイプ取得
    ''' </summary>
    ''' <param name="strAtenaDataKB">宛名データ区分</param>
    ''' <returns>番号タイプ</returns>
    ''' <remarks>宛名データ区分により番号タイプを判定し、返信します。</remarks>
    Private Function GetBangoTypeWithAtenaDataKB(
        ByVal strAtenaDataKB As String) As Integer

        Dim intResult As Integer
        Dim strAtenaDataKBWork As String

        If (strAtenaDataKB Is Nothing) Then
            strAtenaDataKBWork = String.Empty
        Else
            strAtenaDataKBWork = strAtenaDataKB.Trim
        End If

        ' "11"（住登内個人）　→　個人
        ' "12"（住登外個人）　→　個人
        ' "20"（法人）　　　　→　法人
        ' "30"（共有）      　→　個人（共有は個人扱い ※既存に準拠）
        ' 上記以外（不明）    →　法人（宛名データ区分が不明の場合、法人として扱う ※桁数が法人の方が多い為）
        Select Case strAtenaDataKBWork
            Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN,
                 ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN
            Case ABConstClass.ATENADATAKB_HOJIN
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN
            Case ABConstClass.ATENADATAKB_KYOYU
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN
            Case Else
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN
        End Select

        Return intResult

    End Function

#End Region

#Region "番号タイプ取得"

    ''' <summary>
    ''' 番号タイプ取得
    ''' </summary>
    ''' <param name="strUserKB">ユーザー区分</param>
    ''' <returns>番号タイプ</returns>
    ''' <remarks>ユーザー区分により番号タイプを判定し、返信します。</remarks>
    Private Function GetBangoTypeWithUserKB(
        ByVal strUserKB As String) As Integer

        Dim intResult As Integer
        Dim strUserKBWork As String

        If (strUserKB Is Nothing) Then
            strUserKBWork = String.Empty
        Else
            strUserKBWork = strUserKB.Trim
        End If

        ' "2"（個人）　　　→　個人
        ' "1"（法人）　　　→　法人
        ' 上記以外（不明） →　法人（ユーザー区分が不明の場合、法人として扱う ※桁数が法人の方が多い為）
        Select Case strUserKBWork
            Case ABConstClass.eLTAX.USERKB.KOJIN
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN
            Case ABConstClass.eLTAX.USERKB.HOJIN
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN
            Case Else
                intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN
        End Select

        Return intResult

    End Function

#End Region

#End Region

End Class
