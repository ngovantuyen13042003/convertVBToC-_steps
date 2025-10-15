'************************************************************************************************
'* 業務名　　　　   宛名管理システム
'* 
'* クラス名　　     中間サーバーＢＳ連携ビジネスクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 作成日付　　　   2014/08/19
'*
'* 作成者　　　　   石合　亮
'* 
'* 著作権　　　　   (株) 電算
'************************************************************************************************
'* 修正履歴     履歴番号    修正内容
'* 2015/01/08   000001      即時連携廃止に伴う修正（石合）
'* 2015/02/09   000002      登録日時設定値修正（石合）
'* 2015/04/13   000003      中間サーバーＢＳ側仕様変更事項の反映（石合）
'* 2015/05/07   000004      CHAR項目の空白除去対応（石合）
'* 2015/06/09   000005      全角化対応（石合）
'* 2015/07/07   000006      規定値外対応（石合）
'* 2015/07/15   000007      更新日時規定値外対応（石合）
'* 2015/09/29   000008      日付項目、郵便番号規定値外対応（石合）
'* 2015/11/13   000009      全角化対応不具合対応（石合）
'* 2016/06/10   000010      広域対応(大澤汐)
'* 2016/10/19   000011      広域対応２(石合)
'* 2017/05/23   000012      構成市町村コード上５桁対応(石合)
'************************************************************************************************

Option Strict On
Option Compare Binary
Option Explicit On

Imports System.Collections.Generic
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common
Imports Densan.Reams.AB.AB001BX
Imports Densan.Reams.AB.AB001BX.ABEnumDefine
'* 履歴番号 000008 2015/09/29 追加開始
Imports System.Text.RegularExpressions
'* 履歴番号 000008 2015/09/29 追加終了

''' <summary>
''' 中間サーバーＢＳ連携ビジネスクラス
''' </summary>
''' <remarks></remarks>
Public Class ABBSRenkeiBClass

#Region "メンバー変数"

    ' メンバー変数
    Private m_cfLogClass As UFLogClass                                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass                        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                                      ' ＲＤＢクラス

    Private m_cuCityInfo As USSCityInfoClass                                ' 市町村情報
    '* 履歴番号 000001 2015/01/08 追加開始
    Private m_strShichosonCD5 As String                                     ' 市町村コード5桁（管内）
    '* 履歴番号 000001 2015/01/08 追加終了
    Private m_strShichosonMeisho As String                                  ' 市町村名称（管内）
    Private m_cABHojinMeishoB As ABHojinMeishoBClass                        ' 法人名称編集ビジネスクラス

    Private m_csJutonaiJiyuCDConvertTable As Hashtable                      ' 異動事由コード変換テーブル（住登内）
    Private m_csJutogaiJiyuCDConvertTable As Hashtable                      ' 異動事由コード変換テーブル（住登内）

    '* 履歴番号 000001 2015/01/08 削除開始
    '* 履歴番号 000010 2016/06/10 修正開始
    Private m_cABAtenaKanriJohoB As ABAtenaKanriJohoBClass                  ' 宛名管理情報ビジネスクラス
    '* 履歴番号 000010 2016/06/10 修正終了
    'Private m_blnIsExecRenkei As Boolean                                    ' 中間サーバーＢＳ連携有無
    '* 履歴番号 000001 2015/01/08 削除終了

    '* 履歴番号 000007 2015/07/15 追加開始
    Private m_strBeforeJuminCD As String                                    ' 前処理住民コード
    Private m_csTorokuNichijiList As List(Of String)                        ' 登録日時リスト
    '* 履歴番号 000007 2015/07/15 追加終了

    '* 履歴番号 000008 2015/09/29 追加開始
    Private m_cfDate As UFDateClass                                         ' 日付編集クラス
    '* 履歴番号 000008 2015/09/29 追加終了

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABBSRenkeiBClass"            ' クラス名

    '* 履歴番号 000001 2015/01/08 削除開始
    '' 管理情報キー情報
    'Private ReadOnly KEYINFO_35_21() As String = {"35", "21"}               ' 管理情報キー情報.中間サーバーＢＳ連携：連携開始日
    '* 履歴番号 000010 2016/06/10 修正開始
    '' キーインデックス
    Private Enum KEY_INDEX
        SHUKEY = 0                  ' 種別キー
        SHIKIBETSUKEY               ' 識別キー
    End Enum
    '* 履歴番号 000010 2016/06/10 修正終了
    '* 履歴番号 000001 2015/01/08 削除終了

    '* 履歴番号 000010 2016/06/10 追加開始
    '' 管理情報キー情報
    Private ReadOnly KEYINFO_35_40() As String = {"35", "40"}               ' 管理情報キー情報.市町村コード：広域かどうか
    Private ReadOnly KEYINFO_35_41() As String = {"35", "41"}               ' 管理情報キー情報.送信時のシステム識別コード：テストモードかどうか
    '* 履歴番号 000010 2016/06/10 追加終了


    ' 文字数
    Private Class LENGTH
        Public Const SHIMEI_KOMOKU As Integer = 120
        Public Const KANJISHIMEI As Integer = SHIMEI_KOMOKU                 ' 漢字氏名
        Public Const KANASHIMEI As Integer = SHIMEI_KOMOKU                  ' カナ氏名
        Public Const FRNSHIMEI As Integer = SHIMEI_KOMOKU                   ' 外国人氏名
        Public Const FRNKANASHIMEI As Integer = SHIMEI_KOMOKU               ' 外国人カナ名
        Public Const HEIKIMEI As Integer = SHIMEI_KOMOKU                    ' 併記名
        Public Const TSUSHOMEI As Integer = SHIMEI_KOMOKU                   ' 通称名
        Public Const KANATSUSHOMEI As Integer = SHIMEI_KOMOKU               ' カナ通称名
        Public Const JUSHO As Integer = 50                                  ' 住所
    End Class

    ' 宛名異動事由コード
    Private Enum ABAtenaShoriJiyuType
        RonriSakujo = 1
        ShinkiTsuika = 10
        JukiIdoshaTsuika = 11
        Shusei = 12
        Kaifuku = 15
        TokushuTsuika = 91
        TokushuShusei = 92
        ButsuriSakujo = 93
    End Enum

    ' 次期宛名異動事由コード
    '* 履歴番号 000006 2015/07/07 修正開始
    'Private Enum ABJikiAtenaShoriJiyuType
    '    Tsuika = 10
    '    Shusei = 11
    '    JukiIdoshaTsuika = 12
    '    RonriSakujo = 13
    '    Kaifuku = 14
    '    GobyuShusei = 20
    '    ButsuriSakujo = 22
    '    JuminShubetsuHenko = 23
    'End Enum
    Private Enum ABJikiAtenaShoriJiyuType
        Tsuika = 10
        Shusei = 11
        JukiIdoshaTsuika = 12
        RonriSakujo = 13
        Kaifuku = 14
        GobyuShusei = 20
        'RirekiShusei = 21               ' （未使用）
        ButsuriSakujo = 22
        'JuminShubetsuHenko = 23         ' （未使用）
        'YusenShimeiHenko = 24           ' （未使用）
    End Enum
    '* 履歴番号 000006 2015/07/07 修正終了

    ' 次期住基異動事由コード
    '* 履歴番号 000006 2015/07/07 修正開始
    'Private Enum ABJikiJukiShoriJiyuType
    '    TokushuTsuika = 1
    '    TokushuShusei = 2
    '    TokushuSakujo = 3
    '    JuminhyoCDShusei = 4
    '    KojinBangoShusei = 5
    '    RirekiShusei = 6
    '    IdoTorikeshi = 7
    '    Tennyu = 10
    '    Dai30Jo46 = 11
    '    TokureiTennyu = 12
    '    Shussei = 13
    '    Shuseki = 14
    '    ShokkenKisai = 15
    '    JushoSettei = 16
    '    Fusoku5Jo = 17
    '    Dai30Jo47 = 18
    '    Tenshutsu = 20
    '    TokureiTenshutsu = 21
    '    Shibo = 22
    '    ShissoSenkoku = 23
    '    ShokkenShojo = 24
    '    Tenkyo = 30
    '    Kika = 31
    '    KokusekiShutoku = 32
    '    KokusekiSoshitsu = 33
    '    NushiHenko = 40
    '    SetaiBunri = 41
    '    SetaiGappei = 42
    '    SetaiHenko = 43
    '    Dai30Jo48 = 44
    '    Konin = 50
    '    Rikon = 51
    '    YoshiEngumi = 52
    '    YoshiRien = 53
    '    Tenseki = 54
    '    Bunseki = 55
    '    Nyuseki = 56
    '    Ninchi = 57
    '    KosekiSonota = 58
    '    ShokkenShusei = 60
    '    KosekiShusei = 61
    '    TenshutsuTorikeshi = 62
    '    ShokkenKaifuku = 63
    '    TennyuTsuchiJuri = 64
    '    JuminhyoCDKisai = 65
    '    JuminhyoCDHenko = 66
    '    KojinBangoKisai = 67
    '    KojinBangoHenko = 68
    '    KojinBangoShokkenShusei = 69
    '    HomushoJukyochiTodoke = 70
    '    TsushoKisai = 71
    '    TsushoSakujo = 72
    '    TokuEiShoShinsei = 73
    '    TokuEiShoKofu = 74
    '    KosekiTodokeGaiKonin = 75
    '    JuminhyoKaisei = 80
    '    UtsushiSeigyo = 81
    '    HyojijunHenko = 82
    '    KobetsuJikoShusei = 83
    'End Enum
    Private Enum ABJikiJukiShoriJiyuType
        TokushuSakujo = 1
        TokushuTsuika = 2
        TokushuShusei = 3
        JuminhyoCDShusei = 4
        KojinBangoShusei = 5
        KojinBangoKisai = 6
        RirekiShusei = 8
        'IdoTorikeshi = 9                    ' （未使用）
        Tennyu = 10
        Shussei = 11
        ShokkenKisai = 12
        Kika = 13
        KokusekiShutoku = 14
        JushoSettei = 15
        'Dai30Jo46 = 16                      ' （未使用）
        'TokureiTennyu = 17                  ' （未使用）
        'Fusoku5Jo = 18                      ' （未使用）
        'Dai30Jo47 = 19                      ' （未使用）
        Tenshutsu = 20
        Shibo = 21
        ShokkenShojo = 22
        KokusekiSoshitsu = 23
        ShissoSenkoku = 24
        'TokureiTenshutsu = 25               ' （未使用）
        Tenkyo = 30
        SetaiBunri = 31
        SetaiGappei = 32
        SetaiHenko = 33
        'Dai30Jo48 = 34                      ' （未使用）
        NushiHenko = 40
        ShokkenShusei = 41
        KosekiShusei = 42
        TenshutsuTorikeshi = 43
        ShokkenKaifuku = 44
        TennyuTsuchiJuri = 45
        JuminhyoCDHenko = 46
        JuminhyoCDKisai = 47
        KojinBangoHenko = 48
        KojinBangoShokkenShusei = 49
        Konin = 50
        Rikon = 51
        YoshiEngumi = 52
        YoshiRien = 53
        Tenseki = 54
        Bunseki = 55
        Nyuseki = 56
        Ninchi = 57
        KosekiSonota = 58
        'Shuseki = 59                        ' （未使用）
        JuminhyoKaisei = 60
        UtsushiSeigyo = 61
        HyojijunHenko = 62
        KobetsuJikoShusei = 63
        'HomushoJukyochiTodoke = 70          ' （未使用）
        'TsushoKisai = 71                    ' （未使用）
        'TsushoSakujo = 72                   ' （未使用）
        'TokuEiKyokaShinsei = 73             ' （未使用）
        'TokuEiKyokaShinsaKekkaToroku = 74   ' （未使用）
        'TokuEiKyokaKofu = 75                ' （未使用）
        'TokuEiShoShinsei = 76               ' （未使用）
        'TokuEiShoShinsaKekkaToroku = 77     ' （未使用）
        'TokuEiShoKofu = 78                  ' （未使用）
        'KosekiTodokeGaiKonin = 79           ' （未使用）
    End Enum
    '* 履歴番号 000006 2015/07/07 修正終了

    '* 履歴番号 000001 2015/01/08 追加開始
    '* 履歴番号 000003 2015/04/27 修正開始
    'Private Const SYSTEM_SHIKIBETSUCD As String = "000"
    Private Const SYSTEM_SHIKIBETSUCD As String = "001"
    '* 履歴番号 000003 2015/04/27 修正終了
    '* 履歴番号 000001 2015/01/08 追加終了

    '* 履歴番号 000010 2016/06/10 追加開始
    Private Const ISKOIKI As String = "1"
    '* 履歴番号 000010 2016/06/10 追加終了

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

        ' 市町村情報の取得
        m_cuCityInfo = New USSCityInfoClass
        m_cuCityInfo.GetCityInfo(m_cfControlData)
        '* 履歴番号 000001 2015/01/08 追加開始
        m_strShichosonCD5 = m_cuCityInfo.p_strShichosonCD(0).RSubstring(0, 5)
        '* 履歴番号 000001 2015/01/08 追加終了
        m_strShichosonMeisho = m_cuCityInfo.p_strShichosonMeisho(0).Trim

        ' 法人名称編集ビジネスクラスのインスタンス化
        m_cABHojinMeishoB = New ABHojinMeishoBClass(m_cfControlData, m_cfConfigDataClass)

        ' 異動事由変換テーブルの設定
        Me.SetIdoJiyuCD()

        '* 履歴番号 000001 2015/01/08 削除開始
        '' 連携有無取得
        'm_blnIsExecRenkei = IsExecRenkei()
        '* 履歴番号 000001 2015/01/08 削除終了

        '* 履歴番号 000007 2015/07/15 追加開始
        m_strBeforeJuminCD = String.Empty
        m_csTorokuNichijiList = New List(Of String)
        '* 履歴番号 000007 2015/07/15 追加終了

        '* 履歴番号 000008 2015/09/29 追加開始
        m_cfDate = New UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero)
        '* 履歴番号 000008 2015/09/29 追加終了

        '* 履歴番号 000010 2016/06/10 追加開始
        ' 宛名管理情報ビジネスクラスのインスタンス化
        m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '* 履歴番号 000010 2016/06/10 追加終了

    End Sub

