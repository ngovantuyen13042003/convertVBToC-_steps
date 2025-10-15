'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名取得(ABAtenaGetClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/06　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/02/19 000001     簡易宛名取得１で、管理情報が引き渡されないケースがある。
'*                       簡易宛名取得１で、業務コードが指定されていて、取得件数が１件の場合は、送付先データがなくても、送付先レコードを戻す
'* 2003/02/25 000002     簡易宛名取得１メッソッドで、16・17でデータ取得０件の場合は、エラーにせずにcsAtenaHとcsAtenaHS をマージして戻す。
'* 2003/02/26 000003     市町村コードの抽出条件を追加
'* 2003/03/07 000004     プロジェクトのImportsは定義しない（仕様変更）
'* 2003/03/07 000005     有効桁数対応（仕様変更）
'* 2003/03/17 000006     パラメータのチェックをはずす（仕様変更）
'* 2003/03/17 000007     業務"AB"固定でRDBをアクセスする（仕様変更）
'* 2003/03/18 000008     エラーメッセージの変更（仕様変更）
'* 2003/03/27 000009     エラー処理クラスの参照先を"AB"固定にする
'* 2003/04/18 000010     年金宛名取得メソッド・国保宛名履歴取得メソッドを追加
'* 2003/04/22 000011     データが取得出来なくても例外を発生させない
'* 2003/04/30 000012     データが取得できなかった場合も、0件で編集データを返す。
'* 2003/05/22 000013     RDBのConnectはﾒｿｯﾄﾞの先頭に変更(仕様変更)
'* 2003/06/17 000014     チューニング(管理情報取得を最小限にする)
'* 2003/08/21 000015     ＵＲキャッシュ対応／継承可能クラスに変更
'* 2003/09/08 000016     国保宛名履歴取得の仕様変更
'* 2003/10/09 000017     連絡先は、連絡先マスタにデータが存在する場合は、そちらから取得する。但し、業務コードが指定されてた場合のみに限る。
'*                       NenkinAtenaGetもAtenaGet1と同様に指定年月日が指定されたら、宛名履歴より取得する。連絡先も同様。但し、代納・合算は不要。
'* 2003/10/30 000018     p_strJukiJushoCDは8桁
'* 2003/10/30 000019     仕様変更：カタカナチェックをANKチェックに変更
'* 2003/11/19 000020     仕様追加：簡易宛名取得1(オーバーロード)メソッドの追加
'* 2003/12/01 000021     仕様変更：データ区分'1%'の場合、個人のみを取得する
'* 2003/12/02 000022     仕様変更：連絡先取得処理を宛名編集から宛名取得へ移動
'* 2004/08/27 000023     速度改善：（宮沢）
'* 2005/01/25 000024     速度改善２：（宮沢）
'* 2005/04/04 000025     全角でのあいまい検索を可能にする(マルゴ村山)
'* 2005/04/21 000026     代納・送付先の期間指定日をシステム日付にする
'* 2005/05/06 000027     パラメータチェックをTRIMしてから行なう。性別単独は許さない。
'* 2005/12/06 000028     CheckColumnValueメソッドで行政区ＣＤはＡＮＫチェックを行う。(マルゴ村山)
'* 2006/07/31 000029     年金宛名ゲットⅡ追加に伴う修正 (吉澤)
'* 2007/04/21 000030     介護版宛名取得メソッドの追加 (吉澤)
'* 2007/07/28 000031     同一人代表者取得機能の追加 (吉澤)
'* 2007/09/04 000032     外国人本名検索機能の追加：検索カナ名編集用メソッド追加（中沢）
'* 2007/09/13 000033     宛名取得パラメータの住民コードをトリムする「p_strJuminCD」 (吉澤)
'* 2007/10/10 000034     検索用カナ項目にアルファベットが入ってきた場合は大文字に変換（中沢）
'* 2007/10/10 000035     外国人本名検索で名前の先頭が「ウ」の場合の検索漏れ対応（中沢）
'* 2007/11/06 000036     検索カナ編集メソッド、仕様通り編集されない部分を修正（中沢）
'* 2008/01/17 000037     同一人代表者取得による住民コード誤りの不具合対応（吉澤）
'* 2008/01/17 000038     宛名個別情報を取得する時、個別事項取得区分を引数に設定するよう修正（比嘉）
'* 2008/02/17 000039     氏名簡略文字編集処理を追加（比嘉）
'* 2008/11/10 000040     利用届出取得処理を追加（比嘉）
'* 2008/11/17 000041     利用届該当データ絞込み処理の修正（比嘉）
'* 2008/11/18 000042     利用届出取得処理の追加に伴う、連絡先データ取得処理の改修（比嘉）
'* 2009/04/08 000043     検索キー無しでAtnaGet2を使用するとオブジェクト参照エラーが発生する不具合改修（中沢）
'* 2010/04/16 000044     VS2008対応（比嘉）
'* 2010/05/17 000045     本籍筆頭者及び処理停止区分対応（比嘉）
'* 2011/05/18 000046     外国人在留情報取得区分対応（比嘉）
'* 2011/11/07 000047     【AB17010】住基法改正区分追加対応（池田）
'* 2014/04/28 000048     【AB21040】＜共通番号対応＞共通番号取得区分追加（石合）
'* 2018/03/08 000049     【AB26001】履歴検索機能追加（石合）
'* 2020/01/31 000050     【AB00185】AtenaGet1以外の履歴検索機能追加（石合）
'* 2020/11/04 000051     【AB00189】利用届出複数納税者ID対応（須江）
'* 2023/03/10 000052     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
'* 2023/12/04 000053     【AB-1600-1】検索機能対応(下村)
'* 2024/03/07 000054     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
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
Imports System.Data
Imports System.Text
Imports System.Security

'************************************************************************************************
'*
'* 宛名取得に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaGetBClass

#Region " メンバ変数 "
    'パラメータのメンバ変数
    '* 履歴番号 000015 2003/08/21 修正開始
    'Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    'Private m_cfControlData As UFControlData                ' コントロールデータ
    'Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    'Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    'Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス

    'Private m_intHyojiketaJuminCD As Integer                ' 住民コード表示桁数
    'Private m_intHyojiketaStaiCD As Integer                 ' 世帯コード表示桁数
    'Private m_intHyojiketaJushoCD As Integer                ' 住所コード表示桁数（管内のみ）
    'Private m_intHyojiketaGyoseikuCD As Integer             ' 行政区コード表示桁数
    'Private m_intHyojiketaChikuCD1 As Integer               ' 地区コード１表示桁数
    'Private m_intHyojiketaChikuCD2 As Integer               ' 地区コード２表示桁数
    'Private m_intHyojiketaChikuCD3 As Integer               ' 地区コード３表示桁数
    'Private m_strChikuCD1HyojiMeisho As String              ' 地区コード１表示名称
    'Private m_strChikuCD2HyojiMeisho As String              ' 地区コード２表示名称
    'Private m_strChikuCD3HyojiMeisho As String              ' 地区コード３表示名称
    'Private m_strRenrakusaki1HyojiMeisho As String          ' 連絡先１表示名称
    'Private m_strRenrakusaki2HyojiMeisho As String          ' 連絡先２表示名称
    ''* 履歴番号 000014 2003/06/17 追加開始
    'Private m_blnKanriJoho As Boolean                       ' 管理情報取得
    ''* 履歴番号 000014 2003/06/17 追加終了

    ''　コンスタント定義
    'Private Const THIS_CLASS_NAME As String = "ABAtenaGetBClass"                ' クラス名
    'Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード

    Protected m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Protected m_cfControlData As UFControlData                ' コントロールデータ
    Protected m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Protected m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Protected m_cfErrorClass As UFErrorClass                  ' エラー処理クラス

    Protected m_intHyojiketaJuminCD As Integer                ' 住民コード表示桁数
    Protected m_intHyojiketaStaiCD As Integer                 ' 世帯コード表示桁数
    Protected m_intHyojiketaJushoCD As Integer                ' 住所コード表示桁数（管内のみ）
    Protected m_intHyojiketaGyoseikuCD As Integer             ' 行政区コード表示桁数
    Protected m_intHyojiketaChikuCD1 As Integer               ' 地区コード１表示桁数
    Protected m_intHyojiketaChikuCD2 As Integer               ' 地区コード２表示桁数
    Protected m_intHyojiketaChikuCD3 As Integer               ' 地区コード３表示桁数
    Protected m_strChikuCD1HyojiMeisho As String              ' 地区コード１表示名称
    Protected m_strChikuCD2HyojiMeisho As String              ' 地区コード２表示名称
    Protected m_strChikuCD3HyojiMeisho As String              ' 地区コード３表示名称
    Protected m_strRenrakusaki1HyojiMeisho As String          ' 連絡先１表示名称
    Protected m_strRenrakusaki2HyojiMeisho As String          ' 連絡先２表示名称
    Protected m_blnKanriJoho As Boolean                       ' 管理情報取得
    Protected m_blnBatch As Boolean                           ' バッチ区分(True:バッチ系, False:リアル系)
    Protected m_blnBatchRdb As Boolean
    Protected m_cABAtenaHenshuB As ABAtenaHenshuBClass                          ' 宛名編集クラス
    Protected m_cABBatchAtenaHenshuB As ABBatchAtenaHenshuBClass                ' 宛名編集クラス(バッチ系)
    '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
    Private m_cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
    Private m_cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
    Private m_cABSfskB As ABSfskBClass                        '送付先マスタＤＡクラス
    Private m_cABDainoB As ABDainoBClass                      '代納マスタＤＡクラス

    Private m_cUSSCityInfoClass As USSCityInfoClass           '市町村情報管理クラス
    Private m_cRenrakusakiBClass As ABRenrakusakiBClass       ' 連絡先Ｂクラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_cfURAtenaKanriJoho As URAtenaKanriJohoCacheBClass   '宛名管理情報キャッシュＢクラス
    '* 履歴番号 000023 2004/08/27 追加終了
    '*履歴番号 000032 2007/09/04 追加開始
    Private m_cURKanriJohoB As URKANRIJOHOBClass         '管理情報取得クラス
    'バッチから呼ばれた場合エラーが発生するため，キャッシュクラスはコメントアウト
    'Private m_cURKanriJohoB As URKANRIJOHOCacheBClass         '管理情報取得クラス
    '*履歴番号 000032 2007/09/04 追加終了

    '　コンスタント定義
    Protected Const THIS_CLASS_NAME As String = "ABAtenaGetBClass"              ' クラス名
    Protected Const THIS_BUSINESSID As String = "AB"                            ' 業務コード
    '* 履歴番号 000015 2003/08/21 修正終了

    '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
    Protected m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.KaniAll
    Protected m_cABAtenaRirekiBRef As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
    Protected m_cABAtenaBRef As ABAtenaBClass                      '宛名マスタＤＡクラス
    Protected m_cABSfskBRef As ABSfskBClass                        '送付先マスタＤＡクラス
    Protected m_cABDainoBRef As ABDainoBClass                      '代納マスタＤＡクラス
    '* 履歴番号 000024 2005/01/25 追加終了
    '* 履歴番号 000026 2005/04/21 追加開始
    Private m_strSystemDateTime As String                          '処理日時
    '* 履歴番号 000026 2005/04/21 追加終了

    '*履歴番号 000022 2007/04/28 追加開始
    Private m_blnSelectKaigo As ABEnumDefine.MethodKB  'メソッド区分（通常版か、介護版、、、）
    '*履歴番号 000022 2007/04/28 追加終了

    '*履歴番号 000031 2007/07/28 追加開始
    Dim m_cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              '管理情報Ｂクラス
    Dim m_cABGappeiDoitsuninB As ABGappeiDoitsuninBClass            '同一人Ｂクラス
    Dim m_strDoitsu_Param As String                    '同一人判定パラメータ
    Dim m_strHonninJuminCD As String                    '本人住民コード
    '*履歴番号 000031 2007/07/28 追加終了

    '*履歴番号 000042 2008/11/18 追加開始
    Dim m_blnMethodKB As ABEnumDefine.MethodKB
    '*履歴番号 000042 2008/11/18 追加終了

#End Region

#Region "プロパティ "
    '************************************************************************************************
    '* 各メンバ変数のプロパティ定義
    '************************************************************************************************
    Public ReadOnly Property p_intHyojiketaJuminCD() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaJuminCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaStaiCD() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaStaiCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaJushoCD() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaJushoCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaGyoseikuCD() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaGyoseikuCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD1() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaChikuCD1
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD2() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaChikuCD2
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD3() As Integer
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_intHyojiketaChikuCD3
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD1HyojiMeisho() As String
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_strChikuCD1HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD2HyojiMeisho() As String
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_strChikuCD2HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD3HyojiMeisho() As String
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_strChikuCD3HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strRenrakusaki1HyojiMeisho() As String
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_strRenrakusaki1HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strRenrakusaki2HyojiMeisho() As String
        Get
            '* 履歴番号 000014 2003/06/17 追加開始
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* 履歴番号 000014 2003/06/17 追加終了
            Return m_strRenrakusaki2HyojiMeisho
        End Get
    End Property
#End Region

#Region " コンストラクタ "
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        m_blnBatchRdb = False
        ' ＲＤＢクラスのインスタンス化
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)
        Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, True)
        '* 履歴番号 000024 2005/01/25 追加終了

        '* 履歴番号 000024 2005/01/25 削除開始（宮沢）
        '' メンバ変数セット
        'm_cfControlData = cfControlData
        'm_cfConfigDataClass = cfConfigDataClass

        '' ＲＤＢクラスのインスタンス化
        'm_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)

        '' ログ出力クラスのインスタンス化
        'm_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        '' パラメータのメンバ変数初期化
        'm_intHyojiketaJuminCD = 0                           '住民コード表示桁数
        'm_intHyojiketaStaiCD = 0                            '世帯コード表示桁数
        'm_intHyojiketaJushoCD = 0                           '住所コード表示桁数（管内のみ）
        'm_intHyojiketaGyoseikuCD = 0                        '行政区コード表示桁数
        'm_intHyojiketaChikuCD1 = 0                          '地区コード１表示桁数
        'm_intHyojiketaChikuCD2 = 0                          '地区コード２表示桁数
        'm_intHyojiketaChikuCD3 = 0                          '地区コード３表示桁数
        'm_strChikuCD1HyojiMeisho = String.Empty             '地区コード１表示名称
        'm_strChikuCD2HyojiMeisho = String.Empty             '地区コード２表示名称
        'm_strChikuCD3HyojiMeisho = String.Empty             '地区コード３表示名称
        'm_strRenrakusaki1HyojiMeisho = String.Empty         '連絡先１表示名称
        'm_strRenrakusaki2HyojiMeisho = String.Empty         '連絡先２表示名称
        ''* 履歴番号 000014 2003/06/17 追加開始
        '' 管理情報取得済みフラグの初期化
        'm_blnKanriJoho = False
        ''* 履歴番号 000014 2003/06/17 追加終了
        ''* 履歴番号 000015 2003/08/21 追加開始
        'm_blnBatch = False                                  ' バッチ区分
        ''* 履歴番号 000015 2003/08/21 追加終了
        'm_blnBatchRdb = False

        ''* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        ''宛名履歴マスタＤＡクラスのインスタンス作成
        'm_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''宛名マスタＤＡクラスのインスタンス作成
        'm_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''送付先マスタＤＡクラスのインスタンス作成
        'm_cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''代納マスタＤＡクラスのインスタンス作成
        'm_cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        'm_cUSSCityInfoClass = New USSCityInfoClass()
        'm_cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        'm_cfDateClass = New UFDateClass(m_cfConfigDataClass)
        ''* 履歴番号 000023 2004/08/27 追加終了
        '* 履歴番号 000024 2005/01/25 削除終了
    End Sub

    '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 　　                          ByVal blnSelectAll As Boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '*
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal blnSelectAll As Boolean)
        m_blnBatchRdb = False
        ' ＲＤＢクラスのインスタンス化
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)
        Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, blnSelectAll)
    End Sub
    '* 履歴番号 000024 2005/01/25 追加終了

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
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
        '* 履歴番号 000015 2003/08/21 追加開始
        m_blnBatchRdb = True                                  ' バッチ区分
        '* 履歴番号 000015 2003/08/21 追加終了
        Initial(cfControlData, cfConfigDataClass, cfRdbClass, True)
    End Sub
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 　　                          ByVal cfRdbClass As UFRdbClass, _
    '* 　　                          ByVal blnSelectAll As Boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        '* 履歴番号 000015 2003/08/21 追加開始
        m_blnBatchRdb = True                                  ' バッチ区分
        '* 履歴番号 000015 2003/08/21 追加終了
        Initial(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll)
    End Sub
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           'Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　           '               ByVal cfConfigDataClass As UFConfigDataClass)
    '* 構文           Public Sub Initial(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass,
    '* 　　                          ByVal blnSelectAll as boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
    'Public Sub New(ByVal cfControlData As UFControlData, _
    '               ByVal cfConfigDataClass As UFConfigDataClass, _
    '               ByVal cfRdbClass As UFRdbClass)
    <SecuritySafeCritical>
    Private Sub Initial(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        '* 履歴番号 000024 2005/01/25 更新終了
        m_cfRdbClass = cfRdbClass

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' パラメータのメンバ変数初期化
        m_intHyojiketaJuminCD = 0                           '住民コード表示桁数
        m_intHyojiketaStaiCD = 0                            '世帯コード表示桁数
        m_intHyojiketaJushoCD = 0                           '住所コード表示桁数（管内のみ）
        m_intHyojiketaGyoseikuCD = 0                        '行政区コード表示桁数
        m_intHyojiketaChikuCD1 = 0                          '地区コード１表示桁数
        m_intHyojiketaChikuCD2 = 0                          '地区コード２表示桁数
        m_intHyojiketaChikuCD3 = 0                          '地区コード３表示桁数
        m_strChikuCD1HyojiMeisho = String.Empty             '地区コード１表示名称
        m_strChikuCD2HyojiMeisho = String.Empty             '地区コード２表示名称
        m_strChikuCD3HyojiMeisho = String.Empty             '地区コード３表示名称
        m_strRenrakusaki1HyojiMeisho = String.Empty         '連絡先１表示名称
        m_strRenrakusaki2HyojiMeisho = String.Empty         '連絡先２表示名称
        '* 履歴番号 000014 2003/06/17 追加開始
        ' 管理情報取得済みフラグの初期化
        m_blnKanriJoho = False
        '* 履歴番号 000014 2003/06/17 追加終了
        '* 履歴番号 000015 2003/08/21 追加開始
        m_blnBatch = False                                  ' バッチ区分
        '* 履歴番号 000015 2003/08/21 追加終了

        '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        '宛名履歴マスタＤＡクラスのインスタンス作成

        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        'm_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        If (blnSelectAll = True) Then
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniAll
        Else
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniOnly
        End If
        m_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, True)
        m_cABAtenaRirekiBRef = m_cABAtenaRirekiB
        '* 履歴番号 000024 2005/01/25 更新終了

        '宛名マスタＤＡクラスのインスタンス作成
        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        'm_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        m_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, True)
        m_cABAtenaBRef = m_cABAtenaB
        '* 履歴番号 000024 2005/01/25 更新終了

        '送付先マスタＤＡクラスのインスタンス作成
        m_cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        m_cABSfskBRef = m_cABSfskB
        '* 履歴番号 000024 2005/01/25 追加終了(宮沢)
        '代納マスタＤＡクラスのインスタンス作成
        m_cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        m_cABDainoBRef = m_cABDainoB
        '* 履歴番号 000024 2005/01/25 追加終了(宮沢)

        m_cUSSCityInfoClass = New USSCityInfoClass
        m_cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
        '* 履歴番号 000023 2004/08/27 追加終了

        '* 履歴番号 000026 2005/04/21 追加開始
        m_strSystemDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")    '処理日時
        '* 履歴番号 000026 2005/04/21 追加終了

        '*履歴番号 000032 2007/09/04 追加開始
        'UR管理情報を取得
        If (m_cURKanriJohoB Is Nothing) Then
            m_cURKanriJohoB = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        End If
        'バッチから呼ばれた場合エラーが発生するため，コメントアウト
        'm_cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '*履歴番号 000032 2007/09/04 追加終了

    End Sub
