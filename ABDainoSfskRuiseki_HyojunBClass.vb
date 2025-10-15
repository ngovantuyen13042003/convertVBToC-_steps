'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         ＡＢ代納送付先異動累積_標準マスタＤＡ
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2023/10/25
'*
'* 作成者           見城　啓四郎
'*
'* 著作権          （株）電算
'************************************************************************************************
'*  修正履歴　 履歴番号　　修正内容
'* 2024/06/10  000001     【AB-9902-1】不具合対応
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.Common
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* 代納送付先異動累積_標準マスタ取得、更新時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABDainoSfskRuiseki_HyojunBClass

#Region "メンバ変数"

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABDainoSfskRuiseki_HyojunBClass"     ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード
    Private Const ZENGOKB_ZEN As String = "1"                                       ' 前後区分　前
    Private Const ZENGOKB_GO As String = "2"                                        ' 前後区分　後
    Private Const SAKUJOFG_SAKUJO As String = "1"                                   ' 削除フラグ　削除
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    'パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                                              ' ログ出力クラス
    Private m_cfControlData As UFControlData                                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                                ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                              ' ＲＤＢクラス
    Private m_strInsertSQL As String                                                ' INSERT用SQL
    Private m_cfErrorClass As UFErrorClass                                          ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                                            ' 日付クラス
    Private m_csDataSchma As DataSet                                                ' スキーマ保管用データセット
    Private m_csDataSchmaHyojun As DataSet                                          ' スキーマ保管用データセット
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT用パラメータコレクション
    Private m_cUSSCityInfoClass As USSCityInfoClass                                 ' 市町村情報管理クラス

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
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
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(
            "SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, False)

        ' AB代納送付先累積_標準マスタのスキーマ取得
        m_csDataSchmaHyojun = m_cfRdbClass.GetTableSchemaNoRestriction(
            "SELECT * FROM " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"

#Region "代納送付先異動累積マスタ追加"
    '************************************************************************************************
    '* メソッド名     代納送付先異動累積_標準マスタ追加
    '* 
    '* 構文           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能           代納送付先異動累積_標準マスタにデータを追加
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoSfskB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            ' 追加件数
        Dim strUpdateDateTime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                    m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)  '作成日時

            '共通項目の編集を行う
            csDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' 端末ＩＤ
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                ' 更新カウンタ
            csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' 作成日時
            csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' 作成ユーザー
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = strUpdateDateTime           ' 更新日時
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                                                m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

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
    '* 機能           INSERTのSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim strInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim strInsertParam As StringBuilder                  'INSERT用パラメータ定義


        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME + " "
            strInsertColumn = New StringBuilder
            strInsertParam = New StringBuilder

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn.Append(csDataColumn.ColumnName)
                strInsertColumn.Append(", ")

                strInsertParam.Append(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER)
                strInsertParam.Append(csDataColumn.ColumnName)
                strInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' 最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL += "(" + strInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + strInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

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

#Region "代納送付先累積_標準データ作成"
    '************************************************************************************************
    '* メソッド名     代納送付先累積_標準データ作成
    '* 
    '* 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal strShoriNichiji As String) As Integer
    '* 
    '* 機能           代納送付先累積データを作成する
    '* 
    '* 引数           csDataRow As DataRow      : 代納送付先データ
    '*                strShoriKB As String      : 処理区分
    '*                strShoriNichiji As String : 処理日時
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String, ByVal strShoriNichiji As String) As Integer
        Dim intInsCnt As Integer
        Dim cSfskHyojunB As ABSfsk_HyojunBClass               '送付先ＤＡクラス
        Dim csSfskHyojun As DataSet                           '送付先ＤＡクラス

        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"

        Try

            ' 送付先_標準ＤＡクラスのインスタンス化
            cSfskHyojunB = New ABSfsk_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '送付先_標準の取得
            csSfskHyojun = cSfskHyojunB.GetSfskBHoshu(
                               csDataRow(ABSfskEntity.JUMINCD).ToString(),
                               csDataRow(ABSfskEntity.GYOMUCD).ToString(),
                               csDataRow(ABSfskEntity.GYOMUNAISHU_CD).ToString(),
                               csDataRow(ABSfskEntity.TOROKURENBAN).ToString())

            intInsCnt = CreateDainoSfskData(csDataRow, strShoriKB, csSfskHyojun.Tables(ABSfskHyojunEntity.TABLE_NAME).Rows(0), strShoriNichiji)

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

    '************************************************************************************************
    '* メソッド名     代納送付先累積_標準データ作成
    '* 
    '* 構文           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal csABSfskHyojunDataRow As DataRow, _
    '*                                                    ByVal strShoriNichiji As String) As Integer
    '* 
    '* 機能　　    　 代納送付先累積_標準データを作成する
    '* 
    '* 引数           csDataRow As DataRow              : 代納送付先データ
    '*                strShoriKB As String              : 処理区分
    '*                csABSfskHyojunDataRow As DataRow  : AB送付先_標準データ（DataRow形式）
    '*                strShoriNichiji As String         : 処理日時
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String,
                                        ByVal csABSfskHyojunDataRow As DataRow, ByVal strShoriNichiji As String) As Integer
        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"
        Dim csDataSet As DataSet
        Dim csDataSetHyojun As DataSet
        Dim csRuisekiDR As DataRow
        Dim csDataColumn As DataColumn
        'Dim strSystemDate As String                 ' システム日付
        Dim intInsCnt As Integer
        Dim csOriginalDR As DataRow
        Dim csOriginalHyojunDR As DataRow
        Dim csDainoSfskRuisekiHyojunDR As DataRow
        Dim intUpdataCount_zen As Integer
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        Dim cuCityInfo As New USSCityInfoClass()            '市町村情報管理クラス

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'strSystemDate = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' スキーマを取得
            csDataSet = m_csDataSchma.Clone
            csDataSetHyojun = m_csDataSchmaHyojun.Clone

            ' 更新用データのDataRowを作成
            csDainoSfskRuisekiHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

            '***
            '* 代納送付先累積_標準(前)編集処理
            '*

            If (strShoriKB <> ABConstClass.SFSK_ADD) Then

                ' 代納送付先累積データを作成
                csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow
                ' 代納送付先累積_標準データを作成
                csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

                ' 処理区分が追加以外の場合
                If (csDataRow.HasVersion(DataRowVersion.Original)) Then

                    ' 修正前情報が残っている場合、代納送付先累積データを作成
                    csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csDataRow.Table.Columns
                        If (Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB)) Then
                            csOriginalDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    ' 代納送付先累積_標準データを作成
                    csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csABSfskHyojunDataRow.Table.Columns
                        If (Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD1) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD2) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD3) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKKATAGAKICD)) Then

                            csOriginalHyojunDR(csDataColumn.ColumnName) = csABSfskHyojunDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    '(前)データのセット
                    csOriginalHyojunDR = SetDainoSfskRuisekiHyojunData(csOriginalDR, csOriginalHyojunDR, csDainoSfskRuisekiHyojunDR)

                    '共通項目のセット
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji                 '処理日時
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB                           '処理区分
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_ZEN                          '前後区分

                    '削除フラグの設定
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG, DataRowVersion.Original)

                    ' データセットに修正前情報を追加
                    csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csOriginalHyojunDR)

                    ' 代納送付先累積(前)マスタ追加処理
                    intUpdataCount_zen = Me.InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0))

                    ' 更新件数が１件以外の場合、エラーを発生させる
                    If (Not (intUpdataCount_zen = 1)) Then
                        m_cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        ' エラー定義を取得（既に同一データが存在します。：代納送付先累積）
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "代納送付先累積_標準", objErrorStruct.m_strErrorCode)
                    End If

                    ' データセットのクリア
                    csDataSetHyojun.Clear()
                Else

                End If
            Else

            End If

            '***
            '* 代納送付先累積_標準(後)編集処理　追加の場合もこちら
            '*
            ' 代納送付先累積_標準データを作成
            csRuisekiDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

            '共通項目のセット
            csRuisekiDR = SetDainoSfskRuisekiHyojunData(csDataRow, csABSfskHyojunDataRow, csDainoSfskRuisekiHyojunDR)

            ' データセット　　
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji            ' 処理日時
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB                      ' 処理区分
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_GO                      ' 前後区分
            '削除フラグ
            If (strShoriKB = ABConstClass.SFSK_DELETE) Then
                '削除の場合は"1"をセット
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_SAKUJO
            Else
                'それ以外の場合は送付先の値をそのままセット
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG)
            End If

            csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csRuisekiDR)

            '***
            '* 代納送付先累積_標準(後)マスタ追加処理
            '*
            intInsCnt = InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0))

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

    '************************************************************************************************
    '* メソッド名     代納送付先累積_標準データ編集処理
    '* 
    '* 構文           Private Function SetDainoSfskRuisekiHyojunData(ByVal csSfskDataRow As DataRow,
    '*                                                               ByVal csSfskHyojunDataRow As DataRow,
    '*                                                               ByVal csReturnDataRow As DataRow) As DataRow
    '* 
    '* 機能　　    　 代納送付先累積_標準データを編集する
    '* 
    '* 引数           csSfskDataRow As DataRow            : 送付先データ
    '*                csSfskHyojunDataRow As DataRow      : 送付先_標準データ
    '*                csReturnDataRow                     : 戻り値
    '* 
    '* 戻り値         DataRow : 編集したデータ
    '************************************************************************************************
    Private Function SetDainoSfskRuisekiHyojunData(ByVal csSfskDataRow As DataRow,
                                                   ByVal csSfskHyojunDataRow As DataRow,
                                                   ByVal csReturnDataRow As DataRow) As DataRow
        Const THIS_METHOD_NAME As String = "SetDainoSfskRuisekiHyojunData"

        '市町村情報管理クラスの設定
        m_cUSSCityInfoClass = New USSCityInfoClass
        m_cUSSCityInfoClass.GetCityInfo(m_cfControlData)

        Try
            '共通項目　※処理日時、処理区分、前後区分、削除フラグは呼出し元でセットする
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.JUMINCD) = csSfskDataRow(ABSfskEntity.JUMINCD)                                           '住民コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0)                                   '市町村コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0)                                '旧市町村コード

            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUCD) = csSfskDataRow(ABSfskEntity.GYOMUCD)                                           '業務コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD) = csSfskDataRow(ABSfskEntity.GYOMUNAISHU_CD)                             '業務内種別コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN) = csSfskDataRow(ABSfskEntity.TOROKURENBAN)                                 '登録連番
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.STYMD) = csSfskDataRow(ABSfskEntity.STYMD)                                               '開始年月日
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.EDYMD) = csSfskDataRow(ABSfskEntity.EDYMD)                                               '終了年月日
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RRKNO) = csSfskDataRow(ABSfskEntity.RRKNO)                                               '履歴番号

            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANAKATAGAKI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANAKATAGAKI)             '送付先方書フリガナ
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTSUSHO)                         '送付先氏名_通称
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANATSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANATSUSHO)                 '送付先氏名_通称_フリガナ
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIMEIYUSENKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)           '送付先氏名_優先区分
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKEIJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKEIJISHIMEI)                 '送付先氏名_外国人英字
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANJISHIMEI)               '送付先氏名_外国人漢字
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAMEI)           '送付先申請者名
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAKANKEICD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD) '送付先申請者関係コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSONCD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)           '送付先_市区町村コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZACD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZACD)                 '送付先_町字コード
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTODOFUKEN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTODOFUKEN)                   '送付先_都道府県
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSON) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSON)               '送付先_市区郡町村名
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZA) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZA)                     '送付先_町字
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKRENRAKUSAKIKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)           '連絡先区分
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKBN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKBN)                               '送付先区分
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTOROKUYMD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTOROKUYMD)                   '送付先登録年月日
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE1) = String.Empty                                                                 'リザーブ１
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE2) = String.Empty                                                                 'リザーブ２
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE3) = String.Empty                                                                 'リザーブ３
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE4) = String.Empty                                                                 'リザーブ４
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE5) = String.Empty                                                                 'リザーブ５
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                              ' 端末ＩＤ
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csSfskHyojunDataRow(ABSfskHyojunEntity.SAKUJOFG)                             ' 削除フラグ
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                                                            ' 更新カウンタ
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)           ' 作成日時
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                                               ' 作成ユーザー
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)            ' 更新日時
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                ' 更新ユーザー

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

        Return csReturnDataRow

    End Function

