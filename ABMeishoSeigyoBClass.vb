'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         表示名称制御マスタＤＡ(ABMeishoSeigyoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2011/04/13
'*
'* 作成者　　　     小池 可那子
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
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
Imports System.Text

Public Class ABMeishoSeigyoBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                                                'INSERT用SQL
    Private m_strUpdateSQL As String                                                'UPDATE用SQL
    Private m_strDeleteSQL As String                                                'DELETE用SQL（物理）
    Private m_strDelRonriSQL As String                                              'DELETE用SQL（論理）
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE用パラメータコレクション（物理）
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    'DELETE用パラメータコレクション（論理）
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABMeishoSeigyoBClass"
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

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        'メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

        ' SQL文の作成
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABMeishoSeigyoEntity.TABLE_NAME, ABMeishoSeigyoEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "表示名称制御データ抽出"
    '************************************************************************************************
    '* メソッド名     表示名称制御データ抽出
    '* 
    '* 構文           Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　 引数の条件で表示名称制御マスタを抽出する
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した表示名称制御マスタの該当データ（DataSet）
    '*                   構造：csMeishoSeigyoEntity    インテリセンス：ABMeishoSeigyoEntity
    '************************************************************************************************
    Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetMeishoSeigyo"            'このメソッド名
        Dim csMeishoSeigyoEntity As DataSet = Nothing                   '表示名称制御マスタデータ
        Dim strSQL As StringBuilder = Nothing                           'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ストリングビルダーのインスタンス化
            strSQL = New StringBuilder

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABMeishoSeigyoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABMeishoSeigyoEntity.KEY_JUMINCD)
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABMeishoSeigyoEntity.SHITEICD)
            strSQL.Append(" ASC")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csMeishoSeigyoEntity = m_csDataSchma.Clone()
            csMeishoSeigyoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csMeishoSeigyoEntity, _
                                                           ABMeishoSeigyoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csMeishoSeigyoEntity

    End Function
#End Region

#Region "表示名称制御データ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)"
    '************************************************************************************************
    '* メソッド名     表示名称制御データ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String, _
    '*                                                          ByVal strGroupID As String) As DataSet 
    '*                                                        
    '* 機能　　    　 引数の条件で表示名称制御マスタを抽出する(オーバーロード処理）
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '*                strGroupID As String  :グループＩＤ(指定コード）
    '* 
    '* 戻り値         取得した表示名称制御マスタの該当データ（DataSet）
    '*                   構造：csMeishoSeigyoEntity    インテリセンス：ABMeishoSeigyoEntity
    '************************************************************************************************
    Public Overloads Function GetMeishoSeigyo(ByVal strJuminCD As String, ByVal strGroupID As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetMeishoSeigyo"            'このメソッド名
        Dim csMeishoSeigyoEntity As DataSet = Nothing                   '表示名称制御マスタデータ
        Dim strSQL As StringBuilder = Nothing                           'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ストリングビルダーのインスタンス化
            strSQL = New StringBuilder

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABMeishoSeigyoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABMeishoSeigyoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABMeishoSeigyoEntity.SHITEICD)
            strSQL.Append(" = ")
            strSQL.Append(ABMeishoSeigyoEntity.KEY_SHITEICD)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' グループＩＤ(指定コード）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD
            cfUFParameterClass.Value = strGroupID
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csMeishoSeigyoEntity = m_csDataSchma.Clone()
            csMeishoSeigyoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csMeishoSeigyoEntity, _
                                                           ABMeishoSeigyoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csMeishoSeigyoEntity

    End Function
#End Region

#Region "表示名称制御マスタ追加"
    '************************************************************************************************
    '* メソッド名     表示名称制御マスタ追加
    '* 
    '* 構文           Public Function InsertMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 引数のデータを表示名称制御マスタに追加する
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertMeishoSeigyo"         'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '追加件数
        Dim strUpdateDateTime As String = String.Empty

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strInsertSQL Is Nothing) OrElse (m_strInsertSQL.Trim = String.Empty) OrElse _
                (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")           '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId               '端末ＩＤ
            csDataRow(ABMeishoSeigyoEntity.SAKUJOFG) = "0"                                           '削除フラグ
            csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = Decimal.Zero                             '更新カウンタ
            csDataRow(ABMeishoSeigyoEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '作成日時
            csDataRow(ABMeishoSeigyoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '作成ユーザー
            csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = strUpdateDateTime                        '更新日時
            csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value =
                    csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return intInsCnt

    End Function
#End Region

#Region "表示名称制御マスタ更新"
    '************************************************************************************************
    '* メソッド名     表示名称制御マスタ更新
    '* 
    '* 構文           Public Function UpdateMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 引数のデータを表示名称制御マスタに更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateMeishoSeigyo"         'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim intUpdCnt As Integer                                        '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strUpdateSQL Is Nothing) OrElse (m_strUpdateSQL.Trim = String.Empty) OrElse _
                (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER)) + 1      '更新カウンタ
            csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) = ABMeishoSeigyoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength),
                                     csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength),
                                     DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength),
                                                                                                  DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return intUpdCnt
    End Function
#End Region

