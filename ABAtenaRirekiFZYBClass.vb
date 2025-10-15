'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名履歴付随マスタＤＡ
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2011/10/24　小松　知尚
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2023/12/07  000001     【AB-9000-1】住基更新連携標準化対応(下村)
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
'* 宛名履歴付随マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaRirekiFZYBClass
#Region "メンバ変数"
    ' パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDelRonriSQL As String                      ' 論理削除用SQL
    Private m_strDelButuriSQL As String                     ' 物理削除用SQL
    Private m_strDelFromJuminCDSQL As String                ' 物理削除用SQL(１住民コード指定)
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '論理削除用パラメータコレクション
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '物理削除用パラメータコレクション
    Private m_cfDelFromJuminCDPrmCollection As UFParameterCollectionClass           '物理削除用SQL(１住民コード指定)
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット
    Private m_strUpdateDatetime As String                   ' 更新日時

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaRirekiFZYBClass"                 ' クラス名
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
        m_strDelButuriSQL = String.Empty
        m_strDelFromJuminCDSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        m_cfDelFromJuminCDPrmCollection = Nothing
    End Sub
#End Region

#Region "メソッド"
#Region "宛名履歴付随マスタ抽出　[GetAtenaFZYRBHoshu]"
    '************************************************************************************************
    '* メソッド名    宛名履歴付随マスタ抽出
    '* 
    '* 構文          Public Function GetAtenaFZYRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String) As DataSet
    '* 
    '* 機能　　    　住登外マスタより該当データを取得する
    '* 
    '* 引数          strJuminCD         : 住民コード 
    '*               strRrkNo           : 履歴番号
    '*               strJuminJutogaiKB  : 住民住登外区分
    '* 
    '* 戻り値        DataSet : 取得した宛名履歴付随マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaFZYRBHoshu(ByVal strJuminCD As String, _
                                                 ByVal strRrkNo As String, _
                                                 ByVal strJuminJutogaiKB As String) As DataSet
        Return GetAtenaFZYRBHoshu(strJuminCD, strRrkNo, strJuminJutogaiKB, False)
    End Function

    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* 機能　　    　　宛名履歴付随マスタより該当データを取得する
    '* 
    '* 引数          strJuminCD     : 住民コード 
    '*               strRrkNo       : 履歴番号
    '*               strJuminJutogaiKB  : 住民住登外区分
    '*               blnSakujoFG    : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した宛名履歴付随マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaFZYRBHoshu(ByVal strJuminCD As String, _
                                                 ByVal strRrkNo As String, _
                                                 ByVal strJuminJutogaiKB As String, _
                                                 ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaFZYRBHoshu"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaRirekiEntity As DataSet                  '宛名履歴データセット
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 住民コードが指定されていないときエラー
            If IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0) Then
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
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME)

            'ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strRrkNo, strJuminJutogaiKB, blnSakujoFG))

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaRirekiEntity = m_csDataSchma.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaRirekiEntity

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiFZYEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KYUSHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TABLEINSERTKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.LINKNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKYOCHITODOKEFLG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.HONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJITSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.UMAREFUSHOKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TSUSHOMEITOUROKUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHAMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUCARDNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEISTYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEIEDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSKANAMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE6)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE7)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE8)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE9)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE10)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINUSER)

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
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　　WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String, _
                                 ByVal strRrkNo As String, _
                                 ByVal strJuminJutogaiKB As String, _
                                 ByVal blnSakujoFG As Boolean) As String
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '履歴番号
            If (Not strRrkNo.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.RIREKINO, ABAtenaRirekiFZYEntity.KEY_RIREKINO)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRrkNo
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            '住民住登外区分
            If (Not strJuminJutogaiKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB, ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKB
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 削除フラグ
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON)
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

#End Region

#Region "宛名履歴付随マスタ抽出"
    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* 機能　　    　　宛名履歴付随マスタより該当データを取得する
    '* 
    '* 引数          strJuminCD     : 住民コード 
    '*               strRrkNo       : 履歴番号
    '*               strJuminJutogaiKB  : 住民住登外区分
    '*               blnSakujoFG    : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した宛名履歴付随マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaRirekiFZYByLinkNo(ByVal strJuminCD As String,
                                                 ByVal strLinkNo As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaRirekiFZYByLinkNo"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaRirekiFZYEntity As DataSet               '宛名履歴付随データセット
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 住民コードが指定されていないときエラー
            If IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' SELECTパラメータコレクションクラスのインスタンス化
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME)

            'ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            ' 住民コード
            strSQL.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            'リンク番号
            strSQL.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.LINKNO, ABAtenaRirekiFZYEntity.PARAM_LINKNO)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_LINKNO
            cfUFParameterClass.Value = strLinkNo
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '住民住登外区分
            strSQL.AppendFormat(" AND {0} = '1'", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB)

            ' 削除フラグ
            strSQL.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON)

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:GetDataSet】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaRirekiFZYEntity = m_csDataSchma.Clone()
            csAtenaRirekiFZYEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiFZYEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaRirekiFZYEntity

    End Function
