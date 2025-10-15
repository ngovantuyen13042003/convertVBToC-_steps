'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        法人名称(ABHojinMeishoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2002/12/18　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/09/11 000001     チューニング
'* 2015/04/23 000002     支店名の連結時に値有無判定を追加（石合）
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

Public Class ABHojinMeishoBClass
    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass            'ログ出力クラス
    Private m_cfUFControlData As UFControlData      'コントロールデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABHojinMeishoBClass"

    'パラメータのメンバ変数
    Private m_strKeitaiFuyoKB As String             '区分（1桁）
    Private m_strKeitaiSeiRyakuKB As String         '区分（1桁）
    Private m_strKanjiHjnKeitai As String           '形態（全角　Max１０文字）
    Private m_strKanjiMeisho1 As String             '名称（全角　Max４０文字）
    Private m_strKanjiMeisho2 As String             '名称（全角　Max４０文字）

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
        'メンバ変数セット
        m_cfUFControlData = cfControlData
        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)
        'パラメータのメンバ変数
        m_strKeitaiFuyoKB = String.Empty
        m_strKeitaiSeiRyakuKB = String.Empty
        m_strKanjiHjnKeitai = String.Empty
        m_strKanjiMeisho1 = String.Empty
        m_strKanjiMeisho2 = String.Empty
    End Sub

    '************************************************************************************************
    '* メソッド名      法人名称編集
    '* 
    '* 構文            Public Function GetHojinMeisho() As String
    '* 
    '* 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          編集名称（String）
    '************************************************************************************************
    Public Function GetHojinMeisho() As String
        '*履歴番号 000001 2003/09/11 修正開始
        'Dim strKanjiMeisho As String = String.Empty

        'Try
        '    'デバッグ開始ログ出力
        '    m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")

        '    '法人の名称の編集
        '    Select Case m_strKeitaiFuyoKB
        '        Case "1"
        '            Select Case m_strKeitaiSeiRyakuKB
        '                Case "1"
        '                    strKanjiMeisho = m_strKanjiHjnKeitai + m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
        '                Case Else
        '                    strKanjiMeisho = m_strKanjiHjnKeitai + "　" + m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
        '            End Select
        '        Case "2"
        '            Select Case m_strKeitaiSeiRyakuKB
        '                Case "1"
        '                    strKanjiMeisho = m_strKanjiMeisho1 + m_strKanjiHjnKeitai + m_strKanjiMeisho2
        '                Case Else
        '                    strKanjiMeisho = m_strKanjiMeisho1 + "　" + m_strKanjiHjnKeitai + "　" + m_strKanjiMeisho2
        '            End Select
        '        Case Else
        '            strKanjiMeisho = m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
        '    End Select

        '    'デバッグ終了ログ出力
        '    m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")
        'Catch objExp As Exception
        '    'エラーログ出力
        '    m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKjnhjn】【エラー内容:" + objExp.Message + "】")
        '    'エラーをそのままスローする
        '    Throw objExp
        'End Try

        'Return strKanjiMeisho

        Dim strKanjiMeisho As StringBuilder
        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            strKanjiMeisho = New StringBuilder()
            '法人の名称の編集
            Select Case m_strKeitaiFuyoKB
                Case "1"
                    Select Case m_strKeitaiSeiRyakuKB
                        Case "1"
                            '*履歴番号 000002 2015/04/23 修正開始
                            'strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*履歴番号 000002 2015/04/23 修正終了
                        Case Else
                            '*履歴番号 000002 2015/04/23 修正開始
                            'strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho1)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*履歴番号 000002 2015/04/23 修正終了
                    End Select
                Case "2"
                    Select Case m_strKeitaiSeiRyakuKB
                        Case "1"
                            strKanjiMeisho.Append(m_strKanjiMeisho1).Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho2)
                        Case Else
                            '*履歴番号 000002 2015/04/23 修正開始
                            'strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiHjnKeitai)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*履歴番号 000002 2015/04/23 修正終了
                    End Select
                Case Else
                    '*履歴番号 000002 2015/04/23 修正開始
                    'strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                    strKanjiMeisho.Append(m_strKanjiMeisho1)
                    strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                    '*履歴番号 000002 2015/04/23 修正終了
            End Select

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Catch objExp As Exception
            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try


        Return strKanjiMeisho.ToString
        '*履歴番号 000001 2003/09/11 修正終了

    End Function

    '*履歴番号 000002 2015/04/23 追加開始
    ''' <summary>
    ''' 法人名（および法人形態）に支店名を連結して返信します。
    ''' </summary>
    ''' <param name="csHojinmei">法人名（および法人形態）</param>
    ''' <param name="strShitenmei">支店名</param>
    ''' <returns></returns>
    ''' <remarks>値有無判定、および設定値の前後空白は除去しない。</remarks>
    Private Function AppendShitenmei( _
        ByVal csHojinmei As StringBuilder, _
        ByVal strShitenmei As String) As StringBuilder

        Try

            With csHojinmei

                ' 支店名が存在する場合に、全角空白＋支店名を連結する。
                If (strShitenmei.RLength > 0) Then
                    .Append("　")
                    .Append(strShitenmei)
                Else
                    ' noop
                End If

            End With

        Catch csExp As Exception
            Throw
        End Try

        Return csHojinmei

    End Function
    '*履歴番号 000002 2015/04/23 追加終了

End Class
