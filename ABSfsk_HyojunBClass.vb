'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        送付先_標準マスタＤＡ(ABSfsk_HyojunBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/10/20 早崎 雄矢
'*
'* 著作権          （株）電算 
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2024/6/11   000001    【AB-9901-1】不具合対応
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

Public Class ABSfsk_HyojunBClass
#Region "メンバ変数"

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABSfsk_HyojunBClass"
    Private Const THIS_BUSINESSID As String = "AB"                                  '業務コード
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"
    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Public m_blnBatch As Boolean = False                                            'バッチフラグ
    Private m_csDataSchma As DataSet                                                'スキーマ保管用データセット
    Private m_csDataSchma_Hyojun As DataSet                                         'スキーマ保管用データセット_標準版

    'メンバ変数の定義
    Private m_cfLogClass As UFLogClass                                              ' ログ出力クラス
    Private m_cfControlData As UFControlData                                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                                ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                              ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                                          ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                                            ' 日付クラス
    Private m_strInsertSQL As String                                                ' INSERT用SQL
    Private m_strUpdateSQL As String                                                ' UPDATE用SQL
    Private m_strDeleteSQL As String                                                ' DELETE用SQL（物理）
    Private m_strDelRonriSQL As String                                              ' DELETE用SQL（論理）
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      ' DELETE用パラメータコレクション（物理）
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' DELETE用パラメータコレクション（論理）

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData,
    '*                                ByVal cfConfigDataClass As UFConfigDataClass,
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '*                 cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
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
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　 送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet

        Return GetSfskBHoshu(strJuminCD, String.Empty, String.Empty, String.Empty, False)
    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　 送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String    :住民コード
    '*                blnSakujoFG As Boolean  :削除フラグ
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet

        Return GetSfskBHoshu(strJuminCD, String.Empty, String.Empty, String.Empty, blnSakujoFG)

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
    '*                                                          ByVal strGyomuCD As String, _
    '*                                                          ByVal strGyomunaiShuCD As String, _
    '*                                                          ByVal strTorokurenban As String) As DataSet
    '* 
    '* 機能           送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String        :住民コード
    '*                strGyomuCD As String        :業務コード
    '*                strGyomunaiShuCD As String  :業務内種別コード
    '*                strTorokurenban As String   :登録連番
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomunaiShuCD As String,
                                              ByVal strTorokurenban As String) As DataSet

        Return GetSfskBHoshu(strJuminCD, strGyomuCD, strGyomunaiShuCD, strTorokurenban, True)

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
    '*                                                          ByVal strGyomuCD As String, _
    '*                                                          ByVal strGyomunaiShuCD As String, _
    '*                                                          ByVal strTorokurenban As String, _
    '*                                                          ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　 送付先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String        :住民コード
    '*                strGyomuCD As String        :業務コード
    '*                strGyomunaiShuCD As String  :業務内種別コード
    '*                strTorokurenban As String   :登録連番
    '*                blnSakujoFG As Boolean      :削除フラグ
    '* 
    '* 戻り値         取得した送付先マスタの該当データ（DataSet）
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomunaiShuCD As String,
                                              ByVal strTorokurenban As String,
                                              ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"            'このメソッド名
        Dim csSfskEntity As DataSet                                     '送付先マスタデータ
        Dim strSQL As String                                            'SQL文文字列
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean                                        '削除データ読み込み

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()

            'SQL,パラメータコレクションの作成
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiShuCD, True, strTorokurenban, blnSakujo, cfUFParameterCollectionClass)

            ' RDBアクセスログ出力
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If

            'SQLの実行 DataSetの取得
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskHyojunEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            m_cfRdbClass.p_intMaxRows = intWkKensu

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '************************************************************************************************
    '* メソッド名     送付先_標準データ作成
    '* 
    '* 構文           Public Function CreateSfskHyojunData(ByVal csDataRow As DataRow, ByVal csSfskEntity As DataSet) As DataRow
    '*                                      
    '* 
    '* 機能　　    　 送付先_標準データを作成する
    '* 
    '* 引数           csDataRow As DataRow      : 送付先データ
    '*                csSfskEntity As DataSet   : 送付先エンティティ
    '* 
    '* 戻り値         DataRow
    '************************************************************************************************
    Public Function CreateSfskHyojunData(ByVal csDataRow As DataRow, ByVal csSfskEntity As DataSet) As DataRow
        Const THIS_METHOD_NAME As String = "CreateSfskHyojunData"
        Dim csSfskHyojunRows() As DataRow
        Dim csSfskHyojunRow As DataRow
        Dim csDataColumn As DataColumn
        Dim csDataHyojunColumn As DataColumn
        Dim strSelect As StringBuilder                                         ' 抽出SQL

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '送付先_標準のDateRowを作成
            csSfskHyojunRow = csSfskEntity.Tables(ABSfskHyojunEntity.TABLE_NAME).NewRow

            'レコードの特定
            strSelect = New StringBuilder()
            strSelect.Append(ABSfskHyojunEntity.GYOMUCD)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.GYOMUCD), String))
            strSelect.Append("' AND ")

            strSelect.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String))
            strSelect.Append("' AND ")

            strSelect.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.TOROKURENBAN), String))
            strSelect.Append("'")

            csSfskHyojunRows = csSfskEntity.Tables(ABSfskHyojunEntity.TABLE_NAME).Select(strSelect.ToString)
            csSfskHyojunRow = csSfskHyojunRows(0)

            '送付先のデータを送付先_標準に変換
            For Each csDataHyojunColumn In csSfskHyojunRow.Table.Columns
                For Each csDataColumn In csDataRow.Table.Columns
                    If Not (csDataColumn.ColumnName = ABSfskEntity.KOSHINCOUNTER) Then
                        'カラム名が一致するデータを代入
                        If (csDataColumn.ColumnName = csDataHyojunColumn.ColumnName) Then

                            csSfskHyojunRow(csDataHyojunColumn.ColumnName) = csDataRow(csDataColumn.ColumnName)

                            Exit For

                        End If
                    End If
                Next csDataColumn
            Next csDataHyojunColumn

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

        Return csSfskHyojunRow
    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ追加
    '* 
    '* 構文           Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 送付先マスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertSfskB"                'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '追加件数
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                    m_cfInsertUFParameterCollectionClass Is Nothing) Then

                Call CreateInsertSQL(csDataRow)

            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)          '作成日時

            ' 個別項目編集を行う
            csDataRow(ABSfskHyojunEntity.SFSKTOROKUYMD) = Left(strUpdateDateTime, 8)         '送付先登録年月日

            ' 共通項目の編集を行う
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId  '端末ＩＤ
            csDataRow(ABSfskHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                     '削除フラグ
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                '更新カウンタ
            csDataRow(ABSfskHyojunEntity.SAKUSEINICHIJI) = strUpdateDateTime          '作成日時
            csDataRow(ABSfskHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   '作成ユーザー
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = strUpdateDateTime           '更新日時
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId    '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value =
                    csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ更新
    '* 
    '* 構文           Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 送付先マスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateSfskB"                'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim intUpdCnt As Integer                                        '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse
                    m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '端末ＩＤ
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)    '更新日時
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength),
                                     csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength),
                                               DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ削除（論理）
    '* 
    '* 構文           Public Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 送付先マスタのデータを削除（論理）する。
    '* 
    '* 引数           csDataRow As DataRow  :削除データ
    '* 
    '* 戻り値         削除（論理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB（論理）"        'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim intDelCnt As Integer                                        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '端末ＩＤ
            csDataRow(ABSfskHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                                                      '削除フラグ
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)    '更新日時
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")


            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     送付先マスタ削除（物理）
    '* 
    '* 構文           Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　 送付先マスタのデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '*                strSakujoKB As String     :削除フラグ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB（物理）"
        Const SAKUJOKB_D As String = "D"                    '削除区分
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intDelCnt As Integer                            '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 引数の削除区分をチェック
            If (strSakujoKB <> SAKUJOKB_D) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing OrElse m_strDeleteSQL = String.Empty OrElse
                    m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

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
        Dim cfUFParameterClass As UFParameterClass                 'パラメータクラス
        Dim strInsertColumn As StringBuilder                       '追加SQL文項目文字列
        Dim strInsertParam As StringBuilder                        '追加SQL文パラメータ文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABSfskHyojunEntity.TABLE_NAME + " "
            strInsertColumn = New StringBuilder
            strInsertParam = New StringBuilder

            'INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            'パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn.Append(csDataColumn.ColumnName)
                strInsertColumn.Append(", ")
                strInsertParam.Append(ABSfskHyojunEntity.PARAM_PLACEHOLDER)
                strInsertParam.Append(csDataColumn.ColumnName)
                strInsertParam.Append(", ")

                'INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            'INSERT SQL文のトリミング
            m_strInsertSQL += "(" + strInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + strInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     Update用SQL文の作成
    '* 
    '* 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_KOSHINCOUNTER)

            'UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABSfskHyojunEntity.TABLE_NAME + " SET "

            'UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            'パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・作成日時・作成ユーザは更新しない
                If (Not (csDataColumn.ColumnName = ABSfskHyojunEntity.JUMINCD) AndAlso
                        Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SAKUSEIUSER) AndAlso
                        Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SAKUSEINICHIJI)) Then
                    cfUFParameterClass = New UFParameterClass

                    'SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    'UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            'UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            'UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += strWhere.ToString

            'UPDATE コレクションにキー情報を追加
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     論理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteRonriSQL"
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strDelRonriSQL As New StringBuilder                     '論理削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)

            'DELETE（論理） SQL文の作成
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINCOUNTER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.RRKNO)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_RRKNO)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            'DELETE（論理） パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE（論理） コレクションにパラメータを追加
            '端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '削除フラグ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '履歴番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     物理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strDeleteSQL As New StringBuilder                       '物理削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)

            'DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABSfskHyojunEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            'DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE(物理) コレクションにキー情報を追加
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
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
    '* 機能　　       送付先_標準マスタのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       'このメソッド名
        Dim objErrorStruct As UFErrorStruct                         'エラー定義構造体

        Try

            ' 日付クラスのインスタンス化
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABSfskHyojunEntity.JUMINCD                               ' 住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.GYOMUCD                               ' 業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.GYOMUNAISHU_CD                        ' 業務内種別コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.TOROKURENBAN                          ' 登録連番
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TOROKURENBAN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.STYMD                                  ' 開始年月日
                    If (Not (strValue = String.Empty OrElse strValue = "00000000")) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_STYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskHyojunEntity.EDYMD                                   ' 終了年月日
                    If (Not (strValue = String.Empty OrElse strValue = "00000000" OrElse strValue = "99999999")) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_EDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskHyojunEntity.RRKNO                                   ' 履歴番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_RRKNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANAKATAGAKI                        ' 送付先方書フリガナ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANAKATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTSUSHO                              ' 送付先氏名_通称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTSUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANATSUSHO                           ' 送付先氏名_通称_フリガナ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANATSUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIMEIYUSENKB                         ' 送付先氏名_優先区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIMEIYUSENKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKEIJISHIMEI                            ' 送付先氏名_外国人英字
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKEIJISHIMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANJISHIMEI                           ' 送付先氏名_外国人漢字
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANJISHIMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHINSEISHAMEI                          ' 送付先申請者名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD                     ' 送付先申請者関係コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAKANKEICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIKUCHOSONCD                          ' 送付先_市区町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKMACHIAZACD                             ' 送付先_町字コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTODOFUKEN                               ' 送付先_都道府県
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTODOFUKEN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIKUCHOSON                             ' 送付先_市区郡町村名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSON)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKMACHIAZA                                ' 送付先_町字
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZA)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD1                                ' 送付先番地コード１
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD2                                 ' 送付先番地コード２
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD3                                 ' 送付先番地コード３
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKATAGAKICD                                ' 送付先方書コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKATAGAKICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKRENRAKUSAKIKB                             ' 連絡先区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKRENRAKUSAKIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKBN                                       ' 送付先区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKBN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTOROKUYMD                                  ' 送付先登録年月日
                    If (Not strValue = String.Empty) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTOROKUYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskEntity.RESERVE                                               ' リザーブ
                    '何もしない
                Case ABSfskEntity.TANMATSUID                                            ' 端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUJOFG                                        ' 削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINCOUNTER                                   ' 更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUSEINICHIJI                                  ' 作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUSEIUSER                                     ' 作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINNICHIJI                                   ' 更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINUSER                                      ' 更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     ＳＱＬ文・パラメータコレクション作成
    '* 
    '* 構文           Private Function CreateSql_Param(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strTorokurenban As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* 機能　　    　　ＳＱＬ文及びパラメータコレクションを作成し引き渡す。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                blnGyomunaiSHUCD As Boolean   :業務内種別コードの有無（True:有り,False:無し）
    '*                strTorokurenban As String     :登録番号
    '*                blnSakujoFG As Boolean        :削除データの有無(True:有り,False:無し)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :パラメータコレクションクラス
    '* 
    '* 戻り値         ＳＱＬ文(String)
    '*                パラメータコレクションクラス(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strTorokurenban As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param"            'このメソッド名
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskHyojunEntity.TABLE_NAME)

            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)                 '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)

            '業務コード
            If (Not (strGyomuCD = String.Empty)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUCD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            End If

            If (Not (strTorokurenban = String.Empty)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.TOROKURENBAN)
                strSQL.Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_TOROKURENBAN)
            End If

            If (Not (blnSakujoFG)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)            '削除フラグ
                strSQL.Append(" <> ")
                strSQL.Append(SAKUJOFG_ON)
            End If

            'ソート
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABSfskEntity.GYOMUCD)
            strSQL.Append(" DESC,")
            strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strSQL.Append(" DESC")

            '検索条件のパラメータを作成
            '住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            If (blnGyomunaiSHUCD) Then
                cfUFParameterClass.Value = strGyomunaiSHUCD
            Else
                cfUFParameterClass.Value = String.Empty
            End If
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 登録連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokurenban

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return strSQL.ToString

    End Function

#End Region

End Class
