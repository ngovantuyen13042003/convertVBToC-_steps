'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ代納マスタＤＡ(ABDainoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/06　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/02/25 000001     抽出条件から業務内種別コードをはずすとあるが、業務内種別コードを String.Emptyとして取得する
'* 2003/03/27 000002     エラー処理クラスの参照先を"AB"固定にする
'* 2003/04/21 000003     整合性チェック変更(業務内種別・開始年月・終了年月)
'* 2003/05/06 000004     整合性チェック変更
'* 2003/05/20 000005     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000006     RDBアクセスログの修正
'* 2003/09/11 000007     端末ＩＤ整合性チェックをANKにする
'* 2003/10/09 000008     作成ユーザー・更新ユーザーチェックの変更
'* 2004/08/27 000009     速度改善：（宮沢）
'* 2005/01/25 000010     速度改善２：（宮沢）
'* 2005/06/16 000011     SQL文をInsert,Update,Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
'* 2006/12/22 000012     本店情報取得メソッドを追加。
'* 2007/03/09 000013     代納情報取得SQLのソート順を変更(高原)
'* 2010/03/05 000014     代納マスタ抽出処理のオーバーロードを追加（比嘉）
'* 2010/04/16 000015     VS2008対応（比嘉）
'* 2023/03/10 000016     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
'* 2023/04/20 000017     【AB-0970-1】宛名GET取得項目標準化対応_暫定対応（仲西）
'* 2023/10/19 000018     【AB-0840-1】送付先管理項目追加対応（見城）
'* 2023/12/05 000019     【AB-0840-1】送付先管理項目追加対応_追加修正（仲西）
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
'* 代納マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABDainoBClass
#Region "メンバ変数"
    ' パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDelRonriSQL As String                      ' 論理削除用SQL
    Private m_strDelButuriSQL As String                     ' 物理削除用SQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '論理削除用パラメータコレクション
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '物理削除用パラメータコレクション
    Private m_cfParameterCollectionClass As UFParameterCollectionClass            '読込用パラメータコレクション
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABDainoBClass"                       ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード

    '* 履歴番号 000009 2004/08/27 追加開始（宮沢）
    Public m_blnBatch As Boolean = False               'バッチフラグ
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット
    '* 履歴番号 000009 2004/08/27 追加終了
    '* 履歴番号 000018 2023/10/19 修正開始
    Private Const ALL0_YMD As String = "00000000"            ' 年月日オール０
    Private Const ALL9_YMD As String = "99999999"            ' 年月日オール９
    '* 履歴番号 000018 2023/10/19 修正終了

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
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' パラメータのメンバ変数初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_strDelButuriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        m_cfParameterCollectionClass = Nothing
        '* 履歴番号 000009 2004/08/27 追加開始（宮沢）
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoEntity.TABLE_NAME, ABDainoEntity.TABLE_NAME, False)
        '* 履歴番号 000009 2004/08/27 追加終了
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     代納マスタ抽出
    '* 
    '* 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetDainoBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     代納マスタ抽出
    '* 
    '* 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '*                blnSakujoFG  : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, _
                                             ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            'データセット
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' なし

            ' 宛名検索キーのチェック
            ' なし

            ' SQL文の作成    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")
            '*履歴番号 000013 2007/03/09 追加開始
            strSQL.Append(", ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" ASC")
            '*履歴番号 000013 2007/03/09 追加終了
            strSQL.Append(";")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "【クラス名:" + Me.GetType.Name + "】" + _
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                            "【実行メソッド名:GetDataSet】" + _
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000006 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* 履歴番号 000009 2004/08/27 更新終了


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

        Return csDataSet

    End Function

    '************************************************************************************************
    '* メソッド名     代納マスタ抽出
    '* 
    '* 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal strGyomuCD As String,
    '*                                               ByVal strGyomunaiSHUCD As String,
    '*                                               ByVal strKikanYMD As String) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strGyomuCD        : 業務コード
    '*                strGyomunaiSHUCD  : 業務内種別コード
    '*                strKikanYM        : 期間年月日
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                             ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String) As DataSet
        Return GetDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     代納マスタ抽出
    '* 
    '* 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal strGyomuCD As String,
    '*                                               ByVal strGyomunaiSHUCD As String,
    '*                                               ByVal strKikanYMD As String,
    '*                                               ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strGyomuCD        : 業務コード
    '*                strGyomunaiSHUCD  : 業務内種別コード
    '*                strKikanYMD       : 期間年月日
    '*                blnSakujoFG       : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                             ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String,
                                             ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            'データセット
        Dim strSQL As StringBuilder
        Dim cfDateClass As UFDateClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' なし

            '* 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()
            '* 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする

            ' SQL文の作成    
            strSQL = New StringBuilder()
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                '* 履歴番号 000010 2005/01/25 更新開始（宮沢）共通代納も一度に読む
                'strSQL.Append(" AND ")
                'strSQL.Append(ABDainoEntity.GYOMUCD)
                'strSQL.Append(" = ")
                'strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" IN(")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                '* 履歴番号 000010 2005/01/25 更新終了（宮沢）共通代納も一度に読む

                '* 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
                m_cfRdbClass.p_intMaxRows = 1
                '* 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする
            End If
            strSQL.Append(" AND ")

            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）種別無しも一度に読む
            'strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            'strSQL.Append(" = ")
            'strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）種別無しも一度に読む

            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" <= ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" >= ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If

            '* 履歴番号 000010 2005/01/25 追加開始（宮沢）一度で読んだものをソートして先頭の１件を対象にする
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If
            '* 履歴番号 000010 2005/01/25 追加終了（宮沢）一度で読んだものをソートして先頭の１件を対象にする

            strSQL.Append(";")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If文で囲む
            If Not (strGyomuCD = "*1") Then
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = strGyomuCD
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If文で囲む

            ' 検索条件のパラメータを作成
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = strGyomunaiSHUCD
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    cfDateClass = New UFDateClass(m_cfConfigDataClass)
                    cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    '* 履歴番号 000018 2023/10/19 修正開始
                    'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                    cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                    '* 履歴番号 000018 2023/10/19 修正終了
                    cfUFParameterClass.Value = cfDateClass.GetLastDay()
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    '* 履歴番号 000018 2023/10/19 修正開始
                    'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                    '* 履歴番号 000018 2023/10/19 修正終了
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000006 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* 履歴番号 000009 2004/08/27 更新終了

            '* 履歴番号 000010 2005/01/25 追加開始（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する
            '上の番号で一度作成したが、必要なくなったので削除
            'If (strGyomuCD = "*1") Then
            '    If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count > 1) Then
            '        Dim csDataRow As DataRow
            '        Dim csDataTable As DataTable
            '        Dim intRowCount As Integer
            '        csDataTable = csDataSet.Tables(ABDainoEntity.TABLE_NAME)
            '        csDataRow = csDataTable.Rows(0)
            '        For intRowCount = csDataTable.Rows.Count - 1 To 1 Step -1
            '            If (CType(csDataRow.Item(ABDainoEntity.GYOMUNAISHU_CD), String) <> CType(csDataTable.Rows(intRowCount).Item(ABDainoEntity.GYOMUNAISHU_CD), String)) Then
            '                csDataTable.Rows(intRowCount).Delete()
            '            End If
            '        Next
            '        csDataTable.AcceptChanges()
            '    End If
            'End If
            '* 履歴番号 000010 2005/01/25 追加終了（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する

            '* 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にしたものを元に戻す
            m_cfRdbClass.p_intMaxRows = intWkKensu
            '* 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にしたものを元に戻す

            '* 履歴番号 000010 2005/01/25 削除開始（宮沢）
            '' データ件数チェック
            'If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

            '    ' 業務内種別が指定されていた場合
            '    If Not (strGyomunaiSHUCD = String.Empty) Then

            '        ' SQL文の作成
            '        strSQL = Nothing
            '        strSQL = New StringBuilder()
            '        strSQL.Append("SELECT * FROM ")
            '        strSQL.Append(ABDainoEntity.TABLE_NAME)
            '        strSQL.Append(" WHERE ")
            '        strSQL.Append(ABDainoEntity.JUMINCD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            '        If Not (strGyomuCD = "*1") Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.GYOMUCD)
            '            strSQL.Append(" = ")
            '            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            '        End If
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.STYM)
            '        strSQL.Append(" <= ")
            '        strSQL.Append(ABDainoEntity.KEY_STYM)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.EDYM)
            '        strSQL.Append(" >= ")
            '        strSQL.Append(ABDainoEntity.KEY_EDYM)
            '        If Not blnSakujoFG Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.SAKUJOFG)
            '            strSQL.Append(" <> 1")
            '        End If
            '        strSQL.Append(";")

            '        ' 検索条件のパラメータコレクションオブジェクトを作成
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            '        cfUFParameterClass.Value = strJuminCD
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        If Not (strGyomuCD = "*1") Then
            '            ' 検索条件のパラメータを作成
            '            cfUFParameterClass = New UFParameterClass()
            '            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            '            cfUFParameterClass.Value = strGyomuCD
            '            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            '        cfUFParameterClass.Value = ""
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        '*履歴番号 000006 2003/08/28 修正開始
            '        '' RDBアクセスログ出力
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '        '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '        '                    "【実行メソッド名:GetDataSet】" + _
            '        '                    "【SQL内容:" + strSQL.ToString + "】")

            '        ' RDBアクセスログ出力
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "【クラス名:" + Me.GetType.Name + "】" + _
            '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                                    "【実行メソッド名:GetDataSet】" + _
            '                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '        '*履歴番号 000006 2003/08/28 修正終了

            '        ' SQLの実行 DataSetの取得
            '        '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            '        'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        csDataSet = m_csDataSchma.Clone()
            '        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '        '* 履歴番号 000009 2004/08/27 更新終了


            '    End If

            'End If

            '' データ件数チェック
            'If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

            '    ' 業務コード（”00”以外）が指定されていた場合
            '    If Not (strGyomuCD = "00") Then

            '        ' SQL文の作成
            '        strSQL = Nothing
            '        strSQL = New StringBuilder()
            '        strSQL.Append("SELECT * FROM ")
            '        strSQL.Append(ABDainoEntity.TABLE_NAME)
            '        strSQL.Append(" WHERE ")
            '        strSQL.Append(ABDainoEntity.JUMINCD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            '        If Not (strGyomuCD = "*1") Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.GYOMUCD)
            '            strSQL.Append(" = ")
            '            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            '        End If
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.STYM)
            '        strSQL.Append(" <= ")
            '        strSQL.Append(ABDainoEntity.KEY_STYM)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.EDYM)
            '        strSQL.Append(" >= ")
            '        strSQL.Append(ABDainoEntity.KEY_EDYM)
            '        If Not blnSakujoFG Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.SAKUJOFG)
            '            strSQL.Append(" <> 1")
            '        End If
            '        strSQL.Append(";")

            '        ' 検索条件のパラメータコレクションオブジェクトを作成
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            '        cfUFParameterClass.Value = strJuminCD
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        If Not (strGyomuCD = "*1") Then
            '            ' 検索条件のパラメータを作成
            '            cfUFParameterClass = New UFParameterClass()
            '            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            '            cfUFParameterClass.Value = "00"
            '            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            '        cfUFParameterClass.Value = ""
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' 検索条件のパラメータを作成
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        '*履歴番号 000006 2003/08/28 修正開始
            '        '' RDBアクセスログ出力
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '        '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '        '                    "【実行メソッド名:GetDataSet】" + _
            '        '                    "【SQL内容:" + strSQL.ToString + "】")

            '        ' RDBアクセスログ出力
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "【クラス名:" + Me.GetType.Name + "】" + _
            '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                                    "【実行メソッド名:GetDataSet】" + _
            '                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '        '*履歴番号 000006 2003/08/28 修正終了

            '        ' SQLの実行 DataSetの取得
            '        '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            '        'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        csDataSet = m_csDataSchma.Clone()
            '        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '        '* 履歴番号 000009 2004/08/27 更新終了

            '    End If

            'End If
            '* 履歴番号 000010 2005/01/25 削除終了（宮沢）

            ' クラスの解放
            strSQL = Nothing

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

        Return csDataSet

    End Function


    '*履歴番号 000014 2010/03/05 追加開始
    '************************************************************************************************
    '* メソッド名     代納マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetDainoBHoshu(ByVal cABDainoGetParaX As ABDainoGetParaXClass) As DataSet
    '* 
    '* 
    '* 機能　　    　 代納マスタより該当データを取得する
    '* 
    '* 引数           cABDainoGetParaX      :   代納情報パラメータクラス
    '*  
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal cABDainoGetParaX As ABDainoGetParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"             ' メソッド名
        Dim csDainoEntity As DataSet                                    ' 代納マスタデータ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim blnAndFg As Boolean = False                                 ' AND判定フラグ
        Dim strWork As String
        Dim cfDateClass As UFDateClass

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' インスタンス化
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' スキーマ取得処理
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABDainoEntity.TABLE_NAME, False)
            Else
            End If

            ' SQL文の作成
            ' SELECT句
            strSQL.Append("SELECT * ")

            strSQL.Append(" FROM ").Append(ABDainoEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' 住民コード
            If (cABDainoGetParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' 住民コードが設定されている場合

                strSQL.Append(ABDainoEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
                cfUFParameterClass.Value = CStr(cABDainoGetParaX.p_strJuminCD)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 業務コード
            If (cABDainoGetParaX.p_strGyomuCD.Trim <> String.Empty) Then
                ' 業務コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABDainoEntity.GYOMUCD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuCD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 業務内種別コード
            If (cABDainoGetParaX.p_strGyomuneiSHU_CD.Trim <> String.Empty) Then
                ' 業務内種別コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuneiSHU_CD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If

            '---------------------------------------------------------------------------------
            ' 期間
            '* 履歴番号 000018 2023/10/19 修正開始
            'If (cABDainoGetParaX.p_strKikanYM.Trim <> String.Empty) Then
            If (cABDainoGetParaX.p_strKikanYMD.Trim <> String.Empty) Then
            '* 履歴番号 000018 2023/10/19 修正終了
                ' 期間が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append("(")
                strSQL.Append(ABDainoEntity.STYMD)                    '開始年月日
                strSQL.Append(" <= ")
                strSQL.Append(ABDainoEntity.KEY_STYMD)
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.EDYMD)                    '終了年月日
                strSQL.Append(" >= ")
                strSQL.Append(ABDainoEntity.KEY_EDYMD)
                strSQL.Append(")")

                ' 開始年月日
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                '* 履歴番号 000018 2023/10/19 修正開始
                'If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                '    If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                '    Else
                '        cfDateClass = New UFDateClass(m_cfConfigDataClass)
                '        cfDateClass.p_enDateSeparator = UFDateSeparator.None
                '        cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '        cfUFParameterClass.Value = cfDateClass.GetLastDay()
                '    End If
                'Else
                '    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                'End If

                If (cABDainoGetParaX.p_strKikanYMD.Trim.Length = 6) Then
                    If (cABDainoGetParaX.p_strKikanYMD.Trim = ALL0_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    ElseIf (cABDainoGetParaX.p_strKikanYMD.Trim = ALL9_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99"
                    Else
                        cfDateClass = New UFDateClass(m_cfConfigDataClass)
                        cfDateClass.p_enDateSeparator = UFDateSeparator.None
                        cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                        cfUFParameterClass.Value = cfDateClass.GetLastDay()
                    End If
                Else
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD
                End If
                '* 履歴番号 000018 2023/10/19 修正終了

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 終了年月日
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                '* 履歴番号 000018 2023/10/19 修正開始
                'If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                '    If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                '    Else
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    End If
                'Else
                '    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                'End If
                If (cABDainoGetParaX.p_strKikanYMD.Trim.Length = 6) Then
                    If (cABDainoGetParaX.p_strKikanYMD.Trim = ALL0_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    ElseIf (cABDainoGetParaX.p_strKikanYMD.Trim = ALL9_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99"
                    Else
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    End If
                Else
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD
                End If
                '* 履歴番号 000018 2023/10/19 修正終了

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 削除フラグ
            If (cABDainoGetParaX.p_strSakujoFG.Trim = String.Empty) Then
                ' 削除フラグ指定がない場合、削除データは抽出しない
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If
                strSQL.Append(ABDainoEntity.SAKUJOFG).Append(" <> '1'")

            Else
                ' 削除フラグ指定がある場合、削除データも抽出する
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、SQL文生成処理を終了
                Else
                    ' AND判定フラグが"False"の場合、SQL文から｢WHERE｣を削除
                    ' 削除したSQLを一時退避
                    strWork = strSQL.ToString.Replace("WHERE", String.Empty)

                    ' strSQLをクリアし、退避したSQLをセット
                    strSQL.Length = 0
                    strSQL.Append(strWork)
                End If
            End If
            '---------------------------------------------------------------------------------

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csDainoEntity = m_csDataSchma.Clone()
            csDainoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csDainoEntity, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            'デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try

        Return csDainoEntity

    End Function
    '*履歴番号 000014 2010/03/05 追加終了


    '************************************************************************************************
    '* メソッド名     被代納マスタ抽出
    '* 
    '* 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetHiDainoBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     被代納マスタ抽出
    '* 
    '* 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '*                blnSakujoFG   : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, _
                                               ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetHiDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            'データセット
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'パラメータチェック
            'なし

            '宛名検索キーのチェック
            'なし

            ' SQL文の作成    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.DAINOJUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")
            '*履歴番号 000013 2007/03/09 追加開始
            strSQL.Append(", ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" ASC")
            '*履歴番号 000013 2007/03/09 追加終了
            strSQL.Append(";")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "【クラス名:" + Me.GetType.Name + "】" + _
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                            "【実行メソッド名:GetDataSet】" + _
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000006 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* 履歴番号 000009 2004/08/27 更新終了


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

        Return csDataSet

    End Function

    '************************************************************************************************
    '* メソッド名     被代納マスタ抽出
    '* 
    '* 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal strGyomuCD As String,
    '*                                                 ByVal strGyomunaiSHUCD As String,
    '*                                                 ByVal strKikanYMD As String) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strGyomuCD        : 業務コード
    '*                strGyomunaiSHUCD  : 業務内種別コード
    '*                strKikanYM        : 期間年月日
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                               ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String) As DataSet
        Return GetHiDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* メソッド名     被代納マスタ抽出
    '* 
    '* 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal strGyomuCD As String,
    '*                                                 ByVal strGyomunaiSHUCD As String,
    '*                                                 ByVal strKikanYMD As String,
    '*                                                 ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strGyomuCD        : 業務コード
    '*                strGyomunaiSHUCD  : 業務内種別コード
    '*                strKikanYM        : 期間年月日
    '*                blnSakujoFG       : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                               ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String,
                                               ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetHiDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            'データセット
        Dim strSQL As StringBuilder
        Dim cfDateClass As UFDateClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'パラメータチェック
            'なし

            '宛名検索キーのチェック
            'なし

            ' SQL文の作成    
            strSQL = New StringBuilder
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.DAINOJUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            End If
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" <= ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" >= ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(";")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            If Not (strGyomuCD = "*1") Then
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = strGyomuCD
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    cfDateClass = New UFDateClass(m_cfConfigDataClass)
                    cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    '* 履歴番号 000018 2023/10/19 修正開始
                    'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                    cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                    '* 履歴番号 000018 2023/10/19 修正終了
                    cfUFParameterClass.Value = cfDateClass.GetLastDay()
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    '* 履歴番号 000018 2023/10/19 修正開始
                    'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                    '* 履歴番号 000018 2023/10/19 修正終了
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            End If
            '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
            '*履歴番号 000006 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* 履歴番号 000009 2004/08/27 更新終了

            'データ件数チェック
            If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                '業務内種別が指定されていた場合
                If Not (strGyomunaiSHUCD = String.Empty) Then

                    'SQL文の作成
                    strSQL = Nothing
                    strSQL = New StringBuilder
                    strSQL.Append("SELECT * FROM ")
                    strSQL.Append(ABDainoEntity.TABLE_NAME)
                    strSQL.Append(" WHERE ")
                    strSQL.Append(ABDainoEntity.DAINOJUMINCD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
                    If Not (strGyomuCD = "*1") Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.GYOMUCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                    End If
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.STYMD)
                    strSQL.Append(" <= ")
                    strSQL.Append(ABDainoEntity.KEY_STYMD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.EDYMD)
                    strSQL.Append(" >= ")
                    strSQL.Append(ABDainoEntity.KEY_EDYMD)
                    If Not blnSakujoFG Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.SAKUJOFG)
                        strSQL.Append(" <> 1")
                    End If
                    strSQL.Append(";")

                    ' 検索条件のパラメータコレクションオブジェクトを作成
                    cfUFParameterCollectionClass = New UFParameterCollectionClass

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
                    cfUFParameterClass.Value = strJuminCD
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    If Not (strGyomuCD = "*1") Then
                        ' 検索条件のパラメータを作成
                        cfUFParameterClass = New UFParameterClass
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                        cfUFParameterClass.Value = strGyomuCD
                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass)
                    End If

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                    cfUFParameterClass.Value = ""
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            cfDateClass = New UFDateClass(m_cfConfigDataClass)
                            cfDateClass.p_enDateSeparator = UFDateSeparator.None
                            '* 履歴番号 000018 2023/10/19 修正開始
                            'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                            cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                            '* 履歴番号 000018 2023/10/19 修正終了
                            cfUFParameterClass.Value = cfDateClass.GetLastDay()
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            '* 履歴番号 000018 2023/10/19 修正開始
                            'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                            '* 履歴番号 000018 2023/10/19 修正終了
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    '*履歴番号 000006 2003/08/28 修正開始
                    '' RDBアクセスログ出力
                    'm_cfLogClass.RdbWrite(m_cfControlData, _
                    '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                    '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                    '                        "【実行メソッド名:GetDataSet】" + _
                    '                        "【SQL内容:" + strSQL.ToString + "】")

                    ' RDBアクセスログ出力
                    '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                    If (m_blnBatch = False) Then
                        m_cfLogClass.RdbWrite(m_cfControlData,
                                                    "【クラス名:" + Me.GetType.Name + "】" +
                                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                                    "【実行メソッド名:GetDataSet】" +
                                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
                    End If
                    '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                    '*履歴番号 000006 2003/08/28 修正終了

                    ' SQLの実行 DataSetの取得
                    '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
                    'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                    csDataSet = m_csDataSchma.Clone()
                    csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                    '* 履歴番号 000009 2004/08/27 更新終了


                End If

            End If

            'データ件数チェック
            If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                '業務コード（”00”以外）が指定されていた場合
                If Not (strGyomuCD = "00") Then

                    ' SQL文の作成
                    strSQL = Nothing
                    strSQL = New StringBuilder
                    strSQL.Append("SELECT * FROM ")
                    strSQL.Append(ABDainoEntity.TABLE_NAME)
                    strSQL.Append(" WHERE ")
                    strSQL.Append(ABDainoEntity.DAINOJUMINCD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
                    If Not (strGyomuCD = "*1") Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.GYOMUCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                    End If
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.STYMD)
                    strSQL.Append(" <= ")
                    strSQL.Append(ABDainoEntity.KEY_STYMD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.EDYMD)
                    strSQL.Append(" >= ")
                    strSQL.Append(ABDainoEntity.KEY_EDYMD)
                    If Not blnSakujoFG Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.SAKUJOFG)
                        strSQL.Append(" <> 1")
                    End If
                    strSQL.Append(";")

                    ' 検索条件のパラメータコレクションオブジェクトを作成
                    cfUFParameterCollectionClass = New UFParameterCollectionClass

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
                    cfUFParameterClass.Value = strJuminCD
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    If Not (strGyomuCD = "*1") Then
                        ' 検索条件のパラメータを作成
                        cfUFParameterClass = New UFParameterClass
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                        cfUFParameterClass.Value = "00"
                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass)
                    End If

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                    cfUFParameterClass.Value = ""
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            cfDateClass = New UFDateClass(m_cfConfigDataClass)
                            cfDateClass.p_enDateSeparator = UFDateSeparator.None
                            '* 履歴番号 000018 2023/10/19 修正開始
                            'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                            cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                            '* 履歴番号 000018 2023/10/19 修正終了
                            cfUFParameterClass.Value = cfDateClass.GetLastDay()
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            '* 履歴番号 000018 2023/10/19 修正開始
                            'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                            '* 履歴番号 000018 2023/10/19 修正終了
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    '*履歴番号 000006 2003/08/28 修正開始
                    '' RDBアクセスログ出力
                    'm_cfLogClass.RdbWrite(m_cfControlData, _
                    '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                    '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                    '                        "【実行メソッド名:GetDataSet】" + _
                    '                        "【SQL内容:" + strSQL.ToString + "】")

                    ' RDBアクセスログ出力
                    '* 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                    If (m_blnBatch = False) Then
                        m_cfLogClass.RdbWrite(m_cfControlData,
                                                    "【クラス名:" + Me.GetType.Name + "】" +
                                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                                    "【実行メソッド名:GetDataSet】" +
                                                    "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
                    End If
                    '* 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                    '*履歴番号 000006 2003/08/28 修正終了

                    ' SQLの実行 DataSetの取得
                    '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
                    'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                    csDataSet = m_csDataSchma.Clone()
                    csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                    '* 履歴番号 000009 2004/08/27 更新終了

                End If

            End If

            'クラスの解放
            strSQL = Nothing

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

        Return csDataSet

    End Function

    '************************************************************************************************
    '* メソッド名     代納マスタ追加
    '* 
    '* 構文           Public Function InsertDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　代納マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoB"
        Dim cfParam As UFParameterClass     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csInstRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer            '追加件数
        Dim strUpdateDateTime As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If


            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId ' 端末ＩＤ
            csDataRow(ABDainoEntity.SAKUJOFG) = "0"                             ' 削除フラグ
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = Decimal.Zero               ' 更新カウンタ
            csDataRow(ABDainoEntity.SAKUSEINICHIJI) = strUpdateDateTime         ' 作成日時
            csDataRow(ABDainoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId  ' 作成ユーザー
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = strUpdateDateTime          ' 更新日時
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId   ' 更新ユーザー


            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn


            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam


            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000006 2003/08/28 修正終了

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
    '* メソッド名     代納マスタ更新
    '* 
    '* 構文           Public Function UpdateDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　代納マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateDainoB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intUpdCnt As Integer                            '更新件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If

            ' 共通項目の編集を行う
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1               '更新カウンタ
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー
            '* 履歴番号 000019 2023/12/05 削除開始
            ''* 履歴番号 000018 2023/10/19 追加開始
            'csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             '履歴番号
            ''* 履歴番号 000018 2023/10/19 追加終了
            '* 履歴番号 000019 2023/12/05 削除終了

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            '*履歴番号 000006 2003/08/28 修正終了

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
    '* メソッド名     代納マスタ論理削除
    '* 
    '* 構文           Public Function DeleteDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　代納マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteDainoB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intDelCnt As Integer                            '削除件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If


            ' 共通項目の編集を行う
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' 端末ＩＤ
            csDataRow(ABDainoEntity.SAKUJOFG) = "1"                                                               ' 削除フラグ
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1             ' 更新カウンタ
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   ' 更新日時
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' 更新ユーザー
            '* 履歴番号 000019 2023/12/05 削除開始
            ''* 履歴番号 000018 2023/10/19 追加開始
            'csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             ' 履歴番号
            ''* 履歴番号 000018 2023/10/19 追加終了
            '* 履歴番号 000019 2023/12/05 削除終了

            '*履歴番号 000006 2003/08/28 修正開始
            '' 作成済みのパラメータへ更新行から値を設定する。
            'For Each cfParam In m_cfUpdateUFParameterCollectionClass
            '    ' キー項目は更新前の値で設定
            '    If (cfParam.ParameterName.Substring(0, ABDainoEntity.PREFIX_KEY.Length) = ABDainoEntity.PREFIX_KEY) Then
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
            '                csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PREFIX_KEY.Length), _
            '                          DataRowVersion.Original).ToString()
            '    Else
            '        'データ整合性チェック
            '        CheckColumnValue(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
            '    End If
            'Next cfParam

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    'データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam
            '*履歴番号 000006 2003/08/28 修正終了


            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
            '*履歴番号 000006 2003/08/28 修正終了

            '*履歴番号 000006 2003/08/28 修正開始
            '' SQLの実行
            'intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)
            '*履歴番号 000006 2003/08/28 修正終了

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan")

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
    '* メソッド名     代納マスタ物理削除
    '* 
    '* 構文           Public Function DeleteDainoB(ByVal csDataRow As DataRow, _
    '*                                             ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　　代納マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow  : 削除するデータの含まれるDataRowオブジェクト
    '*                strSakujoKB As String : 削除フラグ
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteDainoB(ByVal csDataRow As DataRow, _
                                           ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteDainoB"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intDelCnt As Integer                            '削除件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除区分のチェックを行う
            If Not (strSakujoKB = "D") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

            End If

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or _
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass

                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*履歴番号 000006 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
            '*履歴番号 000006 2003/08/28 修正終了

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

    '* corresponds to VS2008 Start 2010/04/16 000015
    '* 履歴番号 000011 2005/06/16 削除開始
    ''''************************************************************************************************
    ''''* メソッド名     SQL文の作成
    ''''* 
    ''''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''''* 
    ''''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    ''''* 
    ''''* 引数           csDataRow As DataRow : 更新対象の行
    ''''* 
    ''''* 戻り値         なし
    ''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)

    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"
    ''''    Dim cfUFParameterClass As UFParameterClass
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim csInsertColumn As StringBuilder                 'INSERTカラム定義
    ''''    Dim csInsertParam As StringBuilder                  'INSERTパラメータ定義
    ''''    Dim csUpdateParam As StringBuilder                  'UPDATE用パラメータ
    ''''    Dim csWhere As StringBuilder                        'WHERE句
    ''''    Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義

    ''''    Try
    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' INSERT SQL文の作成
    ''''        m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " "
    ''''        csInsertColumn = New StringBuilder()
    ''''        csInsertParam = New StringBuilder()

    ''''        ' UPDATE SQL文の作成
    ''''        m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET "
    ''''        csUpdateParam = New StringBuilder()

    ''''        ' WHERE句の作成
    ''''        csWhere = New StringBuilder()
    ''''        csWhere.Append(" WHERE ")
    ''''        csWhere.Append(ABDainoEntity.JUMINCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_JUMINCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.GYOMUCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.DAINOJUMINCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.STYM)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_STYM)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.EDYM)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_EDYM)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

    ''''        ' 論理DELETE SQL文の作成
    ''''        csDelRonriParam = New StringBuilder()
    ''''        csDelRonriParam.Append("UPDATE ")
    ''''        csDelRonriParam.Append(ABDainoEntity.TABLE_NAME)
    ''''        csDelRonriParam.Append(" SET ")
    ''''        csDelRonriParam.Append(ABDainoEntity.TANMATSUID)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.SAKUJOFG)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINUSER)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER)
    ''''        csDelRonriParam.Append(csWhere)
    ''''        m_strDelRonriSQL = csDelRonriParam.ToString

    ''''        ' 物理DELETE SQL文の作成
    ''''        m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME _
    ''''                + csWhere.ToString

    ''''        ' INSERT パラメータコレクションクラスのインスタンス化
    ''''        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' UPDATE パラメータコレクションのインスタンス化
    ''''        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' 論理削除用パラメータコレクションのインスタンス化
    ''''        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' 物理削除用パラメータコレクションのインスタンス化
    ''''        m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()



    ''''        ' パラメータコレクションの作成
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            ' INSERT SQL文の作成
    ''''            csInsertColumn.Append(csDataColumn.ColumnName)
    ''''            csInsertColumn.Append(", ")

    ''''            csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
    ''''            csInsertParam.Append(csDataColumn.ColumnName)
    ''''            csInsertParam.Append(", ")


    ''''            ' UPDATE SQL文の作成
    ''''            csUpdateParam.Append(csDataColumn.ColumnName)
    ''''            csUpdateParam.Append(" = ")
    ''''            csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
    ''''            csUpdateParam.Append(csDataColumn.ColumnName)
    ''''            csUpdateParam.Append(", ")

    ''''            ' INSERT コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''            ' UPDATE コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        Next csDataColumn


    ''''        '最後のカンマを取り除いてINSERT文を作成
    ''''        m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
    ''''                + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"



    ''''        '最後のカンマを取り除いてUPDATE文を作成
    ''''        m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString


    ''''        ' UPDATE コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)


    ''''        ' 論理削除用コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)


    ''''        ' 物理削除用コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException
    ''''        ' ワーニングログ出力
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
    ''''                                    "【ワーニング内容:" + objAppExp.Message + "】")
    ''''        ' エラーをそのままスローする
    ''''        Throw objAppExp

    ''''    Catch objExp As Exception
    ''''        ' エラーログ出力
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    ''''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    ''''                                    "【エラー内容:" + objExp.Message + "】")
    ''''        ' エラーをそのままスローする
    ''''        Throw objExp

    ''''    End Try

    ''''End Sub
    '* 履歴番号 000011 2005/06/16 削除終了
    '* corresponds to VS2008 End 2010/04/16 000015

    '* 履歴番号 000011 2005/06/16 追加開始
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
        Dim cfUFParameterClass As UFParameterClass
        Dim csDataColumn As DataColumn
        Dim csInsertColumn As StringBuilder                 'INSERTカラム定義
        Dim csInsertParam As StringBuilder                  'INSERTパラメータ定義

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " "
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

                csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csDataColumn As DataColumn
        Dim csUpdateParam As StringBuilder                  'UPDATE用パラメータ
        Dim csWhere As StringBuilder                        'WHERE句

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE句の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABDainoEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABDainoEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABDainoEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(", ")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            '最後のカンマを取り除いてUPDATE文を作成
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString

            ' UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE句
        Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE句の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABDainoEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABDainoEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER)
            '* 履歴番号 000018 2023/10/19 追加開始
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.RRKNO)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_RRKNO)
            '* 履歴番号 000018 2023/10/19 追加終了
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            '* 履歴番号 000018 2023/10/19 追加開始
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '* 履歴番号 000018 2023/10/19 追加終了

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub


    '************************************************************************************************
    '* メソッド名     物理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
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
        Dim csWhere As StringBuilder                        'WHERE句

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE句の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME _
                    + csWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub
    '* 履歴番号 000011 2005/06/16 削除終了

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue as String)
    '* 
    '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           strColumnName As String : 住登外マスタデータセットの項目名
    '*                strValue As String     : 項目に対応する値
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "代納．"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' 日付クラスのインスタンス化
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABDainoEntity.JUMINCD                  '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SHICHOSONCD              '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KYUSHICHOSONCD           '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.GYOMUCD                  '業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.GYOMUNAISHU_CD           '業務内種別コード
                    If (Not UFStringClass.CheckNumber(strValue.Trim)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.DAINOJUMINCD             '代納住民コード
                    If Not (strValue.Trim = String.Empty) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOJUMINCD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABDainoEntity.STYMD                     '開始年月日
                    Select Case strValue.Trim
                        Case "00000000", String.Empty
                            ' ＯＫ
                        Case Else
                            m_cfDateClass.p_strDateValue = strValue
                            If (Not m_cfDateClass.CheckDate()) Then
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                'エラー定義を取得(日付項目入力の誤りです。：)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                                '例外を生成
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "開始年月日", objErrorStruct.m_strErrorCode)
                            End If
                    End Select

                Case ABDainoEntity.EDYMD                     '終了年月日
                    Select Case strValue.Trim
                        Case "00000000", "99999999", String.Empty
                            ' ＯＫ
                        Case Else
                            m_cfDateClass.p_strDateValue = strValue
                            If (Not m_cfDateClass.CheckDate()) Then
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                'エラー定義を取得(日付項目入力の誤りです。：)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                                '例外を生成
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "終了年月日", objErrorStruct.m_strErrorCode)
                            End If
                    End Select

                '* 履歴番号 000018 2023/10/19 追加開始
                Case ABDainoEntity.TOROKURENBAN             '登録連番
                    If (Not (strValue.Trim = String.Empty)) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            '* 履歴番号 000019 2023/12/05 修正開始
                            ''例外を生成
                            'Throw New UFAppException("数字項目入力エラー：ＡＢ代納　登録連番", UFAppException.ERR_EXCEPTION)
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TOROKURENBAN)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                            '* 履歴番号 000019 2023/12/05 修正終了
                        End If
                    End If

                Case ABDainoEntity.RRKNO                     '履歴番号
                    If (Not (strValue.Trim = String.Empty)) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            '* 履歴番号 000019 2023/12/05 修正開始
                            ''例外を生成
                            'Throw New UFAppException("数字項目入力エラー：ＡＢ代納　履歴番号", UFAppException.ERR_EXCEPTION)
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            'エラー定義を取得
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_RRKNO)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                            '* 履歴番号 000019 2023/12/05 修正終了
                        End If
                    End If
                '* 履歴番号 000018 2023/10/19 追加終了

                Case ABDainoEntity.DAINOKB                  '代納区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.RESERVE                  'リザーブ
                    'チェックなし

                Case ABDainoEntity.TANMATSUID               '端末ＩＤ
                    '* 履歴番号 000007 2003/09/11 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/09/11 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUJOFG                 '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINCOUNTER            '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUSEINICHIJI           '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUSEIUSER              '作成ユーザ
                    '* 履歴番号 000008 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000008 2003/10/09 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINNICHIJI            '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINUSER               '更新ユーザ
                    '* 履歴番号 000008 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000008 2003/10/09 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

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

    End Sub
    '* 履歴番号 000010 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名     代納マスタスキーマ取得
    '* 
    '* 構文           Public Function GetDainoSchemaBHoshu() As DataSet
    '* 
    '* 機能　　    　　代納マスタよりスキーマ取得
    '* 
    '* 
    '* 戻り値         DataSet : 取得した代納マスタのスキーマ
    '************************************************************************************************
    Public Overloads Function GetDainoSchemaBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoSchemaBHoshu"              'このメソッド名

        Try
            Return (m_csDataSchma.Clone)
        Catch exAppException As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            'ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            'システムエラーをスローする
            Throw exException

        End Try
    End Function
    '* 履歴番号 000010 2005/01/25 追加終了（宮沢）

    '* 履歴番号 000012 2006/12/22 追加開始
    '************************************************************************************************
    '* メソッド名     本店情報抽出
    '* 
    '* 構文           Public Function GetHontenBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　代納マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '* 
    '* 戻り値         DataSet : 取得した代納マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetHontenBHoshu(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetHontenBHoshu"    ' メソッド名
        Const HONTEN_GYOMUCD As String = "05"                   ' 本店情報レコード業務コード
        Const HONTEN_GYOMUNAISHU_CD As String = "9"             ' 本店情報レコード業務内種コード
        Const HONTEN_STYMD As String = "00000000"                  ' 本店情報レコード開始年月日
        Const HONTEN_EDYMD As String = "99999999"                  ' 本店情報レコード終了年月日
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                     ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                                'データセット
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)

            strSQL.Append(";")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成（住民コード）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成（業務コード）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = HONTEN_GYOMUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成（業務内種コード）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = HONTEN_GYOMUNAISHU_CD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成（開始年月日）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            cfUFParameterClass.Value = HONTEN_STYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成（終了年月日）
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            cfUFParameterClass.Value = HONTEN_EDYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)

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

        Return csDataSet

    End Function
    '* 履歴番号 000012 2006/12/22 追加終了
#End Region

End Class
