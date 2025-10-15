'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ住登外マスタＤＡ(ABJutogaiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2002/12/20　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/12 000001     有効桁数の対応
'* 2003/03/25 000002     郵便番号が追加になりました。
'* 2003/04/16 000003     生和暦年月日の日付チェックを数値チェックに変更
'*                       検索用カナの半角カナチェックをＡＮＫチェックに変更
'* 2003/05/21 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000005     RDBアクセスログの修正
'* 2003/09/11 000006     端末ＩＤ整合性チェックをANKにする
'* 2003/10/09 000007     作成ユーザー・更新ユーザーチェックの変更
'* 2003/10/30 000008     仕様変更、カタカナチェックをANKチェックに変更
'* 2004/05/13 000009     仕様変更、汎用区分をANKチェックに変更
'* 2005/01/15 000010     仕様変更、住所コードをANKチェックに変更
'* 2005/06/16 000011     SQL文をInsert,Update,論理Delete,物理Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
'* 2005/12/26 000012     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
'* 2010/04/16 000013     VS2008対応（比嘉）
'* 2011/10/24 000014     【AB17010】＜住基法改正対応＞宛名付随マスタ追加   (小松)
'* 2023/08/14 000015    【AB-0820-1】住登外管理項目追加(早崎)
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
'* 住登外マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABJutogaiBClass
#Region "メンバ変数"
    ' パラメータのメンバ変数
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDelRonriSQL As String                      ' 論理削除用SQL
    Private m_strDelButuriSQL As String                     ' 物理削除用SQL
    Private m_cfInsertUFParameterCollection As UFParameterCollectionClass       ' INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollection As UFParameterCollectionClass       ' UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollection As UFParameterCollectionClass     ' 論理削除用パラメータコレクション
    Private m_cfDelButuriUFParameterCollection As UFParameterCollectionClass    ' 物理削除用パラメータコレクション

    '*履歴番号 000014 2011/10/24 追加開始
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '施行日判定Bｸﾗｽ
    Private m_csAtenaFZYB As ABAtenaFZYBClass                       '宛名付随マスタBｸﾗｽ
    Private m_strJukihoKaiseiKB As String                           '住基法改正区分
    '*履歴番号 000014 2011/10/24 追加終了
    '*履歴番号 000015 2023/08/14 追加開始
    Private m_blnJukihoKaiseiFG As Boolean = False
    '*履歴番号 000015 2023/08/14 追加終了

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABJutogaiBClass"                 ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