#End Region

#Region "メソッド"

    '* 履歴番号 000001 2015/01/08 削除開始
#Region "【廃止】即時連携がなくなり、スケジューラーでの日次連携のみとなったため、コメントアウト"

    '#Region "連携有無取得"

    '    ''' <summary>
    '    ''' 連携有無取得
    '    ''' </summary>
    '    ''' <returns>連携有無</returns>
    '    ''' <remarks></remarks>
    '    Private Function IsExecRenkei() As Boolean

    '        Dim blnResult As Boolean
    '        Dim csDataSet As DataSet
    '        Dim strParameter As String
    '        Dim strSystemDate As String

    '        Try

    '            ' 返信オブジェクトの初期化
    '            blnResult = False

    '            ' 宛名管理情報ビジネスクラスのインスタンス化
    '            m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

    '            ' 管理情報の取得 [35-21] 中間サーバーＢＳ連携：連携開始日
    '            csDataSet = m_cABAtenaKanriJohoB.GetKanriJohoHoshu(KEYINFO_35_21(KEY_INDEX.SHUKEY), KEYINFO_35_21(KEY_INDEX.SHIKIBETSUKEY))

    '            ' 取得件数を判定
    '            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

    '                ' 取得結果が1件以上の場合、パラメーターを取得
    '                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

    '                ' パラメーター値を判定
    '                If (strParameter.Trim.Length > 0) Then

    '                    ' システム日付を取得
    '                    strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")

    '                    ' システム日付 ＜ 連携開始日　 -> 連携しない
    '                    ' 連携開始日　 ≦ システム日付 -> 連携する
    '                    If (strSystemDate < strParameter) Then
    '                        blnResult = False
    '                    Else
    '                        blnResult = True
    '                    End If

    '                Else
    '                    ' パラメーター値なし -> 連携しない
    '                    blnResult = False
    '                End If

    '            Else
    '                ' レコードなし -> 連携しない
    '                blnResult = False
    '            End If

    '        Catch csExp As Exception
    '            Throw
    '        End Try

    '        Return blnResult

    '    End Function

    '#End Region

    '#Region "中間サーバーＢＳ連携一連処理"

    '    ''' <summary>
    '    ''' 中間サーバーＢＳ連携一連処理
    '    ''' </summary>
    '    ''' <param name="strJuminCD">対象者住民コード</param>
    '    ''' <remarks></remarks>
    '    Public Sub ExecRenkei( _
    '        ByVal strJuminCD As String)
    '        Me.ExecRenkei(New String() {strJuminCD})
    '    End Sub

    '    ''' <summary>
    '    ''' 中間サーバーＢＳ連携一連処理
    '    ''' </summary>
    '    ''' <param name="a_strJuminCD">対象者住民コード配列</param>
    '    ''' <remarks></remarks>
    '    Public Sub ExecRenkei( _
    '        ByVal a_strJuminCD() As String)
    '        Dim csJuminCD As ArrayList
    '        csJuminCD = New ArrayList
    '        For Each strJuminCD As String In a_strJuminCD
    '            csJuminCD.Add(strJuminCD)
    '        Next strJuminCD
    '        Me.ExecRenkei(csJuminCD)
    '    End Sub

    '    ''' <summary>
    '    ''' 中間サーバーＢＳ連携一連処理
    '    ''' </summary>
    '    ''' <param name="csJuminCD">対象者住民コードリスト</param>
    '    ''' <remarks></remarks>
    '    Public Sub ExecRenkei( _
    '        ByVal csJuminCD As ArrayList)

    '        Const JOB_ID As String = "ABJ96210"

    '        Dim cuBatchReg As New USBBatchRegisterClass
    '        Dim csDataSet As DataSet
    '        Dim cfErrorClass As UFErrorClass
    '        Dim cfErrorStruct As UFErrorStruct

    '        Try

    '            ' 連携有無を判定
    '            If (m_blnIsExecRenkei = True) Then

    '                ' バッチパラメーター取得
    '                csDataSet = Me.GetBatchParameter

    '                ' 住民コードパラメーター追加
    '                csDataSet = AddJuminCDParameter(csDataSet, csJuminCD)

    '                ' バッチ登録クラスのインスタンス化
    '                cuBatchReg = New USBBatchRegisterClass()

    '                ' バッチ登録の実行（バッチ登録時のエラーはExceptionがThrowされるため、ステータスの判定は行わない。）
    '                cuBatchReg.RegistBatch(m_cfControlData, ABConstClass.THIS_BUSINESSID, JOB_ID, csDataSet, USBBatchRegisterClass.USLBangoLog.MIX)

    '            Else
    '                ' noop
    '            End If

    '        Catch csExp As Exception

    '            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
    '            cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003304)
    '            Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode, csExp)

    '        End Try

    '    End Sub

    '#End Region

    '#Region "バッチパラメーター取得"

    '    ''' <summary>
    '    ''' バッチパラメーター取得
    '    ''' </summary>
    '    ''' <returns>バッチパラメーター</returns>
    '    ''' <remarks></remarks>
    '    Private Function GetBatchParameter() As DataSet

    '        Dim csDataSet As DataSet
    '        Dim csDataTalbe As DataTable
    '        Dim csDataRow As DataRow

    '        Try

    '            csDataSet = New DataSet()
    '            csDataTalbe = csDataSet.Tables.Add(ABBPBSRenkei.TABLE_NAME)

    '            With csDataTalbe

    '                .Columns.Add(ABBPBSRenkei.CHUSHUTSUKBN)
    '                .Columns.Add(ABBPBSRenkei.CHUSHUTSUJOKEN)

    '            End With

    '            csDataRow = csDataTalbe.NewRow
    '            With csDataRow
    '                .BeginEdit()
    '                .Item(ABBPBSRenkei.CHUSHUTSUKBN) = ABBPBSRenkei.DEFALUT.CHUSHUTSUKBN.IDOBUN
    '                .Item(ABBPBSRenkei.CHUSHUTSUJOKEN) = ABBPBSRenkei.DEFALUT.CHUSHUTSUJOKEN.JUMINCD
    '                .EndEdit()
    '            End With
    '            csDataTalbe.Rows.Add(csDataRow)

    '        Catch csExp As Exception
    '            Throw
    '        End Try

    '        Return csDataSet

    '    End Function

    '#End Region

    '#Region "住民コードパラメーター追加"

    '    ''' <summary>
    '    ''' 住民コードパラメーター追加
    '    ''' </summary>
    '    ''' <param name="csDataSet">バッチパラメーター</param>
    '    ''' <param name="csJuminCD">住民コードリスト</param>
    '    ''' <returns>バッチパラメーター</returns>
    '    ''' <remarks></remarks>
    '    Private Function AddJuminCDParameter( _
    '        ByVal csDataSet As DataSet, _
    '        ByVal csJuminCD As ArrayList) As DataSet

    '        Dim csDataTalbe As DataTable
    '        Dim csDataRow As DataRow

    '        Try

    '            csDataTalbe = csDataSet.Tables.Add(ABBPJuminCD.TABLE_NAME)

    '            With csDataTalbe

    '                .Columns.Add(ABBPJuminCD.JUMINCD)

    '            End With

    '            For Each strJuminCD As String In csJuminCD

    '                csDataRow = csDataTalbe.NewRow
    '                With csDataRow
    '                    .BeginEdit()
    '                    .Item(ABBPJuminCD.JUMINCD) = strJuminCD
    '                    .EndEdit()
    '                End With
    '                csDataTalbe.Rows.Add(csDataRow)

    '            Next strJuminCD

    '        Catch csExp As Exception
    '            Throw
    '        End Try

    '        Return csDataSet

    '    End Function

    '#End Region

