'************************************************************************************************
'* 業務名　　　　   宛名管理システム
'* 
'* クラス名　　　   ABKojinSeigyoBClass：宛名個人情報制御Bクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 作成日付　　     2011/01/18
'*
'* 作成者　　　　   2901 夘之原　和慶
'* 
'* 著作権　　　　   （株）電算
'* 
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2011/04/19  000001      宛名個人制御情報全件取得(戻り:UFDataReaderClass型)メソッド追加（夘之原）
'* 2011/05/10  000002      宛名個人制御情報全件取得時のソート設定追加（夘之原）
'* 2012/06/21  000003      【AB18010】八街市－異動分抽出対応（中嶋）
'* 2012/08/17  000004      【AB18010】異動分の並び順が住民コード順になっていない不具合修正（中嶋）
'* 2016/01/07  000005      【AB00163】個人制御の同一人対応（石合）
'* 2023/10/16  000006      【AB-0890-1】個人制御情報詳細管理項目追加_公開系(下村)
'* 2023/10/25  000007      【AB-1000-1】個人制御同一個人番号者対応(下村)
'* 2024/01/10  000008      【AB-0120-1】 住民データ異動中の排他制御
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

#Region "参照名前空間"

Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports Densan.Reams.AB.AB001BX
'*履歴番号 000003 2012/06/21 追加開始
Imports System.Collections.Generic
'*履歴番号 000003 2012/06/21 追加終了
#End Region

Public Class ABKojinSeigyoBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigData As UFConfigDataClass             ' コンフィグデータ
    Private m_cfRdb As UFRdbClass                           ' ＲＤＢクラス
    Private m_cfError As UFErrorClass                       ' エラー処理クラス
    Private m_cABLogX As ABLogXClass                       ' ABログ出力Xクラス
    '*履歴番号 000008 2024/01/10 追加開始
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_csSb As StringBuilder
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    '*履歴番号 000008 2024/01/10 追加終了

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKojinSeigyoBClass"
    '*履歴番号 000003 2012/06/21 追加開始
    Private Const PRM_CHUSHUTSU_ST_YMD As String = "@CHUSHUTSU_ST_YMD"
    Private Const PRM_CHUSHUTSU_ED_YMD As String = "@CHUSHUTSU_ED_YMD"
    Private Const PRM_CHUSHUTSU_ST_YMDHMS As String = "@CHUSHUTSU_ST_YMDHMS"
    Private Const PRM_CHUSHUTSU_ED_YMDHMS As String = "@CHUSHUTSU_ED_YMDHMS"
    '*履歴番号 000003 2012/06/21 追加終了
    '*履歴番号 000008 2024/01/10 追加開始
    Private Const STR_SELECT_WHERE As String = " WHERE "
    Private Const STR_SELECT_AND As String = " And "
    Private Const STR_SELECT_LEFTKAKKO As String = "("
    Private Const STR_SELECT_RIGHTKAKKO As String = ")"
    Private Const STR_SELECT_EQUAL As String = " = "
    Private Const STR_SELECT_NOTEQUAL As String = " <> "
    Private Const STR_SQL_UPDATE As String = "UPDATE "
    Private Const STR_SQL_INSERT As String = "INSERT INTO "
    Private Const STR_SQL_SET As String = " SET "
    Private Const STR_SQL_VALUES As String = " VALUES ("
    Private Const STR_SQL_KANMA As String = ", "
    Private Const STR_SQL_KUHAKU As String = " "
    Private Const SAKUJOFG_1 As String = "'1'"
    Private Const SAKUJOFG_0 As String = "0"
    Private Const STR_CLASSNAME As String = "【クラス名:"
    Private Const STR_KAKKO As String = "】"
    Private Const STR_UFAPPEXCEPTION_METHODNAME As String = "【メソッド名:"
    Private Const STR_EXCEPTION_ERRORNAME As String = "【エラー内容:"
    Private Const STR_JIKKOMETHODNAME_EXECUTESQL As String = "【実行メソッド名:ExecuteSQL】"
    Private Const STR_SQLNAIYOU As String = "【SQL内容:"
    Private Const STR_DATEFORMATE As String = "yyyyMMddHHmmssfff"
    '*履歴番号 000008 2024/01/10 追加終了