#Region "プロパティ"
    '*履歴番号 000014 2011/10/24 追加開始
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' 住基法改正区分
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*履歴番号 000014 2011/10/24 追加終了
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
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
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
        m_strDelButuriSQL = String.Empty
        m_cfInsertUFParameterCollection = Nothing
        m_cfUpdateUFParameterCollection = Nothing
        m_cfDelRonriUFParameterCollection = Nothing
        m_cfDelButuriUFParameterCollection = Nothing

        '*履歴番号 000014 2011/10/24 追加開始
        m_strJukihoKaiseiKB = String.Empty
        '*履歴番号 000014 2011/10/24 追加終了
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     住登外マスタ抽出
    '* 
    '* 構文           Public Function GetJutogaiBHoshu() As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataSet : 取得した住登外マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu() As DataSet

        Return Me.GetJutogaiBHoshu(False)

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ抽出
    '* 
    '* 構文           Public Function GetJutogaiBHoshu(ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能           住登外マスタより全件データを取得する
    '* 
    '* 引数           blnSakujoFG   : 削除フラグ（省略可）
    '* 
    '* 戻り値         DataSet : 取得した住登外マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu"
        Dim csJutogaiEntity As DataSet
        Dim strSQL As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            If blnSakujoFG = True Then
                strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME
            Else
                strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME _
                        + " WHERE " + ABJutogaiEntity.SAKUJOFG + " <> '1';"
            End If

            '*履歴番号 000015 2023/08/14 追加開始
            '施行日以降フラグを取得する
            m_csSekoYMDHanteiB = New ABSekoYMDHanteiBClass(Me.m_cfControlData, Me.m_cfConfigDataClass, Me.m_cfRdbClass)
            m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD

            '住基法改正以降のとき、は宛名_標準、宛名付随_標準をLEFT OUTER JOINして取得する
            If (m_blnJukihoKaiseiFG) Then
                strSQL = "SELECT A.* FROM (" + strSQL + ") A"
                strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaHyojunEntity.TABLE_NAME + " B ON A." + ABJutogaiEntity.JUMINCD +
                    "  = B." + ABAtenaHyojunEntity.JUMINCD
                strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaFZYHyojunEntity.TABLE_NAME + " C ON A." + ABJutogaiEntity.JUMINCD +
                    " = C." + ABAtenaFZYHyojunEntity.JUMINCD
            End If
            '*履歴番号 000015 2023/08/14 追加終了

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" +
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
            '                            "【実行メソッド名:GetDataSet】" +
            '                            "【SQL内容:" + strSQL + "】")

            ' SQLの実行 DataSetの取得
            csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL, ABJutogaiEntity.TABLE_NAME)


            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return csJutogaiEntity

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ抽出
    '* 
    '* 構文           Public Function GetJutogaiBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード（省略可）
    '* 
    '* 戻り値         DataSet : 取得した住登外マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal strJuminCD As String) As DataSet

        Return Me.GetJutogaiBHoshu(strJuminCD, False)

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ抽出
    '* 
    '* 構文           Public Function GetJutogaiBHoshu(Optional ByVal strJuminCD As String = "", _
    '*                                Optional ByVal blnSakujoFG As Boolean = False) As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '*                blnSakujoFG   : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した住登外マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal strJuminCD As String,
                                               ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu"
        Dim csJutogaiEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            '*履歴番号 000014 2011/10/24 修正開始
            '住基法改正以降は宛名付随マスタを付加
            If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                strSQL.AppendFormat("SELECT {0}.* ", ABJutogaiEntity.TABLE_NAME)
                Me.SetFZYEntity(strSQL)
                strSQL.AppendFormat(" FROM {0} ", ABJutogaiEntity.TABLE_NAME)
                Me.SetFZYJoin(strSQL)
                strSQL.AppendFormat(" WHERE {0}.{1}={2} ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD, ABJutogaiEntity.KEY_JUMINCD)
                If blnSakujoFG = False Then
                    strSQL.AppendFormat(" AND {0}.{1} <> '1' ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.SAKUJOFG)
                End If
            Else
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABJutogaiEntity.TABLE_NAME)
                strSQL.Append(" WHERE ")
                strSQL.Append(ABJutogaiEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
                If blnSakujoFG = False Then
                    strSQL.Append(" AND ")
                    strSQL.Append(ABJutogaiEntity.SAKUJOFG)
                    strSQL.Append(" <> '1';")
                End If
            End If
            '*履歴番号 000014 2011/10/24 修正終了

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaJiteEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:GetDataSet】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABJutogaiEntity.TABLE_NAME, cfUFParameterCollectionClass)


            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return csJutogaiEntity

    End Function

    '*履歴番号 000014 2011/10/24 追加開始
    '************************************************************************************************
    '* メソッド名     宛名付随データ項目編集
    '* 
    '* 構文           Private SetFZYEntity()
    '* 
    '* 機能           宛名付随データの項目編集をします。
    '* 
    '* 引数           strAtenaSQLsb　：　宛名取得用SQL  
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetFZYEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TABLEINSERTKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.LINKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINHYOJOTAIKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKYOCHITODOKEFLG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KATAKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.UMAREFUSHOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHAMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUCARDNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEISTYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEIEDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSKANAMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE10)
    End Sub
    '************************************************************************************************
    '* メソッド名     宛名付随テーブルJOIN句作成
    '* 
    '* 構文           Private SetFZYJoin()
    '* 
    '* 機能           宛名付随テーブルのJOIN句を作成します。
    '* 
    '* 引数           strAtenaSQLsb　：　宛名取得用SQL  
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetFZYJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD,
                                    ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
    End Sub
    '*履歴番号 000014 2011/10/24 追加終了

    '************************************************************************************************
    '* メソッド名     住登外マスタ追加
    '* 
    '* 構文           Public Function InsertJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　住登外マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertJutogaiB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertJutogaiB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csInstRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer        '追加件数
        Dim strUpdateDateTime As String


        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollection Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId   '端末ＩＤ
            csDataRow(ABJutogaiEntity.SAKUJOFG) = "0"                               '削除フラグ
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = Decimal.Zero                 '更新カウンタ
            csDataRow(ABJutogaiEntity.SAKUSEINICHIJI) = strUpdateDateTime           '作成日時
            csDataRow(ABJutogaiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    '作成ユーザー
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = strUpdateDateTime            '更新日時
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollection) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollection)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ更新
    '* 
    '* 構文           Public Function UpdateJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　住登外マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateJutogaiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateJutogaiB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intUpdCnt As Integer                            '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or
                m_cfUpdateUFParameterCollection Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If

            ' 共通項目の編集を行う
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' 端末ＩＤ
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABJutogaiEntity.KOSHINCOUNTER)) + 1           ' 更新カウンタ
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   ' 更新日時
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' 更新ユーザー


            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollection
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollection(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfUpdateUFParameterCollection(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollection) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollection)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan")

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ削除
    '* 
    '* 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　住登外マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteJutogaiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateJutogaiB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intDelCnt As Integer                            '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or
                    m_cfDelRonriUFParameterCollection Is Nothing) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If


            '共通項目の編集を行う
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                     '端末ＩＤ
            csDataRow(ABJutogaiEntity.SAKUJOFG) = "1"                                                                   '削除フラグ
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABJutogaiEntity.KOSHINCOUNTER)) + 1               '更新カウンタ
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")     '更新日時
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                       '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollection
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollection(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength),
                                    DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfDelRonriUFParameterCollection(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     住登外マスタ物理削除
    '* 
    '* 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　　住登外マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteJutogaiB(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteJutogaiB"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        Dim cfParam As UFParameterClass                     ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intDelCnt As Integer                            ' 削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除区分のチェックを行う
            If Not (strSakujoKB = "D") Then
                ' エラー定義を取得
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = "" Or
                    IsNothing(m_cfDelButuriUFParameterCollection)) Then
                '* 履歴番号 000011 2005/06/16 追加開始
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* 履歴番号 000011 2005/06/16 追加終了
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDelButuriUFParameterCollection

                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollection(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '* corresponds to VS2008 Start 2010/04/16 000013
    ''* 履歴番号 000011 2005/06/16 削除開始
    '''''************************************************************************************************
    '''''* メソッド名     SQL文の作成
    '''''* 
    '''''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '''''* 
    '''''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '''''* 
    '''''* 引数           csDataRow As DataRow : 更新対象の行
    '''''* 
    '''''* 戻り値         なし
    '''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)

    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim strInsertColumn As String                       'INSERT用カラム
    ''''    Dim strInsertParam As String
    ''''    Dim cfUFParameterClass As UFParameterClass
    ''''    Dim strUpdateWhere As String
    ''''    Dim strUpdateParam As String
    ''''    Dim csDelRonriSQL As New StringBuilder()            '論理削除用SQL

    ''''    Try
    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' SELECT SQL文の作成
    ''''        m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " "
    ''''        strInsertColumn = ""
    ''''        strInsertParam = ""

    ''''        ' UPDATE SQL文の作成
    ''''        m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET "
    ''''        strUpdateParam = ""
    ''''        strUpdateWhere = ""

    ''''        ' 論理DELETE SQL文の作成
    ''''        csDelRonriSQL.Append("UPDATE ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME)
    ''''        csDelRonriSQL.Append(" SET ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER)
    ''''        csDelRonriSQL.Append(" WHERE ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
    ''''        csDelRonriSQL.Append(" AND ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER)


    ''''        ' 物理DELETE SQL文の作成
    ''''        m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " + _
    ''''                         ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
    ''''                         ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

    ''''        ' SELECT パラメータコレクションクラスのインスタンス化
    ''''        m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' UPDATE パラメータコレクションのインスタンス化
    ''''        m_cfUpdateUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' 論理削除用パラメータコレクションのインスタンス化
    ''''        m_cfDelRonriUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' 物理削除用パラメータコレクションのインスタンス化
    ''''        m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, "UFParameterCollectionClass End")


    ''''        ' パラメータコレクションの作成
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            ' INSERT SQL文の作成
    ''''            strInsertColumn += csDataColumn.ColumnName + ", "
    ''''            strInsertParam += ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            ' UPDATE SQL文の作成
    ''''            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            ' INSERT コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

    ''''            ' UPDATE コレクションにパラメータを追加
    ''''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        Next csDataColumn

    ''''        ' INSERT SQL文のトリミング
    ''''        strInsertColumn = strInsertColumn.Trim()
    ''''        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    ''''        strInsertParam = strInsertParam.Trim()
    ''''        strInsertParam = strInsertParam.Trim(CType(",", Char))

    ''''        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    ''''        ' UPDATE SQL文のトリミング
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim()
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    ''''        ' UPDATE SQL文にWHERE句の追加
    ''''        m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
    ''''                                      ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER


    ''''        ' UPDATE コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        ' 論理削除用コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        ' 物理削除用コレクションにパラメータを追加
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)


    ''''        'パラメータ変数へ格納
    ''''        m_strDelRonriSQL = csDelRonriSQL.ToString

    ''''        ' デバッグログ出力
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
    ''''        ' システムエラーをスローする
    ''''        Throw objExp

    ''''    End Try

    ''''End Sub
    ''* 履歴番号 000011 2005/06/16 削除終了
    '* corresponds to VS2008 End 2010/04/16 000013
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
        Dim csDataColumn As DataColumn
        Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder()
            csInsertParam = New StringBuilder()

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")
                csInsertParam.Append(ABJutogaiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL += "(" + csInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + csInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
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
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csUpdateParam As StringBuilder                  'UPDATE用SQL定義

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder()

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollection = New UFParameterCollectionClass()

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABJutogaiEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABJutogaiEntity.SAKUSEIUSER) AndAlso
                     Not (csDataColumn.ColumnName = ABJutogaiEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass()

                    ' UPDATE SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.ToString.Trim()
            m_strUpdateSQL = m_strUpdateSQL.ToString.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " +
                                          ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

            ' UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
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
        Dim csDelRonriSQL As New StringBuilder()            '論理削除用SQL

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 論理DELETE SQL文の作成
            csDelRonriSQL.Append("UPDATE ")
            csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME)
            csDelRonriSQL.Append(" SET ")
            csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER)
            csDelRonriSQL.Append(" WHERE ")
            csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
            csDelRonriSQL.Append(" AND ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER)

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollection = New UFParameterCollectionClass()

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            'パラメータ変数へ格納
            m_strDelRonriSQL = csDelRonriSQL.ToString

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
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

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " +
                             ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " +
                             ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp

        End Try

    End Sub
    '* 履歴番号 000011 2005/06/16 追加終了

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
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
        Const TABLENAME As String = "住登外．"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体


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

                Case ABJutogaiEntity.JUMINCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.STAICD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_STAICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ATENADATAKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATAKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ATENADATASHU
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATASHU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANASEIMEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓名", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANASEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANAMEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ名", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANAMEISHO1
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIMEISHO1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANAMEISHO2
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIMEISHO2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.UMAREYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_UMAREYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.UMAREWMD               '生和暦年月日
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得(数字項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEIBETSUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEIBETSU
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ZOKUGARACD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ZOKUGARA
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARA)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.DAI2ZOKUGARACD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.DAI2ZOKUGARA
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARA)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.HANYOKB1
                    '*履歴番号 000009 2004/05/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*履歴番号 000009 2004/05/13 修正開始
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIHJNKEITAI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNKEITAI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KJNHJNKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KJNHJNKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.HANYOKB2
                    '*履歴番号 000009 2004/05/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*履歴番号 000009 2004/05/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANNAIKANGAIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANNAIKANGAIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KAOKUSHIKIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KAOKUSHIKIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BIKOZEIMOKU
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BIKOZEIMOKU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.YUBINNO                '郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "郵便番号", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.JUSHOCD
                    '*履歴番号 000010 2005/01/15 修正開始
                    'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 000010 2005/01/15 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.JUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKIFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKIFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKICD
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RENRAKUSAKI1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RENRAKUSAKI2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.GYOSEIKUCD
                    '* 履歴番号 000012 2005/12/26 修正開始
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* 履歴番号 000012 2005/12/26 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.GYOSEIKUMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.TOROKUIDOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.TOROKUJIYUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SHOJOIDOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.SHOJOJIYUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RESERVE
                        'チェックなし

                Case ABJutogaiEntity.TANMATSUID
                    '* 履歴番号 000006 2003/09/11 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000006 2003/09/11 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUJOFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINCOUNTER
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUSEINICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUSEIUSER
                    '* 履歴番号 000007 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/09 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINNICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINUSER
                    '* 履歴番号 000007 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/09 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
            ' システムエラーをスローする
            Throw objExp
        End Try

    End Sub

#End Region

End Class