#End Region
    '* 履歴番号 000001 2015/01/08 削除終了

#Region "連携データ編集処理"

    ''' <summary>
    ''' 連携データ編集処理（バッチ用の入り口）
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>編集結果</returns>
    ''' <remarks></remarks>
    Public Function EditRenkeiDataForBatch(ByVal cParam As ABBSRenkeiPrmXClass) As ABBSRenkeiRetXClass
        Return Me.EditData(cParam)
    End Function

    ''' <summary>
    ''' 連携データ編集処理
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>編集結果</returns>
    ''' <remarks></remarks>
    Private Function EditData(ByVal cParam As ABBSRenkeiPrmXClass) As ABBSRenkeiRetXClass

        Dim cResult As ABBSRenkeiRetXClass

        Try

            ' 返信オブジェクトのインスタンス化
            cResult = New ABBSRenkeiRetXClass

            With cResult

                '* 履歴番号 000001 2015/01/08 追加開始
                ' -----------------------------------------------------------------------------------------------------
                ' システム識別コード
                '* 履歴番号 000010 2016/06/10 修正開始
                '.m_strSystemShikibetsuCD = SYSTEM_SHIKIBETSUCD
                .m_strSystemShikibetsuCD = Me.GetSystemShikibetsuCD()
                '* 履歴番号 000010 2016/06/10 修正終了
                '* 履歴番号 000001 2015/01/08 追加終了
                ' -----------------------------------------------------------------------------------------------------
                ' 識別コード
                .m_strShikibetsuCD = cParam.m_strJuminCd.Trim.RPadLeft(15, "0"c)
                '* 履歴番号 000001 2015/01/08 追加開始
                ' -----------------------------------------------------------------------------------------------------
                ' 登録日時
                .m_strTorokuNichiji = Me.GetKoshinNichiji(cParam)
                '* 履歴番号 000001 2015/01/08 追加終了
                ' -----------------------------------------------------------------------------------------------------
                ' 住民種別コード
                .m_strJuminShubetsuCD = Me.GetJuminShubetsuCD(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 住民状態コード
                .m_strJuminJotaiCD = Me.GetJuminJotaiCD(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 漢字氏名
                .m_strKanjiShimei = Me.EditKanjiShimei(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' カナ氏名
                .m_strKanaShimei = Me.EditKanaShimei(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 外国人氏名項目
                If (Me.IsKojin(cParam) = True) Then
                    ' -------------------------------------------------------------------------------------------------
                    ' 外国人氏名
                    .m_strFrnShimei = Me.Left(cParam.m_strFZYHongokumei, LENGTH.FRNSHIMEI)
                    ' -------------------------------------------------------------------------------------------------
                    ' 併記名
                    .m_strHeikimei = Me.Left(cParam.m_strFZYKanjiHeikimei, LENGTH.HEIKIMEI)
                    ' -------------------------------------------------------------------------------------------------
                    ' 外国人カナ名
                    .m_strFrnKanaShimei = Me.EditFrnKanaShimei(cParam)
                    ' -------------------------------------------------------------------------------------------------
                    ' 通称名
                    .m_strTsushomei = Me.Left(cParam.m_strFZYKanjiTsushomei, LENGTH.TSUSHOMEI)
                    ' -------------------------------------------------------------------------------------------------
                    ' カナ通称名
                    .m_strKanaTsushomei = Me.Left(cParam.m_strFZYKanaTsushomei, LENGTH.KANATSUSHOMEI)
                    ' -------------------------------------------------------------------------------------------------
                Else
                    ' -------------------------------------------------------------------------------------------------
                    ' 外国人氏名
                    .m_strFrnShimei = String.Empty
                    ' -------------------------------------------------------------------------------------------------
                    ' 併記名
                    .m_strHeikimei = String.Empty
                    ' -------------------------------------------------------------------------------------------------
                    ' 外国人カナ名
                    .m_strFrnKanaShimei = String.Empty
                    ' -------------------------------------------------------------------------------------------------
                    ' 通称名
                    .m_strTsushomei = String.Empty
                    ' -------------------------------------------------------------------------------------------------
                    ' カナ通称名
                    .m_strKanaTsushomei = String.Empty
                    ' -------------------------------------------------------------------------------------------------
                End If
                ' -----------------------------------------------------------------------------------------------------
                ' 氏名利用区分
                .m_strShimeiRiyoKB = Me.GetShimeiRiyoKB(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 生年月日
                '* 履歴番号 000003 2015/04/13 修正開始
                'If (Me.IsKojin(cParam) = True) Then
                '    .m_strUmareYMD = cParam.m_strUmareYmd.Trim
                'Else
                '    .m_strUmareYMD = String.Empty
                'End If
                '* 履歴番号 000008 2015/09/29 修正開始
                '.m_strUmareYMD = GetUmareYMD(cParam)
                .m_strUmareYMD = Me.CheckDate(GetUmareYMD(cParam))
                '* 履歴番号 000008 2015/09/29 修正終了
                '* 履歴番号 000003 2015/04/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 生年月日不詳区分
                .m_strUmareFushoKBN = Me.GetUmareFushoKBN(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 性別コード
                .m_strSeibetsuCD = Me.GetSeibetsuCD(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 国籍
                If (Me.IsKojin(cParam) = True) Then
                    .m_strKokuseki = cParam.m_strKokuseki
                Else
                    .m_strKokuseki = String.Empty
                End If
                ' -----------------------------------------------------------------------------------------------------
                ' 郵便番号
                '* 履歴番号 000008 2015/09/29 修正開始
                '.m_strYubinNo = cParam.m_strYubinNo.Trim
                .m_strYubinNo = Me.CheckZIPCode(cParam.m_strYubinNo.Trim)
                '* 履歴番号 000008 2015/09/29 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 住所
                .m_strJusho = Me.EditJusho(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 番地
                .m_strBanchi = cParam.m_strBanchi
                ' -----------------------------------------------------------------------------------------------------
                ' 方書
                .m_strKatagaki = cParam.m_strKatagaki
                ' -----------------------------------------------------------------------------------------------------
                ' 連絡先１
                .m_strRenrakusaki1 = cParam.m_strRenrakusaki1
                ' -----------------------------------------------------------------------------------------------------
                ' 連絡先２
                .m_strRenrakusaki2 = cParam.m_strRenrakusaki2
                ' -----------------------------------------------------------------------------------------------------
                ' 異動日
                '* 履歴番号 000008 2015/09/29 修正開始
                '.m_strIdoYMD = cParam.m_strCkinIdoYmd.Trim
                .m_strIdoYMD = Me.CheckDate(cParam.m_strCkinIdoYmd.Trim)
                '* 履歴番号 000008 2015/09/29 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 異動事由コード
                .m_strIdoJiyuCD = Me.GetIdoJiyuCD(cParam)
                ' -----------------------------------------------------------------------------------------------------
                '* 履歴番号 000001 2015/01/08 削除開始
                '' 更新日時
                '.m_strKoshinNichiji = Me.GetKoshinNichiji(cParam)
                '* 履歴番号 000001 2015/01/08 削除終了
                ' -----------------------------------------------------------------------------------------------------
                ' 備考
                .m_strBiko = Me.GetBiko(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 個人番号
                '* 履歴番号 000001 2015/01/08 修正開始
                '.m_strKojinBango = Me.GetKojinBango(cParam)
                .m_strKojinBango = cParam.m_strMyNumber.Trim
                '* 履歴番号 000001 2015/01/08 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 直近区分
                .m_strCkinKB = Me.GetCkinKB(cParam)
                ' -----------------------------------------------------------------------------------------------------
                '* 履歴番号 000001 2015/01/08 削除開始
                '' 法人番号
                '.m_strHojinBango = Me.GetHojinBango(cParam)
                '* 履歴番号 000001 2015/01/08 削除終了
                ' -----------------------------------------------------------------------------------------------------
                '* 履歴番号 000001 2015/01/08 追加開始
                ' 市町村コード
                .m_strShichosonCD = Me.GetShichosonCD(cParam)
                ' -----------------------------------------------------------------------------------------------------
                ' 注意喚起フラグ
                .m_strChuuiKankiFG = Me.GetChuuiKankiFG(cParam)
                ' -----------------------------------------------------------------------------------------------------
                '* 履歴番号 000001 2015/01/08 追加終了

                '* 履歴番号 000005 2015/06/09 追加開始
                ' -----------------------------------------------------------------------------------------------------
                ' 全角項目に対して全角化を実施する。（イレギュラーデータに対する考慮。）
                ' 切り取り処理等の編集処理と処理順が前後しないように一律最後に全角化を実施する。
                cResult = Me.ToWide(cResult)
                ' -----------------------------------------------------------------------------------------------------
                '* 履歴番号 000005 2015/06/09 追加終了

            End With

        Catch csExp As Exception
            Throw
        End Try

        Return cResult

    End Function

#End Region

#Region "個人判定"

    ''' <summary>
    ''' 個人判定
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>判定結果</returns>
    ''' <remarks></remarks>
    Private Function IsKojin(ByVal cParam As ABBSRenkeiPrmXClass) As Boolean
        Return (cParam.m_strAtenaDataKb = ABConstClass.ATENADATAKB_JUTONAI_KOJIN _
                OrElse cParam.m_strAtenaDataKb = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)
    End Function

#End Region

#Region "法人判定"

    ''' <summary>
    ''' 法人判定
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>判定結果</returns>
    ''' <remarks></remarks>
    Private Function IsHojin(ByVal cParam As ABBSRenkeiPrmXClass) As Boolean
        Return cParam.m_strAtenaDataKb = ABConstClass.ATENADATAKB_HOJIN
    End Function

#End Region

#Region "外国人判定"

    ''' <summary>
    ''' 外国人判定
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>判定結果</returns>
    ''' <remarks></remarks>
    Private Function IsFrn(ByVal cParam As ABBSRenkeiPrmXClass) As Boolean
        Return ((cParam.m_strAtenaDataKb = ABConstClass.ATENADATAKB_JUTONAI_KOJIN _
                OrElse cParam.m_strAtenaDataKb = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) _
                AndAlso cParam.m_strAtenaDataShu.StartsWith("2", StringComparison.CurrentCulture) = True)
    End Function

#End Region

#Region "文字列切り取り"

    ''' <summary>
    ''' 文字列切り取り
    ''' </summary>
    ''' <param name="strValue">対象文字列</param>
    ''' <param name="intMaxLength">最大文字数</param>
    ''' <returns>切り取り結果文字列</returns>
    ''' <remarks></remarks>
    Private Function Left(ByVal strValue As String, ByVal intMaxLength As Integer) As String
        Dim strResult As String = String.Empty
        Try
            If (strValue IsNot Nothing AndAlso strValue.RLength > intMaxLength) Then
                strResult = strValue.RSubstring(0, intMaxLength)
            Else
                strResult = strValue
            End If
        Catch csExp As Exception
            Throw
        End Try
        '* 履歴番号 000005 2015/06/09 修正開始
        'Return strResult.TrimEnd
        ' TrimEndを廃止する。
        Return strResult
        '* 履歴番号 000005 2015/06/09 修正終了
    End Function

#End Region

#Region "住民種別コード取得"

    ''' <summary>
    ''' 住民種別コード取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>住民種別コード</returns>
    ''' <remarks></remarks>
    Private Function GetJuminShubetsuCD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const JUTONAI_JPN As String = "1"
        Const JUTONAI_FRN As String = "2"
        Const JUTOGAI_JPN As String = "3"
        Const JUTOGAI_FRN As String = "4"
        Const HOJIN As String = "5"
        Const KYOYU As String = "6"

        Dim strResult As String = String.Empty

        Try

            Select Case cParam.m_strAtenaDataKb
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    Select Case cParam.m_strAtenaDataShu
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN, _
                             ABConstClass.JUMINSHU_NIHONJIN_SHOJO, _
                             ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_NIHONJIN_SHIBOU
                            strResult = JUTONAI_JPN
                        Case ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            strResult = JUTONAI_FRN
                        Case Else
                            '* 履歴番号 000006 2015/07/07 修正開始
                            ' 組み合わせが不正の場合、日本人とする。
                            'strResult = String.Empty
                            strResult = JUTONAI_JPN
                            '* 履歴番号 000006 2015/07/07 修正終了
                    End Select
                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    Select Case cParam.m_strAtenaDataShu
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUTOGAI, _
                             ABConstClass.JUMINSHU_NIHONJIN_ETC, _
                             ABConstClass.JUMINSHU_NIHONJIN_SHOJO, _
                             ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_NIHONJIN_SHIBOU
                            strResult = JUTOGAI_JPN
                        Case ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUTOGAI, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            strResult = JUTOGAI_FRN
                        Case Else
                            '* 履歴番号 000006 2015/07/07 修正開始
                            ' 組み合わせが不正の場合、日本人（住登外）とする。
                            'strResult = String.Empty
                            strResult = JUTOGAI_JPN
                            '* 履歴番号 000006 2015/07/07 修正終了
                    End Select
                Case ABConstClass.ATENADATAKB_HOJIN
                    strResult = HOJIN
                Case ABConstClass.ATENADATAKB_KYOYU
                    strResult = KYOYU
                Case Else
                    strResult = String.Empty
            End Select

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "住民状態コード取得"

    ''' <summary>
    ''' 住民状態コード取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>住民状態コード</returns>
    ''' <remarks></remarks>
    Private Function GetJuminJotaiCD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const JUMIN As String = "1"
        Const JUTOGAI_KOJIN As String = "2"
        Const TENSHUTSU As String = "3"
        Const SHIBO As String = "4"
        Const SHOJO As String = "9"

        Dim strResult As String = String.Empty

        Try

            Select Case cParam.m_strAtenaDataKb
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    Select Case cParam.m_strAtenaDataShu
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
                            strResult = JUMIN
                        Case ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU
                            strResult = TENSHUTSU
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            strResult = SHIBO
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHOJO, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO
                            strResult = SHOJO
                        Case Else
                            '* 履歴番号 000006 2015/07/07 修正開始
                            ' 組み合わせが不正の場合、消除者とする。
                            'strResult = String.Empty
                            strResult = SHOJO
                            '* 履歴番号 000006 2015/07/07 修正終了
                    End Select
                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    Select Case cParam.m_strAtenaDataShu
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUTOGAI, _
                             ABConstClass.JUMINSHU_NIHONJIN_ETC, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUTOGAI
                            strResult = JUTOGAI_KOJIN
                        Case ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU
                            strResult = TENSHUTSU
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            strResult = SHIBO
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHOJO, _
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO
                            strResult = SHOJO
                        Case Else
                            '* 履歴番号 000006 2015/07/07 修正開始
                            ' 組み合わせが不正の場合、住登外とする。
                            'strResult = String.Empty
                            strResult = JUTOGAI_KOJIN
                            '* 履歴番号 000006 2015/07/07 修正終了
                    End Select
                Case ABConstClass.ATENADATAKB_HOJIN
                    strResult = String.Empty
                Case ABConstClass.ATENADATAKB_KYOYU
                    strResult = String.Empty
                Case Else
                    strResult = String.Empty
            End Select

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "漢字氏名編集"

    ''' <summary>
    ''' 漢字氏名編集
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>漢字氏名</returns>
    ''' <remarks></remarks>
    Private Function EditKanjiShimei(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            ' 宛名Getの編集に準拠する。
            If (Me.IsHojin(cParam) = True) Then
                m_cABHojinMeishoB.p_strKeitaiFuyoKB = cParam.m_strHanyoKb1
                m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = cParam.m_strHanyoKb2
                m_cABHojinMeishoB.p_strKanjiHjnKeitai = cParam.m_strKanjiHjnKeitai
                m_cABHojinMeishoB.p_strKanjiMeisho1 = cParam.m_strKanjiMeisho1
                m_cABHojinMeishoB.p_strKanjiMeisho2 = cParam.m_strKanjiMeisho2
                strResult = m_cABHojinMeishoB.GetHojinMeisho()
            Else
                strResult = cParam.m_strKanjiMeisho1
            End If
            ' Left内でTrimEndして設定することとする。
            strResult = Me.Left(strResult, LENGTH.KANJISHIMEI)

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "カナ氏名編集"

    ''' <summary>
    ''' カナ氏名編集
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>カナ氏名</returns>
    ''' <remarks></remarks>
    Private Function EditKanaShimei(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            ' 宛名Getの編集に準拠する。
            If (Me.IsHojin(cParam) = True) Then
                '* 履歴番号 000005 2015/06/09 追加開始
                ' ※カナ法人名のTrimEndは準拠する為なので放置する。
                '* 履歴番号 000005 2015/06/09 追加終了
                If (cParam.m_strKanaMeisho2.Trim.RLength > 0) Then
                    strResult = String.Concat(cParam.m_strKanaMeisho1.TrimEnd,
                                              " "c,
                                              cParam.m_strKanaMeisho2.TrimEnd)
                Else
                    strResult = cParam.m_strKanaMeisho1.TrimEnd
                End If
            Else
                strResult = cParam.m_strKanaMeisho1
            End If
            ' Left内でTrimEndして設定することとする。
            strResult = Me.Left(strResult, LENGTH.KANASHIMEI)

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "外国人カナ氏名編集"

    ''' <summary>
    ''' 外国人カナ氏名編集
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>外国人カナ氏名</returns>
    ''' <remarks></remarks>
    Private Function EditFrnKanaShimei(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            ' 値有無判定は漢字項目で行う。
            If (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0) Then
                ' 漢字併記名に値が存在する場合->カナ併記名を設定
                strResult = cParam.m_strFZYKanaHeikimei
            Else
                ' 漢字併記名に値が存在しない場合->カナ本国名を設定
                strResult = cParam.m_strFZYKanaHongokumei
            End If
            ' Left内でTrimEndして設定することとする。
            strResult = Me.Left(strResult, LENGTH.FRNKANASHIMEI)

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "氏名利用区分取得"

    ''' <summary>
    ''' 氏名利用区分取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>氏名利用区分</returns>
    ''' <remarks></remarks>
    Private Function GetShimeiRiyoKB(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const TSUSHOMEI As String = "0"
        Const HEIKIMEI As String = "1"
        Const HONGOKUMEI As String = "2"

        Dim strResult As String = String.Empty

        Try

            If (Me.IsFrn(cParam) = True) Then

                If (cParam.m_strHanyoKb2.Trim = "2") Then

                    If (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0) Then
                        strResult = HEIKIMEI
                    Else
                        strResult = HONGOKUMEI
                    End If

                Else

                    If (cParam.m_strFZYKanjiTsushomei.Trim.RLength > 0) Then
                        strResult = TSUSHOMEI
                    ElseIf (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0) Then
                        strResult = HEIKIMEI
                    Else
                        strResult = HONGOKUMEI
                    End If

                End If

            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

    '* 履歴番号 000003 2015/04/13 追加開始
    ''' <summary>
    ''' 生年月日取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>生年月日</returns>
    ''' <remarks></remarks>
    Private Function GetUmareYMD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            If (Me.IsKojin(cParam) = True) Then

                Select Case cParam.m_strFZYUmareFushoKbn
                    Case ABConstClass.UMAREFUSHOKBN_FUSHO_YMD  ' 年月日が不詳
                        ' 年月日不詳の場合、未設定とする。
                        strResult = String.Empty
                    Case Else
                        strResult = cParam.m_strUmareYmd.Trim
                End Select

            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function
    '* 履歴番号 000003 2015/04/13 追加終了

#Region "生年月日不詳区分取得"

    ''' <summary>
    ''' 生年月日不詳区分取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>生年月日不詳区分</returns>
    ''' <remarks></remarks>
    Private Function GetUmareFushoKBN(ByVal cParam As ABBSRenkeiPrmXClass) As String

        '* 履歴番号 000003 2015/04/13 削除開始
        'Const NONE As String = "0"
        '* 履歴番号 000003 2015/04/13 削除終了
        Const D As String = "1"
        Const MD As String = "2"
        '* 履歴番号 000003 2015/04/13 削除開始
        'Const YMD As String = "3"
        '* 履歴番号 000003 2015/04/13 削除終了

        Dim strResult As String = String.Empty

        Try

            If (Me.IsKojin(cParam) = True) Then

                Select Case cParam.m_strFZYUmareFushoKbn
                    Case ABConstClass.UMAREFUSHOKBN_FUSHO_D  ' 日が不詳
                        strResult = D
                    Case ABConstClass.UMAREFUSHOKBN_FUSHO_MD  ' 月日が不詳
                        strResult = MD
                        '* 履歴番号 000003 2015/04/13 修正開始
                        ' 日が不詳、月日が不詳以外の場合、未設定とする。
                        'Case ABConstClass.UMAREFUSHOKBN_FUSHO_YMD  ' 年月日が不詳
                        '    strResult = YMD
                        'Case Else
                        '    strResult = NONE
                    Case Else
                        strResult = String.Empty
                        '* 履歴番号 000003 2015/04/13 修正終了
                End Select

            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "性別コード取得"

    ''' <summary>
    ''' 性別コード取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>性別コード</returns>
    ''' <remarks></remarks>
    Private Function GetSeibetsuCD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        '* 履歴番号 000003 2015/04/13 削除開始
        'Const MALE As String = "1"
        'Const FEMALE As String = "2"
        'Const ETC As String = "3"
        '* 履歴番号 000006 2015/07/07 追加開始
        Const MALE As String = "1"
        Const FEMALE As String = "2"
        '* 履歴番号 000006 2015/07/07 追加終了
        '* 履歴番号 000003 2015/04/13 削除終了

        Dim strResult As String = String.Empty

        Try

            If (Me.IsKojin(cParam) = True) Then

                '* 履歴番号 000003 2015/04/13 修正開始
                ' コード体系がReamsと同値のため、変換は不要となった。
                'Select Case cParam.m_strSeibetsuCd
                '    Case MALE
                '        strResult = MALE
                '    Case FEMALE
                '        strResult = FEMALE
                '    Case Else
                '        strResult = ETC
                'End Select
                '* 履歴番号 000004 2015/05/07 修正開始
                'strResult = cParam.m_strSeibetsuCd
                '* 履歴番号 000006 2015/07/07 修正開始
                ' 規定値以外をString.Emptyとするため、コード変換ロジックを復活させる。
                'strResult = cParam.m_strSeibetsuCd.Trim
                Select Case cParam.m_strSeibetsuCd
                    Case MALE
                        strResult = MALE
                    Case FEMALE
                        strResult = FEMALE
                    Case Else
                        strResult = String.Empty
                End Select
                '* 履歴番号 000006 2015/07/07 修正終了
                '* 履歴番号 000004 2015/05/07 修正終了
                '* 履歴番号 000003 2015/04/13 修正終了

            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "住所編集"

    ''' <summary>
    ''' 住所編集
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>住所</returns>
    ''' <remarks></remarks>
    Private Function EditJusho(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            '* 履歴番号 000011 2016/11/19 追加開始
            If (Me.CheckKoiki() = True) Then
                ' 広域の場合、都道府県名＋郡名＋市町村名を付加しない
                strResult = cParam.m_strJusho
            Else
                '* 履歴番号 000011 2016/11/19 追加終了
                ' 管内の場合、都道府県名＋郡名＋市町村名を付加する
                If (cParam.m_strKannaiKangaiKb = ABConstClass.KANNAIKB) Then
                    strResult = String.Concat(m_strShichosonMeisho, cParam.m_strJusho)
                Else
                    strResult = cParam.m_strJusho
                End If
                '* 履歴番号 000011 2016/11/19 追加開始
            End If
            '* 履歴番号 000011 2016/11/19 追加終了
            strResult = Me.Left(strResult, LENGTH.JUSHO)

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "異動事由コード設定"

    ''' <summary>
    ''' 異動事由コード設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIdoJiyuCD()

        Try

            ' -------------------------------------------------------------------------------------
            ' 【住登内事由】
            m_csJutonaiJiyuCDConvertTable = New Hashtable
            ' -------------------------------------------------------------------------------------
            ' 特殊追加
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.TokushuTsuika)
            ' -------------------------------------------------------------------------------------
            ' 特殊修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TokushuShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.TokushuShusei)
            ' -------------------------------------------------------------------------------------
            ' 特殊削除
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TokushuSakujo.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.TokushuSakujo)
            ' -------------------------------------------------------------------------------------
            ' 住民票ＣＤ修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TokushuCodeShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.JuminhyoCDShusei)
            ' -------------------------------------------------------------------------------------
            ' 個人番号修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KojinBangoShusei)
            ' -------------------------------------------------------------------------------------
            ' 履歴修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TokushuRirekiShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.RirekiShusei)
            ' -------------------------------------------------------------------------------------
            ' 転入
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Tennyu)
            ' -------------------------------------------------------------------------------------
            ' 出生
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Shussei)
            ' -------------------------------------------------------------------------------------
            ' 職権記載
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.ShokkenKisai)
            ' -------------------------------------------------------------------------------------
            ' 住所設定
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.JushoSettei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.JushoSettei)
            ' -------------------------------------------------------------------------------------
            ' 転出
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Tenshutsu.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Tenshutsu)
            ' -------------------------------------------------------------------------------------
            ' 死亡
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Shibo.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Shibo)
            ' -------------------------------------------------------------------------------------
            ' 失踪宣告
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Shisso.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.ShissoSenkoku)
            ' -------------------------------------------------------------------------------------
            ' 職権消除
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.ShokkenShojo.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.ShokkenShojo)
            ' -------------------------------------------------------------------------------------
            ' 転居
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Tenkyo.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Tenkyo)
            ' -------------------------------------------------------------------------------------
            ' 帰化
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Kika.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Kika)
            ' -------------------------------------------------------------------------------------
            ' 国籍取得
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KokusekiShutoku.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KokusekiShutoku)
            ' -------------------------------------------------------------------------------------
            ' 国籍喪失
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KokusekiSoshitsu.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KokusekiSoshitsu)
            ' -------------------------------------------------------------------------------------
            ' 主変更
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.SetainushiHenko.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.NushiHenko)
            ' -------------------------------------------------------------------------------------
            ' 世帯分離
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.SetaiBunri.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.SetaiBunri)
            ' -------------------------------------------------------------------------------------
            ' 世帯合併
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.SetaiGappei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.SetaiGappei)
            ' -------------------------------------------------------------------------------------
            ' 世帯変更
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.SetaiKoseiHenko.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.SetaiHenko)
            ' -------------------------------------------------------------------------------------
            ' 婚姻
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Konin.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Konin)
            ' -------------------------------------------------------------------------------------
            ' 離婚
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Rikon.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Rikon)
            ' -------------------------------------------------------------------------------------
            ' 養子縁組
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.YoshiEngumi.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.YoshiEngumi)
            ' -------------------------------------------------------------------------------------
            ' 養子離縁
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.YoshiRien.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.YoshiRien)
            ' -------------------------------------------------------------------------------------
            ' 転籍
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Tenseki.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Tenseki)
            ' -------------------------------------------------------------------------------------
            ' 分籍
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Bunseki.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Bunseki)
            ' -------------------------------------------------------------------------------------
            ' 入籍
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Nyuseki.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Nyuseki)
            ' -------------------------------------------------------------------------------------
            ' 認知
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Ninchi.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.Ninchi)
            ' -------------------------------------------------------------------------------------
            ' 戸籍その他
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KosekiSonota.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KosekiSonota)
            ' -------------------------------------------------------------------------------------
            ' 職権修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.ShokkenShusei)
            ' -------------------------------------------------------------------------------------
            ' 戸籍修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KosekiShogoShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KosekiShusei)
            ' -------------------------------------------------------------------------------------
            ' 転出取消
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.TenshutsuTorikeshi)
            ' -------------------------------------------------------------------------------------
            ' 職権回復
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.ShokkenKaifuku)
            ' -------------------------------------------------------------------------------------
            ' 転入通知受理
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.TennyuTsuchiJuri)
            ' -------------------------------------------------------------------------------------
            ' 住民票ＣＤ記載
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.CodeShokkenKisai.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.JuminhyoCDKisai)
            ' -------------------------------------------------------------------------------------
            ' 住民票ＣＤ変更
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.CodeHenkoSeikyu.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.JuminhyoCDHenko)
            ' -------------------------------------------------------------------------------------
            ' 個人番号記載
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KojinBangoKisai)
            ' -------------------------------------------------------------------------------------
            ' 個人番号変更
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KojinBangoHenko)
            ' -------------------------------------------------------------------------------------
            ' 個人番号職権修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KojinBangoShokkenShusei)
            ' -------------------------------------------------------------------------------------
            ' 住民票改製
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.JuminhyoKaisei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.JuminhyoKaisei)
            ' -------------------------------------------------------------------------------------
            ' 写し制御
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.UtsushiSeigyo.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.UtsushiSeigyo)
            ' -------------------------------------------------------------------------------------
            ' 表示順変更
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.HyojijunHenko.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.HyojijunHenko)
            ' -------------------------------------------------------------------------------------
            ' 個別事項修正
            m_csJutonaiJiyuCDConvertTable.Add( _
                ABJukiShoriJiyuType.KobetsuShusei.GetHashCode.ToString("00"), _
                ABJikiJukiShoriJiyuType.KobetsuJikoShusei)
            ' -------------------------------------------------------------------------------------

            ' -------------------------------------------------------------------------------------
            ' 【住登外事由】
            m_csJutogaiJiyuCDConvertTable = New Hashtable
            ' -------------------------------------------------------------------------------------
            ' 新規追加
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.ShinkiTsuika.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.Tsuika)
            ' 特殊追加
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.Tsuika)
            ' -------------------------------------------------------------------------------------
            ' 修正
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.Shusei.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.Shusei)
            ' -------------------------------------------------------------------------------------
            ' 住基異動者追加
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.JukiIdoshaTsuika.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.JukiIdoshaTsuika)
            ' -------------------------------------------------------------------------------------
            ' 削除（回復可）
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.RonriSakujo.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.RonriSakujo)
            ' -------------------------------------------------------------------------------------
            ' 削除回復
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.Kaifuku)
            ' -------------------------------------------------------------------------------------
            ' 誤謬修正
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.TokushuShusei.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.GobyuShusei)
            ' -------------------------------------------------------------------------------------
            ' 削除（回復不可）
            m_csJutogaiJiyuCDConvertTable.Add( _
                ABAtenaShoriJiyuType.ButsuriSakujo.GetHashCode.ToString("00"), _
                ABJikiAtenaShoriJiyuType.ButsuriSakujo)
            ' -------------------------------------------------------------------------------------

        Catch csExp As Exception
            Throw
        End Try

    End Sub

