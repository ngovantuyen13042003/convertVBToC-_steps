'************************************************************************************************
'* 業務名　　　　   宛名管理システム
'* 
'* クラス名　　　   ABKojinSeigyoTempBClass：宛名個人情報TempBクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 作成日付　　     2011/02/22
'*
'* 作成者　　　　   2901 夘之原　和慶
'* 
'* 著作権　　　　   （株）電算
'* 
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

#Region "参照名前空間"

Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports Densan.Reams.AB.AB001BX

#End Region

Public Class ABKojinSeigyoTempBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigData As UFConfigDataClass             ' コンフィグデータ
    Private m_cfRdb As UFRdbClass                           ' ＲＤＢクラス
    Private m_cfError As UFErrorClass                       ' エラー処理クラス
    Private m_cABLogX As ABLogXClass                        ' ABログ出力Xクラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strDelButuriSQL As String                     ' 物理削除用SQL
    Private m_cfInsertUFParameterCollection As UFParameterCollectionClass       ' INSERT用パラメータコレクション
    Private m_cfDelButuriUFParameterCollection As UFParameterCollectionClass    ' 物理削除用パラメータコレクション

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKojinSeigyoTempBClass"

#End Region

#Region "メソッド"

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* 　　                          ByVal cfRdb As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdb as UFRdb                          : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass, _
                   ByVal cfRdb As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigData
        m_cfRdb = cfRdb

        ' ABログ出力クラスのインスタンス化
        m_cABLogX = New ABLogXClass(m_cfControlData, m_cfConfigData, THIS_CLASS_NAME)

    End Sub
#End Region

#Region "宛名個人制御Temp取得"
    '************************************************************************************************
    '* メソッド名     宛名個人制御Temp取得
    '* 
    '* 構文           Public Function GetABKojinSeigyoTemp(ByVal strKey As String) As DataSet
    '* 
    '* 機能　　    　 宛名個人制御TempからKeyに合うデータを取得する。
    '* 
    '* 引数           ByVal strKey As String : キー情報
    '* 
    '* 戻り値         取得した宛名個人制御Tempの該当データ（DataSet）
    '*                構造：csABKojinSeigyoTempEntity
    '************************************************************************************************
    Public Function GetABKojinSeigyoTemp(ByVal strKey As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetABKojinSeigyoTemp"       ' メソッド名
        Dim csABKojinSeigyoTempEntity As DataSet                        ' 個人制御情報Tempデータ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABKojinSeigyoTempEntity.KEYCD)
            strSQL.Append(" = ")
            strSQL.Append(ABKojinSeigyoTempEntity.KEY_KEYCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABKojinSeigyoTempEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.KEY_KEYCD
            cfUFParameterClass.Value = strKey
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass))

            ' SQLの実行 DataSetの取得
            csABKojinSeigyoTempEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABKojinSeigyoTempEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csABKojinSeigyoTempEntity

    End Function
#End Region

#Region "宛名個人制御Temp追加"
    '************************************************************************************************
    '* メソッド名     宛名個人制御Temp追加
    '* 
    '* 構文           Public Function InsertABKojinSeigyoTemp(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名個人制御Tempにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertABKojinSeigyoTemp(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertABKojinSeigyoTemp"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer        '追加件数
        Dim strUpdateDateTime As String

        Try

            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollection Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABKojinSeigyoTempEntity.TANMATSUID) = m_cfControlData.m_strClientId   '端末ＩＤ
            csDataRow(ABKojinSeigyoTempEntity.SAKUJOFG) = "0"                               '削除フラグ
            csDataRow(ABKojinSeigyoTempEntity.KOSHINCOUNTER) = Decimal.Zero                 '更新カウンタ
            csDataRow(ABKojinSeigyoTempEntity.SAKUSEINICHIJI) = strUpdateDateTime           '作成日時
            csDataRow(ABKojinSeigyoTempEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    '作成ユーザー
            csDataRow(ABKojinSeigyoTempEntity.KOSHINNICHIJI) = strUpdateDateTime            '更新日時
            csDataRow(ABKojinSeigyoTempEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(THIS_METHOD_NAME, "ExecuteSQL", m_cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollection))

            ' SQLの実行
            intInsCnt = m_cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollection)

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

        Return intInsCnt

    End Function
