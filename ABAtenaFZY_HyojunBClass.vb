'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名付随_標準マスタＤＡ(ABAtenaFZY_HyojunBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/08/14 早崎  雄矢
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
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* 宛名付随_標準マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaFZY_HyojunBClass
#Region "メンバ変数"
    ' パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                                              ' ログ出力クラス
    Private m_cfControlData As UFControlData                                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                                ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                              ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                                          ' エラー処理クラス
    Private m_strInsertSQL As String                                                ' INSERT用SQL
    Private m_strUpdateSQL As String                                                ' UPDATE用SQL
    Private m_strDelRonriSQL As String                                              ' 論理削除用SQL
    Private m_strDelButuriSQL As String                                             ' 物理削除用SQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' 論理削除用パラメータコレクション
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   ' 物理削除用パラメータコレクション
    Private m_csDataSchma As DataSet                                                ' スキーマ保管用データセット
    Private m_strUpdateDatetime As String                                           ' 更新日時

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaFZY_HyojunBClass"             ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード
    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"
    Private Const ERR_JUMINCD As String = "住民コード"

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

        ' パラメータのメンバ変数
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     宛名付随_標準マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaFZYHyojunBHoshu(ByVal strJuminCD As String, _
    '*                                                        ByVal strJuminJutogaiKB As String, _
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　 宛名付随_標準マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD         : 住民コード 
    '*                strJuminJutogaiKB  : 住民住登外区分
    '*                blnSakujoFG        : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
    '************************************************************************************************
    Public Function GetAtenaFZYHyojunBHoshu(ByVal strJuminCD As String, _
                                            ByVal strJuminJutogaiKB As String, _
                                            ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetAtenaFZYHyojunBHoshu"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 住民コードが指定されていないときエラー
            If (IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0)) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABAtenaFZYHyojunEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaFZYHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strJuminJutogaiKB, blnSakujoFG))

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _ 
            '                                strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, _
                                                    ABAtenaFZYHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csAtenaEntity

    End Function

    '************************************************************************************************
    '* メソッド名     SELECT句の作成
    '* 
    '* 構文           Private Sub CreateSelect() As String
    '* 
    '* 機能　　    　 SELECT句を生成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String    :   SELECT句
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim csSELECT As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の作成
            csSELECT.AppendFormat("SELECT {0}", ABAtenaFZYHyojunEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaFZYHyojunEntity.KOSHINUSER)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csSELECT.ToString

    End Function

    '************************************************************************************************
    '* メソッド名     WHERE文の作成
    '* 
    '* 構文           Private Function CreateWhere(ByVal strJuminCD As String, _
    '                                              ByVal strJuminJutogaiKB As String, _
    '                                              ByVal blnSakujoFG As Boolean) As String
    '* 
    '* 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数           strJuminCD         : 住民コード 
    '*                strJuminJutogaiKB  : 住民住登外区分
    '*                blnSakujoFG        : 削除フラグ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String, _
                                 ByVal strJuminJutogaiKB As String, _
                                 ByVal blnSakujoFG As Boolean)  As String

        Const THIS_METHOD_NAME As String = "CreateWhere"
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECTパラメータコレクションクラスのインスタンス化
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE句の作成
            csWHERE = New StringBuilder(256)

            ' 住民コード
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaFZYHyojunEntity.JUMINCD, ABAtenaFZYHyojunEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 住民住登外区分
            If (Not strJuminJutogaiKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB, ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKB
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 削除フラグ
            If (blnSakujoFG = False) Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaFZYHyojunEntity.SAKUJOFG, SAKUJOFG_ON)
            Else
                '処理なし
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csWHERE.ToString

    End Function

    #Region "宛名付随_標準マスタ追加"
    '************************************************************************************************
    '* メソッド名     宛名付随_標準マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaFZYHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名付随_標準マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaFZYHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaFZYHyojunB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            '追加件数

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strInsertSQL Is Nothing) OrElse (m_strInsertSQL = String.Empty) _ 
                OrElse (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '処理なし
            End If

            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            '共通項目の編集を行う
            csDataRow(ABAtenaFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     '端末ＩＤ
            csDataRow(ABAtenaFZYHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        '削除フラグ
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '更新カウンタ
            csDataRow(ABAtenaFZYHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '作成ユーザー
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '更新ユーザー

            '作成日時、更新日時の設定
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYHyojunEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYHyojunEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring( _
                    ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
            '                            m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intInsCnt

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
        Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義
        Dim cfUFParameterClass As UFParameterClass
        Dim strParamName As String

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass
                strParamName = String.Format("{0}{1}", ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABAtenaFZYHyojunEntity.TABLE_NAME,
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray),
                                           csInsertParam.ToString.TrimEnd(",".ToCharArray))

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

    End Sub
    #End Region

    #Region "宛名付随_標準マスタ更新"
    '************************************************************************************************
    '* メソッド名     宛名付随_標準マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaFZYHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名付随_標準マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateAtenaFZYHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaFZYHyojunB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intUpdCnt As Integer                            '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strUpdateSQL Is Nothing) OrElse (m_strUpdateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateUpdateSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                      '端末ＩＤ
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                        '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength) = ABAtenaFZYHyojunEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    'キー項目以外は編集内容取得
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring( _
                              ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
            '                                m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intUpdCnt

    End Function

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE定義
        Dim csUpdateParam As StringBuilder                  'UPDATE用SQL定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABAtenaFZYHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・住民住登外区分・作成日時・作成ユーザは更新しない
                If (Not (csDataColumn.ColumnName = ABAtenaFZYHyojunEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaFZYHyojunEntity.SAKUSEIUSER) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaFZYHyojunEntity.SAKUSEINICHIJI)) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    '処理なし
                End If

            Next csDataColumn

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(",".ToCharArray())

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += csWhere.ToString

            ' UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

    End Sub
#End Region

#Region "宛名付随_標準マスタ削除"
    '************************************************************************************************
    '* メソッド名     宛名付随_標準マスタ削除
    '* 
    '* 構文           Public Function DeleteAtenaFZYHyojun(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名付随_標準マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Function DeleteAtenaFZYHyojun(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYHyojun"
        Dim cfParam As UFParameterClass  'パラメータクラス
        Dim intDelCnt As Integer        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strDelRonriSQL Is Nothing) OrElse (m_strDelRonriSQL = String.Empty) _
                    OrElse (m_cfDelRonriUFParameterCollectionClass Is Nothing)) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                      '端末ＩＤ
            csDataRow(ABAtenaFZYHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                                                          '削除フラグ
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                        '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength) = ABAtenaFZYHyojunEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    'キー項目以外は編集内容を設定
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring( _
                            ABAtenaFZYHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
            '                                m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     宛名付随_標準マスタ物理削除
    '* 
    '* 構文           Public Function DeleteAtenaFZYHyojunB(ByVal csDataRow As DataRow, _
    '*                                                      ByVal strSakujoKB As String) As Integer
    '*
    '* 機能　　    　 宛名付随_標準マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow  : 削除するデータの含まれるDataRowオブジェクト
    '*                strSakujoKB As String : 削除フラグ
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYHyojunB(ByVal csDataRow As DataRow, _
                                                    ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYHyojunB"
        Dim cfErrorStruct As UFErrorStruct 'エラー定義構造体
        Dim cfParam As UFParameterClass     'パラメータクラス
        Dim intDelCnt As Integer            '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除区分のチェックを行う
            If (Not (strSakujoKB = "D")) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                cfErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                '例外を生成
                Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If ((m_strDelButuriSQL Is Nothing) OrElse (m_strDelButuriSQL = String.Empty) _
                OrElse (IsNothing(m_cfDelButuriUFParameterCollectionClass))) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '処理なし
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength) = ABAtenaFZYHyojunEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring( _
                                  ABAtenaFZYHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                    'キー項目以外の取得なし
                Else
                    '処理なし
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
            '                                 m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intDelCnt

    End Function

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE定義
        Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER)

            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaFZYHyojunEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where文の追加
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaFZYHyojunEntity.TABLE_NAME + csWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

    End Sub
#End Region

#Region "更新日時設定"
    '************************************************************************************************
    '* メソッド名     更新日時設定
    '* 
    '* 構文           Private Sub SetUpdateDatetime(ByRef csDate As Object)
    '* 
    '* 機能           未設定のとき更新日時を設定する
    '* 
    '* 引数           csDate As Object : 更新日時の項目
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetUpdateDatetime(ByRef csDate As Object)
        Try
            '未設定のとき
            If ((IsDBNull(csDate)) OrElse (CType(csDate, String).Trim.Equals(String.Empty))) Then
                csDate = m_strUpdateDatetime
            Else
                '処理なし
            End If
        Catch
            Throw
        End Try
    End Sub
#End Region

#End Region

End Class
