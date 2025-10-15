'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        合併同一人ＤＡ(ABGappeiDoitsuninBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/15　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/17 000001     追加時、共通項目を設定する
'* 2003/04/25 000002     合併同一人グループ抽出メソッドを追加
'* 2003/05/13 000003     ＤＢよりスキーマを取得してSQLを作成
'* 2003/05/21 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/21 000005     合併同一人抽出(GetDoitsunin)メソッドを追加
'* 2007/07/27 000006     同一人代表者取得機能の追加 (吉澤)
'* 2010/04/16 000007     VS2008対応（比嘉）
'* 2016/01/07 000008     【AB00163】個人制御の同一人対応（石合）
'* 2018/05/01 000009     【AB27001】該当者一覧への同一人区分表示（石合）
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
'*履歴番号 000009 2018/05/01 追加開始
Imports System.Collections.Generic
'*履歴番号 000009 2018/05/01 追加終了

Public Class ABGappeiDoitsuninBClass
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDeleteSQL As String                        ' DELETE用SQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE用パラメータコレクション

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABGappeiDoitsuninBClass"

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData,
    '* 　　                           ByVal cfConfigDataClass As UFConfigDataClass,
    '* 　　                           ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '* 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '* 　　            cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
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

        ' メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
    End Sub

    '************************************************************************************************
    '* メソッド名     合併同一人全員抽出
    '* 
    '* 構文           Public Function GetDoitsuninAll(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　合併同一人より該当データを全件取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（DataSet）
    '*                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninAll(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninAll"            'このメソッド名
        Dim csGappeiDoitsuninEntity As DataSet                          '合併同一人データ
        Dim strSQL As New StringBuilder()                               'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 取得件数が０件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' 取得件数が１件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL文の作成
                strSQL = New StringBuilder()
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE文結合
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> 1")

                ' 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = New UFParameterCollectionClass()
                ' 検索条件のパラメータを作成
                ' 同一人識別コード
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

                ' SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function

    '* 履歴番号 000005 2003/08/21 追加開始
    '************************************************************************************************
    '* メソッド名     合併同一人抽出
    '* 
    '* 構文           Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　合併同一人より該当データを全件取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（DataSet）
    '*                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsunin"               'このメソッド名
        Dim csGappeiDoitsuninEntity As DataSet                          '合併同一人データ
        Dim strSQL As New StringBuilder()                               'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000007

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 取得件数が０件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                Exit Try
            End If

            ' 取得件数が１件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL文の作成
                strSQL = New StringBuilder()
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE文結合
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> 1")

                ' 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = New UFParameterCollectionClass()
                ' 検索条件のパラメータを作成
                ' 同一人識別コード
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

                ' SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        Finally
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        End Try

        Return csGappeiDoitsuninEntity

    End Function
    '* 履歴番号 000005 2003/08/21 追加終了

    '*履歴番号 000008 2016/01/07 追加開始
    ''' <summary>
    ''' 同一人データ取得
    ''' </summary>
    ''' <param name="a_strJuminCD">住民コード文字列配列</param>
    ''' <returns>同一人データ</returns>
    ''' <remarks></remarks>
    Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strParameterName As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To a_strJuminCD.Length - 1

                    ' -----------------------------------------------------------------------------
                    ' 住民コード
                    strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString

                    If (i > 0) Then
                        .AppendFormat(", {0}", strParameterName)
                    Else
                        .Append(strParameterName)
                    End If

                    cfParameter = New UFParameterClass
                    cfParameter.ParameterName = strParameterName
                    cfParameter.Value = a_strJuminCD(i)
                    cfParameterCollection.Add(cfParameter)
                    ' -----------------------------------------------------------------------------

                Next i

                .Append(")")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")

            End With

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + csSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            ' 取得件数が１件以上の時
            If (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0) Then

                ' SQL文の作成
                csSQL = New StringBuilder
                cfParameterCollection = New UFParameterCollectionClass

                With csSQL

                    .Append("SELECT * FROM ")
                    .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(" IN (")

                    For i As Integer = 0 To csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count - 1

                        ' -----------------------------------------------------------------------------
                        ' 同一人識別コード
                        strParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD + i.ToString

                        If (i > 0) Then
                            .AppendFormat(", {0}", strParameterName)
                        Else
                            .Append(strParameterName)
                        End If

                        cfParameter = New UFParameterClass
                        cfParameter.ParameterName = strParameterName
                        cfParameter.Value = _
                            csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(i).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString
                        cfParameterCollection.Add(cfParameter)
                        ' -----------------------------------------------------------------------------

                    Next i

                    .Append(")")
                    .Append(" AND ")
                    .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(" ORDER BY ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(", ")
                    .Append(ABGappeiDoitsuninEntity.HONNINKB)
                    .Append(", ")
                    .Append(ABGappeiDoitsuninEntity.JUMINCD)

                End With

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + csSQL.ToString + "】")

                ' SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + csAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csDataSet

    End Function
    '*履歴番号 000008 2016/01/07 追加終了

    '*履歴番号 000009 2018/05/01 追加開始
    ''' <summary>
    ''' 同一人区分名称取得
    ''' </summary>
    ''' <param name="csJuminCDList">住民コードリスト</param>
    ''' <returns>同一人区分名称</returns>
    ''' <remarks></remarks>
    Public Function GetDoitsuninMeisho(ByVal csJuminCDList As List(Of String)) As Hashtable

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csResult As Hashtable
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strParameterName As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 返信オブジェクトのインスタンス化
            csResult = New Hashtable

            ' SQL文の作成
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To csJuminCDList.Count - 1

                    ' -----------------------------------------------------------------------------
                    ' 住民コード
                    strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString

                    If (i > 0) Then
                        .AppendFormat(", {0}", strParameterName)
                    Else
                        .Append(strParameterName)
                    End If

                    cfParameter = New UFParameterClass
                    cfParameter.ParameterName = strParameterName
                    cfParameter.Value = csJuminCDList(i)
                    cfParameterCollection.Add(cfParameter)
                    ' -----------------------------------------------------------------------------

                Next i

                .Append(")")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")

            End With

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + csSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            ' 取得件数が１件以上の時
            If (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows

                    ' -----------------------------------------------------------------------------
                    ' 同一人区分名称編集
                    Select Case csDataRow.Item(ABGappeiDoitsuninEntity.HONNINKB).ToString.Trim
                        Case ABConstClass.HONNINKB.CODE.DAIHYO
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DAIHYO)
                        Case ABConstClass.HONNINKB.CODE.DOITSUNIN
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DOITSUNIN)
                        Case ABConstClass.HONNINKB.CODE.HAISHI
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.HAISHI)
                        Case Else
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, String.Empty)
                    End Select
                    ' -----------------------------------------------------------------------------

                Next csDataRow

            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + csAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csResult

    End Function
    '*履歴番号 000009 2018/05/01 追加終了

    '*履歴番号 000006 2007/07/27 追加開始
    '************************************************************************************************
    '* メソッド名     合併同一人代表住民コード取得
    '* 
    '* 構文           Public Function GetDoitsuninDaihyoJuminCD(ByVal strJuminCD As String) As String
    '* 
    '* 機能　　    　 合併同一人代表の住民コードを取得する
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（String）
    '************************************************************************************************
    Public Function GetDoitsuninDaihyoJuminCD(ByVal strJuminCD As String) As String
        Const THIS_METHOD_NAME As String = "GetDoitsuninDaihyoJuminCD"         'このメソッド名
        Dim strDaihyoJuminCD As String                      '住民コード（代表者）
        Dim csDaihyosyaEntity As DataSet              '同一人代表者データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '代表者情報の取得を行う
            ' SQL文の作成
            strSQL.Append("SELECT A.")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" A ")
            ' JOIN文結合
            strSQL.Append("JOIN (SELECT ")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
            strSQL.Append(" IN ('0','1')")
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1) B ON A.")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" = B.")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            ' WHERE文結合
            strSQL.Append(" WHERE A.")
            strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
            strSQL.Append(" = '0'")


            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csDaihyosyaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 取得件数が０件の時
            If (csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                '同一人管理されていない場合は、指定された住民コードを返却する
                strDaihyoJuminCD = strJuminCD
            Else
                '同一人管理されている場合は、同一人代表者の住民コードを返却する
                strDaihyoJuminCD = CStr(csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.JUMINCD))
            End If

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return strDaihyoJuminCD

    End Function
    '*履歴番号 000006 2007/07/27 追加終了

    '************************************************************************************************
    '* メソッド名     合併同一人本人抽出
    '* 
    '* 構文           Public Function GetDoitsuninHonnin(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　合併同一人より該当データを取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（DataSet）
    '*                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninHonnin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninHonnin"         'このメソッド名
        Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
        Dim csGappeiDoitsuninEntity As DataSet                          '合併同一人データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 取得件数が０件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' 取得件数が１件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL文の作成
                strSQL = New StringBuilder
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE文結合
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
                strSQL.Append(" = '0'")
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = New UFParameterCollectionClass
                ' 検索条件のパラメータを作成
                ' 同一人識別コード
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

                ' SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function

    '************************************************************************************************
    '* メソッド名     合併同一人グループ抽出
    '* 
    '* 構文           Public Function GetDoitsuninGroup(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　合併同一人より該当データを全件取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         識別コード(String)         
    '************************************************************************************************
    Public Function GetDoitsuninGroup(ByVal strJuminCD As String) As String
        Const THIS_METHOD_NAME As String = "GetDoitsuninGroup"          'このメソッド名
        Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
        Dim csGappeiDoitsuninEntity As DataSet                          '合併同一人データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim strShikibetsuCD As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 取得件数が０件の時
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            strShikibetsuCD = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD), String)


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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return strShikibetsuCD

    End Function

    '************************************************************************************************
    '* メソッド名     合併同一人追加
    '* 
    '* 構文           Public Function InsertDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  合併同一人にデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertDoitsunin"            'このメソッド名
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                            '追加件数
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId           '端末ＩＤ
            csDataRow(ABGappeiDoitsuninEntity.SAKUJOFG) = "0"                                       '削除フラグ
            csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = Decimal.Zero                         '更新カウンタ
            csDataRow(ABGappeiDoitsuninEntity.SAKUSEINICHIJI) = strUpdateDateTime                   '作成日時
            csDataRow(ABGappeiDoitsuninEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId            '作成ユーザー
            csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = strUpdateDateTime                    '更新日時
            csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId             '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_strInsertSQL + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* メソッド名     合併同一人更新
    '* 
    '* 構文           Public Function UpdateDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  合併同一人のデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateDoitsunin"            'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intUpdCnt As Integer                                        '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = CDec(csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER)) + 1   '更新カウンタ
            csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) = ABGappeiDoitsuninEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_strUpdateSQL + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            'システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     合併同一人削除（物理）
    '* 
    '* 構文           Public Function DeleteDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  合併同一人のデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Function DeleteDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteDoitsunin（物理）"
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intDelCnt As Integer                            '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) = ABGappeiDoitsuninEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_strDeleteSQL + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass          'パラメータクラス
        Dim strInsertColumn As String                       '追加SQL文項目文字列
        Dim strInsertParam As String                        '追加SQL文パラメータ文字列
        Dim strDeleteSQL As New StringBuilder               '削除SQL文文字列
        Dim strWhere As New StringBuilder                   '更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABGappeiDoitsuninEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' 更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGappeiDoitsuninEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABGappeiDoitsuninEntity.TABLE_NAME + " SET "

            ' DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '' カラムが存在する場合
                'If (m_csSchema.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Columns.Contains(csDataColumn.ColumnName)) Then

                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                'End If

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += strWhere.ToString

            ' UPDATE,DELETE(物理) コレクションにキー情報を追加
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 同一人識別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       合併同一のデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABGappeiDoitsuninEntity.JUMINCD                    '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_JUMINCD)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SHICHOSONCD                '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SHICHOSONCD)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KYUSHICHOSONCD             '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KYUSHICHOSONCD)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD      '同一人識別コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_DOITSUNINSHIKIBETSUCD)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HONNINKB                   '本人区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HONNINKB)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HANYOKB1                   '汎用区分1
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB1)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HANYOKB2                   '汎用区分2
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB2)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.BIKO                       '備考
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_BIKO)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.RESERVE                    'リザーブ
                    ' 何もしない
                Case ABGappeiDoitsuninEntity.TANMATSUID                 '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_TANMATSUID)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUJOFG                   '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUJOFG)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINCOUNTER              '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINCOUNTER)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUSEINICHIJI             '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEINICHIJI)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUSEIUSER                '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEIUSER)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINNICHIJI              '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINNICHIJI)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINUSER                 '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINUSER)
                        ' 例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

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
            Throw exAppException
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        End Try

    End Sub

End Class
