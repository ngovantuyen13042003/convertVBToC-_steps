'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        宛名年金ＤＡ(ABAtenaNenkinBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/06　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/17 000001     追加時、共通項目を設定する
'* 2003/05/21 000002     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/06/20 000003     エラークラスをエラー毎にｲﾝｽﾀﾝｽ化する
'* 2003/08/28 000004     RDBアクセスログの修正
'* 2003/09/11 000005     年金番号で取得するメソッドの仕様追加
'* 2003/09/24 000006     ファイル定義変更に伴う修正
'* 2003/10/03 000007     取得理由・喪失理由のチェックはANKに変更
'* 2004/11/11 000008     データチェックを行なわない
'* 2005/02/16 000009     レスポンス改善：ＳＱＬ文作成の修正     
'* 2010/04/16 000010     VS2008対応（比嘉）
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

Public Class ABAtenaNenkinBClass
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
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaNenkinBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
#End Region

#Region "コンストラクタ"
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
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     宛名年金マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaNenkin(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　宛名年金マスタより該当データを取得する。。
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した宛名年金マスタの該当データ（DataSet）
    '*                   構造：csAtenaNenkinEntity    インテリセンス：ABAtenaNenkinEntity
    '************************************************************************************************
    Public Function GetAtenaNenkin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaNenkin"
        Dim csAtenaNenkinEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaNenkinEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaNenkinEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaNenkinEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000004 2003/08/28 修正開始
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
            '*履歴番号 000004 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csAtenaNenkinEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaNenkinEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaNenkinEntity

    End Function

    '*履歴番号 000005 2003/09/11 追加開始
    '************************************************************************************************
    '* メソッド名     宛名年金マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaNenkinBango(ByVal strKsNenknNO As String) As DataSet
    '* 
    '* 機能　　    　　宛名年金マスタより該当データを取得する。。
    '* 
    '* 引数           strKsNenknNO As String  : 基礎年金番号
    '* 
    '* 戻り値         取得した宛名年金マスタの該当データ（DataSet）
    '*                   構造：csAtenaNenkinEntity    インテリセンス：ABAtenaNenkinEntity
    '************************************************************************************************
    Public Function GetAtenaNenkinBango(ByVal strKsNenknNO As String) As DataSet
        Dim csAtenaNenkinEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaNenkinEntity.KSNENKNNO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaNenkinEntity.PARAM_KSNENKNNO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaNenkinEntity.SAKUJOFG)
            strSQL.Append(" <> ")
            strSQL.Append(ABAtenaNenkinEntity.PARAM_SAKUJOFG)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_KSNENKNNO
            cfUFParameterClass.Value = strKsNenknNO
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_SAKUJOFG
            cfUFParameterClass.Value = "1"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaNenkinEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaNenkinEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csAtenaNenkinEntity

    End Function
    '*履歴番号 000005 2003/09/11 追加終了

    '************************************************************************************************
    '* メソッド名     宛名年金マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaNenkin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名年金マスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertAtenaNenkin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertAtenaNenkin"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000010
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000010
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '*履歴番号 000009 2005/02/16 修正開始
                Call CreateInsertSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*履歴番号 000009 2005/02/16 修正終了
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABAtenaNenkinEntity.TANMATSUID) = m_cfControlData.m_strClientId               '端末ＩＤ
            csDataRow(ABAtenaNenkinEntity.SAKUJOFG) = "0"                                           '削除フラグ
            csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER) = Decimal.Zero                             '更新カウンタ
            csDataRow(ABAtenaNenkinEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '作成日時
            csDataRow(ABAtenaNenkinEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '作成ユーザー
            csDataRow(ABAtenaNenkinEntity.KOSHINNICHIJI) = strUpdateDateTime                        '更新日時
            csDataRow(ABAtenaNenkinEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '更新ユーザー

            '*履歴番号 000008 2004/11/11 修正開始
            ' 当クラスのデータ整合性チェックを行う
            'For Each csDataColumn In csDataRow.Table.Columns
            '    ' データ整合性チェック
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            'Next csDataColumn
            '*履歴番号 000008 2004/11/11 修正終了

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

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

        Catch exException As Exception ' システムエラーをキャッチ
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
    '* メソッド名     宛名年金マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaNenkin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名年金マスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaNenkin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaNenkin"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000010
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000010
        Dim intUpdCnt As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '*履歴番号 000009 2005/02/16 修正開始
                Call CreateUpdateSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*履歴番号 000009 2005/02/16 修正終了
            End If

            ' 共通項目の編集を行う
            csDataRow(ABAtenaNenkinEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaNenkinEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABAtenaNenkinEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaNenkinEntity.PREFIX_KEY.RLength) = ABAtenaNenkinEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '*履歴番号 000008 2004/11/11 修正開始
                    ' データ整合性チェック
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
                    '*履歴番号 000008 2004/11/11 修正終了
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
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
            '*履歴番号 000004 2003/08/28 修正終了

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

        Catch exException As Exception ' システムエラーをキャッチ
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

    '*履歴番号 000009 2005/02/16 追加開始
    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 INSERTSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim strInsertColumn As String
        Dim strInsertParam As String
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABAtenaNenkinEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))

            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            'デバッグログ出力
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
    End Sub

    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 UPDATESQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim strUpdateWhere As String
        Dim strUpdateParam As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABAtenaNenkinEntity.TABLE_NAME + " SET "
            strUpdateParam = ""
            strUpdateWhere = ""

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns

                '住民ＣＤ（主キー）と作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABAtenaInkanEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaInkanEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABAtenaInkanEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass()

                    ' SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += " WHERE " + ABAtenaNenkinEntity.JUMINCD + " = " + ABAtenaNenkinEntity.KEY_JUMINCD + " AND " + _
                                          ABAtenaNenkinEntity.KOSHINCOUNTER + " = " + ABAtenaNenkinEntity.KEY_KOSHINCOUNTER

            ' UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            'デバッグログ出力
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
    End Sub
    '*履歴番号 000009 2005/02/16 追加終了

    '*履歴番号 000009 2005/02/16 削除開始
    ''************************************************************************************************
    ''* メソッド名     SQL文の作成
    ''* 
    ''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''* 
    ''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    ''* 
    ''* 引数           csDataRow As DataRow : 更新対象の行
    ''* 
    ''* 戻り値         なし
    ''************************************************************************************************
    'Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '    Const THIS_METHOD_NAME As String = "CreateSQL"
    '    Dim csDataColumn As DataColumn
    '    Dim strInsertColumn As String
    '    Dim strInsertParam As String
    '    Dim cfUFParameterClass As UFParameterClass
    '    Dim strUpdateWhere As String
    '    Dim strUpdateParam As String

    '    Try
    '        ' デバッグログ出力
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        ' SELECT SQL文の作成
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaNenkinEntity.TABLE_NAME + " "
    '        strInsertColumn = ""
    '        strInsertParam = ""

    '        ' UPDATE SQL文の作成
    '        m_strUpdateSQL = "UPDATE " + ABAtenaNenkinEntity.TABLE_NAME + " SET "
    '        strUpdateParam = ""
    '        strUpdateWhere = ""

    '        ' SELECT パラメータコレクションクラスのインスタンス化
    '        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' UPDATE パラメータコレクションのインスタンス化
    '        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' パラメータコレクションの作成
    '        For Each csDataColumn In csDataRow.Table.Columns
    '            cfUFParameterClass = New UFParameterClass()

    '            ' INSERT SQL文の作成
    '            strInsertColumn += csDataColumn.ColumnName + ", "
    '            strInsertParam += ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' SQL文の作成
    '            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' INSERT コレクションにパラメータを追加
    '            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE コレクションにパラメータを追加
    '            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        Next csDataColumn

    '        ' INSERT SQL文のトリミング
    '        strInsertColumn = strInsertColumn.Trim()
    '        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    '        strInsertParam = strInsertParam.Trim()
    '        strInsertParam = strInsertParam.Trim(CType(",", Char))

    '        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    '        ' UPDATE SQL文のトリミング
    '        m_strUpdateSQL = m_strUpdateSQL.Trim()
    '        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    '        ' UPDATE SQL文にWHERE句の追加
    '        m_strUpdateSQL += " WHERE " + ABAtenaNenkinEntity.JUMINCD + " = " + ABAtenaNenkinEntity.KEY_JUMINCD + " AND " + _
    '                                      ABAtenaNenkinEntity.KOSHINCOUNTER + " = " + ABAtenaNenkinEntity.KEY_KOSHINCOUNTER

    '        ' UPDATE コレクションにパラメータを追加
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_KOSHINCOUNTER
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        'デバッグログ出力
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    Catch exAppException As UFAppException
    '        ' ワーニングログ出力
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                    "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
    '                                    "【ワーニング内容:" + exAppException.Message + "】")
    '        ' ワーニングをスローする
    '        Throw exAppException

    '    Catch exException As Exception ' システムエラーをキャッチ
    '        ' エラーログ出力
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                    "【エラー内容:" + exException.Message + "】")
    '        ' システムエラーをスローする
    '        Throw exException

    '    End Try
    'End Sub
    '*履歴番号 000009 2005/02/16 削除終了

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       宛名年金マスタのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体

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
                Case ABAtenaNenkinEntity.JUMINCD                        '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SHICHOSONCD                    '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KYUSHICHOSONCD                 '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KSNENKNNO                      '基礎年金番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KSNENKNNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKYMD                   '資格取得年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKSHU                   '資格取得種別
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKSHU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKRIYUCD                '資格取得理由コード
                    '* 履歴番号 000007 2003/10/03 修正開始
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/03 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKRIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSSHTSYMD                   '資格喪失年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSSHTSYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaNenkinEntity.SKAKSSHTSRIYUCD                '資格喪失理由コード
                    '* 履歴番号 000007 2003/10/03 修正開始
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/03 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSSHTSRIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO1                  '受給年金記号１
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正開始
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU1                   '受給年金番号１
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU1)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO1                    '受給年金種別１
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO1)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO1                    '受給年金番号１
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU1                   '受給年金種別１
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正終了

                Case ABAtenaNenkinEntity.JKYNENKNEDABAN1                '受給年金枝番１
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB1                    '受給年金区分１
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO2                  '受給年金記号２
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正開始
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU2                   '受給年金番号２
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU2)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO2                    '受給年金種別２
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO2)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO2                    '受給年金番号２
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU2                   '受給年金種別２
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正終了
                Case ABAtenaNenkinEntity.JKYNENKNEDABAN2                '受給年金枝番２
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB2                    '受給年金区分２
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO3                  '受給年金記号３
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正開始
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU3                   '受給年金番号３
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU3)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO3                    '受給年金種別３
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        'エラー定義を取得
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO3)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO3                    '受給年金番号３
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU3                   '受給年金種別３
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* 履歴番号 000006 2003/09/24 修正終了
                Case ABAtenaNenkinEntity.JKYNENKNEDABAN3                '受給年金枝番３
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB3                    '受給年金区分３
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.TANMATSUID                     '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUJOFG                       '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINCOUNTER                  '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUSEINICHIJI                 '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUSEIUSER                    '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINNICHIJI                  '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINUSER                     '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINUSER)
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
        Catch exException As Exception ' システムエラーをキャッチ
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