#End Region

    '*履歴番号 000003 2012/06/21 追加開始
#Region "構造体"
    Public Structure KikanColName
        Public m_strSTCol As String
        Public m_strEDCol As String
    End Structure
#End Region
    '*履歴番号 000003 2012/06/21 追加終了

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

        '*履歴番号 000008 2024/01/10 追加開始
        m_CsSb = New StringBuilder
        '*履歴番号 000008 2024/01/10 追加終了

    End Sub
#End Region

#Region "宛名個人制御情報取得"
    '************************************************************************************************
    '* メソッド名     宛名個人制御情報取得
    '* 
    '* 構文           Public Function GetABKojinSeigyo(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　 宛名個人制御情報マスタから条件に合うものを取得する。
    '* 
    '* 引数           ByVal strJuminCD As String  :　住民コード
    '* 
    '* 戻り値         取得した宛名個人制御情報の該当データ（DataSet）
    '*                構造：csABKojinSeigyoEntity
    '************************************************************************************************
    Public Function GetABKojinSeigyo(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetABKojinSeigyo"           ' メソッド名
        Dim csABKojinSeigyoEntity As DataSet                            ' 個人制御情報データ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABKojinseigyomstEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABKojinseigyomstEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABKojinseigyomstEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass))

            ' SQLの実行 DataSetの取得
            csABKojinSeigyoEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABKojinseigyomstEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csABKojinSeigyoEntity

    End Function

    '*履歴番号 000005 2016/01/07 追加開始
    ''' <summary>
    ''' 個人制御データ取得
    ''' </summary>
    ''' <param name="a_strJuminCD">住民コード文字列配列</param>
    ''' <returns>個人制御データ</returns>
    ''' <remarks></remarks>
    Public Function GetABKojinSeigyo(ByVal a_strJuminCD() As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim cfParameter As UFParameterClass
        Dim strParameterName As String

        Try

            ' デバッグ開始ログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQL文の作成
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABKojinseigyomstEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABKojinseigyomstEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To a_strJuminCD.Length - 1

                    ' -----------------------------------------------------------------------------
                    ' 住民コード
                    strParameterName = ABKojinseigyomstEntity.KEY_JUMINCD + i.ToString

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
                .Append(ABKojinseigyomstEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" ORDER BY ")
                .Append(ABKojinseigyomstEntity.JUMINCD)

            End With

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(THIS_METHOD_NAME, m_cfRdb.GetDevelopmentSQLString(csSQL.ToString, cfParameterCollection))

            ' SQLの実行 DataSetの取得
            csDataSet = m_cfRdb.GetDataSet(csSQL.ToString, ABKojinseigyomstEntity.TABLE_NAME, cfParameterCollection)

            ' デバッグ終了ログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch cfRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ

            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, cfRdbTimeOutExp.p_strErrorCode, cfRdbTimeOutExp.Message)
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, cfAppExp.p_strErrorCode, cfAppExp.Message)
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception 'システムエラーをキャッチ

            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, csExp.Message)
            ' システムエラーをスローする
            Throw

        End Try

        Return csDataSet

    End Function
    '*履歴番号 000005 2016/01/07 追加終了

#End Region


    '*履歴番号 000001 2011/04/19 追加開始
