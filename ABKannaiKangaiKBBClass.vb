'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        管内管外(ABKannaiKangaiKBBClass)
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

Public Class ABKannaiKangaiKBBClass
    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass                    'ログ出力クラス
    Private m_cfUFControlData As UFControlData              'コントロールデータ
    Private m_cfUFConfigDataClass As UFConfigDataClass      'コンフィグデータ

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABJuminShubetsuBClass"

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
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfUFConfigDataClass As UFConfigDataClass)

        'メンバ変数セット
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfUFConfigDataClass

        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfUFConfigDataClass, cfControlData.m_strBusinessId)
    End Sub

    '************************************************************************************************
    '* メソッド名      管内管外取得
    '* 
    '* 構文            Public Function GetKannaiKangai(strKannaiKangaiKB As String) As String
    '* 
    '* 機能　　        区分より管内管外名称を取得
    '* 
    '* 引数            strKannaiKangaiKB As String   :管内管外区分
    '* 
    '* 戻り値          管内管外名称
    '************************************************************************************************
    Public Function GetKannaiKangai(ByVal strKannaiKangaiKB As String) As String
        Dim strMeisho As String = String.Empty
        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai")

            Select Case strKannaiKangaiKB
                Case "1"
                    strMeisho = "管内"
                Case "2"
                    strMeisho = "管外"
            End Select

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai")

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKannaiKangai】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return strMeisho
    End Function

    '************************************************************************************************
    '* メソッド名      管内管外編集
    '* 
    '* 構文            Public Function HenKannaiKangai() As DataSet
    '* 
    '* 機能　　        管内管外のコードと名称を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          管内管外名称（DataSet）
    '*                   構造：csKannaiKangaiData    インテリセンス：ABKannaiKangaiData
    '************************************************************************************************
    Public Function HenKannaiKangai() As DataSet
        Dim csKannaiKangaiData As New DataSet()
        Dim csKannaiKangaiDataTbl As DataTable
        Dim csKannaiKangaiDataRow As DataRow

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai")

            'テーブルを作成する
            csKannaiKangaiDataTbl = csKannaiKangaiData.Tables.Add(ABKannaiKangaiData.TABLE_NAME)

            'テーブル配下に必要フィールドを用意する
            csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKB, System.Type.GetType("System.String"))
            csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKBMEI, System.Type.GetType("System.String"))

            '各フィールドにデータを格納する
            '管内管外区分 = 1
            csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow()
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKB) = "1"
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKBMEI) = "管内"
            'データの追加
            csKannaiKangaiData.Tables(ABKannaiKangaiData.TABLE_NAME).Rows.Add(csKannaiKangaiDataRow)

            '管内管外区分 = 2
            csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow()
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKB) = "2"
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKBMEI) = "管外"
            'データの追加
            csKannaiKangaiData.Tables(ABKannaiKangaiData.TABLE_NAME).Rows.Add(csKannaiKangaiDataRow)

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai")
        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenKannaiKangai】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

        Return csKannaiKangaiData
    End Function

End Class
