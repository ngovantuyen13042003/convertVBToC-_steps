'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ共通番号累積_標準マスタＤＡ(ABMyNumberRuisekiHyojunBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/10/04　下村　美江
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
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
Imports System.Data
Imports System.Text

''' <summary>
''' ＡＢ共通番号累積マスタＤＡ
''' </summary>
''' <remarks></remarks>
Public Class ABMyNumberRuisekiHyojunBClass

#Region "メンバー変数"

    ' メンバー変数
    Private m_cfLogClass As UFLogClass                                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                      ' ＲＤＢクラス

    Private m_strSelectSQL As String                                        ' SELECT用SQL
    Private m_strInsertSQL As String                                        ' INSERT用SQL
    Private m_cfSelectParamCollection As UFParameterCollectionClass         ' SELECT用パラメータコレクション
    Private m_cfInsertParamCollection As UFParameterCollectionClass         ' INSERT用パラメータコレクション

    Private m_blnIsCreateSelectSQL As Boolean                               ' SELECT用SQL作成済みフラグ
    Private m_blnIsCreateInsertSQL As Boolean                               ' INSERT用SQL作成済みフラグ

    Private m_csDataSchema As DataSet                                       ' スキーマ保管用データセット

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABMyNumberRuisekiHyojunBClass"     ' クラス名

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

#End Region

#Region "プロパティー"

#End Region

#Region "コンストラクター"

    ''' <summary>
    ''' コンストラクター
    ''' </summary>
    ''' <param name="cfControlData">コントロールデータ</param>
    ''' <param name="cfConfigDataClass">コンフィグデータ</param>
    ''' <param name="cfRdbClass">ＲＤＢクラス</param>
    ''' <remarks></remarks>
    Public Sub New( _
        ByVal cfControlData As UFControlData, _
        ByVal cfConfigDataClass As UFConfigDataClass, _
        ByVal cfRdbClass As UFRdbClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' パラメーター変数の初期化
        m_strSelectSQL = String.Empty
        m_strInsertSQL = String.Empty
        m_cfSelectParamCollection = Nothing
        m_cfInsertParamCollection = Nothing

        ' SQL作成済みフラグの初期化
        m_blnIsCreateSelectSQL = False
        m_blnIsCreateInsertSQL = False

        ' スキーマ保管用データセットの初期化
        m_csDataSchema = Nothing

    End Sub

#End Region

#Region "メソッド"

#Region "Select"

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>全件抽出</remarks>
    Public Overloads Function [Select]() As DataSet
        Return Me.Select(String.Empty)
    End Function

    '''' <summary>
    '''' Select
    '''' </summary>
    '''' <param name="strWhere">SQL文</param>
    '''' <returns>抽出結果DataSet</returns>
    '''' <remarks></remarks>
    'Private Overloads Function [Select](ByVal strWhere As String) As DataSet
    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function [Select](ByVal strWhere As String) As DataSet
        Return Me.Select(strWhere, New UFParameterCollectionClass)
    End Function

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function [Select](
        ByVal strWhere As String,
        ByVal cfParamCollection As UFParameterCollectionClass) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim strSQL As String
        Dim csMyNumberRuisekiHyojunEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateSelectSQL = False) Then

                Call CreateSelectSQL()

                m_blnIsCreateSelectSQL = True

            Else
                ' noop
            End If

            ' WHERE区の作成
            If (strWhere.Trim.RLength > 0) Then
                strSQL = String.Format(m_strSelectSQL, String.Concat(" WHERE ", strWhere))
            Else
                strSQL = String.Format(m_strSelectSQL, String.Empty)
            End If

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】")

            ' SQLの実行 DataSetの取得
            csMyNumberRuisekiHyojunEntity = m_csDataSchema.Clone()
            csMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberRuisekiHyojunEntity, ABMyNumberRuisekiHyojunEntity.TABLE_NAME, cfParamCollection, False)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        ' 抽出結果DataSetの返信
        Return csMyNumberRuisekiHyojunEntity

    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strShoriNichiji">処理日時</param>
    ''' <param name="strZengoKB">前後区分</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCd As String, _
        ByVal strMyNumber As String, _
        ByVal strShoriNichiji As String, _
        ByVal strZengoKB As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csMyNumberRuisekiHyojunEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.JUMINCD, ABMyNumberRuisekiHyojunEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCd
                m_cfSelectParamCollection.Add(cfParam)

                ' 共通番号
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.MYNUMBER, ABMyNumberRuisekiHyojunEntity.PARAM_MYNUMBER)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_MYNUMBER
                cfParam.Value = strMyNumber.RPadRight(13)
                m_cfSelectParamCollection.Add(cfParam)

                ' 処理日時
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI, ABMyNumberRuisekiHyojunEntity.PARAM_SHORINICHIJI)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_SHORINICHIJI
                cfParam.Value = strShoriNichiji
                m_cfSelectParamCollection.Add(cfParam)

                ' 前後区分
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberRuisekiHyojunEntity.ZENGOKB, ABMyNumberRuisekiHyojunEntity.PARAM_ZENGOKB)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberRuisekiHyojunEntity.PARAM_ZENGOKB
                cfParam.Value = strZengoKB
                m_cfSelectParamCollection.Add(cfParam)

            End With

            ' 抽出処理を実行
            csMyNumberRuisekiHyojunEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        ' 抽出結果DataSetの返信
        Return csMyNumberRuisekiHyojunEntity

    End Function