#Region "宛名個人制御情報全件取得(戻り:UFDataReaderClass)"
    '************************************************************************************************
    '* メソッド名     宛名個人制御情報全件取得
    '* 
    '* 構文           Public Function GetABKojinSeigyo() As UFDataReaderClass
    '* 
    '* 機能　　    　 宛名個人制御情報マスタから全件取得する。(UFDataReaderClass型)
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         取得した宛名個人制御情報のDataReader（UFDataReaderClass）
    '*                構造：cfABKojinSeigyoDataReader
    '************************************************************************************************
    Public Function GetABKojinSeigyo() As UFDataReaderClass
        Const THIS_METHOD_NAME As String = "GetABKojinSeigyo"           ' メソッド名
        Dim cfABKojinSeigyoDataReader As UFDataReaderClass              ' 個人制御情報DataReader
        Dim strSQL As New StringBuilder                                 ' SQL文文字列

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")
            '*履歴番号 000002 2011/05/10 追加開始
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABKojinseigyomstEntity.JUMINCD)
            strSQL.Append(" ASC")
            '*履歴番号 000002 2011/05/10 追加終了

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString))

            ' SQLの実行 DataSetの取得
            cfABKojinSeigyoDataReader = m_cfRdb.GetDataReader(strSQL.ToString)

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

        Return cfABKojinSeigyoDataReader

    End Function
#End Region
    '*履歴番号 000001 2011/04/19 追加終了

    '*履歴番号 000003 2012/06/21 追加開始
#Region "宛名個人制御情報取得(UFDataReaderClass型)（異動分）"
    '************************************************************************************************
    '* メソッド名     宛名個人制御情報取得(UFDataReaderClass型)（異動分）
    '* 
    '* 構文               Public Function GetABKojinSeigyo(ByVal strChushutsuST As String, ByVal strChushutsuED As String _
    '*                                 , ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As DataSet
    '* 
    '* 機能　　    　 宛名個人制御情報マスタから全件取得する。(UFDataReaderClass型)
    '* 
    '* 引数           strChushutsuST：抽出開始日時 strChushutsuED：抽出終了日時 
    '*                enSeigyoShiteiKBN：制御指定区分,strShuryoHanteiKBN：終了判定区分
    '* 
    '* 戻り値         取得した宛名個人制御情報のDataReader（UFDataReaderClass）
    '*                構造：cfABKojinSeigyoDataReader
    '************************************************************************************************
    '*履歴番号 000005 2016/01/07 修正開始
    'Public Function GetABKojinSeigyo(ByVal strChushutsuST As String, ByVal strChushutsuED As String _
    '                                 , ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As UFDataReaderClass
    Public Function GetABKojinSeigyo( _
        ByVal strChushutsuST As String, _
        ByVal strChushutsuED As String, _
        ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, _
        ByVal strShuryoHanteiKBN As String, _
        ByVal strDoitsuninUmu As String) As UFDataReaderClass
        '*履歴番号 000005 2016/01/07 修正終了
        Const THIS_METHOD_NAME As String = "GetABKojinSeigyo"           ' メソッド名
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfParameterCollection As UFParameterCollectionClass         ' パラメータクラス
        Dim cfABKojinSeigyoDataReader As UFDataReaderClass              ' 個人制御情報DataReader
        '*履歴番号 000005 2016/01/07 追加開始
        Dim csIdobunKojinSeigyo As StringBuilder
        Dim intSelectLength As Integer = "SELECT *".RLength
        '*履歴番号 000005 2016/01/07 追加終了

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABKojinseigyomstEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE (")
            strSQL.Append(Me.CreateWhere(cfParameterCollection, strChushutsuST, strChushutsuED, enSeigyoShiteiKBN, strShuryoHanteiKBN))
            strSQL.Append(") AND ")
            strSQL.Append(ABKojinseigyomstEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")
            '*履歴番号 000005 2016/01/07 追加開始
            If (strDoitsuninUmu = "1") Then
                ' 同一人対応ありの場合、プラスして異動対象者の同一人の個人制御を取得する
                csIdobunKojinSeigyo = New StringBuilder(strSQL.ToString(intSelectLength, strSQL.RLength - intSelectLength))
                With strSQL
                    .Append(" UNION ")
                    .Append("SELECT * FROM ")
                    .Append(ABKojinseigyomstEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABKojinseigyomstEntity.JUMINCD)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABGappeiDoitsuninEntity.JUMINCD)
                    .Append(" FROM ")
                    .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(" FROM ")
                    .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABGappeiDoitsuninEntity.JUMINCD)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABKojinseigyomstEntity.JUMINCD)
                    .Append(csIdobunKojinSeigyo)
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABKojinseigyomstEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(" UNION ")
                    .Append("SELECT * FROM ")
                    .Append(ABKojinseigyomstEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABKojinseigyomstEntity.JUMINCD)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABMyNumberEntity.JUMINCD)
                    .Append(" FROM ")
                    .Append(ABMyNumberEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABMyNumberEntity.MYNUMBER)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABMyNumberEntity.MYNUMBER)
                    .Append(" FROM ")
                    .Append(ABMyNumberEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABMyNumberEntity.JUMINCD)
                    .Append(" IN (")
                    .Append("SELECT ")
                    .Append(ABKojinseigyomstEntity.JUMINCD)
                    .Append(csIdobunKojinSeigyo)
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABMyNumberEntity.CKINKB)
                    .Append(" = '1'")
                    .Append(" AND ")
                    .Append(ABMyNumberEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABMyNumberEntity.CKINKB)
                    .Append(" = '1'")
                    .Append(" AND ")
                    .Append(ABMyNumberEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(")")
                    .Append(" AND ")
                    .Append(ABKojinseigyomstEntity.SAKUJOFG)
                    .Append(" <> '1'")
                End With
            Else
                ' noop
            End If
            '*履歴番号 000005 2016/01/07 追加終了
            '*履歴番号 000004 2012/08/17 追加開始
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABKojinseigyomstEntity.JUMINCD)
            strSQL.Append(" ASC")
            '*履歴番号 000004 2012/08/17 追加終了

            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection))

            ' SQLの実行 DataSetの取得
            cfABKojinSeigyoDataReader = m_cfRdb.GetDataReader(strSQL.ToString, cfParameterCollection)

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

        Return cfABKojinSeigyoDataReader

    End Function
