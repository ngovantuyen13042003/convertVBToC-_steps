'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ連絡先マスタ３ビジネスクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2018/05/22　石合　亮
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2018/05/22   000000      【AB24011】新規作成（石合）
'* 2024/01/11   000001      【AB-0860-1】連絡先管理項目追加
'* 2024/03/07   000002      【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
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

''' <summary>
''' ＡＢ連絡先マスタ３ビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABRenrakusaki3BClass

#Region "メンバー変数"

    ' メンバー変数
    Private m_cfLogClass As UFLogClass                                              ' ログ出力クラス
    Private m_cfControlData As UFControlData                                        ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                                ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                              ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                                          ' エラー処理クラス
    Private m_cfErrorStruct As UFErrorStruct                                        ' エラー情報構造体

    Private m_strSelectSQL As String                                                ' SELECT用SQL
    Private m_cfSelectParamCollection As UFParameterCollectionClass                 ' SELECT用パラメータコレクション

    Private m_blnIsCreateSelectSQL As Boolean                                       ' SELECT用SQL作成済みフラグ

    Private m_csDataSchema As DataSet                                               ' スキーマ保管用データセット

    Private m_cRenrakusakiFZYB As ABRenrakusakiFZYBClass                            ' ＡＢ連絡先付随マスタビジネスクラス

    '*履歴番号 000001 2024/01/11 追加開始
    Private m_cRenrakusakiFZYHyojunB As ABRenrakusakiFZYHyojunBClass                ' ＡＢ連絡先付随_標準マスタビジネスクラス
    '*履歴番号 000001 2024/01/11 追加終了

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABRenrakusaki3BClass"                ' クラス名

    Private Shared ReadOnly SQL_SAKUJOFG As String = String.Format("{0}.{1} = '0'", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUJOFG)

    Public Const SUFFIX_JOIN As String = "_JOIN"                                    ' 結合用サフィックス
    Public Const SUFFIX_FZY As String = "_FZY"                                      ' 付随用サフィックス
    '*履歴番号 000001 2024/01/11 追加開始
    Public Const SUFFIX_FZY_HYOJUN As String = "_FZY_HYOJUN"                        ' 付随標準用サフィックス
    '*履歴番号 000001 2024/01/11 追加終了

#End Region

#Region "プロパティー"

#End Region

#Region "コンストラクター"

    ''' <summary>
    ''' コンストラクター
    ''' </summary>
    ''' <param name="cfControlData">コントロールデータ</param>
    ''' <param name="cfConfigDataClass">コンフィグデータ</param>
    ''' <param name="cfRdbClass">ＲＤＢクラス</param>
    ''' <remarks></remarks>
    Public Sub New( _
        ByVal cfControlData As UFControlData, _
        ByVal cfConfigDataClass As UFConfigDataClass, _
        ByVal cfRdbClass As UFRdbClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' パラメーター変数の初期化
        m_strSelectSQL = String.Empty
        m_cfSelectParamCollection = Nothing

        ' SQL作成済みフラグの初期化
        m_blnIsCreateSelectSQL = False

        ' スキーマ保管用データセットの初期化
        m_csDataSchema = Nothing

        ' ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
        m_cRenrakusakiFZYB = New ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

    End Sub

#End Region

#Region "メソッド"

#Region "GetRenrakusakiTableSchema"

    ''' <summary>
    ''' GetRenrakusakiTableSchema
    ''' </summary>
    ''' <returns>テーブルスキーマ</returns>
    ''' <remarks></remarks>
    Public Function GetRenrakusakiTableSchema() As DataSet

        Dim csRenrakusakiEntity As DataSet

        Try

            ' スキーマの取得
            csRenrakusakiEntity = m_cfRdbClass.GetTableSchemaNoRestriction(String.Format("SELECT * FROM {0}", ABRenrakusakiEntity.TABLE_NAME), ABRenrakusakiEntity.TABLE_NAME, False)

        Catch csExp As Exception
            Throw
        End Try

        Return csRenrakusakiEntity

    End Function

#End Region

