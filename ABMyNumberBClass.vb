'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ共通番号マスタＤＡ(ABMyNumberBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2014/04/30　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2015/09/24   000001      速度改善のため、削除フラグの指定方法を変更（石合）※規定値以外は設定されないことが大前提
'* 2016/01/27   000002      公表の同意取得用メソッド追加（岩下）
'* 2023/10/25   000003     【AB-1000-1】個人制御同一個人番号者対応(下村)
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
''' ＡＢ共通番号マスタＤＡ
''' </summary>
''' <remarks></remarks>
Public Class ABMyNumberBClass

#Region "メンバー変数"

    ' メンバー変数
    Private m_cfLogClass As UFLogClass                                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                      ' ＲＤＢクラス

    Private m_strSelectSQL As String                                        ' SELECT用SQL
    Private m_strInsertSQL As String                                        ' INSERT用SQL
    Private m_strUpdateSQL As String                                        ' UPDATE用SQL
    Private m_strDeleteSQL As String                                        ' 物理削除用SQL
    Private m_strLogicalDeleteSQL As String                                 ' 論理削除用SQL
    '*履歴番号 000002 2016/01/27 追加開始
    Private m_strSelectConsentSQL As String                                 ' SELECTCONSENT用SQL
    '*履歴番号 000002 2016/01/27 追加終了
    Private m_cfSelectParamCollection As UFParameterCollectionClass         ' SELECT用パラメータコレクション
    Private m_cfInsertParamCollection As UFParameterCollectionClass         ' INSERT用パラメータコレクション
    Private m_cfUpdateParamCollection As UFParameterCollectionClass         ' UPDATE用パラメータコレクション
    Private m_cfDeleteParamCollection As UFParameterCollectionClass         ' 物理削除用パラメータコレクション
    Private m_cfLogicalDeleteParamCollection As UFParameterCollectionClass  ' 論理削除用パラメータコレクション

    Private m_blnIsCreateSelectSQL As Boolean                               ' SELECT用SQL作成済みフラグ
    Private m_blnIsCreateInsertSQL As Boolean                               ' INSERT用SQL作成済みフラグ
    Private m_blnIsCreateUpdateSQL As Boolean                               ' UPDATE用SQL作成済みフラグ
    Private m_blnIsCreateDeleteSQL As Boolean                               ' 物理削除用SQL作成済みフラグ
    Private m_blnIsCreateLogicalDeleteSQL As Boolean                        ' 論理削除用SQL作成済みフラグ
    '*履歴番号 000002 2016/01/27 追加開始
    Private m_blnIsConsentSelectSQL As Boolean                              ' 公表の同意SQL作成済みフラグ
    '*履歴番号 000002 2016/01/27 追加終了

    Private m_csDataSchema As DataSet                                       ' スキーマ保管用データセット

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABMyNumberBClass"            ' クラス名

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    '*履歴番号 000001 2015/09/24 修正開始
    'Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0} <> '1'", ABMyNumberEntity.SAKUJOFG)
    Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0} = '0'", ABMyNumberEntity.SAKUJOFG)
    '*履歴番号 000001 2015/09/24 修正終了

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
        m_strLogicalDeleteSQL = String.Empty
        '*履歴番号 000002 2016/01/27 追加開始
        m_strSelectConsentSQL = String.Empty
        '*履歴番号 000002 2016/01/27 追加開始
        m_cfSelectParamCollection = Nothing
        m_cfInsertParamCollection = Nothing
        m_cfUpdateParamCollection = Nothing
        m_cfDeleteParamCollection = Nothing
        m_cfLogicalDeleteParamCollection = Nothing

        ' SQL作成済みフラグの初期化
        m_blnIsCreateSelectSQL = False
        m_blnIsCreateInsertSQL = False
        m_blnIsCreateUpdateSQL = False
        m_blnIsCreateDeleteSQL = False
        m_blnIsCreateLogicalDeleteSQL = False
        '*履歴番号 000002 2016/01/27 追加開始
        m_blnIsConsentSelectSQL = False
        '*履歴番号 000002 2016/01/27 追加開始

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
        Dim csMyNumberEntity As DataSet

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
            csMyNumberEntity = m_csDataSchema.Clone()
            csMyNumberEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberEntity, ABMyNumberEntity.TABLE_NAME, cfParamCollection, False)

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
        Return csMyNumberEntity

    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCd As String, _
        ByVal strMyNumber As String) As DataSet
        Return Me.SelectByKey(strJuminCd, strMyNumber, False)
    End Function

    ''' <summary>
    ''' SelectByKey
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Public Overloads Function SelectByKey( _
        ByVal strJuminCd As String, _
        ByVal strMyNumber As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csMyNumberEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCd
                m_cfSelectParamCollection.Add(cfParam)

                ' 共通番号
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfParam.Value = strMyNumber.RPadRight(13)
                m_cfSelectParamCollection.Add(cfParam)

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csMyNumberEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csMyNumberEntity

    End Function

    ''' <summary>
    ''' SelectByJuminCd
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ</remarks>
    Public Overloads Function SelectByJuminCd( _
        ByVal strJuminCd As String) As DataSet
        Return Me.SelectByJuminCd(strJuminCd, False)
    End Function

    ''' <summary>
    ''' SelectByJuminCd
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ</remarks>
    Public Overloads Function SelectByJuminCd( _
        ByVal strJuminCd As String, _
        ByVal blnSakujoFG As Boolean) As DataSet
        Return Me.SelectByJuminCd(strJuminCd, ABMyNumberEntity.DEFAULT.CKINKB.CKIN, blnSakujoFG)
    End Function

    ''' <summary>
    ''' SelectByJuminCd
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ、履歴を含む指定が可能</remarks>
    Public Overloads Function SelectByJuminCd( _
        ByVal strJuminCd As String, _
        ByVal strCkinKB As String) As DataSet
        Return Me.SelectByJuminCd(strJuminCd, strCkinKB, False)
    End Function

    ''' <summary>
    ''' SelectByJuminCd
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ、履歴を含む指定が可能</remarks>
    Public Overloads Function SelectByJuminCd( _
        ByVal strJuminCd As String, _
        ByVal strCkinKB As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csMyNumberEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCd
                m_cfSelectParamCollection.Add(cfParam)

                ' 直近区分
                If (strCkinKB IsNot Nothing AndAlso strCkinKB = ABMyNumberEntity.DEFAULT.CKINKB.CKIN) Then

                    .Append("AND ")
                    .AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB)

                    cfParam = New UFParameterClass
                    cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB
                    cfParam.Value = strCkinKB
                    m_cfSelectParamCollection.Add(cfParam)

                Else
                    ' noop
                End If

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csMyNumberEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csMyNumberEntity

    End Function

    ''' <summary>
    ''' SelectByMyNumber
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ</remarks>
    Public Overloads Function SelectByMyNumber( _
        ByVal strMyNumber As String) As DataSet
        Return Me.SelectByMyNumber(strMyNumber, False)
    End Function

    ''' <summary>
    ''' SelectByMyNumber
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ</remarks>
    Public Overloads Function SelectByMyNumber( _
        ByVal strMyNumber As String, _
        ByVal blnSakujoFG As Boolean) As DataSet
        Return Me.SelectByMyNumber(strMyNumber, ABMyNumberEntity.DEFAULT.CKINKB.CKIN, blnSakujoFG)
    End Function

    ''' <summary>
    ''' SelectByMyNumber
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ、履歴を含む指定が可能</remarks>
    Public Overloads Function SelectByMyNumber( _
        ByVal strMyNumber As String, _
        ByVal strCkinKB As String) As DataSet
        Return Me.SelectByMyNumber(strMyNumber, strCkinKB, False)
    End Function

    ''' <summary>
    ''' SelectByMyNumber
    ''' </summary>
    ''' <param name="strMyNumber">共通番号</param>
    ''' <param name="strCkinKB">直近区分（"1"：直近のみ、以外：履歴を含む）</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ、履歴を含む指定が可能</remarks>
    Public Overloads Function SelectByMyNumber( _
        ByVal strMyNumber As String, _
        ByVal strCkinKB As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csMyNumberEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 共通番号
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfParam.Value = strMyNumber.RPadRight(13)
                m_cfSelectParamCollection.Add(cfParam)

                ' 直近区分
                If (strCkinKB IsNot Nothing AndAlso strCkinKB = ABMyNumberEntity.DEFAULT.CKINKB.CKIN) Then

                    .Append("AND ")
                    .AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB)

                    cfParam = New UFParameterClass
                    cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB
                    cfParam.Value = strCkinKB
                    m_cfSelectParamCollection.Add(cfParam)

                Else
                    ' noop
                End If

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csMyNumberEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csMyNumberEntity

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
            csSQL.AppendFormat(" FROM {0}", ABMyNumberEntity.TABLE_NAME)

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0},", ABMyNumberEntity.JUMINCD)
            csSQL.AppendFormat(" {0}", ABMyNumberEntity.MYNUMBER)

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
                .Append(ABMyNumberEntity.JUMINCD)
                .AppendFormat(", {0}", ABMyNumberEntity.SHICHOSONCD)
                .AppendFormat(", {0}", ABMyNumberEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}", ABMyNumberEntity.MYNUMBER)
                .AppendFormat(", {0}", ABMyNumberEntity.CKINKB)
                .AppendFormat(", {0}", ABMyNumberEntity.IDOKB)
                .AppendFormat(", {0}", ABMyNumberEntity.IDOYMD)
                .AppendFormat(", {0}", ABMyNumberEntity.IDOSHA)
                .AppendFormat(", {0}", ABMyNumberEntity.RESERVE)
                .AppendFormat(", {0}", ABMyNumberEntity.TANMATSUID)
                .AppendFormat(", {0}", ABMyNumberEntity.SAKUJOFG)
                .AppendFormat(", {0}", ABMyNumberEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}", ABMyNumberEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}", ABMyNumberEntity.SAKUSEIUSER)
                .AppendFormat(", {0}", ABMyNumberEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}", ABMyNumberEntity.KOSHINUSER)

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
            csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId     ' 端末ＩＤ
            csDataRow(ABMyNumberEntity.SAKUJOFG) = SAKUJOFG_OFF                        ' 削除フラグ
            csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              ' 更新カウンター
            csDataRow(ABMyNumberEntity.SAKUSEINICHIJI) = strUpdateDatetime             ' 作成日時
            csDataRow(ABMyNumberEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      ' 作成ユーザー
            csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime              ' 更新日時
            csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId       ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfInsertParamCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

                strParamName = String.Concat(ABMyNumberEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csColumnList.Add(csDataColumn.ColumnName)
                csParamList.Add(strParamName)

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfInsertParamCollection.Add(cfParam)

            Next csDataColumn

            m_strInsertSQL = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", _
                                           ABMyNumberEntity.TABLE_NAME, _
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
            csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId                                      ' 端末ＩＤ
            csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = CType(csDataRow(ABMyNumberEntity.KOSHINCOUNTER), Decimal) + 1   ' 更新カウンタ
            csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime                                               ' 更新日時
            csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                        ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfUpdateParamCollection

                If (cfParam.ParameterName.StartsWith(ABMyNumberEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

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

                strParamName = String.Concat(ABMyNumberEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                csParamList.Add(String.Format("{0} = {1}", csDataColumn.ColumnName, strParamName))

                cfParam = New UFParameterClass
                cfParam.ParameterName = strParamName
                m_cfUpdateParamCollection.Add(cfParam)

            Next csDataColumn

            m_strUpdateSQL = String.Format("UPDATE {0} SET {1} ", _
                                           ABMyNumberEntity.TABLE_NAME, _
                                           String.Join(","c, CType(csParamList.ToArray(GetType(String)), String())))

            csWhere = New StringBuilder(256)
            With csWhere
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER)
            End With
            m_strUpdateSQL += csWhere.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER
            m_cfUpdateParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER
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
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
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
                .AppendFormat("DELETE FROM {0} ", ABMyNumberEntity.TABLE_NAME)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER)
            End With
            m_strDeleteSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER
            m_cfDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER
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
            If (m_blnIsCreateLogicalDeleteSQL = False) Then

                Call CreateLogicalDeleteSQL(csDataRow)

                m_blnIsCreateLogicalDeleteSQL = True

            Else
                ' noop
            End If

            ' 更新日時を取得
            strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' 共通項目の編集を行う
            csDataRow(ABMyNumberEntity.TANMATSUID) = m_cfControlData.m_strClientId                                      ' 端末ＩＤ
            csDataRow(ABMyNumberEntity.SAKUJOFG) = SAKUJOFG_ON                                                          ' 削除フラグ
            csDataRow(ABMyNumberEntity.KOSHINCOUNTER) = CType(csDataRow(ABMyNumberEntity.KOSHINCOUNTER), Decimal) + 1   ' 更新カウンタ
            csDataRow(ABMyNumberEntity.KOSHINNICHIJI) = strUpdateDatetime                                               ' 更新日時
            csDataRow(ABMyNumberEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                        ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam As UFParameterClass In m_cfLogicalDeleteParamCollection

                If (cfParam.ParameterName.StartsWith(ABMyNumberEntity.PREFIX_KEY, StringComparison.CurrentCulture) = True) Then

                    ' キー項目は更新前の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                Else

                    ' キー項目以外は更新後の値で設定
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABMyNumberEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()

                End If

            Next cfParam

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strLogicalDeleteSQL, m_cfLogicalDeleteParamCollection) + "】")

            ' SQLの実行
            intKoshinCount = m_cfRdbClass.ExecuteSQL(m_strLogicalDeleteSQL, m_cfLogicalDeleteParamCollection)

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

#Region "CreateLogicalDeleteSQL"

    ''' <summary>
    ''' CreateLogicalDeleteSQL
    ''' </summary>
    ''' <param name="csDataRow">更新対象DataRow</param>
    ''' <remarks></remarks>
    Private Sub CreateLogicalDeleteSQL(ByVal csDataRow As DataRow)

        Dim cfParam As UFParameterClass
        Dim csSQL As StringBuilder

        Try

            m_cfLogicalDeleteParamCollection = New UFParameterCollectionClass

            csSQL = New StringBuilder(256)
            With csSQL
                .AppendFormat("UPDATE {0} ", ABMyNumberEntity.TABLE_NAME)
                .Append("SET ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.TANMATSUID, ABMyNumberEntity.PARAM_TANMATSUID)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.SAKUJOFG, ABMyNumberEntity.PARAM_SAKUJOFG)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.PARAM_KOSHINCOUNTER)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINNICHIJI, ABMyNumberEntity.PARAM_KOSHINNICHIJI)
                .Append(", ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.KOSHINUSER, ABMyNumberEntity.PARAM_KOSHINUSER)
                .Append("WHERE ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.KEY_JUMINCD)
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.KEY_MYNUMBER)
                .Append("AND ")
                .AppendFormat("{0} = {1}", ABMyNumberEntity.KOSHINCOUNTER, ABMyNumberEntity.KEY_KOSHINCOUNTER)
            End With
            m_strLogicalDeleteSQL = csSQL.ToString

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.PARAM_TANMATSUID
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.PARAM_SAKUJOFG
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINCOUNTER
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINNICHIJI
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.PARAM_KOSHINUSER
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_JUMINCD
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_MYNUMBER
            m_cfLogicalDeleteParamCollection.Add(cfParam)

            cfParam = New UFParameterClass
            cfParam.ParameterName = ABMyNumberEntity.KEY_KOSHINCOUNTER
            m_cfLogicalDeleteParamCollection.Add(cfParam)

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

    '*履歴番号 000002 2016/01/27 追加開始

    ''' <summary>
    ''' SelectConsentByJuminCd
    ''' </summary>
    ''' <param name="strJuminCd">住民コード</param>
    ''' <param name="blnSakujoFG">削除フラグ</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>直近のみ</remarks>
    Public Overloads Function SelectConsentByJuminCd( _
        ByVal strJuminCd As String, ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csMyNumberEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCd
                m_cfSelectParamCollection.Add(cfParam)

                ' 直近区分
                .Append("AND ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.CKINKB, ABMyNumberEntity.PARAM_CKINKB)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABMyNumberEntity.PARAM_CKINKB
                cfParam.Value = ABConstClass.MYNUMBER.CHOKKIN
                m_cfSelectParamCollection.Add(cfParam)

                ' 削除フラグ
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat("AND {0}", SQL_SAKUJOFG)
                End If

            End With

            ' 抽出処理を実行
            csMyNumberEntity = Me.SelectConsent(csSQL.ToString(), m_cfSelectParamCollection)

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
        Return csMyNumberEntity

    End Function

    ''' <summary>
    ''' SelectConsent
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks></remarks>
    Private Overloads Function SelectConsent( _
        ByVal strWhere As String, _
        ByVal cfParamCollection As UFParameterCollectionClass) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim strSQL As String
        Dim csMyNumberEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsConsentSelectSQL = False) Then

                Call CreateSelectConcentSQL()

                m_blnIsConsentSelectSQL = True

            Else
                ' noop
            End If

            ' WHERE区の作成
            If (strWhere.Trim.RLength > 0) Then
                strSQL = String.Format(m_strSelectConsentSQL, String.Concat(" WHERE ", strWhere))
            Else
                strSQL = String.Format(m_strSelectConsentSQL, String.Empty)
            End If

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】")

            ' SQLの実行 DataSetの取得
            csMyNumberEntity = m_csDataSchema.Clone()
            csMyNumberEntity = m_cfRdbClass.GetDataSet(strSQL, csMyNumberEntity, ABMyNumberEntity.TABLE_NAME, cfParamCollection, False)

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
        Return csMyNumberEntity

    End Function

    ''' <summary>
    ''' CreateSelectConcentSQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSelectConcentSQL()

        Dim csSQL As StringBuilder

        Try

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' SELECT区の生成
            csSQL.Append("SELECT ")
            csSQL.Append(ABMyNumberEntity.JUMINCD)
            csSQL.AppendFormat(", {0}", ABMyNumberEntity.MYNUMBER)
            csSQL.AppendFormat(", {0}", ABMyNumberEntity.CKINKB)
            csSQL.AppendFormat(", {0}", ABMyNumberEntity.RESERVE)
            csSQL.AppendFormat(", {0}", ABMyNumberEntity.SAKUJOFG)

            ' FROM区の生成
            csSQL.AppendFormat(" FROM {0}", ABMyNumberEntity.TABLE_NAME)

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABMyNumberEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0},", ABMyNumberEntity.JUMINCD)
            csSQL.AppendFormat(" {0}", ABMyNumberEntity.MYNUMBER)

            ' メンバー変数に設定
            m_strSelectConsentSQL = csSQL.ToString()

        Catch csExp As Exception
            Throw
        End Try

    End Sub
    '*履歴番号 000002 2016/01/27 追加終了

    '************************************************************************************************
    '* メソッド名      同一人取得
    '* 
    '* 構文            Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet
    '* 
    '* 機能　　        同一個人法人番号のデータを取得する
    '* 
    '* 引数            住民コード配列  : a_strJuminCD()
    '* 
    '* 戻り値          DataSet
    '************************************************************************************************
    Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strParameterName As String
        Dim strSQL As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABMyNumberEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABMyNumberEntity.MYNUMBER)
                .Append(" IN (SELECT ")
                .Append(ABMyNumberEntity.MYNUMBER)
                .Append(" FROM ")
                .Append(ABMyNumberEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABMyNumberEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To a_strJuminCD.Length - 1

                    ' -----------------------------------------------------------------------------
                    ' 住民コード
                    strParameterName = ABMyNumberEntity.KEY_JUMINCD + i.ToString

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
                .Append(ABMyNumberEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" AND ")
                .Append(ABMyNumberEntity.CKINKB)
                .Append(" = '1')")
                .Append(" AND ")
                .Append(ABMyNumberEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" AND ")
                .Append(ABMyNumberEntity.CKINKB)
                .Append(" = '1'")

            End With
            strSQL = csSQL.ToString
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParameterCollection) + "】")

            ' SQLの実行 DataSetの取得
            csDataSet = m_cfRdbClass.GetDataSet(strSQL, ABMyNumberEntity.TABLE_NAME, cfParameterCollection)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + csAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csDataSet

    End Function

    '************************************************************************************************
    '* メソッド名      同一人取得
    '* 
    '* 構文            Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet
    '* 
    '* 機能　　        同一個人法人番号のデータを取得する
    '* 
    '* 引数            住民コード  : strJuminCD
    '* 
    '* 戻り値          DataSet
    '************************************************************************************************
    Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strSQL As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABMyNumberEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABMyNumberEntity.MYNUMBER)
                .Append(" IN (SELECT ")
                .Append(ABMyNumberEntity.MYNUMBER)
                .Append(" FROM ")
                .Append(ABMyNumberEntity.TABLE_NAME)
                .Append(" WHERE ")
                .AppendFormat("{0} = {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.PARAM_JUMINCD)
                .Append(" AND ")
                .Append(ABMyNumberEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" AND ")
                .Append(ABMyNumberEntity.CKINKB)
                .Append(" = '1')")
                .Append(" AND ")
                .Append(ABMyNumberEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" AND ")
                .Append(ABMyNumberEntity.CKINKB)
                .Append(" = '1'")

            End With
            strSQL = csSQL.ToString

            cfParameter = New UFParameterClass
            cfParameter.ParameterName = ABMyNumberEntity.PARAM_JUMINCD
            cfParameter.Value = strJuminCD
            cfParameterCollection.Add(cfParameter)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParameterCollection) + "】")

            ' SQLの実行 DataSetの取得
            csDataSet = m_cfRdbClass.GetDataSet(strSQL, ABMyNumberEntity.TABLE_NAME, cfParameterCollection)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + csAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csDataSet

    End Function
#End Region

End Class