#End Region

#Region "Where句作成：異動分抽出"
    '************************************************************************************************
    '* メソッド名     Where句作成：異動分抽出
    '* 
    '* 構文           Private Function CreateWhere(ByVal cfParameterCollection As UFParameterCollectionClass, _
    '*                             ByVal strChushutsuST As String, ByVal strChushutsuED As String, _
    '*                             ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As String
    '* 
    '* 機能　　    　 異動分のWhere句を作成する
    '* 
    '* 引数           cfParameterCollection：パラメータコレクション strChushutsuST：抽出開始日時 strChushutsuED：抽出終了日時 
    '*                enSeigyoShiteiKBN：制御指定区分,strShuryoHanteiKBN：終了判定区分
    '* 
    '* 戻り値         Where句
    '************************************************************************************************
    Private Function CreateWhere(ByRef cfParameterCollection As UFParameterCollectionClass, _
                                 ByVal strChushutsuST As String, ByVal strChushutsuED As String, _
                                 ByVal enSeigyoShiteiKBN As ABSeigyoKbnType, ByVal strShuryoHanteiKBN As String) As String
        Dim csKikanList As List(Of KikanColName)
        Dim cKikanColName As KikanColName
        Dim csWhere As StringBuilder
        Try
            csKikanList = New List(Of KikanColName)

            Select Case enSeigyoShiteiKBN
                Case ABSeigyoKbnType.SeigyoShinai
                    '指定なしの時、全ての期間をチェックする
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.DVTAISHOKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.DVTAISHOSHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA1KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA1SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA2KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA2SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA3KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA3SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '仮登録の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.KARITOROKUKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.KARITOROKUSHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '特別養子の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '処理注意１の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '処理注意２の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '処理注意３の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '処理保留の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD
                    csKikanList.Add(cKikanColName)
                    '参照不可の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD
                    csKikanList.Add(cKikanColName)

                Case ABSeigyoKbnType.DVTaishoSha
                    'DV対象の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.DVTAISHOKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.DVTAISHOSHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.HakkoTeishi
                    '発行停止の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.JittaiChosa
                    '実態調査の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.SeinenHiKokennin
                    '成年被後見人の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.Sonota1
                    'その他１の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA1KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA1SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.Sonota2
                    'その他２の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA2KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA2SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.Sonota3
                    'その他３の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SONOTA3KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SONOTA3SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.KariToroku
                    '仮登録の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.KARITOROKUKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.KARITOROKUSHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.TokubetsuYoshi
                    '特別養子の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.TokubetsuJijo
                    '特別事情の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.ShoriChui1
                    '処理注意１の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.ShoriChui2
                    '処理注意２の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.ShoriChui3
                    '処理注意３の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.ShoriHoryu
                    '処理保留の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD
                    csKikanList.Add(cKikanColName)
                Case ABSeigyoKbnType.SanshoFuka
                    '参照不可の時
                    cKikanColName = New KikanColName
                    cKikanColName.m_strSTCol = ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD
                    cKikanColName.m_strEDCol = ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD
                    csKikanList.Add(cKikanColName)
            End Select

            csWhere = New StringBuilder
            With csWhere
                '更新日時での抽出条件
                .AppendFormat(" ({0} < {1} AND {1} <= {2}) OR ", PRM_CHUSHUTSU_ST_YMDHMS, ABKojinseigyomstEntity.KOSHINNICHIJI, PRM_CHUSHUTSU_ED_YMDHMS)
                If (strChushutsuST.RSubstring(0, 8) <> strChushutsuED.RSubstring(0, 8)) Then
                    '抽出開始日・終了日が違う時、開始・終了期間で抽出する
                    For Each csKikan As KikanColName In csKikanList
                        '期間リスト分ループ
                        .AppendFormat(" ({0} < {1} AND {1} <= {2}) OR ", PRM_CHUSHUTSU_ST_YMD, csKikan.m_strSTCol, PRM_CHUSHUTSU_ED_YMD)
                        If (strShuryoHanteiKBN = "1") Then
                            '終了判定区分が"1"の時、終了日もチェックする
                            .AppendFormat(" ({0} <= {1} AND {1} < {2}) OR ", PRM_CHUSHUTSU_ST_YMD, csKikan.m_strEDCol, PRM_CHUSHUTSU_ED_YMD)
                        Else
                            '何もしない
                        End If
                    Next
                Else
                    '何もしない
                End If
            End With

            'パラメータの作成
            If (cfParameterCollection Is Nothing) Then
                cfParameterCollection = New UFParameterCollectionClass
            Else
                '何もしない
            End If

            '抽出開始日時
            cfParameterCollection.Add(PRM_CHUSHUTSU_ST_YMDHMS, strChushutsuST)
            '抽出終了日時
            cfParameterCollection.Add(PRM_CHUSHUTSU_ED_YMDHMS, strChushutsuED)
            '抽出開始日
            cfParameterCollection.Add(PRM_CHUSHUTSU_ST_YMD, strChushutsuST.RSubstring(0, 8))
            '抽出終了日
            cfParameterCollection.Add(PRM_CHUSHUTSU_ED_YMD, strChushutsuED.RSubstring(0, 8))


        Catch csException As Exception
            Throw
        End Try

        Return csWhere.ToString.TrimEnd("OR ".ToCharArray)
    End Function
