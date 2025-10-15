// ************************************************************************************************
// * 業務名　　　　   宛名管理システム
// * 
// * クラス名　　     中間サーバーＢＳ連携ビジネスクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 作成日付　　　   2014/08/19
// *
// * 作成者　　　　   石合　亮
// * 
// * 著作権　　　　   (株) 電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2015/01/08   000001      即時連携廃止に伴う修正（石合）
// * 2015/02/09   000002      登録日時設定値修正（石合）
// * 2015/04/13   000003      中間サーバーＢＳ側仕様変更事項の反映（石合）
// * 2015/05/07   000004      CHAR項目の空白除去対応（石合）
// * 2015/06/09   000005      全角化対応（石合）
// * 2015/07/07   000006      規定値外対応（石合）
// * 2015/07/15   000007      更新日時規定値外対応（石合）
// * 2015/09/29   000008      日付項目、郵便番号規定値外対応（石合）
// * 2015/11/13   000009      全角化対応不具合対応（石合）
// * 2016/06/10   000010      広域対応(大澤汐)
// * 2016/10/19   000011      広域対応２(石合)
// * 2017/05/23   000012      構成市町村コード上５桁対応(石合)
// ************************************************************************************************

using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.reams.ab.publicmodule.library.businesscommon.ab001x;
// * 履歴番号 000008 2015/09/29 追加開始
using System.Text.RegularExpressions;

namespace Densan.Reams.AB.AB000BB
{
    // * 履歴番号 000008 2015/09/29 追加終了

    /// <summary>
/// 中間サーバーＢＳ連携ビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABBSRenkeiBClass
    {

        #region メンバー変数

        // メンバー変数
        private UFLogClass m_cfLogClass;                                      // ログ出力クラス
        private UFControlData m_cfControlData;                                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                      // ＲＤＢクラス

        private USSCityInfoClass m_cuCityInfo;                                // 市町村情報
                                                                              // * 履歴番号 000001 2015/01/08 追加開始
        private string m_strShichosonCD5;                                     // 市町村コード5桁（管内）
                                                                              // * 履歴番号 000001 2015/01/08 追加終了
        private string m_strShichosonMeisho;                                  // 市町村名称（管内）
        private ABHojinMeishoBClass m_cABHojinMeishoB;                        // 法人名称編集ビジネスクラス

        private Hashtable m_csJutonaiJiyuCDConvertTable;                      // 異動事由コード変換テーブル（住登内）
        private Hashtable m_csJutogaiJiyuCDConvertTable;                      // 異動事由コード変換テーブル（住登内）

        // * 履歴番号 000001 2015/01/08 削除開始
        // * 履歴番号 000010 2016/06/10 修正開始
        private ABAtenaKanriJohoBClass m_cABAtenaKanriJohoB;                  // 宛名管理情報ビジネスクラス
                                                                              // * 履歴番号 000010 2016/06/10 修正終了
                                                                              // Private m_blnIsExecRenkei As Boolean                                    ' 中間サーバーＢＳ連携有無
                                                                              // * 履歴番号 000001 2015/01/08 削除終了

        // * 履歴番号 000007 2015/07/15 追加開始
        private string m_strBeforeJuminCD;                                    // 前処理住民コード
        private List<string> m_csTorokuNichijiList;                        // 登録日時リスト
                                                                           // * 履歴番号 000007 2015/07/15 追加終了

        // * 履歴番号 000008 2015/09/29 追加開始
        private UFDateClass m_cfDate;                                         // 日付編集クラス
                                                                              // * 履歴番号 000008 2015/09/29 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABBSRenkeiBClass";            // クラス名

        // * 履歴番号 000001 2015/01/08 削除開始
        // ' 管理情報キー情報
        // Private ReadOnly KEYINFO_35_21() As String = {"35", "21"}               ' 管理情報キー情報.中間サーバーＢＳ連携：連携開始日
        // * 履歴番号 000010 2016/06/10 修正開始
        // ' キーインデックス
        private enum KEY_INDEX
        {
            SHUKEY = 0,                  // 種別キー
            SHIKIBETSUKEY               // 識別キー
        }
        // * 履歴番号 000010 2016/06/10 修正終了
        // * 履歴番号 000001 2015/01/08 削除終了

        // * 履歴番号 000010 2016/06/10 追加開始
        // ' 管理情報キー情報
        private readonly string[] KEYINFO_35_40 = new string[] { "35", "40" };               // 管理情報キー情報.市町村コード：広域かどうか
        private readonly string[] KEYINFO_35_41 = new string[] { "35", "41" };               // 管理情報キー情報.送信時のシステム識別コード：テストモードかどうか
                                                                                             // * 履歴番号 000010 2016/06/10 追加終了


        // 文字数
        private class LENGTH
        {
            public const int SHIMEI_KOMOKU = 120;
            public const int KANJISHIMEI = SHIMEI_KOMOKU;                 // 漢字氏名
            public const int KANASHIMEI = SHIMEI_KOMOKU;                  // カナ氏名
            public const int FRNSHIMEI = SHIMEI_KOMOKU;                   // 外国人氏名
            public const int FRNKANASHIMEI = SHIMEI_KOMOKU;               // 外国人カナ名
            public const int HEIKIMEI = SHIMEI_KOMOKU;                    // 併記名
            public const int TSUSHOMEI = SHIMEI_KOMOKU;                   // 通称名
            public const int KANATSUSHOMEI = SHIMEI_KOMOKU;               // カナ通称名
            public const int JUSHO = 50;                                  // 住所
        }

        // 宛名異動事由コード
        private enum ABAtenaShoriJiyuType
        {
            RonriSakujo = 1,
            ShinkiTsuika = 10,
            JukiIdoshaTsuika = 11,
            Shusei = 12,
            Kaifuku = 15,
            TokushuTsuika = 91,
            TokushuShusei = 92,
            ButsuriSakujo = 93
        }

        // 次期宛名異動事由コード
        // * 履歴番号 000006 2015/07/07 修正開始
        // Private Enum ABJikiAtenaShoriJiyuType
        // Tsuika = 10
        // Shusei = 11
        // JukiIdoshaTsuika = 12
        // RonriSakujo = 13
        // Kaifuku = 14
        // GobyuShusei = 20
        // ButsuriSakujo = 22
        // JuminShubetsuHenko = 23
        // End Enum
        private enum ABJikiAtenaShoriJiyuType
        {
            Tsuika = 10,
            Shusei = 11,
            JukiIdoshaTsuika = 12,
            RonriSakujo = 13,
            Kaifuku = 14,
            GobyuShusei = 20,
            // RirekiShusei = 21               ' （未使用）
            ButsuriSakujo = 22
            // JuminShubetsuHenko = 23         ' （未使用）
            // YusenShimeiHenko = 24           ' （未使用）
        }
        // * 履歴番号 000006 2015/07/07 修正終了

        // 次期住基異動事由コード
        // * 履歴番号 000006 2015/07/07 修正開始
        // Private Enum ABJikiJukiShoriJiyuType
        // TokushuTsuika = 1
        // TokushuShusei = 2
        // TokushuSakujo = 3
        // JuminhyoCDShusei = 4
        // KojinBangoShusei = 5
        // RirekiShusei = 6
        // IdoTorikeshi = 7
        // Tennyu = 10
        // Dai30Jo46 = 11
        // TokureiTennyu = 12
        // Shussei = 13
        // Shuseki = 14
        // ShokkenKisai = 15
        // JushoSettei = 16
        // Fusoku5Jo = 17
        // Dai30Jo47 = 18
        // Tenshutsu = 20
        // TokureiTenshutsu = 21
        // Shibo = 22
        // ShissoSenkoku = 23
        // ShokkenShojo = 24
        // Tenkyo = 30
        // Kika = 31
        // KokusekiShutoku = 32
        // KokusekiSoshitsu = 33
        // NushiHenko = 40
        // SetaiBunri = 41
        // SetaiGappei = 42
        // SetaiHenko = 43
        // Dai30Jo48 = 44
        // Konin = 50
        // Rikon = 51
        // YoshiEngumi = 52
        // YoshiRien = 53
        // Tenseki = 54
        // Bunseki = 55
        // Nyuseki = 56
        // Ninchi = 57
        // KosekiSonota = 58
        // ShokkenShusei = 60
        // KosekiShusei = 61
        // TenshutsuTorikeshi = 62
        // ShokkenKaifuku = 63
        // TennyuTsuchiJuri = 64
        // JuminhyoCDKisai = 65
        // JuminhyoCDHenko = 66
        // KojinBangoKisai = 67
        // KojinBangoHenko = 68
        // KojinBangoShokkenShusei = 69
        // HomushoJukyochiTodoke = 70
        // TsushoKisai = 71
        // TsushoSakujo = 72
        // TokuEiShoShinsei = 73
        // TokuEiShoKofu = 74
        // KosekiTodokeGaiKonin = 75
        // JuminhyoKaisei = 80
        // UtsushiSeigyo = 81
        // HyojijunHenko = 82
        // KobetsuJikoShusei = 83
        // End Enum
        private enum ABJikiJukiShoriJiyuType
        {
            TokushuSakujo = 1,
            TokushuTsuika = 2,
            TokushuShusei = 3,
            JuminhyoCDShusei = 4,
            KojinBangoShusei = 5,
            KojinBangoKisai = 6,
            RirekiShusei = 8,
            // IdoTorikeshi = 9                    ' （未使用）
            Tennyu = 10,
            Shussei = 11,
            ShokkenKisai = 12,
            Kika = 13,
            KokusekiShutoku = 14,
            JushoSettei = 15,
            // Dai30Jo46 = 16                      ' （未使用）
            // TokureiTennyu = 17                  ' （未使用）
            // Fusoku5Jo = 18                      ' （未使用）
            // Dai30Jo47 = 19                      ' （未使用）
            Tenshutsu = 20,
            Shibo = 21,
            ShokkenShojo = 22,
            KokusekiSoshitsu = 23,
            ShissoSenkoku = 24,
            // TokureiTenshutsu = 25               ' （未使用）
            Tenkyo = 30,
            SetaiBunri = 31,
            SetaiGappei = 32,
            SetaiHenko = 33,
            // Dai30Jo48 = 34                      ' （未使用）
            NushiHenko = 40,
            ShokkenShusei = 41,
            KosekiShusei = 42,
            TenshutsuTorikeshi = 43,
            ShokkenKaifuku = 44,
            TennyuTsuchiJuri = 45,
            JuminhyoCDHenko = 46,
            JuminhyoCDKisai = 47,
            KojinBangoHenko = 48,
            KojinBangoShokkenShusei = 49,
            Konin = 50,
            Rikon = 51,
            YoshiEngumi = 52,
            YoshiRien = 53,
            Tenseki = 54,
            Bunseki = 55,
            Nyuseki = 56,
            Ninchi = 57,
            KosekiSonota = 58,
            // Shuseki = 59                        ' （未使用）
            JuminhyoKaisei = 60,
            UtsushiSeigyo = 61,
            HyojijunHenko = 62,
            KobetsuJikoShusei = 63
            // HomushoJukyochiTodoke = 70          ' （未使用）
            // TsushoKisai = 71                    ' （未使用）
            // TsushoSakujo = 72                   ' （未使用）
            // TokuEiKyokaShinsei = 73             ' （未使用）
            // TokuEiKyokaShinsaKekkaToroku = 74   ' （未使用）
            // TokuEiKyokaKofu = 75                ' （未使用）
            // TokuEiShoShinsei = 76               ' （未使用）
            // TokuEiShoShinsaKekkaToroku = 77     ' （未使用）
            // TokuEiShoKofu = 78                  ' （未使用）
            // KosekiTodokeGaiKonin = 79           ' （未使用）
        }
        // * 履歴番号 000006 2015/07/07 修正終了

        // * 履歴番号 000001 2015/01/08 追加開始
        // * 履歴番号 000003 2015/04/27 修正開始
        // Private Const SYSTEM_SHIKIBETSUCD As String = "000"
        private const string SYSTEM_SHIKIBETSUCD = "001";
        // * 履歴番号 000003 2015/04/27 修正終了
        // * 履歴番号 000001 2015/01/08 追加終了

        // * 履歴番号 000010 2016/06/10 追加開始
        private const string ISKOIKI = "1";
        // * 履歴番号 000010 2016/06/10 追加終了

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        public ABBSRenkeiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // 市町村情報の取得
            m_cuCityInfo = new USSCityInfoClass();
            m_cuCityInfo.GetCityInfo(m_cfControlData);
            // * 履歴番号 000001 2015/01/08 追加開始
            m_strShichosonCD5 = m_cuCityInfo.p_strShichosonCD(0).RSubstring(0, 5);
            // * 履歴番号 000001 2015/01/08 追加終了
            m_strShichosonMeisho = m_cuCityInfo.p_strShichosonMeisho(0).Trim;

            // 法人名称編集ビジネスクラスのインスタンス化
            m_cABHojinMeishoB = new ABHojinMeishoBClass(m_cfControlData, m_cfConfigDataClass);

            // 異動事由変換テーブルの設定
            SetIdoJiyuCD();

            // * 履歴番号 000001 2015/01/08 削除開始
            // ' 連携有無取得
            // m_blnIsExecRenkei = IsExecRenkei()
            // * 履歴番号 000001 2015/01/08 削除終了

            // * 履歴番号 000007 2015/07/15 追加開始
            m_strBeforeJuminCD = string.Empty;
            m_csTorokuNichijiList = new List<string>();
            // * 履歴番号 000007 2015/07/15 追加終了

            // * 履歴番号 000008 2015/09/29 追加開始
            m_cfDate = new UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero);
            // * 履歴番号 000008 2015/09/29 追加終了

