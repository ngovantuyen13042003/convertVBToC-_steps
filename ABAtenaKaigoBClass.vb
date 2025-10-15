'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        宛名介護ＤＡ(ABAtenaKaigoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/08　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/17 000001     追加時、共通項目を設定する
'* 2003/05/21 000002     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000003     RDBアクセスログの修正
'* 2003/09/11 000004     被保険者番号で取得するメソッドの仕様追加
'* 2003/11/18 000005     仕様変更：項目名の変更 NINTEISYURYOYMD->NinteiShuryoYMD
'* 2005/02/16 000006     レスポンス改善：ＳＱＬ文作成の修正     
'* 2010/04/16 000007     VS2008対応（比嘉）
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

Public Class ABAtenaKaigoBClass
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
    Private Const THIS_CLASS_NAME As String = "ABAtenaKaigoBClass"
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
    '* メソッド名     宛名介護マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaKaigo(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　宛名介護マスタより該当データを取得する。。
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した宛名介護マスタの該当データ（DataSet）
    '*                   構造：csAtenaKaigoEntity    インテリセンス：ABAtenaKaigoEntity
    '************************************************************************************************
    Public Function GetAtenaKaigo(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaKaigo"
        Dim csAtenaKaigoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKaigoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKaigoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKaigoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
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
            csAtenaKaigoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKaigoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKaigoEntity

    End Function

    '*履歴番号 000004 2003/09/11 追加開始
    '************************************************************************************************
    '* メソッド名     宛名介護マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaKaigoBango(ByVal strHihknshaNO As String) As DataSet
    '* 
    '* 機能　　    　　宛名介護マスタより該当データを取得する。。
    '* 
    '* 引数           strJuminCD As String  :被保険者番号
    '* 
    '* 戻り値         取得した宛名介護マスタの該当データ（DataSet）
    '*                   構造：csAtenaKaigoEntity    インテリセンス：ABAtenaKaigoEntity
    '************************************************************************************************
    Public Function GetAtenaKaigoBango(ByVal strHihknshaNO As String) As DataSet
        Dim csAtenaKaigoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKaigoEntity.HIHKNSHANO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKaigoEntity.PARAM_HIHKNSHANO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKaigoEntity.SAKUJOFG)
            strSQL.Append(" <> ")
            strSQL.Append(ABAtenaKaigoEntity.PARAM_SAKUJOFG)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_HIHKNSHANO
            cfUFParameterClass.Value = strHihknshaNO
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_SAKUJOFG
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
            csAtenaKaigoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKaigoEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' デバッグ終了ログ出力
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

        Return csAtenaKaigoEntity

    End Function
    '*履歴番号 000004 2003/09/11 追加終了

    '************************************************************************************************
    '* メソッド名     宛名介護マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaKaigo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名介護マスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertAtenaKaigo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertAtenaKaigo"
        Dim cfParam As UFParameterClass
        Dim csDataColumn As DataColumn
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '*履歴番号 000006 2005/02/16 修正開始
                Call CreateInsertSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*履歴番号 000006 2005/02/16 修正終了
            End If

            '更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '作成日時

            '共通項目の編集を行う
            csDataRow(ABAtenaKaigoEntity.TANMATSUID) = m_cfControlData.m_strClientId                '端末ＩＤ
            csDataRow(ABAtenaKaigoEntity.SAKUJOFG) = "0"                                            '削除フラグ
            csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER) = Decimal.Zero                              '更新カウンタ
            csDataRow(ABAtenaKaigoEntity.SAKUSEINICHIJI) = strUpdateDateTime                        '作成日時
            csDataRow(ABAtenaKaigoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                 '作成ユーザー
            csDataRow(ABAtenaKaigoEntity.KOSHINNICHIJI) = strUpdateDateTime                         '更新日時
            csDataRow(ABAtenaKaigoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                  '更新ユーザー

            '当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                'データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            'パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000003 2003/08/28 修正開始
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
    '* メソッド名     宛名介護マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaKaigo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名介護マスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKaigo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKaigo"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 Ends 2010/04/16 000007
        Dim intUpdCnt As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '*履歴番号 000006 2005/02/16 修正開始
                Call CreateUpdateSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*履歴番号 000006 2005/02/16 修正終了
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaKaigoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER)) + 1         '更新カウンタ
            csDataRow(ABAtenaKaigoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '更新日時
            csDataRow(ABAtenaKaigoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKaigoEntity.PREFIX_KEY.RLength) = ABAtenaKaigoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    'パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

    '*履歴番号 000006 2005/02/16 追加開始
    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 INSERTSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 追加対象の行
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
            m_strInsertSQL = "INSERT INTO " + ABAtenaKaigoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim strInsertColumn As String
        'Dim strInsertParam As String
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cfUFParameterClass As UFParameterClass
        Dim strUpdateWhere As String
        Dim strUpdateParam As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABAtenaKaigoEntity.TABLE_NAME + " SET "
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
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += " WHERE " + ABAtenaKaigoEntity.JUMINCD + " = " + ABAtenaKaigoEntity.KEY_JUMINCD + " AND " + _
                                          ABAtenaKaigoEntity.KOSHINCOUNTER + " = " + ABAtenaKaigoEntity.KEY_KOSHINCOUNTER

            ' UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_KOSHINCOUNTER
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
    '*履歴番号 000006 2005/02/16 追加開始

    '*履歴番号 000006 2005/02/16 削除開始
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
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaKaigoEntity.TABLE_NAME + " "
    '        strInsertColumn = ""
    '        strInsertParam = ""

    '        ' UPDATE SQL文の作成
    '        m_strUpdateSQL = "UPDATE " + ABAtenaKaigoEntity.TABLE_NAME + " SET "
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
    '            strInsertParam += ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' SQL文の作成
    '            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' INSERT コレクションにパラメータを追加
    '            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE コレクションにパラメータを追加
    '            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
    '        m_strUpdateSQL += " WHERE " + ABAtenaKaigoEntity.JUMINCD + " = " + ABAtenaKaigoEntity.KEY_JUMINCD + " AND " + _
    '                                      ABAtenaKaigoEntity.KOSHINCOUNTER + " = " + ABAtenaKaigoEntity.KEY_KOSHINCOUNTER

    '        ' UPDATE コレクションにパラメータを追加
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_KOSHINCOUNTER
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
    '*履歴番号 000006 2005/02/16 削除開始

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       宛名介護マスタのデータ整合性チェックを行います。
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
                Case ABAtenaKaigoEntity.JUMINCD                         '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SHICHOSONCD                     '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KYUSHICHOSONCD                  '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.HIHKNSHANO                      '被保険者番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_HIHKNSHANO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SKAKSHUTKYMD                    '資格取得日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKSHUTKYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.SKAKSSHTSYMD                    '資格喪失日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKSSHTSYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.SKAKHIHOKENSHAKB                '資格被保険者区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKHIHOKENSHAKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.JUSHOCHITKRIKB                  '住所地特例者区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUSHOCHITKRIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.JUKYUSHAKB                      '受給者区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUSHAKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.YOKAIGJOTAIKBCD                 '要介護状態区分コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_YOKAIGJOTAIKBCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KAIGSKAKKB                      '要介護状態区分
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KAIGSKAKKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.NINTEIKAISHIYMD                 '認定有効開始日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_NINTEIKAISHIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                    '* 履歴番号 000005 2003/11/18 修正開始
                    'Case ABAtenaKaigoEntity.NINTEISYURYOYMD                 '認定有効終了日
                Case ABAtenaKaigoEntity.NINTEISHURYOYMD                 '認定有効終了日
                    '* 履歴番号 000005 2003/11/18 修正終了
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_NINTEISYURYOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.JUKYUNINTEIYMD                  '受給認定年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUNINTEIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD         '受給認定取消年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUNINTEITORIKESHIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.TANMATSUID                      '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUJOFG                        '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINCOUNTER                   '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUSEINICHIJI                  '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUSEIUSER                     '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINNICHIJI                   '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINUSER                      '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINUSER)
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