#End Region

#Region "CreateSelectSQL"

    ''' <summary>
    ''' CreateSelectSQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSelectSQL()

        Dim csSQL As StringBuilder

        Try

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' SELECT区の生成
            csSQL.Append(Me.CreateSelect)

            ' FROM区の生成
            csSQL.AppendFormat(" FROM {0}", ABMyNumberRuisekiHyojunEntity.TABLE_NAME)

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberRuisekiHyojunEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.JUMINCD)
            csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.MYNUMBER)
            csSQL.AppendFormat(" {0},", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI)
            csSQL.AppendFormat(" {0}", ABMyNumberRuisekiHyojunEntity.ZENGOKB)

            ' メンバー変数に設定
            m_strSelectSQL = csSQL.ToString()

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "CreateSelect"

    ''' <summary>
    ''' CreateSelect
    ''' </summary>
    ''' <returns>SELECT区</returns>
    ''' <remarks></remarks>
    Private Function CreateSelect() As String

        Dim csSQL As StringBuilder

        Try

            csSQL = New StringBuilder

            With csSQL

                .Append("SELECT ")
                .Append(ABMyNumberRuisekiHyojunEntity.JUMINCD)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SHICHOSONCD)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.MYNUMBER)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SHORINICHIJI)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.ZENGOKB)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.BANGOHOKOSHINKB)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE1)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE2)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE3)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE4)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.RESERVE5)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.TANMATSUID)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUJOFG)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.SAKUSEIUSER)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}", ABMyNumberRuisekiHyojunEntity.KOSHINUSER)

            End With

        Catch csExp As Exception
            Throw
        End Try

        Return csSQL.ToString

    End Function

#End Region

#Region "Insert"

    ''' <summary>
    ''' Insert
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function Insert( _
        ByVal csDataRow As DataRow) As Integer

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim strUpdateDatetime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateInsertSQL = False) Then

                Call CreateInsertSQL(csDataRow)

                m_blnIsCreateInsertSQL = True

            Else
                ' noop
            End If

            ' 更新日時を取得
            strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' 共通項目の編集を行う
            csDataRow(ABMyNumberRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     ' 端末ＩＤ
            csDataRow(ABMyNumberRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        ' 削除フラグ
            csDataRow(ABMyNumberRuisekiHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              ' 更新カウンター
            csDataRow(ABMyNumberRuisekiHyojunEntity.SAKUSEINICHIJI) = strUpdateDatetime             ' 作成日時
            csDataRow(ABMyNumberRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      ' 作成ユーザー
            csDataRow(ABMyNumberRuisekiHyojunEntity.KOSHINNICHIJI) = strUpdateDatetime              ' 更新日時
            csDataRow(ABMyNumberRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfInsertParamCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertParamCollection)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        ' 更新件数の返信
        Return intKoshinCount

    End Function

#End Region

#Region "CreateInsertSQL"

    ''' <summary>
    ''' CreateInsertSQL
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <remarks></remarks>
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)

        Dim csColumnList As ArrayList
        Dim csParamList As ArrayList
        Dim cfParam As UFParameterClass
        Dim strParamName As String

        Try

            csColumnList = New ArrayList
            csParamList = New ArrayList

            m_cfInsertParamCollection = New UFParameterCollectionClass

            For Each csDataColumn As DataColumn In csDataRow.Table.Columns

                strParamName = String.Concat(ABMyNumberRuisekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csColumnList.Add(csDataColumn.ColumnName)
                csParamList.Add(strParamName)

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfInsertParamCollection.Add(cfParam)

            Next csDataColumn

            m_strInsertSQL = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                                           ABMyNumberRuisekiHyojunEntity.TABLE_NAME,
                                           String.Join(","c, CType(csColumnList.ToArray(GetType(String)), String())),
                                           String.Join(","c, CType(csParamList.ToArray(GetType(String)), String())))

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#End Region

End Class
