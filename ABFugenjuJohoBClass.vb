'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         標準化　宛名管理　不現住管理機能
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2024/01/15
'*
'* 作成者　　　     篠原
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2024/01/15           【AB-0830-1】不現住管理機能追加(篠原)
'* 2024/03/07  000001   【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
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
Imports Densan.Common

Public Class ABFugenjuJohoBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_csDataSchma As DataSet                        ' スキーマ保管用データセット:全項目用

    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABFugenjuJohoBClass"

    '定数
    Private Const MAX_ROWS As Integer = 100                       ' 最大取得件数
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

        ' SQL文の作成
        ' 全項目抽出用スキーマ
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.TABLE_NAME, False)
    End Sub
#End Region

#Region "メソッド"

#Region "不現住情報データ取得メソッド"
    '************************************************************************************************
    '* メソッド名   不現住情報データ取得メソッド
    '* 
    '* 構文         Public Function GetFugenjuJohoData(ByVal csABFugenjuJohoParaX As ABFugenjuJohoParaXClass) As DataSet
    '* 
    '* 機能         不現住情報より該当データを取得する。
    '* 
    '* 引数         csABFugenjuJohoParaX As ABFugenjuJohoParaXClass   : 不現住情報パラメータクラス
    '* 
    '* 戻り値       取得した不現住情報の該当データ（DataSet）
    '*                 構造：csFugenjuJohoEntity    
    '************************************************************************************************
    Public Overloads Function GetFugenjuJohoData(ByVal csABFugenjuJohoParaX As ABFugenjuJohoParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetFugenjuJohoData"
        Dim csFugenjuJohoEntity As DataSet                              ' 不現住情報データ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim cABKensakuShimeiB As ABKensakuShimeiBClass                  ' 検索氏名編集Bクラス
        Dim intAimaiKanji As Integer = 0                                ' 半角％が含まれる数(漢字）
        Dim intAimaiKana As Integer = 0                                 ' 半角％が含まれる数(カナ）
        Dim strJushoCD As String = String.Empty                         ' 住所コード
        Dim strJusho As String = String.Empty                           ' 住所
        Dim strBanchi As String = String.Empty                          ' 番地
        Dim strKatagaki As String = String.Empty                        ' 方書
        Dim strShimei As String = String.Empty                          ' 氏名
        Const CHAR_PERCENT As String = "%"                              ' %
        Dim cRuijiClass As New USRuijiClass                             ' 類似文字クラス
        Dim strRuijiJusho As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            ' SELECT句
            strSQL.Append(Me.CreateSelect)
            strSQL.Append(" FROM ").Append(ABFugenjuJohoEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")

            '必須検索条件
            '削除データは抽出しないように以下の条件を追加する。
            strSQL.Append(ABFugenjuJohoEntity.SAKUJOFG).Append(" <> '1'")

            '氏名
            If (csABFugenjuJohoParaX.p_strShimei.Trim.RLength > 0) Then
                'AB000BB.ABKensakuShimeiBClassのGetKensakuShimeiメソッドを利用し、検索用氏名を編集する。
                '※漢字の場合は類字化、カナの場合は半角清音化、アルファベットの場合は大文字化を行い、引数の前方一致の値に応じて文字列の前後に半角％の付与を行う。

                strShimei = csABFugenjuJohoParaX.p_strShimei.Replace("＊", String.Empty).Replace("*", String.Empty).Replace("　", String.Empty).Replace(" ", String.Empty)
                'インスタンス作成
                cABKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
                cABKensakuShimeiB.GetKensakuShimei(csABFugenjuJohoParaX.p_strShimeiZenpoIcchi, strShimei)
                intAimaiKanji = InStr(cABKensakuShimeiB.p_strSearchkanjimei, CHAR_PERCENT)
                intAimaiKana = InStr(cABKensakuShimeiB.p_strSearchKanaseimei, CHAR_PERCENT)

                If (cABKensakuShimeiB.p_strSearchkanjimei.Trim.RLength > 0) Then
                    '検索用氏名クラス.検索用漢字名称≠空白の場合
                    If (intAimaiKanji > 0) Then
                        '検索用氏名クラス.検索用漢字名称に半角％が含まれている場合
                        'AB不現住情報.不現住情報（検索用漢字氏名）　LIKE　'検索用氏名クラス.検索用漢字名称'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI)
                    Else
                        'AB不現住情報.不現住情報（検索用漢字氏名）　＝　'検索用氏名クラス.検索用漢字名称'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI)
                    End If

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI
                    cfUFParameterClass.Value = cABKensakuShimeiB.p_strSearchkanjimei
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ElseIf (cABKensakuShimeiB.p_strSearchKanaseimei.Trim.RLength > 0) Then
                    '検索用氏名クラス.検索用カナ姓名≠空白の場合
                    If (intAimaiKana > 0) Then
                        '検索用氏名クラス.検索用カナ姓名に半角％が含まれている場合
                        'AB不現住情報.不現住情報（検索用カナ氏名）　LIKE　'検索用氏名クラス.検索用カナ姓名'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI)
                    Else
                        'AB不現住情報.不現住情報（検索用カナ氏名）　＝　'検索用氏名クラス.検索用カナ姓名'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI)
                    End If

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI
                    cfUFParameterClass.Value = cABKensakuShimeiB.p_strSearchKanaseimei
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                End If
            End If

            '生年月日
            If (csABFugenjuJohoParaX.p_strUmareymd.Trim.RLength > 0) Then
                'AB不現住情報.不現住情報（生年月日）　＝　'AB不現住情報.生年月日'
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_UMAREYMD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_UMAREYMD
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strUmareymd.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '性別
            If (csABFugenjuJohoParaX.p_strSeibetuCD.Trim.RLength > 0) Then
                'AB不現住情報.不現住情報（性別）　＝　'AB不現住情報.性別'
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEIBETSU)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEIBETSU
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strSeibetuCD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '住所コード
            If ((csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "1") _
                OrElse (csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "3")) Then
                '不現住検索パラメータ.住所検索指定＝1（住所コードで検索） or 3（住所コードと住所で検索）の場合
                If (csABFugenjuJohoParaX.p_strJushoCD.Trim.RLength > 0) Then
                    If (csABFugenjuJohoParaX.p_strKangaiJushoKB.Trim.ToString = "1") Then
                        '不現住検索パラメータ.管外住所区分＝1（管外住所） 
                        If ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(2), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(2).Distinct().Count() = 1)) Then
                            '不現住検索パラメータ.市区町村コードの上2桁以降が全て"0"の場合（都道府県コードで検索）
                            'LTRIM（AB不現住情報.不現住だった住所_住所コード）　LIKE　'不現住検索パラメータ.住所コードの上2桁 + 半角％'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 2) + CHAR_PERCENT
                        ElseIf ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(5), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(5).Distinct().Count() = 1)) Then
                            '不現住検索パラメータ.住所コードの上5桁以降が全て"0"の場合（市区町村コードで検索）
                            'LTRIM（AB不現住情報.不現住だった住所_住所コード）　LIKE　'不現住検索パラメータ.住所コードの上5桁 + 半角％'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 5) + CHAR_PERCENT
                        ElseIf ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(8), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(8).Distinct().Count() = 1)) Then
                            '不現住検索パラメータ.住所コードの上8桁以降が全て"0"の場合（市区町村コードで検索）
                            'LTRIM（AB不現住情報.不現住だった住所_住所コード）　LIKE　'不現住検索パラメータ.住所コードの上8桁 + 半角％'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 8) + CHAR_PERCENT
                        Else
                            '（全国住所コードで検索）
                            'LTRIM（AB不現住情報.不現住だった住所_市区町村コード） +　LTRIM（AB不現住情報.不現住だった住所_町字コード） ＝　'不現住検索パラメータ.住所コード'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" = ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.RPadRight(13)
                        End If
                    Else
                        'LTRIM（AB不現住情報.不現住だった住所_市区町村コード） +　LTRIM（AB不現住情報.不現住だった住所_町字コード） ＝　'不現住検索パラメータ.住所コード'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                        strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RPadLeft(13)
                    End If

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD
                    cfUFParameterClass.Value = strJushoCD
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
            End If

            '住所
            If ((csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "2") _
                OrElse (csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "3")) Then
                '不現住検索パラメータ.住所検索指定＝2（住所で検索） or 3（住所コードと住所で検索）の場合
                If (csABFugenjuJohoParaX.p_strJusho.Trim.RLength > 0) Then
                    strRuijiJusho = cRuijiClass.GetRuijiMojiList((csABFugenjuJohoParaX.p_strJusho).Replace("　", String.Empty)).ToUpper
                    Select Case csABFugenjuJohoParaX.p_strJushoZenpoIcchi.Trim.ToString
                        Case "1"
                            '不現住検索パラメータ.住所前方一致＝1（前方一致）の場合
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = strRuijiJusho + CHAR_PERCENT
                        Case "2"
                            '不現住検索パラメータ.住所前方一致＝2（部分一致）の場合
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = CHAR_PERCENT + strRuijiJusho + CHAR_PERCENT
                        Case Else
                            '（完全一致）
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" = ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = strRuijiJusho
                    End Select

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO
                    cfUFParameterClass.Value = strJusho
                    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
            End If

            '番地
            If (csABFugenjuJohoParaX.p_strBanchi.Trim.RLength > 0) Then
                Select csABFugenjuJohoParaX.p_strBanchiZenpoIcchi.Trim.ToString
                    Case "1"
                        '不現住検索パラメータ.番地前方一致＝1（前方一致）の場合
                        'AB不現住情報.不現住だった住所_番地号表記　LIKE　’不現住検索パラメータ.番地 + 半角％’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = csABFugenjuJohoParaX.p_strBanchi.Trim.ToString + CHAR_PERCENT
                    Case "2"
                        '現住検索パラメータ.番地前方一致＝2（部分一致）の場合
                        'AB不現住情報.不現住だった住所_番地号表記　LIKE　’半角％ + 不現住検索パラメータ.番地 + 半角％’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = CHAR_PERCENT + csABFugenjuJohoParaX.p_strBanchi.Trim.ToString + CHAR_PERCENT
                    Case Else
                        '（完全一致）
                        'AB不現住情報不現住だった住所_番地号表記　＝　’不現住検索パラメータ.番地’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = csABFugenjuJohoParaX.p_strBanchi.Trim.ToString
                End Select

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI
                cfUFParameterClass.Value = strBanchi
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '方書
            If (csABFugenjuJohoParaX.p_strKatagaki.Trim.RLength > 0) Then
                Select csABFugenjuJohoParaX.p_strKatagakiZenpoIcchi.Trim.ToString
                    Case "1"
                        '不現住検索パラメータ.方書前方一致＝1（前方一致）の場合
                        'AB不現住情報.不現住だった住所_方書　LIKE　’不現住検索パラメータ.方書 + 半角％’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString + CHAR_PERCENT
                    Case "2"
                        '不現住検索パラメータ.方書前方一致＝2（部分一致）の場合
                        'AB不現住情報.不現住だった住所_方書　LIKE　’半角％ + 不現住検索パラメータ.方書 + 半角％’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = CHAR_PERCENT + csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString + CHAR_PERCENT
                    Case Else
                        '（完全一致）
                        'AB不現住情報不現住だった住所_方書　＝　’不現住検索パラメータ.方書’
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString
                End Select

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI
                cfUFParameterClass.Value = strKatagaki
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '住民コード
            If (csABFugenjuJohoParaX.p_strJuminCD.Trim.RLength > 0) Then
                'AB不現住情報.住民コード　＝　’不現住検索パラメータ.住民コード’
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_JUMINCD
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strJuminCD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '登録年月日
            If ((csABFugenjuJohoParaX.p_strTorokuSTYMD.Trim.RLength > 0) _
                AndAlso (csABFugenjuJohoParaX.p_strTorokuEDYMD.Trim.RLength > 0)) Then
                'AB不現住情報.不現住登録年月日　≧　’不現住検索パラメータ.開始登録年月日’
                'AND　AB不現住情報.不現住登録年月日　≦　’不現住検索パラメータ.終了登録年月日’
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUTOROKUYMD).Append(" >= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ST")
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUTOROKUYMD).Append(" <= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ED")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ST"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strTorokuSTYMD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ED"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strTorokuEDYMD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '居住不明年月日
            If ((csABFugenjuJohoParaX.p_strKyojuFumeiSTYMD.Trim.RLength > 0) _
                AndAlso (csABFugenjuJohoParaX.p_strKyojuFumeiEDYMD.Trim.RLength > 0)) Then
                'AB不現住情報.不現住居住不明年月日　≧　’不現住検索パラメータ.開始居住不明年月日’
                'AND　AB不現住情報.不現住居住不明年月日　≦　’不現住検索パラメータ.終了居住不明年月日’

                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.KYOJUFUMEI_YMD).Append(" >= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ST")
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.KYOJUFUMEI_YMD).Append(" <= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ED")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ST"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strKyojuFumeiSTYMD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ED"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strKyojuFumeiEDYMD.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '不現住区分
            If (csABFugenjuJohoParaX.p_strFugenjuKB.Trim.RLength > 0) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUKB)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUKB
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strFugenjuKB.Trim.ToString
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '最大取得件数
            If (csABFugenjuJohoParaX.p_intHyojiKensu = 0) Then
                '抽出SQLの実行を行うＲＤＢクラス（UFRdbClass）の最大取得件数プロパティ（p_intMaxRows）に100を設定する
                m_cfRdbClass.p_intMaxRows = MAX_ROWS
            Else
                '抽出SQLの実行を行うＲＤＢクラス（UFRdbClass）の最大取得件数プロパティ（p_intMaxRows）に不現住検索パラメータ.最大取得件数の値を設定する
                m_cfRdbClass.p_intMaxRows = csABFugenjuJohoParaX.p_intHyojiKensu
            End If

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csFugenjuJohoEntity = m_csDataSchma.Clone()
            csFugenjuJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csFugenjuJohoEntity, ABFugenjuJohoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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

        Return csFugenjuJohoEntity
    End Function
