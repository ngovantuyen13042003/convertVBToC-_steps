// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名マスタＤＡ(ABAtenaBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/20　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/02/24 000001     生年月日のあいまい検索処理のバグ
// * 2003/02/25 000002     データ区分がある時も、データ種別が入っている場合は、データ種別も検索とする
// *                       住基優先で世帯コードが、指定されている場合に住民票表示順をソートキーにする
// * 2003/03/10 000003     住所ＣＤ等の整合性チェックに誤り
// * 2003/03/27 000004     エラー処理クラスの参照先を"AB"固定にする
// * 2003/03/31 000005     整合性チェックをTrimした値でチェックする
// * 2003/04/16 000006     生和暦年月日の日付チェックを数値チェックに変更
// *                       検索用カナの半角カナチェックをＡＮＫチェックに変更
// * 2003/05/20 000007     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/06/12 000008     TOP句を外す
// * 2003/08/28 000009     RDBアクセスログの修正
// * 2003/09/11 000010     端末ＩＤ整合性チェックをANKにする
// * 2003/10/09 000011     作成ユーザー・更新ユーザーチェックの変更
// * 2003/10/30 000012     仕様変更：カタカナチェックをANKチェックに変更
// * 2003/11/18 000013     仕様変更：データ区分で個人のみ持ってくる。（データ区分に"1%"と指定された場合）
// *                       仕様追加：宛名個別データ取得メソッドを追加
// * 2004/08/27 000014     速度改善：（宮沢）
// * 2004/10/19 000015     ～全国住所コードのチェックをCheckNumber --> CheckANK(マルゴ村山)
// * 2004/11/12 000016     データチェックを行なわない
// * 2005/01/25 000017     速度改善２：（宮沢）
// * 2005/05/23 000018     SQL文をInsert,Update,論理Delete,物理Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
// * 2005/07/11 000019     CreateWhereﾒｿｯﾄﾞで住所CDのWhere文作成時に住所CDか全国住所CDかの判定を入れる(マルゴ村山)
// * 2005/12/26 000020     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
// * 2006/07/31 000021     年金宛名ゲットⅡ項目追加(吉澤)
// * 2007/04/28 000022     介護版宛名取得メソッドの追加による取得項目の追加 (吉澤)
// * 2007/09/03 000023     外国人本名優先検索用に漢字名称２を追加（中沢）
// * 2007/10/10 000024     外国人本名優先検索機能：カナ名の先頭が"ｳ"のときは"ｵ"とOR条件で検索する（中沢）
// * 2008/01/15 000025     個別事項データ取得機能に後期高齢取得処理を追加（比嘉）＆ネーミング変更（吉澤）
// * 2010/04/16 000026     VS2008対応（比嘉）
// * 2010/05/12 000027     本籍筆頭者及び処理停止区分対応（比嘉）
// * 2011/05/18 000028     外国人在留情報取得区分対応（比嘉）
// * 2011/10/24 000029     【AB17010】＜住基法改正対応＞宛名付随マスタ追加   (小松)
// * 2014/04/28 000030     【AB21040】＜共通番号対応＞共通番号マスタ追加（石合）
// * 2018/03/08 000031     【AB26001】履歴検索機能追加（石合）
// * 2020/01/10 000032     【AB32001】アルファベット検索（石合）
// * 2023/03/10 000033     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2023/08/14 000034     【AB-0820-1】住登外管理項目追加(早崎)
// * 2023/10/19 000035     【AB-0820-1】住登外管理項目追加_追加修正(仲西)
// * 2023/12/04 000036     【AB-1600-1】検索機能対応(下村)
// * 2023/12/11 000037     【AB-9000-1】住基更新連携標準化対応(下村)
// * 2024/03/07 000038     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// * 2024/06/06 000039     【AB-9901-1】不具合対応
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{
    // *履歴番号 000034 2023/08/14 追加終了

    // ************************************************************************************************
    // *
    // * 宛名マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaBClass
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
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション

        // * 履歴番号 000014 2004/08/27 追加開始（宮沢）
        // * 履歴番号 000017 2005/01/25 変更開始（宮沢）
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
                                                        // * 履歴番号 000017 2005/01/25 変更終了
                                                        // * 履歴番号 000014 2004/08/27 追加終了

        // * 履歴番号 000017 2005/01/25 追加開始（宮沢）
        private StringBuilder m_strAtenaSQLsbAll = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbKaniAll = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbKaniOnly = new StringBuilder();
        private StringBuilder m_strAtenaSQLsbNenkinAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbKaniAll = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbKaniOnly = new StringBuilder();
        private StringBuilder m_strKobetuSQLsbNenkinAll = new StringBuilder();
        public ABEnumDefine.AtenaGetKB m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll; // 全項目選択（m_blnAtenaGetがTrueの時宛名Getで必要な項目全てそれ以外はSELECT *）
        public bool m_blnSelectCount = false;            // カウントを取得するかどうか
        public bool m_blnBatch = false;               // バッチフラグ
                                                      // * 履歴番号 000017 2005/01/25 追加終了

        // *履歴番号 000022 2007/04/28 追加開始
        public ABEnumDefine.MethodKB m_blnMethodKB;  // メソッド区分（通常版か、介護版、、、）
                                                     // *履歴番号 000022 2007/04/28 追加終了
                                                     // *履歴番号 000025 2008/01/15 追加開始
        public string m_strKobetsuShutokuKB;                  // 個別事項取得区分
                                                              // *履歴番号 000025 2008/01/15 追加終了

        // *履歴番号 000027 2010/05/12 追加開始
        private string m_strHonsekiKB = string.Empty;                 // 宛名管理情報:本籍取得
        private string m_strShoriteishiKB = string.Empty;             // 宛名管理情報:処理停止区分取得
        private string m_strHonsekiHittoshKB_Param = string.Empty;    // 本籍筆頭者取得区分パラメータ
        private string m_strShoriteishiKB_Param = string.Empty;       // 処理停止区分取得区分パラメータ
                                                                      // *履歴番号 000027 2010/05/12 追加終了

        // *履歴番号 000028 2011/05/18 追加開始
        private string m_strFrnZairyuJohoKB_Param = string.Empty;     // 外国人在留情報取得区分パラメータ
                                                                      // *履歴番号 000028 2011/05/18 追加終了

        // *履歴番号 000029 2011/10/24 追加開始
        private ABSekoYMDHanteiBClass m_csSekoYMDHanteiB;             // 施行日判定Bｸﾗｽ
        private ABAtenaFZYBClass m_csAtenaFZYB;                       // 宛名付随マスタBｸﾗｽ
        private bool m_blnJukihoKaiseiFG = false;
        private string m_strJukihoKaiseiKB;                           // 住基法改正区分
                                                                      // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000030 2014/04/28 追加開始
        private string m_strMyNumberKB_Param;                         // 共通番号取得区分
        private string m_strMyNumberChokkinSearchKB_Param;            // 共通番号直近検索区分
                                                                      // *履歴番号 000030 2014/04/28 追加終了

        // *履歴番号 000032 2020/01/10 追加開始
        private ABKensakuShimeiBClass m_cKensakuShimeiB;              // 検索氏名編集ビジネスクラス
                                                                      // *履歴番号 000032 2020/01/10 追加終了

        public ABEnumDefine.HyojunKB m_intHyojunKB;                   // 宛名GET標準化区分

        // *履歴番号 000034 2023/08/14 追加開始
        private ABAtena_HyojunBClass m_csAtenaHyojunB;                // 宛名_標準マスタBｸﾗｽ
        private ABAtenaFZY_HyojunBClass m_csAtenaFZYHyojunB;          // 宛名付随_標準マスタBｸﾗｽ
                                                                      // *履歴番号 000034 2023/08/14 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaBClass";                       // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード

        private const string JUKIHOKAISEIKB_ON = "1";

        #endregion

        // *履歴番号 000027 2010/05/12 追加開始
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
                m_strShoriteishiKB_Param = value;
            }
        }

        // *履歴番号 000028 2011/05/18 追加開始
        public string p_strFrnZairyuJohoKB      // 外国人在留情報取得区分
        {
            set
            {
                m_strFrnZairyuJohoKB_Param = value;
            }
        }
        // *履歴番号 000028 2011/05/18 追加終了

        // *履歴番号 000029 2011/10/24 追加開始
        public string p_strJukihoKaiseiKB      // 住基法改正区分
        {
            set
            {
                m_strJukihoKaiseiKB = value;
            }
        }
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000030 2014/04/28 追加開始
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
        // *履歴番号 000030 2014/04/28 追加終了

        #endregion
        // *履歴番号 000027 2010/05/12 追加終了

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
        public ABAtenaBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;
            m_cfDelButuriUFParameterCollectionClass = null;

            // *履歴番号 000029 2011/10/24 追加開始
            // 住基法改正区分初期化
            m_strJukihoKaiseiKB = string.Empty;
            // 住基法改正ﾌﾗｸﾞ取得
            GetJukihoKaiseiFG();
            // *履歴番号 000029 2011/10/24 追加終了

            // *履歴番号 000030 2014/04/28 追加開始
            // 共通番号取得区分初期化
            m_strMyNumberKB_Param = string.Empty;
            // 共通番号　宛名取得　直近検索区分取得
            GetMyNumberChokkinSearchKB();
            // *履歴番号 000030 2014/04/28 追加終了

            // *履歴番号 000032 2020/01/10 追加開始
            // 検索氏名編集ビジネスクラスのインスタンス化
            m_cKensakuShimeiB = new ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass);
            // *履歴番号 000032 2020/01/10 追加終了

        }
        // * 履歴番号 000017 2005/01/25 追加開始（宮沢）
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
        public ABAtenaBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass, ABEnumDefine.AtenaGetKB blnSelectAll, bool blnSelectCount)
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
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;
            m_cfDelButuriUFParameterCollectionClass = null;

            m_blnSelectAll = blnSelectAll;
            m_blnSelectCount = blnSelectCount;

            // *履歴番号 000027 2010/05/12 追加開始
            // 管理情報取得処理
            GetKanriJoho();
            // *履歴番号 000027 2010/05/12 追加終了

            // *履歴番号 000029 2011/10/24 追加開始
            // 住基法改正区分初期化
            m_strJukihoKaiseiKB = string.Empty;
            // 住基法改正ﾌﾗｸﾞ取得
            GetJukihoKaiseiFG();
            // *履歴番号 000029 2011/10/24 追加終了

            // *履歴番号 000030 2014/04/28 追加開始
            // 共通番号取得区分初期化
            m_strMyNumberKB_Param = string.Empty;
            // 共通番号　宛名取得　直近検索区分取得
            GetMyNumberChokkinSearchKB();
            // *履歴番号 000030 2014/04/28 追加終了

            // *履歴番号 000032 2020/01/10 追加開始
            // 検索氏名編集ビジネスクラスのインスタンス化
            m_cKensakuShimeiB = new ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass);
            // *履歴番号 000032 2020/01/10 追加終了

        }
        // * 履歴番号 000017 2005/01/25 追加終了
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaBHoshu(ByVal intGetCount As Integer, _
        // *                                               ByVal cSearchKey As ABAtenaSearchKey) As DataSet
        // * 
        // * 機能　　    　　宛名マスタより該当データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // *                cSearchKey    : 宛名マスタ検索キー
        // * 
        // * 戻り値         DataSet : 取得した宛名マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaBHoshu(int intGetCount, ABAtenaSearchKey cSearchKey)
        {

            return GetAtenaBHoshu(intGetCount, cSearchKey, false);

        }

        // ************************************************************************************************
        // * メソッド名     宛名マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaBHoshu(ByVal intGetCount As Integer, 
        // *                                               ByVal cSearchKey As ABAtenaSearchKey, 
        // *                                               ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名マスタより該当データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // *                cSearchKey    : 宛名マスタ検索キー
        // *                blnSakujoFG   : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaBHoshu(int intGetCount, ABAtenaSearchKey cSearchKey, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetAtenaBHoshu";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            // * corresponds to VS2008 Start 2010/04/16 000026
            // Dim csDataTable As DataTable
            // * corresponds to VS2008 End 2010/04/16 000026
            // * 履歴番号 000017 2005/01/25 更新開始（宮沢）
            // Dim strSQL As String
            var strSQL = new StringBuilder();
            string strSQLExec;
            // * 履歴番号 000017 2005/01/25 更新終了

            StringBuilder strWHERE;
            // * 履歴番号 000017 2005/01/25 更新開始（宮沢）
            // Dim strORDER As String
            var strORDER = new StringBuilder();
            // * 履歴番号 000017 2005/01/25 更新終了

            int intMaxRows;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                if (intGetCount < 0 | intGetCount > 999)    // 取得件数の誤り
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_PARA_GETCOUNT);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成
                // * 履歴番号 000008 2003/06/12 修正開始
                // If intGetCount = 0 Then
                // strSQL = "SELECT TOP 100 * FROM " + ABAtenaEntity.TABLE_NAME
                // Else
                // strSQL = "SELECT TOP " + intGetCount.ToString() + " * FROM " + ABAtenaEntity.TABLE_NAME
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
                // * 履歴番号 000017 2005/01/25 更新開始（宮沢）
                // strSQL = "SELECT * FROM " + ABAtenaEntity.TABLE_NAME
                switch (m_blnSelectAll)
                {
                    case var @case when @case == ABEnumDefine.AtenaGetKB.KaniAll:
                        {
                            if (m_strAtenaSQLsbKaniAll.RLength() == 0)
                            {
                                m_strAtenaSQLsbKaniAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strAtenaSQLsbKaniAll.Append(ABAtenaEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbKaniAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniAll;
                            break;
                        }
                    case var case1 when case1 == ABEnumDefine.AtenaGetKB.KaniOnly:
                        {
                            if (m_strAtenaSQLsbKaniOnly.RLength() == 0)
                            {
                                m_strAtenaSQLsbKaniOnly.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbKaniOnly);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strAtenaSQLsbKaniOnly.Append(ABAtenaEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbKaniOnly);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaKaniOnly;
                            break;
                        }
                    case var case2 when case2 == ABEnumDefine.AtenaGetKB.NenkinAll:
                        {
                            if (m_strAtenaSQLsbNenkinAll.RLength() == 0)
                            {
                                m_strAtenaSQLsbNenkinAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strAtenaSQLsbNenkinAll);

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strAtenaSQLsbNenkinAll.Append(ABAtenaEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbNenkinAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetFZYHyojunJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetFugenjuJoin(ref m_strAtenaSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strAtenaSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchma = m_csDataSchmaNenkinAll;
                            break;
                        }

                    default:
                        {
                            if (m_strAtenaSQLsbAll.RLength() == 0)
                            {
                                m_strAtenaSQLsbAll.Append("SELECT ");
                                // 現行
                                m_strAtenaSQLsbAll.Append(ABAtenaEntity.TABLE_NAME).Append(".*");

                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strAtenaSQLsbAll.Append(ABAtenaEntity.TABLE_NAME);

                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strAtenaSQLsbAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strAtenaSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
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
                // m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
                // Case ABEnumDefine.AtenaGetKB.KaniAll
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case ABEnumDefine.AtenaGetKB.KaniOnly
                // Call SetAtenaEntity(m_strAtenaSQLsb)
                // Case Else
                // '現行
                // m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
                // End Select

                // '代理人等のカウントを取得
                // Call SetAtenaCountEntity(m_strAtenaSQLsb)

                // m_strAtenaSQLsb.Append(" FROM ")
                // m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME)

                // '代理人等のカウントを取得
                // Call SetAtenaJoin(m_strAtenaSQLsb)
                // End If
                // strSQL.Append(m_strAtenaSQLsb)
                // * 履歴番号 000017 2005/01/25 更新終了

                // * 履歴番号 000008 2003/06/12 修正終了

                // * 履歴番号 000014 2004/08/27 追加開始（宮沢）
                // If (m_csDataSchma Is Nothing) Then
                // m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                // End If
                // * 履歴番号 000014 2004/08/27 追加終了
                // WHERE句の作成
                // *履歴番号 000031 2018/03/08 修正開始
                // strWHERE = New StringBuilder(Me.CreateWhere(cSearchKey))

                // ' 削除フラグ
                // If blnSakujoFG = False Then
                // If Not (strWHERE.Length = 0) Then
                // strWHERE.Append(" AND ")
                // End If
                // strWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
                // strWHERE.Append(" <> '1'")
                // End If
                strWHERE = new StringBuilder(CreateWhereMain(cSearchKey, blnSakujoFG));
                // *履歴番号 000031 2018/03/08 修正終了

                // ORDER句を結合

                // 住民優先区分が”1”でかつ世帯コードが指定済の場合：住民票表示順
                // * 履歴番号 000017 2005/01/25 更新開始（宮沢）
                // If ((cSearchKey.p_strJuminYuseniKB = "1") And (cSearchKey.p_strStaiCD.Trim() <> String.Empty)) Then
                // strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINHYOHYOJIJUN + " ASC,"
                // strORDER += ABAtenaEntity.JUMINCD + " ASC;"
                // ElseIf Not (cSearchKey.p_strUmareYMD.Trim() = String.Empty) Then
                // strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.UMAREYMD + " ASC,"
                // strORDER += ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINCD + " ASC;"
                // Else
                // strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.SEARCHKANASEIMEI + " ASC,"
                // strORDER += ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINCD + " ASC;"
                // End If
                // If strWHERE.Length = 0 Then
                // strSQL += strORDER
                // Else
                // strSQL += " WHERE " + strWHERE.ToString() + strORDER
                // End If
                if (cSearchKey.p_strJuminYuseniKB == "1" & cSearchKey.p_strStaiCD.Trim() != string.Empty)
                {
                    strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }
                else if (!(cSearchKey.p_strUmareYMD.Trim() == string.Empty))
                {
                    strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }
                else
                {
                    strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }

                if (strWHERE.RLength() == 0)
                {
                    strSQL.Append(strORDER);
                }
                else
                {
                    strSQL.Append(" WHERE ").Append(strWHERE).Append(strORDER);
                }
                strSQLExec = strSQL.ToString();
                // * 履歴番号 000017 2005/01/25 更新終了

                // *履歴番号 000009 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // '* 履歴番号 000017 2005/01/25 更新開始（宮沢）If 文で囲む
                // If (m_blnBatch = False) Then
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQLExec, m_cfSelectUFParameterCollectionClass) + "】")
                // End If
                // * 履歴番号 000017 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000009 2003/08/28 修正終了

                // SQLの実行 DataSetの取得

                // * 履歴番号 000014 2004/08/27 変更開始（宮沢）
                // csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaEntity = m_csDataSchma.Clone();
                // m_csDataSchma.Clear()
                // csAtenaEntity = m_csDataSchma
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQLExec, csAtenaEntity, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // * 履歴番号 000014 2004/08/27 変更終了

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

            return csAtenaEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名個別データ抽出
        // * 
        // * 構文           Friend Function GetAtenaBKobetsu(ByVal intGetCount As Integer, 
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, 
        // *                                                ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名マスタより該当データと個別データを取得する
        // * 
        // * 引数           intGetCount   : 取得件数
        // *                cSearchKey    : 宛名マスタ検索キー
        // *                blnSakujoFG   : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名マスタの該当データ
        // ************************************************************************************************
        // *履歴番号 000025 2008/01/15 修正開始
        // Friend Function GetAtenaBKobetsu(ByVal intGetCount As Integer, _
        // ByVal cSearchKey As ABAtenaSearchKey, _
        // ByVal blnSakujoFG As Boolean) As DataSet
        internal DataSet GetAtenaBKobetsu(int intGetCount, ABAtenaSearchKey cSearchKey, bool blnSakujoFG, string strKobetsuKB)
        {
            // *履歴番号 000025 2008/01/15 修正終了
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            // * corresponds to VS2008 Start 2010/04/16 000026
            // Dim csDataTable As DataTable
            // * corresponds to VS2008 End 2010/04/16 000026
            var strSQL = new StringBuilder();
            // * 履歴番号 000017 2005/01/25 追加開始（宮沢）
            string strSQLExec;
            // * 履歴番号 000017 2005/01/25 追加終了

            StringBuilder strWHERE;
            var strORDER = new StringBuilder();
            int intMaxRows;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // パラメータチェック
                if (intGetCount < 0 | intGetCount > 999)    // 取得件数の誤り
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_PARA_GETCOUNT);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 宛名検索キーのチェック
                // なし

                // *履歴番号 000025 2008/01/15 追加開始
                // 個別事項取得区分をメンバ変数にセット
                m_strKobetsuShutokuKB = strKobetsuKB.Trim();
                // *履歴番号 000025 2008/01/15 追加終了

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

                // * 履歴番号 000017 2005/01/25 更新開始（宮沢）IF文で囲む
                // ' SELECT ABATENA.*
                // strSQL.Append("SELECT ").Append(ABAtenaEntity.TABLE_NAME).Append(".*")
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
                switch (m_blnSelectAll)
                {
                    case var @case when @case == ABEnumDefine.AtenaGetKB.KaniAll:
                        {
                            if (m_strKobetuSQLsbKaniAll.RLength() == 0)
                            {
                                m_strKobetuSQLsbKaniAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbKaniAll);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbKaniAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbKaniAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strKobetuSQLsbKaniAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbKaniAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbKaniAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbKaniAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbKaniAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbKaniAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKobetuKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniAll;
                            break;
                        }
                    case var case1 when case1 == ABEnumDefine.AtenaGetKB.KaniOnly:
                        {
                            if (m_strKobetuSQLsbKaniOnly.RLength() == 0)
                            {
                                m_strKobetuSQLsbKaniOnly.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbKaniOnly);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbKaniOnly);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbKaniOnly);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strKobetuSQLsbKaniOnly.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbKaniOnly);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbKaniOnly);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbKaniOnly);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbKaniOnly);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbKaniOnly);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKobetuKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniOnly;
                            break;
                        }
                    case var case2 when case2 == ABEnumDefine.AtenaGetKB.NenkinAll:
                        {
                            if (m_strKobetuSQLsbNenkinAll.RLength() == 0)
                            {
                                m_strKobetuSQLsbNenkinAll.Append("SELECT ");
                                SetAtenaEntity(ref m_strKobetuSQLsbNenkinAll);
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbNenkinAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbNenkinAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strKobetuSQLsbNenkinAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbNenkinAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbNenkinAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbNenkinAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetHyojunJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetFZYHyojunJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetFugenjuJoin(ref m_strKobetuSQLsbNenkinAll);
                                    SetDenshiShomeishoMSTJoin(ref m_strKobetuSQLsbNenkinAll);
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKobetuNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
                            }
                            m_csDataSchmaKobetu = m_csDataSchmaKobetuNenkinAll;
                            break;
                        }

                    default:
                        {
                            if (m_strKobetuSQLsbAll.RLength() == 0)
                            {
                                m_strKobetuSQLsbAll.Append("SELECT ");
                                // 現行
                                m_strKobetuSQLsbAll.Append(ABAtenaEntity.TABLE_NAME).Append(".*");
                                // 個別事項の項目セット
                                SetKobetsuEntity(ref m_strKobetuSQLsbAll);
                                // 代理人等のカウントを取得
                                SetAtenaCountEntity(ref m_strKobetuSQLsbAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYEntity(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）の場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                                {
                                    SetMyNumberEntity(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                m_strKobetuSQLsbAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME);
                                // 個別事項のJOIN句を作成
                                SetKobetsuJoin(ref m_strKobetuSQLsbAll);
                                // 代理人等のカウントを取得
                                SetAtenaJoin(ref m_strKobetuSQLsbAll);

                                // *履歴番号 000029 2011/10/24 追加開始
                                // 住基法改正以降は宛名付随マスタを付加
                                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON || m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    SetFZYJoin(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // 処理なし
                                }
                                // *履歴番号 000029 2011/10/24 追加終了

                                // *履歴番号 000030 2014/04/28 追加開始
                                // 共通番号取得区分が"1"（取得する）、または共通番号が指定されている場合、共通番号マスタを付加
                                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                                {
                                    SetMyNumberJoin(ref m_strKobetuSQLsbAll);
                                }
                                else
                                {
                                    // noop
                                }
                                // *履歴番号 000030 2014/04/28 追加終了

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
                                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON || cSearchKey.p_strMyNumber.Trim().RLength() > 0)
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
                                m_csDataSchmaKobetuAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, false);
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
                // m_strKobetuSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
                // Case ABEnumDefine.AtenaGetKB.KaniAll
                // Call SetAtenaEntity(m_strKobetuSQLsb)
                // Case ABEnumDefine.AtenaGetKB.KaniOnly
                // Call SetAtenaEntity(m_strKobetuSQLsb)
                // Case Else
                // '現行
                // m_strKobetuSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
                // End Select

                // '個別事項の項目セット
                // Call SetKobetsuEntity(m_strKobetuSQLsb)

                // '代理人等のカウントを取得
                // Call SetAtenaCountEntity(m_strKobetuSQLsb)

                // '  FROM ABATENA 
                // m_strKobetuSQLsb.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)

                // '個別事項のJOIN句を作成
                // Call SetKobetsuJoin(m_strKobetuSQLsb)

                // '代理人等のカウントを取得
                // Call SetAtenaJoin(m_strKobetuSQLsb)
                // End If
                // strSQL.Append(m_strKobetuSQLsb)
                // '* 履歴番号 000017 2005/01/25 更新終了（宮沢）IF文で囲む

                // '* 履歴番号 000014 2004/08/27 追加開始（宮沢）
                // If (m_csDataSchmaKobetu Is Nothing) Then
                // m_csDataSchmaKobetu = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                // End If
                // '* 履歴番号 000014 2004/08/27 追加終了

                // WHERE句の作成
                // *履歴番号 000031 2018/03/08 修正開始
                // strWHERE = New StringBuilder(Me.CreateWhere(cSearchKey))

                // ' 削除フラグ
                // If blnSakujoFG = False Then
                // If Not (strWHERE.Length = 0) Then
                // strWHERE.Append(" AND ")
                // End If
                // strWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
                // strWHERE.Append(" <> '1'")
                // End If
                strWHERE = new StringBuilder(CreateWhereMain(cSearchKey, blnSakujoFG));
                // *履歴番号 000031 2018/03/08 修正終了

                // ORDER句を結合

                // 住民優先区分が”1”でかつ世帯コードが指定済の場合：住民票表示順
                if (cSearchKey.p_strJuminYuseniKB == "1" & cSearchKey.p_strStaiCD.Trim() != string.Empty)
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }
                else if (!(cSearchKey.p_strUmareYMD.Trim() == string.Empty))
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }
                else
                {
                    strORDER.Append(" ORDER BY ");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(" ASC,");
                    strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;");
                }

                if (strWHERE.RLength() == 0)
                {
                    strSQL.Append(strORDER);
                }
                else
                {
                    strSQL.Append(" WHERE ").Append(strWHERE).Append(strORDER);
                }

                // * 履歴番号 000017 2005/01/25 追加開始（宮沢）
                strSQLExec = strSQL.ToString();
                // * 履歴番号 000017 2005/01/25 追加終了

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // '* 履歴番号 000017 2005/01/25 更新開始（宮沢）If 文で囲む
                // If (m_blnBatch = False) Then
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQLExec, m_cfSelectUFParameterCollectionClass) + "】")
                // End If
                // * 履歴番号 000017 2005/01/25 更新終了（宮沢）If 文で囲む

                // * 履歴番号 000014 2004/08/27 変更開始（宮沢）
                // SQLの実行 DataSetの取得
                // csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaEntity = m_csDataSchmaKobetu.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQLExec, csAtenaEntity, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // * 履歴番号 000014 2004/08/27 変更終了

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

            return csAtenaEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaB";
            // * corresponds to VS2008 Start 2010/04/16 000026
            // Dim csInstRow As DataRow
            // Dim csDataColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000026
            int intInsCnt;                            // 追加件数
            string strUpdateDateTime;
            // *履歴番号 000034 2023/08/14 追加開始
            var m_cRuijiClass = new USRuijiClass();                   // 類似文字クラス
                                                                      // *履歴番号 000034 2023/08/14 追加終了

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000018 2005/05/23 修正開始
                    // Call CreateSQL(csDataRow)
                    CreateInsertSQL(csDataRow);
                    // * 履歴番号 000018 2005/05/23 修正終了
                }

                // *履歴番号 000034 2023/08/14 追加開始
                // 検索用漢字名称に類字をセットする
                // *履歴番号 000035 2023/10/19 修正開始
                // csDataRow[ABAtenaEntity.SEARCHKANJIMEISHO] =
                // m_cRuijiClass.GetRuijiMojiList(csDataRow[ABAtenaEntity.SEARCHKANJIMEISHO].ToString())
                csDataRow[ABAtenaEntity.SEARCHKANJIMEISHO] = m_cRuijiClass.GetRuijiMojiList(UFVBAPI.ToString(csDataRow[ABAtenaEntity.SEARCHKANJIMEISHO]).Replace("　", string.Empty)).ToUpper();
                // *履歴番号 000035 2023/10/19 修正終了
                // *履歴番号 000034 2023/08/14 追加終了

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABAtenaEntity.TANMATSUID] = m_cfControlData.m_strClientId; // 端末ＩＤ
                csDataRow[ABAtenaEntity.SAKUJOFG] = "0";                               // 削除フラグ
                csDataRow[ABAtenaEntity.KOSHINCOUNTER] = decimal.Zero;                 // 更新カウンタ
                csDataRow[ABAtenaEntity.SAKUSEINICHIJI] = strUpdateDateTime;           // 作成日時
                csDataRow[ABAtenaEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;  // 作成ユーザー
                csDataRow[ABAtenaEntity.KOSHINNICHIJI] = strUpdateDateTime;            // 更新日時
                csDataRow[ABAtenaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;   // 更新ユーザー


                // '当クラスのデータ整合性チェックを行う
                // For Each csDataColumn In csDataRow.Table.Columns
                // 'データ整合性チェック
                // CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim())
                // Next csDataColumn


                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength())].ToString();


                // *履歴番号 000009 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
                // *履歴番号 000009 2003/08/28 修正終了

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
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaFZYDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intInsCnt = 0;
            int intInsCnt2 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ追加を実行
                intInsCnt = InsertAtenaB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaFZYB == null)
                    {
                        m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 作成日時、更新日時の同期
                    csAtenaFZYDr[ABAtenaFZYEntity.SAKUSEINICHIJI] = csAtenaDr[ABAtenaEntity.SAKUSEINICHIJI];
                    csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名付随マスタ追加を実行
                    intInsCnt2 = m_csAtenaFZYB.InsertAtenaFZYB(csAtenaFZYDr);
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
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000034 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
        // ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow          : 追加するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaHyojunDr As DataRow    : 追加するデータの含まれるDataRowオブジェクト（宛名_標準）
        // * 　　           csAtenaFZYDr As DataRow       : 追加するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名付随_標準）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)
        {
            int intInsCnt = 0;
            int intInsCnt2 = 0;
            int intInsCnt3 = 0;
            int intInsCnt4 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ追加を実行
                intInsCnt = InsertAtenaB(csAtenaDr);

                // '宛名_標準マスタが存在している場合
                if (!(csAtenaHyojunDr == null))
                {
                    // 宛名_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaHyojunB == null)
                    {
                        m_csAtenaHyojunB = new ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 作成日時、更新日時の同期
                    csAtenaHyojunDr[ABAtenaFZYEntity.SAKUSEINICHIJI] = csAtenaDr[ABAtenaEntity.SAKUSEINICHIJI];
                    csAtenaHyojunDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名_標準マスタ追加を実行
                    intInsCnt2 = m_csAtenaHyojunB.InsertAtenaHyojunB(csAtenaHyojunDr);

                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名付随マスタが存在する場合
                    if (!(csAtenaFZYDr == null))
                    {

                        // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYB == null)
                        {
                            m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYDr[ABAtenaFZYEntity.SAKUSEINICHIJI] = csAtenaDr[ABAtenaEntity.SAKUSEINICHIJI];
                        csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随マスタ追加を実行
                        intInsCnt3 = m_csAtenaFZYB.InsertAtenaFZYB(csAtenaFZYDr);

                    }

                    // 宛名付随_標準マスタが存在する場合
                    if (!(csAtenaFZYHyojunDr == null))
                    {

                        // 宛名付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYHyojunB == null)
                        {
                            m_csAtenaFZYHyojunB = new ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYHyojunDr[ABAtenaFZYHyojunEntity.SAKUSEINICHIJI] = csAtenaDr[ABAtenaEntity.SAKUSEINICHIJI];
                        csAtenaFZYHyojunDr[ABAtenaFZYHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随_標準マスタ追加を実行
                        intInsCnt4 = m_csAtenaFZYHyojunB.InsertAtenaFZYHyojunB(csAtenaFZYHyojunDr);

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
        // *履歴番号 000034 2023/08/14 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaB";                     // パラメータクラス
                                                                                // * corresponds to VS2008 Start 2010/04/16 000026
                                                                                // Dim csDataColumn As DataColumn
                                                                                // * corresponds to VS2008 End 2010/04/16 000026
            int intUpdCnt;                            // 更新件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000018 2005/05/23 修正開始
                    // Call CreateSQL(csDataRow)
                    CreateUpdateSQL(csDataRow);
                    // * 履歴番号 000018 2005/05/23 修正終了
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABAtenaEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaEntity.KOSHINCOUNTER]) + 1m;               // 更新カウンタ
                csDataRow[ABAtenaEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow[ABAtenaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー


                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength()) == ABAtenaEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        // *履歴番号 000008 2004/11/12 修正開始
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), csDataRow[cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current].ToString().Trim())
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                        // *履歴番号 000008 2004/11/12 修正終了
                    }
                }

                // *履歴番号 000009 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
                // *履歴番号 000009 2003/08/28 修正終了

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
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを更新する
        // * 
        // * 引数           csAtenaDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaFZYDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "UpdateAtenaB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = UpdateAtenaB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaFZYB == null)
                    {
                        m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名付随マスタ更新を実行
                    intCnt2 = m_csAtenaFZYB.UpdateAtenaFZYB(csAtenaFZYDr);
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
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000034 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
        // *                                             ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを更新する
        // * 
        // * 引数           csAtenaDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaHyojunDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名_標準）
        // * 　　           csAtenaFZYDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow : 更新するデータの含まれるDataRowオブジェクト（宛名付随_標準）
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr, bool blnJutogai = true)
        {
            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "UpdateAtenaB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = UpdateAtenaB(csAtenaDr);

                // 宛名_標準マスタが存在する場合、更新をする
                if (!(csAtenaHyojunDr == null))
                {
                    // 宛名_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaHyojunB == null)
                    {
                        m_csAtenaHyojunB = new ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaHyojunDr[ABAtenaHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名_標準マスタ更新を実行
                    if (blnJutogai)
                    {
                        intCnt2 = m_csAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunDr, csAtenaDr[ABAtenaEntity.ATENADATAKB].ToString());
                    }
                    else
                    {
                        intCnt2 = m_csAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunDr);
                    }
                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名付随マスタが存在する場合、更新をする
                    if (!(csAtenaFZYDr == null))
                    {
                        // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYB == null)
                        {
                            m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随マスタ更新を実行
                        intCnt3 = m_csAtenaFZYB.UpdateAtenaFZYB(csAtenaFZYDr);
                    }

                    // 宛名付随_標準マスタが存在する場合、更新をする
                    if (!(csAtenaFZYHyojunDr == null))
                    {
                        // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYHyojunB == null)
                        {
                            m_csAtenaFZYHyojunB = new ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYHyojunDr[ABAtenaFZYHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随マスタ更新を実行
                        intCnt4 = m_csAtenaFZYHyojunB.UpdateAtenaFZYHyojunB(csAtenaFZYHyojunDr);
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
        // *履歴番号 000034 2023/08/14 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaB";  // パラメータクラス
                                                             // * corresponds to VS2008 Start 2010/04/16 000026
                                                             // Dim csDataColumn As DataColumn
                                                             // * corresponds to VS2008 End 2010/04/16 000026
            int intDelCnt;        // 削除件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null | string.IsNullOrEmpty(m_strDelRonriSQL) | m_cfDelRonriUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000018 2005/05/23 修正開始
                    // CreateSQL(csDataRow)
                    CreateDeleteRonriSQL(csDataRow);
                    // * 履歴番号 000018 2005/05/23 修正終了
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABAtenaEntity.SAKUJOFG] = "1";                                                                 // 削除フラグ
                csDataRow[ABAtenaEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaEntity.KOSHINCOUNTER]) + 1m;               // 更新カウンタ
                csDataRow[ABAtenaEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow[ABAtenaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // * 履歴番号 000018 2005/05/23 修正開始
                // 作成済みのパラメータへ更新行から値を設定する。
                // For Each cfParam In m_cfUpdateUFParameterCollectionClass
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength()) == ABAtenaEntity.PREFIX_KEY)
                    {
                        // m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = _
                        // csDataRow(cfParam.ParameterName.Substring(ABAtenaEntity.PREFIX_KEY.Length), _
                        // DataRowVersion.Original).ToString()
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // *履歴番号 000008 2004/11/12 修正開始
                        // データ整合性チェック
                        // CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), csDataRow[cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current].ToString().Trim())
                        // *履歴番号 000008 2004/11/12 修正終了
                        // m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current].ToString()
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }
                // * 履歴番号 000018 2005/05/23 修正終了


                // *履歴番号 000009 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
                // *履歴番号 000009 2003/08/28 修正終了

                // SQLの実行
                // intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);

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
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ削除
        // * 
        // * 構文           Public Function UpdateAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを論理削除する
        // * 
        // * 引数           csAtenaDr As DataRow : 論理削除するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaFZYDr As DataRow : 論理削除するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = DeleteAtenaB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaFZYB == null)
                    {
                        m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名付随マスタ更新を実行
                    intCnt2 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr);
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
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000034 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ削除
        // * 
        // * 構文           Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, _
        // *                                                       ByVal csAtenaFZYDr As DataRow, _
        // *                                                       ByVal csAtenaHyojunDr As DataRow, _
        // *                                                       ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名マスタ、宛名付随マスタ、宛名_標準マスタ、宛名付随_標準マスタのデータを論理削除する
        // * 
        // * 引数           csAtenaDr As DataRow           : 論理削除するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaHyojunDr As DataRow     : 論理削除するデータの含まれるDataRowオブジェクト（宛名_標準）
        // * 　　           csAtenaFZYDr As DataRow        : 論理削除するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow  : 論理削除するデータの含まれるDataRowオブジェクト（宛名付随_標準）
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        // *履歴番号 000035 2023/10/19 修正開始
        // Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
        // ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        public int DeleteAtenaB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)
        {
            // *履歴番号 000035 2023/10/19 修正終了

            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = DeleteAtenaB(csAtenaDr);

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名_標準マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaHyojunDr == null))
                    {

                        // 宛名_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaHyojunB == null)
                        {
                            m_csAtenaHyojunB = new ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaHyojunDr[ABAtenaHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名_標準マスタ更新を実行
                        intCnt2 = m_csAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunDr);

                    }

                    // 宛名付随マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYDr == null))
                    {

                        // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYB == null)
                        {
                            m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随マスタ更新を実行
                        intCnt3 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr);

                    }

                    // 宛名付随_標準マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYHyojunDr == null))
                    {

                        // 宛名付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYHyojunB == null)
                        {
                            m_csAtenaFZYHyojunB = new ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYHyojunDr[ABAtenaFZYHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随_標準マスタ更新を実行
                        intCnt4 = m_csAtenaFZYHyojunB.DeleteAtenaFZYHyojun(csAtenaFZYHyojunDr);

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
        // *履歴番号 000034 2023/08/14 追加終了

        // ************************************************************************************************
        // * メソッド名     宛名マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaB(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　宛名マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaB";
            UFErrorStruct objErrorStruct; // エラー定義構造体
                                          // パラメータクラス
            int intDelCnt;            // 削除件数


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
                    // * 履歴番号 000018 2005/05/23 修正開始
                    // CreateSQL(csDataRow)
                    CreateDeleteButsuriSQL(csDataRow);
                    // * 履歴番号 000018 2005/05/23 修正終了
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollectionClass)
                {

                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength()) == ABAtenaEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                }


                // *履歴番号 000009 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")
                // *履歴番号 000003 2003/08/28 修正終了

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
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ物理削除
        // * 
        // * 構文           Public Function UpdateAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名マスタのデータを物理削除する
        // * 
        // * 引数           csAtenaDr As DataRow : 物理削除するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaFZYDr As DataRow : 物理削除するデータの含まれるDataRowオブジェクト（宛名付随）
        // *                strSakujoKB As String ： 削除区分  
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaB(DataRow csAtenaDr, DataRow csAtenaFZYDr, string strSakujoKB)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = DeleteAtenaB(csAtenaDr, strSakujoKB);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaFZYB == null)
                    {
                        m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 宛名付随マスタ更新を実行
                    intCnt2 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr, strSakujoKB);
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
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000034 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名マスタ物理削除
        // * 
        // * 構文           Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, _
        // *                                                       ByVal csAtenaFZYDr As DataRow, _
        // *                                                       ByVal csAtenaHyojunDr As DataRow, _
        // *                                                       ByVal csAtenaFZYHyojunDr As DataRow, _
        // *                                                       ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　 宛名マスタ、宛名付随マスタ、宛名_標準マスタ、宛名付随_標準マスタのデータを物理削除する
        // * 
        // * 引数           csAtenaDr As DataRow           : 論理削除するデータの含まれるDataRowオブジェクト（宛名）
        // * 　　           csAtenaHyojunDr As DataRow     : 論理削除するデータの含まれるDataRowオブジェクト（宛名_標準）
        // * 　　           csAtenaFZYDr As DataRow        : 論理削除するデータの含まれるDataRowオブジェクト（宛名付随）
        // * 　　           csAtenaFZYHyojunDr As DataRow  : 論理削除するデータの含まれるDataRowオブジェクト（宛名付随_標準）
        // *                strSakujoKB As String          ： 削除区分  
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        // *履歴番号 000035 2023/10/19 修正開始
        // Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
        // ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow,
        // ByVal strSakujoKB As String) As Integer
        public int DeleteAtenaB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr, string strSakujoKB)
        {
            // *履歴番号 000035 2023/10/19 修正終了

            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "DeleteAtenaB";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名マスタ更新を実行
                intCnt = DeleteAtenaB(csAtenaDr, strSakujoKB);

                // 宛名_標準マスタのデータが存在する場合、処理を行う
                if (!(csAtenaHyojunDr == null))
                {

                    // 宛名_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaHyojunB == null)
                    {
                        m_csAtenaHyojunB = new ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 更新日時の同期
                    csAtenaHyojunDr[ABAtenaHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                    // 宛名_標準マスタ更新を実行
                    intCnt2 = m_csAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunDr, strSakujoKB);

                }

                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名付随マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYDr == null))
                    {

                        // 宛名付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYB == null)
                        {
                            m_csAtenaFZYB = new ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYDr[ABAtenaFZYEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随マスタ更新を実行
                        intCnt3 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr, strSakujoKB);

                    }

                    // 宛名付随_標準マスタのデータが存在する場合、処理を行う
                    if (!(csAtenaFZYHyojunDr == null))
                    {

                        // 宛名付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaFZYHyojunB == null)
                        {
                            m_csAtenaFZYHyojunB = new ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 更新日時の同期
                        csAtenaFZYHyojunDr[ABAtenaFZYHyojunEntity.KOSHINNICHIJI] = csAtenaDr[ABAtenaEntity.KOSHINNICHIJI];

                        // 宛名付随_標準マスタ更新を実行
                        intCnt4 = m_csAtenaFZYHyojunB.DeleteAtenaFZYHyojunB(csAtenaFZYHyojunDr, strSakujoKB);

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
        // *履歴番号 000034 2023/08/14 追加終了

        // * 履歴番号 000018 2005/05/23 削除開始
        // '************************************************************************************************
        // '* メソッド名     SQL文の作成
        // '* 
        // '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // '* 
        // '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // '* 
        // '* 引数           csDataRow As DataRow : 更新対象の行
        // '* 
        // '* 戻り値         なし
        // '************************************************************************************************
        // Private Sub CreateSQL(ByVal csDataRow As DataRow)

        // Const THIS_METHOD_NAME As String = "CreateSQL"
        // Dim csDataColumn As DataColumn
        // Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        // Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義
        // Dim cfUFParameterClass As UFParameterClass
        // Dim csWhere As StringBuilder                        'WHERE定義
        // Dim csUpdateParam As StringBuilder                  'UPDATE用SQL定義
        // Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義


        // Try
        // ' デバッグ開始ログ出力
        // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // ' SELECT SQL文の作成
        // m_strInsertSQL = "INSERT INTO " + ABAtenaEntity.TABLE_NAME + " "
        // csInsertColumn = New StringBuilder()
        // csInsertParam = New StringBuilder()


        // ' UPDATE SQL文の作成
        // m_strUpdateSQL = "UPDATE " + ABAtenaEntity.TABLE_NAME + " SET "
        // csUpdateParam = New StringBuilder()


        // ' WHERE文の作成
        // csWhere = New StringBuilder()
        // csWhere.Append(" WHERE ")
        // csWhere.Append(ABAtenaEntity.JUMINCD)
        // csWhere.Append(" = ")
        // csWhere.Append(ABAtenaEntity.KEY_JUMINCD)
        // csWhere.Append(" AND ")
        // csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB)
        // csWhere.Append(" = ")
        // csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB)
        // csWhere.Append(" AND ")
        // csWhere.Append(ABAtenaEntity.KOSHINCOUNTER)
        // csWhere.Append(" = ")
        // csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER)


        // ' 論理DELETE SQL文の作成
        // csDelRonriParam = New StringBuilder()
        // csDelRonriParam.Append("UPDATE ")
        // csDelRonriParam.Append(ABAtenaEntity.TABLE_NAME)
        // csDelRonriParam.Append(" SET ")
        // csDelRonriParam.Append(ABAtenaEntity.TANMATSUID)
        // csDelRonriParam.Append(" = ")
        // csDelRonriParam.Append(ABAtenaEntity.PARAM_TANMATSUID)
        // csDelRonriParam.Append(", ")
        // csDelRonriParam.Append(ABAtenaEntity.SAKUJOFG)
        // csDelRonriParam.Append(" = ")
        // csDelRonriParam.Append(ABAtenaEntity.PARAM_SAKUJOFG)
        // csDelRonriParam.Append(", ")
        // csDelRonriParam.Append(ABAtenaEntity.KOSHINCOUNTER)
        // csDelRonriParam.Append(" = ")
        // csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINCOUNTER)
        // csDelRonriParam.Append(", ")
        // csDelRonriParam.Append(ABAtenaEntity.KOSHINNICHIJI)
        // csDelRonriParam.Append(" = ")
        // csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINNICHIJI)
        // csDelRonriParam.Append(", ")
        // csDelRonriParam.Append(ABAtenaEntity.KOSHINUSER)
        // csDelRonriParam.Append(" = ")
        // csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINUSER)
        // csDelRonriParam.Append(csWhere)
        // m_strDelRonriSQL = csDelRonriParam.ToString

        // ' 物理DELETE SQL文の作成
        // m_strDelButuriSQL = "DELETE FROM " + ABAtenaEntity.TABLE_NAME + csWhere.ToString

        // ' SELECT パラメータコレクションクラスのインスタンス化
        // m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

        // ' UPDATE パラメータコレクションのインスタンス化
        // m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

        // ' 論理削除用パラメータコレクションのインスタンス化
        // m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

        // ' 物理削除用パラメータコレクションのインスタンス化
        // m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()


        // ' パラメータコレクションの作成
        // For Each csDataColumn In csDataRow.Table.Columns
        // cfUFParameterClass = New UFParameterClass()

        // ' INSERT SQL文の作成
        // csInsertColumn.Append(csDataColumn.ColumnName)
        // csInsertColumn.Append(", ")

        // csInsertParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
        // csInsertParam.Append(csDataColumn.ColumnName)
        // csInsertParam.Append(", ")


        // ' UPDATE SQL文の作成
        // csUpdateParam.Append(csDataColumn.ColumnName)
        // csUpdateParam.Append(" = ")
        // csUpdateParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
        // csUpdateParam.Append(csDataColumn.ColumnName)
        // csUpdateParam.Append(", ")

        // ' INSERT コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // Next csDataColumn

        // '最後のカンマを取り除いてINSERT文を作成
        // m_strInsertSQL += "(" + csInsertColumn.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
        // + " VALUES (" + csInsertParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"


        // ' UPDATE SQL文のトリミング
        // m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

        // ' UPDATE SQL文にWHERE句の追加
        // m_strUpdateSQL += csWhere.ToString


        // ' UPDATE コレクションにパラメータを追加
        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
        // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // ' 論理削除用コレクションにパラメータを追加
        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_TANMATSUID
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SAKUJOFG
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINCOUNTER
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINNICHIJI
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINUSER
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
        // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // ' 物理削除用コレクションにパラメータを追加
        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
        // m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
        // m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // cfUFParameterClass = New UFParameterClass()
        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
        // m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

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
        // ' エラーをそのままスローする
        // Throw objExp
        // End Try

        // End Sub
        // * 履歴番号 000018 2005/05/23 削除終了

        // * 履歴番号 000018 2005/05/23 追加開始
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
                m_strInsertSQL = "INSERT INTO " + ABAtenaEntity.TABLE_NAME + " ";
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

                    csInsertParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                m_strUpdateSQL = "UPDATE " + ABAtenaEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・住民住登外区分・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaEntity.JUMINJUTOGAIKB) && !(csDataColumn.ColumnName == ABAtenaEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(", ");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray());

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += csWhere.ToString();

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER;
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
                csWhere.Append(ABAtenaEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER);


                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER;
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
                csWhere.Append(ABAtenaEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABAtenaEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER;
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
        // * 履歴番号 000018 2005/05/23 追加終了

        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　　WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(ABAtenaSearchKey cSearchKey)
        {
            const string THIS_METHOD_NAME = "CreateWhere";
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;
            string strWhereHyojun;
            string strWhereFzy;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // WHERE句の作成
                // * 履歴番号 000017 2005/01/25 更新開始（宮沢）
                // csWHERE = New StringBuilder()
                csWHERE = new StringBuilder(256);
                // * 履歴番号 000017 2005/01/25 更新終了

                // 住民コード
                if (!(cSearchKey.p_strJuminCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000013 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaEntity.JUMINCD)
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
                    // *履歴番号 000013 2003/11/18 修正終了
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住民優先区分
                if (!(cSearchKey.p_strJuminYuseniKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINYUSENIKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUMINYUSENIKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINYUSENIKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住登外優先区分
                if (!(cSearchKey.p_strJutogaiYusenKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTOGAIYUSENKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 世帯コード
                if (!(cSearchKey.p_strStaiCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_STAICD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_STAICD;
                    cfUFParameterClass.Value = cSearchKey.p_strStaiCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // *履歴番号 000032 2020/01/10 修正開始
                // ' 検索用カナ姓名
                // If Not (cSearchKey.p_strSearchKanaSeiMei.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If

                // If cSearchKey.p_strSearchKanaSeiMei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                // End If
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If

                // ' 検索用カナ姓
                // If Not (cSearchKey.p_strSearchKanaSei.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // '* 履歴番号 000024 2007/10/10 追加開始
                // ' 外国人本名優先検索 OR条件検索するために括弧でくくる
                // If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
                // csWHERE.Append(" ( ")
                // End If
                // '* 履歴番号 000024 2007/10/10 追加終了
                // If cSearchKey.p_strSearchKanaSei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // '* 履歴番号 000024 2007/10/10 追加開始
                // ' 検索用カナ姓２をOR条件で追加
                // ' 検索カナ姓２に検索キーが格納されている場合は検索条件として追加
                // If ((cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty)) Then
                // csWHERE.Append(" OR ")
                // If cSearchKey.p_strSearchKanaSei2.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // ' OR条件は検索用カナ姓のみでの条件なので括弧で括る
                // csWHERE.Append(" ) ")
                // End If
                // '* 履歴番号 000024 2007/10/10 追加終了

                // ' 検索用カナ名
                // If Not (cSearchKey.p_strSearchKanaMei.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strSearchKanaMei.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANAMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANAMEI)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // ' 検索用漢字名称
                // If Not (cSearchKey.p_strSearchKanjiMeisho.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strSearchKanjiMeisho.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                // cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If

                // '* 履歴番号 000023 2007/09/03 追加開始
                // ' 本名漢字姓名 本名検索="2(Tsusho_Seishiki)"のときのみ漢字氏名２は検索項目となる
                // If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                // If Not (cSearchKey.p_strKanjiMeisho2.Trim() = String.Empty) Then
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
                // '* 履歴番号 000023 2007/09/03 追加終了

                // 氏名検索条件を生成
                m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaEntity.TABLE_NAME, ref csWHERE, ref m_cfSelectUFParameterCollectionClass, ABAtenaFZYHyojunEntity.TABLE_NAME);
                // *履歴番号 000032 2020/01/10 修正終了

                // 生年月日
                if (!(cSearchKey.p_strUmareYMD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strUmareYMD.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaEntity.KEY_UMAREYMD);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_UMAREYMD;
                        cfUFParameterClass.Value = cSearchKey.p_strUmareYMD;

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.KEY_UMAREYMD);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_UMAREYMD;
                        cfUFParameterClass.Value = cSearchKey.p_strUmareYMD.TrimEnd();

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // 性別
                if (!(cSearchKey.p_strSeibetsuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_SEIBETSUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEIBETSUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strSeibetsuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住所コード
                if (!(cSearchKey.p_strJushoCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD);
                    // * 履歴番号 000019 2005/07/11 修正開始
                    // *********************************************************
                    // *** 住所CDか全国住所CDかの判定して、Where文を作成する ***
                    // *********************************************************
                    // csWHERE.Append(" = ")
                    // csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUSHOCD;

                    if (cSearchKey.p_strJushoCD.Trim().RLength() == 11 && cSearchKey.p_strJushoCD.RRemove(0, 2) == "000000000")
                    {
                        // 11桁で 且つ 下9桁が"0"のとき、上2桁であいまい検索
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD);
                        cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 2) + "%";
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                    else if (cSearchKey.p_strJushoCD.Trim().RLength() == 11 && cSearchKey.p_strJushoCD.RRemove(0, 5) == "000000")
                    {
                        // 11桁で 且つ 下6桁が"0"のとき、上5桁であいまい検索
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD);
                        cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 5) + "%";
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else if (cSearchKey.p_strJushoCD.Trim().RLength() == 11 && cSearchKey.p_strJushoCD.RRemove(0, 8) == "000")
                    {
                        // 11桁で 且つ 下3桁が"0"のとき、上8桁であいまい検索
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD);
                        cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 8) + "%";
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else
                    {
                        // 13桁で検索
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD);
                        if (cSearchKey.p_strJushoCD.Trim().RLength() == 11)
                        {
                            cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RPadRight(13);
                        }
                        else
                        {
                            cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RPadLeft(13);
                        }

                        // ' 検索条件のパラメータを作成
                        // cfUFParameterClass = New UFParameterClass()
                        // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUSHOCD
                        // cfUFParameterClass.Value = cSearchKey.p_strJushoCD
                        // * 履歴番号 000019 2005/07/11 修正終了

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                }

                // 行政区コード
                if (!(cSearchKey.p_strGyoseikuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_GYOSEIKUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_GYOSEIKUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strGyoseikuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード１
                if (!(cSearchKey.p_strChikuCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD1;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード２
                if (!(cSearchKey.p_strChikuCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD2;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 地区コード３
                if (!(cSearchKey.p_strChikuCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD3;
                    cfUFParameterClass.Value = cSearchKey.p_strChikuCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード１
                if (!(cSearchKey.p_strBanchiCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_BANCHICD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD1;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード２
                if (!(cSearchKey.p_strBanchiCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_BANCHICD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD2;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 番地コード３
                if (!(cSearchKey.p_strBanchiCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_BANCHICD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD3;
                    cfUFParameterClass.Value = cSearchKey.p_strBanchiCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基住所コード
                if (!(cSearchKey.p_strJukiJushoCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUKIJUSHOCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIJUSHOCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiJushoCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基行政区コード
                if (!(cSearchKey.p_strJukiGyoseikuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUKIGYOSEIKUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIGYOSEIKUCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiGyoseikuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード１
                if (!(cSearchKey.p_strJukiChikuCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD1;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード２
                if (!(cSearchKey.p_strJukiChikuCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD2;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基地区コード３
                if (!(cSearchKey.p_strJukiChikuCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD3;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード１
                if (!(cSearchKey.p_strJukiBanchiCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD1);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD1;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD1;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード２
                if (!(cSearchKey.p_strJukiBanchiCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD2);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD2;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD2;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住基番地コード３
                if (!(cSearchKey.p_strJukiBanchiCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD3);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD3;
                    cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD3;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // データ区分
                if (!(cSearchKey.p_strDataKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000013 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                    // csWHERE.Append(" = ")
                    // csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)

                    if (cSearchKey.p_strDataKB.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB);

                    }
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_ATENADATAKB;
                    cfUFParameterClass.Value = cSearchKey.p_strDataKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                    // *履歴番号 000013 2003/11/18 修正終了

                    // 検索条件のパラメータを作成
                }

                if (!(cSearchKey.p_strJuminShubetu1 == string.Empty & cSearchKey.p_strJuminShubetu2 == string.Empty))
                {
                    if (cSearchKey.p_strDataKB.Trim() == string.Empty)
                    {
                        if (!(csWHERE.RLength() == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append("((");
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" = '11')");
                        csWHERE.Append(" OR (");
                        csWHERE.Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" = '12'))");
                    }

                    // 住民種別１
                    if (!(cSearchKey.p_strJuminShubetu1.Trim() == string.Empty))
                    {
                        if (!(csWHERE.RLength() == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append(" {fn SUBSTRING(");
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU);
                        csWHERE.Append(",1,1)} = '");
                        csWHERE.Append(cSearchKey.p_strJuminShubetu1);
                        csWHERE.Append("'");
                    }

                    // 住民種別２
                    if (!(cSearchKey.p_strJuminShubetu2.Trim() == string.Empty))
                    {
                        if (!(csWHERE.RLength() == 0))
                        {
                            csWHERE.Append(" AND ");
                        }
                        csWHERE.Append(" {fn SUBSTRING(");
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU);
                        csWHERE.Append(",2,1)} = '");
                        csWHERE.Append(cSearchKey.p_strJuminShubetu2);
                        csWHERE.Append("'");
                    }
                }

                // '期間年月日
                // If Not (strKikanYMD.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // csWHERE.Append(ABAtenaEntity.RRKST_YMD)
                // csWHERE.Append(" <= ")
                // csWHERE.Append(ABAtenaEntity.KEY_RRKST_YMD)
                // csWHERE.Append(" AND ")
                // csWHERE.Append(ABAtenaEntity.RRKED_YMD)
                // csWHERE.Append(" >= ")
                // csWHERE.Append(ABAtenaEntity.KEY_RRKED_YMD)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_RRKST_YMD
                // cfUFParameterClass.Value = strKikanYMD
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_RRKED_YMD
                // cfUFParameterClass.Value = strKikanYMD
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If

                // 市町村コード
                if (!(cSearchKey.p_strShichosonCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    // *履歴番号 000013 2003/11/18 修正開始
                    // csWHERE.Append(ABAtenaEntity.SHICHOSONCD)
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD);
                    // *履歴番号 000013 2003/11/18 修正終了
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_SHICHOSONCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SHICHOSONCD;
                    cfUFParameterClass.Value = cSearchKey.p_strShichosonCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // *履歴番号 000030 2014/04/28 追加開始
                // ---------------------------------------------------------------------------------------------------------
                // 共通番号が指定されている場合
                if (cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                {

                    // -----------------------------------------------------------------------------------------------------
                    // 【１．直近検索区分による制御】
                    // 直近検索区分の整備
                    switch (cSearchKey.p_strMyNumberChokkinSearchKB)
                    {
                        case var @case when @case == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode().ToString:
                        // noop
                        case var case1 when case1 == ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode().ToString:
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
                    if (cSearchKey.p_strMyNumberChokkinSearchKB == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode().ToString())
                    {

                        // 共通番号カラムに共通番号を指定する。
                        if (csWHERE.RLength() > 0)
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
                        if (csWHERE.RLength() > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
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
                        if (csWHERE.RLength() > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} = {2}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KJNHJNKB, ABAtenaEntity.PARAM_KJNHJNKB);

                        // 検索条件のパラメーターを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KJNHJNKB;
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
                // ---------------------------------------------------------------------------------------------------------
                // *履歴番号 000030 2014/04/28 追加終了

                // 電話番号
                if (!(cSearchKey.p_strRenrakusaki.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append("((");
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_RENRAKUSAKI1);
                    csWHERE.Append(") OR (");
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_RENRAKUSAKI2);
                    csWHERE.Append("))");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_RENRAKUSAKI1;
                    cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_RENRAKUSAKI2;
                    cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 宛名標準
                strWhereHyojun = CreateWhereHyojun(cSearchKey);
                if (strWhereHyojun.RLength() > 0)
                {

                    if (csWHERE.RLength() > 0)
                    {
                        csWHERE.Append(" AND ");
                    }
                    else
                    {
                        // noop
                    }

                    csWHERE.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                    csWHERE.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINCD);
                    csWHERE.AppendFormat(" WHERE {0}", strWhereHyojun);
                    csWHERE.Append(")");
                }
                else
                {
                    // noop
                }

                // 宛名付随
                strWhereFzy = CreateWhereFZY(cSearchKey);
                if (strWhereFzy.RLength() > 0)
                {

                    if (csWHERE.RLength() > 0)
                    {
                        csWHERE.Append(" AND ");
                    }
                    else
                    {
                        // noop
                    }

                    csWHERE.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                    csWHERE.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD);
                    csWHERE.AppendFormat(" WHERE {0}", strWhereFzy);
                    csWHERE.Append(")");
                }
                else
                {
                    // noop
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

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
        // * 
        // * 機能           更新データの整合性をチェックする。
        // * 
        // * 引数           strColumnName As String   : 宛名マスタデータセットの項目名
        // * 　　           strValue As String        : 項目に対応する値
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";
            const string TABLENAME = "宛名．";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグ開始ログ出力
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

                    case var @case when @case == ABAtenaEntity.JUMINCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case1 when case1 == ABAtenaEntity.SHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case2 when case2 == ABAtenaEntity.KYUSHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case3 when case3 == ABAtenaEntity.JUMINJUTOGAIKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINJUTOGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case4 when case4 == ABAtenaEntity.JUMINYUSENIKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINYUSENIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case5 when case5 == ABAtenaEntity.JUTOGAIYUSENKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTOGAIYUSENKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case6 when case6 == ABAtenaEntity.ATENADATAKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ATENADATAKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case7 when case7 == ABAtenaEntity.STAICD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case8 when case8 == ABAtenaEntity.JUMINHYOCD:               // 住民票コード
                        {
                            break;
                        }
                    // チェックなし

                    case var case9 when case9 == ABAtenaEntity.SEIRINO:                  // 整理番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIRINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case10 when case10 == ABAtenaEntity.ATENADATASHU:             // 宛名データ種別
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ATENADATASHU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case11 when case11 == ABAtenaEntity.HANYOKB1:                 // 汎用区分1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANYOKB1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case12 when case12 == ABAtenaEntity.KJNHJNKB:                 // 個人法人区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KJNHJNKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case13 when case13 == ABAtenaEntity.HANYOKB2:                 // 汎用区分2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANYOKB2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case14 when case14 == ABAtenaEntity.KANNAIKANGAIKB:           // 管内管外区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANNAIKANGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case15 when case15 == ABAtenaEntity.KANAMEISHO1:              // カナ名称1
                        {
                            // *履歴番号 000012 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000012 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case16 when case16 == ABAtenaEntity.KANJIMEISHO1:             // 漢字名称1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case17 when case17 == ABAtenaEntity.KANAMEISHO2:              // カナ名称2
                        {
                            // *履歴番号 000012 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000012 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case18 when case18 == ABAtenaEntity.KANJIMEISHO2:             // 漢字名称2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case19 when case19 == ABAtenaEntity.KANJIHJNKEITAI:           // 漢字法人形態
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHJNKEITAI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case20 when case20 == ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI:   // 漢字法人代表者氏名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case21 when case21 == ABAtenaEntity.SEARCHKANJIMEISHO:        // 検索用漢字名称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEARCHKANJIMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case22 when case22 == ABAtenaEntity.KYUSEI:                   // 旧姓
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KYUSEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case23 when case23 == ABAtenaEntity.SEARCHKANASEIMEI:         // 検索用カナ姓名
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

                    case var case24 when case24 == ABAtenaEntity.SEARCHKANASEI:            // 検索用カナ姓
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

                    case var case25 when case25 == ABAtenaEntity.SEARCHKANAMEI:            // 検索用カナ名
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

                    case var case26 when case26 == ABAtenaEntity.JUKIRRKNO:                // 住基履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIRRKNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case27 when case27 == ABAtenaEntity.RRKST_YMD:                // 履歴開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RRKST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case28 when case28 == ABAtenaEntity.RRKED_YMD:                // 履歴終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000" | strValue == "99999999"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RRKED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    // Case ABAtenaEntity.UMAREYMD                 '生年月日
                    // If Not (strValue = String.Empty Or strValue = "00000000") Then
                    // m_cfDateClass.p_strDateValue = strValue
                    // If (Not m_cfDateClass.CheckDate()) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_UMAREYMD)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    // End If
                    // End If

                    // Case ABAtenaEntity.UMAREWMD                 '生和暦年月日
                    // If (Not UFStringClass.CheckNumber(strValue)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得(数字項目入力の誤りです。：)
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode)
                    // End If

                    case var case29 when case29 == ABAtenaEntity.SEIBETSUCD:               // 性別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIBETSUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case30 when case30 == ABAtenaEntity.SEIBETSU:                 // 性別
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIBETSU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case31 when case31 == ABAtenaEntity.SEKINO:                   // 籍番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEKINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case32 when case32 == ABAtenaEntity.JUMINHYOHYOJIJUN:         // 住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case33 when case33 == ABAtenaEntity.ZOKUGARACD:               // 続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case34 when case34 == ABAtenaEntity.ZOKUGARA:                 // 続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case35 when case35 == ABAtenaEntity.DAI2JUMINHYOHYOJIJUN:     // 第２住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case36 when case36 == ABAtenaEntity.DAI2ZOKUGARACD:           // 第２続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case37 when case37 == ABAtenaEntity.DAI2ZOKUGARA:             // 第２続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case38 when case38 == ABAtenaEntity.STAINUSJUMINCD:           // 世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case39 when case39 == ABAtenaEntity.STAINUSMEI:               // 世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case40 when case40 == ABAtenaEntity.KANASTAINUSMEI:           // カナ世帯主名
                        {
                            // *履歴番号 000012 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000012 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANASTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case41 when case41 == ABAtenaEntity.DAI2STAINUSJUMINCD:       // 第２世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case42 when case42 == ABAtenaEntity.DAI2STAINUSMEI:           // 第２世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case43 when case43 == ABAtenaEntity.KANADAI2STAINUSMEI:       // 第２カナ世帯主名
                        {
                            // *履歴番号 000012 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000012 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANADAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case44 when case44 == ABAtenaEntity.YUBINNO:                  // 郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case45 when case45 == ABAtenaEntity.JUSHOCD:                  // 住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case46 when case46 == ABAtenaEntity.JUSHO:                    // 住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case47 when case47 == ABAtenaEntity.BANCHICD1:                // 番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case48 when case48 == ABAtenaEntity.BANCHICD2:                // 番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case49 when case49 == ABAtenaEntity.BANCHICD3:                // 番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case50 when case50 == ABAtenaEntity.BANCHI:                   // 番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case51 when case51 == ABAtenaEntity.KATAGAKIFG:               // 方書フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKIFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case52 when case52 == ABAtenaEntity.KATAGAKICD:               // 方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case53 when case53 == ABAtenaEntity.KATAGAKI:                 // 方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case54 when case54 == ABAtenaEntity.RENRAKUSAKI1:             // 連絡先1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RENRAKUSAKI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case55 when case55 == ABAtenaEntity.RENRAKUSAKI2:             // 連絡先2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RENRAKUSAKI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case56 when case56 == ABAtenaEntity.HON_ZJUSHOCD:             // 本籍全国住所コード
                        {
                            // * 履歴番号 000015 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000015 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HON_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case57 when case57 == ABAtenaEntity.HON_JUSHO:                // 本籍住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HON_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case58 when case58 == ABAtenaEntity.HONSEKIBANCHI:            // 本籍番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HONSEKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case59 when case59 == ABAtenaEntity.HITTOSH:                  // 筆頭者
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HITTOSH);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case60 when case60 == ABAtenaEntity.CKINIDOYMD:               // 直近異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case61 when case61 == ABAtenaEntity.CKINJIYUCD:               // 直近事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case62 when case62 == ABAtenaEntity.CKINJIYU:                 // 直近事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case63 when case63 == ABAtenaEntity.CKINTDKDYMD:              // 直近届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case64 when case64 == ABAtenaEntity.CKINTDKDTUCIKB:           // 直近届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case65 when case65 == ABAtenaEntity.TOROKUIDOYMD:             // 登録異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case66 when case66 == ABAtenaEntity.TOROKUIDOWMD:             // 登録異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case67 when case67 == ABAtenaEntity.TOROKUJIYUCD:             // 登録事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case68 when case68 == ABAtenaEntity.TOROKUJIYU:               // 登録事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case69 when case69 == ABAtenaEntity.TOROKUTDKDYMD:            // 登録届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case70 when case70 == ABAtenaEntity.TOROKUTDKDWMD:            // 登録届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case71 when case71 == ABAtenaEntity.TOROKUTDKDTUCIKB:         // 登録届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case72 when case72 == ABAtenaEntity.JUTEIIDOYMD:              // 住定異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case73 when case73 == ABAtenaEntity.JUTEIIDOWMD:              // 住定異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case74 when case74 == ABAtenaEntity.JUTEIJIYUCD:              // 住定事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case75 when case75 == ABAtenaEntity.JUTEIJIYU:                // 住定事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case76 when case76 == ABAtenaEntity.JUTEITDKDYMD:             // 住定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case77 when case77 == ABAtenaEntity.JUTEITDKDWMD:             // 住定届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case78 when case78 == ABAtenaEntity.JUTEITDKDTUCIKB:          // 住定届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case79 when case79 == ABAtenaEntity.SHOJOIDOYMD:              // 消除異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case80 when case80 == ABAtenaEntity.SHOJOJIYUCD:              // 消除事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case81 when case81 == ABAtenaEntity.SHOJOJIYU:                // 消除事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case82 when case82 == ABAtenaEntity.SHOJOTDKDYMD:             // 消除届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case83 when case83 == ABAtenaEntity.SHOJOTDKDTUCIKB:          // 消除届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case84 when case84 == ABAtenaEntity.TENSHUTSUYOTEIIDOYMD:     // 転出予定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case85 when case85 == ABAtenaEntity.TENSHUTSUKKTIIDOYMD:      // 転出確定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case86 when case86 == ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD:   // 転出確定通知年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case87 when case87 == ABAtenaEntity.TENSHUTSUNYURIYUCD:       // 転出入理由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUNYURIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case88 when case88 == ABAtenaEntity.TENSHUTSUNYURIYU:         // 転出入理由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUNYURIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case89 when case89 == ABAtenaEntity.TENUMAEJ_YUBINNO:         // 転入前郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case90 when case90 == ABAtenaEntity.TENUMAEJ_ZJUSHOCD:        // 転入前住所全国住所コード
                        {
                            // * 履歴番号 000015 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000015 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case91 when case91 == ABAtenaEntity.TENUMAEJ_JUSHO:           // 転入前住所住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case92 when case92 == ABAtenaEntity.TENUMAEJ_BANCHI:          // 転入前住所番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case93 when case93 == ABAtenaEntity.TENUMAEJ_KATAGAKI:        // 転入前住所方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case94 when case94 == ABAtenaEntity.TENUMAEJ_STAINUSMEI:      // 転入前住所世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case95 when case95 == ABAtenaEntity.TENSHUTSUYOTEIYUBINNO:    // 転出予定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case96 when case96 == ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD:   // 転出予定全国住所コード
                        {
                            // * 履歴番号 000015 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000015 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case97 when case97 == ABAtenaEntity.TENSHUTSUYOTEIJUSHO:      // 転出予定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case98 when case98 == ABAtenaEntity.TENSHUTSUYOTEIBANCHI:     // 転出予定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case99 when case99 == ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI:   // 転出予定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case100 when case100 == ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI: // 転出予定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case101 when case101 == ABAtenaEntity.TENSHUTSUKKTIYUBINNO:     // 転出確定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case102 when case102 == ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD:    // 転出確定全国住所コード
                        {
                            // * 履歴番号 000015 2004/10/19 修正開始（マルゴ村山）
                            // If (Not UFStringClass.CheckNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000015 2004/10/19 修正終了（マルゴ村山）
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case103 when case103 == ABAtenaEntity.TENSHUTSUKKTIJUSHO:    // 転出確定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case104 when case104 == ABAtenaEntity.TENSHUTSUKKTIBANCHI:      // 転出確定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case105 when case105 == ABAtenaEntity.TENSHUTSUKKTIKATAGAKI:    // 転出確定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case106 when case106 == ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI:  // 転出確定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case107 when case107 == ABAtenaEntity.TENSHUTSUKKTIMITDKFG:     // 転出確定見届フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case108 when case108 == ABAtenaEntity.BIKOYMD:                  // 備考年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case109 when case109 == ABAtenaEntity.BIKO:                     // 備考
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case110 when case110 == ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG: // 備考転出確定住所フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case111 when case111 == ABAtenaEntity.HANNO:                    // 版番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case112 when case112 == ABAtenaEntity.KAISEIATOFG:              // 改製後フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIATOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case113 when case113 == ABAtenaEntity.KAISEIMAEFG:             // 改製前フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIMAEFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case114 when case114 == ABAtenaEntity.KAISEIYMD:                // 改製年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case115 when case115 == ABAtenaEntity.GYOSEIKUCD:               // 行政区コード
                        {
                            // * 履歴番号 000020 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart())) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000020 2005/12/26 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_GYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case116 when case116 == ABAtenaEntity.GYOSEIKUMEI:              // 行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_GYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case117 when case117 == ABAtenaEntity.CHIKUCD1:                 // 地区コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case118 when case118 == ABAtenaEntity.CHIKUMEI1:                // 地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case119 when case119 == ABAtenaEntity.CHIKUCD2:                 // 地区コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case120 when case120 == ABAtenaEntity.CHIKUMEI2:                // 地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case121 when case121 == ABAtenaEntity.CHIKUCD3:                 // 地区コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case122 when case122 == ABAtenaEntity.CHIKUMEI3:                // 地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case123 when case123 == ABAtenaEntity.TOHYOKUCD:                // 投票区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOHYOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case124 when case124 == ABAtenaEntity.SHOGAKKOKUCD:             // 小学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case125 when case125 == ABAtenaEntity.CHUGAKKOKUCD:             // 中学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHUGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case126 when case126 == ABAtenaEntity.HOGOSHAJUMINCD:           // 保護者住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HOGOSHAJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case127 when case127 == ABAtenaEntity.KANJIHOGOSHAMEI:          // 漢字保護者名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case128 when case128 == ABAtenaEntity.KANAHOGOSHAMEI:           // カナ保護者名
                        {
                            // *履歴番号 000012 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000012 2003/10/30 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case129 when case129 == ABAtenaEntity.KIKAYMD:                  // 帰化年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KIKAYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case130 when case130 == ABAtenaEntity.KARIIDOKB:                // 仮異動区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KARIIDOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case131 when case131 == ABAtenaEntity.SHORITEISHIKB:            // 処理停止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHORITEISHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case132 when case132 == ABAtenaEntity.SHORIYOKUSHIKB:           // 処理抑止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHORIYOKUSHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case133 when case133 == ABAtenaEntity.JUKIYUBINNO:              // 住基郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case134 when case134 == ABAtenaEntity.JUKIJUSHOCD:              // 住基住所コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case135 when case135 == ABAtenaEntity.JUKIJUSHO:                // 住基住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case136 when case136 == ABAtenaEntity.JUKIBANCHICD1:            // 住基番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case137 when case137 == ABAtenaEntity.JUKIBANCHICD2:            // 住基番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case138 when case138 == ABAtenaEntity.JUKIBANCHICD3:            // 住基番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case139 when case139 == ABAtenaEntity.JUKIBANCHI:               // 住基番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case140 when case140 == ABAtenaEntity.JUKIKATAGAKIFG:           // 住基方書フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKIFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case141 when case141 == ABAtenaEntity.JUKIKATAGAKICD:           // 住基方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case142 when case142 == ABAtenaEntity.JUKIKATAGAKI:             // 住基方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case143 when case143 == ABAtenaEntity.JUKIGYOSEIKUCD:           // 住基行政区コード
                        {
                            // * 履歴番号 000020 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart())) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000020 2005/12/26 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIGYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case144 when case144 == ABAtenaEntity.JUKIGYOSEIKUMEI:          // 住基行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIGYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case145 when case145 == ABAtenaEntity.JUKICHIKUCD1:             // 住基地区コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case146 when case146 == ABAtenaEntity.JUKICHIKUMEI1:            // 住基地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case147 when case147 == ABAtenaEntity.JUKICHIKUCD2:             // 住基地区コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case148 when case148 == ABAtenaEntity.JUKICHIKUMEI2:            // 住基地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case149 when case149 == ABAtenaEntity.JUKICHIKUCD3:             // 住基地区コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case150 when case150 == ABAtenaEntity.JUKICHIKUMEI3:            // 住基地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case151 when case151 == ABAtenaEntity.KAOKUSHIKIKB:             // 家屋敷区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAOKUSHIKIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case152 when case152 == ABAtenaEntity.BIKOZEIMOKU:              // 備考税目
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOZEIMOKU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case153 when case153 == ABAtenaEntity.KOKUSEKICD:               // 国籍コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOKUSEKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case154 when case154 == ABAtenaEntity.KOKUSEKI:                 // 国籍
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOKUSEKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case155 when case155 == ABAtenaEntity.ZAIRYUSKAKCD:             // 在留資格コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUSKAKCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case156 when case156 == ABAtenaEntity.ZAIRYUSKAK:               // 在留資格
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUSKAK);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case157 when case157 == ABAtenaEntity.ZAIRYUKIKAN:              // 在留期間
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUKIKAN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case158 when case158 == ABAtenaEntity.ZAIRYU_ST_YMD:            // 在留開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYU_ST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case159 when case159 == ABAtenaEntity.ZAIRYU_ED_YMD:            // 在留終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYU_ED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case160 when case160 == ABAtenaEntity.RESERCE:                  // リザーブ
                        {
                            break;
                        }
                    // チェックなし

                    case var case161 when case161 == ABAtenaEntity.TANMATSUID:               // 端末ＩＤ
                        {
                            // * 履歴番号 000010 2003/09/11 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000010 2003/09/11 修正修正
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case162 when case162 == ABAtenaEntity.SAKUJOFG:                 // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case163 when case163 == ABAtenaEntity.KOSHINCOUNTER:            // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case164 when case164 == ABAtenaEntity.SAKUSEINICHIJI:           // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case165 when case165 == ABAtenaEntity.SAKUSEIUSER:              // 作成ユーザ
                        {
                            // * 履歴番号 000011 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000011 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case166 when case166 == ABAtenaEntity.KOSHINNICHIJI:            // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case167 when case167 == ABAtenaEntity.KOSHINUSER:               // 更新ユーザ
                        {
                            // * 履歴番号 000011 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000011 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                }
            }

            // デバッグ終了ログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSHICHOSONCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KJNHJNKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANNAIKANGAIKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNKEITAI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREWMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSU).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEKINO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZOKUGARACD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZOKUGARA).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2ZOKUGARACD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2ZOKUGARA).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAINUSJUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANASTAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2STAINUSJUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANADAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.YUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUIDOYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUJIYUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUJIYU).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOIDOYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOJIYUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOJIYU).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIYUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI3);

                // *履歴番号 000027 2010/05/12 追加開始
                // 本籍筆頭者情報抽出判定
                if (m_strHonsekiKB == "1" && m_strHonsekiHittoshKB_Param == "1")
                {
                    // 本籍住所、本籍番地、筆頭者を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_JUSHO).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HONSEKIBANCHI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HITTOSH);
                }
                else
                {
                }

                // 処理停止区分抽出判定
                if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                {
                    // 処理停止区分を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHORITEISHIKB);
                }
                else
                {
                }
                // *履歴番号 000027 2010/05/12 追加終了

                // *履歴番号 000028 2011/05/18 追加開始
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // 外国人在留情報(国籍、在留資格コード、在留資格、在留期間、在留開始年月日、在留終了年月日)を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAKCD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAK).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUKIKAN).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ST_YMD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ED_YMD);
                }
                else
                {
                }
            }
            // *履歴番号 000028 2011/05/18 追加終了
            else
            {
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSHICHOSONCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KJNHJNKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANNAIKANGAIKB).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNKEITAI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREWMD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANASTAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANADAI2STAINUSMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.YUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIYUBINNO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHO).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKIFG).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKICD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUMEI).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI1).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI2).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3).Append(",");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI3);

                // *履歴番号 000027 2010/05/12 追加開始
                // 本籍筆頭者情報抽出判定
                if (m_strHonsekiKB == "1" && m_strHonsekiHittoshKB_Param == "1")
                {
                    // 本籍住所、本籍番地、筆頭者を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_JUSHO).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HONSEKIBANCHI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HITTOSH);
                }
                else
                {
                }

                // 処理停止区分抽出判定
                if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                {
                    // 処理停止区分を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHORITEISHIKB);
                }
                else
                {
                }
                // *履歴番号 000027 2010/05/12 追加終了

                // *履歴番号 000028 2011/05/18 追加開始
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // 外国人在留情報(国籍、在留資格コード、在留資格、在留期間、在留開始年月日、在留終了年月日)を抽出項目にセットする
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKI).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAKCD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAK).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUKIKAN).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ST_YMD).Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ED_YMD);
                }
                else
                {
                }
                // *履歴番号 000028 2011/05/18 追加終了
            }
            if (m_blnSelectAll == ABEnumDefine.AtenaGetKB.NenkinAll)
            {
                strAtenaSQLsb.Append(",");
                // 旧姓
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSEI).Append(",");
                // 住定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIIDOYMD).Append(",");
                // 住定事由
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIJIYU).Append(",");
                // 転入前住所郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_YUBINNO).Append(",");
                // 転入前住所全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_ZJUSHOCD).Append(",");
                // 転入前住所住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_JUSHO).Append(",");
                // 転入前住所番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_BANCHI).Append(",");
                // 転入前住所方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_KATAGAKI).Append(",");
                // 転出予定郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO).Append(",");
                // 転出予定全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",");
                // 転出予定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD).Append(",");
                // 転出予定住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIJUSHO).Append(",");
                // 転出予定番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIBANCHI).Append(",");
                // 転出予定方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI).Append(",");
                // 転出確定郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIYUBINNO).Append(",");
                // 転出確定全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD).Append(",");
                // 転出確定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIIDOYMD).Append(",");
                // 転出確定通知年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD).Append(",");
                // 転出確定住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIJUSHO).Append(",");
                // 転出確定番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIBANCHI).Append(",");
                // 転出確定方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI).Append(",");

                // 消除届出年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOTDKDYMD).Append(",");
                // 直近事由コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINJIYUCD).Append(",");

                // 本籍全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_ZJUSHOCD).Append(",");
                // 転出予定世帯主名
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",");
                // 転出確定世帯主名
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI).Append(",");
                // *履歴番号 000021 2006/07/31 追加開始
                // 国籍コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD).Append(",");
                // 転入前住所世帯主名
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_STAINUSMEI);
                // strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD)
                // *履歴番号 000021 2006/07/31 追加終了

            }

            // *履歴番号 000022 2007/04/28 追加開始
            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
            {
                strAtenaSQLsb.Append(",");
                // 旧姓
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSEI).Append(",");
                // 住定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIIDOYMD).Append(",");
                // 住定事由
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIJIYU).Append(",");
                // 転入前住所郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_YUBINNO).Append(",");
                // 転入前住所全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_ZJUSHOCD).Append(",");
                // 転入前住所住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_JUSHO).Append(",");
                // 転入前住所番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_BANCHI).Append(",");
                // 転入前住所方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_KATAGAKI).Append(",");
                // 転出予定郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO).Append(",");
                // 転出予定全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",");
                // 転出予定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD).Append(",");
                // 転出予定住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIJUSHO).Append(",");
                // 転出予定番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIBANCHI).Append(",");
                // 転出予定方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI).Append(",");
                // 転出確定郵便番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIYUBINNO).Append(",");
                // 転出確定全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD).Append(",");
                // 転出確定異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIIDOYMD).Append(",");
                // 転出確定通知年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD).Append(",");
                // 転出確定住所
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIJUSHO).Append(",");
                // 転出確定番地
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIBANCHI).Append(",");
                // 転出確定方書
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI).Append(",");
                // 消除届出年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOTDKDYMD).Append(",");
                // 直近事由コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINJIYUCD).Append(",");
                // 本籍全国住所コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_ZJUSHOCD).Append(",");
                // 転出予定世帯主名
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",");
                // 転出確定世帯主名
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI).Append(",");
                // 国籍コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD).Append(",");
                // 登録届出年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUTDKDYMD).Append(",");
                // 住定届出年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEITDKDYMD).Append(",");
                // 転出入理由
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUNYURIYU).Append(",");
                // 市町村コード
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD).Append(",");
                // 直近異動年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINIDOYMD).Append(",");
                // 更新日時
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOSHINNICHIJI);
            }
            // *履歴番号 000022 2007/04/28 追加終了
            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
            {
                strAtenaSQLsb.Append(",");
                // 直近届出通知区分
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINTDKDTUCIKB).Append(",");
                // 版番号
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANNO).Append(",");
                // 改製年月日
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KAISEIYMD);
                if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && m_blnSelectAll != ABEnumDefine.AtenaGetKB.NenkinAll)
                {
                    // 国籍コード
                    strAtenaSQLsb.Append(",");
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD);
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

            // *履歴番号 000025 2008/01/15 追加開始
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
            // *履歴番号 000025 2008/01/15 追加終了
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
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名付随データ項目編集
        // * 
        // * 構文           Private SetFZYEntity()
        // * 
        // * 機能           宛名付随データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TABLEINSERTKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.LINKNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINHYOJOTAIKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKYOCHITODOKEFLG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KATAKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.UMAREFUSHOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHAMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUCARDNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEISTYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEIEDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSKANAMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE10);
        }
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000030 2014/04/28 追加開始
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
        // *履歴番号 000030 2014/04/28 追加終了

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
            // Dim cfUFParameterClass As UFParameterClass

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
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(".");
                    strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD);
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
                    strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME);
                    strAtenaSQLsb.Append(".");
                    strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD);
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
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME);
                strAtenaSQLsb.Append(".");
                strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD);
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
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD);

            // LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ");
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
            strAtenaSQLsb.Append("=");
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD);

            // *履歴番号 000025 2008/01/15 追加開始
            if (m_strKobetsuShutokuKB == "1")
            {
                // 個別事項取得区分が"1"の場合、後期高齢者マスタもJOINする
                strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(" ON ");
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
                strAtenaSQLsb.Append("=");
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.JUMINCD);
            }
            else
            {
                // 個別事項取得区分が値無しの場合、処理を行わない
            }
            // *履歴番号 000025 2008/01/15 追加終了
        }
        // *履歴番号 000029 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名付随テーブルJOIN句作成
        // * 
        // * 構文           Private SetFZYJoin()
        // * 
        // * 機能           宛名付随テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB, ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINJUTOGAIKB);
        }
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000030 2014/04/28 追加開始
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
            strAtenaSQLsb.AppendFormat("ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.JUMINCD);
        }
        // *履歴番号 000030 2014/04/28 追加終了

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
                m_strHonsekiKB = cABAtenaKanriJoho.GetHonsekiKB_Param();

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
        // *履歴番号 000027 2010/05/12 追加終了

        // *履歴番号 000029 2011/10/24 追加開始
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
                else
                {
                    // 処理なし
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
        // *履歴番号 000029 2011/10/24 追加終了

        // *履歴番号 000030 2014/04/28 追加開始
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
        // *履歴番号 000030 2014/04/28 追加終了

        // *履歴番号 000031 2018/03/08 追加開始
        /// <summary>
    /// 抽出条件文字列の生成
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereMain(ABAtenaSearchKey cSearchKey, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWhere;
            StringBuilder csWhereForRireki;
            string strWhereRirekiHyojun;
            string strWhereRirekiFZY;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 履歴検索判定
                if (cSearchKey.p_blnIsRirekiSearch == true)
                {

                    // [履歴検索]

                    // パラメーターコレクションクラスのインスタンス化
                    m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                    // 直近に対する抽出条件を生成
                    csWhere = new StringBuilder(CreateWhereForChokkin(cSearchKey, blnSakujoFG));

                    // 履歴に対する抽出条件を生成
                    csWhereForRireki = new StringBuilder(CreateWhereForRireki(cSearchKey));

                    // 履歴に対する抽出条件が指定されている場合、
                    // 該当者の住民コードで直近を絞り込む
                    if (csWhereForRireki.RLength() > 0)
                    {

                        if (csWhere.RLength() > 0)
                        {
                            csWhere.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                        csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD);
                        csWhere.AppendFormat(" WHERE {0}", csWhereForRireki);
                        csWhere.Append(")");
                    }

                    else
                    {
                        // noop
                    }

                    // 履歴標準
                    strWhereRirekiHyojun = CreateWhereRirekiHyojun(cSearchKey);
                    if (strWhereRirekiHyojun.RLength() > 0)
                    {

                        if (csWhere.RLength() > 0)
                        {
                            csWhere.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                        csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUMINCD);
                        csWhere.AppendFormat(" WHERE {0}", strWhereRirekiHyojun);
                        csWhere.Append(")");
                    }

                    else
                    {
                        // noop
                    }

                    // 履歴付随
                    strWhereRirekiFZY = CreateWhereRirekiFZY(cSearchKey);
                    if (strWhereRirekiFZY.RLength() > 0)
                    {

                        if (csWhere.RLength() > 0)
                        {
                            csWhere.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                        csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINCD);
                        csWhere.AppendFormat(" WHERE {0}", strWhereRirekiFZY);
                        csWhere.Append(")");
                    }

                    else
                    {
                        // noop
                    }
                }
                else
                {

                    // [直近検索]

                    // 既存の処理をそのまま実行する
                    csWhere = new StringBuilder(CreateWhere(cSearchKey));

                    // 削除フラグ
                    if (blnSakujoFG == false)
                    {
                        if (!(csWhere.RLength() == 0))
                        {
                            csWhere.Append(" AND ");
                        }
                        csWhere.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG);
                        csWhere.Append(" <> '1'");
                    }

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWhere.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（直近用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <param name="blnSakujoFG">削除フラグ</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereForChokkin(ABAtenaSearchKey cSearchKey, bool blnSakujoFG)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 住民コード
                if (!(cSearchKey.p_strJuminCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住民優先区分
                if (!(cSearchKey.p_strJuminYuseniKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINYUSENIKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUMINYUSENIKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINYUSENIKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 住登外優先区分
                if (!(cSearchKey.p_strJutogaiYusenKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTOGAIYUSENKB);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB;
                    cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // データ区分
                if (!(cSearchKey.p_strDataKB.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }

                    if (cSearchKey.p_strDataKB.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaEntity.ATENADATAKB);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB);

                    }

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_ATENADATAKB;
                    cfUFParameterClass.Value = cSearchKey.p_strDataKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 宛名データ種別
                switch (cSearchKey.p_strDataKB.Trim())
                {

                    // noop
                    case var @case when @case == ABConstClass.ATENADATAKB_HOJIN:
                        {
                            break;
                        }

                    default:
                        {

                            // [住登内個人][住登外個人][共有][指定なし]の場合

                            if (!(cSearchKey.p_strJuminShubetu1 == string.Empty & cSearchKey.p_strJuminShubetu2 == string.Empty))
                            {
                                if (cSearchKey.p_strDataKB.Trim() == string.Empty)
                                {
                                    if (!(csWHERE.RLength() == 0))
                                    {
                                        csWHERE.Append(" AND ");
                                    }
                                    csWHERE.Append("((");
                                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB);
                                    csWHERE.Append(" = '11')");
                                    csWHERE.Append(" OR (");
                                    csWHERE.Append(ABAtenaEntity.ATENADATAKB);
                                    csWHERE.Append(" = '12'))");
                                }

                                // 住民種別１
                                if (!(cSearchKey.p_strJuminShubetu1.Trim() == string.Empty))
                                {
                                    if (!(csWHERE.RLength() == 0))
                                    {
                                        csWHERE.Append(" AND ");
                                    }
                                    csWHERE.Append(" {fn SUBSTRING(");
                                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU);
                                    csWHERE.Append(",1,1)} = '");
                                    csWHERE.Append(cSearchKey.p_strJuminShubetu1);
                                    csWHERE.Append("'");
                                }

                                // 住民種別２
                                if (!(cSearchKey.p_strJuminShubetu2.Trim() == string.Empty))
                                {
                                    if (!(csWHERE.RLength() == 0))
                                    {
                                        csWHERE.Append(" AND ");
                                    }
                                    csWHERE.Append(" {fn SUBSTRING(");
                                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU);
                                    csWHERE.Append(",2,1)} = '");
                                    csWHERE.Append(cSearchKey.p_strJuminShubetu2);
                                    csWHERE.Append("'");
                                }
                            }

                            break;
                        }

                }

                // 市町村コード
                if (!(cSearchKey.p_strShichosonCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaEntity.PARAM_SHICHOSONCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SHICHOSONCD;
                    cfUFParameterClass.Value = cSearchKey.p_strShichosonCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // ---------------------------------------------------------------------------------------------------------
                // 共通番号が指定されている場合
                if (cSearchKey.p_strMyNumber.Trim().RLength() > 0)
                {

                    // -----------------------------------------------------------------------------------------------------
                    // 【１．直近検索区分による制御】
                    // 直近検索区分の整備
                    switch (cSearchKey.p_strMyNumberChokkinSearchKB)
                    {
                        case var case1 when case1 == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode().ToString:
                        // noop
                        case var case2 when case2 == ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode().ToString:
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
                    if (cSearchKey.p_strMyNumberChokkinSearchKB == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode().ToString())
                    {

                        // 共通番号カラムに共通番号を指定する。
                        if (csWHERE.RLength() > 0)
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
                        if (csWHERE.RLength() > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
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
                        if (csWHERE.RLength() > 0)
                        {
                            csWHERE.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }
                        csWHERE.AppendFormat("{0}.{1} = {2}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KJNHJNKB, ABAtenaEntity.PARAM_KJNHJNKB);

                        // 検索条件のパラメーターを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KJNHJNKB;
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
                // ---------------------------------------------------------------------------------------------------------

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG);
                    csWHERE.Append(" <> '1'");
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（履歴用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereForRireki(ABAtenaSearchKey cSearchKey)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 世帯コード
                if (!(cSearchKey.p_strStaiCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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

                // *履歴番号 000032 2020/01/10 修正開始
                // '検索用カナ姓名
                // If Not (cSearchKey.p_strSearchKanaSeiMei.Trim() = String.Empty) Then
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
                // If Not (cSearchKey.p_strSearchKanaSei.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // ' 検索用カナ姓２に検索キーが格納されている場合は検索条件として追加
                // If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
                // csWHERE.Append(" ( ")
                // End If
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

                // '検索用カナ名
                // If Not (cSearchKey.p_strSearchKanaMei.Trim() = String.Empty) Then
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
                // If Not (cSearchKey.p_strSearchKanjiMeisho.Trim() = String.Empty) Then
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

                // ' 本名漢字姓名 本名検索="2(Tsusho_Seishiki)"のときのみ漢字氏名２は検索項目となる
                // If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                // If Not (cSearchKey.p_strKanjiMeisho2.Trim() = String.Empty) Then
                // If Not (csWHERE.Length = 0) Then
                // csWHERE.Append(" AND ")
                // End If
                // If cSearchKey.p_strKanjiMeisho2.IndexOf("%") = -1 Then
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2)
                // csWHERE.Append(" = ")
                // csWHERE.Append(ABAtenaRirekiEntity.PARAM_KANJIMEISHO2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KANJIMEISHO2
                // cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // Else
                // csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2)
                // csWHERE.Append(" LIKE ")
                // csWHERE.Append(ABAtenaRirekiEntity.PARAM_KANJIMEISHO2)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KANJIMEISHO2
                // cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If
                // End If
                // End If

                // 氏名検索条件を生成
                m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaRirekiEntity.TABLE_NAME, ref csWHERE, ref m_cfSelectUFParameterCollectionClass, ABAtenaRirekiFZYHyojunEntity.TABLE_NAME);
                // *履歴番号 000032 2020/01/10 修正終了

                // 生年月日
                if (!(cSearchKey.p_strUmareYMD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                        cfUFParameterClass.Value = cSearchKey.p_strUmareYMD.TrimEnd();

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // 性別
                if (!(cSearchKey.p_strSeibetsuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJushoCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strGyoseikuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strChikuCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strChikuCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strChikuCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strBanchiCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strBanchiCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strBanchiCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiJushoCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiGyoseikuCD.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiChikuCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiChikuCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiChikuCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiBanchiCD1.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiBanchiCD2.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strJukiBanchiCD3.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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

                // 法人形態
                switch (cSearchKey.p_strDataKB.Trim())
                {

                    case var @case when @case == ABConstClass.ATENADATAKB_HOJIN:
                        {

                            // [法人]の場合

                            if (!(cSearchKey.p_strJuminShubetu1 == string.Empty & cSearchKey.p_strJuminShubetu2 == string.Empty))
                            {
                                // 住民種別１
                                if (!(cSearchKey.p_strJuminShubetu1.Trim() == string.Empty))
                                {
                                    if (!(csWHERE.RLength() == 0))
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
                                if (!(cSearchKey.p_strJuminShubetu2.Trim() == string.Empty))
                                {
                                    if (!(csWHERE.RLength() == 0))
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

                            break;
                        }

                    default:
                        {
                            break;
                        }
                        // noop
                }

                // 電話番号
                if (!(cSearchKey.p_strRenrakusaki.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }
        // *履歴番号 000031 2018/03/08 追加終了

        /// <summary>
    /// 抽出条件文字列の生成（宛名標準用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereHyojun(ABAtenaSearchKey cSearchKey)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 住所
                if (!(cSearchKey.p_strJusho.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strJusho.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHJUSHO);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHJUSHO);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHJUSHO);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHJUSHO);
                    }
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHJUSHO;
                    cfUFParameterClass.Value = cSearchKey.p_strJusho;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 方書
                if (!(cSearchKey.p_strKatagaki.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strKatagaki.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKATAGAKI);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKATAGAKI);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI);
                    }
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI;
                    cfUFParameterClass.Value = cSearchKey.p_strKatagaki;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 旧氏
                if (!(cSearchKey.p_strKyuuji.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strKyuuji.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI);
                    }
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI;
                    cfUFParameterClass.Value = cSearchKey.p_strKyuuji;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // カナ旧氏
                if (!(cSearchKey.p_strKanaKyuuji.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strKanaKyuuji.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANAKYUUJI);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANAKYUUJI);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI);
                    }
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI;
                    cfUFParameterClass.Value = cSearchKey.p_strKanaKyuuji;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（宛名付随用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereFZY(ABAtenaSearchKey cSearchKey)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // カタカナ併記名
                if (!(cSearchKey.p_strKatakanaHeikimei.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
                    {
                        csWHERE.Append(" AND ");
                    }
                    if (cSearchKey.p_strKatakanaHeikimei.RIndexOf("%") == -1)
                    {
                        csWHERE.Append(ABAtenaFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaFZYEntity.KATAKANAHEIKIMEI);
                        csWHERE.Append(" = ");
                        csWHERE.Append(ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI);
                    }
                    else
                    {
                        csWHERE.Append(ABAtenaFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaFZYEntity.KATAKANAHEIKIMEI);
                        csWHERE.Append(" LIKE ");
                        csWHERE.Append(ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI);
                    }
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI;
                    cfUFParameterClass.Value = cSearchKey.p_strKatakanaHeikimei;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（宛名履歴標準用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereRirekiHyojun(ABAtenaSearchKey cSearchKey)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 住所
                if (!(cSearchKey.p_strJusho.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strKatagaki.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strKyuuji.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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
                if (!(cSearchKey.p_strKanaKyuuji.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（宛名付随用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        private string CreateWhereRirekiFZY(ABAtenaSearchKey cSearchKey)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // カタカナ併記名
                if (!(cSearchKey.p_strKatakanaHeikimei.Trim() == string.Empty))
                {
                    if (!(csWHERE.RLength() == 0))
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

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");
                throw;

            }

            return csWHERE.ToString();

        }
        // *履歴番号 000033 2023/03/10 追加開始
        #region 宛名標準データ項目編集
        // ************************************************************************************************
        // * メソッド名     宛名標準データ項目編集
        // * 
        // * 構文           Private SetHyojunEntity()
        // * 
        // * 機能           宛名標準データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetHyojunEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RRKNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.EDANO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.UMAREBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOUMAREBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JIJITSUSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KANAKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.BANCHIEDABANSUCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KUNIMEICODE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KUNIMEITO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CKINIDOWMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOCKINIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOTOROKUIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KISAIYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KISAIBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKISAIBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOJUTEIIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOJOIDOWMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOSHOJOIDOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_YUBINNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_BANCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_MACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_BANCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_KATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEIBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKAISEIBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEISHOJOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD10);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOKUBETSUYOSHIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNIDOKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NYURYOKUBASHOCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NYURYOKUBASHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKANJIKYUUJI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKANAKYUUJI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TDKDSHIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNIDOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOROKUBUSHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TANKITAIZAISHAFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KYOYUNINZU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIZEIJIMUSHOCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.IDOSHURUI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOKANKUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOGOATENAFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOUMAREBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKISHIKUCHOSONCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIMACHIAZACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKITODOFUKEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKISHIKUCHOSON);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIMACHIAZA);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIKANAKATAGAKI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD10);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE5);
        }
        #endregion

        #region 宛名付随標準データ項目編集
        // ************************************************************************************************
        // * メソッド名     宛名付随標準データ項目編集
        // * 
        // * 構文           Private SetFZYHyojunEntity()
        // * 
        // * 機能           宛名付随標準データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYHyojunEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHFRNMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SHIMEIYUSENKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE5);
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
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD);
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

        #region 宛名標準テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     宛名標準テーブルJOIN句作成
        // * 
        // * 構文           Private SetHyojunJoin()
        // * 
        // * 機能           宛名標準テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetHyojunJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaHyojunEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB, ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINJUTOGAIKB);
        }
        #endregion

        #region 宛名付随標準テーブルJOIN句作成
        // ************************************************************************************************
        // * メソッド名     宛名付随標準テーブルJOIN句作成
        // * 
        // * 構文           Private SetFZYHyojunJoin()
        // * 
        // * 機能           宛名付随標準テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYHyojunJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYHyojunEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINCD);
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB, ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB);
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
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.JUMINCD);
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
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.JUMINCD);
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

                strAtenaSQLsb.AppendFormat(" ON {0}.{1} = DS3.{2} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD, ABDENSHISHOMEISHOMSTEntity.JUMINCD);
                strAtenaSQLsb.AppendFormat(" AND {0}.{1} = DS3.{2} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.STAICD, ABDENSHISHOMEISHOMSTEntity.STAICD);
            }
        }
        #endregion
        // *履歴番号 000033 2023/03/10 追加終了
        #endregion

    }
}
