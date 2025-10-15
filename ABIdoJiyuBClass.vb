'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        異動事由(ABIdoJiyuBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/04/01　滝沢　欽也
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

Public Class ABIdoJiyuBClass

    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABIdoJiyuBClass"

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                  ByVal cfConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '*                   cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

    End Sub

    '************************************************************************************************
    '* メソッド名      異動事由取得
    '* 
    '* 構文            Public Sub GetIdoJiyu(ByVal strAtenaDataKB As String,
    '*                                         ByVal strAtenaDataSHU As String)
    '* 
    '* 機能　　        宛名データ区分、宛名データ種別より名称を編集する
    '* 
    '* 引数            strIdoJiyuCD As String   : 異動事由コード
    '* 
    '* 戻り値          異動事由(String)
    '************************************************************************************************
    Public Function GetIdoJiyu(ByVal strIdoJiyuCD As String) As String
        Const THIS_METHOD_NAME As String = "GetIdoJiyu"
        Dim strIdoJiyu As String

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strIdoJiyuCD
                Case "001", "01"
                    strIdoJiyu = "削除"
                Case "002", "02"
                    strIdoJiyu = "追加"
                Case "010", "10"
                    strIdoJiyu = "転入"
                Case "011", "11"
                    strIdoJiyu = "出生"
                Case "012", "12"
                    strIdoJiyu = "職権記載"
                Case "013", "13"
                    strIdoJiyu = "帰化"
                Case "014", "14"
                    strIdoJiyu = "国籍取得"
                Case "015", "15"
                    strIdoJiyu = "回復"
                Case "020", "20"
                    strIdoJiyu = "転出"
                Case "021", "21"
                    strIdoJiyu = "死亡"
                Case "022", "22"
                    strIdoJiyu = "職権消除"
                Case "023", "23"
                    strIdoJiyu = "国籍喪失"
                Case "024", "24"
                    strIdoJiyu = "失踪"
                Case Else
                    strIdoJiyu = ""
            End Select

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return strIdoJiyu

    End Function

End Class