#End Region

#Region " 簡易宛名取得１(AtenaGet1) "
    '************************************************************************************************
    '* メソッド名     簡易宛名取得１
    '* 
    '* 構文           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    '*履歴番号 000020 2003/11/19 修正開始
    'Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    Public Overloads Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        '*履歴番号 000020 2003/11/19 修正開始

        ''*履歴番号 000020 2003/11/19 修正終了
        'Const THIS_METHOD_NAME As String = "AtenaGet1"
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        'Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
        'Dim csDataTable As DataTable
        'Dim csDataSet As DataSet
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
        'Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
        'Dim cABSfskB As ABSfskBClass                        '送付先マスタＤＡクラス
        'Dim cABDainoB As ABDainoBClass                      '代納マスタＤＡクラス
        ''*履歴番号 000015 2003/08/21 削除開始
        ''Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
        ''*履歴番号 000015 2003/08/21 削除終了
        'Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaH As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaHS As DataSet                            '宛名情報(ABAtena1)
        'Dim csAtenaD As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaDS As DataSet                            '宛名情報(ABAtena1)
        'Dim strStaiCD As String                             '世帯コード
        'Dim intHyojiKensu As Integer                        '最大取得件数
        'Dim intGetCount As Integer                          '取得件数
        'Dim strKikanYM As String                            '期間年月
        'Dim strDainoKB As String                            '代納区分
        'Dim strGyomuCD As String                            '業務コード
        'Dim strGyomunaiSHU_CD As String                     '業務内種別コード
        'Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
        'Dim strShichosonCD As String                        '市町村コード

        'Try
        '    ' デバッグ開始ログ出力
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        '    ' RDBアクセスログ出力
        '    m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '                                    "【実行メソッド名:Connect】")
        '    'ＲＤＢ接続
        '    m_cfRdbClass.Connect()

        '    Try
        '        '* 履歴番号 000014 2003/06/17 削除開始
        '        '' 管理情報取得(内部処理)メソッドを実行する。
        '        'Me.GetKanriJoho()
        '        '* 履歴番号 000014 2003/06/17 削除終了

        '        'パラメータチェック
        '        Me.CheckColumnValue(cAtenaGetPara1)

        '        '宛名履歴マスタＤＡクラスのインスタンス作成
        '        cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '宛名マスタＤＡクラスのインスタンス作成
        '        cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '送付先マスタＤＡクラスのインスタンス作成
        '        cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '代納マスタＤＡクラスのインスタンス作成
        '        cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '*履歴番号 000015 2003/08/21 修正開始
        '        ''宛名編集クラスのインスタンス作成
        '        'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        If (m_blnBatch) Then
        '            '宛名編集バッチクラスのインスタンス作成
        '            m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        Else
        '            '宛名編集クラスのインスタンス作成
        '            m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        End If
        '        '*履歴番号 000015 2003/08/21 修正終了

        '        '*履歴追加 000003 2003/02/26 追加開始
        '        'USSCityInfoClass.GetCityInfo()を使用して、直近市町村情報取得を取得する。
        '        cUSSCityInfoClass.GetCityInfo(m_cfControlData)

        '        '市町村コードの内容を設定する。
        '        If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
        '            strShichosonCD = cUSSCityInfoClass.p_strShichosonCD(0)
        '        Else
        '            strShichosonCD = cAtenaGetPara1.p_strShichosonCD
        '        End If
        '        '*履歴追加 000003 2003/02/26 追加終了

        '        '世帯コードの指定がなく、世帯員編集の指示がある場合
        '        If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

        '            '宛名検索キーのインスタンス化
        '            cSearchKey = New ABAtenaSearchKey()

        '            '住民コードの設定
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

        '            '住基・住登外区分が<>"1"の場合
        '            If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '                cSearchKey.p_strJutogaiYusenKB = "1"
        '            End If

        '            '住基・住登外区分が="1"の場合
        '            If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '                cSearchKey.p_strJuminYuseniKB = "1"
        '            End If

        '            '指定年月日が指定されている場合
        '            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '「宛名履歴マスタ抽出」メゾットを実行する
        '                csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数が１件でない場合、エラー
        '                If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
        '            End If

        '            '指定年月日が指定されていない場合
        '            If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '「宛名マスタ抽出」メゾットを実行する
        '                csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数が１件でない場合、エラー
        '                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                '世帯コードがNULLの場合、エラー
        '                If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
        '            End If
        '            cAtenaGetPara1.p_strStaiCD = strStaiCD
        '            cAtenaGetPara1.p_strJuminCD = String.Empty
        '        End If

        '        cSearchKey = Nothing
        '        cSearchKey = New ABAtenaSearchKey()

        '        '世帯員編集が"1"の場合
        '        If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '        Else
        '            '宛名取得パラメータから宛名検索キーにセットする
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
        '            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
        '            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
        '            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
        '            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
        '            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
        '            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
        '            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
        '            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
        '            cSearchKey.p_strShichosonCD = strShichosonCD
        '        End If

        '        '住基・住登外区分が<>"1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '住基・住登外区分が="1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '住所～番地コード3のセット
        '        '住登外優先の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '住基優先の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            '*履歴番号 000018 2003/10/30 修正開始
        '            'cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
        '            '*履歴番号 000018 2003/10/30 修正終了
        '            cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '最大取得件数をセットする
        '        If cAtenaGetPara1.p_intHyojiKensu = 0 Then
        '            intHyojiKensu = 100
        '        Else
        '            intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
        '        End If

        '        '指定年月日が指定されている場合
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            '「宛名履歴マスタ抽出」メゾットを実行する
        '            csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
        '                                                        cSearchKey, _
        '                                                        cAtenaGetPara1.p_strShiteiYMD, _
        '                                                        cAtenaGetPara1.p_blnSakujoFG)

        '            intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '            '*履歴番号 000015 2003/08/21 修正開始
        '            ''「宛名編集」の「履歴編集」メソッドを実行する
        '            'csAtenaH = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '            Else
        '                '「宛名編集」の「履歴編集」メソッドを実行する
        '                csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '            End If
        '            '*履歴番号 000015 2003/08/21 修正終了
        '        End If

        '        '指定年月日が指定されていない場合
        '        If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            '「宛名マスタ抽出」メゾットを実行する
        '            csDataSet = cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '            '*履歴番号 000015 2003/08/21 修正開始
        '            ''「宛名編集」の「宛名編集」メソッドを実行する
        '            'csAtenaH = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)

        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「宛名編集」メソッドを実行する
        '                csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '            Else
        '                '「宛名編集」の「宛名編集」メソッドを実行する
        '                csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '            End If
        '            '*履歴番号 000015 2003/08/21 修正終了

        '        End If

        '        '取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
        '        If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

        '            csAtena1 = csAtenaH

        '            Exit Try
        '        End If

        '        '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
        '        csDataSet = cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                           cAtenaGetPara1.p_strGyomuCD, _
        '                                           cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                           strKikanYM, _
        '                                           cAtenaGetPara1.p_blnSakujoFG)


        '        '*履歴番号 000015 2003/08/21 修正開始
        '        ''「宛名編集」の「送付先編集」メソッドを実行する
        '        'csAtenaHS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)

        '        If (m_blnBatch) Then
        '            '「宛名編集バッチ」の「送付先編集」メソッドを実行する
        '            csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '        Else
        '            '「宛名編集」の「送付先編集」メソッドを実行する
        '            csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '        End If
        '        '*履歴番号 000015 2003/08/21 修正終了

        '        '指定年月日が指定してある場合
        '        If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
        '        csDataSet = cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                             cAtenaGetPara1.p_strGyomuCD, _
        '                                             cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                             strKikanYM, _
        '                                             cAtenaGetPara1.p_blnSakujoFG)

        '        '取得件数が1件でない場合
        '        If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

        '            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '            Exit Try
        '        End If

        '        With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

        '            '代納区分を退避する
        '            strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

        '            '業務コードを退避する
        '            strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

        '            '業務内種別コードを退避する
        '            strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

        '            '宛名検索キーにセットする
        '            cSearchKey = Nothing
        '            cSearchKey = New ABAtenaSearchKey()

        '            cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

        '        End With

        '        '住基・住登外区分が<>"1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '住基・住登外区分が="1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '⑯指定年月日が指定されている場合
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
        '            '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
        '            csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                        cSearchKey, _
        '                                                        cAtenaGetPara1.p_strShiteiYMD, _
        '                                                        cAtenaGetPara1.p_blnSakujoFG)

        '            '取得件数
        '            intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '            '取得件数が０件の場合、
        '            If (intGetCount = 0) Then

        '                'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '                Exit Try
        '            End If

        '            '*履歴番号 000015 2003/08/21 修正開始
        '            ''「宛名編集」の「履歴編集」メソッドを実行する
        '            'csAtenaD = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '            '                                        strGyomuCD, strGyomunaiSHU_CD)

        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                        strGyomuCD, strGyomunaiSHU_CD)
        '            Else
        '                '「宛名編集」の「履歴編集」メソッドを実行する
        '                csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                        strGyomuCD, strGyomunaiSHU_CD)
        '            End If
        '            '*履歴番号 000015 2003/08/21 修正終了

        '        Else
        '            '⑰指定年月日が指定されていない場合

        '            '「宛名マスタ抽出」メゾットを実行する
        '            csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '            '取得件数
        '            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '            '取得件数が０件の場合、
        '            If (intGetCount = 0) Then

        '                'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '                Exit Try
        '            End If

        '            '*履歴番号 000015 2003/08/21 修正開始
        '            ''「宛名編集」の「宛名編集」メソッドを実行する
        '            'csAtenaD = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '            '                                       strGyomuCD, strGyomunaiSHU_CD)

        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「宛名編集」メソッドを実行する
        '                csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                       strGyomuCD, strGyomunaiSHU_CD)
        '            Else
        '                '「宛名編集」の「宛名編集」メソッドを実行する
        '                csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                       strGyomuCD, strGyomunaiSHU_CD)
        '            End If
        '            '*履歴番号 000015 2003/08/21 修正終了

        '        End If

        '        '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
        '        csDataSet = cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '                                           cAtenaGetPara1.p_strGyomuCD, _
        '                                           cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                           strKikanYM, _
        '                                           cAtenaGetPara1.p_blnSakujoFG)

        '        '*履歴番号 000015 2003/08/21 修正開始
        '        ''「宛名編集」の「送付先編集」メソッドを実行する
        '        'csAtenaDS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)

        '        If (m_blnBatch) Then
        '            '「宛名編集バッチ」の「送付先編集」メソッドを実行する
        '            csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '        Else
        '            '「宛名編集」の「送付先編集」メソッドを実行する
        '            csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '        End If
        '        '*履歴番号 000015 2003/08/21 修正終了

        '        'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '        csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)

        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
        '        ' ワーニングログ出力
        '        m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '                                "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
        '                                "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
        '        ' UFAppExceptionをスローする
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' エラーをそのままスロー
        '        Throw

        '    Finally
        '        ' RDBアクセスログ出力
        '        m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '                                "【実行メソッド名:Disconnect】")
        '        ' RDB切断
        '        m_cfRdbClass.Disconnect()
        '    End Try

        '    ' デバッグ終了ログ出力
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        'Catch objAppExp As UFAppException
        '    ' ワーニングログ出力
        '    m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '                                "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        '                                "【ワーニング内容:" + objAppExp.Message + "】")
        '    ' エラーをそのままスローする
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' エラーログ出力
        '    m_cfLogClass.ErrorWrite(m_cfControlData, _
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '                                "【エラー内容:" + objExp.Message + "】")
        '    Throw objExp
        'End Try

        'Return csAtena1

        Return AtenaGet1(cAtenaGetPara1, False)
        '*履歴番号 000020 2003/11/19 修正終了

    End Function
#End Region