#Region "GetRenrakusakiFZYTableSchema"

    ''' <summary>
    ''' GetRenrakusakiFZYTableSchema
    ''' </summary>
    ''' <returns>テーブルスキーマ</returns>
    ''' <remarks></remarks>
    Public Function GetRenrakusakiFZYTableSchema() As DataSet

        Dim csRenrakusakiFZYEntity As DataSet

        Try

            ' スキーマの取得
            csRenrakusakiFZYEntity = m_cfRdbClass.GetTableSchemaNoRestriction(String.Format("SELECT * FROM {0}", ABRenrakusakiFZYEntity.TABLE_NAME), ABRenrakusakiFZYEntity.TABLE_NAME, False)

        Catch csExp As Exception
            Throw
        End Try

        Return csRenrakusakiFZYEntity

    End Function

#End Region

'*履歴番号 000001 2024/01/11 追加開始
#Region "GetRenrakusakiFZYHyojunTableSchema"

    ''' <summary>
    ''' GetRenrakusakiFZYHyojunTableSchema
    ''' </summary>
    ''' <returns>テーブルスキーマ</returns>
    ''' <remarks></remarks>
    Public Function GetRenrakusakiFZYHyojunTableSchema() As DataSet

        Dim csRenrakusakiFZYHyojunEntity As DataSet

        Try

            ' スキーマの取得
            csRenrakusakiFZYHyojunEntity = m_cfRdbClass.GetTableSchemaNoRestriction(String.Format("SELECT * FROM {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME), ABRenrakusakiFZYHyojunEntity.TABLE_NAME, False)

        Catch csExp As Exception
            Throw
        End Try

        Return csRenrakusakiFZYHyojunEntity

    End Function

#End Region
'*履歴番号 000001 2024/01/11 追加終了

#Region "Select"

    ''' <summary>
    ''' Select
    ''' </summary>
    ''' <param name="strWhere">SQL文</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>※ABRenrakusaki2BClassの動きに準拠し、削除フラグを考慮しない。</remarks>
    Private Overloads Function [Select]( _
        ByVal strWhere As String, _
        ByVal cfParamCollection As UFParameterCollectionClass) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim strSQL As String
        Dim csResultEntity As DataSet
        Dim csRenrakusakiJoinEntity As DataSet
        Dim csRenrakusakiEntity As DataSet
        Dim csRenrakusakiFZYEntity As DataSet
        '*履歴番号 000001 2024/01/11 追加開始
        Dim csRenrakusakiFZYHyojunEntity As DataSet
        '*履歴番号 000001 2024/01/11 追加終了

        Dim csNewRow As DataRow

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_blnIsCreateSelectSQL = False) Then

                Call CreateSelectSQL()

                m_blnIsCreateSelectSQL = True

            Else
                ' noop
            End If

            ' WHERE区の作成
            If (strWhere.Trim.RLength > 0) Then
                strSQL = String.Format(m_strSelectSQL, String.Concat(" WHERE ", strWhere))
            Else
                strSQL = String.Format(m_strSelectSQL, String.Empty)
            End If

            ' ＲＤＢアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiJoinEntity = m_csDataSchema.Clone()
            csRenrakusakiJoinEntity = m_cfRdbClass.GetDataSet(strSQL, csRenrakusakiJoinEntity, String.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN), cfParamCollection, False)

            ' 取得結果を分割
            csResultEntity = New DataSet
            csRenrakusakiEntity = Me.GetRenrakusakiTableSchema()
            csResultEntity.Tables.Add(csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Clone)
            csRenrakusakiFZYEntity = Me.GetRenrakusakiFZYTableSchema()
            csResultEntity.Tables.Add(csRenrakusakiFZYEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Clone)
            '*履歴番号 000001 2024/01/11 追加開始
            csRenrakusakiFZYHyojunEntity = Me.GetRenrakusakiFZYHyojunTableSchema()
            csResultEntity.Tables.Add(csRenrakusakiFZYHyojunEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Clone)
            '*履歴番号 000001 2024/01/11 追加終了

            For Each csDataRow As DataRow In csRenrakusakiJoinEntity.Tables(String.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN)).Rows

                csNewRow = csResultEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).NewRow
                With csNewRow
                    .BeginEdit()
                    .Item(ABRenrakusakiEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiEntity.JUMINCD)
                    .Item(ABRenrakusakiEntity.SHICHOSONCD) = csDataRow.Item(ABRenrakusakiEntity.SHICHOSONCD)
                    .Item(ABRenrakusakiEntity.KYUSHICHOSONCD) = csDataRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD)
                    .Item(ABRenrakusakiEntity.GYOMUCD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUCD)
                    .Item(ABRenrakusakiEntity.GYOMUNAISHU_CD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUNAISHU_CD)
                    '*履歴番号 000001 2024/01/11 追加開始
                    .Item(ABRenrakusakiEntity.TOROKURENBAN) = csDataRow.Item(ABRenrakusakiEntity.TOROKURENBAN)
                    '*履歴番号 000001 2024/01/11 追加終了
                    .Item(ABRenrakusakiEntity.RENRAKUSAKIKB) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKIKB)
                    .Item(ABRenrakusakiEntity.RENRAKUSAKIMEI) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKIMEI)
                    .Item(ABRenrakusakiEntity.RENRAKUSAKI1) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI1)
                    .Item(ABRenrakusakiEntity.RENRAKUSAKI2) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI2)
                    .Item(ABRenrakusakiEntity.RENRAKUSAKI3) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI3)
                    .Item(ABRenrakusakiEntity.RESERVE) = csDataRow.Item(ABRenrakusakiEntity.RESERVE)
                    .Item(ABRenrakusakiEntity.TANMATSUID) = csDataRow.Item(ABRenrakusakiEntity.TANMATSUID)
                    .Item(ABRenrakusakiEntity.SAKUJOFG) = csDataRow.Item(ABRenrakusakiEntity.SAKUJOFG)
                    .Item(ABRenrakusakiEntity.KOSHINCOUNTER) = csDataRow.Item(ABRenrakusakiEntity.KOSHINCOUNTER)
                    .Item(ABRenrakusakiEntity.SAKUSEINICHIJI) = csDataRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI)
                    .Item(ABRenrakusakiEntity.SAKUSEIUSER) = csDataRow.Item(ABRenrakusakiEntity.SAKUSEIUSER)
                    .Item(ABRenrakusakiEntity.KOSHINNICHIJI) = csDataRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI)
                    .Item(ABRenrakusakiEntity.KOSHINUSER) = csDataRow.Item(ABRenrakusakiEntity.KOSHINUSER)
                    .EndEdit()
                End With
                csResultEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Add(csNewRow)

                csNewRow = csResultEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).NewRow
                With csNewRow
                    .BeginEdit()
                    .Item(ABRenrakusakiFZYEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiEntity.JUMINCD)
                    .Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.SHICHOSONCD, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.KYUSHICHOSONCD, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.GYOMUCD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUCD)
                    .Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUNAISHU_CD)
                    '*履歴番号 000001 2024/01/11 追加開始
                    .Item(ABRenrakusakiFZYEntity.TOROKURENBAN) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.TOROKURENBAN, SUFFIX_FZY))
                    '*履歴番号 000001 2024/01/11 追加終了
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.RESERVE) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.RESERVE, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.TANMATSUID) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.TANMATSUID, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.SAKUJOFG, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.KOSHINCOUNTER, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.SAKUSEINICHIJI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.SAKUSEINICHIJI, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.SAKUSEIUSER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.SAKUSEIUSER, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.KOSHINNICHIJI, SUFFIX_FZY))
                    .Item(ABRenrakusakiFZYEntity.KOSHINUSER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYEntity.KOSHINUSER, SUFFIX_FZY))
                    .EndEdit()
                End With
                csResultEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Add(csNewRow)

                '*履歴番号 000001 2024/01/11 追加開始
                csNewRow = csResultEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).NewRow
                With csNewRow
                    .BeginEdit()
                    .Item(ABRenrakusakiFZYHyojunEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUCD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.TOROKUYMD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.BIKO, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE1, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE2, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE3, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE4, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE5, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.TANMATSUID) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.TANMATSUID, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.SAKUJOFG, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, SUFFIX_FZY_HYOJUN))
                    .Item(ABRenrakusakiFZYHyojunEntity.KOSHINUSER) = csDataRow.Item(String.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINUSER, SUFFIX_FZY_HYOJUN))
                    .EndEdit()
                End With
                csResultEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Add(csNewRow)
                '*履歴番号 000001 2024/01/11 追加終了

            Next csDataRow

            csResultEntity.AcceptChanges()

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        ' 抽出結果DataSetの返信
        Return csResultEntity

    End Function

    ''' <summary>
    ''' SelectByJuminCd
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <returns>抽出結果DataSet</returns>
    ''' <remarks>※ABRenrakusaki2BClassの動きに準拠し、削除フラグを考慮しない。</remarks>
    Public Overloads Function SelectByJuminCd( _
        ByVal strJuminCd As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim csSQL As StringBuilder
        Dim cfParam As UFParameterClass
        Dim csResultEntity As DataSet

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' パラメーターコレクションクラスのインスタンス化
            m_cfSelectParamCollection = New UFParameterCollectionClass

            With csSQL

                ' 住民コード
                .AppendFormat("{0}.{1} = {2} ", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiEntity.PARAM_JUMINCD)

                cfParam = New UFParameterClass
                cfParam.ParameterName = ABRenrakusakiEntity.PARAM_JUMINCD
                cfParam.Value = strJuminCd
                m_cfSelectParamCollection.Add(cfParam)

            End With

            ' 抽出処理を実行
            csResultEntity = Me.Select(csSQL.ToString(), m_cfSelectParamCollection)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        End Try

        ' 抽出結果DataSetの返信
        Return csResultEntity

    End Function

#End Region

#Region "CreateSelectSQL"

    ''' <summary>
    ''' CreateSelectSQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSelectSQL()

        Dim csSQL As StringBuilder

        Try

            ' SQL文字列変数のインスタンス化
            csSQL = New StringBuilder(256)

            ' SELECT区の生成
            csSQL.Append(Me.CreateSelect)

            ' FROM区の生成
            csSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME)
            '*履歴番号 000001 2024/01/11 修正開始
            'csSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME)
            csSQL.AppendFormat(" LEFT OUTER JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME)
            '*履歴番号 000001 2024/01/11 修正終了
            csSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD)
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD)
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
            '*履歴番号 000001 2024/01/11 追加開始
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN)
            csSQL.AppendFormat(" LEFT OUTER JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME)
            csSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD)
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD)
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD)
            csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN)
            '*履歴番号 000001 2024/01/11 追加終了

            ' スキーマの取得
            If (m_csDataSchema Is Nothing) Then
                m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), String.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN), False)
            Else
                ' noop
            End If

            ' WHERE区の作成
            csSQL.Append("{0}")

            ' ORDERBY区の生成
            csSQL.Append(" ORDER BY")
            csSQL.AppendFormat(" {0}.{1},", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD)
            csSQL.AppendFormat(" {0}.{1},", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD)
            csSQL.AppendFormat(" {0}.{1} ", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD)

            ' メンバー変数に設定
            m_strSelectSQL = csSQL.ToString()

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "CreateSelect"

    ''' <summary>
    ''' CreateSelect
    ''' </summary>
    ''' <returns>SELECT区</returns>
    ''' <remarks></remarks>
    Private Function CreateSelect() As String

        Dim csSQL As StringBuilder

        Try

            csSQL = New StringBuilder

            With csSQL

                .Append("SELECT ")
                .AppendFormat("  {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SHICHOSONCD)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD)
                '*履歴番号 000001 2024/01/11 追加開始
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN)
                '*履歴番号 000001 2024/01/11 追加終了
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKIKB)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKIMEI)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI1)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI2)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI3)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RESERVE)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TANMATSUID)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUJOFG)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUSEIUSER)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINUSER)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SHICHOSONCD, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KYUSHICHOSONCD, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, SUFFIX_FZY)
                '*履歴番号 000001 2024/01/11 追加開始
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN, SUFFIX_FZY)
                '*履歴番号 000001 2024/01/11 追加終了
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RESERVE, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TANMATSUID, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUJOFG, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINCOUNTER, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUSEINICHIJI, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUSEIUSER, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINNICHIJI, SUFFIX_FZY)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINUSER, SUFFIX_FZY)
                '*履歴番号 000001 2024/01/11 追加開始
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE1, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE2, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE3, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE4, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE5, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TANMATSUID, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUJOFG, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, SUFFIX_FZY_HYOJUN)
                .AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINUSER, SUFFIX_FZY_HYOJUN)
                '*履歴番号 000001 2024/01/11 追加終了

            End With

        Catch csExp As Exception
            Throw
        End Try

        Return csSQL.ToString

    End Function