#End Region

#Region "代納送付先累積_標準データ抽出"
    '************************************************************************************************
    '* メソッド名     代納送付先累積_標準データ抽出
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
                                              ByVal strShoriKB As String) As DataTable

        Const THIS_METHOD_NAME As String = "GetABDainoSfskRuisekiData"
        Dim csDainoSfskRuisekiHyojunEntity As DataSet
        Dim csReturnDataRows As DataRow()
        Dim csReturnDatatable As DataTable
        Dim strSQL As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiHyojunEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban, strShoriKB, THIS_METHOD_NAME))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csDainoSfskRuisekiHyojunEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiHyojunEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiHyojunEntity,
                                                    ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '戻り値用にデータを格納
            csReturnDatatable = csDainoSfskRuisekiHyojunEntity.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME)

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

        Return csReturnDatatable

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
            strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiHyojunEntity.JUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.ZENGOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.STYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.EDYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RRKNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SFSKKBN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TANMATSUID)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUJOFG)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINUSER)

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
    '*                                             ByVal intTorokuRenban As Integer,
    '*                                             ByVal strShoriKB As String,
    '*                                             ByVal strMethodName As String) As String
    '* 
    '* 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数           strJuminCD             : 住民コード 
    '*                strGyomuCD             : 業務コード
    '*                strGyomuNaiShubetsuCD  : 業務内種別コード
    '*                strShoriKB             : 処理区分　"D"：代納、"S"：送付
    '*                strMethodName          : 呼出し元関数名
    '*
    '* 戻り値         String    :   WHERE句
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strGyomuCD As String,
                                 ByVal strGyomuNaiShubetsuCD As String,
                                 ByVal intTorokuRenban As Integer,
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

            '登録連番
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = intTorokuRenban
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '処理区分
            '送付
            strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE)

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

#End Region

End Class
