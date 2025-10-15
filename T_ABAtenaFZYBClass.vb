'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名付随マスタテスト用
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2011/10/24　小松　知尚
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text

Public Class T_ABAtenaFZYBClass
#Region "メンバ変数"
    'パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "T_ABAtenaFZYBClass"                ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード

    Public m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.SelectAll '全項目選択（m_blnAtenaGetがTrueの時宛名Getで必要な項目全てそれ以外はSELECT *）
    Public m_strJukihoKaiseiKB As String = String.Empty

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "メソッド"
    Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
                                              ByVal cSearchKey As ABAtenaSearchKey, _
                                              ByVal strKikanYMD As String, _
                                              ByVal strJuminJutogaiKB As String, _
                                              ByVal blnSakujoFG As Boolean) As DataSet
        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaRBHoshu(intGetCount, cSearchKey, strKikanYMD, strJuminJutogaiKB, blnSakujoFG)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function

    Public Function GetAtenaBKobetsu(ByVal intGetCount As Integer, _
                                     ByVal cSearchKey As ABAtenaSearchKey, _
                                     ByVal blnSakujoFG As Boolean, _
                                     ByVal strKobetsuKB As String) As DataSet
        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaBKobetsu(intGetCount, cSearchKey, blnSakujoFG, strKobetsuKB)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function


    Public Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
                                      ByVal cSearchKey As ABAtenaSearchKey, _
                                      ByVal strKikanYMD As String, _
                                      ByVal blnSakujoFG As Boolean, _
                                      ByVal strKobetsuKB As String) As DataSet

        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaRBKobetsu(intGetCount, cSearchKey, strKikanYMD, blnSakujoFG, strKobetsuKB)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function

#End Region

End Class
