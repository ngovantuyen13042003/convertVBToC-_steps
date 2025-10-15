'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ備考マスタビジネスクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2018/05/07　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2018/05/07   000000      【AB27002】新規作成（石合）
'* 2023/10/20   000001      【AB-0840-1】送付先管理項目追加(早崎)
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
''' ＡＢ備考マスタビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABBikoBClass

#Region "メンバー変数"

    ' メンバー変数
    Private m_cfLogClass As UFLogClass                                              ' ログ出力クラス
    Private m_cfControlData As UFControlData                                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                                ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                              ' ＲＤＢクラス

    Private m_strSelectSQL As String                                                ' SELECT用SQL
    Private m_strInsertSQL As String                                                ' INSERT用SQL
    Private m_strUpdateSQL As String                                                ' UPDATE用SQL
    Private m_strDeleteSQL As String                                                ' 物理削除用SQL
    Private m_strLogicalDeleteRecoverSQL As String                                  ' 論理削除・回復用SQL
    Private m_cfSelectParamCollection As UFParameterCollectionClass                 ' SELECT用パラメータコレクション
    Private m_cfInsertParamCollection As UFParameterCollectionClass                 ' INSERT用パラメータコレクション
    Private m_cfUpdateParamCollection As UFParameterCollectionClass                 ' UPDATE用パラメータコレクション
    Private m_cfDeleteParamCollection As UFParameterCollectionClass                 ' 物理削除用パラメータコレクション
    Private m_cfLogicalDeleteRecoverParamCollection As UFParameterCollectionClass   ' 論理削除・回復用パラメータコレクション

    Private m_blnIsCreateSelectSQL As Boolean                                       ' SELECT用SQL作成済みフラグ
    Private m_blnIsCreateInsertSQL As Boolean                                       ' INSERT用SQL作成済みフラグ
    Private m_blnIsCreateUpdateSQL As Boolean                                       ' UPDATE用SQL作成済みフラグ
    Private m_blnIsCreateDeleteSQL As Boolean                                       ' 物理削除用SQL作成済みフラグ
    Private m_blnIsCreateLogicalDeleteRecoverSQL As Boolean                         ' 論理削除・回復用SQL作成済みフラグ

    Private m_csDataSchema As DataSet                                               ' スキーマ保管用データセット

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABBikoBClass"                        ' クラス名

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0} = '0'", ABBikoEntity.SAKUJOFG)

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
    Public Sub New(
        ByVal cfControlData As UFControlData,
        ByVal cfConfigDataClass As UFConfigDataClass,
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
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strLogicalDeleteRecoverSQL = String.Empty
        m_cfSelectParamCollection = Nothing
        m_cfInsertParamCollection = Nothing
        m_cfUpdateParamCollection = Nothing
        m_cfDeleteParamCollection = Nothing
        m_cfLogicalDeleteRecoverParamCollection = Nothing

        ' SQL作成済みフラグの初期化
        m_blnIsCreateSelectSQL = False
        m_blnIsCreateInsertSQL = False
        m_blnIsCreateUpdateSQL = False
        m_blnIsCreateDeleteSQL = False
        m_blnIsCreateLogicalDeleteRecoverSQL = False

        ' スキーマ保管用データセットの初期化
        m_csDataSchema = Nothing

    End Sub

#End Region

#Region "メソッド"

#Region "GetTableSchema"

    ''' <summary>
    ''' GetTableSchema
    ''' </summary>
    ''' <returns>テーブルスキーマ</returns>
    ''' <remarks></remarks>
    Public Function GetTableSchema() As DataSet

        Dim csSQL As StringBuilder
        Dim csBikoEntity As DataSet

        Try

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' SELECT区の生成
            csSQL.Append(Me.CreateSelect)

            ' FROM区の生成
            csSQL.AppendFormat(" FROM {0}", ABBikoEntity.TABLE_NAME)

            ' スキーマの取得
            csBikoEntity = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABBikoEntity.TABLE_NAME, False)

        Catch csExp As Exception
            Throw
        End Try

        Return csBikoEntity

    End Function

#End Region