#End Region

#Region "不現住情報データ追加メソッド"
    '************************************************************************************************
    '* メソッド名   不現住情報データ追加メソッド
    '* 
    '* 構文         Public Function InsertFugenjuJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     不現住情報に新規データを追加する。
    '* 
    '* 引数         csDataRow As DataRow   : 不現住者情報(ABFUGENJUJOHO)
    '* 
    '* 戻り値       追加件数(Integer)
    '************************************************************************************************
    Public Function InsertFugenjuJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertFugenjuJoho"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        Dim intInsCnt As Integer                                        ' 追加件数
        Dim strUpdateDateTime As String                                 ' システム日付

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty) _
                OrElse (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")            ' 作成日時

            ' 共通項目の編集を行う
            csDataRow(ABFugenjuJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId               ' 端末ＩＤ
            csDataRow(ABFugenjuJohoEntity.SAKUJOFG) = "0"                                           ' 削除フラグ
            csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER) = Decimal.Zero                             ' 更新カウンタ
            csDataRow(ABFugenjuJohoEntity.SAKUSEINICHIJI) = strUpdateDateTime                       ' 作成日時
            csDataRow(ABFugenjuJohoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                ' 作成ユーザー
            csDataRow(ABFugenjuJohoEntity.KOSHINNICHIJI) = strUpdateDateTime                        ' 更新日時
            csDataRow(ABFugenjuJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 ' 更新ユーザー

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

#Region "不現住情報データ更新メソッド"
    '************************************************************************************************
    '* メソッド名   不現住情報データ更新メソッド
    '* 
    '* 構文         Public Function UpdateFugenjuJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     不現住情報のデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   : 不現住者情報(ABFUGENJUJOHO)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateFugenjuJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateFugenjuJoho"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If ((m_strUpDateSQL Is Nothing Or m_strUpDateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABFugenjuJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' 端末ＩＤ
            csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER)) + 1       ' 更新カウンタ
            csDataRow(ABFugenjuJohoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")     ' 更新日時
            csDataRow(ABFugenjuJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABFugenjuJohoEntity.PREFIX_KEY.RLength) = ABFugenjuJohoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PREFIX_KEY.RLength),
                        DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass)

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