#End Region

#Region "Update"

    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="csRenrakusakiRow">連絡先マスタ</param>
    ''' <param name="csRenrakusakiFZYRow">連絡先付随マスタ</param>
    ''' <remarks>※更新時も削除フラグを考慮しない。</remarks>
    Public Sub Update( _
        ByVal csRenrakusakiRow As DataRow, _
        ByVal csRenrakusakiFZYRow As DataRow)

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name        ' メソッド名
        Dim csDataSet As DataSet
        Dim csNewRow As DataRow
        Dim intKoshinCount As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
            If (m_cRenrakusakiFZYB Is Nothing) Then
                m_cRenrakusakiFZYB = New ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            Else
                ' noop
            End If

            ' キー情報で連絡先付随マスタを取得
            '*履歴番号 000001 2024/01/11 修正開始
            'csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD).ToString, _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD).ToString, _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD).ToString, _
            '                    True)
            csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN).ToString, _
                                True)
            '*履歴番号 000001 2024/01/11 修正終了

            If (csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows

                    ' データ編集
                    With csDataRow
                        .BeginEdit()
                        .Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SHICHOSONCD)
                        .Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO)
                        .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO)
                        .Item(ABRenrakusakiFZYEntity.RESERVE) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RESERVE)
                        .Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG)
                        .Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI)
                        .EndEdit()
                    End With

                    ' 連絡先付随マスタの更新処理
                    intKoshinCount = m_cRenrakusakiFZYB.Update(csDataRow)

                    ' 更新件数判定
                    If (intKoshinCount <> 1) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(String.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随マスタ"), m_cfErrorStruct.m_strErrorCode)
                    Else
                        ' noop
                    End If

                Next csDataRow

            Else

                ' データ編集
                csNewRow = csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).NewRow
                With csNewRow
                    .EndEdit()
                    .Item(ABRenrakusakiFZYEntity.JUMINCD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD)
                    .Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SHICHOSONCD)
                    .Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD)
                    .Item(ABRenrakusakiFZYEntity.GYOMUCD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD)
                    .Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
                    '*履歴番号 000001 2024/01/11 追加開始
                    .Item(ABRenrakusakiFZYEntity.TOROKURENBAN) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN)
                    '*履歴番号 000001 2024/01/11 追加終了
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO)
                    .Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO)
                    .Item(ABRenrakusakiFZYEntity.RESERVE) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RESERVE)
                    .Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG)
                    .Item(ABRenrakusakiFZYEntity.SAKUSEINICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI)
                    .Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI)
                    .EndEdit()
                End With
                csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Add(csNewRow)

                ' 連絡先付随マスタの追加処理
                m_cRenrakusakiFZYB.Insert(csNewRow)

            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfRdbDeadLockExp As UFRdbDeadLockException   ' デッドロックをキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp)

        Catch cfUFRdbUniqueExp As UFRdbUniqueException     ' 一意制約違反をキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp)

        Catch cfRdbTimeOutExp As UFRdbTimeOutException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

    End Sub

