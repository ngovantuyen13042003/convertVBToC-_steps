'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名累積付随マスタＤＡ
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2011/10/24　小松　知尚
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
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
'* 宛名累積付随マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaRuisekiFZYBClass
#Region "メンバ変数"
    'パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_csDataSchma As DataSet                        'スキーマ保管用データセット
    Private m_strUpdateDatetime As String                   ' 更新日時

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaRuisekiFZYBClass"                ' クラス名
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
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
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
    End Sub
#End Region

#Region "メソッド"
#Region "宛名累積付随マスタ抽出"
    '************************************************************************************************
    '* メソッド名    宛名累積付随マスタ抽出
    '* 
    '* 構文          Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* 機能　　    　宛名累積付随マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strRrkNo          : 履歴番号
    '*                strShoriYMD       : 処理日時
    '*                strZengoKB        : 前後区分
    '* 
    '* 戻り値         DataSet : 取得した宛名累積付随マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaFZYRBHoshu(ByVal strJuminCD As String, _
                                                 ByVal strRrkNo As String, _
                                                 ByVal strShoriYMD As String, _
                                                 ByVal strZengoKB As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaFZYRBHoshu"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaRuisekiEntity As DataSet                  '宛名累積データセット
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
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiFZYEntity.TABLE_NAME)

            'ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRuisekiFZYEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strJuminCD, strRrkNo, strShoriYMD, strZengoKB))

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
            ' SQLの実行 DataSetの取得
            csAtenaRuisekiEntity = m_csDataSchma.Clone()
            csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRuisekiEntity, ABAtenaRuisekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaRuisekiEntity

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiFZYEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KYUSHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SHORINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZENGOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TABLEINSERTKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.LINKNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUMINHYOJOTAIKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKYOCHITODOKEFLG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.HONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANAHONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANJITSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KATAKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.UMAREFUSHOKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TSUSHOMEITOUROKUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUKIKANCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUKIKANMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUSHACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUSHAMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUCARDNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYOTEISTYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYOTEIEDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.FRNSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.FRNSTAINUSKANAMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSKANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE6)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE7)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE8)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE9)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE10)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINUSER)

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
                                 ByVal strShoriYMD As String, _
                                 ByVal strZengoKB As String) As String
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiFZYEntity.JUMINCD, ABAtenaRuisekiFZYEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '履歴番号
            If (Not strRrkNo.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.RIREKINO, ABAtenaRuisekiFZYEntity.KEY_RIREKINO)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRrkNo
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            '処理日時
            If (Not strShoriYMD.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.SHORINICHIJI, ABAtenaRuisekiFZYEntity.KEY_SHORINICHIJI)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_SHORINICHIJI
                cfUFParameterClass.Value = strShoriYMD
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            '前後区分
            If (Not strZengoKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.ZENGOKB, ABAtenaRuisekiFZYEntity.KEY_ZENGOKB)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_ZENGOKB
                cfUFParameterClass.Value = strZengoKB
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
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

#Region "宛名累積付随マスタ追加　[InsertAtenaFZYRB]"
    '************************************************************************************************
    '* メソッド名     宛名累積付随マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　宛名累積付随マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            ' 追加件数

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaRuisekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' 端末ＩＤ
            csDataRow(ABAtenaRuisekiFZYEntity.SAKUJOFG) = SAKUJOFG_OFF                     ' 削除フラグ
            csDataRow(ABAtenaRuisekiFZYEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF           ' 更新カウンタ
            csDataRow(ABAtenaRuisekiFZYEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' 作成ユーザー
            csDataRow(ABAtenaRuisekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' 更新ユーザー

            '作成日時、更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaFZYEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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

        Return intInsCnt

    End Function
    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義
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
                strParamName = String.Format("{0}{1}", ABAtenaRuisekiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})", _
                                           ABAtenaRuisekiFZYEntity.TABLE_NAME, _
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
