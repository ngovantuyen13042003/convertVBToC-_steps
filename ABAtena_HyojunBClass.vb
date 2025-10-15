'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名_標準マスタＤＡ(ABAtena_HyojunBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/08/14 早崎  雄矢
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2023/12/11  000001     【AB-9000-1】住基更新連携標準化対応(下村)
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
'* 宛名_標準マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtena_HyojunBClass
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
    Private Const THIS_CLASS_NAME As String = "ABAtena_HyojunBClass"                ' クラス名
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
    '* メソッド名     宛名_標準マスタ抽出
    '* 
    '* 構文           Public Function GetAtenaHyojunBHoshu(ByVal strJuminCD As String, _
    '*                                                     ByVal strJuminJutogaiKB As String, _
    '*                                                     ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　 宛名_標準マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD         : 住民コード 
    '*                strJuminJutogaiKB  : 住民住登外区分
    '*                blnSakujoFG        : 削除フラグ
    '* 
    '* 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
    '************************************************************************************************
    Public Function GetAtenaHyojunBHoshu(ByVal strJuminCD As String,
                                         ByVal strJuminJutogaiKB As String,
                                         ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetAtenaHyojunBHoshu"
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
            csSQL.AppendFormat(" FROM {0} ", ABAtenaHyojunEntity.TABLE_NAME)
            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            csSQL.Append(Me.CreateWhere(strJuminCD, strJuminJutogaiKB, blnSakujoFG))

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString( _
            '                                csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity,
                                                    ABAtenaHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaHyojunEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RRKNO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.EDANO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.UMAREBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOUMAREBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JIJITSUSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.BANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KUNIMEICODE)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KUNIMEITO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HON_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CKINIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOCKINIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOTOROKUIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KISAIYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KISAIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKISAIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOJUTEIIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOJOIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOSHOJOIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_YUBINNO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAISHUJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKAISEIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEISHOJOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.CHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOKUBETSUYOSHIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.IDOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NYURYOKUBASHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NYURYOKUBASHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKANJIKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SEARCHKANAKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TDKDSHIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.HYOJUNIDOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOROKUBUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TANKITAIZAISHAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KYOYUNINZU)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHIZEIJIMUSHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.IDOSHURUI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SHOKANKUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TOGOATENAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOUMAREBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIKANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKICHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaHyojunEntity.KOSHINUSER)

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
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strJuminJutogaiKB As String,
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaHyojunEntity.JUMINCD, ABAtenaHyojunEntity.KEY_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 住民住登外区分
            If (Not strJuminJutogaiKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaHyojunEntity.JUMINJUTOGAIKB, ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKB
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 削除フラグ
            If (blnSakujoFG = False) Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaHyojunEntity.SAKUJOFG, SAKUJOFG_ON)
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