#End Region

'*履歴番号 000001 2024/01/11 追加開始
#Region "UpdateFZYHyojun"

    ''' <summary>
    ''' UpdateFZYHyojun
    ''' </summary>
    ''' <param name="csRenrakusakiRow">連絡先マスタ</param>
    ''' <param name="csRenrakusakiFZYHyojunRow">連絡先付随標準マスタ</param>
    ''' <remarks>※更新時も削除フラグを考慮しない。</remarks>
    Public Sub UpdateFZYHyojun( _
        ByVal csRenrakusakiRow As DataRow, _
        ByVal csRenrakusakiFZYHyojunRow As DataRow)

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name        ' メソッド名
        Dim csDataSet As DataSet
        Dim csNewRow As DataRow
        Dim intKoshinCount As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ＡＢ連絡先付随標準マスタビジネスクラスのインスタンス化
            If (m_cRenrakusakiFZYHyojunB Is Nothing) Then
                m_cRenrakusakiFZYHyojunB = New ABRenrakusakiFZYHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            Else
                ' noop
            End If

            ' キー情報で連絡先付随標準マスタを取得
            csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey( _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN).ToString, _
                                True)

            If (csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows

                    ' データ編集
                    With csDataRow
                        .BeginEdit()
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI)
                        .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI)
                        .Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO)
                        .Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1)
                        .Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2)
                        .Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3)
                        .Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4)
                        .Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5)
                        .Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG)
                        .Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI)

                        .EndEdit()
                    End With

                    ' 連絡先付随標準マスタの更新処理
                    intKoshinCount = m_cRenrakusakiFZYHyojunB.Update(csDataRow)

                    ' 更新件数判定
                    If (intKoshinCount <> 1) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(String.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随標準マスタ"), m_cfErrorStruct.m_strErrorCode)
                    Else
                        ' noop
                    End If

                Next csDataRow

            Else

                ' データ編集
                csNewRow = csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).NewRow
                With csNewRow
                    .EndEdit()
                    .Item(ABRenrakusakiFZYHyojunEntity.JUMINCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD)
                    .Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI)
                    .Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD)
                    .Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD)
                    .Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN)
                    .Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO)
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1)
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2)
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3)
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4)
                    .Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5)
                    .Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG)
                    .Item(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI)
                    .Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI)
                    .EndEdit()
                End With
                csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Add(csNewRow)

                ' 連絡先付随標準マスタの追加処理
                m_cRenrakusakiFZYHyojunB.Insert(csNewRow)

            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfRdbDeadLockExp As UFRdbDeadLockException   ' デッドロックをキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp)

        Catch cfUFRdbUniqueExp As UFRdbUniqueException     ' 一意制約違反をキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp)

        Catch cfRdbTimeOutExp As UFRdbTimeOutException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

    End Sub

