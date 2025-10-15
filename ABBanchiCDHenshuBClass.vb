'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         番地コード編集Ｂクラス(ABBanchiCDHenshuBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2009/04/07  工藤　美芙由
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

Public Class ABBanchiCDHenshuBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass                            ' ログ出力クラス
    Private m_cfUFControlData As UFControlData                      ' コントロールデータ
    Private m_cfUFConfigDataClass As UFConfigDataClass              ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                              ' ＲＤＢクラス
    Private m_crBanchiCdMstB As URBANCHICDMSTBClass                 ' ＵＲ番地コードマスタクラス
    Private m_cfErrorClass As UFErrorClass                          ' エラー処理クラス

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABBanchiCDHenshuBClass"
    Private Const THIS_BUSINESSID As String = "AB"


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
    <SecuritySafeCritical>
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfConfigDataClass
        m_cfRdbClass = New UFRdbClass(m_cfUFControlData.m_strBusinessId)

        ' ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId)

        ' ＵＲ番地コードマスタクラスのインスタンス化
        If (m_crBanchiCdMstB Is Nothing) Then
            m_crBanchiCdMstB = New URBANCHICDMSTBClass(cfControlData, cfConfigDataClass, m_cfRdbClass)
        End If

    End Sub
#End Region

#Region "メソッド"

#Region "CreateBanchiCD:番地コード編集"
    '**********************************************************************************************************************
    '* メソッド名     番地コード編集
    '* 
    '* 構文           Public Function CreateBanchiCD(ByVal strBanchi As String) As String()
    '* 
    '* 機能           番地から番地コード１～３を編集する
    '* 
    '* 引数           strBanchi     番地
    '*
    '* 戻り値         String()      編集した番地コード配列
    '*
    '**********************************************************************************************************************
    <SecuritySafeCritical>
    Public Function CreateBanchiCD(ByVal strBanchi As String) As String()
        Dim THIS_METHOD_NAME As String = "CreateBanchiCD"
        Dim strBanchiCD(2) As String                        ' 番地コード配列（取得用）
        Dim strRetBanchiCD(2) As String                     ' 番地コード配列（戻り値用）
        Dim strMotoBanchiCD() As String                     ' 変更前番地コード
        Dim intLoop As Integer                              ' ループカウンタ

        Try

            ' 番地コード取得
            strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(strBanchi, strMotoBanchiCD, True)

            For intLoop = 0 To strBanchiCD.Length - 1
                If (IsNothing(strBanchiCD(intLoop))) Then
                    ' 取得した番地コード配列にNothingがある場合はString.Emptyをセット
                    strBanchiCD(intLoop) = String.Empty
                End If

                '番地コードを右詰する（5桁に満たない場合は半角スペースを左詰）
                strRetBanchiCD(intLoop) = strBanchiCD(intLoop).Trim.RPadLeft(5, " "c)
            Next

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

        Return strRetBanchiCD

    End Function
#End Region

#End Region

End Class
