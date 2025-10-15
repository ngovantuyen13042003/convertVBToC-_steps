'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         個人制御履歴DA
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付             2023/10/13
'*
'* 作成者　　　     下村　美江
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
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

Public Class ABKojinseigyoRirekiBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cABLogX As ABLogXClass                        ' ABログ出力Xクラス
    Private m_csDataSchma As DataSet                        ' スキーマ保管用データセット:全項目用
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    'INSERT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass
    Private m_CsSb As StringBuilder
#End Region

#Region "コンスタント定義"
    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKojinseigyoRirekiBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
    Private Const STR_SELECT_ALL As String = "SELECT *"
    Private Const STR_SELECT_FROM As String = " FROM "
    Private Const STR_SELECT_WHERE As String = " WHERE "
    Private Const STR_SELECT_AND As String = " And "
    Private Const STR_SELECT_LEFTKAKKO As String = "("
    Private Const STR_SELECT_RIGHTKAKKO As String = ")"
    Private Const STR_SELECT_EQUAL As String = " = "
    Private Const STR_SELECT_NOTEQUAL As String = " <> "
    Private Const STR_SQL_INSERT As String = "INSERT INTO "
    Private Const STR_SQL_SET As String = " SET "
    Private Const STR_SQL_VALUES As String = " VALUES ("
    Private Const STR_SQL_KANMA As String = ", "
    Private Const STR_SQL_KUHAKU As String = " "
    Private Const STR_SELECT_ORDERBY As String = " ORDER BY "

    Private Const SAKUJOFG_1 As String = "'1'"
    Private Const SAKUJOFG_0 As String = "0"

    Private Const STR_DATEFORMATE As String = "yyyyMMddHHmmssfff"
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

        ' ABログ出力クラスのインスタンス化
        m_cABLogX = New ABLogXClass(m_cfControlData, m_cfConfigDataClass, THIS_CLASS_NAME)

        m_CsSb = New StringBuilder
        With m_CsSb
            .RRemove(0, .RLength)
            .Append(STR_SELECT_ALL)
            .Append(STR_SELECT_FROM)
            .Append(ABKojinseigyoRirekiEntity.TABLE_NAME)
        End With
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(m_CsSb.ToString,
                                                            ABKojinseigyoRirekiEntity.TABLE_NAME, False)
    End Sub
#End Region

#Region "メソッド"

#Region "個人制御履歴取得メソッド"
    '************************************************************************************************
    '* メソッド名   個人制御履歴取得メソッド
    '* 
    '* 構文         Public Function GetKojinseigyoRireki(
    '                                   ByVal strJuminCd As String) As DataSet
    '* 
    '* 機能　　     個人制御履歴より該当データを取得する。
    '* 
    '* 引数         strJuminCd As String   : 住民コード
    '* 
    '* 戻り値       取得した個人制御履歴の該当データ（DataSet）
    '*                   
    '************************************************************************************************
    Public Function GetKojinseigyoRireki(ByVal strJuminCd As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetKojinseigyoRireki"
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim csABKojinSeigyoRirekiEntity As DataSet                      ' 個人制御履歴DataSet
        Dim strSQL As StringBuilder

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            If (strJuminCd = String.Empty) Then
                Return Nothing
            End If

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            strSQL = New StringBuilder
            ' SELECT句
            With strSQL
                .Append(STR_SELECT_ALL)
                .Append(STR_SELECT_FROM).Append(ABKojinseigyoRirekiEntity.TABLE_NAME)

                ' ﾃﾞｰﾀｽｷｰﾏの取得
                If (m_csDataSchma Is Nothing) Then
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABKojinseigyoRirekiEntity.TABLE_NAME, False)
                End If

                ' WHERE句
                .Append(STR_SELECT_WHERE)
                ' 住民コード
                .Append(ABKojinseigyoRirekiEntity.JUMINCD)
                .Append(STR_SELECT_EQUAL)
                .Append(ABKojinseigyoRirekiEntity.KEY_JUMINCD)
                .Append(STR_SELECT_AND)
                .Append(ABKojinseigyoRirekiEntity.SAKUJOFG)
                .Append(STR_SELECT_NOTEQUAL)
                .Append(SAKUJOFG_1)
                .Append(STR_SELECT_ORDERBY)
                .Append(ABKojinseigyoRirekiEntity.RIREKINO)
                .Append(STR_SQL_KANMA)
                .Append(ABKojinseigyoRirekiEntity.RIREKIEDABAN)

                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABKojinseigyoRirekiEntity.KEY_JUMINCD
                cfUFParameterClass.Value = strJuminCd
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End With

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass))

            ' SQLの実行 DataSetの取得
            csABKojinSeigyoRirekiEntity = m_csDataSchma.Clone()
            csABKojinSeigyoRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABKojinseigyoRirekiEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message)
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csABKojinSeigyoRirekiEntity

    End Function