#End Region

#Region "異動事由コード取得"

    ''' <summary>
    ''' 異動事由コード取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>異動事由コード</returns>
    ''' <remarks></remarks>
    Private Function GetIdoJiyuCD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const JUMINJUTOGAIKB_JUMIN As String = "1"
        Const JUMINJUTOGAIKB_JUTOGAI As String = "2"

        Dim strResult As String = String.Empty
        Dim csIdoJiyuCDConvertTable As Hashtable

        Try

            ' 変換テーブルの決定
            Select Case cParam.m_strJuminJutogaiKb
                Case JUMINJUTOGAIKB_JUMIN
                    '* 履歴番号 000003 2015/04/13 修正開始
                    ' 住登内のコード体系がReamsと同値のため、変換は不要となった。
                    'csIdoJiyuCDConvertTable = m_csJutonaiJiyuCDConvertTable
                    '* 履歴番号 000004 2015/05/07 修正開始
                    'Return cParam.m_strCkinJiyuCd
                    '* 履歴番号 000006 2015/07/07 修正開始
                    ' 規定値以外をString.Emptyとするため、
                    ' コード体系を見直した上でコード変換ロジックを復活させる。
                    'Return cParam.m_strCkinJiyuCd.Trim
                    csIdoJiyuCDConvertTable = m_csJutonaiJiyuCDConvertTable
                    '* 履歴番号 000006 2015/07/07 修正終了
                    '* 履歴番号 000004 2015/05/07 修正終了
                    '* 履歴番号 000003 2015/04/13 修正終了
                Case JUMINJUTOGAIKB_JUTOGAI
                    csIdoJiyuCDConvertTable = m_csJutogaiJiyuCDConvertTable
                Case Else
                    Return String.Empty
            End Select

            ' コード変換処理
            If (csIdoJiyuCDConvertTable.ContainsKey(cParam.m_strCkinJiyuCd) = True) Then
                strResult = csIdoJiyuCDConvertTable.Item(cParam.m_strCkinJiyuCd).GetHashCode.ToString("00")
            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "更新日時取得"

    ''' <summary>
    ''' 更新日時取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>更新日時</returns>
    ''' <remarks></remarks>
    Private Function GetKoshinNichiji(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try

            If (cParam.m_strMyNumberJuminCD.Trim.RLength > 0) Then
                strResult = cParam.m_strMyNumberKoshinNichiji.Trim
            Else
                '* 履歴番号 000002 2015/02/09 修正開始
                ' 宛名マスタの更新日時
                ' 宛名累積マスタの処理日時
                'strResult = cParam.m_strKoshinNichiji.Trim
                strResult = cParam.m_strShoriNichiji.Trim
                '* 履歴番号 000002 2015/02/09 修正終了
            End If

            '* 履歴番号 000006 2015/07/07 追加開始
            '* 履歴番号 000007 2015/07/15 修正開始
            'If (strResult.Trim.Length > 0) Then
            '    ' noop
            'Else
            '    ' 値が存在しない場合は、システム日時を設定する。
            '    strResult = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")
            'End If
            strResult = Me.CheckDateTime(cParam.m_strJuminCd, strResult)
            '* 履歴番号 000007 2015/07/15 修正終了
            '* 履歴番号 000006 2015/07/07 追加終了

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "備考編集"

    ''' <summary>
    ''' 備考編集
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>備考</returns>
    ''' <remarks></remarks>
    Private Function GetBiko(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const SHISAN_TOCHI As String = "土"
        Const SHISAN_KAOKU As String = "家"
        Const SHISAN_SHOKYAKU As String = "償"
        Const SHISAN_FUKA As String = "賦"
        Const MINZEI_KAZEITAISHOSHA As String = "住"
        Const MINZEI_JIMUSHO As String = "住事"
        Const HOJIN As String = "法"
        Const KEIJI_SHOYUSHA As String = "軽所"
        Const KEIJI_SHIYOSHA As String = "軽使"
        Const SHUNO_GENNEN As String = "収現"
        Const SHUNO_KANEN As String = "収過"
        Const KOZA As String = "口"
        Const KANINOFUSHO As String = "簡"
        Const KOKUHO As String = "国"
        Const JITE_JUKYUSHA As String = "児受"
        Const JITE_JIDO As String = "児児"
        Const FLG_ON As String = "1"
        Const SEPALATER As String = "／"

        Dim strResult As String = String.Empty
        Dim cBikoList As New List(Of String)

        Try

            With cParam

                If (.m_strShisan_Tochi = FLG_ON) Then
                    cBikoList.Add(SHISAN_TOCHI)
                End If

                If (.m_strShisan_Kaoku = FLG_ON) Then
                    cBikoList.Add(SHISAN_KAOKU)
                End If

                If (.m_strShisan_Shokyaku = FLG_ON) Then
                    cBikoList.Add(SHISAN_SHOKYAKU)
                End If

                If (.m_strShisan_Fuka = FLG_ON) Then
                    cBikoList.Add(SHISAN_FUKA)
                End If

                If (.m_strMinzei_Kazei = FLG_ON) Then
                    cBikoList.Add(MINZEI_KAZEITAISHOSHA)
                End If

                If (.m_strMinzei_Jimusho = FLG_ON) Then
                    cBikoList.Add(MINZEI_JIMUSHO)
                End If

                If (.m_strHojin = FLG_ON) Then
                    cBikoList.Add(HOJIN)
                End If

                If (.m_strKeiji_Shiyosha = FLG_ON) Then
                    cBikoList.Add(KEIJI_SHIYOSHA)
                End If

                If (.m_strKeiji_Shoyusha = FLG_ON) Then
                    cBikoList.Add(KEIJI_SHOYUSHA)
                End If

                If (.m_strShuno_Gennen = FLG_ON) Then
                    cBikoList.Add(SHUNO_GENNEN)
                End If

                If (.m_strShuno_Kanen = FLG_ON) Then
                    cBikoList.Add(SHUNO_KANEN)
                End If

                If (.m_strKoza = FLG_ON) Then
                    cBikoList.Add(KOZA)
                End If

                If (.m_strKaniNofu = FLG_ON) Then
                    cBikoList.Add(KANINOFUSHO)
                End If

                If (.m_strKokuho = FLG_ON) Then
                    cBikoList.Add(KOKUHO)
                End If

                If (.m_strJite_Jukyusha = FLG_ON) Then
                    cBikoList.Add(JITE_JUKYUSHA)
                End If

                If (.m_strJite_Jido = FLG_ON) Then
                    cBikoList.Add(JITE_JIDO)
                End If

            End With

            strResult = String.Join(SEPALATER, cBikoList.ToArray())

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "個人番号取得"

    '* 履歴番号 000001 2015/01/08 削除開始
    '''' <summary>
    '''' 個人番号取得
    '''' </summary>
    '''' <param name="cParam">編集パラメーター</param>
    '''' <returns>個人番号</returns>
    '''' <remarks></remarks>
    'Private Function GetKojinBango(ByVal cParam As ABBSRenkeiPrmXClass) As String

    '    Dim strResult As String = String.Empty

    '    Try

    '        If (cParam.m_strMyNumber.Trim.Length = ABConstClass.MYNUMBER.LENGTH.KOJIN) Then
    '            strResult = cParam.m_strMyNumber.Trim
    '        Else
    '            strResult = String.Empty
    '        End If

    '    Catch csExp As Exception
    '        Throw
    '    End Try

    '    Return strResult

    'End Function
    '* 履歴番号 000001 2015/01/08 削除終了

#End Region

#Region "直近区分取得"

    ''' <summary>
    ''' 直近区分取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>直近区分</returns>
    ''' <remarks></remarks>
    Private Function GetCkinKB(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const CKIN As String = "0"
        Const RRK As String = "1"

        Dim strResult As String = String.Empty

        Try

            Select Case cParam.m_strMyNumberCkinKB
                Case ABMyNumberEntity.DEFAULT.CKINKB.CKIN
                    strResult = CKIN
                Case ABMyNumberEntity.DEFAULT.CKINKB.RRK
                    strResult = RRK
                Case Else
                    strResult = String.Empty
            End Select

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "法人番号取得"

    '* 履歴番号 000001 2015/01/08 削除開始
    '''' <summary>
    '''' 法人番号取得
    '''' </summary>
    '''' <param name="cParam">編集パラメーター</param>
    '''' <returns>法人番号</returns>
    '''' <remarks></remarks>
    'Private Function GetHojinBango(ByVal cParam As ABBSRenkeiPrmXClass) As String

    '    Dim strResult As String = String.Empty

    '    Try

    '        If (cParam.m_strMyNumber.Trim.Length = ABConstClass.MYNUMBER.LENGTH.HOJIN) Then
    '            strResult = cParam.m_strMyNumber.Trim
    '        Else
    '            strResult = String.Empty
    '        End If

    '    Catch csExp As Exception
    '        Throw
    '    End Try

    '    Return strResult

    'End Function
    '* 履歴番号 000001 2015/01/08 削除終了

#End Region

    '* 履歴番号 000001 2015/01/08 追加開始
#Region "市町村コード取得"

    ''' <summary>
    ''' 市町村コード取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>市町村コード</returns>
    ''' <remarks></remarks>
    Private Function GetShichosonCD(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Dim strResult As String = String.Empty

        Try
            '* 履歴番号 000010 2016/06/10 修正開始
            'Select Case cParam.m_strKannaiKangaiKb
            '    Case ABConstClass.KANNAIKB
            '        strResult = m_strShichosonCD5
            '    Case Else
            '        If (cParam.m_strJushoCd.Trim.Length > 5) Then
            '            strResult = cParam.m_strJushoCd.Trim.Substring(0, 5).Trim
            '        Else
            '            strResult = cParam.m_strJushoCd.Trim
            '        End If
            'End Select
            '広域の場合は、地区コード3を設定する。
            If CheckKoiki() Then

                '* 履歴番号 000012 2017/05/23 修正開始
                'strResult = cParam.m_strChikuCd3.Trim
                If (cParam.m_strChikuCd3.Trim.RLength > 5) Then
                    strResult = cParam.m_strChikuCd3.Trim.RSubstring(0, 5).Trim
                Else
                    strResult = cParam.m_strChikuCd3.Trim
                End If
                '* 履歴番号 000012 2017/05/23 修正終了

            Else
                Select Case cParam.m_strKannaiKangaiKb

                    Case ABConstClass.KANNAIKB

                        strResult = m_strShichosonCD5

                    Case Else

                        If (cParam.m_strJushoCd.Trim.RLength > 5) Then
                            strResult = cParam.m_strJushoCd.Trim.RSubstring(0, 5).Trim
                        Else
                            strResult = cParam.m_strJushoCd.Trim
                        End If

                End Select
            End If
            '* 履歴番号 000010 2016/06/10 修正終了
        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "注意喚起フラグ取得"

    ''' <summary>
    ''' 注意喚起フラグ取得
    ''' </summary>
    ''' <param name="cParam">編集パラメーター</param>
    ''' <returns>注意喚起フラグ</returns>
    ''' <remarks></remarks>
    Private Function GetChuuiKankiFG(ByVal cParam As ABBSRenkeiPrmXClass) As String

        Const SEIGYOKBN_ON As String = "1"
        Const SEIGYOKBN_OFF As String = "0"
        Dim strResult As String = String.Empty

        Try

            If (cParam.m_strKojinSeigyoKbn.Trim.RLength > 0) Then
                strResult = SEIGYOKBN_ON
            Else
                strResult = SEIGYOKBN_OFF
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region
    '* 履歴番号 000001 2015/01/08 追加終了

    '* 履歴番号 000005 2015/06/09 追加開始
#Region "全角化処理"

    ''' <summary>
    ''' 全角化処理
    ''' </summary>
    ''' <param name="cRenkeiData">連携データ</param>
    ''' <remarks></remarks>
    Private Function ToWide(ByRef cRenkeiData As ABBSRenkeiRetXClass) As ABBSRenkeiRetXClass

        Try

            With cRenkeiData


                ' -----------------------------------------------------------------------------------------------------
                ' 漢字氏名
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strKanjiShimei = UFStringClass.ConvertNarrowToWide(.m_strKanjiShimei)
                .m_strKanjiShimei = ConvertNarrowToWideWrap(.m_strKanjiShimei)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 外国人氏名
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strFrnShimei = UFStringClass.ConvertNarrowToWide(.m_strFrnShimei)
                .m_strFrnShimei = ConvertNarrowToWideWrap(.m_strFrnShimei)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 外国人併記名
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strHeikimei = UFStringClass.ConvertNarrowToWide(.m_strHeikimei)
                .m_strHeikimei = ConvertNarrowToWideWrap(.m_strHeikimei)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 通称名
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strTsushomei = UFStringClass.ConvertNarrowToWide(.m_strTsushomei)
                .m_strTsushomei = ConvertNarrowToWideWrap(.m_strTsushomei)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 国籍名
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strKokuseki = UFStringClass.ConvertNarrowToWide(.m_strKokuseki)
                .m_strKokuseki = ConvertNarrowToWideWrap(.m_strKokuseki)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 住所
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strJusho = UFStringClass.ConvertNarrowToWide(.m_strJusho)
                .m_strJusho = ConvertNarrowToWideWrap(.m_strJusho)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 番地
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strBanchi = UFStringClass.ConvertNarrowToWide(.m_strBanchi)
                .m_strBanchi = ConvertNarrowToWideWrap(.m_strBanchi)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 方書
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strKatagaki = UFStringClass.ConvertNarrowToWide(.m_strKatagaki)
                .m_strKatagaki = ConvertNarrowToWideWrap(.m_strKatagaki)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------
                ' 備考
                '* 履歴番号 000009 2015/11/13 修正開始
                '.m_strBiko = UFStringClass.ConvertNarrowToWide(.m_strBiko)
                .m_strBiko = ConvertNarrowToWideWrap(.m_strBiko)
                '* 履歴番号 000009 2015/11/13 修正終了
                ' -----------------------------------------------------------------------------------------------------

            End With


        Catch csExp As Exception
            Throw
        End Try

        Return cRenkeiData

    End Function

#End Region
    '* 履歴番号 000005 2015/06/09 追加終了

    '* 履歴番号 000009 2015/11/13 追加開始
#Region "ConvertNarrowToWideWrap"

    ''' <summary>
    ''' 全角変換（.NET Frameworkの挙動を補正）
    ''' </summary>
    ''' <param name="strValue">対象文字列</param>
    ''' <returns>変換後文字列</returns>
    ''' <remarks>
    ''' Windows7以降OSにてStrConvを使用すると
    ''' 対象文字列に単独の濁点、半濁点が含まれる場合半角の "?" が返信されてしまう。
    ''' 全角のみを許容している項目に半角文字が混入してしまうため、
    ''' 代替え文字列 "●" に置換した上で連携することとする。
    ''' </remarks>
    Private Function ConvertNarrowToWideWrap(ByVal strValue As String) As String

        Const ERROR_STRING As String = "?"c
        Const REPLACE_STRING As String = "●"c

        Dim strResult As String = String.Empty

        Try

            strResult = UFStringClass.ConvertNarrowToWide(strValue)

            If (strResult.RIndexOf(ERROR_STRING) < 0) Then
                ' noop
            Else
                strResult = strResult.Replace(ERROR_STRING, REPLACE_STRING)
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region
    '* 履歴番号 000009 2015/11/13 追加終了

    '* 履歴番号 000007 2015/07/15 追加開始
#Region "登録日時チェック処理"

    ''' <summary>
    ''' 登録日時チェック処理
    ''' </summary>
    ''' <param name="strJuminCD">住民コード</param>
    ''' <param name="strTorokuNichiji">登録日時</param>
    ''' <returns>登録日時（チェック＆編集後）</returns>
    ''' <remarks></remarks>
    Private Function CheckDateTime( _
        ByVal strJuminCD As String, _
        ByVal strTorokuNichiji As String) As String

        Dim strResult As String = String.Empty
        Dim strYear As String = String.Empty
        Dim strMonth As String = String.Empty
        Dim strDay As String = String.Empty
        Dim strHour As String = String.Empty
        Dim strMinute As String = String.Empty
        Dim strSecond As String = String.Empty
        Dim strMilliSecond As String = String.Empty
        Dim csDateTime As DateTime

        Const DATE_TIME_MAX_LENGTH As Integer = 17
        Const HOUR_MAX As String = "23"
        Const MINUTE_MAX As String = "59"
        Const SECOND_MAX As String = "59"
        Const DATE_SEPARATOR As String = "/"
        Const DATE_TIME_SEPARATOR As String = " "
        Const TIME_SEPARATOR As String = ":"
        Const MSEC_SEPARATOR As String = "."

        Try

            strResult = strTorokuNichiji.Trim

            ' 歴上日になるように登録日時を整備する。
            If (strResult.RLength = DATE_TIME_MAX_LENGTH _
                AndAlso UFStringClass.CheckNumber(strResult) = True) Then

                strResult = strResult.RPadRight(17, "0"c)
                strYear = strResult.RSubstring(0, 4).Trim.RPadLeft(4, "0"c)
                strMonth = strResult.RSubstring(4, 2).Trim.RPadLeft(2, "0"c)
                strDay = strResult.RSubstring(6, 2).Trim.RPadLeft(2, "0"c)
                strHour = strResult.RSubstring(8, 2).Trim.RPadLeft(2, "0"c)
                strMinute = strResult.RSubstring(10, 2).Trim.RPadLeft(2, "0"c)
                strSecond = strResult.RSubstring(12, 2).Trim.RPadLeft(2, "0"c)
                strMilliSecond = strResult.RSubstring(14, 3).Trim.RPadLeft(3, "0"c)

                If (HOUR_MAX < strHour) Then
                    strHour = HOUR_MAX
                Else
                    ' noop
                End If

                If (MINUTE_MAX < strMinute) Then
                    strMinute = MINUTE_MAX
                Else
                    ' noop
                End If

                If (SECOND_MAX < strSecond) Then
                    strSecond = SECOND_MAX
                Else
                    ' noop
                End If

                If (DateTime.TryParse(String.Concat(
                                            String.Join(DATE_SEPARATOR, New String() {strYear, strMonth, strDay}),
                                            DATE_TIME_SEPARATOR,
                                            String.Join(TIME_SEPARATOR, New String() {strHour, strMinute, strSecond}),
                                            MSEC_SEPARATOR, strMilliSecond), csDateTime) = True) Then
                    ' noop
                Else
                    csDateTime = m_cfRdbClass.GetSystemDate
                End If

            Else
                csDateTime = m_cfRdbClass.GetSystemDate
            End If

            ' 住民コード単位にブレイクする。
            If (m_strBeforeJuminCD = strJuminCD) Then
                ' noop
            Else
                m_strBeforeJuminCD = strJuminCD
                m_csTorokuNichijiList.Clear()
            End If

            ' 登録日時が一意となるように１ミリ秒ずつ加算していく。
            Do

                strResult = csDateTime.ToString("yyyyMMddHHmmssfff")
                If (m_csTorokuNichijiList.Contains(strResult) = True) Then
                    csDateTime = csDateTime.AddMilliseconds(1)
                Else
                    m_csTorokuNichijiList.Add(strResult)
                    Exit Do
                End If

            Loop

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region
    '* 履歴番号 000007 2015/07/15 追加終了

    '* 履歴番号 000008 2015/09/29 追加開始
#Region "日付チェック処理"

    ''' <summary>
    ''' 日付チェック処理
    ''' </summary>
    ''' <param name="strDateValue">対象日付</param>
    ''' <returns>変換後日付</returns>
    ''' <remarks></remarks>
    Private Function CheckDate( _
        ByVal strDateValue As String) As String

        Dim strResult As String = String.Empty

        Try

            m_cfDate.p_strDateValue = strDateValue
            If (m_cfDate.CheckDate() = True) Then
                strResult = m_cfDate.p_strSeirekiYMD
            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region

#Region "郵便番号チェック処理"

    ''' <summary>
    ''' 郵便番号チェック処理
    ''' </summary>
    ''' <param name="strZIPCode">対象郵便番号</param>
    ''' <returns>変換後郵便番号</returns>
    ''' <remarks></remarks>
    Private Function CheckZIPCode( _
        ByVal strZIPCode As String) As String

        Dim strResult As String = String.Empty

        Try

            ' 半角数字で3桁、5桁、7桁のいずれかで構成された値の場合のみ設定する。
            If (Regex.IsMatch(strZIPCode, "^(\d{3}|\d{5}|\d{7})$") = True) Then
                strResult = strZIPCode
            Else
                strResult = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return strResult

    End Function

#End Region
    '* 履歴番号 000008 2015/09/29 追加終了

    '* 履歴番号 000010 2016/06/10 追加開始
#Region "広域判定取得"

    ''' <summary>
    ''' 広域判定取得
    ''' </summary>
    ''' <returns>広域判定取得</returns>
    ''' <remarks></remarks>
    Private Function CheckKoiki() As Boolean
        Try
            ' 管理情報の取得 [35-40] 広域判定
            ' パラメーター値が1なら広域でTrueを返却する
            Return ISKOIKI = Me.GetKanriJoho(KEYINFO_35_40(KEY_INDEX.SHUKEY), KEYINFO_35_40(KEY_INDEX.SHIKIBETSUKEY))
        Catch csExp As Exception
            Throw
        End Try
    End Function

#End Region

#Region "システム識別コードの取得"

    ''' <summary>
    ''' システム識別コードの取得
    ''' </summary>
    ''' <returns>システム識別コード</returns>
    ''' <remarks></remarks>
    Private Function GetSystemShikibetsuCD() As String
        Dim strParameter As String

        Try
            ' 管理情報の取得 [35-41] テストモード判定
            strParameter = Me.GetKanriJoho(KEYINFO_35_41(KEY_INDEX.SHUKEY), KEYINFO_35_41(KEY_INDEX.SHIKIBETSUKEY))

            ' 取得値を判定
            If (strParameter <> String.Empty) Then
                Return strParameter
            End If

        Catch csExp As Exception
            Throw
        End Try
        'パラメーター値がない場合SYSTEM_SHIKIBETSUCDを返却する
        Return SYSTEM_SHIKIBETSUCD
    End Function

#End Region


#Region "管理情報取得"

    ''' <summary>
    ''' ABATENAKANRIJOHOから管理情報取得
    ''' </summary>
    ''' <returns>パラメータ値</returns>
    ''' <remarks></remarks>
    Private Function GetKanriJoho(ByVal strShukey As String, ByVal strShikibetsukey As String) As String

        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' 管理情報の取得
            csDataSet = m_cABAtenaKanriJohoB.GetKanriJohoHoshu(strShukey, strShikibetsukey)

            ' 取得件数を判定
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                ' 取得結果が1件以上の場合、パラメーターを取得
                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString
                ' パラメーター値を返却する
                Return strParameter.Trim
            End If

        Catch csExp As Exception
            Throw
        End Try
        'パラメーター値がないEmptyを返却する
        Return String.Empty

    End Function

#End Region
    '* 履歴番号 000010 2016/06/10 追加終了
#End Region

End Class
