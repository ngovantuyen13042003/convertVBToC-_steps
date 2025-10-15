'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        業務テーブルＤＡ(ABGyomuTableBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/16　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/17 000001     追加時、共通項目を設定する
'* 2003/05/21 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000003     RDBアクセスログの修正
'* 2010/04/16  000005      VS2008対応（比嘉）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools

Public Class ABGyomuTableBClass
#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_strInsertSQL As String                                            'INSERT用SQL
    Private m_strUpdateSQL As String                                            'UPDATE用SQL
    Private m_strDeleteSQL As String                                            'DELETE用SQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'DELETE用パラメータコレクション

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABGyomuTableBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
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
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        'メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     業務テーブル抽出
    '* 
    '* 構文           Public Function GetGyomuTable(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　業務テーブルより該当データを全件取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した業務テーブルの該当データ（DataSet）
    '*                   構造：csGyomuTableEntity    インテリセンス：ABGyomuTableEntity
    '************************************************************************************************
    Public Function GetGyomuTable(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetGyomuTable"              'このメソッド名
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim csGyomuTableEntity As DataSet                               '業務テーブルデータ
        Dim strSQL As New StringBuilder()                               'SQL文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGyomuTableEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGyomuTableEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGyomuTableEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGyomuTableEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000003 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '*履歴番号 000003 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csGyomuTableEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGyomuTableEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csGyomuTableEntity

    End Function

    '************************************************************************************************
    '* メソッド名     業務テーブル追加
    '* 
    '* 構文           Public Function InsertGyomuTable(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  業務テーブルにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertGyomuTable(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertGyomuTable"            'このメソッド名
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
            csDataRow(ABGyomuTableEntity.TANMATSUID) = m_cfControlData.m_strClientId                '端末ＩＤ
            csDataRow(ABGyomuTableEntity.SAKUJOFG) = "0"                                            '削除フラグ
            csDataRow(ABGyomuTableEntity.KOSHINCOUNTER) = Decimal.Zero                              '更新カウンタ
            csDataRow(ABGyomuTableEntity.SAKUSEINICHIJI) = strUpdateDateTime                        '作成日時
            csDataRow(ABGyomuTableEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                 '作成ユーザー
            csDataRow(ABGyomuTableEntity.KOSHINNICHIJI) = strUpdateDateTime                         '更新日時
            csDataRow(ABGyomuTableEntity.KOSHINUSER) = m_cfControlData.m_strUserId                  '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000003 2003/08/28 修正開始
            ' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000003 2003/08/28 修正終了

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
    '* メソッド名     業務テーブル更新
    '* 
    '* 構文           Public Function UpdateGyomuTable(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  業務テーブルのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateGyomuTable(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateGyomuTable"           'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
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
            csDataRow(ABGyomuTableEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABGyomuTableEntity.KOSHINCOUNTER) = CDec(csDataRow(ABGyomuTableEntity.KOSHINCOUNTER)) + 1         '更新カウンタ
            csDataRow(ABGyomuTableEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '更新日時
            csDataRow(ABGyomuTableEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABGyomuTableEntity.PREFIX_KEY.RLength) = ABGyomuTableEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000003 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            '*履歴番号 000003 2003/08/28 修正終了

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

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     業務テーブル削除（物理）
    '* 
    '* 構文           Public Function DeleteGyomuTable(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  業務テーブルにデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteGyomuTable(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteGyomuTable（物理）"   'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
        Dim intDelCnt As Integer                                        '削除件数

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
                If (cfParam.ParameterName.RSubstring(0, ABGyomuTableEntity.PREFIX_KEY.RLength) = ABGyomuTableEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGyomuTableEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000003 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strDeleteSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")
            '*履歴番号 000003 2003/08/28 修正終了

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
    '* メソッド名     業務テーブル削除（物理・オーバーロード）
    '* 
    '* 構文           Public Overloads Function DeleteGyomuTable(ByVal csGyomuCD As String, 
    '*                                                           ByVal csGyomuEdaCD As String) As Integer
    '* 
    '* 機能　　    　  業務テーブルにデータを削除（物理）する。
    '* 
    '* 引数           csGyomuCD As String       :業務コード
    '*                csGyomuEdaCD As String    :業務コード枝番
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteGyomuTable(ByVal csGyomuCD As String, ByVal csGyomuEdaCD As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteGyomuTable（物理・オーバーロード）"   'このメソッド名
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim cfParam As UFParameterClass                                 'パラメータクラス
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
        Dim intDelCnt As Integer                                        '削除件数
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim strSQL As New StringBuilder()                               'SQL文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            strSQL.Append("DELETE FROM ")
            strSQL.Append(ABGyomuTableEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGyomuTableEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGyomuTableEntity.KEY_GYOMUCD)
            If Not (csGyomuEdaCD Is Nothing Or csGyomuEdaCD = String.Empty) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABGyomuTableEntity.GYOMUEDACD)
                strSQL.Append(" = ")
                strSQL.Append(ABGyomuTableEntity.KEY_GYOMUEDACD)
            End If
            m_strDeleteSQL = strSQL.ToString

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()
            ' DELETE(物理) コレクションにキー情報を追加
            ' 業務コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_GYOMUCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' キー項目は更新前の値で設定
            m_cfDeleteUFParameterCollectionClass(ABGyomuTableEntity.KEY_GYOMUCD).Value = csGyomuCD
            ' 業務コード枝番
            If Not (csGyomuEdaCD Is Nothing Or csGyomuEdaCD = String.Empty) Then
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_GYOMUEDACD
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
                ' キー項目は更新前の値で設定
                m_cfDeleteUFParameterCollectionClass(ABGyomuTableEntity.KEY_GYOMUEDACD).Value = csGyomuEdaCD
            End If

            '*履歴番号 000003 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strDeleteSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")
            '*履歴番号 000003 2003/08/28 修正終了

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
        Const THIS_METHOD_NAME As String = "CreateSQL"              'このメソッド名
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim strInsertColumn As String                               '追加SQL文項目文字列
        Dim strInsertParam As String                                '追加SQL文パラメータ文字列
        Dim strDeleteSQL As New StringBuilder()                     '削除SQL文文字列
        Dim strWhere As New StringBuilder()                         '更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABGyomuTableEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' 更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABGyomuTableEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGyomuTableEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGyomuTableEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGyomuTableEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGyomuTableEntity.GYOMUEDACD)
            strWhere.Append(" = ")
            strWhere.Append(ABGyomuTableEntity.KEY_GYOMUEDACD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGyomuTableEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABGyomuTableEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABGyomuTableEntity.TABLE_NAME + " SET "

            ' DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABGyomuTableEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABGyomuTableEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABGyomuTableEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABGyomuTableEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABGyomuTableEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
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
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード枝番
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_GYOMUEDACD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGyomuTableEntity.KEY_KOSHINCOUNTER
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
    '* 機能　　       業務テーブルのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       'このメソッド名
        Dim objErrorStruct As UFErrorStruct                         'エラー定義構造体

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 日付クラスのインスタンス化
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABGyomuTableEntity.JUMINCD                         '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.SHICHOSONCD                     '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.KYUSHICHOSONCD                  '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.GYOMUCD                         '業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.GYOMUEDACD                      '業務ｺｰﾄﾞ枝番
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_GYOMUEDACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.GYOMUMEI                        '業務名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_GYOMUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.GYOMUEDAMEI                     '業務枝番名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_GYOMUEDAMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.SAKUSEIYMD                      'データ作成日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_SAKUSEIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABGyomuTableEntity.RESERVE                         'リザーブ
                    '何もしない
                Case ABGyomuTableEntity.TANMATSUID                      '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.SAKUJOFG                        '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.KOSHINCOUNTER                   '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.SAKUSEINICHIJI                  '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.SAKUSEIUSER                     '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.KOSHINNICHIJI                   '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGyomuTableEntity.KOSHINUSER                      '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGYOMUTABLEB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
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
#End Region

End Class