#End Region

#Region "個人制御履歴データ追加メソッド"
    '************************************************************************************************
    '* メソッド名   個人制御履歴データ追加メソッド
    '* 
    '* 構文         Public Function InsertKojinseigyoRireki(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     個人制御履歴に新規データを追加する。
    '* 
    '* 引数         csDataRow As DataRow   : 個人制御履歴データ(Kojinseigyomst)
    '* 
    '* 戻り値       追加件数(Integer)
    '************************************************************************************************
    Public Function InsertKojinseigyoRireki(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertKojinseigyoRireki"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        Dim intInsCnt As Integer = 0                                    ' 追加件数
        Dim strUpdateDateTime As String = String.Empty                  ' システム日付

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString(STR_DATEFORMATE)        ' 作成日時

            ' 共通項目の編集を行う
            csDataRow(ABKojinseigyoRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId      ' 端末ＩＤ
            csDataRow(ABKojinseigyoRirekiEntity.SAKUJOFG) = SAKUJOFG_0                           ' 削除フラグ
            csDataRow(ABKojinseigyoRirekiEntity.KOSHINCOUNTER) = Decimal.Zero                    ' 更新カウンタ
            csDataRow(ABKojinseigyoRirekiEntity.SAKUSEINICHIJI) = strUpdateDateTime              ' 作成日時
            csDataRow(ABKojinseigyoRirekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId       ' 作成ユーザー
            csDataRow(ABKojinseigyoRirekiEntity.KOSHINNICHIJI) = strUpdateDateTime               ' 更新日時
            csDataRow(ABKojinseigyoRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId        ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                                    ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass))

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intInsCnt

    End Function
#End Region

#Region " SQL文の作成"
    '************************************************************************************************
    '* メソッド名   SQL文の作成
    '* 
    '* 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　     INSERTSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数         csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  ' パラメータクラス
        Dim strInsertColumnSB As StringBuilder                      ' 追加SQL文項目文字列
        Dim strInsertParamSB As StringBuilder                       ' 追加SQL文パラメータ文字列
        Dim strInsertSQLSB As StringBuilder
        Dim strInsertColumn As String = String.Empty
        Dim strInsertParam As String = String.Empty

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            strInsertSQLSB = New StringBuilder
            strInsertColumnSB = New StringBuilder
            strInsertParamSB = New StringBuilder

            ' INSERT SQL文の作成
            strInsertSQLSB.Append(STR_SQL_INSERT).Append(ABKojinseigyoRirekiEntity.TABLE_NAME)
            strInsertSQLSB.Append(STR_SQL_KUHAKU)

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumnSB.Append(csDataColumn.ColumnName).Append(STR_SQL_KANMA)

                With strInsertParamSB
                    .Append(ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                    .Append(STR_SQL_KANMA)
                End With

                ' INSERT コレクションにパラメータを追加
                With m_CsSb
                    .RRemove(0, .RLength)
                    .Append(ABKojinseigyoRirekiEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                End With
                cfUFParameterClass.ParameterName = m_CsSb.ToString

                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumnSB.ToString
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParamSB.ToString
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))

            With strInsertSQLSB
                .Append(STR_SELECT_LEFTKAKKO)
                .Append(strInsertColumn)
                .Append(STR_SELECT_RIGHTKAKKO)
                .Append(STR_SQL_VALUES)
                .Append(strInsertParam)
                .Append(STR_SELECT_RIGHTKAKKO)
                m_strInsertSQL = .ToString
            End With

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub
#End Region

#End Region

End Class
