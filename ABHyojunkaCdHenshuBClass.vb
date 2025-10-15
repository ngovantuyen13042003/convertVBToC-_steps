'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         標準化コード編集Ｂクラス(ABHyojunkaCdHenshuBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/03/13  仲西　勝
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

Public Class ABHyojunkaCdHenshuBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                ' コンフィグデータ
    Private m_cfLogClass As UFLogClass                              ' ログ出力クラス

    'パラメータのメンバ変数
    Private m_strJuminKbn As String                                 '住民区分
    Private m_strJuminShubetsu As String                            '住民種別
    Private m_strJuminJotai As String                               '住民状態

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABHyojunkaCdHenshuBClass"

    '各メンバ変数のプロパティ定義
    Public ReadOnly Property p_strJuminKbn() As String
        Get
            Return m_strJuminKbn
        End Get
    End Property
    Public ReadOnly Property p_strJuminShubetsu() As String
        Get
            Return m_strJuminShubetsu
        End Get
    End Property
    Public ReadOnly Property p_strJuminJotai() As String
        Get
            Return m_strJuminJotai
        End Get
    End Property

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* 　　                          ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        'パラメータのメンバ変数
        m_strJuminKbn = String.Empty
        m_strJuminShubetsu = String.Empty
        m_strJuminJotai = String.Empty

    End Sub
#End Region

#Region "メソッド"

#Region "HenshuHyojunkaCd:標準化コード編集"
    '**********************************************************************************************************************
    '* メソッド名     標準化コード編集
    '* 
    '* 構文           Public Sub HenshuHyojunkaCd(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
    '* 
    '* 機能           各コードを標準化準拠に準ずる体系に編集する
    '* 
    '* 引数           strAtenaDataKB     宛名データ区分
    '*                strAtenaDataSHU    宛名データ種別
    '*
    '* 戻り値         なし
    '*
    '**********************************************************************************************************************
    Public Sub HenshuHyojunkaCd(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
        Dim THIS_METHOD_NAME As String = "HenshuHyojunkaCd"

        Try
            m_strJuminKbn = GetJuminKbn(strAtenaDataKB)
            m_strJuminShubetsu = GetJuminShubetsu(strAtenaDataKB, strAtenaDataSHU)
            m_strJuminJotai = GetJuminJotai(strAtenaDataKB, strAtenaDataSHU)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            Throw

        End Try

    End Sub
#End Region

#Region "GetJuminKbn:住民区分取得"
    '**********************************************************************************************************************
    '* メソッド名     住民区分取得
    '* 
    '* 構文           Private Function GetJuminKbn(ByVal strAtenaDataKB As String) As String
    '* 
    '* 機能           標準化準拠のコード体系に準ずる住民区分を返却する
    '* 
    '* 引数           strAtenaDataKB     宛名データ区分
    '*
    '* 戻り値         String             住民区分
    '*
    '**********************************************************************************************************************
    Private Function GetJuminKbn(ByVal strAtenaDataKB As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminKbn"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    '住民
                    strRet = "1"
                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    '住登外
                    strRet = "2"
                Case ABConstClass.ATENADATAKB_HOJIN
                    '法人
                    strRet = "3"
                Case Else
                    '以外の場合、空白を設定
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#Region "GetJuminShubetsu:住民種別取得"
    '**********************************************************************************************************************
    '* メソッド名     住民種別取得
    '* 
    '* 構文           Private Function GetJuminShubetsu(ByVal strAtenaDataKB As String) As String
    '* 
    '* 機能           標準化準拠のコード体系に準ずる住民種別を返却する
    '* 
    '* 引数           strAtenaDataKB     宛名データ区分
    '*                strAtenaDataSHU    宛名データ種別
    '*
    '* 戻り値         String             住民種別
    '*
    '**********************************************************************************************************************
    Private Function GetJuminShubetsu(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminShubetsu"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN,
                     ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    If (strAtenaDataSHU.Trim.RLength > 0) AndAlso (strAtenaDataSHU.Trim.RSubstring(0, 1) = "1") Then
                        '日本人
                        strRet = "1"
                    ElseIf (strAtenaDataSHU.Trim.RLength > 0) AndAlso (strAtenaDataSHU.Trim.RSubstring(0, 1) = "2") Then
                        '外国人
                        strRet = "2"
                    Else
                        '以外の場合、空白を設定
                        strRet = String.Empty
                    End If
                Case Else
                    '以外の場合、空白を設定
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#Region "GetJuminJotai:住民状態取得"
    '**********************************************************************************************************************
    '* メソッド名     住民状態取得
    '* 
    '* 構文           Private Function GetJuminJotai(ByVal strAtenaDataKB As String) As String
    '* 
    '* 機能           標準化準拠のコード体系に準ずる住民状態を返却する
    '* 
    '* 引数           strAtenaDataKB     宛名データ区分
    '*                strAtenaDataSHU    宛名データ種別
    '*
    '* 戻り値         String             住民状態
    '*
    '**********************************************************************************************************************
    Private Function GetJuminJotai(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminJotai"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    Select Case strAtenaDataSHU
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
                            '住登者
                            strRet = "1"
                        Case ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU
                            '転出者
                            strRet = "2"
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            '死亡者
                            strRet = "3"
                        Case Else
                            'その他消除者
                            strRet = "9"
                    End Select

                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    Select Case strAtenaDataSHU
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
                            '住登外者
                            strRet = "1"
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            '死亡者
                            strRet = "2"
                        Case Else
                            'その他消除者
                            strRet = "9"
                    End Select

                Case Else
                    '以外の場合、空白を設定
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#End Region

End Class