#Region " 簡易宛名取得１(AtenaGet1) "
    '*履歴番号 000020 2003/11/19 追加開始
    '************************************************************************************************
    '* メソッド名     簡易宛名取得１
    '* 
    '* 構文           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
    '* 
    '* 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                        ByVal blnKobetsu As Boolean) As DataSet
        '*履歴番号 000030 2007/04/21 修正開始
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        'Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
        'Dim csDataTable As DataTable
        'Dim csDataSet As DataSet
        ''* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        ''Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
        ''Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
        ''Dim cABSfskB As ABSfskBClass                        '送付先マスタＤＡクラス
        ''Dim cABDainoB As ABDainoBClass                      '代納マスタＤＡクラス
        ''* 履歴番号 000023 2004/08/27 削除終了
        'Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaH As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaHS As DataSet                            '宛名情報(ABAtena1)
        'Dim csAtenaD As DataSet                             '宛名情報(ABAtena1)
        'Dim csAtenaDS As DataSet                            '宛名情報(ABAtena1)
        'Dim strStaiCD As String                             '世帯コード
        'Dim intHyojiKensu As Integer                        '最大取得件数
        'Dim intGetCount As Integer                          '取得件数
        'Dim strKikanYM As String                            '期間年月
        'Dim strDainoKB As String                            '代納区分
        'Dim strGyomuCD As String                            '業務コード
        'Dim strGyomunaiSHU_CD As String                     '業務内種別コード
        ''* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        ''Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
        ''* 履歴番号 000023 2004/08/27 削除終了
        'Dim strShichosonCD As String                        '市町村コード

        ''* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        'Dim csWkAtena As DataSet                             '宛名情報(ABAtena1)
        ''* 履歴番号 000024 2005/01/25 追加終了

        'Try
        '    ' デバッグ開始ログ出力
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '    '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '    ' RDBアクセスログ出力
        '    'm_cfLogClass.RdbWrite(m_cfControlData, _
        '    '                                "【クラス名:" + Me.GetType.Name + "】" + _
        '    '                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '    '                                "【実行メソッド名:Connect】")
        '    '* 履歴番号 000023 2004/08/27 削除終了
        '    'ＲＤＢ接続
        '    If m_blnBatchRdb = False Then
        '        '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        '        ' RDBアクセスログ出力
        '        m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                        "【クラス名:" + Me.GetType.Name + "】" + _
        '                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '                                        "【実行メソッド名:Connect】")
        '        '* 履歴番号 000023 2004/08/27 追加終了
        '        m_cfRdbClass.Connect()
        '    End If
        '    Try
        '        'パラメータチェック
        '        Me.CheckColumnValue(cAtenaGetPara1)
        '        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '        ''宛名履歴マスタＤＡクラスのインスタンス作成
        '        'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''宛名マスタＤＡクラスのインスタンス作成
        '        'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''送付先マスタＤＡクラスのインスタンス作成
        '        'cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''代納マスタＤＡクラスのインスタンス作成
        '        'cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        '* 履歴番号 000023 2004/08/27 削除開始

        '        If (m_blnBatch) Then
        '            If (m_cABBatchAtenaHenshuB Is Nothing) Then
        '                '宛名編集バッチクラスのインスタンス作成
        '                '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '                'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* 履歴番号 000024 2005/01/25 更新終了
        '            End If
        '        Else
        '            If (m_cABAtenaHenshuB Is Nothing) Then
        '                '宛名編集クラスのインスタンス作成
        '                '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '                'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* 履歴番号 000024 2005/01/25 更新終了
        '            End If
        '        End If

        '        'USSCityInfoClass.GetCityInfo()を使用して、直近市町村情報取得を取得する。
        '        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '        'cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        '        '* 履歴番号 000023 2004/08/27 削除終了

        '        '市町村コードの内容を設定する。
        '        If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
        '            strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
        '        Else
        '            strShichosonCD = cAtenaGetPara1.p_strShichosonCD
        '        End If

        '        '世帯コードの指定がなく、世帯員編集の指示がある場合
        '        If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

        '            '宛名検索キーのインスタンス化
        '            cSearchKey = New ABAtenaSearchKey

        '            '住民コードの設定
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

        '            '住基・住登外区分が<>"1"の場合
        '            If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '                cSearchKey.p_strJutogaiYusenKB = "1"
        '            End If

        '            '住基・住登外区分が="1"の場合
        '            If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '                cSearchKey.p_strJuminYuseniKB = "1"
        '            End If

        '            '指定年月日が指定されている場合
        '            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '「宛名履歴マスタ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数が１件でない場合、エラー
        '                If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
        '            End If

        '            '指定年月日が指定されていない場合
        '            If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '「宛名マスタ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数が１件でない場合、エラー
        '                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                '世帯コードがNULLの場合、エラー
        '                If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
        '                    'エラー定義を取得
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
        '            End If
        '            cAtenaGetPara1.p_strStaiCD = strStaiCD
        '            cAtenaGetPara1.p_strJuminCD = String.Empty
        '        End If

        '        cSearchKey = Nothing
        '        cSearchKey = New ABAtenaSearchKey

        '        '世帯員編集が"1"の場合
        '        If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '        Else
        '            '宛名取得パラメータから宛名検索キーにセットする
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
        '            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
        '            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
        '            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
        '            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
        '            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
        '            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
        '            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
        '            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
        '            cSearchKey.p_strShichosonCD = strShichosonCD
        '        End If

        '        '住基・住登外区分が<>"1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '住基・住登外区分が="1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '住所～番地コード3のセット
        '        '住登外優先の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '住基優先の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
        '            cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '最大取得件数をセットする
        '        If cAtenaGetPara1.p_intHyojiKensu = 0 Then
        '            intHyojiKensu = 100
        '        Else
        '            intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
        '        End If

        '        '指定年月日が指定されている場合
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            ' 宛名個別情報の場合
        '            If (blnKobetsu) Then
        '                '「宛名個別履歴データ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
        '                                                              cSearchKey, _
        '                                                              cAtenaGetPara1.p_strShiteiYMD, _
        '                                                              cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '「宛名編集」の「履歴編集」メソッドを実行する
        '                    csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            Else
        '                '「宛名履歴マスタ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '「宛名編集」の「履歴編集」メソッドを実行する
        '                    csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            End If
        '        Else
        '            '指定年月日が指定されていない場合

        '            ' 宛名個別情報の場合
        '            If (blnKobetsu) Then
        '                '「宛名個別情報抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「宛名個別編集」メソッドを実行する
        '                    csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '「宛名編集」の「宛名個別編集」メソッドを実行する
        '                    csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            Else
        '                '「宛名マスタ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「宛名編集」メソッドを実行する
        '                    csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '「宛名編集」の「宛名編集」メソッドを実行する
        '                    csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '                End If

        '            End If

        '        End If

        '        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        '        csWkAtena = csDataSet
        '        '* 履歴番号 000024 2005/01/25 追加終了

        '        '*履歴番号 000022 2003/12/02 追加開始
        '        ' 連絡先編集処理

        '        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '        'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH)
        '        Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH, csWkAtena)
        '        '* 履歴番号 000024 2005/01/25 更新終了
        '        '*履歴番号 000022 2003/12/02 追加終了

        '        '取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
        '        If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

        '            csAtena1 = csAtenaH

        '            Exit Try
        '        End If

        '        '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* 履歴番号 000026 2005/04/21 修正開始
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* 履歴番号 000026 2005/04/21 修正終了
        '        End If

        '        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '        ''「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
        '        'csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '        '                                   cAtenaGetPara1.p_strGyomuCD, _
        '        '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                   strKikanYM, _
        '        '                                   cAtenaGetPara1.p_blnSakujoFG)
        '        '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
        '        If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
        '            '送付先があるので読み込む
        '            csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                               cAtenaGetPara1.p_strGyomuCD, _
        '                                               cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                               strKikanYM, _
        '                                               cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '送付先が無いので、空のテーブル作成
        '            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
        '        End If
        '        '* 履歴番号 000024 2005/01/25 更新終了

        '        ' 宛名個別情報の場合
        '        If (blnKobetsu) Then
        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「送付先個別編集」メソッドを実行する
        '                csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            Else
        '                '「宛名編集」の「送付先個別編集」メソッドを実行する
        '                csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「送付先編集」メソッドを実行する
        '                csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            Else
        '                '「宛名編集」の「送付先編集」メソッドを実行する
        '                csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            End If
        '        End If

        '        '指定年月日が指定してある場合
        '        If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* 履歴番号 000026 2005/04/21 修正開始
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* 履歴番号 000026 2005/04/21 修正終了
        '        End If

        '        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '        ''「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
        '        'csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '        '                                     cAtenaGetPara1.p_strGyomuCD, _
        '        '                                     cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                     strKikanYM, _
        '        '                                     cAtenaGetPara1.p_blnSakujoFG)
        '        '「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
        '        If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0) Then
        '            '代納があるので読み込む
        '            csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                                cAtenaGetPara1.p_strGyomuCD, _
        '                                                 cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                                 strKikanYM, _
        '                                                 cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '代納が無いので、空のテーブル作成
        '            csDataSet = m_cABDainoB.GetDainoSchemaBHoshu()
        '        End If
        '        '* 履歴番号 000024 2005/01/25 更新終了

        '        '取得件数が1件でない場合
        '        If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

        '            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)

        '            Exit Try
        '        End If

        '        With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

        '            '代納区分を退避する
        '            strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

        '            '業務コードを退避する
        '            strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

        '            '業務内種別コードを退避する
        '            strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

        '            '宛名検索キーにセットする
        '            cSearchKey = Nothing
        '            cSearchKey = New ABAtenaSearchKey

        '            cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

        '        End With

        '        '住基・住登外区分が<>"1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '住基・住登外区分が="1"の場合
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '⑯指定年月日が指定されている場合
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then

        '            ' 宛名個別情報の場合
        '            If (blnKobetsu) Then

        '                '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                              cSearchKey, _
        '                                                              cAtenaGetPara1.p_strShiteiYMD, _
        '                                                              cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数
        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '                '取得件数が０件の場合、
        '                If (intGetCount = 0) Then

        '                    'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「履歴個別編集」メソッドを実行する
        '                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '「宛名編集」の「履歴個別編集」メソッドを実行する
        '                    csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            Else
        '                '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数
        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '                '取得件数が０件の場合、
        '                If (intGetCount = 0) Then

        '                    'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '「宛名編集」の「履歴編集」メソッドを実行する
        '                    csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            End If
        '        Else

        '            '⑰指定年月日が指定されていない場合
        '            ' 宛名個別情報の場合
        '            If (blnKobetsu) Then

        '                '「宛名個別データ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数
        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '                '取得件数が０件の場合、
        '                If (intGetCount = 0) Then

        '                    'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「宛名編集」メソッドを実行する
        '                    csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '「宛名編集」の「宛名編集」メソッドを実行する
        '                    csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                End If

        '            Else

        '                '「宛名マスタ抽出」メゾットを実行する
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '取得件数
        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '                '取得件数が０件の場合、
        '                If (intGetCount = 0) Then

        '                    'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '「宛名編集バッチ」の「宛名編集」メソッドを実行する
        '                    csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '「宛名編集」の「宛名編集」メソッドを実行する
        '                    csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            End If
        '        End If

        '        '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* 履歴番号 000026 2005/04/21 修正開始
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* 履歴番号 000026 2005/04/21 修正終了
        '        End If

        '        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '        '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
        '        'csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '        '                                   cAtenaGetPara1.p_strGyomuCD, _
        '        '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                   strKikanYM, _
        '        '                                   cAtenaGetPara1.p_blnSakujoFG)
        '        If (csDataSet.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
        '            '送付先があるので読み込む
        '            csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '                                               cAtenaGetPara1.p_strGyomuCD, _
        '                                               cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                               strKikanYM, _
        '                                               cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '送付先が無いので、空のテーブル作成
        '            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
        '        End If
        '        '* 履歴番号 000024 2005/01/25 更新終了

        '        ' 宛名個別情報の場合
        '        If (blnKobetsu) Then
        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「送付先編集」メソッドを実行する
        '                csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            Else
        '                '「宛名編集」の「送付先編集」メソッドを実行する
        '                csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '「宛名編集バッチ」の「送付先編集」メソッドを実行する
        '                csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            Else
        '                '「宛名編集」の「送付先編集」メソッドを実行する
        '                csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            End If
        '        End If

        '        'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
        '        csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)



        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
        '        ' ワーニングログ出力
        '        m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "【クラス名:" + Me.GetType.Name + "】" + _
        '                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '                                "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
        '                                "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
        '        ' UFAppExceptionをスローする
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' エラーをそのままスロー
        '        Throw

        '    Finally
        '        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '        ' RDBアクセスログ出力
        '        'm_cfLogClass.RdbWrite(m_cfControlData, _
        '        '                        "【クラス名:" + Me.GetType.Name + "】" + _
        '        '                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '        '                        "【実行メソッド名:Disconnect】")
        '        '* 履歴番号 000023 2004/08/27 削除終了
        '        ' RDB切断
        '        If m_blnBatchRdb = False Then
        '            '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        '            ' RDBアクセスログ出力
        '            m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                    "【クラス名:" + Me.GetType.Name + "】" + _
        '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '                                    "【実行メソッド名:Disconnect】")
        '            '* 履歴番号 000023 2004/08/27 追加終了
        '            m_cfRdbClass.Disconnect()
        '        End If
        '    End Try

        '    ' デバッグ終了ログ出力
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Catch objAppExp As UFAppException
        '    ' ワーニングログ出力
        '    m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "【クラス名:" + Me.GetType.Name + "】" + _
        '                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '                                "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        '                                "【ワーニング内容:" + objAppExp.Message + "】")
        '    ' エラーをそのままスローする
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' エラーログ出力
        '    m_cfLogClass.ErrorWrite(m_cfControlData, _
        '                                "【クラス名:" + Me.GetType.Name + "】" + _
        '                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        '                                "【エラー内容:" + objExp.Message + "】")
        '    Throw objExp
        'End Try

        'Return csAtena1

        Return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Tsujo)
        '*履歴番号 000030 2007/04/21 修正終了

    End Function
    '*履歴番号 000020 2003/11/19 追加終了
#End Region

    '*履歴番号 000030 2007/04/21 追加開始
