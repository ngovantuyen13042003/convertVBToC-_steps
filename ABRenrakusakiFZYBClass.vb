'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ連絡先付随マスタビジネスクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2018/05/22　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2018/05/22   000000      【AB24011】新規作成（石合）
'* 2024/01/11   000001      【AB-0860-1】連絡先管理項目追加
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
''' ＡＢ連絡先付随マスタビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABRenrakusakiFZYBClass

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
    Private Const THIS_CLASS_NAME As String = "ABRenrakusakiFZYBClass"              ' クラス名

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0} = '0'", ABRenrakusakiFZYEntity.SAKUJOFG)

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

        Dim csRenrakusakiFZYEntity As DataSet

        Try

            ' スキーマの取得
            csRenrakusakiFZYEntity = m_cfRdbClass.GetTableSchemaNoRestriction(String.Format("SELECT * FROM {0}", ABRenrakusakiFZYEntity.TABLE_NAME), ABRenrakusakiFZYEntity.TABLE_NAME, False)

        Catch csExp As Exception
            Throw
        End Try

        Return csRenrakusakiFZYEntity

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
    Private Overloads Function [Select]( _
        ByVal strWhere As String, _
        ByVal cfParamCollection As UFParameterCollectionClass) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim strSQL As String
        Dim csRenrakusakiFZYEntity As DataSet

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
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiFZYEntity = m_csDataSchema.Clone()
            csRenrakusakiFZYEntity = m_cfRdbClass.GetDataSet(strSQL, csRenrakusakiFZYEntity, ABRenrakusakiFZYEntity.TABLE_NAME, cfParamCollection, False)

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
        Return csRenrakusakiFZYEntity

    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strGyomuCD">業務コード</param>
    ''' <param name="strGyomuNaiShuCD">業務内種別コード</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCD As String, _
        ByVal strGyomuCD As String, _
        ByVal strGyomuNaiShuCD As String) As DataSet
        Return Me.SelectByKey(strJuminCD, strGyomuCD, strGyomuNaiShuCD, False)
    End Function

    '*履歴番号 000001 2024/01/11 追加開始
    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strGyomuCD">業務コード</param>
    ''' <param name="strGyomuNaiShuCD">業務内種別コード</param>
    ''' <param name="strTorokuRenban">登録連番</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCD As String, _
        ByVal strGyomuCD As String, _
        ByVal strGyomuNaiShuCD As String, _
        ByVal strTorokuRenban As String) As DataSet
        Return Me.SelectByKey(strJuminCD, strGyomuCD, strGyomuNaiShuCD, strTorokuRenban, False)
    End Function
    '*履歴番号 000001 2024/01/11 追加終了

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strGyomuCD">業務コード</param>
    ''' <param name="strGyomuNaiShuCD">業務内種別コード</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCD As String, _
        ByVal strGyomuCD As String, _
        ByVal strGyomuNaiShuCD As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csRenrakusakiFZYEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.JUMINCD, ABRenrakusakiFZYEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 業務コード
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUCD, ABRenrakusakiFZYEntity.PARAM_GYOMUCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_GYOMUCD
                cfParam.Value = strGyomuCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 業務内種別コード
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.PARAM_GYOMUNAISHU_CD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_GYOMUNAISHU_CD
                cfParam.Value = strGyomuNaiShuCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csRenrakusakiFZYEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csRenrakusakiFZYEntity

    End Function

    '*履歴番号 000001 2024/01/11 追加開始
    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strGyomuCD">業務コード</param>
    ''' <param name="strGyomuNaiShuCD">業務内種別コード</param>
    ''' <param name="strTorokuRenban">登録連番</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCD As String, _
        ByVal strGyomuCD As String, _
        ByVal strGyomuNaiShuCD As String, _
        ByVal strTorokuRenban As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csRenrakusakiFZYEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.JUMINCD, ABRenrakusakiFZYEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 業務コード
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUCD, ABRenrakusakiFZYEntity.PARAM_GYOMUCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_GYOMUCD
                cfParam.Value = strGyomuCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 業務内種別コード
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.PARAM_GYOMUNAISHU_CD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_GYOMUNAISHU_CD
                cfParam.Value = strGyomuNaiShuCD
                m_cfSelectParamCollection.Add(cfParam)

                ' 登録連番
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.PARAM_TOROKURENBAN)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_TOROKURENBAN
                cfParam.Value = strTorokuRenban
                m_cfSelectParamCollection.Add(cfParam)

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csRenrakusakiFZYEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csRenrakusakiFZYEntity

    End Function
    '*履歴番号 000001 2024/01/11 追加終了

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
            csSQL.AppendFormat(" FROM {0}", ABRenrakusakiFZYEntity.TABLE_NAME)

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABRenrakusakiFZYEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0},", ABRenrakusakiFZYEntity.JUMINCD)
            csSQL.AppendFormat(" {0},", ABRenrakusakiFZYEntity.GYOMUCD)
            csSQL.AppendFormat(" {0},", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
            csSQL.AppendFormat(" {0} ", ABRenrakusakiFZYEntity.TOROKURENBAN)

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
                .Append(ABRenrakusakiFZYEntity.JUMINCD)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.SHICHOSONCD)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.GYOMUCD)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
                '*履歴番号 000001 2024/01/11 追加開始
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.TOROKURENBAN)
                '*履歴番号 000001 2024/01/11 追加終了
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI4)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI5)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI6)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.RESERVE)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.TANMATSUID)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.SAKUJOFG)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.SAKUSEIUSER)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}", ABRenrakusakiFZYEntity.KOSHINUSER)

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
            csDataRow(ABRenrakusakiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                                            ' 端末ＩＤ
            csDataRow(ABRenrakusakiFZYEntity.SAKUJOFG) = GetValue(csDataRow(ABRenrakusakiFZYEntity.SAKUJOFG), SAKUJOFG_OFF)                         ' 削除フラグ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF                                                                     ' 更新カウンター
            csDataRow(ABRenrakusakiFZYEntity.SAKUSEINICHIJI) = GetValue(csDataRow(ABRenrakusakiFZYEntity.SAKUSEINICHIJI), strUpdateDatetime)        ' 作成日時
            csDataRow(ABRenrakusakiFZYEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                                                             ' 作成ユーザー
            csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = GetValue(csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI), strUpdateDatetime)          ' 更新日時
            csDataRow(ABRenrakusakiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                              ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfInsertParamCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

                strParamName = String.Concat(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csColumnList.Add(csDataColumn.ColumnName)
                csParamList.Add(strParamName)

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfInsertParamCollection.Add(cfParam)

            Next csDataColumn

            m_strInsertSQL = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", _
                                           ABRenrakusakiFZYEntity.TABLE_NAME, _
                                           String.Join(","c, CType(csColumnList.ToArray(GetType(String)), String())), _
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
    Public Overloads Function Update( _
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
            csDataRow(ABRenrakusakiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                                            ' 端末ＩＤ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = CType(csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER), Decimal) + 1                   ' 更新カウンタ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = GetValue(csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI), strUpdateDatetime)          ' 更新日時
            csDataRow(ABRenrakusakiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                              ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfUpdateParamCollection

                If (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateParamCollection)

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

                strParamName = String.Concat(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csParamList.Add(String.Format("{0} = {1}", csDataColumn.ColumnName, strParamName))

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfUpdateParamCollection.Add(cfParam)

            Next csDataColumn

            m_strUpdateSQL = String.Format("UPDATE {0} SET {1} ", _
                                           ABRenrakusakiFZYEntity.TABLE_NAME, _
                                           String.Join(","c, CType(csParamList.ToArray(GetType(String)), String())))

            csWhere = New StringBuilder(256)
            With csWhere
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.JUMINCD, ABRenrakusakiFZYEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUCD, ABRenrakusakiFZYEntity.KEY_GYOMUCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD)
                '*履歴番号 000001 2024/01/11 追加開始
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.KEY_TOROKURENBAN)
                '*履歴番号 000001 2024/01/11 追加終了
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABRenrakusakiFZYEntity.KOSHINCOUNTER, ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER)
            End With
            m_strUpdateSQL += csWhere.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_JUMINCD
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUCD
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateParamCollection.Add(cfParam)

            '*履歴番号 000001 2024/01/11 追加開始
            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_TOROKURENBAN
            m_cfUpdateParamCollection.Add(cfParam)
            '*履歴番号 000001 2024/01/11 追加終了

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER
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
    Public Overloads Function Delete( _
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
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteParamCollection)

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
                .AppendFormat("DELETE FROM {0} ", ABRenrakusakiFZYEntity.TABLE_NAME)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.JUMINCD, ABRenrakusakiFZYEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUCD, ABRenrakusakiFZYEntity.KEY_GYOMUCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD)
                '*履歴番号 000001 2024/01/11 追加開始
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.KEY_TOROKURENBAN)
                '*履歴番号 000001 2024/01/11 追加終了
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABRenrakusakiFZYEntity.KOSHINCOUNTER, ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER)
            End With
            m_strDeleteSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_JUMINCD
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUCD
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD
            m_cfDeleteParamCollection.Add(cfParam)

            '*履歴番号 000001 2024/01/11 追加開始
            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_TOROKURENBAN
            m_cfDeleteParamCollection.Add(cfParam)
            '*履歴番号 000001 2024/01/11 追加終了

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER
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
    Public Overloads Function LogicalDelete( _
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
            csDataRow(ABRenrakusakiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                                            ' 端末ＩＤ
            csDataRow(ABRenrakusakiFZYEntity.SAKUJOFG) = SAKUJOFG_ON                                                                                ' 削除フラグ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = CType(csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER), Decimal) + 1                   ' 更新カウンタ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = GetValue(csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI), strUpdateDatetime)          ' 更新日時
            csDataRow(ABRenrakusakiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                              ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfLogicalDeleteRecoverParamCollection

                If (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection)

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

#Region "Recover"

    ''' <summary>
    ''' Recover
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function Recover( _
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
            csDataRow(ABRenrakusakiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                                            ' 端末ＩＤ
            csDataRow(ABRenrakusakiFZYEntity.SAKUJOFG) = SAKUJOFG_OFF                                                                               ' 削除フラグ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = CType(csDataRow(ABRenrakusakiFZYEntity.KOSHINCOUNTER), Decimal) + 1                   ' 更新カウンタ
            csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = GetValue(csDataRow(ABRenrakusakiFZYEntity.KOSHINNICHIJI), strUpdateDatetime)          ' 更新日時
            csDataRow(ABRenrakusakiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                              ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfLogicalDeleteRecoverParamCollection

                If (cfParam.ParameterName.StartsWith(ABRenrakusakiFZYEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteRecoverSQL, m_cfLogicalDeleteRecoverParamCollection)

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
                .AppendFormat("UPDATE {0} ", ABRenrakusakiFZYEntity.TABLE_NAME)
                .Append("SET ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.TANMATSUID, ABRenrakusakiFZYEntity.PARAM_TANMATSUID)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.SAKUJOFG, ABRenrakusakiFZYEntity.PARAM_SAKUJOFG)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.KOSHINCOUNTER, ABRenrakusakiFZYEntity.PARAM_KOSHINCOUNTER)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.KOSHINNICHIJI, ABRenrakusakiFZYEntity.PARAM_KOSHINNICHIJI)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.KOSHINUSER, ABRenrakusakiFZYEntity.PARAM_KOSHINUSER)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.JUMINCD, ABRenrakusakiFZYEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUCD, ABRenrakusakiFZYEntity.KEY_GYOMUCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD)
                '*履歴番号 000001 2024/01/11 追加開始
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABRenrakusakiFZYEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.KEY_TOROKURENBAN)
                '*履歴番号 000001 2024/01/11 追加終了
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABRenrakusakiFZYEntity.KOSHINCOUNTER, ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER)
            End With
            m_strLogicalDeleteRecoverSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_TANMATSUID
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_SAKUJOFG
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_KOSHINCOUNTER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_KOSHINNICHIJI
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.PARAM_KOSHINUSER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_JUMINCD
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUCD
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_GYOMUNAISHU_CD
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

            '*履歴番号 000001 2024/01/11 追加開始
            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_TOROKURENBAN
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)
            '*履歴番号 000001 2024/01/11 追加終了

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABRenrakusakiFZYEntity.KEY_KOSHINCOUNTER
            m_cfLogicalDeleteRecoverParamCollection.Add(cfParam)

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "GetValue"

    ''' <summary>
    ''' GetValue
    ''' </summary>
    ''' <param name="objValue">対象オブジェクト</param>
    ''' <param name="strValue">代替値</param>
    ''' <returns>編集後オブジェクト</returns>
    ''' <remarks></remarks>
    Private Function GetValue( _
        ByVal objValue As Object, _
        ByVal strValue As String) As Object

        Dim objResult As Object

        Try

            If (IsDBNull(objValue) _
                OrElse objValue Is Nothing _
                OrElse objValue.ToString.Trim.RLength = 0) Then
                objResult = strValue
            Else
                objResult = objValue
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return objResult

    End Function

#End Region

#End Region

End Class
