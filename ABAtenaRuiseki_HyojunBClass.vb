'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名累積_標準マスタＤＡ(ABAtenaRuiseki_HyojunBClass)
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
'* 宛名累積_標準マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaRuiseki_HyojunBClass
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
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' 論理削除用パラメータコレクション
    Private m_csDataSchma As DataSet                                                ' スキーマ保管用データセット
    Private m_strUpdateDatetime As String                                           ' 更新日時

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaRuiseki_HyojunBClass"         ' クラス名
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
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     宛名累積_標準マスタ抽出
    '* 
    '* 構文           Public Function GetABAtenaRuisekiHyojunBClassBHoshu(ByVal strJuminCD As String, _
    '*                                                                    ByVal strRirekiNO As String, _
    '*                                                                    ByVal strShoriNichiji As String, _
    '*                                                                    ByVal strZengoKB As String) As DataSet
    '* 
    '* 機能　　    　 宛名累積_標準マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD         : 住民コード 
    '*                strRirekiNO        : 履歴番号
    '*                strShoriNichiji    : 処理日時
    '*                strZengoKB         : 前後区分
    '* 
    '* 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
    '************************************************************************************************
    Public Function GetABAtenaRuisekiHyojunBClassBHoshu(ByVal strJuminCD As String,
                                                        ByVal strRirekiNO As String,
                                                        ByVal strShoriNichiji As String,
                                                        ByVal strZengoKB As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetABAtenaRuisekiHyojunBClassBHoshu"
        Dim cfErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaEntity As DataSet
        Dim csSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 住民コードが指定されていないときエラー
            If (IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0)) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' 例外を生成
                Throw New UFAppException(cfErrorStruct.m_strErrorMessage + ERR_JUMINCD, cfErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' SELECT句の生成
            csSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            csSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiHyojunEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaRuisekiHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            csSQL.Append(Me.CreateWhere(strJuminCD, strRirekiNO, strShoriNichiji, strZengoKB))

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:GetDataSet】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity,
                                                    ABAtenaRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiHyojunEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.EDANO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIMEIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.UMAREBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JIJITSUSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.BANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEICODE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEITO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUGUNCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOTOROKUIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNKISAIJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKISAIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUTEIIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOJUTEIIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNSHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOKUSEKISOSHITSUBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_YUBINNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEISHOJOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOKUBETSUYOSHIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANJIKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANAKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYUUJIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TDKDSHIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNIDOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NICHIJOSEIKATSUKENIKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUBUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANKITAIZAISHAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYOYUNINZU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIZEIJIMUSHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ST)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ED)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOSHURUI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOKANKUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOGOATENAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIKANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIBANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINUSER)

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

        Return csSELECT.ToString

    End Function

    '************************************************************************************************
    '* メソッド名     WHERE文の作成
    '* 
    '* 構文           Private Function CreateWhere(ByVal strJuminCD As String, _
    '*                                             ByVal strRirekiNO As String, _
    '*                                             ByVal strShoriNichiji As String, _
    '*                                             ByVal strZengoKB As String) As String
    '* 
    '* 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数           strJuminCD         : 住民コード 
    '*                strRirekiNO        : 履歴番号
    '*                strShoriNichiji    : 処理日時
    '*                strZengoKB         : 前後区分
    '*
    '* 戻り値         なし
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strRirekiNO As String,
                                 ByVal strShoriNichiji As String,
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiHyojunEntity.JUMINCD, ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 履歴番号
            If (Not strRirekiNO.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.RIREKINO, ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRirekiNO
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 処理日時
            If (Not strShoriNichiji.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI, ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI
                cfUFParameterClass.Value = strShoriNichiji
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 前後区分
            If (Not strZengoKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.ZENGOKB, ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB
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

        Return csWHERE.ToString

    End Function

#Region "宛名累積_標準マスタ追加"
    '************************************************************************************************
    '* メソッド名     宛名累積_標準マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名累積_標準マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRuisekiHyojunB"
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
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     '端末ＩＤ
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        '削除フラグ
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '更新カウンタ
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '作成ユーザー
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '更新ユーザー

            '作成日時、更新日時の設定
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(
                    ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                            m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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
                strParamName = String.Format("{0}{1}", ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABAtenaRuisekiHyojunEntity.TABLE_NAME,
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

#Region "宛名累積_標準マスタ更新"
    '************************************************************************************************
    '* メソッド名     宛名累積_標準マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名累積_標準マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRuisekiHyojunB"
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
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId   '端末ＩＤ
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1                  '更新カウンタ
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRuisekiHyojunEntity.PREFIX_KEY) Then

                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    'キー項目以外は編集内容取得
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(
                              ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

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
            m_strUpdateSQL = "UPDATE " + ABAtenaRuisekiHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・履歴番号・処理日時・前後区分・作成日時・作成ユーザは更新しない
                If (Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.RIREKINO) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SHORINICHIJI) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.ZENGOKB) AndAlso
                     Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) AndAlso
                      Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI)) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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

#Region "宛名累積_標準マスタ削除"
    '************************************************************************************************
    '* メソッド名     宛名累積_標準マスタ削除
    '* 
    '* 構文           Public Function DeleteAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名累積_標準マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Function DeleteAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaRuisekiHyojunB"
        Dim cfParam As UFParameterClass  'パラメータクラス
        Dim intDelCnt As Integer        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId      '端末ＩＤ
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                          '削除フラグ
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1                     '更新カウンタ
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId        '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRuisekiHyojunEntity.PREFIX_KEY) Then

                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    'キー項目以外は編集内容を設定
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                            ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

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
            csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER)

            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where文の追加
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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
