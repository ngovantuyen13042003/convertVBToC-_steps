'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         ＡＢ代納送付先異動累積マスタＤＡ
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2007/08/10
'*
'* 作成者           比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'*  修正履歴　 履歴番号　　修正内容
'* 2010/02/26   000001     送付先データ更新の場合、代納送付先累積マスタ:代納区分に｢40｣をセットするよう改修（比嘉）
'* 2010/04/16   000002     VS2008対応（比嘉）
'* 2023/10/25   000003    【AB-0840-1】送付先管理項目追加（見城）
'* 2023/12/05   000004    【AB-0840-1】送付先管理項目追加_追加修正（仲西）
'* 2024/03/07   000005    【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
'* 2024/06/10   000006    【AB-9902-1】不具合対応 
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
'*履歴番号 000003 2023/10/25 追加開始
Imports System.CodeDom
Imports System.Web.UI.WebControls
Imports Densan.Reams.UR.UR002BB
Imports Densan.Reams.UR.UR002BX
'*履歴番号 000003 2023/10/25 追加終了

'************************************************************************************************
'*
'* 代納送付先異動累積マスタ取得、更新時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABDainoSfskRuisekiBClass

#Region "メンバ変数"
    'パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_csDataSchma As DataSet                        ' スキーマ保管用データセット
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABDainoSfskRuisekiBClass"            ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード
    Private Const STRING_D As String = "D"                                          ' 代納
    Private Const string_S As String = "S"                                          ' 送付先
    '*履歴番号 000003 2023/10/25 追加開始
    Private Const ZENGOKB_ZEN As String = "1"                                       '前後区分　前
    Private Const ZENGOKB_GO As String = "2"                                        '前後区分　後
    Private Const SOUFU_TSUIKA As String = "S0"                                     '処理区分　送付_追加
    Private Const SOUFU_SHUSEI As String = "S1"                                     '処理区分　送付_修正
    Private Const SOUFU_SAKUJO As String = "S2"                                     '処理区分　送付_削除
    Private Const DAINO_TSUIKA As String = "D0"                                     '処理区分　代納_追加
    Private Const DAINO_SHUSEI As String = "D1"                                     '処理区分　代納_修正
    Private Const DAINO_SAKUJO As String = "D2"                                     '処理区分　代納_削除
    Private Const SAKUJO_ON As String = "1"                                         '削除フラグ
    '*履歴番号 000003 2023/10/25 追加終了
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' パラメータのメンバ変数
        m_strInsertSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing

        ' AB代納送付先累積マスタのスキーマ取得
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"

#Region "代納送付先異動累積マスタ抽出"
    ' 使用していないが、作ったので残しておく
    '''''************************************************************************************************
    '''''* メソッド名     代納送付先異動累積マスタ抽出
    '''''* 
    '''''* 構文           Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
    '''''* 
    '''''* 機能　　    　 代納送付先異動累積マスタよりデータを抽出する
    '''''* 
    '''''* 引数           strJuminCD        : 住民コード
    '''''* 
    '''''* 戻り値         DataSet : 取得した代納送付先異動累積マスタの該当データ
    '''''************************************************************************************************
    ''''Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
    ''''    Const THIS_METHOD_NAME As String = "GetDainoSfsk"
    ''''    Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
    ''''    Dim cfUFParameterClass As UFParameterClass          ' パラメータクラス
    ''''    Dim csDainoSfskEntity As DataSet                    ' 代納送付先累積DataSet
    ''''    Dim strSQL As StringBuilder
    ''''    Dim strWHERE As StringBuilder

    ''''    Try
    ''''        ' デバッグ開始ログ出力
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' パラメータコレクションのインスタンス化
    ''''        m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

    ''''        ' SQL文の作成
    ''''        strSQL = New StringBuilder
    ''''        strSQL.Append("SELECT * FROM ")
    ''''        strSQL.Append(ABDainoSfskRuisekiEntity.TABLE_NAME)
    ''''        strSQL.Append(" WHERE ")

    ''''        'WHERE句の作成
    ''''        strWHERE = New StringBuilder
    ''''        '住民コード
    ''''        If Not (strJuminCD = String.Empty) Then
    ''''            strWHERE.Append(ABDainoSfskRuisekiEntity.JUMINCD)
    ''''            strWHERE.Append(" = ")
    ''''            strWHERE.Append(ABDainoSfskRuisekiEntity.KEY_JUMINCD)
    ''''            ' 検索条件のパラメータを作成
    ''''            cfUFParameterClass = New UFParameterClass
    ''''            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
    ''''            cfUFParameterClass.Value = strJuminCD
    ''''            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
    ''''            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        End If

    ''''        'ORDER句を結合
    ''''        If (strWHERE.Length <> 0) Then
    ''''            strSQL.Append(strWHERE)
    ''''            strSQL.Append(" ORDER BY ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
    ''''            strSQL.Append(" , ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
    ''''        Else
    ''''            strSQL.Append(" ORDER BY ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
    ''''            strSQL.Append(", ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
    ''''            strSQL.Append(", ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
    ''''        End If

    ''''        ' RDBアクセスログ出力
    ''''        m_cfLogClass.RdbWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + Me.GetType.Name + "】" + _
    ''''                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
    ''''                                    "【実行メソッド名:GetDataSet】" + _
    ''''                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")

    ''''        ' SQLの実行 DataSetの取得
    ''''        csDainoSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)


    ''''        ' デバッグ終了ログ出力
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException
    ''''        ' ワーニングログ出力
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
    ''''                                    "【ワーニング内容:" + objAppExp.Message + "】")
    ''''        ' エラーをそのままスローする
    ''''        Throw

    ''''    Catch objExp As Exception
    ''''        ' エラーログ出力
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【エラー内容:" + objExp.Message + "】")
    ''''        ' エラーをそのままスローする
    ''''        Throw
    ''''    End Try

    ''''    Return csDainoSfskEntity

    ''''End Function
#End Region