#Region " SQL文の作成"
    '************************************************************************************************
    '* メソッド名   SQL文の作成
    '* 
    '* 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数         csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  ' パラメータクラス
        Dim strInsertColumn As String                               ' 追加SQL文項目文字列
        Dim strInsertParam As String                                ' 追加SQL文パラメータ文字列
        Dim strWhere As New StringBuilder                           ' 更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABFugenjuJohoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL文の作成
            m_strUpDateSQL = "UPDATE " + ABFugenjuJohoEntity.TABLE_NAME + " SET "

            ' UPDATE Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABFugenjuJohoEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABFugenjuJohoEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.KOSHINCOUNTER)

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL文の作成
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL文のトリミング
            m_strUpDateSQL = m_strUpDateSQL.Trim()
            m_strUpDateSQL = m_strUpDateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpDateSQL += strWhere.ToString

            ' UPDATE コレクションにキー情報を追加
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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

#Region "SELECT句の作成"
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
            csSELECT.AppendFormat("SELECT {0}", ABFugenjuJohoEntity.SHICHOSONCD)                      ' 市町村コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.JUMINCD)                               ' 住民コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUKB)                             ' 不現住区分
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO)             ' 不現住だった住所_郵便番号
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANNAIKANGAIKB)      ' 不現住だった住所_管内管外区分
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)             ' 不現住だった住所_住所コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHO)               ' 不現住だった住所_住所
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD)         ' 不現住だった住所_市区町村コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD)          ' 不現住だった住所_町字コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN)           ' 不現住だった住所_都道府県
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON)      ' 不現住だった住所_市区郡町村名
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA)            ' 不現住だった住所_町字
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)              ' 不現住だった住所_番地号表記
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)            ' 不現住だった住所_方書
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI)        ' 不現住だった住所_方書_フリガナ
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN)            ' 不現住情報（対象者区分）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI)           ' 不現住情報（対象者氏名）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKANASHIMEI)       ' 不現住情報（対象者カナ氏名）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)         ' 不現住情報（検索用漢字氏名）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)          ' 不現住情報（検索用カナ氏名）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)         ' 不現住だった住所_検索用住所
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_SEI)       ' 不現住情報（対象者氏名_姓）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_MEI)       ' 不現住情報（対象者氏名_名）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)                  ' 不現住情報（生年月日）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)                  ' 不現住情報（性別）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KYOJUFUMEI_YMD)                        ' 居住不明年月日
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUTOROKUYMD)                      ' 不現住登録年月日
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD)                     ' 指定都市_行政区等コード
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_BIKO)                      ' 不現住情報（備考）
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.RESERVE)                               ' リザーブ
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.TANMATSUID)                            ' 端末ID
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUJOFG)                              ' 削除フラグ
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINCOUNTER)                         ' 更新カウンタ
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUSEINICHIJI)                        ' 作成日時
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUSEIUSER)                           ' 作成ユーザ
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINNICHIJI)                         ' 更新日時
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINUSER)                            ' 更新ユーザ

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

#End Region
#End Region

End Class