#Region " 宛名取得メイン（簡易宛名取得１、介護用宛名取得） "
    '************************************************************************************************
    '* メソッド名     宛名取得メイン（簡易宛名取得１、介護用宛名取得）
    '* 
    '* 構文           Public Function AtenaGetMain(ByVal cAtenaGetPara1 As ABAtenaGetPara1, _
    '*                    ByVal blnKobetsu As Boolean, ByVal MethodKB As ABEnumDefine.MethodKB) As DataSet
    '*
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
    '* 　　           MethodKB         : callされたメソッドの種類を表す
    '* 
    '* 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
    '************************************************************************************************
    Private Function AtenaGetMain(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                  ByVal blnKobetsu As Boolean, ByVal blnMethodKB As ABEnumDefine.MethodKB,
                                  ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csDataSet As DataSet
        Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
        Dim csAtenaH As DataSet                             '宛名情報(ABAtena1)
        Dim csAtenaHS As DataSet                            '宛名情報(ABAtena1)
        Dim csAtenaD As DataSet                             '宛名情報(ABAtena1)
        Dim csAtenaDS As DataSet                            '宛名情報(ABAtena1)
        Dim strStaiCD As String                             '世帯コード
        Dim intHyojiKensu As Integer                        '最大取得件数
        Dim intGetCount As Integer                          '取得件数
        Dim strKikanYMD As String                           '期間年月日
        Dim strDainoKB As String                            '代納区分
        Dim strGyomuCD As String                            '業務コード
        Dim strGyomunaiSHU_CD As String                     '業務内種別コード
        Dim strShichosonCD As String                        '市町村コード
        Dim csWkAtena As DataSet                             '宛名情報(ABAtena1)

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '=====================================================================================================================
            '== １．ＲＤＢ接続
            '==　　　　
            '==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
            '==　　　　
            '=====================================================================================================================
            If m_blnBatchRdb = False Then
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + Me.GetType.Name + "】" +
                                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                                "【実行メソッド名:Connect】")
                m_cfRdbClass.Connect()
            End If

            Try
                '=====================================================================================================================
                '== ２．宛名取得パラメータチェック
                '==　　　　
                '==　　　　<説明>　パラメータクラスに指定された内容をチェックする。
                '==　　　　
                '=====================================================================================================================
                Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)

                '=====================================================================================================================
                '== ３．各種クラスのインスタンス化
                '==　　　　
                '==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
                '==　　　　
                '=====================================================================================================================
                If (m_blnBatch) Then
                    If (m_cABBatchAtenaHenshuB Is Nothing) Then
                        '宛名編集バッチクラスのインスタンス作成
                        m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                        m_cABBatchAtenaHenshuB.m_blnMethodKB = blnMethodKB               '宛名編集Ｂクラス
                    End If
                    m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
                Else
                    If (m_cABAtenaHenshuB Is Nothing) Then
                        '宛名編集クラスのインスタンス作成
                        m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                        '実行メソッドにより出力レイアウトを変更する
                        m_cABAtenaHenshuB.m_blnMethodKB = blnMethodKB               '宛名編集Ｂクラス
                    End If
                    m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
                End If
                '実行メソッドにより出力レイアウトを変更する
                m_cABAtenaB.m_blnMethodKB = blnMethodKB                             '宛名Ｂクラス
                m_cABAtenaRirekiB.m_blnMethodKB = blnMethodKB                      '宛名履歴Ｂクラス
                m_cABAtenaB.m_intHyojunKB = intHyojunKB
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

                '*履歴番号 000042 2008/11/18 追加開始
                m_blnMethodKB = blnMethodKB
                '*履歴番号 000042 2008/11/18 追加終了

                '*履歴番号 000045 2010/05/17 追加開始
                ' 宛名Ｂクラス各種プロパティをセット
                m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                '*履歴番号 000046 2011/05/18 追加開始
                m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
                '*履歴番号 000046 2011/05/18 追加終了
                '*履歴番号 000047 2011/11/07 追加開始
                m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
                '*履歴番号 000047 2011/11/07 追加終了
                '*履歴番号 000048 2014/04/28 追加開始
                m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
                '*履歴番号 000048 2014/04/28 追加終了

                ' 宛名履歴Ｂクラス各種プロパティをセット
                m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                '*履歴番号 000046 2011/05/18 追加開始
                m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
                '*履歴番号 000046 2011/05/18 追加終了
                '*履歴番号 000047 2011/11/07 追加開始
                m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
                '*履歴番号 000047 2011/11/07 追加終了
                '*履歴番号 000045 2010/05/17 追加終了
                '*履歴番号 000048 2014/04/28 追加開始
                m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
                '*履歴番号 000048 2014/04/28 追加終了

                '=====================================================================================================================
                '== ４．市町村コード設定
                '==　　　　
                '==　　　　<説明>　＠市町村コードの指定がない場合は、現在(直近)の市町村コードを設定する。
                '==　　　　
                '=====================================================================================================================
                If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                    strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
                Else
                    strShichosonCD = cAtenaGetPara1.p_strShichosonCD
                End If


                '=====================================================================================================================
                '== ５．世帯員編集時の世帯コードを取得
                '==　　　　
                '==　　　　<説明>　＠世帯員編集の指定がある場合は、＠世帯コードを使用し世帯員を取得する。
                '==　　　　　　　　＠世帯コードが指定されていなかった場合は＠住民コードにより世帯コードの取得を行う。
                '==　　　　
                '=====================================================================================================================
                '世帯コードの指定がなく、世帯員編集の指示がある場合
                If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

                    '宛名検索キーのインスタンス化
                    cSearchKey = New ABAtenaSearchKey

                    '住民コードの設定
                    cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

                    '住基・住登外区分が<>"1"の場合
                    If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                        cSearchKey.p_strJutogaiYusenKB = "1"
                    End If

                    '住基・住登外区分が="1"の場合
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        cSearchKey.p_strJuminYuseniKB = "1"
                    End If

                    '指定年月日が指定されている場合
                    If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                        '「宛名履歴マスタ抽出」メゾットを実行する
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)

                        '取得件数が１件でない場合、エラー
                        If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                        End If

                        strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
                    End If

                    '指定年月日が指定されていない場合
                    If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                        '「宛名マスタ抽出」メゾットを実行する
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                             cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        '取得件数が１件でない場合、エラー
                        If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                        End If

                        '世帯コードがNULLの場合、エラー
                        If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                        End If

                        strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
                    End If
                    cAtenaGetPara1.p_strStaiCD = strStaiCD
                    cAtenaGetPara1.p_strJuminCD = String.Empty
                End If



                '*履歴番号 000031 2007/07/28 追加開始
                '=====================================================================================================================
                '== ６．同一人代表者取得処理
                '==　　　　
                '==　　　　<説明>　住民コード・住登外優先・同一人判定FG有効の検索条件の場合のみ、同一人代表者取得を行う。
                '==　　　　　　　　管理情報により、ユーザごとの取得判定有り。
                '==　　　　
                '=====================================================================================================================
                '同一人代表者住民コードを検索パラメータに上書きする
                GetDaihyoJuminCD(cAtenaGetPara1)
                '*履歴番号 000031 2007/07/28 追加終了



                '=====================================================================================================================
                '== ７．本人宛名取得検索キーの設定
                '==　　　　
                '==　　　　<説明>　本人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
                '==　　　　　　　　最大取得件数も取得する。
                '==　　　　
                '=====================================================================================================================
                '検索キークラスの初期化とインスタンス化
                cSearchKey = Nothing
                cSearchKey = New ABAtenaSearchKey

                '世帯員編集が"1"の場合
                If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
                    cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
                Else
                    '宛名取得パラメータから宛名検索キーにセットする
                    cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
                    cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
                    cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
                    cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
                    cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
                    cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
                    cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
                    cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
                    cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
                    cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
                    cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
                    cSearchKey.p_strShichosonCD = strShichosonCD

                    '*履歴番号 000032 2007/09/04 追加開始
                    '検索用カナ姓名・検索用カナ姓・検索用カナ名の編集
                    cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen)
                    '*履歴番号 000032 2007/09/04 追加終了

                    '*履歴番号 000048 2014/04/28 追加開始
                    cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13)
                    cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB
                    cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB
                    '*履歴番号 000048 2014/04/28 追加終了
                    cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji
                    cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji
                    cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei
                    cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho
                    cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki
                    cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki
                End If

                '住基・住登外区分が<>"1"の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJutogaiYusenKB = "1"
                End If

                '住基・住登外区分が="1"の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJuminYuseniKB = "1"
                End If

                '住所～番地コード3のセット
                '住登外優先の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD
                    cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                    cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                    cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                    cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                    cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                    cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                    cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
                End If

                '住基優先の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8)
                    cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                    cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                    cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                    cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                    cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                    cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                    cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
                End If

                '*履歴番号 000049 2018/03/08 追加開始
                ' 履歴検索フラグ
                cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch
                '*履歴番号 000049 2018/03/08 追加終了

                '最大取得件数をセットする
                If cAtenaGetPara1.p_intHyojiKensu = 0 Then
                    intHyojiKensu = 100
                Else
                    intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
                End If


                '=====================================================================================================================
                '== ８．本人宛名データの取得
                '==　　　　
                '==　　　　<説明>　本人の宛名情報を取得する。
                '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                '==　　　　　　　　ⅲ. 個別事項ＦＧの指定がある場合は個別事項データも取得する
                '==　　　　　　　　ⅳ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定されている場合
                If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                    ' 宛名個別情報の場合
                    If (blnKobetsu) Then
                        '*履歴番号 000038 2008/01/17 修正開始
                        '「宛名個別履歴データ抽出」メゾットを実行する
                        'csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
                        '                                              cSearchKey, _
                        '                                              cAtenaGetPara1.p_strShiteiYMD, _
                        '                                              cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu,
                                                                     cSearchKey,
                                                                     cAtenaGetPara1.p_strShiteiYMD,
                                                                     cAtenaGetPara1.p_blnSakujoFG,
                                                                     cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*履歴番号 000038 2008/01/17 修正終了

                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「履歴編集」メソッドを実行する
                            csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '「宛名編集」の「履歴編集」メソッドを実行する
                            csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    Else
                        '「宛名履歴マスタ抽出」メゾットを実行する
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)

                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「履歴編集」メソッドを実行する
                            csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '「宛名編集」の「履歴編集」メソッドを実行する
                            csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    End If
                Else
                    '指定年月日が指定されていない場合

                    ' 宛名個別情報の場合
                    If (blnKobetsu) Then
                        '*履歴番号 000038 2008/01/17 修正開始
                        '「宛名個別情報抽出」メソッドを実行する
                        'csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*履歴番号 000038 2008/01/17 修正終了

                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「宛名個別編集」メソッドを実行する
                            csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '「宛名編集」の「宛名個別編集」メソッドを実行する
                            csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    Else
                        '「宛名マスタ抽出」メゾットを実行する
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「宛名編集」メソッドを実行する
                            csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '「宛名編集」の「宛名編集」メソッドを実行する
                            csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
                        End If

                    End If

                End If

                csWkAtena = csDataSet

                '*履歴番号 000040 2008/11/10 追加開始
                '=====================================================================================================================
                '== ９．利用届データの取得
                '==　　　　
                '==　　　　<説明>　利用届データの取得
                '==　　　　　　　　ⅰ. 標準レイアウトの場合かつ、宛名個別情報以外の場合に処理を行う
                '==　　　　　　　　ⅱ. 利用届出取得区分が"1,2"の場合に処理を行う
                '==　　　　　　　　ⅲ. 住民コード、税目区分などから利用届データを取得し、納税者ID、利用者IDにセットする
                '==　　　　
                '=====================================================================================================================
                Me.RiyoTdkHenshu(cAtenaGetPara1, blnKobetsu, csAtenaH)

                '*履歴番号 000041 2008/11/17 追加開始
                ' 利用届区分が"2"の場合、該当データ以外が削除されるので新規件数をセットする
                If (cAtenaGetPara1.p_strTdkdKB = "2") Then
                    intGetCount = csAtenaH.Tables(0).Rows.Count
                Else
                End If
                '*履歴番号 000041 2008/11/17 追加終了
                '*履歴番号 000040 2008/11/10 追加終了

                '=====================================================================================================================
                '== １０．連絡先データの取得
                '==　　　　
                '==　　　　<説明>　連絡先情報を取得する。
                '==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
                '==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
                '==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
                '==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If
                Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH, csWkAtena, intHyojunKB, strKikanYMD)


                '=====================================================================================================================
                '== １１．代納・送付先データ取得の判定
                '==　　　　
                '==　　　　<説明>　＠業務コードの指定がない場合は、処理を強制的に終了する。
                '==　　　　　　　　本人データの取得件数が１件でない場合も処理を強制的に終了する。
                '==　　　　
                '=====================================================================================================================
                '取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
                If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

                    csAtena1 = csAtenaH

                    '処理を終了する
                    Exit Try
                End If


                '=====================================================================================================================
                '== １２．送付先データの抽出日を設定
                '==　　　　
                '==　　　　<説明>　送付先データの抽出において、＠指定日の指定があり、かつ＠送付先データ区分が "1" の場合は
                '==　　　　　　　　指定された日付が有効期間に含まれていることを条件とする。
                '==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== １３．送付先データの取得
                '==　　　　
                '==　　　　<説明>　送付先データの件数により、存在している場合のみ送付先データの取得を行う。
                '==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                '==　　　　
                '=====================================================================================================================
                '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
                If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
                    '送付先があるので読み込む
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cAtenaGetPara1.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    Else
                        csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    End If
                Else
                    '送付先が無いので、空のテーブル作成
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun()
                    Else
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
                    End If
                End If


                '=====================================================================================================================
                '== １４．送付先データのレイアウト編集
                '==　　　　
                '==　　　　<説明>　個別事項ＦＧの指定がある場合は、送付先データを個別事項項目が付加されたレイアウトに編集する。
                '==　　　　　　　　また、バッチ版・リアル版により使用するクラスを分ける。
                '==　　　　
                '=====================================================================================================================
                ' 宛名個別情報の場合
                If (blnKobetsu) Then
                    If (m_blnBatch) Then
                        '「宛名編集バッチ」の「送付先個別編集」メソッドを実行する
                        csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    Else
                        '「宛名編集」の「送付先個別編集」メソッドを実行する
                        csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    End If
                Else
                    If (m_blnBatch) Then
                        '「宛名編集バッチ」の「送付先編集」メソッドを実行する
                        csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    Else
                        '「宛名編集」の「送付先編集」メソッドを実行する
                        csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    End If
                End If


                '=====================================================================================================================
                '== １５．代納データの抽出日を設定
                '==　　　　
                '==　　　　<説明>　代納データの抽出において、＠指定日の指定がある場合は、指定された日付が有効期間に含まれている
                '==　　　　　　　　ことを条件とする。
                '==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定してある場合
                If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== １６．代納データの取得
                '==　　　　
                '==　　　　<説明>　代納データの件数により、存在している場合のみ代納データの取得を行う。
                '==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                '==　　　　
                '=====================================================================================================================
                '「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
                If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0) Then
                    '代納があるので読み込む
                    csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD,
                                                        cAtenaGetPara1.p_strGyomuCD,
                                                         cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                         strKikanYMD,
                                                         cAtenaGetPara1.p_blnSakujoFG)
                Else
                    '代納が無いので、空のテーブル作成
                    csDataSet = m_cABDainoB.GetDainoSchemaBHoshu()
                End If


                '=====================================================================================================================
                '== １７．取得データのマージ
                '==　　　　
                '==　　　　<説明>　代納データの取得件数が１件でない場合は、「本人」「送付先」「代納人」「代納送付先」データを
                '==　　　　　　　　１つのデータセットにマージし、処理を強制的に終了する。
                '==　　　　　　　　この時点では、「代納人」「代納送付先」データは空である。
                '==　　　　
                '=====================================================================================================================
                '取得件数が1件でない場合
                If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

                    'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                    '処理を終了する
                    Exit Try
                End If


                '=====================================================================================================================
                '== １８．代納人宛名取得検索キーの設定
                '==　　　　
                '==　　　　<説明>　代納人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
                '==　　　　　　　　この時、代納区分・業務コード・業務内種別コードを退避する。
                '==　　　　
                '=====================================================================================================================
                With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

                    '代納区分を退避する
                    strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

                    '業務コードを退避する
                    strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

                    '業務内種別コードを退避する
                    strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

                    '宛名検索キーにセットする
                    cSearchKey = Nothing
                    cSearchKey = New ABAtenaSearchKey

                    cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

                End With

                '住基・住登外区分が<>"1"の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJutogaiYusenKB = "1"
                End If

                '住基・住登外区分が="1"の場合
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJuminYuseniKB = "1"
                End If


                '=====================================================================================================================
                '== １９．代納人宛名データの取得
                '==　　　　
                '==　　　　<説明>　代納人の宛名情報を取得する。
                '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                '==　　　　　　　　ⅲ. 個別事項ＦＧの指定がある場合は個別事項データも取得する
                '==　　　　　　　　ⅳ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定されている場合
                If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then

                    ' 宛名個別情報の場合
                    If (blnKobetsu) Then

                        '*履歴番号 000038 2008/01/17 修正開始
                        '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
                        'csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                        '                                              cSearchKey, _
                        '                                              cAtenaGetPara1.p_strShiteiYMD, _
                        '                                              cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu,
                                                                        cSearchKey,
                                                                        cAtenaGetPara1.p_strShiteiYMD,
                                                                        cAtenaGetPara1.p_blnSakujoFG,
                                                                        cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*履歴番号 000038 2008/01/17 修正終了

                        '取得件数
                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
                        '取得件数が０件の場合、
                        If (intGetCount = 0) Then

                            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '処理を終了する
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「履歴個別編集」メソッドを実行する
                            csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '「宛名編集」の「履歴個別編集」メソッドを実行する
                            csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    Else
                        '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)
                        '取得件数
                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
                        '取得件数が０件の場合、
                        If (intGetCount = 0) Then

                            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '処理を終了する
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「履歴編集」メソッドを実行する
                            csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '「宛名編集」の「履歴編集」メソッドを実行する
                            csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    End If
                Else

                    '⑰指定年月日が指定されていない場合
                    ' 宛名個別情報の場合
                    If (blnKobetsu) Then

                        '*履歴番号 000038 2008/01/17 修正開始
                        '「宛名個別データ抽出」メゾットを実行する
                        'csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                        '                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu,
                                                                 cSearchKey, cAtenaGetPara1.p_blnSakujoFG,
                                                                 cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*履歴番号 000038 2008/01/17 修正終了

                        '取得件数
                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                        '取得件数が０件の場合、
                        If (intGetCount = 0) Then

                            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '処理を終了する
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「宛名編集」メソッドを実行する
                            csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '「宛名編集」の「宛名編集」メソッドを実行する
                            csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        End If

                    Else

                        '「宛名マスタ抽出」メゾットを実行する
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                            cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        '取得件数
                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                        '取得件数が０件の場合、
                        If (intGetCount = 0) Then

                            'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '処理を終了する
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '「宛名編集バッチ」の「宛名編集」メソッドを実行する
                            csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '「宛名編集」の「宛名編集」メソッドを実行する
                            csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    End If
                End If


                '=====================================================================================================================
                '== ２０．代納人送付先データの抽出日を設定
                '==　　　　
                '==　　　　<説明>　代納人の送付先データの抽出において、＠指定日の指定があり、かつ＠送付先データ区分が "1" の場合は
                '==　　　　　　　　指定された日付が有効期間に含まれていることを条件とする。
                '==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== ２１．代納人送付先データの取得
                '==　　　　
                '==　　　　<説明>　代納人の送付先データの件数により、存在している場合のみ送付先データの取得を行う。
                '==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                '==　　　　
                '=====================================================================================================================
                If (csDataSet.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
                    '送付先があるので読み込む
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cSearchKey.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    Else
                        csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    End If
                Else
                    '送付先が無いので、空のテーブル作成
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun()
                    Else
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
                    End If
                End If


                '=====================================================================================================================
                '== ２２．代納送付先データのレイアウト編集
                '==　　　　
                '==　　　　<説明>　個別事項ＦＧの指定がある場合は、送付先データを個別事項項目が付加されたレイアウトに編集する。
                '==　　　　　　　　また、バッチ版・リアル版により使用するクラスを分ける。
                '==　　　　
                '=====================================================================================================================
                ' 宛名個別情報の場合
                If (blnKobetsu) Then
                    If (m_blnBatch) Then
                        '「宛名編集バッチ」の「送付先編集」メソッドを実行する
                        csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    Else
                        '「宛名編集」の「送付先編集」メソッドを実行する
                        csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    End If
                Else
                    If (m_blnBatch) Then
                        '「宛名編集バッチ」の「送付先編集」メソッドを実行する
                        csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    Else
                        '「宛名編集」の「送付先編集」メソッドを実行する
                        csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    End If
                End If


                '=====================================================================================================================
                '== ２３．取得データのマージ
                '==　　　　
                '==　　　　<説明>　「本人」「送付先」「代納人」「代納送付先」データを１つのデータセットにマージし処理を強制的に終了する。
                '==　　　　
                '=====================================================================================================================
                'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)



            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally

                '=====================================================================================================================
                '== ２４．ＲＤＢ切断
                '==　　　　
                '==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ切断を行わない制御を行う。
                '==　　　　
                '=====================================================================================================================
                ' RDB切断
                If m_blnBatchRdb = False Then
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + Me.GetType.Name + "】" +
                                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                            "【実行メソッド名:Disconnect】")
                    m_cfRdbClass.Disconnect()
                End If


                '*履歴番号 000031 2007/07/30 修正開始
                '=====================================================================================================================
                '== ２５．返却する住民コードを指定された住民コードで上書きする
                '==　　　　
                '==　　　　<説明>　同一人代表者取得された場合は、指定された住民コードを返す
                '==　　　　
                '=====================================================================================================================
                '退避した住民コードが存在する場合は、上書きする
                SetJuminCD(csAtena1)
                '*履歴番号 000031 2007/07/30 修正終了

                '*履歴番号 000041 2008/11/17 削除開始
                ''*履歴番号 000040 2008/11/10 追加開始
                ''=====================================================================================================================
                ''== ２６．利用届出データの絞込み
                ''==　　　　
                ''==　　　　<説明>　利用届出取得区分 = "2" の場合、返却データの納税者IDが存在しないレコードは返却しない
                ''==　　　　
                ''=====================================================================================================================
                ''退避した住民コードが存在する場合は、上書きする
                'RiyoTdkHenshu_Select(cAtenaGetPara1, blnKobetsu, csAtena1)
                ''*履歴番号 000040 2008/11/10 追加終了
                '*履歴番号 000041 2008/11/17 削除シュウリョう

            End Try

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try

        Return csAtena1

    End Function
#End Region
    '*履歴番号 000030 2007/04/21 追加終了

    '*履歴番号 000030 2007/04/21 追加開始
#Region " 介護用宛名取得 "
    '************************************************************************************************
    '* メソッド名     介護用宛名取得
    '* 
    '* 構文           Public Function GetKaigoAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet : 取得した宛名情報
    '************************************************************************************************
    Public Function GetKaigoAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim blnAtenaKani As Boolean
        'Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnRirekiKani As Boolean
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csAtenaEntity As DataSet                        '介護用宛名Entity

        Try
            'コンストラクタの設定を保存
            blnAtenaSelectAll = m_blnSelectAll
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If

            '宛名取得メインメソッドの呼出し（引数：取得パラメータクラス、個別事項データ取得フラグ、呼び出しメソッド区分）
            csAtenaEntity = AtenaGetMain(cAtenaGetPara1, False, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Tsujo)

            'コンストラクタの設定を元にもどす
            m_blnSelectAll = blnAtenaSelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll
            End If

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try

        Return csAtenaEntity

    End Function
#End Region
    '*履歴番号 000030 2007/04/21 追加終了

#Region " 簡易宛名取得２(AtenaGet2) "
    '************************************************************************************************
    '* メソッド名     簡易宛名取得２
    '* 
    '* 構文           Public Function AtenaGet2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Function AtenaGet2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "AtenaGet2"
        Dim csAtenaEntity As DataSet                        '宛名Entity
        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean
        '* 履歴番号 000024 2005/01/25 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            ' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                                "【実行メソッド名:Connect】")
            '* 履歴番号 000023 2004/08/27 削除終了
            'ＲＤＢ接続
            If m_blnBatchRdb = False Then
                '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + THIS_CLASS_NAME + "】" +
                                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                                "【実行メソッド名:Connect】")
                '* 履歴番号 000023 2004/08/27 追加終了
                m_cfRdbClass.Connect()
            End If

            Try
                '* 履歴番号 000014 2003/06/17 削除開始
                '' 管理情報取得(内部処理)メソッドを実行する。
                'Me.GetKanriJoho()
                '* 履歴番号 000014 2003/06/17 削除終了

                '* 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
                'コンストラクタの設定を保存
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = False
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = False

                End If
                '* 履歴番号 000024 2005/01/25 追加終了

                ' 簡易宛名取得２(内部処理)メソッドを実行する。
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo)

                '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
                'コンストラクタの設定を元にもどす
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If
                '* 履歴番号 000024 2005/01/25 追加終了

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
                ' RDBアクセスログ出力
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                '                        "【実行メソッド名:Disconnect】")
                '* 履歴番号 000023 2004/08/27 削除終了
                ' RDB切断
                If m_blnBatchRdb = False Then
                    '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    '* 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtenaEntity

    End Function
#End Region

#Region " 管理情報取得(KanriJohoGet) "
    '************************************************************************************************
    '* メソッド名     管理情報取得
    '* 
    '* 構文           Public Function KanriJohoGet()
    '* 
    '* 機能　　    　　管理情報を取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub KanriJohoGet()
        Const THIS_METHOD_NAME As String = "KanriJohoGet"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000014 2003/06/17 追加開始
            If (m_blnKanriJoho) Then
                Exit Sub
            End If
            '* 履歴番号 000014 2003/06/17 追加終了

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            ' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:Connect】")
            '* 履歴番号 000023 2004/08/27 削除終了
            ' ＲＤＢ接続
            If m_blnBatchRdb = False Then
                '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Connect】")
                '* 履歴番号 000023 2004/08/27 追加終了
                m_cfRdbClass.Connect()
            End If

            Try

                ' 管理情報取得(内部処理)メソッドを実行する。
                Me.GetKanriJoho()

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
                ' RDBアクセスログ出力
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                '                        "【実行メソッド名:Disconnect】")
                '* 履歴番号 000023 2004/08/27 削除終了
                ' RDB切断
                If m_blnBatchRdb = False Then
                    '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    '* 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

#Region " 年金宛名取得(NenkinAtenaGet) "
    '*履歴番号 000029 2006/07/31 追加開始
    '************************************************************************************************
    '* メソッド名     年金宛名取得
    '* 
    '* 構文           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       年金宛名情報を取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
        '* corresponds to VS2008 End 2010/04/16 000044
        '年金宛名ゲットより年金宛名情報を取得する
        Return NenkinAtenaGet(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01)
    End Function
    '*履歴番号 000029 2006/07/31 追加終了
#End Region

