// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名履歴マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/10　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/02/25 000001     データ区分がある時も、データ種別が入っている場合は、データ種別も検索とする
// * 2003/03/10 000002     住所ＣＤ等の整合性チェックに誤り
// * 2003/03/17 000003     エラーメッセージの誤り
// * 2003/03/27 000004     エラー処理クラスの参照先を"AB"固定にする
// * 2003/03/31 000005     整合性チェックをTrimした値でチェックする
// * 2003/04/11 000006     宛名履歴取得で、期日年月日=99999999を許す
// * 2003/04/16 000007     生和暦年月日の日付チェックを数値チェックに変更
// *                       検索用カナの半角カナチェックをＡＮＫチェックに変更
// * 2003/04/30 000008     指定日が無くてもエラーにしない。
// * 2003/05/20 000009     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/06/12 000010     TOP句を外す
// * 2003/08/28 000011     RDBアクセスログの修正
// * 2003/09/11 000012     端末ＩＤ整合性チェックをANKにする
// * 2003/10/09 000013     作成ユーザー・更新ユーザーチェックの変更
// * 2003/10/30 000014     仕様変更、カタカナチェックをANKチェックに変更
// * 2003/11/18 000015     仕様変更：データ区分で個人のみ持ってくる。（データ区分に"1%"と指定された場合）
// *                       仕様追加：宛名個別データ取得メソッドを追加
// * 2004/04/12 000016     仕様変更：直近事由チェックをコメントアウトに修正
// *          　　         地区コードをANKチェックに変更
// * 2004/10/19 000017     ～全国住所コードのチェックをCheckNumber --> CheckANK(マルゴ村山)
// * 2004/11/12 000018     データチェックを行なわない
// * 2005/01/25 000019     速度改善２：（宮沢）
// * 2005/06/15 000020     SQL文をInsert,Update,論理Delete,物理Deleteの各メソッドが呼ばれた時に各自作成する
// * 2005/06/17 000021     履歴番号のみを取得するメソッド追加
// * 2005/11/18 000022     住民ＣＤ指定(１住民ＣＤ）で該当住民ＣＤの全履歴データを削除する処理を追加(マルゴ村山)
// * 2005/12/26 000023     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
// * 2006/07/31 000024     年金宛名ゲットⅡ項目追加(吉澤)
// * 2007/04/28 000025     介護版宛名取得メソッドの追加による取得項目の追加 (吉澤)
// * 2007/09/04 000026     外国人本名優先検索用に漢字名称２を追加（中沢）
// * 2007/10/10 000027     外国人本名優先検索が可能な市町村は、カナ名の先頭が"ｳ"のときは"ｵ"とOR条件で検索する（中沢）
// * 2008/01/17 000028     個別事項データ取得機能に後期高齢取得処理を追加（比嘉）＆ネーミング変更（吉澤）
// * 2010/04/16 000029     VS2008対応（比嘉）
// * 2010/05/14 000030     本籍筆頭者及び処理停止区分対応（比嘉）
// * 2011/05/18 000031     外国人在留情報取得区分対応（比嘉）
// * 2011/10/24 000032     【AB17010】＜住基法改正対応＞宛名履歴付随マスタ追加   (小松)
// * 2014/04/28 000033     【AB21040】＜共通番号対応＞共通番号マスタ追加（石合）
// * 2014/06/05 000034     【AB21040-00】＜共通番号対応＞個別取得メソッドの対応漏れ改修（石合）
// * 2015/05/08 000035     【AB21052】＜共通番号対応＞個人番号一斉取得履歴取得メソッド追加（岩下）
// * 2020/01/10 000036     【AB32001】アルファベット検索（石合）
// * 2023/03/10 000037     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2023/08/14 000038     【AB-0820-1】住登外管理項目追加(早崎)
// * 2023/10/19 000039     【AB-0820-1】住登外管理項目追加_追加修正(仲西)
// * 2023/12/04 000040     【AB-1600-1】検索機能対応(下村)
// * 2023/12/07 000041     【AB-9000-1】住基更新連携標準化対応(下村)
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 宛名履歴マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRirekiBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
                                                              // * 履歴番号 000022 2005/11/18 追加開始
        private string m_strDelFromJuminCDSQL;                // 物理削除用SQL(１住民コード指定)
                                                              // * 履歴番号 000022 2005/11/18 追加終了
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
                                                                                      // * 履歴番号 000022 2005/11/18 追加開始
        private UFParameterCollectionClass m_cfDelFromJuminCDPrmCollection;           // 物理削除用SQL(１住民コード指定)
                                                                                      // * 履歴番号 000022 2005/11/18 追加終了

        // * 履歴番号 000019 2005/01/25 追加開始（宮沢）
        private StringBuilder m_strAtenaSQLsbAll = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbKaniAll = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbKaniOnly = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbNenkinAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbKaniAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbKaniOnly = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbNenkinAll = new StringBuilder();
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKobetu;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaAll;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKaniAll;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKaniOnly;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaNenkinAll;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKobetuAll;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKobetuKaniAll;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKobetuKaniOnly;   // スキーマ保管用データセット
        private DataSet m_csDataSchmaKobetuNenkinAll;   // スキーマ保管用データセット
        public ABEnumDefine.AtenaGetKB m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll; // 全項目選択（m_blnAtenaGetがTrueの時宛名Getで必要な項目全てそれ以外はSELECT *）
        public bool m_blnSelectCount = false;            // カウントを取得するかどうか
        public bool m_blnBatch = false;               // バッチフラグ
                                                      // * 履歴番号 000019 2005/01/25 追加終了（宮沢）

        // *履歴番号 000025 2007/04/28 追加開始
        public ABEnumDefine.MethodKB m_blnMethodKB;  // メソッド区分（通常版か、介護版、、、）
                                                     // *履歴番号 000025 2007/04/28 追加終了

        // *履歴番号 000028 2008/01/17 追加開始
        public string m_strKobetsuShutokuKB;                  // 個別事項取得区分
                                                              // *履歴番号 000028 2008/01/17 追加終了

        // *履歴番号 000030 2010/05/14 追加開始
        private string m_strHonsekiHittoshKB = string.Empty;          // 本籍筆頭者取得区分(宛名管理情報)
        private string m_strShoriteishiKB = string.Empty;             // 処理停止区分取得区分(宛名管理情報)
        private string m_strHonsekiHittoshKB_Param = string.Empty;    // 本籍筆頭者取得区分パラメータ
        private string m_strShoriTeishiKB_Param = string.Empty;       // 処理停止区分取得区分パラメータ
                                                                      // *履歴番号 000030 2010/05/14 追加終了

        // *履歴番号 000031 2011/05/18 追加開始
        private string m_strFrnZairyuJohoKB_Param = string.Empty;     // 外国人在留情報取得区分パラメータ
                                                                      // *履歴番号 000031 2011/05/18 追加終了

        // *履歴番号 000032 2011/10/24 追加開始
        private ABSekoYMDHanteiBClass m_csSekoYMDHanteiB;             // 施行日判定Bｸﾗｽ
        private ABAtenaRirekiFZYBClass m_csAtenaRirekiFZYB;                // 宛名付随マスタBｸﾗｽ
        private bool m_blnJukihoKaiseiFG = false;
        private string m_strJukihoKaiseiKB;                           // 住基法改正区分
                                                                      // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000033 2014/04/28 追加開始
        private string m_strMyNumberKB_Param;                         // 共通番号取得区分
        private string m_strMyNumberChokkinSearchKB_Param;            // 共通番号直近検索区分
                                                                      // *履歴番号 000033 2014/04/28 追加終了

        // *履歴番号 000036 2020/01/10 追加開始
        private ABKensakuShimeiBClass m_cKensakuShimeiB;              // 検索氏名編集ビジネスクラス
                                                                      // *履歴番号 000036 2020/01/10 追加終了

        // *履歴番号 000038 2023/08/14 追加開始
        private ABAtenaRireki_HyojunBClass m_csAtenaRirekiHyojunB;            // 宛名履歴_標準マスタBｸﾗｽ
        private ABAtenaRirekiFZY_HyojunBClass m_csAtenaRirekiFZYHyojunB;      // 宛名履歴付随_標準マスタBｸﾗｽ
                                                                              // *履歴番号 000038 2023/08/14 追加終了

        public ABEnumDefine.HyojunKB m_intHyojunKB;                   // 宛名GET標準化区分

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRirekiBClass";                 // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード

        private const string JUKIHOKAISEIKB_ON = "1";

        #endregion

        // *履歴番号 000030 2010/05/14 追加開始
        #region プロパティ
        public string p_strHonsekiHittoshKB     // 本籍筆頭者取得区分
        {
            set
            {
                m_strHonsekiHittoshKB_Param = value;
            }
        }
        public string p_strShoriteishiKB        // 処理停止区分取得区分
        {
            set
            {
                m_strShoriTeishiKB_Param = value;
            }
        }

        // *履歴番号 000031 2011/05/18 追加開始
        public string p_strFrnZairyuJohoKB      // 外国人在留資格情報取得区分
        {
            set
            {
                m_strFrnZairyuJohoKB_Param = value;
            }
        }
        // *履歴番号 000031 2011/05/18 追加終了

        // *履歴番号 000032 2011/10/24 追加開始
        public string p_strJukihoKaiseiKB      // 住基法改正区分
        {
            set
            {
                m_strJukihoKaiseiKB = value;
            }
        }
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000033 2014/04/28 追加開始
        public string p_strMyNumberKB                     // 共通番号取得区分
        {
            get
            {
                return m_strMyNumberKB_Param;
            }
            set
            {
                m_strMyNumberKB_Param = value;
            }
        }
        // *履歴番号 000033 2014/04/28 追加終了

        #endregion
        // *履歴番号 000030 2010/05/14 追加終了

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaRirekiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDelRonriSQL = string.Empty;
            m_strDelButuriSQL = string.Empty;
            // * 履歴番号 000022 2005/11/18 追加開始
            m_strDelFromJuminCDSQL = string.Empty;
            // * 履歴番号 000022 2005/11/18 追加終了
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;
            m_cfDelButuriUFParameterCollectionClass = (object)null;
            // * 履歴番号 000022 2005/11/18 追加開始
            m_cfDelFromJuminCDPrmCollection = (object)null;
            // * 履歴番号 000022 2005/11/18 追加終了

            // *履歴番号 000032 2011/10/24 追加開始
            // 住基法改正区分初期化
            m_strJukihoKaiseiKB = string.Empty;
            // 住基法改正ﾌﾗｸﾞ取得
            GetJukihoKaiseiFG();
            // *履歴番号 000032 2011/10/24 追加終了

            // *履歴番号 000033 2014/04/28 追加開始
            // 共通番号取得区分初期化
            m_strMyNumberKB_Param = string.Empty;
            // 共通番号　宛名取得　直近検索区分取得
            GetMyNumberChokkinSearchKB();
            // *履歴番号 000033 2014/04/28 追加終了

            // *履歴番号 000036 2020/01/10 追加開始
            // 検索氏名編集ビジネスクラスのインスタンス化
            m_cKensakuShimeiB = new ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass);
            // *履歴番号 000036 2020/01/10 追加終了

        }
        // * 履歴番号 000019 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdbClass As UFRdbClass)
        // * 　　                          ByVal blnSelectAll As Boolean, _
        // * 　　                          ByVal blnAtenaGet As Boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           blnSelectAll As Boolean                : データベースアクセス用オブジェクト
        // * 　　           blnAtenaGet As Boolean                 : データベースアクセス用オブジェクト
        // *                フラグの組み合わせ説明
        // *                            blnSelectAll binAtenaGet
        // *                              True         True       宛名Get専用の項目を全て取得（代納人、送付先、連絡先件数を含む）
        // *                              True         False      宛名項目を全て読み込む（現行の読み方）（代納人、送付先、連絡先件数を含まない）（デフォルト設定）
        // *                              False        True       宛名Get専用の項目で簡易的な項目のみ（代納人、送付先、連絡先件数を含む）
        // *                              False        False      簡易的な項目のみ（代納人、送付先、連絡先件数を含まない）
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaRirekiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass, ABEnumDefine.AtenaGetKB blnSelectAll, bool blnSelectCount)



        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDelRonriSQL = string.Empty;
            m_strDelButuriSQL = string.Empty;
            // * 履歴番号 000022 2005/11/18 追加開始
            m_strDelFromJuminCDSQL = string.Empty;
            // * 履歴番号 000022 2005/11/18 追加終了
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;
            m_cfDelButuriUFParameterCollectionClass = (object)null;
            // * 履歴番号 000022 2005/11/18 追加開始
            m_cfDelFromJuminCDPrmCollection = (object)null;
            // * 履歴番号 000022 2005/11/18 追加終了
            m_blnSelectAll = blnSelectAll;
            m_blnSelectCount = blnSelectCount;

            // *履歴番号 000030 2010/05/14 追加開始
            // 管理情報取得処理
            GetKanriJoho();
            // *履歴番号 000030 2010/05/14 追加終了

            // *履歴番号 000032 2011/10/24 追加開始
            // 住基法改正区分初期化
            m_strJukihoKaiseiKB = string.Empty;

            // 住基法改正ﾌﾗｸﾞ取得
            GetJukihoKaiseiFG();
            // *履歴番号 000032 2011/10/24 追加終了

            // *履歴番号 000033 2014/04/28 追加開始
            // 共通番号取得区分初期化
            m_strMyNumberKB_Param = string.Empty;
            // 共通番号　宛名取得　直近検索区分取得
            GetMyNumberChokkinSearchKB();
            // *履歴番号 000033 2014/04/28 追加終了

            // *履歴番号 000036 2020/01/10 追加開始
            // 検索氏名編集ビジネスクラスのインスタンス化
            m_cKensakuShimeiB = new ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass);
            // *履歴番号 000036 2020/01/10 追加終了

        }
        // * 履歴番号 000019 2005/01/25 追加終了（宮沢）
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // *                  cSearchKey    : 宛名履歴マスタ検索キー
        // *                  strKikanYMD   : 期間年月日
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRBHoshu(int intGetCount, ABAtenaSearchKey cSearchKey, string strKikanYMD)

        {
            return GetAtenaRBHoshu(intGetCount, cSearchKey, strKikanYMD, false);
        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String, _
        // *                                                ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名履歴マスタより該当データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // * 　　           cSearchKey    : 宛名履歴マスタ検索キー
        // * 　　           strKikanYMD   : 期間年月日
        // * 　　           blnSakujoKB   : 削除区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRBHoshu(int intGetCount, ABAtenaSearchKey cSearchKey, string strKikanYMD, bool blnSakujoFG)


        {
            const string THIS_METHOD_NAME = "GetAtenaRBHoshu";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();
            string strWHERE;
            StringBuilder strORDER;
            int intMaxRows;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                // 引数のチェックを行なう

                // 取得件数のチェック
                if (intGetCount < 0 | intGetCount > 999)                // 取得件数の誤り
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }


                // 期日年月日のチェック
                if (!(strKikanYMD == "99999999" | string.IsNullOrEmpty(strKikanYMD)))
                {
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                    m_cfDateClass.p_strDateValue = strKikanYMD;
                    if (!m_cfDateClass.CheckDate())
                    {
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                    }
                }

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成
                // * 修正番号 000010 2003/06/12 修正開始
                // If intGetCount = 0 Then
                // strSQL = "SELECT TOP 100 * FROM " + ABAtenaRirekiEntity.TABLE_NAME
                // Else
                // strSQL = "SELECT TOP " + intGetCount.ToString + " * FROM " + ABAtenaRirekiEntity.TABLE_NAME
                // End If

                // p_intMaxRowsを退避する
                intMaxRows = m_cfRdbClass.p_intMaxRows;
                if (intGetCount == 0)
                {
                    m_cfRdbClass.p_intMaxRows = 100;
                }
                else
                {
                    m_cfRdbClass.p_intMaxRows = intGetCount;
                }
                // *履歴番号 000011 2003/08/28 修正開始
                // strSQL = "SELECT * FROM " + ABAtenaRirekiEntity.TABLE_NAME

                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）
                // strSQL.Append("SELECT * FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                switch (m_blnSelectAll)
                {
                    case var @case when @case == ABEnumDefine.AtenaGetKB.KaniAll:
                        {
                            if (m_strAtenaSQLsbKaniAll.RLength == 0)
                            {
                                m_strAtenaSQLsbKaniAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strAtenaSQLsbKaniAll);
                                    SetFZYHyojunEntity(ref m_strAtenaSQLsbKaniAll);
                                    SetFugenjuEntity(ref m_strAtenaSQLsbKaniAll);
                                    SetDenshiShomeishoMSTEntity(ref m_strAtenaSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strAtenaSQLsbKaniAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                m_strAtenaSQLsbKaniAll.Append(" FROM ");
                                m_strAtenaSQLsbKaniAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strAtenaSQLsbKaniAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strAtenaSQLsbKaniAll);
                            if (m_csDataSchmaKaniAll is null)
                            {
                                m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniAll;
                            break;
                        }
                    case var case1 when case1 == ABEnumDefine.AtenaGetKB.KaniOnly:
                        {
                            if (m_strAtenaSQLsbKaniOnly.RLength == 0)
                            {
                                m_strAtenaSQLsbKaniOnly.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniOnly);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strAtenaSQLsbKaniOnly);
                                    SetFZYHyojunEntity(ref m_strAtenaSQLsbKaniOnly);
                                    SetFugenjuEntity(ref m_strAtenaSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTEntity(ref m_strAtenaSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strAtenaSQLsbKaniOnly);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                m_strAtenaSQLsbKaniOnly.Append(" FROM ");
                                m_strAtenaSQLsbKaniOnly.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strAtenaSQLsbKaniOnly);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strAtenaSQLsbKaniOnly);
                            if (m_csDataSchmaKaniOnly is null)
                            {
                                m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniOnly;
                            break;
                        }
                    case var case2 when case2 == ABEnumDefine.AtenaGetKB.NenkinAll:
                        {
                            if (m_strAtenaSQLsbNenkinAll.RLength == 0)
                            {
                                m_strAtenaSQLsbNenkinAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbNenkinAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strAtenaSQLsbNenkinAll);
                                    SetFZYHyojunEntity(ref m_strAtenaSQLsbNenkinAll);
                                    SetFugenjuEntity(ref m_strAtenaSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTEntity(ref m_strAtenaSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strAtenaSQLsbNenkinAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                m_strAtenaSQLsbNenkinAll.Append(" FROM ");
                                m_strAtenaSQLsbNenkinAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strAtenaSQLsbNenkinAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strAtenaSQLsbNenkinAll);
                            if (m_csDataSchmaNenkinAll is null)
                            {
                                m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaNenkinAll;
                            break;
                        }

                    default:
                        {
                            if (m_strAtenaSQLsbAll.RLength == 0)
                            {
                                m_strAtenaSQLsbAll.Append("SELECT ");
                                // 現行
                                m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*");

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strAtenaSQLsbAll);
                                    SetFZYHyojunEntity(ref m_strAtenaSQLsbAll);
                                    if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                                    {
                                        SetFugenjuEntity(ref m_strAtenaSQLsbAll);
                                        SetDenshiShomeishoMSTEntity(ref m_strAtenaSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strAtenaSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                m_strAtenaSQLsbAll.Append(" FROM ");
                                m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbAll);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbAll);
                                    if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                                    {
                                        SetFugenjuJoin(ref m_strAtenaSQLsbAll);
                                        SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strAtenaSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strAtenaSQLsbAll);
                            if (m_csDataSchmaAll is null)
                            {
                                m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaAll;
                            break;
                        }
                }
                // If (m_strAtenaSQLsb.Length = 0) Then
                // m_strAtenaSQLsb.Append("SELECT ")
                // Select Case (Me.m_blnSelectAll)
                // Case ABEnumDefine.AtenaGetKB.SelectAll
                // '現行
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // Case ABEnumDefine.AtenaGetKB.KaniAll
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case ABEnumDefine.AtenaGetKB.KaniOnly
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case Else
                // '現行
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // End Select

                // '代理人等のカウントを取得
                // Call SetAtenaCountEntity(m_strAtenaSQLsb)

                // m_strAtenaSQLsb.Append(" FROM ")
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)

                // '代理人等のカウントを取得
                // Call SetAtenaJoin(m_strAtenaSQLsb)
                // End If
                // strSQL.Append(m_strAtenaSQLsb)
                // '* 履歴番号 000019 2005/01/25 更新終了（宮沢）

                // '* 履歴番号 000014 2004/08/27 追加開始（宮沢）
                // If (m_csDataSchma Is Nothing) Then
                // m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
                // End If
                // * 履歴番号 000014 2004/08/27 追加終了

                // *履歴番号 000011 2003/08/28 修正終了
                // * 修正番号 000010 2003/06/12 修正終了

                // WHERE句の作成
                strWHERE = CreateWhere(cSearchKey, strKikanYMD);

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    if (!string.IsNullOrEmpty(strWHERE))
                    {
                        strWHERE += " AND ";
                    }
                    strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.SAKUJOFG + " <> '1'";
                }

                // ORDER句を結合
                strORDER = new StringBuilder();
                if (cSearchKey.p_strJuminYuseniKB == "1" & !(cSearchKey.p_strStaiCD == string.Empty))
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN);
                    strORDER.Append(" ASC;");
                }
                else if (!(cSearchKey.p_strUmareYMD == string.Empty))
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD);
                    strORDER.Append(" ASC,");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                    strORDER.Append(" ASC;");
                }
                else
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI);
                    strORDER.Append(" ASC,");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                    strORDER.Append(" ASC;");
                }

                // *履歴番号 000011 2003/08/28 修正開始
                // If strWHERE = String.Empty Then
                // strSQL += strORDER.ToString
                // Else
                // strSQL += " WHERE " + strWHERE + strORDER.ToString
                // End If

                if (!string.IsNullOrEmpty(strWHERE))
                {
                    strSQL.Append(" WHERE ").Append(strWHERE);
                }
                strSQL.Append(strORDER);
                // *履歴番号 000011 2003/08/28 修正終了

                // *履歴番号 000011 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // '* 履歴番号 000019 2005/01/25 更新開始（宮沢）If 文で囲む
                // If (m_blnBatch = False) Then
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
                // End If
                // '* 履歴番号 000019 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000011 2003/08/28 修正終了

                // *履歴番号 000011 2003/08/28 修正開始
                // ' SQLの実行 DataSetの取得
                // csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)

                // SQLの実行 DataSetの取得

                // * 履歴番号 000019 2005/01/25 追加開始（宮沢）
                // csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaRirekiEntity = m_csDataSchma.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // * 履歴番号 000019 2005/01/25 追加終了（宮沢）


                // *履歴番号 000011 2003/08/28 修正終了

                // MaxRows値を戻す
                m_cfRdbClass.p_intMaxRows = intMaxRows;

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


                // エラーをそのままスローする
                throw objExp;
            }

            return csAtenaRirekiEntity;

        }

        // *履歴番号 000015 2003/11/18 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String, _
        // *                                                ByVal strJuminJutogaiKB As String, _
        // *                                                ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名履歴マスタより該当データを取得する（住基データ更新用）
        // * 
        // * 引数           intGetCount   : 取得件数
        // * 　　           cSearchKey    : 宛名履歴マスタ検索キー
        // * 　　           strKikanYMD   : 期間年月日
        // * 　　           strJuminJutogaiKB : 住民住登外区分
        // * 　　           blnSakujoKB   : 削除区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        internal DataSet GetAtenaRBHoshu(int intGetCount, ABAtenaSearchKey cSearchKey, string strKikanYMD, string strJuminJutogaiKB, bool blnSakujoFG)



        {
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();
            string strWHERE;
            StringBuilder strORDER;
            int intMaxRows;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                // 引数のチェックを行なう

                // 取得件数のチェック
                if (intGetCount < 0 | intGetCount > 999)                // 取得件数の誤り
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }


                // 期日年月日のチェック
                if (!(strKikanYMD == "99999999" | string.IsNullOrEmpty(strKikanYMD)))
                {
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                    m_cfDateClass.p_strDateValue = strKikanYMD;
                    if (!m_cfDateClass.CheckDate())
                    {
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                    }
                }

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成

                // p_intMaxRowsを退避する
                intMaxRows = m_cfRdbClass.p_intMaxRows;
                if (intGetCount == 0)
                {
                    m_cfRdbClass.p_intMaxRows = 100;
                }
                else
                {
                    m_cfRdbClass.p_intMaxRows = intGetCount;
                }
                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）
                // strSQL.Append("SELECT * FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                switch (m_blnSelectAll)
                {
                    case var @case when @case == ABEnumDefine.AtenaGetKB.KaniAll:
                        {
                            if (m_strAtenaSQLsbKaniAll.RLength == 0)
                            {
                                m_strAtenaSQLsbKaniAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                m_strAtenaSQLsbKaniAll.Append(" FROM ");
                                m_strAtenaSQLsbKaniAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                            }
                            strSQL.Append(m_strAtenaSQLsbKaniAll);
                            if (m_csDataSchmaKaniAll is null)
                            {
                                m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniAll;
                            break;
                        }
                    case var case1 when case1 == ABEnumDefine.AtenaGetKB.KaniOnly:
                        {
                            if (m_strAtenaSQLsbKaniOnly.RLength == 0)
                            {
                                m_strAtenaSQLsbKaniOnly.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniOnly);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                m_strAtenaSQLsbKaniOnly.Append(" FROM ");
                                m_strAtenaSQLsbKaniOnly.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                            }
                            strSQL.Append(m_strAtenaSQLsbKaniOnly);
                            if (m_csDataSchmaKaniOnly is null)
                            {
                                m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniOnly;
                            break;
                        }
                    case var case2 when case2 == ABEnumDefine.AtenaGetKB.NenkinAll:
                        {
                            if (m_strAtenaSQLsbNenkinAll.RLength == 0)
                            {
                                m_strAtenaSQLsbNenkinAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbNenkinAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                m_strAtenaSQLsbNenkinAll.Append(" FROM ");
                                m_strAtenaSQLsbNenkinAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                            }
                            strSQL.Append(m_strAtenaSQLsbNenkinAll);
                            if (m_csDataSchmaNenkinAll is null)
                            {
                                m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaNenkinAll;
                            break;
                        }

                    default:
                        {
                            if (m_strAtenaSQLsbAll.RLength == 0)
                            {
                                m_strAtenaSQLsbAll.Append("SELECT ");
                                // 現行
                                m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*");

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                                m_strAtenaSQLsbAll.Append(" FROM ");
                                m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000033 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000033 2014/04/28 追加終了

                            }
                            strSQL.Append(m_strAtenaSQLsbAll);
                            if (m_csDataSchmaAll is null)
                            {
                                m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaAll;
                            break;
                        }
                }
                // If (m_strAtenaSQLsb.Length = 0) Then
                // m_strAtenaSQLsb.Append("SELECT ")
                // Select Case (Me.m_blnSelectAll)
                // Case ABEnumDefine.AtenaGetKB.SelectAll
                // '現行
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // Case ABEnumDefine.AtenaGetKB.KaniAll
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case ABEnumDefine.AtenaGetKB.KaniOnly
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case Else
                // '現行
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // End Select

                // '代理人等のカウントを取得
                // Call SetAtenaCountEntity(m_strAtenaSQLsb)

                // m_strAtenaSQLsb.Append(" FROM ")
                // m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)

                // '代理人等のカウントを取得
                // Call SetAtenaJoin(m_strAtenaSQLsb)
                // End If
                // strSQL.Append(m_strAtenaSQLsb)
                // If (m_csDataSchma Is Nothing) Then
                // m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
                // End If
                // * 履歴番号 000019 2005/01/25 更新終了（宮沢）

                // WHERE句の作成
                strWHERE = CreateWhere(cSearchKey, strKikanYMD);

                // 住民住登外区分
                if (!string.IsNullOrEmpty(strJuminJutogaiKB.Trim()))
                {
                    if (!string.IsNullOrEmpty(strWHERE))
                    {
                        strWHERE += " AND ";
                    }
                    strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = ";
                    strWHERE += ABAtenaRirekiEntity.PARAM_JUMINJUTOGAIKB;

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUMINJUTOGAIKB;
                    cfUFParameterClass.Value = strJuminJutogaiKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    if (!string.IsNullOrEmpty(strWHERE))
                    {
                        strWHERE += " AND ";
                    }
                    strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.SAKUJOFG + " <> '1'";
                }

                // ORDER句を結合
                strORDER = new StringBuilder();
                strORDER.Append(" ORDER BY ");
                strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RIREKINO);
                strORDER.Append(" DESC;");

                if (!string.IsNullOrEmpty(strWHERE))
                {
                    strSQL.Append(" WHERE ").Append(strWHERE);
                }
                strSQL.Append(strORDER);

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // '* 履歴番号 000019 2005/01/25 更新開始（宮沢）If 文で囲む
                // If (m_blnBatch = False) Then
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
                // End If
                // '* 履歴番号 000019 2005/01/25 更新終了（宮沢）If 文で囲む

                // SQLの実行 DataSetの取得
                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）
                // csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaRirekiEntity = m_csDataSchma.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // * 履歴番号 000019 2005/01/25 更新終了（宮沢）
                // MaxRows値を戻す
                m_cfRdbClass.p_intMaxRows = intMaxRows;

                // デバッグログ出力
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


                // エラーをそのままスローする
                throw objExp;
            }

            return csAtenaRirekiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名個別履歴データ抽出
        // * 
        // * 構文           Friend Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
        // *                                                  ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                  ByVal strKikanYMD As String, _
        // *                                                  ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名履歴マスタより該当データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // * 　　           cSearchKey    : 宛名履歴マスタ検索キー
        // * 　　           strKikanYMD   : 期間年月日
        // * 　　           blnSakujoKB   : 削除区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        // *履歴番号 000028 2008/01/17 修正開始
        // Friend Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
        // ByVal cSearchKey As ABAtenaSearchKey, _
        // ByVal strKikanYMD As String, _
        // ByVal blnSakujoFG As Boolean) As DataSet
        internal DataSet GetAtenaRBKobetsu(int intGetCount, ABAtenaSearchKey cSearchKey, string strKikanYMD, bool blnSakujoFG, string strKobetsuKB)



        {
            // *履歴番号 000028 2008/01/17 修正終了
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();
            StringBuilder strWHERE;
            StringBuilder strORDER;
            int intMaxRows;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                // 引数のチェックを行なう

                // 取得件数のチェック
                if (intGetCount < 0 | intGetCount > 999)                // 取得件数の誤り
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }


                // 期日年月日のチェック
                if (!(strKikanYMD == "99999999" | string.IsNullOrEmpty(strKikanYMD)))
                {
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                    m_cfDateClass.p_strDateValue = strKikanYMD;
                    if (!m_cfDateClass.CheckDate())
                    {
                        m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        // エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                    }
                }

                // *履歴番号 000028 2008/01/17 追加開始
                // 個別事項取得区分をメンバ変数にセット
                m_strKobetsuShutokuKB = strKobetsuKB.Trim();
                // *履歴番号 000028 2008/01/17 追加終了

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成

                // p_intMaxRowsを退避する
                intMaxRows = m_cfRdbClass.p_intMaxRows;
                if (intGetCount == 0)
                {
                    m_cfRdbClass.p_intMaxRows = 100;
                }
                else
                {
                    m_cfRdbClass.p_intMaxRows = intGetCount;
                }
                // SELECT ABATENA.*
                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）IF文で囲む
                // strSQL.Append("SELECT ").Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // ' , ABATENANENKIN.KSNENKNNO AS KSNENKNNO
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1)

                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2)

                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3)

                // ' 国保
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)

                // ' 印鑑
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB)

                // ' 選挙
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)

                // ' 児童手当
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM)

                // ' 介護
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
                // strSQL.Append(", ")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD)
                // strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)
                // '  FROM ABATENA 
                // strSQL.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)

                // ' LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
                // strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
                // strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // strSQL.Append("=")
                // strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)
                switch (m_blnSelectAll)
                {
                    case var @case when @case == ABEnumDefine.AtenaGetKB.KaniAll:
                        {
                            if (m_strKobetuSQLsbKaniAll.RLength == 0)
                            {
                                m_strKobetuSQLsbKaniAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbKaniAll);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbKaniAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strKobetuSQLsbKaniAll);
                                    SetFZYHyojunEntity(ref m_strKobetuSQLsbKaniAll);
                                    SetFugenjuEntity(ref m_strKobetuSQLsbKaniAll);
                                    SetDenshiShomeishoMSTEntity(ref m_strKobetuSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strKobetuSQLsbKaniAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                // FROM ABATENA 
                                m_strKobetuSQLsbKaniAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbKaniAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbKaniAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strKobetuSQLsbKaniAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strKobetuSQLsbKaniAll);
                            if (m_csDataSchmaKobetuKaniAll is null)
                            {
                                m_csDataSchmaKobetuKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniAll;
                            break;
                        }
                    case var case1 when case1 == ABEnumDefine.AtenaGetKB.KaniOnly:
                        {
                            if (m_strKobetuSQLsbKaniOnly.RLength == 0)
                            {
                                m_strKobetuSQLsbKaniOnly.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbKaniOnly);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbKaniOnly);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strKobetuSQLsbKaniOnly);
                                    SetFZYHyojunEntity(ref m_strKobetuSQLsbKaniOnly);
                                    SetFugenjuEntity(ref m_strKobetuSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTEntity(ref m_strKobetuSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strKobetuSQLsbKaniOnly);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                // FROM ABATENA 
                                m_strKobetuSQLsbKaniOnly.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbKaniOnly);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbKaniOnly);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strKobetuSQLsbKaniOnly);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strKobetuSQLsbKaniOnly);
                            if (m_csDataSchmaKobetuKaniOnly is null)
                            {
                                m_csDataSchmaKobetuKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniOnly;
                            break;
                        }
                    case var case2 when case2 == ABEnumDefine.AtenaGetKB.NenkinAll:
                        {
                            if (m_strKobetuSQLsbNenkinAll.RLength == 0)
                            {
                                m_strKobetuSQLsbNenkinAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbNenkinAll);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbNenkinAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strKobetuSQLsbNenkinAll);
                                    SetFZYHyojunEntity(ref m_strKobetuSQLsbNenkinAll);
                                    SetFugenjuEntity(ref m_strKobetuSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTEntity(ref m_strKobetuSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strKobetuSQLsbNenkinAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                // FROM ABATENA 
                                m_strKobetuSQLsbNenkinAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbNenkinAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbNenkinAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strKobetuSQLsbNenkinAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strKobetuSQLsbNenkinAll);
                            if (m_csDataSchmaKobetuNenkinAll is null)
                            {
                                m_csDataSchmaKobetuNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuNenkinAll;
                            break;
                        }

                    default:
                        {
                            if (m_strKobetuSQLsbAll.RLength == 0)
                            {
                                m_strKobetuSQLsbAll.Append("SELECT ");
                                // 現行
                                m_strKobetuSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*");
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunEntity(ref m_strKobetuSQLsbAll);
                                    SetFZYHyojunEntity(ref m_strKobetuSQLsbAll);
                                    if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                                    {
                                        SetFugenjuEntity(ref m_strKobetuSQLsbAll);
                                        SetDenshiShomeishoMSTEntity(ref m_strKobetuSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                    {
                                        SetMyNumberHyojunEntity(ref m_strKobetuSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                                // FROM ABATENA 
                                m_strKobetuSQLsbAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbAll);

                                // *履歴番号 000032 2011/10/24 追加開始
                                // 住基法改正以降は宛名履歴付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000032 2011/10/24 追加終了

                                // *履歴番号 000034 2014/06/05 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000034 2014/06/05 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbAll);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbAll);
                                    if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                                    {
                                        SetFugenjuJoin(ref m_strKobetuSQLsbAll);
                                        SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim.RLength > 0)
                                    {
                                        SetMyNumberHyojunJoin(ref m_strKobetuSQLsbAll);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    // 処理なし
                                }

                            }
                            strSQL.Append(m_strKobetuSQLsbAll);
                            if (m_csDataSchmaKobetuAll is null)
                            {
                                m_csDataSchmaKobetuAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuAll;
                            break;
                        }
                }
                // If (m_strKobetuSQLsb.Length = 0) Then
                // m_strKobetuSQLsb.Append("SELECT ")
                // Select Case (Me.m_blnSelectAll)
                // Case ABEnumDefine.AtenaGetKB.SelectAll
                // '現行
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // Case ABEnumDefine.AtenaGetKB.KaniAll
                // Call SetAtenaEntity(m_strKobetuSQLsb)
                // Case ABEnumDefine.AtenaGetKB.KaniOnly
                // Call SetAtenaEntity(m_strKobetuSQLsb)
                // Case Else
                // '現行
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                // End Select
                // ' , ABATENANENKIN.KSNENKNNO AS KSNENKNNO
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1)

                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2)

                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3)

                // ' 国保
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)

                // ' 印鑑
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB)

                // ' 選挙
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)

                // ' 児童手当
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM)

                // ' 介護
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
                // m_strKobetuSQLsb.Append(", ")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD)
                // m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)

                // '代理人等のカウントを取得
                // Call SetAtenaCountEntity(m_strKobetuSQLsb)

                // '  FROM ABATENA 
                // m_strKobetuSQLsb.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)

                // ' LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

                // ' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
                // m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
                // m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                // m_strKobetuSQLsb.Append("=")
                // m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)

                // '代理人等のカウントを取得
                // Call SetAtenaJoin(m_strKobetuSQLsb)
                // End If
                // strSQL.Append(m_strKobetuSQLsb)
                // If (m_csDataSchmaKobetu Is Nothing) Then
                // m_csDataSchmaKobetu = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
                // End If
                // * 履歴番号 000019 2005/01/25 更新終了（宮沢）IF文で囲む

                // WHERE句の作成
                strWHERE = new StringBuilder(CreateWhere(cSearchKey, strKikanYMD));

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    strWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SAKUJOFG);
                    strWHERE.Append(" <> '1'");
                }

                // ORDER句を結合
                strORDER = new StringBuilder();
                if (cSearchKey.p_strJuminYuseniKB == "1" & !(cSearchKey.p_strStaiCD == string.Empty))
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN);
                    strORDER.Append(" ASC;");
                }
                else if (!(cSearchKey.p_strUmareYMD == string.Empty))
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD);
                    strORDER.Append(" ASC,");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                    strORDER.Append(" ASC;");
                }
                else
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI);
                    strORDER.Append(" ASC,");
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                    strORDER.Append(" ASC;");
                }

                if (!string.IsNullOrEmpty(strWHERE.ToString()))
                {
                    strSQL.Append(" WHERE ").Append(strWHERE);
                }
                strSQL.Append(strORDER);

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // '* 履歴番号 000019 2005/01/25 更新開始（宮沢）If 文で囲む
                // If (m_blnBatch = False) Then
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
                // End If
                // '* 履歴番号 000019 2005/01/25 更新終了（宮沢）If 文で囲む

                // SQLの実行 DataSetの取得
                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）
                // csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaRirekiEntity = m_csDataSchmaKobetu.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // * 履歴番号 000019 2005/01/25 更新終了（宮沢）

                // MaxRows値を戻す
                m_cfRdbClass.p_intMaxRows = intMaxRows;

                // デバッグログ出力
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


                // エラーをそのままスローする
                throw objExp;
            }

            return csAtenaRirekiEntity;

        }

        // *履歴番号 000015 2003/11/18 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRB";
            // * corresponds to VS2008 Start 2010/04/16 000029
            // Dim csInstRow As DataRow
            // Dim csDataColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000029
            int intInsCnt;        // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000020 2005/06/15 修正開始
                    // Call CreateSQL(csDataRow)
                    CreateInsertSQL(csDataRow);
                    // * 履歴番号 000020 2005/06/15 修正終了
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow(ABAtenaRirekiEntity.SAKUJOFG) = "0";                               // 削除フラグ
                csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = decimal.Zero;                 // 更新カウンタ
                csDataRow(ABAtenaRirekiEntity.SAKUSEINICHIJI) = strUpdateDateTime;           // 作成日時
                csDataRow(ABAtenaRirekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;    // 作成ユーザー
                csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = strUpdateDateTime;            // 更新日時
                csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;     // 更新ユーザー


                // ' 当クラスのデータ整合性チェックを行う
                // For Each csDataColumn In csDataRow.Table.Columns
                // CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
                // Next csDataColumn


                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // *履歴番号 000011 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
                // *履歴番号 000011 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intInsCnt;

        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRB() As Integer
        // * 
        // * 機能　　    　 宛名履歴マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaFZYDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ追加を実行
                intCnt = InsertAtenaRB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiFZYB == null)
                    {
                        m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 作成日時、更新日時の同期
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名付随マスタ追加を実行
                    intCnt2 = m_csAtenaRirekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr);
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000038 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
        // *                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名履歴マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow          : 追加するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaHyojunDr As DataRow    : 追加するデータの含まれるDataRowオブジェクト（宛名履歴_標準）
        // * 　　           csAtenaFZYDr As DataRow       : 追加するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名履歴付随_標準）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ追加を実行
                intCnt = InsertAtenaRB(csAtenaDr);

                // 宛名履歴_標準が存在する場合
                if (!(csAtenaHyojunDr == null))
                {
                    // 宛名履歴_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiHyojunB == null)
                    {
                        m_csAtenaRirekiHyojunB = new ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 作成日時、更新日時の同期
                    csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                    csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名履歴_標準マスタ追加を実行
                    intCnt2 = m_csAtenaRirekiHyojunB.InsertAtenaRirekiHyojunB(csAtenaHyojunDr);

                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名履歴付随が存在する場合
                    if (!(csAtenaFZYDr == null))
                    {
                        // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYB == null)
                        {
                            m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYDr(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                        csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名付随マスタ追加を実行
                        intCnt3 = m_csAtenaRirekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr);
                    }

                    // 宛名履歴付随_標準が存在する場合
                    if (!(csAtenaFZYHyojunDr == null))
                    {
                        // 宛名履歴付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYHyojunB == null)
                        {
                            m_csAtenaRirekiFZYHyojunB = new ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI);
                        csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名付随_標準マスタ追加を実行
                        intCnt4 = m_csAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr);
                    }
                }

                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }
        // *履歴番号 000038 2023/08/14 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaRB";                     // パラメータクラス
                                                                                 // * corresponds to VS2008 Start 2010/04/16 000029
                                                                                 // Dim csDataColumn As DataColumn
                                                                                 // * corresponds to VS2008 End 2010/04/16 000029
            int intUpdCnt;                            // 更新件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000020 2005/06/15 修正開始
                    // Call CreateSQL(csDataRow)
                    CreateUpdateSQL(csDataRow);
                    // * 履歴番号 000020 2005/06/15 修正終了
                }

                // 共通項目の編集を行う
                csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                 // 端末ＩＤ
                csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER)) + 1m;       // 更新カウンタ
                csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff"); // 更新日時
                csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                   // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) == ABAtenaRirekiEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // *履歴番号 000016 2004/11/12 修正開始
                        // データ整合性チェック
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                        // *履歴番号 000016 2004/11/12 修正開始
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000011 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
                // *履歴番号 000011 2003/08/28 修正終了

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intUpdCnt;

        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRB() As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを更新する
        // * 
        // * 引数           csAtenaDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaFZYDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intInsCnt = 0;
            int intInsCnt2 = 0;

            const string THIS_METHOD_NAME = "UpdateAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intInsCnt = UpdateAtenaRB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiFZYB == null)
                    {
                        m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名履歴付随マスタ更新を実行
                    intInsCnt2 = m_csAtenaRirekiFZYB.UpdateAtenaFZYRB(csAtenaFZYDr);
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intInsCnt;

        }
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000038 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRB(ByVal csAtenaDr As DataRow, _
        // *                                              ByVal csAtenaHyojunDr As DataRow, _
        // *                                              ByVal csAtenaFZYDr As DataRow, _
        // *                                              ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを更新する
        // * 
        // * 引数           csAtenaDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaHyojunDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴_標準）
        // * 　　           csAtenaFZYDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名履歴付随_標準）
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)


        {

            int intInsCnt = 0;
            int intInsCnt2 = 0;
            int intInsCnt3 = 0;
            int intInsCnt4 = 0;

            const string THIS_METHOD_NAME = "UpdateAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intInsCnt = UpdateAtenaRB(csAtenaDr);

                // 宛名履歴_標準マスタが存在する場合
                if (!(csAtenaHyojunDr == null))
                {
                    // 宛名履歴_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiHyojunB == null)
                    {
                        m_csAtenaRirekiHyojunB = new ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名履歴_標準マスタ更新を実行
                    intInsCnt2 = m_csAtenaRirekiHyojunB.UpdateAtenaRirekiHyojunB(csAtenaHyojunDr);

                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {
                    // 宛名履歴付随マスタが存在する場合
                    if (!(csAtenaFZYDr == null))
                    {
                        // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYB == null)
                        {
                            m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名履歴付随マスタ更新を実行
                        intInsCnt3 = m_csAtenaRirekiFZYB.UpdateAtenaFZYRB(csAtenaFZYDr);

                    }

                    // 宛名履歴付随_標準マスタが存在する場合
                    if (!(csAtenaFZYHyojunDr == null))
                    {
                        // 宛名履歴付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYHyojunB == null)
                        {
                            m_csAtenaRirekiFZYHyojunB = new ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名履歴付随マスタ更新を実行
                        intInsCnt4 = m_csAtenaRirekiFZYHyojunB.UpdateAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr);

                    }
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intInsCnt;

        }
        // *履歴番号 000038 2023/08/14 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaRB";                     // パラメータクラス
                                                                                 // * corresponds to VS2008 Start 2010/04/16 000029
                                                                                 // Dim csDataColumn As DataColumn
                                                                                 // * corresponds to VS2008 End 2010/04/16 000029
            int intDelCnt;                            // 削除件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null | string.IsNullOrEmpty(m_strDelRonriSQL) | m_cfDelRonriUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000020 2005/06/15 修正開始
                    // CreateSQL(csDataRow)
                    CreateDeleteRonriSQL(csDataRow);
                    // * 履歴番号 000020 2005/06/15 修正終了
                }


                // 共通項目の編集を行う
                csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABAtenaRirekiEntity.SAKUJOFG) = "1";                                                                 // 削除フラグ
                csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER)) + 1m;               // 更新カウンタ
                csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // *履歴番号 000011 2003/08/28 修正開始
                // '作成済みのパラメータへ更新行から値を設定する。
                // For Each cfParam In m_cfUpdateUFParameterCollectionClass
                // 'キー項目は更新前の値で設定
                // If (cfParam.ParameterName.Substring(0, ABAtenaRirekiEntity.PREFIX_KEY.Length) = ABAtenaRirekiEntity.PREFIX_KEY) Then
                // m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
                // csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PREFIX_KEY.Length), _
                // DataRowVersion.Original).ToString()
                // Else
                // 'データ整合性チェック
                // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                // m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
                // End If
                // Next cfParam

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) == ABAtenaRirekiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // *履歴番号 000016 2004/11/12 修正開始
                        // データ整合性チェック
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                        // *履歴番号 000016 2004/11/12 修正終了
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }
                // *履歴番号 000011 2003/08/28 修正終了

                // *履歴番号 000011 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
                // *履歴番号 000011 2003/08/28 修正終了

                // *履歴番号 000011 2003/08/28 修正開始
                // ' SQLの実行
                // intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);
                // *履歴番号 000011 2003/08/28 修正終了

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intDelCnt;

        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ削除
        // * 
        // * 構文           Public Function UpdateAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名履歴マスタのデータを論理削除する
        // * 
        // * 引数           csAtenaDr As DataRow : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaFZYDr As DataRow : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaRB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intCnt = DeleteAtenaRB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiFZYB == null)
                    {
                        m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名履歴付随マスタ削除を実行
                    intCnt2 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr);
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }

        // *履歴番号 000038 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ削除
        // * 
        // * 構文           Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow, _
        // *                                             ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 引数           csAtenaDr As DataRow          : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaHyojunDr As DataRow    : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴_標準）
        // * 　　           csAtenaFZYDr As DataRow       : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow : 論理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随_標準）
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        // *履歴番号 000039 2023/10/19 修正開始
        // Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
        // ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        public int DeleteAtenaRB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)
        {
            // *履歴番号 000039 2023/10/19 修正終了
            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaRB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intCnt = DeleteAtenaRB(csAtenaDr);

                // 宛名履歴_標準マスタのデータが存在する場合、処理を行う
                if (!(csAtenaHyojunDr == null))
                {

                    // 宛名履歴_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiHyojunB == null)
                    {
                        m_csAtenaRirekiHyojunB = new ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                    // 宛名履歴_標準マスタ削除を実行
                    intCnt2 = m_csAtenaRirekiHyojunB.DeleteAtenaRirekiHyojunB(csAtenaHyojunDr);

                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名履歴付随マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYDr == null))
                    {

                        // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYB == null)
                        {
                            m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名履歴付随マスタ削除を実行
                        intCnt3 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr);

                    }

                    // 宛名履歴付随_標準マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYHyojunDr == null))
                    {

                        // 宛名履歴付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYHyojunB == null)
                        {
                            m_csAtenaRirekiFZYHyojunB = new ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI);

                        // 宛名履歴付随_標準マスタ削除を実行
                        intCnt4 = m_csAtenaRirekiFZYHyojunB.DeleteAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr);

                    }

                    // 処理なし
                }

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
                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }
        // *履歴番号 000038 2023/08/14 追加終了
        // *履歴番号 000032 2011/10/24 追加終了
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaRB(ByVal csDataRow As DataRow, _
        // *                                              ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaRB";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
                                                          // * corresponds to VS2008 Start 2010/04/16 000029
                                                          // Dim csDataColumn As DataColumn
                                                          // * corresponds to VS2008 End 2010/04/16 000029
            int intDelCnt;                            // 削除件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除区分のチェックを行う
                if (!(strSakujoKB == "D"))
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);

                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null | string.IsNullOrEmpty(m_strDelButuriSQL) | m_cfDelButuriUFParameterCollectionClass == null)
                {
                    // * 履歴番号 000020 2005/06/15 修正開始
                    // CreateSQL(csDataRow)
                    CreateDeleteButsuriSQL(csDataRow);
                    // * 履歴番号 000020 2005/06/15 修正終了
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollectionClass)
                {

                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) == ABAtenaRirekiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                }


                // *履歴番号 000011 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
                // *履歴番号 000011 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intDelCnt;

        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaRB() As Integer
        // * 
        // * 機能　　    　 宛名履歴マスタのデータを物理削除する
        // * 
        // * 引数           csAtenaDr As DataRow : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴）
        // * 　　           csAtenaFZYDr As DataRow : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // *                strSakujoKB As String ： 削除区分  
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(DataRow csAtenaDr, DataRow csAtenaFZYDr, string strSakujoKB)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intCnt = DeleteAtenaRB(csAtenaDr, strSakujoKB);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiFZYB == null)
                    {
                        m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 宛名履歴付随マスタ物理削除実行
                    intCnt2 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr, strSakujoKB);
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000038 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ物理削除
        // * 
        // * 構文           Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, _
        // *                                                        ByVal csAtenaHyojunDr As DataRow, _
        // *                                                        ByVal csAtenaFZYDr As DataRow, _
        // *                                                        ByVal csAtenaFZYHyojunDr As DataRow, _
        // *                                                        ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 宛名履歴マスタのデータを物理削除する
        // * 
        // * 引数           csAtenaDr As DataRow          : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴）
        // *                csAtenaHyojunDr As DataRow    : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴_標準）
        // *                csAtenaFZYDr As DataRow       : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随）
        // *                csAtenaFZYHyojunDr As DataRow : 物理削除するデータの含まれるDataRowオブジェクト（宛名履歴付随_標準）
        // *                strSakujoKB As String         : 削除区分  
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr, string strSakujoKB)



        {

            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaRB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名履歴マスタ更新を実行
                intCnt = DeleteAtenaRB(csAtenaDr, strSakujoKB);

                // 宛名履歴_標準マスタが存在すれば更新を実行
                if (!(csAtenaHyojunDr == null))
                {
                    // 宛名履歴_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRirekiHyojunB == null)
                    {
                        m_csAtenaRirekiHyojunB = new ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 宛名履歴_標準マスタ物理削除実行
                    intCnt2 = m_csAtenaRirekiHyojunB.DeleteAtenaHyojunRB(csAtenaHyojunDr, strSakujoKB);
                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名履歴付随マスタが存在する場合、更新する
                    if (!(csAtenaFZYDr == null))
                    {
                        // 宛名履歴付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYB == null)
                        {
                            m_csAtenaRirekiFZYB = new ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 宛名履歴付随マスタ物理削除実行
                        intCnt3 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr, strSakujoKB);
                    }

                    // 宛名履歴付随マスタが存在する場合、更新する
                    if (!(csAtenaFZYHyojunDr == null))
                    {
                        // 宛名履歴付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRirekiFZYHyojunB == null)
                        {
                            m_csAtenaRirekiFZYHyojunB = new ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 宛名履歴付随_標準マスタ物理削除実行
                        intCnt4 = m_csAtenaRirekiFZYHyojunB.DeleteAtenaFZYHyojunRB(csAtenaFZYHyojunDr, strSakujoKB);
                    }
                }
                else
                {
                    // 処理なし
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intCnt;

        }
        // *履歴番号 000038 2023/08/14 追加終了

        // * 履歴番号 000022 2005/11/18 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ物理削除(１住民コード指定)
        // * 
        // * 構文           Public Overloads Function DeleteAtenaRB(ByVal strJuminCD As String) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタのデータを物理削除する
        // * 
        // * 引数           strJuminCD As String : 削除する対象となる住民コード
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRB(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "DeleteAtenaRB";
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);


                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelFromJuminCDSQL is null || string.IsNullOrEmpty(m_strDelFromJuminCDSQL) || m_cfDelFromJuminCDPrmCollection == null)
                {
                    CreateDelFromJuminCDSQL();
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                this.m_cfDelFromJuminCDPrmCollection(ABAtenaRirekiEntity.KEY_JUMINCD).Value = strJuminCD;

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection) + "】")

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return intDelCnt;

        }

        // * 履歴番号 000022 2005/11/18 追加終了

        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // *                strKikanYMD As String : 期間年月日
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(ABAtenaSearchKey cSearchKey, string strKikanYMD)
        {
            const string THIS_METHOD_NAME = "CreateWhere";
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // WHERE句の作成
                // * 履歴番号 000019 2005/01/25 更新開始（宮沢）
                // csWHERE = New StringBuilder()
                csWHERE = new StringBuilder(256);
                // * 履歴番号 000019 2005/01/25 更新終了（宮沢）

                // 住民コード
                if (!(cSearchKey.p_strJuminCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000015 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaRirekiEntity.JUMINCD)
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                    // *履歴番号 000015 2003/11/18 修正終了
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住民優先区分
                if (!(cSearchKey.p_strJuminYuseniKB.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINYUSENIKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUMINYUSENIKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINYUSENIKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住登外優先区分
                if (!(cSearchKey.p_strJutogaiYusenKB.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTOGAIYUSENKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 世帯コード
                if (!(cSearchKey.p_strStaiCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_STAICD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_STAICD;
                    cfUFParameterClass.Value = cSearchKey.p_strStaiCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // *履歴番号 000036 2020/01/10 修正開始
                // '検索用カナ姓名
                // If Not (cSearchKey.p_strSearchKanaSeiMei.Trim = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If

                // If cSearchKey.p_strSearchKanaSeiMei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei
                // Else
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                // End If
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If

                // '検索用カナ姓
                // If Not (cSearchKey.p_strSearchKanaSei.Trim = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // '* 履歴番号 000027 2007/10/10 追加開始
                // ' 検索用カナ姓２に検索キーが格納されている場合は検索条件として追加
                // If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
                // csWHERE.Append(" ( ")
                // End If
                // '* 履歴番号 000027 2007/10/10 追加終了
                // If cSearchKey.p_strSearchKanaSei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // '* 履歴番号 000027 2007/10/10 追加開始
                // ' 検索用カナ姓２OR条件
                // ' 検索用カナ姓２に検索キーが格納されている場合は検索条件として追加
                // If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
                // csWHERE.Append(" OR ")
                // If cSearchKey.p_strSearchKanaSei2.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI2
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI2
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // ' OR条件は検索用カナ姓のみでの条件なので括弧で括る
                // csWHERE.Append(" ) ")
                // End If
                // '* 履歴番号 000027 2007/10/10 追加終了

                // '検索用カナ名
                // If Not (cSearchKey.p_strSearchKanaMei.Trim = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strSearchKanaMei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANAMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANAMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANAMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANAMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // '検索用漢字名称
                // If Not (cSearchKey.p_strSearchKanjiMeisho.Trim = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strSearchKanjiMeisho.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // '* 履歴番号 000026 2007/09/04 追加開始
                // ' 本名漢字姓名 本名検索="2(Tsusho_Seishiki)"のときのみ漢字氏名２は検索項目となる
                // If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                // If Not (cSearchKey.p_strKanjiMeisho2.Trim = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strKanjiMeisho2.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.PARAM_KANJIMEISHO2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                // cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.PARAM_KANJIMEISHO2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                // cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If
                // End If
                // '* 履歴番号 000026 2007/09/04 追加終了

                // 氏名検索条件を生成
                m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaRirekiEntity.TABLE_NAME, ref csWHERE, ref m_cfSelectUFParameterCollectionClass, ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, true, m_intHyojunKB);
                // *履歴番号 000036 2020/01/10 修正終了

                // 生年月日
                if (!(cSearchKey.p_strUmareYMD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strUmareYMD.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaRirekiEntity.KEY_UMAREYMD);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_UMAREYMD;
                        cfUFParameterClass.Value = cSearchKey.p_strUmareYMD;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaRirekiEntity.KEY_UMAREYMD);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_UMAREYMD;
                        cfUFParameterClass.Value = cSearchKey.p_strUmareYMD.TrimEnd;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // 性別
                if (!(cSearchKey.p_strSeibetsuCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_SEIBETSUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEIBETSUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strSeibetsuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住所コード
                if (!(cSearchKey.p_strJushoCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUSHOCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUSHOCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJushoCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 行政区コード
                if (!(cSearchKey.p_strGyoseikuCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_GYOSEIKUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_GYOSEIKUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strGyoseikuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード１
                if (!(cSearchKey.p_strChikuCD1.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD1;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード２
                if (!(cSearchKey.p_strChikuCD2.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD2;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード３
                if (!(cSearchKey.p_strChikuCD3.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD3;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード１
                if (!(cSearchKey.p_strBanchiCD1.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD1;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード２
                if (!(cSearchKey.p_strBanchiCD2.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD2;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード３
                if (!(cSearchKey.p_strBanchiCD3.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD3;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基住所コード
                if (!(cSearchKey.p_strJukiJushoCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIJUSHOCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIJUSHOCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiJushoCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基行政区コード
                if (!(cSearchKey.p_strJukiGyoseikuCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIGYOSEIKUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIGYOSEIKUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiGyoseikuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード１
                if (!(cSearchKey.p_strJukiChikuCD1.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD1;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード２
                if (!(cSearchKey.p_strJukiChikuCD2.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD2;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード３
                if (!(cSearchKey.p_strJukiChikuCD3.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD3;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード１
                if (!(cSearchKey.p_strJukiBanchiCD1.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD1;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード２
                if (!(cSearchKey.p_strJukiBanchiCD2.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD2;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード３
                if (!(cSearchKey.p_strJukiBanchiCD3.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD3;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // データ区分
                if (!(cSearchKey.p_strDataKB.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000015 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaRirekiEntity.ATENADATAKB)
                    // csWHERE.Append(" = ")
                    // csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB)

                    if (cSearchKey.p_strDataKB.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB);
                    }
                    // 検索条件のパラメータを作成
                    else
                    {
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB);
                        // 検索条件のパラメータを作成
                    }
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_ATENADATAKB;
                    cfUFParameterClass.Value = cSearchKey.p_strDataKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    // *履歴番号 000015 2003/11/18 修正終了


                }

                if (!(cSearchKey.p_strJuminShubetu1 == string.Empty & cSearchKey.p_strJuminShubetu2 == string.Empty))
                {
                    if (cSearchKey.p_strDataKB.Trim == string.Empty)
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append("((");
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB);
                        csWHERE.Append(" = '11')");
                        csWHERE.Append(" OR (");
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB);
                        csWHERE.Append(" = '12'))");
                    }

                    // 住民種別１
                    if (!(cSearchKey.p_strJuminShubetu1.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append(" {fn SUBSTRING(");
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU);
                        csWHERE.Append(",1,1)} = '");
                        csWHERE.Append(cSearchKey.p_strJuminShubetu1);
                        csWHERE.Append("'");
                    }

                    // 住民種別２
                    if (!(cSearchKey.p_strJuminShubetu2.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append(" {fn SUBSTRING(");
                        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU);
                        csWHERE.Append(",2,1)} = '");
                        csWHERE.Append(cSearchKey.p_strJuminShubetu2);
                        csWHERE.Append("'");
                    }
                }

                // 期間年月日

                if (!string.IsNullOrEmpty(strKikanYMD.Trim()))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }

                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKST_YMD);
                    csWHERE.Append(" <= ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_RRKST_YMD);
                    csWHERE.Append(" AND ");
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKED_YMD);
                    csWHERE.Append(" >= ");
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_RRKED_YMD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKST_YMD;
                    cfUFParameterClass.Value = strKikanYMD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKED_YMD;
                    cfUFParameterClass.Value = strKikanYMD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 市町村コード
                if (!(cSearchKey.p_strShichosonCD.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000015 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaRirekiEntity.SHICHOSONCD)
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHICHOSONCD);
                    // *履歴番号 000015 2003/11/18 修正終了
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_SHICHOSONCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SHICHOSONCD;
                    cfUFParameterClass.Value = cSearchKey.p_strShichosonCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // *履歴番号 000033 2014/04/28 追加開始
                // --------------------------------------------------------------------------------------------------------
                // 共通番号が指定されている場合
                if (cSearchKey.p_strMyNumber.Trim.RLength > 0)
                {

                    // -----------------------------------------------------------------------------------------------------
                    // 【１．直近検索区分による制御】
                    // 直近検索区分の整備
                    switch (cSearchKey.p_strMyNumberChokkinSearchKB)
                    {
                        case var @case when @case == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString:
                        // noop
                        case var case1 when case1 == ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString:
                            {
                                break;
                            }

                        default:
                            {
                                // 規定値以外（値なしを含む）の場合、管理情報登録値にて制御する。
                                cSearchKey.p_strMyNumberChokkinSearchKB = m_strMyNumberChokkinSearchKB_Param;
                                break;
                            }
                    }

                    // 直近検索区分が"1"（直近のみ）の場合
                    if (cSearchKey.p_strMyNumberChokkinSearchKB == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString)
                    {

                        // 共通番号カラムに共通番号を指定する。
                        if (csWHERE.RLength > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} = {2}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER);

                        // 検索条件のパラメーターを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
                        cfUFParameterClass.Value = cSearchKey.p_strMyNumber;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    else
                    {

                        // 共通番号マスタへのサブクエリに共通番号を指定する。
                        if (csWHERE.RLength > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD);
                        csWHERE.Append("IN ( ");
                        csWHERE.AppendFormat("SELECT {0} FROM {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.TABLE_NAME);
                        csWHERE.AppendFormat("WHERE {0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER);
                        csWHERE.Append(")");

                        // 検索条件のパラメーターを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
                        cfUFParameterClass.Value = cSearchKey.p_strMyNumber;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                    }
                    // -----------------------------------------------------------------------------------------------------
                    // 【２．個人法人区分による制御】
                    // 個人法人区分が"1"（個人）、または"2"（法人）の場合
                    if (cSearchKey.p_strMyNumberKojinHojinKB == "1" || cSearchKey.p_strMyNumberKojinHojinKB == "2")
                    {

                        // 個人法人区分カラムに個人法人区分を指定する。
                        if (csWHERE.RLength > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} = {2}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KJNHJNKB, ABAtenaRirekiEntity.PARAM_KJNHJNKB);

                        // 検索条件のパラメーターを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KJNHJNKB;
                        cfUFParameterClass.Value = cSearchKey.p_strMyNumberKojinHojinKB;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    else
                    {
                        // noop
                    }
                }
                // -----------------------------------------------------------------------------------------------------

                else
                {
                    // noop
                }
                // --------------------------------------------------------------------------------------------------------
                // *履歴番号 000033 2014/04/28 追加終了            

                // 電話番号
                if (!(cSearchKey.p_strRenrakusaki.Trim == string.Empty))
                {
                    if (!(csWHERE.RLength == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append("((");
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_RENRAKUSAKI1);
                    csWHERE.Append(") OR (");
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_RENRAKUSAKI2);
                    csWHERE.Append("))");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_RENRAKUSAKI1;
                    cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_RENRAKUSAKI2;
                    cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                {
                    // 住所
                    if (!(cSearchKey.p_strJusho.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        if (cSearchKey.p_strJusho.RIndexOf("%") == -1)
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHJUSHO);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO);
                        }
                        else
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHJUSHO);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO);
                        }
                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO;
                        cfUFParameterClass.Value = cSearchKey.p_strJusho;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    // 方書
                    if (!(cSearchKey.p_strKatagaki.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        if (cSearchKey.p_strKatagaki.RIndexOf("%") == -1)
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI);
                        }
                        else
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI);
                        }
                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI;
                        cfUFParameterClass.Value = cSearchKey.p_strKatagaki;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    // 旧氏
                    if (!(cSearchKey.p_strKyuuji.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        if (cSearchKey.p_strKyuuji.RIndexOf("%") == -1)
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI);
                        }
                        else
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI);
                        }
                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI;
                        cfUFParameterClass.Value = cSearchKey.p_strKyuuji;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    // カナ旧氏
                    if (!(cSearchKey.p_strKanaKyuuji.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        if (cSearchKey.p_strKanaKyuuji.RIndexOf("%") == -1)
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI);
                        }
                        else
                        {
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI);
                        }
                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI;
                        cfUFParameterClass.Value = cSearchKey.p_strKanaKyuuji;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    // カタカナ併記名
                    if (!(cSearchKey.p_strKatakanaHeikimei.Trim == string.Empty))
                    {
                        if (!(csWHERE.RLength == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        if (cSearchKey.p_strKatakanaHeikimei.RIndexOf("%") == -1)
                        {
                            csWHERE.Append(ABAtenaRirekiFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI);
                        }
                        else
                        {
                            csWHERE.Append(ABAtenaRirekiFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI);
                        }
                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI;
                        cfUFParameterClass.Value = cSearchKey.p_strKatakanaHeikimei;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                }

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


                // エラーをそのままスローする
                throw objExp;
            }

            return csWHERE.ToString();

        }

        // * corresponds to VS2008 Start 2010/04/16 000029
        // * 履歴番号 000020 2005/06/15 削除開始
        // '''************************************************************************************************
        // '''* メソッド名     SQL文の作成
        // '''* 
        // '''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // '''* 
        // '''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // '''* 
        // '''* 引数           csDataRow As DataRow : 更新対象の行
        // '''* 
        // '''* 戻り値         なし
        // '''************************************************************************************************
        // 'Private Sub CreateSQL(ByVal csDataRow As DataRow)

        // '    Const THIS_METHOD_NAME As String = "CreateSQL"
        // '    Dim csDataColumn As DataColumn
        // '    Dim cfUFParameterClass As UFParameterClass
        // '    Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        // '    Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義
        // '    Dim csWhere As StringBuilder                        'WHERE定義
        // '    Dim csUpdateParam As StringBuilder                  'UPDATE用SQL定義
        // '    Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義


        // '    Try
        // ''' デバッグログ出力
        // 'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ''' SELECT SQL文の作成
        // 'm_strInsertSQL = "INSERT INTO " + ABAtenaRirekiEntity.TABLE_NAME + " "
        // 'csInsertColumn = New StringBuilder()
        // 'csInsertParam = New StringBuilder()


        // ''' UPDATE SQL文の作成
        // 'm_strUpdateSQL = "UPDATE " + ABAtenaRirekiEntity.TABLE_NAME + " SET "
        // 'csUpdateParam = New StringBuilder()


        // ''' WHERE文の作成
        // 'csWhere = New StringBuilder()
        // 'csWhere.Append(" WHERE ")
        // 'csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
        // 'csWhere.Append(" = ")
        // 'csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
        // 'csWhere.Append(" AND ")
        // 'csWhere.Append(ABAtenaRirekiEntity.RIREKINO)
        // 'csWhere.Append(" = ")
        // 'csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO)
        // 'csWhere.Append(" AND ")
        // 'csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
        // 'csWhere.Append(" = ")
        // 'csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER)


        // ''' 論理DELETE SQL文の作成
        // 'csDelRonriParam = New StringBuilder()
        // 'csDelRonriParam.Append("UPDATE ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.TABLE_NAME)
        // 'csDelRonriParam.Append(" SET ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.TANMATSUID)
        // 'csDelRonriParam.Append(" = ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_TANMATSUID)
        // 'csDelRonriParam.Append(", ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.SAKUJOFG)
        // 'csDelRonriParam.Append(" = ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_SAKUJOFG)
        // 'csDelRonriParam.Append(", ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
        // 'csDelRonriParam.Append(" = ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER)
        // 'csDelRonriParam.Append(", ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINNICHIJI)
        // 'csDelRonriParam.Append(" = ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI)
        // 'csDelRonriParam.Append(", ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINUSER)
        // 'csDelRonriParam.Append(" = ")
        // 'csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINUSER)
        // 'csDelRonriParam.Append(csWhere)
        // 'm_strDelRonriSQL = csDelRonriParam.ToString

        // ''' 物理DELETE SQL文の作成
        // 'm_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString

        // ''' INSERT パラメータコレクションクラスのインスタンス化
        // 'm_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

        // ''' UPDATE パラメータコレクションのインスタンス化
        // 'm_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

        // ''' 論理削除用パラメータコレクションのインスタンス化
        // 'm_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

        // ''' 物理削除用パラメータコレクションのインスタンス化
        // 'm_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()


        // ''' パラメータコレクションの作成
        // 'For Each csDataColumn In csDataRow.Table.Columns
        // '    cfUFParameterClass = New UFParameterClass()

        // ''' INSERT SQL文の作成
        // 'csInsertColumn.Append(csDataColumn.ColumnName)
        // 'csInsertColumn.Append(", ")

        // 'csInsertParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
        // 'csInsertParam.Append(csDataColumn.ColumnName)
        // 'csInsertParam.Append(", ")


        // ''' UPDATE SQL文の作成
        // 'csUpdateParam.Append(csDataColumn.ColumnName)
        // 'csUpdateParam.Append(" = ")
        // 'csUpdateParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
        // 'csUpdateParam.Append(csDataColumn.ColumnName)
        // 'csUpdateParam.Append(", ")

        // ''' INSERT コレクションにパラメータを追加
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // 'm_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

        // ''' UPDATE コレクションにパラメータを追加
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // 'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'Next csDataColumn

        // '''最後のカンマを取り除いてINSERT文を作成
        // 'm_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
        // '        + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"


        // ''' UPDATE SQL文のトリミング
        // 'm_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

        // ''' UPDATE SQL文にWHERE句の追加
        // 'm_strUpdateSQL += csWhere.ToString


        // ''' UPDATE コレクションにパラメータを追加
        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
        // 'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
        // 'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
        // 'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)


        // ''' 論理削除用コレクションにパラメータを追加
        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_TANMATSUID
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SAKUJOFG
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINUSER
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
        // 'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)


        // ''' 物理削除用コレクションにパラメータを追加
        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
        // 'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
        // 'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // 'cfUFParameterClass = New UFParameterClass()
        // 'cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
        // 'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // ''' デバッグログ出力
        // 'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '    Catch objAppExp As UFAppException
        // '        ' ワーニングログ出力
        // '        m_cfLogClass.WarningWrite(m_cfControlData, _
        // '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // '                                    "【ワーニング内容:" + objAppExp.Message + "】")
        // '        ' エラーをそのままスローする
        // '        Throw objAppExp

        // '    Catch objExp As Exception
        // '        ' エラーログ出力
        // '        m_cfLogClass.ErrorWrite(m_cfControlData, _
        // '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '                                    "【エラー内容:" + objExp.Message + "】")
        // '        ' エラーをそのままスローする
        // '        Throw objExp
        // '    End Try

        // 'End Sub
        // * 履歴番号 000020 2005/06/15 削除終了
        // * corresponds to VS2008 End 2010/04/16 000029


        // * 履歴番号 000020 2005/06/15 追加開始
        // ************************************************************************************************
        // * メソッド名     Insert用SQL文の作成
        // * 
        // * 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           INSERT用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateInsertSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateInsertSQL";
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義
            UFParameterClass cfUFParameterClass;


            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABAtenaRirekiEntity.TABLE_NAME + " ";
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    csInsertColumn.Append(csDataColumn.ColumnName);
                    csInsertColumn.Append(", ");

                    csInsertParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + csInsertColumn.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" + " VALUES (" + csInsertParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")";

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


                // エラーをそのままスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     Update用SQL文の作成
        // * 
        // * 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csUpdateParam;                  // UPDATE用SQL定義


            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaRirekiEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・履歴番号・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaRirekiEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaRirekiEntity.RIREKINO) && !(csDataColumn.ColumnName == ABAtenaRirekiEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaRirekiEntity.SAKUSEINICHIJI))


                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(", ");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }


                // UPDATE SQL文のトリミング
                m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray());

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += csWhere.ToString();


                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     論理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteRonriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteRonriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csDelRonriParam;                // 論理削除パラメータ定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();


                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateButsuriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

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


                // エラーをそのままスローする
                throw objExp;
            }

        }
        // * 履歴番号 000020 2005/06/15 追加終了

        // * 履歴番号 000022 2005/11/18 追加開始
        // ************************************************************************************************
        // * メソッド名     物理削除用(１住民ＣＤ指定)SQL文の作成
        // * 
        // * 構文           Private Sub CreateDelFromJuminCDSQL()
        // * 
        // * 機能           住民ＣＤで該当全履歴データを物理削除するSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDelFromJuminCDSQL()
        {
            const string THIS_METHOD_NAME = "CreateDelFromJuminCDSQL";
            // * corresponds to VS2008 Start 2010/04/16 000029
            // Dim cfUFParameterClass As UFParameterClass
            // * corresponds to VS2008 End 2010/04/16 000029
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD);

                // 物理DELETE(１住民ＣＤ指定) SQL文の作成
                m_strDelFromJuminCDSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用コレクションにパラメータを追加
                m_cfDelFromJuminCDPrmCollection = new UFParameterCollectionClass();
                m_cfDelFromJuminCDPrmCollection.Add(ABAtenaRirekiEntity.KEY_JUMINCD, DbType.String);

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


                // エラーをそのままスローする
                throw objExp;
            }

        }
        // * 履歴番号 000022 2005/11/18 追加終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
        // * 
        // * 機能           更新データの整合性をチェックする。
        // * 
        // * 引数           strColumnName As String : 宛名履歴マスタデータセットの項目名
        // *                strValue As String     : 項目に対応する値
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {

            const string THIS_METHOD_NAME = "CheckColumnValue";
            const string TABLENAME = "宛名履歴．";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグログ出力
                // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                switch (strColumnName.ToUpper() ?? "")
                {

                    case var @case when @case == ABAtenaRirekiEntity.JUMINCD:            // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case1 when case1 == ABAtenaRirekiEntity.SHICHOSONCD:        // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case2 when case2 == ABAtenaRirekiEntity.KYUSHICHOSONCD:     // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case3 when case3 == ABAtenaRirekiEntity.RIREKINO:           // 履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RIREKINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case4 when case4 == ABAtenaRirekiEntity.RRKST_YMD:          // 履歴開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RRKST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case5 when case5 == ABAtenaRirekiEntity.RRKED_YMD:          // 履歴終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000" | strValue == "99999999"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RRKED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case6 when case6 == ABAtenaRirekiEntity.JUMINJUTOGAIKB:     // 住民住登外区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINJUTOGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case7 when case7 == ABAtenaRirekiEntity.JUMINYUSENIKB:      // 住民優先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINYUSENIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case8 when case8 == ABAtenaRirekiEntity.JUTOGAIYUSENKB:     // 住登外優先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTOGAIYUSENKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case9 when case9 == ABAtenaRirekiEntity.ATENADATAKB:        // 宛名データ区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ATENADATAKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case10 when case10 == ABAtenaRirekiEntity.STAICD:             // 世帯コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case11 when case11 == ABAtenaRirekiEntity.JUMINHYOCD:         // 住民票コード
                        {
                            break;
                        }
                    // チェックなし

                    case var case12 when case12 == ABAtenaRirekiEntity.SEIRINO:            // 整理番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIRINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case13 when case13 == ABAtenaRirekiEntity.ATENADATASHU:       // 宛名データ種別
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ATENADATASHU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case14 when case14 == ABAtenaRirekiEntity.HANYOKB1:           // 汎用区分1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANYOKB1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case15 when case15 == ABAtenaRirekiEntity.KJNHJNKB:           // 個人法人区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KJNHJNKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case16 when case16 == ABAtenaRirekiEntity.HANYOKB2:           // 汎用区分2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANYOKB2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case17 when case17 == ABAtenaRirekiEntity.KANNAIKANGAIKB:     // 管内管外区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANNAIKANGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case18 when case18 == ABAtenaRirekiEntity.KANAMEISHO1:        // カナ名称1
                        {
                            // *履歴番号 000014 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000014 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case19 when case19 == ABAtenaRirekiEntity.KANJIMEISHO1:       // 漢字名称1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case20 when case20 == ABAtenaRirekiEntity.KANAMEISHO2:        // カナ名称2
                        {
                            // *履歴番号 000014 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000014 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case21 when case21 == ABAtenaRirekiEntity.KANJIMEISHO2:       // 漢字名称2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case22 when case22 == ABAtenaRirekiEntity.KANJIHJNKEITAI:     // 漢字法人形態
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHJNKEITAI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case23 when case23 == ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI:   // 漢字法人代表者氏名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case24 when case24 == ABAtenaRirekiEntity.SEARCHKANJIMEISHO:  // 検索用漢字名称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEARCHKANJIMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case25 when case25 == ABAtenaRirekiEntity.KYUSEI:             // 旧姓
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KYUSEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case26 when case26 == ABAtenaRirekiEntity.SEARCHKANASEIMEI:   // 検索用カナ姓名
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓名", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case27 when case27 == ABAtenaRirekiEntity.SEARCHKANASEI:      // 検索用カナ姓
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case28 when case28 == ABAtenaRirekiEntity.SEARCHKANAMEI:      // 検索用カナ名
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ名", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case29 when case29 == ABAtenaRirekiEntity.JUKIRRKNO:          // 住基履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIRRKNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // Case ABAtenaRirekiEntity.UMAREYMD           '生年月日
                    // If Not (strValue = String.Empty Or strValue = "00000000") Then
                    // m_cfDateClass.p_strDateValue = strValue
                    // If (Not m_cfDateClass.CheckDate()) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_UMAREYMD)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // End If
                    // End If

                    // Case ABAtenaRirekiEntity.UMAREWMD           '生和暦年月日
                    // If (Not UFStringClass.CheckNumber(strValue)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得(数字項目入力の誤りです。：)
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode)
                    // End If

                    case var case30 when case30 == ABAtenaRirekiEntity.SEIBETSUCD:         // 性別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIBETSUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case31 when case31 == ABAtenaRirekiEntity.SEIBETSU:           // 性別
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIBETSU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case32 when case32 == ABAtenaRirekiEntity.SEKINO:             // 籍番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEKINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case33 when case33 == ABAtenaRirekiEntity.JUMINHYOHYOJIJUN:   // 住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case34 when case34 == ABAtenaRirekiEntity.ZOKUGARACD:         // 続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case35 when case35 == ABAtenaRirekiEntity.ZOKUGARA:           // 続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case36 when case36 == ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN:     // 第２住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case37 when case37 == ABAtenaRirekiEntity.DAI2ZOKUGARACD:           // 第２続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case38 when case38 == ABAtenaRirekiEntity.DAI2ZOKUGARA:             // 第２続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case39 when case39 == ABAtenaRirekiEntity.STAINUSJUMINCD:     // 世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case40 when case40 == ABAtenaRirekiEntity.STAINUSMEI:         // 世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case41 when case41 == ABAtenaRirekiEntity.KANASTAINUSMEI:     // カナ世帯主名
                        {
                            // *履歴番号 000014 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000014 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANASTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case42 when case42 == ABAtenaRirekiEntity.DAI2STAINUSJUMINCD:       // 第２世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case43 when case43 == ABAtenaRirekiEntity.DAI2STAINUSMEI:           // 第２世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case44 when case44 == ABAtenaRirekiEntity.KANADAI2STAINUSMEI:       // 第２カナ世帯主名
                        {
                            // *履歴番号 000014 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000014 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANADAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case45 when case45 == ABAtenaRirekiEntity.YUBINNO:            // 郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case46 when case46 == ABAtenaRirekiEntity.JUSHOCD:            // 住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case47 when case47 == ABAtenaRirekiEntity.JUSHO:              // 住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case48 when case48 == ABAtenaRirekiEntity.BANCHICD1:          // 番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case49 when case49 == ABAtenaRirekiEntity.BANCHICD2:          // 番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case50 when case50 == ABAtenaRirekiEntity.BANCHICD3:          // 番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case51 when case51 == ABAtenaRirekiEntity.BANCHI:             // 番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case52 when case52 == ABAtenaRirekiEntity.KATAGAKIFG:         // 方書フラグ
                        {
                            if (!string.IsNullOrEmpty(strValue.Trim()))
                            {
                                if (!UFStringClass.CheckNumber(strValue))
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKIFG);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case53 when case53 == ABAtenaRirekiEntity.KATAGAKICD:         // 方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case54 when case54 == ABAtenaRirekiEntity.KATAGAKI:           // 方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case55 when case55 == ABAtenaRirekiEntity.RENRAKUSAKI1:       // 連絡先1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RENRAKUSAKI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case56 when case56 == ABAtenaRirekiEntity.RENRAKUSAKI2:       // 連絡先2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RENRAKUSAKI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case57 when case57 == ABAtenaRirekiEntity.HON_ZJUSHOCD:       // 本籍全国住所コード
                        {
                            // * 履歴番号 000017 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000017 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HON_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case58 when case58 == ABAtenaRirekiEntity.HON_JUSHO:          // 本籍住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HON_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case59 when case59 == ABAtenaRirekiEntity.HONSEKIBANCHI:      // 本籍番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HONSEKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case60 when case60 == ABAtenaRirekiEntity.HITTOSH:            // 筆頭者
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HITTOSH);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case61 when case61 == ABAtenaRirekiEntity.CKINIDOYMD:         // 直近異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case62 when case62 == ABAtenaRirekiEntity.CKINJIYUCD:         // 直近事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // Case ABAtenaRirekiEntity.CKINJIYU           '直近事由
                    // If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINJIYU)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // End If

                    case var case63 when case63 == ABAtenaRirekiEntity.CKINTDKDYMD:        // 直近届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case64 when case64 == ABAtenaRirekiEntity.CKINTDKDTUCIKB:     // 直近届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case65 when case65 == ABAtenaRirekiEntity.TOROKUIDOYMD:       // 登録異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case66 when case66 == ABAtenaRirekiEntity.TOROKUIDOWMD:       // 登録異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case67 when case67 == ABAtenaRirekiEntity.TOROKUJIYUCD:       // 登録事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case68 when case68 == ABAtenaRirekiEntity.TOROKUJIYU:         // 登録事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case69 when case69 == ABAtenaRirekiEntity.TOROKUTDKDYMD:      // 登録届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case70 when case70 == ABAtenaRirekiEntity.TOROKUTDKDWMD:      // 登録届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case71 when case71 == ABAtenaRirekiEntity.TOROKUTDKDTUCIKB:   // 登録届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case72 when case72 == ABAtenaRirekiEntity.JUTEIIDOYMD:        // 住定異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case73 when case73 == ABAtenaRirekiEntity.JUTEIIDOWMD:        // 住定異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case74 when case74 == ABAtenaRirekiEntity.JUTEIJIYUCD:        // 住定事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case75 when case75 == ABAtenaRirekiEntity.JUTEIJIYU:          // 住定事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case76 when case76 == ABAtenaRirekiEntity.JUTEITDKDYMD:       // 住定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case77 when case77 == ABAtenaRirekiEntity.JUTEITDKDWMD:       // 住定届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case78 when case78 == ABAtenaRirekiEntity.JUTEITDKDTUCIKB:    // 住定届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case79 when case79 == ABAtenaRirekiEntity.SHOJOIDOYMD:        // 消除異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case80 when case80 == ABAtenaRirekiEntity.SHOJOJIYUCD:        // 消除事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case81 when case81 == ABAtenaRirekiEntity.SHOJOJIYU:          // 消除事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case82 when case82 == ABAtenaRirekiEntity.SHOJOTDKDYMD:       // 消除届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case83 when case83 == ABAtenaRirekiEntity.SHOJOTDKDTUCIKB:    // 消除届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case84 when case84 == ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD:     // 転出予定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case85 when case85 == ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD:      // 転出確定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case86 when case86 == ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD:   // 転出確定通知年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case87 when case87 == ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD:       // 転出入理由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUNYURIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case88 when case88 == ABAtenaRirekiEntity.TENSHUTSUNYURIYU:         // 転出入理由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUNYURIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case89 when case89 == ABAtenaRirekiEntity.TENUMAEJ_YUBINNO:         // 転入前住所郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case90 when case90 == ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD:        // 転入前住所全国住所コード
                        {
                            // * 履歴番号 000017 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000017 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case91 when case91 == ABAtenaRirekiEntity.TENUMAEJ_JUSHO:           // 転入前住所住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case92 when case92 == ABAtenaRirekiEntity.TENUMAEJ_BANCHI:          // 転入前住所番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case93 when case93 == ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI:        // 転入前住所方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case94 when case94 == ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI:      // 転入前住所世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case95 when case95 == ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO:    // 転出予定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case96 when case96 == ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD:   // 転出予定全国住所コード
                        {
                            // * 履歴番号 000017 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000017 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case97 when case97 == ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO:      // 転出予定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case98 when case98 == ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI:     // 転出予定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case99 when case99 == ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI:   // 転出予定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case100 when case100 == ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI: // 転出予定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case101 when case101 == ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO:     // 転出確定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case102 when case102 == ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD:    // 転出確定全国住所コード
                        {
                            // * 履歴番号 000017 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000017 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case103 when case103 == ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO:     // 転出確定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case104 when case104 == ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI:      // 転出確定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case105 when case105 == ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI:    // 転出確定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case106 when case106 == ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI:  // 転出確定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case107 when case107 == ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG:     // 転出確定未届フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case108 when case108 == ABAtenaRirekiEntity.BIKOYMD:                  // 備考年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case109 when case109 == ABAtenaRirekiEntity.BIKO:                     // 備考
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case110 when case110 == ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG: // 備考転出確定住所フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case111 when case111 == ABAtenaRirekiEntity.HANNO:                    // 版番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case112 when case112 == ABAtenaRirekiEntity.KAISEIATOFG:              // 改製後フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIATOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case113 when case113 == ABAtenaRirekiEntity.KAISEIMAEFG:             // 改製前フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIMAEFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case114 when case114 == ABAtenaRirekiEntity.KAISEIYMD:                // 改製年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case115 when case115 == ABAtenaRirekiEntity.GYOSEIKUCD:               // 行政区コード
                        {
                            // * 履歴番号 000023 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000023 2005/12/26 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_GYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case116 when case116 == ABAtenaRirekiEntity.GYOSEIKUMEI:              // 行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_GYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case117 when case117 == ABAtenaRirekiEntity.CHIKUCD1:                 // 地区コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case118 when case118 == ABAtenaRirekiEntity.CHIKUMEI1:                // 地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case119 when case119 == ABAtenaRirekiEntity.CHIKUCD2:                 // 地区コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case120 when case120 == ABAtenaRirekiEntity.CHIKUMEI2:                // 地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case121 when case121 == ABAtenaRirekiEntity.CHIKUCD3:                 // 地区コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case122 when case122 == ABAtenaRirekiEntity.CHIKUMEI3:                // 地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case123 when case123 == ABAtenaRirekiEntity.TOHYOKUCD:                // 投票区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOHYOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case124 when case124 == ABAtenaRirekiEntity.SHOGAKKOKUCD:             // 小学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case125 when case125 == ABAtenaRirekiEntity.CHUGAKKOKUCD:             // 中学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHUGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case126 when case126 == ABAtenaRirekiEntity.HOGOSHAJUMINCD:           // 保護者住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HOGOSHAJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case127 when case127 == ABAtenaRirekiEntity.KANJIHOGOSHAMEI:          // 漢字保護者名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case128 when case128 == ABAtenaRirekiEntity.KANAHOGOSHAMEI:           // カナ保護者名
                        {
                            // *履歴番号 000014 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000014 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case129 when case129 == ABAtenaRirekiEntity.KIKAYMD:                  // 帰化年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KIKAYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case130 when case130 == ABAtenaRirekiEntity.KARIIDOKB:                // 仮異動区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KARIIDOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case131 when case131 == ABAtenaRirekiEntity.SHORITEISHIKB:            // 処理停止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHORITEISHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case132 when case132 == ABAtenaRirekiEntity.JUKIYUBINNO:              // 住基郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case133 when case133 == ABAtenaRirekiEntity.SHORIYOKUSHIKB:           // 処理抑止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHORIYOKUSHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case134 when case134 == ABAtenaRirekiEntity.JUKIJUSHOCD:              // 住基住所コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case135 when case135 == ABAtenaRirekiEntity.JUKIJUSHO:                // 住基住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case136 when case136 == ABAtenaRirekiEntity.JUKIBANCHICD1:            // 住基番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case137 when case137 == ABAtenaRirekiEntity.JUKIBANCHICD2:            // 住基番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case138 when case138 == ABAtenaRirekiEntity.JUKIBANCHICD3:            // 住基番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case139 when case139 == ABAtenaRirekiEntity.JUKIBANCHI:               // 住基番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case140 when case140 == ABAtenaRirekiEntity.JUKIKATAGAKIFG:           // 住基方書フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKIFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case141 when case141 == ABAtenaRirekiEntity.JUKIKATAGAKICD:           // 住基方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case142 when case142 == ABAtenaRirekiEntity.JUKIKATAGAKI:             // 住基方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case143 when case143 == ABAtenaRirekiEntity.JUKIGYOSEIKUCD:           // 住基行政区コード
                        {
                            // * 履歴番号 000023 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000023 2005/12/26 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIGYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case144 when case144 == ABAtenaRirekiEntity.JUKIGYOSEIKUMEI:          // 住基行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIGYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case145 when case145 == ABAtenaRirekiEntity.JUKICHIKUCD1:             // 住基地区コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case146 when case146 == ABAtenaRirekiEntity.JUKICHIKUMEI1:            // 住基地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case147 when case147 == ABAtenaRirekiEntity.JUKICHIKUCD2:             // 住基地区コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case148 when case148 == ABAtenaRirekiEntity.JUKICHIKUMEI2:            // 住基地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case149 when case149 == ABAtenaRirekiEntity.JUKICHIKUCD3:             // 住基地区コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case150 when case150 == ABAtenaRirekiEntity.JUKICHIKUMEI3:            // 住基地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case151 when case151 == ABAtenaRirekiEntity.KAOKUSHIKIKB:             // 家屋敷区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAOKUSHIKIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case152 when case152 == ABAtenaRirekiEntity.BIKOZEIMOKU:              // 備考税目
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOZEIMOKU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case153 when case153 == ABAtenaRirekiEntity.KOKUSEKICD:               // 国籍コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOKUSEKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case154 when case154 == ABAtenaRirekiEntity.KOKUSEKI:                 // 国籍
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOKUSEKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case155 when case155 == ABAtenaRirekiEntity.ZAIRYUSKAKCD:             // 在留資格コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUSKAKCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case156 when case156 == ABAtenaRirekiEntity.ZAIRYUSKAK:               // 在留資格
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUSKAK);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case157 when case157 == ABAtenaRirekiEntity.ZAIRYUKIKAN:              // 在留期間
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUKIKAN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case158 when case158 == ABAtenaRirekiEntity.ZAIRYU_ST_YMD:            // 在留開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYU_ST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case159 when case159 == ABAtenaRirekiEntity.ZAIRYU_ED_YMD:            // 在留終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYU_ED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case160 when case160 == ABAtenaRirekiEntity.RESERCE:                  // リザーブ
                        {
                            break;
                        }
                    // チェックなし

                    case var case161 when case161 == ABAtenaRirekiEntity.TANMATSUID:               // 端末ＩＤ
                        {
                            // * 履歴番号 000012 2003/09/11 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000012 2003/09/11 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case162 when case162 == ABAtenaRirekiEntity.SAKUJOFG:                 // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case163 when case163 == ABAtenaRirekiEntity.KOSHINCOUNTER:            // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case164 when case164 == ABAtenaRirekiEntity.SAKUSEINICHIJI:           // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case165 when case165 == ABAtenaRirekiEntity.SAKUSEIUSER:              // 作成ユーザ
                        {
                            // * 履歴番号 000013 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000013 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case166 when case166 == ABAtenaRirekiEntity.KOSHINNICHIJI:            // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case167 when case167 == ABAtenaRirekiEntity.KOSHINUSER:               // 更新ユーザ
                        {
                            // * 履歴番号 000013 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000013 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                }
            }

            // デバッグログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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


                // エラーをそのままスローする
                throw objExp;
            }

        }
        // ************************************************************************************************
        // * メソッド名     宛名Get用の項目を編集
        // * 
        // * 構文           Private SetAtenaEntity(ByRef strSql As StringBuilder)
        // * 
        // * 機能           宛名Get用の項目を編集する。
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetAtenaEntity(ref StringBuilder strAtenaSQLsb)
        {
            if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
            {
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSHICHOSONCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KJNHJNKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANNAIKANGAIKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNKEITAI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREWMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSU).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEKINO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZOKUGARACD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZOKUGARA).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2ZOKUGARACD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2ZOKUGARA).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAINUSJUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANASTAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANADAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.YUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUIDOYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUJIYUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUJIYU).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOIDOYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOJIYUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOJIYU).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIYUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI3);

                // *履歴番号 000030 2010/05/14 追加開始
                // 本籍筆頭者情報抽出判定
                if (m_strHonsekiHittoshKB == "1" && m_strHonsekiHittoshKB_Param == "1")
                {
                    // 本籍住所、本籍番地、筆頭者を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_JUSHO).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HONSEKIBANCHI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HITTOSH);
                }
                else
                {
                }

                // 処理停止区分抽出判定
                if (m_strShoriteishiKB == "1" && m_strShoriTeishiKB_Param == "1")
                {
                    // 処理停止区分を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHORITEISHIKB);
                }
                else
                {
                }
                // *履歴番号 000030 2010/05/14 追加終了

                // *履歴番号 000031 2011/05/18 追加開始
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // 外国人在留情報(国籍、在留資格コード、在留資格、在留期間、在留開始年月日、在留終了年月日)を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAKCD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAK).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUKIKAN).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ST_YMD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ED_YMD);
                }
                else
                {
                }
            }
            // *履歴番号 000031 2011/05/18 追加終了
            else
            {
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSHICHOSONCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KJNHJNKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANNAIKANGAIKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNKEITAI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREWMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANASTAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANADAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.YUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIYUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI3);

                // *履歴番号 000030 2010/05/14 追加開始
                // 本籍筆頭者情報抽出判定
                if (m_strHonsekiHittoshKB == "1" && m_strHonsekiHittoshKB_Param == "1")
                {
                    // 本籍住所、本籍番地、筆頭者を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_JUSHO).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HONSEKIBANCHI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HITTOSH);
                }
                else
                {
                }

                // 処理停止区分抽出判定
                if (m_strShoriteishiKB == "1" && m_strShoriTeishiKB_Param == "1")
                {
                    // 処理停止区分を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHORITEISHIKB);
                }
                else
                {
                }
                // *履歴番号 000030 2010/05/14 追加終了

                // *履歴番号 000031 2011/05/18 追加開始
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // 外国人在留情報(国籍、在留資格コード、在留資格、在留期間、在留開始年月日、在留終了年月日)を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAKCD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAK).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUKIKAN).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ST_YMD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ED_YMD);
                }
                else
                {
                }
                // *履歴番号 000031 2011/05/18 追加終了

            }
            if (m_blnSelectAll == ABEnumDefine.AtenaGetKB.NenkinAll)
            {
                strAtenaSQLsb.Append(",");
                // 旧姓
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSEI).Append(",");
                // 住定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIIDOYMD).Append(",");
                // 住定事由
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIJIYU).Append(",");
                // 転入前住所郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO).Append(",");
                // 転入前住所全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD).Append(",");
                // 転入前住所住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_JUSHO).Append(",");
                // 転入前住所番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_BANCHI).Append(",");
                // 転入前住所方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI).Append(",");
                // 転出予定郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO).Append(",");
                // 転出予定全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",");
                // 転出予定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD).Append(",");
                // 転出予定住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO).Append(",");
                // 転出予定番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI).Append(",");
                // 転出予定方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI).Append(",");
                // 転出確定郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO).Append(",");
                // 転出確定全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD).Append(",");
                // 転出確定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD).Append(",");
                // 転出確定通知年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD).Append(",");
                // 転出確定住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO).Append(",");
                // 転出確定番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI).Append(",");
                // 転出確定方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI).Append(",");

                // 消除届出年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOTDKDYMD).Append(",");
                // 直近事由コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINJIYUCD).Append(",");

                // 本籍全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_ZJUSHOCD).Append(",");
                // 転出予定世帯主名
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",");
                // 転出確定世帯主名
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI).Append(",");
                // *履歴番号 000024 2006/07/31 追加開始
                // 国籍コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD).Append(",");
                // 転入前住所世帯主名
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI);
                // strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD)
                // *履歴番号 000024 2006/07/31 追加終了

            }

            // *履歴番号 000025 2007/04/28 追加開始
            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
            {
                strAtenaSQLsb.Append(",");
                // 旧姓
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSEI).Append(",");
                // 住定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIIDOYMD).Append(",");
                // 住定事由
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIJIYU).Append(",");
                // 転入前住所郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO).Append(",");
                // 転入前住所全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD).Append(",");
                // 転入前住所住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_JUSHO).Append(",");
                // 転入前住所番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_BANCHI).Append(",");
                // 転入前住所方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI).Append(",");
                // 転出予定郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO).Append(",");
                // 転出予定全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",");
                // 転出予定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD).Append(",");
                // 転出予定住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO).Append(",");
                // 転出予定番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI).Append(",");
                // 転出予定方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI).Append(",");
                // 転出確定郵便番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO).Append(",");
                // 転出確定全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD).Append(",");
                // 転出確定異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD).Append(",");
                // 転出確定通知年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD).Append(",");
                // 転出確定住所
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO).Append(",");
                // 転出確定番地
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI).Append(",");
                // 転出確定方書
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI).Append(",");
                // 消除届出年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOTDKDYMD).Append(",");
                // 直近事由コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINJIYUCD).Append(",");
                // 本籍全国住所コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_ZJUSHOCD).Append(",");
                // 転出予定世帯主名
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",");
                // 転出確定世帯主名
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI).Append(",");
                // 国籍コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD).Append(",");
                // 登録届出年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUTDKDYMD).Append(",");
                // 住定届出年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEITDKDYMD).Append(",");
                // 転出入理由
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUNYURIYU).Append(",");
                // 市町村コード
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHICHOSONCD).Append(",");
                // 直近異動年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINIDOYMD).Append(",");
                // 更新日時
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOSHINNICHIJI);
            }
            // *履歴番号 000025 2007/04/28 追加終了
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(",");
                // 直近届出通知区分
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINTDKDTUCIKB).Append(",");
                // 版番号
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANNO).Append(",");
                // 改製年月日
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KAISEIYMD);
                if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll)
                {
                    // 国籍コード
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD);
                }
            }
        }
        // ************************************************************************************************
        // * メソッド名     宛名Get用の個別事項項目を編集
        // * 
        // * 構文           Private SetKobetsuaEntity(ByRef strSql As StringBuilder)
        // * 
        // * 機能           宛名Get用の項目を編集する。
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetKobetsuEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1);

            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2);

            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3);
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.HIHOKENSHAGAITOKB);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SHUBETSUHENKOYMD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD);
            }

            // 国保
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO);
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.HIHOKENSHAGAITOKB);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB);
            }

            // 印鑑
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB);

            // 選挙
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB);
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.TOROKUJOTAIKBN);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN);
            }

            // 児童手当
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM);

            // 介護
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD);
            strAtenaSQLsb.Append(", ");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD);
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD);
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHOKENSHAGAITOKB);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB);
            }

            // *履歴番号 000028 2008/01/17 追加開始
            // 後期高齢
            if (m_strKobetsuShutokuKB == "1")
            {
                // 個別事項取得区分が"1"の場合、後期高齢者マスタ項目を取得する
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SHIKAKUKB);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.HIHKNSHANO);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKJIYUCD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKJIYUMEI);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKYMD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSJIYUCD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSJIYUMEI);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSYMD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.TEKIYOKAISHIYMD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD);
                strAtenaSQLsb.Append(", ");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.TEKIYOSHURYOYMD);
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD);
                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                {
                    strAtenaSQLsb.Append(", ");
                    strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.HIHOKENSHAGAITOKB);
                    strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB);
                }
            }
            else
            {
                // 個別事項取得区分が値無しの場合、処理を行わない
            }
            // *履歴番号 000028 2008/01/17 追加終了
        }
        // ************************************************************************************************
        // * メソッド名     宛名Get用のCOUNTEntityを編集
        // * 
        // * 構文           Private SetAtenaCountEntity()
        // * 
        // * 機能           宛名Get用の項目を編集する。
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetAtenaCountEntity(ref StringBuilder strAtenaSQLsb)
        {
            if (m_blnSelectCount == true)
            {
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll)
                {
                    strAtenaSQLsb.Append(",B.");
                    strAtenaSQLsb.Append(ABAtenaCountEntity.DAINOCOUNT);
                    strAtenaSQLsb.Append(",C.");
                    strAtenaSQLsb.Append(ABAtenaCountEntity.SFSKCOUNT);
                }
                strAtenaSQLsb.Append(",D.");
                strAtenaSQLsb.Append(ABAtenaCountEntity.RENERAKUSAKICOUNT);
            }
        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随データ項目編集
        // * 
        // * 構文           Private SetFZYEntity()
        // * 
        // * 機能           宛名履歴付随データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TABLEINSERTKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.LINKNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKYOCHITODOKEFLG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.HONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANAHONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANJITSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.UMAREFUSHOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TSUSHOMEITOUROKUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUKIKANCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUKIKANMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUSHACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUSHAMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUCARDNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYOTEISTYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYOTEIEDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.FRNSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.FRNSTAINUSKANAMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE10);
        }

        // *履歴番号 000033 2014/04/28 追加開始
        // ************************************************************************************************
        // * メソッド名     共通番号データ項目編集
        // * 
        // * 構文           Private SetMyNumberEntity()
        // * 
        // * 機能           共通番号データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetMyNumberEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.CKINKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOSHA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.RESERVE);
        }
        // *履歴番号 000033 2014/04/28 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名Get用のJOIN句を編集
        // * 
        // * 構文           Private SetAtenaJoin()
        // * 
        // * 機能           宛名Get用の項目を編集する。
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetAtenaJoin(ref StringBuilder strAtenaSQLsb)
        {
            if (m_blnSelectCount == true)
            {
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll)
                {
                    strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ");
                    strAtenaSQLsb.Append(ABDainoEntity.JUMINCD);
                    strAtenaSQLsb.Append(",COUNT(*) AS ");
                    strAtenaSQLsb.Append(ABAtenaCountEntity.DAINOCOUNT);
                    strAtenaSQLsb.Append(" FROM ");
                    strAtenaSQLsb.Append(ABDainoEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(" GROUP BY ");
                    strAtenaSQLsb.Append(ABDainoEntity.JUMINCD);
                    strAtenaSQLsb.Append(") B ON ");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(".");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD);
                    strAtenaSQLsb.Append(" = B.");
                    strAtenaSQLsb.Append(ABDainoEntity.JUMINCD);
                    strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ");
                    strAtenaSQLsb.Append(ABSfskEntity.JUMINCD);
                    strAtenaSQLsb.Append(",COUNT(*) AS ");
                    strAtenaSQLsb.Append(ABAtenaCountEntity.SFSKCOUNT);
                    strAtenaSQLsb.Append(" FROM ");
                    strAtenaSQLsb.Append(ABSfskEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(" GROUP BY ");
                    strAtenaSQLsb.Append(ABSfskEntity.JUMINCD);
                    strAtenaSQLsb.Append(") C ON ");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(".");
                    strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD);
                    strAtenaSQLsb.Append(" = C.");
                    strAtenaSQLsb.Append(ABSfskEntity.JUMINCD);
                }
                strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ");
                strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD);
                strAtenaSQLsb.Append(",COUNT(*) AS ");
                strAtenaSQLsb.Append(ABAtenaCountEntity.RENERAKUSAKICOUNT);
                strAtenaSQLsb.Append(" FROM ");
                strAtenaSQLsb.Append(ABRenrakusakiEntity.TABLE_NAME);
                strAtenaSQLsb.Append(" GROUP BY ");
                strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD);
                strAtenaSQLsb.Append(") D ON ");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME);
                strAtenaSQLsb.Append(".");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD);
                strAtenaSQLsb.Append(" = D.");
                strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD);
            }
        }
        // ************************************************************************************************
        // * メソッド名     宛名Get用の個別事項JOIN句を編集
        // * 
        // * 構文           Private SetKobetsuJoin()
        // * 
        // * 機能           宛名Get用の項目を編集する。
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetKobetsuJoin(ref StringBuilder strAtenaSQLsb)
        {

            // LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD);

            // *履歴番号 000025 2008/01/15 追加開始
            if (m_strKobetsuShutokuKB == "1")
            {
                // 個別事項取得区分が"1"の場合、後期高齢者マスタもJOINする
                strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(" ON ");
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                strAtenaSQLsb.Append("=");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.JUMINCD);
            }
            else
            {
                // 個別事項取得区分が値無しの場合、処理を行わない
            }
            // *履歴番号 000025 2008/01/15 追加終了
        }
        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随テーブルJOIN句作成
        // * 
        // * 構文           Private SetFZYJoin()
        // * 
        // * 機能           宛名履歴付随テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINCD);

            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO, ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RIREKINO);

        }
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000033 2014/04/28 追加開始
        // ************************************************************************************************
        // * メソッド名     共通番号テーブルJOIN句作成
        // * 
        // * 構文           Private SetMyNumberJoin()
        // * 
        // * 機能           共通番号テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetMyNumberJoin(ref StringBuilder strAtenaSQLsb)
        {
            // 共通番号テーブルは直近レコードのみを結合する。
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ");
            strAtenaSQLsb.AppendFormat("(SELECT * FROM {0} WHERE {1} = '{2}') AS {0} ", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.CKINKB, ABMyNumberEntity.DEFAULT.CKINKB.CKIN);
            strAtenaSQLsb.AppendFormat("ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.JUMINCD);

        }
        // *履歴番号 000033 2014/04/28 追加終了

        // * 履歴番号 000021 2005/06/17 追加開始
        // ************************************************************************************************
        // * メソッド名     履歴番号の取得
        // * 
        // * 構文           Private Function GetRirekiNo(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能           履歴番号の取得を行う
        // * 
        // * 引数           strJuminCD As string : 対象となる住民ＣＤ
        // * 
        // * 戻り値         csRirekiNoDataEntity as DataSet:履歴番号
        // ************************************************************************************************
        public DataSet GetRirekiNo(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetRirekiNo";
            DataSet csRirekiNoDataEntity;                // 履歴番号データセット
            StringBuilder strGetRirekiNoSQL;        // ＳＱＬ文
            UFParameterClass cfUFParameterClass;      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;    // パラメータコレクションクラス

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // ＳＱＬ文の作成
                strGetRirekiNoSQL = new StringBuilder();
                strGetRirekiNoSQL.Append("SELECT ");
                strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.RIREKINO);
                strGetRirekiNoSQL.Append(" FROM ");
                strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.TABLE_NAME);
                strGetRirekiNoSQL.Append(" WHERE ");
                strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.JUMINCD);
                strGetRirekiNoSQL.Append(" = ");
                strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.PARAM_JUMINCD);
                strGetRirekiNoSQL.Append(" ORDER BY ");
                strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.RIREKINO);
                strGetRirekiNoSQL.Append(" DESC ");

                // パラメータクラスのインスタンス化
                cfUFParameterClass = new UFParameterClass();
                // パラメータのセット
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // パラメータコレクションクラスのインスタンス化
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                // パラメータコレクションにセット
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号の取得
                csRirekiNoDataEntity = m_cfRdbClass.GetDataSet(strGetRirekiNoSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return csRirekiNoDataEntity;

        }
        // * 履歴番号 000021 2005/06/17 追加終了

        // *履歴番号 000030 2010/05/14 追加開始
        // ************************************************************************************************
        // * メソッド名       管理情報取得
        // * 
        // * 構文             Private Function GetKanriJoho()
        // * 
        // * 機能　　    　   管理情報を取得する
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void GetKanriJoho()
        {
            const string THIS_METHOD_NAME = "GetKanriJoho";
            var cABAtenaKanriJoho = default(ABAtenaKanriJohoBClass);

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名管理情報Ｂクラスのインスタンス作成
                if (cABAtenaKanriJoho is null)
                {
                    cABAtenaKanriJoho = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }

                // 本籍取得区分取得
                m_strHonsekiHittoshKB = cABAtenaKanriJoho.GetHonsekiKB_Param();

                // 処理停止区分取得区分取得
                m_strShoriteishiKB = cABAtenaKanriJoho.GetShoriteishiKB_Param();

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
        // *履歴番号 000030 2010/05/14 追加終了

        // *履歴番号 000032 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名       住基法改正ﾌﾗｸﾞ取得
        // * 
        // * 構文             Private Function GetJukihoKaiseiFG()
        // * 
        // * 機能　　    　   管理情報を取得する
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void GetJukihoKaiseiFG()
        {
            const string THIS_METHOD_NAME = "GetJukihoKaiseiFG";
            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (m_csSekoYMDHanteiB == null)
                {
                    // 施行日判定Ｂｸﾗｽのｲﾝｽﾀﾝｽ化
                    m_csSekoYMDHanteiB = new ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 住基法改正ﾌﾗｸﾞ＝施行日判定結果
                    m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD();
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
        // *履歴番号 000032 2011/10/24 追加終了

        // *履歴番号 000033 2014/04/28 追加開始
        /// <summary>
    /// 共通番号　宛名取得　直近検索区分取得
    /// </summary>
    /// <remarks></remarks>
        private void GetMyNumberChokkinSearchKB()
        {

            ABAtenaKanriJohoBClass cABAtenaKanriJoho;

            try
            {

                // 宛名管理情報ビジネスクラスのインスタンス化
                cABAtenaKanriJoho = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 共通番号　宛名取得　直近検索区分の取得
                m_strMyNumberChokkinSearchKB_Param = cABAtenaKanriJoho.GetMyNumberChokkinSearchKB_Param();
            }

            catch (Exception csExp)
            {
                throw;
            }

        }
        // *履歴番号 000033 2014/04/28 追加終了

        #endregion

        // *履歴番号 000035 2015/05/08 追加開始
        #region 宛名履歴マスタ抽出(番号一括取得バッチから呼出)
        /// <summary>
    /// 宛名履歴マスタ抽出
    /// </summary>
    /// <param name="cSearchKey">宛名履歴マスタ検索キー</param>
    /// <returns>取得した宛名履歴マスタの直近データ</returns>
        public DataSet CreateRuisekiData(ABAtenaSearchKey cSearchKey)
        {
            const string THIS_METHOD_NAME = "CreateRuisekiData";
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();
            StringBuilder strAtenaSQLsbWhere;
            StringBuilder strORDER;
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfSelectUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (string.IsNullOrEmpty(m_strAtenaSQLsbAll.ToString()))
                {
                    // 初回SQL作成
                    GetRirekiSQLString();
                }
                strSQL.Append(m_strAtenaSQLsbAll);

                if (m_csDataSchmaAll is null)
                {
                    m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);
                }
                m_csDataSchma = m_csDataSchmaAll;

                // Where句作成(住民コード/住登外優先区分)
                strAtenaSQLsbWhere = new StringBuilder();
                strAtenaSQLsbWhere.Append(" WHERE ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                strAtenaSQLsbWhere.Append(" = ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD);
                strAtenaSQLsbWhere.Append(" AND ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTOGAIYUSENKB);
                strAtenaSQLsbWhere.Append(" = ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB);
                strAtenaSQLsbWhere.Append(" AND ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKED_YMD);
                strAtenaSQLsbWhere.Append(" = ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_RRKED_YMD);
                strAtenaSQLsbWhere.Append(" AND ");
                strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SAKUJOFG);
                strAtenaSQLsbWhere.Append(" <> '1' ");

                // ORDER BY句作成(住民コード)
                strORDER = new StringBuilder();
                strORDER.Append(" ORDER BY ");
                strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD);
                strORDER.Append(" ASC;");

                strSQL.Append(strAtenaSQLsbWhere);
                strSQL.Append(strORDER);

                // SELECT パラメータコレクションクラスのインスタンス化
                cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();
                // パラメータ(住民コード)
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = cSearchKey.p_strJuminCD;
                cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                // パラメータ(住登外優先区分)
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB;
                cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB;
                cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                // パラメータ(履歴終了年月日)
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKED_YMD;
                cfUFParameterClass.Value = "99999999";
                cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // SQLの実行 DataSetの取得
                csAtenaRirekiEntity = m_csDataSchma.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, cfSelectUFParameterCollectionClass, false);

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


                // エラーをそのままスローする
                throw objExp;
            }

            return csAtenaRirekiEntity;

        }

        /// <summary>
    /// SQL文字列を取得する
    /// </summary>
    /// <remarks></remarks>
        private void GetRirekiSQLString()
        {
            const string THIS_METHOD_NAME = "GetRirekiSQLString";

            try
            {
                m_strAtenaSQLsbAll.Append("SELECT ");

                // 宛名履歴付加
                SetRirekiEntity(ref m_strAtenaSQLsbAll);

                // 宛名年金付加
                SetNenkinEntity(ref m_strAtenaSQLsbAll);

                // 宛名国保付加
                SetKokuhoEntity(ref m_strAtenaSQLsbAll);

                // FROM句
                m_strAtenaSQLsbAll.Append(" FROM ");
                m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME);

                // 宛名年金マスタを付加
                SetNENKINJoin(ref m_strAtenaSQLsbAll);

                // 宛名国保マスタを付加
                SetKOKUHOJoin(ref m_strAtenaSQLsbAll);
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


                // エラーをそのままスローする
                throw objExp;
            }
        }

        #region 宛名履歴データ項目編集
        /// <summary>
    /// 宛名履歴データ項目編集
    /// </summary>
    /// <param name="strAtenaSQLsb">履歴取得用SQL</param>
    /// <remarks></remarks>
        private void SetRirekiEntity(ref StringBuilder strAtenaSQLsb)
        {
            const string THIS_METHOD_NAME = "SetRirekiEntity";
            try
            {
                strAtenaSQLsb.AppendFormat("{0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHICHOSONCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KYUSHICHOSONCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RRKST_YMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RRKED_YMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINYUSENIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTOGAIYUSENKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ATENADATAKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAICD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINHYOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIRINO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ATENADATASHU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANYOKB1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KJNHJNKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANYOKB2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANNAIKANGAIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAMEISHO1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIMEISHO1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAMEISHO2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIMEISHO2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHJNKEITAI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANJIMEISHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KYUSEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANASEIMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANASEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANAMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIRRKNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.UMAREYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.UMAREWMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIBETSUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIBETSU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEKINO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINHYOHYOJIJUN);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZOKUGARACD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZOKUGARA);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2ZOKUGARACD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2ZOKUGARA);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAINUSJUMINCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANASTAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2STAINUSJUMINCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2STAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANADAI2STAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.YUBINNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKIFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKICD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RENRAKUSAKI1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RENRAKUSAKI2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HON_ZJUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HON_JUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HONSEKIBANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HITTOSH);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINJIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINJIYU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINTDKDYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINTDKDTUCIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUIDOWMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUJIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUJIYU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDWMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDTUCIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIIDOWMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIJIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIJIYU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDWMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDTUCIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOJIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOJIYU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOTDKDYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOTDKDTUCIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUNYURIYU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_YUBINNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_JUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_BANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIATOFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIMAEFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.GYOSEIKUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.GYOSEIKUMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOHYOKUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOGAKKOKUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHUGAKKOKUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HOGOSHAJUMINCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHOGOSHAMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAHOGOSHAMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KIKAYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KARIIDOKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHORITEISHIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIYUBINNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHORIYOKUSHIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIJUSHOCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIJUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKIFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKICD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIGYOSEIKUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIGYOSEIKUMEI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAOKUSHIKIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOZEIMOKU);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOKUSEKICD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOKUSEKI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUSKAKCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUSKAK);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUKIKAN);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYU_ST_YMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYU_ED_YMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RESERCE);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TANMATSUID);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUJOFG);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINCOUNTER);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUSEINICHIJI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUSEIUSER);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINNICHIJI);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINUSER);
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


                // エラーをそのままスローする
                throw objExp;
            }

        }
        #endregion

        #region 宛名年金データ項目編集
        /// <summary>
    /// 年金データ項目編集
    /// </summary>
    /// <param name="strAtenaSQLsb">履歴取得用SQL</param>
    /// <remarks></remarks>
        private void SetNenkinEntity(ref StringBuilder strAtenaSQLsb)
        {
            const string THIS_METHOD_NAME = "SetNenkinEntity";
            try
            {
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.KSNENKNNO);
                strAtenaSQLsb.AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKYMD, ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKSHU, ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU);
                strAtenaSQLsb.AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKRIYUCD, ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSSHTSYMD, ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSSHTSRIYUCD, ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB1);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB2);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN3);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB3);
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


                // エラーをそのままスローする
                throw objExp;
            }
        }
        #endregion

        #region 国保データ項目編集
        /// <summary>
    /// 国保データ項目編集
    /// </summary>
    /// <param name="strAtenaSQLsb">履歴取得用SQL</param>
    /// <remarks></remarks>
        private void SetKokuhoEntity(ref StringBuilder strAtenaSQLsb)
        {
            const string THIS_METHOD_NAME = "SetKokuhoEntity";
            try
            {
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHONO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO);
                strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOHOKENSHONO);
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


                // エラーをそのままスローする
                throw objExp;
            }
        }
        #endregion

        #region 宛名年金JOIN句作成
        /// <summary>
    /// 宛名年金テーブルのJOIN句を作成
    /// </summary>
    /// <param name="strAtenaSQLsb">履歴取得用SQL</param>
    /// <remarks></remarks>
        private void SetNENKINJoin(ref StringBuilder strAtenaSQLsb)
        {
            const string THIS_METHOD_NAME = "SetNENKINJoin";
            try
            {
                strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaNenkinEntity.TABLE_NAME);
                strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JUMINCD);

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


                // エラーをそのままスローする
                throw objExp;
            }
        }
        #endregion

        #region 宛名国保JOIN句作成
        /// <summary>
    /// 宛名国保テーブルのJOIN句を作成
    /// </summary>
    /// <param name="strAtenaSQLsb">履歴取得用SQL</param>
    /// <remarks></remarks>
        private void SetKOKUHOJoin(ref StringBuilder strAtenaSQLsb)
        {
            const string THIS_METHOD_NAME = "SetKOKUHOJoin";
            try
            {
                strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaKokuhoEntity.TABLE_NAME);
                strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.JUMINCD);

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


                // エラーをそのままスローする
                throw objExp;
            }
        }
        #endregion

        #endregion
        // *履歴番号 000035 2015/05/08 追加終了

        // *履歴番号 000037 2023/03/10 追加開始
        #region 宛名履歴標準データ項目編集
        // ************************************************************************************************
        // * メソッド名     宛名履歴標準データ項目編集
        // * 
        // * 構文           Private SetHyojunEntity()
        // * 
        // * 機能           宛名履歴標準データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetHyojunEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.EDANO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIMEIKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.UMAREBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOUMAREBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JIJITSUSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KANAKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.BANCHIEDABANSUCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEICODE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEITO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_SHIKUGUNCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CKINIDOWMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CKINIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOROKUIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOTOROKUIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNKISAIJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KISAIYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KISAIBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKISAIBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUTEIIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOJUTEIIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNSHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KOKUSEKISOSHITSUBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOJOIDOWMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOJOIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_YUBINNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_BANCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_KATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_BANCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_KATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEIBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKAISEIBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEISHOJOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEISHOJOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKAISEISHOJOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD10);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOKUBETSUYOSHIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNIDOKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NYURYOKUBASHOCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NYURYOKUBASHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KYUUJIKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TDKDSHIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNIDOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NICHIJOSEIKATSUKENIKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOROKUBUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TANKITAIZAISHAFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KYOYUNINZU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIZEIJIMUSHOCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ST);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ED);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.IDOSHURUI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOKANKUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOGOATENAFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOUMAREBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIKANAKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD10);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIBANCHIEDABANSUCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE5);
        }
        #endregion

        #region 宛名履歴付随標準データ項目編集
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随標準データ項目編集
        // * 
        // * 構文           Private SetFZYHyojunEntity()
        // * 
        // * 機能           宛名履歴付随標準データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYHyojunEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHFRNMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANAFRNMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SHIMEIYUSENKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.ZAIRYUCARDNOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.JUKYOCHIHOSEICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.HODAI30JO46MATAHA47KB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE5);
        }
        #endregion

        #region 不現住情報データ項目編集
        // ************************************************************************************************
        // * メソッド名     不現住情報データ項目編集
        // * 
        // * 構文           Private SetFugenjuEntity()
        // * 
        // * 機能           不現住情報データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFugenjuEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_SEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_MEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.KYOJUFUMEI_YMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUTOROKUYMD);
            // *履歴番号 000038 2023/08/14 修正開始
            // strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.GYOSEIKUCD)
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD);
            // *履歴番号 000038 2023/08/14 修正終了
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_BIKO);
        }
        #endregion

        #region 共通番号標準データ項目編集
        // ************************************************************************************************
        // * メソッド名     共通番号標準データ項目編集
        // * 
        // * 構文           Private SetMyNumberHyojunEntity()
        // * 
        // * 機能           共通番号標準データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetMyNumberHyojunEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.BANGOHOKOSHINKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE5);
        }
        #endregion

        #region 電子証明書情報データ項目編集
        // ************************************************************************************************
        // * メソッド名     電子証明書情報データ項目編集
        // * 
        // * 構文           Private SetDenshiShomeishoMSTEntity()
        // * 
        // * 機能           電子証明書情報データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetDenshiShomeishoMSTEntity(ref StringBuilder strAtenaSQLsb)
        {
            if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll && m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1)
            {
                strAtenaSQLsb.AppendFormat(", DS3.{0}", ABDENSHISHOMEISHOMSTEntity.SERIALNO);
            }
        }
        #endregion

        #region 宛名履歴標準テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     宛名履歴標準テーブルJOIN句作成
        // * 
        // * 構文           Private SetHyojunJoin()
        // * 
        // * 機能           宛名履歴標準テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetHyojunJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiHyojunEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO, ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RIREKINO);
        }
        #endregion

        #region 宛名履歴付随標準テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随標準テーブルJOIN句作成
        // * 
        // * 構文           Private SetFZYHyojunJoin()
        // * 
        // * 機能           宛名履歴付随標準テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYHyojunJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO, ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RIREKINO);
        }
        #endregion

        #region 不現住情報テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     不現住情報テーブルJOIN句作成
        // * 
        // * 構文           Private SetFugenjuJoin()
        // * 
        // * 機能           不現住情報テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFugenjuJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABFugenjuJohoEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.JUMINCD);
        }
        #endregion

        #region 共通番号標準テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     共通番号標準テーブルJOIN句作成
        // * 
        // * 構文           Private SetMyNumberHyojunJoin()
        // * 
        // * 機能           共通番号標準テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetMyNumberHyojunJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABMyNumberHyojunEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER, ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.MYNUMBER);
        }
        #endregion

        #region 電子証明書情報テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     電子証明書情報テーブルJOIN句作成
        // * 
        // * 構文           Private SetDenshiShomeishoMST()
        // * 
        // * 機能           電子証明書情報テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetDenshiShomeishoMSTJoin(ref StringBuilder strAtenaSQLsb)
        {
            if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll && m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1)
            {
                // 電子証明書情報テーブルは処理日時が最新のレコードのみを結合する。
                strAtenaSQLsb.Append(" LEFT OUTER JOIN ");
                strAtenaSQLsb.AppendFormat("(SELECT DS1.* FROM {0} AS DS1 INNER JOIN (SELECT {1}, {2}, MAX({3}) AS {3} FROM {0} GROUP BY {1}, {2}) AS DS2 ON DS1.{1} = DS2.{1} AND DS1.{2} = DS2.{2} AND DS1.{3} = DS2.{3}) AS DS3 ", ABDENSHISHOMEISHOMSTEntity.TABLE_NAME, ABDENSHISHOMEISHOMSTEntity.JUMINCD, ABDENSHISHOMEISHOMSTEntity.STAICD, ABDENSHISHOMEISHOMSTEntity.SHORINICHIJI);

                strAtenaSQLsb.AppendFormat(" ON {0}.{1} = DS3.{2} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, ABDENSHISHOMEISHOMSTEntity.JUMINCD);
                strAtenaSQLsb.AppendFormat(" AND {0}.{1} = DS3.{2} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAICD, ABDENSHISHOMEISHOMSTEntity.STAICD);
            }
        }
        #endregion
        // *履歴番号 000037 2023/03/10 追加終了

        #region 宛名履歴取得
        // ************************************************************************************************
        // * メソッド名     宛名履歴データ抽出
        // * 
        // * 構文           Public Function GetAtenaRirekiByRirekiNO(ByVal strJuminCD As String, ByVal strRirekiNO As String) As DataSet
        // * 
        // * 機能　　    　　宛名履歴マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD   : 住民コード
        // * 　　           strRirekiNO  : 履歴番号
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRirekiByRirekiNO(string strJuminCD, string strRirekiNO)
        {
            UFParameterClass cfUFParameterClass;
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();
            DataSet csDataSchma;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // SQL文の作成
                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiEntity.TABLE_NAME);

                csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, false);

                // WHERE句の作成
                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // 住民コード
                strSQL.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiEntity.JUMINCD, ABAtenaRirekiEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                strSQL.AppendFormat(" AND {0} = {1}", ABAtenaRirekiEntity.RIREKINO, ABAtenaRirekiEntity.KEY_RIREKINO);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO;
                cfUFParameterClass.Value = strRirekiNO;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 住民住登外区分
                strSQL.AppendFormat(" AND {0} = '1'", ABAtenaRirekiEntity.JUMINJUTOGAIKB);
                // 削除フラグ
                strSQL.AppendFormat(" AND {0} <> '1'", ABAtenaRirekiEntity.SAKUJOFG);

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")

                csAtenaRirekiEntity = csDataSchma.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

                // デバッグログ出力
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
                // エラーをそのままスローする
                throw objExp;
            }

            return csAtenaRirekiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     SELECT句の作成
        // * 
        // * 構文           Private Sub CreateSelect() As String
        // * 
        // * 機能　　    　 SELECT句を生成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String    :   SELECT句
        // ************************************************************************************************
        private string CreateSelect()
        {
            const string THIS_METHOD_NAME = "CreateSelect";
            var csSELECT = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の作成
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiEntity.JUMINCD);               // 住民コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHICHOSONCD);                // 市町村コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KYUSHICHOSONCD);             // 旧市町村コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RIREKINO);                   // 履歴番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RRKST_YMD);                  // 履歴開始年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RRKED_YMD);                  // 履歴終了年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINJUTOGAIKB);             // 住民住登外区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINYUSENIKB);              // 住民優先区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTOGAIYUSENKB);             // 住登外優先区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ATENADATAKB);                // 宛名データ区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAICD);                     // 世帯コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINHYOCD);                 // 住民票コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIRINO);                    // 整理番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ATENADATASHU);               // 宛名データ種別
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANYOKB1);                   // 汎用区分1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KJNHJNKB);                   // 個人法人区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANYOKB2);                   // 汎用区分2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANNAIKANGAIKB);             // 管内管外区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAMEISHO1);                // カナ名称1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIMEISHO1);               // 漢字名称1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAMEISHO2);                // カナ名称2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIMEISHO2);               // 漢字名称2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHJNKEITAI);             // 漢字法人形態
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI);     // 漢字法人代表者氏名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANJIMEISHO);          // 検索用漢字名称
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KYUSEI);                     // 旧姓
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANASEIMEI);           // 検索用カナ姓名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANASEI);              // 検索用カナ姓
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANAMEI);              // 検索用カナ名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIRRKNO);                  // 住基履歴番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.UMAREYMD);                   // 生年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.UMAREWMD);                   // 生和暦年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIBETSUCD);                 // 性別コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIBETSU);                   // 性別
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEKINO);                     // 籍番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINHYOHYOJIJUN);           // 住民票表示順
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZOKUGARACD);                 // 続柄コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZOKUGARA);                   // 続柄
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN);       // 第2住民票表示順
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2ZOKUGARACD);             // 第2続柄コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2ZOKUGARA);               // 第2続柄
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAINUSJUMINCD);             // 世帯主住民コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAINUSMEI);                 // 世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANASTAINUSMEI);             // カナ世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2STAINUSJUMINCD);         // 第2世帯主住民コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2STAINUSMEI);             // 第2世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANADAI2STAINUSMEI);         // カナ第2世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.YUBINNO);                    // 郵便番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUSHOCD);                    // 住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUSHO);                      // 住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD1);                  // 番地コード1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD2);                  // 番地コード2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD3);                  // 番地コード3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHI);                     // 番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKIFG);                 // 方書フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKICD);                 // 方書コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKI);                   // 方書
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RENRAKUSAKI1);               // 連絡先1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RENRAKUSAKI2);               // 連絡先2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HON_ZJUSHOCD);               // 本籍全国住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HON_JUSHO);                  // 本籍住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HONSEKIBANCHI);              // 本籍番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HITTOSH);                    // 筆頭者
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINIDOYMD);                 // 直近異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINJIYUCD);                 // 直近事由コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINJIYU);                   // 直近事由
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINTDKDYMD);                // 直近届出年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINTDKDTUCIKB);             // 直近届出通知区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUIDOYMD);               // 登録異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUIDOWMD);               // 登録異動和暦年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUJIYUCD);               // 登録事由コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUJIYU);                 // 登録事由
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDYMD);              // 登録届出年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDWMD);              // 登録届出和暦年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDTUCIKB);           // 登録届出通知区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIIDOYMD);                // 住定異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIIDOWMD);                // 住定異動和暦年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIJIYUCD);                // 住定事由コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIJIYU);                  // 住定事由
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDYMD);               // 住定届出年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDWMD);               // 住定届出和暦年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDTUCIKB);            // 住定届出通知区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOIDOYMD);                // 消除異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOJIYUCD);                // 消除事由コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOJIYU);                  // 消除事由
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOTDKDYMD);               // 消除届出年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOTDKDTUCIKB);            // 消除届出通知区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD);       // 転出予定異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD);        // 転出確定異動年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD);     // 転出確定通知年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD);         // 転出入理由コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUNYURIYU);           // 転出入理由
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_YUBINNO);           // 転入前住所郵便番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD);          // 転入前住所全国住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_JUSHO);             // 転入前住所住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_BANCHI);            // 転入前住所番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI);          // 前住所方書
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI);        // 転入前住所世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO);      // 転出予定郵便番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD);     // 転出予定全国住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO);        // 転出予定住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI);       // 転出予定番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI);     // 転出予定方書
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI);   // 転出予定世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO);       // 転出確定郵便番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD);      // 転出確定全国住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO);         // 転出確定住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI);        // 転出確定番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI);      // 転出確定方書
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI);    // 転出確定世帯主名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG);       // 転出確定未届フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOYMD);                    // 備考年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKO);                       // 備考
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG);   // 備考転出確定住所フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANNO);                      // 版番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIATOFG);                // 改製後フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIMAEFG);                // 改製前フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIYMD);                  // 改製年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.GYOSEIKUCD);                 // 行政区コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.GYOSEIKUMEI);                // 行政区名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD1);                   // 地区コード1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI1);                  // 地区名1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD2);                   // 地区コード2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI2);                  // 地区名2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD3);                   // 地区コード3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI3);                  // 地区名3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOHYOKUCD);                  // 投票区コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOGAKKOKUCD);               // 小学校区コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHUGAKKOKUCD);               // 中学校区コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HOGOSHAJUMINCD);             // 保護者住民コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHOGOSHAMEI);            // 漢字保護者名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAHOGOSHAMEI);             // カナ保護者名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KIKAYMD);                    // 帰化年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KARIIDOKB);                  // 仮異動区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHORITEISHIKB);              // 処理停止区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIYUBINNO);                // 住基郵便番号
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHORIYOKUSHIKB);             // 処理抑止区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIJUSHOCD);                // 住基住所コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIJUSHO);                  // 住基住所
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD1);              // 住基番地コード1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD2);              // 住基番地コード2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD3);              // 住基番地コード3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHI);                 // 住基番地
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKIFG);             // 住基方書フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKICD);             // 住基方書コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKI);               // 住基方書
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIGYOSEIKUCD);             // 住基行政区コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIGYOSEIKUMEI);            // 住基行政区名
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD1);               // 住基地区コード1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI1);              // 住基地区名1
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD2);               // 住基地区コード2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI2);              // 住基地区名2
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD3);               // 住基地区コード3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI3);              // 住基地区名3
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAOKUSHIKIKB);               // 家屋敷区分
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOZEIMOKU);                // 備考税目
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOKUSEKICD);                 // 国籍コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOKUSEKI);                   // 国籍
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUSKAKCD);               // 在留資格コード
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUSKAK);                 // 在留資格
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUKIKAN);                // 在留期間
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYU_ST_YMD);              // 在留開始年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYU_ED_YMD);              // 在留終了年月日
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RESERCE);                    // リザーブ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TANMATSUID);                 // 端末ID
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUJOFG);                   // 削除フラグ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINCOUNTER);              // 更新カウンタ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUSEINICHIJI);             // 作成日時
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUSEIUSER);                // 作成ユーザ
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINNICHIJI);              // 更新日時
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINUSER);                 // 更新ユーザ
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
                // エラーをそのままスローする
                throw objExp;
            }

            return csSELECT.ToString();

        }
        #endregion
    }
}