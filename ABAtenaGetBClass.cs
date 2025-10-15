// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名取得(ABAtenaGetClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/06　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/02/19 000001     簡易宛名取得１で、管理情報が引き渡されないケースがある。
// *                       簡易宛名取得１で、業務コードが指定されていて、取得件数が１件の場合は、送付先データがなくても、送付先レコードを戻す
// * 2003/02/25 000002     簡易宛名取得１メッソッドで、16・17でデータ取得０件の場合は、エラーにせずにcsAtenaHとcsAtenaHS をマージして戻す。
// * 2003/02/26 000003     市町村コードの抽出条件を追加
// * 2003/03/07 000004     プロジェクトのImportsは定義しない（仕様変更）
// * 2003/03/07 000005     有効桁数対応（仕様変更）
// * 2003/03/17 000006     パラメータのチェックをはずす（仕様変更）
// * 2003/03/17 000007     業務"AB"固定でRDBをアクセスする（仕様変更）
// * 2003/03/18 000008     エラーメッセージの変更（仕様変更）
// * 2003/03/27 000009     エラー処理クラスの参照先を"AB"固定にする
// * 2003/04/18 000010     年金宛名取得メソッド・国保宛名履歴取得メソッドを追加
// * 2003/04/22 000011     データが取得出来なくても例外を発生させない
// * 2003/04/30 000012     データが取得できなかった場合も、0件で編集データを返す。
// * 2003/05/22 000013     RDBのConnectはﾒｿｯﾄﾞの先頭に変更(仕様変更)
// * 2003/06/17 000014     チューニング(管理情報取得を最小限にする)
// * 2003/08/21 000015     ＵＲキャッシュ対応／継承可能クラスに変更
// * 2003/09/08 000016     国保宛名履歴取得の仕様変更
// * 2003/10/09 000017     連絡先は、連絡先マスタにデータが存在する場合は、そちらから取得する。但し、業務コードが指定されてた場合のみに限る。
// *                       NenkinAtenaGetもAtenaGet1と同様に指定年月日が指定されたら、宛名履歴より取得する。連絡先も同様。但し、代納・合算は不要。
// * 2003/10/30 000018     p_strJukiJushoCDは8桁
// * 2003/10/30 000019     仕様変更：カタカナチェックをANKチェックに変更
// * 2003/11/19 000020     仕様追加：簡易宛名取得1(オーバーロード)メソッドの追加
// * 2003/12/01 000021     仕様変更：データ区分'1%'の場合、個人のみを取得する
// * 2003/12/02 000022     仕様変更：連絡先取得処理を宛名編集から宛名取得へ移動
// * 2004/08/27 000023     速度改善：（宮沢）
// * 2005/01/25 000024     速度改善２：（宮沢）
// * 2005/04/04 000025     全角でのあいまい検索を可能にする(マルゴ村山)
// * 2005/04/21 000026     代納・送付先の期間指定日をシステム日付にする
// * 2005/05/06 000027     パラメータチェックをTRIMしてから行なう。性別単独は許さない。
// * 2005/12/06 000028     CheckColumnValueメソッドで行政区ＣＤはＡＮＫチェックを行う。(マルゴ村山)
// * 2006/07/31 000029     年金宛名ゲットⅡ追加に伴う修正 (吉澤)
// * 2007/04/21 000030     介護版宛名取得メソッドの追加 (吉澤)
// * 2007/07/28 000031     同一人代表者取得機能の追加 (吉澤)
// * 2007/09/04 000032     外国人本名検索機能の追加：検索カナ名編集用メソッド追加（中沢）
// * 2007/09/13 000033     宛名取得パラメータの住民コードをトリムする「p_strJuminCD」 (吉澤)
// * 2007/10/10 000034     検索用カナ項目にアルファベットが入ってきた場合は大文字に変換（中沢）
// * 2007/10/10 000035     外国人本名検索で名前の先頭が「ウ」の場合の検索漏れ対応（中沢）
// * 2007/11/06 000036     検索カナ編集メソッド、仕様通り編集されない部分を修正（中沢）
// * 2008/01/17 000037     同一人代表者取得による住民コード誤りの不具合対応（吉澤）
// * 2008/01/17 000038     宛名個別情報を取得する時、個別事項取得区分を引数に設定するよう修正（比嘉）
// * 2008/02/17 000039     氏名簡略文字編集処理を追加（比嘉）
// * 2008/11/10 000040     利用届出取得処理を追加（比嘉）
// * 2008/11/17 000041     利用届該当データ絞込み処理の修正（比嘉）
// * 2008/11/18 000042     利用届出取得処理の追加に伴う、連絡先データ取得処理の改修（比嘉）
// * 2009/04/08 000043     検索キー無しでAtnaGet2を使用するとオブジェクト参照エラーが発生する不具合改修（中沢）
// * 2010/04/16 000044     VS2008対応（比嘉）
// * 2010/05/17 000045     本籍筆頭者及び処理停止区分対応（比嘉）
// * 2011/05/18 000046     外国人在留情報取得区分対応（比嘉）
// * 2011/11/07 000047     【AB17010】住基法改正区分追加対応（池田）
// * 2014/04/28 000048     【AB21040】＜共通番号対応＞共通番号取得区分追加（石合）
// * 2018/03/08 000049     【AB26001】履歴検索機能追加（石合）
// * 2020/01/31 000050     【AB00185】AtenaGet1以外の履歴検索機能追加（石合）
// * 2020/11/04 000051     【AB00189】利用届出複数納税者ID対応（須江）
// * 2023/03/10 000052     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2023/12/04 000053     【AB-1600-1】検索機能対応(下村)
// * 2024/03/07 000054     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Security;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 宛名取得に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaGetBClass
    {

        #region  メンバ変数 
        // パラメータのメンバ変数
        // * 履歴番号 000015 2003/08/21 修正開始
        // Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
        // Private m_cfControlData As UFControlData                ' コントロールデータ
        // Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
        // Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
        // Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス

        // Private m_intHyojiketaJuminCD As Integer                ' 住民コード表示桁数
        // Private m_intHyojiketaStaiCD As Integer                 ' 世帯コード表示桁数
        // Private m_intHyojiketaJushoCD As Integer                ' 住所コード表示桁数（管内のみ）
        // Private m_intHyojiketaGyoseikuCD As Integer             ' 行政区コード表示桁数
        // Private m_intHyojiketaChikuCD1 As Integer               ' 地区コード１表示桁数
        // Private m_intHyojiketaChikuCD2 As Integer               ' 地区コード２表示桁数
        // Private m_intHyojiketaChikuCD3 As Integer               ' 地区コード３表示桁数
        // Private m_strChikuCD1HyojiMeisho As String              ' 地区コード１表示名称
        // Private m_strChikuCD2HyojiMeisho As String              ' 地区コード２表示名称
        // Private m_strChikuCD3HyojiMeisho As String              ' 地区コード３表示名称
        // Private m_strRenrakusaki1HyojiMeisho As String          ' 連絡先１表示名称
        // Private m_strRenrakusaki2HyojiMeisho As String          ' 連絡先２表示名称
        // '* 履歴番号 000014 2003/06/17 追加開始
        // Private m_blnKanriJoho As Boolean                       ' 管理情報取得
        // '* 履歴番号 000014 2003/06/17 追加終了

        // '　コンスタント定義
        // Private Const THIS_CLASS_NAME As String = "ABAtenaGetBClass"                ' クラス名
        // Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード

        protected UFLogClass m_cfLogClass;                      // ログ出力クラス
        protected UFControlData m_cfControlData;                // コントロールデータ
        protected UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        protected UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        protected UFErrorClass m_cfErrorClass;                  // エラー処理クラス

        protected int m_intHyojiketaJuminCD;                // 住民コード表示桁数
        protected int m_intHyojiketaStaiCD;                 // 世帯コード表示桁数
        protected int m_intHyojiketaJushoCD;                // 住所コード表示桁数（管内のみ）
        protected int m_intHyojiketaGyoseikuCD;             // 行政区コード表示桁数
        protected int m_intHyojiketaChikuCD1;               // 地区コード１表示桁数
        protected int m_intHyojiketaChikuCD2;               // 地区コード２表示桁数
        protected int m_intHyojiketaChikuCD3;               // 地区コード３表示桁数
        protected string m_strChikuCD1HyojiMeisho;              // 地区コード１表示名称
        protected string m_strChikuCD2HyojiMeisho;              // 地区コード２表示名称
        protected string m_strChikuCD3HyojiMeisho;              // 地区コード３表示名称
        protected string m_strRenrakusaki1HyojiMeisho;          // 連絡先１表示名称
        protected string m_strRenrakusaki2HyojiMeisho;          // 連絡先２表示名称
        protected bool m_blnKanriJoho;                       // 管理情報取得
        protected bool m_blnBatch;                           // バッチ区分(True:バッチ系, False:リアル系)
        protected bool m_blnBatchRdb;
        protected ABAtenaHenshuBClass m_cABAtenaHenshuB;                          // 宛名編集クラス
        protected ABBatchAtenaHenshuBClass m_cABBatchAtenaHenshuB;                // 宛名編集クラス(バッチ系)
                                                                                  // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
        private ABAtenaRirekiBClass m_cABAtenaRirekiB;          // 宛名履歴マスタＤＡクラス
        private ABAtenaBClass m_cABAtenaB;                      // 宛名マスタＤＡクラス
        private ABSfskBClass m_cABSfskB;                        // 送付先マスタＤＡクラス
        private ABDainoBClass m_cABDainoB;                      // 代納マスタＤＡクラス

        private USSCityInfoClass m_cUSSCityInfoClass;           // 市町村情報管理クラス
        private ABRenrakusakiBClass m_cRenrakusakiBClass;       // 連絡先Ｂクラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private URAtenaKanriJohoCacheBClass m_cfURAtenaKanriJoho;   // 宛名管理情報キャッシュＢクラス
                                                                    // * 履歴番号 000023 2004/08/27 追加終了
                                                                    // *履歴番号 000032 2007/09/04 追加開始
        private URKANRIJOHOBClass m_cURKanriJohoB;         // 管理情報取得クラス
                                                           // バッチから呼ばれた場合エラーが発生するため，キャッシュクラスはコメントアウト
                                                           // Private m_cURKanriJohoB As URKANRIJOHOCacheBClass         '管理情報取得クラス
                                                           // *履歴番号 000032 2007/09/04 追加終了

        // コンスタント定義
        protected const string THIS_CLASS_NAME = "ABAtenaGetBClass";              // クラス名
        protected const string THIS_BUSINESSID = "AB";                            // 業務コード
                                                                                  // * 履歴番号 000015 2003/08/21 修正終了

        // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
        protected ABEnumDefine.AtenaGetKB m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniAll;
        protected ABAtenaRirekiBClass m_cABAtenaRirekiBRef;          // 宛名履歴マスタＤＡクラス
        protected ABAtenaBClass m_cABAtenaBRef;                      // 宛名マスタＤＡクラス
        protected ABSfskBClass m_cABSfskBRef;                        // 送付先マスタＤＡクラス
        protected ABDainoBClass m_cABDainoBRef;                      // 代納マスタＤＡクラス
                                                                     // * 履歴番号 000024 2005/01/25 追加終了
                                                                     // * 履歴番号 000026 2005/04/21 追加開始
        private string m_strSystemDateTime;                          // 処理日時
                                                                     // * 履歴番号 000026 2005/04/21 追加終了

        // *履歴番号 000022 2007/04/28 追加開始
        private ABEnumDefine.MethodKB m_blnSelectKaigo;  // メソッド区分（通常版か、介護版、、、）
                                                         // *履歴番号 000022 2007/04/28 追加終了

        // *履歴番号 000031 2007/07/28 追加開始
        private ABAtenaKanriJohoBClass m_cABAtenaKanriJohoB;              // 管理情報Ｂクラス
        private ABGappeiDoitsuninBClass m_cABGappeiDoitsuninB;            // 同一人Ｂクラス
        private string m_strDoitsu_Param;                    // 同一人判定パラメータ
        private string m_strHonninJuminCD;                    // 本人住民コード
                                                              // *履歴番号 000031 2007/07/28 追加終了

        // *履歴番号 000042 2008/11/18 追加開始
        private ABEnumDefine.MethodKB m_blnMethodKB;
        // *履歴番号 000042 2008/11/18 追加終了

        #endregion

        #region プロパティ 
        // ************************************************************************************************
        // * 各メンバ変数のプロパティ定義
        // ************************************************************************************************
        public int p_intHyojiketaJuminCD
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaJuminCD;
            }
        }
        public int p_intHyojiketaStaiCD
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaStaiCD;
            }
        }
        public int p_intHyojiketaJushoCD
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaJushoCD;
            }
        }
        public int p_intHyojiketaGyoseikuCD
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaGyoseikuCD;
            }
        }
        public int p_intHyojiketaChikuCD1
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaChikuCD1;
            }
        }
        public int p_intHyojiketaChikuCD2
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaChikuCD2;
            }
        }
        public int p_intHyojiketaChikuCD3
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_intHyojiketaChikuCD3;
            }
        }
        public string p_strChikuCD1HyojiMeisho
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_strChikuCD1HyojiMeisho;
            }
        }
        public string p_strChikuCD2HyojiMeisho
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_strChikuCD2HyojiMeisho;
            }
        }
        public string p_strChikuCD3HyojiMeisho
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_strChikuCD3HyojiMeisho;
            }
        }
        public string p_strRenrakusaki1HyojiMeisho
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_strRenrakusaki1HyojiMeisho;
            }
        }
        public string p_strRenrakusaki2HyojiMeisho
        {
            get
            {
                // * 履歴番号 000014 2003/06/17 追加開始
                if (!m_blnKanriJoho)
                {
                    KanriJohoGet();
                }
                // * 履歴番号 000014 2003/06/17 追加終了
                return m_strRenrakusaki2HyojiMeisho;
            }
        }
        #endregion

        #region  コンストラクタ 
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {
            // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
            m_blnBatchRdb = false;
            // ＲＤＢクラスのインスタンス化
            m_cfRdbClass = new UFRdbClass(THIS_BUSINESSID);
            Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, true);
            // * 履歴番号 000024 2005/01/25 追加終了

            // * 履歴番号 000024 2005/01/25 削除開始（宮沢）
            // ' メンバ変数セット
            // m_cfControlData = cfControlData
            // m_cfConfigDataClass = cfConfigDataClass

            // ' ＲＤＢクラスのインスタンス化
            // m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)

            // ' ログ出力クラスのインスタンス化
            // m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

            // ' パラメータのメンバ変数初期化
            // m_intHyojiketaJuminCD = 0                           '住民コード表示桁数
            // m_intHyojiketaStaiCD = 0                            '世帯コード表示桁数
            // m_intHyojiketaJushoCD = 0                           '住所コード表示桁数（管内のみ）
            // m_intHyojiketaGyoseikuCD = 0                        '行政区コード表示桁数
            // m_intHyojiketaChikuCD1 = 0                          '地区コード１表示桁数
            // m_intHyojiketaChikuCD2 = 0                          '地区コード２表示桁数
            // m_intHyojiketaChikuCD3 = 0                          '地区コード３表示桁数
            // m_strChikuCD1HyojiMeisho = String.Empty             '地区コード１表示名称
            // m_strChikuCD2HyojiMeisho = String.Empty             '地区コード２表示名称
            // m_strChikuCD3HyojiMeisho = String.Empty             '地区コード３表示名称
            // m_strRenrakusaki1HyojiMeisho = String.Empty         '連絡先１表示名称
            // m_strRenrakusaki2HyojiMeisho = String.Empty         '連絡先２表示名称
            // '* 履歴番号 000014 2003/06/17 追加開始
            // ' 管理情報取得済みフラグの初期化
            // m_blnKanriJoho = False
            // '* 履歴番号 000014 2003/06/17 追加終了
            // '* 履歴番号 000015 2003/08/21 追加開始
            // m_blnBatch = False                                  ' バッチ区分
            // '* 履歴番号 000015 2003/08/21 追加終了
            // m_blnBatchRdb = False

            // '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // '宛名履歴マスタＤＡクラスのインスタンス作成
            // m_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // '宛名マスタＤＡクラスのインスタンス作成
            // m_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // '送付先マスタＤＡクラスのインスタンス作成
            // m_cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // '代納マスタＤＡクラスのインスタンス作成
            // m_cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // m_cUSSCityInfoClass = New USSCityInfoClass()
            // m_cUSSCityInfoClass.GetCityInfo(m_cfControlData)
            // m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
            // '* 履歴番号 000023 2004/08/27 追加終了
            // * 履歴番号 000024 2005/01/25 削除終了
        }

        // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 　　                          ByVal blnSelectAll As Boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // *
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, bool blnSelectAll)
        {
            m_blnBatchRdb = false;
            // ＲＤＢクラスのインスタンス化
            m_cfRdbClass = new UFRdbClass(THIS_BUSINESSID);
            Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, blnSelectAll);
        }
        // * 履歴番号 000024 2005/01/25 追加終了

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // * 履歴番号 000015 2003/08/21 追加開始
            m_blnBatchRdb = true;                                  // バッチ区分
                                                                   // * 履歴番号 000015 2003/08/21 追加終了
            Initial(cfControlData, cfConfigDataClass, cfRdbClass, true);
        }
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 　　                          ByVal cfRdbClass As UFRdbClass, _
        // * 　　                          ByVal blnSelectAll As Boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass, bool blnSelectAll)
        {
            // * 履歴番号 000015 2003/08/21 追加開始
            m_blnBatchRdb = true;                                  // バッチ区分
                                                                   // * 履歴番号 000015 2003/08/21 追加終了
            Initial(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll);
        }
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           'Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　           '               ByVal cfConfigDataClass As UFConfigDataClass)
        // * 構文           Public Sub Initial(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass,
        // * 　　                          ByVal blnSelectAll as boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
        // Public Sub New(ByVal cfControlData As UFControlData, _
        // ByVal cfConfigDataClass As UFConfigDataClass, _
        // ByVal cfRdbClass As UFRdbClass)
        [SecuritySafeCritical]
        private void Initial(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass, bool blnSelectAll)
        {
            // * 履歴番号 000024 2005/01/25 更新終了
            m_cfRdbClass = cfRdbClass;

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数初期化
            m_intHyojiketaJuminCD = 0;                           // 住民コード表示桁数
            m_intHyojiketaStaiCD = 0;                            // 世帯コード表示桁数
            m_intHyojiketaJushoCD = 0;                           // 住所コード表示桁数（管内のみ）
            m_intHyojiketaGyoseikuCD = 0;                        // 行政区コード表示桁数
            m_intHyojiketaChikuCD1 = 0;                          // 地区コード１表示桁数
            m_intHyojiketaChikuCD2 = 0;                          // 地区コード２表示桁数
            m_intHyojiketaChikuCD3 = 0;                          // 地区コード３表示桁数
            m_strChikuCD1HyojiMeisho = string.Empty;             // 地区コード１表示名称
            m_strChikuCD2HyojiMeisho = string.Empty;             // 地区コード２表示名称
            m_strChikuCD3HyojiMeisho = string.Empty;             // 地区コード３表示名称
            m_strRenrakusaki1HyojiMeisho = string.Empty;         // 連絡先１表示名称
            m_strRenrakusaki2HyojiMeisho = string.Empty;         // 連絡先２表示名称
                                                                 // * 履歴番号 000014 2003/06/17 追加開始
                                                                 // 管理情報取得済みフラグの初期化
            m_blnKanriJoho = false;
            // * 履歴番号 000014 2003/06/17 追加終了
            // * 履歴番号 000015 2003/08/21 追加開始
            m_blnBatch = false;                                  // バッチ区分
                                                                 // * 履歴番号 000015 2003/08/21 追加終了

            // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // 宛名履歴マスタＤＡクラスのインスタンス作成

            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // m_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            if (blnSelectAll == true)
            {
                m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniAll;
            }
            else
            {
                m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniOnly;
            }
            m_cABAtenaRirekiB = new ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, true);
            m_cABAtenaRirekiBRef = m_cABAtenaRirekiB;
            // * 履歴番号 000024 2005/01/25 更新終了

            // 宛名マスタＤＡクラスのインスタンス作成
            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // m_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            m_cABAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, true);
            m_cABAtenaBRef = m_cABAtenaB;
            // * 履歴番号 000024 2005/01/25 更新終了

            // 送付先マスタＤＡクラスのインスタンス作成
            m_cABSfskB = new ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
            // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
            m_cABSfskBRef = m_cABSfskB;
            // * 履歴番号 000024 2005/01/25 追加終了(宮沢)
            // 代納マスタＤＡクラスのインスタンス作成
            m_cABDainoB = new ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
            // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
            m_cABDainoBRef = m_cABDainoB;
            // * 履歴番号 000024 2005/01/25 追加終了(宮沢)

            m_cUSSCityInfoClass = new USSCityInfoClass();
            m_cUSSCityInfoClass.GetCityInfo(m_cfControlData);
            m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
            // * 履歴番号 000023 2004/08/27 追加終了

            // * 履歴番号 000026 2005/04/21 追加開始
            m_strSystemDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd");    // 処理日時
                                                                                        // * 履歴番号 000026 2005/04/21 追加終了

            // *履歴番号 000032 2007/09/04 追加開始
            // UR管理情報を取得
            if (m_cURKanriJohoB is null)
            {
                m_cURKanriJohoB = new URKANRIJOHOBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
            }
            // バッチから呼ばれた場合エラーが発生するため，コメントアウト
            // m_cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // *履歴番号 000032 2007/09/04 追加終了

        }
        #endregion

        #region  簡易宛名取得１(AtenaGet1) 
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得１
        // * 
        // * 構文           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        // *履歴番号 000020 2003/11/19 修正開始
        // Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        public DataSet AtenaGet1(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            // *履歴番号 000020 2003/11/19 修正開始

            // '*履歴番号 000020 2003/11/19 修正終了
            // Const THIS_METHOD_NAME As String = "AtenaGet1"
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
            // Dim csDataTable As DataTable
            // Dim csDataSet As DataSet
            // Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
            // Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
            // Dim cABSfskB As ABSfskBClass                        '送付先マスタＤＡクラス
            // Dim cABDainoB As ABDainoBClass                      '代納マスタＤＡクラス
            // '*履歴番号 000015 2003/08/21 削除開始
            // 'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
            // '*履歴番号 000015 2003/08/21 削除終了
            // Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaH As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaHS As DataSet                            '宛名情報(ABAtena1)
            // Dim csAtenaD As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaDS As DataSet                            '宛名情報(ABAtena1)
            // Dim strStaiCD As String                             '世帯コード
            // Dim intHyojiKensu As Integer                        '最大取得件数
            // Dim intGetCount As Integer                          '取得件数
            // Dim strKikanYM As String                            '期間年月
            // Dim strDainoKB As String                            '代納区分
            // Dim strGyomuCD As String                            '業務コード
            // Dim strGyomunaiSHU_CD As String                     '業務内種別コード
            // Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
            // Dim strShichosonCD As String                        '市町村コード

            // Try
            // ' デバッグ開始ログ出力
            // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // "【実行メソッド名:Connect】")
            // 'ＲＤＢ接続
            // m_cfRdbClass.Connect()

            // Try
            // '* 履歴番号 000014 2003/06/17 削除開始
            // '' 管理情報取得(内部処理)メソッドを実行する。
            // 'Me.GetKanriJoho()
            // '* 履歴番号 000014 2003/06/17 削除終了

            // 'パラメータチェック
            // Me.CheckColumnValue(cAtenaGetPara1)

            // '宛名履歴マスタＤＡクラスのインスタンス作成
            // cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // '宛名マスタＤＡクラスのインスタンス作成
            // cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // '送付先マスタＤＡクラスのインスタンス作成
            // cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // '代納マスタＤＡクラスのインスタンス作成
            // cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''宛名編集クラスのインスタンス作成
            // 'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // If (m_blnBatch) Then
            // '宛名編集バッチクラスのインスタンス作成
            // m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // Else
            // '宛名編集クラスのインスタンス作成
            // m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // '*履歴追加 000003 2003/02/26 追加開始
            // 'USSCityInfoClass.GetCityInfo()を使用して、直近市町村情報取得を取得する。
            // cUSSCityInfoClass.GetCityInfo(m_cfControlData)

            // '市町村コードの内容を設定する。
            // If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
            // strShichosonCD = cUSSCityInfoClass.p_strShichosonCD(0)
            // Else
            // strShichosonCD = cAtenaGetPara1.p_strShichosonCD
            // End If
            // '*履歴追加 000003 2003/02/26 追加終了

            // '世帯コードの指定がなく、世帯員編集の指示がある場合
            // If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

            // '宛名検索キーのインスタンス化
            // cSearchKey = New ABAtenaSearchKey()

            // '住民コードの設定
            // cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名履歴マスタ抽出」メゾットを実行する
            // csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数が１件でない場合、エラー
            // If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
            // End If

            // '指定年月日が指定されていない場合
            // If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数が１件でない場合、エラー
            // If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // '世帯コードがNULLの場合、エラー
            // If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
            // End If
            // cAtenaGetPara1.p_strStaiCD = strStaiCD
            // cAtenaGetPara1.p_strJuminCD = String.Empty
            // End If

            // cSearchKey = Nothing
            // cSearchKey = New ABAtenaSearchKey()

            // '世帯員編集が"1"の場合
            // If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
            // cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            // Else
            // '宛名取得パラメータから宛名検索キーにセットする
            // cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
            // cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            // cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
            // cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
            // cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
            // cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
            // cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
            // cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
            // cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
            // cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
            // cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
            // cSearchKey.p_strShichosonCD = strShichosonCD
            // End If

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '住所～番地コード3のセット
            // '住登外優先の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
            // cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
            // cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
            // cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
            // cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
            // cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
            // cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
            // cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
            // End If

            // '住基優先の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // '*履歴番号 000018 2003/10/30 修正開始
            // 'cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
            // cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
            // '*履歴番号 000018 2003/10/30 修正終了
            // cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
            // cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
            // cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
            // cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
            // cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
            // cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
            // cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
            // End If

            // '最大取得件数をセットする
            // If cAtenaGetPara1.p_intHyojiKensu = 0 Then
            // intHyojiKensu = 100
            // Else
            // intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
            // End If

            // '指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名履歴マスタ抽出」メゾットを実行する
            // csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「履歴編集」メソッドを実行する
            // 'csAtenaH = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了
            // End If

            // '指定年月日が指定されていない場合
            // If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「宛名編集」メソッドを実行する
            // 'csAtenaH = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「宛名編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // End If

            // '取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
            // If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

            // csAtena1 = csAtenaH

            // Exit Try
            // End If

            // '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
            // If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // strKikanYM = "999999"
            // End If

            // '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
            // csDataSet = cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)


            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「送付先編集」メソッドを実行する
            // 'csAtenaHS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先編集」メソッドを実行する
            // csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // Else
            // '「宛名編集」の「送付先編集」メソッドを実行する
            // csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // '指定年月日が指定してある場合
            // If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // strKikanYM = "999999"
            // End If

            // '「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
            // csDataSet = cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数が1件でない場合
            // If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
            // Exit Try
            // End If

            // With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

            // '代納区分を退避する
            // strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

            // '業務コードを退避する
            // strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

            // '業務内種別コードを退避する
            // strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

            // '宛名検索キーにセットする
            // cSearchKey = Nothing
            // cSearchKey = New ABAtenaSearchKey()

            // cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

            // End With

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '⑯指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
            // '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
            // csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
            // Exit Try
            // End If

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「履歴編集」メソッドを実行する
            // 'csAtenaD = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // '                                        strGyomuCD, strGyomunaiSHU_CD)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // Else
            // '⑰指定年月日が指定されていない場合

            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
            // Exit Try
            // End If

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「宛名編集」メソッドを実行する
            // 'csAtenaD = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // '                                       strGyomuCD, strGyomunaiSHU_CD)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // End If

            // '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
            // If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // strKikanYM = "999999"
            // End If

            // '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
            // csDataSet = cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '*履歴番号 000015 2003/08/21 修正開始
            // ''「宛名編集」の「送付先編集」メソッドを実行する
            // 'csAtenaDS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // Else
            // '「宛名編集」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)

            // Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
            // "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            // ' UFAppExceptionをスローする
            // Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            // Catch
            // ' エラーをそのままスロー
            // Throw

            // Finally
            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // "【実行メソッド名:Disconnect】")
            // ' RDB切断
            // m_cfRdbClass.Disconnect()
            // End Try

            // ' デバッグ終了ログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            // Catch objAppExp As UFAppException
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
            // "【ワーニング内容:" + objAppExp.Message + "】")
            // ' エラーをそのままスローする
            // Throw objAppExp

            // Catch objExp As Exception
            // ' エラーログ出力
            // m_cfLogClass.ErrorWrite(m_cfControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // "【エラー内容:" + objExp.Message + "】")
            // Throw objExp
            // End Try

            // Return csAtena1

            return AtenaGet1(cAtenaGetPara1, false);
            // *履歴番号 000020 2003/11/19 修正終了

        }
        #endregion

        #region  簡易宛名取得１(AtenaGet1) 
        // *履歴番号 000020 2003/11/19 追加開始
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得１
        // * 
        // * 構文           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
        // * 
        // * 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaGet1(ABAtenaGetPara1XClass cAtenaGetPara1, bool blnKobetsu)
        {
            // *履歴番号 000030 2007/04/21 修正開始
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // Dim cSearchKey As ABAtenaSearchKey                  '宛名検索キー
            // Dim csDataTable As DataTable
            // Dim csDataSet As DataSet
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // 'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
            // 'Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
            // 'Dim cABSfskB As ABSfskBClass                        '送付先マスタＤＡクラス
            // 'Dim cABDainoB As ABDainoBClass                      '代納マスタＤＡクラス
            // '* 履歴番号 000023 2004/08/27 削除終了
            // Dim csAtena1 As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaH As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaHS As DataSet                            '宛名情報(ABAtena1)
            // Dim csAtenaD As DataSet                             '宛名情報(ABAtena1)
            // Dim csAtenaDS As DataSet                            '宛名情報(ABAtena1)
            // Dim strStaiCD As String                             '世帯コード
            // Dim intHyojiKensu As Integer                        '最大取得件数
            // Dim intGetCount As Integer                          '取得件数
            // Dim strKikanYM As String                            '期間年月
            // Dim strDainoKB As String                            '代納区分
            // Dim strGyomuCD As String                            '業務コード
            // Dim strGyomunaiSHU_CD As String                     '業務内種別コード
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // 'Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
            // '* 履歴番号 000023 2004/08/27 削除終了
            // Dim strShichosonCD As String                        '市町村コード

            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            // Dim csWkAtena As DataSet                             '宛名情報(ABAtena1)
            // '* 履歴番号 000024 2005/01/25 追加終了

            // Try
            // ' デバッグ開始ログ出力
            // m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // ' RDBアクセスログ出力
            // 'm_cfLogClass.RdbWrite(m_cfControlData, _
            // '                                "【クラス名:" + Me.GetType.Name + "】" + _
            // '                                "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // '                                "【実行メソッド名:Connect】")
            // '* 履歴番号 000023 2004/08/27 削除終了
            // 'ＲＤＢ接続
            // If m_blnBatchRdb = False Then
            // '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData, _
            // "【クラス名:" + Me.GetType.Name + "】" + _
            // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // "【実行メソッド名:Connect】")
            // '* 履歴番号 000023 2004/08/27 追加終了
            // m_cfRdbClass.Connect()
            // End If
            // Try
            // 'パラメータチェック
            // Me.CheckColumnValue(cAtenaGetPara1)
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // ''宛名履歴マスタＤＡクラスのインスタンス作成
            // 'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // ''宛名マスタＤＡクラスのインスタンス作成
            // 'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // ''送付先マスタＤＡクラスのインスタンス作成
            // 'cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            // ''代納マスタＤＡクラスのインスタンス作成
            // 'cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // '* 履歴番号 000023 2004/08/27 削除開始

            // If (m_blnBatch) Then
            // If (m_cABBatchAtenaHenshuB Is Nothing) Then
            // '宛名編集バッチクラスのインスタンス作成
            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // End If
            // Else
            // If (m_cABAtenaHenshuB Is Nothing) Then
            // '宛名編集クラスのインスタンス作成
            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // End If
            // End If

            // 'USSCityInfoClass.GetCityInfo()を使用して、直近市町村情報取得を取得する。
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // 'cUSSCityInfoClass.GetCityInfo(m_cfControlData)
            // '* 履歴番号 000023 2004/08/27 削除終了

            // '市町村コードの内容を設定する。
            // If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
            // strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
            // Else
            // strShichosonCD = cAtenaGetPara1.p_strShichosonCD
            // End If

            // '世帯コードの指定がなく、世帯員編集の指示がある場合
            // If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

            // '宛名検索キーのインスタンス化
            // cSearchKey = New ABAtenaSearchKey

            // '住民コードの設定
            // cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名履歴マスタ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数が１件でない場合、エラー
            // If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
            // End If

            // '指定年月日が指定されていない場合
            // If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数が１件でない場合、エラー
            // If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // '世帯コードがNULLの場合、エラー
            // If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
            // 'エラー定義を取得
            // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
            // End If

            // strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
            // End If
            // cAtenaGetPara1.p_strStaiCD = strStaiCD
            // cAtenaGetPara1.p_strJuminCD = String.Empty
            // End If

            // cSearchKey = Nothing
            // cSearchKey = New ABAtenaSearchKey

            // '世帯員編集が"1"の場合
            // If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
            // cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            // Else
            // '宛名取得パラメータから宛名検索キーにセットする
            // cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
            // cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            // cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
            // cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
            // cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
            // cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
            // cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
            // cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
            // cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
            // cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
            // cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
            // cSearchKey.p_strShichosonCD = strShichosonCD
            // End If

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '住所～番地コード3のセット
            // '住登外優先の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
            // cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
            // cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
            // cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
            // cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
            // cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
            // cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
            // cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
            // End If

            // '住基優先の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
            // cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
            // cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
            // cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
            // cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
            // cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
            // cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
            // cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
            // End If

            // '最大取得件数をセットする
            // If cAtenaGetPara1.p_intHyojiKensu = 0 Then
            // intHyojiKensu = 100
            // Else
            // intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
            // End If

            // '指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then
            // '「宛名個別履歴データ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
            // End If
            // Else
            // '「宛名履歴マスタ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            // End If
            // End If
            // Else
            // '指定年月日が指定されていない場合

            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then
            // '「宛名個別情報抽出」メゾットを実行する
            // csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名個別編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「宛名個別編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
            // End If
            // Else
            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名編集」メソッドを実行する
            // csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
            // Else
            // '「宛名編集」の「宛名編集」メソッドを実行する
            // csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
            // End If

            // End If

            // End If

            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            // csWkAtena = csDataSet
            // '* 履歴番号 000024 2005/01/25 追加終了

            // '*履歴番号 000022 2003/12/02 追加開始
            // ' 連絡先編集処理

            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH)
            // Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH, csWkAtena)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // '*履歴番号 000022 2003/12/02 追加終了

            // '取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
            // If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

            // csAtena1 = csAtenaH

            // Exit Try
            // End If

            // '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
            // If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // '* 履歴番号 000026 2005/04/21 修正開始
            // strKikanYM = m_strSystemDateTime
            // ''''strKikanYM = "999999"
            // '* 履歴番号 000026 2005/04/21 修正終了
            // End If

            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // ''「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
            // 'csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // '                                   cAtenaGetPara1.p_strGyomuCD, _
            // '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // '                                   strKikanYM, _
            // '                                   cAtenaGetPara1.p_blnSakujoFG)
            // '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
            // If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
            // '送付先があるので読み込む
            // csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)
            // Else
            // '送付先が無いので、空のテーブル作成
            // csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
            // End If
            // '* 履歴番号 000024 2005/01/25 更新終了

            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then
            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先個別編集」メソッドを実行する
            // csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // Else
            // '「宛名編集」の「送付先個別編集」メソッドを実行する
            // csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // End If
            // Else
            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先編集」メソッドを実行する
            // csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // Else
            // '「宛名編集」の「送付先編集」メソッドを実行する
            // csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
            // End If
            // End If

            // '指定年月日が指定してある場合
            // If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // '* 履歴番号 000026 2005/04/21 修正開始
            // strKikanYM = m_strSystemDateTime
            // ''''strKikanYM = "999999"
            // '* 履歴番号 000026 2005/04/21 修正終了
            // End If

            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // ''「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
            // 'csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // '                                     cAtenaGetPara1.p_strGyomuCD, _
            // '                                     cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // '                                     strKikanYM, _
            // '                                     cAtenaGetPara1.p_blnSakujoFG)
            // '「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
            // If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0) Then
            // '代納があるので読み込む
            // csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)
            // Else
            // '代納が無いので、空のテーブル作成
            // csDataSet = m_cABDainoB.GetDainoSchemaBHoshu()
            // End If
            // '* 履歴番号 000024 2005/01/25 更新終了

            // '取得件数が1件でない場合
            // If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)

            // Exit Try
            // End If

            // With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

            // '代納区分を退避する
            // strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

            // '業務コードを退避する
            // strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

            // '業務内種別コードを退避する
            // strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

            // '宛名検索キーにセットする
            // cSearchKey = Nothing
            // cSearchKey = New ABAtenaSearchKey

            // cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

            // End With

            // '住基・住登外区分が<>"1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
            // cSearchKey.p_strJutogaiYusenKB = "1"
            // End If

            // '住基・住登外区分が="1"の場合
            // If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
            // cSearchKey.p_strJuminYuseniKB = "1"
            // End If

            // '⑯指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then

            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then

            // '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
            // csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
            // Exit Try
            // End If

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴個別編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「履歴個別編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If
            // Else
            // '「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
            // csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, _
            // cAtenaGetPara1.p_strShiteiYMD, _
            // cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
            // Exit Try
            // End If

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If
            // End If
            // Else

            // '⑰指定年月日が指定されていない場合
            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then

            // '「宛名個別データ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
            // Exit Try
            // End If

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If

            // Else

            // '「宛名マスタ抽出」メゾットを実行する
            // csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            // '取得件数
            // intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
            // '取得件数が０件の場合、
            // If (intGetCount = 0) Then

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
            // Exit Try
            // End If

            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // Else
            // '「宛名編集」の「宛名編集」メソッドを実行する
            // csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
            // strGyomuCD, strGyomunaiSHU_CD)
            // End If
            // End If
            // End If

            // '指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
            // If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
            // strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
            // Else
            // '* 履歴番号 000026 2005/04/21 修正開始
            // strKikanYM = m_strSystemDateTime
            // ''''strKikanYM = "999999"
            // '* 履歴番号 000026 2005/04/21 修正終了
            // End If

            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // '「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
            // 'csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
            // '                                   cAtenaGetPara1.p_strGyomuCD, _
            // '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // '                                   strKikanYM, _
            // '                                   cAtenaGetPara1.p_blnSakujoFG)
            // If (csDataSet.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
            // '送付先があるので読み込む
            // csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
            // cAtenaGetPara1.p_strGyomuCD, _
            // cAtenaGetPara1.p_strGyomunaiSHU_CD, _
            // strKikanYM, _
            // cAtenaGetPara1.p_blnSakujoFG)
            // Else
            // '送付先が無いので、空のテーブル作成
            // csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
            // End If
            // '* 履歴番号 000024 2005/01/25 更新終了

            // ' 宛名個別情報の場合
            // If (blnKobetsu) Then
            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // Else
            // '「宛名編集」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // End If
            // Else
            // If (m_blnBatch) Then
            // '「宛名編集バッチ」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // Else
            // '「宛名編集」の「送付先編集」メソッドを実行する
            // csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
            // End If
            // End If

            // 'csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
            // csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)



            // Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData, _
            // "【クラス名:" + Me.GetType.Name + "】" + _
            // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
            // "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            // ' UFAppExceptionをスローする
            // Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            // Catch
            // ' エラーをそのままスロー
            // Throw

            // Finally
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // ' RDBアクセスログ出力
            // 'm_cfLogClass.RdbWrite(m_cfControlData, _
            // '                        "【クラス名:" + Me.GetType.Name + "】" + _
            // '                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // '                        "【実行メソッド名:Disconnect】")
            // '* 履歴番号 000023 2004/08/27 削除終了
            // ' RDB切断
            // If m_blnBatchRdb = False Then
            // '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData, _
            // "【クラス名:" + Me.GetType.Name + "】" + _
            // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // "【実行メソッド名:Disconnect】")
            // '* 履歴番号 000023 2004/08/27 追加終了
            // m_cfRdbClass.Disconnect()
            // End If
            // End Try

            // ' デバッグ終了ログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            // Catch objAppExp As UFAppException
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData, _
            // "【クラス名:" + Me.GetType.Name + "】" + _
            // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
            // "【ワーニング内容:" + objAppExp.Message + "】")
            // ' エラーをそのままスローする
            // Throw objAppExp

            // Catch objExp As Exception
            // ' エラーログ出力
            // m_cfLogClass.ErrorWrite(m_cfControlData, _
            // "【クラス名:" + Me.GetType.Name + "】" + _
            // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            // "【エラー内容:" + objExp.Message + "】")
            // Throw objExp
            // End Try

            // Return csAtena1

            return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Tsujo);
            // *履歴番号 000030 2007/04/21 修正終了

        }
        // *履歴番号 000020 2003/11/19 追加終了
        #endregion

        // *履歴番号 000030 2007/04/21 追加開始
        #region  宛名取得メイン（簡易宛名取得１、介護用宛名取得） 
        // ************************************************************************************************
        // * メソッド名     宛名取得メイン（簡易宛名取得１、介護用宛名取得）
        // * 
        // * 構文           Public Function AtenaGetMain(ByVal cAtenaGetPara1 As ABAtenaGetPara1, _
        // *                    ByVal blnKobetsu As Boolean, ByVal MethodKB As ABEnumDefine.MethodKB) As DataSet
        // *
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
        // * 　　           MethodKB         : callされたメソッドの種類を表す
        // * 
        // * 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
        // ************************************************************************************************
        private DataSet AtenaGetMain(ABAtenaGetPara1XClass cAtenaGetPara1, bool blnKobetsu, ABEnumDefine.MethodKB blnMethodKB, ABEnumDefine.HyojunKB intHyojunKB)
        {
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            ABAtenaSearchKey cSearchKey;                  // 宛名検索キー
                                                          // * corresponds to VS2008 Start 2010/04/16 000044
                                                          // Dim csDataTable As DataTable
                                                          // * corresponds to VS2008 End 2010/04/16 000044
            DataSet csDataSet;
            var csAtena1 = default(DataSet);                             // 宛名情報(ABAtena1)
            DataSet csAtenaH;                             // 宛名情報(ABAtena1)
            DataSet csAtenaHS;                            // 宛名情報(ABAtena1)
            var csAtenaD = default(DataSet);                             // 宛名情報(ABAtena1)
            var csAtenaDS = default(DataSet);                            // 宛名情報(ABAtena1)
            var strStaiCD = default(string);                             // 世帯コード
            int intHyojiKensu;                        // 最大取得件数
            int intGetCount;                          // 取得件数
            string strKikanYMD;                           // 期間年月日
            string strDainoKB;                            // 代納区分
            string strGyomuCD;                            // 業務コード
            string strGyomunaiSHU_CD;                     // 業務内種別コード
            string strShichosonCD;                        // 市町村コード
            DataSet csWkAtena;                             // 宛名情報(ABAtena1)

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // =====================================================================================================================
                // == １．ＲＤＢ接続
                // ==　　　　
                // ==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
                // ==　　　　
                // =====================================================================================================================
                if (m_blnBatchRdb == false)
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:Connect】");
                    m_cfRdbClass.Connect();
                }

                do
                {
                    try
                    {
                        // =====================================================================================================================
                        // == ２．宛名取得パラメータチェック
                        // ==　　　　
                        // ==　　　　<説明>　パラメータクラスに指定された内容をチェックする。
                        // ==　　　　
                        // =====================================================================================================================
                        CheckColumnValue(cAtenaGetPara1, intHyojunKB);

                        // =====================================================================================================================
                        // == ３．各種クラスのインスタンス化
                        // ==　　　　
                        // ==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
                        // ==　　　　
                        // =====================================================================================================================
                        if (m_blnBatch)
                        {
                            if (m_cABBatchAtenaHenshuB is null)
                            {
                                // 宛名編集バッチクラスのインスタンス作成
                                m_cABBatchAtenaHenshuB = new ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                                m_cABBatchAtenaHenshuB.m_blnMethodKB = blnMethodKB;               // 宛名編集Ｂクラス
                            }
                            m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                        }
                        else
                        {
                            if (m_cABAtenaHenshuB is null)
                            {
                                // 宛名編集クラスのインスタンス作成
                                m_cABAtenaHenshuB = new ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                                // 実行メソッドにより出力レイアウトを変更する
                                m_cABAtenaHenshuB.m_blnMethodKB = blnMethodKB;               // 宛名編集Ｂクラス
                            }
                            m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                        }
                        // 実行メソッドにより出力レイアウトを変更する
                        m_cABAtenaB.m_blnMethodKB = blnMethodKB;                             // 宛名Ｂクラス
                        m_cABAtenaRirekiB.m_blnMethodKB = blnMethodKB;                      // 宛名履歴Ｂクラス
                        m_cABAtenaB.m_intHyojunKB = intHyojunKB;
                        m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB;

                        // *履歴番号 000042 2008/11/18 追加開始
                        m_blnMethodKB = blnMethodKB;
                        // *履歴番号 000042 2008/11/18 追加終了

                        // *履歴番号 000045 2010/05/17 追加開始
                        // 宛名Ｂクラス各種プロパティをセット
                        m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB;
                        m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB;
                        // *履歴番号 000046 2011/05/18 追加開始
                        m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB;
                        // *履歴番号 000046 2011/05/18 追加終了
                        // *履歴番号 000047 2011/11/07 追加開始
                        m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB;
                        // *履歴番号 000047 2011/11/07 追加終了
                        // *履歴番号 000048 2014/04/28 追加開始
                        m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB;
                        // *履歴番号 000048 2014/04/28 追加終了

                        // 宛名履歴Ｂクラス各種プロパティをセット
                        m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB;
                        m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB;
                        // *履歴番号 000046 2011/05/18 追加開始
                        m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB;
                        // *履歴番号 000046 2011/05/18 追加終了
                        // *履歴番号 000047 2011/11/07 追加開始
                        m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB;
                        // *履歴番号 000047 2011/11/07 追加終了
                        // *履歴番号 000045 2010/05/17 追加終了
                        // *履歴番号 000048 2014/04/28 追加開始
                        m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB;
                        // *履歴番号 000048 2014/04/28 追加終了

                        // =====================================================================================================================
                        // == ４．市町村コード設定
                        // ==　　　　
                        // ==　　　　<説明>　＠市町村コードの指定がない場合は、現在(直近)の市町村コードを設定する。
                        // ==　　　　
                        // =====================================================================================================================
                        if (cAtenaGetPara1.p_strShichosonCD == string.Empty)
                        {
                            strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0);
                        }
                        else
                        {
                            strShichosonCD = cAtenaGetPara1.p_strShichosonCD;
                        }


                        // =====================================================================================================================
                        // == ５．世帯員編集時の世帯コードを取得
                        // ==　　　　
                        // ==　　　　<説明>　＠世帯員編集の指定がある場合は、＠世帯コードを使用し世帯員を取得する。
                        // ==　　　　　　　　＠世帯コードが指定されていなかった場合は＠住民コードにより世帯コードの取得を行う。
                        // ==　　　　
                        // =====================================================================================================================
                        // 世帯コードの指定がなく、世帯員編集の指示がある場合
                        if (cAtenaGetPara1.p_strStaiCD == "" & cAtenaGetPara1.p_strStaiinHenshu == "1")
                        {

                            // 宛名検索キーのインスタンス化
                            cSearchKey = new ABAtenaSearchKey();

                            // 住民コードの設定
                            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD;

                            // 住基・住登外区分が<>"1"の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB != "1")
                            {
                                cSearchKey.p_strJutogaiYusenKB = "1";
                            }

                            // 住基・住登外区分が="1"の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                            {
                                cSearchKey.p_strJuminYuseniKB = "1";
                            }

                            // 指定年月日が指定されている場合
                            if (!(cAtenaGetPara1.p_strShiteiYMD == string.Empty))
                            {

                                // 「宛名履歴マスタ抽出」メゾットを実行する
                                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG);

                                // 取得件数が１件でない場合、エラー
                                if (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count != 1)
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode);
                                }

                                strStaiCD = (string)csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)(ABAtenaRirekiEntity.STAICD);
                            }

                            // 指定年月日が指定されていない場合
                            if (cAtenaGetPara1.p_strShiteiYMD == string.Empty)
                            {

                                // 「宛名マスタ抽出」メゾットを実行する
                                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG);

                                // 取得件数が１件でない場合、エラー
                                if (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count != 1)
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode);
                                }

                                // 世帯コードがNULLの場合、エラー
                                if (new string(((string)csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD)).Trim ?? new char[0]) == "")
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode);
                                }

                                strStaiCD = (string)csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD);
                            }
                            cAtenaGetPara1.p_strStaiCD = strStaiCD;
                            cAtenaGetPara1.p_strJuminCD = string.Empty;
                        }



                        // *履歴番号 000031 2007/07/28 追加開始
                        // =====================================================================================================================
                        // == ６．同一人代表者取得処理
                        // ==　　　　
                        // ==　　　　<説明>　住民コード・住登外優先・同一人判定FG有効の検索条件の場合のみ、同一人代表者取得を行う。
                        // ==　　　　　　　　管理情報により、ユーザごとの取得判定有り。
                        // ==　　　　
                        // =====================================================================================================================
                        // 同一人代表者住民コードを検索パラメータに上書きする
                        GetDaihyoJuminCD(ref cAtenaGetPara1);
                        // *履歴番号 000031 2007/07/28 追加終了



                        // =====================================================================================================================
                        // == ７．本人宛名取得検索キーの設定
                        // ==　　　　
                        // ==　　　　<説明>　本人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
                        // ==　　　　　　　　最大取得件数も取得する。
                        // ==　　　　
                        // =====================================================================================================================
                        // 検索キークラスの初期化とインスタンス化
                        cSearchKey = default;
                        cSearchKey = new ABAtenaSearchKey();

                        // 世帯員編集が"1"の場合
                        if (cAtenaGetPara1.p_strStaiinHenshu == "1")
                        {
                            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD;
                        }
                        else
                        {
                            // 宛名取得パラメータから宛名検索キーにセットする
                            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD;
                            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD;
                            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei;
                            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei;
                            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei;
                            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei;
                            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD;
                            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu;
                            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB;
                            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1;
                            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2;
                            cSearchKey.p_strShichosonCD = strShichosonCD;

                            // *履歴番号 000032 2007/09/04 追加開始
                            // 検索用カナ姓名・検索用カナ姓・検索用カナ名の編集
                            cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen);
                            // *履歴番号 000032 2007/09/04 追加終了

                            // *履歴番号 000048 2014/04/28 追加開始
                            cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13);
                            cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB;
                            cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB;
                            // *履歴番号 000048 2014/04/28 追加終了
                            cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji;
                            cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji;
                            cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei;
                            cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho;
                            cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki;
                            cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki;
                        }

                        // 住基・住登外区分が<>"1"の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB != "1")
                        {
                            cSearchKey.p_strJutogaiYusenKB = "1";
                        }

                        // 住基・住登外区分が="1"の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            cSearchKey.p_strJuminYuseniKB = "1";
                        }

                        // 住所～番地コード3のセット
                        // 住登外優先の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB != "1")
                        {
                            cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD;
                            cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9);
                            cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8);
                            cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8);
                            cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8);
                            cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5);
                            cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5);
                            cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5);
                        }

                        // 住基優先の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8);
                            cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9);
                            cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8);
                            cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8);
                            cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8);
                            cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5);
                            cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5);
                            cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5);
                        }

                        // *履歴番号 000049 2018/03/08 追加開始
                        // 履歴検索フラグ
                        cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch;
                        // *履歴番号 000049 2018/03/08 追加終了

                        // 最大取得件数をセットする
                        if (cAtenaGetPara1.p_intHyojiKensu == 0)
                        {
                            intHyojiKensu = 100;
                        }
                        else
                        {
                            intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu;
                        }


                        // =====================================================================================================================
                        // == ８．本人宛名データの取得
                        // ==　　　　
                        // ==　　　　<説明>　本人の宛名情報を取得する。
                        // ==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                        // ==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                        // ==　　　　　　　　ⅲ. 個別事項ＦＧの指定がある場合は個別事項データも取得する
                        // ==　　　　　　　　ⅳ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定されている場合
                        if (!(cAtenaGetPara1.p_strShiteiYMD == string.Empty))
                        {

                            // 宛名個別情報の場合
                            if (blnKobetsu)
                            {
                                // *履歴番号 000038 2008/01/17 修正開始
                                // 「宛名個別履歴データ抽出」メゾットを実行する
                                // csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
                                // cSearchKey, _
                                // cAtenaGetPara1.p_strShiteiYMD, _
                                // cAtenaGetPara1.p_blnSakujoFG)
                                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB);
                                // *履歴番号 000038 2008/01/17 修正終了

                                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count;

                                if (m_blnBatch)
                                {
                                    // 「宛名編集バッチ」の「履歴編集」メソッドを実行する
                                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet);
                                }
                                else
                                {
                                    // 「宛名編集」の「履歴編集」メソッドを実行する
                                    csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet);
                                }
                            }
                            else
                            {
                                // 「宛名履歴マスタ抽出」メゾットを実行する
                                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG);

                                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count;

                                if (m_blnBatch)
                                {
                                    // 「宛名編集バッチ」の「履歴編集」メソッドを実行する
                                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet);
                                }
                                else
                                {
                                    // 「宛名編集」の「履歴編集」メソッドを実行する
                                    csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet);
                                }
                            }
                        }
                        // 指定年月日が指定されていない場合

                        // 宛名個別情報の場合
                        else if (blnKobetsu)
                        {
                            // *履歴番号 000038 2008/01/17 修正開始
                            // 「宛名個別情報抽出」メソッドを実行する
                            // csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                            csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB);
                            // *履歴番号 000038 2008/01/17 修正終了

                            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count;

                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「宛名個別編集」メソッドを実行する
                                csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet);
                            }
                            else
                            {
                                // 「宛名編集」の「宛名個別編集」メソッドを実行する
                                csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet);
                            }
                        }
                        else
                        {
                            // 「宛名マスタ抽出」メゾットを実行する
                            csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG);

                            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count;

                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「宛名編集」メソッドを実行する
                                csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet);
                            }
                            else
                            {
                                // 「宛名編集」の「宛名編集」メソッドを実行する
                                csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet);
                            }


                        }

                        csWkAtena = csDataSet;

                        // *履歴番号 000040 2008/11/10 追加開始
                        // =====================================================================================================================
                        // == ９．利用届データの取得
                        // ==　　　　
                        // ==　　　　<説明>　利用届データの取得
                        // ==　　　　　　　　ⅰ. 標準レイアウトの場合かつ、宛名個別情報以外の場合に処理を行う
                        // ==　　　　　　　　ⅱ. 利用届出取得区分が"1,2"の場合に処理を行う
                        // ==　　　　　　　　ⅲ. 住民コード、税目区分などから利用届データを取得し、納税者ID、利用者IDにセットする
                        // ==　　　　
                        // =====================================================================================================================
                        RiyoTdkHenshu(cAtenaGetPara1, blnKobetsu, ref csAtenaH);

                        // *履歴番号 000041 2008/11/17 追加開始
                        // 利用届区分が"2"の場合、該当データ以外が削除されるので新規件数をセットする
                        if (cAtenaGetPara1.p_strTdkdKB == "2")
                        {
                            intGetCount = csAtenaH.Tables[0].Rows.Count;
                        }
                        else
                        {
                        }
                        // *履歴番号 000041 2008/11/17 追加終了
                        // *履歴番号 000040 2008/11/10 追加終了

                        // =====================================================================================================================
                        // == １０．連絡先データの取得
                        // ==　　　　
                        // ==　　　　<説明>　連絡先情報を取得する。
                        // ==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
                        // ==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
                        // ==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
                        // ==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                        if (cAtenaGetPara1.p_strShiteiYMD != "" & cAtenaGetPara1.p_strSfskDataKB == "1")
                        {
                            strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8);
                        }
                        else
                        {
                            strKikanYMD = m_strSystemDateTime;
                        }
                        this.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, ref csAtenaH, ref csWkAtena, intHyojunKB, strKikanYMD);


                        // =====================================================================================================================
                        // == １１．代納・送付先データ取得の判定
                        // ==　　　　
                        // ==　　　　<説明>　＠業務コードの指定がない場合は、処理を強制的に終了する。
                        // ==　　　　　　　　本人データの取得件数が１件でない場合も処理を強制的に終了する。
                        // ==　　　　
                        // =====================================================================================================================
                        // 取得パラメータの業務コードが指定されていないか、取得件数が1件でない場合は、値を返す
                        if (cAtenaGetPara1.p_strGyomuCD == "" | intGetCount != 1)
                        {

                            csAtena1 = csAtenaH;

                            // 処理を終了する
                            break;
                        }


                        // =====================================================================================================================
                        // == １２．送付先データの抽出日を設定
                        // ==　　　　
                        // ==　　　　<説明>　送付先データの抽出において、＠指定日の指定があり、かつ＠送付先データ区分が "1" の場合は
                        // ==　　　　　　　　指定された日付が有効期間に含まれていることを条件とする。
                        // ==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                        if (cAtenaGetPara1.p_strShiteiYMD != "" & cAtenaGetPara1.p_strSfskDataKB == "1")
                        {
                            strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8);
                        }
                        else
                        {
                            strKikanYMD = m_strSystemDateTime;
                        }


                        // =====================================================================================================================
                        // == １３．送付先データの取得
                        // ==　　　　
                        // ==　　　　<説明>　送付先データの件数により、存在している場合のみ送付先データの取得を行う。
                        // ==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                        // ==　　　　
                        // =====================================================================================================================
                        // 「送付先マスタＤＡ」の「送付先マスタ抽出」メソッドを実行する
                        if (csWkAtena.Tables[0].Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0)
                        {
                            // 送付先があるので読み込む
                            if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cAtenaGetPara1.p_strJuminCD, cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, strKikanYMD, cAtenaGetPara1.p_blnSakujoFG);
                            }
                            else
                            {
                                csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, strKikanYMD, cAtenaGetPara1.p_blnSakujoFG);
                            }
                        }
                        // 送付先が無いので、空のテーブル作成
                        else if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun();
                        }
                        else
                        {
                            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu();
                        }


                        // =====================================================================================================================
                        // == １４．送付先データのレイアウト編集
                        // ==　　　　
                        // ==　　　　<説明>　個別事項ＦＧの指定がある場合は、送付先データを個別事項項目が付加されたレイアウトに編集する。
                        // ==　　　　　　　　また、バッチ版・リアル版により使用するクラスを分ける。
                        // ==　　　　
                        // =====================================================================================================================
                        // 宛名個別情報の場合
                        if (blnKobetsu)
                        {
                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「送付先個別編集」メソッドを実行する
                                csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet);
                            }
                            else
                            {
                                // 「宛名編集」の「送付先個別編集」メソッドを実行する
                                csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet);
                            }
                        }
                        else if (m_blnBatch)
                        {
                            // 「宛名編集バッチ」の「送付先編集」メソッドを実行する
                            csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet);
                        }
                        else
                        {
                            // 「宛名編集」の「送付先編集」メソッドを実行する
                            csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet);
                        }


                        // =====================================================================================================================
                        // == １５．代納データの抽出日を設定
                        // ==　　　　
                        // ==　　　　<説明>　代納データの抽出において、＠指定日の指定がある場合は、指定された日付が有効期間に含まれている
                        // ==　　　　　　　　ことを条件とする。
                        // ==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定してある場合
                        if (cAtenaGetPara1.p_strShiteiYMD != "")
                        {
                            strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8);
                        }
                        else
                        {
                            strKikanYMD = m_strSystemDateTime;
                        }


                        // =====================================================================================================================
                        // == １６．代納データの取得
                        // ==　　　　
                        // ==　　　　<説明>　代納データの件数により、存在している場合のみ代納データの取得を行う。
                        // ==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                        // ==　　　　
                        // =====================================================================================================================
                        // 「代納マスタＤＡ」の「代納マスタ抽出」メソッドを実行する
                        if (csWkAtena.Tables[0].Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0)
                        {
                            // 代納があるので読み込む
                            csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, strKikanYMD, cAtenaGetPara1.p_blnSakujoFG);
                        }
                        else
                        {
                            // 代納が無いので、空のテーブル作成
                            csDataSet = m_cABDainoB.GetDainoSchemaBHoshu();
                        }


                        // =====================================================================================================================
                        // == １７．取得データのマージ
                        // ==　　　　
                        // ==　　　　<説明>　代納データの取得件数が１件でない場合は、「本人」「送付先」「代納人」「代納送付先」データを
                        // ==　　　　　　　　１つのデータセットにマージし、処理を強制的に終了する。
                        // ==　　　　　　　　この時点では、「代納人」「代納送付先」データは空である。
                        // ==　　　　
                        // =====================================================================================================================
                        // 取得件数が1件でない場合
                        if (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count != 1)
                        {

                            // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                            csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);

                            // 処理を終了する
                            break;
                        }


                        // =====================================================================================================================
                        // == １８．代納人宛名取得検索キーの設定
                        // ==　　　　
                        // ==　　　　<説明>　代納人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
                        // ==　　　　　　　　この時、代納区分・業務コード・業務内種別コードを退避する。
                        // ==　　　　
                        // =====================================================================================================================
                        {
                            var withBlock = csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0);

                            // 代納区分を退避する
                            strDainoKB = (string)withBlock(ABDainoEntity.DAINOKB);

                            // 業務コードを退避する
                            strGyomuCD = (string)withBlock(ABDainoEntity.GYOMUCD);

                            // 業務内種別コードを退避する
                            strGyomunaiSHU_CD = (string)withBlock(ABDainoEntity.GYOMUNAISHU_CD);

                            // 宛名検索キーにセットする
                            cSearchKey = default;
                            cSearchKey = new ABAtenaSearchKey();

                            cSearchKey.p_strJuminCD = (string)withBlock(ABDainoEntity.DAINOJUMINCD);

                        }

                        // 住基・住登外区分が<>"1"の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB != "1")
                        {
                            cSearchKey.p_strJutogaiYusenKB = "1";
                        }

                        // 住基・住登外区分が="1"の場合
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            cSearchKey.p_strJuminYuseniKB = "1";
                        }


                        // =====================================================================================================================
                        // == １９．代納人宛名データの取得
                        // ==　　　　
                        // ==　　　　<説明>　代納人の宛名情報を取得する。
                        // ==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                        // ==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                        // ==　　　　　　　　ⅲ. 個別事項ＦＧの指定がある場合は個別事項データも取得する
                        // ==　　　　　　　　ⅳ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定されている場合
                        if (!(cAtenaGetPara1.p_strShiteiYMD == ""))
                        {

                            // 宛名個別情報の場合
                            if (blnKobetsu)
                            {

                                // *履歴番号 000038 2008/01/17 修正開始
                                // 「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
                                // csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                                // cSearchKey, _
                                // cAtenaGetPara1.p_strShiteiYMD, _
                                // cAtenaGetPara1.p_blnSakujoFG)
                                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB);
                                // *履歴番号 000038 2008/01/17 修正終了

                                // 取得件数
                                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count;
                                // 取得件数が０件の場合、
                                if (intGetCount == 0)
                                {

                                    // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                                    csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);

                                    // 処理を終了する
                                    break;
                                }

                                if (m_blnBatch)
                                {
                                    // 「宛名編集バッチ」の「履歴個別編集」メソッドを実行する
                                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                                }
                                else
                                {
                                    // 「宛名編集」の「履歴個別編集」メソッドを実行する
                                    csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                                }
                            }
                            else
                            {
                                // 「宛名履歴マスタＤＡ」の「宛名履歴マスタ抽出」メソッドを実行する
                                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG);
                                // 取得件数
                                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count;
                                // 取得件数が０件の場合、
                                if (intGetCount == 0)
                                {

                                    // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                                    csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);

                                    // 処理を終了する
                                    break;
                                }

                                if (m_blnBatch)
                                {
                                    // 「宛名編集バッチ」の「履歴編集」メソッドを実行する
                                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                                }
                                else
                                {
                                    // 「宛名編集」の「履歴編集」メソッドを実行する
                                    csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                                }
                            }
                        }

                        // ⑰指定年月日が指定されていない場合
                        // 宛名個別情報の場合
                        else if (blnKobetsu)
                        {

                            // *履歴番号 000038 2008/01/17 修正開始
                            // 「宛名個別データ抽出」メゾットを実行する
                            // csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                            // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                            csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB);
                            // *履歴番号 000038 2008/01/17 修正終了

                            // 取得件数
                            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count;
                            // 取得件数が０件の場合、
                            if (intGetCount == 0)
                            {

                                // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                                csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);

                                // 処理を終了する
                                break;
                            }

                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「宛名編集」メソッドを実行する
                                csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                            }
                            else
                            {
                                // 「宛名編集」の「宛名編集」メソッドを実行する
                                csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                            }
                        }

                        else
                        {

                            // 「宛名マスタ抽出」メゾットを実行する
                            csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG);

                            // 取得件数
                            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count;
                            // 取得件数が０件の場合、
                            if (intGetCount == 0)
                            {

                                // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                                csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);

                                // 処理を終了する
                                break;
                            }

                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「宛名編集」メソッドを実行する
                                csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                            }
                            else
                            {
                                // 「宛名編集」の「宛名編集」メソッドを実行する
                                csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, strGyomuCD, strGyomunaiSHU_CD);
                            }
                        }


                        // =====================================================================================================================
                        // == ２０．代納人送付先データの抽出日を設定
                        // ==　　　　
                        // ==　　　　<説明>　代納人の送付先データの抽出において、＠指定日の指定があり、かつ＠送付先データ区分が "1" の場合は
                        // ==　　　　　　　　指定された日付が有効期間に含まれていることを条件とする。
                        // ==　　　　　　　　上記以外は、システム日付が有効期間に含まれるていることを条件とする。
                        // ==　　　　
                        // =====================================================================================================================
                        // 指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                        if (cAtenaGetPara1.p_strShiteiYMD != "" & cAtenaGetPara1.p_strSfskDataKB == "1")
                        {
                            strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8);
                        }
                        else
                        {
                            strKikanYMD = m_strSystemDateTime;
                        }


                        // =====================================================================================================================
                        // == ２１．代納人送付先データの取得
                        // ==　　　　
                        // ==　　　　<説明>　代納人の送付先データの件数により、存在している場合のみ送付先データの取得を行う。
                        // ==　　　　　　　　取得を行わなかった場合は、空のテーブルを作成する。
                        // ==　　　　
                        // =====================================================================================================================
                        if (csDataSet.Tables[0].Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0)
                        {
                            // 送付先があるので読み込む
                            if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cSearchKey.p_strJuminCD, cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, strKikanYMD, cAtenaGetPara1.p_blnSakujoFG);
                            }
                            else
                            {
                                csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, strKikanYMD, cAtenaGetPara1.p_blnSakujoFG);
                            }
                        }
                        // 送付先が無いので、空のテーブル作成
                        else if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun();
                        }
                        else
                        {
                            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu();
                        }


                        // =====================================================================================================================
                        // == ２２．代納送付先データのレイアウト編集
                        // ==　　　　
                        // ==　　　　<説明>　個別事項ＦＧの指定がある場合は、送付先データを個別事項項目が付加されたレイアウトに編集する。
                        // ==　　　　　　　　また、バッチ版・リアル版により使用するクラスを分ける。
                        // ==　　　　
                        // =====================================================================================================================
                        // 宛名個別情報の場合
                        if (blnKobetsu)
                        {
                            if (m_blnBatch)
                            {
                                // 「宛名編集バッチ」の「送付先編集」メソッドを実行する
                                csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet);
                            }
                            else
                            {
                                // 「宛名編集」の「送付先編集」メソッドを実行する
                                csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet);
                            }
                        }
                        else if (m_blnBatch)
                        {
                            // 「宛名編集バッチ」の「送付先編集」メソッドを実行する
                            csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet);
                        }
                        else
                        {
                            // 「宛名編集」の「送付先編集」メソッドを実行する
                            csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet);
                        }


                        // =====================================================================================================================
                        // == ２３．取得データのマージ
                        // ==　　　　
                        // ==　　　　<説明>　「本人」「送付先」「代納人」「代納送付先」データを１つのデータセットにマージし処理を強制的に終了する。
                        // ==　　　　
                        // =====================================================================================================================
                        // csAtenaH と csAtenaHS をマージして、caAtena1 にセットする
                        csAtena1 = CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB);
                    }



                    catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                    {
                        // ワーニングログ出力
                        m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                        // UFAppExceptionをスローする
                        throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                    }
                    catch
                    {
                        // エラーをそのままスロー
                        throw;
                    }
                    finally
                    {

                        // =====================================================================================================================
                        // == ２４．ＲＤＢ切断
                        // ==　　　　
                        // ==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ切断を行わない制御を行う。
                        // ==　　　　
                        // =====================================================================================================================
                        // RDB切断
                        if (m_blnBatchRdb == false)
                        {
                            // RDBアクセスログ出力
                            m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:Disconnect】");
                            m_cfRdbClass.Disconnect();
                        }


                        // *履歴番号 000031 2007/07/30 修正開始
                        // =====================================================================================================================
                        // == ２５．返却する住民コードを指定された住民コードで上書きする
                        // ==　　　　
                        // ==　　　　<説明>　同一人代表者取得された場合は、指定された住民コードを返す
                        // ==　　　　
                        // =====================================================================================================================
                        // 退避した住民コードが存在する場合は、上書きする
                        SetJuminCD(ref csAtena1);
                    }
                }
                // *履歴番号 000031 2007/07/30 修正終了

                // *履歴番号 000041 2008/11/17 削除開始
                // '*履歴番号 000040 2008/11/10 追加開始
                // '=====================================================================================================================
                // '== ２６．利用届出データの絞込み
                // '==　　　　
                // '==　　　　<説明>　利用届出取得区分 = "2" の場合、返却データの納税者IDが存在しないレコードは返却しない
                // '==　　　　
                // '=====================================================================================================================
                // '退避した住民コードが存在する場合は、上書きする
                // RiyoTdkHenshu_Select(cAtenaGetPara1, blnKobetsu, csAtena1)
                // '*履歴番号 000040 2008/11/10 追加終了
                // *履歴番号 000041 2008/11/17 削除シュウリョう

                while (false);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

            return csAtena1;

        }
        #endregion
        // *履歴番号 000030 2007/04/21 追加終了

        // *履歴番号 000030 2007/04/21 追加開始
        #region  介護用宛名取得 
        // ************************************************************************************************
        // * メソッド名     介護用宛名取得
        // * 
        // * 構文           Public Function GetKaigoAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet : 取得した宛名情報
        // ************************************************************************************************
        public DataSet GetKaigoAtena(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            ABEnumDefine.AtenaGetKB blnAtenaSelectAll;
            // * corresponds to VS2008 Start 2010/04/16 000044
            // Dim blnAtenaKani As Boolean
            // Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
            // Dim blnRirekiKani As Boolean
            // * corresponds to VS2008 End 2010/04/16 000044
            DataSet csAtenaEntity;                        // 介護用宛名Entity

            try
            {
                // コンストラクタの設定を保存
                blnAtenaSelectAll = m_blnSelectAll;
                m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                if (m_cABAtenaB is not null)
                {
                    m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                }
                if (m_cABAtenaRirekiB is not null)
                {
                    m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                }

                // 宛名取得メインメソッドの呼出し（引数：取得パラメータクラス、個別事項データ取得フラグ、呼び出しメソッド区分）
                csAtenaEntity = AtenaGetMain(cAtenaGetPara1, false, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Tsujo);

                // コンストラクタの設定を元にもどす
                m_blnSelectAll = blnAtenaSelectAll;
                if (m_cABAtenaB is not null)
                {
                    m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll;
                }
                if (m_cABAtenaRirekiB is not null)
                {
                    m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll;
                }
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

            return csAtenaEntity;

        }
        #endregion
        // *履歴番号 000030 2007/04/21 追加終了

        #region  簡易宛名取得２(AtenaGet2) 
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得２
        // * 
        // * 構文           Public Function AtenaGet2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaGet2(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            const string THIS_METHOD_NAME = "AtenaGet2";
            DataSet csAtenaEntity;                        // 宛名Entity
                                                          // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
            var blnAtenaSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnAtenaKani = default(bool);
            var blnRirekiSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnRirekiKani = default(bool);
            // * 履歴番号 000024 2005/01/25 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:Connect】")
                // * 履歴番号 000023 2004/08/27 削除終了
                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    // * 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Connect();
                }

                try
                {
                    // * 履歴番号 000014 2003/06/17 削除開始
                    // ' 管理情報取得(内部処理)メソッドを実行する。
                    // Me.GetKanriJoho()
                    // * 履歴番号 000014 2003/06/17 削除終了

                    // * 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
                    // コンストラクタの設定を保存
                    if (m_cABAtenaB is not null)
                    {
                        blnAtenaSelectAll = m_cABAtenaB.m_blnSelectAll;
                        blnAtenaKani = m_cABAtenaB.m_blnSelectCount;
                        m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                        m_cABAtenaB.m_blnSelectCount = false;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        blnRirekiSelectAll = m_cABAtenaRirekiB.m_blnSelectAll;
                        blnRirekiKani = m_cABAtenaRirekiB.m_blnSelectCount;
                        m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = false;

                    }
                    // * 履歴番号 000024 2005/01/25 追加終了

                    // 簡易宛名取得２(内部処理)メソッドを実行する。
                    csAtenaEntity = this.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo);

                    // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
                    // コンストラクタの設定を元にもどす
                    if (m_cABAtenaB is not null)
                    {
                        m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll;
                        m_cABAtenaB.m_blnSelectCount = blnAtenaKani;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani;
                    }
                }
                // * 履歴番号 000024 2005/01/25 追加終了

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                    // RDBアクセスログ出力
                    // m_cfLogClass.RdbWrite(m_cfControlData, _
                    // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                    // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                    // "【実行メソッド名:Disconnect】")
                    // * 履歴番号 000023 2004/08/27 削除終了
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        // * 履歴番号 000023 2004/08/27 追加終了
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtenaEntity;

        }
        #endregion

        #region  管理情報取得(KanriJohoGet) 
        // ************************************************************************************************
        // * メソッド名     管理情報取得
        // * 
        // * 構文           Public Function KanriJohoGet()
        // * 
        // * 機能　　    　　管理情報を取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void KanriJohoGet()
        {
            const string THIS_METHOD_NAME = "KanriJohoGet";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000014 2003/06/17 追加開始
                if (m_blnKanriJoho)
                {
                    return;
                }
                // * 履歴番号 000014 2003/06/17 追加終了

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:Connect】")
                // * 履歴番号 000023 2004/08/27 削除終了
                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    // * 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Connect();
                }

                try
                {

                    // 管理情報取得(内部処理)メソッドを実行する。
                    GetKanriJoho();
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                    // RDBアクセスログ出力
                    // m_cfLogClass.RdbWrite(m_cfControlData, _
                    // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                    // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                    // "【実行メソッド名:Disconnect】")
                    // * 履歴番号 000023 2004/08/27 削除終了
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        // * 履歴番号 000023 2004/08/27 追加終了
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }
        }
        #endregion

        #region  年金宛名取得(NenkinAtenaGet) 
        // *履歴番号 000029 2006/07/31 追加開始
        // ************************************************************************************************
        // * メソッド名     年金宛名取得
        // * 
        // * 構文           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       年金宛名情報を取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet NenkinAtenaGet(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            // * corresponds to VS2008 Start 2010/04/16 000044
            // Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
            // * corresponds to VS2008 End 2010/04/16 000044
            // 年金宛名ゲットより年金宛名情報を取得する
            return NenkinAtenaGet(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01);
        }
        // *履歴番号 000029 2006/07/31 追加終了
        #endregion

        #region  年金宛名取得(NenkinAtenaGet) 
        // ************************************************************************************************
        // * メソッド名     年金宛名取得
        // * 
        // * 構文           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       年金宛名情報を取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        // *履歴番号 000029 2006/07/31 修正開始
        public DataSet NenkinAtenaGet(ABAtenaGetPara1XClass cAtenaGetPara1, int intNenkinAtenaGetKB)
        {
            // Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
            // 'Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
            // '    Const THIS_METHOD_NAME As String = "KanriJohoGet"
            // '*履歴番号 000029 2006/07/31 修正終了
            // '*履歴番号 000015 2003/08/21 削除開始
            // 'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
            // '*履歴番号 000015 2003/08/21 削除終了
            // Dim csAtenaEntity As DataSet                        '宛名Entity
            // Dim csAtena1Entity As DataSet                       '宛名1Entity
            // '*履歴番号 000022 2003/12/02 追加開始
            // Dim cAtenaGetPara1Save As New ABAtenaGetPara1XClass     ' 退避用
            // '*履歴番号 000022 2003/12/02 追加終了

            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            // Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
            // Dim blnAtenaKani As Boolean
            // Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
            // Dim blnRirekiKani As Boolean
            // '* 履歴番号 000024 2005/01/25 追加終了

            // Try
            // ' デバッグログ出力
            // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            // '=====================================================================================================================
            // '== １．ＲＤＢ接続
            // '==　　　　
            // '==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
            // '==　　　　
            // '=====================================================================================================================
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // ' RDBアクセスログ出力
            // 'm_cfLogClass.RdbWrite(m_cfControlData, _
            // '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // '                                "【実行メソッド名:Connect】")
            // '* 履歴番号 000023 2004/08/27 削除終了
            // 'ＲＤＢ接続
            // If m_blnBatchRdb = False Then
            // '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData,
            // "【クラス名:" + THIS_CLASS_NAME + "】" +
            // "【メソッド名:" + THIS_METHOD_NAME + "】" +
            // "【実行メソッド名:Connect】")
            // '* 履歴番号 000023 2004/08/27 追加終了
            // m_cfRdbClass.Connect()
            // End If

            // Try
            // '=====================================================================================================================
            // '== ２．各種クラスのインスタンス化
            // '==　　　　
            // '==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
            // '==　　　　
            // '=====================================================================================================================
            // '*履歴番号 000015 2003/08/21 修正開始
            // ''宛名編集クラスのインスタンス作成
            // 'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // If (m_blnBatch) Then
            // If (m_cABBatchAtenaHenshuB Is Nothing) Then
            // '宛名編集バッチクラスのインスタンス作成
            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // End If
            // Else
            // If (m_cABAtenaHenshuB Is Nothing) Then
            // '宛名編集クラスのインスタンス作成
            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            // m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // End If
            // End If
            // '*履歴番号 000015 2003/08/21 修正終了

            // '*履歴番号 000045 2010/05/17 追加開始
            // ' 宛名Ｂクラス各種プロパティをセット
            // m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
            // m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
            // '*履歴番号 000046 2011/05/18 追加開始
            // m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
            // '*履歴番号 000046 2011/05/18 追加終了

            // ' 宛名履歴Ｂクラス各種プロパティをセット
            // m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
            // m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
            // '*履歴番号 000046 2011/05/18 追加開始
            // m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
            // '*履歴番号 000046 2011/05/18 追加終了
            // '*履歴番号 000045 2010/05/17 追加終了


            // '=====================================================================================================================
            // '== ３．コンストラクタの設定を保存
            // '==　　　　
            // '==　　　　<説明>　簡易版・通常版の情報を保存する。
            // '==　　　　
            // '=====================================================================================================================
            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
            // 'コンストラクタの設定を保存
            // If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
            // Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            // End If
            // If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
            // Me.m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            // End If
            // '* 履歴番号 000024 2005/01/25 追加終了（宮沢）



            // '=====================================================================================================================
            // '== ４．管理情報の取得
            // '==　　　　
            // '==　　　　<説明>　各種管理情報の取得を行う。
            // '==　　　　
            // '=====================================================================================================================
            // ' 管理情報取得(内部処理)メソッドを実行する。
            // Me.GetKanriJoho()



            // '=====================================================================================================================
            // '== ５．業務コードの退避
            // '==　　　　
            // '==　　　　<説明>　業務コード・業務内種別コードを退避する。
            // '==　　　　
            // '=====================================================================================================================
            // '*履歴番号 000022 2003/12/02 追加開始
            // ' 業務コード・業務内種別コードを退避する
            // cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD
            // cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD
            // cAtenaGetPara1.p_strGyomuCD = String.Empty
            // cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty
            // '*履歴番号 000022 2003/12/02 追加終了



            // '=====================================================================================================================
            // '== ６．コンストラクタの設定を保存
            // '==　　　　
            // '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を保存する。
            // '==　　　　
            // '=====================================================================================================================
            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）簡易読み込み可能にしたため年金対応（全て読むように）
            // 'コンストラクタの設定を保存
            // If Not (Me.m_cABAtenaB Is Nothing) Then
            // blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
            // blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
            // Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
            // Me.m_cABAtenaB.m_blnSelectCount = True
            // End If
            // If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
            // blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
            // blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
            // Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
            // Me.m_cABAtenaRirekiB.m_blnSelectCount = True

            // End If
            // '* 履歴番号 000024 2005/01/25 追加終了



            // '=====================================================================================================================
            // '== ６．宛名情報の取得
            // '==　　　　
            // '==　　　　<説明>　宛名情報の取得を行う。
            // '==　　　　
            // '=====================================================================================================================
            // ' 簡易宛名取得(内部処理)２メソッドを実行する。
            // csAtenaEntity = Me.GetAtena2(cAtenaGetPara1)



            // '=====================================================================================================================
            // '== ７．コンストラクタの設定を戻す
            // '==　　　　
            // '==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を戻す。
            // '==　　　　
            // '=====================================================================================================================
            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            // 'コンストラクタの設定を元にもどす
            // If Not (Me.m_cABAtenaB Is Nothing) Then
            // Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
            // Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
            // End If
            // If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
            // Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
            // Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
            // End If
            // '* 履歴番号 000024 2005/01/25 追加終了



            // '=====================================================================================================================
            // '== ８．宛名情報の編集
            // '==　　　　
            // '==　　　　<説明>　宛名情報の編集を行う。
            // '==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
            // '==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
            // '==　　　　　　　　ⅲ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
            // '==　　　　
            // '=====================================================================================================================
            // '*履歴番号 000015 2003/08/21 修正開始
            // '' 宛名編集クラスの年金宛名編集メソッドを実行する。
            // 'csAtena1Entity = cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // '*履歴番号 000016 2003/10/09 修正開始
            // 'If (m_blnBatch) Then
            // '    ' 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
            // '    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // 'Else
            // '    ' 宛名編集クラスの年金宛名編集メソッドを実行する。
            // '    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // 'End If
            // ' 指定年月日が指定されている場合
            // If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
            // If (m_blnBatch) Then
            // '*履歴番号 000029 2006/07/31 修正開始
            // '「宛名編集バッチ」の「履歴編集」メソッドを実行する
            // If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
            // csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
            // Else
            // csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
            // End If
            // 'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
            // '*履歴番号 000029 2006/07/31 修正終了

            // Else
            // '*履歴番号 000029 2006/07/31 修正開始
            // '「宛名編集」の「履歴編集」メソッドを実行する
            // If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
            // csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
            // Else
            // csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
            // End If
            // 'csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
            // '*履歴番号 000029 2006/07/31 修正終了
            // End If
            // Else
            // If (m_blnBatch) Then
            // '*履歴番号 000029 2006/07/31 修正開始
            // ' 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
            // If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
            // csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // Else
            // csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
            // End If
            // 'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // '*履歴番号 000029 2006/07/31 修正終了
            // Else
            // '*履歴番号 000029 2006/07/31 修正開始
            // ' 宛名編集クラスの年金宛名編集メソッドを実行する。
            // If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
            // csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // Else
            // csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
            // End If
            // 'csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
            // '*履歴番号 000029 2006/07/31 修正終了
            // End If
            // End If
            // '*履歴番号 000016 2003/10/09 修正終了
            // '*履歴番号 000015 2003/08/21 修正終了



            // '=====================================================================================================================
            // '== ９．業務コードの退避
            // '==　　　　
            // '==　　　　<説明>　業務コード・業務内種別コードを退避する。
            // '==　　　　
            // '=====================================================================================================================
            // '*履歴番号 000022 2003/12/02 追加開始
            // ' 業務コード・業務内種別コードを復元する
            // cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD
            // cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD




            // '=====================================================================================================================
            // '== １０．連絡先データの取得
            // '==　　　　
            // '==　　　　<説明>　連絡先情報を取得する。
            // '==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
            // '==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
            // '==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
            // '==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
            // '==　　　　
            // '=====================================================================================================================
            // ' 連絡先編集処理
            // '* 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // 'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity)
            // Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity, csAtenaEntity)
            // '* 履歴番号 000024 2005/01/25 更新終了
            // '*履歴番号 000022 2003/12/02 追加終了



            // '=====================================================================================================================
            // '== １１．コンストラクタの設定を戻す
            // '==　　　　
            // '==　　　　<説明>　簡易版・通常版の情報を戻す。
            // '==　　　　
            // '=====================================================================================================================
            // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
            // 'コンストラクタの設定を元にもどす
            // If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
            // Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
            // End If
            // If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
            // Me.m_cABAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
            // End If
            // '* 履歴番号 000024 2005/01/25 追加終了

            // Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData,
            // "【クラス名:" + THIS_CLASS_NAME + "】" +
            // "【メソッド名:" + THIS_METHOD_NAME + "】" +
            // "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
            // "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            // ' UFAppExceptionをスローする
            // Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            // Catch
            // ' エラーをそのままスロー
            // Throw

            // Finally
            // '* 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // ' RDBアクセスログ出力
            // 'm_cfLogClass.RdbWrite(m_cfControlData, _
            // '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
            // '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            // '                        "【実行メソッド名:Disconnect】")
            // '* 履歴番号 000023 2004/08/27 削除終了
            // ' RDB切断
            // If m_blnBatchRdb = False Then
            // '* 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // ' RDBアクセスログ出力
            // m_cfLogClass.RdbWrite(m_cfControlData,
            // "【クラス名:" + THIS_CLASS_NAME + "】" +
            // "【メソッド名:" + THIS_METHOD_NAME + "】" +
            // "【実行メソッド名:Disconnect】")
            // '* 履歴番号 000023 2004/08/27 追加終了
            // m_cfRdbClass.Disconnect()
            // End If

            // End Try

            // ' デバッグログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            // Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            // ' ワーニングログ出力
            // m_cfLogClass.WarningWrite(m_cfControlData,
            // "【クラス名:" + THIS_CLASS_NAME + "】" +
            // "【メソッド名:" + THIS_METHOD_NAME + "】" +
            // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
            // "【ワーニング内容:" + objAppExp.Message + "】")
            // ' エラーをそのままスローする
            // Throw objAppExp

            // Catch objExp As Exception
            // ' エラーログ出力
            // m_cfLogClass.ErrorWrite(m_cfControlData,
            // "【クラス名:" + THIS_CLASS_NAME + "】" +
            // "【メソッド名:" + THIS_METHOD_NAME + "】" +
            // "【エラー内容:" + objExp.Message + "】")
            // ' システムエラーをスローする
            // Throw objExp

            // End Try

            // Return csAtena1Entity

            return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Tsujo);

        }
        #endregion

        #region  年金宛名取得(GetNenkinAtena) 
        // ************************************************************************************************
        // * メソッド名     年金宛名取得（内部処理）
        // * 
        // * 構文           Private Function GetNenkinAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       年金宛名情報を取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        private DataSet GetNenkinAtena(ABAtenaGetPara1XClass cAtenaGetPara1, int intNenkinAtenaGetKB, ABEnumDefine.HyojunKB intHyojunKB)
        {
            const string THIS_METHOD_NAME = "GetNenkinAtena";
            DataSet csAtenaEntity;                        // 宛名Entity
            DataSet csAtena1Entity;                       // 宛名1Entity
            var cAtenaGetPara1Save = new ABAtenaGetPara1XClass();     // 退避用
            var blnAtenaSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnAtenaKani = default(bool);
            var blnRirekiSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnRirekiKani = default(bool);
            string strKikanYMD;                           // 期間年月日

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // =====================================================================================================================
                // == １．ＲＤＢ接続
                // ==　　　　
                // ==　　　　<説明>　バッチプログラムから呼び出された場合など、毎回ＲＤＢ接続を行わない制御を行う。
                // ==　　　　
                // =====================================================================================================================
                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    m_cfRdbClass.Connect();
                }

                try
                {
                    // =====================================================================================================================
                    // == ２．各種クラスのインスタンス化
                    // ==　　　　
                    // ==　　　　<説明>　バッチフラグの場合分けにより、リアル用・バッチ用クラスをインスタンス化する。
                    // ==　　　　
                    // =====================================================================================================================
                    if (m_blnBatch)
                    {
                        if (m_cABBatchAtenaHenshuB is null)
                        {
                            // 宛名編集バッチクラスのインスタンス作成
                            m_cABBatchAtenaHenshuB = new ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                        }
                        m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                    }
                    else
                    {
                        if (m_cABAtenaHenshuB is null)
                        {
                            // 宛名編集クラスのインスタンス作成
                            m_cABAtenaHenshuB = new ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                        }
                        m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                    }

                    m_cABAtenaB.m_intHyojunKB = intHyojunKB;
                    m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB;

                    // 宛名Ｂクラス各種プロパティをセット
                    m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB;
                    m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB;
                    m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB;

                    // 宛名履歴Ｂクラス各種プロパティをセット
                    m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB;
                    m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB;
                    m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB;


                    // =====================================================================================================================
                    // == ３．コンストラクタの設定を保存
                    // ==　　　　
                    // ==　　　　<説明>　簡易版・通常版の情報を保存する。
                    // ==　　　　
                    // =====================================================================================================================
                    // コンストラクタの設定を保存
                    if (m_cABBatchAtenaHenshuB is not null)
                    {
                        m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                    }
                    if (m_cABAtenaHenshuB is not null)
                    {
                        m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                    }



                    // =====================================================================================================================
                    // == ４．管理情報の取得
                    // ==　　　　
                    // ==　　　　<説明>　各種管理情報の取得を行う。
                    // ==　　　　
                    // =====================================================================================================================
                    // 管理情報取得(内部処理)メソッドを実行する。
                    GetKanriJoho();



                    // =====================================================================================================================
                    // == ５．業務コードの退避
                    // ==　　　　
                    // ==　　　　<説明>　業務コード・業務内種別コードを退避する。
                    // ==　　　　
                    // =====================================================================================================================
                    // 業務コード・業務内種別コードを退避する
                    cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD;
                    cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD;
                    cAtenaGetPara1.p_strGyomuCD = string.Empty;
                    cAtenaGetPara1.p_strGyomunaiSHU_CD = string.Empty;



                    // =====================================================================================================================
                    // == ６．コンストラクタの設定を保存
                    // ==　　　　
                    // ==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を保存する。
                    // ==　　　　
                    // =====================================================================================================================
                    // コンストラクタの設定を保存
                    if (m_cABAtenaB is not null)
                    {
                        blnAtenaSelectAll = m_cABAtenaB.m_blnSelectAll;
                        blnAtenaKani = m_cABAtenaB.m_blnSelectCount;
                        m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll;
                        m_cABAtenaB.m_blnSelectCount = true;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        blnRirekiSelectAll = m_cABAtenaRirekiB.m_blnSelectAll;
                        blnRirekiKani = m_cABAtenaRirekiB.m_blnSelectCount;
                        m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = true;

                    }



                    // =====================================================================================================================
                    // == ６．宛名情報の取得
                    // ==　　　　
                    // ==　　　　<説明>　宛名情報の取得を行う。
                    // ==　　　　
                    // =====================================================================================================================
                    // 簡易宛名取得(内部処理)２メソッドを実行する。
                    csAtenaEntity = GetAtena2(cAtenaGetPara1, intHyojunKB);



                    // =====================================================================================================================
                    // == ７．コンストラクタの設定を戻す
                    // ==　　　　
                    // ==　　　　<説明>　簡易版・通常版、直近版・履歴版の情報を戻す。
                    // ==　　　　
                    // =====================================================================================================================
                    // コンストラクタの設定を元にもどす
                    if (m_cABAtenaB is not null)
                    {
                        m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll;
                        m_cABAtenaB.m_blnSelectCount = blnAtenaKani;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani;
                    }



                    // =====================================================================================================================
                    // == ８．宛名情報の編集
                    // ==　　　　
                    // ==　　　　<説明>　宛名情報の編集を行う。
                    // ==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                    // ==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                    // ==　　　　　　　　ⅲ. バッチ版の指定がある場合はバッチ版のクラスにより取得する
                    // ==　　　　
                    // =====================================================================================================================
                    // 指定年月日が指定されている場合
                    if (!(cAtenaGetPara1.p_strShiteiYMD == ""))
                    {
                        if (m_blnBatch)
                        {
                            // 「宛名編集バッチ」の「履歴編集」メソッドを実行する
                            if (intNenkinAtenaGetKB == ABEnumDefine.NenkinAtenaGetKB.Version01)
                            {
                                csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity);
                            }
                            else
                            {
                                csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity);
                            }
                        }

                        // 「宛名編集」の「履歴編集」メソッドを実行する
                        else if (intNenkinAtenaGetKB == ABEnumDefine.NenkinAtenaGetKB.Version01)
                        {
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity);
                        }
                        else
                        {
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity);
                        }
                    }
                    else if (m_blnBatch)
                    {
                        // 宛名編集バッチクラスの年金宛名編集メソッドを実行する。
                        if (intNenkinAtenaGetKB == ABEnumDefine.NenkinAtenaGetKB.Version01)
                        {
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity);
                        }
                        else
                        {
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity);
                        }
                    }
                    // 宛名編集クラスの年金宛名編集メソッドを実行する。
                    else if (intNenkinAtenaGetKB == ABEnumDefine.NenkinAtenaGetKB.Version01)
                    {
                        csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity);
                    }
                    else
                    {
                        csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity);
                    }



                    // =====================================================================================================================
                    // == ９．業務コードの退避
                    // ==　　　　
                    // ==　　　　<説明>　業務コード・業務内種別コードを退避する。
                    // ==　　　　
                    // =====================================================================================================================
                    // 業務コード・業務内種別コードを復元する
                    cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD;
                    cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD;




                    // =====================================================================================================================
                    // == １０．連絡先データの取得
                    // ==　　　　
                    // ==　　　　<説明>　連絡先情報を取得する。
                    // ==　　　　　　　　ⅰ. 業務コードが存在しない場合は、何もしない
                    // ==　　　　　　　　ⅱ. 指定した業務コード・業務内種別コードを条件に「連絡先マスタ：ABRENRAKUSAKI」から取得する
                    // ==　　　　　　　　ⅲ. ⅱ.でデータが取得した場合、無条件に連絡先１、連絡先２を返却する
                    // ==　　　　　　　　ⅳ. 年金宛名ゲット・個別ゲットのレイアウトの場合のみ「連絡先業務コード」に抽出条件の業務コードをセットする
                    // ==　　　　
                    // =====================================================================================================================
                    // 指定年月日が指定してあり且つ取得パラメータの送付先データ区分が"1"の場合
                    if (cAtenaGetPara1.p_strShiteiYMD != "" & cAtenaGetPara1.p_strSfskDataKB == "1")
                    {
                        strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8);
                    }
                    else
                    {
                        strKikanYMD = m_strSystemDateTime;
                    }
                    // 連絡先編集処理
                    this.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, ref csAtena1Entity, ref csAtenaEntity, intHyojunKB, strKikanYMD);



                    // =====================================================================================================================
                    // == １１．コンストラクタの設定を戻す
                    // ==　　　　
                    // ==　　　　<説明>　簡易版・通常版の情報を戻す。
                    // ==　　　　
                    // =====================================================================================================================
                    // コンストラクタの設定を元にもどす
                    if (m_cABBatchAtenaHenshuB is not null)
                    {
                        m_cABBatchAtenaHenshuB.m_blnSelectAll = m_blnSelectAll;
                    }
                    if (m_cABAtenaHenshuB is not null)
                    {
                        m_cABAtenaHenshuB.m_blnSelectAll = m_blnSelectAll;
                    }
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtena1Entity;

        }
        #endregion

        #region  国保宛名履歴取得(KokuhoAtenaRirekiGet) 
        // ************************************************************************************************
        // * メソッド名     国保宛名履歴取得
        // * 
        // * 構文           Public Function KokuhoAtenaRirekiGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       国保宛名履歴データを取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet KokuhoAtenaRirekiGet(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            const string THIS_METHOD_NAME = "KokuhoAtenaRirekiGet";
            // *履歴番号 000015 2003/08/21 削除開始
            // Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
            // *履歴番号 000015 2003/08/21 削除終了
            DataSet csAtena1Entity;                       // 宛名1Entity

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:Connect】")
                // * 履歴番号 000023 2004/08/27 削除終了
                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    // * 履歴番号 000023 2004/08/27 追加終了
                    m_cfRdbClass.Connect();
                }

                try
                {
                    // 管理情報取得(内部処理)メソッドを実行する。
                    GetKanriJoho();

                    // 国保宛名履歴取得(内部処理)メソッドを実行する。
                    csAtena1Entity = this.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo);
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                    // RDBアクセスログ出力
                    // m_cfLogClass.RdbWrite(m_cfControlData, _
                    // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                    // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                    // "【実行メソッド名:Disconnect】")
                    // * 履歴番号 000023 2004/08/27 削除終了
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        // * 履歴番号 000023 2004/08/27 追加終了
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtena1Entity;

        }
        #endregion

        #region  簡易宛名取得２(GetAtena2) 
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得２（内部処理）
        // * 
        // * 構文           Private Function GetAtena2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        private DataSet GetAtena2(ABAtenaGetPara1XClass cAtenaGetPara1, ABEnumDefine.HyojunKB intHyojunKB)
        {
            const string THIS_METHOD_NAME = "GetAtena2";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            ABAtenaSearchKey cSearchKey;                  // 宛名検索キー
                                                          // * corresponds to VS2008 Start 2010/04/16 000044
                                                          // Dim csDataTable As DataTable
                                                          // * corresponds to VS2008 End 2010/04/16 000044
            var csDataSet = default(DataSet);
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
            // Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
            // * 履歴番号 000023 2004/08/27 削除終了
            // *履歴番号 000015 2003/08/21 削除開始
            // Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
            // *履歴番号 000015 2003/08/21 削除終了
            int intHyojiKensu;                        // 最大取得件数
                                                      // * corresponds to VS2008 Start 2010/04/16 000044
                                                      // Dim intGetCount As Integer                          '取得件数
                                                      // * corresponds to VS2008 End 2010/04/16 000044
                                                      // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                                                      // Dim cUSSCityInfoClass As New USSCityInfoClass()     '市町村情報管理クラス
                                                      // * 履歴番号 000023 2004/08/27 削除終了
            string strShichosonCD;                        // 市町村コード
                                                          // * 履歴番号 000039 2008/02/17 追加開始
            int intIdx;
            ABMojiretsuHenshuBClass cABMojiHenshuB;       // 文字編集Ｂクラス
                                                          // * 履歴番号 000039 2008/02/17 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // =====================================================================================================================
                // == １．宛名取得パラメータチェック
                // ==　　　　
                // ==　　　　<説明>　パラメータクラスに指定された内容をチェックする。
                // ==　　　　
                // =====================================================================================================================
                // パラメータチェック
                CheckColumnValue(cAtenaGetPara1, intHyojunKB);


                // =====================================================================================================================
                // == ２．業務コード存在チェック
                // ==　　　　
                // ==　　　　<説明>　業務コードが検索キーにしてされていた場合は、エラーを返す。
                // ==　　　　
                // =====================================================================================================================
                // 業務コードが指定されている場合は、エラー
                if (!(cAtenaGetPara1.p_strGyomuCD == string.Empty))
                {
                    // エラー定義を取得
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002002);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "業務コード", objErrorStruct.m_strErrorCode);
                }

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // 宛名履歴マスタＤＡクラスのインスタンス作成
                // cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                // 宛名マスタＤＡクラスのインスタンス作成
                // cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // * 履歴番号 000023 2004/08/27 削除終了


                // *履歴番号 000015 2003/08/21 修正開始
                // ' 宛名編集クラスのインスタンス作成
                // cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // *履歴番号 000015 2003/08/21 修正終了

                // 直近市町村情報取得を取得する。
                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // cUSSCityInfoClass.GetCityInfo(m_cfControlData)
                // * 履歴番号 000023 2004/08/27 削除終了


                // =====================================================================================================================
                // == ３．市町村コードの取得
                // ==　　　　
                // ==　　　　<説明>　直近の市町村コードを取得する。
                // ==　　　　
                // =====================================================================================================================
                // 市町村コードの内容を設定する。
                if (cAtenaGetPara1.p_strShichosonCD == string.Empty)
                {
                    strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0);
                }
                else
                {
                    strShichosonCD = cAtenaGetPara1.p_strShichosonCD;
                }



                // *履歴番号 000031 2007/07/31 追加開始
                // =====================================================================================================================
                // == ４．同一人代表者取得処理
                // ==　　　　
                // ==　　　　<説明>　住民コード・住登外優先・同一人判定FG有効の検索条件の場合のみ、同一人代表者取得を行う。
                // ==　　　　　　　　管理情報により、ユーザごとの取得判定有り。
                // ==　　　　
                // =====================================================================================================================
                // 同一人代表者住民コードを検索パラメータに上書きする
                GetDaihyoJuminCD(ref cAtenaGetPara1);
                // *履歴番号 000031 2007/07/31 追加終了



                // =====================================================================================================================
                // == ５．本人宛名取得検索キーの設定
                // ==　　　　
                // ==　　　　<説明>　本人の宛名情報を取得するための検索キーを指定されたパラメータクラスより設定する。
                // ==　　　　　　　　最大取得件数も取得する。
                // ==　　　　
                // =====================================================================================================================
                // 宛名検索キーのインスタンス化
                cSearchKey = new ABAtenaSearchKey();

                // 宛名取得パラメータから宛名検索キーにセットする
                cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD;
                cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD;
                cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei;
                cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei;
                cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei;
                cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei;
                cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD;
                cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu;
                cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB;
                cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1;
                cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2;
                cSearchKey.p_strShichosonCD = strShichosonCD;
                // *履歴番号 000032 2007/09/04 追加開始
                // 検索用カナ姓名・検索用カナ姓・検索用カナ名の編集
                cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen);
                // *履歴番号 000032 2007/09/04 追加終了

                // 住所～番地コード3のセット
                if (!(cAtenaGetPara1.p_strJukiJutogaiKB == "1"))
                {
                    // 住登外優先の場合
                    cSearchKey.p_strJutogaiYusenKB = "1";
                    cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD;
                    cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9);
                    cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8);
                    cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8);
                    cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8);
                    cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5);
                    cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5);
                    cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5);
                }
                else
                {
                    // 住基優先の場合
                    cSearchKey.p_strJuminYuseniKB = "1";
                    // *履歴番号 000018 2003/10/30 修正開始
                    // cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
                    cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8);
                    // *履歴番号 000018 2003/10/30 修正終了
                    cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9);
                    cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8);
                    cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8);
                    cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8);
                    cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5);
                    cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5);
                    cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5);
                }
                // *履歴番号 000048 2014/04/28 追加開始
                cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13);
                cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB;
                cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB;
                // *履歴番号 000048 2014/04/28 追加終了
                // 最大取得件数をセットする
                if (cAtenaGetPara1.p_intHyojiKensu == 0)
                {
                    intHyojiKensu = 100;
                }
                else
                {
                    intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu;
                }
                // *履歴番号 000047 2011/11/07 追加開始
                m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB;
                m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB;
                // *履歴番号 000047 2011/11/07 追加終了
                // *履歴番号 000048 2014/04/28 追加開始
                m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB;
                m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB;
                // *履歴番号 000048 2014/04/28 追加終了

                // *履歴番号 000050 2020/01/31 追加開始
                // 履歴検索フラグ
                cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch;
                // *履歴番号 000050 2020/01/31 追加終了
                cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji;
                cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji;
                cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei;
                cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho;
                cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki;
                cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki;

                m_cABAtenaB.m_intHyojunKB = intHyojunKB;
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB;

                // =====================================================================================================================
                // == ６．本人宛名データの取得
                // ==　　　　
                // ==　　　　<説明>　本人の宛名情報を取得する。
                // ==　　　　　　　　ⅰ. 指定年月日がある場合は「宛名履歴マスタ：ABATENARIREKI」により取得する
                // ==　　　　　　　　ⅱ. 指定年月日がない場合は「宛名マスタ：ABATENA」により取得する
                // ==　　　　
                // =====================================================================================================================
                if (!(cAtenaGetPara1.p_strShiteiYMD == string.Empty))
                {
                    // 指定年月日が指定されている場合
                    // 「宛名履歴マスタ抽出」メゾットを実行する
                    csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD, cAtenaGetPara1.p_blnSakujoFG);
                }

                else
                {
                    // 指定年月日が指定されていない場合
                    // 「宛名マスタ抽出」メゾットを実行する
                    csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG);
                }

                // * 履歴番号 000024 2005/01/25 追加終了
                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする

                // *履歴番号 000031 2007/07/31 追加開始
                throw objExp;
            }
            finally
            {
                // =====================================================================================================================
                // == ２４．返却する住民コードを指定された住民コードで上書きする
                // ==　　　　
                // ==　　　　<説明>　同一人代表者取得された場合は、指定された住民コードを返す
                // ==　　　　
                // =====================================================================================================================
                // 退避した住民コードが存在する場合は、上書きする
                SetJuminCD(ref csDataSet);
                // *履歴番号 000031 2007/07/31 追加終了

                // *履歴番号 000039 2008/02/17 追加開始
                // =====================================================================================================================
                // == ８．外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                // ==　　　　
                // ==　　　　<説明>　外国人データ:漢字氏名１、２、または漢字世帯主名(転出確定、転出予定、転入前含む)の括弧で括られた文字列の除去を行う
                // ==　　　　　　　　
                // =====================================================================================================================
                // *履歴番号 000043 2009/04/08 修正開始
                if (csDataSet is not null)
                {
                    if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                    {
                        // 漢字氏名に含まれる括弧で括られた文字列の除去を行う

                        cABMojiHenshuB = new ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass);

                        // 全取得データ分行う
                        // * 宛名マスタ、宛名履歴マスタともに同じレイアウトのため、テーブル指定："0"、項目名は宛名Entityを使用。
                        var loopTo = csDataSet.Tables[0].Rows.Count - 1;
                        for (intIdx = 0; intIdx <= loopTo; intIdx++)
                        {
                            // 漢字名称１
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATASHU)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIMEISHO1)));
                            // 漢字名称２
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATASHU)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIMEISHO2)));
                            // 世帯主名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.STAINUSMEI)));
                            // 第２世帯主名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.DAI2STAINUSMEI)));
                            // 漢字法人代表者名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.ATENADATASHU)), Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)));
                            // 転入前世帯主名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENUMAEJ_STAINUSMEI)));
                            // 転出予定世帯主名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)));
                            // 転出確定世帯主名
                            csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataSet.Tables[0].Rows[intIdx](ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)));
                        }
                    }
                    else
                    {
                        // 漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                    }
                }

                // If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                // '漢字氏名に含まれる括弧で括られた文字列の除去を行う

                // cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass)

                // ' 全取得データ分行う
                // '* 宛名マスタ、宛名履歴マスタともに同じレイアウトのため、テーブル指定："0"、項目名は宛名Entityを使用。
                // For intIdx = 0 To csDataSet.Tables(0).Rows.Count - 1
                // ' 漢字名称１
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1)))
                // ' 漢字名称２
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2)))
                // ' 世帯主名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI)))
                // ' 第２世帯主名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI)))
                // ' 漢字法人代表者名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
                // CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
                // ' 転入前世帯主名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
                // ' 転出予定世帯主名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
                // ' 転出確定世帯主名
                // csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
                // Next
                // Else
                // ' 漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                // End If
                // '*履歴番号 000039 2008/02/17 追加終了
                // *履歴番号 000043 2009/04/08 修正終了

            }

            return csDataSet;

        }
        #endregion

        #region  国保宛名履歴取得(GetKokuhoAtenaRireki) 
        // ************************************************************************************************
        // * メソッド名     国保宛名履歴取得（内部処理）
        // * 
        // * 構文           Private Function GetKokuhoAtenaRireki(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　取得パラメータより宛名履歴データを返す。
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        private DataSet GetKokuhoAtenaRireki(ABAtenaGetPara1XClass cAtenaGetPara1, ABEnumDefine.HyojunKB intHyojunKB)
        {
            const string THIS_METHOD_NAME = "GetKokuhoAtenaRireki";
            // * corresponds to VS2008 Start 2010/04/16 000044
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000044
            ABAtenaSearchKey cSearchKey;                  // 宛名検索キー
            DataSet csDataSet;
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '宛名履歴マスタＤＡクラス
            // Dim cABAtenaB As ABAtenaBClass                      '宛名マスタＤＡクラス
            // * 履歴番号 000023 2004/08/27 削除終了
            // *履歴番号 000015 2003/08/21 削除開始
            // Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '宛名編集クラス
            // *履歴番号 000015 2003/08/21 削除終了
            DataSet csAtena1Entity;                       // 宛名1Entity
                                                          // * corresponds to VS2008 Start 2010/04/16 000044
                                                          // Dim strShiteiYMD As String                          ' 指定日
                                                          // * corresponds to VS2008 End 2010/04/16 000044

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // 宛名履歴マスタＤＡクラスのインスタンス作成
                // cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // 
                // 宛名マスタＤＡクラスのインスタンス作成
                // cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000015 2003/08/21 修正開始
                // ' 宛名編集クラスのインスタンス作成
                // cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                if (m_blnBatch)
                {
                    if (m_cABBatchAtenaHenshuB is null)
                    {
                        // 宛名編集バッチクラスのインスタンス作成
                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                        // m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                        m_cABBatchAtenaHenshuB = new ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                        // * 履歴番号 000024 2005/01/25 更新終了
                    }
                    m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                }
                else
                {
                    if (m_cABAtenaHenshuB is null)
                    {
                        // 宛名編集クラスのインスタンス作成
                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                        // m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                        m_cABAtenaHenshuB = new ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll);
                        // * 履歴番号 000024 2005/01/25 更新終了
                    }
                    m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB;
                }
                // *履歴番号 000015 2003/08/21 修正終了

                // ①パラメータチェック
                CheckColumnValue(cAtenaGetPara1, intHyojunKB);

                // 宛名検索キーのインスタンス化
                cSearchKey = new ABAtenaSearchKey();

                // ③宛名取得パラメータから宛名検索キーにセットする
                cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD;

                // *履歴番号 000016 2003/09/08 修正開始
                // '「宛名マスタ抽出」メゾットを実行する
                // csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
                // cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                // ' 取得件数が１件でない場合、エラー
                // If Not (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count = 1) Then
                // 'エラー定義を取得(検索キーの誤りです。：住民コード)
                // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                // End If

                // ' 世帯コードがNull場合、エラー
                // If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String).Trim = String.Empty) Then
                // 'エラー定義を取得(検索キーの誤りです。：住民コード)
                // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode)
                // End If

                // ' 宛名検索キーのインスタンス化
                // cSearchKey = New ABAtenaSearchKey()

                // ' ④	ABAtenaSearchKeyに世帯コードをセット
                // cSearchKey.p_strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String)

                // If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB), String) = "1") Then
                // ' 住基・住登外区分が”1”の時、”1”を住民優先区分にセット
                // cSearchKey.p_strJuminYuseniKB = "1"
                // Else
                // ' 住基・住登外区分が<>”1”の時、”1”を住登外優先区分にセット
                // cSearchKey.p_strJutogaiYusenKB = "1"
                // End If

                // 住基・住登外区分が<>”1”の時、”1”を住登外優先区分にセット
                if (cAtenaGetPara1.p_strJukiJutogaiKB != "1")
                {
                    cSearchKey.p_strJutogaiYusenKB = "1";
                }
                else
                {
                    cSearchKey.p_strJuminYuseniKB = "1";
                }
                // *履歴番号 000016 2003/09/08 修正終了
                // *履歴番号 000047 2011/11/07 追加開始
                m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB;
                // *履歴番号 000047 2011/11/07 追加終了
                // *履歴番号 000048 2014/04/28 追加開始
                m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB;
                // *履歴番号 000048 2014/04/28 追加終了
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB;

                // ⑤	宛名履歴マスタＤＡ」クラスの「宛名履歴マスタ抽出」メソッドを実行する
                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, cSearchKey, cAtenaGetPara1.p_strShiteiYMD);

                // *履歴番号 000015 2003/08/21 修正開始
                // ' 「宛名編集」クラスの「履歴編集」メソッドを実行する。
                // csAtena1Entity = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

                if (m_blnBatch)
                {
                    // 「宛名編集」クラスの「履歴編集」メソッドを実行する。
                    csAtena1Entity = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet);
                }
                else
                {
                    // 「宛名編集」クラスの「履歴編集」メソッドを実行する。
                    csAtena1Entity = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet);
                }
                // *履歴番号 000015 2003/08/21 修正終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtena1Entity;

        }
        #endregion

        #region  管理情報取得(GetKanriJoho) 
        // ************************************************************************************************
        // * メソッド名     管理情報取得（内部処理）
        // * 
        // * 構文           Private Function GetKanriJoho()
        // * 
        // * 機能　　    　　管理情報を取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        // * 履歴番号 000015 2003/08/21 修正開始
        // Private Sub GetKanriJoho()
        [SecuritySafeCritical]
        protected virtual void GetKanriJoho()
        {
            // * 履歴番号 000015 2003/08/21 修正終了
            const string THIS_METHOD_NAME = "GetKanriJoho";
            // * 履歴番号 000015 2003/08/21 削除開始
            // Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '宛名管理情報Ｂクラス
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cfURAtenaKanriJoho As URAtenaKanriJohoCacheBClass   '宛名管理情報キャッシュＢクラス
            // * 履歴番号 000023 2004/08/27 削除終了
            // * 履歴番号 000015 2003/08/21 削除終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000014 2003/06/17 追加開始
                if (m_blnKanriJoho)
                {
                    return;
                }
                // * 履歴番号 000014 2003/06/17 追加終了

                // * 履歴番号 000015 2003/08/21 修正開始
                // 管理情報クラスのインスタンス作成
                // cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                // 宛名管理情報キャッシュＢクラスのインスタンス作成
                // * 履歴番号 000023 2004/08/27 更新開始（宮沢）
                // cfURAtenaKanriJoho = New URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                if (m_cfURAtenaKanriJoho is null)
                {
                    m_cfURAtenaKanriJoho = new URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                // * 履歴番号 000023 2004/08/27 更新終了
                // * 履歴番号 000015 2003/08/21 修正終了

                m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD;                // 住民コード表示桁数
                m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD;                 // 世帯コード表示桁数
                m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD;                // 住所コード表示桁数（管内のみ）
                m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD;          // 行政区コード表示桁数
                m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1;              // 地区コード１表示桁数
                m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2;              // 地区コード２表示桁数
                m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3;              // 地区コード３表示桁数
                m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho;          // 地区コード１表示名称
                m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho;          // 地区コード２表示名称
                m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho;          // 地区コード３表示名称
                m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho;  // 連絡先１表示名称
                m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho;  // 連絡先２表示名称

                // * 履歴番号 000014 2003/06/17 追加開始
                // 管理情報取得済みフラグ設定
                m_blnKanriJoho = true;
                // * 履歴番号 000014 2003/06/17 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

        }
        #endregion

        #region  パラメーターチェック(CheckColumnValue) 
        // ************************************************************************************************
        // * メソッド名     パラメーターチェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1)
        // * 
        // * 機能　　    　　宛名取得パラメータのチェックを行なう
        // * 
        // * 引数           cAtenaGetPara1 As ABAtenaGetPara1 : 宛名取得パラメータ
        // *                intHyojunKB                       : 標準化区分
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(ABAtenaGetPara1XClass cAtenaGetPara1, ABEnumDefine.HyojunKB intHyojunKB)
        {

            const string THIS_METHOD_NAME = "CheckColumnValue";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                                                          // Dim m_cfDateClass As UFDateClass                    ' 日付クラス
                                                          // * 履歴番号 000023 2004/08/27 削除終了

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 日付クラスのインスタンス化
                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                // * 履歴番号 000023 2004/08/27 削除終了
                // 必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;


                // 住基・住登外区分
                if (!(cAtenaGetPara1.p_strJukiJutogaiKB.Trim == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strJukiJutogaiKB == "1"))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住基・住登外区分", objErrorStruct.m_strErrorCode);
                    }
                }


                // 送付先データ区分
                if (!(cAtenaGetPara1.p_strSfskDataKB == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strSfskDataKB == "1"))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "送付先データ区分", objErrorStruct.m_strErrorCode);
                    }
                }

                // 世帯員編集
                if (!(cAtenaGetPara1.p_strStaiinHenshu == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strStaiinHenshu == "1"))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "世帯員編集", objErrorStruct.m_strErrorCode);
                    }
                }


                // 住民コード
                if (!(cAtenaGetPara1.p_strJuminCD.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminCD.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // 世帯コード
                if (!(cAtenaGetPara1.p_strStaiCD.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strStaiCD.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "世帯コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // カナ姓名
                if (!(cAtenaGetPara1.p_strKanaSeiMei == string.Empty))
                {
                    // *履歴番号 000019 2003/10/30 修正開始
                    // If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd("%"c))) Then
                    if (!UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd('%')))
                    {
                        // *履歴番号 000019 2003/10/30 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "カナ姓名", objErrorStruct.m_strErrorCode);
                    }
                }

                // カナ姓
                if (!(cAtenaGetPara1.p_strKanaSei == string.Empty))
                {
                    // *履歴番号 000019 2003/10/30 修正開始
                    // If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSei.TrimEnd("%"c))) Then
                    if (!UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSei.TrimEnd('%')))
                    {
                        // *履歴番号 000019 2003/10/30 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "カナ姓", objErrorStruct.m_strErrorCode);
                    }
                }

                // カナ名
                if (!(cAtenaGetPara1.p_strKanaMei == string.Empty))
                {
                    // *履歴番号 000019 2003/10/30 修正開始
                    // If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaMei.TrimEnd("%"c))) Then
                    if (!UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaMei.TrimEnd('%')))
                    {
                        // *履歴番号 000019 2003/10/30 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "カナ名", objErrorStruct.m_strErrorCode);
                    }
                }

                // 漢字名称
                if (!(cAtenaGetPara1.p_strKanjiShimei == string.Empty))
                {
                    // * 履歴番号 000025 2005/04/04 修正開始
                    // If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.TrimEnd("%"c), m_cfConfigDataClass)) Then
                    if (!UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.Replace('%', string.Empty), m_cfConfigDataClass))
                    {
                        // * 履歴番号 000025 2005/04/04 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "漢字名称", objErrorStruct.m_strErrorCode);
                    }
                }

                // 生年月日
                if (!(cAtenaGetPara1.p_strUmareYMD == string.Empty | cAtenaGetPara1.p_strUmareYMD == "00000000"))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strUmareYMD.TrimEnd('%')))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "生年月日", objErrorStruct.m_strErrorCode);
                    }
                }

                // 性別コード
                if (!(cAtenaGetPara1.p_strSeibetsu == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strSeibetsu))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "性別コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // 住所コード
                if (!(cAtenaGetPara1.p_strJushoCD.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strJushoCD.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // 行政区コード
                if (!(cAtenaGetPara1.p_strGyoseikuCD.Trim == string.Empty))
                {
                    // *履歴番号 000028 2005/12/06 修正開始
                    // 'If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strGyoseikuCD.Trim)) Then
                    if (!UFStringClass.CheckANK(cAtenaGetPara1.p_strGyoseikuCD.Trim))
                    {
                        // *履歴番号 000028 2005/12/06 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "行政区コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // 地区コード１
                if (!(cAtenaGetPara1.p_strChikuCD1.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD1.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "地区コード１", objErrorStruct.m_strErrorCode);
                    }
                }

                // 地区コード２
                if (!(cAtenaGetPara1.p_strChikuCD2.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD2.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "地区コード２", objErrorStruct.m_strErrorCode);
                    }
                }

                // 地区コード３
                if (!(cAtenaGetPara1.p_strChikuCD3.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD3))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "地区コード３", objErrorStruct.m_strErrorCode);
                    }
                }

                // 番地コード１
                if (!(cAtenaGetPara1.p_strBanchiCD1.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD1.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "番地コード１", objErrorStruct.m_strErrorCode);
                    }
                }

                // 番地コード２
                if (!(cAtenaGetPara1.p_strBanchiCD2.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD2.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "番地コード２", objErrorStruct.m_strErrorCode);
                    }
                }

                // 番地コード３
                if (!(cAtenaGetPara1.p_strBanchiCD3.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD3.Trim))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "番地コード３", objErrorStruct.m_strErrorCode);
                    }
                }

                // データ区分
                // *履歴番号 000021 2003/12/01 修正開始
                // If Not (cAtenaGetPara1.p_strDataKB = String.Empty) Then
                if (!(cAtenaGetPara1.p_strDataKB == string.Empty | cAtenaGetPara1.p_strDataKB == "1%"))
                {
                    // *履歴番号 000021 2003/12/01 修正終了
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strDataKB))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "データ区分", objErrorStruct.m_strErrorCode);
                    }
                }

                // 住民種別１
                if (!(cAtenaGetPara1.p_strJuminSHU1 == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU1))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民種別１", objErrorStruct.m_strErrorCode);
                    }
                }

                // 住民種別２
                if (!(cAtenaGetPara1.p_strJuminSHU2 == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU2))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民種別２", objErrorStruct.m_strErrorCode);
                    }
                }

                // 指定年月日
                if (!(cAtenaGetPara1.p_strShiteiYMD == string.Empty | cAtenaGetPara1.p_strShiteiYMD == "00000000"))
                {
                    m_cfDateClass.p_strDateValue = cAtenaGetPara1.p_strShiteiYMD;
                    if (!m_cfDateClass.CheckDate())
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "指定年月日", objErrorStruct.m_strErrorCode);
                    }
                }

                // 市町村コード
                if (!(cAtenaGetPara1.p_strShichosonCD == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strShichosonCD))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "市町村コード", objErrorStruct.m_strErrorCode);
                    }
                }

                // 表示件数
                if (cAtenaGetPara1.p_intHyojiKensu < 0 | cAtenaGetPara1.p_intHyojiKensu > 999)
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "表示件数", objErrorStruct.m_strErrorCode);
                }

                // 住民コードと世帯コードがNULLで、世帯員編集が"1"の時、例外エラー
                if (cAtenaGetPara1.p_strJuminCD == string.Empty & cAtenaGetPara1.p_strStaiCD == string.Empty & cAtenaGetPara1.p_strStaiinHenshu == "1")

                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "世帯員編集", objErrorStruct.m_strErrorCode);
                }

                // 旧氏
                if (!(cAtenaGetPara1.p_strKyuuji.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKyuuji.Replace('%', string.Empty), m_cfConfigDataClass))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "旧氏", objErrorStruct.m_strErrorCode);
                    }
                }

                // カナ旧氏
                if (!(cAtenaGetPara1.p_strKanaKyuuji.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaKyuuji.Replace('%', string.Empty)))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "カナ旧氏", objErrorStruct.m_strErrorCode);
                    }
                }

                // カタカナ併記名
                if (!(cAtenaGetPara1.p_strKatakanaHeikimei.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckKataKanaWide(cAtenaGetPara1.p_strKatakanaHeikimei.Replace('%', string.Empty)))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "カタカナ併記名", objErrorStruct.m_strErrorCode);
                    }
                }

                // 住所
                if (!(cAtenaGetPara1.p_strJusho.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strJusho.Replace('%', string.Empty), m_cfConfigDataClass))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所", objErrorStruct.m_strErrorCode);
                    }
                }

                // 方書
                if (!(cAtenaGetPara1.p_strKatagaki.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKatagaki.Replace('%', string.Empty), m_cfConfigDataClass))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "方書", objErrorStruct.m_strErrorCode);
                    }
                }

                // 電話番号
                if (!(cAtenaGetPara1.p_strRenrakusaki.Trim == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strRenrakusaki.Replace("-", string.Empty)))
                    {

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "電話番号", objErrorStruct.m_strErrorCode);
                    }
                }

                // 住民コード～番地コード３すべてがNULLの時、例外エラー
                // *履歴番号 000027 2005/05/06 修正開始
                // *履歴番号 000048 2014/04/28 修正開始
                // 共通番号の単独指定を可能とするため、判定項目に追加する。
                // If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
                // And (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) Then

                if (!(cAtenaGetPara1.p_strShiteiYMD.Trim == string.Empty) && intHyojunKB == ABEnumDefine.HyojunKB.KB_Tsujo)
                {
                    if (cAtenaGetPara1.p_strJuminCD.Trim == string.Empty & cAtenaGetPara1.p_strStaiCD.Trim == string.Empty & cAtenaGetPara1.p_strKanaSeiMei.Trim == string.Empty & cAtenaGetPara1.p_strKanaSei.Trim == string.Empty & cAtenaGetPara1.p_strKanaMei.Trim == string.Empty & cAtenaGetPara1.p_strKanjiShimei.Trim == string.Empty & cAtenaGetPara1.p_strUmareYMD.Trim == string.Empty & cAtenaGetPara1.p_strJushoCD.Trim == string.Empty & cAtenaGetPara1.p_strGyoseikuCD.Trim == string.Empty & cAtenaGetPara1.p_strChikuCD1.Trim == string.Empty & cAtenaGetPara1.p_strChikuCD2.Trim == string.Empty & cAtenaGetPara1.p_strChikuCD3.Trim == string.Empty & cAtenaGetPara1.p_strBanchiCD1.Trim == string.Empty & cAtenaGetPara1.p_strBanchiCD2.Trim == string.Empty & cAtenaGetPara1.p_strBanchiCD3.Trim == string.Empty & cAtenaGetPara1.p_strMyNumber.Trim == string.Empty & cAtenaGetPara1.p_strRenrakusaki.Trim == string.Empty)















                    {
                        // *履歴番号 000048 2014/04/28 修正終了
                        // *履歴番号 000027 2005/05/06 修正終了

                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "検索キーなし", objErrorStruct.m_strErrorCode);
                    }
                }
                else if (cAtenaGetPara1.p_strJuminCD.Trim == string.Empty && cAtenaGetPara1.p_strStaiCD.Trim == string.Empty && cAtenaGetPara1.p_strKanaSeiMei.Trim == string.Empty && cAtenaGetPara1.p_strKanaSei.Trim == string.Empty && cAtenaGetPara1.p_strKanaMei.Trim == string.Empty && cAtenaGetPara1.p_strKanjiShimei.Trim == string.Empty && cAtenaGetPara1.p_strUmareYMD.Trim == string.Empty && cAtenaGetPara1.p_strJushoCD.Trim == string.Empty && cAtenaGetPara1.p_strGyoseikuCD.Trim == string.Empty && cAtenaGetPara1.p_strChikuCD1.Trim == string.Empty && cAtenaGetPara1.p_strChikuCD2.Trim == string.Empty && cAtenaGetPara1.p_strChikuCD3.Trim == string.Empty && cAtenaGetPara1.p_strBanchiCD1.Trim == string.Empty && cAtenaGetPara1.p_strBanchiCD2.Trim == string.Empty && cAtenaGetPara1.p_strBanchiCD3.Trim == string.Empty && cAtenaGetPara1.p_strMyNumber.Trim == string.Empty && cAtenaGetPara1.p_strKyuuji.Trim == string.Empty && cAtenaGetPara1.p_strKanaKyuuji.Trim == string.Empty && cAtenaGetPara1.p_strKatakanaHeikimei.Trim == string.Empty && cAtenaGetPara1.p_strJusho.Trim == string.Empty && cAtenaGetPara1.p_strKatagaki.Trim == string.Empty && cAtenaGetPara1.p_strRenrakusaki.Trim == string.Empty)




















                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "検索キーなし", objErrorStruct.m_strErrorCode);
                }

                // *履歴番号 000040 2008/11/10 追加開始
                if ((cAtenaGetPara1.p_strTdkdKB == "1" || cAtenaGetPara1.p_strTdkdKB == "2") && cAtenaGetPara1.p_strTdkdZeimokuCD == ABEnumDefine.ZeimokuCDType.Empty)
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "利用届出取得区分を使用する場合は、利用届出取得用税目コードを指定してください。", objErrorStruct.m_strErrorCode);
                }
                // *履歴番号 000040 2008/11/10 追加終了

                // *履歴番号 000051 2020/11/02 追加開始
                // 利用届出利用区分
                if ((cAtenaGetPara1.p_strTdkdKB == "1" || cAtenaGetPara1.p_strTdkdKB == "2") && !(cAtenaGetPara1.p_strTdkdRiyoKB == string.Empty || cAtenaGetPara1.p_strTdkdRiyoKB == "1" || cAtenaGetPara1.p_strTdkdRiyoKB == "2" || cAtenaGetPara1.p_strTdkdRiyoKB == "3"))
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "利用届出利用区分", objErrorStruct.m_strErrorCode);
                }
                // *履歴番号 000051 2020/11/02 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

        }
        #endregion

        #region  宛名情報のマージ(CreateAtenaDataSet) 
        // ************************************************************************************************
        // * メソッド名     宛名情報のマージ
        // * 
        // * 構文           Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, _
        // *                                                  ByVal csAtenaHS As DataSet, _
        // *                                                  ByVal csAtenaD As DataSet, _
        // *                                                  ByVal csAtenaDS As DataSet) As DataSet
        // * 
        // * 機能　　    　　各宛名情報データセットをマージする
        // * 
        // * 引数           csAtenaH As DataSet   : 宛名データ
        // *                csAtenaHS As DataSet  : 送付先データ
        // *                csAtenaD  As DataSet  : 代納データ
        // *                csAtenaDS As DataSet  : 代納送付先データ
        // * 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        // *履歴番号 000020 2003/11/19 修正開始
        // Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, ByVal csAtenaHS As DataSet, _
        // ByVal csAtenaD As DataSet, ByVal csAtenaDS As DataSet) As DataSet
        private DataSet CreateAtenaDataSet(DataSet csAtenaH, DataSet csAtenaHS, DataSet csAtenaD, DataSet csAtenaDS, bool blnKobetsu, ABEnumDefine.HyojunKB intHyojunKB)
        {
            // *履歴番号 000020 2003/11/19 修正終了
            const string THIS_METHOD_NAME = "CreateAtenaDataSet";
            DataSet csAtena1;                             // 宛名情報(ABAtena1)
                                                          // * corresponds to VS2008 Start 2010/04/16 000044
                                                          // Dim csRow As DataRow
                                                          // Dim csNewRow As DataRow
                                                          // * corresponds to VS2008 End 2010/04/16 000044
                                                          // Dim cABCommon As ABCommonClass                      '宛名業務共通クラス
            string strTableName;

            try
            {

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // ログ出力用クラスインスタンス化
                // m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)
                // * 履歴番号 000023 2004/08/27 削除終了

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名業務共通クラスのインスタンス化
                // cABCommon = New ABCommonClass()

                // 宛名情報のインスタンス化
                csAtena1 = new DataSet();

                if (blnKobetsu)
                {
                    if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        strTableName = ABAtena1KobetsuHyojunEntity.TABLE_NAME;
                    }
                    else
                    {
                        strTableName = ABAtena1KobetsuEntity.TABLE_NAME;
                    }
                }
                else if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                {
                    strTableName = ABAtena1HyojunEntity.TABLE_NAME;
                }
                else
                {
                    strTableName = ABAtena1Entity.TABLE_NAME;
                }

                // 宛名データ存在チェック
                if (csAtenaH is not null)
                {
                    // '*履歴番号 000020 2003/11/19 修正開始
                    /// 宛名情報に宛名データを追加する
                    // 'csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))

                    // If (blnKobetsu) Then
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                    // Else
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))
                    // End If
                    // '*履歴番号 000020 2003/11/19 修正終了
                    // 宛名情報に宛名データを追加する
                    csAtena1.Merge(csAtenaH.Tables[strTableName]);
                }

                // 代納データ存在チェック
                if (csAtenaD is not null)
                {
                    // '*履歴番号 000020 2003/11/19 修正開始
                    /// 代納データを追加する
                    // 'csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))

                    // If (blnKobetsu) Then
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaD.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                    // Else
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))
                    // End If
                    // '*履歴番号 000020 2003/11/19 修正終了
                    // 宛名情報に代納データを追加する
                    csAtena1.Merge(csAtenaD.Tables[strTableName]);
                }

                // 送付先データ存在チェック
                if (csAtenaHS is not null)
                {
                    // '*履歴番号 000020 2003/11/19 修正開始
                    /// 送付先データを追加する
                    // 'csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))

                    // If (blnKobetsu) Then
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaHS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                    // Else
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))
                    // End If
                    // '*履歴番号 000020 2003/11/19 修正終了
                    // 宛名情報に送付先データを追加する
                    csAtena1.Merge(csAtenaHS.Tables[strTableName]);
                }

                // 代納送付先データ存在チェック
                if (csAtenaDS is not null)
                {
                    // '*履歴番号 000020 2003/11/19 修正開始
                    /// 代納送付先データを追加する
                    // 'csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))

                    // If (blnKobetsu) Then
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaDS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                    // Else
                    // '宛名情報に宛名データを追加する
                    // csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))
                    // End If
                    // '*履歴番号 000020 2003/11/19 修正終了
                    // 宛名情報に代納送付先データを追加する
                    csAtena1.Merge(csAtenaDS.Tables[strTableName]);
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

            return csAtena1;

        }
        #endregion

        #region  連絡先編集処理(RenrakusakiHenshu) 
        // *履歴番号 000022 2003/12/02 追加開始
        // ************************************************************************************************
        // * メソッド名     連絡先編集処理
        // * 
        // * 構文           Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, 
        // * 　　                                         ByVal strGyomunaiSHU_CD As String, 
        // * 　　                                         ByRef csAtenaH As DataSet,
        // * 　　                                         ByRef csOrgAtena As DataSet)
        // * 
        // * 機能　　    　　連絡先を取得して、編集する
        // * 
        // * 引数           strGyomuCD As String          : 業務コード
        // * 　　           strGyomunaiSHU_CD As String   : 業務内種別コード
        // *                csAtenaH  As DataSet          : 本人データ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        // Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, ByVal strGyomunaiSHU_CD As String, ByRef csAtenaH As DataSet)
        private void RenrakusakiHenshu(string strGyomuCD, string strGyomunaiSHU_CD, ref DataSet csAtenaH, ref DataSet csOrgAtena, ABEnumDefine.HyojunKB intHyojunKB, string strKikanYMD)
        {
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' 連絡先Ｂクラス
            // * 履歴番号 000023 2004/08/27 削除終了
            DataSet csRenrakusakiEntity;                  // 連絡先DataSet
            DataRow csRenrakusakiRow;                     // 連絡先Row
            var csAtena1Table = default(DataTable);                      // AtenaTable

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // 業務コードが存在しない場合は、処理しない
                if (string.IsNullOrEmpty(strGyomuCD.Trim()))
                {
                    return;
                }

                // 連絡先Ｂクラスのインスタンス作成
                // cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                if (m_cRenrakusakiBClass is null)
                {
                    m_cRenrakusakiBClass = new ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }

                if (csAtenaH.Tables.Contains(ABAtena1Entity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABAtena1Entity.TABLE_NAME);
                }
                else if (csAtenaH.Tables.Contains(ABNenkinAtenaEntity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABNenkinAtenaEntity.TABLE_NAME);
                }
                else if (csAtenaH.Tables.Contains(ABAtena1KobetsuEntity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME);
                }
                else if (csAtenaH.Tables.Contains(ABAtena1HyojunEntity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABAtena1HyojunEntity.TABLE_NAME);
                }
                else if (csAtenaH.Tables.Contains(ABNenkinAtenaHyojunEntity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABNenkinAtenaHyojunEntity.TABLE_NAME);
                }
                else if (csAtenaH.Tables.Contains(ABAtena1KobetsuHyojunEntity.TABLE_NAME))
                {
                    csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuHyojunEntity.TABLE_NAME);
                }
                else
                {
                    // システムエラー
                }

                // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
                int intCount = 0;
                DataRow csAtenaRow;
                // * 履歴番号 000024 2005/01/25 追加終了

                foreach (DataRow csRow in csAtena1Table.Rows)
                {
                    // * 履歴番号 000024 2005/01/25 追加開始（宮沢）IF文を追加
                    csAtenaRow = csOrgAtena.Tables[0].Rows[intCount];
                    if (!object.ReferenceEquals(csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT), DBNull.Value))
                    {
                        if (Conversions.ToInteger(csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT)) > 0)
                        {
                            // * 履歴番号 000024 2005/01/25 追加終了（宮沢）IF文を追加
                            // 連絡先データを取得する
                            csRenrakusakiEntity = m_cRenrakusakiBClass.GetRenrakusakiBHoshu_Hyojun(Conversions.ToString(csRow(ABAtena1Entity.JUMINCD)), strGyomuCD, strGyomunaiSHU_CD, strKikanYMD);
                            if (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count != 0)
                            {
                                csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0);
                                // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
                                csRenrakusakiRow.BeginEdit();
                                // * 履歴番号 000023 2004/08/27 追加終了
                                // 連絡先１
                                if (Conversions.ToString(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)).Trim != "03" && Conversions.ToString(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)).RLength <= 15)
                                {
                                    csRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1);
                                }
                                // 連絡先２
                                if (Conversions.ToString(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)).Trim != "03" && Conversions.ToString(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)).RLength <= 15)
                                {
                                    csRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2);
                                }
                                switch (csAtena1Table.TableName ?? "")
                                {
                                    case var @case when @case == ABNenkinAtenaEntity.TABLE_NAME:
                                    case var case1 when case1 == ABNenkinAtenaHyojunEntity.TABLE_NAME:
                                        {
                                            // 連絡先取得業務コード
                                            csRow(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD;
                                            break;
                                        }
                                    case var case2 when case2 == ABAtena1KobetsuEntity.TABLE_NAME:
                                    case var case3 when case3 == ABAtena1KobetsuHyojunEntity.TABLE_NAME:
                                        {
                                            // 連絡先取得業務コード
                                            csRow(ABAtena1KobetsuEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD;
                                            break;
                                        }
                                    // *履歴番号 000030 2007/04/21 修正開始
                                    case var case4 when case4 == ABAtena1Entity.TABLE_NAME:
                                    case var case5 when case5 == ABAtena1HyojunEntity.TABLE_NAME:
                                        {
                                            // *履歴番号 000042 2008/11/18 修正開始
                                            // メソッド区分が介護の場合のみセットする
                                            // 連絡先取得業務コード (介護用テーブルの場合のみセットする。項目数68個以上は介護用テーブルとみなす。)
                                            // If csRow.ItemArray.Length > 67 Then
                                            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                                            {
                                                csRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD;
                                            }

                                            break;
                                        }
                                        // *履歴番号 000042 2008/11/18 修正終了
                                        // *履歴番号 000030 2007/04/21 修正終了
                                }
                                // * 履歴番号 000023 2004/08/27 追加開始（宮沢）

                                if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 連絡先区分
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIKB);
                                    // 連絡先名
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIMEI);
                                    // 連絡先１
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1);
                                    // 連絡先２
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2);
                                    // 連絡先３
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI3);
                                    // 連絡先種別１
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                                    // 連絡先種別２
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                                    // 連絡先種別３
                                    csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                                }
                                else
                                {
                                }

                                csRenrakusakiRow.EndEdit();
                                // * 履歴番号 000023 2004/08/27 追加終了
                            }
                        }
                    }
                    intCount = intCount + 1;
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

        }
        // *履歴番号 000022 2003/12/02 追加終了
        #endregion

        // *履歴番号 000031 2007/07/28 追加開始
        #region  同一人代表者住民コード取得(GetDaihyoJuminCD)
        // ************************************************************************************************
        // * メソッド名     同一人代表者住民コード取得
        // * 
        // * 構文           Private Sub GetDaihyoJuminCD(ByRef cAtenaGetPara1 As ABAtenaGetPara1XClass)
        // * 
        // * 機能　　    　　住民コードセット
        // * 
        // * 引数           cAtenaGetPara1　：　検索パラめー亜
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void GetDaihyoJuminCD(ref ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            const string THIS_METHOD_NAME = "GetDaihyoJuminCD";
            // * corresponds to VS2008 Start 2010/04/16 000044
            // Dim strDaihyoJuminCD As String                  '代表者住民コード
            // * corresponds to VS2008 End 2010/04/16 000044

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件により、代表者取得の処理を行う
                if (cAtenaGetPara1.p_strJuminCD != string.Empty && cAtenaGetPara1.p_strJukiJutogaiKB == "" && cAtenaGetPara1.p_strDaihyoShaKB == "")
                {

                    // 管理情報取得を行う
                    if (string.IsNullOrEmpty(m_strDoitsu_Param))
                    {
                        // メンバに無い場合のみインスタンス化を行う
                        if (m_cABAtenaKanriJohoB is null)
                        {
                            m_cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        // 管理情報より取得
                        m_strDoitsu_Param = m_cABAtenaKanriJohoB.GetDoitsuHantei_Param();
                    }

                    // 管理情報により、同一人代表者取得を行うか判定する
                    if (m_strDoitsu_Param == ABConstClass.PRM_DAIHYO)
                    {
                        // 住民コードを退避させる
                        m_strHonninJuminCD = cAtenaGetPara1.p_strJuminCD.Trim;
                        // メンバに無い場合のみインスタンス化を行う
                        if (m_cABGappeiDoitsuninB is null)
                        {
                            m_cABGappeiDoitsuninB = new ABGappeiDoitsuninBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }

                        // 同一人代表者の情報を取得し、検索パラメータへセットする
                        cAtenaGetPara1.p_strJuminCD = m_cABGappeiDoitsuninB.GetDoitsuninDaihyoJuminCD(m_strHonninJuminCD);
                    }
                    else
                    {
                        // 退避用住民コードをクリアする
                        m_strHonninJuminCD = string.Empty;
                    }
                }
                else
                {
                    // *履歴番号 000037 2008/01/17 追加開始
                    // 退避用住民コードをクリアする
                    m_strHonninJuminCD = string.Empty;
                    // *履歴番号 000037 2008/01/17 追加終了
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

        }
        #endregion

        #region  住民コードセット(SetJuminCD) 
        // ************************************************************************************************
        // * メソッド名     住民コードセット（内部処理）
        // * 
        // * 構文           Private Sub SetJuminCD(ByRef csDataSet As DataSet)
        // * 
        // * 機能　　    　　住民コードセット
        // * 
        // * 引数           csDataSet　：　宛名データセット
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetJuminCD(ref DataSet csDataSet)
        {
            const string THIS_METHOD_NAME = "SetJuminCD";
            int intCnt;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 退避した住民コードが存在する場合は、上書きする
                if (!string.IsNullOrEmpty(m_strHonninJuminCD))
                {

                    // テーブル名によって場合分けを行う(テーブルは必ず１つしかない)
                    switch (csDataSet.Tables[0].TableName ?? "")
                    {
                        case var @case when @case == ABAtena1Entity.TABLE_NAME:
                        case var case1 when case1 == ABAtena1KobetsuEntity.TABLE_NAME:
                        case var case2 when case2 == ABAtena1HyojunEntity.TABLE_NAME:
                        case var case3 when case3 == ABAtena1KobetsuHyojunEntity.TABLE_NAME:
                            {
                                // 同一人代表者取得を行った場合は、退避した住民コード(本人)で上書きする
                                var loopTo = csDataSet.Tables[0].Rows.Count - 1;
                                for (intCnt = 0; intCnt <= loopTo; intCnt++)
                                {
                                    // 本人・送付先（本人）レコードのみ上書きする
                                    if (Conversions.ToString(csDataSet.Tables[0].Rows[intCnt].Item(ABAtena1Entity.DAINOKB)) == ABConstClass.DAINOKB_HONNIN || Conversions.ToString(csDataSet.Tables[0].Rows[intCnt].Item(ABAtena1Entity.DAINOKB)) == ABConstClass.DAINOKB_H_SFSK)
                                    {
                                        csDataSet.Tables[0].Rows[intCnt].Item(ABAtena1Entity.JUMINCD) = m_strHonninJuminCD;
                                    }
                                }

                                break;
                            }

                        default:
                            {
                                // 同一人代表者取得を行った場合は、退避した住民コード(本人)で上書きする
                                var loopTo1 = csDataSet.Tables[0].Rows.Count - 1;
                                for (intCnt = 0; intCnt <= loopTo1; intCnt++)
                                    csDataSet.Tables[0].Rows[intCnt].Item(ABAtenaEntity.JUMINCD) = m_strHonninJuminCD;
                                break;
                            }

                    }
                }
                else
                {
                    // 何もしない
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

        }
        #endregion
        // *履歴番号 000031 2007/07/28 追加終了

        // *履歴番号 000032 2007/09/04 追加開始
        #region  検索カナ姓名・検索カナ名・検索カナ名編集(HenshuSearchKana)
        // ************************************************************************************************
        // * メソッド名     検索カナ姓名・検索カナ名・検索カナ名編集
        // * 
        // * 構文           Private Function HenshuSearchKana(ByRef cSearchKey As ABAtenaSearchKey,
        // *                                                  ByRef blnHommyoYusen As Boolean) As ABAtenaSearchKey 
        // * 
        // * 機能　　    　 宛名検索のカナ姓名を標準仕様と外国人本名検索機能用に編集する
        // * 
        // * 引数           ABAtenaSearchKey　：　宛名検索キーパラメータ
        // * 
        // * 戻り値         ABAtenaSearchKey　：　宛名検索キーパラメータ
        // ************************************************************************************************
        [SecuritySafeCritical]
        private ABAtenaSearchKey HenshuSearchKana(ABAtenaSearchKey cSearchKey, bool blnHommyoYusen)
        {
            const string THIS_METHOD_NAME = "HenshuSearchKana";

            ABAtenaSearchKey cSearch_Param; // 宛名検索キーパラメータ
            string HenshuKanaSeiMei = string.Empty;  // 編集検索用カナ姓名(英文字は大文字で格納すること)
            string HenshuKanaSei = string.Empty;     // 編集検索用カナ姓(英文字は大文字で格納すること)
            string HenshuKanaMei = string.Empty;     // 編集検索用カナ名(英文字は大文字で格納すること)
                                                     // * 履歴番号 000034 2007/10/10 追加開始
            string HenshuKanaSei2 = string.Empty;    // 編集検索用カナ姓２(英文字は大文字で格納すること)
            var cuString = new USStringClass();              // ミドルネーム等清音化
                                                             // * 履歴番号 000034 2007/10/10 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名検索キーパラメータをコピー
                cSearch_Param = cSearchKey;

                // 外国人本名検索機能初期設定を宛名検索キーパラメータに設定
                cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho;

                // 標準仕様の場合は何も編集せずにそのまま返す
                // 外国人本名優先検索機能が導入された市町村は
                // ＤＢ項目が専用なので(検索用カナ姓名・検索用カナ姓・検索用カナ名・検索用漢字名称をそれぞれ再セット)
                if (m_cURKanriJohoB.GetFrn_HommyoKensaku_Param() == 2)
                {
                    // 外国人本名検索機能を宛名検索キーパラメータに設定
                    cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki;
                    if (blnHommyoYusen == true)
                    {
                        // 検索パラメータの編集
                        // *履歴番号 000036 2007/11/06 追加開始
                        // 検索カナ姓名が有り、検索カナ姓が無しの場合、検索カナ姓名は検索カナ姓と同様の扱いをする
                        if (cSearchKey.p_strSearchKanaSeiMei != string.Empty && cSearchKey.p_strSearchKanaSei == string.Empty)
                        {
                            cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper();
                        }
                        // '検索カナ姓名を検索カナ姓の検索キーパラメータとしてセット
                        // If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                        // HenshuKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                        // End If
                        // *履歴番号 000036 2007/11/06 追加終了
                        // 検索カナ姓を検索カナ姓の検索キーパラメータとしてセット
                        if (cSearchKey.p_strSearchKanaSei != string.Empty)
                        {
                            // *履歴番号 000036 2007/11/06 修正開始
                            // 検索用カナ姓のアルファベットを大文字に変換する
                            HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper();
                            // '検索カナ姓の文字の最後に"%"を必ず付加する
                            // If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                            // HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()
                            // Else
                            // HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                            // End If
                            // *履歴番号 000036 2007/11/06 修正終了
                        }
                        // カナ姓とカナ名がある場合，結合して検索カナ姓の検索キーパラメータとしてセット
                        // 全ての検索カナ項目で検索がかけられた場合はこの検索キーがセットされる
                        if (cSearchKey.p_strSearchKanaSei != string.Empty && cSearchKey.p_strSearchKanaMei != string.Empty)
                        {
                            // * 履歴番号 000034 2007/10/10 追加開始
                            // カナ名の先頭文字が"ｳ"の場合のみ"ｵ"に置換して検索用カナ姓２を生成する
                            if (cSearchKey.p_strSearchKanaMei.StartsWith("ｳ"))
                            {
                                // カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                                if (Strings.InStr(cSearchKey.p_strSearchKanaMei, " ") != 0)
                                {
                                    HenshuKanaSei2 = HenshuKanaSei + cuString.ToKanaKey(Strings.Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).Replace(" ", string.Empty)).ToUpper();
                                }
                                else
                                {
                                    HenshuKanaSei2 = HenshuKanaSei + Strings.Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).ToUpper();
                                }
                            }
                            // カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                            if (Strings.InStr(cSearchKey.p_strSearchKanaMei, " ") != 0)
                            {
                                HenshuKanaSei = HenshuKanaSei + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", string.Empty)).ToUpper();
                            }
                            else
                            {
                                HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper();
                            }
                            // HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper()
                            // * 履歴番号 000034 2007/10/10 追加終了
                        }
                        // カナ名のみの場合，先頭に％を加え検索カナ姓の検索キーパラメータとしてセット
                        if (cSearchKey.p_strSearchKanaSei == string.Empty && cSearchKey.p_strSearchKanaMei != string.Empty)
                        {
                            // * 履歴番号 000034 2007/10/10 追加開始
                            // カナ名の先頭文字が"ｳ"の場合のみ"ｵ"に置換して検索用カナ姓２を生成する
                            if (cSearchKey.p_strSearchKanaMei.StartsWith("ｳ"))
                            {
                                // カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                                if (Strings.InStr(cSearchKey.p_strSearchKanaMei, " ") != 0)
                                {
                                    HenshuKanaSei2 = "%" + cuString.ToKanaKey(Strings.Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).Replace(" ", string.Empty)).ToUpper();
                                }
                                else
                                {
                                    HenshuKanaSei2 = "%" + Strings.Replace(cSearchKey.p_strSearchKanaMei, "ｳ", "ｵ", 1, 1).ToUpper();
                                }
                            }
                            // カナ名に含まれるミドルネーム等でも検索ヒットするようにスペースがある場合はスペース除去し清音化を行う
                            if (Strings.InStr(cSearchKey.p_strSearchKanaMei, " ") != 0)
                            {
                                HenshuKanaSei = "%" + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", string.Empty)).ToUpper();
                            }
                            else
                            {
                                HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper();
                            }
                            // HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper()
                            // * 履歴番号 000034 2007/10/10 追加終了
                        }
                        // 検索用カナ姓２に編集した検索キーを検索キーパラメータにセット
                        // 本名の検索パラメータをセット
                        cSearch_Param.p_strSearchKanaSeiMei = string.Empty;
                        cSearch_Param.p_strSearchKanaSei = HenshuKanaSei;                            // カナは検索カナ姓の項目のみで検索
                        cSearch_Param.p_strSearchKanaMei = string.Empty;
                        cSearch_Param.p_strSearchKanaSei2 = HenshuKanaSei2;                    // 検索用カナ姓２
                                                                                               // 検索漢字名称
                        cSearch_Param.p_strKanjiMeisho2 = cSearchKey.p_strSearchKanjiMeisho;         // 漢字名称２に検索用漢字名称をセット
                        cSearch_Param.p_strSearchKanjiMeisho = string.Empty;
                    }
                    else
                    {
                        // 検索パラメータの編集
                        // *履歴番号 000036 2007/11/06 追加開始
                        // 検索カナ姓名が有り、検索カナ姓が無しの場合、検索カナ姓名は検索カナ姓と同様の扱いをする
                        if (cSearchKey.p_strSearchKanaSeiMei != string.Empty && cSearchKey.p_strSearchKanaSei == string.Empty)
                        {
                            cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper();
                        }
                        // '検索カナ姓名を検索カナ姓名の検索キーパラメータとしてセット
                        // If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                        // HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                        // End If
                        // *履歴番号 000036 2007/11/06 追加終了
                        // 検索カナ姓がある場合は検索カナ姓名にパラメータをセット
                        if (cSearchKey.p_strSearchKanaSei != string.Empty)
                        {
                            // *履歴番号 000036 2007/11/06 修正開始
                            // 検索カナ姓と検索カナ名の両方に"%"が無い場合は完全一致
                            if (Strings.InStr(cSearchKey.p_strSearchKanaSei, "%") == 0 && Strings.InStr(cSearchKey.p_strSearchKanaMei, "%") == 0)
                            {
                                // 完全一致時のみ検索カナ姓名として結合するので、清音化を行う
                                HenshuKanaSeiMei = cuString.ToKanaKey(cSearchKey.p_strSearchKanaSei + cSearchKey.p_strSearchKanaMei).ToUpper();
                            }
                            else
                            {
                                // "%"がある場合はそのまま検索カナ姓名に大文字化してセット
                                // ただし"%"のみの場合は何もセットしない
                                if (cSearchKey.p_strSearchKanaSei != "%")
                                {
                                    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper();
                                }
                                // 検索カナ名をアルファベット大文字化してセット
                                if (cSearchKey.p_strSearchKanaMei != string.Empty)
                                {
                                    HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper();
                                }
                            }
                        }
                        // '検索カナ姓の文字の最後に"%"を必ず付加し，検索カナ姓名の検索キーパラメータとしてセット
                        // If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                        // HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        // Else
                        // HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                        // End If
                        // '検索カナ名をアルファベット大文字化してセット
                        // If (cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        // HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()
                        // End If
                        // *履歴番号 000036 2007/11/06 修正終了
                        else
                        {
                            // 検索カナ名
                            HenshuKanaMei = cSearch_Param.p_strSearchKanaMei.ToUpper();
                        }
                        // 検索用カナ姓２に編集した検索キーを検索キーパラメータにセット
                        // 通称名の検索パラメータをセット
                        cSearch_Param.p_strSearchKanaSeiMei = HenshuKanaSeiMei;                      // カナ姓名，カナ姓
                        cSearch_Param.p_strSearchKanaSei = string.Empty;
                        cSearch_Param.p_strSearchKanaMei = HenshuKanaMei;                            // カナ名
                        cSearch_Param.p_strSearchKanaSei2 = string.Empty;                         // 検索用カナ姓２（空にする）
                                                                                                  // 検索漢字名称
                        cSearch_Param.p_strSearchKanjiMeisho = cSearchKey.p_strSearchKanjiMeisho;    // 検索用漢字名称に検索用漢字名称をセット
                        cSearch_Param.p_strKanjiMeisho2 = string.Empty;
                    }
                }
                // * 履歴番号 000034 2007/10/10 追加開始
                else
                {
                    // 標準仕様の市町村においても検索カナ項目のアルファベットは大文字で扱う
                    cSearch_Param.p_strSearchKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper(); // カナ姓名
                    cSearch_Param.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper();       // カナ姓
                    cSearch_Param.p_strSearchKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper();       // カナ名
                    cSearch_Param.p_strSearchKanaSei2 = string.Empty;
                    // * 履歴番号 000034 2007/10/10 追加終了
                }                              // 検索用カナ姓２（空にする）

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return cSearch_Param;

        }
        #endregion
        // *履歴番号 000032 2007/09/04 追加終了

        // *履歴番号 000040 2008/11/10 追加開始
        #region  利用届編集処理(RiyoTdkHenshu) 
        // ************************************************************************************************
        // * メソッド名     利用届編集処理
        // * 
        // * 構文           Private Sub RiyoTdkHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
        // * 　　                                     ByVal blnKobetsu As Boolean, 
        // * 　　                                     ByRef csAtenaH As DataSet)
        // * 
        // * 機能　　    　 利用届データを取得し、宛名データセットへセットする
        // * 
        // * 引数           cAtenaGetPara1 As ABAtenaGetPara1XClass   : 宛名取得パラメータ
        // * 　　           blnKobetsu As Boolean                     : 個別事項判定フラグ
        // *                csAtenaH As DataSet                       : 本人データ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void RiyoTdkHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, bool blnKobetsu, ref DataSet csAtenaH)
        {
            ABLTRiyoTdkBClass cABLTRiyoTdkB;                      // ABeLTAX利用届マスタＤＡ
            ABLTRiyoTdkParaXClass cABLTRiyoTdkParaX;              // ABeLTAX利用届パラメータクラス
            DataSet csRiyoTdkEntity;                              // 利用届データセット
            DataRow csRiyoTdkRow;                                 // 利用届データセット
            DataRow csRow;
            // *履歴番号 000041 2008/11/17 追加開始
            DataRow[] csNotRiyouTdkdRows;
            // *履歴番号 000041 2008/11/17 追加終了

            do
            {
                try
                {
                    // デバッグ開始ログ出力
                    m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                    // *履歴番号 000041 2008/11/17 追加開始
                    if (!(cAtenaGetPara1.p_strShiteiYMD == string.Empty))
                    {
                        break;
                    }
                    else
                    {
                    }
                    // *履歴番号 000041 2008/11/17 追加終了

                    // *履歴番号 000042 2008/11/18 修正開始
                    // If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
                    // blnKobetsu = False AndAlso (cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2")) Then
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly && blnKobetsu == false && m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && (cAtenaGetPara1.p_strTdkdKB == "1" || cAtenaGetPara1.p_strTdkdKB == "2"))
                    {
                        // *履歴番号 000042 2008/11/18 修正終了
                        // 簡易版ではない場合かつ個別事項取得しない場合かつ利用届出取得区分が"1,2"の場合、納税者IDと利用者IDをセット

                        // ABeLTAX利用届マスタＤＡクラスのインスタンス作成
                        cABLTRiyoTdkB = new ABLTRiyoTdkBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                        // ABeLTAX利用届パラメータクラスのインスタンス化
                        cABLTRiyoTdkParaX = new ABLTRiyoTdkParaXClass();

                        // 取得データセット処理
                        foreach (DataRow currentCsRow in csAtenaH.Tables[0].Rows)
                        {
                            csRow = currentCsRow;

                            // 利用届出パラメータセット
                            // 住民コード
                            if (string.IsNullOrEmpty(m_strHonninJuminCD.Trim()))
                            {
                                // 住民コードをセット
                                cABLTRiyoTdkParaX.p_strJuminCD = Conversions.ToString(csRow(ABAtena1Entity.JUMINCD));
                            }
                            else
                            {
                                // 同一人代表者データのため、本人住民コードをセット
                                cABLTRiyoTdkParaX.p_strJuminCD = m_strHonninJuminCD;
                            }

                            // 税目コード:業務コードをセット
                            cABLTRiyoTdkParaX.p_strZeimokuCD = cAtenaGetPara1.p_strTdkdZeimokuCD;

                            // 廃止フラグ:廃止データ以外を取得
                            cABLTRiyoTdkParaX.p_blnHaishiFG = false;

                            // 出力区分:納税者ID、利用者IDの２項目を取得
                            cABLTRiyoTdkParaX.p_strOutKB = "1";

                            // *履歴番号 000051 2020/11/02 追加開始
                            // 利用区分：利用届出利用区分をセット
                            cABLTRiyoTdkParaX.p_strRiyoKB = cAtenaGetPara1.p_strTdkdRiyoKB;
                            // *履歴番号 000051 2020/11/02 追加終了

                            // 利用届出データを取得
                            csRiyoTdkEntity = cABLTRiyoTdkB.GetLTRiyoTdkData(cABLTRiyoTdkParaX);

                            // 利用届出データを本人データにセット
                            csRow.BeginEdit();
                            if (csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Count != 0)
                            {
                                csRiyoTdkRow = csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows(0);

                                csRow(ABAtena1Entity.NOZEIID) = csRiyoTdkRow(ABLtRiyoTdkEntity.NOZEIID);         // 納税者ID
                                csRow(ABAtena1Entity.RIYOSHAID) = csRiyoTdkRow(ABLtRiyoTdkEntity.RIYOSHAID);     // 利用者ID
                            }
                            else
                            {
                                csRow(ABAtena1Entity.NOZEIID) = string.Empty;                                    // 納税者ID
                                csRow(ABAtena1Entity.RIYOSHAID) = string.Empty;

                            }                                  // 利用者ID
                            csRow.EndEdit();
                        }

                        // *履歴番号 000041 2008/11/17 追加開始
                        if (cAtenaGetPara1.p_strTdkdKB == "2")
                        {
                            // 本人データから納税者IDが空白のデータを取得する
                            csNotRiyouTdkdRows = csAtenaH.Tables[0].Select(ABAtena1Entity.NOZEIID + " = ''");

                            // 納税者IDが空白のデータを削除する
                            foreach (var currentCsRow1 in csNotRiyouTdkdRows)
                            {
                                csRow = currentCsRow1;
                                csRow.Delete();
                            }
                        }
                        else
                        {
                        }
                    }
                    // *履歴番号 000041 2008/11/17 追加終了
                    else
                    {
                    }

                    // デバッグ終了ログ出力
                    m_cfLogClass.DebugEndWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }

                catch (UFAppException objAppExp)
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                    // エラーをそのままスローする
                    throw;
                }

                catch (Exception objExp)
                {
                    // エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");
                    throw;
                }
            }
            while (false);

        }
        #endregion

        // *履歴番号 000041 2008/11/17 削除開始
        #region  利用届データ絞込み(RiyoTdkHenshu_Select) 
        // '************************************************************************************************
        // '* メソッド名     利用届編集処理
        // '* 
        // '* 構文           Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
        // '* 　　                                            ByVal blnKobetsu As Boolean, 
        // '* 　　                                            ByRef csAtenaH As DataSet)
        // '* 
        // '* 機能　　    　 本人データから納税者IDが存在しないレコードを削除する
        // '* 
        // '* 引数           cAtenaGetPara1 As ABAtenaGetPara1XClass   : 宛名取得パラメータ
        // '* 　　           blnKobetsu As Boolean                     : 個別事項判定フラグ
        // '*                csAtenaH As DataSet                       : 本人データ
        // '* 
        // '* 戻り値         なし
        // '************************************************************************************************
        // Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal blnKobetsu As Boolean, ByRef csAtena1 As DataSet)
        // Dim csRow As DataRow
        // Dim csNotRiyouTdkdRows As DataRow()

        // Try
        // 'デバッグ開始ログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        // If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
        // blnKobetsu = False AndAlso cAtenaGetPara1.p_strTdkdKB = "2") Then
        // ' 簡易版ではない場合かつ個別事項取得しない場合かつ利用届出取得区分が"2"の場合、納税者IDが存在しないデータを削除する

        // ' 本人データから納税者IDが空白のデータを取得する
        // csNotRiyouTdkdRows = csAtena1.Tables(0).Select(ABAtena1Entity.NOZEIID + " = ''")

        // ' 納税者IDが空白のデータを削除する
        // For Each csRow In csNotRiyouTdkdRows
        // csRow.Delete()
        // Next
        // Else
        // End If

        // ' デバッグ終了ログ出力
        // m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        // Catch objAppExp As UFAppException
        // ' ワーニングログ出力
        // m_cfLogClass.WarningWrite(m_cfControlData, _
        // "【クラス名:" + Me.GetType.Name + "】" + _
        // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        // "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + objAppExp.Message + "】")
        // ' エラーをそのままスローする
        // Throw

        // Catch objExp As Exception
        // ' エラーログ出力
        // m_cfLogClass.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + Me.GetType.Name + "】" + _
        // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
        // "【エラー内容:" + objExp.Message + "】")
        // Throw
        // End Try

        // End Sub
        #endregion
        // *履歴番号 000041 2008/11/17 削除終了
        // *履歴番号 000040 2008/11/10 追加終了

        // *履歴番号 000052 2023/03/10 追加開始
        #region  簡易宛名取得１_標準版(AtenaGet1_Hyojun) 
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得１_標準版
        // * 
        // * 構文           Public Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaGet1_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1)
        {

            return AtenaGet1_Hyojun(cAtenaGetPara1, false);

        }

        // ************************************************************************************************
        // * メソッド名     簡易宛名取得１_標準版
        // * 
        // * 構文           Public Function AtenaGet1_Hyoujn(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 　　           blnKobetsu       : 個別取得(True:各個別マスタよりデータを取得する)
        // * 
        // * 戻り値         DataSet(ABAtena1Kobetsu) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaGet1_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1, bool blnKobetsu)
        {

            return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Hyojun);

        }
        #endregion

        #region  簡易宛名取得２_標準版(AtenaGet2_Hyojun) 
        // ************************************************************************************************
        // * メソッド名     簡易宛名取得２_標準版
        // * 
        // * 構文           Public Function AtenaGet2_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaGet2_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            const string THIS_METHOD_NAME = "AtenaGet2_Hyojun";
            DataSet csAtenaEntity;                        // 宛名Entity
            var blnAtenaSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnAtenaKani = default(bool);
            var blnRirekiSelectAll = default(ABEnumDefine.AtenaGetKB);
            var blnRirekiKani = default(bool);

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    m_cfRdbClass.Connect();
                }

                try
                {
                    // コンストラクタの設定を保存
                    if (m_cABAtenaB is not null)
                    {
                        blnAtenaSelectAll = m_cABAtenaB.m_blnSelectAll;
                        blnAtenaKani = m_cABAtenaB.m_blnSelectCount;
                        m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                        m_cABAtenaB.m_blnSelectCount = false;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        blnRirekiSelectAll = m_cABAtenaRirekiB.m_blnSelectAll;
                        blnRirekiKani = m_cABAtenaRirekiB.m_blnSelectCount;
                        m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = false;

                    }

                    // 簡易宛名取得２(内部処理)メソッドを実行する。
                    csAtenaEntity = this.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun);

                    // コンストラクタの設定を元にもどす
                    if (m_cABAtenaB is not null)
                    {
                        m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll;
                        m_cABAtenaB.m_blnSelectCount = blnAtenaKani;
                    }
                    if (m_cABAtenaRirekiB is not null)
                    {
                        m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll;
                        m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani;
                    }
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtenaEntity;

        }
        #endregion

        #region  介護用宛名取得_標準版(GetKaigoAtena_Hyojun) 
        // ************************************************************************************************
        // * メソッド名     介護用宛名取得_標準版
        // * 
        // * 構文           Public Function GetKaigoAtena_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　    　　宛名を取得する
        // * 
        // * 引数           cAtenaGetPara1   : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet : 取得した宛名情報
        // ************************************************************************************************
        public DataSet GetKaigoAtena_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            ABEnumDefine.AtenaGetKB blnAtenaSelectAll;
            DataSet csAtenaEntity;                        // 介護用宛名Entity

            try
            {
                // コンストラクタの設定を保存
                blnAtenaSelectAll = m_blnSelectAll;
                m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                if (m_cABAtenaB is not null)
                {
                    m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                }
                if (m_cABAtenaRirekiB is not null)
                {
                    m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll;
                }

                // 宛名取得メインメソッドの呼出し（引数：取得パラメータクラス、個別事項データ取得フラグ、呼び出しメソッド区分）
                csAtenaEntity = AtenaGetMain(cAtenaGetPara1, false, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Hyojun);

                // コンストラクタの設定を元にもどす
                m_blnSelectAll = blnAtenaSelectAll;
                if (m_cABAtenaB is not null)
                {
                    m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll;
                }
                if (m_cABAtenaRirekiB is not null)
                {
                    m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll;
                }
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");
                throw objExp;
            }

            return csAtenaEntity;

        }
        #endregion

        #region  年金宛名取得_標準版(NenkinAtenaGet_Hyojun) 
        // ************************************************************************************************
        // * メソッド名     年金宛名取得_標準版
        // * 
        // * 構文           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       年金宛名情報を取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet NenkinAtenaGet_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1)
        {

            // 年金宛名ゲットより年金宛名情報を取得する
            return NenkinAtenaGet_Hyojun(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01);
        }

        // ************************************************************************************************
        // * メソッド名     年金宛名取得_標準版
        // * 
        // * 構文           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       年金宛名情報を取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet NenkinAtenaGet_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1, int intNenkinAtenaGetKB)
        {

            return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Hyojun);

        }
        #endregion

        #region  国保宛名履歴取得_標準版(KokuhoAtenaRirekiGet_Hyojun) 
        // ************************************************************************************************
        // * メソッド名     国保宛名履歴取得_標準版
        // * 
        // * 構文           Public Function KokuhoAtenaRirekiGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        // * 
        // * 機能　　       国保宛名履歴データを取得する
        // * 
        // * 引数           cAtenaGetPara1    : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet KokuhoAtenaRirekiGet_Hyojun(ABAtenaGetPara1XClass cAtenaGetPara1)
        {
            const string THIS_METHOD_NAME = "KokuhoAtenaRirekiGet_Hyojun";
            DataSet csAtena1Entity;                       // 宛名1Entity

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＲＤＢ接続
                if (m_blnBatchRdb == false)
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");
                    m_cfRdbClass.Connect();
                }

                try
                {
                    // 管理情報取得(内部処理)メソッドを実行する。
                    GetKanriJoho();

                    // 国保宛名履歴取得(内部処理)メソッドを実行する。
                    csAtena1Entity = this.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun);
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDB切断
                    if (m_blnBatchRdb == false)
                    {
                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");
                        m_cfRdbClass.Disconnect();
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

            return csAtena1Entity;

        }
        #endregion
        // *履歴番号 000052 2023/03/10 追加終了

        public ABAtenaGetBClass()
        {

        }
    }
}