#Region " 年金宛名取得(NenkinAtenaGet) "
    '************************************************************************************************
    '* メソッド名     年金宛名取得
    '* 
    '* 構文           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       年金宛名情報を取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    '*履歴番号 000029 2006/07/31 修正開始
    Public Overloads Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer) As DataSet
        'Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
        ''Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        ''    Const THIS_METHOD_NAME As String = "KanriJohoGet"
        ''*履歴番号 000029 2006/07/31 修正終了
        ''*履歴番号 000015 2003/08/21 削除開始
        ''Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
        ''*履歴番号 000015 2003/08/21 削除終了
        'Dim csAtenaEntity As DataSet                        '宛名Entity
        'Dim csAtena1Entity As DataSet                       '宛名1Entity
        ''*履歴番号 000022 2003/12/02 追加開始
        'Dim cAtenaGetPara1Save As New ABAtenaGetPara1XClass     ' 退避用
        ''*履歴番号 000022 2003/12/02 追加終了

        ''* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        'Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnAtenaKani As Boolean
        'Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnRirekiKani As Boolean
        ''* 履歴番号 000024 2005/01/25 追加終了

        'Try
        '    ' デバッグログ出力
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        '    '=====================================================================================================================
        '    '== １．ＲＤＢ接続
        '    '==　　　　
        '    '==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
        '    '==　　　　
        '    '=====================================================================================================================
        '    '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '    ' RDBアクセスログ出力
        '    'm_cfLogClass.RdbWrite(m_cfControlData, _
        '    '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '    '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '    '                                "【実行メソッド名:Connect】")
        '    '* 履歴番号 000023 2004/08/27 削除終了
        '    'ＲＤＢ接続
        '    If m_blnBatchRdb = False Then
        '        '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        '        ' RDBアクセスログ出力
        '        m_cfLogClass.RdbWrite(m_cfControlData,
        '                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
        '                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
        '                                        "【実行メソッド名:Connect】")
        '        '* 履歴番号 000023 2004/08/27 追加終了
        '        m_cfRdbClass.Connect()
        '    End If

        '    Try
        '        '=====================================================================================================================
        '        '== ２．各種クラスのインスタンス化
        '        '==　　　　
        '        '==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '*履歴番号 000015 2003/08/21 修正開始
        '        ''宛名編集クラスのインスタンス作成
        '        'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        If (m_blnBatch) Then
        '            If (m_cABBatchAtenaHenshuB Is Nothing) Then
        '                '宛名編集バッチクラスのインスタンス作成
        '                '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '                'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* 履歴番号 000024 2005/01/25 更新終了
        '            End If
        '        Else
        '            If (m_cABAtenaHenshuB Is Nothing) Then
        '                '宛名編集クラスのインスタンス作成
        '                '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '                'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* 履歴番号 000024 2005/01/25 更新終了
        '            End If
        '        End If
        '        '*履歴番号 000015 2003/08/21 修正終了

        '        '*履歴番号 000045 2010/05/17 追加開始
        '        ' 宛名Ｂクラス各種プロパティをセット
        '        m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
        '        m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
        '        '*履歴番号 000046 2011/05/18 追加開始
        '        m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
        '        '*履歴番号 000046 2011/05/18 追加終了

        '        ' 宛名履歴Ｂクラス各種プロパティをセット
        '        m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
        '        m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
        '        '*履歴番号 000046 2011/05/18 追加開始
        '        m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
        '        '*履歴番号 000046 2011/05/18 追加終了
        '        '*履歴番号 000045 2010/05/17 追加終了


        '        '=====================================================================================================================
        '        '== ３．コンストラクタの設定を保存
        '        '==　　　　
        '        '==　　　　<説明>　簡易版・通常版の情報を保存する。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
        '        'コンストラクタの設定を保存
        '        If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
        '            Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
        '        End If
        '        If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
        '            Me.m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
        '        End If
        '        '* 履歴番号 000024 2005/01/25 追加終了（宮沢）



        '        '=====================================================================================================================
        '        '== ４．管理情報の取得
        '        '==　　　　
        '        '==　　　　<説明>　各種管理情報の取得を行う。
        '        '==　　　　
        '        '=====================================================================================================================
        '        ' 管理情報取得(内部処理)メソッドを実行する。
        '        Me.GetKanriJoho()



        '        '=====================================================================================================================
        '        '== ５．業務コードの退避
        '        '==　　　　
        '        '==　　　　<説明>　業務コード・業務内種別コードを退避する。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '*履歴番号 000022 2003/12/02 追加開始
        '        ' 業務コード・業務内種別コードを退避する
        '        cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD
        '        cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD
        '        cAtenaGetPara1.p_strGyomuCD = String.Empty
        '        cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty
        '        '*履歴番号 000022 2003/12/02 追加終了



        '        '=====================================================================================================================
        '        '== ６．コンストラクタの設定を保存
        '        '==　　　　
        '        '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を保存する。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
        '        'コンストラクタの設定を保存
        '        If Not (Me.m_cABAtenaB Is Nothing) Then
        '            blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
        '            blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
        '            Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
        '            Me.m_cABAtenaB.m_blnSelectCount = True
        '        End If
        '        If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
        '            blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
        '            blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
        '            Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
        '            Me.m_cABAtenaRirekiB.m_blnSelectCount = True

        '        End If
        '        '* 履歴番号 000024 2005/01/25 追加終了



        '        '=====================================================================================================================
        '        '== ６．宛名情報の取得
        '        '==　　　　
        '        '==　　　　<説明>　宛名情報の取得を行う。
        '        '==　　　　
        '        '=====================================================================================================================
        '        ' 簡易宛名取得(内部処理)２メソッドを実行する。
        '        csAtenaEntity = Me.GetAtena2(cAtenaGetPara1)



        '        '=====================================================================================================================
        '        '== ７．コンストラクタの設定を戻す
        '        '==　　　　
        '        '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を戻す。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        '        'コンストラクタの設定を元にもどす
        '        If Not (Me.m_cABAtenaB Is Nothing) Then
        '            Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
        '            Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
        '        End If
        '        If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
        '            Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
        '            Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
        '        End If
        '        '* 履歴番号 000024 2005/01/25 追加終了



        '        '=====================================================================================================================
        '        '== ８．宛名情報の編集
        '        '==　　　　
        '        '==　　　　<説明>　宛名情報の編集を行う。
        '        '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
        '        '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
        '        '==　　　　　　　　ⅲ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
        '        '==　　　　
        '        '=====================================================================================================================
        '        '*履歴番号 000015 2003/08/21 修正開始
        '        '' 宛名編集クラスの年金宛名編集メソッドを実行する。
        '        'csAtena1Entity = cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        '*履歴番号 000016 2003/10/09 修正開始
        '        'If (m_blnBatch) Then
        '        '    ' 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
        '        '    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        'Else
        '        '    ' 宛名編集クラスの年金宛名編集メソッドを実行する。
        '        '    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        'End If
        '        ' 指定年月日が指定されている場合
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
        '            If (m_blnBatch) Then
        '                '*履歴番号 000029 2006/07/31 修正開始
        '                '「宛名編集バッチ」の「履歴編集」メソッドを実行する
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*履歴番号 000029 2006/07/31 修正終了

        '            Else
        '                '*履歴番号 000029 2006/07/31 修正開始
        '                '「宛名編集」の「履歴編集」メソッドを実行する
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*履歴番号 000029 2006/07/31 修正終了
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '*履歴番号 000029 2006/07/31 修正開始
        '                ' 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*履歴番号 000029 2006/07/31 修正終了
        '            Else
        '                '*履歴番号 000029 2006/07/31 修正開始
        '                ' 宛名編集クラスの年金宛名編集メソッドを実行する。
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*履歴番号 000029 2006/07/31 修正終了
        '            End If
        '        End If
        '        '*履歴番号 000016 2003/10/09 修正終了
        '        '*履歴番号 000015 2003/08/21 修正終了



        '        '=====================================================================================================================
        '        '== ９．業務コードの退避
        '        '==　　　　
        '        '==　　　　<説明>　業務コード・業務内種別コードを退避する。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '*履歴番号 000022 2003/12/02 追加開始
        '        ' 業務コード・業務内種別コードを復元する
        '        cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD
        '        cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD




        '        '=====================================================================================================================
        '        '== １０．連絡先データの取得
        '        '==　　　　
        '        '==　　　　<説明>　連絡先情報を取得する。
        '        '==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
        '        '==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
        '        '==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
        '        '==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
        '        '==　　　　
        '        '=====================================================================================================================
        '        ' 連絡先編集処理
        '        '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
        '        'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity)
        '        Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity, csAtenaEntity)
        '        '* 履歴番号 000024 2005/01/25 更新終了
        '        '*履歴番号 000022 2003/12/02 追加終了



        '        '=====================================================================================================================
        '        '== １１．コンストラクタの設定を戻す
        '        '==　　　　
        '        '==　　　　<説明>　簡易版・通常版の情報を戻す。
        '        '==　　　　
        '        '=====================================================================================================================
        '        '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
        '        'コンストラクタの設定を元にもどす
        '        If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
        '            Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
        '        End If
        '        If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
        '            Me.m_cABAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
        '        End If
        '        '* 履歴番号 000024 2005/01/25 追加終了

        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
        '        ' ワーニングログ出力
        '        m_cfLogClass.WarningWrite(m_cfControlData,
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" +
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
        '                                "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
        '                                "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
        '        ' UFAppExceptionをスローする
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' エラーをそのままスロー
        '        Throw

        '    Finally
        '        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        '        ' RDBアクセスログ出力
        '        'm_cfLogClass.RdbWrite(m_cfControlData, _
        '        '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
        '        '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        '        '                        "【実行メソッド名:Disconnect】")
        '        '* 履歴番号 000023 2004/08/27 削除終了
        '        ' RDB切断
        '        If m_blnBatchRdb = False Then
        '            '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
        '            ' RDBアクセスログ出力
        '            m_cfLogClass.RdbWrite(m_cfControlData,
        '                                    "【クラス名:" + THIS_CLASS_NAME + "】" +
        '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" +
        '                                    "【実行メソッド名:Disconnect】")
        '            '* 履歴番号 000023 2004/08/27 追加終了
        '            m_cfRdbClass.Disconnect()
        '        End If

        '    End Try

        '    ' デバッグログ出力
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        'Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
        '    ' ワーニングログ出力
        '    m_cfLogClass.WarningWrite(m_cfControlData,
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" +
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
        '                                "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
        '                                "【ワーニング内容:" + objAppExp.Message + "】")
        '    ' エラーをそのままスローする
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' エラーログ出力
        '    m_cfLogClass.ErrorWrite(m_cfControlData,
        '                                "【クラス名:" + THIS_CLASS_NAME + "】" +
        '                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
        '                                "【エラー内容:" + objExp.Message + "】")
        '    ' システムエラーをスローする
        '    Throw objExp

        'End Try

        'Return csAtena1Entity

        Return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Tsujo)

    End Function
#End Region

#Region " 年金宛名取得(GetNenkinAtena) "
    '************************************************************************************************
    '* メソッド名     年金宛名取得（内部処理）
    '* 
    '* 構文           Private Function GetNenkinAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       年金宛名情報を取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Private Function GetNenkinAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer,
                                    ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetNenkinAtena"
        Dim csAtenaEntity As DataSet                        '宛名Entity
        Dim csAtena1Entity As DataSet                       '宛名1Entity
        Dim cAtenaGetPara1Save As New ABAtenaGetPara1XClass     ' 退避用
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean
        Dim strKikanYMD As String                           '期間年月日

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '=====================================================================================================================
            '== １．ＲＤＢ接続
            '==　　　　
            '==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
            '==　　　　
            '=====================================================================================================================
            'ＲＤＢ接続
            If m_blnBatchRdb = False Then
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + THIS_CLASS_NAME + "】" +
                                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                                "【実行メソッド名:Connect】")
                m_cfRdbClass.Connect()
            End If

            Try
                '=====================================================================================================================
                '== ２．各種クラスのインスタンス化
                '==　　　　
                '==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
                '==　　　　
                '=====================================================================================================================
                If (m_blnBatch) Then
                    If (m_cABBatchAtenaHenshuB Is Nothing) Then
                        '宛名編集バッチクラスのインスタンス作成
                        m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    End If
                    m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
                Else
                    If (m_cABAtenaHenshuB Is Nothing) Then
                        '宛名編集クラスのインスタンス作成
                        m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    End If
                    m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
                End If

                m_cABAtenaB.m_intHyojunKB = intHyojunKB
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

                ' 宛名Ｂクラス各種プロパティをセット
                m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB

                ' 宛名履歴Ｂクラス各種プロパティをセット
                m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB


                '=====================================================================================================================
                '== ３．コンストラクタの設定を保存
                '==　　　　
                '==　　　　<説明>　簡易版・通常版の情報を保存する。
                '==　　　　
                '=====================================================================================================================
                'コンストラクタの設定を保存
                If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
                    Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                End If
                If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
                    Me.m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                End If



                '=====================================================================================================================
                '== ４．管理情報の取得
                '==　　　　
                '==　　　　<説明>　各種管理情報の取得を行う。
                '==　　　　
                '=====================================================================================================================
                ' 管理情報取得(内部処理)メソッドを実行する。
                Me.GetKanriJoho()



                '=====================================================================================================================
                '== ５．業務コードの退避
                '==　　　　
                '==　　　　<説明>　業務コード・業務内種別コードを退避する。
                '==　　　　
                '=====================================================================================================================
                ' 業務コード・業務内種別コードを退避する
                cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD
                cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD
                cAtenaGetPara1.p_strGyomuCD = String.Empty
                cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty



                '=====================================================================================================================
                '== ６．コンストラクタの設定を保存
                '==　　　　
                '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を保存する。
                '==　　　　
                '=====================================================================================================================
                'コンストラクタの設定を保存
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
                    Me.m_cABAtenaB.m_blnSelectCount = True
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = True

                End If



                '=====================================================================================================================
                '== ６．宛名情報の取得
                '==　　　　
                '==　　　　<説明>　宛名情報の取得を行う。
                '==　　　　
                '=====================================================================================================================
                ' 簡易宛名取得(内部処理)２メソッドを実行する。
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, intHyojunKB)



                '=====================================================================================================================
                '== ７．コンストラクタの設定を戻す
                '==　　　　
                '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を戻す。
                '==　　　　
                '=====================================================================================================================
                'コンストラクタの設定を元にもどす
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If



                '=====================================================================================================================
                '== ８．宛名情報の編集
                '==　　　　
                '==　　　　<説明>　宛名情報の編集を行う。
                '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                '==　　　　　　　　ⅲ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                '==　　　　
                '=====================================================================================================================
                ' 指定年月日が指定されている場合
                If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
                    If (m_blnBatch) Then
                        '「宛名編集バッチ」の「履歴編集」メソッドを実行する
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If

                    Else
                        '「宛名編集」の「履歴編集」メソッドを実行する
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    End If
                Else
                    If (m_blnBatch) Then
                        ' 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    Else
                        ' 宛名編集クラスの年金宛名編集メソッドを実行する。
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    End If
                End If



                '=====================================================================================================================
                '== ９．業務コードの退避
                '==　　　　
                '==　　　　<説明>　業務コード・業務内種別コードを退避する。
                '==　　　　
                '=====================================================================================================================
                ' 業務コード・業務内種別コードを復元する
                cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD
                cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD




                '=====================================================================================================================
                '== １０．連絡先データの取得
                '==　　　　
                '==　　　　<説明>　連絡先情報を取得する。
                '==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
                '==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
                '==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
                '==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
                '==　　　　
                '=====================================================================================================================
                '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If
                ' 連絡先編集処理
                Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity, csAtenaEntity, intHyojunKB, strKikanYMD)



                '=====================================================================================================================
                '== １１．コンストラクタの設定を戻す
                '==　　　　
                '==　　　　<説明>　簡易版・通常版の情報を戻す。
                '==　　　　
                '=====================================================================================================================
                'コンストラクタの設定を元にもどす
                If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
                    Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
                End If
                If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
                    Me.m_cABAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                ' RDB切断
                If m_blnBatchRdb = False Then
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region

#Region " 国保宛名履歴取得(KokuhoAtenaRirekiGet) "
    '************************************************************************************************
    '* メソッド名     国保宛名履歴取得
    '* 
    '* 構文           Public Function KokuhoAtenaRirekiGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       国保宛名履歴データを取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Function KokuhoAtenaRirekiGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "KokuhoAtenaRirekiGet"
        '*履歴番号 000015 2003/08/21 削除開始
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
        '*履歴番号 000015 2003/08/21 削除終了
        Dim csAtena1Entity As DataSet                       '宛名1Entity

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            ' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                                "【実行メソッド名:Connect】")
            '* 履歴番号 000023 2004/08/27 削除終了
            'ＲＤＢ接続
            If m_blnBatchRdb = False Then
                '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + THIS_CLASS_NAME + "】" +
                                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                                "【実行メソッド名:Connect】")
                '* 履歴番号 000023 2004/08/27 追加終了
                m_cfRdbClass.Connect()
            End If

            Try
                ' 管理情報取得(内部処理)メソッドを実行する。
                Me.GetKanriJoho()

                ' 国保宛名履歴取得(内部処理)メソッドを実行する。
                csAtena1Entity = Me.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo)

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
                ' RDBアクセスログ出力
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                '                        "【実行メソッド名:Disconnect】")
                '* 履歴番号 000023 2004/08/27 削除終了
                ' RDB切断
                If m_blnBatchRdb = False Then
                    '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    '* 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region

