'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        検索氏名編集(ABKensakuShimeiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2002/12/18　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/11 000001     区切り文字の変更
'* 2005/04/04 000002     全角でのあいまい検索を可能にする(マルゴ村山)
'* 2007/09/03 000003     多摩市用編集サブルーチンのオーバーロード（中沢）
'* 2007/10/10 000004     標準の仕様でも氏名がアルファベットの場合は大文字に変換する（中沢）
'* 2007/11/06 000005     検索カナ姓名編集パターンの修正、検索カナ項目メンバ変数を初期化（中沢）
'* 2011/09/26 000006     全角アルファベット検索時の清音化判定処理を追加（比嘉）
'* 2012/01/20 000007     【AB17051】アルファベット氏名検索機能の改善(北村)
'* 2020/01/10 000008     【AB32001】アルファベット検索（石合）
'* 2023/12/04 000009     【AB-1600-1】検索機能対応(下村)
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common
Imports System.Text
Imports System.Security

Public Class ABKensakuShimeiBClass
    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass            'ログ出力クラス
    Private m_cfConfigData As UFConfigDataClass     '環境情報データクラス
    Private m_cfUFControlData As UFControlData      'コントロールデータ
    Private m_cRuijiClass As USRuijiClass       ' 類似文字クラス

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKensakuShimeiBClass"
    Private Const BUBUNITCHI As String = "2"

    'パラメータのメンバ変数
    Private m_strSearchkanjimei As String           '検索用漢字名称（全角漢字　Max４０文字）
    Private m_strSearchKanaseimei As String         '検索用カナ姓名（半角カナ　Max４０文字）
    Private m_strSearchKanasei As String            '検索用カナ姓　（半角カナ　Max２４文字）
    Private m_strSearchKanamei As String            '検索用カナ名　（半角カナ　Max１６文字）

    '各メンバ変数のプロパティ定義
    Public ReadOnly Property p_strSearchkanjimei() As String
        Get
            Return m_strSearchkanjimei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanaseimei() As String
        Get
            Return m_strSearchKanaseimei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanasei() As String
        Get
            Return m_strSearchKanasei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanamei() As String
        Get
            Return m_strSearchKanamei
        End Get
    End Property

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal csUFControlData As UFControlData, 
    '*                               ByVal csUFConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            csUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 csUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)
        'メンバ変数セット
        m_cfUFControlData = cfControlData
        m_cfConfigData = cfConfigData

        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        'パラメータのメンバ変数
        m_strSearchkanjimei = String.Empty
        m_strSearchKanaseimei = String.Empty
        m_strSearchKanasei = String.Empty
        m_strSearchKanamei = String.Empty
    End Sub

    '************************************************************************************************
    '* メソッド名      検索氏名取得
    '* 
    '* 構文            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
    '* 
    '* 機能　　        氏名を検索キーとして編集する
    '* 
    '* 引数            strAimai As String        :前方一致
    '*                 strShimei As String      ：氏名
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    '*履歴番号 000003 2007/09/03 修正開始
    Public Overloads Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
        ''Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
        'Const THIS_METHOD_NAME As String = "GetKensakuShimei"                   'メソッド名
        'Dim cuString As New USStringClass
        'Dim strHenshu As String = String.Empty              '引数の編集名称を格納
        'Dim strHenshuSei As String = String.Empty           '編集名称姓
        'Dim strHenshuMei As String = String.Empty           '編集名称名
        'Dim intIchi As Integer = 0                          '桁位置
        ''04/02/28 追加開始
        'Dim strChkHenshu As String = String.Empty           'ひらがらチェック
        ''04/02/28 追加終了

        'Try
        '    'デバッグ開始ログ出力
        '    m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        '    '04/02/28 追加開始
        '    If cuString.ToHankaku(strShimei, strChkHenshu) Then
        '        strShimei = strChkHenshu
        '    End If
        '    '04/02/28 追加終了

        '    strHenshu = strShimei

        '    '* 履歴番号 000002 2005/04/04 修正開始
        '    'If (UFStringClass.CheckKanjiCode(strHenshu, m_cfConfigData)) Then
        '    If (UFStringClass.CheckKanjiCode(strHenshu.Trim("%"c).Trim("％"c), m_cfConfigData)) Then
        '        '* 履歴番号 000002 2005/04/04 修正終了
        '        '全角
        '        '* 履歴番号 000001 2003/03/11 修正開始
        '        'intIchi = InStr(strHenshu, "：")
        '        intIchi = InStr(strHenshu, "＊")
        '        '* 履歴番号 000001 2003/03/11 修正終了
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "　"
        '        End If
        '        '* 履歴番号 000002 2005/04/04 追加開始
        '        intIchi = InStr(strHenshu, "％")
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "%"
        '        End If
        '        '* 履歴番号 000002 2005/04/04 追加終了
        '        If (strAimai = "1") Then
        '            strHenshu = strHenshu + "%"
        '        End If
        '        m_strSearchkanjimei = strHenshu
        '    Else
        '        '半角
        '        '* 履歴番号 000002 2005/04/04 追加開始
        '        intIchi = InStr(strShimei, "％")
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "%"
        '        End If
        '        '* 履歴番号 000002 2005/04/04 追加終了
        '        '* 履歴番号 000001 2003/03/11 修正開始
        '        'intIchi = InStr(strShimei, ":")
        '        intIchi = InStr(strShimei, "*")
        '        '* 履歴番号 000001 2003/03/11 修正終了
        '        If (intIchi = 0) Then
        '            intIchi = InStr(strShimei, " ")
        '        End If
        '        If (intIchi <> 0) Then
        '            '分割
        '            '姓
        '            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
        '            If (strAimai = "1") Then
        '                strHenshuSei = strHenshuSei + "%"
        '            End If
        '            m_strSearchKanasei = strHenshuSei
        '            '名
        '            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
        '            If (strAimai = "1") Then
        '                strHenshuMei = strHenshuMei + "%"
        '            End If
        '            m_strSearchKanamei = strHenshuMei
        '        Else
        '            '分割なし
        '            strHenshu = cuString.ToKanaKey(strHenshu)
        '            If (strAimai = "1") Then
        '                strHenshu = strHenshu + "%"
        '            End If
        '            m_strSearchKanaseimei = strHenshu
        '        End If
        '    End If

        '    'デバッグ終了ログ出力
        '    m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        'Catch objExp As Exception
        '    'エラーログ出力
        '    m_cfUFLogClass.ErrorWrite(m_cfUFControlData, _
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" _
        '                              + "【メソッド名:" + THIS_METHOD_NAME + "】" _
        '                              + "【エラー内容:" + objExp.Message + "】")
        '    'エラーをそのままスローする
        '    Throw objExp
        'End Try

        GetKensakuShimei(strAimai, strShimei, 0)
        '*履歴番号 000003 2007/09/03 修正終了
    End Sub

    '*履歴番号 000003 2007/09/03 追加開始
    '************************************************************************************************
    '* メソッド名      検索氏名取得（オーバーロード）
    '* 
    '* 構文            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String, 
    '*                                                                  ByVal intHommyoYusen As Integer)
    '* 
    '* 機能　　        氏名を検索キーとして編集する
    '* 
    '* 引数            strAimai As String        :前方一致
    '*                 strShimei As String      ：氏名
    '*                 intHommyoYusen As Integer：標準(0)，本名(1)，通称名(2)
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    <SecuritySafeCritical>
    Public Overloads Sub GetKensakuShimei(ByVal strAimai As String,
                                          ByVal strShimei As String,
                                          ByVal intHommyoYusen As Integer)
        Const THIS_METHOD_NAME As String = "GetKensakuShimei"                   'メソッド名
        Dim cuString As New USStringClass
        Dim strHenshu As String = String.Empty              '引数の編集名称を格納
        Dim strHenshuSei As String = String.Empty           '編集名称姓
        Dim strHenshuMei As String = String.Empty           '編集名称名
        Dim intIchi As Integer = 0                          '桁位置
        Dim strChkHenshu As String = String.Empty           'ひらがなチェック
        Dim cfRdb As UFRdbClass                             'RDBクラス
        Dim crKanriJohoB As URKANRIJOHOCacheBClass          '管理情報Ｂクラス
        Dim enGaikokujinKensakuKB As FrnHommyoKensakuType   '外国人本名検索区分
        '*履歴番号 000006 2011/09/26 追加開始
        Dim cABKanriJohoB As ABAtenaKanriJohoBClass         '宛名管理情報クラス
        Dim csABKanriJohoDS As DataSet
        Dim strZenAlphabetKB As String
        '*履歴番号 000006 2011/09/26 追加終了

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDBクラスのインスタンス作成
            cfRdb = New UFRdbClass(m_cfUFControlData.m_strBusinessId)

            '*履歴番号 000005 2007/11/06 追加開始
            ' 検索用メンバ変数初期化
            m_strSearchkanjimei = String.Empty
            m_strSearchKanaseimei = String.Empty
            m_strSearchKanasei = String.Empty
            m_strSearchKanamei = String.Empty
            '*履歴番号 000005 2007/11/06 追加終了

            ' 宛名取得ビジネスクラスのインスタンス作成
            crKanriJohoB = New URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfConfigData, cfRdb)
            ' 管理情報取得メソッド実行
            enGaikokujinKensakuKB = crKanriJohoB.GetFrn_HommyoKensaku_Param()

            '*履歴番号 000006 2011/09/26 追加開始
            ' 宛名管理情報クラスのインスタンス化
            cABKanriJohoB = New ABAtenaKanriJohoBClass(m_cfUFControlData, m_cfConfigData, cfRdb)
            ' 管理情報取得メソッド実行(検索画面(03)、全角アルファベット検索制御(14))
            csABKanriJohoDS = cABKanriJohoB.GetKanriJohoHoshu("03", "14")

            ' 管理情報チェック
            If (Not (csABKanriJohoDS Is Nothing) AndAlso csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                strZenAlphabetKB = csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString
            Else
                strZenAlphabetKB = "0"
            End If
            '*履歴番号 000006 2011/09/26 追加終了

            If (m_cRuijiClass Is Nothing) Then
                m_cRuijiClass = New USRuijiClass
            End If

            If cuString.ToHankaku(strShimei, strChkHenshu) Then
                '*履歴番号 000006 2011/09/26 追加開始
                If (strZenAlphabetKB = "1") Then
                    ' 管理情報：検索画面・全角アルファベット検索制御(03・14) = "1" の場合
                    If (UFStringClass.CheckAlphabetNumber(strChkHenshu.Replace(" ", "").Trim("%"c).Trim("*"c).Trim("."c).Trim("･"c))) Then
                        If (strShimei = strChkHenshu) Then
                            '入力が半角アルファベットということになるため半角で検索させる
                            strShimei = strChkHenshu
                            '*履歴番号 000007 2012/01/20 修正開始
                        ElseIf (strChkHenshu = "*") Then
                            '半角変換後の値が'*'の場合、'*'で検索させる
                            strShimei = strChkHenshu
                            '*履歴番号 000007 2012/01/20 修正終了
                        Else
                            '*履歴番号 000008 2020/01/10 修正開始
                            ''入力が全角アルファベットということだから全角で検索させる
                            ' 入力が全角アルファベットということだから全角半角両方で検索させる
                            Call SetSearchKanjiShimei(strShimei, strAimai)
                            strShimei = strChkHenshu
                            '*履歴番号 000008 2020/01/10 修正終了
                        End If
                    Else
                        'アルファベットではないので通常通り半角での検索
                        strShimei = strChkHenshu
                    End If
                Else
                    strShimei = strChkHenshu
                End If
                'strShimei = strChkHenshu
                '*履歴番号 000006 2011/09/26 追加終了
            End If

            strHenshu = strShimei

            If (UFStringClass.CheckKanjiCode(strHenshu.Trim("%"c).Trim("％"c), m_cfConfigData)) Then
                '全角
                intIchi = InStr(strHenshu, "＊")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "　"
                End If
                strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("　", String.Empty)).ToUpper
                intIchi = InStr(strHenshu, "％")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "%"
                End If
                If (strAimai = "1") Then
                    strHenshu = strHenshu + "%"
                ElseIf (strAimai = BUBUNITCHI) Then
                    strHenshu = "%" + strHenshu + "%"
                End If
                m_strSearchkanjimei = strHenshu
            Else
                '半角
                intIchi = InStr(strShimei, "％")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "%"
                End If
                intIchi = InStr(strShimei, "*")
                If (intIchi = 0) Then
                    intIchi = InStr(strShimei, " ")
                End If

                '本名優先検索パラメータが１，２以外のときはAtenaGetのインターフェース用に検索カナ用変数を設定
                '外国人本名検索機能区分が標準のときはAtenaGetのインターフェース用に検索カナ用変数を設定
                '*履歴番号 000003 2007/09/03以前からGetKensakuShimeiを使用している業務には影響なし。
                If (enGaikokujinKensakuKB = FrnHommyoKensakuType.Tsusho OrElse
                                        (intHommyoYusen <> 1 AndAlso intHommyoYusen <> 2)) Then
                    '標準仕様
                    If (intIchi <> 0) Then
                        '分割
                        '姓
                        '* 履歴番号 000004 2007/10/10 修正開始
                        strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                        'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
                        '* 履歴番号 000004 2007/10/10 修正終了
                        If (strAimai = "1") Then
                            strHenshuSei = strHenshuSei + "%"
                        End If
                        m_strSearchKanasei = strHenshuSei
                        '名
                        '* 履歴番号 000004 2007/10/10 修正開始
                        strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                        'strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
                        '* 履歴番号 000004 2007/10/10 修正終了
                        If (strAimai = "1") Then
                            strHenshuMei = strHenshuMei + "%"
                        End If
                        m_strSearchKanamei = strHenshuMei
                        If (strAimai = BUBUNITCHI) Then
                            m_strSearchKanasei = String.Empty
                            m_strSearchKanamei = String.Empty
                            strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty).Replace("*", String.Empty)).ToUpper()
                            strHenshu = "%" + strHenshu + "%"
                            m_strSearchKanaseimei = strHenshu
                        End If
                    Else
                        '分割なし
                        '* 履歴番号 000004 2007/10/10 修正開始
                        strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                        'strHenshu = cuString.ToKanaKey(strHenshu)
                        '* 履歴番号 000004 2007/10/10 修正終了
                        If (strAimai = "1") Then
                            strHenshu = strHenshu + "%"
                        ElseIf (strAimai = BUBUNITCHI) Then
                            strHenshu = "%" + strHenshu + "%"
                        End If
                        m_strSearchKanaseimei = strHenshu
                    End If
                Else
                    '本名と通称名の両方で検索可能なＤＢ
                    'アルファベットは全て大文字でセットする
                    If (intHommyoYusen = 2) Then
                        '本名優先検索以外
                        '検索カナ姓名　検索カナ名に検索文字列がセットされる
                        'カナ通称名の場合
                        If (intIchi <> 0) Then
                            '*履歴番号 000005 2007/11/06 修正開始
                            '分割あり カナ姓カナ名を抽出
                            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                                If (strHenshuSei.Trim <> String.Empty) Then
                                    m_strSearchKanaseimei = strHenshuSei + "%"  '検索カナ姓
                                End If
                                m_strSearchKanamei = strHenshuMei + "%"     '検索カナ名
                            ElseIf (straimai = BUBUNITCHI) Then
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty)).ToUpper()
                                strHenshu = "%" + strHenshu + "%"
                                m_strSearchKanaseimei = strHenshu
                            Else
                                '完全一致
                                '検索カナ姓名
                                If (strHenshuSei.Trim <> String.Empty) Then
                                    m_strSearchKanaseimei = cuString.ToKanaKey((strHenshu).Replace(" ", String.Empty)).ToUpper()
                                Else
                                    m_strSearchKanamei = strHenshuMei
                                End If
                            End If
                            ''分割あり カナ姓カナ名を抽出
                            'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            'strHenshuMei = cuString.ToKanaKey((Mid(strHenshu, intIchi + 1))).ToUpper()
                            'If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                            '    strHenshuMei = strHenshuMei + "%"
                            'End If
                            'm_strSearchKanaseimei = strHenshuSei + "%"  '検索カナ姓（曖昧の有無にかかわらず％が付加される）
                            'm_strSearchKanamei = strHenshuMei           '検索カナ名
                            '*履歴番号 000005 2007/11/06 修正終了
                        Else
                            '分割なし
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                            If (strAimai = "1") Then
                                strHenshu = strHenshu + "%"
                            ElseIf (strAimai = BUBUNITCHI) Then
                                strHenshu = "%" + strHenshu + "%"
                            End If
                            m_strSearchKanaseimei = strHenshu           '検索カナ姓名
                        End If
                    Else
                        '本名優先検索
                        'カナ本名の場合（検索カナ姓のみで検索可能にする変数を生成）
                        '検索カナ姓に検索文字列がセットされる
                        If (intIchi <> 0) Then
                            '*履歴番号 000005 2007/11/06 修正開始
                            '分割ありの場合姓名分割
                            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                                strHenshuSei = strHenshuSei + "%"
                                strHenshuMei = strHenshuMei + "%"
                                '本名カナ名称は検索用カナ姓名で返される（検索カナ姓と検索カナ名を結合）
                                m_strSearchKanasei = strHenshuSei + strHenshuMei
                            ElseIf (straimai = BUBUNITCHI) Then
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty)).ToUpper()
                                strHenshu = "%" + strHenshu + "%"
                                m_strSearchKanaseimei = strHenshu
                            Else
                                '完全一致の場合
                                If (strHenshuSei.Trim = String.Empty) Then
                                    m_strSearchKanasei = "%" + strHenshuMei
                                Else
                                    m_strSearchKanasei = cuString.ToKanaKey((strHenshu).Replace(" ", String.Empty)).ToUpper()
                                End If
                            End If
                            ''分割ありの場合姓名分割
                            'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            'strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            'If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                            '    strHenshuSei = strHenshuSei + "%"
                            '    strHenshuMei = strHenshuMei + "%"
                            'End If
                            ''本名カナ名称は検索用カナ姓名で返される（検索カナ姓と検索カナ名を結合）
                            'm_strSearchKanasei = strHenshuSei + strHenshuMei
                            '*履歴番号 000005 2007/11/06 修正終了
                        Else
                            '分割なしの場合そのまま曖昧検索を付加
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                            If (strAimai = "1") Then
                                strHenshu = strHenshu + "%"
                            ElseIf (strAimai = BUBUNITCHI) Then
                                strHenshu = "%" + strHenshu + "%"
                            End If
                            '本名カナ名称は検索用カナ姓名で返される
                            m_strSearchKanasei = strHenshu
                        End If
                    End If
                End If
            End If

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" _
                                      + "【メソッド名:" + THIS_METHOD_NAME + "】" _
                                      + "【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp
        End Try

    End Sub
    '*履歴番号 000003 2007/09/03 追加終了

    '*履歴番号 000008 2020/01/10 追加開始
    ''' <summary>
    ''' 検索用漢字氏名設定
    ''' </summary>
    ''' <param name="strShimei">対象文字列</param>
    ''' <param name="strAimai">あいまい検索</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKanjiShimei(ByVal strShimei As String, ByVal strAimai As String)

        Dim strHenshu As String
        Dim intIchi As Integer

        Try

            strHenshu = strShimei
            intIchi = InStr(strHenshu, "＊")
            If (intIchi > 0) Then
                Mid(strHenshu, intIchi, 1) = "　"
            End If
            strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("　", String.Empty)).ToUpper
            intIchi = InStr(strHenshu, "％")
            If (intIchi > 0) Then
                Mid(strHenshu, intIchi, 1) = "%"
            End If
            If (strAimai = "1") Then
                strHenshu = strHenshu + "%"
            ElseIf (strAimai = BUBUNITCHI) Then
                strHenshu = "%" + strHenshu + "%"
            End If
            m_strSearchkanjimei = strHenshu

        Catch csExp As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' 氏名検索条件生成
    ''' </summary>
    ''' <param name="cSearchKey">宛名検索キー</param>
    ''' <param name="strTableName">テーブル名</param>
    ''' <param name="csWhere">作成中条件</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <remarks></remarks>
    Public Sub CreateWhereForShimei(
        ByVal cSearchKey As ABAtenaSearchKey,
        ByVal strTableName As String,
        ByRef csWhere As StringBuilder,
        ByRef cfParamCollection As UFParameterCollectionClass)

        Dim csWhereForKanaShimei As StringBuilder
        Dim csWhereForKanjiShimei As StringBuilder
        Dim cfParam As UFParameterClass

        Try

            ' カナ検索部、漢字検索部に１つでも値が存在する場合に検索条件を追加する
            If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 _
                OrElse (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki _
                        AndAlso cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)) Then

                If (csWhere.RLength > 0) Then
                    csWhere.Append(" AND ")
                Else
                    ' noop
                End If

                ' ---------------------------------------------------------------------------------
                ' カナ検索部編集
                csWhereForKanaShimei = New StringBuilder
                With csWhereForKanaShimei

                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ姓名
                    If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ姓
                    If (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0) Then

                        If (csWhereForKanaShimei.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then
                            .Append("(")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索カナ姓２
                    If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then

                        .Append(" OR ")

                        If (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                        .Append(")")

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ名
                    If (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0) Then

                        If (.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' 漢字検索部編集
                csWhereForKanjiShimei = New StringBuilder
                With csWhereForKanjiShimei

                    ' -----------------------------------------------------------------------------
                    ' 検索用漢字名称
                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 漢字氏名２
                    If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then

                        If (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then

                            If (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0) Then

                                .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' 検索条件のパラメータを作成
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2
                                End With

                            Else

                                .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' 検索条件のパラメータを作成
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                                End With

                            End If

                            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfParamCollection.Add(cfParam)

                        Else
                            ' noop
                        End If

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' カナ検索部と漢字検索部が両方設定されている場合、ＯＲ条件で連結する
                If (csWhereForKanaShimei.RLength > 0) Then
                    If (csWhereForKanjiShimei.RLength > 0) Then
                        csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString, csWhereForKanjiShimei.ToString)
                    Else
                        csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString)
                    End If
                Else
                    csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString)
                End If
                ' ---------------------------------------------------------------------------------

            Else
                ' noop
            End If

        Catch csExp As Exception
            Throw
        End Try

    End Sub
    '*履歴番号 000008 2020/01/10 追加終了

    ''' <summary>
    ''' 氏名検索条件生成(オーバーロード)
    ''' </summary>
    ''' <param name="cSearchKey">宛名検索キー</param>
    ''' <param name="strTableName">テーブル名</param>
    ''' <param name="csWhere">作成中条件</param>
    ''' <param name="cfParamCollection">パラメーターコレクション</param>
    ''' <param name="strFZYHyojunTableName">宛名付随標準テーブル名</param>
    ''' <param name="blnFromAtenaRireki">宛名履歴判定フラグ:Optional-False</param>
    ''' <param name="intHyojunKB">標準化版判定:Optional通常</param>
    ''' <remarks></remarks>
    Public Sub CreateWhereForShimei(ByVal cSearchKey As ABAtenaSearchKey,
                                    ByVal strTableName As String,
                                    ByRef csWhere As StringBuilder,
                                    ByRef cfParamCollection As UFParameterCollectionClass,
                                    ByVal strFZYHyojunTableName As String,
                                    Optional ByVal blnFromAtenaRireki As Boolean = False,
                                    Optional ByVal intHyojunKB As ABEnumDefine.HyojunKB = ABEnumDefine.HyojunKB.KB_Tsujo)

        Dim csWhereForKanaShimei As StringBuilder
        Dim csWhereForKanjiShimei As StringBuilder
        Dim cfParam As UFParameterClass
        Dim strWhereFZYHyojunKana As String
        Dim strWhereFzyHyojunKanji As String

        Try

            ' カナ検索部、漢字検索部に１つでも値が存在する場合に検索条件を追加する
            If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 _
                OrElse (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki _
                        AndAlso cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)) Then

                If (csWhere.RLength > 0) Then
                    csWhere.Append(" AND ")
                Else
                    ' noop
                End If

                ' ---------------------------------------------------------------------------------
                ' カナ検索部編集
                csWhereForKanaShimei = New StringBuilder
                With csWhereForKanaShimei

                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ姓名
                    If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0) Then
                        strWhereFZYHyojunKana = CreateWhereFZYHyojunKana(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB)
                        If (strWhereFZYHyojunKana.RLength > 0) Then
                            .Append("(")
                        End If
                        If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0) Then
                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)
                        If (strWhereFZYHyojunKana.RLength > 0) Then
                            If (blnFromAtenaRireki) Then
                                .Append(strWhereFZYHyojunKana)
                            Else
                                .Append(" OR ")
                                .AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD)
                                .AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD)
                                .AppendFormat(" WHERE {0}", strWhereFZYHyojunKana)
                                .Append("))")
                            End If
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)
                        End If
                    Else
                        ' noop
                    End If

                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ姓
                    If (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0) Then

                        If (csWhereForKanaShimei.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then
                            .Append("(")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索カナ姓２
                    If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then

                        .Append(" OR ")

                        If (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                        .Append(")")

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索用カナ名
                    If (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0) Then

                        If (.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' 漢字検索部編集
                csWhereForKanjiShimei = New StringBuilder
                With csWhereForKanjiShimei

                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) OrElse
                        (cSearchKey.p_enGaikokuHommyoKensaku = 2 And cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then
                        strWhereFzyHyojunKanji = CreateWhereFZYHyojunKanji(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB)
                    Else
                        strWhereFzyHyojunKanji = String.Empty
                    End If
                    If (strWhereFzyHyojunKanji.RLength > 0) Then
                        .Append("(")
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 検索用漢字名称
                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' 検索条件のパラメータを作成
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            End With

                        End If

                        ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' 漢字氏名２
                    If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then

                        If (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then

                            If (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0) Then

                                .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' 検索条件のパラメータを作成
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2
                                End With

                            Else

                                .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' 検索条件のパラメータを作成
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                                End With

                            End If

                            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfParamCollection.Add(cfParam)

                        Else
                            ' noop
                        End If

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    If (strWhereFzyHyojunKanji.RLength > 0) Then
                        If (blnFromAtenaRireki) Then
                            .Append(strWhereFzyHyojunKanji)
                        Else
                            .Append(" OR ")
                            .AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD)
                            .AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD)
                            .AppendFormat(" WHERE {0}", strWhereFzyHyojunKanji)
                            .Append("))")
                        End If
                        If (cSearchKey.p_strSearchKanjiMeisho.RLength > 0) Then
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)
                        Else
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)
                        End If

                    End If
                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' カナ検索部と漢字検索部が両方設定されている場合、ＯＲ条件で連結する
                If (csWhereForKanaShimei.RLength > 0) Then
                    If (csWhereForKanjiShimei.RLength > 0) Then
                        csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString, csWhereForKanjiShimei.ToString)
                    Else
                        csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString)
                    End If
                Else
                    csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString)
                End If
                ' ---------------------------------------------------------------------------------

            Else
                ' noop
            End If

        Catch csExp As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' 抽出条件文字列の生成（宛名付随標準・カナ姓名用）
    ''' </summary>
    ''' <param name="cSearchKey">検索キー</param>
    ''' <param name="strTable">テーブル名</param>
    ''' <param name="blnRireki">履歴区分</param>
    ''' <param name="intHyojunKB">標準化区分</param>
    ''' <returns>抽出条件文字列</returns>
    ''' <remarks></remarks>
    Public Function CreateWhereFZYHyojunKana(ByVal cSearchKey As ABAtenaSearchKey, ByVal strTable As String,
                                              ByVal blnRireki As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder

        Try

            ' デバッグログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE句の作成
            csWHERE = New StringBuilder(256)

            If (blnRireki) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") = -1) Then
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                        csWHERE.Append(")")
                    Else
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                        csWHERE.Append(")")
                    End If
                Else
                    Return String.Empty
                End If
            Else
                If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") = -1) Then
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                    csWHERE.Append(")")
                Else
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                    csWHERE.Append(")")
                End If
            End If

            ' デバッグログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            Throw cfAppExp

        Catch csExp As Exception

            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" _
                                      + "【メソッド名:" + THIS_METHOD_NAME + "】" _
                                      + "【エラー内容:" + csExp.Message + "】")
            Throw csExp

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' 抽出条件文字列の生成（宛名付随標準・漢字姓名用）
    ''' </summary>
    ''' <param name="cSearchKey">検索キー</param>
    ''' <param name="strTable">テーブル名</param>
    ''' <param name="blnRireki">履歴区分</param>
    ''' <param name="intHyojunKB">標準化区分</param>
    ''' <returns>抽出条件文字列</returns>
    ''' <remarks></remarks>
    Public Function CreateWhereFZYHyojunKanji(ByVal cSearchKey As ABAtenaSearchKey, ByVal strTable As String,
                                               ByVal blnRireki As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder

        Try

            ' デバッグログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE句の作成
            csWHERE = New StringBuilder(256)

            If (blnRireki) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") = -1) Then
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(")")
                    Else
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(")")
                    End If
                Else
                    Return String.Empty
                End If
            Else
                If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") = -1) Then
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(")")
                Else
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(")")
                End If
            End If

            ' デバッグログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            Throw cfAppExp

        Catch csExp As Exception

            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" _
                                      + "【メソッド名:" + THIS_METHOD_NAME + "】" _
                                      + "【エラー内容:" + csExp.Message + "】")
            Throw csExp

        End Try

        Return csWHERE.ToString

    End Function
End Class