            // * 履歴番号 000010 2016/06/10 追加開始
            // 宛名管理情報ビジネスクラスのインスタンス化
            m_cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
            // * 履歴番号 000010 2016/06/10 追加終了

        }

        #endregion

        #region メソッド

        // * 履歴番号 000001 2015/01/08 削除開始
        #region 【廃止】即時連携がなくなり、スケジューラーでの日次連携のみとなったため、コメントアウト

        // #Region "連携有無取得"

        // ''' <summary>
        // ''' 連携有無取得
        // ''' </summary>
        // ''' <returns>連携有無</returns>
        // ''' <remarks></remarks>
        // Private Function IsExecRenkei() As Boolean

        // Dim blnResult As Boolean
        // Dim csDataSet As DataSet
        // Dim strParameter As String
        // Dim strSystemDate As String

        // Try

        // ' 返信オブジェクトの初期化
        // blnResult = False

        // ' 宛名管理情報ビジネスクラスのインスタンス化
        // m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        // ' 管理情報の取得 [35-21] 中間サーバーＢＳ連携：連携開始日
        // csDataSet = m_cABAtenaKanriJohoB.GetKanriJohoHoshu(KEYINFO_35_21(KEY_INDEX.SHUKEY), KEYINFO_35_21(KEY_INDEX.SHIKIBETSUKEY))

        // ' 取得件数を判定
        // If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

        // ' 取得結果が1件以上の場合、パラメーターを取得
        // strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

        // ' パラメーター値を判定
        // If (strParameter.Trim.Length > 0) Then

        // ' システム日付を取得
        // strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")

        // ' システム日付 ＜ 連携開始日　 -> 連携しない
        // ' 連携開始日　 ≦ システム日付 -> 連携する
        // If (strSystemDate < strParameter) Then
        // blnResult = False
        // Else
        // blnResult = True
        // End If

        // Else
        // ' パラメーター値なし -> 連携しない
        // blnResult = False
        // End If

        // Else
        // ' レコードなし -> 連携しない
        // blnResult = False
        // End If

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return blnResult

        // End Function

        // #End Region

        // #Region "中間サーバーＢＳ連携一連処理"

        // ''' <summary>
        // ''' 中間サーバーＢＳ連携一連処理
        // ''' </summary>
        // ''' <param name="strJuminCD">対象者住民コード</param>
        // ''' <remarks></remarks>
        // Public Sub ExecRenkei( _
        // ByVal strJuminCD As String)
        // Me.ExecRenkei(New String() {strJuminCD})
        // End Sub

        // ''' <summary>
        // ''' 中間サーバーＢＳ連携一連処理
        // ''' </summary>
        // ''' <param name="a_strJuminCD">対象者住民コード配列</param>
        // ''' <remarks></remarks>
        // Public Sub ExecRenkei( _
        // ByVal a_strJuminCD() As String)
        // Dim csJuminCD As ArrayList
        // csJuminCD = New ArrayList
        // For Each strJuminCD As String In a_strJuminCD
        // csJuminCD.Add(strJuminCD)
        // Next strJuminCD
        // Me.ExecRenkei(csJuminCD)
        // End Sub

        // ''' <summary>
        // ''' 中間サーバーＢＳ連携一連処理
        // ''' </summary>
        // ''' <param name="csJuminCD">対象者住民コードリスト</param>
        // ''' <remarks></remarks>
        // Public Sub ExecRenkei( _
        // ByVal csJuminCD As ArrayList)

        // Const JOB_ID As String = "ABJ96210"

        // Dim cuBatchReg As New USBBatchRegisterClass
        // Dim csDataSet As DataSet
        // Dim cfErrorClass As UFErrorClass
        // Dim cfErrorStruct As UFErrorStruct

        // Try

        // ' 連携有無を判定
        // If (m_blnIsExecRenkei = True) Then

        // ' バッチパラメーター取得
        // csDataSet = Me.GetBatchParameter

        // ' 住民コードパラメーター追加
        // csDataSet = AddJuminCDParameter(csDataSet, csJuminCD)

        // ' バッチ登録クラスのインスタンス化
        // cuBatchReg = New USBBatchRegisterClass()

        // ' バッチ登録の実行（バッチ登録時のエラーはExceptionがThrowされるため、ステータスの判定は行わない。）
        // cuBatchReg.RegistBatch(m_cfControlData, ABConstClass.THIS_BUSINESSID, JOB_ID, csDataSet, USBBatchRegisterClass.USLBangoLog.MIX)

        // Else
        // ' noop
        // End If

        // Catch csExp As Exception

        // cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
        // cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003304)
        // Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode, csExp)

        // End Try

        // End Sub

        // #End Region

        // #Region "バッチパラメーター取得"

        // ''' <summary>
        // ''' バッチパラメーター取得
        // ''' </summary>
        // ''' <returns>バッチパラメーター</returns>
        // ''' <remarks></remarks>
        // Private Function GetBatchParameter() As DataSet

        // Dim csDataSet As DataSet
        // Dim csDataTalbe As DataTable
        // Dim csDataRow As DataRow

        // Try

        // csDataSet = New DataSet()
        // csDataTalbe = csDataSet.Tables.Add(ABBPBSRenkei.TABLE_NAME)

        // With csDataTalbe

        // .Columns.Add(ABBPBSRenkei.CHUSHUTSUKBN)
        // .Columns.Add(ABBPBSRenkei.CHUSHUTSUJOKEN)

        // End With

        // csDataRow = csDataTalbe.NewRow
        // With csDataRow
        // .BeginEdit()
        // .Item(ABBPBSRenkei.CHUSHUTSUKBN) = ABBPBSRenkei.DEFALUT.CHUSHUTSUKBN.IDOBUN
        // .Item(ABBPBSRenkei.CHUSHUTSUJOKEN) = ABBPBSRenkei.DEFALUT.CHUSHUTSUJOKEN.JUMINCD
        // .EndEdit()
        // End With
        // csDataTalbe.Rows.Add(csDataRow)

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return csDataSet

        // End Function

        // #End Region

        // #Region "住民コードパラメーター追加"

        // ''' <summary>
        // ''' 住民コードパラメーター追加
        // ''' </summary>
        // ''' <param name="csDataSet">バッチパラメーター</param>
        // ''' <param name="csJuminCD">住民コードリスト</param>
        // ''' <returns>バッチパラメーター</returns>
        // ''' <remarks></remarks>
        // Private Function AddJuminCDParameter( _
        // ByVal csDataSet As DataSet, _
        // ByVal csJuminCD As ArrayList) As DataSet

        // Dim csDataTalbe As DataTable
        // Dim csDataRow As DataRow

        // Try

        // csDataTalbe = csDataSet.Tables.Add(ABBPJuminCD.TABLE_NAME)

        // With csDataTalbe

        // .Columns.Add(ABBPJuminCD.JUMINCD)

        // End With

        // For Each strJuminCD As String In csJuminCD

        // csDataRow = csDataTalbe.NewRow
        // With csDataRow
        // .BeginEdit()
        // .Item(ABBPJuminCD.JUMINCD) = strJuminCD
        // .EndEdit()
        // End With
        // csDataTalbe.Rows.Add(csDataRow)

        // Next strJuminCD

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return csDataSet

        // End Function

        // #End Region

        #endregion
        // * 履歴番号 000001 2015/01/08 削除終了

        #region 連携データ編集処理

        /// <summary>
    /// 連携データ編集処理（バッチ用の入り口）
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>編集結果</returns>
    /// <remarks></remarks>
        public ABBSRenkeiRetXClass EditRenkeiDataForBatch(ABBSRenkeiPrmXClass cParam)
        {
            return EditData(cParam);
        }

        /// <summary>
    /// 連携データ編集処理
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>編集結果</returns>
    /// <remarks></remarks>
        private ABBSRenkeiRetXClass EditData(ABBSRenkeiPrmXClass cParam)
        {

            ABBSRenkeiRetXClass cResult;

            try
            {

                // 返信オブジェクトのインスタンス化
                cResult = new ABBSRenkeiRetXClass();

                {
                    ref var withBlock = ref cResult;

                    // * 履歴番号 000001 2015/01/08 追加開始
                    // -----------------------------------------------------------------------------------------------------
                    // システム識別コード
                    // * 履歴番号 000010 2016/06/10 修正開始
                    // .m_strSystemShikibetsuCD = SYSTEM_SHIKIBETSUCD
                    withBlock.m_strSystemShikibetsuCD = GetSystemShikibetsuCD();
                    // * 履歴番号 000010 2016/06/10 修正終了
                    // * 履歴番号 000001 2015/01/08 追加終了
                    // -----------------------------------------------------------------------------------------------------
                    // 識別コード
                    withBlock.m_strShikibetsuCD = cParam.m_strJuminCd.Trim.RPadLeft(15, '0');
                    // * 履歴番号 000001 2015/01/08 追加開始
                    // -----------------------------------------------------------------------------------------------------
                    // 登録日時
                    withBlock.m_strTorokuNichiji = GetKoshinNichiji(cParam);
                    // * 履歴番号 000001 2015/01/08 追加終了
                    // -----------------------------------------------------------------------------------------------------
                    // 住民種別コード
                    withBlock.m_strJuminShubetsuCD = GetJuminShubetsuCD(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 住民状態コード
                    withBlock.m_strJuminJotaiCD = GetJuminJotaiCD(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 漢字氏名
                    withBlock.m_strKanjiShimei = EditKanjiShimei(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // カナ氏名
                    withBlock.m_strKanaShimei = EditKanaShimei(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 外国人氏名項目
                    if (IsKojin(cParam) == true)
                    {
                        // -------------------------------------------------------------------------------------------------
                        // 外国人氏名
                        withBlock.m_strFrnShimei = this.Left(cParam.m_strFZYHongokumei, LENGTH.FRNSHIMEI);
                        // -------------------------------------------------------------------------------------------------
                        // 併記名
                        withBlock.m_strHeikimei = this.Left(cParam.m_strFZYKanjiHeikimei, LENGTH.HEIKIMEI);
                        // -------------------------------------------------------------------------------------------------
                        // 外国人カナ名
                        withBlock.m_strFrnKanaShimei = EditFrnKanaShimei(cParam);
                        // -------------------------------------------------------------------------------------------------
                        // 通称名
                        withBlock.m_strTsushomei = this.Left(cParam.m_strFZYKanjiTsushomei, LENGTH.TSUSHOMEI);
                        // -------------------------------------------------------------------------------------------------
                        // カナ通称名
                        withBlock.m_strKanaTsushomei = this.Left(cParam.m_strFZYKanaTsushomei, LENGTH.KANATSUSHOMEI);
                    }
                    // -------------------------------------------------------------------------------------------------
                    else
                    {
                        // -------------------------------------------------------------------------------------------------
                        // 外国人氏名
                        withBlock.m_strFrnShimei = string.Empty;
                        // -------------------------------------------------------------------------------------------------
                        // 併記名
                        withBlock.m_strHeikimei = string.Empty;
                        // -------------------------------------------------------------------------------------------------
                        // 外国人カナ名
                        withBlock.m_strFrnKanaShimei = string.Empty;
                        // -------------------------------------------------------------------------------------------------
                        // 通称名
                        withBlock.m_strTsushomei = string.Empty;
                        // -------------------------------------------------------------------------------------------------
                        // カナ通称名
                        withBlock.m_strKanaTsushomei = string.Empty;
                        // -------------------------------------------------------------------------------------------------
                    }
                    // -----------------------------------------------------------------------------------------------------
                    // 氏名利用区分
                    withBlock.m_strShimeiRiyoKB = GetShimeiRiyoKB(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 生年月日
                    // * 履歴番号 000003 2015/04/13 修正開始
                    // If (Me.IsKojin(cParam) = True) Then
                    // .m_strUmareYMD = cParam.m_strUmareYmd.Trim
                    // Else
                    // .m_strUmareYMD = String.Empty
                    // End If
                    // * 履歴番号 000008 2015/09/29 修正開始
                    // .m_strUmareYMD = GetUmareYMD(cParam)
                    withBlock.m_strUmareYMD = CheckDate(GetUmareYMD(cParam));
                    // * 履歴番号 000008 2015/09/29 修正終了
                    // * 履歴番号 000003 2015/04/13 修正終了
                    // -----------------------------------------------------------------------------------------------------
                    // 生年月日不詳区分
                    withBlock.m_strUmareFushoKBN = GetUmareFushoKBN(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 性別コード
                    withBlock.m_strSeibetsuCD = GetSeibetsuCD(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 国籍
                    if (IsKojin(cParam) == true)
                    {
                        withBlock.m_strKokuseki = cParam.m_strKokuseki;
                    }
                    else
                    {
                        withBlock.m_strKokuseki = string.Empty;
                    }
                    // -----------------------------------------------------------------------------------------------------
                    // 郵便番号
                    // * 履歴番号 000008 2015/09/29 修正開始
                    // .m_strYubinNo = cParam.m_strYubinNo.Trim
                    withBlock.m_strYubinNo = this.CheckZIPCode(cParam.m_strYubinNo.Trim);
                    // * 履歴番号 000008 2015/09/29 修正終了
                    // -----------------------------------------------------------------------------------------------------
                    // 住所
                    withBlock.m_strJusho = EditJusho(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 番地
                    withBlock.m_strBanchi = cParam.m_strBanchi;
                    // -----------------------------------------------------------------------------------------------------
                    // 方書
                    withBlock.m_strKatagaki = cParam.m_strKatagaki;
                    // -----------------------------------------------------------------------------------------------------
                    // 連絡先１
                    withBlock.m_strRenrakusaki1 = cParam.m_strRenrakusaki1;
                    // -----------------------------------------------------------------------------------------------------
                    // 連絡先２
                    withBlock.m_strRenrakusaki2 = cParam.m_strRenrakusaki2;
                    // -----------------------------------------------------------------------------------------------------
                    // 異動日
                    // * 履歴番号 000008 2015/09/29 修正開始
                    // .m_strIdoYMD = cParam.m_strCkinIdoYmd.Trim
                    withBlock.m_strIdoYMD = this.CheckDate(cParam.m_strCkinIdoYmd.Trim);
                    // * 履歴番号 000008 2015/09/29 修正終了
                    // -----------------------------------------------------------------------------------------------------
                    // 異動事由コード
                    withBlock.m_strIdoJiyuCD = GetIdoJiyuCD(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // * 履歴番号 000001 2015/01/08 削除開始
                    // ' 更新日時
                    // .m_strKoshinNichiji = Me.GetKoshinNichiji(cParam)
                    // * 履歴番号 000001 2015/01/08 削除終了
                    // -----------------------------------------------------------------------------------------------------
                    // 備考
                    withBlock.m_strBiko = GetBiko(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 個人番号
                    // * 履歴番号 000001 2015/01/08 修正開始
                    // .m_strKojinBango = Me.GetKojinBango(cParam)
                    withBlock.m_strKojinBango = cParam.m_strMyNumber.Trim;
                    // * 履歴番号 000001 2015/01/08 修正終了
                    // -----------------------------------------------------------------------------------------------------
                    // 直近区分
                    withBlock.m_strCkinKB = GetCkinKB(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // * 履歴番号 000001 2015/01/08 削除開始
                    // ' 法人番号
                    // .m_strHojinBango = Me.GetHojinBango(cParam)
                    // * 履歴番号 000001 2015/01/08 削除終了
                    // -----------------------------------------------------------------------------------------------------
                    // * 履歴番号 000001 2015/01/08 追加開始
                    // 市町村コード
                    withBlock.m_strShichosonCD = GetShichosonCD(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // 注意喚起フラグ
                    withBlock.m_strChuuiKankiFG = GetChuuiKankiFG(cParam);
                    // -----------------------------------------------------------------------------------------------------
                    // * 履歴番号 000001 2015/01/08 追加終了

                    // * 履歴番号 000005 2015/06/09 追加開始
                    // -----------------------------------------------------------------------------------------------------
                    // 全角項目に対して全角化を実施する。（イレギュラーデータに対する考慮。）
                    // 切り取り処理等の編集処理と処理順が前後しないように一律最後に全角化を実施する。
                    cResult = ToWide(ref cResult);
                    // -----------------------------------------------------------------------------------------------------
                    // * 履歴番号 000005 2015/06/09 追加終了

                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return cResult;

        }

        #endregion

        #region 個人判定

        /// <summary>
    /// 個人判定
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>判定結果</returns>
    /// <remarks></remarks>
        private bool IsKojin(ABBSRenkeiPrmXClass cParam)
        {
            return cParam.m_strAtenaDataKb == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || cParam.m_strAtenaDataKb == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN;
        }

        #endregion

        #region 法人判定

        /// <summary>
    /// 法人判定
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>判定結果</returns>
    /// <remarks></remarks>
        private bool IsHojin(ABBSRenkeiPrmXClass cParam)
        {
            return cParam.m_strAtenaDataKb == ABConstClass.ATENADATAKB_HOJIN;
        }

        #endregion

        #region 外国人判定

        /// <summary>
    /// 外国人判定
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>判定結果</returns>
    /// <remarks></remarks>
        private bool IsFrn(ABBSRenkeiPrmXClass cParam)
        {
            return (cParam.m_strAtenaDataKb == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || cParam.m_strAtenaDataKb == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) && cParam.m_strAtenaDataShu.StartsWith("2", StringComparison.CurrentCulture) == true;

        }

        #endregion

        #region 文字列切り取り

        /// <summary>
    /// 文字列切り取り
    /// </summary>
    /// <param name="strValue">対象文字列</param>
    /// <param name="intMaxLength">最大文字数</param>
    /// <returns>切り取り結果文字列</returns>
    /// <remarks></remarks>
        private string Left(string strValue, int intMaxLength)
        {
            string strResult = string.Empty;
            try
            {
                if (strValue is not null && strValue.RLength > intMaxLength)
                {
                    strResult = strValue.RSubstring(0, intMaxLength);
                }
                else
                {
                    strResult = strValue;
                }
            }
            catch (Exception csExp)
            {
                throw;
            }
            // * 履歴番号 000005 2015/06/09 修正開始
            // Return strResult.TrimEnd
            // TrimEndを廃止する。
            return strResult;
            // * 履歴番号 000005 2015/06/09 修正終了
        }

        #endregion

        #region 住民種別コード取得

        /// <summary>
    /// 住民種別コード取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>住民種別コード</returns>
    /// <remarks></remarks>
        private string GetJuminShubetsuCD(ABBSRenkeiPrmXClass cParam)
        {

            const string JUTONAI_JPN = "1";
            const string JUTONAI_FRN = "2";
            const string JUTOGAI_JPN = "3";
            const string JUTOGAI_FRN = "4";
            const string HOJIN = "5";
            const string KYOYU = "6";

            string strResult = string.Empty;

            try
            {

                switch (cParam.m_strAtenaDataKb)
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                        {
                            switch (cParam.m_strAtenaDataShu)
                            {
                                case var case1 when case1 == ABConstClass.JUMINSHU_NIHONJIN_JUMIN:
                                case var case2 when case2 == ABConstClass.JUMINSHU_NIHONJIN_SHOJO:
                                case var case3 when case3 == ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU:
                                case var case4 when case4 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                    {
                                        strResult = JUTONAI_JPN;
                                        break;
                                    }
                                case var case5 when case5 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                case var case6 when case6 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO:
                                case var case7 when case7 == ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU:
                                case var case8 when case8 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        strResult = JUTONAI_FRN;
                                        break;
                                    }

                                default:
                                    {
                                        // * 履歴番号 000006 2015/07/07 修正開始
                                        // 組み合わせが不正の場合、日本人とする。
                                        // strResult = String.Empty
                                        strResult = JUTONAI_JPN;
                                        break;
                                    }
                                    // * 履歴番号 000006 2015/07/07 修正終了
                            }

                            break;
                        }
                    case var case9 when case9 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                        {
                            switch (cParam.m_strAtenaDataShu)
                            {
                                case var case10 when case10 == ABConstClass.JUMINSHU_NIHONJIN_JUTOGAI:
                                case var case11 when case11 == ABConstClass.JUMINSHU_NIHONJIN_ETC:
                                case var case12 when case12 == ABConstClass.JUMINSHU_NIHONJIN_SHOJO:
                                case var case13 when case13 == ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU:
                                case var case14 when case14 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                    {
                                        strResult = JUTOGAI_JPN;
                                        break;
                                    }
                                case var case15 when case15 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                case var case16 when case16 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUTOGAI:
                                case var case17 when case17 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO:
                                case var case18 when case18 == ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU:
                                case var case19 when case19 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        strResult = JUTOGAI_FRN;
                                        break;
                                    }

                                default:
                                    {
                                        // * 履歴番号 000006 2015/07/07 修正開始
                                        // 組み合わせが不正の場合、日本人（住登外）とする。
                                        // strResult = String.Empty
                                        strResult = JUTOGAI_JPN;
                                        break;
                                    }
                                    // * 履歴番号 000006 2015/07/07 修正終了
                            }

                            break;
                        }
                    case var case20 when case20 == ABConstClass.ATENADATAKB_HOJIN:
                        {
                            strResult = HOJIN;
                            break;
                        }
                    case var case21 when case21 == ABConstClass.ATENADATAKB_KYOYU:
                        {
                            strResult = KYOYU;
                            break;
                        }

                    default:
                        {
                            strResult = string.Empty;
                            break;
                        }
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 住民状態コード取得

        /// <summary>
    /// 住民状態コード取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>住民状態コード</returns>
    /// <remarks></remarks>
        private string GetJuminJotaiCD(ABBSRenkeiPrmXClass cParam)
        {

            const string JUMIN = "1";
            const string JUTOGAI_KOJIN = "2";
            const string TENSHUTSU = "3";
            const string SHIBO = "4";
            const string SHOJO = "9";

            string strResult = string.Empty;

            try
            {

                switch (cParam.m_strAtenaDataKb)
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                        {
                            switch (cParam.m_strAtenaDataShu)
                            {
                                case var case1 when case1 == ABConstClass.JUMINSHU_NIHONJIN_JUMIN:
                                case var case2 when case2 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                    {
                                        strResult = JUMIN;
                                        break;
                                    }
                                case var case3 when case3 == ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU:
                                case var case4 when case4 == ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU:
                                    {
                                        strResult = TENSHUTSU;
                                        break;
                                    }
                                case var case5 when case5 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                case var case6 when case6 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        strResult = SHIBO;
                                        break;
                                    }
                                case var case7 when case7 == ABConstClass.JUMINSHU_NIHONJIN_SHOJO:
                                case var case8 when case8 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO:
                                    {
                                        strResult = SHOJO;
                                        break;
                                    }

                                default:
                                    {
                                        // * 履歴番号 000006 2015/07/07 修正開始
                                        // 組み合わせが不正の場合、消除者とする。
                                        // strResult = String.Empty
                                        strResult = SHOJO;
                                        break;
                                    }
                                    // * 履歴番号 000006 2015/07/07 修正終了
                            }

                            break;
                        }
                    case var case9 when case9 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                        {
                            switch (cParam.m_strAtenaDataShu)
                            {
                                case var case10 when case10 == ABConstClass.JUMINSHU_NIHONJIN_JUTOGAI:
                                case var case11 when case11 == ABConstClass.JUMINSHU_NIHONJIN_ETC:
                                case var case12 when case12 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                case var case13 when case13 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUTOGAI:
                                    {
                                        strResult = JUTOGAI_KOJIN;
                                        break;
                                    }
                                case var case14 when case14 == ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU:
                                case var case15 when case15 == ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU:
                                    {
                                        strResult = TENSHUTSU;
                                        break;
                                    }
                                case var case16 when case16 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                case var case17 when case17 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        strResult = SHIBO;
                                        break;
                                    }
                                case var case18 when case18 == ABConstClass.JUMINSHU_NIHONJIN_SHOJO:
                                case var case19 when case19 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHOJO:
                                    {
                                        strResult = SHOJO;
                                        break;
                                    }

                                default:
                                    {
                                        // * 履歴番号 000006 2015/07/07 修正開始
                                        // 組み合わせが不正の場合、住登外とする。
                                        // strResult = String.Empty
                                        strResult = JUTOGAI_KOJIN;
                                        break;
                                    }
                                    // * 履歴番号 000006 2015/07/07 修正終了
                            }

                            break;
                        }
                    case var case20 when case20 == ABConstClass.ATENADATAKB_HOJIN:
                        {
                            strResult = string.Empty;
                            break;
                        }
                    case var case21 when case21 == ABConstClass.ATENADATAKB_KYOYU:
                        {
                            strResult = string.Empty;
                            break;
                        }

                    default:
                        {
                            strResult = string.Empty;
                            break;
                        }
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 漢字氏名編集

        /// <summary>
    /// 漢字氏名編集
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>漢字氏名</returns>
    /// <remarks></remarks>
        private string EditKanjiShimei(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                // 宛名Getの編集に準拠する。
                if (IsHojin(cParam) == true)
                {
                    m_cABHojinMeishoB.p_strKeitaiFuyoKB = cParam.m_strHanyoKb1;
                    m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = cParam.m_strHanyoKb2;
                    m_cABHojinMeishoB.p_strKanjiHjnKeitai = cParam.m_strKanjiHjnKeitai;
                    m_cABHojinMeishoB.p_strKanjiMeisho1 = cParam.m_strKanjiMeisho1;
                    m_cABHojinMeishoB.p_strKanjiMeisho2 = cParam.m_strKanjiMeisho2;
                    strResult = m_cABHojinMeishoB.GetHojinMeisho();
                }
                else
                {
                    strResult = cParam.m_strKanjiMeisho1;
                }
                // Left内でTrimEndして設定することとする。
                strResult = Left(strResult, LENGTH.KANJISHIMEI);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region カナ氏名編集

        /// <summary>
    /// カナ氏名編集
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>カナ氏名</returns>
    /// <remarks></remarks>
        private string EditKanaShimei(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                // 宛名Getの編集に準拠する。
                if (IsHojin(cParam) == true)
                {
                    // * 履歴番号 000005 2015/06/09 追加開始
                    // ※カナ法人名のTrimEndは準拠する為なので放置する。
                    // * 履歴番号 000005 2015/06/09 追加終了
                    if (cParam.m_strKanaMeisho2.Trim.RLength > 0)
                    {
                        strResult = string.Concat(cParam.m_strKanaMeisho1.TrimEnd, ' ', cParam.m_strKanaMeisho2.TrimEnd);
                    }
                    else
                    {
                        strResult = cParam.m_strKanaMeisho1.TrimEnd;
                    }
                }
                else
                {
                    strResult = cParam.m_strKanaMeisho1;
                }
                // Left内でTrimEndして設定することとする。
                strResult = Left(strResult, LENGTH.KANASHIMEI);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 外国人カナ氏名編集

        /// <summary>
    /// 外国人カナ氏名編集
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>外国人カナ氏名</returns>
    /// <remarks></remarks>
        private string EditFrnKanaShimei(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                // 値有無判定は漢字項目で行う。
                if (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0)
                {
                    // 漢字併記名に値が存在する場合->カナ併記名を設定
                    strResult = cParam.m_strFZYKanaHeikimei;
                }
                else
                {
                    // 漢字併記名に値が存在しない場合->カナ本国名を設定
                    strResult = cParam.m_strFZYKanaHongokumei;
                }
                // Left内でTrimEndして設定することとする。
                strResult = Left(strResult, LENGTH.FRNKANASHIMEI);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 氏名利用区分取得

        /// <summary>
    /// 氏名利用区分取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>氏名利用区分</returns>
    /// <remarks></remarks>
        private string GetShimeiRiyoKB(ABBSRenkeiPrmXClass cParam)
        {

            const string TSUSHOMEI = "0";
            const string HEIKIMEI = "1";
            const string HONGOKUMEI = "2";

            string strResult = string.Empty;

            try
            {

                if (IsFrn(cParam) == true)
                {

                    if (cParam.m_strHanyoKb2.Trim == "2")
                    {

                        if (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0)
                        {
                            strResult = HEIKIMEI;
                        }
                        else
                        {
                            strResult = HONGOKUMEI;
                        }
                    }


                    else if (cParam.m_strFZYKanjiTsushomei.Trim.RLength > 0)
                    {
                        strResult = TSUSHOMEI;
                    }
                    else if (cParam.m_strFZYKanjiHeikimei.Trim.RLength > 0)
                    {
                        strResult = HEIKIMEI;
                    }
                    else
                    {
                        strResult = HONGOKUMEI;

                    }
                }

                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        // * 履歴番号 000003 2015/04/13 追加開始
        /// <summary>
    /// 生年月日取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>生年月日</returns>
    /// <remarks></remarks>
        private string GetUmareYMD(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                if (IsKojin(cParam) == true)
                {

                    switch (cParam.m_strFZYUmareFushoKbn)
                    {
                        case var @case when @case == ABConstClass.UMAREFUSHOKBN_FUSHO_YMD:  // 年月日が不詳
                            {
                                // 年月日不詳の場合、未設定とする。
                                strResult = string.Empty;
                                break;
                            }

                        default:
                            {
                                strResult = cParam.m_strUmareYmd.Trim;
                                break;
                            }
                    }
                }

                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }
        // * 履歴番号 000003 2015/04/13 追加終了

        #region 生年月日不詳区分取得

        /// <summary>
    /// 生年月日不詳区分取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>生年月日不詳区分</returns>
    /// <remarks></remarks>
        private string GetUmareFushoKBN(ABBSRenkeiPrmXClass cParam)
        {

            // * 履歴番号 000003 2015/04/13 削除開始
            // Const NONE As String = "0"
            // * 履歴番号 000003 2015/04/13 削除終了
            const string D = "1";
            const string MD = "2";
            // * 履歴番号 000003 2015/04/13 削除開始
            // Const YMD As String = "3"
            // * 履歴番号 000003 2015/04/13 削除終了

            string strResult = string.Empty;

            try
            {

                if (IsKojin(cParam) == true)
                {

                    switch (cParam.m_strFZYUmareFushoKbn)
                    {
                        case var @case when @case == ABConstClass.UMAREFUSHOKBN_FUSHO_D:  // 日が不詳
                            {
                                strResult = D;
                                break;
                            }
                        case var case1 when case1 == ABConstClass.UMAREFUSHOKBN_FUSHO_MD:  // 月日が不詳
                            {
                                // * 履歴番号 000003 2015/04/13 修正開始
                                // 日が不詳、月日が不詳以外の場合、未設定とする。
                                // Case ABConstClass.UMAREFUSHOKBN_FUSHO_YMD  ' 年月日が不詳
                                // strResult = YMD
                                // Case Else
                                // strResult = NONE
                                strResult = MD;
                                break;
                            }

                        default:
                            {
                                strResult = string.Empty;
                                break;
                            }
                            // * 履歴番号 000003 2015/04/13 修正終了
                    }
                }

                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 性別コード取得

        /// <summary>
    /// 性別コード取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>性別コード</returns>
    /// <remarks></remarks>
        private string GetSeibetsuCD(ABBSRenkeiPrmXClass cParam)
        {

            // * 履歴番号 000003 2015/04/13 削除開始
            // Const MALE As String = "1"
            // Const FEMALE As String = "2"
            // Const ETC As String = "3"
            // * 履歴番号 000006 2015/07/07 追加開始
            const string MALE = "1";
            const string FEMALE = "2";
            // * 履歴番号 000006 2015/07/07 追加終了
            // * 履歴番号 000003 2015/04/13 削除終了

            string strResult = string.Empty;

            try
            {

                if (IsKojin(cParam) == true)
                {

                    // * 履歴番号 000003 2015/04/13 修正開始
                    // コード体系がReamsと同値のため、変換は不要となった。
                    // Select Case cParam.m_strSeibetsuCd
                    // Case MALE
                    // strResult = MALE
                    // Case FEMALE
                    // strResult = FEMALE
                    // Case Else
                    // strResult = ETC
                    // End Select
                    // * 履歴番号 000004 2015/05/07 修正開始
                    // strResult = cParam.m_strSeibetsuCd
                    // * 履歴番号 000006 2015/07/07 修正開始
                    // 規定値以外をString.Emptyとするため、コード変換ロジックを復活させる。
                    // strResult = cParam.m_strSeibetsuCd.Trim
                    switch (cParam.m_strSeibetsuCd)
                    {
                        case MALE:
                            {
                                strResult = MALE;
                                break;
                            }
                        case FEMALE:
                            {
                                strResult = FEMALE;
                                break;
                            }

                        default:
                            {
                                strResult = string.Empty;
                                break;
                            }
                    }
                }
                // * 履歴番号 000006 2015/07/07 修正終了
                // * 履歴番号 000004 2015/05/07 修正終了
                // * 履歴番号 000003 2015/04/13 修正終了

                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 住所編集

        /// <summary>
    /// 住所編集
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>住所</returns>
    /// <remarks></remarks>
        private string EditJusho(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                // * 履歴番号 000011 2016/11/19 追加開始
                if (CheckKoiki() == true)
                {
                    // 広域の場合、都道府県名＋郡名＋市町村名を付加しない
                    strResult = cParam.m_strJusho;
                }
                // * 履歴番号 000011 2016/11/19 追加終了
                // 管内の場合、都道府県名＋郡名＋市町村名を付加する
                else if (cParam.m_strKannaiKangaiKb == ABConstClass.KANNAIKB)
                {
                    strResult = string.Concat(m_strShichosonMeisho, cParam.m_strJusho);
                }
                else
                {
                    strResult = cParam.m_strJusho;
                    // * 履歴番号 000011 2016/11/19 追加開始
                }
                // * 履歴番号 000011 2016/11/19 追加終了
                strResult = Left(strResult, LENGTH.JUSHO);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 異動事由コード設定

        /// <summary>
    /// 異動事由コード設定
    /// </summary>
    /// <remarks></remarks>
        private void SetIdoJiyuCD()
        {

            try
            {

                // -------------------------------------------------------------------------------------
                // 【住登内事由】
                m_csJutonaiJiyuCDConvertTable = new Hashtable();
                // -------------------------------------------------------------------------------------
                // 特殊追加
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.TokushuTsuika);

                // -------------------------------------------------------------------------------------
                // 特殊修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TokushuShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.TokushuShusei);

                // -------------------------------------------------------------------------------------
                // 特殊削除
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TokushuSakujo.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.TokushuSakujo);

                // -------------------------------------------------------------------------------------
                // 住民票ＣＤ修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TokushuCodeShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.JuminhyoCDShusei);

                // -------------------------------------------------------------------------------------
                // 個人番号修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KojinBangoShusei);

                // -------------------------------------------------------------------------------------
                // 履歴修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TokushuRirekiShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.RirekiShusei);

                // -------------------------------------------------------------------------------------
                // 転入
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Tennyu);

                // -------------------------------------------------------------------------------------
                // 出生
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Shussei);

                // -------------------------------------------------------------------------------------
                // 職権記載
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.ShokkenKisai);

                // -------------------------------------------------------------------------------------
                // 住所設定
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.JushoSettei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.JushoSettei);

                // -------------------------------------------------------------------------------------
                // 転出
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Tenshutsu.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Tenshutsu);

                // -------------------------------------------------------------------------------------
                // 死亡
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Shibo.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Shibo);

                // -------------------------------------------------------------------------------------
                // 失踪宣告
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Shisso.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.ShissoSenkoku);

                // -------------------------------------------------------------------------------------
                // 職権消除
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.ShokkenShojo.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.ShokkenShojo);

                // -------------------------------------------------------------------------------------
                // 転居
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Tenkyo.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Tenkyo);

                // -------------------------------------------------------------------------------------
                // 帰化
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Kika.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Kika);

                // -------------------------------------------------------------------------------------
                // 国籍取得
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KokusekiShutoku.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KokusekiShutoku);

                // -------------------------------------------------------------------------------------
                // 国籍喪失
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KokusekiSoshitsu.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KokusekiSoshitsu);

                // -------------------------------------------------------------------------------------
                // 主変更
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.SetainushiHenko.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.NushiHenko);

                // -------------------------------------------------------------------------------------
                // 世帯分離
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.SetaiBunri.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.SetaiBunri);

                // -------------------------------------------------------------------------------------
                // 世帯合併
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.SetaiGappei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.SetaiGappei);

                // -------------------------------------------------------------------------------------
                // 世帯変更
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.SetaiKoseiHenko.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.SetaiHenko);

                // -------------------------------------------------------------------------------------
                // 婚姻
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Konin.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Konin);

                // -------------------------------------------------------------------------------------
                // 離婚
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Rikon.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Rikon);

                // -------------------------------------------------------------------------------------
                // 養子縁組
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.YoshiEngumi.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.YoshiEngumi);

                // -------------------------------------------------------------------------------------
                // 養子離縁
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.YoshiRien.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.YoshiRien);

                // -------------------------------------------------------------------------------------
                // 転籍
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Tenseki.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Tenseki);

                // -------------------------------------------------------------------------------------
                // 分籍
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Bunseki.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Bunseki);

                // -------------------------------------------------------------------------------------
                // 入籍
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Nyuseki.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Nyuseki);

                // -------------------------------------------------------------------------------------
                // 認知
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Ninchi.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.Ninchi);

                // -------------------------------------------------------------------------------------
                // 戸籍その他
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KosekiSonota.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KosekiSonota);

                // -------------------------------------------------------------------------------------
                // 職権修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.ShokkenShusei);

                // -------------------------------------------------------------------------------------
                // 戸籍修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KosekiShogoShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KosekiShusei);

                // -------------------------------------------------------------------------------------
                // 転出取消
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.TenshutsuTorikeshi);

                // -------------------------------------------------------------------------------------
                // 職権回復
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.ShokkenKaifuku);

                // -------------------------------------------------------------------------------------
                // 転入通知受理
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.TennyuTsuchiJuri);

                // -------------------------------------------------------------------------------------
                // 住民票ＣＤ記載
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.CodeShokkenKisai.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.JuminhyoCDKisai);

                // -------------------------------------------------------------------------------------
                // 住民票ＣＤ変更
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.CodeHenkoSeikyu.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.JuminhyoCDHenko);

                // -------------------------------------------------------------------------------------
                // 個人番号記載
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KojinBangoKisai);

                // -------------------------------------------------------------------------------------
                // 個人番号変更
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KojinBangoHenko);

                // -------------------------------------------------------------------------------------
                // 個人番号職権修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KojinBangoShokkenShusei);

                // -------------------------------------------------------------------------------------
                // 住民票改製
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.JuminhyoKaisei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.JuminhyoKaisei);

                // -------------------------------------------------------------------------------------
                // 写し制御
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.UtsushiSeigyo.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.UtsushiSeigyo);

                // -------------------------------------------------------------------------------------
                // 表示順変更
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.HyojijunHenko.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.HyojijunHenko);

                // -------------------------------------------------------------------------------------
                // 個別事項修正
                m_csJutonaiJiyuCDConvertTable.Add(ABJukiShoriJiyuType.KobetsuShusei.GetHashCode.ToString("00"), ABJikiJukiShoriJiyuType.KobetsuJikoShusei);

                // -------------------------------------------------------------------------------------

                // -------------------------------------------------------------------------------------
                // 【住登外事由】
                m_csJutogaiJiyuCDConvertTable = new Hashtable();
                // -------------------------------------------------------------------------------------
                // 新規追加
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.ShinkiTsuika.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.Tsuika);

                // 特殊追加
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.TokushuTsuika.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.Tsuika);

                // -------------------------------------------------------------------------------------
                // 修正
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.Shusei.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.Shusei);

                // -------------------------------------------------------------------------------------
                // 住基異動者追加
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.JukiIdoshaTsuika.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.JukiIdoshaTsuika);

                // -------------------------------------------------------------------------------------
                // 削除（回復可）
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.RonriSakujo.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.RonriSakujo);

                // -------------------------------------------------------------------------------------
                // 削除回復
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.Kaifuku.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.Kaifuku);

                // -------------------------------------------------------------------------------------
                // 誤謬修正
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.TokushuShusei.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.GobyuShusei);

                // -------------------------------------------------------------------------------------
                // 削除（回復不可）
                m_csJutogaiJiyuCDConvertTable.Add(ABAtenaShoriJiyuType.ButsuriSakujo.GetHashCode().ToString("00"), ABJikiAtenaShoriJiyuType.ButsuriSakujo);

            }
            // -------------------------------------------------------------------------------------

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region 異動事由コード取得

        /// <summary>
    /// 異動事由コード取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>異動事由コード</returns>
    /// <remarks></remarks>
        private string GetIdoJiyuCD(ABBSRenkeiPrmXClass cParam)
        {

            const string JUMINJUTOGAIKB_JUMIN = "1";
            const string JUMINJUTOGAIKB_JUTOGAI = "2";

            string strResult = string.Empty;
            Hashtable csIdoJiyuCDConvertTable;

            try
            {

                // 変換テーブルの決定
                switch (cParam.m_strJuminJutogaiKb)
                {
                    case JUMINJUTOGAIKB_JUMIN:
                        {
                            // * 履歴番号 000003 2015/04/13 修正開始
                            // 住登内のコード体系がReamsと同値のため、変換は不要となった。
                            // csIdoJiyuCDConvertTable = m_csJutonaiJiyuCDConvertTable
                            // * 履歴番号 000004 2015/05/07 修正開始
                            // Return cParam.m_strCkinJiyuCd
                            // * 履歴番号 000006 2015/07/07 修正開始
                            // 規定値以外をString.Emptyとするため、
                            // コード体系を見直した上でコード変換ロジックを復活させる。
                            // Return cParam.m_strCkinJiyuCd.Trim
                            csIdoJiyuCDConvertTable = m_csJutonaiJiyuCDConvertTable;
                            break;
                        }
                    // * 履歴番号 000006 2015/07/07 修正終了
                    // * 履歴番号 000004 2015/05/07 修正終了
                    // * 履歴番号 000003 2015/04/13 修正終了
                    case JUMINJUTOGAIKB_JUTOGAI:
                        {
                            csIdoJiyuCDConvertTable = m_csJutogaiJiyuCDConvertTable;
                            break;
                        }

                    default:
                        {
                            return string.Empty;
                        }
                }

                // コード変換処理
                if (csIdoJiyuCDConvertTable.ContainsKey(cParam.m_strCkinJiyuCd) == true)
                {
                    strResult = csIdoJiyuCDConvertTable(cParam.m_strCkinJiyuCd).GetHashCode.ToString("00");
                }
                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 更新日時取得

        /// <summary>
    /// 更新日時取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>更新日時</returns>
    /// <remarks></remarks>
        private string GetKoshinNichiji(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {

                if (cParam.m_strMyNumberJuminCD.Trim.RLength > 0)
                {
                    strResult = cParam.m_strMyNumberKoshinNichiji.Trim;
                }
                else
                {
                    // * 履歴番号 000002 2015/02/09 修正開始
                    // 宛名マスタの更新日時
                    // 宛名累積マスタの処理日時
                    // strResult = cParam.m_strKoshinNichiji.Trim
                    strResult = cParam.m_strShoriNichiji.Trim;
                    // * 履歴番号 000002 2015/02/09 修正終了
                }

                // * 履歴番号 000006 2015/07/07 追加開始
                // * 履歴番号 000007 2015/07/15 修正開始
                // If (strResult.Trim.Length > 0) Then
                // ' noop
                // Else
                // ' 値が存在しない場合は、システム日時を設定する。
                // strResult = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")
                // End If
                strResult = this.CheckDateTime(cParam.m_strJuminCd, strResult);
            }
            // * 履歴番号 000007 2015/07/15 修正終了
            // * 履歴番号 000006 2015/07/07 追加終了

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 備考編集

        /// <summary>
    /// 備考編集
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>備考</returns>
    /// <remarks></remarks>
        private string GetBiko(ABBSRenkeiPrmXClass cParam)
        {

            const string SHISAN_TOCHI = "土";
            const string SHISAN_KAOKU = "家";
            const string SHISAN_SHOKYAKU = "償";
            const string SHISAN_FUKA = "賦";
            const string MINZEI_KAZEITAISHOSHA = "住";
            const string MINZEI_JIMUSHO = "住事";
            const string HOJIN = "法";
            const string KEIJI_SHOYUSHA = "軽所";
            const string KEIJI_SHIYOSHA = "軽使";
            const string SHUNO_GENNEN = "収現";
            const string SHUNO_KANEN = "収過";
            const string KOZA = "口";
            const string KANINOFUSHO = "簡";
            const string KOKUHO = "国";
            const string JITE_JUKYUSHA = "児受";
            const string JITE_JIDO = "児児";
            const string FLG_ON = "1";
            const string SEPALATER = "／";

            string strResult = string.Empty;
            var cBikoList = new List<string>();

            try
            {


                if (cParam.m_strShisan_Tochi == FLG_ON)
                {
                    cBikoList.Add(SHISAN_TOCHI);
                }

                if (cParam.m_strShisan_Kaoku == FLG_ON)
                {
                    cBikoList.Add(SHISAN_KAOKU);
                }

                if (cParam.m_strShisan_Shokyaku == FLG_ON)
                {
                    cBikoList.Add(SHISAN_SHOKYAKU);
                }

                if (cParam.m_strShisan_Fuka == FLG_ON)
                {
                    cBikoList.Add(SHISAN_FUKA);
                }

                if (cParam.m_strMinzei_Kazei == FLG_ON)
                {
                    cBikoList.Add(MINZEI_KAZEITAISHOSHA);
                }

                if (cParam.m_strMinzei_Jimusho == FLG_ON)
                {
                    cBikoList.Add(MINZEI_JIMUSHO);
                }

                if (cParam.m_strHojin == FLG_ON)
                {
                    cBikoList.Add(HOJIN);
                }

                if (cParam.m_strKeiji_Shiyosha == FLG_ON)
                {
                    cBikoList.Add(KEIJI_SHIYOSHA);
                }

                if (cParam.m_strKeiji_Shoyusha == FLG_ON)
                {
                    cBikoList.Add(KEIJI_SHOYUSHA);
                }

                if (cParam.m_strShuno_Gennen == FLG_ON)
                {
                    cBikoList.Add(SHUNO_GENNEN);
                }

                if (cParam.m_strShuno_Kanen == FLG_ON)
                {
                    cBikoList.Add(SHUNO_KANEN);
                }

                if (cParam.m_strKoza == FLG_ON)
                {
                    cBikoList.Add(KOZA);
                }

                if (cParam.m_strKaniNofu == FLG_ON)
                {
                    cBikoList.Add(KANINOFUSHO);
                }

                if (cParam.m_strKokuho == FLG_ON)
                {
                    cBikoList.Add(KOKUHO);
                }

                if (cParam.m_strJite_Jukyusha == FLG_ON)
                {
                    cBikoList.Add(JITE_JUKYUSHA);
                }

                if (cParam.m_strJite_Jido == FLG_ON)
                {
                    cBikoList.Add(JITE_JIDO);

                }

                strResult = string.Join(SEPALATER, cBikoList.ToArray());
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 個人番号取得

        // * 履歴番号 000001 2015/01/08 削除開始
        // ''' <summary>
        // ''' 個人番号取得
        // ''' </summary>
        // ''' <param name="cParam">編集パラメーター</param>
        // ''' <returns>個人番号</returns>
        // ''' <remarks></remarks>
        // Private Function GetKojinBango(ByVal cParam As ABBSRenkeiPrmXClass) As String

        // Dim strResult As String = String.Empty

        // Try

        // If (cParam.m_strMyNumber.Trim.Length = ABConstClass.MYNUMBER.LENGTH.KOJIN) Then
        // strResult = cParam.m_strMyNumber.Trim
        // Else
        // strResult = String.Empty
        // End If

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return strResult

        // End Function
        // * 履歴番号 000001 2015/01/08 削除終了

        #endregion

        #region 直近区分取得

        /// <summary>
    /// 直近区分取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>直近区分</returns>
    /// <remarks></remarks>
        private string GetCkinKB(ABBSRenkeiPrmXClass cParam)
        {

            const string CKIN = "0";
            const string RRK = "1";

            string strResult = string.Empty;

            try
            {

                switch (cParam.m_strMyNumberCkinKB)
                {
                    case var @case when @case == ABMyNumberEntity.DEFAULT.CKINKB.CKIN:
                        {
                            strResult = CKIN;
                            break;
                        }
                    case var case1 when case1 == ABMyNumberEntity.DEFAULT.CKINKB.RRK:
                        {
                            strResult = RRK;
                            break;
                        }

                    default:
                        {
                            strResult = string.Empty;
                            break;
                        }
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 法人番号取得

        // * 履歴番号 000001 2015/01/08 削除開始
        // ''' <summary>
        // ''' 法人番号取得
        // ''' </summary>
        // ''' <param name="cParam">編集パラメーター</param>
        // ''' <returns>法人番号</returns>
        // ''' <remarks></remarks>
        // Private Function GetHojinBango(ByVal cParam As ABBSRenkeiPrmXClass) As String

        // Dim strResult As String = String.Empty

        // Try

        // If (cParam.m_strMyNumber.Trim.Length = ABConstClass.MYNUMBER.LENGTH.HOJIN) Then
        // strResult = cParam.m_strMyNumber.Trim
        // Else
        // strResult = String.Empty
        // End If

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return strResult

        // End Function
        // * 履歴番号 000001 2015/01/08 削除終了

        #endregion

        // * 履歴番号 000001 2015/01/08 追加開始
        #region 市町村コード取得

        /// <summary>
    /// 市町村コード取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>市町村コード</returns>
    /// <remarks></remarks>
        private string GetShichosonCD(ABBSRenkeiPrmXClass cParam)
        {

            string strResult = string.Empty;

            try
            {
                // * 履歴番号 000010 2016/06/10 修正開始
                // Select Case cParam.m_strKannaiKangaiKb
                // Case ABConstClass.KANNAIKB
                // strResult = m_strShichosonCD5
                // Case Else
                // If (cParam.m_strJushoCd.Trim.Length > 5) Then
                // strResult = cParam.m_strJushoCd.Trim.Substring(0, 5).Trim
                // Else
                // strResult = cParam.m_strJushoCd.Trim
                // End If
                // End Select
                // 広域の場合は、地区コード3を設定する。
                if (CheckKoiki())
                {

                    // * 履歴番号 000012 2017/05/23 修正開始
                    // strResult = cParam.m_strChikuCd3.Trim
                    if (cParam.m_strChikuCd3.Trim.RLength > 5)
                    {
                        strResult = cParam.m_strChikuCd3.Trim.RSubstring(0, 5).Trim;
                    }
                    else
                    {
                        strResult = cParam.m_strChikuCd3.Trim;
                    }
                }
                // * 履歴番号 000012 2017/05/23 修正終了

                else
                {
                    switch (cParam.m_strKannaiKangaiKb)
                    {

                        case var @case when @case == ABConstClass.KANNAIKB:
                            {

                                strResult = m_strShichosonCD5;
                                break;
                            }

                        default:
                            {

                                if (cParam.m_strJushoCd.Trim.RLength > 5)
                                {
                                    strResult = cParam.m_strJushoCd.Trim.RSubstring(0, 5).Trim;
                                }
                                else
                                {
                                    strResult = cParam.m_strJushoCd.Trim;
                                }

                                break;
                            }

                    }
                }
            }
            // * 履歴番号 000010 2016/06/10 修正終了
            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 注意喚起フラグ取得

        /// <summary>
    /// 注意喚起フラグ取得
    /// </summary>
    /// <param name="cParam">編集パラメーター</param>
    /// <returns>注意喚起フラグ</returns>
    /// <remarks></remarks>
        private string GetChuuiKankiFG(ABBSRenkeiPrmXClass cParam)
        {

            const string SEIGYOKBN_ON = "1";
            const string SEIGYOKBN_OFF = "0";
            string strResult = string.Empty;

            try
            {

                if (cParam.m_strKojinSeigyoKbn.Trim.RLength > 0)
                {
                    strResult = SEIGYOKBN_ON;
                }
                else
                {
                    strResult = SEIGYOKBN_OFF;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion
        // * 履歴番号 000001 2015/01/08 追加終了

        // * 履歴番号 000005 2015/06/09 追加開始
        #region 全角化処理

        /// <summary>
    /// 全角化処理
    /// </summary>
    /// <param name="cRenkeiData">連携データ</param>
    /// <remarks></remarks>
        private ABBSRenkeiRetXClass ToWide(ref ABBSRenkeiRetXClass cRenkeiData)
        {

            try
            {



                // -----------------------------------------------------------------------------------------------------
                // 漢字氏名
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strKanjiShimei = UFStringClass.ConvertNarrowToWide(.m_strKanjiShimei)
                cRenkeiData.m_strKanjiShimei = ConvertNarrowToWideWrap(cRenkeiData.m_strKanjiShimei);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 外国人氏名
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strFrnShimei = UFStringClass.ConvertNarrowToWide(.m_strFrnShimei)
                cRenkeiData.m_strFrnShimei = ConvertNarrowToWideWrap(cRenkeiData.m_strFrnShimei);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 外国人併記名
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strHeikimei = UFStringClass.ConvertNarrowToWide(.m_strHeikimei)
                cRenkeiData.m_strHeikimei = ConvertNarrowToWideWrap(cRenkeiData.m_strHeikimei);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 通称名
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strTsushomei = UFStringClass.ConvertNarrowToWide(.m_strTsushomei)
                cRenkeiData.m_strTsushomei = ConvertNarrowToWideWrap(cRenkeiData.m_strTsushomei);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 国籍名
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strKokuseki = UFStringClass.ConvertNarrowToWide(.m_strKokuseki)
                cRenkeiData.m_strKokuseki = ConvertNarrowToWideWrap(cRenkeiData.m_strKokuseki);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 住所
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strJusho = UFStringClass.ConvertNarrowToWide(.m_strJusho)
                cRenkeiData.m_strJusho = ConvertNarrowToWideWrap(cRenkeiData.m_strJusho);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 番地
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strBanchi = UFStringClass.ConvertNarrowToWide(.m_strBanchi)
                cRenkeiData.m_strBanchi = ConvertNarrowToWideWrap(cRenkeiData.m_strBanchi);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 方書
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strKatagaki = UFStringClass.ConvertNarrowToWide(.m_strKatagaki)
                cRenkeiData.m_strKatagaki = ConvertNarrowToWideWrap(cRenkeiData.m_strKatagaki);
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------
                // 備考
                // * 履歴番号 000009 2015/11/13 修正開始
                // .m_strBiko = UFStringClass.ConvertNarrowToWide(.m_strBiko)
                // * 履歴番号 000009 2015/11/13 修正終了
                // -----------------------------------------------------------------------------------------------------

                cRenkeiData.m_strBiko = ConvertNarrowToWideWrap(cRenkeiData.m_strBiko);
            }


            catch (Exception csExp)
            {
                throw;
            }

            return cRenkeiData;

        }

        #endregion
        // * 履歴番号 000005 2015/06/09 追加終了

        // * 履歴番号 000009 2015/11/13 追加開始
        #region ConvertNarrowToWideWrap

        /// <summary>
    /// 全角変換（.NET Frameworkの挙動を補正）
    /// </summary>
    /// <param name="strValue">対象文字列</param>
    /// <returns>変換後文字列</returns>
    /// <remarks>
    /// Windows7以降OSにてStrConvを使用すると
    /// 対象文字列に単独の濁点、半濁点が含まれる場合半角の "?" が返信されてしまう。
    /// 全角のみを許容している項目に半角文字が混入してしまうため、
    /// 代替え文字列 "●" に置換した上で連携することとする。
    /// </remarks>
        private string ConvertNarrowToWideWrap(string strValue)
        {

            const string ERROR_STRING = "?";
            const string REPLACE_STRING = "●";

            string strResult = string.Empty;

            try
            {

                strResult = UFStringClass.ConvertNarrowToWide(strValue);

                if (strResult.RIndexOf(ERROR_STRING) < 0)
                {
                }
                // noop
                else
                {
                    strResult = strResult.Replace(ERROR_STRING, REPLACE_STRING);
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion
        // * 履歴番号 000009 2015/11/13 追加終了

        // * 履歴番号 000007 2015/07/15 追加開始
        #region 登録日時チェック処理

        /// <summary>
    /// 登録日時チェック処理
    /// </summary>
    /// <param name="strJuminCD">住民コード</param>
    /// <param name="strTorokuNichiji">登録日時</param>
    /// <returns>登録日時（チェック＆編集後）</returns>
    /// <remarks></remarks>
        private string CheckDateTime(string strJuminCD, string strTorokuNichiji)

        {

            string strResult = string.Empty;
            string strYear = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;
            string strHour = string.Empty;
            string strMinute = string.Empty;
            string strSecond = string.Empty;
            string strMilliSecond = string.Empty;
            DateTime csDateTime;

            const int DATE_TIME_MAX_LENGTH = 17;
            const string HOUR_MAX = "23";
            const string MINUTE_MAX = "59";
            const string SECOND_MAX = "59";
            const string DATE_SEPARATOR = "/";
            const string DATE_TIME_SEPARATOR = " ";
            const string TIME_SEPARATOR = ":";
            const string MSEC_SEPARATOR = ".";

            try
            {

                strResult = strTorokuNichiji.Trim();

                // 歴上日になるように登録日時を整備する。
                if (strResult.RLength == DATE_TIME_MAX_LENGTH && UFStringClass.CheckNumber(strResult) == true)
                {

                    strResult = strResult.RPadRight(17, '0');
                    strYear = strResult.RSubstring(0, 4).Trim.RPadLeft(4, '0');
                    strMonth = strResult.RSubstring(4, 2).Trim.RPadLeft(2, '0');
                    strDay = strResult.RSubstring(6, 2).Trim.RPadLeft(2, '0');
                    strHour = strResult.RSubstring(8, 2).Trim.RPadLeft(2, '0');
                    strMinute = strResult.RSubstring(10, 2).Trim.RPadLeft(2, '0');
                    strSecond = strResult.RSubstring(12, 2).Trim.RPadLeft(2, '0');
                    strMilliSecond = strResult.RSubstring(14, 3).Trim.RPadLeft(3, '0');

                    if (Operators.CompareString(HOUR_MAX, strHour, false) < 0)
                    {
                        strHour = HOUR_MAX;
                    }
                    else
                    {
                        // noop
                    }

                    if (Operators.CompareString(MINUTE_MAX, strMinute, false) < 0)
                    {
                        strMinute = MINUTE_MAX;
                    }
                    else
                    {
                        // noop
                    }

                    if (Operators.CompareString(SECOND_MAX, strSecond, false) < 0)
                    {
                        strSecond = SECOND_MAX;
                    }
                    else
                    {
                        // noop
                    }

                    if (DateTime.TryParse(string.Concat(string.Join(DATE_SEPARATOR, new string[] { strYear, strMonth, strDay }), DATE_TIME_SEPARATOR, string.Join(TIME_SEPARATOR, new string[] { strHour, strMinute, strSecond }), MSEC_SEPARATOR, strMilliSecond), out csDateTime) == true)
                    {
                    }
                    // noop
                    else
                    {
                        csDateTime = m_cfRdbClass.GetSystemDate;
                    }
                }

                else
                {
                    csDateTime = m_cfRdbClass.GetSystemDate;
                }

                // 住民コード単位にブレイクする。
                if ((m_strBeforeJuminCD ?? "") == (strJuminCD ?? ""))
                {
                }
                // noop
                else
                {
                    m_strBeforeJuminCD = strJuminCD;
                    m_csTorokuNichijiList.Clear();
                }

                // 登録日時が一意となるように１ミリ秒ずつ加算していく。
                do
                {

                    strResult = csDateTime.ToString("yyyyMMddHHmmssfff");
                    if (m_csTorokuNichijiList.Contains(strResult) == true)
                    {
                        csDateTime = csDateTime.AddMilliseconds(1d);
                    }
                    else
                    {
                        m_csTorokuNichijiList.Add(strResult);
                        break;
                    }
                }

                while (true);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion
        // * 履歴番号 000007 2015/07/15 追加終了

        // * 履歴番号 000008 2015/09/29 追加開始
        #region 日付チェック処理

        /// <summary>
    /// 日付チェック処理
    /// </summary>
    /// <param name="strDateValue">対象日付</param>
    /// <returns>変換後日付</returns>
    /// <remarks></remarks>
        private string CheckDate(string strDateValue)
        {

            string strResult = string.Empty;

            try
            {

                m_cfDate.p_strDateValue = strDateValue;
                if (m_cfDate.CheckDate() == true)
                {
                    strResult = m_cfDate.p_strSeirekiYMD;
                }
                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion

        #region 郵便番号チェック処理

        /// <summary>
    /// 郵便番号チェック処理
    /// </summary>
    /// <param name="strZIPCode">対象郵便番号</param>
    /// <returns>変換後郵便番号</returns>
    /// <remarks></remarks>
        private string CheckZIPCode(string strZIPCode)
        {

            string strResult = string.Empty;

            try
            {

                // 半角数字で3桁、5桁、7桁のいずれかで構成された値の場合のみ設定する。
                if (Regex.IsMatch(strZIPCode, @"^(\d{3}|\d{5}|\d{7})$") == true)
                {
                    strResult = strZIPCode;
                }
                else
                {
                    strResult = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return strResult;

        }

        #endregion
        // * 履歴番号 000008 2015/09/29 追加終了

        // * 履歴番号 000010 2016/06/10 追加開始
        #region 広域判定取得

        /// <summary>
    /// 広域判定取得
    /// </summary>
    /// <returns>広域判定取得</returns>
    /// <remarks></remarks>
        private bool CheckKoiki()
        {
            try
            {
                // 管理情報の取得 [35-40] 広域判定
                // パラメーター値が1なら広域でTrueを返却する
                return ISKOIKI == (GetKanriJoho(KEYINFO_35_40[(int)KEY_INDEX.SHUKEY], KEYINFO_35_40[(int)KEY_INDEX.SHIKIBETSUKEY]) ?? "");
            }
            catch (Exception csExp)
            {
                throw;
            }
        }

        #endregion

        #region システム識別コードの取得

        /// <summary>
    /// システム識別コードの取得
    /// </summary>
    /// <returns>システム識別コード</returns>
    /// <remarks></remarks>
        private string GetSystemShikibetsuCD()
        {
            string strParameter;

            try
            {
                // 管理情報の取得 [35-41] テストモード判定
                strParameter = GetKanriJoho(KEYINFO_35_41[(int)KEY_INDEX.SHUKEY], KEYINFO_35_41[(int)KEY_INDEX.SHIKIBETSUKEY]);

                // 取得値を判定
                if (!string.IsNullOrEmpty(strParameter))
                {
                    return strParameter;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }
            // パラメーター値がない場合SYSTEM_SHIKIBETSUCDを返却する
            return SYSTEM_SHIKIBETSUCD;
        }

        #endregion


        #region 管理情報取得

        /// <summary>
    /// ABATENAKANRIJOHOから管理情報取得
    /// </summary>
    /// <returns>パラメータ値</returns>
    /// <remarks></remarks>
        private string GetKanriJoho(string strShukey, string strShikibetsukey)
        {

            DataSet csDataSet;
            string strParameter;

            try
            {

                // 管理情報の取得
                csDataSet = m_cABAtenaKanriJohoB.GetKanriJohoHoshu(strShukey, strShikibetsukey);

                // 取得件数を判定
                if (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 取得結果が1件以上の場合、パラメーターを取得
                    strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString;
                    // パラメーター値を返却する
                    return strParameter.Trim();
                }
            }

            catch (Exception csExp)
            {
                throw;
            }
            // パラメーター値がないEmptyを返却する
            return string.Empty;

        }

        #endregion
        // * 履歴番号 000010 2016/06/10 追加終了
        #endregion

    }
}