#Region " 簡易宛名取得２(GetAtena2) "
    '************************************************************************************************
    '* メソッド名     簡易宛名取得２（内部処理）
    '* 
    '* 構文           Private Function GetAtena2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Private Function GetAtena2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtena2"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csDataSet As DataSet
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
        'Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
        '* 履歴番号 000023 2004/08/27 削除終了
        '*履歴番号 000015 2003/08/21 削除開始
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
        '*履歴番号 000015 2003/08/21 削除終了
        Dim intHyojiKensu As Integer                        '最大取得件数
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim intGetCount As Integer                          '取得件数
        '* corresponds to VS2008 End 2010/04/16 000044
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
        '* 履歴番号 000023 2004/08/27 削除終了
        Dim strShichosonCD As String                        '市町村コード
        '* 履歴番号 000039 2008/02/17 追加開始
        Dim intIdx As Integer
        Dim cABMojiHenshuB As ABMojiretsuHenshuBClass       '文字編集Ｂクラス
        '* 履歴番号 000039 2008/02/17 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '=====================================================================================================================
            '== １．宛名取得パラメータチェック
            '==　　　　
            '==　　　　<説明>　パラメータクラスに指定された内容をチェックする。
            '==　　　　
            '=====================================================================================================================
            ' パラメータチェック
            Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)


            '=====================================================================================================================
            '== ２．業務コード存在チェック
            '==　　　　
            '==　　　　<説明>　業務コードが検索キーにしてされていた場合は、エラーを返す。
            '==　　　　
            '=====================================================================================================================
            ' 業務コードが指定されている場合は、エラー
            If Not (cAtenaGetPara1.p_strGyomuCD = String.Empty) Then
                ' エラー定義を取得
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002002)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "業務コード", objErrorStruct.m_strErrorCode)
            End If

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            ' 宛名履歴マスタＤＡクラスのインスタンス作成
            'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名マスタＤＡクラスのインスタンス作成
            'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '* 履歴番号 000023 2004/08/27 削除終了


            '*履歴番号 000015 2003/08/21 修正開始
            '' 宛名編集クラスのインスタンス作成
            'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '*履歴番号 000015 2003/08/21 修正終了

            ' 直近市町村情報取得を取得する。
            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            'cUSSCityInfoClass.GetCityInfo(m_cfControlData)
            '* 履歴番号 000023 2004/08/27 削除終了


            '=====================================================================================================================
            '== ３．市町村コードの取得
            '==　　　　
            '==　　　　<説明>　直近の市町村コードを取得する。
            '==　　　　
            '=====================================================================================================================
            ' 市町村コードの内容を設定する。
            If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
            Else
                strShichosonCD = cAtenaGetPara1.p_strShichosonCD
            End If



            '*履歴番号 000031 2007/07/31 追加開始
            '=====================================================================================================================
            '== ４．同一人代表者取得処理
            '==　　　　
            '==　　　　<説明>　住民コード・住登外優先・同一人判定FG有効の検索条件の場合のみ、同一人代表者取得を行う。
            '==　　　　　　　　管理情報により、ユーザごとの取得判定有り。
            '==　　　　
            '=====================================================================================================================
            '同一人代表者住民コードを検索パラメータに上書きする
            GetDaihyoJuminCD(cAtenaGetPara1)
            '*履歴番号 000031 2007/07/31 追加終了



            '=====================================================================================================================
            '== ５．本人宛名取得検索キーの設定
            '==　　　　
            '==　　　　<説明>　本人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
            '==　　　　　　　　最大取得件数も取得する。
            '==　　　　
            '=====================================================================================================================
            ' 宛名検索キーのインスタンス化
            cSearchKey = New ABAtenaSearchKey

            ' 宛名取得パラメータから宛名検索キーにセットする
            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
            cSearchKey.p_strShichosonCD = strShichosonCD
            '*履歴番号 000032 2007/09/04 追加開始
            '検索用カナ姓名・検索用カナ姓・検索用カナ名の編集
            cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen)
            '*履歴番号 000032 2007/09/04 追加終了

            ' 住所～番地コード3のセット
            If Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1") Then
                ' 住登外優先の場合
                cSearchKey.p_strJutogaiYusenKB = "1"
                cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD
                cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
            Else
                ' 住基優先の場合
                cSearchKey.p_strJuminYuseniKB = "1"
                '*履歴番号 000018 2003/10/30 修正開始
                'cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
                cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8)
                '*履歴番号 000018 2003/10/30 修正終了
                cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
            End If
            '*履歴番号 000048 2014/04/28 追加開始
            cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13)
            cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB
            cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB
            '*履歴番号 000048 2014/04/28 追加終了
            ' 最大取得件数をセットする
            If cAtenaGetPara1.p_intHyojiKensu = 0 Then
                intHyojiKensu = 100
            Else
                intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
            End If
            '*履歴番号 000047 2011/11/07 追加開始
            m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*履歴番号 000047 2011/11/07 追加終了
            '*履歴番号 000048 2014/04/28 追加開始
            m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            '*履歴番号 000048 2014/04/28 追加終了

            '*履歴番号 000050 2020/01/31 追加開始
            ' 履歴検索フラグ
            cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch
            '*履歴番号 000050 2020/01/31 追加終了
            cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji
            cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji
            cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei
            cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho
            cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki
            cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki

            m_cABAtenaB.m_intHyojunKB = intHyojunKB
            m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

            '=====================================================================================================================
            '== ６．本人宛名データの取得
            '==　　　　
            '==　　　　<説明>　本人の宛名情報を取得する。
            '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
            '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
            '==　　　　
            '=====================================================================================================================
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then
                ' 指定年月日が指定されている場合
                '「宛名履歴マスタ抽出」メゾットを実行する
                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                            cSearchKey,
                                                            cAtenaGetPara1.p_strShiteiYMD,
                                                            cAtenaGetPara1.p_blnSakujoFG)

            Else
                ' 指定年月日が指定されていない場合
                '「宛名マスタ抽出」メゾットを実行する
                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
            End If

            '* 履歴番号 000024 2005/01/25 追加終了
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

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

            '*履歴番号 000031 2007/07/31 追加開始
        Finally
            '=====================================================================================================================
            '== ２４．返却する住民コードを指定された住民コードで上書きする
            '==　　　　
            '==　　　　<説明>　同一人代表者取得された場合は、指定された住民コードを返す
            '==　　　　
            '=====================================================================================================================
            '退避した住民コードが存在する場合は、上書きする
            SetJuminCD(csDataSet)
            '*履歴番号 000031 2007/07/31 追加終了

            '*履歴番号 000039 2008/02/17 追加開始
            '=====================================================================================================================
            '== ８．外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
            '==　　　　
            '==　　　　<説明>　外国人データ:漢字氏名１、２、または漢字世帯主名(転出確定、転出予定、転入前含む)の括弧で括られた文字列の除去を行う
            '==　　　　　　　　
            '=====================================================================================================================
            '*履歴番号 000043 2009/04/08 修正開始
            If Not (csDataSet Is Nothing) Then
                If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                    '漢字氏名に含まれる括弧で括られた文字列の除去を行う

                    cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass)

                    ' 全取得データ分行う
                    '* 宛名マスタ、宛名履歴マスタともに同じレイアウトのため、テーブル指定："0"、項目名は宛名Entityを使用。
                    For intIdx = 0 To csDataSet.Tables(0).Rows.Count - 1
                        ' 漢字名称１
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1)))
                        ' 漢字名称２
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2)))
                        ' 世帯主名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI)))
                        ' 第２世帯主名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI)))
                        ' 漢字法人代表者名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
                        ' 転入前世帯主名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
                        ' 転出予定世帯主名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
                        ' 転出確定世帯主名
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
                    Next
                Else
                    ' 漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                End If
            End If

            'If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
            '    '漢字氏名に含まれる括弧で括られた文字列の除去を行う

            '    cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass)

            '    ' 全取得データ分行う
            '    '* 宛名マスタ、宛名履歴マスタともに同じレイアウトのため、テーブル指定："0"、項目名は宛名Entityを使用。
            '    For intIdx = 0 To csDataSet.Tables(0).Rows.Count - 1
            '        ' 漢字名称１
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1)))
            '        ' 漢字名称２
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2)))
            '        ' 世帯主名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI)))
            '        ' 第２世帯主名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI)))
            '        ' 漢字法人代表者名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
            '        ' 転入前世帯主名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
            '        ' 転出予定世帯主名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
            '        ' 転出確定世帯主名
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
            '    Next
            'Else
            '    ' 漢字氏名に含まれる括弧で括られた文字列の除去を行わない
            'End If
            ''*履歴番号 000039 2008/02/17 追加終了
            '*履歴番号 000043 2009/04/08 修正終了

        End Try

        Return csDataSet

    End Function
#End Region

#Region " 国保宛名履歴取得(GetKokuhoAtenaRireki) "
    '************************************************************************************************
    '* メソッド名     国保宛名履歴取得（内部処理）
    '* 
    '* 構文           Private Function GetKokuhoAtenaRireki(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　取得パラメータより宛名履歴データを返す。
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Private Function GetKokuhoAtenaRireki(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetKokuhoAtenaRireki"
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
        Dim csDataSet As DataSet
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
        'Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
        '* 履歴番号 000023 2004/08/27 削除終了
        '*履歴番号 000015 2003/08/21 削除開始
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
        '*履歴番号 000015 2003/08/21 削除終了
        Dim csAtena1Entity As DataSet                       '宛名1Entity
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim strShiteiYMD As String                          ' 指定日
        '* corresponds to VS2008 End 2010/04/16 000044

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            ' 宛名履歴マスタＤＡクラスのインスタンス作成
            'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '
            ' 宛名マスタＤＡクラスのインスタンス作成
            'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '* 履歴番号 000023 2004/08/27 削除終了

            '*履歴番号 000015 2003/08/21 修正開始
            '' 宛名編集クラスのインスタンス作成
            'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            If (m_blnBatch) Then
                If (m_cABBatchAtenaHenshuB Is Nothing) Then
                    '宛名編集バッチクラスのインスタンス作成
                    '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
                    'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    '* 履歴番号 000024 2005/01/25 更新終了
                End If
                m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
            Else
                If (m_cABAtenaHenshuB Is Nothing) Then
                    '宛名編集クラスのインスタンス作成
                    '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
                    'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    '* 履歴番号 000024 2005/01/25 更新終了
                End If
                m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
            End If
            '*履歴番号 000015 2003/08/21 修正終了

            ' ①パラメータチェック
            Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)

            ' 宛名検索キーのインスタンス化
            cSearchKey = New ABAtenaSearchKey

            ' ③宛名取得パラメータから宛名検索キーにセットする
            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

            '*履歴番号 000016 2003/09/08 修正開始
            ''「宛名マスタ抽出」メゾットを実行する
            'csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            '                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            '' 取得件数が１件でない場合、エラー
            'If Not (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count = 1) Then
            '    'エラー定義を取得(検索キーの誤りです。：住民コード)
            '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            'End If

            '' 世帯コードがNull場合、エラー
            'If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String).Trim = String.Empty) Then
            '    'エラー定義を取得(検索キーの誤りです。：住民コード)
            '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            'End If

            '' 宛名検索キーのインスタンス化
            'cSearchKey = New ABAtenaSearchKey()

            '' ④	ABAtenaSearchKeyに世帯コードをセット
            'cSearchKey.p_strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String)

            'If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB), String) = "1") Then
            '    ' 住基・住登外区分が”1”の時、”1”を住民優先区分にセット
            '    cSearchKey.p_strJuminYuseniKB = "1"
            'Else
            '    ' 住基・住登外区分が<>”1”の時、”1”を住登外優先区分にセット
            '    cSearchKey.p_strJutogaiYusenKB = "1"
            'End If

            ' 住基・住登外区分が<>”1”の時、”1”を住登外優先区分にセット
            If (cAtenaGetPara1.p_strJukiJutogaiKB <> "1") Then
                cSearchKey.p_strJutogaiYusenKB = "1"
            Else
                cSearchKey.p_strJuminYuseniKB = "1"
            End If
            '*履歴番号 000016 2003/09/08 修正終了
            '*履歴番号 000047 2011/11/07 追加開始
            m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*履歴番号 000047 2011/11/07 追加終了
            '*履歴番号 000048 2014/04/28 追加開始
            m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            '*履歴番号 000048 2014/04/28 追加終了
            m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

            ' ⑤	宛名履歴マスタＤＡ」クラスの「宛名履歴マスタ抽出」メソッドを実行する
            csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                        cSearchKey, cAtenaGetPara1.p_strShiteiYMD)

            '*履歴番号 000015 2003/08/21 修正開始
            '' 「宛名編集」クラスの「履歴編集」メソッドを実行する。
            'csAtena1Entity = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

            If (m_blnBatch) Then
                ' 「宛名編集」クラスの「履歴編集」メソッドを実行する。
                csAtena1Entity = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            Else
                ' 「宛名編集」クラスの「履歴編集」メソッドを実行する。
                csAtena1Entity = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            End If
            '*履歴番号 000015 2003/08/21 修正終了

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

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

        Return csAtena1Entity

    End Function
#End Region

#Region " 管理情報取得(GetKanriJoho) "
    '************************************************************************************************
    '* メソッド名     管理情報取得（内部処理）
    '* 
    '* 構文           Private Function GetKanriJoho()
    '* 
    '* 機能　　    　　管理情報を取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    '* 履歴番号 000015 2003/08/21 修正開始
    'Private Sub GetKanriJoho()
    <SecuritySafeCritical>
    Protected Overridable Sub GetKanriJoho()
        '* 履歴番号 000015 2003/08/21 修正終了
        Const THIS_METHOD_NAME As String = "GetKanriJoho"
        '* 履歴番号 000015 2003/08/21 削除開始
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '宛名管理情報Ｂクラス
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoCacheBClass   '宛名管理情報キャッシュＢクラス
        '* 履歴番号 000023 2004/08/27 削除終了
        '* 履歴番号 000015 2003/08/21 削除終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* 履歴番号 000014 2003/06/17 追加開始
            If (m_blnKanriJoho) Then
                Exit Sub
            End If
            '* 履歴番号 000014 2003/06/17 追加終了

            '* 履歴番号 000015 2003/08/21 修正開始
            '管理情報クラスのインスタンス作成
            'cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' 宛名管理情報キャッシュＢクラスのインスタンス作成
            '* 履歴番号 000023 2004/08/27 更新開始（宮沢）
            'cfURAtenaKanriJoho = New URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cfURAtenaKanriJoho Is Nothing) Then
                m_cfURAtenaKanriJoho = New URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If
            '* 履歴番号 000023 2004/08/27 更新終了
            '* 履歴番号 000015 2003/08/21 修正終了

            m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD                '住民コード表示桁数
            m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD                 '世帯コード表示桁数
            m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD                '住所コード表示桁数（管内のみ）
            m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD          '行政区コード表示桁数
            m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1              '地区コード１表示桁数
            m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2              '地区コード２表示桁数
            m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3              '地区コード３表示桁数
            m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho          '地区コード１表示名称
            m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho          '地区コード２表示名称
            m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho          '地区コード３表示名称
            m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho  '連絡先１表示名称
            m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho  '連絡先２表示名称

            '* 履歴番号 000014 2003/06/17 追加開始
            ' 管理情報取得済みフラグ設定
            m_blnKanriJoho = True
            '* 履歴番号 000014 2003/06/17 追加終了

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