#End Region
    '*履歴番号 000003 2012/06/21 終了開始

    '*履歴番号 000008 2024/01/10 追加開始
#Region "個人制御情報マスタデータ更新メソッド"
    '************************************************************************************************
    '* メソッド名   個人制御情報マスタデータ更新メソッド
    '* 
    '* 構文         Public Function UpdateKojinSeigyo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     個人制御情報マスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   :  個人制御情報データ(Kojinseigyomst)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateKojinSeigyo(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateKojinSeigyo"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        Dim intUpdCnt As Integer = 0                            ' 更新件数

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL Is Nothing Or m_strUpDateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABKojinseigyomstEntity.TANMATSUID) = _
                                                m_cfControlData.m_strClientId           ' 端末ＩＤ
            csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER) = _
                        CDec(csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER)) + 1       ' 更新カウンタ
            csDataRow(ABKojinseigyomstEntity.KOSHINNICHIJI) = _
                            m_cfRdb.GetSystemDate.ToString(STR_DATEFORMATE)             ' 更新日時
            csDataRow(ABKojinseigyomstEntity.KOSHINUSER) = m_cfControlData.m_strUserId  '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABKojinseigyomstEntity.PREFIX_KEY.RLength) =
                                                            ABKojinseigyomstEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(
                                                        ABKojinseigyomstEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                                        csDataRow(cfParam.ParameterName.RSubstring(
                                             ABKojinseigyomstEntity.PARAM_PLACEHOLDER.RLength),
                                                            DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            With m_csSb
                .RRemove(0, .RLength)
                .Append(STR_CLASSNAME)
                .Append(Me.GetType.Name)
                .Append(STR_KAKKO)
                .Append(STR_UFAPPEXCEPTION_METHODNAME)
                .Append(System.Reflection.MethodBase.GetCurrentMethod.Name)
                .Append(STR_KAKKO)
                .Append(STR_JIKKOMETHODNAME_EXECUTESQL)
                .Append(STR_SQLNAIYOU)
                .Append(m_cfRdb.GetDevelopmentSQLString(m_strUpDateSQL, _
                        m_cfUpdateUFParameterCollectionClass))
                .Append(STR_KAKKO)
            End With
            
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(m_csSb.ToString))

            ' SQLの実行
            intUpdCnt = m_cfRdb.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw New UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException)

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function
#End Region

