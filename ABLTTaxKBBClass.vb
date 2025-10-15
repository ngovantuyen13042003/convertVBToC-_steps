'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         ｅＬＴＡＸ税目区分マスタ(ABLTTaxKBBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付             2008/11/25
'*
'* 作成者           比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2009/07/16   000001     税目区分マスタ業務コード指定取得メソッドを追加（比嘉）
'* 2010/04/16   000002     VS2008対応（比嘉）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABLTTaxKBBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス

    Private m_csDataSchma As DataSet   'スキーマ保管用データセット

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABLTTaxKBBClass"

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '*                 cfRdbClass As UFRdbClass               : ＲＤＢデータオブジェクト
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
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' SQL文の作成
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTTaxKBEntity.TABLE_NAME, ABLTTaxKBEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     税目区分マスタ取得
    '* 
    '* 構文           Public Overloads Function GetLTTaxKB() As DataSet
    '* 
    '* 機能　　    　 税目区分マスタより全件データを取得する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         取得した税目区分マスタの該当データ（DataSet）
    '************************************************************************************************
    Public Overloads Function GetLTTaxKB() As DataSet
        Const THIS_METHOD_NAME As String = "GetLTTaxKB"
        Dim csLTTaxKBEntity As DataSet                                      ' 税目区分マスタデータ
        Dim strSQL As New System.Text.StringBuilder                         ' SQL文文字列
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABLTTaxKBEntity.TABLE_NAME)
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTTaxKBEntity.TAXKB)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csLTTaxKBEntity = m_csDataSchma.Clone()
            csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csLTTaxKBEntity

    End Function

    '*履歴番号 000001 2009/07/16 追加開始
    '************************************************************************************************
    '* メソッド名     税目区分マスタ取得
    '* 
    '* 構文           Public Overloads Function GetLTTaxKB(ByVal strGyomuCD() As String) As DataSet
    '* 
    '* 機能　　    　 税目区分マスタより全件データを取得する。
    '* 
    '* 引数           strGyomuCD() As String        :業務コード配列
    '* 
    '* 戻り値         取得した税目区分マスタの該当データ（DataSet）
    '************************************************************************************************
    Public Overloads Function GetLTTaxKB(ByVal strGyomuCD() As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTTaxKB"
        Dim csLTTaxKBEntity As DataSet                                      ' 税目区分マスタデータ
        Dim strSQL As New System.Text.StringBuilder                         ' SQL文文字列
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' パラメータコレクションクラス
        Dim intI As Integer
        Dim strWhere As New System.Text.StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABLTTaxKBEntity.TABLE_NAME)

            ' WHERE句
            If (strGyomuCD.Length > 0) Then
                strSQL.Append(" WHERE ")
                strSQL.Append(ABLTTaxKBEntity.GYOMUCD)
                strSQL.Append(" IN(")

                For intI = 0 To strGyomuCD.Length - 1
                    strSQL.Append("'")
                    strSQL.Append(strGyomuCD(intI))
                    strSQL.Append("',")
                Next
                strSQL.RRemove(strSQL.RLength - 1, 1)
                strSQL.Append(")")

            Else
            End If

            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTTaxKBEntity.TAXKB)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csLTTaxKBEntity = m_csDataSchma.Clone()
            csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csLTTaxKBEntity

    End Function
    '*履歴番号 000001 2009/07/16 追加終了
#End Region

End Class
