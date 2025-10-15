'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ共通キャッシュビジネスクラス(ABCommonCacheBClass)
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
Imports System.Web.UI
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common
Imports Densan.Reams.UR.UR010BX
Imports Densan.Reams.UR.UR010BB
Imports System.Security

''' <summary>
''' ＡＢ共通キャッシュビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABCommonCacheBClass
    Inherits ABCommonBClass

#Region "メンバー変数"

    ' コンスタント定義
    Protected Shadows Const THIS_CLASS_NAME As String = "ABCommonCacheBClass"              ' クラス名

#End Region

#Region "コンストラクター"

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

        ' 基底クラスのコンストラクター呼び出し
        MyBase.New(THIS_CLASS_NAME)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' 施行日判定ビジネスクラスのインスタンス化
        Try
            m_crSekoYMDHanteiB = New URSekoYMDHanteiCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_cfControlData.m_strBusinessId)
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

End Class