#End Region

#Region "宛名個人制御Temp物理削除"
    '************************************************************************************************
    '* メソッド名     宛名個人制御Temp物理削除
    '* 
    '* 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　 宛名個人制御Tempのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteABKojinSeigyoTemp(ByVal csDataRow As DataRow, _
                                             ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteABKojinSeigyoTemp"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        Dim cfParam As UFParameterClass                     ' パラメータクラス
        Dim intDelCnt As Integer                            ' 削除件数

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' 削除区分のチェックを行う
            If Not (strSakujoKB = "D") Then
                ' エラー定義を取得
                m_cfError = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                objErrorStruct = m_cfError.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = "" Or _
                    IsNothing(m_cfDelButuriUFParameterCollection)) Then

                Call CreateDeleteButsuriSQL()

            End If

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfDelButuriUFParameterCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(THIS_METHOD_NAME, m_cfRdb.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdb.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection)

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

        Return intDelCnt

    End Function
#End Region

#Region "InsertSQL文作成"
    '************************************************************************************************
    '* メソッド名     Insert用SQL文の作成
    '* 
    '* 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           INSERT用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim csInsertColumn As New StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As New StringBuilder                  'INSERT用パラメータ定義
        Dim csInsertSQL As New StringBuilder                ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            csInsertSQL.Append("INSERT INTO ")
            csInsertSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME)
            csInsertSQL.Append(" (")

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")
                csInsertParam.Append(ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            csInsertSQL.Append(csInsertColumn.ToString.Trim().Trim(CType(",", Char)))
            csInsertSQL.Append(") VALUES (")
            csInsertSQL.Append(csInsertParam.ToString.Trim().TrimEnd(CType(",", Char)))
            csInsertSQL.Append(")")

            ' メンバ変数に設定する
            m_strInsertSQL = csInsertSQL.ToString

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

#Region "DeleteSQL文作成"
    '************************************************************************************************
    '* メソッド名     物理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL()
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csDeleteButsuriSQL As New StringBuilder                ' SQL文文字列

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' 物理DELETE SQL文の作成
            csDeleteButsuriSQL.Append("DELETE FROM ")
            csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.TABLE_NAME)
            csDeleteButsuriSQL.Append(" WHERE ")
            csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.KEYCD)
            csDeleteButsuriSQL.Append(" = ")
            csDeleteButsuriSQL.Append(ABKojinSeigyoTempEntity.KEY_KEYCD)

            ' メンバ変数に設定する。
            m_strDelButuriSQL = csDeleteButsuriSQL.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABKojinSeigyoTempEntity.KEY_KEYCD
            m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

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

#Region "宛名個人制御Tempスキーマ取得"
    '************************************************************************************************
    '* メソッド名     宛名個人制御Tempスキーマ取得
    '* 
    '* 構文           Public Function GetSchemaABKojinSeigyoTemp() As DataSet
    '* 
    '* 機能　　    　 宛名個人制御Tempのスキーマ情報を取得する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         宛名個人制御Tempのスキーマ（DataSet）
    '*                構造：csABKojinSeigyoTempEntity
    '************************************************************************************************
    Public Function GetSchemaABKojinSeigyoTemp() As DataSet
        Const THIS_METHOD_NAME As String = "GetSchemaABKojinSeigyoTemp"  ' メソッド名
        Dim csABKojinSeigyoTempEntity As DataSet                         ' 個人制御情報Tempデータ

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' 宛名個人制御Tempのテーブルスキーマを取得する
            csABKojinSeigyoTempEntity = m_cfRdb.GetTableSchema(ABKojinSeigyoTempEntity.TABLE_NAME)

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

        Return csABKojinSeigyoTempEntity

    End Function
#End Region

#End Region

End Class