#Region "代納送付先異動累積マスタ追加"
    '************************************************************************************************
    '* メソッド名     代納送付先異動累積マスタ追加
    '* 
    '* 構文           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     　代納送付先異動累積マスタにデータを追加
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoSfskB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intInsCnt As Integer                            ' 追加件数
        Dim strUpdateDateTime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '作成日時

            '共通項目の編集を行う
            csDataRow(ABDainoSfskRuisekiEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' 端末ＩＤ
            'csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG) = "0"                              ' 削除フラグ
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER) = Decimal.Zero                ' 更新カウンタ
            csDataRow(ABDainoSfskRuisekiEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' 作成日時
            csDataRow(ABDainoSfskRuisekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' 作成ユーザー
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINNICHIJI) = strUpdateDateTime           ' 更新日時
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw
        End Try

        Return intInsCnt

    End Function
#End Region

#Region "SQL文作成"
    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 INSERTのSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義


        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass


            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' 最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw
        End Try

    End Sub
#End Region

#Region "代納送付先累積データ作成"
    '*履歴番号 000003 2023/10/25 修正開始
    '************************************************************************************************
    '* メソッド名     代納送付先累積データ作成
    '* 
    '* 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String) As Integer
    '* 
    '* 機能　　    　 代納送付先累積データを作成する
    '* 
    '* 引数           csDataRow As DataRow      : 代納送付先データ
    '*                strShoriKB As String      : 処理区分
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String) As Integer
        Dim strShoriNichiji As String = String.Empty

        Return CreateDainoSfskData(csDataRow, strShoriKB, Nothing, strShoriNichiji)

    End Function

    '************************************************************************************************
    '* メソッド名     代納送付先累積データ作成
    '* 
    '* 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '                                                     ByRef strShoriNichiji As String) As Integer
    '* 
    '* 機能　　    　 代納送付先累積データを作成する
    '* 
    '* 引数           csDataRow As DataRow      : 代納送付先データ
    '*                strShoriKB As String      : 処理区分
    '*                strShoriNichiji As String : 処理日時
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String, ByRef strShoriNichiji As String) As Integer

        Return CreateDainoSfskData(csDataRow, strShoriKB, Nothing, strShoriNichiji)

    End Function

    '************************************************************************************************
    '* メソッド名     代納送付先累積データ作成
    '* 
    '* 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal csABSfskHyojunDataRow As DataRow, _
    '*                                                    ByRef strShoriNichiji As String) As Integer
    '* 
    '* 機能　　    　 代納送付先累積データを作成する
    '* 
    '* 引数           csDataRow As DataRow                : 代納送付先データ
    '*                strShoriKB As String                : 処理区分
    '*                csABSfskHyojunDataRow As DataRow    : AB送付先_標準データ（DataRow形式）
    '*                strShoriNichiji As String           : 処理日時
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************

    'Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String) As Integer
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String,
                                        ByVal csABSfskHyojunDataRow As DataRow, ByRef strShorinichiji As String) As Integer
        '*履歴番号 000003 2023/10/25 修正終了
        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"
        Dim csDataSet As DataSet
        Dim csRuisekiDR As DataRow
        Dim csDataColumn As DataColumn
        Dim strSystemDate As String                 ' システム日付
        Dim intInsCnt As Integer
        'Dim csDainoSfskRows() As DataRow
        'Dim csDainoSfskRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csNewDainosfskRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim csOriginalDR As DataRow
        'Dim csDainoSfskEntity As DataSet
        Dim intUpdataCount_zen As Integer
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")
            strShorinichiji = strSystemDate
            ' スキーマを取得
            csDataSet = m_csDataSchma.Clone

            '***
            '* 代納送付先累積(前)編集処理
            '*
            If (strShoriKB <> ABConstClass.DAINO_ADD AndAlso strShoriKB <> ABConstClass.SFSK_ADD) Then
                ' 処理区分が追加以外の場合
                If (csDataRow.HasVersion(DataRowVersion.Original)) Then
                    ' 修正前情報が残っている場合

                    ' 代納送付先累積データを作成
                    csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csDataRow.Table.Columns
                        If Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB) Then
                            csOriginalDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    csOriginalDR(ABDainoSfskRuisekiEntity.SHORINICHIJI) = strSystemDate
                    csOriginalDR(ABDainoSfskRuisekiEntity.SHORIKB) = strShoriKB               ' 処理区分
                    csOriginalDR(ABDainoSfskRuisekiEntity.ZENGOKB) = "1"                      ' 前後区分

                    '*履歴番号 000001 2010/02/26 修正開始
                    ' -- コメント修正 --
                    ''''' 送付先データの場合、送付先区分を代納区分にセットする
                    ' 送付先データの場合、代納区分に｢40｣をセットする。送付先データは｢40｣固定のため。
                    ' -- コメント修正 --
                    If (strShoriKB.RSubstring(0, 1) = "S") Then
                        'csOriginalDR(ABDainoSfskRuisekiEntity.DAINOKB) = csDataRow(ABSfskEntity.SFSKDATAKB)
                        csOriginalDR(ABDainoSfskRuisekiEntity.DAINOKB) = "40"

                        '*履歴番号 000003 2023/10/25 追加開始
                        If ((Not IsNothing(csABSfskHyojunDataRow)) AndAlso (csABSfskHyojunDataRow.HasVersion(DataRowVersion.Original))) Then
                            ' 送付先_標準がNothing以外でかつ、修正前情報が残っている場合
                            '送付先番地コード１
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD1, DataRowVersion.Original)
                            '送付先番地コード２
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD2, DataRowVersion.Original)
                            '送付先番地コード３
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD3, DataRowVersion.Original)
                            '送付先方書コード
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKATAGAKICD, DataRowVersion.Original)
                        End If
                        '*履歴番号 000003 2023/10/25 追加終了

                    Else
                    End If
                    '*履歴番号 000001 2010/02/26 修正終了

                    ' データセットに修正前情報を追加
                    csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Add(csOriginalDR)

                    ' 代納送付先累積(前)マスタ追加処理
                    intUpdataCount_zen = Me.InsertDainoSfskB(csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows(0))

                    ' 更新件数が１件以外の場合、エラーを発生させる
                    If Not (intUpdataCount_zen = 1) Then
                        m_cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        ' エラー定義を取得（既に同一データが存在します。：代納送付先累積）
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "代納送付先累積", objErrorStruct.m_strErrorCode)
                    End If

                    ' データセットのクリア
                    csDataSet.Clear()
                Else

                End If
            Else

            End If


            '***
            '* 代納送付先累積(後)編集処理
            '*
            ' 代納送付先累積データを作成
            csRuisekiDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

            For Each csDataColumn In csDataRow.Table.Columns
                If Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                    Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB) Then
                    csRuisekiDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName)
                End If
            Next

            ' 共通項目のデータセット
            csRuisekiDR(ABDainoSfskRuisekiEntity.SHORINICHIJI) = strSystemDate              ' 処理日時
            csRuisekiDR(ABDainoSfskRuisekiEntity.SHORIKB) = strShoriKB                      ' 処理区分
            csRuisekiDR(ABDainoSfskRuisekiEntity.ZENGOKB) = "2"                             ' 前後区分
            csRuisekiDR(ABDainoSfskRuisekiEntity.RESERVE1) = String.Empty                   ' リザーブ1
            csRuisekiDR(ABDainoSfskRuisekiEntity.RESERVE2) = String.Empty                   ' リザーブ2

            '*履歴番号 000003 2023/10/25 追加開始
            '代納、送付先の処理区分が削除の場合、削除フラグを立てる
            If (strShoriKB = ABConstClass.DAINO_DELETE OrElse strShoriKB = ABConstClass.SFSK_DELETE) Then
                csRuisekiDR(ABDainoSfskRuisekiEntity.SAKUJOFG) = SAKUJO_ON                  ' 削除フラグ

            End If
            '*履歴番号 000003 2023/10/25 追加終了

            ' 代納データ、送付先データ別処理の場合
            'If (CStr(csDataRow(ABDainoSfskRuisekiEntity.DAINOKB)) <> "40") Then
            If (strShoriKB.RSubstring(0, 1) = "D") Then
                ' 代納データの場合
                ' 代納区分が"40"以外の場合
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB) = String.Empty     ' 送付先管内管外区分
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANAMEISHO) = String.Empty         ' 送付先カナ名称
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO) = String.Empty        ' 送付先漢字名称
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKYUBINNO) = String.Empty            ' 送付先郵便番号
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKZJUSHOCD) = String.Empty           ' 送付先住所コード
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKJUSHO) = String.Empty              ' 送付先住所
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' 送付先番地コード1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' 送付先番地コード2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' 送付先番地コード3
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHI) = String.Empty             ' 送付先番地
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' 送付先方書コード
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKI) = String.Empty           ' 送付先方書
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1) = String.Empty       ' 送付先連絡先1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2) = String.Empty       ' 送付先連絡先2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD) = String.Empty         ' 送付先行政区コード
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI) = String.Empty        ' 送付先行政区名
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD1) = String.Empty           ' 送付先地区コード1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1) = String.Empty          ' 送付先地区名1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD2) = String.Empty           ' 送付先地区コード2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2) = String.Empty          ' 送付先地区名2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD3) = String.Empty           ' 送付先地区コード3
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3) = String.Empty          ' 送付先地区名3
            Else
                ' 送付先データの場合
                ' 代納区分が"40"の場合
                '*履歴番号 000001 2010/02/26 修正開始
                '**コメント ： 送付先データの場合、代納区分に｢40｣をセット。送付先データは｢40｣固定のため。
                'csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOKB) = csDataRow(ABSfskEntity.SFSKDATAKB)
                csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOKB) = "40"
                '*履歴番号 000001 2010/02/26 修正終了
                '*履歴番号 000003 2023/10/25 修正開始
                'csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOJUMINCD) = String.Empty
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' 送付先番地コード1
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' 送付先番地コード2
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' 送付先番地コード3
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' 送付先方書コード
                If (Not IsNothing(csABSfskHyojunDataRow)) Then
                    ' 送付先_標準がNothing以外の場合
                    '送付先番地コード１
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD1)
                    '送付先番地コード２
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD2)
                    '送付先番地コード３
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD3)
                    '送付先方書コード
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKATAGAKICD)
                Else
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' 送付先番地コード1
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' 送付先番地コード2
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' 送付先番地コード3
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' 送付先方書コード

                End If
                '*履歴番号 000003 2023/10/25 修正終了
            End If

            csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Add(csRuisekiDR)

            '***
            '* 代納送付先累積(後)マスタ追加処理
            '*
            intInsCnt = InsertDainoSfskB(csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows(0))

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw
        End Try

        Return intInsCnt

    End Function
#End Region

    '*履歴番号 000003 2023/10/25 追加開始
#Region "代納送付先累積データ抽出"
    '************************************************************************************************
    '* メソッド名     代納送付先累積データ抽出
    '* 
    '* 構文           PPublic Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
    '*                                                           ByVal strGyomuCD As String,
    '*                                                           ByVal strGyomuNaiShubetsuCD As String,
    '*                                                           ByVal intTorokuRenban As Integer,
    '*                                                           ByVal strShoriKB As String) As DataRow()
    '* 
    '* 機能　　    　 代納送付先累積マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD             : 住民コード 
    '*                strGyomuCD             : 業務コード
    '*                strGyomuNaiShubetsuCD  : 業務内種別コード
    '*                intTorokuRenban        : 登録番号
    '*                strShoriKB             : 処理区分　"D"：代納、"S"：送付
    '* 
    '* 戻り値         DataSet : 取得した代納送付先累積マスタの該当データ(DataRow())
    '************************************************************************************************
    Public Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomuNaiShubetsuCD As String,
                                              ByVal intTorokuRenban As Integer,
                                              ByVal strShoriKB As String) As DataRow()

        Const THIS_METHOD_NAME As String = "GetABDainoSfskRuisekiData"
        Dim csDainoSfskRuisekiEntity As DataSet
        Dim csReturnDataRows As DataRow()
        Dim strSQL As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban.ToString(), strShoriKB, THIS_METHOD_NAME))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csDainoSfskRuisekiEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity,
                                                    ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '戻り値用にデータを格納
            strSQL.Clear()
            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
            strSQL.Append(" = '")
            strSQL.Append(strJuminCD)
            strSQL.Append("'")
            csReturnDataRows = csDainoSfskRuisekiEntity.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Select(strSQL.ToString)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csReturnDataRows

    End Function

    '************************************************************************************************
    '* メソッド名     SELECT句の作成
    '* 
    '* 構文           Private Sub CreateSelect() As String
    '* 
    '* 機能           SELECT句を生成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String    :   SELECT句
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim strSELECT As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の作成
            strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiEntity.JUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.ZENGOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TOROKURENBAN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.STYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.EDYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RRKNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOJUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANAMEISHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKYUBINNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKZJUSHOCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKJUSHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKICD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TANMATSUID)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUJOFG)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINCOUNTER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEIUSER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINNICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINUSER)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return strSELECT.ToString

    End Function

    '************************************************************************************************
    '* メソッド名     WHERE文の作成
    '* 
    '* 構文           Private Function CreateWhere(ByVal strJuminCD As String,
    '*                                             ByVal strGyomuCD As String,
    '*                                             ByVal strGyomuNaiShubetsuCD As String,
    '*                                             ByVal strTorokuRenban As String,
    '*                                             ByVal strShoriKB As String,
    '*                                             ByVal strMethodName As String) As String
    '* 
    '* 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数           strJuminCD             : 住民コード 
    '*                strGyomuCD             : 業務コード
    '*                strGyomuNaiShubetsuCD  : 業務内種別コード
    '*                strTorokuRenban        : 登録連番
    '*                strShoriKB             : 処理区分　"D"：代納、"S"：送付
    '*                strMethodName          : 呼出し元関数名
    '*
    '* 戻り値         String    :   WHERE句
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strGyomuCD As String,
                                 ByVal strGyomuNaiShubetsuCD As String,
                                 ByVal strTorokuRenban As String,
                                 ByVal strShoriKB As String,
                                 ByVal strMethodName As String) As String

        Const THIS_METHOD_NAME As String = "CreateWhere"
        Const GET_MAX_TOROKURENBAN As String = "GetMaxTorokuRenban"

        Dim strWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECTパラメータコレクションクラスのインスタンス化
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE句の作成
            strWHERE = New StringBuilder(256)

            ' 住民コード
            strWHERE.AppendFormat("WHERE {0} = {1}", ABDainoSfskRuisekiEntity.JUMINCD, ABDainoSfskRuisekiEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 業務コード
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUCD, ABDainoSfskRuisekiEntity.KEY_GYOMUCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 業務内種別コード
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD, ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomuNaiShubetsuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 登録連番
            If (Not (strTorokuRenban = String.Empty)) Then
                strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN
                cfUFParameterClass.Value = strTorokuRenban
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '処理区分
            Select Case strShoriKB
                Case string_S
                    '送付
                    strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE)

                Case STRING_D
                    '代納
                    '*履歴番号 000004 2023/12/05 修正開始
                    'strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                    '                     ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_SHUSEI)
                    strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_DELETE)
                    '*履歴番号 000004 2023/12/05 修正終了

            End Select

            '前後区分
            strWHERE.AppendFormat(" AND {0} = '{1}'", ABDainoSfskRuisekiEntity.ZENGOKB, ZENGOKB_GO)

            '履歴番号　降番でソート　
            If (strMethodName <> GET_MAX_TOROKURENBAN) Then
                strWHERE.AppendFormat(" ORDER BY {0} DESC", ABDainoSfskRuisekiEntity.RRKNO)
            End If


            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return strWHERE.ToString

    End Function
#End Region

#Region "登録連番最大値取得処理"
    '************************************************************************************************
    '* メソッド名     登録連番最大値取得処理
    '* 
    '* 構文           Public Function GetMaxTorokuRenban(ByVal strJuminCD As String,
    '*                                                    ByVal strGyomuCD As String,
    '*                                                    ByVal strGyomuNaiShubetsuCD As String,
    '*                                                    ByVal strShoriKB As String) As Integer
    '* 
    '* 機能　　    　 代納送付先累積マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD             : 住民コード 
    '*                strGyomuCD             : 業務コード
    '*                strGyomuNaiShubetsuCD  : 業務内種別コード
    '*                strShoriKB             : 処理区分　"D"：代納、"S"：送付
    '* 
    '* 戻り値         Integer : 取得した登録連番の最大
    '************************************************************************************************
    Public Function GetMaxTorokuRenban(ByVal strJuminCD As String,
                                       ByVal strGyomuCD As String,
                                       ByVal strGyomuNaiShubetsuCD As String,
                                       ByVal strShoriKB As String) As Integer

        Const THIS_METHOD_NAME As String = "GetMaxTorokuRenban"
        Dim csDainoSfskRuisekiEntity As DataSet
        Dim intMaxTorokuRenban As Integer = 0
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の生成
            strSQL.AppendFormat("SELECT MAX({0}) AS MAXTOROKURENBAN ", ABDainoSfskRuisekiEntity.TOROKURENBAN)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, String.Empty, strShoriKB, THIS_METHOD_NAME))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csDainoSfskRuisekiEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity,
                                                    Nothing, m_cfSelectUFParameterCollectionClass, False)

            If (0 < csDainoSfskRuisekiEntity.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Count) Then
                'データがある場合は戻り値に格納する
                If (IsNumeric(csDainoSfskRuisekiEntity.Tables(0).Rows(0).Item(0))) Then
                    intMaxTorokuRenban = CInt(csDainoSfskRuisekiEntity.Tables(0).Rows(0).Item(0))
                Else
                    'データが無い場合は0を戻り値にセット
                    intMaxTorokuRenban = 0
                End If

            End If
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intMaxTorokuRenban

    End Function
#End Region


#Region "代納送付先累積データをエンティティに格納する"

    '************************************************************************************************
    '* メソッド名     代納送付先累積と備考のデータをエンティティに格納する
    '* 
    '* 構文        Public Function SetDainoSfsfRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                    ByVal strShoriKB As String) As DataSet
    '* 
    '* 機能　　    　 代納送付先累積マスタより該当データを格納する
    '* 
    '* 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
    '*                strShoriKB As String                   ：処理区分　"D"：代納、"S"：送付先
    '* 
    '* 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
    '************************************************************************************************
    Public Function SetDainoSfsfRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        Const SHORIKB_SFSK As String = "S"
        Const SHORIKB_DAINO As String = "D"

        Dim csReturnDataset As DataSet

        If (strShoriKB = SHORIKB_SFSK) Then
            csReturnDataset = SetSfskRirekiData(csDainoSfskRuisekiDataset, strShoriKB)
        ElseIf (strShoriKB = SHORIKB_DAINO) Then
            csReturnDataset = SetDainoRirekiData(csDainoSfskRuisekiDataset, strShoriKB)
        End If

        Return csReturnDataset
    End Function

    '************************************************************************************************
    '* メソッド名     代納送付先累積と備考のデータをエンティティに格納する
    '* 
    '* 構文           Public Function SetSfskRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                  ByVal strShoriKB As String) As DataSet
    '* 
    '* 機能　　    　 代納送付先累積マスタより該当データを格納する
    '* 
    '* 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
    '*                strShoriKB As String                   ：処理区分　"D"：代納、"S"：送付先
    '* 
    '* 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
    '************************************************************************************************
    Public Function SetSfskRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        '定数
        Const ALL9_YMD As String = "99999999"               '年月日オール９
        Const SFSK As String = "送付先"                      '送付先文言

        Dim csReturnDataset As DataSet
        Dim csDataRow As DataRow
        Dim csDataNewRow As DataRow
        Dim csDataColumn As DataColumn

        Dim blnIsDainoSfskBiko As Boolean = False
        Dim csBikoDataSet As DataSet
        '*履歴番号 000004 2023/12/05 修正開始
        'Dim blnSakujoFG As Boolean = False
        Dim blnSakujoFG As Boolean = True
        '*履歴番号 000004 2023/12/05 修正終了

        Dim cGyomuCDMstB As URGyomuCDMstBClass              '業務コードマスタＤＡ
        Dim csGyomuCDMstEntity As DataSet                   '業務コードマスタDataSet
        Dim cfDate As UFDateClass                           '日付クラス
        Dim cDainoKankeiB As ABDainoKankeiBClass            '代納関係取得クラス
        Dim cAtenaGetB As ABAtenaGetBClass                  '宛名取得クラス
        Dim cAtenaHenshuB As ABAtenaHenshuBClass            '宛名編集Ｂ
        Dim cJuminShubetsuB As ABJuminShubetsuBClass        '住民種別名称取得クラス
        Dim cKannaiKangaiKBB As ABKannaiKangaiKBBClass      '管内管外名称取得クラス
        Dim cABBikoB As ABBikoBClass

        Dim csDataTable As DataTable
        Dim cDainoSfskRuisekiB As ABDainoSfskRuisekiBClass               ' 代納送付先累積ＤＡビジネスクラス
        Dim cDainoSfskRuisekiHyojunB As ABDainoSfskRuiseki_HyojunBClass  ' 代納送付先累積_標準ＤＡビジネスクラス
        Dim csSfskRirekiDataRows As DataRow()
        Dim csSfskRirekiHyojunDataRow As DataRow
        Dim csSfskRirekiHyojunDataTable As New DataTable

        'データ抽出用変数
        Dim strJuminCd As String
        Dim strGyomuCD As String
        Dim strGyomuNaiShuCD As String
        Dim intTorokuRenban As Integer
        '*履歴番号 000004 2023/12/05 追加開始
        Dim strKannaiKangaiCD As String
        Dim strKannaiKangaiMeisho As String
        '*履歴番号 000004 2023/12/05 追加終了

        Try

            Dim csDataRows As DataRow()

            csDataRows = csDainoSfskRuisekiDataset.Tables(ABSfskDataEntity.TABLE_NAME).Select(
                                                                    String.Format("{0} = 'True'", ABSfskDataEntity.CHECK))

            strJuminCd = csDataRows(0).Item(ABSfskDataEntity.JUMINCD).ToString
            strGyomuCD = csDataRows(0).Item(ABSfskDataEntity.GYOMUCD).ToString
            strGyomuNaiShuCD = csDataRows(0).Item(ABSfskDataEntity.GYOMUNAISHUCD).ToString
            intTorokuRenban = CInt(csDataRows(0).Item(ABSfskDataEntity.TOROKURENBAN))

            '代納送付先累積データの取得
            ' 代納送付先累積ＤＡクラスのインスタンス化
            cDainoSfskRuisekiB = New ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            csSfskRirekiDataRows = cDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd,
                                                  strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)
            '代納送付先累積_標準データの取得
            cDainoSfskRuisekiHyojunB = New ABDainoSfskRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' データセットインスタンス化
            csReturnDataset = New DataSet

            ' テーブルセットの取得
            csDataTable = Me.CreateColumnsABSfskRirekiData()

            ' データセットにテーブルセットの追加
            csReturnDataset.Tables.Add(csDataTable)

            ' 日付クラスのインスタンス化
            cfDate = New UFDateClass(m_cfConfigDataClass)

            ' 代納関係取得インスタンス化
            cDainoKankeiB = New ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 業務コードマスタＤＡのインスタンス作成
            cGyomuCDMstB = New URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名編集Ｂのインスタンス作成
            cAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名取得クラスインスタンス化
            cAtenaGetB = New ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass)

            ' 住民種別クラスインスタンス化
            cJuminShubetsuB = New ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass)

            ' 管内管外クラスインスタンス化
            cKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass)

            ' 備考クラスのインスタンス化
            cABBikoB = New ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            For Each csDataRow In csSfskRirekiDataRows

                csDataNewRow = csReturnDataset.Tables(ABSfskDataEntity.TABLE_NAME).NewRow

                ' 初期値の設定
                For Each csDataColumn In csDataNewRow.Table.Columns
                    If (csDataColumn.ColumnName = ABSfskDataEntity.KOSHINCOUNTER) Then
                        csDataNewRow(csDataColumn.ColumnName) = Decimal.Zero
                    Else
                        csDataNewRow(csDataColumn.ColumnName) = String.Empty
                    End If
                Next csDataColumn

                ' 住民コード
                csDataNewRow(ABSfskDataEntity.JUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.JUMINCD)
                ' 市町村コード
                csDataNewRow(ABSfskDataEntity.SHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.SHICHOSONCD)
                ' 旧市町村コード
                csDataNewRow(ABSfskDataEntity.KYUSHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
                ' 業務コード
                csDataNewRow(ABSfskDataEntity.GYOMUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD)

                ' 業務コードマスタより取得する
                strGyomuCD = CType(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD), String)
                csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD)

                If (csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows.Count = 0) Then
                    ' 業務名称
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHO) = String.Empty
                    ' 業務名称略
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHORYAKU) = String.Empty
                Else
                    ' 業務名称
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHO) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMUMEI)
                    ' 業務名称略
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHORYAKU) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMURYAKUSHO)
                End If

                ' 業務内種別コード
                csDataNewRow(ABSfskDataEntity.GYOMUNAISHUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
                ' 代納住民コード
                csDataNewRow(ABSfskDataEntity.DAINOJUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD)
                ' 開始年月
                csDataNewRow(ABSfskDataEntity.STYMD) = csDataRow(ABDainoSfskRuisekiEntity.STYMD)
                ' 終了年月
                csDataNewRow(ABSfskDataEntity.EDYMD) = csDataRow(ABDainoSfskRuisekiEntity.EDYMD)

                ' 表示用開始年月
                cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.STYMD), String)
                cfDate.p_enEraType = UFEraType.KanjiRyaku
                cfDate.p_enDateSeparator = UFDateSeparator.Period
                csDataNewRow(ABSfskDataEntity.DISP_STYMD) = cfDate.p_strWarekiYMD

                ' 表示用終了年月（999999の時は、非表示）
                If (CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String) = ALL9_YMD) Then
                    csDataNewRow(ABSfskDataEntity.DISP_EDYMD) = String.Empty
                Else
                    cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String)
                    csDataNewRow(ABSfskDataEntity.DISP_EDYMD) = cfDate.p_strWarekiYMD
                End If

                ' 送付先カナ名称
                csDataNewRow(ABSfskDataEntity.SFSKKANAMEISHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANAMEISHO)
                ' 送付先漢字名称
                csDataNewRow(ABSfskDataEntity.SFSKKANJIMEISHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO)

                ' 送付先管内管外区分
                csDataNewRow(ABSfskDataEntity.SFSKKANNAiKANGAIKB) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB)
                '*履歴番号 000004 2023/12/05 追加開始
                ' 管内管外名称キーセット
                strKannaiKangaiCD = CType(csDataRow(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB), String)
                ' 管内管外名称取得メゾット実行
                strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD)
                ' 管内管外名称
                csDataNewRow(ABSfskDataEntity.SFSKKANNAIKANGAIMEI) = strKannaiKangaiMeisho
                '*履歴番号 000004 2023/12/05 追加終了
                ' 送付先郵便番号
                csDataNewRow(ABSfskDataEntity.SFSKYUBINNO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKYUBINNO)
                ' 送付先住所コード
                csDataNewRow(ABSfskDataEntity.SFSKZJUSHOCD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKZJUSHOCD)
                ' 送付先住所
                csDataNewRow(ABSfskDataEntity.SFSKJUSHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKJUSHO)
                ' 送付先番地
                csDataNewRow(ABSfskDataEntity.SFSKBANCHI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHI)
                ' 送付先番地コード1
                csDataNewRow(ABSfskDataEntity.BANCHICD1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD1)
                ' 送付先番地コード2
                csDataNewRow(ABSfskDataEntity.BANCHICD2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD2)
                ' 送付先番地コード3
                csDataNewRow(ABSfskDataEntity.BANCHICD3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD3)
                ' 送付先方書
                csDataNewRow(ABSfskDataEntity.SFSKKATAGAKI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKATAGAKI)
                ' 送付先連絡先１
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
                ' 送付先連絡先２
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)
                ' 方書コード
                csDataNewRow.Item(ABSfskDataEntity.SFSKKATAGAKICD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD)
                ' 送付先行政区コード
                csDataNewRow(ABSfskDataEntity.SFSKGYOSEIKUCD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD)
                ' 送付先行政区名
                ' 行政区ＣＤに数字以外のものが混入している場合はそのまま行政区名称をセット
                csDataNewRow(ABSfskDataEntity.SFSKGYOSEIKUMEI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI)
                ' 送付先地区コード１
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD1)
                ' 送付先地区名１
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1)
                ' 送付先地区コード２
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD2)
                ' 送付先地区名２
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2)
                ' 送付先地区コード３
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD3)
                ' 送付先地区名３
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3)
                ' 送付先連絡先１
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
                ' 送付先連絡先２
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)


                csSfskRirekiHyojunDataTable = cDainoSfskRuisekiHyojunB.GetABDainoSfskRuisekiData(strJuminCd,
                                                                                             strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)
                csSfskRirekiHyojunDataRow = csSfskRirekiHyojunDataTable.Select(String.Format("{0}='{1}'",
                                                                                ABDainoSfskRuisekiHyojunEntity.RRKNO,
                                                                                csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString))(0)

                ' 備考マスタを取得
                csBikoDataSet = cABBikoB.SelectByKey(
                                        ABBikoEntity.DEFAULT.BIKOKBN.SFSK,
                                        csDataRow(ABDainoSfskRuisekiEntity.JUMINCD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString(),
                                        blnSakujoFG)

                If (csBikoDataSet IsNot Nothing _
                        AndAlso 0 < csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows.Count) Then
                    ' 住民コード
                    csDataNewRow(ABSfskDataEntity.DAINOJUMINCD) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.RESERVE)
                    csDataNewRow(ABSfskDataEntity.BIKO) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.BIKO)
                Else
                    csDataNewRow(ABSfskDataEntity.BIKO) = String.Empty
                End If

                csDataNewRow(ABSfskDataEntity.CHECK) = False
                csDataNewRow(ABSfskDataEntity.JOTAI) = ABDainoSfskShoriMode.Empty.GetHashCode.ToString
                csDataNewRow(ABSfskDataEntity.DISP_JOTAI) = String.Empty
                csDataNewRow(ABSfskDataEntity.SEIGYOKB) = String.Empty

                csDataNewRow(ABSfskDataEntity.TOROKURENBAN) = csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN)     '登録連番
                csDataNewRow(ABSfskDataEntity.RRKNO) = csDataRow(ABDainoSfskRuisekiEntity.RRKNO)                   '履歴番号
                csDataNewRow(ABSfskDataEntity.SHIKUCHOSONCD) = String.Empty                                        '市区町村コート
                csDataNewRow(ABSfskDataEntity.MACHIAZACD) = String.Empty                                           '町字コード
                csDataNewRow(ABSfskDataEntity.TODOFUKEN) = String.Empty                                            '都道府県
                csDataNewRow(ABSfskDataEntity.SHIKUCHOSON) = String.Empty
                csDataNewRow(ABSfskDataEntity.MACHIAZA) = String.Empty

                '送付先区分
                csDataNewRow(ABSfskDataEntity.SFSKKBN) = csSfskRirekiHyojunDataRow.Item(ABDainoSfskRuisekiHyojunEntity.SFSKKBN).ToString()

                csDataNewRow(ABSfskDataEntity.DISP_DAINOKB) = SFSK

                ' 削除フラグ
                csDataNewRow(ABSfskDataEntity.SAKUJOFG) = csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG)

                ' 更新ユーザ
                csDataNewRow(ABSfskDataEntity.KOSHINUSER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINUSER)
                ' 更新カウンタ
                csDataNewRow(ABSfskDataEntity.KOSHINCOUNTER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER)

                csReturnDataset.Tables(ABSfskDataEntity.TABLE_NAME).Rows.Add(csDataNewRow)

            Next csDataRow
            csReturnDataset.AcceptChanges()


        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                    "【クラス名:" + THIS_CLASS_NAME + "】" +
                                    "【メソッド名:" + Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                    "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" +
                                    "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                    "【クラス名:" + THIS_CLASS_NAME + "】" +
                                    "【メソッド名:" + Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                    "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        Return csReturnDataset

    End Function

    '************************************************************************************************
    '* メソッド名     代納送付先累積と備考のデータをエンティティに格納する
    '* 
    '* 構文        Public Function SetDainoRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                    ByVal strShoriKB As String) As DataSet
    '* 
    '* 機能　　    　 代納送付先累積マスタより該当データを格納する
    '* 
    '* 引数           csDainoSfskRuisekiDataset As DataSet   ：代納送付先累積データセット
    '*                strShoriKB As String                  : 処理区分　"D"：代納、"S"：送付先
    '* 
    '* 戻り値         DataSet : 代納履歴一覧表示用のデータ(DataSet)
    '************************************************************************************************
    Public Function SetDainoRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        '定数
        Const ALL9_YMD As String = "99999999"               '年月日オール９
        Const JUSHOHENSHU1_PARA_ONE As String = "1"         '情報編集1　パラメータ＝1
        Const GET_HONNINDATA As String = "1"                '本人データ取得
        Const DATAKB_HOJIN As String = "20"                 'データ区分　法人
        Const DATASHU_FRN As String = "2"                   'データ種　外国人

        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体

        Dim csReturnDataset As DataSet
        Dim csDataRow As DataRow
        Dim csDataNewRow As DataRow
        Dim csDataColumn As DataColumn
        Dim csDainoKankeiDataSet As DataSet
        Dim csAtenaDataSet As DataSet
        Dim csAtenaRow As DataRow

        Dim strDainoKB As String
        Dim intRowCount As Integer
        Dim strDataKB As String
        Dim strDataShu As String
        Dim strMeisho As String
        Dim strKannaiKangaiCD As String
        Dim strKannaiKangaiMeisho As String
        Dim strKanjiShimei As String                        '漢字氏名
        Dim strKanaShimei As String                         'カナ氏名
        Dim strYubinNO As String                            '郵便番号
        Dim csBikoDataSet As DataSet
        '*履歴番号 000004 2023/12/05 修正開始
        'Dim blnSakujoFG As Boolean = False
        Dim blnSakujoFG As Boolean = True
        '*履歴番号 000004 2023/12/05 修正終了

        Dim cGyomuCDMstB As URGyomuCDMstBClass              '業務コードマスタＤＡ
        Dim csGyomuCDMstEntity As DataSet                   '業務コードマスタDataSet
        Dim cfDate As UFDateClass                           '日付クラス
        Dim cDainoKankeiB As ABDainoKankeiBClass            '代納関係取得クラス
        Dim cAtenaGetB As ABAtenaGetBClass                  '宛名取得クラス
        Dim cAtenaGetPara1X As ABAtenaGetPara1XClass        '宛名取得パラメータクラス
        Dim cAtenaHenshuB As ABAtenaHenshuBClass            '宛名編集Ｂ
        Dim csAtena1Entity As DataSet                       '宛名データEntity
        Dim cJuminShubetsuB As ABJuminShubetsuBClass        '住民種別名称取得クラス
        Dim cKannaiKangaiKBB As ABKannaiKangaiKBBClass      '管内管外名称取得クラス
        Dim cABBikoB As ABBikoBClass

        Dim csDataTable As DataTable
        Dim csDainoSfskRuisekiB As ABDainoSfskRuisekiBClass ' 代納送付先累積ＤＡビジネスクラス
        Dim csDainoRirekiDataRows As DataRow()

        'データ抽出用変数
        Dim strJuminCd As String
        Dim strGyomuCD As String
        Dim strGyomuNaiShuCD As String
        Dim intTorokuRenban As Integer

        Try

            Dim csDataRows As DataRow()
            csDataRows = csDainoSfskRuisekiDataset.Tables(ABDainoDataEntity.TABLE_NAME).Select(
                                                                    String.Format("{0} = 'True'", ABDainoDataEntity.CHECK))

            strJuminCd = csDataRows(0).Item(ABDainoDataEntity.JUMINCD).ToString
            strGyomuCD = csDataRows(0).Item(ABDainoDataEntity.GYOMUCD).ToString
            strGyomuNaiShuCD = csDataRows(0).Item(ABDainoDataEntity.GYOMUNAISHUCD).ToString
            intTorokuRenban = CInt(csDataRows(0).Item(ABDainoDataEntity.TOROKURENBAN))


            '代納送付先累積データの取得
            ' 代納送付先累積ＤＡクラスのインスタンス化
            csDainoSfskRuisekiB = New ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            csDainoRirekiDataRows = csDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd,
                                                  strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)

            ' データセットインスタンス化
            csReturnDataset = New DataSet

            ' テーブルセットの取得
            csDataTable = Me.CreateColumnsABDainoRirekiData()

            ' データセットにテーブルセットの追加
            csReturnDataset.Tables.Add(csDataTable)

            ' 日付クラスのインスタンス化
            cfDate = New UFDateClass(m_cfConfigDataClass)

            ' 代納関係取得インスタンス化
            cDainoKankeiB = New ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 業務コードマスタＤＡのインスタンス作成
            cGyomuCDMstB = New URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名編集Ｂのインスタンス作成
            cAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名取得クラスインスタンス化
            cAtenaGetB = New ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass)

            ' 住民種別クラスインスタンス化
            cJuminShubetsuB = New ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass)

            ' 管内管外クラスインスタンス化
            cKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass)

            ' 備考クラスのインスタンス化
            cABBikoB = New ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            For Each csDataRow In csDainoRirekiDataRows
                csDataNewRow = csReturnDataset.Tables(ABDainoDataEntity.TABLE_NAME).NewRow

                ' 初期値の設定
                For Each csDataColumn In csDataNewRow.Table.Columns
                    If (csDataColumn.ColumnName = ABDainoDataEntity.KOSHINCOUNTER) Then
                        csDataNewRow(csDataColumn.ColumnName) = Decimal.Zero
                    Else
                        csDataNewRow(csDataColumn.ColumnName) = String.Empty
                    End If
                Next csDataColumn

                ' 住民コード
                csDataNewRow(ABDainoDataEntity.JUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.JUMINCD)
                ' 市町村コード
                csDataNewRow(ABDainoDataEntity.SHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.SHICHOSONCD)
                ' 旧市町村コード
                csDataNewRow(ABDainoDataEntity.KYUSHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
                ' 業務コード
                csDataNewRow(ABDainoDataEntity.GYOMUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD)

                ' 業務コードマスタより取得する
                strGyomuCD = CType(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD), String)
                csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD)

                If (csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows.Count = 0) Then
                    ' 業務名称
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHO) = String.Empty
                    ' 業務名称略
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHORYAKU) = String.Empty
                Else
                    ' 業務名称
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHO) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMUMEI)
                    ' 業務名称略
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHORYAKU) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMURYAKUSHO)
                End If

                ' 業務内種別コード
                csDataNewRow(ABDainoDataEntity.GYOMUNAISHUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
                ' 代納住民コード
                csDataNewRow(ABDainoDataEntity.DAINOJUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD)
                ' 開始年月
                csDataNewRow(ABDainoDataEntity.STYMD) = csDataRow(ABDainoSfskRuisekiEntity.STYMD)
                ' 終了年月
                csDataNewRow(ABDainoDataEntity.EDYMD) = csDataRow(ABDainoSfskRuisekiEntity.EDYMD)

                ' 表示用開始年月
                cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.STYMD), String)
                cfDate.p_enEraType = UFEraType.KanjiRyaku
                cfDate.p_enDateSeparator = UFDateSeparator.Period
                csDataNewRow(ABDainoDataEntity.DISP_STYMD) = cfDate.p_strWarekiYMD

                ' 表示用終了年月（999999の時は、非表示）
                If (CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String) = ALL9_YMD) Then
                    csDataNewRow(ABDainoDataEntity.DISP_EDYMD) = String.Empty
                Else
                    cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String)
                    csDataNewRow(ABDainoDataEntity.DISP_EDYMD) = cfDate.p_strWarekiYMD
                End If

                ' 代納区分
                csDataNewRow(ABDainoDataEntity.DAINOKB) = csDataRow(ABDainoSfskRuisekiEntity.DAINOKB)
                ' 代納区分名称
                strDainoKB = CType(csDataRow(ABDainoSfskRuisekiEntity.DAINOKB), String)
                csDainoKankeiDataSet = cDainoKankeiB.GetDainoKBHoshu(strDainoKB)
                intRowCount = csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count
                If (Not (intRowCount = 0)) Then
                    csDataNewRow(ABDainoDataEntity.DAINOKBMEISHO) = CType(csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)
                    csDataNewRow(ABDainoDataEntity.DAINOKBRYAKUMEI) = CType(csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                End If

                ' 宛名取得パラメータインスタンス化
                cAtenaGetPara1X = New ABAtenaGetPara1XClass

                ' 宛名抽出キーセット
                cAtenaGetPara1X.p_strJuminCD = CType(csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD), String)
                cAtenaGetPara1X.p_strJushoHenshu1 = JUSHOHENSHU1_PARA_ONE
                cAtenaGetPara1X.p_blnSakujoFG = True
                cAtenaGetPara1X.p_strDaihyoShaKB = GET_HONNINDATA       '*本人データ取得
                '個人番号取得パラメータを設定
                cAtenaGetPara1X.p_strMyNumberKB = ABConstClass.MYNUMBER.MYNUMBERKB.ON

                Try
                    '「宛名取得Ｂ」クラスの「宛名取得２」メソッドを実行
                    csAtenaDataSet = cAtenaGetB.AtenaGet2(cAtenaGetPara1X)

                    intRowCount = csAtenaDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                    If (Not (intRowCount = 1)) Then
                        'エラークラスのインスタンス化
                        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        'エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003078)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    '「宛名編集Ｂ」クラスの「宛名編集」メソッドを実行する
                    csAtena1Entity = cAtenaHenshuB.AtenaHenshu(cAtenaGetPara1X, csAtenaDataSet)

                    csAtenaRow = csAtenaDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)

                    ' 住民名称取得キーセット
                    strDataKB = CType(csAtenaRow(ABAtenaEntity.ATENADATAKB), String)
                    strDataShu = CType(csAtenaRow(ABAtenaEntity.ATENADATASHU), String)
                    ' 住民名称取得メゾット実行
                    cJuminShubetsuB.GetJuminshubetsu(strDataKB, strDataShu)
                    ' 住民種別名称
                    csDataNewRow(ABDainoDataEntity.JUMINSHUMEISHO) = cJuminShubetsuB.p_strHenshuShubetsu

                    ' カナ名
                    strMeisho = CType(csAtenaRow(ABAtenaEntity.KANAMEISHO2), String)
                    If (strMeisho = String.Empty) Then
                        csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO1)
                    Else
                        '### 法人の時はカナ名称１とカナ名称２を半角スペースでくっつける
                        If (strDataKB = DATAKB_HOJIN) Then
                            ' 文字列を結合した場合，MaxLengthを超えないように切り詰め
                            strKanaShimei = CType(csAtenaRow(ABAtenaEntity.KANAMEISHO1), String) + " " + CType(csAtenaRow(ABAtenaEntity.KANAMEISHO2), String)
                            If (strKanaShimei.RLength > csDataNewRow.Table.Columns(ABDainoDataEntity.KANASHIMEI).MaxLength) Then
                                csDataNewRow(ABDainoDataEntity.KANASHIMEI) = strKanaShimei.RSubstring(0, csDataNewRow.Table.Columns(ABDainoDataEntity.KANASHIMEI).MaxLength)
                            Else
                                csDataNewRow(ABDainoDataEntity.KANASHIMEI) = strKanaShimei
                            End If
                        ElseIf (strDataShu.Chars(0) = DATASHU_FRN) Then
                            '### 外国人の時はカナ名称１
                            csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO1)
                        Else
                            csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO2)
                        End If
                    End If

                    strKanjiShimei = CType(csAtena1Entity.Tables(ABAtena1Entity.TABLE_NAME).Rows(0).Item(ABAtena1Entity.HENSHUKANJISHIMEI), String)
                    If (csDataNewRow.Table.Columns(ABDainoDataEntity.KANJISHIMEI).MaxLength < strKanjiShimei.RLength) Then
                        csDataNewRow(ABDainoDataEntity.KANJISHIMEI) = strKanjiShimei.RSubstring(0, csDataNewRow.Table.Columns(ABDainoDataEntity.KANJISHIMEI).MaxLength)
                    Else
                        csDataNewRow(ABDainoDataEntity.KANJISHIMEI) = strKanjiShimei
                    End If

                    ' 管内管外名称キーセット
                    strKannaiKangaiCD = CType(csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB), String)
                    ' 管内管外名称取得メゾット実行
                    strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD)
                    ' 管内管外名称
                    csDataNewRow(ABDainoDataEntity.KANNAIKANGAIMEISHO) = strKannaiKangaiMeisho
                    ' 郵便番号
                    csDataNewRow(ABDainoDataEntity.YUBINNO) = csAtenaRow(ABAtenaEntity.YUBINNO)
                    ' 住所コード
                    csDataNewRow(ABDainoDataEntity.JUSHOCD) = csAtenaRow(ABAtenaEntity.JUSHOCD)
                    ' 住所名
                    csDataNewRow(ABDainoDataEntity.JUSHO) = csAtenaRow(ABAtenaEntity.JUSHO)
                    ' 番地コード１
                    csDataNewRow(ABDainoDataEntity.BANCHICD1) = csAtenaRow(ABAtenaEntity.BANCHICD1)
                    ' 番地コード２
                    csDataNewRow(ABDainoDataEntity.BANCHICD2) = csAtenaRow(ABAtenaEntity.BANCHICD2)
                    ' 番地コード３
                    csDataNewRow(ABDainoDataEntity.BANCHICD3) = csAtenaRow(ABAtenaEntity.BANCHICD3)
                    ' 番地
                    csDataNewRow(ABDainoDataEntity.BANCHI) = csAtenaRow(ABAtenaEntity.BANCHI)
                    ' 方書フラグ
                    csDataNewRow(ABDainoDataEntity.KATAGAKIFG) = csAtenaRow(ABAtenaEntity.KATAGAKIFG)
                    ' 方書コード
                    csDataNewRow(ABDainoDataEntity.KATAGAKICD) = csAtenaRow(ABAtenaEntity.KATAGAKICD)
                    ' 方書
                    csDataNewRow(ABDainoDataEntity.KATAGAKI) = csAtenaRow(ABAtenaEntity.KATAGAKI)
                    ' 連絡先１
                    csDataNewRow(ABDainoDataEntity.RENRAKUSAKI1) = csAtenaRow(ABAtenaEntity.RENRAKUSAKI1)
                    ' 連絡先２
                    csDataNewRow(ABDainoDataEntity.RENRAKUSAKI2) = csAtenaRow(ABAtenaEntity.RENRAKUSAKI2)
                    ' 行政区コード
                    csDataNewRow(ABDainoDataEntity.GYOSEIKUCD) = csAtenaRow(ABAtenaEntity.GYOSEIKUCD)
                    ' 行政区名
                    csDataNewRow(ABDainoDataEntity.GYOSEIKUMEI) = csAtenaRow(ABAtenaEntity.GYOSEIKUMEI)
                    ' 地区コード１
                    csDataNewRow(ABDainoDataEntity.CHIKUCD1) = csAtenaRow(ABAtenaEntity.CHIKUCD1)
                    ' 地区名１
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI1) = csAtenaRow(ABAtenaEntity.CHIKUMEI1)
                    ' 地区コード２
                    csDataNewRow(ABDainoDataEntity.CHIKUCD2) = csAtenaRow(ABAtenaEntity.CHIKUCD2)
                    ' 地区名２
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI2) = csAtenaRow(ABAtenaEntity.CHIKUMEI2)
                    ' 地区コード３
                    csDataNewRow(ABDainoDataEntity.CHIKUCD3) = csAtenaRow(ABAtenaEntity.CHIKUCD3)
                    ' 地区名３
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI3) = csAtenaRow(ABAtenaEntity.CHIKUMEI3)
                    ' 郵便番号
                    strYubinNO = CType(csAtenaRow(ABAtenaEntity.YUBINNO), String).Trim
                    If (3 < strYubinNO.RLength) Then
                        csDataNewRow(ABDainoDataEntity.DISP_YUBINNO) = strYubinNO.RSubstring(0, 3) + "-" + strYubinNO.RSubstring(3)
                    Else
                        csDataNewRow(ABDainoDataEntity.DISP_YUBINNO) = strYubinNO
                    End If
                    ' 表示用編集住所
                    csDataNewRow(ABDainoDataEntity.DISP_HENSHUJUSHO) = csAtena1Entity.Tables(ABAtena1Entity.TABLE_NAME).Rows(0).Item(ABAtena1Entity.HENSHUJUSHO)
                    csDataNewRow(ABDainoDataEntity.KOSHINUSER) = csDataRow(ABAtenaEntity.KOSHINUSER)
                    csDataNewRow(ABDainoDataEntity.MYNUMBER) = csAtenaRow(ABMyNumberEntity.MYNUMBER)
                    csDataNewRow(ABDainoDataEntity.ATENADATAKB) = csAtenaRow(ABAtenaEntity.ATENADATAKB)

                    ' 備考マスタを取得
                    csBikoDataSet = cABBikoB.SelectByKey(
                                            ABBikoEntity.DEFAULT.BIKOKBN.DAINO,
                                            csDataRow(ABDainoSfskRuisekiEntity.JUMINCD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString(),
                                            blnSakujoFG)

                    If (csBikoDataSet IsNot Nothing _
                                AndAlso 0 < csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows.Count) Then
                        csDataNewRow(ABDainoDataEntity.BIKO) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.BIKO)
                    Else
                        csDataNewRow(ABDainoDataEntity.BIKO) = String.Empty
                    End If

                    csDataNewRow(ABDainoDataEntity.CHECK) = False
                    csDataNewRow(ABDainoDataEntity.JOTAI) = ABDainoSfskShoriMode.Empty.GetHashCode.ToString
                    csDataNewRow(ABDainoDataEntity.DISP_JOTAI) = String.Empty
                    csDataNewRow(ABDainoDataEntity.SEIGYOKB) = String.Empty

                    csDataNewRow(ABDainoDataEntity.TOROKURENBAN) = csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN)     '登録連番
                    csDataNewRow(ABDainoDataEntity.RRKNO) = csDataRow(ABDainoSfskRuisekiEntity.RRKNO)                   '履歴番号
                    csDataNewRow(ABDainoDataEntity.SHIKUCHOSONCD) = String.Empty                                        '市区町村コート
                    csDataNewRow(ABDainoDataEntity.MACHIAZACD) = String.Empty                                           '町字コード
                    csDataNewRow(ABDainoDataEntity.TODOFUKEN) = String.Empty                                            '都道府県

                    csDataNewRow(ABDainoDataEntity.SHORINICHIJI) = csDataRow(ABDainoSfskRuisekiEntity.SHORINICHIJI)     '処理日時
                    csDataNewRow(ABDainoDataEntity.ZENGOKB) = csDataRow(ABDainoSfskRuisekiEntity.ZENGOKB)               '前後区分

                Catch
                    'そのままスローする
                    Throw
                End Try


                ' 削除フラグ
                csDataNewRow(ABDainoDataEntity.SAKUJOFG) = csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG)

                ' 更新カウンタ
                csDataNewRow(ABDainoDataEntity.KOSHINCOUNTER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER)

                csReturnDataset.Tables(ABDainoDataEntity.TABLE_NAME).Rows.Add(csDataNewRow)

            Next csDataRow
            csReturnDataset.AcceptChanges()

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        Return csReturnDataset

    End Function
#End Region

#Region "代納送付先累積履歴データカラム作成"

    '************************************************************************************************
    '* メソッド名      データカラム作成
    '* 
    '* 構文            Private Function CreateColumnsABSfskRirekiData() As DataTable
    '* 
    '* 機能　　        送付先履歴情報セッションのカラム定義を作成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataTable() 代納履歴情報テーブル
    '************************************************************************************************
    Private Function CreateColumnsABSfskRirekiData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsABSfskRirekiData"
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn
        Dim csDataPrimaryKey(8) As DataColumn               '主キー

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 送付先情報カラム定義
            csDataTable = New DataTable()
            csDataTable.TableName = ABSfskDataEntity.TABLE_NAME
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 15
            csDataPrimaryKey(0) = csDataColumn              '主キー①　住民コード
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(1) = csDataColumn              '主キー②　業務コード
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHORYAKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUNAISHUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(2) = csDataColumn              '主キー③　業務内種コード
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn.AllowDBNull = False
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn.AllowDBNull = False
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKDATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAiKANGAIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAIKANGAIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANAMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120        '60
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANJIMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480        '40
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKYUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKBANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200         '30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SAKUJOFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINCOUNTER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' 更新ユーザー
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINUSER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BIKO, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.CHECK, GetType(String))
            csDataColumn.DefaultValue = Boolean.FalseString
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_DAINOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOJUMINCD, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataPrimaryKey(3) = csDataColumn              '主キー④　代納住民コード
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SEIGYOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TOROKURENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataPrimaryKey(4) = csDataColumn              '主キー⑤　登録連番
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.RRKNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataPrimaryKey(5) = csDataColumn              '主キー⑥　履歴番号
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSONCD, GetType(String))
            csDataColumn.DefaultValue = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZACD, GetType(String))
            csDataColumn.DefaultValue = 7
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TODOFUKEN, GetType(String))
            csDataColumn.DefaultValue = 16
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSON, GetType(String))
            csDataColumn.DefaultValue = 48
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZA, GetType(String))
            csDataColumn.DefaultValue = 480
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHORINICHIJI, GetType(String))
            csDataColumn.DefaultValue = 17
            csDataPrimaryKey(6) = csDataColumn              '主キー⑦　処理日時
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.ZENGOKB, GetType(String))
            csDataColumn.DefaultValue = 1
            csDataPrimaryKey(7) = csDataColumn              '主キー⑧　前後区分

            csDataTable.PrimaryKey = csDataPrimaryKey       '主キー

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csDataTable

    End Function

    '************************************************************************************************
    '* メソッド名      データカラム作成
    '* 
    '* 構文            Private Function CreateColumnsABDainoRirekiData() As DataTable
    '* 
    '* 機能　　        代納履歴情報セッションのカラム定義を作成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataTable() 代納履歴情報テーブル
    '************************************************************************************************
    Private Function CreateColumnsABDainoRirekiData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsABDainoRirekiData"
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn
        Dim csDataPrimaryKey(8) As DataColumn               '主キー

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 代納情報カラム定義
            csDataTable = New DataTable
            csDataTable.TableName = ABDainoDataEntity.TABLE_NAME
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 15
            csDataPrimaryKey(0) = csDataColumn              '主キー①　住民コード
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(1) = csDataColumn              '主キー②　業務コード
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHORYAKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUNAISHUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(2) = csDataColumn              '主キー③　業務内種コード
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOJUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(3) = csDataColumn              '主キー④　代納住民コード
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBRYAKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINSHUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANASHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANJISHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANNAIKANGAIMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKIFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SAKUJOFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINCOUNTER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_HENSHUJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 160
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINUSER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MYNUMBER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ATENADATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BIKO, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHECK, GetType(String))
            csDataColumn.DefaultValue = Boolean.FalseString
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SEIGYOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SFSKZJUSHOCD, GetType(String))
            csDataColumn.DefaultValue = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TOROKURENBAN, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataPrimaryKey(4) = csDataColumn              '主キー⑤　登録連番
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RRKNO, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataPrimaryKey(5) = csDataColumn              '主キー⑥　履歴番号
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHIKUCHOSONCD, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MACHIAZACD, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TODOFUKEN, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHORINICHIJI, GetType(String))
            csDataColumn.DefaultValue = 17
            csDataPrimaryKey(6) = csDataColumn              '主キー⑦　処理日時
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ZENGOKB, GetType(String))
            csDataColumn.DefaultValue = 1
            csDataPrimaryKey(7) = csDataColumn              '主キー⑧　前後区分

            csDataTable.PrimaryKey = csDataPrimaryKey       '主キー

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csDataTable

    End Function
#End Region
    '*履歴番号 000003 2023/10/25 追加終了
#End Region

End Class
