'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ支援措置対象者マスタＤＡ(ABShienSochiTaishoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/10/13　下村　美江
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
'* 2024/01/18   000001    【AB-0070-1】 支援措置通知書標準化対応
'* 2024/03/07   000002   【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
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

Public Class ABShienSochiTaishoBClass
#Region "メンバ変数"
    ' パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_strUpdateSQL As String                        ' UPDATE用SQL
    Private m_strDelRonriSQL As String                      ' 論理削除用SQL
    Private m_strDelButuriSQL As String                     ' 物理削除用SQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '論理削除用パラメータコレクション
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '物理削除用パラメータコレクション
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット
    Private m_strUpdateDatetime As String                   ' 更新日時

    Public m_blnBatch As Boolean = False               'バッチフラグ
    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABShienSochiTaishoBClass"                     ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                   ' 業務コード

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Const ERR_JUMINCD As String = "住民コード"
    Private Const ERR_SHIENSOCHIKANRINO As String = "支援措置管理番号"

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
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
#Region "支援措置対象者マスタ抽出　[GetShienSochiTaisho]"
    '************************************************************************************************
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaisho As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strShienSochiKanriNo : 支援措置管理番号 
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo As String) As DataSet

        Return Me.GetShienSochiTaisho(strShienSochiKanriNo, False)

    End Function
    '************************************************************************************************
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaisho As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strShienSochiKanriNo : 支援措置管理番号
    '*               blnSakujoFG        : 削除フラグ
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo As String,
                                                  ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiTaisho"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 支援措置管理番号が指定されていないときエラー
            If IsNothing(strShienSochiKanriNo) OrElse (strShienSochiKanriNo.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME)

            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strShienSochiKanriNo, 0, blnSakujoFG))
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            strSQL.AppendFormat(", {0}", ABShienSochiTaishoEntity.RENBAN)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaisho As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strShienSochiKanriNo : 支援措置管理番号 
    '*               intRenban            : 連番
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo As String,
                                                  ByVal intRenban As Integer) As DataSet

        Return Me.GetShienSochiTaisho(strShienSochiKanriNo, intRenban, False)

    End Function
    '************************************************************************************************
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaisho As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strShienSochiKanriNo : 支援措置管理番号
    '*               intRenban            : 連番
    '*               blnSakujoFG        : 削除フラグ
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo As String,
                                                  ByVal intRenban As Integer,
                                                  ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiTaisho"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 支援措置管理番号が指定されていないときエラー
            If IsNothing(strShienSochiKanriNo) OrElse (strShienSochiKanriNo.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode)
            Else
                '処理なし
            End If

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME)

            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhere(strShienSochiKanriNo, intRenban, blnSakujoFG))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
    '* メソッド名    支援措置対象者抽出
    '* 
    '* 構文          Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo() As String) As DataSet
    '* 
    '* 機能　　    　支援措置管理番号より該当データを取得する
    '* 
    '* 引数          strShienSochiKanriNo : 支援措置管理番号の配列       
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者の該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaisho(ByVal strShienSochiKanriNo() As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiTaisho"
        Dim csShienSochitaishoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfParameter As UFParameterClass
        Dim strParameterName As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME)

            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            If (strShienSochiKanriNo.Length = 0) Then
                csShienSochitaishoEntity = m_csDataSchma.Clone()
            Else
                With strSQL
                    .Append(" WHERE ")
                    .Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
                    .Append(" IN (")

                    For i As Integer = 0 To strShienSochiKanriNo.Length - 1
                        ' -----------------------------------------------------------------------------
                        ' 支援措置管理番号
                        strParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO + i.ToString

                        If (i > 0) Then
                            .AppendFormat(", {0}", strParameterName)
                        Else
                            .Append(strParameterName)
                        End If

                        cfParameter = New UFParameterClass
                        cfParameter.ParameterName = strParameterName
                        cfParameter.Value = strShienSochiKanriNo(i)
                        m_cfSelectUFParameterCollectionClass.Add(cfParameter)
                        ' -----------------------------------------------------------------------------
                    Next i

                    .Append(")")
                    .Append(" AND ")
                    .Append(ABShienSochiTaishoEntity.SAKUJOFG)
                    .Append(" <> '1'")

                End With

                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:GetDataSet】" +
                                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                ' SQLの実行 DataSetの取得
                csShienSochitaishoEntity = m_csDataSchma.Clone()
                csShienSochitaishoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochitaishoEntity,
                                                                   ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csShienSochitaishoEntity

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
            csSELECT.AppendFormat("SELECT {0}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RENBAN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MOSHIDEJOKYOKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TAISHOSHAKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TAISHOSHAKANKEI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.YUBINNO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_KANNAIKANGAIKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_JUSHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.JUSHO_JUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.BANCHI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KATAGAKICD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUKIDAICHOETSURAN_GENJUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUMINHYOUTSUSHIKOFU_GENJUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_JUMINHYOUTSUSHIKOFU_ZENJUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TENSHUTSUKAKUTEI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TENSHUTSUYOTEI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJUSHO_TOGOKISAIRAN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_JUSHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_JUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_BANCHI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAE_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOSEKIFUHYOUTSUSHIKOFU_HONSEKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_JUSHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_JUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_SHIKUGUNCHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HON_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.HONSEKIBANCHI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOSEKIFUHYOUTSUSHIKOFU_ZENHONSEKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_JUSHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_JUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_SHIKUGUNCHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHON_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.MAEHONSEKIBANCHI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SHIENJIMUTAISHOKB_KOTEISHISAN)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD1)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON1)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD2)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON2)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD3)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON3)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD4)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON4)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSONCD5)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOTEI_SHICHOSON5)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiTaishoEntity.KOSHINUSER)

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
    '* メソッド名   WHERE文の作成
    '* 
    '* 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能         WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数         strShienSochiKanriNo : 支援措置管理番号 
    '*              intRenban            : 連番
    '*              blnSakujoFG          : 削除フラグ
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Function CreateWhere(ByVal strShienSochiKanriNo As String,
                                 ByVal intRenban As Integer,
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

            ' 支援措置管理番号
            csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO, ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO
            cfUFParameterClass.Value = strShienSochiKanriNo
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 連番
            If (Not intRenban = 0) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiTaishoEntity.RENBAN, ABShienSochiTaishoEntity.KEY_RENBAN)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN
                cfUFParameterClass.Value = intRenban.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 削除フラグ
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiTaishoEntity.SAKUJOFG, SAKUJOFG_ON)
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

    '************************************************************************************************
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaishoByJuminCD() As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          a_strJuminCd()       : 住民コードの配列
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaishoByJuminCD(ByVal a_strJuminCd() As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiTaishoByJuminCD"
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim strParameterName As String
        Dim cfParameter As UFParameterClass

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass
            ' SELECT句の生成
            strSQL.Append(Me.CreateSelect)
            ' FROM句の生成
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME)

            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            With strSQL
                .Append(" WHERE ")
                .Append(ABShienSochiTaishoEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To a_strJuminCd.Length - 1
                    ' -----------------------------------------------------------------------------
                    ' 住民コード
                    strParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD + i.ToString

                    If (i > 0) Then
                        .AppendFormat(", {0}", strParameterName)
                    Else
                        .Append(strParameterName)
                    End If

                    cfParameter = New UFParameterClass
                    cfParameter.ParameterName = strParameterName
                    cfParameter.Value = a_strJuminCd(i)
                    m_cfSelectUFParameterCollectionClass.Add(cfParameter)
                    ' -----------------------------------------------------------------------------
                Next i
                .Append(")")

            End With

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaishoByJuminCD As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strJuminCD : 住民コード 
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaishoByJuminCD(ByVal strJuminCD As String) As DataSet

        Return Me.GetShienSochiTaishoByJuminCD(strJuminCD, String.Empty, False)

    End Function
    '************************************************************************************************
    '* メソッド名    支援措置対象者マスタ抽出
    '* 
    '* 構文          Public Function GetShienSochiTaishoByJuminCD() As DataSet
    '* 
    '* 機能　　    　支援措置対象者マスタより該当データを取得する
    '* 
    '* 引数          strJuminCD           : 住民コード
    '*               strShienSochiKanriNo : 支援措置管理番号
    '*               blnSakujoFG          : 削除フラグ
    '* 
    '* 戻り値        DataSet : 取得した支援措置対象者マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetShienSochiTaishoByJuminCD(ByVal strJuminCd As String,
                                                           ByVal strShienSochiKanriNo As String,
                                                           ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiTaishoByJuminCD"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            ' 住民コードが指定されていないときエラー
            If IsNothing(strJuminCd) OrElse (strJuminCd.Trim.RLength = 0) Then
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
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiTaishoEntity.TABLE_NAME)

            ' ﾃﾞｰﾀｽｷｰﾏの取得
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiTaishoEntity.TABLE_NAME, False)
            End If

            ' WHERE句の作成
            strSQL.Append(Me.CreateWhereJuminCd(strJuminCd, strShienSochiKanriNo, blnSakujoFG))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiTaishoEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
    '* メソッド名   WHERE文の作成
    '* 
    '* 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能         WHERE分を作成、パラメータコレクションを作成する
    '* 
    '* 引数         strJuminCD           : 住民コード 
    '*              strShienSochiKanriNo : 支援措置管理番号
    '*              blnSakujoFG          : 削除フラグ
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Function CreateWhereJuminCD(ByVal strJuminCD As String,
                                        ByVal strShienSochiKanriNo As String,
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiTaishoEntity.JUMINCD, ABShienSochiTaishoEntity.PARAM_JUMINCD)
            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 支援措置管理番号
            If (Not strShienSochiKanriNo = String.Empty) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiTaishoEntity.SHIENSOCHIKANRINO, ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO
                cfUFParameterClass.Value = strShienSochiKanriNo
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '処理なし
            End If

            ' 削除フラグ
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiTaishoEntity.SAKUJOFG, SAKUJOFG_ON)
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
#End Region

