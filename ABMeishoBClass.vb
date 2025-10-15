'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        名称(ABMeishoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/07/25　滝沢　欽也
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
Imports System.Text

Public Class ABMeishoBClass
    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass            'ログ出力クラス
    Private m_cfUFControlData As UFControlData      'コントロールデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABMeishoBClass"

    'パラメータのメンバ変数
    Private m_strKeitaiFuyoKB As String                     ' 区分（1桁）
    Private m_strKeitaiSeiRyakuKB As String                 ' 区分（1桁）
    Private m_strKanjiHjnKeitai As String                   ' 形態（全角　Max１０文字）
    Private m_strKanjiMeisho1 As String                     ' 名称（全角　Max４０文字）
    Private m_strKanjiMeisho2 As String                     ' 名称（全角　Max４０文字）
    Private m_strAtenaDataKB As String                      ' 宛名データ区分
    Private m_cHojinMeishoBClass As ABHojinMeishoBClass     ' 法人名称クラス

    '各メンバ変数のプロパティ定義
    Public WriteOnly Property p_strKeitaiFuyoKB() As String
        Set(ByVal Value As String)
            m_strKeitaiFuyoKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strKeitaiSeiRyakuKB() As String
        Set(ByVal Value As String)
            m_strKeitaiSeiRyakuKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiHjnKeitai() As String
        Set(ByVal Value As String)
            m_strKanjiHjnKeitai = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiMeisho1() As String
        Set(ByVal Value As String)
            m_strKanjiMeisho1 = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiMeisho2() As String
        Set(ByVal Value As String)
            m_strKanjiMeisho2 = Value
        End Set
    End Property
    Public WriteOnly Property p_strAtenaDataKB() As String
        Set(ByVal Value As String)
            m_strAtenaDataKB = Value
        End Set
    End Property

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)

        ' メンバ変数セット
        m_cfUFControlData = cfControlData

        ' ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        ' 法人名称クラスのインスタンス作成
        m_cHojinMeishoBClass = New ABHojinMeishoBClass(cfControlData, cfConfigData)

        ' パラメータのメンバ変数
        m_strKeitaiFuyoKB = String.Empty
        m_strKeitaiSeiRyakuKB = String.Empty
        m_strKanjiHjnKeitai = String.Empty
        m_strKanjiMeisho1 = String.Empty
        m_strKanjiMeisho2 = String.Empty
        m_strAtenaDataKB = String.Empty
    End Sub

    '************************************************************************************************
    '* メソッド名      名称編集
    '* 
    '* 構文            Public Function GetMeisho() As String
    '* 
    '* 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
    '* 
    '* 引数            名称
    '* 
    '* 戻り値          編集名称（String）
    '************************************************************************************************
    Public Overloads Function GetMeisho() As String
        Const THIS_METHOD_NAME As String = "GetHojinMeisho"
        Dim strKanjiMeisho As String = String.Empty

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case m_strAtenaDataKB
                Case ABConstClass.ATENADATAKB_HOJIN
                    '法人の名称の編集
                    m_cHojinMeishoBClass.p_strKeitaiFuyoKB = m_strKeitaiFuyoKB
                    m_cHojinMeishoBClass.p_strKeitaiSeiRyakuKB = m_strKeitaiSeiRyakuKB
                    m_cHojinMeishoBClass.p_strKanjiHjnKeitai = m_strKanjiHjnKeitai
                    m_cHojinMeishoBClass.p_strKanjiMeisho1 = m_strKanjiMeisho1
                    m_cHojinMeishoBClass.p_strKanjiMeisho2 = m_strKanjiMeisho2
                    strKanjiMeisho = m_cHojinMeishoBClass.GetHojinMeisho
                Case Else
                    strKanjiMeisho = m_strKanjiMeisho1
            End Select

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception

            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:" + THIS_METHOD_NAME + "】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return strKanjiMeisho

    End Function

    '************************************************************************************************
    '* メソッド名      名称編集
    '* 
    '* 構文            Public Function GetHojinMeisho(ByVal cABHojinMeishoParaX() As ABHojinMeishoParaXClass) As String()
    '* 
    '* 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
    '* 
    '* 引数            名称パラメータクラス   : ABMeishoParaXClass[]
    '* 
    '* 戻り値          編集名称（String[]）
    '************************************************************************************************
    Public Overloads Function GetMeisho(ByVal cABMeishoParaX() As ABMeishoParaXClass) As String()
        Const THIS_METHOD_NAME As String = "GetHojinMeisho"
        Dim strKanjiMeisho() As String
        Dim intIndex As Integer

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ReDim strKanjiMeisho(UBound(cABMeishoParaX))
            For intIndex = 0 To UBound(cABMeishoParaX)
                With cABMeishoParaX(intIndex)
                    m_strKeitaiFuyoKB = .p_strKeitaiFuyoKB
                    m_strKeitaiSeiRyakuKB = .p_strKeitaiSeiRyakuKB
                    m_strKanjiHjnKeitai = .p_strKanjiHjnKeitai
                    m_strKanjiMeisho1 = .p_strKanjiMeisho1
                    m_strKanjiMeisho2 = .p_strKanjiMeisho2
                    m_strAtenaDataKB = .p_strAtenaDataKB
                End With
                strKanjiMeisho(intIndex) = Me.GetMeisho
            Next intIndex

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:" + THIS_METHOD_NAME + "】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return strKanjiMeisho

    End Function
End Class