#Region "表示名称制御マスタ削除(論理）"
    '************************************************************************************************
    '* メソッド名     表示名称制御マスタ削除（論理）
    '* 
    '* 構文           Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 引数のデータを表示名称制御マスタから削除（論理）する。
    '* 
    '* 引数           csDataRow As DataRow  :削除データ
    '* 
    '* 戻り値         削除（論理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteMeishoSeigyo（論理）"  'このメソッド名
        Dim cfParam As UFParameterClass                                  'パラメータクラス
        Dim intDelCnt As Integer                                         '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strDelRonriSQL Is Nothing) OrElse (m_strDelRonriSQL.Trim = String.Empty) OrElse _
                (m_cfDelRonriUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABMeishoSeigyoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                 '端末ＩＤ
            csDataRow(ABMeishoSeigyoEntity.SAKUJOFG) = 1                                                               '削除フラグ
            csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABMeishoSeigyoEntity.KOSHINCOUNTER)) + 1    '更新カウンタ
            csDataRow(ABMeishoSeigyoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff") '更新日時
            csDataRow(ABMeishoSeigyoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                   '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) = ABMeishoSeigyoEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                    = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PARAM_PLACEHOLDER.RLength),
                                                                                                    DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return intDelCnt

    End Function
#End Region

#Region "表示名称制御マスタ削除（物理）"
    '************************************************************************************************
    '* メソッド名     表示名称制御マスタ削除（物理）
    '* 
    '* 構文           Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　  引数のデータを表示名称制御マスタから削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '*                strSakujoKB As String     :削除フラグ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteMeishoSeigyo(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteMeishoSeigyo（物理）"  'このメソッド名
        Dim objErrorStruct As UFErrorStruct                              'エラー定義構造体
        Dim cfParam As UFParameterClass                                  'パラメータクラス
        Dim intDelCnt As Integer                                         '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 引数の削除区分をチェック
            If (strSakujoKB.Trim <> "D") Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQLが作成されていなければ作成
            If ((m_strDeleteSQL Is Nothing) OrElse (m_strDeleteSQL.Trim = String.Empty) OrElse _
                (m_cfDeleteUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABMeishoSeigyoEntity.PREFIX_KEY.RLength) = ABMeishoSeigyoEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value _
                    = csDataRow(cfParam.ParameterName.RSubstring(ABMeishoSeigyoEntity.PREFIX_KEY.RLength),
                                DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return intDelCnt

    End Function
#End Region

#Region "SQL文作成"
    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"              'このメソッド名
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strInsertColumn As String = String.Empty                '追加SQL文項目文字列
        Dim strInsertParam As String = String.Empty                 '追加SQL文パラメータ文字列
        Dim strDelRonriSQL As StringBuilder = Nothing               '論理削除SQL文文字列
        Dim strDeleteSQL As StringBuilder = Nothing                 '物理削除SQL文文字列
        Dim strWhere As StringBuilder = Nothing                     '更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ストリングビルダーのインスタンス化
            strDelRonriSQL = New StringBuilder
            strDeleteSQL = New StringBuilder
            strWhere = New StringBuilder

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABMeishoSeigyoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' 更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABMeishoSeigyoEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABMeishoSeigyoEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABMeishoSeigyoEntity.SHITEICD)
            strWhere.Append(" = ")
            strWhere.Append(ABMeishoSeigyoEntity.KEY_SHITEICD)
            strWhere.Append(" AND ")
            strWhere.Append(ABMeishoSeigyoEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABMeishoSeigyoEntity.TABLE_NAME + " SET "

            ' DELETE（論理） SQL文の作成
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINCOUNTER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABMeishoSeigyoEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            ' DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABMeishoSeigyoEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（論理） パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += strWhere.ToString

            ' UPDATE,DELETE(物理) コレクションにキー情報を追加
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 指定コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' DELETE（論理） コレクションにパラメータを追加
            ' 端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 削除フラグ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 指定コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_SHITEICD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABMeishoSeigyoEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try
    End Sub
#End Region

#Region "データ整合性チェック"
    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       表示名称制御マスタのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String   :項目名称
    '*                strValue As String        :値
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       'このメソッド名
        Dim objErrorStruct As UFErrorStruct                         'エラー定義構造体

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABMeishoSeigyoEntity.JUMINCD                        '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.SHICHOSONCD                    '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.KYUSHICHOSONCD                 '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.SHITEICD                       '指定コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SHITEICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.RIYOFG                         '利用名フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_RIYOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    'TODO:【リサーブ1～リサーブ5】現時点(2011/04/15)では対応しないが、住基法改正時に対応が必要。
                Case ABMeishoSeigyoEntity.RESERVE1                       'リサーブ1
                    '何もしない
                Case ABMeishoSeigyoEntity.RESERVE2                       'リサーブ2
                    '何もしない
                Case ABMeishoSeigyoEntity.RESERVE3                       'リサーブ3
                    '何もしない
                Case ABMeishoSeigyoEntity.RESERVE4                       'リサーブ4
                    '何もしない
                Case ABMeishoSeigyoEntity.RESERVE5                       'リサーブ5
                    '何もしない

                Case ABMeishoSeigyoEntity.TANMATSUID                     '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.SAKUJOFG                       '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.KOSHINCOUNTER                  '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.SAKUSEINICHIJI                 '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.SAKUSEIUSER                    '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.KOSHINNICHIJI                  '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABMeishoSeigyoEntity.KOSHINUSER                     '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABMEISHOSEIGYOB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        End Try
    End Sub
#End Region

End Class