#Region "Select"

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>全件抽出</remarks>
    Public Overloads Function [Select]() As DataSet
        Return Me.Select(False)
    End Function

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>全件抽出</remarks>
    Public Overloads Function [Select](ByVal blnSakujoFG As Boolean) As DataSet
        If (blnSakujoFG = True) Then
            Return Me.Select(String.Empty)
        Else
            Return Me.Select(SQL_SAKUJOFG)
        End If
    End Function

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Private Overloads Function [Select](ByVal strWhere As String) As DataSet
        Return Me.Select(strWhere, New UFParameterCollectionClass)
    End Function

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Private Overloads Function [Select](
        ByVal strWhere As String,
        ByVal cfParamCollection As UFParameterCollectionClass) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim strSQL As String
        Dim csBikoEntity As DataSet

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
            csBikoEntity = m_csDataSchema.Clone()
            csBikoEntity = m_cfRdbClass.GetDataSet(strSQL, csBikoEntity, ABBikoEntity.TABLE_NAME, cfParamCollection, False)

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
        Return csBikoEntity

    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strBikoKbn">備考区分</param>
    ''' <param name="strDataKey1">データキー１</param>
    ''' <param name="strDataKey2">データキー２</param>
    ''' <param name="strDataKey3">データキー３</param>
    ''' <param name="strDataKey4">データキー４</param>
    ''' <param name="strDataKey5">データキー５</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey(
        ByVal strBikoKbn As String,
        ByVal strDataKey1 As String,
        ByVal strDataKey2 As String,
        ByVal strDataKey3 As String,
        ByVal strDataKey4 As String,
        ByVal strDataKey5 As String) As DataSet
        Return Me.SelectByKey(strBikoKbn, strDataKey1, strDataKey2, strDataKey3, strDataKey4, strDataKey5, False)
    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strBikoKbn">備考区分</param>
    ''' <param name="strDataKey1">データキー１</param>
    ''' <param name="strDataKey2">データキー２</param>
    ''' <param name="strDataKey3">データキー３</param>
    ''' <param name="strDataKey4">データキー４</param>
    ''' <param name="strDataKey5">データキー５</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey(
        ByVal strBikoKbn As String,
        ByVal strDataKey1 As String,
        ByVal strDataKey2 As String,
        ByVal strDataKey3 As String,
        ByVal strDataKey4 As String,
        ByVal strDataKey5 As String,
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csBikoEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 備考区分
                .AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.PARAM_BIKOKBN)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_BIKOKBN
                cfParam.Value = strBikoKbn
                m_cfSelectParamCollection.Add(cfParam)

                ' データキー１
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.PARAM_DATAKEY1)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY1
                cfParam.Value = strDataKey1
                m_cfSelectParamCollection.Add(cfParam)

                ' データキー２
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.PARAM_DATAKEY2)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY2
                cfParam.Value = strDataKey2
                m_cfSelectParamCollection.Add(cfParam)

                ' データキー３
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.PARAM_DATAKEY3)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY3
                cfParam.Value = strDataKey3
                m_cfSelectParamCollection.Add(cfParam)

                ' データキー４
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.PARAM_DATAKEY4)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY4
                cfParam.Value = strDataKey4
                m_cfSelectParamCollection.Add(cfParam)

                ' データキー５
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.PARAM_DATAKEY5)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABBikoEntity.PARAM_DATAKEY5
                cfParam.Value = strDataKey5
                m_cfSelectParamCollection.Add(cfParam)

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csBikoEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csBikoEntity

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
            csSQL.AppendFormat(" FROM {0}", ABBikoEntity.TABLE_NAME)

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABBikoEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0},", ABBikoEntity.BIKOKBN)
            csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY1)
            csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY2)
            csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY3)
            csSQL.AppendFormat(" {0},", ABBikoEntity.DATAKEY4)
            csSQL.AppendFormat(" {0} ", ABBikoEntity.DATAKEY5)

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
                .Append(ABBikoEntity.SHICHOSONCD)
                .AppendFormat(", {0}", ABBikoEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}", ABBikoEntity.BIKOKBN)
                .AppendFormat(", {0}", ABBikoEntity.DATAKEY1)
                .AppendFormat(", {0}", ABBikoEntity.DATAKEY2)
                .AppendFormat(", {0}", ABBikoEntity.DATAKEY3)
                .AppendFormat(", {0}", ABBikoEntity.DATAKEY4)
                .AppendFormat(", {0}", ABBikoEntity.DATAKEY5)
                .AppendFormat(", {0}", ABBikoEntity.BIKO)
                .AppendFormat(", {0}", ABBikoEntity.RESERVE)
                .AppendFormat(", {0}", ABBikoEntity.TANMATSUID)
                .AppendFormat(", {0}", ABBikoEntity.SAKUJOFG)
                .AppendFormat(", {0}", ABBikoEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}", ABBikoEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}", ABBikoEntity.SAKUSEIUSER)
                .AppendFormat(", {0}", ABBikoEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}", ABBikoEntity.KOSHINUSER)

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
    Public Overloads Function Insert(
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
            csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId          ' 端末ＩＤ
            '*履歴番号 000001 2023/10/20 修正開始
            'csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF                         
            If (csDataRow(ABBikoEntity.SAKUJOFG).ToString = String.Empty) Then          ' 削除フラグ
                csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF
            End If
            '*履歴番号 000001 2023/10/20 修正終了
            csDataRow(ABBikoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF                   ' 更新カウンター
            csDataRow(ABBikoEntity.SAKUSEINICHIJI) = strUpdateDatetime                  ' 作成日時
            csDataRow(ABBikoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId           ' 作成ユーザー
            csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime                   ' 更新日時
            csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId            ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfInsertParamCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertParamCollection)

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

                strParamName = String.Concat(ABBikoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csColumnList.Add(csDataColumn.ColumnName)
                csParamList.Add(strParamName)

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfInsertParamCollection.Add(cfParam)

            Next csDataColumn

            m_strInsertSQL = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                                           ABBikoEntity.TABLE_NAME,
                                           String.Join(","c, CType(csColumnList.ToArray(GetType(String)), String())),
                                           String.Join(","c, CType(csParamList.ToArray(GetType(String)), String())))

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "Update"

    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function Update(
        ByVal csDataRow As DataRow) As Integer

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim strUpdateDatetime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateUpdateSQL = False) Then

                Call CreateUpdateSQL(csDataRow)

                m_blnIsCreateUpdateSQL = True

            Else
                ' noop
            End If

            ' 更新日時を取得
            strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' 共通項目の編集を行う
            csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                          ' 端末ＩＤ
            csDataRow(ABBikoEntity.KOSHINCOUNTER) = CType(csDataRow(ABBikoEntity.KOSHINCOUNTER), Decimal) + 1           ' 更新カウンタ
            csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime                                                   ' 更新日時
            csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                            ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfUpdateParamCollection

                If (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateParamCollection)

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

        ' 更新件数の返信
        Return intKoshinCount

    End Function

#End Region

#Region "CreateUpdateSQL"

    ''' <summary>
    ''' CreateUpdateSQL
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <remarks></remarks>
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)

        Dim csParamList As ArrayList
        Dim cfParam As UFParameterClass
        Dim strParamName As String
        Dim csWhere As StringBuilder

        Try

            csParamList = New ArrayList

            m_cfUpdateParamCollection = New UFParameterCollectionClass

            For Each csDataColumn As DataColumn In csDataRow.Table.Columns

                strParamName = String.Concat(ABBikoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csParamList.Add(String.Format("{0} = {1}", csDataColumn.ColumnName, strParamName))

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfUpdateParamCollection.Add(cfParam)

            Next csDataColumn

            m_strUpdateSQL = String.Format("UPDATE {0} SET {1} ",
                                           ABBikoEntity.TABLE_NAME,
                                           String.Join(","c, CType(csParamList.ToArray(GetType(String)), String())))

            csWhere = New StringBuilder(256)
            With csWhere
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER)
            End With
            m_strUpdateSQL += csWhere.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateParamCollection.Add(cfParam)

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "Delete"

    ''' <summary>
    ''' Delete
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function Delete(
        ByVal csDataRow As DataRow) As Integer

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateDeleteSQL = False) Then

                Call CreateDeleteSQL(csDataRow)

                m_blnIsCreateDeleteSQL = True

            Else
                ' noop
            End If

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfDeleteParamCollection
                ' キー項目は更新前の値で設定
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteParamCollection)

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

        ' 更新件数の返信
        Return intKoshinCount

    End Function

