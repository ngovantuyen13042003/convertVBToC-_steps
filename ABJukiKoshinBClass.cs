// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名住基更新(ABJukiKoshinBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/06/02　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/09/11 000001     住所コード桁相違対応
// * 2003/09/18 000002     修正
// * 2003/11/21 000003     仕様変更：年金・国保の個別情報を付けて、前後を出力（新規の場合は後のみ）
// * 2004/02/16 000004     追加・更新時にRⅢ連携（ワークフロー）処理を追加。レプリカDBへの当初登録
// * 2004/03/09 000005     固定資産税への登録
// * 2004/08/27 000006     固定資産税データ連携制御処理追加
// * 2004/10/20 000007     固定資産税連動個人法人区分を修正
// * 2005/02/15 000008     固定資産税連動追加処理以外の更新を追加
// * 2005/02/28 000009     レプリカ連動メソッドを追加（レプリカ連動起動箇所改修）
// * 2005/04/04 000010     固定資産税連動異動年月日を修正(マルゴ村山)
// * 2005/06/05 000011     履歴開始年月日を当日にする
// * 2005/06/07 000012     前履歴終了年月日を直近履歴開始年月日の前日にする
// * 2005/06/17 000013     履歴更新の修正
// * 2005/08/17 000014     宛名累積追加時、汎用ＣＤをABATENARUISEKIのRESERCEにセットする修正(前後区分２の時だけ)(マルゴ村山)
// * 2005/08/17 000015     宛名累積追加時、汎用ＣＤをABATENARUISEKIのRESERCEにセットする修正(前後区分１の時も)(マルゴ村山)
// * 2005/11/01 000016     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/11/22 000017     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/11/27 000018     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/02 000019     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/07 000020     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/12 000021     行政区ＣＤ＆行政区名称のカスタマイズ(マルゴ村山)
// * 2005/12/15 000022     仕様変更：行政区ＣＤ＆行政区名称のカスタマイズ　名称はセットしない
// * 2005/12/16 000023     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/17 000024     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/18 000025     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/18 000026     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/20 000027     JukiDataKoshinメソッドの修正(マルゴ村山)
// * 2005/12/27 000028     CKINJIYUCDにSHORIJIYUCDをセットしない（仕様変更）
// * 2006/04/19 000029     ABATENARUISEKIのRESERCEにセットする内容を汎用ＣＤから処理ＣＤに変更する
// * 2006/08/10 000030     履歴を残さない修正(SHORIJIYUCD="03"or"04")の場合、履歴開始年月日は更新しない
// *                       住基異動者追加処理直後に住基で"03"or"04"修正すると開始年月日がおかしくなってしまう(マルゴ村山)
// * 2007/01/30 000031     転出確定住所あり、転出予定住所ありの場合も番地コードを設定するように修正
// * 2007/02/15 000032     宛名累積マスタの更新方法を変更
// * 2007/07/13 000033     DB拡張対応，カラム作成時のMaxlength値を拡張後のDBのサイズに対応させる
// *                       （適用範囲が分散しているため履歴番号の付加無し，前数値のみコメントアウト）（中沢）
// * 2007/08/31 000034     UR管理情報：外国人本名検索制御が"2"のときは外国人本名優先検索用に本名カナ姓名をセット（中沢）
// * 2007/09/05 000035     UR番地コードマスタクラスのインスタンス化部分を修正（中沢）
// * 2007/09/28 000036     氏名利用区分のが１のときは通称名優先、２のときは本名優先（中沢）
// * 2008/05/12 000037     管内管外区分の編集仕様の変更に伴う修正（比嘉）
// * 2009/04/07 000038     番地CDが左詰になる不具合対応：転出確定･予定番地からの番地CD生成を番地CD編集Ｂｸﾗｽで行う（工藤）
// * 2009/05/12 000039     バッチフラグを追加、及びUR管理情報取得方法を一部変更（比嘉）
// * 2009/05/22 000040     住登外から再転入し、さらに転出した場合の住登外優先区分が"0"になる不具合の対応（吉澤）
// *                       さらに不要なロジックを削除（吉澤）
// * 2009/06/18 000041     履歴修正で履歴修正データが1件のみ(直近データのみ)の場合に履歴データが追加される不具合の対応（比嘉）
// * 2009/08/10 000042     履歴番号000041の改修漏れによる不具合対応（比嘉）
// * 2010/04/16 000043     VS2008対応（比嘉）
// * 2011/11/09 000044     【AB17020】住基法改正対応（中嶋）
// * 2011/11/28 000045     【AB17020】住基法改正対応：生年月日不詳区分編集仕様変更（大澤）
// * 2011/12/05 000046     【AB17020】住基法改正対応：リンクNo列型変更（大澤）
// * 2011/12/26 000047     【AB17020】住基法改正対応：転居の時に付随が更新されない不具合の対応（中嶋）
// * 2012/01/05 000048     【AB17020】住基法改正対応：履歴修正時、キー重複となるエラー修正（中嶋）
// * 2012/04/06 000049     【AB17020】住基法改正対応：履歴修正（住登外を住登内の間に入れる）時、異常終了する不具合修正（中嶋）
// * 2014/06/25 000050     【AB21051】＜共通番号対応＞共通番号更新処理追加（石合）
// * 2014/07/08 000051     【AB21051】＜共通番号対応＞共通番号更新処理事由追加（石合）
// * 2014/09/10 000052     【AB21051】＜共通番号対応＞共通番号更新処理事由追加２（石合）
// * 2014/09/10 000053     【AB21080】＜共通番号対応＞中間サーバーＢＳ連携機能追加（石合）
// * 2014/12/26 000054     【AB21051】＜共通番号対応＞共通番号更新処理事由追加３（石合）
// * 2015/01/08 000055     【AB21080】＜共通番号対応＞中間サーバーＢＳ連携機能削除（石合）
// * 2015/01/28 000056     【AB21051】＜共通番号対応＞共通番号更新処理修正（石合）
// * 2015/02/17 000057     【AB21051】＜共通番号対応＞共通番号更新処理修正（石合）
// * 2015/10/14 000058     【AB21051】＜共通番号対応＞本付番処理レコードに対する特殊処理への考慮追加（石合）
// * 2018/01/04 000059     【AB25001】旧氏併記対応（石合）
// * 2022/12/16 000060     【AB-8010】住民コード世帯コード15桁対応(下村)
// * 2023/08/14 000061     【AB-0820-1】住登外管理項目追加(早崎)
// * 2023/12/07 000062     【AB-9000-1】住基更新連携標準化対応(下村)
// * 2024/02/06 000063     【AB-1580-1】転出住所自動更新対応(掛川)
// * 2024/03/07 000064     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// * 2024/04/02 000065     【AB-6047-1】住基情報の異動に伴う他業務への各種情報提供のための連携(原)
// * 2024/06/10 000066     【AB-9902-1】不具合対応
// * 2024/06/18 000067     【AB-9903-1】不具合対応
// * 2024/07/05 000068     【AB-9907-1】氏名優先区分の対応
// * 2024/07/09 000069     【AB-9907-1】不具合対応　不詳生年月日DATEの編集
// ************************************************************************************************
// * ☆☆宛名累積の取得は、スキーマーが取得出来るようになれば、スキーマー取得に変更する。(2003/06/05)

using System;
using System.Data;
using System.Linq;
// * 履歴番号 000058 2015/10/14 追加終了
using System.Security;
using Microsoft.VisualBasic.CompilerServices;

namespace Densan.Reams.AB.AB000BB
{

    public class ABJukiKoshinBClass
    {

        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strGyosekuInit;
        private string m_strChiku1Init;
        private string m_strChiku2Init;
        private string m_strChiku3Init;
        private string m_strZokugara1Init;
        private string m_strZokugara2Init;
        private ABJutogaiBClass m_cJutogaiB;                  // 住登外ＤＡクラス
        private ABAtenaBClass m_cAtenaB;                      // 宛名マスタＤＡクラス
        private ABAtenaRirekiBClass m_cAtenaRirekiB;          // 宛名履歴ＤＡクラス
        private ABAtenaRuisekiBClass m_cAtenaRuisekiB;        // 宛名累積ＤＡクラス
        private DataSet m_csAtenaEntity;                      // 宛名Entity
        private DataSet m_csAtenaRuisekiEntity;               // 宛名累積Entity
                                                              // *履歴番号 000003 2003/11/21 追加開始
        private ABAtenaNenkinBClass m_cAtenaNenkinB;          // 宛名年金ＤＡクラス
        private ABAtenaKokuhoBClass m_cAtenaKokuhoB;          // 宛名国保ＤＡクラス
                                                              // *履歴番号 000003 2003/11/21 追加終了
                                                              // *履歴番号 000004 2004/02/13 追加開始   000009 2005/02/28 削除開始
                                                              // '''''Dim m_ABToshoProperty() As ABToshoProperty
                                                              // '''''Dim m_intCnt As Integer
                                                              // *履歴番号 000004 2004/02/13 追加開始   000009 2005/02/28 削除終了
                                                              // *履歴番号 000009 2005/03/18 追加開始
        private DataSet m_csAtenaKanriEntity;                      // 宛名管理情報データセット
        private string m_strR3RenkeiFG;                            // R3レプリカ連携フラグ
        private string m_strKoteiRenkeiFG;                         // 固定連携フラグ
        private string m_strGapeiDate = string.Empty;              // 合併日
                                                                   // *履歴番号 000027 2005/12/20 追加開始
        private string m_strBefGapeiDate = string.Empty;           // 合併日一日前
        private string m_strSystemDate = string.Empty;             // システム日付
                                                                   // *履歴番号 000027 2005/12/20 追加終了
                                                                   // *履歴番号 000016 2005/11/01 削除開始
                                                                   // * corresponds to VS2008 Start 2010/04/16 000043
                                                                   // '''Private m_blnGappei As Boolean = False                         '合併判定フラグ
                                                                   // * corresponds to VS2008 End 2010/04/16 000043
                                                                   // *履歴番号 000016 2005/11/01 削除終了
        private BAAtenaLinkageBClass m_cBAAtenaLinkageBClass;      // 固定資産税宛名クラス
        private BAAtenaLinkageIFXClass m_cBAAtenaLinkageIFXClass;
        // *履歴番号 000009 2005/03/18 追加終了
        // *履歴番号 000016 2005/11/01 追加開始
        private DataSet m_csReRirekiEntity;                        // 全履歴データ退避用
        private bool m_blnJutogaiAriFG = false;                 // 履歴の中に住登外があるかどうかのフラグ
        private DataRow[] m_csJutogaiRows;                         // 元のＤＢの住登外のＲＯＷＳ
        private DataRow m_csFirstJutogaiRow;                       // 元のＤＢの最初の住登外ＲＯＷ
        private int m_intRenbanCnt = 0;                        // 履歴編集で用いる連番用のカウント
        private int m_intJutogaiRowCnt = 0;                    // 元のＤＢに含まれる住登外ＲＯＷの件数
        private int m_intJutogaiInCnt = 0;                     // 住登外を追加した件数
        private int m_intJutogaiST_YMD;                        // 住登外ＲＯＷの開始年月日を代入する
        private bool m_blnHenkanFG = false;                     // 住登外を起こしたかどうかのフラグ
                                                                // *履歴番号 000018 2005/11/27 削除開始
                                                                // Private m_blnSaiTenyuFG As Boolean = False                   ' 再転入したかどうかのフラグ
                                                                // *履歴番号 000018 2005/11/27 削除終了
                                                                // *履歴番号 000016 2005/11/01 追加終了
                                                                // *履歴番号 000021 2005/12/12 追加開始
        private string m_strTenshutsuGyoseikuCD;                   // 転出者の行政区ＣＤ
                                                                   // *履歴番号 000022 2005/12/15 削除開始
                                                                   // Private m_strTenshutsuGyoseikuMei As String                  ' 転出者の行政区名称
                                                                   // Private m_cuGyoseikuCDCashB As URGYOSEIKUCDMSTCacheBClass    ' 行政区コードマスタキャッシュＢ
                                                                   // *履歴番号 000022 2005/12/15 削除終了
                                                                   // *履歴番号 000021 2005/12/12 追加終了
                                                                   // *履歴番号 000038 2009/04/07 削除開始
                                                                   // '*履歴番号 000031 2007/01/30 追加開始
                                                                   // Private m_crBanchiCdMstB As URBANCHICDMSTBClass              ' UR番地コードマスタクラス
                                                                   // '*履歴番号 000031 2007/01/30 追加終了
                                                                   // *履歴番号 000038 2009/04/07 削除終了
                                                                   // *履歴番号 000034 2007/08/31 追加開始
        private URKANRIJOHOCacheBClass cuKanriJohoB;               // 管理情報Ｂクラス(キャッシュ対応版)
                                                                   // *履歴番号 000034 2007/08/31 追加終了
                                                                   // *履歴番号 000038 2009/04/07 追加開始
        private ABBanchiCDHenshuBClass m_cBanchiCDHenshuB;         // 番地コード編集Ｂクラス
                                                                   // *履歴番号 000038 2009/04/07 追加終了
                                                                   // *履歴番号 000039 2009/05/12 追加開始
        protected bool m_blnBatch = false;                      // バッチ区分(True:バッチ系,False:リアル系)
        private URKANRIJOHOBClass m_cuKanriJohoB_Batch;            // 管理情報Ｂクラス ※バッチ用
        private FrnHommyoKensakuType m_cFrnHommyoKensakuType;
        // *履歴番号 000039 2009/05/12 追加終了
        // *履歴番号 000041 2009/06/18 追加開始
        private bool m_blnRirekiShusei = false;                 // 履歴修正データ削除判定フラグ
                                                                // *履歴番号 000041 2009/06/18 追加終了
                                                                // * 履歴番号 000044 2011/11/09 追加開始
        private ABAtenaRirekiFZYBClass m_cAtenaRirekiFzyB;         // 宛名履歴付随Bクラス
        private ABAtenaFZYBClass m_cAtenaFzyB;                     // 宛名付随Bクラス
        private DataSet m_csReRirekiFzyEntity;                     // 宛名履歴付随テーブルスキーマ
        private DataSet m_csAtenaRuisekiFzyEntity;                 // 宛名累積付随テーブルスキーマ
                                                                   // * 履歴番号 000044 2011/11/09 追加終了
                                                                   // * 履歴番号 000050 2014/06/25 追加開始
        private ABMyNumberBClass m_cABMyNumberB;                  // 共通番号ビジネスクラス
        private ABMyNumberRuisekiBClass m_cABMyNumberRuisekiB;    // 共通番号累積ビジネスクラス
                                                                  // * 履歴番号 000050 2014/06/25 追加終了
        private ABAtena_HyojunBClass m_cABAtenaHyojunB;                      // 宛名標準B
        private ABAtenaFZY_HyojunBClass m_cABAtenaFZYHyojunB;                // 宛名付随標準B
        private ABAtenaRireki_HyojunBClass m_cABAtenaRirekiHyojunB;          // 宛名履歴標準B
        private ABAtenaRirekiFZY_HyojunBClass m_cABAtenaRirekiFZYHyojunB;    // 宛名履歴付随標準B 
        private ABAtenaRuiseki_HyojunBClass m_cABAtenaRuisekiHyojunB;        // 宛名累積標準B
        private ABAtenaRuisekiFZY_HyojunBClass m_cABatenaRuisekiFZYHyojunB;  // 宛名累積付随標準B
        private USRuijiClass m_cuUsRuiji;                                    // 類字変換
        private DataSet m_csAtenaRuisekiHyojunEntity;                        // 宛名累積_標準Entity
        private DataSet m_csAtenaRuisekiFZYHyojunEntity;                     // 宛名累積付随_標準Entity
        private ABBanchiEdabanSuchiBClass m_cABBanchiEdabanSuchiB;           // 番地コード編集Ｂクラス
        private ABMyNumberHyojunBClass m_csABMyNumberHyojunB;                // 共通番号標準
        private ABMyNumberRuisekiHyojunBClass m_csAbMyNumberRuisekiHyojunB;  // 共通番号累積標準
        private DataSet m_csReRirekiHyojunEntity;
        private DataSet m_csRERirekiFZYHyojunEntity;
        // *履歴番号 000065 2024/04/02 追加開始
        private ABKojinSeigyoBClass m_cABKojinSeigyoB;                       // 宛名個人情報制御Ｂ
        private ABKojinseigyoRirekiBClass m_cABKojinseigyoRirekiB;           // 宛名個人情報制御履歴Ｂ
        private string m_strSeinenKoKenShokiMsg;                             // 成年後見人メッセージ
                                                                             // *履歴番号 000065 2024/04/02 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABJukiKoshinBClass";              // クラス名
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
                                                                                  // *履歴番号 000004 2004/02/14 追加開始   000009 2005/02/28 削除開始
                                                                                  // '''''Private Const WORK_FLOW_NAME As String = "宛名異動"             ' ワークフロー名
                                                                                  // '''''Private Const DATA_NAME As String = "宛名"                      'データ名
                                                                                  // *履歴番号 000004 2004/02/14 追加終了   000009 2005/02/28 削除終了
        private const string FUSHOPTN_FUSHO = "1";
        private const string FUSHOPTN_NASHI = "0";
        // *履歴番号 000065 2024/04/02 追加開始
        private const string ERR_MSG_KOJINSEIGYO = "個人制御情報";           // エラーメッセージ_個人制御情報
        private const string ERR_MSG_KOJINSEIGYORIREKI = "個人制御履歴情報"; // エラーメッセージ_個人制御履歴情報
                                                                     // *履歴番号 000065 2024/04/02 追加終了
        private const string CNS_KURAN = "空欄";

        // * 履歴番号 000050 2014/06/25 追加開始
        private enum ABMyNumberType
        {
            New = 0,                   // 共通番号
            Old                         // 旧共通番号
        }
        // * 履歴番号 000050 2014/06/25 追加終了

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass
        // * 　　                          ByVal csUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABJukiKoshinBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数初期化
            m_strGyosekuInit = string.Empty;
            m_strChiku1Init = string.Empty;
            m_strChiku2Init = string.Empty;
            m_strChiku3Init = string.Empty;
            m_strZokugara1Init = string.Empty;
            m_strZokugara2Init = string.Empty;
            // *履歴番号 000021 2005/12/12 追加開始
            m_strTenshutsuGyoseikuCD = string.Empty;
            // *履歴番号 000022 2005/12/15 削除開始
            // m_strTenshutsuGyoseikuMei = String.Empty
            // *履歴番号 000022 2005/12/15 削除終了
            // *履歴番号 000021 2005/12/12 追加終了

        }

        #region データセット作成
        // ************************************************************************************************
        // * メソッド名     データセット作成
        // * 
        // * 構文           Public Function DataSetSakusei() As DataSet
        // * 
        // * 機能 　    　　住基データセットを作成する
        // * 
        // * 引数           無し
        // * 
        // * 戻り値         DataSet(ABJukiDataEntity) : 住基データセット
        // ************************************************************************************************
        public DataSet DataSetSakusei()
        {
            const string THIS_METHOD_NAME = "DataSetSakusei";
            DataSet csJukiDataEntity;                     // データセット
            DataTable csJukiDataTable;                    // テーブル
            DataColumn csJukiDataColumn;                  // カラム
            var csJukiPrimaryKey = new DataColumn[2];               // 主キー

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 住基データEntityのインスタンス作成
                csJukiDataEntity = new DataSet();

                // 住基データテーブルの作成
                csJukiDataTable = csJukiDataEntity.Tables.Add(ABJukiData.TABLE_NAME);

                // カラム定義の作成
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn.AllowDBNull = false;
                csJukiPrimaryKey[0] = csJukiDataColumn;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHICHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUSHICHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOSHICHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RIREKINO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn.AllowDBNull = false;
                csJukiPrimaryKey[1] = csJukiDataColumn;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RRKST_YMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RRKED_YMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIRINO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 12;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINSHU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAMEISHO1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 80
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIMEISHO1, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120        '40
                csJukiDataColumn.MaxLength = 480;        // 40
                                                         // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAMEISHO2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 80
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIMEISHO2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 40
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUSEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 60;         // 15
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANASEIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 60
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIMEISHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 40
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANASEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 72;         // 24
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;         // 16
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIBETSUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIBETSU, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 1
                csJukiDataColumn.MaxLength = 10;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIKINO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOHYOJIJUN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 40;         // 15
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJIJUN2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARACD2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARA2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 40;         // 15
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSJUMINCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJISTAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANASTAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 40
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSJUMINCD2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJISTAINUSMEI2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANASTAINUSMEI2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 40
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIYUBINNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIJUSHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIJUSHO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '30
                csJukiDataColumn.MaxLength = 200;         // 30
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 5;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 5;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD3, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 5;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '20
                csJukiDataColumn.MaxLength = 200;         // 20
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKIFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 20;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1200;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RENRAKUSAKI1, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 14
                csJukiDataColumn.MaxLength = 15;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RENRAKUSAKI2, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 14
                csJukiDataColumn.MaxLength = 15;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KJNRENRAKUSAKI1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 14;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KJNRENRAKUSAKI2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 14;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_ZJUSHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 13;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_JUSHO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '30
                csJukiDataColumn.MaxLength = 200;         // 30
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_BANCHI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '20
                csJukiDataColumn.MaxLength = 200;         // 20
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HITTOSHA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINJIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINTDKDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINTDKDTUCIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUIDOWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUJIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDTUCIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIIDOWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIJIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDTUCIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOJIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOTDKDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOTDKDTUCIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTITUCIYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUNYURIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUNYURIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 30;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_YUBINNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_ZJUSHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 13;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_JUSHO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '30
                csJukiDataColumn.MaxLength = 200;         // 30
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_BANCHI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '20
                csJukiDataColumn.MaxLength = 200;         // 20
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KATAGAKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1200;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MITDKFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIYUBINNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIZJUSHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 13;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIJUSHO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '30
                csJukiDataColumn.MaxLength = 200;         // 30
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIBANCHI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '20
                csJukiDataColumn.MaxLength = 200;         // 20
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKATAGAKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1200;         // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIYUBINNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIZJUSHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 13;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIJUSHO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '30
                csJukiDataColumn.MaxLength = 200;         // 30
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIBANCHI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50         '20
                csJukiDataColumn.MaxLength = 200;         // 20
                                                          // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIKATAGAKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1200;         // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;        // 30
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMITDKFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKO, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 50
                csJukiDataColumn.MaxLength = 200;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UTSUSHIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HANNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 5;       // 2
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIATOFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIMAEFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIGYOSEIKUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 9;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIGYOSEIKUMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD3, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI3, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOHYOKUCD, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 4
                csJukiDataColumn.MaxLength = 5;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOGAKKOKUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 4;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CHUGAKKOKUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 4;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HOGOSHAJUMINCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIHOGOSHAMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120        '30
                csJukiDataColumn.MaxLength = 480;        // 30
                                                         // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHOGOSHAMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;        // 40
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KIKAYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KARIIDOKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORITEISHIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORIYOKUSHIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOKUSEKICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 3;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOKUSEKI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 20
                csJukiDataColumn.MaxLength = 100;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSKAKCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 3;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSKAK, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 15;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKAN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 4;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYU_ST_YMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYU_ED_YMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HANYOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                // *履歴番号 000016 2005/11/01 追加開始
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORIJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                // *履歴番号 000016 2005/11/01 追加終了
                // *履歴番号 000036 2007/09/28 追加開始
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIRIYOKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                // *履歴番号 000036 2007/09/28 追加終了
                // * 履歴番号 000044 2011/11/09 追加開始
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TABLEINSERTKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                // * 履歴番号 000045 2011/12/05 追加開始
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.LINKNO, Type.GetType("System.Decimal"));
                // csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.LINKNO, System.Type.GetType("System.String"))
                // csJukiDataColumn.MaxLength = 6
                // * 履歴番号 000045 2011/12/05 追加終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOJOTAIKBN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKYOCHITODOKEFLG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HONGOKUMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHONGOKUMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIHEIKIMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHEIKIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJITSUSHOMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANATSUSHOMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KATAKANAHEIKIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREFUSHOKBN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TSUSHOMEITOUROKUYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKANCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKANMEISHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSHACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSHAMEISHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 20;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUCARDNO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 12;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYOTEISTYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYOTEIEDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOIDOYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOJIYU, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOTDKDYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOTDKDTUCIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNSTAINUSMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNSTAINUSKANAMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSHEIKIMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSKANAHEIKIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSTSUSHOMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSKANATSUSHOMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSMEI_KYOTSU, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSHEIKIMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSTSUSHOMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSMEI_KYOTSU, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSHEIKIMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSTSUSHOMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSMEI_KYOTSU, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSHEIKIMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSTSUSHOMEI, Type.GetType("System.String"));
                // *履歴番号 000061 2023/08/14 修正開始
                // csJukiDataColumn.MaxLength = 120
                csJukiDataColumn.MaxLength = 480;
                // *履歴番号 000061 2023/08/14 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE2, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE3, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE4, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE5, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE1, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE2, Type.GetType("System.String"));
                // * 履歴番号 000059 2018/01/04 修正開始
                // csJukiDataColumn.MaxLength = 50
                csJukiDataColumn.MaxLength = 80;
                // * 履歴番号 000059 2018/01/04 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE3, Type.GetType("System.String"));
                // * 履歴番号 000059 2018/01/04 修正開始
                // csJukiDataColumn.MaxLength = 50
                csJukiDataColumn.MaxLength = 20;
                // * 履歴番号 000059 2018/01/04 修正終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE4, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE5, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 50;
                // * 履歴番号 000044 2011/11/09 追加終了
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.EDANO, Type.GetType("System.Decimal"));
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIKANAKAKUNINFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOUMAREBI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 72;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JIJITSUSTAINUSMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIKUCHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.MACHIAZACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TODOFUKEN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 16;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIKUGUNCHOSON, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.MACHIAZA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHJUSHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 200;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKATAGAKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1200;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BANCHIEDABANSUCHI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 20;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_SHIKUCHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_MACHIAZACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_TODOFUKEN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 16;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_SHIKUGUNCHOSON, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_MACHIAZA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINIDOWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOCKINIDOBI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 72;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNKISAIJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KISAIYMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 8;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNSHOJOJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOIDOWMD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOSHOJOIDOBI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 72;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_SHIKUCHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MACHIAZACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_TODOFUKEN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 16;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_SHIKUCHOSON, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MACHIAZA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUSEKICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 3;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUSEKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 200;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUGAIJUSHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 300;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIMACHIAZACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEITODOFUKEN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 16;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIMACHIAZA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUSEKICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 3;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUSEKI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 200;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUGAIJUSHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 300;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 6;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMACHIAZACD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 7;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTITODOFUKEN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 16;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISHIKUCHOSON, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 48;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMACHIAZA, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOKUBETSUYOSHIKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.IDOKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.NYURYOKUBASHOCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 4;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.NYURYOKUBASHO, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 30;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIKYUUJI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 80;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAKYUUJI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 20;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUUJIKANAKAKUNINFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TDKDSHIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNIDOJIYUCD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 2;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOUMAREBIDATE, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOCKINIDOBIDATE, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOSHOJOIDOBIDATE, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 10;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHFRNMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAFRNMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHTSUSHOMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANATSUSHOMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TSUSHOKANAKAKUNINFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIYUSENKB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIHEIKIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 480;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAHEIKIMEI, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 120;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUCARDNOKBN, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKYOCHIHOSEICD, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HODAI30JO46MATAHA47KB, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;
                csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOGOATENAFG, Type.GetType("System.String"));
                csJukiDataColumn.MaxLength = 1;


                csJukiDataTable.PrimaryKey = csJukiPrimaryKey;   // 主キー

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

            return csJukiDataEntity;

        }

        #endregion

        // ************************************************************************************************
        // * メソッド名     住基データ更新
        // * 
        // * 構文           Public Sub JukiDataKoshin(ByVal csJukiDataEntity As DataSet)
        // * 
        // * 機能 　    　　住基データの更新処理を行なう
        // * 
        // * 引数           DataSet(csJukiDataEntity) : 住基データセット
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        [SecuritySafeCritical]
        public void JukiDataKoshin(DataSet csJukiDataEntity)
        {
            const string THIS_METHOD_NAME = "JukiDataKoshin";
            // *履歴番号 000009 2005/03/18 削除開始
            // '''''Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
            // *履歴番号 000009 2005/03/18 削除終了
            // Dim csAtenaRirekiEntity As DataSet                  '宛名履歴データセット
            ABAtenaKanriJohoBClass cAtenaKanriJohoB;      // 宛名管理情報ＤＡビジネスクラス
                                                          // 宛名管理情報データRow
                                                          // 住基データRow
                                                          // *履歴番号 000004 2004/02/13 追加開始
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim csDataRow As DataRow                            ' ＤａｔａＲｏｗ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // ''''Dim cABAtenaCnvBClass As ABAtenaCnvBClass
                                                          // Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // Dim cSearchKey As ABAtenaSearchKey                  ' 宛名検索キー
                                                          // '''''Dim blnGappei As Boolean = False
                                                          // *履歴番号 000016 2005/11/01 削除開始
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // '''Dim strsvjumincd As String
                                                          // '''Dim strSystemDate As String                         'システム日付
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // *履歴番号 000016 2005/11/01 削除開始
                                                          // *履歴番号 000004 2004/02/13 追加終了
                                                          // *履歴番号 000016 2005/11/18 追加開始
                                                          // Dim intDelCnt As Integer
                                                          // Dim intAllCnt As Integer
                                                          // *履歴番号 000016 2005/11/18 追加終了
                                                          // *履歴番号 000017 2005/11/22 追加開始
            string[] strBreakJuminCD = new string[] { string.Empty, string.Empty };
            // *履歴番号 000017 2005/11/22 追加終了
            // *履歴番号 000019 2005/12/02 追加開始
            DataRow[] csJukiDataRows;
            // *履歴番号 000019 2005/12/02 追加終了
            // *履歴番号 000021 2005/12/12 追加開始 000022 2005/12/15 削除開始
            // Dim csGyoseikuCDMstEntity As DataSet
            // *履歴番号 000021 2005/12/12 追加終了 000022 2005/12/15 削除終了
            // *履歴番号 000027 2005/12/20 追加開始
            // Dim csJukiCkinDataRows() As DataRow                 ' 住基データの直近ロウ
            // Dim strJukiCkinST_YMD As String                     ' 住基データの直近ロウの開始年月日
            // *履歴番号 000027 2005/12/20 追加終了
            // * 履歴番号 000044 2011/11/09 追加開始
            // Dim csAtenaRirekiFzyEntity As DataSet               ' 宛名履歴付随
            // * 履歴番号 000044 2011/11/09 追加終了
            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // ---------------------------------------------------------------------------------------
                // 1. 管理情報の取得
                // ---------------------------------------------------------------------------------------

                // *履歴番号 000009 2005/03/18 修正開始
                // 管理情報ﾃﾞｰﾀｾｯﾄが無い場合は取得する
                if (m_csAtenaKanriEntity is null)
                {

                    // 日付クラスのインスタンス化
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // *履歴番号 000027 2005/12/20 追加開始
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                    m_cfDateClass.p_enEraType = UFEraType.Number;
                    // *履歴番号 000027 2005/12/20 追加終了
                    // 住登外ＤＡクラスのインスタンス作成
                    m_cJutogaiB = new ABJutogaiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名マスタＤＡクラスのインスタンス作成
                    m_cAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名履歴ＤＡクラスのインスタンス作成
                    m_cAtenaRirekiB = new ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名累積ＤＡクラスのインスタンス作成
                    m_cAtenaRuisekiB = new ABAtenaRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // * 履歴番号 000044 2011/11/09 追加開始
                    // 宛名履歴付随ＤＡクラスのインスタンス作成
                    m_cAtenaRirekiFzyB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名付随ＤＡクラスのインスタンス作成
                    m_cAtenaFzyB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // * 履歴番号 000044 2011/11/09 追加終了
                    // * 履歴番号 000050 2014/06/25 追加開始
                    // 共通番号ビジネスクラスのインスタンス化
                    m_cABMyNumberB = new ABMyNumberBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 共通番号累積ビジネスクラスのインスタンス化
                    m_cABMyNumberRuisekiB = new ABMyNumberRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // * 履歴番号 000050 2014/06/25 追加終了

                    // *履歴番号 000003 2003/11/21 追加開始
                    // 宛名年金ＤＡクラスのインスタンス作成
                    if (m_cAtenaNenkinB is null)
                    {
                        m_cAtenaNenkinB = new ABAtenaNenkinBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }

                    // 宛名国保ＤＡクラスのインスタンス作成
                    if (m_cAtenaKokuhoB is null)
                    {
                        m_cAtenaKokuhoB = new ABAtenaKokuhoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    // *履歴番号 000003 2003/11/21 追加終了

                    // *履歴番号 000039 2009/05/12 修正開始
                    if (m_blnBatch == true)
                    {
                        // ＵＲ管理情報Ｂクラスをインスタンス化
                        if (m_cuKanriJohoB_Batch is null)
                        {
                            m_cuKanriJohoB_Batch = new URKANRIJOHOBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                        }
                        // 外国人本名検索パラメータ
                        m_cFrnHommyoKensakuType = m_cuKanriJohoB_Batch.GetFrn_HommyoKensaku_Param;
                    }
                    else
                    {
                        // ＵＲ管理情報Ｂキャッシュクラスをインスタンス化
                        if (cuKanriJohoB is null)
                        {
                            cuKanriJohoB = new URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                        }
                        // 外国人本名検索パラメータ
                        m_cFrnHommyoKensakuType = cuKanriJohoB.GetFrn_HommyoKensaku_Param;
                    }

                    // '*履歴番号 000034 2007/08/31 追加開始
                    // ' ＵＲ管理情報Ｂクラスのインスタンス化
                    // If (cuKanriJohoB Is Nothing) Then
                    // cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    // End If
                    // '*履歴番号 000034 2007/08/31 追加終了
                    // *履歴番号 000039 2009/05/12 修正終了

                    // **
                    // * 管理情報の取得
                    // *
                    // 宛名管理情報ＤＡビジネスクラスのインスタンス作成
                    cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                    // 宛名管理情報抽出（全件）メソッド実行
                    m_csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu();

                    // 種別キー
                    // 識別キー
                    // 識別キー
                    // 識別キー
                    // *履歴番号 000027 2005/12/20 追加開始
                    // 合併日の一日前を取得
                    // システム日付を取得する
                    // *履歴番号 000027 2005/12/20 追加終了
                    // *履歴番号 000021 2005/12/12 追加開始
                    // *履歴番号 000022 2005/12/15 削除開始
                    // If m_strTenshutsuGyoseikuCD.Trim <> String.Empty Then
                    // ' 行政区コードマスタキャッシュＢクラスのインスタンス作成
                    // m_cuGyoseikuCDCashB = New URGYOSEIKUCDMSTCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    // ' キャッシュの内容が最新かチェック
                    // m_cuGyoseikuCDCashB.NewestCacheCheck()
                    // ' 転出者用の行政区ＣＤで行政区名称を取得する
                    // csGyoseikuCDMstEntity = m_cuGyoseikuCDCashB.GetGYOSEIKUCDMST(m_strTenshutsuGyoseikuCD.PadLeft(9, " "c))
                    // m_strTenshutsuGyoseikuMei = CType(csGyoseikuCDMstEntity.Tables(URGYOSEIKUCDMSTData.TABLE_NAME).Rows(0)(URGYOSEIKUCDMSTData.GYOSEIKUMEI), String)
                    // End If
                    // *履歴番号 000022 2005/12/15 削除開始
                    // *履歴番号 000021 2005/12/12 追加終了

                    // *履歴番号 000065 2024/04/02 追加開始
                    // *履歴番号 000065 2024/04/02 追加終了
                    foreach (DataRow csAtenaKanriRow in m_csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows)   // 異動処理
                                                                                                                               // 行政区初期化
                                                                                                                               // 地区１
                                                                                                                               // 地区２
                                                                                                                               // 地区３
                                                                                                                               // 続柄１初期化
                                                                                                                               // 続柄２初期化
                                                                                                                               // データ連動制御
                                                                                                                               // 宛名レプリカ連携ワークフロー
                                                                                                                               // 固定連動
                                                                                                                               // 合併関連
                                                                                                                               // 合併日
                                                                                                                               // 独自処理
                                                                                                                               // 転出者行政区ＣＤ
                                                                                                                               // 個人情報制御機能
                                                                                                                               // 成年後見人初期メッセージ
                        ;

                    // ☆☆宛名累積のスキーマーを取得する。(GetTableSchemaがトランザクション中取得できない)
#error Cannot convert SelectBlockSyntax - see comment for details
                    /* Cannot convert SelectBlockSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
                                               at ICSharpCode.CodeConverter.CSharp.MethodBodyExecutableStatementVisitor.<VisitSelectBlock>d__66.MoveNext()
                                            --- End of stack trace from previous location where exception was thrown ---
                                               at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                               at ICSharpCode.CodeConverter.CSharp.PerScopeStateVisitorDecorator.<AddLocalVariablesAsync>d__6.MoveNext()
                                            --- End of stack trace from previous location where exception was thrown ---
                                               at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                               at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.<DefaultVisitInnerAsync>d__3.MoveNext()

                                            Input:
                                                                '種別キー
                                                                Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHUKEY).ToString
                                                                    Case "01"   '異動処理
                                                                        '識別キー
                                                                        Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                                                            Case "06"   '行政区初期化
                                                                                Me.m_strGyosekuInit = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "07"   '地区１
                                                                                Me.m_strChiku1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "08"   '地区２
                                                                                Me.m_strChiku2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "09"   '地区３
                                                                                Me.m_strChiku3Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "10"   '続柄１初期化
                                                                                Me.m_strZokugara1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "11"   '続柄２初期化
                                                                                Me.m_strZokugara2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                        End Select
                                                                    Case "04"   'データ連動制御
                                                                        '識別キー
                                                                        Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                                                            Case "01"   '宛名レプリカ連携ワークフロー
                                                                                Me.m_strR3RenkeiFG = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                            Case "12"   '固定連動
                                                                                Me.m_strKoteiRenkeiFG = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                        End Select
                                                                    Case "05"   '合併関連
                                                                        '識別キー
                                                                        Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                                                            Case "01"   '合併日
                                                                                Me.m_strGapeiDate = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                                                                '*履歴番号 000027 2005/12/20 追加開始
                                                                                ' 合併日の一日前を取得
                                                                                If Me.m_strGapeiDate <> String.Empty Then
                                                                                    Me.m_cfDateClass.p_strDateValue = Me.m_strGapeiDate
                                                                                    Me.m_strBefGapeiDate = Me.m_cfDateClass.AddDay(-1)
                                                                                End If
                                                                                ' システム日付を取得する
                                                                                Me.m_strSystemDate = Me.m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")
                                                                                '*履歴番号 000027 2005/12/20 追加終了
                                                                        End Select
                                                                        '*履歴番号 000021 2005/12/12 追加開始
                                                                    Case "10"   '独自処理
                                                                        Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                                                            Case "03"   '転出者行政区ＣＤ
                                                                                Me.m_strTenshutsuGyoseikuCD = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String)
                                                                                '*履歴番号 000022 2005/12/15 削除開始
                                                                                'If m_strTenshutsuGyoseikuCD.Trim <> String.Empty Then
                                                                                '    ' 行政区コードマスタキャッシュＢクラスのインスタンス作成
                                                                                '    m_cuGyoseikuCDCashB = New URGYOSEIKUCDMSTCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                                                                                '    ' キャッシュの内容が最新かチェック
                                                                                '    m_cuGyoseikuCDCashB.NewestCacheCheck()
                                                                                '    ' 転出者用の行政区ＣＤで行政区名称を取得する
                                                                                '    csGyoseikuCDMstEntity = m_cuGyoseikuCDCashB.GetGYOSEIKUCDMST(m_strTenshutsuGyoseikuCD.PadLeft(9, " "c))
                                                                                '    m_strTenshutsuGyoseikuMei = CType(csGyoseikuCDMstEntity.Tables(URGYOSEIKUCDMSTData.TABLE_NAME).Rows(0)(URGYOSEIKUCDMSTData.GYOSEIKUMEI), String)
                                                                                'End If
                                                                                '*履歴番号 000022 2005/12/15 削除開始
                                                                        End Select
                                                                        '*履歴番号 000021 2005/12/12 追加終了

                                                                    '*履歴番号 000065 2024/04/02 追加開始
                                                                    Case "20"   '個人情報制御機能
                                                                        Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                                                            Case "08"   '成年後見人初期メッセージ
                                                                                Me.m_strSeinenKoKenShokiMsg = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim

                                                                        End Select
                                                                    '*履歴番号 000065 2024/04/02 追加終了
                                                                End Select

                                             */
                    m_csAtenaRuisekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME);

                    // * 履歴番号 000044 2011/11/09 追加開始
                    // 宛名累積付随テーブルのスキーマを保持
                    m_csAtenaRuisekiFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiFZYEntity.TABLE_NAME);
                    // * 履歴番号 000044 2011/11/09 追加終了

                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // *履歴番号 000016 2005/11/01 削除開始
                    // 現在日時を取得する
                    // '''strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")

                    // '''合併年月日が管理情報に存在し、合併年月日があり、かつ合併年月日以前の更新の場合は合併年月日を格納する
                    // '''If Not m_strGapeiDate Is Nothing AndAlso m_strGapeiDate > strSystemDate Then
                    // '''    m_blnGappei = True
                    // '''End If
                    // *履歴番号 000016 2005/11/01 削除終了
                    // * corresponds to VS2008 End 2010/04/16 000043

                    // 宛名標準ＤＡクラスのインスタンス作成
                    m_cABAtenaHyojunB = new ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名付随標準ＤＡクラスのインスタンス作成
                    m_cABAtenaFZYHyojunB = new ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名履歴標準ＤＡクラスのインスタンス作成
                    m_cABAtenaRirekiHyojunB = new ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名履歴付随標準ＤＡクラスのインスタンス作成
                    m_cABAtenaRirekiFZYHyojunB = new ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名累積標準ＤＡクラスのインスタンス作成
                    m_cABAtenaRuisekiHyojunB = new ABAtenaRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名累積付随標準ＤＡクラスのインスタンス作成
                    m_cABatenaRuisekiFZYHyojunB = new ABAtenaRuisekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 類字クラス
                    m_cuUsRuiji = new USRuijiClass();
                    // 番地コード編集Ｂクラス
                    m_cABBanchiEdabanSuchiB = new ABBanchiEdabanSuchiBClass(m_cfControlData, m_cfConfigDataClass);
                    // 宛名累積標準テーブルのスキーマを保持
                    m_csAtenaRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiHyojunEntity.TABLE_NAME);
                    // 宛名累積付随標準テーブルのスキーマを保持
                    m_csAtenaRuisekiFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME);
                    // 共通番号標準
                    m_csABMyNumberHyojunB = new ABMyNumberHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 共通番号累積標準
                    m_csAbMyNumberRuisekiHyojunB = new ABMyNumberRuisekiHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // *履歴番号 000065 2024/04/02 追加開始
                    // 宛名個人情報制御Ｂ
                    m_cABKojinSeigyoB = new ABKojinSeigyoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 宛名個人情報制御履歴Ｂ
                    m_cABKojinseigyoRirekiB = new ABKojinseigyoRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // *履歴番号 000065 2024/04/02 追加終了
                }
                // '''''''' 宛名管理情報ＤＡビジネスクラスのインスタンス作成
                // '''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                // '''''''' 宛名管理情報抽出（全件）メソッド実行
                // '''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu()

                // '''''''For Each csAtenaKanriRow In csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows
                // '''''''    '種別キー
                // '''''''    Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHUKEY).ToString
                // '''''''        Case "01"   '異動処理
                // '''''''            '識別キー
                // '''''''            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                // '''''''                Case "06"   '行政区初期化
                // '''''''                    m_strGyosekuInit = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''                Case "07"   '地区１
                // '''''''                    m_strChiku1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''                Case "08"   '地区２
                // '''''''                    m_strChiku2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''                Case "09"   '地区３
                // '''''''                    m_strChiku3Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''                Case "10"   '続柄１初期化
                // '''''''                    m_strZokugara1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''                Case "11"   '続柄２初期化
                // '''''''                    m_strZokugara2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                // '''''''            End Select
                // '''''''    End Select
                // '''''''Next csAtenaKanriRow

                // '''''' ☆☆宛名累積のスキーマーを取得する。(GetTableSchemaがトランザクション中取得できない)
                // '''''m_csAtenaRuisekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME)
                // ☆☆宛名累積マスタを取得する(上記の代替対策)
                // m_csAtenaRuisekiEntity = m_cAtenaRuisekiB.GetAtenaRuiseki("000000000000", "1")
                // *履歴番号 000009 2005/03/18 修正終了

                // *履歴番号 000004 2004/02/13 追加開始   000009 2005/02/28 削除開始
                // '''''''m_ABToshoPropertyのカウンタの初期値を"0"に設定
                // ''''''m_intCnt = 0
                // '''''''m_ABToshoPropertyの配列数を定義
                // ''''''ReDim m_ABToshoProperty(csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows.Count - 1)
                // *履歴番号 000004 2004/02/13 追加終了   000009 2005/02/28 削除終了

                // *履歴番号 000006 2004/08/27 修正開始
                // ''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("05", "01")
                // ''''''strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")
                // '''''''合併年月日が管理情報に存在し、合併年月日があり、かつ合併年月日以前の更新の場合は合併年月日を格納する
                // ''''''If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
                // ''''''   CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) > strSystemDate Then
                // ''''''    blnGappei = True
                // ''''''End If

                // *履歴番号 000016 2005/11/01 修正開始
                // * コメント***********************************************************************
                // * 【追いかけ期間・猶予期間・通常期間】や【異動事由】によって判断するのではなく、*
                // * 【直近のみの連携】か【履歴全件の連携】なのかだけを判断して宛名側に反映する。  *
                // *********************************************************************************
                // * corresponds to VS2008 Start 2010/04/16 000043
                // '''If m_blnGappei Then
                // '''    strsvjumincd = String.Empty
                // '''    For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
                // '''        If CType(csJukiDataRow(ABJukiData.JUMINCD), String) <> strsvjumincd And _
                // '''           (Not ((CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "03") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "04") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "45") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "63"))) And _
                // '''             (CType(csJukiDataRow(ABJukiData.RIREKINO), Integer) = 1) Then
                // '''            strsvjumincd = CType(csJukiDataRow(ABJukiData.JUMINCD), String)
                // '''            cSearchKey = New ABAtenaSearchKey()
                // '''            cSearchKey.p_strJuminCD = strsvjumincd
                // '''            cSearchKey.p_strJuminYuseniKB = "1"
                // '''            'cSearchKey.p_strStaiCD = CType(csJukiDataRow(ABJukiData.STAICD), String)
                // '''            csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)
                // '''            If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                // '''                For Each csDataRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows
                // '''                    m_cAtenaRirekiB.DeleteAtenaRB(csDataRow, "D")
                // '''                Next csDataRow
                // '''            End If
                // '''        End If
                // '''    Next csJukiDataRow
                // '''End If
                // '''' データ分繰り返す
                // '''For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
                // '''    'If Not (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") And _
                // '''    If CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

                // '''        Me.JukiDataKoshin01(csJukiDataRow)

                // '''    ElseIf m_blnGappei And _
                // '''    Not ((CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") Or _
                // '''           (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "03") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "04") Or _
                // '''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "45") Or _
                // '''           (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "63")) Then

                // '''        Me.JukiDataKoshin08(csJukiDataRow)

                // '''    End If
                // '''Next csJukiDataRow
                // * corresponds to VS2008 End 2010/04/16 000043

                // ---------------------------------------------------------------------------------------
                // 2. 住基データを住民コード、履歴番号の昇順に並び替える
                // ---------------------------------------------------------------------------------------

                // *履歴番号 000019 2005/12/02 追加開始
                csJukiDataRows = csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select("", ABJukiData.JUMINCD + " ASC , " + ABJukiData.RIREKINO + " ASC");
                // *履歴番号 000019 2005/12/02 追加終了

                // *履歴番号 000038 2009/04/07 削除開始
                // '*履歴番号 000031 2007/01/30 追加開始
                // ' UR番地コードマスタクラスのインスタンス生成
                // '*履歴番号 000035 2007/09/05 修正開始
                // If (m_crBanchiCdMstB Is Nothing) Then
                // m_crBanchiCdMstB = New URBANCHICDMSTBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // End If
                // 'm_crBanchiCdMstB = New URBANCHICDMSTBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // '*履歴番号 000035 2007/09/05 修正終了
                // '*履歴番号 000031 2007/01/30 追加終了
                // *履歴番号 000038 2009/04/07 削除終了

                // *履歴番号 000038 2009/04/07 追加開始
                // 番地コード編集クラスのインスタンス生成
                if (m_cBanchiCDHenshuB is null)
                {
                    m_cBanchiCDHenshuB = new ABBanchiCDHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                // *履歴番号 000038 2009/04/07 追加終了

                // ---------------------------------------------------------------------------------------
                // 3. 住基データが無くなるまで更新する
                // ---------------------------------------------------------------------------------------

                // *履歴番号 000017 2005/11/22 修正開始
                // *履歴番号 000019 2005/12/02 修正開始
                // * corresponds to VS2008 Start 2010/04/16 000043
                // '''For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
                // * corresponds to VS2008 End 2010/04/16 000043
                foreach (var csJukiDataRow in csJukiDataRows)
                {
                    // *履歴番号 000019 2005/12/02 修正終了

                    strBreakJuminCD[0] = strBreakJuminCD[1];
                    strBreakJuminCD[1] = Conversions.ToString(csJukiDataRow(ABJukiData.JUMINCD));

                    // 住民コードがブレイクしたら各種項目を初期化する
                    if ((strBreakJuminCD[0] ?? "") != (strBreakJuminCD[1] ?? ""))
                    {
                        m_intRenbanCnt = 0;
                        m_intJutogaiInCnt = 0;
                        m_intJutogaiRowCnt = 0;
                        m_blnHenkanFG = false;
                        // *履歴番号 000018 2005/11/27 削除開始
                        // m_blnSaiTenyuFG = False
                        // *履歴番号 000018 2005/11/27 削除終了
                        // *履歴番号 000041 2009/06/18 追加開始
                        m_blnRirekiShusei = false;
                        // *履歴番号 000041 2009/06/18 追加終了
                        // *履歴番号 000042 2009/08/10 修正開始
                        m_csReRirekiEntity = null;
                        // *履歴番号 000042 2009/08/10 修正終了
                        m_csReRirekiHyojunEntity = null;
                        m_csRERirekiFZYHyojunEntity = null;
                    }

                    // 直近のデータか履歴データなのかを判定
                    // 履歴終了年月日がオール９の場合は、直近データの場合である
                    if (Conversions.ToString(csJukiDataRow(ABJukiData.RRKED_YMD)) == "99999999")
                    {

                        // ---------------------------------------------------------------------------------------
                        // 3-1. 直近レコードを編集し更新する
                        // ---------------------------------------------------------------------------------------

                        // 住基更新メソッドを呼ぶ
                        JukiDataKoshin01(csJukiDataRow);
                    }

                    else
                    {
                        // * 履歴番号000062 2023/12/07 削除開始
                        // '---------------------------------------------------------------------------------------
                        // ' 3-2-1. ＤＢから対象データの全履歴を退避し、ＤＢを削除する
                        // '---------------------------------------------------------------------------------------

                        // ' 住民コードがブレイクしたらDB内の該当レコードを全件削除する
                        // If strBreakJuminCD(0) <> strBreakJuminCD(1) Then
                        // ' 履歴全件データの時
                        // ' 宛名検索キーのインスタンス化
                        // cSearchKey = New ABAtenaSearchKey
                        // ' 検索キーに住民コードを設定する
                        // cSearchKey.p_strJuminCD = CType(csJukiDataRow(ABJukiData.JUMINCD), String)

                        // ' 該当の履歴データを取得する(住基・住登外全件)
                        // csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                        // '* 履歴番号 000044 2011/11/09 追加開始
                        // '履歴付随を取得
                        // csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(cSearchKey.p_strJuminCD, String.Empty, String.Empty, True)
                        // '* 履歴番号 000044 2011/11/09 追加終了

                        // ' 全履歴データの件数を取得
                        // intAllCnt = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                        // ' 全履歴データを退避する
                        // m_csReRirekiEntity = csAtenaRirekiEntity

                        // '* 履歴番号 000044 2011/11/09 追加開始
                        // '履歴付随の退避をする
                        // m_csReRirekiFzyEntity = csAtenaRirekiFzyEntity
                        // '* 履歴番号 000044 2011/11/09 追加終了

                        // ' 退避した履歴Ｅｅｎｔｉｔｙから住登外のＲＯＷだけを取り出す
                        // m_csJutogaiRows = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINJUTOGAIKB ='2'", ABAtenaRirekiEntity.RIREKINO)
                        // m_intJutogaiRowCnt = m_csJutogaiRows.Length

                        // ' 住登外レコードが存在する場合はフラグを立てる。
                        // If m_intJutogaiRowCnt >= 1 Then
                        // ' 住登外ありフラグをＴｒｕｅ
                        // m_blnJutogaiAriFG = True

                        // '*履歴番号 000027 2005/12/20 追加開始
                        // ' 合併追いかけ期間中で住基データの直近が住民である場合、退避した住登外ロウを編集する。
                        // If m_strGapeiDate <> String.Empty AndAlso m_strSystemDate < m_strGapeiDate Then
                        // ' 合併追いかけ期間である
                        // ' 住基データ対象住民ＣＤの直近レコードを取得する
                        // csJukiCkinDataRows = csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select("JUMINCD = '" + CType(csJukiDataRow(ABJukiData.JUMINCD), String) + "' AND RRKED_YMD = '99999999'")
                        // ' 住基データ直近が住民の場合
                        // If CType(csJukiCkinDataRows(0)(ABJukiData.JUMINSHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" Then
                        // ' 住基データ直近レコードの開始年月日を取得する
                        // strJukiCkinST_YMD = CType(csJukiCkinDataRows(0)(ABJukiData.RRKST_YMD), String)
                        // ' 住登外ロウを編集する
                        // m_csJutogaiRows = EditJutogaiRows(m_csJutogaiRows, strJukiCkinST_YMD)
                        // ' 改めて住登外ロウの件数を取得する
                        // m_intJutogaiRowCnt = m_csJutogaiRows.Length
                        // End If
                        // End If
                        // '*履歴番号 000027 2005/12/20 追加終了

                        // ' 最初の住登外ＲＯＷを取得する
                        // m_csFirstJutogaiRow = m_csJutogaiRows(0)

                        // ' 履歴開始年月日を取得する
                        // m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                        // Else
                        // m_blnJutogaiAriFG = False
                        // End If

                        // ' 該当の履歴データを全件削除する
                        // intDelCnt = m_cAtenaRirekiB.DeleteAtenaRB(CType(csJukiDataRow(ABJukiData.JUMINCD), String))

                        // '* 履歴番号 000044 2011/11/09 追加開始
                        // '* 履歴番号 000062 2023/12/07 削除開始
                        // ''履歴付随の削除
                        // 'Me.m_cAtenaRirekiFzyB.DeleteAtenaFZYRB(csJukiDataRow(ABJukiData.JUMINCD).ToString)
                        // ''* 履歴番号 000044 2011/11/09 追加終了

                        // '' 全履歴データの件数と削除した件数が一致しない場合はエラー
                        // 'If intAllCnt <> intDelCnt Then
                        // '    ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                        // '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        // '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        // '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                        // 'End If

                        // ''*履歴番号 000041 2009/06/18 追加開始
                        // 'm_blnRirekiShusei = True
                        // '* 履歴番号 000062 2023/12/07 削除終了
                        // '*履歴番号 000041 2009/06/18 追加終了

                        // End If
                        // *履歴番号000062 2023/12/07 削除終了

                        // ---------------------------------------------------------------------------------------
                        // 3-2-2. 履歴レコードを編集し更新する
                        // ---------------------------------------------------------------------------------------

                        // 履歴データを再セットする
                        JukiDataKoshin08N(csJukiDataRow);
                        // *履歴番号 000017 2005/11/22 修正終了
                    }

                }

                // *履歴番号 000016 2005/11/01 修正終了

                // *履歴番号 000004 2004/02/13 追加開始   000009 2005/02/28 削除開始
                // **
                // * ワークフロー処理
                // *
                // カウントが"0"の時はワークフロー処理を行わない
                // '''''''If Not (m_intCnt = 0) Then
                // '''''''    '  宛名管理情報の種別04識別キー01のデータを全件取得する
                // '''''''    csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

                // '''''''    '管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
                // '''''''    If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                // '''''''        If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then

                // '''''''            'm_ABToshoPropertyの配列数を再定義
                // '''''''            ReDim Preserve m_ABToshoProperty(m_intCnt - 1)
                // '''''''            'データセット取得クラスのインスタンス化
                // '''''''            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // '''''''            'ワークフロー送信処理呼び出し
                // '''''''            cABAtenaCnvBClass.AtenaCnv(m_ABToshoProperty, WORK_FLOW_NAME, DATA_NAME)

                // '''''''        End If
                // '''''''    End If
                // '''''''End If
                // *履歴番号 000004 2004/02/13 追加終了   000009 2005/02/28 削除終了


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

        }

        // ************************************************************************************************
        // * メソッド名     住基データ更新（通常）
        // * 
        // * 構文           Public Sub JukiDataKoshin1(ByVal csJukiDataRow As DataRow) 
        // * 
        // * 機能 　    　　住基データを更新する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        [SecuritySafeCritical]
        private void JukiDataKoshin01(DataRow csJukiDataRow)
        {
            const string THIS_METHOD_NAME = "JukiDataKoshin01";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            bool blnJutogaiUmu;                        // 住登外有無FLG
            bool blnJukiUmu;                           // 住基有無FLG
            string strJuminCD;                            // 住民コード
            DataSet csJutogaiEntity;                      // 住登外DataSet
            ABAtenaSearchKey cSearchKey;                  // 宛名検索キー
            DataSet csAtenaEntity;                        // 宛名マスタEntity
            DataRow csAtenaRow;                           // 宛名マスタRow
            DataRow csDataRow;                            // ＤａｔａＲｏｗ
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim csDataSet As DataSet                            ' ＤａｔａＳｅｔ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
            DataColumn csDataColumn;                      // ＤａｔａＣｏｌｕｍｎ
            DataSet csAtenaRirekiEntity;                  // 宛名履歴DataSet
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim csAtenaRirekiRows() As DataRow                  ' 宛名履歴Rows
                                                          // * corresponds to VS2008 End 2010/04/16 000043
            var csAtenaRirekiRow = default(DataRow);                     // 宛名履歴Row
            int intCount;                             // 更新件数
            DataSet csAtenaRuisekiEntity;                 // 宛名累積DataSet
            DataRow csAtenaRuisekiRow;                    // 宛名累積Row
                                                          // *履歴番号 000003 2003/11/21 追加開始
            DataSet csAtenaNenkinEntity;                  // 宛名年金DataSet
            DataSet csAtenaKokuhoEntity;                  // 宛名国保DataSet
                                                          // *履歴番号 000003 2003/11/21 追加終了
            string StrShoriNichiji;
            // '*履歴番号 000004 2004/02/13 追加開始  000009 2005/03/18 削除開始
            // ''''Dim cABToshoProperty As ABToshoProperty
            // ''''Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
            // ''''Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
            // *履歴番号 000004 2004/02/13 追加終了  000009 2005/03/18 削除開始
            // *履歴番号 000005 2004/03/08 追加開始   000009 2005/03/18 削除
            // ''''''Dim cBAAtenaLinkageBClass As BAAtenaLinkageBClass   ' 固定資産税宛名クラス
            // ''''''Dim cBAAtenaLinkageIFXClass As BAAtenaLinkageIFXClass
            bool BlnRcd;
            // *履歴番号 000005 2004/03/08 追加終了
            // *履歴番号 000013 2005/06/19 追加開始
            // * corresponds to VS2008 Start 2010/04/16 000043
            // Dim csRirekiNoEntity As DataSet         '履歴番号データセット
            // * corresponds to VS2008 End 2010/04/16 000043
            string strMaxRirekino;            // 最大履歴番号
            var blnTokushuFG = default(bool);             // 特殊処理フラグ
                                                          // *履歴番号 000016 2005/11/01 削除開始
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // '''Dim csSortRirekiDataRow() As DataRow      '履歴番号データロウ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // *履歴番号 000016 2005/11/01 削除終了
                                                          // *履歴番号 000013 2005/06/19 追加終了
                                                          // *履歴番号 000016 2005/11/01 追加開始
            DataRow[] csUpRirekiRows;           // 全履歴よりセレクトしたレコード群を格納する
            var csUpRirekiRow = default(DataRow);              // 特殊処理修正時の修正済みの更新レコード
            int intIdx;                     // For文で使用するインデックス
            int intJukiInCnt = 0;           // 住基データをインサートした件数
                                            // *履歴番号 000016 2005/11/01 追加終了
                                            // *履歴番号 000017 2005/11/22 追加開始
            int intForCnt = 0;
            // *履歴番号 000017 2005/11/22 追加終了
            // *履歴番号 000023 2005/12/16 追加開始
            DataRow[] csRirekiNORows;
            int intMaxRirekiNO;
            // *履歴番号 000023 2005/12/16 追加終了
            // *履歴番号 000031 2007/01/30 追加開始
            string[] strBanchiCD;                         // 番地コード取得用配列
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim strMotoBanchiCD() As String                     ' 変更前番地コード
                                                          // Dim intLoop As Integer                              ' ループカウンタ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // *履歴番号 000031 2007/01/30 追加終了
                                                          // *履歴番号 000032 2007/02/15 追加開始
            DataRow[] csBeforeRirekiRows;                 // 更新前履歴レコード取得用DataRows
                                                          // *履歴番号 000032 2007/02/15 追加終了
                                                          // *履歴番号 000036 2007/09/28 追加開始
            ABHenshuSearchShimeiBClass cHenshuSearchKana; // 検索用カナ生成クラス
            var strSearchKana = new string[5];                      // 検索用カナ名称用
                                                                    // *履歴番号 000036 2007/09/28 追加終了
                                                                    // * 履歴番号 000044 2011/11/09 追加開始
            DataRow[] csSelectedRows;                     // 検索結果配列
            DataRow csCkinRirekiFzyRows;                  // 直近宛名履歴付随行
            DataSet csAtenaFzyEntity;                     // 宛名付随
            DataRow csAtenaFzyRow;                        // 宛名付随行
            DataSet csAtenaRirekiFzyEntity;               // 宛名履歴付随
            var csAtenaRirekiFzyRow = default(DataRow);                  // 宛名履歴付随行
            var csAtenaRirekiFzyTokushuRow = default(DataRow);           // 宛名履歴付随特殊行
            DataSet csAtenaRuisekiFzyEntity;              // 宛名累積付随
            DataRow csAtenaRuisekiFzyRow;                 // 宛名累積付随行
            ABSekoYMDHanteiBClass cSekoYMDHanteiB;        // 施行日判定B
            bool blnAfterSekobi = false;               // 施行日以降かどうか
                                                       // * 履歴番号 000044 2011/11/09 追加終了
                                                       // * 履歴番号 000050 2014/06/25 追加開始
            string[] a_strMyNumber;                       // 共通番号・旧共通番号分割用
            ABMyNumberPrmXClass cABMyNumberPrm;           // 共通番号パラメータークラス
                                                          // * 履歴番号 000050 2014/06/25 追加終了
                                                          // * 履歴番号 000058 2015/10/14 追加開始
            URSekoYMDHanteiBClass crBangoSekoYMDHanteiB;  // 共通番号施行日判定クラス
            string strBangoSekoYMD;                       // 共通番号施行日
            bool blnIsCreateAtenaRireki;               // 宛名履歴を作成するかどうか（特殊修正の場合に特例として）
                                                       // * 履歴番号 000058 2015/10/14 追加終了
            DataSet csAtenaHyojunEntity;                  // 宛名標準
            DataRow csAtenaHyojunRow;                     // 宛名標準Row
            DataSet csAtenaFzyHyojunEntity;               // 宛名付随標準
            DataRow csAtenaFzyHyojunRow;                  // 宛名付随標準Row
            DataSet csAtenaRirekiHyojunEntity;            // 宛名履歴標準
            var csAtenaRirekiHyojunRow = default(DataRow);               // 宛名履歴標準Row
            DataSet csAtenaRirekiFZYHyojunEntity;         // 宛名履歴付随標準
            var csAtenaRirekiFZYHyojunRow = default(DataRow);            // 宛名履歴付随標準Row
            DataSet csAtenaRuisekiHyojunEntity;           // 宛名累積標準
            DataRow csAtenaRuisekiHyojunRow;              // 宛名累積標準Row
            DataSet csAtenaRuisekiFZYHyojunEntity;        // 宛名累積付随標準
            DataRow csAtenaRuisekiFZYHyojunRow;           // 宛名累積付随標準Row
            var csAtenaRirekiHyojunTokushuRow = default(DataRow);        // 宛名履歴標準特殊行
            var csAtenaRirekiFzyHyojunTokushuRow = default(DataRow);     // 宛名履歴付随標準特殊行
            DataRow csCkinRirekiHyojunRows;               // 直近宛名履歴標準行
            DataRow csCkinRirekiFzyHyojunRows;            // 直近宛名履歴付随標準行

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // *履歴番号 000016 2005/11/18 追加開始
                // 使用するときにいちいちセットしてたので最初に行う。(今まで点在していた分は削除)
                // 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                m_cfDateClass.p_enEraType = UFEraType.Number;
                // *履歴番号 000016 2005/11/18 追加終了

                // *履歴番号 000036 2007/09/28 追加開始
                // 検索用カナ生成クラスインスタンス化
                cHenshuSearchKana = new ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass);
                // *履歴番号 000036 2007/09/28 追加終了

                // * 履歴番号 000044 2011/11/09 追加開始
                // 施行日以降フラグを取得しておく
                cSekoYMDHanteiB = new ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                blnAfterSekobi = cSekoYMDHanteiB.CheckAfterSekoYMD();
                // * 履歴番号 000044 2011/11/09 追加終了

                // * 履歴番号 000058 2015/10/14 追加開始
                // 本付番処理の際に、住基と宛名で履歴数が異なっている。
                // 本付番処理にて作成された住基のみに存在する履歴に対して特殊修正が発生した場合は、
                // 宛名に該当履歴が存在しないため、宛名履歴を刻む動きとする。
                // ただし、履歴修正等で履歴数が一致した以降の特殊修正は今まで通り上書き処理となる。

                // 「03：特殊修正」「04：住民票コード修正」「05：個人番号修正」の場合

                // 宛名履歴の直近レコードを取得する

                // 宛名履歴付随の直近レコードを取得する

                // 住民に対する特殊処理かどうか判定する（本付番処理対象となったレコードに対する処理かの判定）
                // ※「10：日本人住民」「20-0：外国人住民」かどうか判定する

                // 番号制度施行日を取得する

                // 履歴開始日が番号制度施行日未満、かつ作成年月日が番号制度施行日未満の場合、処理事由コードに直近異動事由コードを設定する
                // 上記条件を満たす場合、本付番処理以降に異動が発生しておらず、履歴数不一致の状態となっているため、
                // 宛名履歴を刻み、履歴数（直近の異動状態）を一致させる
                // ※番号施行日以降に通常異動が発生した場合、履歴開始日・作成日時ともに番号施行日以降となる（直近履歴が一致している状態のため上書き処理とする）
                // ※番号施行日以降に履歴修正が発生した場合、作成日時のみが番号施行日以降となる（履歴修正にて直近履歴が一致、履歴数も一致している状態のため上書き処理とする）
                // ※番号施行日以降に特殊修正が１回でも発生した場合、履歴開始日・作成日時ともに番号施行日以降となる（特殊処理（当考慮）にて直近履歴が一致している状態のため上書き処理とする）
                // ※今後発生すると思われる移行処理で履歴を作成する場合、作成日時のみが番号施行日以降となる（移行時に履歴数を一致されてあることが前提とし上書き処理とする）
                // ※住登内修正（他社住基）の場合、当判定にて履歴が刻まれる可能性があるが問題なしとする

                // 宛名履歴を作成する（特殊処理の場合に特例として）
                // noop
                // noop
                // noop

                // 宛名履歴標準の直近レコードを取得する

                // 宛名履歴付随標準の直近レコードを取得する
                // noop
                // noop
                blnIsCreateAtenaRireki = false;


                ;
                // * 履歴番号 000058 2015/10/14 追加終了

                // ---------------------------------------------------------------------------------------
                // 1. 変数の初期化
                // 
                // ---------------------------------------------------------------------------------------
#error Cannot convert SelectBlockSyntax - see comment for details
                /* Cannot convert SelectBlockSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
                                   at ICSharpCode.CodeConverter.CSharp.MethodBodyExecutableStatementVisitor.<VisitSelectBlock>d__66.MoveNext()
                                --- End of stack trace from previous location where exception was thrown ---
                                   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                   at ICSharpCode.CodeConverter.CSharp.PerScopeStateVisitorDecorator.<AddLocalVariablesAsync>d__6.MoveNext()
                                --- End of stack trace from previous location where exception was thrown ---
                                   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.<DefaultVisitInnerAsync>d__3.MoveNext()

                                Input:
                                            Select Case csJukiDataRow.Item(ABJukiData.SHORIJIYUCD).ToString

                                                Case ABEnumDefine.ABJukiShoriJiyuType.TokushuShusei.GetHashCode.ToString("00"),
                                                     ABEnumDefine.ABJukiShoriJiyuType.TokushuCodeShusei.GetHashCode.ToString("00"),
                                                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00")

                                                    ' 「03：特殊修正」「04：住民票コード修正」「05：個人番号修正」の場合

                                                    ' 宛名履歴の直近レコードを取得する
                                                    cSearchKey = New ABAtenaSearchKey
                                                    cSearchKey.p_strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString
                                                    csAtenaRirekiEntity = Me.m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", True)

                                                    If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then

                                                        csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)

                                                        ' 宛名履歴付随の直近レコードを取得する
                                                        csAtenaRirekiFzyEntity = Me.m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                        "1",
                                                                                        True)

                                                        If (csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0) Then

                                                            csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0)

                                                            ' 住民に対する特殊処理かどうか判定する（本付番処理対象となったレコードに対する処理かの判定）
                                                            ' ※「10：日本人住民」「20-0：外国人住民」かどうか判定する
                                                            If (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.ATENADATASHU).ToString = ABConstClass.JUMINSHU_NIHONJIN_JUMIN _
                                                                OrElse (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.ATENADATASHU).ToString = ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN _
                                                                        AndAlso csAtenaRirekiFzyRow.Item(ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN).ToString = ABConstClass.JUMINHYOJOTAIKB_TAISHO)) Then

                                                                ' 番号制度施行日を取得する
                                                                crBangoSekoYMDHanteiB = New URSekoYMDHanteiBClass(Me.m_cfControlData, Me.m_cfConfigDataClass, Me.m_cfRdbClass, ABConstClass.THIS_BUSINESSID)
                                                                strBangoSekoYMD = crBangoSekoYMDHanteiB.GetBangoSeidoSekoYMD

                                                                ' 履歴開始日が番号制度施行日未満、かつ作成年月日が番号制度施行日未満の場合、処理事由コードに直近異動事由コードを設定する
                                                                ' 上記条件を満たす場合、本付番処理以降に異動が発生しておらず、履歴数不一致の状態となっているため、
                                                                ' 宛名履歴を刻み、履歴数（直近の異動状態）を一致させる
                                                                ' ※番号施行日以降に通常異動が発生した場合、履歴開始日・作成日時ともに番号施行日以降となる（直近履歴が一致している状態のため上書き処理とする）
                                                                ' ※番号施行日以降に履歴修正が発生した場合、作成日時のみが番号施行日以降となる（履歴修正にて直近履歴が一致、履歴数も一致している状態のため上書き処理とする）
                                                                ' ※番号施行日以降に特殊修正が１回でも発生した場合、履歴開始日・作成日時ともに番号施行日以降となる（特殊処理（当考慮）にて直近履歴が一致している状態のため上書き処理とする）
                                                                ' ※今後発生すると思われる移行処理で履歴を作成する場合、作成日時のみが番号施行日以降となる（移行時に履歴数を一致されてあることが前提とし上書き処理とする）
                                                                ' ※住登内修正（他社住基）の場合、当判定にて履歴が刻まれる可能性があるが問題なしとする
                                                                If (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKST_YMD).ToString < strBangoSekoYMD _
                                                                    AndAlso csAtenaRirekiRow.Item(ABAtenaRirekiEntity.SAKUSEINICHIJI).ToString.RPadRight(8).RSubstring(0, 8) < strBangoSekoYMD) Then

                                                                    ' 宛名履歴を作成する（特殊処理の場合に特例として）
                                                                    blnIsCreateAtenaRireki = True

                                                                Else
                                                                    ' noop
                                                                End If

                                                            Else
                                                                ' noop
                                                            End If

                                                        Else
                                                            ' noop
                                                        End If

                                                        ' 宛名履歴標準の直近レコードを取得する
                                                        csAtenaRirekiHyojunEntity = Me.m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                        "1",
                                                                                        True)

                                                        If (csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                                                            csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0)
                                                        End If

                                                        ' 宛名履歴付随標準の直近レコードを取得する
                                                        csAtenaRirekiFZYHyojunEntity = Me.m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                        "1",
                                                                                        True)

                                                        If (csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                                                            csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0)
                                                        End If
                                                    Else
                                                        ' noop
                                                    End If

                                                Case Else
                                                    ' noop
                                            End Select

                                 */
                blnJutogaiUmu = false;           // 住登外データが存在している場合はTrue
                blnJukiUmu = false;              // 住基データが存在している場合はTrue
                strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString;    // 対象データの住民コードを取得


                // ---------------------------------------------------------------------------------------
                // 2. 住登外データの存在チェック
                // 直近の住登外データが存在しているか住登外マスタから取得する。
                // ---------------------------------------------------------------------------------------
                // 住民コードで住登外マスタを取得する（存在する場合は、住登外有りＦＬＧに”1”をセット）
                csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, true);
                if (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0)
                {
                    blnJutogaiUmu = true;
                }


                // ---------------------------------------------------------------------------------------
                // 3. 再転入の処理
                // 直近の住登外データが存在している場合は削除する。
                // ---------------------------------------------------------------------------------------
                // 住民種別の下１桁が”0”（住民）でかつ住登外有りＦＬＧが”1”の時
                // ・住登外データを削除する
                // ・住登外優先で指定年月日”99999999”で宛名マスタを取得し、そのデータを削除する
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) == "0" & blnJutogaiUmu)
                {
                    foreach (DataRow currentCsDataRow in csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows)
                    {
                        csDataRow = currentCsDataRow;
                        m_cJutogaiB.DeleteJutogaiB(csDataRow, "D");
                    }
                    cSearchKey = new ABAtenaSearchKey();
                    cSearchKey.p_strJuminCD = strJuminCD;
                    cSearchKey.p_strJutogaiYusenKB = "1";
                    csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, true);
                    foreach (DataRow currentCsDataRow1 in csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows)
                    {
                        csDataRow = currentCsDataRow1;
                        m_cAtenaB.DeleteAtenaB(csDataRow, "D");
                        // 宛名標準
                        csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(cSearchKey.p_strJuminCD, csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                        if (csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 検索結果が存在したら０番目データでDeleteを行う（１か０しかないはず）
                            m_cABAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows(0), "D");
                        }
                        else
                        {
                            // 何もしない
                        }

                        // * 履歴番号 000044 2011/11/09 追加開始
                        // 宛名付随データ取得
                        csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(cSearchKey.p_strJuminCD, csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                        if (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 検索結果が存在したら０番目データでDeleteを行う（１か０しかないはず）
                            m_cAtenaFzyB.DeleteAtenaFZYB(csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0), "D");
                        }
                        else
                        {
                            // 何もしない
                        }
                        // * 履歴番号 000044 2011/11/09 追加終了

                        // 宛名付随標準
                        csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(cSearchKey.p_strJuminCD, csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                        if (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 検索結果が存在したら０番目データでDeleteを行う（１か０しかないはず）
                            m_cABAtenaFZYHyojunB.DeleteAtenaFZYHyojunB(csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows(0), "D");
                        }
                        else
                        {
                            // 何もしない
                        }
                    }
                }


                // ---------------------------------------------------------------------------------------
                // 4. 住基データの存在チェック
                // 直近の住基データが存在しているか宛名マスタから取得する。
                // ---------------------------------------------------------------------------------------
                // 住基優先で宛名マスタを取得する（存在する場合は、住基有りＦＬＧに”1”をセット）
                // 宛名検索キーのインスタンス化
                cSearchKey = new ABAtenaSearchKey();
                cSearchKey.p_strJuminCD = strJuminCD;
                cSearchKey.p_strJuminYuseniKB = "1";
                csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, true);
                if (csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0)
                {
                    blnJukiUmu = true;
                    // * 履歴番号 000044 2011/11/09 追加開始
                    // 宛名付随情報を住民コード指定で取得
                    csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                    // 宛名標準
                    csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(strJuminCD, csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                    // 宛名付随標準
                    csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(strJuminCD, csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                }
                else
                {
                    // そうでないとき、住民住登外区分を空で検索
                    csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, string.Empty);
                    // * 履歴番号 000044 2011/11/09 追加終了
                    // 宛名標準
                    csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(strJuminCD, string.Empty, false);
                    // 宛名付随標準
                    csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(strJuminCD, string.Empty, false);
                }


                // ---------------------------------------------------------------------------------------
                // 5. データの編集
                // 直近の住基データが存在している場合は修正、していなければ追加となる。
                // 
                // ---------------------------------------------------------------------------------------
                // 宛名マスタ
                // 宛名マスタの列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）
                if (blnJukiUmu)
                {
                    csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow;
                    ClearAtena(ref csAtenaRow);
                }

                // 住基データより宛名マスタの編集を行う（ＡＬＬ．ＮＵＬＬ又は、ＡＬＬスペースの時は、String.Emptyにして）
                foreach (DataColumn currentCsDataColumn in csJukiDataRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn;
                    if (csJukiDataRow[csDataColumn] is DBNull || string.IsNullOrEmpty(Conversions.ToString(csJukiDataRow[csDataColumn]).Trim()))
                    {
                        csJukiDataRow[csDataColumn] = string.Empty;
                    }
                }

                // 住基データの同一項目を宛名マスタの項目にセットする
                // ・住民コード
                csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD);
                // ・市町村コード
                csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD);
                // ・旧市町村コード
                csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD);

                // 何もセットしない項目
                // ・住民票コード
                // ・汎用区分２
                // ・漢字法人形態
                // ・漢字法人代表者氏名
                // ・家屋敷区分
                // ・備考税目

                // 編集してセットする項目
                // ・住民住登外区分   1
                csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1";
                // ・住民優先区分     1
                csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1";
                // ・住登外優先区分
                // 住民種別の下１桁が”0”（住民）でなく、且つ住登外有りＦＬＧが”1”の時、　0
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) != "0" & blnJutogaiUmu)
                {
                    csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "0";
                }
                else
                {
                    // 上記以外       1
                    csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1";
                }
                // ・宛名データ区分=(11)
                csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11";
                // ・世帯コード～整理番号
                csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD);
                // csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
                csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO);
                // ・宛名データ種別=(住民種別)
                csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU);
                // ・汎用区分１=(写し区分)
                csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB);
                // ・個人法人区分=(1)
                csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1";
                // ・汎用区分２
                // csAtenaRow(ABAtenaEntity.HANYOKB2) = String.Empty
                // *履歴番号 000037 2008/05/12 削除開始
                // * corresponds to VS2008 Start 2010/04/16 000043
                // ''' ・管内管外区分
                // ''' 　　住民種別の下１桁が”8”（転出者）の場合、　　2
                // * corresponds to VS2008 End 2010/04/16 000043
                // 'If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "8") Then
                // '    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                // 'Else
                // '    ' 住民種別の下１桁が”8”（転出者）でない場合、1			
                // '    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
                // 'End If
                // *履歴番号 000037 2008/05/12 削除終了

                // *履歴番号 000068 2024/07/05 追加開始
                if (new string(Conversions.ToString(csJukiDataRow(ABJukiData.HONGOKUMEI)).Trim ?? new char[0]) != "" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim ?? new char[0]) != "" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJITSUSHOMEI)).Trim ?? new char[0]) == "")
                {
                    // 本国名≠空白 かつ 併記名≠空白 かつ 通称名＝空白の場合
                    // 漢字名称２・カナ名称２に空白を設定
                    csJukiDataRow(ABJukiData.KANJIMEISHO2) = string.Empty;
                    csJukiDataRow(ABJukiData.KANAMEISHO2) = string.Empty;
                }
                else
                {
                }
                // *履歴番号 000068 2024/07/05 追加終了

                // *履歴番号 000036 2007/09/28 修正開始
                // ・カナ名称１～検索用カナ名
                if (Conversions.ToString(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim == "2" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim ?? new char[0]) != "")
                {
                    // 本名優先(本名と通称名を持つ外国人かつ氏名利用区分が"2")
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = GetSearchMoji(csJukiDataRow(ABJukiData.KANJIMEISHO2).ToString);

                    // *履歴番号 000039 2009/05/12 修正開始
                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), string.Empty, m_cFrnHommyoKensakuType);
                    // strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                    // String.Empty, cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                    // *履歴番号 000039 2009/05/12 修正終了

                    // 通称名を漢字法人代表者氏名に格納
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }

                // *履歴番号 000039 2009/05/12 修正開始
                else if (m_cFrnHommyoKensakuType == FrnHommyoKensakuType.Tsusho_Seishiki)
                {
                    // ElseIf (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                    // *履歴番号 000039 2009/05/12 修正終了

                    // 通称名優先(本名優先の条件以外の場合)
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);

                    // *履歴番号 000039 2009/05/12 修正開始
                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO1)), Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), m_cFrnHommyoKensakuType);
                    // strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)), _
                    // CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                    // cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                    // *履歴番号 000039 2009/05/12 修正終了

                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }
                else
                {
                    // 通称名優先（既存ユーザ）
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI);
                }
                // ' ・カナ名称１～検索用カナ名
                // csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                // csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                // csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                // csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                // 'csAtenaRow(ABAtenaEntity.KANJIHJNKEITAI) = String.Empty
                // 'csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                // csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                // '*履歴番号 000034 2007/08/31 修正開始
                // If (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                // '外国人本名検索機能が"2(Tsusho_Seishiki)"のとき英字は大文字にする
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANASEIMEI), String).ToUpper()
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = GetSearchKana(CType(csJukiDataRow(ABJukiData.KANAMEISHO2), String))
                // csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANAMEI), String).ToUpper()
                // Else
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                // csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
                // End If
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
                // '*履歴番号 000034 2007/08/31 修正終了
                // *履歴番号 000036 2007/09/28 修正終了
                csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI);

                // ・住基履歴番号=(履歴番号)
                csAtenaRow(ABAtenaEntity.JUKIRRKNO) = Conversions.ToString(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4);
                // ・履歴開始年月日～住民票表示順
                csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD);
                csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD);
                csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD);
                csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD);
                csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD);
                csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU);
                csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO);
                csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN);
                // ・第２住民票表示順
                csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2);
                // ・続柄コード・続柄・第2続柄コード・第2続柄
                // 住民種別の下１桁が”8”（転出者）の場合で続柄が”01”（世帯主）の場合、管理情報のコードに変更し、			
                // 名称はクリアする
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) == "8")
                {
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd == "02")
                    {
                        if (m_strZokugara1Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = string.Empty;
                            csAtenaRow(ABAtenaEntity.ZOKUGARA) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init;
                            csAtenaRow(ABAtenaEntity.ZOKUGARA) = CNS_KURAN;
                        }
                    }

                    else
                    {
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    }
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd == "02")
                    {
                        if (m_strZokugara2Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = string.Empty;
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init;
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = CNS_KURAN;
                        }
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                    }
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）でない場合は、そのままセット			
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                }
                // ・世帯主住民コード～カナ第２世帯主名
                csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD);
                csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2);
                csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2);

                // ・郵便番号～方書
                // ・転出確定住所がある場合は、転出確定欄からセット（ない項目はセットなし）
                if (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    // *履歴番号 000031 2007/01/30 修正開始
                    // 番地情報から番地コードを取得

                    // *履歴番号 000038 2009/04/07 修正開始
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)));
                    // strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)), strMotoBanchiCD, True)
                    // ' 取得した番地コード配列にNothingの項目がある場合はString.Emptyをセットする
                    // For intLoop = 0 To strBanchiCD.Length - 1
                    // If (IsNothing(strBanchiCD(intLoop))) Then
                    // strBanchiCD(intLoop) = String.Empty
                    // End If
                    // Next
                    // *履歴番号 000038 2009/04/07 修正終了

                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    // csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                    // *履歴番号 000031 2007/01/30 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管外にセット    ※コメント:転出確定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }
                // *履歴番号 000037 2008/05/12 追加終了

                else if (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    // ・転出確定住所が無く、転出予定住所がある場合は、転出予定欄からセット（ない項目はセットなし）
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    // *履歴番号 000031 2007/01/30 修正開始
                    // 番地情報から番地コードを取得
                    // *履歴番号 000038 2009/04/07 修正開始
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)));
                    // strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)), strMotoBanchiCD, True)
                    // ' 取得した番地コード配列にNothingの項目がある場合はString.Emptyをセットする
                    // For intLoop = 0 To strBanchiCD.Length - 1
                    // If (IsNothing(strBanchiCD(intLoop))) Then
                    // strBanchiCD(intLoop) = String.Empty
                    // End If
                    // Next
                    // *履歴番号 000038 2009/04/07 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    // csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                    // *履歴番号 000031 2007/01/30 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管外にセット    ※コメント:転出予定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }
                // *履歴番号 000037 2008/05/12 追加終了

                else
                {
                    // ・両方も無い場合は、住基住所欄からセット
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.JUKIJUSHOCD)).RPadLeft(13);
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管内にセット    ※コメント:転出確定住所、転出予定住所が存在しない場合は管内に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1";
                    // *履歴番号 000037 2008/05/12 追加終了

                }
                // ・連絡先１～改正年月日
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1);
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = csJukiDataRow(ABJukiData.HON_ZJUSHOCD)
                csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.HON_ZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO);
                csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI);
                csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA);
                csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD);
                csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD);
                csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU);
                csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD);
                csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD);
                csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU);
                csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD)
                csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI);
                // * 履歴番号 000063 2024/02/06 修正開始
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                // '*履歴番号 000001 2003/09/11 修正開始
                // 'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
                // '*履歴番号 000001 2003/09/11 修正終了
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

                // 住基データ.処理事由コード＝45（転入通知受理）の場合
                if (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() == ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"))
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                }
                else
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);
                }
                // * 履歴番号 000063 2024/02/06 修正終了
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG);
                csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD);
                csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO);
                csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG);
                csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO);
                csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG);
                csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG);
                csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD);

                // ・行政区コード～地区名３
                // 住民種別の下１桁が”8”（転出者）でない場合、住基行政区～住基地区名３をセット			
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) != "8")
                {
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    // *履歴番号 000002 2003/09/18 修正開始
                    // csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    // csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                    // *履歴番号 000002 2003/09/18 修正終了
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）の場合、管理情報（行政区初期化～地区３）を見て、
                    // クリアになっている場合は、セットしない
                    if (m_strGyosekuInit.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = string.Empty;
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                    }
                    // *履歴番号 000021 2005/12/12 修正開始
                    // 'csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                    // 'csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    else if (string.IsNullOrEmpty(m_strTenshutsuGyoseikuCD.Trim()))
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されていない場合は
                        // そのまま住基側のデータを設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    }
                    else
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されている場合は
                        // 行政区ＣＤマスタより行政区名称を取得し、設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, ' ');
                        // *履歴番号 000022 2005/12/15 修正開始
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                        // csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = m_strTenshutsuGyoseikuMei
                        // *履歴番号 000022 2005/12/15 修正終了
                        // *履歴番号 000021 2005/12/12 修正終了
                    }
                    if (m_strChiku1Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    }
                    if (m_strChiku2Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = string.Empty;
                    }
                    else
                    {
                        // *履歴番号 000002 2003/09/18 修正開始
                        // csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                        // csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                        // *履歴番号 000002 2003/09/18 修正終了
                    }
                    if (m_strChiku3Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                    }
                }

                // ・投票区コード～在留終了年月日
                csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD).ToString.RPadLeft(5);
                csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD);
                csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD);
                csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB);
                csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB);
                csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB);
                csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD);
                // csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).PadLeft(11)
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                // csAtenaRow(ABAtenaEntity.KAOKUSHIKIKB) = String.Empty
                // csAtenaRow(ABAtenaEntity.BIKOZEIMOKU) = String.Empty
                csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD);
                csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK);
                csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD);

                // * 履歴番号 000044 2011/11/09 追加開始
                if (blnJukiUmu && csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 住基が存在する且つ住基付随情報が存在する時、０番目を取得
                    csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    // 存在しない時、空行取得
                    csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow;
                    ClearAtenaFZY(csAtenaFzyRow);
                }

                // 宛名・住基よりデータ設定
                csAtenaFzyRow = SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow);
                // * 履歴番号 000044 2011/11/09 追加終了

                // 宛名標準
                if (blnJukiUmu && csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 住基が存在する且つ住基標準情報が存在する時、０番目を取得
                    csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    // 存在しない時、空行取得
                    csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).NewRow;
                    ClearAtenaHyojun(csAtenaHyojunRow);
                }

                // 宛名・住基よりデータ設定
                csAtenaHyojunRow = SetAtenaHyojun(csAtenaHyojunRow, csAtenaRow, csJukiDataRow);

                // 宛名付随標準
                if (blnJukiUmu && csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 住基が存在する且つ住基標準情報が存在する時、０番目を取得
                    csAtenaFzyHyojunRow = csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    // 存在しない時、空行取得
                    csAtenaFzyHyojunRow = csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).NewRow;
                    ClearAtenaFZYHyojun(csAtenaFzyHyojunRow);
                }

                // 宛名・住基よりデータ設定
                csAtenaFzyHyojunRow = SetAtenaFZYHyojun(csAtenaFzyHyojunRow, csAtenaRow, csJukiDataRow);

                // ---------------------------------------------------------------------------------------
                // 6. 宛名マスタの更新
                // 直近の住基データが存在している場合は修正、していなければ追加となる。
                // ---------------------------------------------------------------------------------------

                // 住基有りＦＬＧが”1”の時は、宛名マスタの更新を行なう
                if (blnJukiUmu)
                {
                    // * 履歴番号 000044 2011/11/09 修正開始
                    // intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow)
                    // If (intCount <> 1) Then
                    // ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名）
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode)
                    // End If

                    if (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0 && csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0 && csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                    {
                        intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow, csAtenaHyojunRow, csAtenaFzyRow, csAtenaFzyHyojunRow, false);
                        if (intCount != 1)
                        {
                            // * 履歴番号 000047 2011/12/26 追加開始
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            // * 履歴番号 000047 2011/12/26 追加終了
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // 何もしない
                        }
                    }
                    else
                    {
                        // 宛名
                        intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow);
                        if (intCount != 1)
                        {
                            // * 履歴番号 000047 2011/12/26 追加開始
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            // * 履歴番号 000047 2011/12/26 追加終了
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // 何もしない
                        }

                        // 宛名標準
                        csAtenaHyojunRow(ABAtenaHyojunEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI);
                        if (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            m_cABAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunRow);
                        }
                        else
                        {
                            m_cABAtenaHyojunB.InsertAtenaHyojunB(csAtenaHyojunRow);
                        }

                        if (blnAfterSekobi)
                        {
                            // 宛名付随
                            csAtenaFzyRow(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI);
                            if (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0)
                            {
                                m_cAtenaFzyB.UpdateAtenaFZYB(csAtenaFzyRow);
                            }
                            else
                            {
                                m_cAtenaFzyB.InsertAtenaFZYB(csAtenaFzyRow);
                            }
                            // 宛名付随標準
                            csAtenaFzyHyojunRow(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI);
                            if (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                            {
                                m_cABAtenaFZYHyojunB.UpdateAtenaFZYHyojunB(csAtenaFzyHyojunRow);
                            }
                            else
                            {
                                m_cABAtenaFZYHyojunB.InsertAtenaFZYHyojunB(csAtenaFzyHyojunRow);
                            }
                        }
                    }
                }
                // * 履歴番号 000044 2011/11/09 修正終了
                else
                {
                    // 上記以外は、宛名マスタの追加を行なう
                    csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Add(csAtenaRow);
                    // * 履歴番号 000044 2011/11/09 修正開始
                    // intCount = m_cAtenaB.InsertAtenaB(csAtenaRow)
                    intCount = m_cAtenaB.InsertAtenaB(csAtenaRow, csAtenaHyojunRow, csAtenaFzyRow, csAtenaFzyHyojunRow);
                    // * 履歴番号 000044 2011/11/09 修正終了
                    if (intCount != 1)
                    {
                        // エラー定義を取得（既に同一データが存在します。：宛名）
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode);
                    }
                }



                // ---------------------------------------------------------------------------------------
                // 7. 宛名累積マスタの更新　（前）
                // 履歴修正の場合は、退避されていた更新前データから直近レコードを取得し、
                // 更新前データとする。
                // ---------------------------------------------------------------------------------------

                // *履歴番号 000016 2005/11/01 追加開始
                // **
                // * 宛名累積（前）
                // *
                // *履歴番号 000016 2005/11/01 追加終了
                // *履歴番号 000003 2003/11/21 追加開始
                // *履歴番号 000032 2007/02/15 追加開始
                if (!(m_csReRirekiEntity == null) && m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count != 0)
                {
                    // 住基から履歴が全件渡ってくる処理の場合
                    // 更新前の宛名履歴情報から住登外優先区分＝１の直近レコードを取得
                    csBeforeRirekiRows = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUTOGAIYUSENKB='1' AND RRKED_YMD='99999999'");
                    // 処理日時を取得
                    StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");

                    // 対象レコードが存在する場合
                    if (csBeforeRirekiRows.Length >= 1)
                    {
                        // 宛名累積の新規レコードを取得
                        csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone();
                        csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow;
                        ClearAtenaRuiseki(ref csAtenaRuisekiRow);

                        // 処理日時をセット
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji;

                        // 前後区分 = 1
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "1";

                        // 履歴マスタの直近レコードをそのまま編集する
                        foreach (DataColumn currentCsDataColumn1 in csBeforeRirekiRows[0].Table.Columns)
                        {
                            csDataColumn = currentCsDataColumn1;
                            csAtenaRuisekiRow[csDataColumn.ColumnName] = csBeforeRirekiRows[0][csDataColumn.ColumnName];
                        }

                        // 処理事由ＣＤを宛名累積のRESERCEにセットする
                        // * 履歴番号 000058 2015/10/14 修正開始
                        // 宛名履歴を作成する（特殊処理の場合に特例として）は、「41：職権修正」を固定でリザーブを登録する
                        // csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                        if (blnIsCreateAtenaRireki == true)
                        {
                            csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00");
                        }
                        else
                        {
                            csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD);
                        }
                        // * 履歴番号 000058 2015/10/14 修正終了

                        // 宛名年金を取得する
                        csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD);
                        if (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 宛名累積設定(宛名年金)
                            this.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                        }
                        // 宛名国保を取得する
                        csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD);
                        if (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 宛名累積設定(宛名国保)
                            this.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                        }

                        // 宛名累積へ追加する
                        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow);

                        // 宛名累積マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)
                        // 宛名付随
                        if (m_csReRirekiFzyEntity is not null && m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 退避した宛名履歴付随にデータが存在する場合
                            csSelectedRows = m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYEntity.JUMINCD, csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString, ABAtenaRirekiFZYEntity.RIREKINO, csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString));
                            if (csSelectedRows.Count() > 0)
                            {
                                // 直近行が存在する時、累積付随の新規行を作成
                                csAtenaRuisekiFzyEntity = m_csAtenaRuisekiFzyEntity.Clone();
                                csAtenaRuisekiFzyRow = csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow;
                                ClearAtenaFZY(csAtenaRuisekiFzyRow);
                                // 直近履歴行を退避しておく
                                csAtenaRirekiFzyRow = csSelectedRows[0];
                                csAtenaRuisekiFzyRow = SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow);
                            }
                            else
                            {
                                // 上記以外の時、Nothing
                                csAtenaRuisekiFzyRow = null;
                            }
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiFzyRow = null;
                        }

                        // 宛履歴標準
                        if (m_csReRirekiHyojunEntity is not null && m_csReRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 退避した宛名履歴標準にデータが存在する場合
                            csSelectedRows = m_csReRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiHyojunEntity.JUMINCD, csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString, ABAtenaRirekiHyojunEntity.RIREKINO, csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString));
                            if (csSelectedRows.Count() > 0)
                            {
                                // 直近行が存在する時、累積標準の新規行を作成
                                csAtenaRuisekiHyojunEntity = m_csAtenaRuisekiHyojunEntity.Clone();
                                csAtenaRuisekiHyojunRow = csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow;
                                ClearAtenaHyojun(csAtenaRuisekiHyojunRow);
                                // 直近履歴行を退避しておく
                                csAtenaRirekiHyojunRow = csSelectedRows[0];
                                csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow);
                            }
                            else
                            {
                                // 上記以外の時、Nothing
                                csAtenaRuisekiHyojunRow = null;
                            }
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiHyojunRow = null;
                        }

                        // 宛名履歴付随標準
                        if (m_csRERirekiFZYHyojunEntity is not null && m_csRERirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 退避した宛名履歴付随標準にデータが存在する場合
                            csSelectedRows = m_csRERirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYHyojunEntity.JUMINCD, csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString, ABAtenaRirekiFZYHyojunEntity.RIREKINO, csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString));
                            if (csSelectedRows.Count() > 0)
                            {
                                // 直近行が存在する時、累積付随標準の新規行を作成
                                csAtenaRuisekiFZYHyojunEntity = m_csAtenaRuisekiFZYHyojunEntity.Clone();
                                csAtenaRuisekiFZYHyojunRow = csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow;
                                ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow);
                                // 直近履歴行を退避しておく
                                csAtenaRirekiFZYHyojunRow = csSelectedRows[0];
                                csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow);
                            }
                            else
                            {
                                // 上記以外の時、Nothing
                                csAtenaRuisekiFZYHyojunRow = null;
                            }
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiFZYHyojunRow = null;
                        }

                        intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow);
                        // * 履歴番号 000044 2011/11/09 修正終了
                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名累積）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名累積", objErrorStruct.m_strErrorCode);
                        }

                    }
                }
                else
                {
                    // *履歴番号 000032 2007/02/15 追加終了
                    // 宛名履歴マスタの住民住登外区分が１（住民）で履歴番号が一番大きいものを取得
                    cSearchKey = new ABAtenaSearchKey();
                    cSearchKey.p_strJuminCD = strJuminCD;
                    csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", true);
                    StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");
                    // データが存在する場合は、
                    if (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0)
                    {
                        csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0);

                        // 宛名累積の列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）　			
                        // 宛名累積より新しいRowを取得する
                        csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone();
                        csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow;
                        // 宛名履歴を初期化する
                        ClearAtenaRuiseki(ref csAtenaRuisekiRow);

                        // 宛名履歴マスタより宛名累積マスタの編集を行う(共通)
                        // 処理日時=システム日時
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji;

                        // 前後区分 = 1
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "1";

                        // それ以外の項目については、宛名マスタをそのまま編集する
                        // 宛名履歴を宛名履歴へそのまま編集する
                        foreach (DataColumn currentCsDataColumn2 in csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns)
                        {
                            csDataColumn = currentCsDataColumn2;
                            csAtenaRuisekiRow[csDataColumn.ColumnName] = csAtenaRirekiRow[csDataColumn];
                        }

                        // *履歴番号 000015 2005/08/17 追加開始 000029 2006/04/19 修正開始
                        // 処理事由ＣＤを宛名累積のRESERCEにセットする
                        // * 履歴番号 000058 2015/10/14 修正開始
                        // 宛名履歴を作成する（特殊処理の場合に特例として）は、「41：職権修正」を固定でリザーブを登録する
                        // csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                        if (blnIsCreateAtenaRireki == true)
                        {
                            csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00");
                        }
                        else
                        {
                            csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD);
                        }
                        // * 履歴番号 000058 2015/10/14 修正終了
                        // ' 汎用ＣＤを宛名累積のRESERCEにセットする
                        // csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.HANYOCD)
                        // *履歴番号 000015 2005/08/17 追加終了 000029 2006/04/19 修正終了

                        // *履歴番号 000003 2003/11/21 追加開始
                        // 宛名年金を取得する
                        csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD);
                        if (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 宛名累積設定(宛名年金)
                            this.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                        }
                        // 宛名国保を取得する
                        csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD);
                        if (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 宛名累積設定(宛名国保)
                            this.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                        }
                        // *履歴番号 000003 2003/11/21 追加終了

                        // 宛名累積へ追加する
                        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow);

                        // * 履歴番号 000044 2011/11/09 修正開始
                        // ' 宛名累積マスタの追加を行う
                        // intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)

                        // 宛名履歴付随データ取得
                        csAtenaRuisekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString, string.Empty, true);
                        if (csAtenaRuisekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 直近の宛名履歴付随が存在した時、宛名累積付随を作成
                            csAtenaRuisekiFzyRow = m_csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow;
                            ClearAtenaFZY(csAtenaRuisekiFzyRow);
                            // 直近履歴行を退避しておく
                            csAtenaRirekiFzyRow = csAtenaRuisekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0);
                            csAtenaRuisekiFzyRow = SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow);
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiFzyRow = null;
                        }

                        // 宛名履歴標準
                        csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString, string.Empty, true);
                        if (csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 直近の宛名履歴標準が存在した時、宛名累積標準を作成
                            csAtenaRuisekiHyojunRow = m_csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow;
                            ClearAtenaHyojun(csAtenaRuisekiHyojunRow);
                            // 直近履歴行を退避しておく
                            csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0);
                            csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow);
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiHyojunRow = null;
                        }

                        // 宛名履歴付随標準
                        csAtenaRirekiFZYHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString, string.Empty, true);
                        if (csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                            // 直近の宛名履歴付随標準が存在した時、宛名累積付随標準を作成
                            csAtenaRuisekiFZYHyojunRow = m_csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow;
                            ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow);
                            // 直近履歴行を退避しておく
                            csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0);
                            csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow);
                        }
                        else
                        {
                            // 上記以外の時、Nothing
                            csAtenaRuisekiFZYHyojunRow = null;
                        }

                        // 宛名累積マスタの追加を行う
                        intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名累積）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名累積", objErrorStruct.m_strErrorCode);
                        }
                    }
                    // *履歴番号 000032 2007/02/15 追加開始
                }
                // *履歴番号 000032 2007/02/15 追加終了
                // *履歴番号 000003 2003/11/21 追加終了



                // ---------------------------------------------------------------------------------------
                // 8. 宛名履歴マスタの更新
                // ---------------------------------------------------------------------------------------

                // **
                // * 宛名履歴
                // *
                // *履歴番号 000016 2005/11/18 修正開始
                // * corresponds to VS2008 Start 2010/04/16 000043
                // '''*履歴番号 000013 2005/06/19 追加開始
                // ''''履歴番号の取得
                // '''csRirekiNoEntity = m_cAtenaRirekiB.GetRirekiNo(strJuminCD)

                // '''' 宛名マスタより宛名履歴マスタの編集を行う(共通)
                // '''' 履歴番号　　　新規のばあいは、0001　　修正の場合は、宛名履歴マスタの最終番号にＡＤＤ　１する
                // '''' それ以外の項目については、宛名マスタをそのまま編集する			
                // '''If (csRirekiNoEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                // '''    ' 履歴番号
                // '''    strMaxRirekino = "0001"
                // '''Else
                // '''    ' 履歴番号(先頭行の履歴番号+1)
                // '''    strMaxRirekino = CType((CType(csRirekiNoEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                // '''End If
                // '''*履歴番号 000013 2005/06/19 追加修正
                // * corresponds to VS2008 End 2010/04/16 000043


                // ---------------------------------------------------------------------------------------
                // 8-1. 該当の履歴データを全件取得する
                // ---------------------------------------------------------------------------------------

                cSearchKey = new ABAtenaSearchKey();
                cSearchKey.p_strJuminCD = strJuminCD;
                csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", true);

                // * 履歴番号 000044 2011/11/09 追加開始
                // 宛名履歴付随の全レコードを取得
                csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(strJuminCD, string.Empty, string.Empty, true);
                // * 履歴番号 000044 2011/11/09 追加終了

                // 宛名履歴標準
                csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(strJuminCD, string.Empty, string.Empty, true);

                // 宛名履歴付随標準
                csAtenaRirekiFZYHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(strJuminCD, string.Empty, string.Empty, true);

                // 履歴番号の取得
                if (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count == 0)
                {
                    // 履歴番号
                    strMaxRirekino = "0001";
                }
                else
                {
                    // 履歴番号
                    // *履歴番号 000023 2005/12/16 修正開始
                    // 履歴番号降順で並べ替えて最大履歴番号＋１を取得する
                    // 'strMaxRirekino = CType(csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count + 1, String).PadLeft(4, "0"c)
                    csRirekiNORows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC");
                    intMaxRirekiNO = Conversions.ToInteger(csRirekiNORows[0](ABAtenaRirekiEntity.RIREKINO)) + 1;
                    strMaxRirekino = intMaxRirekiNO.ToString().RPadLeft(4, '0');
                    // *履歴番号 000023 2005/12/16 修正終了
                }
                // *履歴番号 000016 2005/11/18 修正終了


                // ---------------------------------------------------------------------------------------
                // 8-2. 直前の履歴データを更新する
                // 住基データが存在している場合のみ行う。
                // ---------------------------------------------------------------------------------------

                // ・住基有りＦＬＧが”1”の時は、住基優先で指定年月日に99999999で宛名履歴マスタをよみ履歴終了年月日をシステ、
                // ム日付の前日をセットし、宛名履歴マスタ更新を実行する
                if (blnJukiUmu)
                {

                    // *履歴番号 000016 2005/11/01 修正開始
                    // * コメント**********************************************************************************************
                    // * ＜宛名履歴マスタ更新方法＞                                                                           *
                    // * 住基との連携方法を直近データか履歴全件かの２パターンでしか行わないようにしたので、以下を修正します。 *
                    // * 住基より【処理事由ＣＤ】項目追加してもらったので、それを見て特殊処理修正(03)の時は、宛名履歴マスタの *
                    // * 直近データを更新する。それ以外のときは直近レコードの終了年月日を更新→新規レコード追加となります。   *
                    // ********************************************************************************************************
                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // '''*履歴番号 000013 2005/06/19 追加開始
                    // '''' 日付クラスの必要な設定を行う
                    // '''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    // '''m_cfDateClass.p_enEraType = UFEraType.Number

                    // ''''履歴データ全件取得
                    // '''cSearchKey = New ABAtenaSearchKey()
                    // '''cSearchKey.p_strJuminCD = strJuminCD
                    // '''cSearchKey.p_strJuminYuseniKB = "1"
                    // '''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                    // ''''履歴番号を昇順に並び替え
                    // '''csSortRirekiDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO)

                    // ''''履歴データがなくなるまで繰返す
                    // '''' データ分繰り返す
                    // '''For Each csDataRow In csSortRirekiDataRow
                    // '''    'ＤＢにある開始年月日と同じかそれ以上のもの　かつ　ＤＢの開始年月日が終了年月日より過去のもの
                    // '''    If (CType(csJukiDataRow(ABAtenaRirekiEntity.RRKST_YMD), String) <= CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String)) AndAlso _
                    // '''        (CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String) < CType(csDataRow(ABAtenaRirekiEntity.RRKED_YMD), String)) Then

                    // '''        ' 宛名マスタを宛名履歴へそのまま編集する
                    // '''        For Each csDataColumn In csAtenaRow.Table.Columns
                    // '''            csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                    // '''        Next csDataColumn

                    // '''        '追加用レコードの編集を行う
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino                                         '履歴番号
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB) = csDataRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)   '住民住登外区分
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINYUSENIKB) = csDataRow(ABAtenaRirekiEntity.JUMINYUSENIKB)     '住民優先区分
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = csDataRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB)   '住登外優先区分
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = csDataRow(ABAtenaRirekiEntity.RRKST_YMD)             '開始年月日
                    // '''        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = csDataRow(ABAtenaRirekiEntity.RRKED_YMD)             '終了年月日

                    // '''        '履歴番号に＋１
                    // '''        strMaxRirekino = CType(CType(strMaxRirekino, Integer) + 1, String).PadLeft(4, "0"c)

                    // '''        ' 宛名履歴マスタの追加を行う
                    // '''        'csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
                    // '''        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                    // '''        If (intCount <> 1) Then
                    // '''            ' エラー定義を取得（既に同一データが存在します。：宛名履歴）
                    // '''            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // '''            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    // '''            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                    // '''        End If

                    // '''        '終了年月日に開始年月日の前日をセットする
                    // '''        m_cfDateClass.p_strDateValue = CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                    // '''        csDataRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                    // '''        '宛名履歴マスタの修正を行う
                    // '''        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                    // '''        If (intCount <> 1) Then
                    // '''            ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                    // '''            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // '''            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    // '''            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                    // '''        End If

                    // '''        '特殊処理フラグをTrueにする
                    // '''        blnTokushuFG = True

                    // '''    End If
                    // '''Next
                    // ''''*履歴番号 000013 2005/06/19 追加終了
                    // *履歴番号 000016 2005/11/18 削除開始
                    // ''' 日付クラスの必要な設定を行う
                    // '''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    // '''m_cfDateClass.p_enEraType = UFEraType.Number
                    // *履歴番号 000016 2005/11/18 削除終了
                    // * corresponds to VS2008 End 2010/04/16 000043

                    // ---------------------------------------------------------------------------------------
                    // 8-2-1. 特殊処理の場合は、今ある直近レコードを上書きする。
                    // ---------------------------------------------------------------------------------------

                    // *履歴番号 000041 2009/06/18 修正開始
                    // *履歴番号 000018 2005/11/27 修正開始
                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // ''' 処理事由コードが"03"(特殊処理修正)の場合は直近レコードの修正→更新のみを行う
                    // * corresponds to VS2008 End 2010/04/16 000043
                    // 'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" Then
                    // ' 処理事由コードが"03"(特殊処理修正)　または　"04"(住民票ＣＤ修正)の場合は
                    // ' 直近レコードの修正→更新のみを行う(追加せずに更新だけ)
                    // 処理事由コードが"03"(特殊処理修正)　または　"04"(住民票ＣＤ修正)の場合 または 
                    // 履歴データ全件削除が行われず かつ 処理事由コードが"08"(履歴修正)の場合は
                    // 直近レコードの修正→更新のみを行う(追加せずに更新だけ)
                    // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                    // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" Then
                    // * 履歴番号 000050 2014/06/25 修正開始
                    // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                    // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                    // (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                    // * 履歴番号 000058 2015/10/14 修正開始
                    // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                    // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                    // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse _
                    // (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                    if (blnIsCreateAtenaRireki == false && (Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "03" || Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "04" || Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "05" || m_blnRirekiShusei == false && Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "08"))
                    {
                        // * 履歴番号 000058 2015/10/14 修正終了
                        // * 履歴番号 000050 2014/06/25 修正終了
                        // *履歴番号 000018 2005/11/27 修正終了
                        // *履歴番号 000041 2009/06/18 修正終了
                        // 宛名履歴データ抽出(住民住登外区分が"1"で履歴番号最大降順で並び替え)
                        csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINJUTOGAIKB = '1'", ABAtenaRirekiEntity.RIREKINO + " DESC");

                        // 直近レコードの取得
                        // 特殊処理修正の場合は必ず履歴マスタにあるはずなので無い場合は考慮しない
                        csUpRirekiRow = csUpRirekiRows[0];

                        // 直近レコードを修正して更新する
                        // 宛名マスタを宛名履歴へそのまま編集する
                        foreach (DataColumn currentCsDataColumn3 in csAtenaRow.Table.Columns)
                        {
                            csDataColumn = currentCsDataColumn3;
                            // *履歴番号 000030 2006/08/10 修正開始
                            // 履歴開始年月日は更新しない
                            // 'csUpRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                            if (csDataColumn.ColumnName != ABAtenaEntity.RRKST_YMD)
                            {
                                csUpRirekiRow[csDataColumn.ColumnName] = csAtenaRow[csDataColumn];
                            }
                            // *履歴番号 000030 2006/08/10 修正終了
                        }

                        // * 履歴番号 000044 2011/11/09 修正開始
                        // ' 宛名履歴マスタを更新する
                        // intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                        // 宛名履歴の直近行から宛名履歴付随の直近行検索
                        csSelectedRows = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYEntity.JUMINCD, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, ABAtenaRirekiFZYEntity.RIREKINO, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString));
                        if (csSelectedRows.Count() > 0)
                        {
                            // 検索結果が存在する時、宛名付随から宛名履歴付随にデータを写す
                            csAtenaRirekiFzyTokushuRow = csSelectedRows[0];
                            csAtenaRirekiFzyTokushuRow = SetAtenaRirekiFzy(csAtenaRirekiFzyTokushuRow, csAtenaFzyRow);
                        }
                        else
                        {

                            // 上記以外の時、Nothing
                            csAtenaRirekiFzyTokushuRow = null;
                        }

                        // 宛名履歴標準
                        // 宛名履歴の直近行から宛名履歴標準の直近行検索
                        csSelectedRows = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiHyojunEntity.JUMINCD, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, ABAtenaRirekiHyojunEntity.RIREKINO, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString));
                        if (csSelectedRows.Count() > 0)
                        {
                            // 検索結果が存在する時、宛名付随から宛名履歴付随にデータを写す
                            csAtenaRirekiHyojunTokushuRow = csSelectedRows[0];
                            csAtenaRirekiHyojunTokushuRow = SetAtenaRirekiHyojun(csAtenaRirekiHyojunTokushuRow, csAtenaHyojunRow, csUpRirekiRow);
                        }
                        else
                        {

                            // 上記以外の時、Nothing
                            csAtenaRirekiHyojunTokushuRow = null;
                        }

                        // 宛名履歴付随標準
                        // 宛名履歴の直近行から宛名履歴付随標準の直近行検索
                        csSelectedRows = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYHyojunEntity.JUMINCD, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, ABAtenaRirekiFZYHyojunEntity.RIREKINO, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString));
                        if (csSelectedRows.Count() > 0)
                        {
                            // 検索結果が存在する時、宛名付随から宛名履歴付随にデータを写す
                            csAtenaRirekiFzyHyojunTokushuRow = csSelectedRows[0];
                            csAtenaRirekiFzyHyojunTokushuRow = SetAtenaRirekiFZYHyojun(csAtenaRirekiFzyHyojunTokushuRow, csAtenaFzyHyojunRow);
                        }
                        else
                        {

                            // 上記以外の時、Nothing
                            csAtenaRirekiFzyHyojunTokushuRow = null;
                        }

                        // 宛名履歴マスタを更新する
                        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csAtenaRirekiHyojunTokushuRow, csAtenaRirekiFzyTokushuRow, csAtenaRirekiFzyHyojunTokushuRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        // 更新件数が１件でないとエラー
                        if (intCount != 1)
                        {
                            // エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        blnTokushuFG = true;
                    }
                    else
                    {
                        blnTokushuFG = false;
                    }
                    // *履歴番号 000016 2005/11/01 修正終了

                    // ---------------------------------------------------------------------------------------
                    // 8-2-2. 特殊処理以外の場合、今ある直近レコードの終了年月日を閉じる。
                    // ---------------------------------------------------------------------------------------

                    // *履歴番号 000013 2005/06/19 修正開始
                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // ''' 日付クラスの必要な設定を行う
                    // * corresponds to VS2008 End 2010/04/16 000043
                    // 'm_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    // 'm_cfDateClass.p_enEraType = UFEraType.Number
                    // 特殊処理の判定
                    if (!blnTokushuFG)
                    {
                        // ☆宛名履歴マスタの直近レコードの終了年月日を修正して更新☆
                        // *履歴番号 000013 2005/06/19 修正終了
                        // *履歴番号 000016 2005/11/18 修正開始
                        // * corresponds to VS2008 Start 2010/04/16 000043
                        // '''cSearchKey = New ABAtenaSearchKey()
                        // '''cSearchKey.p_strJuminCD = strJuminCD
                        // '''cSearchKey.p_strJuminYuseniKB = "1"
                        // '''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
                        // '''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                        // '''    csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
                        // '''    '*履歴番号 000012 2005/06/07 修正開始
                        // '''    'm_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") 'システム日付
                        // '''    m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                        // '''    '*履歴番号 000012 2005/06/07 修正終了
                        // '''    csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        // '''    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                        // '''    If (intCount <> 1) Then
                        // '''        ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                        // '''        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        // '''        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        // '''        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                        // '''    End If
                        // '''End If
                        // * corresponds to VS2008 End 2010/04/16 000043
                        // 宛名履歴データ抽出(住民優先区分が"1"で履歴終了年月日が'99999999')
                        csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINYUSENIKB = '1' AND RRKED_YMD = '99999999'");

                        // 直近レコードを取得し、アップデート
                        if (csUpRirekiRows.Length > 0)
                        {
                            csUpRirekiRow = csUpRirekiRows[0];
                            m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRow(ABAtenaRirekiEntity.RRKST_YMD));
                            csUpRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);
                            // * 履歴番号 000044 2011/11/09 修正開始
                            // intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                            // 直近宛名履歴データを退避データから取得
                            // * 履歴番号 000047 2011/12/26 修正開始
                            // csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                            // csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                            // csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                            csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(csAtenaRirekiFzyEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                            // * 履歴番号 000047 2011/12/26 修正終了
                            // 宛名履歴標準
                            csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(csAtenaRirekiHyojunEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                            // 宛名履歴付随標準
                            csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(csAtenaRirekiFZYHyojunEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                            intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                            // * 履歴番号 000044 2011/11/09 修正終了
                            if (intCount != 1)
                            {
                                // エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                            }
                        }
                        else
                        {

                        }
                        // *履歴番号 000016 2005/11/18 修正終了

                        // *履歴番号 000013 2005/06/19 修正開始
                    }
                    // *履歴番号 000013 2005/06/19 修正終了
                }

                // ---------------------------------------------------------------------------------------
                // 8-3. 直近レコードを更新する
                // 特殊処理以外の場合のみ行う。
                // ---------------------------------------------------------------------------------------

                // *履歴番号 000013 2005/06/19 修正開始
                // 特殊処理の判定
                if (!blnTokushuFG)
                {

                    // ---------------------------------------------------------------------------------------
                    // 8-3-1. 直前の履歴が住登外、かつ再転入の場合は直前の履歴の住登外データの終了年月日を閉じる。
                    // ---------------------------------------------------------------------------------------

                    // ・住民種別の下１桁が”0”（住民）でかつ住登外有りＦＬＧが”1”の時、住登外優先で指定年月日に99999999で宛名
                    // 履歴マスタをよみ履歴終了年月日をシステム日付の前日をセットし、宛名履歴マスタ更新を実行する。
                    if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) == "0" & blnJutogaiUmu)
                    {
                        // *履歴番号 000016 2005/11/18 修正開始
                        // 日付クラスの必要な設定を行う
                        // * corresponds to VS2008 Start 2010/04/16 000043
                        // '''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                        // '''m_cfDateClass.p_enEraType = UFEraType.Number
                        // '''cSearchKey = New ABAtenaSearchKey()
                        // '''cSearchKey.p_strJuminCD = strJuminCD
                        // '''cSearchKey.p_strJutogaiYusenKB = "1"
                        // '''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
                        // '''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                        // '''    csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
                        // '''    m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaEntity.RRKST_YMD), String)
                        // '''    csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        // '''    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                        // '''    If (intCount <> 1) Then
                        // '''        ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                        // '''        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        // '''        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        // '''        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                        // '''    End If
                        // '''End If
                        // * corresponds to VS2008 End 2010/04/16 000043
                        // 宛名履歴データ抽出(住登外優先区分が"1"で履歴終了年月日が'99999999')
                        csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUTOGAIYUSENKB = '1' AND RRKED_YMD = '99999999'");

                        // 直近レコードを取得し、アップデート
                        if (csUpRirekiRows.Length > 0)
                        {
                            csUpRirekiRow = csUpRirekiRows[0];
                            m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRow(ABAtenaEntity.RRKST_YMD));
                            csUpRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);
                            // * 履歴番号 000044 2011/11/09 修正開始
                            // intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                            // 直近宛名履歴データを退避データから取得
                            // * 履歴番号 000047 2011/12/26 修正開始
                            // csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                            // csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                            // csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                            csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(csAtenaRirekiFzyEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                            // * 履歴番号 000047 2011/12/26 修正終了

                            // 宛名履歴標準
                            csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(csAtenaRirekiHyojunEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                            // 宛名履歴付随標準
                            csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(csAtenaRirekiFZYHyojunEntity, csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                            intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                            // * 履歴番号 000044 2011/11/09 修正終了
                            if (intCount != 1)
                            {
                                // エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                            }
                        }
                        else
                        {

                        }

                        // *履歴番号 000016 2005/11/18 修正終了
                    }

                    // *履歴番号 000013 2005/06/19 修正開始
                    // ''' 宛名履歴マスタを該当者の全履歴を取得する
                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // '''cSearchKey = New ABAtenaSearchKey()
                    // '''cSearchKey.p_strJuminCD = strJuminCD
                    // '''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                    // '''' 宛名履歴の列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）
                    // '''csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
                    // '''Me.ClearAtenaRireki(csAtenaRirekiRow)

                    // '''' 宛名マスタより宛名履歴マスタの編集を行う(共通)
                    // '''' 履歴番号　　　新規のばあいは、0001　　修正の場合は、宛名履歴マスタの最終番号にＡＤＤ　１する
                    // '''' それ以外の項目については、宛名マスタをそのまま編集する			
                    // '''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                    // '''    ' 履歴番号
                    // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = "0001"
                    // '''Else
                    // '''    ' 履歴番号で降順に並び替え
                    // '''    csAtenaRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                    // '''    ' 履歴番号(先頭行の履歴番号+1)
                    // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csAtenaRirekiRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                    // '''End If
                    // * corresponds to VS2008 End 2010/04/16 000043

                    // ---------------------------------------------------------------------------------------
                    // 8-3-2. 更新用の直近レコードを作成する。
                    // ---------------------------------------------------------------------------------------

                    // 宛名履歴ロウがnothingの場合はスキーマを取得する
                    if (csAtenaRirekiRow is null)
                    {
                        // 宛名履歴マスタのスキーマを取得する
                        csAtenaRirekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiEntity.TABLE_NAME);
                        // 宛名履歴ロウを新規作成する
                        csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow();
                    } // 最大履歴番号をセットする
                      // *履歴番号 000016 2005/11/01 削除開始
                      // * corresponds to VS2008 Start 2010/04/16 000043
                      // '''csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino
                      // * corresponds to VS2008 End 2010/04/16 000043
                      // *履歴番号 000016 2005/11/01 削除終了
                      // *履歴番号 000013 2005/06/19 修正終了

                    // 宛名マスタを宛名履歴へそのまま編集する
                    foreach (DataColumn currentCsDataColumn4 in csAtenaRow.Table.Columns)
                    {
                        csDataColumn = currentCsDataColumn4;
                        csAtenaRirekiRow[csDataColumn.ColumnName] = csAtenaRow[csDataColumn];
                    }
                    // * 履歴番号 000044 2011/11/09 追加開始

                    if (csAtenaRirekiFzyRow is null)
                    {
                        // 宛名履歴付随の新規行作成
                        csAtenaRirekiFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiFZYEntity.TABLE_NAME);
                        csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow;
                    }
                    else
                    {
                        // 何もしない
                    }

                    // 宛名付随を宛名履歴付随にコピー
                    foreach (DataColumn csColumn in csAtenaFzyRow.Table.Columns)
                        csAtenaRirekiFzyRow[csColumn.ColumnName] = csAtenaFzyRow[csColumn.ColumnName];
                    // * 履歴番号 000044 2011/11/09 追加終了

                    // 宛名履歴標準
                    if (csAtenaRirekiHyojunRow is null)
                    {
                        // 宛名履歴標準の新規行作成
                        csAtenaRirekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiHyojunEntity.TABLE_NAME);
                        csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow;
                    }
                    else
                    {
                        // 何もしない
                    }

                    // 宛名標準を宛名履歴標準にコピー
                    foreach (DataColumn csColumn in csAtenaHyojunRow.Table.Columns)
                    {
                        if (csAtenaRirekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName))
                        {
                            csAtenaRirekiHyojunRow[csColumn.ColumnName] = csAtenaHyojunRow[csColumn.ColumnName];
                        }
                    }

                    // 宛名履歴付随標準
                    if (csAtenaRirekiFZYHyojunRow is null)
                    {
                        // 宛名履歴付随標準の新規行作成
                        csAtenaRirekiFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME);
                        csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow;
                    }
                    else
                    {
                        // 何もしない
                    }

                    // 宛名付随標準を宛名履歴付随標準にコピー
                    foreach (DataColumn csColumn in csAtenaFzyHyojunRow.Table.Columns)
                        csAtenaRirekiFZYHyojunRow[csColumn.ColumnName] = csAtenaFzyHyojunRow[csColumn.ColumnName];

                    // *履歴番号 000012 2005/06/07 削除開始
                    // *履歴番号 000011 2005/06/05 追加開始
                    // '宛名マスタの開始日を当日にする
                    // m_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") 'システム日付
                    // csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = m_cfDateClass.p_strDay
                    // *履歴番号 000011 2005/06/05 追加終了
                    // *履歴番号 000012 2005/06/07 追加終了

                    // 宛名履歴マスタの追加を行う
                    // *履歴番号 000013 2005/06/21 削除開始
                    // csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
                    // *履歴番号 000013 2005/06/21 削除終了
                    // *履歴番号 000016 2005/11/01 修正開始
                    // * corresponds to VS2008 Start 2010/04/16 000043
                    // '''intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                    // '''If (intCount <> 1) Then
                    // '''    ' エラー定義を取得（既に同一データが存在します。：宛名履歴）
                    // '''    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // '''    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    // '''    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                    // '''End If
                    // * corresponds to VS2008 End 2010/04/16 000043


                    // ---------------------------------------------------------------------------------------
                    // 8-3-3. 住登外データ、住基データを更新する。
                    // ---------------------------------------------------------------------------------------

                    // 住登外が起きているデータで　かつ　元の住登外レコードを全て再セットし終わってない場合
                    if (m_blnJutogaiAriFG == true && m_intJutogaiRowCnt > m_intJutogaiInCnt)
                    {

                        // ---------------------------------------------------------------------------------------
                        // 8-3-3-1. 履歴データの更新処理で退避していた住登外データが残っている時。
                        // 残っている住登外データを全て更新する。
                        // ---------------------------------------------------------------------------------------

                        // 残りの住登外レコードを再セットしていく
                        var loopTo = m_intJutogaiRowCnt - 1;
                        for (intIdx = m_intJutogaiInCnt; intIdx <= loopTo; intIdx += 1)
                        {
                            intForCnt += 1;

                            // 住登外レコードが残っている状態のときJukiDataKoshin08ﾒｿｯﾄﾞで既に取得してあるので
                            // 一回目のループでは取得しない。
                            if (intForCnt > 1)
                            {
                                m_intJutogaiST_YMD = Conversions.ToInteger(m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKST_YMD));
                            }

                            if (Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD)) > m_intJutogaiST_YMD)
                            {

                                // 連番用カウントを＋１
                                m_intRenbanCnt += 1;
                                // 履歴番号をセット
                                m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                                // *履歴番号 000023 2005/12/16 追加開始
                                // 住基のレコードが再転入レコードの時でかつ住登外のレコードが直近レコードの場合
                                // 終了年月日を住基レコードの開始年月日の一日前にセットする
                                if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) == "0" && Conversions.ToString(m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD)) == "99999999")
                                {

                                    m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD));
                                    m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);

                                }
                                // *履歴番号 000023 2005/12/16 追加終了
                                // * 履歴番号 000044 2011/11/09 修正開始
                                // ' 住登外ロウをインサート
                                // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx))

                                // 直近宛名履歴データを退避データから取得
                                csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO).ToString);

                                // 宛名履歴標準
                                csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO).ToString);
                                // 宛名履歴付随標準
                                csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO).ToString);

                                intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows[intIdx], csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                                // * 履歴番号 000044 2011/11/09 修正終了
                                if (intCount != 1)
                                {
                                    // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                }
                            }
                            else
                            {
                                if (intJukiInCnt == 0)
                                {
                                    // 住基データの直近をインサート
                                    // 連番用カウントを＋１
                                    m_intRenbanCnt += 1;
                                    // 履歴番号をセット
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                                    // * corresponds to VS2008 Start 2010/04/16 000043
                                    // '*履歴番号 000020 2005/12/07 修正開始
                                    // ''''*履歴番号 000018 2005/11/27 修正開始
                                    // ''''If m_blnSaiTenyuFG = True Then
                                    // '''If m_blnHenkanFG = False Then
                                    // '''    '*履歴番号 000018 2005/11/27 修正終了
                                    // '''    ' 住登外優先区分は"1"
                                    // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                                    // '''    ' 履歴終了年月日を住登外ロウの履歴開始年月日の一日前にセットする
                                    // '''    m_cfDateClass.p_strDateValue = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                    // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                                    // '''End If
                                    // * corresponds to VS2008 End 2010/04/16 000043

                                    if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) == "0")
                                    {
                                        // 住民の時
                                        // 住登外優先区分は"1"
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                                    }
                                    // 住民でない時
                                    else if (m_blnHenkanFG == false)
                                    {
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                                        // *履歴番号 000023 2005/12/16 追加開始
                                        // 履歴終了年月日を住登外ロウの履歴開始年月日の一日前にセットする
                                        m_cfDateClass.p_strDateValue = Conversions.ToString(m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKST_YMD));
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);
                                    }
                                    // *履歴番号 000023 2005/12/16 追加終了
                                    else
                                    {
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                                    }
                                    // * corresponds to VS2008 Start 2010/04/16 000043
                                    // '*履歴番号 000023 2005/12/16 削除開始
                                    // '''' 履歴終了年月日を住登外ロウの履歴開始年月日の一日前にセットする
                                    // '''m_cfDateClass.p_strDateValue = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                    // '''csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                                    // '*履歴番号 000023 2005/12/16 削除終了
                                    // * corresponds to VS2008 End 2010/04/16 000043
                                    // *履歴番号 000020 2005/12/07 修正終了

                                    // * 履歴番号 000044 2011/11/09 修正開始
                                    // intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                                    // 直近宛名履歴データを退避データから取得
                                    csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                                    // 宛名履歴標準
                                    csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                                    // 宛名履歴付随標準
                                    csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                                    intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                                    // * 履歴番号 000044 2011/11/09 修正終了

                                    if (intJukiInCnt != 1)
                                    {
                                        // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                    }
                                }

                                // 住基データの直近がインサートされていて
                                // 再転入フラグがTrueのとき住登外を起こす作業を行う
                                // *履歴番号 000018 2005/11/27 修正開始
                                // If intJukiInCnt <> 0 AndAlso m_blnSaiTenyuFG = True Then
                                if (intJukiInCnt != 0 && m_blnHenkanFG == false && Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) != "0")
                                {
                                    // *履歴番号 000018 2005/11/27 修正終了
                                    // 連番用カウントを＋１
                                    m_intRenbanCnt += 1;
                                    // 履歴番号をセット
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');
                                    // 住登外優先区分を"0"に設定
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                                    // 履歴開始年月日を住登外ロウの履歴開始と同一のものにする
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = Conversions.ToString(m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKST_YMD));
                                    // 履歴終了年月日をオール９に設定
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = "99999999";

                                    // * 履歴番号 000044 2011/11/09 修正開始
                                    // intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                                    // 直近宛名履歴データを退避データから取得
                                    csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                                    // 宛名履歴標準
                                    csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                                    // 宛名履歴付随標準
                                    csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                                    intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                                    // * 履歴番号 000044 2011/11/09 修正終了

                                    if (intCount != 1)
                                    {
                                        // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                    }

                                    // *履歴番号 000018 2005/11/27 追加開始
                                    m_blnHenkanFG = true;
                                    // *履歴番号 000018 2005/11/27 追加終了
                                }

                                // 住登外ロウをインサート
                                // 連番用カウントを＋１
                                m_intRenbanCnt += 1;
                                // 履歴番号をセット
                                m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                                // * 履歴番号 000044 2011/11/09 修正開始
                                // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx))

                                // 直近宛名履歴データを退避データから取得
                                csCkinRirekiFzyRows = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[intIdx](ABAtenaRirekiEntity.RIREKINO).ToString);
                                // 宛名履歴標準
                                csCkinRirekiHyojunRows = this.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                                // 宛名履歴付随標準
                                csCkinRirekiFzyHyojunRows = this.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString);

                                intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows[intIdx], csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows);
                                // * 履歴番号 000044 2011/11/09 修正終了

                                if (intCount != 1)
                                {
                                    // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                }

                            }
                        }

                        // 住基の直近レコードがまだインサートされていなければインサート
                        if (intJukiInCnt == 0)
                        {

                            // 連番用カウントを＋１
                            m_intRenbanCnt += 1;
                            // 履歴番号をセット
                            csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                            if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RSubstring(1, 1) == "0")
                            {
                                // *履歴番号 000020 2005/12/07 修正開始
                                // データ種別が住民の時は住登外優先区分は"1"
                                // * corresponds to VS2008 Start 2010/04/16 000043
                                // '''csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                                // * corresponds to VS2008 End 2010/04/16 000043
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                            }
                            else if (m_blnHenkanFG == false)
                            {
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                            }
                            else
                            {
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                                // *履歴番号 000020 2005/12/07 修正終了
                            }

                            // * 履歴番号 000044 2011/11/09 修正開始
                            // intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                            // さきほど取得した宛名履歴付随行の履歴番号を宛名履歴行の履歴番号で上書き
                            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                            // 宛名履歴標準
                            csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                            // 宛名履歴付随標準
                            csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);

                            // インサート
                            intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow);
                            // * 履歴番号 000044 2011/11/09 修正終了
                            if (intCount != 1)
                            {
                                // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                            }
                        }
                    }
                    else
                    {

                        // ---------------------------------------------------------------------------------------
                        // 8-3-3-2. 直近の住登外データが存在しない、または、
                        // 履歴データの更新処理で退避していた住登外データが全て更新されている時、
                        // 住基データを更新する。
                        // ---------------------------------------------------------------------------------------

                        // 住登外が起きていないデータ、住登外データを再セットし終わっているデータはそのままインサート
                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino;

                        // *履歴番号 000020 2005/12/07 修正開始
                        // * corresponds to VS2008 Start 2010/04/16 000043
                        // '''*履歴番号 000018 2005/11/27 修正開始
                        // '''If m_blnSaiTenyuFG = True Then
                        // 'If m_blnHenkanFG = False Then
                        // '    ' 再転入が起きている場合には住登外優先区分は"1"
                        // '    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        // 'Else
                        // '    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                        // 'End If
                        // '''*履歴番号 000018 2005/11/27 修正終了
                        // * corresponds to VS2008 End 2010/04/16 000043

                        if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) == "0")
                        {
                            // 種別が"*0"の場合は無条件で住登外優先区分は"1"
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                            m_blnHenkanFG = false;
                        }
                        // 種別が"*0"でないとき
                        // *履歴番号 000023 2005/12/16 修正開始
                        // 'If m_blnHenkanFG = False Then
                        // '    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        // 'Else
                        // '    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                        // 'End If
                        else if (m_blnHenkanFG == true)
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                        }
                        else if (blnJutogaiUmu == true)
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                        }
                        else
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                            // *履歴番号 000023 2005/12/16 修正終了
                        }
                        // *履歴番号 000020 2005/12/07 修正終了

                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        // さきほど取得した宛名履歴付随行の履歴番号を宛名履歴行の履歴番号で上書き
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        // 宛名履歴標準
                        csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        // 宛名履歴付随標準
                        csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);

                        // インサート
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }
                    }
                }
                // *履歴番号 000016 2005/11/01 修正開始

                // * 履歴番号 000044 2011/11/09 追加開始
                else
                {
                    if (csAtenaRirekiFzyTokushuRow is null && blnAfterSekobi)
                    {
                        // 宛名履歴付随特殊が存在しない且つ施行日以降の時、宛名付随から作成
                        csAtenaRirekiFzyTokushuRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow;
                        csAtenaRirekiFzyTokushuRow = SetAtenaRirekiFzy(csAtenaRirekiFzyTokushuRow, csAtenaFzyRow);
                        // 履歴番号・更新日時を直近宛名履歴付随より取得
                        csAtenaRirekiFzyTokushuRow(ABAtenaRirekiEntity.RIREKINO) = csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        csAtenaRirekiFzyTokushuRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = csUpRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                        // インサート
                        m_cAtenaRirekiFzyB.InsertAtenaFZYRB(csAtenaRirekiFzyTokushuRow);
                    }
                    else
                    {
                        // 何もしない
                    }
                    if (csAtenaRirekiFzyHyojunTokushuRow is null && blnAfterSekobi)
                    {
                        // 宛名履歴付随標準特殊が存在しない且つ施行日以降の時、宛名付随標準から作成
                        csAtenaRirekiFzyHyojunTokushuRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow;
                        csAtenaRirekiFzyHyojunTokushuRow = SetAtenaRirekiFZYHyojun(csAtenaRirekiFzyHyojunTokushuRow, csAtenaFzyHyojunRow);
                        // 履歴番号・更新日時を直近宛名履歴付随より取得
                        csAtenaRirekiFzyHyojunTokushuRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        csAtenaRirekiFzyHyojunTokushuRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csUpRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                        // インサート
                        m_cABAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaRirekiFzyHyojunTokushuRow);
                    }
                    else
                    {
                        // 何もしない
                    }
                    // * 履歴番号 000044 2011/11/09 追加終了
                }
                // *履歴番号 000013 2005/06/19 修正終了



                // ---------------------------------------------------------------------------------------
                // 9. 宛名累積マスタの更新　（後）
                // 特殊修正（03、04）の場合は更新データが異なる。
                // ---------------------------------------------------------------------------------------
                // **
                // * 宛名累積（後）
                // *
                // 宛名累積の列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）　			
                // 宛名累積より新しいRowを取得する
                csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone();
                csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow;
                // 宛名履歴を初期化する
                ClearAtenaRuiseki(ref csAtenaRuisekiRow);

                // 宛名履歴マスタより宛名累積マスタの編集を行う(共通)
                // 処理日時=システム日時
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji;

                // 前後区分=2
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "2";

                // * 履歴番号 000044 2011/11/09 追加開始
                // 宛名累積付随行を作成
                csAtenaRuisekiFzyRow = m_csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow;
                ClearAtenaFZY(csAtenaRuisekiFzyRow);
                // 処理日時と前後区分は宛名累積から取得
                csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);
                // * 履歴番号 000044 2011/11/09 追加終了

                // 宛名累積標準
                csAtenaRuisekiHyojunRow = m_csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow;
                ClearAtenaHyojun(csAtenaRuisekiHyojunRow);
                // 処理日時と前後区分は宛名累積から取得
                csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);

                // 宛名累積付随標準
                csAtenaRuisekiFZYHyojunRow = m_csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow;
                ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow);
                // 処理日時と前後区分は宛名累積から取得
                csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiFZYHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);

                // *履歴番号 000013 2005/06/19 修正開始
                // それ以外の項目については、宛名マスタをそのまま編集する			
                // 宛名履歴を宛名履歴へそのまま編集する
                // *履歴番号 000026 2005/12/18 修正開始
                // 処理事由コードが"03"(特殊処理修正)　または　"04"(住民票ＣＤ修正)の場合は
                // 別のロウを累積(後)に反映させる
                // 'For Each csDataColumn In csAtenaRirekiRow.Table.Columns
                // '    csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
                // 'Next csDataColumn
                // *履歴番号 000042 2009/08/10 修正開始
                // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" Then
                // * 履歴番号 000050 2014/06/25 修正開始
                // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                // (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                // * 履歴番号 000058 2015/10/14 修正開始
                // If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                // CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse _
                // (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                if (blnIsCreateAtenaRireki == false && (Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "03" || Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "04" || Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "05" || m_blnRirekiShusei == false && Conversions.ToString(csJukiDataRow(ABJukiData.SHORIJIYUCD)) == "08"))
                {
                    // * 履歴番号 000058 2015/10/14 修正終了
                    // * 履歴番号 000050 2014/06/25 修正終了

                    foreach (DataColumn currentCsDataColumn5 in csUpRirekiRow.Table.Columns)
                    {
                        csDataColumn = currentCsDataColumn5;
                        csAtenaRuisekiRow[csDataColumn.ColumnName] = csUpRirekiRow[csDataColumn];
                    }

                    // 宛名累積標準
                    csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunTokushuRow, csAtenaRuisekiRow);

                    // * 履歴番号 000044 2011/11/09 追加開始
                    if (blnAfterSekobi)
                    {
                        // 施行日以降の時、宛名累積付随特殊から全項目コピー
                        csAtenaRuisekiFzyRow = SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyTokushuRow, csAtenaRuisekiRow);

                        // 宛名累積付随標準
                        csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFzyHyojunTokushuRow, csAtenaRuisekiRow);
                    }
                    else
                    {
                        // 施行日以前は付随はNothingにして追加しない
                        csAtenaRuisekiFzyRow = null;
                        csAtenaRuisekiFZYHyojunRow = null;
                    }
                }
                // * 履歴番号 000044 2011/11/09 追加終了
                else
                {
                    foreach (DataColumn currentCsDataColumn6 in csAtenaRirekiRow.Table.Columns)
                    {
                        csDataColumn = currentCsDataColumn6;
                        csAtenaRuisekiRow[csDataColumn.ColumnName] = csAtenaRirekiRow[csDataColumn];
                    }

                    // * 履歴番号 000044 2011/11/09 追加開始
                    // 宛名履歴付随からコピー
                    csAtenaRuisekiFzyRow = SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow);
                    // * 履歴番号 000044 2011/11/09 追加終了
                    // 宛名累積標準
                    csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow);
                    // 宛名累積付随標準
                    csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow);
                }
                // *履歴番号 000042 2009/08/10 修正終了
                // *履歴番号 000026 2005/12/18 修正終了
                // For Each csDataColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                // csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
                // Next csDataColumn
                // *履歴番号 000013 2005/06/19 修正終了

                // *履歴番号 000014 2005/08/17 追加開始 000029 2006/04/19 修正開始
                // 処理事由ＣＤを宛名累積のRESERCEにセットする
                // * 履歴番号 000058 2015/10/14 修正開始
                // 宛名履歴を作成する（特殊処理の場合に特例として）は、「41：職権修正」を固定でリザーブを登録する
                // csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                if (blnIsCreateAtenaRireki == true)
                {
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00");
                }
                else
                {
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD);
                }
                // * 履歴番号 000058 2015/10/14 修正終了
                // ' 汎用ＣＤを宛名累積のRESERCEにセットする
                // csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.HANYOCD)
                // *履歴番号 000014 2005/08/17 追加終了 000029 2006/04/19 修正終了

                // *履歴番号 000016 2005/11/01 追加開始   000028 2005/12/27 削除開始
                // 処理事由コードを宛名累積のCKINJIYUCDにセットする
                // 'csAtenaRuisekiRow(ABAtenaRuisekiEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                // *履歴番号 000016 2005/11/01 追加終了   000028 2005/12/27 削除終了

                // *履歴番号 000003 2003/11/21 追加開始
                // 宛名年金を取得する
                csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD);
                if (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 宛名累積設定(宛名年金)
                    this.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                }
                // 宛名国保を取得する
                csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD);
                if (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 宛名累積設定(宛名国保)
                    this.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), ref csAtenaRuisekiRow);
                }
                // *履歴番号 000003 2003/11/21 追加終了

                // 宛名累積へ追加する
                csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow);

                // 宛名累積マスタの追加を行う
                // * 履歴番号 000044 2011/11/09 修正開始
                // intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)

                intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow);
                // * 履歴番号 000044 2011/11/09 修正終了
                if (intCount != 1)
                {
                    // エラー定義を取得（既に同一データが存在します。：宛名累積）
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名累積", objErrorStruct.m_strErrorCode);
                }

                // * 履歴番号 000050 2014/06/25 追加開始
                // ---------------------------------------------------------------------------------------
                // x. 共通番号マスタの更新
                // ---------------------------------------------------------------------------------------
                // 共通番号マスタ更新判定
                if (IsUpdateMyNumber(csJukiDataRow) == true)
                {

                    // 共通番号・旧共通番号の取得（分割）
                    a_strMyNumber = GetMyNumber(csJukiDataRow);

                    // 共通番号パラメーターの設定
                    cABMyNumberPrm = SetMyNumber(csJukiDataRow, a_strMyNumber[(int)ABMyNumberType.New]);

                    // 共通番号マスタ更新
                    // Select Case csJukiDataRow.Item(ABJukiData.SHORIJIYUCD).ToString
                    // Case ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00")
                    // ' 【特殊処理】
                    // '* 履歴番号 000057 2015/02/17 修正開始
                    // 'Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, a_strMyNumber(ABMyNumberType.Old), IsJumin(csJukiDataRow))
                    // Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, a_strMyNumber(ABMyNumberType.Old))
                    // '* 履歴番号 000057 2015/02/17 修正終了
                    // Case Else
                    // 【通常処理】
                    // * 履歴番号 000054 2014/12/26 修正開始
                    // Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji)
                    // * 履歴番号 000056 2015/01/28 修正開始
                    // Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, IsJumin(csJukiDataRow))
                    UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji);
                }
                // * 履歴番号 000056 2015/01/28 修正終了
                // * 履歴番号 000054 2014/12/26 修正終了
                // End Select

                else
                {
                    // noop
                }
                // * 履歴番号 000050 2014/06/25 追加終了

                // ---------------------------------------------------------------------------------------
                // 10. 固定資産税システムへの連携
                // 管理情報により連携を制御する。（04.12）
                // ---------------------------------------------------------------------------------------
                // *履歴番号 000006 2004/08/27 修正開始
                // *履歴番号 000009 2005/03/18 修正開始
                // 管理情報の固定連携レコードが存在しない時と、パラメータが“0”の時に固定連携処理を行う
                if (m_strKoteiRenkeiFG is null || m_strKoteiRenkeiFG == "0")
                {
                    // 固定連動クラスがnothingならインスタンス化を行う
                    if (m_cBAAtenaLinkageBClass is null)
                    {
                        // 固定連動クラスのインスタンス化を行う
                        m_cBAAtenaLinkageBClass = new BAAtenaLinkageBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        m_cBAAtenaLinkageIFXClass = new BAAtenaLinkageIFXClass();
                    }
                    // ''''''''' 宛名管理情報Ｂクラスのインスタンス作成
                    // ''''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    // '''''''''  宛名管理情報の種別04識別キー01のデータを全件取得する
                    // ''''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "12")

                    // '''''''''管理情報の固定連携レコードが存在し、パラメータが“１”の時には固定連携処理を行なわない
                    // ''''''''If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
                    // ''''''''     CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then
                    // *履歴番号 000009 2005/03/18 修正終了

                    // *履歴番号 000005 2004/03/09 追加開始
                    // 固定資産税データ渡しを行なう
                    if (!blnJukiUmu)
                    {
                        m_cBAAtenaLinkageIFXClass.ShichosonCD = Conversions.ToString(csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD));
                        m_cBAAtenaLinkageIFXClass.JuminCD = Conversions.ToString(csAtenaRow(ABAtenaEntity.JUMINCD));
                        // *履歴番号 000010 2005/04/04 修正開始
                        // m_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                        if (new string(Conversions.ToString(csAtenaRow(ABAtenaEntity.CKINIDOYMD)).Trim ?? new char[0]) == "")
                        {
                            m_cBAAtenaLinkageIFXClass.IdoYMD = "00000000";
                        }
                        else
                        {
                            m_cBAAtenaLinkageIFXClass.IdoYMD = Conversions.ToString(csAtenaRow(ABAtenaEntity.CKINIDOYMD));
                        }
                        // *履歴番号 000010 2005/04/04 修正終了
                        // *履歴番号 000007 2004/10/20 修正開始
                        m_cBAAtenaLinkageIFXClass.KjnHjnKB = Conversions.ToString(csAtenaRow(ABAtenaEntity.KJNHJNKB));
                        // '''cBAAtenaLinkageIFXClass.KjnHjnKB = CType(csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB), String)
                        // *履歴番号 000007 2004/10/20 修正終了
                        BlnRcd = m_cBAAtenaLinkageBClass.BAAtenaLinkage(m_cBAAtenaLinkageIFXClass);
                        if (BlnRcd == false)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            // エラー定義を取得（該当データは処理できません。：固定資産税）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001046);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "固定資産税", objErrorStruct.m_strErrorCode);
                        }
                    }
                    else
                    {
                        // *履歴番号 000008 2005/02/15     追加開始
                        m_cBAAtenaLinkageIFXClass.ShichosonCD = Conversions.ToString(csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD));
                        m_cBAAtenaLinkageIFXClass.JuminCD = Conversions.ToString(csAtenaRow(ABAtenaEntity.JUMINCD));
                        // *履歴番号 000010 2005/04/04 修正開始
                        // m_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                        if (new string(Conversions.ToString(csAtenaRow(ABAtenaEntity.CKINIDOYMD)).Trim ?? new char[0]) == "")
                        {
                            m_cBAAtenaLinkageIFXClass.IdoYMD = "00000000";
                        }
                        else
                        {
                            m_cBAAtenaLinkageIFXClass.IdoYMD = Conversions.ToString(csAtenaRow(ABAtenaEntity.CKINIDOYMD));
                        }
                        // *履歴番号 000010 2005/04/04 修正終了
                        m_cBAAtenaLinkageIFXClass.KjnHjnKB = Conversions.ToString(csAtenaRow(ABAtenaEntity.KJNHJNKB));
                        BlnRcd = m_cBAAtenaLinkageBClass.BAAtenaLinkage_IR(m_cBAAtenaLinkageIFXClass);
                        if (BlnRcd == false)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            // エラー定義を取得（該当データは処理できません。：固定資産税）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001046);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "固定資産税", objErrorStruct.m_strErrorCode);
                        }
                        // *履歴番号 000008 2005/02/15     追加終了
                    }
                    // *履歴番号 000005 2004/03/09 追加終了

                }
                // *履歴番号 000006 2004/08/27 修正終了


                // *履歴番号 000004 2004/02/16 追加開始   000009 2005/02/28 削除開始
                // **
                // * ワークフロー処理(パラメータ格納)
                // *
                // '''''''''' 宛名管理情報Ｂクラスのインスタンス作成
                // ''''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                // '''''''  宛名管理情報の種別04識別キー01のデータを全件取得する
                // ''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

                // '''''''管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
                // ''''''If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                // ''''''    If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then

                // ''''''        '住登外FLGが"1"でなく汎用区分が"02","10","11","12","14","15"で履歴終了年月日が"99999999"（直近データ）の場合
                // ''''''        If Not (blnJutogaiUmu) And _
                // ''''''            (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "02" Or _
                // ''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "10" Or _
                // ''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "11" Or _
                // ''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "12" Or _
                // ''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "14" Or _
                // ''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "15") And _
                // ''''''            CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then
                // ''''''            'インスタンス化
                // ''''''            m_ABToshoProperty(m_intCnt) = New ABToshoProperty()
                // ''''''            '住民コードをプロパティにセット
                // ''''''            m_ABToshoProperty(m_intCnt).p_strJuminCD = strJuminCD
                // ''''''            '更新区分をプロパティにセット（追加:1 修正:2 削除:D）
                // ''''''            m_ABToshoProperty(m_intCnt).p_strKoshinKB = "1"
                // ''''''            'カウンターに1プラス
                // ''''''            m_intCnt += 1

                // ''''''        ElseIf CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then
                // ''''''            'インスタンス化
                // ''''''            m_ABToshoProperty(m_intCnt) = New ABToshoProperty()
                // ''''''            '住民コードをプロパティにセット
                // ''''''            m_ABToshoProperty(m_intCnt).p_strJuminCD = strJuminCD
                // ''''''            '更新区分をプロパティにセット（追加:1 修正:2 削除:D）
                // ''''''            m_ABToshoProperty(m_intCnt).p_strKoshinKB = "2"
                // ''''''            'カウンターに1プラス
                // ''''''            m_intCnt += 1

                // ''''''        End If

                // ''''''    End If
                // ''''''End If
                // *履歴番号 000004 2004/02/16 追加終了   000009 2005/02/28 削除終了
                // *履歴番号 000065 2024/04/02 追加開始
                // 個人制御の更新
                UpdateKojinSeigyo(csJukiDataRow);
                // *履歴番号 000065 2024/04/02 追加終了

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
        }

        // ************************************************************************************************
        // * メソッド名     住基データ更新（履歴）
        // * 
        // * 構文           Public Sub JukiDataKoshin08() 
        // * 
        // * 機能 　    　　住基履歴データを更新する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void JukiDataKoshin08(DataRow csJukiDataRow)
        {
            const string THIS_METHOD_NAME = "JukiDataKoshin08";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // *履歴番号 000040 2009/05/22 削除開始
                                                          // Dim blnJutogaiUmu As Boolean                        ' 住登外有無FLG
                                                          // *履歴番号 000040 2009/05/22 削除終了
            bool blnJukiUmu;                           // 住基有無FLG
            string strJuminCD;                            // 住民コード
                                                          // *履歴番号 000040 2009/05/22 削除開始
                                                          // Dim csJutogaiEntity As DataSet                      ' 住登外DataSet
                                                          // *履歴番号 000040 2009/05/22 削除終了
            ABAtenaSearchKey cSearchKey;                  // 宛名検索キー
            DataSet csAtenaEntity;                        // 宛名マスタEntity
            DataRow csAtenaRow;                           // 宛名マスタRow
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim csDataRow As DataRow                            ' ＤａｔａＲｏｗ
                                                          // Dim csDataSet As DataSet                            ' ＤａｔａＳｅｔ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
            DataColumn csDataColumn;                      // ＤａｔａＣｏｌｕｍｎ
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim csAtenaRirekiEntity As DataSet                  ' 宛名履歴DataSet
                                                          // Dim csAtenaRirekiRows() As DataRow                  ' 宛名履歴Rows
                                                          // * corresponds to VS2008 End 2010/04/16 000043
            DataRow csAtenaRirekiRow;                     // 宛名履歴Row
            int intCount;                             // 更新件数
                                                      // * corresponds to VS2008 Start 2010/04/16 000043
                                                      // Dim csAtenaRuisekiEntity As DataSet                 ' 宛名累積DataSet
                                                      // Dim csAtenaRuisekiRow As DataRow                    ' 宛名累積Row
                                                      // * corresponds to VS2008 End 2010/04/16 000043
                                                      // *履歴番号 000003 2003/11/21 追加開始
                                                      // * corresponds to VS2008 Start 2010/04/16 000043
                                                      // Dim csAtenaNenkinEntity As DataSet                  ' 宛名年金DataSet
                                                      // Dim csAtenaKokuhoEntity As DataSet                  ' 宛名国保DataSet
                                                      // * corresponds to VS2008 End 2010/04/16 000043
                                                      // *履歴番号 000003 2003/11/21 追加終了
                                                      // * corresponds to VS2008 Start 2010/04/16 000043
                                                      // Dim StrShoriNichiji As String
                                                      // * corresponds to VS2008 End 2010/04/16 000043
                                                      // *履歴番号 000016 2005/11/01 追加開始
            int intYMD;
            int intIdx;
            // *履歴番号 000016 2005/11/01 追加終了
            // *履歴番号 000031 2007/01/30 追加開始
            string[] strBanchiCD;                         // 番地コード取得用配列
                                                          // * corresponds to VS2008 Start 2010/04/16 000043
                                                          // Dim strMotoBanchiCD() As String                     ' 変更前番地コード
                                                          // Dim intLoop As Integer                              ' ループカウンタ
                                                          // * corresponds to VS2008 End 2010/04/16 000043
                                                          // *履歴番号 000031 2007/01/30 追加終了
                                                          // *履歴番号 000036 2007/09/28 追加開始
            ABHenshuSearchShimeiBClass cHenshuSearchKana; // 検索用カナ生成クラス
            var strSearchKana = new string[5];                      // 検索用カナ名称用
                                                                    // *履歴番号 000036 2007/09/28 追加終了
                                                                    // * 履歴番号 000044 2011/11/09 追加開始
            DataSet csAtenaFzyEntity;                     // 宛名付随データ
            DataRow csAtenaFzyRow;                        // 宛名付随行
            DataRow csAtenaRirekiFzyRow;                  // 宛名履歴付随行
            DataRow csAtenaRirekiFzyJugaiRow;             // 宛名履歴付随行（住登外）
                                                          // * 履歴番号 000044 2011/11/09 追加終了
            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // ---------------------------------------------------------------------------------------
                // 1. 変数の初期化
                // 
                // ---------------------------------------------------------------------------------------
                // 変数の初期化
                // *履歴番号 000040 2009/05/22 削除開始
                // blnJutogaiUmu = False           '住登外データが存在している場合はTrue
                // *履歴番号 000040 2009/05/22 削除終了
                blnJukiUmu = false;              // 住基データが存在している場合はTrue
                strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString;    // 対象データの住民コードを取得

                // *履歴番号 000036 2007/09/28 追加開始
                // 検索用カナ生成クラスインスタンス化
                cHenshuSearchKana = new ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass);
                // *履歴番号 000036 2007/09/28 追加終了



                // ---------------------------------------------------------------------------------------
                // 2. 住登外データの存在チェック
                // 直近の住登外データが存在しているか住登外マスタから取得する。
                // ---------------------------------------------------------------------------------------
                // *履歴番号 000040 2009/05/22 削除開始
                // ' 住民コードで住登外マスタを取得する（存在する場合は、住登外有りＦＬＧに”1”をセット）
                // csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, True)
                // If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0) Then
                // blnJutogaiUmu = True
                // End If
                // *履歴番号 000040 2009/05/22 削除終了

                // 住民種別の下１桁が”0”（住民）でかつ住登外有りＦＬＧが”1”の時
                // ・住登外データを削除する
                // ・住登外優先で指定年月日”99999999”で宛名マスタを取得し、そのデータを削除する
                // If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "0") _
                // And blnJutogaiUmu) Then
                // For Each csDataRow In csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows
                // m_cJutogaiB.DeleteJutogaiB(csDataRow, "D")
                // Next csDataRow
                // cSearchKey = New ABAtenaSearchKey()
                // cSearchKey.p_strJuminCD = strJuminCD
                // cSearchKey.p_strJutogaiYusenKB = "1"
                // csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, True)
                // For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
                // m_cAtenaB.DeleteAtenaB(csDataRow, "D")
                // Next csDataRow
                // End If



                // ---------------------------------------------------------------------------------------
                // 3. 住基データの存在チェック
                // 直近の住基データが存在しているか宛名マスタから取得する。
                // ---------------------------------------------------------------------------------------
                // 住基優先で宛名マスタを取得する（存在する場合は、住基有りＦＬＧに”1”をセット）
                // 宛名検索キーのインスタンス化
                cSearchKey = new ABAtenaSearchKey();
                cSearchKey.p_strJuminCD = strJuminCD;
                cSearchKey.p_strJuminYuseniKB = "1";
                csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, true);
                if (csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0)
                {
                    blnJukiUmu = true;
                    // * 履歴番号 000044 2011/11/09 追加開始
                    // 宛名付随データ取得（住民住登外区分は宛名から取得）
                    csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, true);
                }
                else
                {
                    // 宛名付随データ取得（住民住登外区分はString.Empty）
                    csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, string.Empty, true);
                    // * 履歴番号 000044 2011/11/09 追加終了
                }



                // ---------------------------------------------------------------------------------------
                // 4. データの編集
                // 直近の住基データが存在している場合は修正、していなければ追加となる。
                // 住基レイアウトから宛名レイアウトにする。
                // ---------------------------------------------------------------------------------------
                // 宛名マスタ

                // 宛名マスタの列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）
                if (blnJukiUmu)
                {
                    csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow;
                    ClearAtena(ref csAtenaRow);
                }

                // 住基データより宛名マスタの編集を行う（ＡＬＬ．ＮＵＬＬ又は、ＡＬＬスペースの時は、String.Emptyにして）
                foreach (DataColumn currentCsDataColumn in csJukiDataRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn;
                    if (csJukiDataRow[csDataColumn] is DBNull || string.IsNullOrEmpty(Conversions.ToString(csJukiDataRow[csDataColumn]).Trim()))
                    {
                        csJukiDataRow[csDataColumn] = string.Empty;
                    }
                }

                // 住基データの同一項目を宛名マスタの項目にセットする
                // ・住民コード
                csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD);
                // ・市町村コード
                csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD);
                // ・旧市町村コード
                csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD);

                // 何もセットしない項目
                // ・住民票コード
                // ・汎用区分２
                // ・漢字法人形態
                // ・漢字法人代表者氏名
                // ・家屋敷区分
                // ・備考税目

                // 編集してセットする項目
                // ・住民住登外区分   1
                csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1";
                // ・住民優先区分     1
                csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1";
                // ・住登外優先区分
                // 住民種別の下１桁が”0”（住民）でなく、且つ住登外有りＦＬＧが”1”の時、　0
                // *履歴番号 000040 2009/05/22 修正開始
                // とりあえず無条件に "1" としてセットする
                csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1";
                // If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) <> "0") _
                // And blnJutogaiUmu) Then
                // csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "0"
                // Else
                // '   　上記以外       1
                // csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"
                // End If
                // *履歴番号 000040 2009/05/22 修正終了
                // ・宛名データ区分=(11)
                csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11";
                // ・世帯コード～整理番号
                csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD);
                // csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
                csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO);
                // ・宛名データ種別=(住民種別)
                csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU);
                // ・汎用区分１=(写し区分)
                csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB);
                // ・個人法人区分=(1)
                csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1";
                // ・汎用区分２
                // csAtenaRow(ABAtenaEntity.HANYOKB2) = String.Empty

                // *履歴番号 000037 2008/05/12 削除開始
                // * corresponds to VS2008 Start 2010/04/16 000043
                // ''' ・管内管外区分
                // ''' 　　住民種別の下１桁が”8”（転出者）の場合、　　2
                // * corresponds to VS2008 End 2010/04/16 000043
                // 'If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "8") Then
                // '    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                // 'Else
                // '    ' 住民種別の下１桁が”8”（転出者）でない場合、1			
                // '    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
                // 'End If
                // *履歴番号 000037 2008/05/12 削除終了

                // *履歴番号 000068 2024/07/05 追加開始
                if (new string(Conversions.ToString(csJukiDataRow(ABJukiData.HONGOKUMEI)).Trim ?? new char[0]) != "" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim ?? new char[0]) != "" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJITSUSHOMEI)).Trim ?? new char[0]) == "")
                {
                    // 本国名≠空白 かつ 併記名≠空白 かつ 通称名＝空白の場合
                    // 漢字名称２・カナ名称２に空白を設定
                    csJukiDataRow(ABJukiData.KANJIMEISHO2) = string.Empty;
                    csJukiDataRow(ABJukiData.KANAMEISHO2) = string.Empty;
                }
                else
                {
                }
                // *履歴番号 000068 2024/07/05 追加終了

                // *履歴番号 000036 2007/09/28 修正開始
                // ・カナ名称１～検索用カナ名
                if (Conversions.ToString(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim == "2" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim ?? new char[0]) != "")
                {
                    // 本名優先(本名と通称名を持つ外国人かつ氏名利用区分が"2")
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.KANJIMEISHO2);

                    // 履歴番号 000039 2009/05/12 修正開始
                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), string.Empty, m_cFrnHommyoKensakuType);
                    // strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                    // String.Empty, cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                    // 履歴番号 000039 2009/05/12 修正終了

                    // 通称名を漢字法人代表者氏名に格納
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }

                // *履歴番号 000039 2009/05/12 修正開始
                else if (m_cFrnHommyoKensakuType == FrnHommyoKensakuType.Tsusho_Seishiki)
                {
                    // ElseIf (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                    // *履歴番号 000039 2009/05/12 修正終了

                    // 通称名優先(本名優先の条件以外の場合)
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);

                    // *履歴番号 000039 2009/05/12 修正開始
                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO1)), Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), m_cFrnHommyoKensakuType);
                    // strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)), _
                    // CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                    // cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                    // *履歴番号 000039 2009/05/12 修正終了

                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }
                else
                {
                    // 通称名優先（既存ユーザ）
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI);
                }
                // ' ・カナ名称１～検索用カナ名
                // csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                // csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                // csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                // csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                // 'csAtenaRow(ABAtenaEntity.KANJIHJNKEITAI) = String.Empty
                // 'csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                // csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                // '*履歴番号 000034 2007/08/31 修正開始
                // If (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                // '外国人本名検索機能が"2(Tsusho_Seishiki)"のとき英字は大文字にする
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANASEIMEI), String).ToUpper()
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = GetSearchKana(CType(csJukiDataRow(ABJukiData.KANAMEISHO2), String))
                // csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANAMEI), String).ToUpper()
                // Else
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                // csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                // csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
                // End If
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                // 'csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
                // '*履歴番号 000034 2007/08/31 修正終了
                // *履歴番号 000036 2007/09/28 修正終了
                csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI);

                // ・住基履歴番号=(履歴番号)
                csAtenaRow(ABAtenaEntity.JUKIRRKNO) = Conversions.ToString(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4);
                // ・履歴開始年月日～住民票表示順
                csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD);
                csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD);
                csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD);
                csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD);
                csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD);
                csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU);
                csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO);
                csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN);
                // ・第２住民票表示順
                csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2);
                // ・続柄コード・続柄・第2続柄コード・第2続柄
                // 住民種別の下１桁が”8”（転出者）の場合で続柄が”01”（世帯主）の場合、管理情報のコードに変更し、			
                // 名称はクリアする
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) == "8")
                {
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd == "01")
                    {
                        if (m_strZokugara1Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init;
                        }
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    }
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd == "01")
                    {
                        if (m_strZokugara2Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init;
                        }
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                    }
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）でない場合は、そのままセット			
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                }
                // ・世帯主住民コード～カナ第２世帯主名
                csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD);
                csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2);
                csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2);

                // ・郵便番号～方書
                // ・転出確定住所がある場合は、転出確定欄からセット（ない項目はセットなし）
                if (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    // *履歴番号 000031 2007/01/30 修正開始
                    // 番地情報から番地コードを取得
                    // *履歴番号 000038 2009/04/07 修正開始
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)));
                    // strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)), strMotoBanchiCD, True)
                    // ' 取得した番地コード配列にNothingの項目がある場合はString.Emptyをセットする
                    // For intLoop = 0 To strBanchiCD.Length - 1
                    // If (IsNothing(strBanchiCD(intLoop))) Then
                    // strBanchiCD(intLoop) = String.Empty
                    // End If
                    // Next
                    // *履歴番号 000038 2009/04/07 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    // csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                    // *履歴番号 000031 2007/01/30 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管外にセット    ※コメント:転出確定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }
                // *履歴番号 000037 2008/05/12 追加終了

                else if (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    // ・転出確定住所が無く、転出予定住所がある場合は、転出予定欄からセット（ない項目はセットなし）
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    // 番地情報から番地コードを取得
                    // *履歴番号 000038 2009/04/07 修正開始
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)));
                    // strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)), strMotoBanchiCD, True)
                    // ' 取得した番地コード配列にNothingの項目がある場合はString.Emptyをセットする
                    // For intLoop = 0 To strBanchiCD.Length - 1
                    // If (IsNothing(strBanchiCD(intLoop))) Then
                    // strBanchiCD(intLoop) = String.Empty
                    // End If
                    // Next
                    // *履歴番号 000038 2009/04/07 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    // csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                    // csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                    // *履歴番号 000031 2007/01/30 修正終了
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管外にセット    ※コメント:転出予定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }
                // *履歴番号 000037 2008/05/12 追加終了

                else
                {
                    // ・両方も無い場合は、住基住所欄からセット
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                    // *履歴番号 000001 2003/09/11 修正開始
                    // csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.JUKIJUSHOCD)).RPadLeft(13);
                    // *履歴番号 000001 2003/09/11 修正終了
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);

                    // *履歴番号 000037 2008/05/12 追加開始
                    // 管内管外区分：管内にセット    ※コメント:転出確定住所、転出予定住所が存在しない場合は管内に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1";
                    // *履歴番号 000037 2008/05/12 追加終了

                }
                // ・連絡先１～改正年月日
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1);
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = csJukiDataRow(ABJukiData.HON_ZJUSHOCD)
                csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.HON_ZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO);
                csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI);
                csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA);
                csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD);
                csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD);
                csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU);
                csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD);
                csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD);
                csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU);
                csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD)
                csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI);
                // * 履歴番号 000063 2024/02/06 修正開始
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                // '*履歴番号 000001 2003/09/11 修正開始
                // 'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
                // '*履歴番号 000001 2003/09/11 修正終了
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

                // 住基データ.処理事由コード＝45（転入通知受理）の場合
                if (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() == ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"))
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                }
                else
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);
                }
                // * 履歴番号 000063 2024/02/06 修正終了
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                // csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG);
                csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD);
                csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO);
                csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG);
                csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO);
                csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG);
                csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG);
                csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD);

                // ・行政区コード～地区名３
                // 住民種別の下１桁が”8”（転出者）でない場合、住基行政区～住基地区名３をセット			
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) != "8")
                {
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    // *履歴番号 000002 2003/09/18 修正開始
                    // csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    // csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                    // *履歴番号 000002 2003/09/18 修正終了
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）の場合、管理情報（行政区初期化～地区３）を見て、
                    // クリアになっている場合は、セットしない
                    if (m_strGyosekuInit.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = string.Empty;
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                    }
                    // *履歴番号 000021 2005/12/12 修正開始
                    // 'csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                    // 'csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    else if (string.IsNullOrEmpty(m_strTenshutsuGyoseikuCD.Trim()))
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されていない場合は
                        // そのまま住基側のデータを設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    }
                    else
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されている場合は
                        // 行政区ＣＤマスタより行政区名称を取得し、設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, ' ');
                        // *履歴番号 000022 2005/12/15 修正開始
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                        // csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = m_strTenshutsuGyoseikuMei
                        // *履歴番号 000022 2005/12/15 修正終了
                        // *履歴番号 000021 2005/12/12 修正終了
                    }
                    if (m_strChiku1Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    }
                    if (m_strChiku2Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = string.Empty;
                    }
                    else
                    {
                        // *履歴番号 000002 2003/09/18 修正開始
                        // csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                        // csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                        // *履歴番号 000002 2003/09/18 修正終了
                    }
                    if (m_strChiku3Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                    }
                }

                // ・投票区コード～在留終了年月日
                csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD);
                csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD);
                csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD);
                csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB);
                csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB);
                csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB);
                csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                // *履歴番号 000001 2003/09/11 修正開始
                csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD);
                // csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).PadLeft(11)
                // *履歴番号 000001 2003/09/11 修正終了
                csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                // csAtenaRow(ABAtenaEntity.KAOKUSHIKIKB) = String.Empty
                // csAtenaRow(ABAtenaEntity.BIKOZEIMOKU) = String.Empty
                csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD);
                csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK);
                csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD);

                // *履歴番号 000003 2003/11/21 追加開始
                // 宛名履歴マスタの住民住登外区分が１（住民）で履歴番号が一番大きいものを取得
                // cSearchKey = New ABAtenaSearchKey()
                // cSearchKey.p_strJuminCD = strJuminCD
                // csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", True)
                // StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")
                // ' データが存在する場合は、

                // *履歴番号 000003 2003/11/21 追加終了

                // * 履歴番号 000044 2011/11/09 追加開始
                if (blnJukiUmu && csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 宛名付随のデータが存在する場合、0行目を取得
                    csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0);
                }
                else
                {
                    // 上記以外の時、空の行を作成
                    csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow;
                    ClearAtenaFZY(csAtenaFzyRow);
                }

                // 宛名付随行の編集
                csAtenaFzyRow = SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow);
                // * 履歴番号 000044 2011/11/09 追加終了

                // ---------------------------------------------------------------------------------------
                // 5. 宛名履歴マスタの更新
                // 住登外データが存在している場合は開始・終了年月日と住登外優先区分を編集する。
                // ---------------------------------------------------------------------------------------
                // **
                // * 宛名履歴
                // *
                // ・住基有りＦＬＧが”1”の時は、住基優先で指定年月日に99999999で宛名履歴マスタをよみ履歴終了年月日をシステ、
                // ム日付の前日をセットし、宛名履歴マスタ更新を実行する
                // If (blnJukiUmu) Then
                // ' 日付クラスの必要な設定を行う
                // m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                // m_cfDateClass.p_enEraType = UFEraType.Number
                // cSearchKey = New ABAtenaSearchKey()
                // cSearchKey.p_strJuminCD = strJuminCD
                // cSearchKey.p_strJuminYuseniKB = "1"
                // csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
                // If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                // csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
                // 'm_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") 'システム日付
                // 'csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                // ' 宛名マスタを宛名履歴へそのまま編集する
                // For Each csDataColumn In csAtenaRow.Table.Columns
                // csDataRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                // Next csDataColumn
                // m_cfDateClass.p_strDateValue = CType(csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD), String)
                // csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                // intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                // If (intCount <> 1) Then
                // ' エラー定義を取得（該当データは他で更新されてしまいました。再度･･･：宛名履歴）
                // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                // End If
                // End If
                // Else

                // *履歴番号 000016 2005/11/01 修正開始
                // *コメント**********************************************************
                // 住登外が起きているデータに関してはそれを考慮してやらないと、     *
                // 正しく履歴マスタは作られない。修正前は一切考慮されていないので、 *
                // 住登外が起きている場合は新たに作りこんでやる必要がある。         *
                // *******************************************************************
                // * corresponds to VS2008 Start 2010/04/16 000043
                // '''' 宛名履歴マスタを該当者の全履歴を取得する
                // '''cSearchKey = New ABAtenaSearchKey()
                // '''cSearchKey.p_strJuminCD = strJuminCD
                // '''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                // '''' 宛名履歴の列を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）
                // '''csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
                // '''Me.ClearAtenaRireki(csAtenaRirekiRow)

                // '''' 宛名マスタより宛名履歴マスタの編集を行う(共通)
                // '''' 履歴番号　　　新規のばあいは、0001　　修正の場合は、宛名履歴マスタの最終番号にＡＤＤ　１する
                // '''' それ以外の項目については、宛名マスタをそのまま編集する			
                // '''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                // '''    ' 履歴番号
                // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = "0001"
                // '''Else
                // '''    ' 履歴番号で降順に並び替え
                // '''    csAtenaRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                // '''    ' 履歴番号(先頭行の履歴番号+1)
                // '''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csAtenaRirekiRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                // '''End If
                // '''' 宛名マスタを宛名履歴へそのまま編集する
                // '''For Each csDataColumn In csAtenaRow.Table.Columns
                // '''    csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                // '''Next csDataColumn

                // '''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                // '''m_cfDateClass.p_enEraType = UFEraType.Number

                // '''m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD), String)
                // '''csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                // '''' 宛名履歴マスタの追加を行う
                // '''csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
                // '''intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                // '''If (intCount <> 1) Then
                // '''    ' エラー定義を取得（既に同一データが存在します。：宛名履歴）
                // '''    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // '''    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                // '''    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                // '''End If
                // * corresponds to VS2008 End 2010/04/16 000043

                // ---------------------------------------------------------------------------------------
                // 5-1. 更新用の履歴レコードを作成する。
                // ---------------------------------------------------------------------------------------

                // 宛名履歴の行を取得し、初期化する。（更新カウターは、0、それ以外は、String Empty）（共通）
                csAtenaRirekiRow = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow;
                ClearAtenaRireki(ref csAtenaRirekiRow);

                // 宛名マスタを宛名履歴へそのまま編集する
                foreach (DataColumn currentCsDataColumn1 in csAtenaRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn1;
                    csAtenaRirekiRow[csDataColumn.ColumnName] = csAtenaRow[csDataColumn];
                }

                // * 履歴番号 000044 2011/11/09 追加開始
                // 退避した宛名履歴付随より新規行作成
                csAtenaRirekiFzyRow = m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow;
                ClearAtenaFZY(csAtenaRirekiFzyRow);
                // 宛名付随からデータコピー
                csAtenaRirekiFzyRow = SetAtenaRirekiFzy(csAtenaRirekiFzyRow, csAtenaFzyRow);
                // * 履歴番号 000044 2011/11/09 追加終了
                // ---------------------------------------------------------------------------------------
                // 5-2. 開始・終了年月日の編集準備、終了年月日を一日マイナスにする
                // ---------------------------------------------------------------------------------------

                // 日付クラスの必要な設定をする
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                m_cfDateClass.p_enEraType = UFEraType.Number;

                // 終了年月日を住基側からのデータの一日前を設定する。
                // (住基側は履歴年月日が１レコード目の終了と２レコード目の開始が同一日。宛名は一日ずれる)
                m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD));
                csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);


                // ---------------------------------------------------------------------------------------
                // 5-3. 住基・住登外の履歴データを更新する
                // ---------------------------------------------------------------------------------------

                // 住登外が起きていないデータに関してはそのまま追加
                if (m_blnJutogaiAriFG == false)
                {

                    // ---------------------------------------------------------------------------------------
                    // 5-3-1. 住登外データが存在しないので、住基データをそのまま更新する
                    // ---------------------------------------------------------------------------------------

                    m_intRenbanCnt += 1;
                    // 履歴番号を設定する
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                    // 住登外優先区分を"1"に設定する
                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";

                    // 宛名履歴マスタの追加を行う
                    // * 履歴番号 000044 2011/11/09 修正開始
                    // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    if (this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString) is null)
                    {
                        // Insertする宛名履歴と一致する宛名履歴付随が存在しなければ、Nothingにする
                        csAtenaRirekiFzyRow = null;
                    }
                    else
                    {
                        // 履歴番号の設定
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                    }

                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                    // * 履歴番号 000044 2011/11/09 修正終了
                    if (intCount != 1)
                    {
                        // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                    }
                }
                else
                {

                    // ---------------------------------------------------------------------------------------
                    // 5-3-2. 住登外データが存在しているので、住基・住登外データの編集を行い更新する
                    // ---------------------------------------------------------------------------------------

                    // 住登外が起きているデータに関しては考慮する
                    // 連番用カウントを＋１
                    m_intRenbanCnt += 1;

                    // 追加する住基レコードが住登外を起こすべきレコードかどうかを判定する
                    // *履歴番号 000020 2005/12/07 追加開始
                    // 住基レコードと住登外レコードの開始年月日が同じ場合の処理を追加
                    // *履歴番号 000024 2005/12/17 修正開始
                    // 'If m_blnHenkanFG = False AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) = m_intJutogaiST_YMD AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                    // *履歴番号 000025 2005/12/18 修正開始
                    // 'If m_blnHenkanFG = False AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) = m_intJutogaiST_YMD AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" AndAlso _
                    // '   m_intJutogaiRowCnt > m_intJutogaiInCnt Then

                    if (m_blnHenkanFG == false && Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD)) >= m_intJutogaiST_YMD && Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) != "0" && m_intJutogaiRowCnt > m_intJutogaiInCnt)
                    {
                        // *履歴番号 000025 2005/12/18 修正終了
                        // *履歴番号 000024 2005/12/17 修正終了

                        // ---------------------------------------------------------------------------------------
                        // 5-3-2-1. 住登外が存在している期間で住基データを分割しないケース
                        // 
                        // 住登外データ作成がまだない、　かつ
                        // 退避した住登外データの開始年月日と住基データの開始年月日が同じか、
                        // 住基データの方が未来日である、 かつ
                        // 住民以外、  かつ
                        // 退避した住登外データがまだ残っている　場合は
                        // 
                        // 住基データ（1件）と住登外データ（1件）の計2件を更新する。
                        // ---------------------------------------------------------------------------------------

                        // 開始年月日が小さくて、住基レコードが除票者であるなら、そのレコードから住登外を起こす。
                        // 宛名側で別途レコードを作成して、追加することはしない。
                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');
                        // 住登外優先区分を"0"に設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                        // 履歴番号を宛名履歴よりコピー
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                        // * 履歴番号 000044 2011/11/09 修正終了
                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 連番用カウントを＋１
                        m_intRenbanCnt += 1;

                        // * 履歴番号 000044 2011/11/09 追加開始
                        // 退避した履歴付随データから初回住登外レコードに一致するデータを取得
                        csAtenaRirekiFzyJugaiRow = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                        // * 履歴番号 000049 2012/04/06 削除開始
                        // If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        // '空でない時は履歴番号を上書き
                        // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                        // Else
                        // '何もしない
                        // End If
                        // * 履歴番号 000049 2012/04/06 削除終了
                        // * 履歴番号 000044 2011/11/09 追加終了

                        // 履歴番号を設定する
                        this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');
                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow)
                        // * 履歴番号 000049 2012/04/06 修正開始
                        // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                        if (csAtenaRirekiFzyJugaiRow is not null)
                        {
                            // 空でない時は履歴番号を上書き
                            csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO);
                        }
                        else
                        {
                            // 何もしない
                        }
                        // * 履歴番号 000049 2012/04/06 修正終了
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow, csAtenaRirekiFzyJugaiRow);
                        // * 履歴番号 000044 2011/11/09 修正終了
                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }
                        // 住登外データカウントを＋１
                        m_intJutogaiInCnt += 1;
                        // 次の住登外ＲＯＷを取得する
                        if (m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1)
                        {
                            m_csFirstJutogaiRow = m_csJutogaiRows[m_intJutogaiInCnt];
                            m_intJutogaiST_YMD = Conversions.ToInteger(this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD));
                        }
                        // 住登外を起こしたかどうかのフラグをＴｒｕｅにする
                        m_blnHenkanFG = true;
                    }
                    // *履歴番号 000020 2005/12/07 追加終了
                    // *履歴番号 000024 2005/12/17 修正開始
                    // 'ElseIf m_blnHenkanFG = False AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) <= m_intJutogaiST_YMD AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer) > m_intJutogaiST_YMD AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                    // *履歴番号 000040 2009/05/22 修正開始
                    else if (m_blnHenkanFG == false && Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD)) < m_intJutogaiST_YMD && Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD)) >= m_intJutogaiST_YMD && Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) != "0" && m_intJutogaiRowCnt > m_intJutogaiInCnt)
                    {
                        // ElseIf m_blnHenkanFG = False AndAlso _
                        // CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) <= m_intJutogaiST_YMD AndAlso _
                        // CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer) > m_intJutogaiST_YMD AndAlso _
                        // CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" AndAlso _
                        // m_intJutogaiRowCnt > m_intJutogaiInCnt Then
                        // *履歴番号 000040 2009/05/22 修正終了

                        // ---------------------------------------------------------------------------------------
                        // 5-3-2-2. 住登外が存在している期間で住基データを分割するケース
                        // 
                        // 住登外データ作成がまだない、　かつ
                        // 退避した住登外データの開始年月日と住基データの開始年月日が同じか、
                        // 住基データの方が過去日である、 かつ
                        // 退避した住登外データの開始年月日より住基データの終了年月日の方が未来日である、 かつ
                        // 住民以外、  かつ　退避した住登外データがまだ残っている　場合は
                        // 
                        // 住基データ（2件）と住登外データ（1件）の計3件を更新する。
                        // ---------------------------------------------------------------------------------------

                        // *履歴番号 000024 2005/12/17 修正終了
                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // 住登外優先区分を"1"に設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";

                        // 終了年月日を最初の住登外ＲＯＷの開始年月日の一日前に設定する
                        intYMD = Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD));   // 退避する
                        m_cfDateClass.p_strDateValue = Conversions.ToString(this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD));
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);

                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        // 宛名履歴の履歴番号を宛名履歴付随に設定
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                        // * 履歴番号 000044 2011/11/09 修正終了
                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 連番用カウントを＋１
                        m_intRenbanCnt += 1;

                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // 住登外優先区分を"0"に設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";

                        // 開始年月日を最初の住登外ＲＯＷの開始年月日に設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD);

                        // 終了年月日を住登外を起こす前のレコードの終了年月日に設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = intYMD.ToString();

                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        // 宛名履歴の履歴番号を宛名履歴付随に設定
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                        // * 履歴番号 000044 2011/11/09 修正終了
                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 連番用カウントを＋１
                        m_intRenbanCnt += 1;

                        // * 履歴番号 000044 2011/11/09 追加開始
                        // 退避した履歴付随データから初回住登外レコードに一致するデータを取得
                        csAtenaRirekiFzyJugaiRow = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString);
                        // * 履歴番号 000049 2012/04/06 削除開始
                        // If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        // '空でない時は履歴番号を上書き
                        // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                        // Else
                        // '何もしない
                        // End If
                        // * 履歴番号 000049 2012/04/06 削除終了
                        // * 履歴番号 000044 2011/11/09 追加終了


                        // 履歴番号を設定する
                        this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow)
                        // * 履歴番号 000049 2012/04/06 修正開始
                        // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                        if (csAtenaRirekiFzyJugaiRow is not null)
                        {
                            // 空でない時は履歴番号を上書き
                            csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO);
                        }
                        else
                        {
                            // 何もしない
                        }
                        // * 履歴番号 000049 2012/04/06 修正終了
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow, csAtenaRirekiFzyJugaiRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 住登外データカウントを＋１
                        m_intJutogaiInCnt += 1;

                        // 次の住登外ＲＯＷを取得する
                        if (m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1)
                        {
                            m_csFirstJutogaiRow = m_csJutogaiRows[m_intJutogaiInCnt];
                            m_intJutogaiST_YMD = Conversions.ToInteger(this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD));
                        }

                        // 住登外を起こしたかどうかのフラグをＴｒｕｅにする
                        m_blnHenkanFG = true;
                    }
                    // *履歴番号 000018 2005/11/27 削除開始
                    // ' 再転入フラグをＦａｌｓｅにする
                    // 'm_blnSaiTenyuFG = False
                    // *履歴番号 000018 2005/11/27 削除終了

                    // *履歴番号 000040 2009/05/22 修正開始
                    // *履歴番号 000024 2005/12/17 修正開始
                    // 'ElseIf m_intJutogaiRowCnt > m_intJutogaiInCnt AndAlso _
                    // '   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) > m_intJutogaiST_YMD Then
                    // ElseIf m_intJutogaiRowCnt > m_intJutogaiInCnt AndAlso _
                    // CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) >= m_intJutogaiST_YMD AndAlso _
                    // CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                    // *履歴番号 000024 2005/12/17 修正終了
                    else if (m_intJutogaiRowCnt > m_intJutogaiInCnt && Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD)) >= m_intJutogaiST_YMD)
                    {
                        // *履歴番号 000040 2009/05/22 修正終了

                        // ---------------------------------------------------------------------------------------
                        // 5-3-2-3. 住登外が存在している期間で住基データを分割しないケース
                        // （２レコード目以降の住登外データセット時）
                        // 
                        // 退避した住登外データがまだ残っている、　かつ
                        // 退避した住登外データの開始年月日と住基データの開始年月日が同じか、
                        // 住基データの方が未来日である、 かつ
                        // 
                        // 住民以外　の場合
                        // ---------------------------------------------------------------------------------------
                        // ** コメント ***************************************************************************
                        // 住基データの全履歴が全て住民、かつ住登外の履歴が混在するケース（通常ありえないが）が発生する可能性がある。
                        // デグレートの危険が大きいことと、発生頻度もかなり少ないのでこの考慮は行わないこととする。
                        // ***************************************************************************************

                        // * 履歴番号 000048 2012/01/05 修正開始
                        // '* 履歴番号 000044 2011/11/09 追加開始
                        // '退避した履歴付随データから初回住登外レコードに一致するデータを取得
                        // csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                        // m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                        // m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        // If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        // '空でない時は履歴番号を上書き
                        // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                        // Else
                        // '何もしない
                        // End If
                        // '* 履歴番号 000044 2011/11/09 追加終了
                        // 退避した履歴付随データから初回住登外レコードに一致するデータを取得
                        csAtenaRirekiFzyJugaiRow = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO).ToString);
                        // * 履歴番号 000048 2012/01/05 修正終了

                        // 履歴番号を設定する
                        m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // *履歴番号 000023 2005/12/16 追加開始
                        // 住基のレコードが再転入レコードの時でかつ住登外のレコードが直近レコードの場合
                        // 終了年月日を住基レコードの開始年月日の一日前にセットする
                        if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) == "0" && Conversions.ToString(m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RRKED_YMD)) == "99999999")
                        {
                            m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD));
                            m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);
                        }

                        // * 履歴番号 000048 2012/01/05 追加開始
                        if (csAtenaRirekiFzyJugaiRow is not null)
                        {
                            // 空でない時は履歴番号を上書き
                            csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO);
                        }
                        else
                        {
                            // 何もしない
                        }
                        // * 履歴番号 000048 2012/01/05 追加終了

                        // *履歴番号 000023 2005/12/16 追加終了
                        // 宛名履歴マスタの追加を行う(住登外ＲＯＷ)
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt))

                        intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows[m_intJutogaiInCnt], csAtenaRirekiFzyJugaiRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 住登外データカウントを＋１
                        m_intJutogaiInCnt += 1;

                        // 住登外レコードが最後のひとつになるまで繰り返すと思われるが、何のためにやっているか分からない（よしざわ）
                        var loopTo = m_intJutogaiRowCnt - 1;
                        for (intIdx = m_intJutogaiInCnt; intIdx <= loopTo; intIdx++)
                        {

                            // *履歴番号 000040 2009/05/22 削除開始
                            // すぐ下でセットし直しているけど、これって意味あんの？消すことにする（よしざわ）
                            // m_intJutogaiST_YMD = CType(m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                            // *履歴番号 000040 2009/05/22 削除終了

                            // 次の住登外ＲＯＷを取得
                            m_csFirstJutogaiRow = m_csJutogaiRows[m_intJutogaiInCnt];
                            m_intJutogaiST_YMD = Conversions.ToInteger(this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD));

                            // *履歴番号 000024 2005/12/17 修正開始
                            // 'If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) > m_intJutogaiST_YMD Then
                            // 住登外データの開始年月日と住基データの開始年月日が同じか、未来日の場合は住登外を更新する
                            if (Conversions.ToInteger(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD)) >= m_intJutogaiST_YMD)
                            {
                                // *履歴番号 000024 2005/12/17 修正終了
                                // 連番用カウントを＋１
                                m_intRenbanCnt += 1;
                                // * 履歴番号 000048 2012/01/05 修正開始
                                // '* 履歴番号 000044 2011/11/09 追加開始
                                // '退避した履歴付随データから初回住登外レコードに一致するデータを取得
                                // csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                                // m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                                // m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                                // If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                                // '空でない時は履歴番号を上書き
                                // csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                                // Else
                                // '何もしない
                                // End If
                                // '* 履歴番号 000044 2011/11/09 追加終了
                                // 退避した履歴付随データから初回住登外レコードに一致するデータを取得
                                csAtenaRirekiFzyJugaiRow = this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.JUMINCD).ToString, m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO).ToString);

                                // * 履歴番号 000048 2012/01/05 修正終了

                                // 履歴番号を設定する
                                m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                                // *履歴番号 000023 2005/12/16 追加開始
                                // 住基のレコードが再転入レコードの時でかつ住登外のレコードが直近レコードの場合
                                // 終了年月日を住基レコードの開始年月日の一日前にセットする
                                if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RRemove(0, 1) == "0" && Conversions.ToString(m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RRKED_YMD)) == "99999999")
                                {

                                    m_cfDateClass.p_strDateValue = Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD));
                                    m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);

                                }
                                // *履歴番号 000023 2005/12/16 追加終了

                                // * 履歴番号 000048 2012/01/05 追加開始
                                if (csAtenaRirekiFzyJugaiRow is not null)
                                {
                                    // 空でない時は履歴番号を上書き
                                    csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csJutogaiRows[m_intJutogaiInCnt](ABAtenaRirekiEntity.RIREKINO);
                                }
                                else
                                {
                                    // 何もしない
                                }
                                // * 履歴番号 000048 2012/01/05 追加終了

                                // 宛名履歴マスタの追加を行う(住登外ＲＯＷ)
                                // * 履歴番号 000044 2011/11/09 修正開始
                                // intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt))

                                intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows[m_intJutogaiInCnt], csAtenaRirekiFzyJugaiRow);
                                // * 履歴番号 000044 2011/11/09 修正終了

                                if (intCount != 1)
                                {
                                    // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                }

                                // 住登外データカウントを＋１
                                m_intJutogaiInCnt += 1;
                            }
                            else
                            {
                                // *履歴番号 000040 2009/05/22 削除開始
                                // 前の住登外ＲＯＷを取得
                                // 前レコードの開始年月日を取得しても使用されてないけど、これって意味あんの？消すことにする（よしざわ）
                                // m_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt - 1)
                                // m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                                // *履歴番号 000040 2009/05/22 削除終了
                                break;
                            }
                        }

                        // 連番用カウントを＋１
                        m_intRenbanCnt += 1;

                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // 住民種別が住民なら住登外を起こしたかどうかのフラグをFalseにする
                        if (Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RSubstring(1, 1) == "0")
                        {
                            m_blnHenkanFG = false;
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                        }
                        // *履歴番号 000018 2005/11/27 削除開始
                        // 再転入フラグをTrueにする
                        // m_blnSaiTenyuFG = True
                        // *履歴番号 000018 2005/11/27 削除終了
                        else if (m_blnHenkanFG == false)
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                        }
                        else
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                        }

                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        csAtenaRirekiFzyRow(ABAtenaRirekiEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }

                        // 次の住登外ＲＯＷを取得する
                        if (m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1)
                        {
                            m_csFirstJutogaiRow = m_csJutogaiRows[m_intJutogaiInCnt];
                            m_intJutogaiST_YMD = Conversions.ToInteger(this.m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD));
                        }
                    }
                    else
                    {

                        // ---------------------------------------------------------------------------------------
                        // 5-3-2-4. どれにも当てはまらない場合
                        // ---------------------------------------------------------------------------------------

                        // 履歴番号を設定する
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = m_intRenbanCnt.ToString().RPadLeft(4, '0');

                        // 住登外が起きていて　かつ　種別が住民でなければ住登外優先区分は"0"
                        if (m_blnHenkanFG == true && Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RSubstring(1, 1) != "0")
                        {
                            // 住登外優先区分は"0"
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0";
                        }
                        else if (m_blnHenkanFG == true && Conversions.ToString(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU)).RPadLeft(2, ' ').RSubstring(1, 1) == "0")
                        {
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                            // *履歴番号 000018 2005/11/27 削除開始
                            // ' 再転入フラグをTrueにする
                            // 'm_blnSaiTenyuFG = True
                            // *履歴番号 000018 2005/11/27 削除終了
                            m_blnHenkanFG = false;
                        }
                        else
                        {
                            // 住登外優先区分は"1"
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1";
                        }

                        // 宛名履歴マスタの追加を行う
                        // * 履歴番号 000044 2011/11/09 修正開始
                        // intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        if (this.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString) is null)
                        {
                            // Insertする宛名履歴と一致する宛名履歴付随が存在しなければ、Nothingにする
                            csAtenaRirekiFzyRow = null;
                        }
                        else
                        {
                            // 履歴番号を宛名履歴より取得
                            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                        }
                        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                        // * 履歴番号 000044 2011/11/09 修正終了

                        if (intCount != 1)
                        {
                            // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                        }
                    }

                }
                // *履歴番号 000016 2005/11/01 修正終了

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

        }
        // ************************************************************************************************
        // * メソッド名     宛名Rowの初期化
        // * 
        // * 構文           Public Sub ClearAtena(ByRef csAtenaRow As DataRow)
        // * 
        // * 機能 　    　　宛名Rowを初期化する
        // * 
        // * 引数           DataRow : AtenaEntity
        // * 
        // * 戻り値         DataRow : AtenaEntity
        // ************************************************************************************************
        private void ClearAtena(ref DataRow csAtenaDataRow)
        {
            const string THIS_METHOD_NAME = "ClearAtena";                      // ＤａｔａＣｏｌｕｍｎ

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csAtenaDataRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaEntity.KOSHINCOUNTER:
                            {
                                csAtenaDataRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csAtenaDataRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
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
        }
        // ************************************************************************************************
        // * メソッド名     宛名履歴Rowの初期化
        // * 
        // * 構文           Public Sub ClearAtenaRireki(ByRef csAtenaRirekiRow As DataRow)
        // * 
        // * 機能 　    　　宛名履歴Rowの初期化
        // * 
        // * 引数           DataRow : AtenaRirekiEntity
        // * 
        // * 戻り値         DataRow : AtenaRirekiEntity
        // ************************************************************************************************
        private void ClearAtenaRireki(ref DataRow csAtenaRirekiRow)
        {
            const string THIS_METHOD_NAME = "ClearAtenaRireki";                      // ＤａｔａＣｏｌｕｍｎ

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csAtenaRirekiRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaRirekiEntity.KOSHINCOUNTER:
                            {
                                csAtenaRirekiRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csAtenaRirekiRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
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
        }
        // ************************************************************************************************
        // * メソッド名     宛名累積Rowの初期化
        // * 
        // * 構文           Public Sub ClearAtenaRuiseki(ByRef csAtenaRuisekiRow As DataRow)
        // * 
        // * 機能 　    　　宛名累積Rowを初期化する
        // * 
        // * 引数           DataRow : AtenaRuisekiEntity
        // * 
        // * 戻り値         DataRow : AtenaRuisekiEntity
        // ************************************************************************************************
        private void ClearAtenaRuiseki(ref DataRow csAtenaRuisekiRow)
        {
            const string THIS_METHOD_NAME = "ClearAtenaRuiseki";                      // ＤａｔａＣｏｌｕｍｎ

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csAtenaRuisekiRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaRuisekiEntity.KOSHINCOUNTER:
                            {
                                csAtenaRuisekiRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csAtenaRuisekiRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
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
        }

        // *履歴番号 000003 2003/11/21 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名累積Rowへ宛名年金を設定
        // * 
        // * 構文           Public Sub SetNenkinToRuiseki(ByVal csAtenaNenkinRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
        // * 
        // * 機能 　    　　宛名累積Rowを初期化する
        // * 
        // * 引数           DataRow : AtenaNenkinEntity
        // * 　　           DataRow : AtenaRuisekiEntity
        // * 
        // * 戻り値         DataRow : AtenaRuisekiEntity
        // ************************************************************************************************
        private void SetNenkinToRuiseki(DataRow csAtenaNenkinRow, ref DataRow csAtenaRuisekiRow)
        {
            // * corresponds to VS2008 Start 2010/04/16 000043
            // Dim csDataColumn As DataColumn                      ' ＤａｔａＣｏｌｕｍｎ
            // * corresponds to VS2008 End 2010/04/16 000043

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KSNENKNNO) = csAtenaNenkinRow(ABAtenaNenkinEntity.KSNENKNNO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKSHU);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSSHTSYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO1);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO1);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU1);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN1);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB1);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO2);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO2);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU2);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN2);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB2);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO3);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO3);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU3);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN3);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB3);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKINHIHOKENSHAGAITOKB) = string.Empty;
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHUBETSUHENKOYMD) = string.Empty;

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

        // ************************************************************************************************
        // * メソッド名     宛名累積Rowへ宛名国保を設定
        // * 
        // * 構文           Public Sub SetKokuhoToRuiseki(ByVal csAtenaKokuhoRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
        // * 
        // * 機能 　    　　宛名累積Rowを初期化する
        // * 
        // * 引数           DataRow : csAtenaKokuhoEntity
        // * 　　           DataRow : AtenaRuisekiEntity
        // * 
        // * 戻り値         DataRow : AtenaRuisekiEntity
        // ************************************************************************************************
        private void SetKokuhoToRuiseki(DataRow csAtenaKokuhoRow, ref DataRow csAtenaRuisekiRow)
        {
            // * corresponds to VS2008 Start 2010/04/16 000043
            // Dim csDataColumn As DataColumn                      ' ＤａｔａＣｏｌｕｍｎ
            // * corresponds to VS2008 End 2010/04/16 000043

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHONO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHONO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKB);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHUTOKUYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSOSHITSUYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKB);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKGAITOYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHIGAITOYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOHOKENSHONO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO);
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKHOHIHOKENSHAGAITOKB) = string.Empty;

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
        // *履歴番号 000003 2003/11/21 追加終了

        // *履歴番号 000009 2005/02/28     追加開始
        // ************************************************************************************************
        // * メソッド名     住基レプリカデータ更新
        // * 
        // * 構文           Public Sub JukiDataReplicaKoshin(ByVal csJukiDataEntity As DataSet)
        // * 
        // * 機能 　    　　住基レプリカデータの更新処理を行なう
        // * 
        // * 引数           DataSet(csJukiDataEntity) : 住基データセット
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void JukiDataReplicaKoshin(DataSet csJukiDataEntity)
        {
            const string THIS_METHOD_NAME = "JukiDataReplicaKoshin";
            // *履歴番号 000009 2005/03/18 削除開始
            // '''''Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
            // '''''Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
            // *履歴番号 000009 2005/03/18 削除終了
            var csABToshoPrmEntity = new DataSet();               // レプリカ作成用パラメータデータセット
            DataTable csABToshoPrmTable;                  // レプリカ作成用パラメータデータテーブル
            DataRow csABToshoPrmRow;                      // レプリカ作成用パラメータデータテーブル
                                                          // 住基データRow
            bool blnJutogaiUmu = false;                // 住登外有無FLG
            DataSet csJutogaiEntity;                      // 住登外DataSet
            string strJuminCD;                            // 住民コード
            ABAtenaCnvBClass cABAtenaCnvBClass;
            const string WORK_FLOW_NAME = "宛名異動";             // ワークフロー名
            const string DATA_NAME = "宛名";                      // データ名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // 管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
                if (m_strR3RenkeiFG is not null && m_strR3RenkeiFG == "1")
                {

                    // データセット取得クラスのインスタンス化
                    cABAtenaCnvBClass = new ABAtenaCnvBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // テーブルセットの取得
                    csABToshoPrmTable = cABAtenaCnvBClass.CreateColumnsToshoPrmData();
                    csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME;
                    // データセットにテーブルセットの追加
                    csABToshoPrmEntity.Tables.Add(csABToshoPrmTable);

                    // データ分繰り返す
                    foreach (DataRow csJukiDataRow in csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows)
                    {
                        if (Conversions.ToString(csJukiDataRow(ABJukiData.RRKED_YMD)) == "99999999")
                        {

                            // 住民ＣＤの取得
                            strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString;

                            // 住民コードで住登外マスタを取得する（存在する場合は、住登外有りＦＬＧに”1”をセット）
                            csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, true);
                            if (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0)
                            {
                                blnJutogaiUmu = true;
                            }

                            // 住登外FLGが"1"でなく汎用区分が"02","10","11","12","14","15"で履歴終了年月日が"99999999"（直近データ）の場合
                            if (!blnJutogaiUmu & (Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "02" | Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "10" | Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "11" | Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "12" | Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "14" | Conversions.ToString(csJukiDataRow(ABJukiData.HANYOCD)) == "15") & Conversions.ToString(csJukiDataRow(ABJukiData.RRKED_YMD)) == "99999999")
                            {

                                // 新規ロウの作成
                                csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow();
                                // プロパティにセット
                                csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD;                                 // 住民コード
                                csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD).ToString;   // 世帯コード
                                csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = ABConstClass.WF_INSERT_KOSHINKB;           // 更新区分（追加:1 修正:2 削除:D）
                                                                                                                             // データセットにロウを追加する
                                csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow);
                            }

                            else if (Conversions.ToString(csJukiDataRow(ABJukiData.RRKED_YMD)) == "99999999")
                            {

                                // 新規ロウの作成
                                csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow();
                                // プロパティにセット
                                csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD;                                 // 住民コード
                                csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD).ToString;   // 世帯コード
                                csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = ABConstClass.WF_UPDATE_KOSHINKB;           // 更新区分（追加:1 修正:2 削除:D）
                                                                                                                             // データセットにロウを追加する
                                csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow);

                            }
                        }
                    }

                    // レコード件数が"0"出ない時はワークフロー処理を行う
                    if (!(csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Count == 0))
                    {
                        // ワークフロー送信処理呼び出し
                        cABAtenaCnvBClass.WorkFlowExec(csABToshoPrmEntity, WORK_FLOW_NAME, DATA_NAME);
                    }
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
        }
        // *履歴番号 000009 2005/02/28     追加終了

        // * 履歴番号 000055 2015/01/08 削除開始
        // '* 履歴番号 000053 2014/09/10 追加開始
        // ''' <summary>
        // ''' 中間サーバーＢＳデータ更新
        // ''' </summary>
        // ''' <param name="csJukiDataEntity">住基データ</param>
        // ''' <remarks></remarks>
        // Public Sub JukiDataBSKoshin(ByVal csJukiDataEntity As DataSet)

        // Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        // Dim cfErrorClass As UFErrorClass
        // Dim cfErrorStruct As UFErrorStruct
        // Dim csJuminCD As ArrayList
        // Dim cABBSRenkeiB As ABBSRenkeiBClass

        // Try

        // ' デバッグ開始ログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' 引数チェック
        // If (csJukiDataEntity Is Nothing OrElse _
        // csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows.Count = 0) Then
        // cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
        // cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003304)
        // Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode)
        // Else
        // ' noop
        // End If

        // ' 住基データより直近データ（履歴終了日が"99999999"）の住民コードを取得
        // csJuminCD = New ArrayList
        // For Each csDataRow As DataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select( _
        // String.Format("{0} = '99999999'", ABJukiData.RRKED_YMD), _
        // ABJukiData.JUMINCD)
        // csJuminCD.Add(csDataRow.Item(ABJukiData.JUMINCD).ToString)
        // Next csDataRow

        // ' 中間サーバーＢＳ連携ビジネスクラスのインスタンス化
        // cABBSRenkeiB = New ABBSRenkeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        // ' 中間サーバーＢＳ連携の実行
        // cABBSRenkeiB.ExecRenkei(csJuminCD)

        // ' デバッグ終了ログ出力
        // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // Catch cfAppExp As UFAppException

        // ' ワーニングログ出力
        // m_cfLogClass.WarningWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
        // "【ワーニング内容:" + cfAppExp.Message + "】")
        // Throw

        // Catch csExp As Exception

        // ' エラーログ出力
        // m_cfLogClass.ErrorWrite(m_cfControlData, _
        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // "【エラー内容:" + csExp.Message + "】")
        // Throw

        // End Try

        // End Sub
        // * 履歴番号 000053 2014/09/10 追加終了
        // * 履歴番号 000055 2015/01/08 削除終了

        // *履歴番号 000027 2005/12/20 追加開始
        // ************************************************************************************************
        // * メソッド名     インサートする住登外レコードを編集する
        // * 
        // * 構文           Public Function EditJutogaiRows(ByVal csJutogaiRows() As DataRow) As DataRow()
        // * 
        // * 機能 　    　　インサートする住登外レコードを編集する
        // * 
        // * 引数           DataRow(csJutogaiRows()) : 住登外データロウ(複数)
        // * 
        // * 戻り値         DataRow()：編集した住登外データロウ(複数)
        // ************************************************************************************************
        public DataRow[] EditJutogaiRows(DataRow[] csJutogaiRows, string strJukiCkinST_YMD)
        {
            // * corresponds to VS2008 Start 2010/04/16 000043
            // Const THIS_METHOD_NAME As String = "EditJutogaiRows"
            // * corresponds to VS2008 End 2010/04/16 000043
            int intIdx = 0;
            int intNewIdx = 0;
            var csNewJutogaiRow = new DataRow[1];

            var loopTo = csJutogaiRows.Length - 1;
            for (intIdx = 0; intIdx <= loopTo; intIdx++)
            {

                if ((Conversions.ToString(csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKST_YMD)) ?? "") == (m_strGapeiDate ?? "") && ((Conversions.ToString(csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD)) ?? "") == (m_strBefGapeiDate ?? "") || Conversions.ToString(csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD)) == "99999999"))
                {
                }
                // 住登外レコードの開始年月日が合併日　かつ　(終了年月日が合併日一日前　または　"99999999")の場合、
                // この住登外レコードは必要なくなるので何もしない。

                else if ((Conversions.ToString(csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD)) ?? "") == (m_strBefGapeiDate ?? ""))
                {
                    // 住登外レコードの終了年月日が合併日一日前の場合、
                    // この住登外レコードの終了年月日を住基データ直近レコードの開始年月日の一日前を設定する。
                    // 履歴エンティティの新規ロウを取得する
                    Array.Resize(ref csNewJutogaiRow, intNewIdx + 1);
                    csNewJutogaiRow[intNewIdx] = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow;

                    m_cfDateClass.p_strDateValue = strJukiCkinST_YMD;
                    csJutogaiRows[intIdx](ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1);
                    csNewJutogaiRow[intNewIdx] = csJutogaiRows[intIdx];

                    intNewIdx += 1;
                }
                else
                {
                    // それ以外はそのままセット
                    // 履歴エンティティの新規ロウを取得する
                    Array.Resize(ref csNewJutogaiRow, intNewIdx + 1);
                    csNewJutogaiRow[intNewIdx] = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow;

                    csNewJutogaiRow[intNewIdx] = csJutogaiRows[intIdx];

                    intNewIdx += 1;
                }

            }

            return csNewJutogaiRow;

        }
        // *履歴番号 000027 2005/12/20 追加終了

        // *履歴番号 000036 2007/09/28 削除開始
        // '*履歴番号 000034 2007/08/31 追加開始
        // '************************************************************************************************
        // '* メソッド名     検索用カナ取得：外国人本名検索機能
        // '* 
        // '* 構文           Public Function GetSearchKana(ByVal strKanaMeisho As String,) As String
        // '* 
        // '* 機能           検索用カナ名称を編集する
        // '* 
        // '* 引数           strKanaMeisho As String     : カナ名称
        // '* 
        // '* 戻り値         String                      : カナ姓名（清音化，文字数24文字以内）
        // '************************************************************************************************
        // Private Function GetSearchKana(ByVal strKanaMeisho As String) As String
        // Const THIS_METHOD_NAME As String = "GetSearchKana"                      'メソッド名
        // Dim strSearchKana As String                         '検索用カナ
        // Dim cuString As New USStringClass                   '文字列編集
        // Dim intIndex As Integer                             '先頭からの空白位置

        // Try
        // ' デバッグ開始ログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '本名カナ姓名
        // If (strKanaMeisho.Length > 0) Then
        // strSearchKana = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()
        // Else
        // strSearchKana = String.Empty
        // End If

        // '検索カナ姓の桁チェック
        // If strSearchKana.Length > 24 Then
        // strSearchKana = strSearchKana.Substring(0, 24)
        // End If

        // ' デバッグ終了ログ出力
        // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        // Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
        // ' システムエラーをスローする
        // Throw objExp
        // End Try

        // Return strSearchKana

        // End Function
        // '*履歴番号 000034 2007/08/31 追加終了
        // *履歴番号 000036 2007/09/28 削除終了

        // * 履歴番号 000044 2011/11/09 追加開始
        #region 宛名付随初期化
        // ************************************************************************************************
        // * メソッド名     宛名付随系DataRwo初期化処理
        // * 
        // * 構文           Private Sub ClearAtenaFZY(ByVal csFzyRow As DataRow)
        // * 
        // * 機能           宛名付随系DataRowの初期化を行う
        // * 
        // * 引数           csFzyRow As DataRow     : 付随行
        // ************************************************************************************************
        private void ClearAtenaFZY(DataRow csFzyRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csFzyRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaFZYEntity.KOSHINCOUNTER:
                        case var case1 when case1 == ABAtenaFZYEntity.LINKNO:
                            {
                                csFzyRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csFzyRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
        }
        #endregion
        #region 宛名付随データ設定
        // ************************************************************************************************
        // * メソッド名     宛名付随データ設定処理
        // * 
        // * 構文           Private Function SetAtenaFzy(ByVal csAtenaFzyRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        // * 
        // * 機能           宛名付随系DataRowの初期化を行う
        // * 
        // * 引数           csAtenaFzyRow As DataRow     : 宛名付随データ
        // *                csAtenaRow As DataRow        ：宛名データ
        // *                csJukiDataRow As DataRow     ：住基データ
        // *
        // * 戻り値         宛名付随のデータ設定を行う
        // ************************************************************************************************
        private DataRow SetAtenaFzy(DataRow csAtenaFzyRow, DataRow csAtenaRow, DataRow csJukiDataRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // データ編集
                csAtenaFzyRow(ABAtenaFZYEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD);
                csAtenaFzyRow(ABAtenaFZYEntity.SHICHOSONCD) = csAtenaRow(ABAtenaEntity.SHICHOSONCD);
                csAtenaFzyRow(ABAtenaFZYEntity.KYUSHICHOSONCD) = csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD);
                csAtenaFzyRow(ABAtenaFZYEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB);
                csAtenaFzyRow(ABAtenaFZYEntity.TABLEINSERTKB) = csJukiDataRow(ABJukiData.TABLEINSERTKB);
                csAtenaFzyRow(ABAtenaFZYEntity.LINKNO) = csJukiDataRow(ABJukiData.LINKNO);
                csAtenaFzyRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csJukiDataRow(ABJukiData.JUMINHYOJOTAIKBN);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csJukiDataRow(ABJukiData.JUKYOCHITODOKEFLG);
                csAtenaFzyRow(ABAtenaFZYEntity.HONGOKUMEI) = csJukiDataRow(ABJukiData.HONGOKUMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csJukiDataRow(ABJukiData.KANAHONGOKUMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csJukiDataRow(ABJukiData.KANJIHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csJukiDataRow(ABJukiData.KANAHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csJukiDataRow(ABJukiData.KANJITSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csJukiDataRow(ABJukiData.KANATSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csJukiDataRow(ABJukiData.KATAKANAHEIKIMEI);
                // * 履歴番号 000045 2011/11/28 追加開始
                if (csJukiDataRow(ABJukiData.FUSHOUMAREBI).ToString.Trim.RLength > 0)
                {
                    csAtenaFzyRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = ABConstClass.UMAREFUSHOKBN_FUSHO_YMD;
                }
                else
                {
                    csAtenaFzyRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = ABConstClass.UMAREFUSHOKBN_FUSHONASHI;
                }
                // * 履歴番号 000045 2011/11/28 追加終了
                csAtenaFzyRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csJukiDataRow(ABJukiData.TSUSHOMEITOUROKUYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csJukiDataRow(ABJukiData.ZAIRYUKIKANCD);
                csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csJukiDataRow(ABJukiData.ZAIRYUKIKANMEISHO);
                csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csJukiDataRow(ABJukiData.ZAIRYUSHACD);
                csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csJukiDataRow(ABJukiData.ZAIRYUSHAMEISHO);
                csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csJukiDataRow(ABJukiData.ZAIRYUCARDNO);
                csAtenaFzyRow(ABAtenaFZYEntity.KOFUYMD) = csJukiDataRow(ABJukiData.KOFUYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csJukiDataRow(ABJukiData.KOFUYOTEISTYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csJukiDataRow(ABJukiData.KOFUYOTEIEDYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOIDOYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOJIYUCD);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOJIYU);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOTDKDYMD);
                csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOTDKDTUCIKB);
                csAtenaFzyRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csJukiDataRow(ABJukiData.FRNSTAINUSMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csJukiDataRow(ABJukiData.FRNSTAINUSKANAMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.STAINUSHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csJukiDataRow(ABJukiData.STAINUSKANAHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.STAINUSTSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csJukiDataRow(ABJukiData.STAINUSKANATSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI_KYOTSU);
                csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSTSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSHEIKIMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSTSUSHOMEI);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE1) = csJukiDataRow(ABJukiData.FRNRESERVE1);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE2) = csJukiDataRow(ABJukiData.FRNRESERVE2);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE3) = csJukiDataRow(ABJukiData.FRNRESERVE3);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE4) = csJukiDataRow(ABJukiData.FRNRESERVE4);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE5) = csJukiDataRow(ABJukiData.FRNRESERVE5);
                // * 履歴番号 000050 2014/06/25 修正開始
                // csAtenaFzyRow(ABAtenaFZYEntity.RESERVE6) = csJukiDataRow(ABJukiData.JUKIRESERVE1)
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE6) = string.Empty;
                // * 履歴番号 000050 2014/06/25 修正終了
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE7) = csJukiDataRow(ABJukiData.JUKIRESERVE2);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE8) = csJukiDataRow(ABJukiData.JUKIRESERVE3);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE9) = csJukiDataRow(ABJukiData.JUKIRESERVE4);
                csAtenaFzyRow(ABAtenaFZYEntity.RESERVE10) = csJukiDataRow(ABJukiData.JUKIRESERVE5);

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaFzyRow;
        }
        #endregion

        #region 宛名履歴付随データ設定
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随データ設定処理
        // * 
        // * 構文           Private Function SetAtenaRirekiFzy(ByVal csAtenaRirekiFzy As DataRow, ByVal csAtenaFzyRow As DataRow) As DataRow
        // * 
        // * 機能           宛名付随系DataRowの初期化を行う
        // * 
        // * 引数           csAtenaRirekiFzy As DataRow     : 宛名履歴付随データ
        // *                csAtenaFzyRow As DataRow        ：宛名付随データ
        // *
        // * 戻り値         宛名履歴付随のデータ設定を行う
        // ************************************************************************************************
        private DataRow SetAtenaRirekiFzy(DataRow csAtenaRirekiFzy, DataRow csAtenaFzyRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 付随から履歴付随にセット
                foreach (DataColumn csColumn in csAtenaFzyRow.Table.Columns)
                {
                    if (csAtenaRirekiFzy[csColumn.ColumnName] is not null)
                    {
                        // 列があった時だけ設定
                        csAtenaRirekiFzy[csColumn.ColumnName] = csAtenaFzyRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRirekiFzy;
        }
        #endregion
        #region 宛名累積付随データ設定
        // ************************************************************************************************
        // * メソッド名     宛名累積付随データ設定処理
        // * 
        // * 構文           Private Function SetAtenaRirekiFzy(ByVal csAtenaRirekiFzy As DataRow, ByVal csAtenaFzyRow As DataRow) As DataRow
        // * 
        // * 機能           宛名付随系DataRowの初期化を行う
        // * 
        // * 引数           csAtenaRuisekiFzyRow As DataRow     : 宛名累積付随データ
        // *                csAtenaRirekiRow As DataRow        ：宛名履歴データ
        // *                csAtenaRuisekiRow As DataRow       ：宛名累積データ
        // *
        // * 戻り値         宛名履歴付随から宛名累積付随を作る
        // ************************************************************************************************
        private DataRow SetAtenaRuisekiFzy(DataRow csAtenaRuisekiFzyRow, DataRow csAtenaRirekiRow, DataRow csAtenaRuisekiRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 履歴or付随履歴から付随累積にセット
                foreach (DataColumn csColumn in csAtenaRirekiRow.Table.Columns)
                {
                    if (csAtenaRuisekiFzyRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRuisekiFzyRow[csColumn.ColumnName] = csAtenaRirekiRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }

                // 処理日時と前後区分は累積からセット
                csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRuisekiFzyRow;
        }
        #endregion
        #region 宛名履歴付随直近データ取得
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随直近データ取得
        // * 
        // * 構文           Private Function GetChokkin_RirekiFzy(ByVal csAtenaRirekiFzy As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        // * 
        // * 機能           宛名付随系DataRowの初期化を行う
        // * 
        // * 引数           csAtenaRirekiFzy As DataSet     : 宛名履歴付随データ
        // *                strJuminCD As String            ：住民コード
        // *                strRirekiNo As String           ：履歴番号
        // *
        // * 戻り値         宛名履歴付随を引数の条件で検索し、結果の０番目を返す。無い時はNothingを返す
        // ************************************************************************************************
        private DataRow GetChokkin_RirekiFzy(DataSet csAtenaRirekiFzy, string strJuminCD, string strRirekiNo)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataRow[] csSelectedRows; // 検索結果配列
            DataRow csCkinRow;        // 直近行
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (csAtenaRirekiFzy is not null)
                {
                    // 引数宛名履歴付随がNothingでない時
                    csSelectedRows = csAtenaRirekiFzy.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYEntity.JUMINCD, strJuminCD, ABAtenaRirekiFZYEntity.RIREKINO, strRirekiNo));
                    if (csSelectedRows.Count() > 0)
                    {
                        // 直近データが存在した時、０行目を取っておく
                        csCkinRow = csSelectedRows[0];
                    }
                    else
                    {
                        // それ以外の時、Nothingで返す
                        csCkinRow = null;
                    }
                }
                else
                {
                    // Nothingの時はNothingで返す
                    csCkinRow = null;
                }


                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csCkinRow;
        }
        #endregion
        // * 履歴番号 000044 2011/11/09 追加終了

        // * 履歴番号 000050 2014/06/25 追加開始
        #region 共通番号マスタの更新判定

        /// <summary>
    /// 共通番号マスタの更新判定
    /// </summary>
    /// <param name="csDataRow">住基データ</param>
    /// <returns>更新判定結果（True：更新する、False：更新しない）</returns>
    /// <remarks></remarks>
        private bool IsUpdateMyNumber(DataRow csDataRow)
        {

            bool blnResult = false;


            // 処理事由判定
            // * 履歴番号 000054 2014/12/26 修正開始
            // * 履歴番号 000052 2014/09/10 修正開始
            // * 履歴番号 000051 2014/07/08 修正開始
            // Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00")
            // ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
            // ' "05"（個人番号修正）、"48"（個人番号変更請求）、"49"（個人番号職権記載）
            // Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
            // ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
            // ' "43"（転出取消）、"44"（回復）
            // ' "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
            // Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
            // ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
            // ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
            // ' "41"（職権修正）、"43"（転出取消）、"44"（回復）
            // ' "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
            // "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）、"15"（住所設定）
            // "41"（職権修正）、"43"（転出取消）、"44"（回復）、"45"（転入通知受理）
            // "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
            // * 履歴番号 000051 2014/07/08 修正終了
            // * 履歴番号 000052 2014/09/10 修正終了
            // * 履歴番号 000054 2014/12/26 修正終了
            try
            {
                ;
#error Cannot convert SelectBlockSyntax - see comment for details
                /* Cannot convert SelectBlockSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
                   at ICSharpCode.CodeConverter.CSharp.MethodBodyExecutableStatementVisitor.<VisitSelectBlock>d__66.MoveNext()
                --- End of stack trace from previous location where exception was thrown ---
                   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                   at ICSharpCode.CodeConverter.CSharp.PerScopeStateVisitorDecorator.<AddLocalVariablesAsync>d__6.MoveNext()
                --- End of stack trace from previous location where exception was thrown ---
                   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.<DefaultVisitInnerAsync>d__3.MoveNext()

                Input:

                            ' 処理事由判定
                            Select Case csDataRow.Item(ABJukiData.SHORIJIYUCD).ToString
                                '* 履歴番号 000054 2014/12/26 修正開始
                                '* 履歴番号 000052 2014/09/10 修正開始
                                '* 履歴番号 000051 2014/07/08 修正開始
                                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00")
                                '    ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
                                '    ' "05"（個人番号修正）、"48"（個人番号変更請求）、"49"（個人番号職権記載）
                                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                                '    ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
                                '    ' "43"（転出取消）、"44"（回復）
                                '    ' "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
                                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                                '    ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）
                                '    ' "41"（職権修正）、"43"（転出取消）、"44"（回復）
                                '    ' "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
                                Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.JushoSettei.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"),
                                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                                    ' "02"（特殊追加）、"10"（転入）、"11"（出生）、"12"（職権記載）、"15"（住所設定）
                                    ' "41"（職権修正）、"43"（転出取消）、"44"（回復）、"45"（転入通知受理）
                                    ' "05"（個人番号修正）、"06"（個人番号職権記載）、"48"（個人番号変更請求）、"49"（個人番号職権修正）
                                    '* 履歴番号 000051 2014/07/08 修正終了
                                    '* 履歴番号 000052 2014/09/10 修正終了
                                    '* 履歴番号 000054 2014/12/26 修正終了
                                    blnResult = True
                                Case Else
                                    blnResult = False
                            End Select

                 */
            }

            catch (Exception csExp)
            {
                throw;
            }

            return blnResult;

        }

        #endregion

        #region 住民判定

        // * 履歴番号 000057 2015/02/17 削除開始
        // ''' <summary>
        // ''' 住民判定
        // ''' </summary>
        // ''' <param name="csDataRow">住基データ</param>
        // ''' <returns>住民判定結果（True：住民、False：住民以外）</returns>
        // ''' <remarks></remarks>
        // Private Function IsJumin( _
        // ByVal csDataRow As DataRow) As Boolean

        // Dim blnResult As Boolean = False

        // Try

        // ' 住民判定
        // Select Case csDataRow.Item(ABJukiData.JUMINSHU).ToString
        // Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN, _
        // ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
        // ' "10"（日本人住民）、"20"（外国人住民）
        // blnResult = True
        // Case Else
        // blnResult = False
        // End Select

        // Catch csExp As Exception
        // Throw
        // End Try

        // Return blnResult

        // End Function
        // * 履歴番号 000057 2015/02/17 削除終了

        #endregion

        #region 共通番号の取得

        /// <summary>
    /// 共通番号の取得
    /// </summary>
    /// <param name="csDataRow">住基データ</param>
    /// <returns>共通番号（要素0：共通番号、要素1：旧共通番号）</returns>
    /// <remarks></remarks>
        private string[] GetMyNumber(DataRow csDataRow)
        {

            string[] a_strResult = new string[] { string.Empty, string.Empty };
            string[] a_strMyNumber;
            const string SEPARATOR = ",";

            try
            {

                // 住基リザーブ１をカンマで分割する
                a_strMyNumber = csDataRow.Item(ABJukiData.JUKIRESERVE1).ToString.Split(SEPARATOR.ToCharArray());

                // 共通番号
                a_strResult[(int)ABMyNumberType.New] = a_strMyNumber[(int)ABMyNumberType.New];

                // 旧共通番号
                if (a_strMyNumber.Length > 1)
                {
                    a_strResult[(int)ABMyNumberType.Old] = a_strMyNumber[(int)ABMyNumberType.Old];
                }
                else
                {
                    a_strResult[(int)ABMyNumberType.Old] = string.Empty;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return a_strResult;

        }

        #endregion

        #region 共通番号パラメータークラスの設定

        /// <summary>
    /// 共通番号パラメータークラスの設定
    /// </summary>
    /// <param name="csDataRow">住基データ</param>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>共通番号パラメータークラス</returns>
    /// <remarks></remarks>
        private ABMyNumberPrmXClass SetMyNumber(DataRow csDataRow, string strMyNumber)
        {

            ABMyNumberPrmXClass csResult = default;

            try
            {

                csResult = new ABMyNumberPrmXClass();
                csResult.p_strJuminCD = csDataRow.Item(ABJukiData.JUMINCD).ToString;
                csResult.p_strShichosonCD = csDataRow.Item(ABJukiData.SHICHOSONCD).ToString;
                csResult.p_strKyuShichosonCD = csDataRow.Item(ABJukiData.KYUSHICHOSONCD).ToString;
                csResult.p_strMyNumber = strMyNumber;
                csResult.p_strCkinKB = ABMyNumberEntity.DEFAULT.CKINKB.CKIN;
                csResult.p_strIdoKB = ABMyNumberEntity.DEFAULT.IDOKB.JUKIIDO;
                csResult.p_strIdoYMD = m_cfRdbClass.GetSystemDate.ToString("yyyyMMdd");
                csResult.p_strIdoSha = m_cfControlData.m_strUserName;
                csResult.p_strReserve = string.Empty;
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csResult;

        }

        #endregion

        #region 共通番号マスタの更新処理

        // * 履歴番号 000054 2014/12/26 修正開始
        // ''' <summary>
        // ''' 共通番号マスタの更新処理
        // ''' </summary>
        // ''' <param name="cABMyNumberPrm">共通番号パラメータークラス</param>
        // ''' <param name="strShoriNichiji">処理日時</param>
        // ''' <returns>更新件数</returns>
        // ''' <remarks>通常処理に使用します。</remarks>
        // Public Overloads Function UpdateMyNumber( _
        // ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
        // ByVal strShoriNichiji As String) As Integer
        // * 履歴番号 000056 2015/01/28 修正開始
        // ''' <summary>
        // ''' 共通番号マスタの更新処理
        // ''' </summary>
        // ''' <param name="cABMyNumberPrm">共通番号パラメータークラス</param>
        // ''' <param name="strShoriNichiji">処理日時</param>
        // ''' <param name="blnIsJuminFG">住民フラグ</param>
        // ''' <returns>更新件数</returns>
        // ''' <remarks>通常処理に使用します。</remarks>
        // Public Overloads Function UpdateMyNumber( _
        // ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
        // ByVal strShoriNichiji As String, _
        // ByVal blnIsJuminFG As Boolean) As Integer
        /// <summary>
    /// 共通番号マスタの更新処理
    /// </summary>
    /// <param name="cABMyNumberPrm">共通番号パラメータークラス</param>
    /// <param name="strShoriNichiji">処理日時</param>
    /// <returns>更新件数</returns>
    /// <remarks>通常処理に使用します。</remarks>
        public int UpdateMyNumber(ABMyNumberPrmXClass cABMyNumberPrm, string strShoriNichiji)
        {
            // * 履歴番号 000056 2015/01/28 修正終了
            // * 履歴番号 000054 2014/12/26 修正終了

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var intKoshinCount = default(int);
            int intCount;
            UFErrorClass cfErrorClass;
            UFErrorStruct objErrorStruct;
            DataSet csABMyNumberEntity;
            DataSet csABMyNumberRuisekiEntity;
            DataSet csDataSet;
            DataRow csNewRow;
            DataSet csRrkDataSet;
            string strShoriKB;
            DataSet csABMyNumberHyojunEntity;
            DataSet csABMyNumberRuisekiHyojunEntity;
            DataSet csMyNumberDS;
            DataRow csHyojunNewRow;
            DataRow csRuisekiNewRow;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ---------------------------------------------------------------------------------------------------------

                // 共通番号のスキーマを取得
                csABMyNumberEntity = m_cfRdbClass.GetTableSchema(ABMyNumberEntity.TABLE_NAME);

                // 共通番号累積のスキーマを取得
                csABMyNumberRuisekiEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiEntity.TABLE_NAME);

                // 共通番号標準
                csABMyNumberHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberHyojunEntity.TABLE_NAME);
                // 共通番号累積標準
                csABMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiHyojunEntity.TABLE_NAME);

                // ---------------------------------------------------------------------------------------------------------
                // 【共通番号存在有無を判定】

                if (cABMyNumberPrm.p_strMyNumber.Trim.RLength > 0)
                {
                }
                // noop
                else
                {
                    // 共通番号に値が存在しないため、更新件数0にて処理を離脱する。（通常処理では、値なしでの更新は行わない。）
                    return 0;
                }

                // * 履歴番号 000056 2015/01/28 削除開始
                // '* 履歴番号 000054 2014/12/26 追加開始
                // ' ---------------------------------------------------------------------------------------------------------
                // ' 【共通番号マスタのレコード有無を判定】　※除票者に対する更新の考慮

                // ' 住民フラグを判定
                // If (blnIsJuminFG = True) Then
                // ' noop
                // Else

                // ' 既存レコードの取得
                // csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD, String.Empty)

                // If (csDataSet IsNot Nothing _
                // AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then
                // ' 既存レコードが存在するため、更新件数0にて処理を離脱する。（除票者に対しては、新規付番のみ行う。）
                // Return 0
                // Else
                // ' noop
                // End If

                // End If

                // ' ---------------------------------------------------------------------------------------------------------
                // '* 履歴番号 000054 2014/12/26 追加終了
                // * 履歴番号 000056 2015/01/28 削除終了

                // ---------------------------------------------------------------------------------------------------------
                // 【直近の共通番号変更有無を判定】

                // 直近レコードの取得
                csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD);

                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {

                    // 共通番号の変更有無を判定
                    if (cABMyNumberPrm.p_strMyNumber.Trim == csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.MYNUMBER).ToString.Trim)
                    {
                        // 直近の共通番号に変更がないため、更新件数0にて処理を離脱する。
                        return 0;
                    }
                    else
                    {
                        // noop
                    }
                }

                else
                {
                    // noop
                }

                // ---------------------------------------------------------------------------------------------------------
                // 【共通番号の更新】

                // 更新後同一キーレコードの取得
                csDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);

                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {

                    // -----------------------------------------------------------------------------------------------------

                    // 処理区分を設定
                    strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD;

                    // -----------------------------------------------------------------------------------------------------
                    // 【共通番号累積マスタの更新（異動前）】

                    // 共通番号累積DataRowの生成
                    csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);

                    // 共通番号累積追加処理
                    m_cABMyNumberRuisekiB.Insert(csNewRow);

                    // -----------------------------------------------------------------------------------------------------
                    // 【共通番号の更新】

                    {
                        var withBlock = csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0);
                        withBlock.BeginEdit();
                        withBlock(ABMyNumberEntity.CKINKB) = cABMyNumberPrm.p_strCkinKB;
                        withBlock.EndEdit();
                    }
                    intCount = m_cABMyNumberB.Update(csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0));
                    if (intCount != 1)
                    {
                        cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                        throw new UFAppException(string.Concat(objErrorStruct.m_strErrorMessage, "共通番号"), objErrorStruct.m_strErrorCode);
                    }
                    else
                    {
                        // noop
                    }

                    // 返信値へ設定
                    intKoshinCount += intCount;
                }

                // -----------------------------------------------------------------------------------------------------

                else
                {

                    // -----------------------------------------------------------------------------------------------------

                    // 処理区分を設定
                    strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.INS;

                    // -----------------------------------------------------------------------------------------------------
                    // 【共通番号の追加】

                    csNewRow = csABMyNumberEntity.Tables(ABMyNumberEntity.TABLE_NAME).NewRow;
                    {
                        ref var withBlock1 = ref csNewRow;
                        withBlock1.BeginEdit();
                        withBlock1.Item(ABMyNumberEntity.JUMINCD) = cABMyNumberPrm.p_strJuminCD;
                        withBlock1.Item(ABMyNumberEntity.SHICHOSONCD) = cABMyNumberPrm.p_strShichosonCD;
                        withBlock1.Item(ABMyNumberEntity.KYUSHICHOSONCD) = cABMyNumberPrm.p_strKyuShichosonCD;
                        withBlock1.Item(ABMyNumberEntity.MYNUMBER) = cABMyNumberPrm.p_strMyNumber;
                        withBlock1.Item(ABMyNumberEntity.CKINKB) = cABMyNumberPrm.p_strCkinKB;
                        withBlock1.Item(ABMyNumberEntity.IDOKB) = cABMyNumberPrm.p_strIdoKB;
                        withBlock1.Item(ABMyNumberEntity.IDOYMD) = cABMyNumberPrm.p_strIdoYMD;
                        withBlock1.Item(ABMyNumberEntity.IDOSHA) = cABMyNumberPrm.p_strIdoSha;
                        withBlock1.Item(ABMyNumberEntity.RESERVE) = cABMyNumberPrm.p_strReserve;
                        withBlock1.EndEdit();
                    }
                    csABMyNumberEntity.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Add(csNewRow);
                    intKoshinCount += m_cABMyNumberB.Insert(csNewRow);

                    // -----------------------------------------------------------------------------------------------------
                    // 共通番号標準
                    csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);
                    if (csMyNumberDS is not null && csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0)
                    {
                    }
                    else
                    {
                        csHyojunNewRow = CreateMyNumberHyojun(csABMyNumberHyojunEntity, cABMyNumberPrm);
                        m_csABMyNumberHyojunB.Insert(csHyojunNewRow);
                        // 共通番号累積標準
                        csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiHyojunEntity, csHyojunNewRow, cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO);
                        m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow);
                    }
                }

                // ---------------------------------------------------------------------------------------------------------
                // 【共通番号累積マスタの更新（異動後）】

                // 更新後同一キーレコードの取得
                csDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);

                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {

                    // 共通番号累積DataRowの生成
                    csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO);

                    // 共通番号累積追加処理
                    m_cABMyNumberRuisekiB.Insert(csNewRow);
                }

                else
                {
                    // noop
                }

                // ---------------------------------------------------------------------------------------------------------
                // 【処理日時のインクリメント】

                strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();

                // ---------------------------------------------------------------------------------------------------------
                // 【更新後同一キーレコード以外を履歴化】

                // 全履歴レコードの取得
                csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD, string.Empty);

                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows)
                    {

                        // 共通番号値有無判定
                        if (csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength > 0)
                        {

                            // 更新後同一キーレコード判定
                            if (cABMyNumberPrm.p_strMyNumber.Trim == csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString.Trim)
                            {
                            }
                            // noop

                            // 直近判定
                            else if (csDataRow.Item(ABMyNumberEntity.CKINKB).ToString == ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
                            {

                                // -------------------------------------------------------------------------------------

                                // 処理区分を設定
                                strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD;

                                // -------------------------------------------------------------------------------------
                                // 【共通番号累積マスタの更新（異動前）】

                                // 共通番号累積DataRowの生成
                                csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csDataRow, strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);

                                // 共通番号累積追加処理
                                m_cABMyNumberRuisekiB.Insert(csNewRow);

                                // -------------------------------------------------------------------------------------
                                // 【既存レコードの履歴化】

                                csDataRow.BeginEdit();
                                csDataRow.Item(ABMyNumberEntity.CKINKB) = ABMyNumberEntity.DEFAULT.CKINKB.RRK;
                                csDataRow.EndEdit();
                                intCount = m_cABMyNumberB.Update(csDataRow);
                                if (intCount != 1)
                                {
                                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                                    throw new UFAppException(string.Concat(objErrorStruct.m_strErrorMessage, "共通番号"), objErrorStruct.m_strErrorCode);
                                }
                                else
                                {
                                    // noop
                                }

                                // 返信値へ設定
                                intKoshinCount += intCount;

                                // -------------------------------------------------------------------------------------
                                // 【共通番号累積マスタの更新（異動後）】

                                // 履歴化したレコードの取得
                                csRrkDataSet = m_cABMyNumberB.SelectByKey(csDataRow.Item(ABMyNumberEntity.JUMINCD).ToString, csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString);

                                if (csRrkDataSet is not null && csRrkDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                                {

                                    // 共通番号累積DataRowの生成
                                    csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csRrkDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO);

                                    // 共通番号累積追加処理
                                    m_cABMyNumberRuisekiB.Insert(csNewRow);
                                }

                                else
                                {
                                    // noop
                                }

                                // -------------------------------------------------------------------------------------
                                // 【処理日時のインクリメント】

                                strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();
                            }

                            // -------------------------------------------------------------------------------------

                            else
                            {
                                // noop

                                // -----------------------------------------------------------------------------------------

                            }
                        }

                        // ---------------------------------------------------------------------------------------------

                        else
                        {

                            // ---------------------------------------------------------------------------------------------

                            // 処理区分を設定
                            strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.DEL;

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号累積マスタの更新（異動前）】

                            // 共通番号累積DataRowの生成
                            csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csDataRow, strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);

                            // 共通番号累積追加処理
                            m_cABMyNumberRuisekiB.Insert(csNewRow);

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号なしレコードの削除】

                            intCount = m_cABMyNumberB.Delete(csDataRow);
                            if (intCount != 1)
                            {
                                cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                                throw new UFAppException(string.Concat(objErrorStruct.m_strErrorMessage, "共通番号"), objErrorStruct.m_strErrorCode);
                            }
                            else
                            {
                                // noop
                            }

                            // 返信値へ設定
                            intKoshinCount += intCount;

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号累積マスタの更新（異動後）】

                            // 共通番号累積DataRowの生成
                            csNewRow.BeginEdit();
                            csNewRow.Item(ABMyNumberRuisekiEntity.ZENGOKB) = ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO;
                            csNewRow.EndEdit();

                            // 共通番号累積追加処理
                            m_cABMyNumberRuisekiB.Insert(csNewRow);

                            // ---------------------------------------------------------------------------------------------
                            // 【処理日時のインクリメント】

                            strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();

                            // ---------------------------------------------------------------------------------------------

                        }

                        // -------------------------------------------------------------------------------------------------

                    }
                }

                else
                {
                    // noop
                }

                // ---------------------------------------------------------------------------------------------------------

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            return intKoshinCount;

        }

        // * 履歴番号 000057 2015/02/17 修正開始
        // ''' <summary>
        // ''' 共通番号マスタの更新処理
        // ''' </summary>
        // ''' <param name="cABMyNumberPrm">共通番号パラメータークラス</param>
        // ''' <param name="strShoriNichiji">処理日時</param>
        // ''' <param name="strOldMyNumber">旧共通番号</param>
        // ''' <param name="blnIsJuminFG">住民フラグ</param>
        // ''' <returns>更新件数</returns>
        // ''' <remarks>特殊処理に使用します。</remarks>
        // Public Overloads Function UpdateMyNumber( _
        // ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
        // ByVal strShoriNichiji As String, _
        // ByVal strOldMyNumber As String, _
        // ByVal blnIsJuminFG As Boolean) As Integer
        /// <summary>
    /// 共通番号マスタの更新処理
    /// </summary>
    /// <param name="cABMyNumberPrm">共通番号パラメータークラス</param>
    /// <param name="strShoriNichiji">処理日時</param>
    /// <param name="strOldMyNumber">旧共通番号</param>
    /// <returns>更新件数</returns>
    /// <remarks>特殊処理に使用します。</remarks>
        public int UpdateMyNumber(ABMyNumberPrmXClass cABMyNumberPrm, string strShoriNichiji, string strOldMyNumber)
        {
            // * 履歴番号 000057 2015/02/17 修正終了

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var intKoshinCount = default(int);
            int intCount;
            UFErrorClass cfErrorClass;
            UFErrorStruct objErrorStruct;
            DataSet csABMyNumberEntity;
            DataSet csABMyNumberRuisekiEntity;
            DataRow csNewRow;
            DataSet csMaeDataSet;
            DataSet csAtoDataSet;
            string strShoriKB;
            DataSet csABMyNumberHyojunEntity;
            DataSet csABMyNumberRuisekiHyojunEntity;
            DataSet csMyNumberDS;
            DataRow csHyojunNewRow;
            DataRow csRuisekiNewRow;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ---------------------------------------------------------------------------------------------------------

                // 共通番号のスキーマを取得
                csABMyNumberEntity = m_cfRdbClass.GetTableSchema(ABMyNumberEntity.TABLE_NAME);

                // 共通番号累積のスキーマを取得
                csABMyNumberRuisekiEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiEntity.TABLE_NAME);

                // 共通番号標準
                csABMyNumberHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberHyojunEntity.TABLE_NAME);
                // 共通番号累積標準
                csABMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiHyojunEntity.TABLE_NAME);
                // ---------------------------------------------------------------------------------------------------------
                // 【共通番号の変更有無を判定】

                if (cABMyNumberPrm.p_strMyNumber.Trim == strOldMyNumber.Trim())
                {
                    // 共通番号に変更がないため、更新件数0にて処理を離脱する。
                    return 0;
                }
                else
                {
                    // noop
                }

                // ---------------------------------------------------------------------------------------------------------
                // 【更新前同一キーレコードの存在有無を判定】

                // 更新前同一キーレコードを取得
                csMaeDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, strOldMyNumber);

                if (csMaeDataSet is not null && csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {

                    // -----------------------------------------------------------------------------------------------------

                    // 処理区分を設定
                    strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.DEL;

                    // -----------------------------------------------------------------------------------------------------
                    // 【共通番号累積マスタの更新（異動前）】

                    // 共通番号累積DataRowの生成
                    csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);

                    // 共通番号累積追加処理
                    m_cABMyNumberRuisekiB.Insert(csNewRow);

                    // -----------------------------------------------------------------------------------------------------
                    // 【更新前同一キーレコードの削除】

                    intCount = m_cABMyNumberB.Delete(csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0));
                    if (intCount != 1)
                    {
                        cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                        throw new UFAppException(string.Concat(objErrorStruct.m_strErrorMessage, "共通番号"), objErrorStruct.m_strErrorCode);
                    }
                    else
                    {
                        // noop
                    }

                    // 返信値へ設定
                    intKoshinCount += intCount;

                    // -----------------------------------------------------------------------------------------------------
                    // 【共通番号累積マスタの更新（異動後）】

                    // 共通番号累積DataRowの生成
                    csNewRow.BeginEdit();
                    csNewRow.Item(ABMyNumberRuisekiEntity.ZENGOKB) = ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO;
                    csNewRow.EndEdit();

                    // 共通番号累積追加処理
                    m_cABMyNumberRuisekiB.Insert(csNewRow);

                    // 共通番号標準削除
                    csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, strOldMyNumber);
                    if (csMyNumberDS is not null && csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0)
                    {
                        // 共通番号累積標準-前追加
                        csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiEntity, csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows(0), cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);
                        m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow);
                        // 共通番号標準削除
                        m_csABMyNumberHyojunB.Delete(csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows(0));
                        // 共通番号累積標準-後追加
                        csRuisekiNewRow.BeginEdit();
                        csRuisekiNewRow.Item(ABMyNumberRuisekiHyojunEntity.ZENGOKB) = ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO;
                        csRuisekiNewRow.EndEdit();
                        m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow);
                    }
                    else
                    {
                    }

                    // -----------------------------------------------------------------------------------------------------
                    // 【処理日時のインクリメント】

                    strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();

                    // -----------------------------------------------------------------------------------------------------

                    // 更新後同一キーレコードを取得
                    csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);

                    if (csAtoDataSet is not null && csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                    {

                        // -------------------------------------------------------------------------------------------------
                        // 【更新前同一キーレコードの直近区分と更新後同一キーレコードの直近区分を判定】

                        if (csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.CKINKB).ToString == ABMyNumberEntity.DEFAULT.CKINKB.CKIN && csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.CKINKB).ToString == ABMyNumberEntity.DEFAULT.CKINKB.RRK)
                        {
                            // 更新前同一キーレコードの直近区分が"1"、かつ更新後同一キーレコードの直近区分が"0"の場合

                            // ---------------------------------------------------------------------------------------------

                            // 処理区分を設定
                            strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD;

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号累積マスタの更新（異動前）】

                            // 共通番号累積DataRowの生成
                            csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE);

                            // 共通番号累積追加処理
                            m_cABMyNumberRuisekiB.Insert(csNewRow);

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号の更新】

                            {
                                var withBlock = csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0);
                                withBlock.BeginEdit();
                                withBlock(ABMyNumberEntity.CKINKB) = ABMyNumberEntity.DEFAULT.CKINKB.CKIN;
                                withBlock.EndEdit();
                            }
                            intCount = m_cABMyNumberB.Update(csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0));
                            if (intCount != 1)
                            {
                                cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                                throw new UFAppException(string.Concat(objErrorStruct.m_strErrorMessage, "共通番号"), objErrorStruct.m_strErrorCode);
                            }
                            else
                            {
                                // noop
                            }

                            // 返信値へ設定
                            intKoshinCount += intCount;

                            // ---------------------------------------------------------------------------------------------
                            // 【共通番号累積マスタの更新（異動後）】

                            // 更新後同一キーレコードの取得
                            csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);

                            if (csAtoDataSet is not null && csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                            {

                                // 共通番号累積DataRowの生成
                                csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO);

                                // 共通番号累積追加処理
                                m_cABMyNumberRuisekiB.Insert(csNewRow);
                            }

                            else
                            {
                                // noop
                            }

                            // ---------------------------------------------------------------------------------------------
                            // 【処理日時のインクリメント】

                            strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();
                        }

                        // ---------------------------------------------------------------------------------------------

                        else
                        {
                            // noop
                        }
                    }

                    // -------------------------------------------------------------------------------------------------

                    else
                    {

                        // -------------------------------------------------------------------------------------------------

                        // 処理区分を設定
                        strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.INS;

                        // -------------------------------------------------------------------------------------------------
                        // 【共通番号の追加】

                        // 更新処理だが、DELETE/INSERTで処理させるので必要項目のみ上書きとする。
                        // ※主キーの更新を伴う、UPDATEを行わないようにするため。（異動累積への配慮を含む。）
                        {
                            var withBlock1 = csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0);
                            withBlock1.BeginEdit();
                            withBlock1(ABMyNumberEntity.MYNUMBER) = cABMyNumberPrm.p_strMyNumber;
                            withBlock1(ABMyNumberEntity.IDOKB) = cABMyNumberPrm.p_strIdoKB;
                            withBlock1(ABMyNumberEntity.IDOYMD) = cABMyNumberPrm.p_strIdoYMD;
                            withBlock1(ABMyNumberEntity.IDOSHA) = cABMyNumberPrm.p_strIdoSha;
                            withBlock1.EndEdit();
                        }
                        intKoshinCount += m_cABMyNumberB.Insert(csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0));

                        // -------------------------------------------------------------------------------------------------
                        // 【共通番号累積マスタの更新（異動後）】

                        // 更新後同一キーレコードの取得
                        csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);

                        if (csAtoDataSet is not null && csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                        {

                            // 共通番号累積DataRowの生成
                            csNewRow = CreateMyNumberRuiseki(csABMyNumberRuisekiEntity, csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0), strShoriNichiji, strShoriKB, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO);

                            // 共通番号累積追加処理
                            m_cABMyNumberRuisekiB.Insert(csNewRow);
                        }

                        else
                        {
                            // noop
                        }

                        // 共通番号標準
                        csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber);
                        if (csMyNumberDS is not null && csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0)
                        {
                        }
                        else
                        {
                            csHyojunNewRow = CreateMyNumberHyojun(csABMyNumberHyojunEntity, cABMyNumberPrm);
                            m_csABMyNumberHyojunB.Insert(csHyojunNewRow);
                            // 共通番号累積標準
                            csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiEntity, csHyojunNewRow, cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO);
                            m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow);
                        }
                        // -------------------------------------------------------------------------------------------------
                        // 【処理日時のインクリメント】

                        strShoriNichiji = (Conversions.ToLong(strShoriNichiji) + 1000L).ToString();

                        // -------------------------------------------------------------------------------------------------

                    }
                }
                // -----------------------------------------------------------------------------------------------------

                else
                {

                    // -----------------------------------------------------------------------------------------------------

                    // * 履歴番号 000057 2015/02/17 修正開始
                    // 住民判定を行わないこととする。
                    // 除票者に対する修正の場合に、
                    // 本来直近とすべきでない番号が直近となる可能性があるが、
                    // 正しい更新であったか確認していただく運用を徹底することとする。
                    // ' 住民フラグを判定
                    // If (blnIsJuminFG = True) Then
                    // ' 住民の場合は、通常処理として処理させる。
                    // '* 履歴番号 000054 2014/12/26 修正開始
                    // 'Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji)
                    // '* 履歴番号 000056 2015/01/28 修正開始
                    // 'Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji, blnIsJuminFG)
                    // Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji)
                    // '* 履歴番号 000056 2015/01/28 修正終了
                    // '* 履歴番号 000054 2014/12/26 修正終了
                    // Else
                    // ' 更新対象レコードが存在しないため、更新件数0にて処理を離脱する。
                    // Return 0
                    // End If
                    return UpdateMyNumber(cABMyNumberPrm, strShoriNichiji);
                    // * 履歴番号 000057 2015/02/17 修正終了

                    // -----------------------------------------------------------------------------------------------------

                }

                // ---------------------------------------------------------------------------------------------------------

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");

                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                // エラーをそのままスローする
                throw;

            }

            return intKoshinCount;

        }

        #endregion

        #region 共通番号累積DataRowの生成

        /// <summary>
    /// 共通番号累積DataRowの生成
    /// </summary>
    /// <param name="csMyNumberRuisekiEntity">共通番号累積マスタ</param>
    /// <param name="csDataRow">対象DataRow</param>
    /// <param name="strShoriNichiji">処理日時</param>
    /// <param name="strShoriKB">処理区分</param>
    /// <param name="strZengoKB">前後区分</param>
    /// <returns>共通番号累積DataRow</returns>
    /// <remarks></remarks>
        private DataRow CreateMyNumberRuiseki(DataSet csMyNumberRuisekiEntity, DataRow csDataRow, string strShoriNichiji, string strShoriKB, string strZengoKB)
        {

            DataRow csNewRow;

            try
            {

                csNewRow = csMyNumberRuisekiEntity.Tables(ABMyNumberRuisekiEntity.TABLE_NAME).NewRow;
                csNewRow.BeginEdit();
                csNewRow.Item(ABMyNumberRuisekiEntity.JUMINCD) = csDataRow.Item(ABMyNumberEntity.JUMINCD);
                csNewRow.Item(ABMyNumberRuisekiEntity.SHICHOSONCD) = csDataRow.Item(ABMyNumberEntity.SHICHOSONCD);
                csNewRow.Item(ABMyNumberRuisekiEntity.KYUSHICHOSONCD) = csDataRow.Item(ABMyNumberEntity.KYUSHICHOSONCD);
                csNewRow.Item(ABMyNumberRuisekiEntity.MYNUMBER) = csDataRow.Item(ABMyNumberEntity.MYNUMBER);
                csNewRow.Item(ABMyNumberRuisekiEntity.SHORINICHIJI) = strShoriNichiji;
                csNewRow.Item(ABMyNumberRuisekiEntity.SHORIKB) = strShoriKB;
                csNewRow.Item(ABMyNumberRuisekiEntity.ZENGOKB) = strZengoKB;
                csNewRow.Item(ABMyNumberRuisekiEntity.CKINKB) = csDataRow.Item(ABMyNumberEntity.CKINKB);
                csNewRow.Item(ABMyNumberRuisekiEntity.IDOKB) = csDataRow.Item(ABMyNumberEntity.IDOKB);
                csNewRow.Item(ABMyNumberRuisekiEntity.IDOYMD) = csDataRow.Item(ABMyNumberEntity.IDOYMD);
                csNewRow.Item(ABMyNumberRuisekiEntity.IDOSHA) = csDataRow.Item(ABMyNumberEntity.IDOSHA);
                csNewRow.Item(ABMyNumberRuisekiEntity.RESERVE) = csDataRow.Item(ABMyNumberEntity.RESERVE);
                csNewRow.EndEdit();
                csMyNumberRuisekiEntity.Tables(ABMyNumberRuisekiEntity.TABLE_NAME).Rows.Add(csNewRow);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csNewRow;

        }

        #endregion
        // * 履歴番号 000050 2014/06/25 追加終了

        #region 宛名標準初期化
        // ************************************************************************************************
        // * メソッド名     宛名標準系DataRwo初期化処理
        // * 
        // * 構文           Private Sub ClearAtenaHyojun(ByVal csRow As DataRow)
        // * 
        // * 機能           宛名標準系DataRowの初期化を行う
        // * 
        // * 引数           csRow As DataRow     : 宛名標準Row
        // ************************************************************************************************
        private void ClearAtenaHyojun(DataRow csRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaHyojunEntity.KOSHINCOUNTER:
                        case var case1 when case1 == ABAtenaHyojunEntity.RRKNO:
                        case var case2 when case2 == ABAtenaHyojunEntity.EDANO:
                        case var case3 when case3 == ABAtenaHyojunEntity.KYOYUNINZU:
                            {
                                csRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
        }
        #endregion

        #region 宛名標準データ設定
        // ************************************************************************************************
        // * メソッド名     宛名標準データ設定処理
        // * 
        // * 構文           Private Function SetAtenaHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        // * 
        // * 機能           宛名標準の編集を行う
        // * 
        // * 引数           csRow As DataRow             : 宛名標準データ
        // *                csAtenaRow As DataRow        ：宛名データ
        // *                csJukiDataRow As DataRow     ：住基データ
        // *
        // * 戻り値         宛名標準データ
        // ************************************************************************************************
        private DataRow SetAtenaHyojun(DataRow csRow, DataRow csAtenaRow, DataRow csJukiDataRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            // *履歴番号 000069 2024/07/09 追加開始
            UFDateClass cfDate;
            // *履歴番号 000069 2024/07/09 追加終了
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // *履歴番号 000069 2024/07/09 追加開始
                // 日付クラス
                cfDate = new UFDateClass(m_cfConfigDataClass);
                cfDate.p_enDateFillType = UFDateFillType.Zero;
                cfDate.p_enDateSeparator = UFDateSeparator.Hyphen;
                cfDate.p_enEraType = UFEraType.Number;
                // *履歴番号 000069 2024/07/09 追加終了

                // データ編集
                csRow(ABAtenaHyojunEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD);                            // 住民コード
                csRow(ABAtenaHyojunEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB);              // 住民住登外区分
                csRow(ABAtenaHyojunEntity.RRKNO) = csJukiDataRow(ABJukiData.RIREKINO);                             // 履歴番号
                csRow(ABAtenaHyojunEntity.EDANO) = csJukiDataRow(ABJukiData.EDANO);                                // 枝番号
                csRow(ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG) = csJukiDataRow(ABJukiData.SHIMEIKANAKAKUNINFG);    // 氏名フリガナ確認フラグ
                if (csJukiDataRow(ABJukiData.FUSHOUMAREBI).ToString.Trim == string.Empty)
                {
                    csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN) = FUSHOPTN_NASHI;                                   // 生年月日不詳パターン
                }
                else
                {
                    csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN) = FUSHOPTN_FUSHO;
                }
                csRow(ABAtenaHyojunEntity.FUSHOUMAREBI) = csJukiDataRow(ABJukiData.FUSHOUMAREBI);                  // 不詳生年月日
                csRow(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI) = csJukiDataRow(ABJukiData.JIJITSUSTAINUSMEI);        // 事実上の世帯主

                if (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    // 転出確定
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD);                 // 住所_市区町村コード
                    csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD);                       // 住所_町字コード
                    csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN);                         // 住所_都道府県
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON);                     // 住所_市区郡町村名
                    csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA);                           // 住所_町字
                    csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString);   // 検索用住所
                    csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI).ToString);  // 検索用方書
                    csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = m_cABBanchiEdabanSuchiB.GetBanchiEdabanSuchi(csAtenaRow(ABAtenaEntity.BANCHICD1).ToString, csAtenaRow(ABAtenaEntity.BANCHICD2).ToString, csAtenaRow(ABAtenaEntity.BANCHICD3).ToString);                    // 番地枝番数値
                }
                else if (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    // 転出予定
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD);                // 住所_市区町村コード
                    csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD);                      // 住所_町字コード
                    csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN);                        // 住所_都道府県
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON);                    // 住所_市区郡町村名
                    csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA);                          // 住所_町字
                    csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString);  // 検索用住所
                    csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI).ToString); // 検索用方書
                    csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = m_cABBanchiEdabanSuchiB.GetBanchiEdabanSuchi(csAtenaRow(ABAtenaEntity.BANCHICD1).ToString, csAtenaRow(ABAtenaEntity.BANCHICD2).ToString, csAtenaRow(ABAtenaEntity.BANCHICD3).ToString);                    // 番地枝番数値
                }
                else
                {
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.SHIKUCHOSONCD);                              // 住所_市区町村コード
                    csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.MACHIAZACD);                                    // 住所_町字コード
                    csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TODOFUKEN);                                      // 住所_都道府県
                    csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.SHIKUGUNCHOSON);                               // 住所_市区郡町村名
                    csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.MACHIAZA);                                        // 住所_町字
                    csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = csJukiDataRow(ABJukiData.SEARCHJUSHO);                                  // 検索用住所
                    csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = csJukiDataRow(ABJukiData.SEARCHKATAGAKI);                            // 検索用方書
                    csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = csJukiDataRow(ABJukiData.BANCHIEDABANSUCHI);
                }                      // 番地枝番数値
                csRow(ABAtenaHyojunEntity.KANAKATAGAKI) = string.Empty;                                         // 方書フリガナ
                csRow(ABAtenaHyojunEntity.JUSHO_KUNIMEICODE) = string.Empty;                                    // 住所_国名コード
                csRow(ABAtenaHyojunEntity.JUSHO_KUNIMEITO) = string.Empty;                                      // 住所_国名等
                csRow(ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO) = string.Empty;                                   // 住所_国外住所
                csRow(ABAtenaHyojunEntity.HON_SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.HON_SHIKUCHOSONCD);     // 本籍_市区町村コード
                csRow(ABAtenaHyojunEntity.HON_MACHIAZACD) = csJukiDataRow(ABJukiData.HON_MACHIAZACD);           // 本籍_町字コード
                csRow(ABAtenaHyojunEntity.HON_TODOFUKEN) = csJukiDataRow(ABJukiData.HON_TODOFUKEN);             // 本籍_都道府県
                csRow(ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON) = csJukiDataRow(ABJukiData.HON_SHIKUGUNCHOSON);   // 本籍_市区郡町村名
                csRow(ABAtenaHyojunEntity.HON_MACHIAZA) = csJukiDataRow(ABJukiData.HON_MACHIAZA);               // 本籍_町字
                csRow(ABAtenaHyojunEntity.CKINIDOWMD) = csJukiDataRow(ABJukiData.CKINIDOWMD);                   // 直近異動和暦年月日
                if (csJukiDataRow(ABJukiData.FUSHOCKINIDOBI).ToString.Trim == string.Empty)
                {
                    csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN) = FUSHOPTN_NASHI;                              // 直近異動日不詳パターン
                }
                else
                {
                    csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN) = FUSHOPTN_FUSHO;
                }
                csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBI);           // 不詳直近異動日
                csRow(ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN) = FUSHOPTN_NASHI;                                // 登録異動日不詳パターン
                csRow(ABAtenaHyojunEntity.FUSHOTOROKUIDOBI) = string.Empty;                                     // 不詳登録異動日
                csRow(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNKISAIJIYUCD);     // 記載事由
                csRow(ABAtenaHyojunEntity.KISAIYMD) = csJukiDataRow(ABJukiData.KISAIYMD);                       // 記載年月日
                csRow(ABAtenaHyojunEntity.KISAIBIFUSHOPTN) = FUSHOPTN_NASHI;                                    // 記載年月日不詳パターン
                csRow(ABAtenaHyojunEntity.FUSHOKISAIBI) = string.Empty;                                         // 不詳記載年月日
                csRow(ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN) = FUSHOPTN_NASHI;                                 // 住定異動日不詳パターン
                csRow(ABAtenaHyojunEntity.FUSHOJUTEIIDOBI) = string.Empty;                                      // 不詳住定異動日
                csRow(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNSHOJOJIYUCD);     // 消除事由
                csRow(ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI) = string.Empty;                                   // 国籍喪失日
                csRow(ABAtenaHyojunEntity.SHOJOIDOWMD) = csJukiDataRow(ABJukiData.SHOJOIDOWMD);                 // 消除異動和暦年月日
                if (csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBI).ToString.Trim == string.Empty)
                {
                    csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN) = FUSHOPTN_NASHI;                             // 消除異動日不詳パターン
                }
                else
                {
                    csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN) = FUSHOPTN_FUSHO;
                }
                csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBI);                // 不詳消除異動日
                csRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENUMAEJ_SHIKUCHOSONCD);  // 転入前住所_市区町村コード
                csRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csJukiDataRow(ABJukiData.TENUMAEJ_MACHIAZACD);        // 転入前住所_町字コード
                csRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csJukiDataRow(ABJukiData.TENUMAEJ_TODOFUKEN);          // 転入前住所_都道府県
                csRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENUMAEJ_SHIKUCHOSON);      // 転入前住所_市区郡町村名
                csRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csJukiDataRow(ABJukiData.TENUMAEJ_MACHIAZA);            // 転入前住所_町字
                csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUSEKICD);        // 転入前住所_国名コード
                csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUSEKI);            // 転入前住所_国名
                csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUGAIJUSHO);    // 転入前住所_国外住所
                csRow(ABAtenaHyojunEntity.SAISHUTJ_YUBINNO) = string.Empty;                                     // 最終登録住所_郵便番号
                csRow(ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD) = string.Empty;                               // 最終登録住所_市区町村コード
                csRow(ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD) = string.Empty;                                  // 最終登録住所_町字コード
                csRow(ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN) = string.Empty;                                   // 最終登録住所_都道府県
                csRow(ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON) = string.Empty;                                 // 最終登録住所_市区郡町村名
                csRow(ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA) = string.Empty;                                    // 最終登録住所_町字
                csRow(ABAtenaHyojunEntity.SAISHUTJ_BANCHI) = string.Empty;                                      // 最終登録住所_番地号表記
                csRow(ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI) = string.Empty;                                    // 最終登録住所_方書
                csRow(ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN) = string.Empty;                                    // 最終住所_都道府県
                csRow(ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON) = string.Empty;                                  // 最終住所_市区郡町村名
                csRow(ABAtenaHyojunEntity.SAISHUJ_MACHIAZA) = string.Empty;                                     // 最終住所_町字
                csRow(ABAtenaHyojunEntity.SAISHUJ_BANCHI) = string.Empty;                                       // 最終住所_番地号表記
                csRow(ABAtenaHyojunEntity.SAISHUJ_KATAGAKI) = string.Empty;                                     // 最終住所_方書
                                                                                                                // * 履歴番号 000063 2024/02/06 修正開始
                                                                                                                // csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD) ' 転出予定_市区町村コード
                                                                                                                // csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD)       ' 転出予定_町字コード
                                                                                                                // csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN)         ' 転出予定_都道府県
                                                                                                                // csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON)     ' 転出予定_市区郡町村名
                                                                                                                // csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA)           ' 転出予定_町字

                // 住基データ.処理事由コード＝45（転入通知受理）の場合
                if (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() == ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"))
                {
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD);  // 転出予定_市区町村コード
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD);        // 転出予定_町字コード
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN);          // 転出予定_都道府県
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON);      // 転出予定_市区郡町村名
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA);            // 転出予定_町字
                }
                else
                {
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD); // 転出予定_市区町村コード
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD);       // 転出予定_町字コード
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN);         // 転出予定_都道府県
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON);     // 転出予定_市区郡町村名
                    csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA);
                }           // 転出予定_町字
                            // * 履歴番号 000063 2024/02/06 修正終了
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUSEKICD);       // 転出予定_国名コード
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUSEKI);           // 転出予定_国名等
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUGAIJUSHO);   // 転出予定_国外住所
                csRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD);   // 転出確定_市区町村コード
                csRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD);         // 転出確定_町字コード
                csRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN);           // 転出確定_都道府県
                csRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON);       // 転出確定_市区郡町村名
                csRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA);             // 転出確定_町字
                csRow(ABAtenaHyojunEntity.KAISEIBIFUSHOPTN) = FUSHOPTN_NASHI;                                   // 改製年月日不詳パターン
                csRow(ABAtenaHyojunEntity.FUSHOKAISEIBI) = string.Empty;                                        // 不詳改製年月日
                csRow(ABAtenaHyojunEntity.KAISEISHOJOYMD) = string.Empty;                                       // 改製消除年月日
                csRow(ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN) = FUSHOPTN_NASHI;                              // 改製消除年月日不詳パターン
                csRow(ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI) = string.Empty;                                   // 不詳改製消除年月日
                csRow(ABAtenaHyojunEntity.CHIKUCD4) = string.Empty;                                             // 地区コード４
                csRow(ABAtenaHyojunEntity.CHIKUCD5) = string.Empty;                                             // 地区コード５
                csRow(ABAtenaHyojunEntity.CHIKUCD6) = string.Empty;                                             // 地区コード６
                csRow(ABAtenaHyojunEntity.CHIKUCD7) = string.Empty;                                             // 地区コード７
                csRow(ABAtenaHyojunEntity.CHIKUCD8) = string.Empty;                                             // 地区コード８
                csRow(ABAtenaHyojunEntity.CHIKUCD9) = string.Empty;                                             // 地区コード９
                csRow(ABAtenaHyojunEntity.CHIKUCD10) = string.Empty;                                            // 地区コード１０
                csRow(ABAtenaHyojunEntity.TOKUBETSUYOSHIKB) = csJukiDataRow(ABJukiData.TOKUBETSUYOSHIKB);       // 特別養子区分
                csRow(ABAtenaHyojunEntity.HYOJUNIDOKB) = csJukiDataRow(ABJukiData.IDOKB);                       // 異動区分
                csRow(ABAtenaHyojunEntity.NYURYOKUBASHOCD) = csJukiDataRow(ABJukiData.NYURYOKUBASHOCD);         // 入力場所コード
                csRow(ABAtenaHyojunEntity.NYURYOKUBASHO) = csJukiDataRow(ABJukiData.NYURYOKUBASHO);             // 入力場所表記
                csRow(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI) = csJukiDataRow(ABJukiData.SEARCHKANJIKYUUJI);     // 検索用漢字旧氏
                csRow(ABAtenaHyojunEntity.SEARCHKANAKYUUJI) = csJukiDataRow(ABJukiData.SEARCHKANAKYUUJI);       // 検索用カナ旧氏
                csRow(ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG) = csJukiDataRow(ABJukiData.KYUUJIKANAKAKUNINFG); // 旧氏フリガナ確認フラグ
                csRow(ABAtenaHyojunEntity.TDKDSHIMEI) = csJukiDataRow(ABJukiData.TDKDSHIMEI);                   // 届出人氏名
                csRow(ABAtenaHyojunEntity.HYOJUNIDOJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNIDOJIYUCD);         // 標準準拠異動事由コード
                csRow(ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD) = string.Empty;                              // 日常生活圏域コード
                csRow(ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA) = string.Empty;                     // 公簿上の住所（所在地）_読み仮名
                csRow(ABAtenaHyojunEntity.TOROKUBUSHO) = string.Empty;                                          // 登録部署
                csRow(ABAtenaHyojunEntity.TANKITAIZAISHAFG) = string.Empty;                                     // 短期滞在者フラグ
                csRow(ABAtenaHyojunEntity.KYOYUNINZU) = decimal.Zero;                                           // 共有者人数
                csRow(ABAtenaHyojunEntity.SHIZEIJIMUSHOCD) = string.Empty;                                      // 市税事務所コード
                csRow(ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST) = string.Empty;                                     // 出国期間_開始年月日
                csRow(ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED) = string.Empty;                                     // 出国期間_終了年月日
                csRow(ABAtenaHyojunEntity.IDOSHURUI) = string.Empty;                                            // 異動の種類
                csRow(ABAtenaHyojunEntity.SHOKANKUCD) = "000000";                                               // 所管区コード
                csRow(ABAtenaHyojunEntity.TOGOATENAFG) = csJukiDataRow(ABJukiData.TOGOATENAFG);                 // 統合宛名フラグ
                                                                                                                // *履歴番号 000069 2024/07/09 修正開始
                                                                                                                // csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = csJukiDataRow(ABJukiData.FUSHOUMAREBIDATE)      ' 不詳生年月日DATE
                                                                                                                // csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBIDATE)  ' 不詳直近異動日DATE
                                                                                                                // csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBIDATE) ' 不詳消除異動日DATE
                                                                                                                // 不詳生年月日DATE
                if (new string(csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN).ToString ?? new char[0]) == FUSHOPTN_FUSHO)
                {
                    csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = csJukiDataRow(ABJukiData.FUSHOUMAREBIDATE);
                }
                else
                {
                    cfDate.p_strDateValue = csJukiDataRow(ABJukiData.UMAREYMD).ToString;
                    csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = cfDate.p_strSeirekiYMD;
                }
                // 不詳直近異動日DATE
                if (new string(csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN).ToString ?? new char[0]) == FUSHOPTN_FUSHO)
                {
                    csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBIDATE);
                }
                else
                {
                    cfDate.p_strDateValue = csJukiDataRow(ABJukiData.CKINIDOYMD).ToString;
                    csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = cfDate.p_strSeirekiYMD;
                }
                // 不詳消除異動日DATE
                if (new string(csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN).ToString ?? new char[0]) == FUSHOPTN_FUSHO)
                {
                    csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBIDATE);
                }
                else
                {
                    cfDate.p_strDateValue = csJukiDataRow(ABJukiData.SHOJOIDOYMD).ToString;
                    csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = cfDate.p_strSeirekiYMD;
                }
                // *履歴番号 000069 2024/07/09 修正終了
                csRow(ABAtenaHyojunEntity.JUKISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.SHIKUCHOSONCD);         // 住基住所_市区町村コード
                csRow(ABAtenaHyojunEntity.JUKIMACHIAZACD) = csJukiDataRow(ABJukiData.MACHIAZACD);               // 住基住所_町字コード
                csRow(ABAtenaHyojunEntity.JUKITODOFUKEN) = csJukiDataRow(ABJukiData.TODOFUKEN);                 // 住基住所_都道府県
                csRow(ABAtenaHyojunEntity.JUKISHIKUCHOSON) = csJukiDataRow(ABJukiData.SHIKUGUNCHOSON);          // 住基住所_市区郡町村名
                csRow(ABAtenaHyojunEntity.JUKIMACHIAZA) = csJukiDataRow(ABJukiData.MACHIAZA);                   // 住基住所_町字
                csRow(ABAtenaHyojunEntity.JUKIKANAKATAGAKI) = string.Empty;                                     // 住基住所_方書フリガナ
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD4) = string.Empty;                                         // 住基地区コード4
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD5) = string.Empty;                                         // 住基地区コード5
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD6) = string.Empty;                                         // 住基地区コード6
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD7) = string.Empty;                                         // 住基地区コード7
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD8) = string.Empty;                                         // 住基地区コード8
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD9) = string.Empty;                                         // 住基地区コード9
                csRow(ABAtenaHyojunEntity.JUKICHIKUCD10) = string.Empty;                                        // 住基地区コード10
                csRow(ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI) = csJukiDataRow(ABJukiData.BANCHIEDABANSUCHI); // 住基番地枝番数値
                csRow(ABAtenaHyojunEntity.RESERVE1) = string.Empty;                              // リザーブ１
                csRow(ABAtenaHyojunEntity.RESERVE2) = string.Empty;                              // リザーブ２
                csRow(ABAtenaHyojunEntity.RESERVE3) = string.Empty;                              // リザーブ３
                csRow(ABAtenaHyojunEntity.RESERVE4) = string.Empty;                              // リザーブ４
                csRow(ABAtenaHyojunEntity.RESERVE5) = string.Empty;                              // リザーブ５


                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csRow;
        }
        #endregion

        #region 検索文字作成
        // ************************************************************************************************
        // * メソッド名     検索文字作成
        // * 
        // * 構文           Private Function GetSearchMoji(ByVal strData As String) As String
        // * 
        // * 機能           類字化・大文字化を行なう
        // * 
        // * 引数           strData As String     :対象データ
        // *
        // * 戻り値         類字化データ
        // ************************************************************************************************
        private string GetSearchMoji(string strData)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string strResult;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // データ編集
                if (string.IsNullOrEmpty(strData.Trim()))
                {
                    strResult = string.Empty;
                }
                else
                {
                    strResult = m_cuUsRuiji.GetRuijiMojiList(strData.Replace("　", string.Empty)).ToUpper;
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return strResult;
        }
        #endregion

        #region 宛名作成
        // ************************************************************************************************
        // * メソッド名     宛名Row作成
        // * 
        // * 構文           Private Function SetAtena(ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        // * 
        // * 機能           宛名Rowを作成する
        // * 
        // * 引数           csAtenaRow As DataRow     :宛名Rowt
        // *                csJukiDataRow As DataRow  :住基データRow
        // *
        // * 戻り値         宛名Row
        // ************************************************************************************************
        private DataRow SetAtena(DataRow csAtenaRow, DataRow csJukiDataRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string[] strBanchiCD;                         // 番地コード取得用配列
            ABHenshuSearchShimeiBClass cHenshuSearchKana; // 検索用カナ生成クラス
            var strSearchKana = new string[5];                      // 検索用カナ名称用

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                cHenshuSearchKana = new ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass);
                // データ編集

                // 住基データの同一項目を宛名マスタの項目にセットする
                // ・住民コード
                csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD);
                // ・市町村コード
                csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD);
                // ・旧市町村コード
                csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD);

                // 何もセットしない項目
                // ・住民票コード
                // ・汎用区分２
                // ・漢字法人形態
                // ・漢字法人代表者氏名
                // ・家屋敷区分
                // ・備考税目

                // 編集してセットする項目
                // ・住民住登外区分   1
                csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1";
                // ・住民優先区分     1
                csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1";
                // ・住登外優先区分
                // 住民種別の下１桁が”0”（住民）でなく、且つ住登外有りＦＬＧが”1”の時、　0
                // *履歴番号 000040 2009/05/22 修正開始
                // とりあえず無条件に "1" としてセットする
                csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1";
                // ・宛名データ区分=(11)
                csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11";
                // ・世帯コード～整理番号
                csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD);
                // csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
                csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO);
                // ・宛名データ種別=(住民種別)
                csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU);
                // ・汎用区分１=(写し区分)
                csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB);
                // ・個人法人区分=(1)
                csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1";

                // ・カナ名称１～検索用カナ名
                if (Conversions.ToString(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim == "2" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim ?? new char[0]) != "")
                {
                    // 本名優先(本名と通称名を持つ外国人かつ氏名利用区分が"2")
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = string.Empty;
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = GetSearchMoji(csJukiDataRow(ABJukiData.KANJIMEISHO2).ToString);

                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), string.Empty, m_cFrnHommyoKensakuType);
                    // 通称名を漢字法人代表者氏名に格納
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }

                else if (m_cFrnHommyoKensakuType == FrnHommyoKensakuType.Tsusho_Seishiki)
                {
                    // 通称名優先(本名優先の条件以外の場合)
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);
                    // 検索用カナ姓名、検索用カナ姓、検索用カナ名を生成し格納
                    strSearchKana = cHenshuSearchKana.GetSearchKana(Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO1)), Conversions.ToString(csJukiDataRow(ABJukiData.KANAMEISHO2)), m_cFrnHommyoKensakuType);
                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    // 取得した検索用カナ姓名、検索用カナ姓、検索用カナ名を格納
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana[0];
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana[1];
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana[2];
                }
                else
                {
                    // 通称名優先（既存ユーザ）
                    csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1);
                    csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2);
                    csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2);
                    // 通称名を漢字法人代表者氏名を空にする
                    csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = string.Empty;
                    // 汎用区分２に氏名利用区分のパラメータを格納
                    csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB);
                    csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI);
                    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI);
                }
                csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI);

                // ・住基履歴番号=(履歴番号)
                csAtenaRow(ABAtenaEntity.JUKIRRKNO) = Conversions.ToString(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4);
                // ・履歴開始年月日～住民票表示順
                csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD);
                csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD);
                csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD);
                csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD);
                csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD);
                csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU);
                csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO);
                csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN);
                // ・第２住民票表示順
                csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2);
                // ・続柄コード・続柄・第2続柄コード・第2続柄
                // 住民種別の下１桁が”8”（転出者）の場合で続柄が”01”（世帯主）の場合、管理情報のコードに変更し、			
                // 名称はクリアする
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) == "8")
                {
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd == "02")
                    {
                        if (m_strZokugara1Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = string.Empty;
                            csAtenaRow(ABAtenaEntity.ZOKUGARA) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init;
                            csAtenaRow(ABAtenaEntity.ZOKUGARA) = CNS_KURAN;
                        }
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    }
                    if (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd == "02")
                    {
                        if (m_strZokugara2Init == "00")
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = string.Empty;
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = string.Empty;
                        }
                        else
                        {
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init;
                            csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = CNS_KURAN;
                        }
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                    }
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）でない場合は、そのままセット			
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD);
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2);
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2);
                }
                // ・世帯主住民コード～カナ第２世帯主名
                csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD);
                csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2);
                csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2);
                csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2);

                // ・郵便番号～方書
                // ・転出確定住所がある場合は、転出確定欄からセット（ない項目はセットなし）
                if (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    // 番地情報から番地コードを取得
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)));
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                    // 管内管外区分：管外にセット    ※コメント:転出確定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }

                else if (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd != string.Empty)
                {
                    // ・転出確定住所が無く、転出予定住所がある場合は、転出予定欄からセット（ない項目はセットなし）
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    // 番地情報から番地コードを取得
                    strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)));
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD[0];
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD[1];
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD[2];
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = string.Empty;
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);
                    // 管内管外区分：管外にセット    ※コメント:転出予定住所が存在する場合は管外に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2";
                }

                else
                {
                    // ・両方も無い場合は、住基住所欄からセット
                    csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                    csAtenaRow(ABAtenaEntity.JUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.JUKIJUSHOCD)).RPadLeft(13);
                    csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                    csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                    csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                    csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                    csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                    csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                    csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                    csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);
                    // 管内管外区分：管内にセット    ※コメント:転出確定住所、転出予定住所が存在しない場合は管内に設定する。
                    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1";

                }
                // ・連絡先１～改製年月日
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1);
                csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2);
                csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.HON_ZJUSHOCD));
                csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO);
                csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI);
                csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA);
                csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD);
                csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD);
                csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU);
                csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD);
                csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD);
                csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD);
                csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD);
                csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD);
                csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU);
                csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD);
                csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD);
                csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD);
                csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB);
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD);
                csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD));
                csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI);
                csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI);
                // * 履歴番号 000063 2024/02/06 修正開始
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                // csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

                // 住基データ.処理事由コード＝45（転入通知受理）の場合
                if (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() == ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"))
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                }
                else
                {
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD));
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI);
                    csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI);
                }
                // * 履歴番号 000063 2024/02/06 修正終了

                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = Conversions.ToString(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD));
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI);
                csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG);
                csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD);
                csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO);
                csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG);
                csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO);
                csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG);
                csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG);
                csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD);

                // ・行政区コード～地区名３
                // 住民種別の下１桁が”8”（転出者）でない場合、住基行政区～住基地区名３をセット			
                if ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) != "8")
                {
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                }
                else
                {
                    // 住民種別の下１桁が”8”（転出者）の場合、管理情報（行政区初期化～地区３）を見て、
                    // クリアになっている場合は、セットしない
                    if (m_strGyosekuInit.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = string.Empty;
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                    }
                    else if (string.IsNullOrEmpty(m_strTenshutsuGyoseikuCD.Trim()))
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されていない場合は
                        // そのまま住基側のデータを設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                    }
                    else
                    {
                        // クリアしない場合で転出者用の行政区ＣＤが設定されている場合は
                        // 行政区ＣＤマスタより行政区名称を取得し、設定する。
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, ' ');
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = string.Empty;
                    }
                    if (m_strChiku1Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                    }
                    if (m_strChiku2Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                    }
                    if (m_strChiku3Init.TrimEnd() == "1")
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = string.Empty;
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = string.Empty;
                    }
                    else
                    {
                        csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                        csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                    }
                }

                // ・投票区コード～在留終了年月日
                csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD).ToString.RPadLeft(5);
                csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD);
                csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD);
                csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI);
                csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD);
                csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB);
                csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB);
                csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB);
                csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO);
                csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD);
                csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2);
                csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3);
                csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20);
                csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD);
                csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2);
                csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3);
                csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3);
                csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD);
                csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD);
                csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK);
                csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD);
                csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD);


                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRow;
        }
        #endregion

        #region 宛名履歴作成
        // ************************************************************************************************
        // * メソッド名     宛名履歴作成
        // * 
        // * 構文           Private Function SetAtenaRireki(ByVal csAtenaRirekiRow As DataRow, ByVal csAtenaRow As DataRow) As DataRow
        // * 
        // * 機能           宛名Rowから宛名履歴Rowを作成する
        // * 
        // * 引数           csAtenaRirekiRow As DataRow     :宛名履歴Row
        // *                csAtenaRow As DataRow           :宛名Row
        // *
        // * 戻り値         宛名履歴Row
        // ************************************************************************************************
        private DataRow SetAtenaRireki(DataRow csAtenaRirekiRow, DataRow csAtenaRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string strRirekiSTYMD;
            string strRirekiEDYMD;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strRirekiSTYMD = csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD).ToString;
                strRirekiEDYMD = csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD).ToString;

                // データ編集
                foreach (DataColumn csColumn in csAtenaRow.Table.Columns)
                {
                    if (csAtenaRirekiRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRirekiRow[csColumn.ColumnName] = csAtenaRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }
                csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = strRirekiSTYMD;
                csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = strRirekiEDYMD;

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRirekiRow;
        }
        #endregion

        #region 宛名履歴標準作成
        // ************************************************************************************************
        // * メソッド名     宛名履歴標準作成
        // * 
        // * 構文           Private Function SetAtenaRirekiHyojun(ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaHyojunRow As DataRow,
        // *                                      ByVal csAtenaRirekiRow As DataRow) As DataRow
        // * 
        // * 機能           宛名標準Rowから宛名履歴標準Rowを作成する
        // * 
        // * 引数           csAtenaRirekiHyojunRow As DataRow     :宛名履歴標準Row
        // *                csAtenaHyojunRow As DataRow           :宛名標準Row
        // *                csAtenaRirekiRow As DataRow           :宛名履歴Row
        // *
        // * 戻り値         宛名履歴標準Row
        // ************************************************************************************************
        private DataRow SetAtenaRirekiHyojun(DataRow csAtenaRirekiHyojunRow, DataRow csAtenaHyojunRow, DataRow csAtenaRirekiRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // データ編集
                foreach (DataColumn csColumn in csAtenaHyojunRow.Table.Columns)
                {
                    if (csAtenaRirekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRirekiHyojunRow[csColumn.ColumnName] = csAtenaHyojunRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRirekiHyojunRow;
        }
        #endregion

        #region 宛名累積標準作成
        // ************************************************************************************************
        // * メソッド名     宛名累積標準作成
        // * 
        // * 構文           Private Function SetAtenaRuisekiHyojun(ByVal csAtenaRuisekiHyojunRow As DataRow,
        // *                                 ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
        // * 
        // * 機能           宛名履歴標準Rowから宛名累積標準Rowを作成する
        // * 
        // * 引数           csAtenaRuisekiHyojunRow As DataRow :宛名累積標準Row
        // *                csAtenaRirekiHyojunRowAs DataRow   :宛名履歴標準Row
        // *                csAtenaRuisekiRow As DataRow       :宛名累積Row
        // *
        // * 戻り値         宛名累積標準Row
        // ************************************************************************************************
        private DataRow SetAtenaRuisekiHyojun(DataRow csAtenaRuisekiHyojunRow, DataRow csAtenaRirekiHyojunRow, DataRow csAtenaRuisekiRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // データ編集
                foreach (DataColumn csColumn in csAtenaRirekiHyojunRow.Table.Columns)
                {
                    if (csAtenaRuisekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRuisekiHyojunRow[csColumn.ColumnName] = csAtenaRirekiHyojunRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }
                csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRuisekiHyojunRow;
        }
        #endregion

        #region 宛名履歴標準直近データ取得
        // ************************************************************************************************
        // * メソッド名     宛名履歴標準直近データ取得
        // * 
        // * 構文           Private Function GetChokkin_RirekiHyojun(ByVal csAtenaRirekiHyojun As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        // * 
        // * 機能           宛名履歴標準の直近データを取得する
        // * 
        // * 引数           csAtenaRirekiHyojun As DataSet   : 宛名履歴標準データ
        // *                strJuminCD As String             : 住民コード
        // *                strRirekiNo As String            : 履歴番号
        // *
        // * 戻り値         宛名履歴標準を引数の条件で検索し、結果の０番目を返す。無い時はNothingを返す
        // ************************************************************************************************
        private DataRow GetChokkin_RirekiHyojun(DataSet csAtenaRirekiHyojun, string strJuminCD, string strRirekiNo)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataRow[] csSelectedRows; // 検索結果配列
            DataRow csCkinRow;        // 直近行
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (csAtenaRirekiHyojun is not null)
                {
                    // 引数宛名履歴標準がNothingでない時
                    csSelectedRows = csAtenaRirekiHyojun.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiHyojunEntity.JUMINCD, strJuminCD, ABAtenaRirekiHyojunEntity.RIREKINO, strRirekiNo));
                    if (csSelectedRows.Count() > 0)
                    {
                        // 直近データが存在した時、０行目を取っておく
                        csCkinRow = csSelectedRows[0];
                    }
                    else
                    {
                        // それ以外の時、Nothingで返す
                        csCkinRow = null;
                    }
                }
                else
                {
                    // Nothingの時はNothingで返す
                    csCkinRow = null;
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csCkinRow;
        }
        #endregion

        #region 宛名付随標準初期化
        // ************************************************************************************************
        // * メソッド名     宛名付随標準系DataRwo初期化処理
        // * 
        // * 構文           Private Sub ClearAtenaFZYHyojun(ByVal csRow As DataRow)
        // * 
        // * 機能           宛名付随標準系DataRowの初期化を行う
        // * 
        // * 引数           csRow As DataRow     : 宛名付随標準Row
        // ************************************************************************************************
        private void ClearAtenaFZYHyojun(DataRow csRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 項目の初期化
                foreach (DataColumn csDataColumn in csRow.Table.Columns)
                {
                    switch (csDataColumn.ColumnName ?? "")
                    {
                        case var @case when @case == ABAtenaFZYHyojunEntity.KOSHINCOUNTER:
                            {
                                csRow[csDataColumn] = decimal.Zero;
                                break;
                            }

                        default:
                            {
                                csRow[csDataColumn] = string.Empty;
                                break;
                            }
                    }
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
        }
        #endregion

        #region 宛名付随標準データ設定
        // ************************************************************************************************
        // * メソッド名     宛名付随標準データ設定処理
        // * 
        // * 構文           Private Function SetAtenaHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        // * 
        // * 機能           宛名付随標準の編集を行う
        // * 
        // * 引数           csRow As DataRow             : 宛名付随標準データ
        // *                csAtenaRow As DataRow        ：宛名データ
        // *                csJukiDataRow As DataRow     ：住基データ
        // *
        // * 戻り値         宛名付随標準データ
        // ************************************************************************************************
        private DataRow SetAtenaFZYHyojun(DataRow csRow, DataRow csAtenaRow, DataRow csJukiDataRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // データ編集
                csRow(ABAtenaFZYHyojunEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD);                         // 住民コード
                csRow(ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB);           // 住民住登外区分
                csRow(ABAtenaFZYHyojunEntity.SEARCHFRNMEI) = csJukiDataRow(ABJukiData.SEARCHFRNMEI);               // 検索用外国人名
                csRow(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI) = csJukiDataRow(ABJukiData.SEARCHKANAFRNMEI);       // 検索用カナ外国人名
                csRow(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI) = csJukiDataRow(ABJukiData.SEARCHTSUSHOMEI);         // 検索用通称名
                csRow(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI) = csJukiDataRow(ABJukiData.SEARCHKANATSUSHOMEI); // 検索用カナ通称名
                csRow(ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG) = csJukiDataRow(ABJukiData.TSUSHOKANAKAKUNINFG); // 通称フリガナ確認フラグ
                                                                                                                   // *履歴番号 000068 2024/07/05 修正開始
                                                                                                                   // csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = csJukiDataRow(ABJukiData.SHIMEIYUSENKB)             ' 氏名優先区分
                                                                                                                   // 氏名優先区分
                if (Conversions.ToString(csJukiDataRow(ABJukiData.SHIMEIYUSENKB)).Trim == "2" && new string(Conversions.ToString(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim ?? new char[0]) != "")
                {
                    // 氏名優先区分＝2（本名優先） かつ 併記名≠空白 の場合
                    // 氏名優先区分に3（併記名優先）を設定
                    csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = "3";
                }
                else
                {
                    csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = csJukiDataRow(ABJukiData.SHIMEIYUSENKB);
                }
                // *履歴番号 000068 2024/07/05 修正終了
                csRow(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI) = csJukiDataRow(ABJukiData.SEARCHKANJIHEIKIMEI); // 検索用漢字併記名
                csRow(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI) = csJukiDataRow(ABJukiData.SEARCHKANAHEIKIMEI);   // 検索用カナ併記名
                csRow(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN) = csJukiDataRow(ABJukiData.ZAIRYUCARDNOKBN);         // 在留カード等番号区分
                csRow(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD) = csJukiDataRow(ABJukiData.JUKYOCHIHOSEICD);         // 住居地補正コード
                csRow(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB) = csJukiDataRow(ABJukiData.HODAI30JO46MATAHA47KB); // 法第30条46又は47区分
                csRow(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB) = string.Empty;                                   // 世帯主氏名優先区分
                csRow(ABAtenaFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN) = string.Empty;                                   // 特別永住者証明書有効期限
                csRow(ABAtenaFZYHyojunEntity.RESERVE1) = string.Empty;                                               // リザーブ１
                csRow(ABAtenaFZYHyojunEntity.RESERVE2) = string.Empty;                                               // リザーブ２
                csRow(ABAtenaFZYHyojunEntity.RESERVE3) = string.Empty;                                               // リザーブ３
                csRow(ABAtenaFZYHyojunEntity.RESERVE4) = string.Empty;                                               // リザーブ４
                csRow(ABAtenaFZYHyojunEntity.RESERVE5) = string.Empty;                                               // リザーブ５

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csRow;
        }
        #endregion

        #region 宛名履歴付随標準作成
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随標準作成
        // * 
        // * 構文           Private Function SetAtenaRirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojunRow As DataRow,
        // *                                                         ByVal csAtenaFZYHyojunRow As DataRow) As DataRow
        // * 
        // * 機能           宛名付随標準Rowから宛名履歴付随標準Rowを作成する
        // * 
        // * 引数           csAtenaRirekiFZYRow As DataRow     :宛名履歴付随標準Row
        // *                csAtenaFZYHyojunRow As DataRow     :宛名付随標準Row
        // *
        // * 戻り値         宛名履歴付随標準Row
        // ************************************************************************************************
        private DataRow SetAtenaRirekiFZYHyojun(DataRow csAtenaRirekiFZYHyojunRow, DataRow csAtenaFZYHyojunRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // データ編集
                foreach (DataColumn csColumn in csAtenaFZYHyojunRow.Table.Columns)
                {
                    if (csAtenaRirekiFZYHyojunRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRirekiFZYHyojunRow[csColumn.ColumnName] = csAtenaFZYHyojunRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRirekiFZYHyojunRow;
        }
        #endregion

        #region 宛名累積付随標準作成
        // ************************************************************************************************
        // * メソッド名     宛名累積付随標準作成
        // * 
        // * 構文           Private Function SetAtenaRuisekiHyojun(ByVal csAtenaRuisekiFZYHyojunRow As DataRow,
        // *                                 ByVal csAtenaRirekiFZYHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
        // * 
        // * 機能           宛名履歴付随標準Rowから宛名累積付随標準Rowを作成する
        // * 
        // * 引数           csAtenaRuisekiFZYHyojunRow As DataRow :宛名累積付随標準Row
        // *                csAtenaRirekiFZYHyojunRowAs DataRow   :宛名履歴付随標準Row
        // *                csAtenaRuisekiRow As DataRow       :宛名累積Row
        // *
        // * 戻り値         宛名累積付随標準Row
        // ************************************************************************************************
        private DataRow SetAtenaRuisekiFZYHyojun(DataRow csAtenaRuisekiFZYHyojunRow, DataRow csAtenaRirekiFZYHyojunRow, DataRow csAtenaRuisekiRow)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // データ編集
                foreach (DataColumn csColumn in csAtenaRirekiFZYHyojunRow.Table.Columns)
                {
                    if (csAtenaRuisekiFZYHyojunRow.Table.Columns.Contains(csColumn.ColumnName))
                    {
                        // 列があった時だけセット
                        csAtenaRuisekiFZYHyojunRow[csColumn.ColumnName] = csAtenaRirekiFZYHyojunRow[csColumn.ColumnName];
                    }
                    else
                    {
                        // 何もしない
                    }
                }
                csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI);
                csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB);

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csAtenaRuisekiFZYHyojunRow;
        }
        #endregion

        #region 宛名履歴付随標準直近データ取得
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随標準直近データ取得
        // * 
        // * 構文           Private Function GetChokkin_RirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojun As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        // * 
        // * 機能           宛名履歴付随標準の直近データを取得する
        // * 
        // * 引数           csAtenaRirekiFZYHyojun As DataSet: 宛名履歴付随標準データ
        // *                strJuminCD As String             : 住民コード
        // *                strRirekiNo As String            : 履歴番号
        // *
        // * 戻り値         宛名履歴付随標準を引数の条件で検索し、結果の０番目を返す。無い時はNothingを返す
        // ************************************************************************************************
        private DataRow GetChokkin_RirekiFZYHyojun(DataSet csAtenaRirekiFZYHyojun, string strJuminCD, string strRirekiNo)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataRow[] csSelectedRows; // 検索結果配列
            DataRow csCkinRow;        // 直近行
            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (csAtenaRirekiFZYHyojun is not null)
                {
                    // 引数宛名履歴付随標準がNothingでない時
                    csSelectedRows = csAtenaRirekiFZYHyojun.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Select(string.Format("{0}='{1}' AND {2}='{3}'", ABAtenaRirekiFZYHyojunEntity.JUMINCD, strJuminCD, ABAtenaRirekiFZYHyojunEntity.RIREKINO, strRirekiNo));
                    if (csSelectedRows.Count() > 0)
                    {
                        // 直近データが存在した時、０行目を取っておく
                        csCkinRow = csSelectedRows[0];
                    }
                    else
                    {
                        // それ以外の時、Nothingで返す
                        csCkinRow = null;
                    }
                }
                else
                {
                    // Nothingの時はNothingで返す
                    csCkinRow = null;
                }

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csCkinRow;
        }
        #endregion

        #region 共通番号累積標準作成
        // ************************************************************************************************
        // * メソッド名     共通番号累積標準作成
        // * 
        // * 構文            Private Function CreateMyNumberRuisekiHyojun(ByVal csMyNumberRuisekiHyojunEntity As DataSet,
        // *                                                              ByVal csMyNumberHyojunRow As DataRow, ByVal csMyNumberPrm As ABMyNumberPrmXClass,
        // *                                                              ByVal strShoriNichiji As String, ByVal strZengoKbn As String) As DataRow
        // * 
        // * 機能           共通番号標準Rowから共通番号累積標準Rowを作成する
        // * 
        // * 引数           csMyNumberRuisekiHyojunEntity As DataSet:共通番号累積標準DataSet
        // *                csMyNumberHyojunRow As DataRow          :共通番号標準Row
        // *                csMyNumberPrm                           :共通番号パラメータ
        // *                strShoriNichiji As String               :処理日時
        // *                ByVal strZengoKbn As String             :前後区分
        // *
        // * 戻り値         共通番号累積標準Row
        // ************************************************************************************************
        private DataRow CreateMyNumberRuisekiHyojun(DataSet csMyNumberRuisekiHyojunEntity, DataRow csMyNumberHyojunRow, ABMyNumberPrmXClass csMyNumberPrm, string strShoriNichiji, string strZengoKbn)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataRow csNewRow;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                csNewRow = csMyNumberRuisekiHyojunEntity.Tables(ABMyNumberRuisekiHyojunEntity.TABLE_NAME).NewRow;
                // データ編集
                csNewRow(ABMyNumberRuisekiHyojunEntity.JUMINCD) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.JUMINCD);                 // 住民コード
                csNewRow(ABMyNumberRuisekiHyojunEntity.SHICHOSONCD) = csMyNumberPrm.p_strShichosonCD;                                  // 市町村コード
                csNewRow(ABMyNumberRuisekiHyojunEntity.KYUSHICHOSONCD) = csMyNumberPrm.p_strKyuShichosonCD;                            // 旧市町村コード
                csNewRow(ABMyNumberRuisekiHyojunEntity.MYNUMBER) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.MYNUMBER);               // 個人法人番号
                csNewRow(ABMyNumberRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji;                                                // 処理日時
                csNewRow(ABMyNumberRuisekiHyojunEntity.ZENGOKB) = strZengoKbn;                                                         // 前後区分
                csNewRow(ABMyNumberRuisekiHyojunEntity.BANGOHOKOSHINKB) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB); // 番号法更新区分
                csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE1) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE1);               // リザーブ１
                csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE2) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE2);               // リザーブ２
                csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE3) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE3);               // リザーブ３
                csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE4) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE4);               // リザーブ４
                csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE5) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE5);               // リザーブ５

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csNewRow;
        }
        #endregion

        #region 共通番号標準作成
        // ************************************************************************************************
        // * メソッド名     共通番号標準作成
        // * 
        // * 構文           Private Function CreateMyNumberHyojun(ByVal csMyNumberHyojunEntity As DataSet,
        // *                                                      ByVal csMyNumberPrm As ABMyNumberPrmXClass) As DataRow
        // * 
        // * 機能           共通番号標準Rowを作成する
        // * 
        // * 引数           csMyNumberHyojunEntity As DataSet:共通番号累積標準DataSet
        // *                csMyNumberPrm                           :共通番号パラメータ
        // *
        // * 戻り値         共通番号標準Row
        // ************************************************************************************************
        private DataRow CreateMyNumberHyojun(DataSet csMyNumberHyojunEntity, ABMyNumberPrmXClass csMyNumberPrm)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataRow csNewRow;

            try
            {
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                csNewRow = csMyNumberHyojunEntity.Tables(ABMyNumberHyojunEntity.TABLE_NAME).NewRow;
                // データ編集
                csNewRow(ABMyNumberHyojunEntity.JUMINCD) = csMyNumberPrm.p_strJuminCD;    // 住民コード
                csNewRow(ABMyNumberHyojunEntity.MYNUMBER) = csMyNumberPrm.p_strMyNumber;  // 個人法人番号
                csNewRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB) = string.Empty;          // 番号法更新区分
                csNewRow(ABMyNumberHyojunEntity.RESERVE1) = string.Empty;                 // リザーブ１
                csNewRow(ABMyNumberHyojunEntity.RESERVE2) = string.Empty;                 // リザーブ２
                csNewRow(ABMyNumberHyojunEntity.RESERVE3) = string.Empty;                 // リザーブ３
                csNewRow(ABMyNumberHyojunEntity.RESERVE4) = string.Empty;                 // リザーブ４
                csNewRow(ABMyNumberHyojunEntity.RESERVE5) = string.Empty;                 // リザーブ５

                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }
            catch (UFAppException objAppExp)
            {
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                throw;
            }
            catch (Exception objExp)
            {
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                throw;
            }
            return csNewRow;
        }
        #endregion

        #region 住基データ更新（履歴）
        // ************************************************************************************************
        // * メソッド名     住基データ更新（履歴）
        // * 
        // * 構文           Public Sub JukiDataKoshin08N() 
        // * 
        // * 機能 　    　　住基履歴データを更新する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void JukiDataKoshin08N(DataRow csJukiDataRow)
        {
            const string THIS_METHOD_NAME = "JukiDataKoshin08N";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            string strJuminCD;                            // 住民コード
            string strLinkNo;                             // リンク用連番
            string strRirekiNO;                           // 履歴番号
            DataSet csAtenaEntity;                        // 宛名マスタEntity
            DataRow csAtenaRow;                           // 宛名マスタRow
            DataRow csAtenaRirekiRow;                     // 宛名履歴Row
            int intCount;                             // 更新件数
            DataSet csAtenaRirekiEntity;                  // 宛名履歴
            DataRow csBkAtenaRirekiRow;
            DataSet csAtenaFzyEntity;                     // 宛名付随データ
            DataRow csAtenaFzyRow;                        // 宛名付随行
            DataSet csAtenaRirekiFzyEntity;               // 宛名履歴付随
            DataRow csAtenaRirekiFzyRow;                  // 宛名履歴付随行
            DataRow csBkAtenaRirekiFzyRow;                // 宛名履歴付随行
            DataSet csAtenaHyojunEntity;                  // 宛名標準
            DataRow csAtenaHyojunRow;                     // 宛名標準Row
            DataSet csAtenaRirekiHyojunEntity;            // 宛名履歴標準
            DataRow csAtenaRirekiHyojunRow;               // 宛名履歴標準Row
            DataRow csBkAtenaRirekiHyojunRow;
            DataSet csAtenaFZYHyojunEntity;               // 宛名付随標準
            DataRow csAtenaFZYHyojunRow;                  // 宛名付随標準Row
            DataSet csAtenaRirekiFZyHyojunEntity;         // 宛名履歴付随標準
            DataRow csAtenaRirekiFZYHyojunRow;            // 宛名履歴付随標準Row
            DataRow csBkAtenaRirekiFZYHyojunRow;
            bool blnRirekiHyojunUpdate;
            bool blnRirekiFZYHyojunUpdate;
            DataColumn csDataColumn;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ---------------------------------------------------------------------------------------
                // 1. 変数の初期化
                // 
                // ---------------------------------------------------------------------------------------
                strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString;    // 対象データの住民コードを取得
                strLinkNo = csJukiDataRow(ABJukiData.LINKNO).ToString.Trim;

                // ---------------------------------------------------------------------------------------
                // 2. データ編集
                // ---------------------------------------------------------------------------------------
                // 宛名
                csAtenaEntity = m_cfRdbClass.GetTableSchema(ABAtenaEntity.TABLE_NAME);
                csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow;
                ClearAtena(ref csAtenaRow);
                csAtenaRow = SetAtena(csAtenaRow, csJukiDataRow);

                // 宛名付随
                csAtenaFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaFZYEntity.TABLE_NAME);
                csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow;
                ClearAtenaFZY(csAtenaFzyRow);
                csAtenaFzyRow = SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow);

                // 宛名標準
                csAtenaHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaHyojunEntity.TABLE_NAME);
                csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).NewRow;
                ClearAtenaHyojun(csAtenaHyojunRow);
                csAtenaHyojunRow = SetAtenaHyojun(csAtenaHyojunRow, csAtenaRow, csJukiDataRow);

                // 宛名付随標準
                csAtenaFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaFZYHyojunEntity.TABLE_NAME);
                csAtenaFZYHyojunRow = csAtenaFZYHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).NewRow;
                ClearAtenaFZYHyojun(csAtenaFZYHyojunRow);
                csAtenaFZYHyojunRow = SetAtenaFZYHyojun(csAtenaFZYHyojunRow, csAtenaRow, csJukiDataRow);

                // ---------------------------------------------------------------------------------------
                // 3. 履歴データ取得
                // ---------------------------------------------------------------------------------------
                // 宛名履歴付随
                csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaRirekiFZYByLinkNo(strJuminCD, strLinkNo);
                if (csAtenaRirekiFzyEntity is null || csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count != 1)
                {
                    // エラー定義を取得（宛名履歴の更新でエラーしました。）
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003459);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + strJuminCD + "-" + strLinkNo, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0);
                    strRirekiNO = csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO).ToString;
                }

                // 宛名履歴
                csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRirekiByRirekiNO(strJuminCD, strRirekiNO);
                if (csAtenaRirekiEntity is null || csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count != 1)
                {
                    // エラー定義を取得（宛名履歴の更新でエラーしました。）
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003459);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + strJuminCD + "-" + strRirekiNO, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0);
                }

                // 宛名履歴標準
                csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(strJuminCD, strRirekiNO, true);
                if (csAtenaRirekiHyojunEntity is not null && csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {
                    csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0);
                    blnRirekiHyojunUpdate = true;
                }
                else
                {
                    csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow;
                    ClearAtenaHyojun(csAtenaRirekiHyojunRow);
                    blnRirekiHyojunUpdate = false;
                }

                // 宛名履歴付随標準
                csAtenaRirekiFZyHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(strJuminCD, strRirekiNO, true);
                if (csAtenaRirekiFZyHyojunEntity is not null && csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {
                    csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0);
                    blnRirekiFZYHyojunUpdate = true;
                }
                else
                {
                    csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow;
                    ClearAtenaFZYHyojun(csAtenaRirekiFZYHyojunRow);
                    blnRirekiFZYHyojunUpdate = false;
                }

                // ---------------------------------------------------------------------------------------
                // 4. 履歴データ編集
                // ---------------------------------------------------------------------------------------
                // 宛名履歴
                csBkAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow;
                foreach (DataColumn currentCsDataColumn in csAtenaRirekiRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn;
                    csBkAtenaRirekiRow[csDataColumn.ColumnName] = csAtenaRirekiRow[csDataColumn.ColumnName];
                }
                csAtenaRirekiRow = SetAtenaRireki(csAtenaRirekiRow, csAtenaRow);
                csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD);
                csAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID);
                csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG);
                csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER);
                csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER);
                csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER);

                // 宛名履歴標準
                csBkAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow;
                foreach (DataColumn currentCsDataColumn1 in csAtenaRirekiHyojunRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn1;
                    csBkAtenaRirekiHyojunRow[csDataColumn.ColumnName] = csAtenaRirekiHyojunRow[csDataColumn.ColumnName];
                }
                csAtenaRirekiHyojunRow = SetAtenaRirekiHyojun(csAtenaRirekiHyojunRow, csAtenaHyojunRow, csAtenaRirekiRow);
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                if (blnRirekiHyojunUpdate)
                {
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER);
                }
                else
                {
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER);
                }

                // 宛名履歴付随
                csBkAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow;
                foreach (DataColumn currentCsDataColumn2 in csAtenaRirekiFzyRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn2;
                    csBkAtenaRirekiFzyRow[csDataColumn.ColumnName] = csAtenaRirekiFzyRow[csDataColumn.ColumnName];
                }
                csAtenaRirekiFzyRow = SetAtenaRirekiFzy(csAtenaRirekiFzyRow, csAtenaFzyRow);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.TANMATSUID) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.TANMATSUID);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUJOFG);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI);
                csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINUSER);

                // 宛名履歴付随標準
                csBkAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow;
                foreach (DataColumn currentCsDataColumn3 in csAtenaRirekiFZYHyojunRow.Table.Columns)
                {
                    csDataColumn = currentCsDataColumn3;
                    csBkAtenaRirekiFZYHyojunRow[csDataColumn.ColumnName] = csAtenaRirekiFZYHyojunRow[csDataColumn.ColumnName];
                }
                csAtenaRirekiFZYHyojunRow = SetAtenaRirekiFZYHyojun(csAtenaRirekiFZYHyojunRow, csAtenaFZYHyojunRow);
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO);
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                if (blnRirekiFZYHyojunUpdate)
                {
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER);
                }
                else
                {
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER);
                }

                // ---------------------------------------------------------------------------------------
                // 5. 履歴データ更新
                // ---------------------------------------------------------------------------------------
                if (blnRirekiHyojunUpdate && blnRirekiFZYHyojunUpdate)
                {
                    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow);
                    if (intCount != 1)
                    {
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                    }
                    else
                    {
                    }
                }
                else
                {
                    // 宛名履歴・宛名履歴付随
                    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow);
                    if (intCount != 1)
                    {
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                    }
                    else
                    {
                    }
                    // 宛名履歴標準
                    if (blnRirekiHyojunUpdate)
                    {
                        intCount = m_cABAtenaRirekiHyojunB.UpdateAtenaRirekiHyojunB(csAtenaRirekiHyojunRow);
                    }
                    else
                    {
                        intCount = m_cABAtenaRirekiHyojunB.InsertAtenaRirekiHyojunB(csAtenaRirekiHyojunRow);
                    }
                    // 宛名履歴付随標準
                    if (blnRirekiFZYHyojunUpdate)
                    {
                        intCount = m_cABAtenaRirekiFZYHyojunB.UpdateAtenaRirekiFZYHyojunB(csAtenaRirekiFZYHyojunRow);
                    }
                    else
                    {
                        intCount = m_cABAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaRirekiFZYHyojunRow);
                    }
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

        }
        #endregion

        // *履歴番号 000065 2024/04/02 追加開始
        #region 個人制御マスタの更新
        // ************************************************************************************************
        // * メソッド名     個人制御マスタの更新
        // * 
        // * 構文           Public Function UpdateKojinSeigyo(ByVal cABKariTorokuPrm As ABKariTorokuParamXClass) As Integer
        // * 
        // * 機能　　    　 個人制御マスタの更新を行う
        // * 
        // * 引数           cdrJukiData：住基データ
        // * 
        // * 戻り値         更新件数：Integer
        // ************************************************************************************************
        public int UpdateKojinSeigyo(DataRow cdrJukiData)
        {
            const string THIS_METHOD_NAME = "UpdateKojinSeigyo";          // メソッド名
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            string strSeinenHikokenninGaitoUmu;           // 成年被後見人_該当有無
            string strSeinenHikokenninShinpanKakuteiYMD;  // 成年被後見人_審判確定日
            string strSeinenHikokenninTokiYMD;            // 成年被後見人の登記日
            string strSeinenHikokenninShittaYMD;          // 成年被後見人である旨を知った日
            string[] strJukiReserve4;                     // 住基データ_リザーブ4をセパレータで分割して保持
            DataSet cdsKojinseigyo;                       // 個人制御マスタDataSet
            DataSet cdsKojinseigyoRrk;                    // 個人制御履歴DataSet
            DataRow cdrKojinseigyoRow;                    // 個人制御マスタDataRow
            DataRow cdrKojinseigyoRrkRow;                 // 個人制御履歴DataRow
                                                          // カラム情報
            DataRow[] csSortDataRow;                      // 履歴番号取得用DataRow
            int intKoshinCnt = 0;                     // 個人制御マスタ更新件数
            int intRrkKoshinCnt = 0;                  // 個人制御履歴更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // 引数.住基データから必要項目を取得
                strJukiReserve4 = cdrJukiData.Item(ABJukiData.JUKIRESERVE4).ToString.Split('^');
                if (strJukiReserve4.Count() == 4)
                {
                    strSeinenHikokenninGaitoUmu = strJukiReserve4[0];
                    strSeinenHikokenninShinpanKakuteiYMD = strJukiReserve4[1];
                    strSeinenHikokenninTokiYMD = strJukiReserve4[2];
                    strSeinenHikokenninShittaYMD = strJukiReserve4[3];
                }
                else
                {
                    strSeinenHikokenninGaitoUmu = "0";
                    strSeinenHikokenninShinpanKakuteiYMD = string.Empty;
                    strSeinenHikokenninTokiYMD = string.Empty;
                    strSeinenHikokenninShittaYMD = string.Empty;
                }


                // 個人制御データを取得
                cdsKojinseigyo = m_cABKojinSeigyoB.GetABKojinSeigyo(cdrJukiData.Item(ABJukiData.JUMINCD).ToString);

                // 個人制御履歴データを取得
                cdsKojinseigyoRrk = m_cABKojinseigyoRirekiB.GetKojinseigyoRireki(cdrJukiData.Item(ABJukiData.JUMINCD).ToString);

                // 個人制御情報の更新
                if (cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows.Count == 0)
                {
                    // 取得した個人制御マスタのデータが0件の場合

                    if (strSeinenHikokenninGaitoUmu == "1")
                    {
                        // 成年被後見人_該当有無の値が"1"（有）の場合

                        cdrKojinseigyoRow = cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).NewRow;

                        cdrKojinseigyoRow.BeginEdit();

                        foreach (DataColumn csColumn in cdrKojinseigyoRow.Table.Columns)
                        {
                            if ((csColumn.DataType.Name ?? "") == (typeof(decimal).Name ?? ""))
                            {

                                cdrKojinseigyoRow[csColumn] = 0;
                            }

                            else
                            {

                                cdrKojinseigyoRow[csColumn] = string.Empty;

                            }

                        }

                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JUMINCD) = cdrJukiData.Item(ABJukiData.JUMINCD).ToString;                         // 住民コード
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHICHOSONCD) = cdrJukiData.Item(ABJukiData.SHICHOSONCD).ToString;                 // 市町村コード
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KYUSHICHOSONCD) = cdrJukiData.Item(ABJukiData.KYUSHICHOSONCD).ToString;           // 旧市町村コード
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = "1";                                                  // 成年後見区分
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = m_strSeinenKoKenShokiMsg;                            // 成年後見メッセージ
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = cdrJukiData.Item(ABJukiData.CKINIDOYMD).ToString;         // 成年後見開始日
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = "99999999";                                    // 成年後見終了日
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = strSeinenHikokenninShinpanKakuteiYMD;  // 成年被後見人の審判確定日
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = strSeinenHikokenninTokiYMD;                 // 成年被後見人の登記日
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = strSeinenHikokenninShittaYMD;             // 成年被後見人である旨を知った日

                        cdrKojinseigyoRow.EndEdit();

                        intKoshinCnt = m_cABKojinSeigyoB.InsertKojinSeigyo(cdrKojinseigyoRow);

                        if (intKoshinCnt == 0)
                        {
                            // 更新件数が0件の場合   
                            // エラー定義を取得
                            cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode);
                        }
                    }
                    else
                    {
                        return intKoshinCnt;
                    }
                }
                else
                {
                    cdrKojinseigyoRow = cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows(0);
                    if (cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString == "1" && strSeinenHikokenninGaitoUmu == "0" || cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString.Trim == string.Empty && strSeinenHikokenninGaitoUmu == "1" || new string(cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD).ToString ?? new char[0]) != (strSeinenHikokenninShinpanKakuteiYMD ?? "") || new string(cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD).ToString ?? new char[0]) != (strSeinenHikokenninTokiYMD ?? "") || new string(cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD).ToString ?? new char[0]) != (strSeinenHikokenninShittaYMD ?? ""))
                    {

                        if (strSeinenHikokenninGaitoUmu == "1")
                        {
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = "1";                                                  // 成年被後見区分
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = m_strSeinenKoKenShokiMsg;                            // 成年後見メッセージ
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = cdrJukiData.Item(ABJukiData.CKINIDOYMD).ToString;         // 成年後見開始日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = "99999999";                                    // 成年後見終了日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = strSeinenHikokenninShinpanKakuteiYMD;  // 成年被後見人の審判確定日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = strSeinenHikokenninTokiYMD;                 // 成年被後見人の登記日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = strSeinenHikokenninShittaYMD;             // 成年被後見人である旨を知った日
                        }

                        else
                        {
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = string.Empty;                                         // 成年被後見区分
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = string.Empty;                                        // 成年後見メッセージ
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = string.Empty;                                  // 成年後見開始日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = string.Empty;                                  // 成年後見終了日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = string.Empty;                          // 成年被後見人の審判確定日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = string.Empty;                               // 成年被後見人の登記日
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = string.Empty;
                        }                             // 成年被後見人である旨を知った日

                        intKoshinCnt = m_cABKojinSeigyoB.UpdateKojinSeigyo(cdrKojinseigyoRow);

                        if (intKoshinCnt == 0)
                        {
                            // 更新件数が0件の場合   
                            // エラー定義を取得
                            cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001048);
                            throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode);
                        }
                    }
                    else
                    {
                        return intKoshinCnt;
                    }
                }

                // 個人制御履歴の更新
                cdrKojinseigyoRrkRow = cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).NewRow;

                cdrKojinseigyoRrkRow.BeginEdit();

                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JUMINCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JUMINCD).ToString;                                              // 住民コード
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHICHOSONCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHICHOSONCD).ToString;                                      // 市町村コード
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KYUSHICHOSONCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KYUSHICHOSONCD).ToString;                                // 旧市町村コード
                if (cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Rows.Count == 0)                                                                                     // 履歴番号
                {
                    cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKINO) = 1;
                }
                else
                {
                    csSortDataRow = cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Select(string.Empty, ABKojinseigyoRirekiEntity.RIREKINO + " DESC, " + ABKojinseigyoRirekiEntity.RIREKIEDABAN + " DESC ");
                    cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKINO) = Conversions.ToInteger(csSortDataRow[0].Item(ABKojinseigyoRirekiEntity.RIREKINO).ToString()) + 1;
                }
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKIEDABAN) = 0;                                                                                                       // 履歴枝番
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOKB).ToString;                                        // ＤＶ対象区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOMSG).ToString;                                      // ＤＶ対象メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOSHINSEIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD).ToString;                        // ＤＶ対象申請日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOKAISHIYMD).ToString;                          // ＤＶ対象開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOSHURYOYMD).ToString;                          // ＤＶ対象終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIKB).ToString;                                  // 発行停止区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIMSG).ToString;                                // 発行停止メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD).ToString;                    // 発行停止開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHISHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD).ToString;                    // 発行停止終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAKB).ToString;                                  // 実態調査区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAMSG).ToString;                                // 実態調査メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD).ToString;                    // 実態調査開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSASHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD).ToString;                    // 実態調査終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString;                                  // 成年後見区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG).ToString;                                // 成年後見メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD).ToString;                    // 成年後見開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD).ToString;                    // 成年後見終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD).ToString;    // 成年被後見人の審判確定日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINTOKIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD).ToString;              // 成年被後見人の登記日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINSHITTAYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD).ToString;          // 成年被後見人である旨を知った日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUKB).ToString;                                    // 仮登録中区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUMSG).ToString;                                  // 仮登録中メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD).ToString;                      // 仮登録中開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD).ToString;                      // 仮登録中終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIKB).ToString;                            // 特別養子区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG).ToString;                          // 特別養子メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD).ToString;              // 特別養子開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHISHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD).ToString;              // 特別養子終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOKB).ToString;                              // 特別事情区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOMSG).ToString;                            // 特別事情メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD).ToString;                // 特別事情開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD).ToString;                // 特別事情終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1KB).ToString;                                    // 処理注意1区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1MSG).ToString;                                  // 処理注意1メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD).ToString;                      // 処理注意1開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD).ToString;                      // 処理注意1終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2KB).ToString;                                    // 処理注意2区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2MSG).ToString;                                  // 処理注意2メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD).ToString;                      // 処理注意2開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD).ToString;                      // 処理注意2終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUCD_CHUI) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUCD_CHUI).ToString;                                    // 業務コード注意
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_CHUI) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI).ToString;                        // 業務詳細（税目）コード注意
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3KB).ToString;                                    // 処理注意3区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3MSG).ToString;                                  // 処理注意3メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD).ToString;                      // 処理注意3開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD).ToString;                      // 処理注意3終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUKB).ToString;                                    // 処理保留区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUMSG).ToString;                                  // 処理保留メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD).ToString;                      // 処理保留開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD).ToString;                      // 処理保留終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUCD_HORYU) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUCD_HORYU).ToString;                                  // 業務コード保留
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_HORYU) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU).ToString;                      // 業務詳細（税目）コード保留
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAKB).ToString;                                    // 他業務不可区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAMSG).ToString;                                  // 他業務不可メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD).ToString;                      // 他業務不可開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKASHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD).ToString;                      // 他業務不可終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKATOROKUGYOMUCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD).ToString;              // 登録業務コード
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1KB).ToString;                                          // その他１区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1MSG).ToString;                                        // その他１メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1KAISHIYMD).ToString;                            // その他１開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1SHURYOYMD).ToString;                            // その他１終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2KB).ToString;                                          // その他２区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2MSG).ToString;                                        // その他２メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2KAISHIYMD).ToString;                            // その他２開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2SHURYOYMD).ToString;                            // その他２終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3KB).ToString;                                          // その他３区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3MSG).ToString;                                        // その他３メッセージ
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3KAISHIYMD).ToString;                            // その他３開始日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3SHURYOYMD).ToString;                            // その他３終了日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KINSHIKAIJOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KINSHIKAIJOKB).ToString;                                  // 禁止解除区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SETAIYOKUSHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SETAIYOKUSHIKB).ToString;                                // 世帯抑止区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD).ToString;                            // 一時解除開始年月日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTTIME) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME).ToString;                          // 一時解除開始時刻
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD).ToString;                            // 一時解除終了年月日
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDTIME) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME).ToString;                          // 一時解除終了時刻
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOUSER) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOUSER).ToString;                              // 一時解除設定操作者ID
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KANRIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KANRIKB).ToString;                                              // 管理区分
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.BIKO) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.BIKO).ToString;                                                    // 備考
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RESERVE) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.RESERVE).ToString;                                              // リザーブ

                cdrKojinseigyoRrkRow.EndEdit();

                intRrkKoshinCnt = m_cABKojinseigyoRirekiB.InsertKojinseigyoRireki(cdrKojinseigyoRrkRow);

                if (intRrkKoshinCnt == 0)
                {
                    // 更新件数が0件の場合   
                    // エラー定義を取得
                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYORIREKI, objErrorStruct.m_strErrorCode);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, objRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, exAppException.Message);
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return intKoshinCnt;

        }
        #endregion
        // *履歴番号 000065 2024/04/02 追加終了

    }
}