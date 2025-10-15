'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        代納関係ＤＡ(ABDainoKankeiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2002/12/19　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/14 000001     整合性チェックは、空白をとって処理する
'* 2003/03/27 000002     エラー処理クラスの参照先を"AB"固定にする
'* 2003/05/21 000003     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000004     RDBアクセスログの修正
'* 2005/01/25 000005     速度改善２：（宮沢）
'* 2010/04/16  000006      VS2008対応（比嘉）
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

Public Class ABDainoKankeiBClass
#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDeleteSQL As String                        ' DELETE用SQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  ' INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  ' UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  ' DELETE用パラメータコレクション

    '* 履歴番号 000005 2005/01/25 追加開始（宮沢）
    Private m_csDainoKankeiCDMSTEntity As DataSet
    '* 履歴番号 000005 2005/01/25 追加終了（宮沢）

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABDainoKankeiBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfUFControlData As UFControlData,
    '* 　　                           ByVal cfUFConfigDataClass As UFConfigDataClass,
    '* 　　                           ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
    '* 　　            cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '* 　　            cfUFRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        'メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        'ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' メンバ変数の初期化
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
    '* メソッド名     代納関係コードマスタ抽出
    '* 
    '* 構文           Public Overloads Function GetDainoKBHoshu() As DataSet
    '* 
    '* 機能       　　代納関係コードマスタより該当データを全件取得する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         取得した代納関係コードマスタの該当データ（DataSet）
    '*                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoKBHoshu"
        Dim csDainoKankeiCDMSTEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME)

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

        Return csDainoKankeiCDMSTEntity

    End Function

    '************************************************************************************************
    '* メソッド名     代納関係コードマスタ抽出（オーバーロード）
    '* 
    '* 構文           Public Overloads Function GetDainoKBHoshu(ByVal strDainoKB As String) As DataSet
    '* 
    '* 機能　　    　　指定された代納区分を条件に代納関係コードマスタの該当データを取得する。
    '* 
    '* 引数           strDainoKB As String  :代納区分
    '* 
    '* 戻り値         取得した代納関係コードマスタの該当データ（DataSet）
    '*                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu(ByVal strDainoKB As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu(String)"
        Dim csDainoKankeiCDMSTEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB
            cfUFParameterClass.Value = strDainoKB

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
            csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csDainoKankeiCDMSTEntity

    End Function

    '* 履歴番号 000005 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名     代納関係コードマスタ抽出
    '* 
    '* 構文           Public Overloads Function GetDainoKBHoshu2(ByVal strDainoKB As String) As DataSet
    '* 
    '* 機能　　    　　指定された代納区分を条件に代納関係コードマスタの該当データを取得する。
    '* 
    '* 引数           strDainoKB As String  :代納区分
    '* 
    '* 戻り値         取得した代納関係コードマスタの該当データ（DataRow）
    '*                   構造：csDainoKankeiCDMSTEntity    インテリセンス：ABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu2(ByVal strDainoKB As String) As DataRow()
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu2(String)"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDainoKankeiCDMSTEntity As DataSet
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim strSQL As New StringBuilder()
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim cfUFParameterClass As UFParameterClass
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDainoKankeiCDMSTDataRows As DataRow()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_csDainoKankeiCDMSTEntity Is Nothing) Then
                ' SQL文の作成
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
                strSQL.Append(" WHERE ")
                strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "【クラス名:" + Me.GetType.Name + "】" + _
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                            "【実行メソッド名:GetDataSet】" + _
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
                '*履歴番号 000004 2003/08/28 修正終了

                ' SQLの実行 DataSetの取得

                m_csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

            strSQL.RRemove(0, strSQL.RLength)
            strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strSQL.Append(" = '")
            strSQL.Append(strDainoKB)
            strSQL.Append("'")
            csDainoKankeiCDMSTDataRows = m_csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Select(strSQL.ToString())

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

        Return csDainoKankeiCDMSTDataRows

    End Function
    '* 履歴番号 000005 2005/01/25 追加終了（宮沢）

    '************************************************************************************************
    '* メソッド名     代納関係コードマスタ追加
    '* 
    '* 構文           Public Function InsertDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  代納関係コードマスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertDainoKB"
        Dim cfParam As UFParameterClass
        Dim csDataColumn As DataColumn
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          ' 作成日時

            '共通項目の編集を行う
            csDataRow(ABDainoKankeiCDMSTEntity.TANMATSUID) = m_cfControlData.m_strClientId          ' 端末ＩＤ
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUJOFG) = "0"                                      ' 削除フラグ
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER) = Decimal.Zero                        ' 更新カウンタ
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI) = strUpdateDateTime                  ' 作成日時
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId           ' 作成ユーザー
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINNICHIJI) = strUpdateDateTime                   ' 更新日時
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINUSER) = m_cfControlData.m_strUserId            ' 更新ユーザー

            '当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                'データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            'パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
    '* メソッド名     代納関係コードマスタ更新
    '* 
    '* 構文           Public Function UpdateDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  代納関係コードマスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateDainoKB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intUpdCnt As Integer
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim strUpdateDateTime As String
        '* corresponds to VS2008 End 2010/04/16 000006

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '共通項目の編集を行う
            csDataRow(ABDainoKankeiCDMSTEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER)) + 1     '更新カウンタ
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '更新日時
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength) = ABDainoKankeiCDMSTEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    'パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

    '************************************************************************************************
    '* メソッド名     代納関係コードマスタ削除
    '* 
    '* 構文           Public Function DeleteDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  代納関係コードマスタのデータを削除する。
    '* 
    '* 引数           csDataRow As DataRow  :削除データ
    '* 
    '* 戻り値         削除件数(Integer)
    '************************************************************************************************
    Public Function DeleteDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteDainoKB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intDelCnt As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength) = ABDainoKankeiCDMSTEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
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
            '*履歴番号 000004 2003/08/28 修正終了

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "DeleteDainoKB")

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
        Dim strInsertColumn As String
        Dim strInsertParam As String
        Dim cfUFParameterClass As UFParameterClass
        Dim strDeleteSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " SET "

            ' DELETE SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strDeleteSQL.Append(" WHERE ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strDeleteSQL.Append(" = ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB)
            strDeleteSQL.Append(" AND ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER)
            strDeleteSQL.Append(" = ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' DELETE パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            m_strUpdateSQL += " WHERE " + ABDainoKankeiCDMSTEntity.DAINOKB + " = " + ABDainoKankeiCDMSTEntity.KEY_DAINOKB + " AND " + _
                                          ABDainoKankeiCDMSTEntity.KOSHINCOUNTER + " = " + ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER

            ' UPDATE,DELETE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            'デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "CreateSQL")

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
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       代納関係コードマスタのデータ整合性チェックを行います。
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

            Select Case strColumnName.ToUpper()
                Case ABDainoKankeiCDMSTEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKBMEISHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBRYAKUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.TANMATSUID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUJOFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINCOUNTER
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUSEIUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINNICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINUSER)
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