#End Region

#Region "CreateDeleteSQL"

    ''' <summary>
    ''' CreateDeleteSQL
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <remarks></remarks>
    Private Sub CreateDeleteSQL(ByVal csDataRow As DataRow)

        Dim cfParam As UFParameterClass
        Dim csSQL As StringBuilder

        Try

            m_cfDeleteParamCollection = New UFParameterCollectionClass

            csSQL = New StringBuilder(256)
            With csSQL
                .AppendFormat("DELETE FROM {0} ", ABBikoEntity.TABLE_NAME)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER)
            End With
            m_strDeleteSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER
            m_cfDeleteParamCollection.Add(cfParam)

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "LogicalDelete"

    ''' <summary>
    ''' LogicalDelete
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function LogicalDelete(
        ByVal csDataRow As DataRow) As Integer

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim strUpdateDatetime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateLogicalDeleteRecoverSQL = False) Then

                Call CreateLogicalDeleteRecoverSQL(csDataRow)

                m_blnIsCreateLogicalDeleteRecoverSQL = True

            Else
                ' noop
            End If

            ' 更新日時を取得
            strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' 共通項目の編集を行う
            csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                          ' 端末ＩＤ
            csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_ON                                                              ' 削除フラグ
            '*履歴番号 000001 2023/10/20 修正開始
            'csDataRow(ABBikoEntity.KOSHINCOUNTER) = CType(csDataRow(ABBikoEntity.KOSHINCOUNTER), Decimal) + 1           ' 更新カウンタ
            csDataRow(ABBikoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF                                       　          ' 更新カウンタ 
            '*履歴番号 000001 2023/10/20 修正終了
            csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime                                                   ' 更新日時
            csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                            ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfLogicalDeleteRecoverParamCollection

                If (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection)

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

        ' 更新件数の返信
        Return intKoshinCount

    End Function

#End Region

#Region "Recover"

    ''' <summary>
    ''' Recover
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function Recover(
        ByVal csDataRow As DataRow) As Integer

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim strUpdateDatetime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateLogicalDeleteRecoverSQL = False) Then

                Call CreateLogicalDeleteRecoverSQL(csDataRow)

                m_blnIsCreateLogicalDeleteRecoverSQL = True

            Else
                ' noop
            End If

            ' 更新日時を取得
            strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' 共通項目の編集を行う
            csDataRow(ABBikoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                          ' 端末ＩＤ
            csDataRow(ABBikoEntity.SAKUJOFG) = SAKUJOFG_OFF                                                             ' 削除フラグ
            csDataRow(ABBikoEntity.KOSHINCOUNTER) = CType(csDataRow(ABBikoEntity.KOSHINCOUNTER), Decimal) + 1           ' 更新カウンタ
            csDataRow(ABBikoEntity.KOSHINNICHIJI) = strUpdateDatetime                                                   ' 更新日時
            csDataRow(ABBikoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                            ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfLogicalDeleteRecoverParamCollection

                If (cfParam.ParameterName.StartsWith(ABBikoEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABBikoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection)

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

        ' 更新件数の返信
        Return intKoshinCount

    End Function

#End Region

#Region "CreateLogicalDeleteRecoverSQL"

    ''' <summary>
    ''' CreateLogicalDeleteRecoverSQL
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <remarks></remarks>
    Private Sub CreateLogicalDeleteRecoverSQL(ByVal csDataRow As DataRow)

        Dim cfParam As UFParameterClass
        Dim csSQL As StringBuilder

        Try

            m_cfLogicalDeleteRecoverParamCollection = New UFParameterCollectionClass

            csSQL = New StringBuilder(256)
            With csSQL
                .AppendFormat("UPDATE {0} ", ABBikoEntity.TABLE_NAME)
                .Append("SET ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.TANMATSUID, ABBikoEntity.PARAM_TANMATSUID)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.SAKUJOFG, ABBikoEntity.PARAM_SAKUJOFG)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.PARAM_KOSHINCOUNTER)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINNICHIJI, ABBikoEntity.PARAM_KOSHINNICHIJI)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.KOSHINUSER, ABBikoEntity.PARAM_KOSHINUSER)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.BIKOKBN, ABBikoEntity.KEY_BIKOKBN)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY1, ABBikoEntity.KEY_DATAKEY1)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY2, ABBikoEntity.KEY_DATAKEY2)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY3, ABBikoEntity.KEY_DATAKEY3)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY4, ABBikoEntity.KEY_DATAKEY4)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABBikoEntity.DATAKEY5, ABBikoEntity.KEY_DATAKEY5)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABBikoEntity.KOSHINCOUNTER, ABBikoEntity.KEY_KOSHINCOUNTER)
            End With
            m_strLogicalDeleteRecoverSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.PARAM_TANMATSUID
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.PARAM_SAKUJOFG
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINCOUNTER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINNICHIJI
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.PARAM_KOSHINUSER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_BIKOKBN
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY1
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY2
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY3
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY4
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_DATAKEY5
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABBikoEntity.KEY_KOSHINCOUNTER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#End Region

End Class
