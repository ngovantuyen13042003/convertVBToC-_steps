'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        ＡＢ宛名＿生年月日編集
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/24　芳沢　昇
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/06/27 000001     変換元の値がSteing.Emptyの場合エラーするバグを修正
'* 2023/03/10 000002     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
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
Imports System.Text

Public Class ABUmareHenshuBClass
    '************************************************************************************************
    '*
    '* 生年月日編集に使用するパラメータクラス
    '*
    '************************************************************************************************
    'パラメータのメンバ変数
    Private m_cfUFLogClass As UFLogClass                'ログ出力クラス
    Private m_cfUFControlData As UFControlData          'コントロールデータ
    Private m_cfUFConfigDataClass As UFConfigDataClass  'コンフィグデータ

    Private m_strDataKB As String                       '区分(2桁)
    Private m_strJuminSHU As String                     '種別(2桁)
    Private m_strUmareYMD As String                     '生年月日
    Private m_strUmareWMD As String                     '生和暦年月日
    Private m_strHyojiUmareYMD As String                '表示用生年月日
    Private m_strShomeiUmareYMD As String               '証明用生年月日
    Private m_cfDateClass As UFDateClass                '日付編集 

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABUmareHenshuBClass"             'クラス名

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData, _
                   ByVal cfUFConfigDataClass As UFConfigDataClass)
        'メンバ変数セット
        m_cfUFControlData = cfUFControlData
        m_cfUFConfigDataClass = cfUFConfigDataClass

        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfUFConfigDataClass, cfUFControlData.m_strBusinessId)

        'パラメータのメンバ変数初期化
        m_strDataKB = String.Empty
        m_strJuminSHU = String.Empty
        m_strUmareYMD = String.Empty
        m_strUmareWMD = String.Empty
        m_strHyojiUmareYMD = String.Empty
        m_strShomeiUmareYMD = String.Empty
        ' 日付処理クラスインスタンス化
        m_cfDateClass = New UFDateClass(m_cfUFConfigDataClass)

    End Sub

    '************************************************************************************************
    '* メソッド名      生年月日編集
    '* 
    '* 構文           Public Sub HenshuUmare()
    '* 
    '* 機能　　       生年月日・生和暦年月日より表示用生年月日・証明用年月日を編集する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub HenshuUmare()
        Dim strNengo As String = String.Empty
        Dim strUmareYmd As String = String.Empty

        Try


            ' 和暦１文字目を取得
            strNengo = m_strUmareWMD.RSubstring(0, 1)
            If ((strNengo = "0") Or (strNengo = "8") Or (strNengo = "9")) Then
                If (m_strUmareYMD.Trim() = "") Then
                    Select Case (strNengo)
                        Case "0"
                            strUmareYmd = "20" + m_strUmareWMD.RSubstring(1)
                        Case "8"
                            strUmareYmd = "18" + m_strUmareWMD.RSubstring(1)
                        Case "9"
                            strUmareYmd = "19" + m_strUmareWMD.RSubstring(1)
                        Case Else
                            strUmareYmd = "20" + m_strUmareWMD.RSubstring(1)
                    End Select
                    m_cfDateClass.p_strDateValue = strUmareYmd
                Else
                    m_cfDateClass.p_strDateValue = m_strUmareYMD
                End If

                If (Not m_cfDateClass.CheckDate()) Then
                    m_strHyojiUmareYMD = String.Empty
                    m_strShomeiUmareYMD = String.Empty
                    Exit Try
                End If

                ' 生年月日より表示用日付の編集を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period
                m_cfDateClass.p_blnWideType = False
                m_cfDateClass.p_enDateFillType = UFDateFillType.Zero
                m_strHyojiUmareYMD = m_cfDateClass.p_strSeirekiYMD

                ' 生年月日より証明用日付の編集を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese
                m_cfDateClass.p_blnWideType = True
                m_cfDateClass.p_enEraType = UFEraType.Kanji
                m_cfDateClass.p_enDateFillType = UFDateFillType.Blank
                m_strShomeiUmareYMD = m_cfDateClass.p_strSeirekiYMD
            Else
                ' 生和暦年月日より表示用日付の編集を行う
                m_cfDateClass.p_strDateValue = m_strUmareWMD

                If (Not m_cfDateClass.CheckDate()) Then
                    m_strHyojiUmareYMD = String.Empty
                    m_strShomeiUmareYMD = String.Empty
                    Exit Try
                End If

                m_cfDateClass.p_blnWideType = False
                m_cfDateClass.p_enEraType = UFEraType.KanjiRyaku
                m_cfDateClass.p_enDateFillType = UFDateFillType.Zero
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period
                m_strHyojiUmareYMD = m_cfDateClass.p_strWarekiYMD

                ' 生和暦年月日より証明用日付の編集を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese
                m_cfDateClass.p_blnWideType = True
                m_cfDateClass.p_enEraType = UFEraType.Kanji
                m_cfDateClass.p_enDateFillType = UFDateFillType.Blank
                m_strShomeiUmareYMD = m_cfDateClass.p_strWarekiYMD
            End If
        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenshuUmare】【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* 各メンバ変数のプロパティ定義
    '************************************************************************************************
    Public WriteOnly Property p_strDataKB() As String
        Set(ByVal Value As String)
            m_strDataKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strJuminSHU() As String
        Set(ByVal Value As String)
            m_strJuminSHU = Value
        End Set
    End Property
    Public WriteOnly Property p_strUmareYMD() As String
        Set(ByVal Value As String)
            '* 履歴番号 000001 2003/06/27 修正開始
            'm_strUmareYMD = Value
            m_strUmareYMD = Value.RPadRight(8)
            '* 履歴番号 000001 2003/06/27 修正終了
        End Set
    End Property
    Public WriteOnly Property p_strUmareWMD() As String
        Set(ByVal Value As String)
            '* 履歴番号 000001 2003/06/27 修正開始
            'm_strUmareWMD = Value
            m_strUmareWMD = Value.RPadRight(7)
            '* 履歴番号 000001 2003/06/27 修正終了
        End Set
    End Property
    Public ReadOnly Property p_strHyojiUmareYMD() As String
        Get
            Return m_strHyojiUmareYMD
        End Get
    End Property
    Public ReadOnly Property p_strShomeiUmareYMD() As String
        Get
            Return m_strShomeiUmareYMD
        End Get
    End Property

End Class
