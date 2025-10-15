'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        法人個人(ABKjnhjnKBBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2002/12/17　山崎　敏生
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

Public Class ABKjnhjnKBBClass
    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass            'ログ出力クラス
    Private m_cfUFControlData As UFControlData      'コントロールデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKjnhjnKBBClass"

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData AS UFControlData,
    '*         　　　　               ByVal cfConfigData  AS UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigDataClass As UFConfigDataClass)
        'メンバ変数セット
        m_cfUFControlData = cfControlData
        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)
    End Sub

    '************************************************************************************************
    '* メソッド名      個人法人取得
    '* 
    '* 構文            Public Function GetKjnhjn(strKjnhjnKB As String) As String
    '* 
    '* 機能　　        区分より管内管外名称を取得
    '* 
    '* 引数            strKjnhjnKB As String   :個人法人区分
    '* 
    '* 戻り値          個人法人名称
    '************************************************************************************************
    Public Function GetKjnhjn(ByVal strKjnhjnKB As String) As String
        Dim strMeisho As String = String.Empty
        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn")

            Select Case strKjnhjnKB
                Case "1"
                    strMeisho = "個人"
                Case "2"
                    strMeisho = "法人"
            End Select

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn")
        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKjnhjn】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return strMeisho
    End Function

    '************************************************************************************************
    '* メソッド名      個人法人編集
    '* 
    '* 構文            Public Function HenKangaiKangai() As DataSet
    '* 
    '* 機能　　        個人法人のコードと名称を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          個人法人名称（DataSet）
    '*                   構造：csKjnHjnData    インテリセンス：ABKjnHjnData
    '************************************************************************************************
    Public Function HenKangaiKangai() As DataSet
        Dim csKjnHjnData As New DataSet()
        Dim csKjnHjnDataTbl As DataTable
        Dim csKjnHjnDataRow As DataRow

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai")

            'テーブルを作成する
            csKjnHjnDataTbl = csKjnHjnData.Tables.Add(ABKjnHjnData.TABLE_NAME)

            'テーブル配下に必要フィールドを用意する
            csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKB, System.Type.GetType("System.String"))
            csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKBMEI, System.Type.GetType("System.String"))

            '各フィールドにデータを格納する
            '個人法人区分 = 1
            csKjnHjnDataRow = csKjnHjnDataTbl.NewRow()
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "1"
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "個人"
            'データの追加
            csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow)

            '個人法人区分 = 2
            csKjnHjnDataRow = csKjnHjnDataTbl.NewRow()
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "2"
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "法人"
            'データの追加
            csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow)

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai")
        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenKangaiKangai】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return csKjnHjnData
    End Function

End Class