#End Region
'*履歴番号 000001 2024/01/11 追加終了

#Region "Delete"

    ''' <summary>
    ''' Delete
    ''' </summary>
    ''' <param name="csRenrakusakiFZYRow">連絡先付随マスタ</param>
    ''' <remarks>※更新時も削除フラグを考慮しない。</remarks>
    Public Sub Delete( _
        ByVal csRenrakusakiFZYRow As DataRow)

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name        ' メソッド名
        Dim csDataSet As DataSet
        Dim intKoshinCount As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
            If (m_cRenrakusakiFZYB Is Nothing) Then
                m_cRenrakusakiFZYB = New ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            Else
                ' noop
            End If

            ' キー情報で連絡先付随マスタを取得
            '*履歴番号 000001 2024/01/11 修正開始
            'csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original).ToString, _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original).ToString, _
            '                    csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, _
            '                    True)
            csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN, DataRowVersion.Original).ToString, _
                                True)
            '*履歴番号 000001 2024/01/11 修正終了

            If (csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows

                    ' 連絡先付随マスタの物理削除処理
                    intKoshinCount = m_cRenrakusakiFZYB.Delete(csDataRow)

                    ' 更新件数判定
                    If (intKoshinCount <> 1) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(String.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随マスタ"), m_cfErrorStruct.m_strErrorCode)
                    Else
                        ' noop
                    End If

                Next csDataRow

            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfRdbDeadLockExp As UFRdbDeadLockException   ' デッドロックをキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp)

        Catch cfUFRdbUniqueExp As UFRdbUniqueException     ' 一意制約違反をキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp)

        Catch cfRdbTimeOutExp As UFRdbTimeOutException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

    End Sub

#End Region

'*履歴番号 000001 2024/01/11 追加開始
#Region "DeleteFzyHyojun"

    ''' <summary>
    ''' DeleteFzyHyojun
    ''' </summary>
    ''' <param name="csRenrakusakiFZYHyojunRow">連絡先付随標準マスタ</param>
    ''' <remarks>※更新時も削除フラグを考慮しない。</remarks>
    Public Sub DeleteFzyHyojun( _
        ByVal csRenrakusakiFZYHyojunRow As DataRow)

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name        ' メソッド名
        Dim csDataSet As DataSet
        Dim intKoshinCount As Integer

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ＡＢ連絡先付随標準マスタビジネスクラスのインスタンス化
            If (m_cRenrakusakiFZYHyojunB Is Nothing) Then
                m_cRenrakusakiFZYHyojunB = New ABRenrakusakiFZYHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            Else
                ' noop
            End If

            ' キー情報で連絡先付随標準マスタを取得
            csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey( _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, _
                                csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, DataRowVersion.Original).ToString, _
                                True)

            If (csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows

                    ' 連絡先付随標準マスタの物理削除処理
                    intKoshinCount = m_cRenrakusakiFZYHyojunB.Delete(csDataRow)

                    ' 更新件数判定
                    If (intKoshinCount <> 1) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(String.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随標準マスタ"), m_cfErrorStruct.m_strErrorCode)
                    Else
                        ' noop
                    End If

                Next csDataRow

            Else
                ' noop
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfRdbDeadLockExp As UFRdbDeadLockException   ' デッドロックをキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp)

        Catch cfUFRdbUniqueExp As UFRdbUniqueException     ' 一意制約違反をキャッチ

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp)

        Catch cfRdbTimeOutExp As UFRdbTimeOutException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' エラーをそのままスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

    End Sub

#End Region
'*履歴番号 000001 2024/01/11 追加終了

#End Region

End Class