#Region "個人制御情報マスタデータ追加メソッド"
    '************************************************************************************************
    '* メソッド名   個人制御情報マスタデータ追加メソッド
    '* 
    '* 構文         Public Function InsertKojinSeigyo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     個人制御情報マスタに新規データを追加する。
    '* 
    '* 引数         csDataRow As DataRow   : 個人制御情報データ(Kojinseigyomst)
    '* 
    '* 戻り値       追加件数(Integer)
    '************************************************************************************************
    Public Function InsertKojinSeigyo(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertKojinSeigyo"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        Dim intInsCnt As Integer = 0                                    ' 追加件数
        Dim strUpdateDateTime As String = String.Empty                  ' システム日付

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdb.GetSystemDate.ToString(STR_DATEFORMATE)               ' 作成日時

            ' 共通項目の編集を行う
            csDataRow(ABKojinseigyomstEntity.TANMATSUID) = m_cfControlData.m_strClientId      ' 端末ＩＤ
            csDataRow(ABKojinseigyomstEntity.SAKUJOFG) = SAKUJOFG_0                           ' 削除フラグ
            csDataRow(ABKojinseigyomstEntity.KOSHINCOUNTER) = Decimal.Zero                    ' 更新カウンタ
            csDataRow(ABKojinseigyomstEntity.SAKUSEINICHIJI) = strUpdateDateTime              ' 作成日時
            csDataRow(ABKojinseigyomstEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId       ' 作成ユーザー
            csDataRow(ABKojinseigyomstEntity.KOSHINNICHIJI) = strUpdateDateTime               ' 更新日時
            csDataRow(ABKojinseigyomstEntity.KOSHINUSER) = m_cfControlData.m_strUserId        ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                                    ABKojinseigyomstEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            With m_csSb
                .RRemove(0, .RLength)
                .Append(STR_CLASSNAME)
                .Append(Me.GetType.Name)
                .Append(STR_KAKKO)
                .Append(STR_UFAPPEXCEPTION_METHODNAME)
                .Append(System.Reflection.MethodBase.GetCurrentMethod.Name)
                .Append(STR_KAKKO)
                .Append(STR_JIKKOMETHODNAME_EXECUTESQL)
                .Append(STR_SQLNAIYOU)
                .Append(m_cfRdb.GetDevelopmentSQLString(m_strInsertSQL, _
                        m_cfUpdateUFParameterCollectionClass))
                .Append(STR_KAKKO)
            End With

            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(m_csSb.ToString))

            ' SQLの実行
            intInsCnt = m_cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw New UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException)


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
    '* 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
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
        Dim strWhereSB As StringBuilder                             ' 更新削除SQL文Where文文字列
        Dim strInsertSQLSB As StringBuilder
        Dim strUpDateSQLSB As StringBuilder
        Dim strInsertColumn As String = String.Empty
        Dim strInsertParam As String = String.Empty

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)
            strWhereSB = New StringBuilder
            strInsertSQLSB = New StringBuilder
            strUpDateSQLSB = New StringBuilder
            strInsertColumnSB = New StringBuilder
            strInsertParamSB = New StringBuilder

            ' INSERT SQL文の作成
            strInsertSQLSB.Append(STR_SQL_INSERT).Append(ABKojinseigyomstEntity.TABLE_NAME)
            strInsertSQLSB.Append(STR_SQL_KUHAKU)

            ' UPDATE SQL文の作成
            strUpDateSQLSB.Append(STR_SQL_UPDATE).Append(ABKojinseigyomstEntity.TABLE_NAME)
            strUpDateSQLSB.Append(STR_SQL_SET)

            ' UPDATE Where文作成
            With strWhereSB
                .Append(STR_SELECT_WHERE)
                .Append(ABKojinseigyomstEntity.JUMINCD)
                .Append(STR_SELECT_EQUAL)
                .Append(ABKojinseigyomstEntity.PREFIX_KEY)
                .Append(ABKojinseigyomstEntity.JUMINCD)
                .Append(STR_SELECT_AND)
                .Append(ABKojinseigyomstEntity.SAKUJOFG)
                .Append(STR_SELECT_NOTEQUAL)
                .Append(SAKUJOFG_1)
                .Append(STR_SELECT_AND)
                .Append(ABKojinseigyomstEntity.KOSHINCOUNTER)
                .Append(STR_SELECT_EQUAL)
                .Append(ABKojinseigyomstEntity.PREFIX_KEY)
                .Append(ABKojinseigyomstEntity.KOSHINCOUNTER)
            End With

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumnSB.Append(csDataColumn.ColumnName).Append(STR_SQL_KANMA)

                With strInsertParamSB
                    .Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                    .Append(STR_SQL_KANMA)
                End With

                ' UPDATE SQL文の作成
                With strUpDateSQLSB
                    .Append(csDataColumn.ColumnName).Append(STR_SELECT_EQUAL)
                    .Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                    .Append(STR_SQL_KANMA)
                End With

                ' INSERT コレクションにパラメータを追加
                With m_csSb
                    .RRemove(0, .RLength)
                    .Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                End With
                cfUFParameterClass.ParameterName = m_csSb.ToString

                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                With m_csSb
                    .RRemove(0, .RLength)
                    .Append(ABKojinseigyomstEntity.PARAM_PLACEHOLDER)
                    .Append(csDataColumn.ColumnName)
                End With
                cfUFParameterClass.ParameterName = m_csSb.ToString
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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

            ' UPDATE SQL文のトリミング
            m_strUpDateSQL = strUpDateSQLSB.ToString.Trim()
            m_strUpDateSQL = m_strUpDateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            strUpDateSQLSB.RRemove(0, strUpDateSQLSB.RLength)
            strUpDateSQLSB.Append(m_strUpDateSQL)
            strUpDateSQLSB.Append(strWhereSB.ToString)
            m_strUpDateSQL = strUpDateSQLSB.ToString

            ' UPDATE コレクションにキー情報を追加
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            With m_csSb
                .RRemove(0, .RLength)
                .Append(ABKojinseigyomstEntity.PREFIX_KEY)
                .Append(ABKojinseigyomstEntity.JUMINCD)
            End With
            cfUFParameterClass.ParameterName = m_csSb.ToString
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            With m_csSb
                .RRemove(0, .RLength)
                .Append(ABKojinseigyomstEntity.PREFIX_KEY)
                .Append(ABKojinseigyomstEntity.KOSHINCOUNTER)
            End With
            cfUFParameterClass.ParameterName = m_csSb.ToString
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw New UFAppException(exAppException.Message, exAppException.p_intErrorCode, exAppException)


        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub
#End Region
    '*履歴番号 000008 2024/01/10 追加終了
#End Region

End Class