#Region " パラメーターチェック(CheckColumnValue) "
    '************************************************************************************************
    '* メソッド名     パラメーターチェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1)
    '* 
    '* 機能　　    　　宛名取得パラメータのチェックを行なう
    '* 
    '* 引数           cAtenaGetPara1 As ABAtenaGetPara1 : 宛名取得パラメータ
    '*                intHyojunKB                       : 標準化区分
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim m_cfDateClass As UFDateClass                    ' 日付クラス
        '* 履歴番号 000023 2004/08/27 削除終了

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '日付クラスのインスタンス化
            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            'm_cfDateClass = New UFDateClass(m_cfConfigDataClass)
            '* 履歴番号 000023 2004/08/27 削除終了
            '必要な設定を行う
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None


            '住基・住登外区分
            If Not (cAtenaGetPara1.p_strJukiJutogaiKB.Trim = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住基・住登外区分", objErrorStruct.m_strErrorCode)
                End If
            End If


            '送付先データ区分
            If Not (cAtenaGetPara1.p_strSfskDataKB = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strSfskDataKB = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "送付先データ区分", objErrorStruct.m_strErrorCode)
                End If
            End If

            '世帯員編集
            If Not (cAtenaGetPara1.p_strStaiinHenshu = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strStaiinHenshu = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "世帯員編集", objErrorStruct.m_strErrorCode)
                End If
            End If


            '住民コード
            If Not (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            '世帯コード
            If Not (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strStaiCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "世帯コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            'カナ姓名
            If Not (cAtenaGetPara1.p_strKanaSeiMei = String.Empty) Then
                '*履歴番号 000019 2003/10/30 修正開始
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd("%"c))) Then
                    '*履歴番号 000019 2003/10/30 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "カナ姓名", objErrorStruct.m_strErrorCode)
                End If
            End If

            'カナ姓
            If Not (cAtenaGetPara1.p_strKanaSei = String.Empty) Then
                '*履歴番号 000019 2003/10/30 修正開始
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSei.TrimEnd("%"c))) Then
                    '*履歴番号 000019 2003/10/30 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "カナ姓", objErrorStruct.m_strErrorCode)
                End If
            End If

            'カナ名
            If Not (cAtenaGetPara1.p_strKanaMei = String.Empty) Then
                '*履歴番号 000019 2003/10/30 修正開始
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaMei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaMei.TrimEnd("%"c))) Then
                    '*履歴番号 000019 2003/10/30 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "カナ名", objErrorStruct.m_strErrorCode)
                End If
            End If

            '漢字名称
            If Not (cAtenaGetPara1.p_strKanjiShimei = String.Empty) Then
                '* 履歴番号 000025 2005/04/04 修正開始
                'If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.TrimEnd("%"c), m_cfConfigDataClass)) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then
                    '* 履歴番号 000025 2005/04/04 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "漢字名称", objErrorStruct.m_strErrorCode)
                End If
            End If

            '生年月日
            If Not (cAtenaGetPara1.p_strUmareYMD = String.Empty Or cAtenaGetPara1.p_strUmareYMD = "00000000") Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strUmareYMD.TrimEnd("%"c))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "生年月日", objErrorStruct.m_strErrorCode)
                End If
            End If

            '性別コード
            If Not (cAtenaGetPara1.p_strSeibetsu = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strSeibetsu)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "性別コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            '住所コード
            If Not (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJushoCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住所コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            '行政区コード
            If Not (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) Then
                '*履歴番号 000028 2005/12/06 修正開始
                ''If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strGyoseikuCD.Trim)) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strGyoseikuCD.Trim)) Then
                    '*履歴番号 000028 2005/12/06 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "行政区コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            '地区コード１
            If Not (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD1.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "地区コード１", objErrorStruct.m_strErrorCode)
                End If
            End If

            '地区コード２
            If Not (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD2.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "地区コード２", objErrorStruct.m_strErrorCode)
                End If
            End If

            '地区コード３
            If Not (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD3)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "地区コード３", objErrorStruct.m_strErrorCode)
                End If
            End If

            '番地コード１
            If Not (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD1.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "番地コード１", objErrorStruct.m_strErrorCode)
                End If
            End If

            '番地コード２
            If Not (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD2.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "番地コード２", objErrorStruct.m_strErrorCode)
                End If
            End If

            '番地コード３
            If Not (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD3.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "番地コード３", objErrorStruct.m_strErrorCode)
                End If
            End If

            'データ区分
            '*履歴番号 000021 2003/12/01 修正開始
            'If Not (cAtenaGetPara1.p_strDataKB = String.Empty) Then
            If Not ((cAtenaGetPara1.p_strDataKB = String.Empty) Or (cAtenaGetPara1.p_strDataKB = "1%")) Then
                '*履歴番号 000021 2003/12/01 修正終了
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strDataKB)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "データ区分", objErrorStruct.m_strErrorCode)
                End If
            End If

            '住民種別１
            If Not (cAtenaGetPara1.p_strJuminSHU1 = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU1)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民種別１", objErrorStruct.m_strErrorCode)
                End If
            End If

            '住民種別２
            If Not (cAtenaGetPara1.p_strJuminSHU2 = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU2)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民種別２", objErrorStruct.m_strErrorCode)
                End If
            End If

            '指定年月日
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty Or cAtenaGetPara1.p_strShiteiYMD = "00000000") Then
                m_cfDateClass.p_strDateValue = cAtenaGetPara1.p_strShiteiYMD
                If (Not m_cfDateClass.CheckDate()) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "指定年月日", objErrorStruct.m_strErrorCode)
                End If
            End If

            '市町村コード
            If Not (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strShichosonCD)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "市町村コード", objErrorStruct.m_strErrorCode)
                End If
            End If

            '表示件数
            If (cAtenaGetPara1.p_intHyojiKensu < 0) Or (cAtenaGetPara1.p_intHyojiKensu > 999) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "表示件数", objErrorStruct.m_strErrorCode)
            End If

            '住民コードと世帯コードがNULLで、世帯員編集が"1"の時、例外エラー
            If (cAtenaGetPara1.p_strJuminCD = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiCD = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiinHenshu = "1") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "世帯員編集", objErrorStruct.m_strErrorCode)
            End If

            '旧氏
            If Not (cAtenaGetPara1.p_strKyuuji.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKyuuji.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "旧氏", objErrorStruct.m_strErrorCode)
                End If
            End If

            'カナ旧氏
            If Not (cAtenaGetPara1.p_strKanaKyuuji.Trim = String.Empty) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaKyuuji.Replace("%"c, String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "カナ旧氏", objErrorStruct.m_strErrorCode)
                End If
            End If

            'カタカナ併記名
            If Not (cAtenaGetPara1.p_strKatakanaHeikimei.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKataKanaWide(cAtenaGetPara1.p_strKatakanaHeikimei.Replace("%"c, String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "カタカナ併記名", objErrorStruct.m_strErrorCode)
                End If
            End If

            '住所
            If Not (cAtenaGetPara1.p_strJusho.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strJusho.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住所", objErrorStruct.m_strErrorCode)
                End If
            End If

            '方書
            If Not (cAtenaGetPara1.p_strKatagaki.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKatagaki.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "方書", objErrorStruct.m_strErrorCode)
                End If
            End If

            '電話番号
            If Not (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strRenrakusaki.Replace("-", String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "電話番号", objErrorStruct.m_strErrorCode)
                End If
            End If

            '住民コード～番地コード３すべてがNULLの時、例外エラー
            '*履歴番号 000027 2005/05/06 修正開始
            '*履歴番号 000048 2014/04/28 修正開始
            ' 共通番号の単独指定を可能とするため、判定項目に追加する。
            'If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) Then

            If (Not cAtenaGetPara1.p_strShiteiYMD.Trim = String.Empty) AndAlso
               (intHyojunKB = ABEnumDefine.HyojunKB.KB_Tsujo) Then
                If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strMyNumber.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then
                    '*履歴番号 000048 2014/04/28 修正終了
                    '*履歴番号 000027 2005/05/06 修正終了

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "検索キーなし", objErrorStruct.m_strErrorCode)
                End If
            Else
                If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strMyNumber.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKyuuji.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaKyuuji.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKatakanaHeikimei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strJusho.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKatagaki.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    'エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '例外を生成
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "検索キーなし", objErrorStruct.m_strErrorCode)
                End If
            End If

            '*履歴番号 000040 2008/11/10 追加開始
            If ((cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2") AndAlso
                cAtenaGetPara1.p_strTdkdZeimokuCD = ABEnumDefine.ZeimokuCDType.Empty) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "利用届出取得区分を使用する場合は、利用届出取得用税目コードを指定してください。",
                                         objErrorStruct.m_strErrorCode)
            End If
            '*履歴番号 000040 2008/11/10 追加終了

            '*履歴番号 000051 2020/11/02 追加開始
            '利用届出利用区分
            If ((cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2") AndAlso
                Not (cAtenaGetPara1.p_strTdkdRiyoKB = String.Empty OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "1" OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "2" OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "3")) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "利用届出利用区分", objErrorStruct.m_strErrorCode)
            End If
            '*履歴番号 000051 2020/11/02 追加終了

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
            Throw objExp
        End Try

    End Sub
#End Region

#Region " 宛名情報のマージ(CreateAtenaDataSet) "
    '************************************************************************************************
    '* メソッド名     宛名情報のマージ
    '* 
    '* 構文           Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, _
    '*                                                  ByVal csAtenaHS As DataSet, _
    '*                                                  ByVal csAtenaD As DataSet, _
    '*                                                  ByVal csAtenaDS As DataSet) As DataSet
    '* 
    '* 機能　　    　　各宛名情報データセットをマージする
    '* 
    '* 引数           csAtenaH As DataSet   : 宛名データ
    '*                csAtenaHS As DataSet  : 送付先データ
    '*                csAtenaD  As DataSet  : 代納データ
    '*                csAtenaDS As DataSet  : 代納送付先データ
    '* 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    '*履歴番号 000020 2003/11/19 修正開始
    'Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, ByVal csAtenaHS As DataSet, _
    '                                    ByVal csAtenaD As DataSet, ByVal csAtenaDS As DataSet) As DataSet
    Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, ByVal csAtenaHS As DataSet,
                                        ByVal csAtenaD As DataSet, ByVal csAtenaDS As DataSet,
                                        ByVal blnKobetsu As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        '*履歴番号 000020 2003/11/19 修正終了
        Const THIS_METHOD_NAME As String = "CreateAtenaDataSet"
        Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csRow As DataRow
        'Dim csNewRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000044
        'Dim cABCommon As ABCommonClass                      '宛名業務共通クラス
        Dim strTableName As String

        Try

            '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            'ログ出力用クラスインスタンス化
            'm_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)
            '* 履歴番号 000023 2004/08/27 削除終了

            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '宛名業務共通クラスのインスタンス化
            'cABCommon = New ABCommonClass()

            '宛名情報のインスタンス化
            csAtena1 = New DataSet

            If (blnKobetsu) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    strTableName = ABAtena1KobetsuHyojunEntity.TABLE_NAME
                Else
                    strTableName = ABAtena1KobetsuEntity.TABLE_NAME
                End If
            Else
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    strTableName = ABAtena1HyojunEntity.TABLE_NAME
                Else
                    strTableName = ABAtena1Entity.TABLE_NAME
                End If
            End If

            '宛名データ存在チェック
            If Not (csAtenaH Is Nothing) Then
                ''*履歴番号 000020 2003/11/19 修正開始
                '''宛名情報に宛名データを追加する
                ''csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*履歴番号 000020 2003/11/19 修正終了
                '宛名情報に宛名データを追加する
                csAtena1.Merge(csAtenaH.Tables(strTableName))
            End If

            '代納データ存在チェック
            If Not (csAtenaD Is Nothing) Then
                ''*履歴番号 000020 2003/11/19 修正開始
                '''代納データを追加する
                ''csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaD.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*履歴番号 000020 2003/11/19 修正終了
                '宛名情報に代納データを追加する
                csAtena1.Merge(csAtenaD.Tables(strTableName))
            End If

            '送付先データ存在チェック
            If Not (csAtenaHS Is Nothing) Then
                ''*履歴番号 000020 2003/11/19 修正開始
                '''送付先データを追加する
                ''csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaHS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*履歴番号 000020 2003/11/19 修正終了
                '宛名情報に送付先データを追加する
                csAtena1.Merge(csAtenaHS.Tables(strTableName))
            End If

            '代納送付先データ存在チェック
            If Not (csAtenaDS Is Nothing) Then
                ''*履歴番号 000020 2003/11/19 修正開始
                '''代納送付先データを追加する
                ''csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaDS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '宛名情報に宛名データを追加する
                '    csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*履歴番号 000020 2003/11/19 修正終了
                '宛名情報に代納送付先データを追加する
                csAtena1.Merge(csAtenaDS.Tables(strTableName))
            End If

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
            Throw objExp
        End Try

        Return csAtena1

    End Function
#End Region

#Region " 連絡先編集処理(RenrakusakiHenshu) "
    '*履歴番号 000022 2003/12/02 追加開始
    '************************************************************************************************
    '* メソッド名     連絡先編集処理
    '* 
    '* 構文           Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, 
    '* 　　                                         ByVal strGyomunaiSHU_CD As String, 
    '* 　　                                         ByRef csAtenaH As DataSet,
    '* 　　                                         ByRef csOrgAtena As DataSet)
    '* 
    '* 機能　　    　　連絡先を取得して、編集する
    '* 
    '* 引数           strGyomuCD As String          : 業務コード
    '* 　　           strGyomunaiSHU_CD As String   : 業務内種別コード
    '*                csAtenaH  As DataSet          : 本人データ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    'Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, ByVal strGyomunaiSHU_CD As String, ByRef csAtenaH As DataSet)
    Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, ByVal strGyomunaiSHU_CD As String, ByRef csAtenaH As DataSet,
                                  ByRef csOrgAtena As DataSet, ByVal intHyojunKB As ABEnumDefine.HyojunKB, ByVal strKikanYMD As String)
        '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
        'Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' 連絡先Ｂクラス
        '* 履歴番号 000023 2004/08/27 削除終了
        Dim csRenrakusakiEntity As DataSet                  ' 連絡先DataSet
        Dim csRenrakusakiRow As DataRow                     ' 連絡先Row
        Dim csRow As DataRow
        Dim csAtena1Table As DataTable                      ' AtenaTable

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' 業務コードが存在しない場合は、処理しない
            If (strGyomuCD.Trim = String.Empty) Then
                Exit Sub
            End If

            ' 連絡先Ｂクラスのインスタンス作成
            'cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cRenrakusakiBClass Is Nothing) Then
                m_cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If

            If (csAtenaH.Tables.Contains(ABAtena1Entity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1Entity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABNenkinAtenaEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABNenkinAtenaEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1KobetsuEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1HyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1HyojunEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABNenkinAtenaHyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABNenkinAtenaHyojunEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1KobetsuHyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuHyojunEntity.TABLE_NAME)
            Else
                ' システムエラー
            End If

            '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            Dim intCount As Integer = 0
            Dim csAtenaRow As DataRow
            '* 履歴番号 000024 2005/01/25 追加終了

            For Each csRow In csAtena1Table.Rows
                '* 履歴番号 000024 2005/01/25 追加開始（宮沢）IF文を追加
                csAtenaRow = csOrgAtena.Tables(0).Rows(intCount)
                If (Not (csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT) Is System.DBNull.Value)) Then
                    If (CType(csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT), Integer) > 0) Then
                        '* 履歴番号 000024 2005/01/25 追加終了（宮沢）IF文を追加
                        ' 連絡先データを取得する
                        csRenrakusakiEntity = m_cRenrakusakiBClass.GetRenrakusakiBHoshu_Hyojun(CType(csRow(ABAtena1Entity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD, strKikanYMD)
                        If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                            csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                            '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
                            csRenrakusakiRow.BeginEdit()
                            '* 履歴番号 000023 2004/08/27 追加終了
                            '連絡先１
                            If (CType(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1), String).Trim <> "03") AndAlso
                               (CType(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1), String).RLength <= 15) Then
                                csRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                            End If
                            '連絡先２
                            If (CType(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2), String).Trim <> "03") AndAlso
                               (CType(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2), String).RLength <= 15) Then
                                csRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                            End If
                            Select Case csAtena1Table.TableName
                                Case ABNenkinAtenaEntity.TABLE_NAME, ABNenkinAtenaHyojunEntity.TABLE_NAME
                                    '連絡先取得業務コード
                                    csRow(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                Case ABAtena1KobetsuEntity.TABLE_NAME, ABAtena1KobetsuHyojunEntity.TABLE_NAME
                                    '連絡先取得業務コード
                                    csRow(ABAtena1KobetsuEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                    '*履歴番号 000030 2007/04/21 修正開始
                                Case ABAtena1Entity.TABLE_NAME, ABAtena1HyojunEntity.TABLE_NAME
                                    '*履歴番号 000042 2008/11/18 修正開始
                                    ' メソッド区分が介護の場合のみセットする
                                    '連絡先取得業務コード (介護用テーブルの場合のみセットする。項目数68個以上は介護用テーブルとみなす。)
                                    'If csRow.ItemArray.Length > 67 Then
                                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                        csRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                    End If
                                    '*履歴番号 000042 2008/11/18 修正終了
                                    '*履歴番号 000030 2007/04/21 修正終了
                            End Select
                            '* 履歴番号 000023 2004/08/27 追加開始（宮沢）

                            If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                '連絡先区分
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIKB)
                                '連絡先名
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIMEI)
                                '連絡先１
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                                '連絡先２
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                                '連絡先３
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI3)
                                '連絡先種別１
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
                                '連絡先種別２
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
                                '連絡先種別３
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
                            Else
                            End If

                            csRenrakusakiRow.EndEdit()
                            '* 履歴番号 000023 2004/08/27 追加終了
                        End If
                    End If
                End If
                intCount = intCount + 1
            Next csRow

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try

    End Sub
    '*履歴番号 000022 2003/12/02 追加終了
#End Region

    '*履歴番号 000031 2007/07/28 追加開始
#Region " 同一人代表者住民コード取得(GetDaihyoJuminCD)"
    '************************************************************************************************
    '* メソッド名     同一人代表者住民コード取得
    '* 
    '* 構文           Private Sub GetDaihyoJuminCD(ByRef cAtenaGetPara1 As ABAtenaGetPara1XClass)
    '* 
    '* 機能　　    　　住民コードセット
    '* 
    '* 引数           cAtenaGetPara1　：　検索パラめー亜
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub GetDaihyoJuminCD(ByRef cAtenaGetPara1 As ABAtenaGetPara1XClass)
        Const THIS_METHOD_NAME As String = "GetDaihyoJuminCD"
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim strDaihyoJuminCD As String                  '代表者住民コード
        '* corresponds to VS2008 End 2010/04/16 000044

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '検索条件により、代表者取得の処理を行う
            If cAtenaGetPara1.p_strJuminCD <> String.Empty AndAlso cAtenaGetPara1.p_strJukiJutogaiKB = "" AndAlso cAtenaGetPara1.p_strDaihyoShaKB = "" Then

                '管理情報取得を行う
                If m_strDoitsu_Param = String.Empty Then
                    'メンバに無い場合のみインスタンス化を行う
                    If (m_cABAtenaKanriJohoB Is Nothing) Then
                        m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    End If
                    '管理情報より取得
                    m_strDoitsu_Param = m_cABAtenaKanriJohoB.GetDoitsuHantei_Param()
                End If

                '管理情報により、同一人代表者取得を行うか判定する
                If m_strDoitsu_Param = ABConstClass.PRM_DAIHYO Then
                    '住民コードを退避させる
                    m_strHonninJuminCD = cAtenaGetPara1.p_strJuminCD.Trim
                    'メンバに無い場合のみインスタンス化を行う
                    If (m_cABGappeiDoitsuninB Is Nothing) Then
                        m_cABGappeiDoitsuninB = New ABGappeiDoitsuninBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    End If

                    '同一人代表者の情報を取得し、検索パラメータへセットする
                    cAtenaGetPara1.p_strJuminCD = m_cABGappeiDoitsuninB.GetDoitsuninDaihyoJuminCD(m_strHonninJuminCD)
                Else
                    '退避用住民コードをクリアする
                    m_strHonninJuminCD = String.Empty
                End If
            Else
                '*履歴番号 000037 2008/01/17 追加開始
                '退避用住民コードをクリアする
                m_strHonninJuminCD = String.Empty
                '*履歴番号 000037 2008/01/17 追加終了
            End If

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

#Region " 住民コードセット(SetJuminCD) "
    '************************************************************************************************
    '* メソッド名     住民コードセット（内部処理）
    '* 
    '* 構文           Private Sub SetJuminCD(ByRef csDataSet As DataSet)
    '* 
    '* 機能　　    　　住民コードセット
    '* 
    '* 引数           csDataSet　：　宛名データセット
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetJuminCD(ByRef csDataSet As DataSet)
        Const THIS_METHOD_NAME As String = "SetJuminCD"
        Dim intCnt As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '退避した住民コードが存在する場合は、上書きする
            If (m_strHonninJuminCD <> String.Empty) Then

                'テーブル名によって場合分けを行う(テーブルは必ず１つしかない)
                Select Case csDataSet.Tables(0).TableName
                    Case ABAtena1Entity.TABLE_NAME, ABAtena1KobetsuEntity.TABLE_NAME, ABAtena1HyojunEntity.TABLE_NAME, ABAtena1KobetsuHyojunEntity.TABLE_NAME
                        '同一人代表者取得を行った場合は、退避した住民コード(本人)で上書きする
                        For intCnt = 0 To csDataSet.Tables(0).Rows.Count - 1
                            '本人・送付先（本人）レコードのみ上書きする
                            If (CStr(csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.DAINOKB)) = ABConstClass.DAINOKB_HONNIN) OrElse
                                (CStr(csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.DAINOKB)) = ABConstClass.DAINOKB_H_SFSK) Then
                                csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.JUMINCD) = m_strHonninJuminCD
                            End If
                        Next

                    Case Else
                        '同一人代表者取得を行った場合は、退避した住民コード(本人)で上書きする
                        For intCnt = 0 To csDataSet.Tables(0).Rows.Count - 1
                            csDataSet.Tables(0).Rows(intCnt).Item(ABAtenaEntity.JUMINCD) = m_strHonninJuminCD
                        Next

                End Select
            Else
                '何もしない
            End If

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
    '*履歴番号 000031 2007/07/28 追加終了

    '*履歴番号 000032 2007/09/04 追加開始