#Region "支援措置対象者マスタ追加　[InsertShienSochiTaisho]"
    '************************************************************************************************
    '* メソッド名     支援措置対象者マスタ追加
    '* 
    '* 構文           Public Function InsertShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　支援措置対象者マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertShienSochiTaisho(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertShienSochiTaisho"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            '追加件数

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '処理なし
            End If

            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            '共通項目の編集を行う
            csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId     '端末ＩＤ
            csDataRow(ABShienSochiTaishoEntity.SAKUJOFG) = SAKUJOFG_OFF                        '削除フラグ
            csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '更新カウンタ
            csDataRow(ABShienSochiTaishoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '作成ユーザー
            csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '更新ユーザー

            '作成日時、更新日時の設定
            Me.SetUpdateDatetime(csDataRow(ABShienSochiTaishoEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI))

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
                strParamName = String.Format("{0}{1}", ABShienSochiTaishoEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL文の作成
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABShienSochiTaishoEntity.TABLE_NAME,
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray),
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

#Region "支援措置対象者マスタ更新　[UpdateShienSochiTaisho]"
    '************************************************************************************************
    '* メソッド名     支援措置対象者マスタ更新
    '* 
    '* 構文           Public Function UpdateShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　 支援措置対象者マスタのデータを更新する
    '* 
    '* 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 更新したデータの件数
    '************************************************************************************************
    Public Function UpdateShienSochiTaisho(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateShienSochiTaisho"
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
            csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER)) + 1  '更新カウンタ
            csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI) = m_strUpdateDatetime

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) = ABShienSochiTaishoEntity.PREFIX_KEY) Then
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    'キー項目以外は編集内容取得
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
            m_strUpdateSQL = "UPDATE " + ABShienSochiTaishoEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE文の作成
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                '支援措置管理番号・連番・作成日時・作成ユーザは更新しない
                If Not (csDataColumn.ColumnName = ABShienSochiTaishoEntity.SHIENSOCHIKANRINO) AndAlso
                    Not (csDataColumn.ColumnName = ABShienSochiTaishoEntity.RENBAN) AndAlso
                     Not (csDataColumn.ColumnName = ABShienSochiTaishoEntity.SAKUSEIUSER) AndAlso
                      Not (csDataColumn.ColumnName = ABShienSochiTaishoEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL文の作成
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER
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

#Region "支援措置対象者マスタ削除　[DeleteShienSochiTaisho]"
    '************************************************************************************************
    '* メソッド名     支援措置対象者マスタ削除
    '* 
    '* 構文           Public Function DeleteShienSochiTaisho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　支援措置対象者マスタのデータを論理削除する
    '* 
    '* 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 論理削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteShienSochiTaisho(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShienSochiTaisho"
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
            csDataRow(ABShienSochiTaishoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '端末ＩＤ
            csDataRow(ABShienSochiTaishoEntity.SAKUJOFG) = SAKUJOFG_ON                                                       '削除フラグ
            csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiTaishoEntity.KOSHINCOUNTER)) + 1  '更新カウンタ
            csDataRow(ABShienSochiTaishoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '更新ユーザー

            '更新日時の設定
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABShienSochiTaishoEntity.KOSHINNICHIJI))

            '作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                'キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) = ABShienSochiTaishoEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    'キー項目以外は編集内容を設定
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
    '* メソッド名     支援措置対象者物理削除
    '* 
    '* 構文           Public Function DeleteShiensochiTaisho(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　　支援措置対象者マスタのデータを物理削除する
    '* 
    '* 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 削除したデータの件数
    '************************************************************************************************
    Public Overloads Function DeleteShiensochiTaisho(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShiensochiTaisho"
        Dim objErrorStruct As UFErrorStruct 'エラー定義構造体
        Dim cfParam As UFParameterClass     'パラメータクラス
        Dim intDelCnt As Integer            '削除件数


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
            If (m_strDelButuriSQL Is Nothing OrElse m_strDelButuriSQL = String.Empty OrElse
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '処理なし
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiTaishoEntity.PREFIX_KEY.RLength) = ABShienSochiTaishoEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiTaishoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                    'キー項目以外の取得なし
                Else
                    '処理なし
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:ExecuteSQL】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
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
            csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER)


            ' 論理DELETE SQL文の作成
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiTaishoEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where文の追加
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' 論理削除用パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 論理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiTaishoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDelButuriSQL = "DELETE FROM " + ABShienSochiTaishoEntity.TABLE_NAME + csWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_RENBAN
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.KEY_KOSHINCOUNTER
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