#End Region

#Region "宛名履歴付随マスタ追加　[InsertAtenaFZYRB]"
    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　宛名履歴付随マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer        '追加件数

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId   ' 端末ＩＤ
            csDataRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = SAKUJOFG_OFF                      ' 削除フラグ
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF            ' 更新カウンタ
            csDataRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    ' 作成ユーザー
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId     ' 更新ユーザー

            '作成日時、更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

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
                strParamName = String.Format("{0}{1}", ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})", _
                                           ABAtenaRirekiFZYEntity.TABLE_NAME, _
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray), _
                                           csInsertParam.ToString.TrimEnd(",".ToCharArray))

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

#Region "宛名履歴付随マスタ更新　[UpdateAtenaFZYRB]"
    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　宛名履歴付随マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intUpdCnt As Integer                            '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)) + 1     '更新カウンタ
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

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
            m_strUpdateSQL = "UPDATE " + ABAtenaRirekiFZYEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・履歴番号・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.RIREKINO) AndAlso _
                     Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.SAKUSEIUSER) AndAlso _
                      Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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

#Region "宛名履歴付随マスタ削除　[DeleteAtenaFZYRB]"
    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ削除
    '* 
    '* 構文           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　宛名履歴付随マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intDelCnt As Integer                            '削除件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse _
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = SAKUJOFG_ON                                                        '削除フラグ
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)) + 1     '更新カウンタ
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")

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
    '* メソッド名     宛名履歴付随マスタ物理削除
    '* 
    '* 構文           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow, _
    '*                                              ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　　宛名履歴付随マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow, _
                                            ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        Dim cfParam As UFParameterClass                     ' パラメータクラス
        Dim intDelCnt As Integer                            ' 削除件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除区分のチェックを行う
            If Not (strSakujoKB = "D") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or _
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '処理なし
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    '処理なし
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")

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
    '* 履歴番号 000022 2005/11/18 追加開始
    '************************************************************************************************
    '* メソッド名     宛名履歴付随マスタ物理削除(１住民コード指定)
    '* 
    '* 構文           Public Overloads Function DeleteAtenaFZYRB(ByVal strJuminCD As String) As Integer
    '* 
    '* 機能　　    　　宛名履歴付随マスタのデータを物理削除する
    '* 
    '* 引数           strJuminCD As String : 削除する対象となる住民コード
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal strJuminCD As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim intDelCnt As Integer                            ' 削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If (m_strDelFromJuminCDSQL Is Nothing OrElse m_strDelFromJuminCDSQL = String.Empty OrElse _
                    IsNothing(m_cfDelFromJuminCDPrmCollection)) Then
                Call CreateDelFromJuminCDSQL()
            Else
                '処理なし
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            m_cfDelFromJuminCDPrmCollection(ABAtenaRirekiFZYEntity.KEY_JUMINCD).Value = strJuminCD

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection)

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
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where文の追加
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass


            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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
    '* 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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
    '************************************************************************************************
    '* メソッド名     物理削除用(１住民ＣＤ指定)SQL文の作成
    '* 
    '* 構文           Private Sub CreateDelFromJuminCDSQL()
    '* 
    '* 機能           住民ＣＤで該当全履歴データを物理削除するSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateDelFromJuminCDSQL()
        Const THIS_METHOD_NAME As String = "CreateDelFromJuminCDSQL"
        Dim csWhere As StringBuilder                        'WHERE定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)

            ' 物理DELETE(１住民ＣＤ指定) SQL文の作成
            m_strDelFromJuminCDSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString

            ' 物理削除用コレクションにパラメータを追加
            m_cfDelFromJuminCDPrmCollection = New UFParameterCollectionClass
            m_cfDelFromJuminCDPrmCollection.Add(ABAtenaRirekiFZYEntity.KEY_JUMINCD, DbType.String)

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

#Region "その他"
    '************************************************************************************************
    '* メソッド名     更新日時設定
    '* 
    '* 構文           Private Sub SetUpdateDatetime()
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
            If (IsDBNull(csDate)) OrElse (CType(csDate, String).Trim.Equals(String.Empty)) Then
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