#Region "宛名_標準マスタ追加"
    '************************************************************************************************
    '* メソッド名     宛名_標準マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名_標準マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaHyojunB"
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
            csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     '端末ＩＤ
            csDataRow(ABAtenaHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        '削除フラグ
            csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '更新カウンタ
            csDataRow(ABAtenaHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '作成ユーザー
            csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '更新ユーザー

            '作成日時、更新日時の設定
            Me.SetUpdateDatetime(csDataRow(ABAtenaHyojunEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(
                    ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
                strParamName = String.Format("{0}{1}", ABAtenaHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABAtenaHyojunEntity.TABLE_NAME,
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

#Region "宛名_標準マスタ更新"
    '************************************************************************************************
    '* メソッド名     宛名_標準マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名_標準マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow 　　: 更新するデータの含まれるDataRowオブジェクト
    '*                strAtenaDataKB As String : 宛名データ区分
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateAtenaHyojunB(ByVal csDataRow As DataRow, ByVal strAtenaDataKB As String) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaHyojunB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intUpdCnt As Integer                            '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strUpdateSQL Is Nothing) OrElse (m_strUpdateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateUpdateSQL(csDataRow, strAtenaDataKB)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) = ABAtenaHyojunEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    If cfParam.ParameterName <> ABAtenaHyojunEntity.KOSHINCOUNTER Then
                        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength),
                                     DataRowVersion.Original).ToString()
                    Else
                        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength),
                                     DataRowVersion.Original)
                    End If
                Else
                    'キー項目以外は編集内容取得
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(
                              ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
    '* 引数           csDataRow As DataRow 　　: 更新対象の行
    '*                strAtenaDataKB As String : 宛名データ区分
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow, ByVal strAtenaDataKB As String)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Const strAtenaDataKbn_Hojin As String = "13"
        Const strAtenaDataKbn_Kyoyu As String = "14"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE定義
        Dim csUpdateParam As StringBuilder                  'UPDATE用SQL定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABAtenaHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '以下の項目だけ更新する
                If ((csDataColumn.ColumnName = ABAtenaHyojunEntity.JUMINJUTOGAIKB) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.RRKNO) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.UMAREBIFUSHOPTN) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.FUSHOUMAREBI) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SHIKUCHOSONCD) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.MACHIAZACD) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.TODOFUKEN) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SHIKUCHOSON) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.MACHIAZA) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SEARCHJUSHO) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SEARCHKATAGAKI) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.BANCHIEDABANSUCHI) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.FUSHOSHOJOIDOBI) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.NYURYOKUBASHOCD) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.NYURYOKUBASHO) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.TANMATSUID) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.SAKUJOFG) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.KOSHINCOUNTER) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.KOSHINNICHIJI) OrElse
                     (csDataColumn.ColumnName = ABAtenaHyojunEntity.KOSHINUSER)) Then

                    '以下の条件の時はループの先頭に戻る
                    Select Case csDataColumn.ColumnName
                        Case ABAtenaHyojunEntity.RRKNO
                            '履歴番号
                            If (m_cfControlData.m_strMenuId = ABMenuIdCNST.MENU_ATENATOKUSHU_UPDATE) Then
                                'メニューID「AB09092」(特殊修正)の場合は更新しない
                                Continue For
                            End If
                        Case ABAtenaHyojunEntity.UMAREBIFUSHOPTN, ABAtenaHyojunEntity.FUSHOUMAREBI
                            '生年月日不詳パターン,不詳生年月日
                            If (strAtenaDataKB Is strAtenaDataKbn_Hojin OrElse strAtenaDataKB Is strAtenaDataKbn_Kyoyu) Then
                                '消除異動日不詳パターン
                                Continue For
                            End If
                    End Select

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB
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

#Region "宛名付随マスタ更新　[UpdateAtenaHyojunB]"
    '************************************************************************************************
    '* メソッド名     宛名付随マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaFZYB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名付随マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateAtenaHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaHyojunB"
        Dim cfParam As UFParameterClass                     'パラメータクラス
        Dim intUpdCnt As Integer                            '更新件数


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            Else
                '処理なし
            End If

            '共通項目の編集を行う
            csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) = ABAtenaHyojunEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    'キー項目以外は編集内容取得
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "【クラス名:" + Me.GetType.Name + "】" +
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
            '                            "【実行メソッド名:ExecuteSQL】" +
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

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
            m_strUpdateSQL = "UPDATE " + ABAtenaHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '住民ＣＤ・住民住登外区分・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABAtenaHyojunEntity.JUMINCD) AndAlso
                   Not (csDataColumn.ColumnName = ABAtenaHyojunEntity.JUMINJUTOGAIKB) AndAlso
                   Not (csDataColumn.ColumnName = ABAtenaHyojunEntity.SAKUSEIUSER) AndAlso
                   Not (csDataColumn.ColumnName = ABAtenaHyojunEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER
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

#Region "宛名_標準マスタ削除"
    '************************************************************************************************
    '* メソッド名     宛名_標準マスタ削除
    '* 
    '* 構文           Public Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名_標準マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaHyojunB"
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
            csDataRow(ABAtenaHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABAtenaHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                                                       '削除フラグ
            csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaHyojunEntity.KOSHINCOUNTER)) + 1       '更新カウンタ
            csDataRow(ABAtenaHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaHyojunEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) = ABAtenaHyojunEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    'キー項目以外は編集内容を設定
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                            ABAtenaHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
    '* メソッド名     宛名_標準マスタ物理削除
    '* 
    '* 構文           Public Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow, _
    '*                                                   ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　 宛名_標準マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow  : 削除するデータの含まれるDataRowオブジェクト
    '*                strSakujoKB As String : 削除フラグ
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteAtenaHyojunB(ByVal csDataRow As DataRow,
                                                 ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaHyojunB"
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
                If (cfParam.ParameterName.RSubstring(0, ABAtenaHyojunEntity.PREFIX_KEY.RLength) = ABAtenaHyojunEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

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
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

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
            csWhere.Append(ABAtenaHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER)

            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaHyojunEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where文の追加
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaHyojunEntity.KEY_KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaHyojunEntity.TABLE_NAME + csWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

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