#Region " 検索カナ姓名・検索カナ名・検索カナ名編集(HenshuSearchKana)"
    '************************************************************************************************
    '* メソッド名     検索カナ姓名・検索カナ名・検索カナ名編集
    '* 
    '* 構文           Private Function HenshuSearchKana(ByRef cSearchKey As ABAtenaSearchKey,
    '*                                                  ByRef blnHommyoYusen As Boolean) As ABAtenaSearchKey 
    '* 
    '* 機能　　    　 宛名検索のカナ姓名を標準仕様と外国人本名検索機能用に編集する
    '* 
    '* 引数           ABAtenaSearchKey　：　宛名検索キーパラメータ
    '* 
    '* 戻り値         ABAtenaSearchKey　：　宛名検索キーパラメータ
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Function HenshuSearchKana(ByVal cSearchKey As ABAtenaSearchKey,
                                        ByVal blnHommyoYusen As Boolean) As ABAtenaSearchKey
        Const THIS_METHOD_NAME As String = "HenshuSearchKana"

        Dim cSearch_Param As ABAtenaSearchKey '宛名検索キーパラメータ
        Dim HenshuKanaSeiMei As String = String.Empty  '編集検索用カナ姓名(英文字は大文字で格納すること)
        Dim HenshuKanaSei As String = String.Empty     '編集検索用カナ姓(英文字は大文字で格納すること)
        Dim HenshuKanaMei As String = String.Empty     '編集検索用カナ名(英文字は大文字で格納すること)
        '* 履歴番号 000034 2007/10/10 追加開始
        Dim HenshuKanaSei2 As String = String.Empty    '編集検索用カナ姓２(英文字は大文字で格納すること)
        Dim cuString As New USStringClass              'ミドルネーム等清音化
        '* 履歴番号 000034 2007/10/10 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '宛名検索キーパラメータをコピー
            cSearch_Param = cSearchKey

            '外国人本名検索機能初期設定を宛名検索キーパラメータに設定
            cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho

            '標準仕様の場合は何も編集せずにそのまま返す
            '外国人本名優先検索機能が導入された市町村は
            'ＤＢ項目が専用なので(検索用カナ姓名・検索用カナ姓・検索用カナ名・検索用漢字名称をそれぞれ再セット)
            If (m_cURKanriJohoB.GetFrn_HommyoKensaku_Param() = 2) Then
                '外国人本名検索機能を宛名検索キーパラメータに設定
                cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki
                If (blnHommyoYusen = True) Then
                    '検索パラメータの編集
                    '*履歴番号 000036 2007/11/06 追加開始
                    ' 検索カナ姓名が有り、検索カナ姓が無しの場合、検索カナ姓名は検索カナ姓と同様の扱いをする
                    If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty AndAlso
                        cSearchKey.p_strSearchKanaSei = String.Empty) Then
                        cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    End If
                    ''検索カナ姓名を検索カナ姓の検索キーパラメータとしてセット
                    'If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                    '    HenshuKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    'End If
                    '*履歴番号 000036 2007/11/06 追加終了
                    '検索カナ姓を検索カナ姓の検索キーパラメータとしてセット
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty) Then
                        '*履歴番号 000036 2007/11/06 修正開始
                        ' 検索用カナ姓のアルファベットを大文字に変換する
                        HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        ''検索カナ姓の文字の最後に"%"を必ず付加する
                        'If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                        '    HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        'Else
                        '    HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                        'End If
                        '*履歴番号 000036 2007/11/06 修正終了
                    End If
                    'カナ姓とカナ名がある場合，結合して検索カナ姓の検索キーパラメータとしてセット
                    '全ての検索カナ項目で検索がかけられた場合はこの検索キーがセットされる
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty AndAlso cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '* 履歴番号 000034 2007/10/10 追加開始
                        ' カナ名の先頭文字が"ｳ"の場合のみ"ｵ"に置換して検索用カナ姓２を生成する
                        If (cSearchKey.p_strSearchKanaMei.StartsWith("ｳ")) Then
                            ' カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                            If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                                HenshuKanaSei2 = HenshuKanaSei + cuString.ToKanaKey(Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).Replace(" ", String.Empty)).ToUpper()
                            Else
                                HenshuKanaSei2 = HenshuKanaSei + Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).ToUpper()
                            End If
                        End If
                        ' カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                        If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                            HenshuKanaSei = HenshuKanaSei + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", String.Empty)).ToUpper()
                        Else
                            HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper()
                        End If
                        'HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper()
                        '* 履歴番号 000034 2007/10/10 追加終了
                    End If
                    'カナ名のみの場合，先頭に％を加え検索カナ姓の検索キーパラメータとしてセット
                    If (cSearchKey.p_strSearchKanaSei = String.Empty AndAlso cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '* 履歴番号 000034 2007/10/10 追加開始
                        ' カナ名の先頭文字が"ｳ"の場合のみ"ｵ"に置換して検索用カナ姓２を生成する
                        If (cSearchKey.p_strSearchKanaMei.StartsWith("ｳ")) Then
                            ' カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                            If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                                HenshuKanaSei2 = "%" + cuString.ToKanaKey(Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).Replace(" ", String.Empty)).ToUpper()
                            Else
                                HenshuKanaSei2 = "%" + Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).ToUpper()
                            End If
                        End If
                        ' カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                        If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                            HenshuKanaSei = "%" + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", String.Empty)).ToUpper()
                        Else
                            HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper()
                        End If
                        'HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper()
                        '* 履歴番号 000034 2007/10/10 追加終了
                    End If
                    '検索用カナ姓２に編集した検索キーを検索キーパラメータにセット
                    '本名の検索パラメータをセット
                    cSearch_Param.p_strSearchKanaSeiMei = String.Empty
                    cSearch_Param.p_strSearchKanaSei = HenshuKanaSei                            'カナは検索カナ姓の項目のみで検索
                    cSearch_Param.p_strSearchKanaMei = String.Empty
                    cSearch_Param.p_strSearchKanaSei2 = HenshuKanaSei2                    '検索用カナ姓２
                    '検索漢字名称
                    cSearch_Param.p_strKanjiMeisho2 = cSearchKey.p_strSearchKanjiMeisho         '漢字名称２に検索用漢字名称をセット
                    cSearch_Param.p_strSearchKanjiMeisho = String.Empty
                Else
                    '検索パラメータの編集
                    '*履歴番号 000036 2007/11/06 追加開始
                    ' 検索カナ姓名が有り、検索カナ姓が無しの場合、検索カナ姓名は検索カナ姓と同様の扱いをする
                    If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty AndAlso
                        cSearchKey.p_strSearchKanaSei = String.Empty) Then
                        cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    End If
                    ''検索カナ姓名を検索カナ姓名の検索キーパラメータとしてセット
                    'If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                    '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    'End If
                    '*履歴番号 000036 2007/11/06 追加終了
                    '検索カナ姓がある場合は検索カナ姓名にパラメータをセット
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty) Then
                        '*履歴番号 000036 2007/11/06 修正開始
                        ' 検索カナ姓と検索カナ名の両方に"%"が無い場合は完全一致
                        If (InStr(cSearchKey.p_strSearchKanaSei, "%") = 0 AndAlso
                            InStr(cSearchKey.p_strSearchKanaMei, "%") = 0) Then
                            ' 完全一致時のみ検索カナ姓名として結合するので、清音化を行う
                            HenshuKanaSeiMei = cuString.ToKanaKey(cSearchKey.p_strSearchKanaSei + cSearchKey.p_strSearchKanaMei).ToUpper()
                        Else
                            ' "%"がある場合はそのまま検索カナ姓名に大文字化してセット
                            ' ただし"%"のみの場合は何もセットしない
                            If (cSearchKey.p_strSearchKanaSei <> "%") Then
                                HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper()
                            End If
                            '検索カナ名をアルファベット大文字化してセット
                            If (cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                                HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()
                            End If
                        End If
                        ''検索カナ姓の文字の最後に"%"を必ず付加し，検索カナ姓名の検索キーパラメータとしてセット
                        'If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                        '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        'Else
                        '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                        'End If
                        ''検索カナ名をアルファベット大文字化してセット
                        'If (cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '    HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()
                        'End If
                        '*履歴番号 000036 2007/11/06 修正終了
                    Else
                        '検索カナ名
                        HenshuKanaMei = cSearch_Param.p_strSearchKanaMei.ToUpper()
                    End If
                    '検索用カナ姓２に編集した検索キーを検索キーパラメータにセット
                    '通称名の検索パラメータをセット
                    cSearch_Param.p_strSearchKanaSeiMei = HenshuKanaSeiMei                      'カナ姓名，カナ姓
                    cSearch_Param.p_strSearchKanaSei = String.Empty
                    cSearch_Param.p_strSearchKanaMei = HenshuKanaMei                            'カナ名
                    cSearch_Param.p_strSearchKanaSei2 = String.Empty                         '検索用カナ姓２（空にする）
                    '検索漢字名称
                    cSearch_Param.p_strSearchKanjiMeisho = cSearchKey.p_strSearchKanjiMeisho    '検索用漢字名称に検索用漢字名称をセット
                    cSearch_Param.p_strKanjiMeisho2 = String.Empty
                End If
                '* 履歴番号 000034 2007/10/10 追加開始
            Else
                ' 標準仕様の市町村においても検索カナ項目のアルファベットは大文字で扱う
                cSearch_Param.p_strSearchKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper() 'カナ姓名
                cSearch_Param.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()       'カナ姓
                cSearch_Param.p_strSearchKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()       'カナ名
                cSearch_Param.p_strSearchKanaSei2 = String.Empty                              '検索用カナ姓２（空にする）
                '* 履歴番号 000034 2007/10/10 追加終了
            End If

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

        Return cSearch_Param

    End Function
#End Region
    '*履歴番号 000032 2007/09/04 追加終了

    '*履歴番号 000040 2008/11/10 追加開始
#Region " 利用届編集処理(RiyoTdkHenshu) "
    '************************************************************************************************
    '* メソッド名     利用届編集処理
    '* 
    '* 構文           Private Sub RiyoTdkHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
    '* 　　                                     ByVal blnKobetsu As Boolean, 
    '* 　　                                     ByRef csAtenaH As DataSet)
    '* 
    '* 機能　　    　 利用届データを取得し、宛名データセットへセットする
    '* 
    '* 引数           cAtenaGetPara1 As ABAtenaGetPara1XClass   : 宛名取得パラメータ
    '* 　　           blnKobetsu As Boolean                     : 個別事項判定フラグ
    '*                csAtenaH As DataSet                       : 本人データ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub RiyoTdkHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal blnKobetsu As Boolean, ByRef csAtenaH As DataSet)
        Dim cABLTRiyoTdkB As ABLTRiyoTdkBClass                      ' ABeLTAX利用届マスタＤＡ
        Dim cABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass              ' ABeLTAX利用届パラメータクラス
        Dim csRiyoTdkEntity As DataSet                              ' 利用届データセット
        Dim csRiyoTdkRow As DataRow                                 ' 利用届データセット
        Dim csRow As DataRow
        '*履歴番号 000041 2008/11/17 追加開始
        Dim csNotRiyouTdkdRows As DataRow()
        '*履歴番号 000041 2008/11/17 追加終了

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '*履歴番号 000041 2008/11/17 追加開始
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then
                Exit Try
            Else
            End If
            '*履歴番号 000041 2008/11/17 追加終了

            '*履歴番号 000042 2008/11/18 修正開始
            'If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
            '    blnKobetsu = False AndAlso (cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2")) Then
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso
                blnKobetsu = False AndAlso m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo AndAlso
                (cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2")) Then
                '*履歴番号 000042 2008/11/18 修正終了
                ' 簡易版ではない場合かつ個別事項取得しない場合かつ利用届出取得区分が"1,2"の場合、納税者IDと利用者IDをセット

                ' ABeLTAX利用届マスタＤＡクラスのインスタンス作成
                cABLTRiyoTdkB = New ABLTRiyoTdkBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                ' ABeLTAX利用届パラメータクラスのインスタンス化
                cABLTRiyoTdkParaX = New ABLTRiyoTdkParaXClass

                ' 取得データセット処理
                For Each csRow In csAtenaH.Tables(0).Rows

                    ' 利用届出パラメータセット
                    ' 住民コード
                    If (m_strHonninJuminCD.Trim = String.Empty) Then
                        ' 住民コードをセット
                        cABLTRiyoTdkParaX.p_strJuminCD = CStr(csRow(ABAtena1Entity.JUMINCD))
                    Else
                        ' 同一人代表者データのため、本人住民コードをセット
                        cABLTRiyoTdkParaX.p_strJuminCD = m_strHonninJuminCD
                    End If

                    ' 税目コード:業務コードをセット
                    cABLTRiyoTdkParaX.p_strZeimokuCD = cAtenaGetPara1.p_strTdkdZeimokuCD

                    ' 廃止フラグ:廃止データ以外を取得
                    cABLTRiyoTdkParaX.p_blnHaishiFG = False

                    ' 出力区分:納税者ID、利用者IDの２項目を取得
                    cABLTRiyoTdkParaX.p_strOutKB = "1"

                    '*履歴番号 000051 2020/11/02 追加開始
                    ' 利用区分：利用届出利用区分をセット
                    cABLTRiyoTdkParaX.p_strRiyoKB = cAtenaGetPara1.p_strTdkdRiyoKB
                    '*履歴番号 000051 2020/11/02 追加終了

                    ' 利用届出データを取得
                    csRiyoTdkEntity = cABLTRiyoTdkB.GetLTRiyoTdkData(cABLTRiyoTdkParaX)

                    ' 利用届出データを本人データにセット
                    csRow.BeginEdit()
                    If (csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Count <> 0) Then
                        csRiyoTdkRow = csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows(0)

                        csRow(ABAtena1Entity.NOZEIID) = csRiyoTdkRow(ABLtRiyoTdkEntity.NOZEIID)         ' 納税者ID
                        csRow(ABAtena1Entity.RIYOSHAID) = csRiyoTdkRow(ABLtRiyoTdkEntity.RIYOSHAID)     ' 利用者ID
                    Else
                        csRow(ABAtena1Entity.NOZEIID) = String.Empty                                    ' 納税者ID
                        csRow(ABAtena1Entity.RIYOSHAID) = String.Empty                                  ' 利用者ID

                    End If
                    csRow.EndEdit()
                Next csRow

                '*履歴番号 000041 2008/11/17 追加開始
                If (cAtenaGetPara1.p_strTdkdKB = "2") Then
                    ' 本人データから納税者IDが空白のデータを取得する
                    csNotRiyouTdkdRows = csAtenaH.Tables(0).Select(ABAtena1Entity.NOZEIID + " = ''")

                    ' 納税者IDが空白のデータを削除する
                    For Each csRow In csNotRiyouTdkdRows
                        csRow.Delete()
                    Next
                Else
                End If
                '*履歴番号 000041 2008/11/17 追加終了
            Else
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            Throw
        End Try

    End Sub
#End Region

    '*履歴番号 000041 2008/11/17 削除開始
#Region " 利用届データ絞込み(RiyoTdkHenshu_Select) "
    ''************************************************************************************************
    ''* メソッド名     利用届編集処理
    ''* 
    ''* 構文           Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
    ''* 　　                                            ByVal blnKobetsu As Boolean, 
    ''* 　　                                            ByRef csAtenaH As DataSet)
    ''* 
    ''* 機能　　    　 本人データから納税者IDが存在しないレコードを削除する
    ''* 
    ''* 引数           cAtenaGetPara1 As ABAtenaGetPara1XClass   : 宛名取得パラメータ
    ''* 　　           blnKobetsu As Boolean                     : 個別事項判定フラグ
    ''*                csAtenaH As DataSet                       : 本人データ
    ''* 
    ''* 戻り値         なし
    ''************************************************************************************************
    'Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal blnKobetsu As Boolean, ByRef csAtena1 As DataSet)
    '    Dim csRow As DataRow
    '    Dim csNotRiyouTdkdRows As DataRow()

    '    Try
    '        'デバッグ開始ログ出力
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    '        If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
    '            blnKobetsu = False AndAlso cAtenaGetPara1.p_strTdkdKB = "2") Then
    '            ' 簡易版ではない場合かつ個別事項取得しない場合かつ利用届出取得区分が"2"の場合、納税者IDが存在しないデータを削除する

    '            ' 本人データから納税者IDが空白のデータを取得する
    '            csNotRiyouTdkdRows = csAtena1.Tables(0).Select(ABAtena1Entity.NOZEIID + " = ''")

    '            ' 納税者IDが空白のデータを削除する
    '            For Each csRow In csNotRiyouTdkdRows
    '                csRow.Delete()
    '            Next
    '        Else
    '        End If

    '        ' デバッグ終了ログ出力
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    '    Catch objAppExp As UFAppException
    '        ' ワーニングログ出力
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "【クラス名:" + Me.GetType.Name + "】" + _
    '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
    '                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
    '                                    "【ワーニング内容:" + objAppExp.Message + "】")
    '        ' エラーをそのままスローする
    '        Throw

    '    Catch objExp As Exception
    '        ' エラーログ出力
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "【クラス名:" + Me.GetType.Name + "】" + _
    '                                    "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
    '                                    "【エラー内容:" + objExp.Message + "】")
    '        Throw
    '    End Try

    'End Sub
#End Region
    '*履歴番号 000041 2008/11/17 削除終了
    '*履歴番号 000040 2008/11/10 追加終了

    '*履歴番号 000052 2023/03/10 追加開始
#Region " 簡易宛名取得１_標準版(AtenaGet1_Hyojun) "
    '************************************************************************************************
    '* メソッド名     簡易宛名取得１_標準版
    '* 
    '* 構文           Public Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet

        Return AtenaGet1_Hyojun(cAtenaGetPara1, False)

    End Function

    '************************************************************************************************
    '* メソッド名     簡易宛名取得１_標準版
    '* 
    '* 構文           Public Function AtenaGet1_Hyoujn(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
    '* 
    '* 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                ByVal blnKobetsu As Boolean) As DataSet

        Return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Hyojun)

    End Function
#End Region

#Region " 簡易宛名取得２_標準版(AtenaGet2_Hyojun) "
    '************************************************************************************************
    '* メソッド名     簡易宛名取得２_標準版
    '* 
    '* 構文           Public Function AtenaGet2_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Function AtenaGet2_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "AtenaGet2_Hyojun"
        Dim csAtenaEntity As DataSet                        '宛名Entity
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'ＲＤＢ接続
            If m_blnBatchRdb = False Then
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + THIS_CLASS_NAME + "】" +
                                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                                "【実行メソッド名:Connect】")
                m_cfRdbClass.Connect()
            End If

            Try
                'コンストラクタの設定を保存
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = False
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = False

                End If

                ' 簡易宛名取得２(内部処理)メソッドを実行する。
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun)

                'コンストラクタの設定を元にもどす
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                ' RDB切断
                If m_blnBatchRdb = False Then
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtenaEntity

    End Function
#End Region

#Region " 介護用宛名取得_標準版(GetKaigoAtena_Hyojun) "
    '************************************************************************************************
    '* メソッド名     介護用宛名取得_標準版
    '* 
    '* 構文           Public Function GetKaigoAtena_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　    　　宛名を取得する
    '* 
    '* 引数           cAtenaGetPara1   : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet : 取得した宛名情報
    '************************************************************************************************
    Public Function GetKaigoAtena_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim csAtenaEntity As DataSet                        '介護用宛名Entity

        Try
            'コンストラクタの設定を保存
            blnAtenaSelectAll = m_blnSelectAll
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If

            '宛名取得メインメソッドの呼出し（引数：取得パラメータクラス、個別事項データ取得フラグ、呼び出しメソッド区分）
            csAtenaEntity = AtenaGetMain(cAtenaGetPara1, False, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Hyojun)

            'コンストラクタの設定を元にもどす
            m_blnSelectAll = blnAtenaSelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll
            End If

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try

        Return csAtenaEntity

    End Function
#End Region

#Region " 年金宛名取得_標準版(NenkinAtenaGet_Hyojun) "
    '************************************************************************************************
    '* メソッド名     年金宛名取得_標準版
    '* 
    '* 構文           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       年金宛名情報を取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet

        '年金宛名ゲットより年金宛名情報を取得する
        Return NenkinAtenaGet_Hyojun(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01)
    End Function

    '************************************************************************************************
    '* メソッド名     年金宛名取得_標準版
    '* 
    '* 構文           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       年金宛名情報を取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer) As DataSet

        Return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Hyojun)

    End Function
#End Region

#Region " 国保宛名履歴取得_標準版(KokuhoAtenaRirekiGet_Hyojun) "
    '************************************************************************************************
    '* メソッド名     国保宛名履歴取得_標準版
    '* 
    '* 構文           Public Function KokuhoAtenaRirekiGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* 機能　　       国保宛名履歴データを取得する
    '* 
    '* 引数           cAtenaGetPara1    : 宛名取得パラメータ
    '* 
    '* 戻り値         DataSet(ABAtena1) : 取得した宛名情報
    '************************************************************************************************
    Public Function KokuhoAtenaRirekiGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "KokuhoAtenaRirekiGet_Hyojun"
        Dim csAtena1Entity As DataSet                       '宛名1Entity

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'ＲＤＢ接続
            If m_blnBatchRdb = False Then
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "【クラス名:" + THIS_CLASS_NAME + "】" +
                                                "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                                "【実行メソッド名:Connect】")
                m_cfRdbClass.Connect()
            End If

            Try
                ' 管理情報取得(内部処理)メソッドを実行する。
                Me.GetKanriJoho()

                ' 国保宛名履歴取得(内部処理)メソッドを実行する。
                csAtena1Entity = Me.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun)

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                ' RDB切断
                If m_blnBatchRdb = False Then
                    ' RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "【クラス名:" + THIS_CLASS_NAME + "】" +
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                            "【実行メソッド名:Disconnect】")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region
    '*履歴番号 000052 2023/03/10 追加終了

    Public Sub New()

    End Sub
End Class
