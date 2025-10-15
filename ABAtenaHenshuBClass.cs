// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名編集クラス(ABAtenaHenshuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/14　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/02/19 000001     本人送付先編集で、送付先が編集されない場合がある
// * 2003/02/20 000002     データが空白の場合の、判定に不備
// *                       送付先データマージ条件の変更
// * 2003/02/21 000003     送付先データを編集する時、業務コード・業務内種別は、送付先レコードよりセット
// * 2003/02/25 000004     住所編集3で、3，4の場合（）を付加する。但し、無い場合は、（）無し
// *                       方書を付加する時に漢字スペースを１個入れて付加してください（仕様変更）
// * 2003/02/25 000005     送付先が存在しない場合、業務コード・業務内種別コードは String.Empty とする
// * 2003/03/07 000006     プロジェクトのImportsは定義しない（仕様変更）
// * 2003/03/17 000007     パラメータのチェックを入れる
// * 2003/03/17 000008     住所編集３の値なしの考慮を追加（仕様変更）
// * 2003/03/18 000009     エラーメッセージの変更（仕様変更）
// * 2003/03/27 000010     エラー処理クラスの参照先を"AB"固定にする
// * 2003/04/01 000011     ABAtena1のプライマリーキーを外す
// * 2003/04/18 000012     宛名情報Entityに続柄コード・続柄・カナ名称２・漢字名称２・籍番号を追加
// * 2003/04/18 000013     年金用宛名情報Entityを追加
// * 2003/04/30 000014     法人の時、カナ名称2、漢字名称２は、セットしない（仕様変更）
// * 2003/04/30 000015     宛名編集項目を初期化後、設定する。                      
// * 2003/08/22 000016     ＵＲキャッシュ対応／継承可能クラスに変更
// * 2003/10/09 000017     連絡先は、連絡先マスタにデータが存在する場合は、そちらから取得する。但し、業務コードが指定されてた場合のみに限る。
// *                       NenkinAtenaGetもAtenaGet1と同様に指定年月日が指定されたら、宛名履歴より取得する。連絡先も同様。但し、代納・合算は不要。
// * 2003/10/14 000018     履歴編集で、続柄２が無い場合、続柄が編集されない。
// * 2003/11/19 000019     宛名個別情報編集処理を追加
// * 2003/12/01 000020     連絡先業務コードをABAtena1からはずす。ABNenkinAtena・個別宛名に追加
// * 2003/12/02 000021     連絡先取得・編集を宛名取得へ移動
// * 2003/12/04 000022     仕様変更：年金用宛名情報Entity項目追加に伴う変更
// * 2004/08/27 000023     速度改善：（宮沢）
// * 2005/01/25 000024     速度改善２：（宮沢）
// * 2005/07/14 000025     CheckColumnValueメソッドでの住所編集３の値の範囲を修正(マルゴ村山)
// * 2005/12/21 000026     住民票表示順の編集仕様変更(笹井)
// * 2006/07/31 000027     年金宛名ゲットⅡ追加(吉澤)
// * 2007/01/15 000028     住所編集パターン追加
// *                       履歴編集・住基優先ではない場合のコーディング修正
// * 2007/01/25 000029     送付先に番地コードを設定するように修正
// * 2007/04/28 000030     介護版宛名取得メソッドの追加による取得項目の追加 (吉澤)
// * 2007/06/28 000031     DB文字数拡張対応，文字数拡張にともなう宛名情報カラム定義部および年金用宛名情報カラム定義部MaxLength値修正
// *                       （対応個所が複数に渡る為，履歴番号付加無し）（中沢）
// * 2007/07/09 000032     文字列結合後に切り詰めている文字数の修正（中沢）
// * 2007/07/17 000033     支店名が無い場合は，法人名称と支店名の結合処理を行わない（中沢）
// * 2008/01/15 000034     宛名個別情報カラム作成に後期高齢情報項目を追加（比嘉）＆ネーミング変更（吉澤）
// * 2008/02/15 000035     氏名簡略文字編集処理を追加（比嘉）
// * 2008/11/10 000036     宛名データセットの作成時に納税者ID・利用者IDを追加（比嘉）
// * 2008/11/17 000037     送付先編集項目を初期化する処理を追加（比嘉）
// * 2008/11/18 000038     履歴番号:000036の追加に伴う改修（比嘉）
// * 2010/04/16 000039     VS2008対応（比嘉）
// * 2010/05/14 000040     本籍筆頭者及び処理停止区分対応（比嘉）
// * 2011/05/18 000041     外国人在留情報取得区分対応（比嘉）
// * 2011/05/18 000042     本名・通称名優先制御対応（比嘉）
// * 2011/06/23 000043     本名・通称名優先制御対応US機能組み込み改修（比嘉）
// * 2011/06/24 000044     レイアウト：年金用の外国人在留情報の設定位置を変更（比嘉）
// * 2011/06/27 000045     名称編集処理で本名優先処理の場合に漢字名称２の存在チェック行うように改修（比嘉）
// * 2011/11/07 000046     【AB17010】住基法改正により宛名付随データを結合して取得するように改修（池田）
// * 2012/03/13 000047     【AB17010-00】連続処理により異常終了する不具合修正（中嶋）
// * 2014/04/28 000048     【AB21040】＜共通番号対応＞共通番号追加（石合）
// * 2022/12/16 000049     【AB-8010】住民コード世帯コード15桁対応(下村)
// * 2023/03/10 000050     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2023/10/19 000051     【AB-0820-1】住登外管理項目追加_追加修正(仲西)
// * 2023/12/22 000020     【AB-0970-1_2】宛名GET日付項目設定対応(下村)
// * 2024/06/17 000021     【AB-9903-1】不具合対応
// ************************************************************************************************
using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
// *履歴番号 000006  2003/03/07 削除開始
// Imports Densan.Reams.AB.AB001BX
// *履歴番号 000006  2003/03/07 削除終了
using System.Data;
using System.Linq;
using System.Security;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAtenaHenshuBClass
    {

        #region  メンバ変数 
        // ************************************************************************************************
        // *
        // * 宛名編集に使用するパラメータクラス
        // *
        // ************************************************************************************************
        // *履歴番号 000016 2003/08/22 修正開始
        // 'パラメータのメンバ変数
        // Private m_cfUFLogClass As UFLogClass                    'ログ出力クラス
        // Private m_cfUFControlData As UFControlData              'コントロールデータ
        // Private m_cfUFConfigDataClass As UFConfigDataClass      'コンフィグデータ
        // Private m_cfUFRdbClass As UFRdbClass                    'ＲＤＢクラス

        // '　コンスタント定義
        // Private Const THIS_CLASS_NAME As String = "ABAtenaHenshuBClass"             ' クラス名
        // Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
        // Private Const NENKIN As String = "NENKIN"

        // パラメータのメンバ変数
        protected UFLogClass m_cfUFLogClass;                                      // ログ出力クラス
        protected UFControlData m_cfUFControlData;                                // コントロールデータ
        protected UFConfigDataClass m_cfUFConfigDataClass;                        // コンフィグデータ
        protected UFRdbClass m_cfUFRdbClass;                                      // ＲＤＢクラス

        // コンスタント定義
        protected const string THIS_CLASS_NAME = "ABAtenaHenshuBClass";           // クラス名
        protected const string THIS_BUSINESSID = "AB";                            // 業務コード
        protected const string NENKIN = "NENKIN";                                 // 年金処理
                                                                                  // *履歴番号 000027 2006/07/31 追加開始
        protected const string NENKIN_2 = "NENKIN_2";                                 // 年金処理パートⅡ
                                                                                      // *履歴番号 000027 2006/07/31 追加終了
                                                                                      // *履歴番号 000016 2003/08/22 修正終了

        // *履歴番号 000019 2003/11/19 追加開始
        protected const string KOBETSU = "KOBETSU";                               // 宛名個別情報処理
                                                                                  // *履歴番号 000019 2003/11/19 追加終了

        // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
        private USSCityInfoClass m_cuUSSCityInfo;               // 市町村情報管理クラス
        private ABDainoKankeiBClass m_cABDainoKankeiB;          // 代納関係クラス
        private ABJuminShubetsuBClass m_cABJuminShubetsuB;      // 住民種別クラス
        private ABHojinMeishoBClass m_cABHojinMeishoB;          // 法人名称クラス
        private ABKjnhjnKBBClass m_cABKjnhjnKBB;                // 個人法人クラス
        private ABKannaiKangaiKBBClass m_cABKannaiKangaiKBB;    // 管内管外クラス
        private ABUmareHenshuBClass m_cABUmareHenshuB;          // 生年月日編集クラス
        private ABCommonClass m_cABCommon;                      // 宛名共通クラス
        private URKANRIJOHOCacheBClass m_cURKanriJohoB;         // 管理情報取得クラス
                                                                // * 履歴番号 000023 2004/08/27 追加終了
                                                                // * 履歴開始 000035 2008/02/15 追加開始
        private ABMojiretsuHenshuBClass m_cABMojiHenshuB;       // 文字編集Ｂクラス
                                                                // * 履歴開始 000035 2008/02/15 追加終了
                                                                // *履歴番号 000042 2011/05/18 追加開始
        private ABMeishoSeigyoBClass m_cABMeishoSeigyoB;        // 名称制御Ｂクラス
                                                                // *履歴番号 000043 2011/06/23 修正開始
        private USUrlParmClass m_cuUSSUrlParm;                  // USURLパラメータクラス
                                                                // *履歴番号 000043 2011/06/23 修正終了
                                                                // *履歴番号 000042 2011/05/18 追加終了
        private ABHyojunkaCdHenshuBClass m_cABHyojunkaCdHenshuB;    // 標準化コード編集クラス

        // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
        protected SofuJushoGyoseikuType m_cSofuJushoGyoseikuType;
        protected bool m_bSofuJushoGyoseikuTypeFlg = false;
        public ABEnumDefine.AtenaGetKB m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniAll;
        private StringBuilder m_strHenshuJusho = new StringBuilder(200);                        // 編集住所名
        private DataTable m_csOrgAtena1;
        private DataTable m_csOrgAtena1Kobetsu;
        private DataTable m_csOrgAtena1Nenkin;
        // * 履歴番号 000024 2005/01/25 追加終了

        // *履歴番号 000030 2007/04/28 追加開始
        public ABEnumDefine.MethodKB m_blnMethodKB;  // メソッド区分（通常版か、介護版、、、）
                                                     // *履歴番号 000030 2007/04/28 追加終了

        // *履歴番号 000034 2008/01/15 追加開始
        private string m_strKobetsuShutokuKB;         // 宛名取得パラメータ:個別時効取得区分
                                                      // *履歴番号 000034 2008/01/15 追加終了

        // *履歴番号 000036 2008/11/10 追加開始
        private string m_strRiyoTdkdKB;               // 利用届出取得区分
        private bool m_blnKobetsu;                 // 個別事項判定フラグ
                                                   // *履歴番号 000036 2008/11/10 追加終了

        // *履歴番号 000040 2010/05/14 追加開始
        private string m_strHonsekiHittoshKB_Param;                   // 本籍筆頭者区分パラメータ
        private string m_strShoriteishiKB_Param;                      // 処理停止区分パラメータ
        private string m_strHonsekiHittoshKB = string.Empty;          // 本籍筆頭者取得区分(宛名管理情報)
        private string m_strShoriteishiKB = string.Empty;             // 処理停止区分取得区分(宛名管理情報)
        private bool m_blnNenKin = false;                          // 年金版判定フラグ
                                                                   // *履歴番号 000040 2010/05/14 追加終了

        // *履歴番号 000041 2011/05/18 追加開始
        private string m_strFrnZairyuJohoKB_Param = string.Empty;     // 外国人在留情報取得区分パラメータ
                                                                      // *履歴番号 000041 2011/05/18 追加終了

        // *履歴番号 000042 2011/05/18 追加開始
        private string m_strHonmyoTsushomeiYusenKB = string.Empty;    // 本名通称名優先設定制御区分(宛名管理情報)
                                                                      // *履歴番号 000042 2011/05/18 追加終了
                                                                      // *履歴番号 000046 2011/11/07 追加開始
        private string m_strJukiHokaiseiKB_Param;                     // 住基法改正区分
                                                                      // *履歴番号 000046 2011/11/07 追加終了
                                                                      // *履歴番号 000048 2014/04/28 追加開始
        private string m_strMyNumberKB_Param = string.Empty;          // 共通番号取得区分
                                                                      // *履歴番号 000048 2014/04/28 追加終了
                                                                      // *履歴番号 000047 2012/03/13 追加開始
        private DataTable m_csOrgNenkinKobetsu;                       // 年金or個別の時の保持スキーマ
                                                                      // *履歴番号 000047 2012/03/13 追加終了
        public ABEnumDefine.HyojunKB m_intHyojunKB;                   // 宛名GET標準化区分
        private DataTable m_csOrgAtena1Hyojun;
        private DataTable m_csOrgAtena1KobetsuHyojun;
        private DataTable m_csOrgAtena1NenkinHyojun;
        private UFDateClass m_cfDate;
        private string m_strUmareYMDHenkanParam;
        private string m_strUmareWmdHenkan;
        private string m_strUmareWmdhenkanSeireki;
        private string m_strShojoIdobiHenkanParam;
        private string m_strShojoIdoWmdHenkan;
        private string m_strCknIdobiHenkanParam;
        private string m_strCknIdoWmdHenkan;

        #endregion

        #region  コンストラクタ 
        // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass,
        // *                               ByVal cfUFRdbClass as UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaHenshuBClass(UFControlData cfUFControlData, UFConfigDataClass cfUFConfigDataClass, UFRdbClass cfUFRdbClass)
        {
            Initial(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, ABEnumDefine.AtenaGetKB.KaniAll);
        }
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass,
        // *                               ByVal cfUFRdbClass as UFRdbClass)
        // * 　　                          ByVal blnSelectAll as boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaHenshuBClass(UFControlData cfUFControlData, UFConfigDataClass cfUFConfigDataClass, UFRdbClass cfUFRdbClass, ABEnumDefine.AtenaGetKB blnSelectAll)
        {
            Initial(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, blnSelectAll);
        }
        // * 履歴番号 000024 2005/01/25 追加終了
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass,
        // *                               ByVal cfUFRdbClass as UFRdbClass)
        // * 　　                          ByVal blnSelectAll as boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
        // Public Sub New(ByVal cfUFControlData As UFControlData, _
        // ByVal cfUFConfigDataClass As UFConfigDataClass, _
        // ByVal cfUFRdbClass As UFRdbClass)
        [SecuritySafeCritical]
        public void Initial(UFControlData cfUFControlData, UFConfigDataClass cfUFConfigDataClass, UFRdbClass cfUFRdbClass, ABEnumDefine.AtenaGetKB blnSelectAll)
        {
            // * 履歴番号 000024 2005/01/25 更新終了

            // メンバ変数セット
            m_cfUFControlData = cfUFControlData;
            m_cfUFConfigDataClass = cfUFConfigDataClass;
            m_cfUFRdbClass = cfUFRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfUFConfigDataClass, cfUFControlData.m_strBusinessId);

            // * 履歴番号 000023 2004/08/27 追加開始（宮沢）
            // '市町村情報のインスタンス作成
            m_cuUSSCityInfo = new USSCityInfoClass();
            m_cuUSSCityInfo.GetCityInfo(m_cfUFControlData);

            // '代納関係のインスタンス作成
            m_cABDainoKankeiB = new ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass);

            // '住民種別のインスタンス作成
            m_cABJuminShubetsuB = new ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass);

            // '法人名称のインスタンス作成
            m_cABHojinMeishoB = new ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass);

            // '個人法人のインスタンス作成
            m_cABKjnhjnKBB = new ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass);

            // '管内管外のインスタンス作成
            m_cABKannaiKangaiKBB = new ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass);

            // '生年月日編集のインスタンス作成
            m_cABUmareHenshuB = new ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass);

            m_cABCommon = new ABCommonClass();

            // 管理情報取得Ｂのインスタンス作成
            m_cURKanriJohoB = new URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass);
            // * 履歴番号 000023 2004/08/27 追加開始

            // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
            m_blnSelectAll = blnSelectAll;
            // * 履歴番号 000024 2005/01/25 追加終了

            // * 履歴番号 000035 2008/02/15 追加開始
            m_cABMojiHenshuB = new ABMojiretsuHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass);
            // * 履歴番号 000035 2008/02/15 追加終了

            // *履歴番号 000040 2010/05/14 追加開始
            // 管理情報取得処理
            GetKanriJoho();
            // *履歴番号 000040 2010/05/14 追加終了

            // '標準化コード編集のインスタンス作成
            m_cABHyojunkaCdHenshuB = new ABHyojunkaCdHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass);

        }


        // *履歴番号 000040 2010/05/14 追加開始
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
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名管理情報Ｂクラスのインスタンス作成
                if (cABAtenaKanriJoho is null)
                {
                    cABAtenaKanriJoho = new ABAtenaKanriJohoBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass);
                }

                // 本籍取得区分取得
                m_strHonsekiHittoshKB = cABAtenaKanriJoho.GetHonsekiKB_Param();

                // 処理停止区分取得区分取得
                m_strShoriteishiKB = cABAtenaKanriJoho.GetShoriteishiKB_Param();

                // *履歴番号 000042 2011/05/18 追加開始
                // 本名通称名優先設定制御取得
                m_strHonmyoTsushomeiYusenKB = cABAtenaKanriJoho.GetHonmyoTsushomeiYusenKB_Param();
                // *履歴番号 000042 2011/05/18 追加終了

                if (m_cfDate == null)
                {
                    m_cfDate = new UFDateClass(m_cfUFConfigDataClass);
                    m_cfDate.p_enDateSeparator = UFDateSeparator.None;
                }
                m_strUmareYMDHenkanParam = cABAtenaKanriJoho.GetUmareYMDHenkanHizuke_Param();
                m_cfDate.p_strDateValue = m_strUmareYMDHenkanParam;
                m_strUmareWmdHenkan = m_cfDate.p_strWarekiYMD;
                if (m_strUmareYMDHenkanParam.Trim().RLength >= 8)
                {
                    m_strUmareWmdhenkanSeireki = m_strUmareYMDHenkanParam.RSubstring(1, 7);
                }
                else
                {
                    m_strUmareWmdhenkanSeireki = string.Empty;
                }

                m_strShojoIdobiHenkanParam = cABAtenaKanriJoho.GetShojoIdobiHenkanHizuke_Param();
                m_cfDate.p_strDateValue = m_strShojoIdobiHenkanParam;
                m_strShojoIdoWmdHenkan = m_cfDate.p_strWarekiYMD;

                m_strCknIdobiHenkanParam = cABAtenaKanriJoho.GetCknIdobiHenkanHizuke_Param();
                m_cfDate.p_strDateValue = m_strCknIdobiHenkanParam;
                m_strCknIdoWmdHenkan = m_cfDate.p_strWarekiYMD;

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;

            }

        }
        // *履歴番号 000040 2010/05/14 追加終了
        #endregion

        #region  宛名編集(AtenaHenshu) 
        // ************************************************************************************************
        // * メソッド名     宛名編集
        // * 
        // * 構文           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
        // *                                           ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　編集宛名データを作成する
        // * 
        // * 引数           cAtenaGetPara1     : 宛名取得パラメータ
        // *               csAtenaEntity      : 宛名データ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {

            // *履歴番号 000013 2003/04/18 追加開始
            // Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "")
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", "");
            // *履歴番号 000013 2003/04/18 追加終了
        }

        // *履歴番号 000013 2003/04/18 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名編集
        // * 
        // * 構文           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet, 
        // *                                           ByVal strDainoKB As String,
        // *                                           ByVal strGyomuCD As String,
        // *                                           ByVal strGyomunaiSHU_CD As String) As DataSet
        // * 
        // * 機能　　    　　編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // * 　　         csAtenaEntity       : 宛名データ
        // * 　　         strDainoKB          : 代納区分
        // * 　　         strGyomuCD          : 業務コード
        // * 　　         strGyomunaiSHU_CD   : 業務内種別コード
        // * 
        // * 戻り値       DataSet(ABAtena1)   : 取得した宛名情報
        // ************************************************************************************************
        public DataSet AtenaHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD)
        {
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, "");
        }
        // ************************************************************************************************
        // * メソッド名     宛名編集
        // * 
        // * 構文           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet, 
        // *                                           ByVal strDainoKB As String,
        // *                                           ByVal strGyomuCD As String,
        // *                                           ByVal strGyomunaiSHU_CD As String,
        // *                                           ByVal strGyomuMei As String) As DataSet
        // * 
        // * 機能　　    　　編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ(ABAtenaGetPara1XClass)
        // *              csAtenaEntity       : 宛名データ(ABAtenaEntity)
        // *              strDainoKB          : 代納区分
        // *              strGyomuCD          : 業務コード
        // *              strGyomunaiSHU_CD   : 業務内種別コード
        // *              strGyomuMei         : 業務名
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        private DataSet AtenaHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD, string strGyomuMei)
        {
            // *履歴番号 000013 2003/04/18 追加終了
            const string THIS_METHOD_NAME = "AtenaHenshu";
            // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            DataTable csDataTable;
            DataSet csAtena1;                             // 宛名情報(ABAtena1)
            DataRow csDataNewRow;
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cuUSSCityInfo As USSCityInfoClass               '市町村情報管理クラス
            // Dim cABDainoKankeiB As ABDainoKankeiBClass          '代納関係クラス
            // Dim cABJuminShubetsuB As ABJuminShubetsuBClass      '住民種別クラス
            // Dim cABHojinMeishoB As ABHojinMeishoBClass          '法人名称クラス
            // Dim cABKjnhjnKBB As ABKjnhjnKBBClass                '個人法人クラス
            // Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '管内管外クラス
            // Dim cABUmareHenshuB As ABUmareHenshuBClass          '生年月日編集クラス
            // * 履歴番号 000023 2004/08/27 削除終了
            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // Dim csDainoKankeiCDMSTEntity As DataSet             '代納関係DataSet
            DataRow[] csDainoKankeiCDMSTEntity;             // 代納関係DataRow()
                                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）

            // * 履歴番号 000024 2005/01/25 削除開始（宮沢）
            // Dim strHenshuJusho As String                        '編集住所名
            // * 履歴番号 000024 2005/01/25 削除終了

            string strHenshuKanaMeisho;                   // 編集カナ名称
            string strHenshuKanjiShimei;                  // 編集漢字氏名
                                                          // *履歴番号 000008 2003/03/17 追加開始
                                                          // *履歴番号 000016 2003/08/22 削除開始
                                                          // Dim cURKanriJohoB As URKANRIJOHOBClass              '管理情報取得クラス
                                                          // *履歴番号 000016 2003/08/22 削除終了
            SofuJushoGyoseikuType cSofuJushoGyoseikuType;
            var strJushoHenshu3 = default(string);                       // 住所編集３
                                                                         // *履歴番号 000008 2003/03/17 追加終了
                                                                         // *履歴番号 000015 2003/04/30 追加開始
            var strJushoHenshu4 = default(string);                       // 住所編集４
                                                                         // *履歴番号 000015 2003/04/30 追加終了

            // *履歴番号 000021 2003/12/02 削除開始
            // '*履歴番号 000017 2003/10/09 追加開始
            // Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' 連絡先Ｂクラス
            // Dim csRenrakusakiEntity As DataSet                  ' 連絡先DataSet
            // Dim csRenrakusakiRow As DataRow                     ' 連絡先Row
            // '*履歴番号 000017 2003/10/09 追加終了
            // *履歴番号 000021 2003/12/02 削除終了

            // * 履歴番号 000026 2005/12/21 追加開始
            string strWork;
            // * 履歴番号 000026 2005/12/21 追加終了
            // *履歴番号 000042 2011/05/18 追加開始
            var strMeisho = new string[2];                          // 本名通称名優先制御用
                                                                    // *履歴番号 000042 2011/05/18 追加終了
            string strAtenaDataKB;
            string strAtenaDataSHU;


            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 'エラー処理クラスのインスタンス作成
                // '*履歴番号 000010  2003/03/27 修正開始
                // 'cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
                // cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // '*履歴番号 000010  2003/03/27 修正終了

                // *履歴番号 000013 2003/04/18 修正開始
                // 'カラム情報作成
                // csDataTable = Me.CreateAtena1Columns()
                // csAtena1 = New DataSet()
                // csAtena1.Tables.Add(csDataTable)

                // *履歴番号 000019 2003/11/19 修正開始
                // 'カラム情報作成
                // If (strGyomuMei = NENKIN) Then
                // csDataTable = Me.CreateNenkinAtenaColumns()
                // Else
                // csDataTable = Me.CreateAtena1Columns()
                // End If

                // *履歴番号 000036 2008/11/10 追加開始
                // 利用届出取得区分を変数にセット()
                m_strRiyoTdkdKB = cAtenaGetPara1.p_strTdkdKB;
                // *履歴番号 000036 2008/11/10 追加終了

                // *履歴番号 000040 2010/05/14 追加開始
                // 本籍筆頭者区分パラメータに変数をセット
                m_strHonsekiHittoshKB_Param = cAtenaGetPara1.p_strHonsekiHittoshKB;

                // 処理停止区分パラメータに変数をセット
                m_strShoriteishiKB_Param = cAtenaGetPara1.p_strShoriTeishiKB;
                // *履歴番号 000040 2010/05/14 追加終了

                // *履歴番号 000041 2011/05/18 追加開始
                // 外国人在留情報取得区分パラメータに変数をセット
                m_strFrnZairyuJohoKB_Param = cAtenaGetPara1.p_strFrnZairyuJohoKB;
                // *履歴番号 000041 2011/05/18 追加終了
                // *履歴番号 000046 2011/11/07 追加開始
                // 住基法改正区分を変数にセット
                m_strJukiHokaiseiKB_Param = cAtenaGetPara1.p_strJukiHokaiseiKB;
                // *履歴番号 000046 2011/11/07 追加終了
                // *履歴番号 000048 2014/04/28 追加開始
                // 共通番号取得区分を変数にセット
                m_strMyNumberKB_Param = cAtenaGetPara1.p_strMyNumberKB;
                // *履歴番号 000048 2014/04/28 追加終了

                // カラム情報作成
                switch (strGyomuMei ?? "")
                {
                    // *履歴番号 000027 2006/07/31 修正開始
                    case NENKIN:
                    case NENKIN_2:    // 年金宛名情報
                        {
                            // *履歴番号 000040 2010/05/14 追加開始
                            m_blnNenKin = true;
                            // *履歴番号 000040 2010/05/14 追加終了
                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnKobetsu = false;
                            m_strKobetsuShutokuKB = string.Empty;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateNenkinAtenaHyojunColumns(strGyomuMei);
                            }
                            else
                            {
                                csDataTable = CreateNenkinAtenaColumns(strGyomuMei);
                            }

                            break;
                        }
                    // Case NENKIN     ' 年金宛名情報
                    // csDataTable = Me.CreateNenkinAtenaColumns()
                    // *履歴番号 000027 2006/07/31 修正終了
                    case KOBETSU:    // 宛名個別情報
                        {
                            // *履歴番号 000034 2008/01/15 追加開始
                            // 個別事項取得区分をメンバ変数にセット
                            m_strKobetsuShutokuKB = cAtenaGetPara1.p_strKobetsuShutokuKB.Trim;
                            // *履歴番号 000034 2008/01/15 追加終了

                            // *履歴番号 000036 2008/11/10 追加開始
                            m_blnKobetsu = true;
                            // *履歴番号 000036 2008/11/10 追加終了
                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnNenKin = false;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1KobetsuHyojunColumns();
                            }
                            else
                            {
                                csDataTable = CreateAtena1KobetsuColumns();
                            }       // 宛名情報

                            break;
                        }

                    default:
                        {
                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnKobetsu = false;
                            m_blnNenKin = false;
                            m_strKobetsuShutokuKB = string.Empty;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1HyojunColumns();
                            }
                            else
                            {
                                csDataTable = CreateAtena1Columns();
                            }

                            break;
                        }
                }
                // *履歴番号 000019 2003/11/19 修正終了
                csAtena1 = new DataSet();
                csAtena1.Tables.Add(csDataTable);
                // *履歴番号 000013 2003/04/18 修正修正

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // '市町村情報のインスタンス作成
                // cuUSSCityInfo = New USSCityInfoClass()

                // '代納関係のインスタンス作成
                // cABDainoKankeiB = New ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)

                // '住民種別のインスタンス作成
                // cABJuminShubetsuB = New ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '法人名称のインスタンス作成
                // cABHojinMeishoB = New ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '個人法人のインスタンス作成
                // cABKjnhjnKBB = New ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '管内管外のインスタンス作成
                // cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '生年月日編集のインスタンス作成
                // cABUmareHenshuB = New ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // *履歴番号 000016 2003/08/22 削除開始
                // 管理情報取得Ｂのインスタンス作成
                // cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // *履歴番号 000016 2003/08/22 削除終了
                // *履歴番号 000008 2003/03/17 追加終了

                // *履歴番号 000021 2003/12/02 削除開始
                // '*履歴番号 000017 2003/10/09 追加開始
                // ' 連絡先Ｂクラスのインスタンス作成
                // cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // '*履歴番号 000017 2003/10/09 追加終了
                // *履歴番号 000021 2003/12/02 削除終了

                // *履歴番号 000007 2003/03/17 追加開始
                // パラメータのチェック
                CheckColumnValue(cAtenaGetPara1);
                // *履歴番号 000007 2003/03/17 追加終了

                // 住所編集１が"1"且つ住所編集２が"1"の場合
                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                // '直近の市町村名を取得する
                // cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
                // End If
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // 住所編集１が"1"且つ住所編集３が""の場合
                if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu3 == string.Empty)
                {
                    // *履歴番号 000016 2003/08/22 修正開始
                    // cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                    cSofuJushoGyoseikuType = GetSofuJushoGyoseikuType();
                    // *履歴番号 000016 2003/08/22 修正終了
                    switch (cSofuJushoGyoseikuType)
                    {
                        case var @case when @case == SofuJushoGyoseikuType.Jusho_Banchi:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case1 when case1 == SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "1";
                                break;
                            }
                        case var case2 when case2 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case3 when case3 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "1";
                                break;
                            }
                    }
                }
                else
                {
                    strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3;
                    strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4;
                }
                // *履歴番号 000008 2003/03/17 追加終了

                // 編集宛名データを作成する
                foreach (DataRow csDataRow in csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows)
                {
                    // *履歴番号 000013 2003/04/18 修正開始
                    // csDataNewRow = csAtena1.Tables(ABAtena1Entity.TABLE_NAME).NewRow

                    csDataNewRow = csDataTable.NewRow();
                    // *履歴番号 000013 2003/04/18 修正終了

                    // *履歴番号 000026 2005/12/21 追加開始
                    csDataNewRow.BeginEdit();
                    // *履歴番号 000026 2005/12/21 追加終了

                    // *履歴番号 000015 2003/04/30 追加開始
                    foreach (DataColumn csColumn in csDataNewRow.Table.Columns)
                        csDataNewRow[csColumn] = string.Empty;
                    // *履歴番号 000015 2003/04/30 追加終了

                    // *履歴番号 000021 2003/12/02 削除開始
                    // '*履歴番号 000017 2003/10/09 追加開始
                    // ' 業務コードが指定された場合
                    // If (strGyomuCD <> String.Empty) Then

                    // ' 連絡先データを取得する
                    // csRenrakusakiEntity = cRenrakusakiBClass.GetRenrakusakiBHoshu(CType(csDataRow(ABAtenaEntity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD)
                    // If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                    // csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                    // Else
                    // csRenrakusakiRow = Nothing
                    // End If
                    // Else
                    // csRenrakusakiRow = Nothing
                    // End If
                    // *履歴番号 000017 2003/10/09 追加終了
                    // *履歴番号 000021 2003/12/02 削除終了

                    // 住民コード
                    csDataNewRow(ABAtena1Entity.JUMINCD) = csDataRow(ABAtenaEntity.JUMINCD);

                    // 代納区分指定なしの場合
                    if (string.IsNullOrEmpty(strDainoKB))
                    {
                        // 代納区分
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "00";
                    }
                    else
                    {
                        // 代納区分
                        csDataNewRow(ABAtena1Entity.DAINOKB) = strDainoKB;
                    }

                    if (Conversions.ToString(csDataNewRow(ABAtena1Entity.DAINOKB)) == "00")
                    {

                        // 代納区分名称
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;

                        // 代納区分略式名称
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;
                    }
                    else
                    {

                        // 代納関係データを取得する

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                        // csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                        // ' ０件の場合、
                        // If csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count = 0 Then
                        // csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '代納区分名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '代納区分略式名称
                        // Else
                        // With csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0)

                        // ' 代納区分名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                        // ' 代納区分略式名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                        // End With
                        // End If
                        csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu2(Conversions.ToString(csDataNewRow(ABAtena1Entity.DAINOKB)));
                        if (csDainoKankeiCDMSTEntity.Length == 0)
                        {
                            csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;                   // 代納区分名称
                            csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;              // 代納区分略式名称
                        }
                        else
                        {

                            // 代納区分名称
                            csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = Conversions.ToString(csDainoKankeiCDMSTEntity[0].Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO));

                            // 代納区分略式名称
                            csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = Conversions.ToString(csDainoKankeiCDMSTEntity[0].Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI));
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                    }

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {

                        // 代納区分指定なしの場合
                        if (string.IsNullOrEmpty(strGyomuCD))
                        {

                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = "00";

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = string.Empty;
                        }
                        else
                        {
                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = strGyomuCD;

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = strGyomunaiSHU_CD;
                        }

                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 旧市町村コード
                    csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csDataRow(ABAtenaEntity.KYUSHICHOSONCD);

                    // 世帯コード
                    csDataNewRow(ABAtena1Entity.STAICD) = csDataRow(ABAtenaEntity.STAICD);

                    // 宛名データ区分
                    csDataNewRow(ABAtena1Entity.ATENADATAKB) = csDataRow(ABAtenaEntity.ATENADATAKB);

                    // 宛名データ種別
                    csDataNewRow(ABAtena1Entity.ATENADATASHU) = csDataRow(ABAtenaEntity.ATENADATASHU);

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 編集種別
                        m_cABJuminShubetsuB.GetJuminshubetsu(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU)));
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = m_cABJuminShubetsuB.p_strHenshuShubetsu;

                        // 編集種別略称
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = m_cABJuminShubetsuB.p_strHenshuShubetsuRyaku;
                        // 検索用カナ姓名
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = csDataRow(ABAtenaEntity.SEARCHKANASEIMEI);

                        // 検索用カナ姓
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = csDataRow(ABAtenaEntity.SEARCHKANASEI);

                        // 検索用カナ名
                        csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = csDataRow(ABAtenaEntity.SEARCHKANAMEI);

                        // 検索用漢字名称
                        csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO);
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // *履歴番号 000042 2011/05/18 追加開始
                    // 本名通称名切替対応 - カナ名称、漢字名称取得
                    switch (Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) ?? "")
                    {
                        case "11":
                        case "12":         // 住登内、住登外
                            {

                                if (m_strHonmyoTsushomeiYusenKB.Trim() == "1")
                                {
                                    // 管理情報：本名通称名優先制御 = "1" の場合
                                    strMeisho = MeishoHenshu(csDataRow);
                                }
                                else
                                {
                                    strMeisho[0] = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1));       // カナ名称１
                                    strMeisho[1] = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO1));
                                }      // 

                                break;
                            }
                        case "20":               // 法人
                            {
                                break;
                            }

                        case "30":               // 共有
                            {
                                strMeisho[0] = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1));
                                strMeisho[1] = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO1));
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                    // *履歴番号 000042 2011/05/18 追加終了

                    // 編集カナ名称
                    // 宛名区分="20"(法人)の場合
                    if (Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) == "20")
                    {
                        // * 履歴番号 000033 2007/07/17 修正開始
                        // カナ名称２（支店名）が無い場合はカナ名称１（法人名）とカナ名称２（支店名）の結合は行わない
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO2)).Trim ?? new char[0]) != "")
                        {
                            strHenshuKanaMeisho = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1)).TrimEnd + " " + Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO2)).TrimEnd;
                        }
                        else
                        {
                            strHenshuKanaMeisho = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1)).TrimEnd;
                        }
                        // strHenshuKanaMeisho = CType(csDataRow(ABAtenaEntity.KANAMEISHO1), String).TrimEnd + _
                        // " " + CType(csDataRow(ABAtenaEntity.KANAMEISHO2), String).TrimEnd
                        // * 履歴番号 000033 2007/07/17 修正終了
                        // * 履歴番号 000032 2007/07/09 修正開始
                        if (strHenshuKanaMeisho.RLength > 240)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.RSubstring(0, 240);
                        }
                        // If (strHenshuKanaMeisho.Length > 60) Then
                        // csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.Substring(0, 60)
                        // * 履歴番号 000032 2007/07/09 修正終了
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho;
                        }
                    }
                    else
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        strHenshuKanaMeisho = strMeisho[0];
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strHenshuKanaMeisho, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO);
                        // csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = csDataRow(ABAtenaEntity.KANAMEISHO1)
                        // *履歴番号 000042 2011/05/18 修正終了
                    }
                    // 編集カナ名称（フル）
                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = strHenshuKanaMeisho;
                    }
                    else
                    {
                    }

                    // 編集漢字名称
                    // 宛名区分="20"(法人)の場合
                    if (Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) == "20")
                    {
                        m_cABHojinMeishoB.p_strKeitaiFuyoKB = Conversions.ToString(csDataRow(ABAtenaEntity.HANYOKB1));
                        m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = Conversions.ToString(csDataRow(ABAtenaEntity.HANYOKB2));
                        m_cABHojinMeishoB.p_strKanjiHjnKeitai = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIHJNKEITAI));
                        m_cABHojinMeishoB.p_strKanjiMeisho1 = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO1));
                        m_cABHojinMeishoB.p_strKanjiMeisho2 = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO2));
                        strHenshuKanjiShimei = m_cABHojinMeishoB.GetHojinMeisho();
                        // * 履歴番号 000032 2007/07/09 修正開始
                        if (strHenshuKanjiShimei.RLength > 240)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.RSubstring(0, 240);
                        }
                        // If (strHenshuKanjiShimei.Length > 80) Then
                        // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.Substring(0, 80)
                        // * 履歴番号 000032 2007/07/09 修正終了
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei;
                        }
                    }
                    // * 履歴開始 000035 2008/02/15 修正開始
                    // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaEntity.KANJIMEISHO1)
                    else if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                        strHenshuKanjiShimei = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU)), strMeisho[1]);
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO);
                    }
                    // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.ATENADATAKB)), _
                    // CStr(csDataRow(ABAtenaEntity.ATENADATASHU)), _
                    // CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1)))
                    // *履歴番号 000042 2011/05/18 修正終了
                    else
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                        strHenshuKanjiShimei = strMeisho[1];
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO);
                        // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaEntity.KANJIMEISHO1)
                        // *履歴番号 000042 2011/05/18 修正終了
                        // * 履歴開始 000035 2008/02/15 修正終了
                    }
                    // 編集漢字名称（フル）
                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = strHenshuKanjiShimei;
                    }
                    else
                    {
                    }

                    if (csDataRow(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN)
                    {
                        if (csDataRow(ABAtenaEntity.UMAREYMD).ToString.Trim == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = m_strUmareYMDHenkanParam;
                            if (csDataRow(ABAtenaEntity.ATENADATASHU).ToString.RSubstring(0, 1) == "1")
                            {
                                csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdHenkan;
                            }
                            else
                            {
                                csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdhenkanSeireki;
                            }
                        }
                        else if (CheckDate(csDataRow(ABAtenaEntity.UMAREYMD).ToString))
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaEntity.UMAREYMD);
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaEntity.UMAREWMD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = GetSeirekiLastDay(csDataRow(ABAtenaEntity.UMAREYMD).ToString);
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = GetWarekiLastDay(csDataRow(ABAtenaEntity.UMAREWMD).ToString, csDataRow(ABAtenaEntity.UMAREYMD).ToString);
                        }
                    }
                    else
                    {
                        // 生年月日
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaEntity.UMAREYMD);

                        // 生年月日編集
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaEntity.UMAREWMD);
                    }

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        m_cABUmareHenshuB.p_strDataKB = Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB));
                        m_cABUmareHenshuB.p_strJuminSHU = Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU));
                        m_cABUmareHenshuB.p_strUmareYMD = Conversions.ToString(csDataNewRow(ABAtena1Entity.UMAREYMD));
                        m_cABUmareHenshuB.p_strUmareWMD = Conversions.ToString(csDataNewRow(ABAtena1Entity.UMAREWMD));
                        m_cABUmareHenshuB.HenshuUmare();
                        // 生表示年月日
                        csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = m_cABUmareHenshuB.p_strHyojiUmareYMD;

                        // 生証明年月日
                        csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = m_cABUmareHenshuB.p_strShomeiUmareYMD;

                        // 性別コード
                        csDataNewRow(ABAtena1Entity.SEIBETSUCD) = csDataRow(ABAtenaEntity.SEIBETSUCD);

                        // 性別
                        strWork = Conversions.ToString(csDataRow(ABAtenaEntity.SEIBETSU)).Trim;
                        csDataNewRow(ABAtena1Entity.SEIBETSU) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_SEIBETSU);
                        // 性別（フル）
                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.SEIBETSU_FULL) = csDataRow(ABAtenaEntity.SEIBETSU);
                        }
                        else
                        {
                        }

                        // 編集続柄コード
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(ABAtenaEntity.DAI2ZOKUGARACD, String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2ZOKUGARACD)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaEntity.ZOKUGARACD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaEntity.DAI2ZOKUGARACD);
                        }

                        // 編集続柄
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(ABAtenaEntity.DAI2ZOKUGARA, String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2ZOKUGARA)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaEntity.ZOKUGARA);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaEntity.DAI2ZOKUGARA);
                        }

                        // * 履歴開始 000035 2008/02/15 修正開始
                        // 法人代表者名
                        // csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
                        if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                            csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU)), Conversions.ToString(csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)));
                        }
                        else
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                            csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI);
                        }
                        // * 履歴開始 000035 2008/02/15 修正終了
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 個人法人区分
                    csDataNewRow(ABAtena1Entity.KJNHJNKB) = csDataRow(ABAtenaEntity.KJNHJNKB);

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 個人法人区分名称
                        csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = m_cABKjnhjnKBB.GetKjnhjn(Conversions.ToString(csDataRow(ABAtenaEntity.KJNHJNKB)));

                        // 管内管外区分名称
                        csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(Conversions.ToString(csDataRow(ABAtenaEntity.KANNAIKANGAIKB)));
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 管内管外区分
                    csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csDataRow(ABAtenaEntity.KANNAIKANGAIKB);

                    // 住基優先の場合
                    if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                    {

                        // 郵便番号
                        csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaEntity.JUKIYUBINNO);

                        // 住所コード
                        csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaEntity.JUKIJUSHOCD);

                        // 住所
                        csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaEntity.JUKIJUSHO);

                        // 編集住所名
                        if (cAtenaGetPara1.p_strJushoHenshu1 == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }

                        else if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // * 履歴番号 000024 2005/01/25 更新開始
                            // strHenshuJusho = String.Empty
                            m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength);
                            // * 履歴番号 000024 2005/01/25 更新終了

                            if (cAtenaGetPara1.p_strJushoHenshu2 == "1")
                            {

                                // 市町村名を頭に付加する（管内のみ）
                                if (Conversions.ToString(csDataRow(ABAtenaEntity.KANNAIKANGAIKB)) == "1")
                                {
                                    // * 履歴番号 000024 2005/01/25 更新開始
                                    // strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                    m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0));
                                    // * 履歴番号 000024 2005/01/25 更新終了
                                }


                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // Select Case cAtenaGetPara1.p_strJushoHenshu3
                            switch (strJushoHenshu3 ?? "")
                            {
                                // *履歴番号 000008 2003/03/17 修正終了
                                // * 履歴番号 000028 2007/01/15 修正開始
                                case "1":
                                case "6":   // 住所＋番地
                                    {
                                        // Case "1"    '住所＋番地
                                        // * 履歴番号 000028 2007/01/15 修正終了
                                        // * 履歴番号 000024 2005/01/25 更新開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        break;
                                    }
                                // * 履歴番号 000024 2005/01/25 更新終了
                                case "2":    // 行政区＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                        // 行政区名がない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).Trim ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了
                                        else
                                        {
                                            // 行政区＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 修正終了
                                case "3":    // 住所＋（行政区）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了
                                        }

                                        break;
                                    }
                                // *履歴番号 000004  2003/02/25 修正終了
                                case "4":    // 行政区＋（住所）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd

                                        // 住所が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了
                                        // *履歴番号 000009 2003/03/17 追加開始
                                        // 行政区が存在しない場合
                                        else if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了
                                        // *履歴番号 000009 2003/03/17 追加終了
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 追加開始
                                case "5":    // 行政区＋△＋番地
                                    {
                                        // 行政区名がない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).Trim ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了
                                        else
                                        {
                                            // 行政区＋番地
                                            // 
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "　" _
                                            // + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("　");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIBANCHI)).TrimEnd);
                                        }

                                        break;
                                    }
                                    // *履歴番号 000009 2003/03/17 修正終了
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                            // * 履歴番号 000028 2007/01/15 修正開始
                            if (strJushoHenshu4 == "1" && new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIKATAGAKI)).Trim ?? new char[0]) != "")
                            {
                                // If strJushoHenshu4 = "1" Then
                                // * 履歴番号 000028 2007/01/15 修正終了
                                // *履歴番号 000008 2003/03/17 修正終了
                                // *履歴番号 000004  2003/02/25 修正開始
                                // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).TrimEnd

                                // * 履歴番号 000024 2005/01/25 更新開始
                                // strHenshuJusho += "　" + CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).TrimEnd
                                m_strHenshuJusho.Append("　");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIKATAGAKI)).TrimEnd);
                                // * 履歴番号 000024 2005/01/25 更新終了
                                // *履歴番号 000004  2003/02/25 修正終了
                            }
                            // * 履歴番号 000028 2007/01/15 追加開始
                            // 住所編集３パラメータが６、且つ行政区名があるときは、編集住所に（行政区）を追加する
                            if (strJushoHenshu3 == "6" && new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).Trim ?? new char[0]) != "")
                            {
                                m_strHenshuJusho.Append("（");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                m_strHenshuJusho.Append("）");
                            }
                            // * 履歴番号 000028 2007/01/15 追加終了
                            // * 履歴番号 000024 2005/01/25 更新開始
                            // If strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            // Else
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                            // End If
                            // * 履歴番号 000032 2007/07/09 修正開始
                            if (m_strHenshuJusho.RLength >= 160)
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160);
                            }
                            // If m_strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString.Substring(0, 80)
                            // * 履歴番号 000032 2007/07/09 修正終了
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString();
                            }
                            // * 履歴番号 000024 2005/01/25 更新終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString();
                            }
                            else
                            {
                            }
                        }

                        // 番地コード１
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaEntity.JUKIBANCHICD1);

                        // 番地コード２
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaEntity.JUKIBANCHICD2);

                        // 番地コード３
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaEntity.JUKIBANCHICD3);

                        // 番地
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {

                            // 住所編集ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.BANCHI) = string.Empty;
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI);
                        }

                        // 方書フラグ
                        csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaEntity.JUKIKATAGAKIFG);

                        // 方書コード
                        csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaEntity.JUKIKATAGAKICD);

                        // 方書
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu4 == "1")
                        {
                            // 方書付加ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.JUKIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI);
                            }
                            else
                            {
                            }
                        }

                        // *履歴番号 000017 2003/10/09 修正開始
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)

                        // *履歴番号 000021 2003/12/02 修正開始
                        // ' 連絡先マスタが存在する場合は、連絡先マスタの連絡先を設定する
                        // If (csRenrakusakiRow Is Nothing) Then
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                        // Else
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                        // '連絡先取得業務コード
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                        // End If
                        // '*履歴番号 000017 2003/10/09 修正終了

                        // 連絡先１
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1);
                        // 連絡先２
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2);
                        // *履歴番号 000021 2003/12/02 修正終了

                        // 行政区コード
                        csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaEntity.JUKIGYOSEIKUCD);

                        // 行政区名
                        csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI);

                        // 地区コード１
                        csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaEntity.JUKICHIKUCD1);

                        // 地区１
                        csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaEntity.JUKICHIKUMEI1);

                        // 地区コード２
                        csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaEntity.JUKICHIKUCD2);

                        // 地区２
                        csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaEntity.JUKICHIKUMEI2);

                        // 地区コード３
                        csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaEntity.JUKICHIKUCD3);

                        // 地区３
                        csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaEntity.JUKICHIKUMEI3);

                        // 表示順（第２住民票表示順がある場合は、第２住民票表示順）
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN), String) = String.Empty Then
                        // * 履歴番号 000024 2005/01/25 更新開始 IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            if (Conversions.ToString(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN)).Trim == "00")
                            {
                                // *履歴番号 000002 2003/02/20 修正終了
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN);
                            }
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN);
                            }
                        }
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了 IF文で囲む
                    else
                    {

                        // 郵便番号
                        csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaEntity.YUBINNO);

                        // 住所コード
                        csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaEntity.JUSHOCD);

                        // 住所
                        csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaEntity.JUSHO);

                        // 編集住所名
                        if (cAtenaGetPara1.p_strJushoHenshu1 == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }

                        else if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // strHenshuJusho = String.Empty
                            m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength);
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (cAtenaGetPara1.p_strJushoHenshu2 == "1")
                            {

                                // 管内のみ市町村名を付加する
                                if (Conversions.ToString(csDataRow(ABAtenaEntity.KANNAIKANGAIKB)) == "1")
                                {
                                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                    // strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                    m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0));
                                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                }
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // Select Case cAtenaGetPara1.p_strJushoHenshu3
                            switch (strJushoHenshu3 ?? "")
                            {
                                // *履歴番号 000008 2003/03/17 修正終了
                                // * 履歴番号 000028 2007/01/15 修正開始
                                case "1":
                                case "6":   // 住所＋番地
                                    {
                                        // Case "1"    '住所＋番地
                                        // * 履歴番号 000028 2007/01/15 修正終了
                                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        break;
                                    }
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                case "2":    // 行政区＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // 行政区＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 修正終了
                                case "3":    // 住所＋（行政区）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000004  2003/02/25 修正終了

                                case "4":    // 行政区＋（住所）＋番地
                                    {
                                        // *履歴番号 000004 2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd

                                        // 住所が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加開始
                                        else if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加終了
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000004 2003/02/25 修正終了
                                // *履歴番号 000009 2003/03/17 追加開始
                                case "5":    // 行政区＋△＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // 行政区＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "　" _
                                            // + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("　");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                    // *履歴番号 000009 2003/03/17 追加終了
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                            // * 履歴番号 000028 2007/01/15 修正開始
                            if (strJushoHenshu4 == "1" && new string(Conversions.ToString(csDataRow(ABAtenaEntity.KATAGAKI)).Trim ?? new char[0]) != "")
                            {
                                // If strJushoHenshu4 = "1" Then
                                // * 履歴番号 000028 2007/01/15 修正終了
                                // *履歴番号 000008 2003/03/17 修正終了
                                // *履歴番号 000004  2003/02/25 修正開始
                                // strHenshuJusho += CType(csDataRow(ABAtenaEntity.KATAGAKI), String).TrimEnd

                                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                // strHenshuJusho += "　" + CType(csDataRow(ABAtenaEntity.KATAGAKI), String).TrimEnd
                                m_strHenshuJusho.Append("　");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.KATAGAKI)).TrimEnd);
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                // *履歴番号 000004  2003/02/25 修正終了
                            }
                            // * 履歴番号 000028 2007/01/15 追加開始
                            // 住所編集３パラメータが６、且つ行政区名があるときは、編集住所に（行政区）を追加する
                            if (strJushoHenshu3 == "6" && new string(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).Trim ?? new char[0]) != "")
                            {
                                m_strHenshuJusho.Append("（");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                m_strHenshuJusho.Append("）");
                            }
                            // * 履歴番号 000028 2007/01/15 追加終了
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // If strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            // Else
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                            // End If
                            // * 履歴番号 000032 2007/07/09 修正開始
                            if (m_strHenshuJusho.RLength >= 160)
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160);
                            }
                            // If m_strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            // * 履歴番号 000032 2007/07/09 修正終了
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString();
                            }
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString();
                            }
                            else
                            {
                            }
                        }

                        // 番地コード１
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaEntity.BANCHICD1);

                        // 番地コード２
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaEntity.BANCHICD2);

                        // 番地コード３
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaEntity.BANCHICD3);

                        // 番地
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {

                            // 住所編集ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.BANCHI) = "";
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaEntity.BANCHI);
                        }

                        // 方書フラグ
                        csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaEntity.KATAGAKIFG);

                        // 方書コード
                        csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaEntity.KATAGAKICD);

                        // 方書
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu4 == "1")
                        {

                            // 方書付加ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = "";
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.KATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI);
                            }
                            else
                            {
                            }
                        }

                        // *履歴番号 000017 2003/10/09 修正開始
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)

                        // *履歴番号 000021 2003/12/02 修正開始
                        // ' 連絡先マスタが存在する場合は、連絡先マスタの連絡先を設定する
                        // If (csRenrakusakiRow Is Nothing) Then
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                        // Else
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                        // '連絡先取得業務コード
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                        // End If
                        // '*履歴番号 000017 2003/10/09 修正終了

                        // 連絡先１
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1);
                        // 連絡先２
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2);
                        // *履歴番号 000021 2003/12/02 修正終了

                        // 行政区コード
                        csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaEntity.GYOSEIKUCD);

                        // 行政区名
                        csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaEntity.GYOSEIKUMEI);

                        // 地区コード１
                        csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaEntity.CHIKUCD1);

                        // 地区１
                        csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaEntity.CHIKUMEI1);

                        // 地区コード２
                        csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaEntity.CHIKUCD2);

                        // 地区２
                        csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaEntity.CHIKUMEI2);

                        // 地区コード３
                        csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaEntity.CHIKUCD3);

                        // 地区３
                        csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaEntity.CHIKUMEI3);

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            // * 履歴番号 000026 2005/12/21 修正開始
                            // '表示順
                            // csDataNewRow(ABAtena1Entity.HYOJIJUN) = String.Empty

                            // 表示順（第２住民票表示順がある場合は、第２住民票表示順）
                            if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                            {
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN)).Trim;
                                if (strWork == "00")
                                {
                                    strWork = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN).ToString().Trim;
                                }
                                if (string.IsNullOrEmpty(strWork))
                                {
                                    strWork = "99";
                                }
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = strWork;
                            }
                            // * 履歴番号 000026 2005/12/21 修正終了
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    }
                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {

                        // 登録異動年月日 
                        csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csDataRow(ABAtenaEntity.TOROKUIDOYMD);

                        // 登録事由コード
                        csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csDataRow(ABAtenaEntity.TOROKUJIYUCD);

                        // 登録事由
                        csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csDataRow(ABAtenaEntity.TOROKUJIYU);

                        if ((csDataRow(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || csDataRow(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) && !(csDataRow(ABAtenaEntity.SHOJOJIYUCD).ToString.Trim == string.Empty))
                        {
                            if (csDataRow(ABAtenaEntity.SHOJOIDOYMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = m_strShojoIdobiHenkanParam;
                            }
                            else
                            {
                                csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaEntity.SHOJOIDOYMD);
                            }
                        }
                        else
                        {
                            // 消除異動年月日
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaEntity.SHOJOIDOYMD);
                        }

                        // 消除事由コード
                        csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csDataRow(ABAtenaEntity.SHOJOJIYUCD);

                        // 消除事由名称
                        csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csDataRow(ABAtenaEntity.SHOJOJIYU);

                        // 編集世帯主住民コード
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD), String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaEntity.STAINUSJUMINCD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD);
                        }
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 編集カナ世帯主名
                    // *履歴番号 000002 2003/02/20 修正開始
                    // If CType(csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI), String) = String.Empty Then
                    if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI)).Trim ?? new char[0]) == "")
                    {
                        // *履歴番号 000002 2003/02/20 修正終了
                        csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaEntity.KANASTAINUSMEI);
                    }
                    else
                    {
                        csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI);
                    }

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 編集漢字世帯主名
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaEntity.DAI2STAINUSMEI), String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2STAINUSMEI)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.STAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.STAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.STAINUSMEI);
                            }
                        }
                        // * 履歴開始 000035 2008/02/15 修正終了
                        // * 履歴開始 000035 2008/02/15 修正開始
                        // csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.DAI2STAINUSMEI)
                        else if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2STAINUSMEI)));
                        }
                        else
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.DAI2STAINUSMEI);
                            // * 履歴開始 000035 2008/02/15 修正終了
                        }

                        // *履歴番号 000012 2003/04/18 追加開始
                        // 続柄コード
                        csDataNewRow(ABAtena1Entity.ZOKUGARACD) = csDataRow(ABAtenaEntity.ZOKUGARACD);
                        // 続柄
                        csDataNewRow(ABAtena1Entity.ZOKUGARA) = csDataRow(ABAtenaEntity.ZOKUGARA);

                        // *履歴番号 000014 2003/04/30 修正開始
                        // ' カナ名称２
                        // csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaEntity.KANAMEISHO2)
                        // ' 漢字名称２
                        // csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2)

                        // 宛名区分≠"20"(法人)の場合
                        if (!(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) == "20"))
                        {
                            // カナ名称２
                            csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaEntity.KANAMEISHO2);
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 漢字名称２
                            // csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU)), Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO2)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                        }
                        // *履歴番号 000014 2003/04/30 修正終了

                        // 籍番号
                        csDataNewRow(ABAtena1Entity.SEKINO) = csDataRow(ABAtenaEntity.SEKINO);
                        // *履歴番号 000012 2003/04/18 追加終了
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // *履歴番号 000040 2010/05/14 追加開始
                    // 本籍筆頭者情報出力判定
                    if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                    {
                        // パラメータ:本籍筆頭者取得区分が"1"かつ、管理情報:本籍取得区分(10･18)が"1"の場合のみセット
                        // 本籍住所
                        csDataNewRow(ABAtena1Entity.HON_JUSHO) = csDataRow(ABAtenaEntity.HON_JUSHO);
                        // 本籍番地
                        csDataNewRow(ABAtena1Entity.HONSEKIBANCHI) = csDataRow(ABAtenaEntity.HONSEKIBANCHI);
                        // 筆頭者
                        csDataNewRow(ABAtena1Entity.HITTOSH) = csDataRow(ABAtenaEntity.HITTOSH);
                    }
                    else
                    {
                    }

                    // 処理停止区分出力判定
                    if (m_strShoriteishiKB_Param == "1" && m_strShoriteishiKB == "1")
                    {
                        // パラメータ:処理停止区分取得区分が"1"かつ、管理情報:処理停止区分取得区分(10･19)が"1"の場合のみセット
                        // 処理停止区分
                        csDataNewRow(ABAtena1Entity.SHORITEISHIKB) = csDataRow(ABAtenaEntity.SHORITEISHIKB);
                    }
                    else
                    {
                    }
                    // *履歴番号 000040 2010/05/14 追加終了

                    // *履歴番号 000041 2011/05/18 追加開始
                    if (m_strFrnZairyuJohoKB_Param == "1")
                    {
                        // パラメータ：外国人在留資格取得区分が"1"の場合
                        // 国籍
                        strWork = Conversions.ToString(csDataRow(ABAtenaEntity.KOKUSEKI)).Trim;
                        csDataNewRow(ABAtena1Entity.KOKUSEKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KOKUSEKI);
                        // 国籍（フル）
                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.KOKUSEKI_FULL) = csDataRow(ABAtenaEntity.KOKUSEKI);
                        }
                        else
                        {
                        }
                        // 在留資格コード
                        csDataNewRow(ABAtena1Entity.ZAIRYUSKAKCD) = csDataRow(ABAtenaEntity.ZAIRYUSKAKCD);
                        // 在留資格
                        csDataNewRow(ABAtena1Entity.ZAIRYUSKAK) = csDataRow(ABAtenaEntity.ZAIRYUSKAK);
                        // 在留期間
                        csDataNewRow(ABAtena1Entity.ZAIRYUKIKAN) = csDataRow(ABAtenaEntity.ZAIRYUKIKAN);
                        // 在留開始年月日
                        csDataNewRow(ABAtena1Entity.ZAIRYU_ST_YMD) = csDataRow(ABAtenaEntity.ZAIRYU_ST_YMD);
                        // 在留終了年月日
                        csDataNewRow(ABAtena1Entity.ZAIRYU_ED_YMD) = csDataRow(ABAtenaEntity.ZAIRYU_ED_YMD);
                    }
                    else
                    {
                    }
                    // *履歴番号 000041 2011/05/18 追加終了

                    // *履歴番号 000013 2003/04/18 修正開始
                    // 'データレコードの追加
                    // csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 年金用データ作成
                        // *履歴番号 000027 2006/07/31 修正開始
                        if ((strGyomuMei ?? "") == NENKIN | (strGyomuMei ?? "") == NENKIN_2)
                        {
                            // If (strGyomuMei = NENKIN) Then
                            // *履歴番号 000027 2006/07/31 修正終了

                            // 旧姓
                            csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = csDataRow(ABAtenaEntity.KYUSEI);
                            // 住定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = csDataRow(ABAtenaEntity.JUTEIIDOYMD);
                            // 住定事由
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = csDataRow(ABAtenaEntity.JUTEIJIYU);
                            // 転入前住所郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaEntity.TENUMAEJ_YUBINNO);
                            // *履歴番号 000017 2003/10/09 追加開始
                            // 転入前住所全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD);
                            // *履歴番号 000017 2003/10/09 追加終了
                            // 転入前住所住所
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaEntity.TENUMAEJ_JUSHO);
                            // 転入前住所番地
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaEntity.TENUMAEJ_BANCHI);
                            // 転入前住所方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出予定郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO);
                            // 転出予定全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD);
                            // 転出予定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD);
                            // 転出予定住所
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO);
                            // 転出予定番地
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI);
                            // 転出予定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出確定郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO);
                            // *履歴番号 000017 2003/10/09 追加開始
                            // 転出確定全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD);
                            // *履歴番号 000017 2003/10/09 追加終了
                            // 転出確定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD);
                            // 転出確定通知年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD);
                            // 転出確定住所
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO);
                            // 転出確定番地
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI);
                            // 転出確定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);

                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転入前住所方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI);
                                // 転出予定方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI);
                                // 転出確定方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI);
                            }
                            else
                            {
                            }

                            // 住基優先の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                            {
                                // 編集前番地
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.JUKIKATAGAKI)).Trim;
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                // 編集前番地
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.BANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.KATAGAKI)).Trim;
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI);
                                }
                                else
                                {
                                }
                            }

                            // 消除届出年月日
                            csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = csDataRow(ABAtenaEntity.SHOJOTDKDYMD);
                            // 直近事由コード
                            csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = csDataRow(ABAtenaEntity.CKINJIYUCD);

                            // *履歴番号 000022 2003/12/04 追加開始
                            // 本籍全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = csDataRow(ABAtenaEntity.HON_ZJUSHOCD);
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出予定世帯主名
                            // csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                            // 転出確定世帯主名
                            // csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                // 転出予定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)));
                                // 転出確定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                // 転出予定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI);
                                // 転出確定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                            // 国籍コード
                            csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD);
                            // *履歴番号 000022 2003/12/04 追加終了
                            // *履歴番号 000027 2006/07/31 追加開始
                            if ((strGyomuMei ?? "") == NENKIN_2)
                            {
                                // * 履歴開始 000035 2008/02/15 修正開始
                                // 転入前住所世帯主名
                                // csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI)
                                if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                                {
                                    // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                    csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI)));
                                }
                                else
                                {
                                    // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                    csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI);
                                }
                                // *履歴番号 000022 2003/12/04 追加終了
                            }
                            // *履歴番号 000027 2006/07/31 追加終了
                        }

                        // *履歴番号 000030 2007/04/28 追加開始
                        // 介護用サブルーチン取得項目
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                        {
                            // 旧姓
                            csDataNewRow(ABAtena1Entity.KYUSEI) = csDataRow(ABAtenaEntity.KYUSEI);
                            // 住定異動年月日
                            csDataNewRow(ABAtena1Entity.JUTEIIDOYMD) = csDataRow(ABAtenaEntity.JUTEIIDOYMD);
                            // 住定事由
                            csDataNewRow(ABAtena1Entity.JUTEIJIYU) = csDataRow(ABAtenaEntity.JUTEIJIYU);
                            // 本籍全国住所コード
                            csDataNewRow(ABAtena1Entity.HON_ZJUSHOCD) = csDataRow(ABAtenaEntity.HON_ZJUSHOCD);
                            // 転入前住所郵便番号
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaEntity.TENUMAEJ_YUBINNO);
                            // 転入前住所全国住所コード
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD);
                            // 転入前住所住所
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaEntity.TENUMAEJ_JUSHO);
                            // 転入前住所番地
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaEntity.TENUMAEJ_BANCHI);
                            // 転入前住所方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出予定郵便番号
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO);
                            // 転出予定全国住所コード
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD);
                            // 転出予定異動年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD);
                            // 転出予定住所
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO);
                            // 転出予定番地
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI);
                            // 転出予定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転入前住所方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI);
                                // 転出予定方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI);
                            }
                            else
                            {
                            }
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出予定世帯主名
                            // csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                            // 転出確定郵便番号
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO);
                            // 転出確定全国住所コード
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD);
                            // 転出確定異動年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD);
                            // 転出確定通知年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD);
                            // 転出確定住所
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO);
                            // 転出確定番地
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI);
                            // 転出確定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転出確定方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI);
                            }
                            else
                            {
                            }
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出確定世帯主名
                            // csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了

                            // 住基優先の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                            {
                                // 編集前番地
                                csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.JUKIKATAGAKI)).Trim;
                                csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                // 編集前番地
                                csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.BANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.KATAGAKI)).Trim;
                                csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI);
                                }
                                else
                                {
                                }
                            }

                            // 消除届出年月日
                            csDataNewRow(ABAtena1Entity.SHOJOTDKDYMD) = csDataRow(ABAtenaEntity.SHOJOTDKDYMD);
                            // 直近事由コード
                            csDataNewRow(ABAtena1Entity.CKINJIYUCD) = csDataRow(ABAtenaEntity.CKINJIYUCD);
                            // 国籍コード
                            csDataNewRow(ABAtena1Entity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD);
                            // 登録届出年月日
                            csDataNewRow(ABAtena1Entity.TOROKUTDKDYMD) = csDataRow(ABAtenaEntity.TOROKUTDKDYMD);
                            // 住定届出年月日
                            csDataNewRow(ABAtena1Entity.JUTEITDKDYMD) = csDataRow(ABAtenaEntity.JUTEITDKDYMD);
                            // 転出入理由
                            csDataNewRow(ABAtena1Entity.TENSHUTSUNYURIYU) = csDataRow(ABAtenaEntity.TENSHUTSUNYURIYU);
                            // 市町村コード
                            csDataNewRow(ABAtena1Entity.SHICHOSONCD) = csDataRow(ABAtenaEntity.SHICHOSONCD);

                            if (!(csDataRow(ABAtenaEntity.CKINJIYUCD).ToString.Trim == string.Empty) && csDataRow(ABAtenaEntity.CKINIDOYMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1Entity.CKINIDOYMD) = m_strCknIdobiHenkanParam;
                            }
                            else
                            {

                                // 直近異動年月日
                                csDataNewRow(ABAtena1Entity.CKINIDOYMD) = csDataRow(ABAtenaEntity.CKINIDOYMD);
                            }
                            // 更新日時
                            csDataNewRow(ABAtena1Entity.KOSHINNICHIJI) = csDataRow(ABAtenaEntity.KOSHINNICHIJI);
                        }
                        // *履歴番号 000030 2007/04/28 追加終了

                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // *履歴番号 000019 2003/11/19 追加開始
                    // 宛名個別情報用データ作成(本人レコードのみ設定)
                    if ((strGyomuMei ?? "") == KOBETSU & string.IsNullOrEmpty(strDainoKB.Trim()))
                    {
                        // 基礎年金番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KSNENKNNO) = csDataRow(ABAtena1KobetsuEntity.KSNENKNNO);
                        // 年金資格取得年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD);
                        // 年金資格取得種別	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU);
                        // 年金資格取得理由コード	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD);
                        // 年金資格喪失年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD);
                        // 年金資格喪失理由コード	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD);
                        // 受給年金記号１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1);
                        // 受給年金番号１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO1);
                        // 受給年金種別１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU1);
                        // 受給年金枝番１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1);
                        // 受給年金区分１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB1);
                        // 受給年金記号２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2);
                        // 受給年金番号２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO2);
                        // 受給年金種別２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU2);
                        // 受給年金枝番２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2);
                        // 受給年金区分２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB2);
                        // 受給年金記号３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3);
                        // 受給年金番号３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO3);
                        // 受給年金種別３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU3);
                        // 受給年金枝番３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3);
                        // 受給年金区分３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB3);
                        // 国保番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHONO);
                        // 国保資格区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB);
                        // 国保資格区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO);
                        // 国保資格区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO);
                        // 国保学遠区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB);
                        // 国保学遠区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO);
                        // 国保学遠区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO);
                        // 国保取得年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD);
                        // 国保喪失年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD);
                        // 国保退職区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB);
                        // 国保退職区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO);
                        // 国保退職区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO);
                        // 国保退職本被区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB);
                        // 国保退職本被区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO);
                        // 国保退職本被区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
                        // 国保退職該当年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD);
                        // 国保退職非該当年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD);
                        // 国保保険証記号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO);
                        // 国保保険証番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO);
                        // 印鑑番号	
                        csDataNewRow(ABAtena1KobetsuEntity.INKANNO) = csDataRow(ABAtena1KobetsuEntity.INKANNO);
                        // 印鑑登録区分	
                        csDataNewRow(ABAtena1KobetsuEntity.INKANTOROKUKB) = csDataRow(ABAtena1KobetsuEntity.INKANTOROKUKB);
                        // 選挙資格区分	
                        csDataNewRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB);
                        // 児手被用区分	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB);
                        // 児手開始年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATESTYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATESTYM);
                        // 児手終了年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM);
                        // 介護被保険者番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO);
                        // 介護資格取得日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD);
                        // 介護資格喪失日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD);
                        // 介護資格被保険者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB);
                        // 介護住所地特例者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB);
                        // 介護受給者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB);
                        // 要介護状態区分コード	
                        csDataNewRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD) = csDataRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD);
                        // 要介護状態区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKKB);
                        // 介護認定有効開始日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD);
                        // 介護認定有効終了日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD);
                        // 介護受給認定年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD);
                        // 介護受給認定取消年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD);

                        // *履歴番号 000034 2008/01/15 追加開始
                        if (m_strKobetsuShutokuKB == "1")
                        {
                            // 個別事項取得区分が"1"の場合は後期高齢項目を追加する
                            // 資格区分
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB);
                            // 被保険者番号
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO);
                            // 被保険者資格取得事由コード
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD);
                            // 被保険者資格取得事由名称
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI);
                            // 被保険者資格取得年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD);
                            // 被保険者資格喪失事由コード
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD);
                            // 被保険者資格喪失事由名称
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI);
                            // 被保険者資格喪失年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD);
                            // 保険者番号適用開始年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD);
                            // 保険者番号適用終了年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD);
                        }
                        else
                        {
                            // 個別事項取得区分が値なしの場合は後期高齢項目を追加しない
                        }
                        // *履歴番号 000034 2008/01/15 追加終了

                    }
                    // *履歴番号 000019 2003/11/19 追加終了

                    // *履歴番号 000046 2011/11/07 追加開始
                    // 住基法改正判定
                    if (m_strJukiHokaiseiKB_Param == "1")
                    {
                        // 住民票状態区分
                        csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csDataRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN);
                        // 住居地届出有無フラグ
                        csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csDataRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG);
                        // 本国名
                        csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csDataRow(ABAtenaFZYEntity.HONGOKUMEI);
                        // カナ本国名
                        csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csDataRow(ABAtenaFZYEntity.KANAHONGOKUMEI);
                        // 併記名
                        csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANJIHEIKIMEI);
                        // カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANAHEIKIMEI);
                        // 通称名
                        csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANJITSUSHOMEI);
                        // カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANATSUSHOMEI);
                        // カタカナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI);
                        // 生年月日不詳区分
                        csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csDataRow(ABAtenaFZYEntity.UMAREFUSHOKBN);
                        // 通称名登録（変更）年月日
                        csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csDataRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD);
                        // 在留期間コード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANCD);
                        // 在留期間名称
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO);
                        // 中長期在留者である旨等のコード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHACD);
                        // 中長期在留者である旨等
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO);
                        // 在留カード等番号
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csDataRow(ABAtenaFZYEntity.ZAIRYUCARDNO);
                        // 特別永住者証明書交付年月日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csDataRow(ABAtenaFZYEntity.KOFUYMD);
                        // 特別永住者証明書交付予定期間開始日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEISTYMD);
                        // 特定永住者証明書交付予定期間終了日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD);
                        // 住基対象者（第30条45非該当）消除異動年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
                        // 住基対象者（第30条45非該当）消除事由コード
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
                        // 住基対象者（第30条45非該当）消除事由
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU);
                        // 住基対象者（第30条45非該当）消除届出年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
                        // 住基対象者（第30条45非該当）消除届出通知区分
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
                        // 外国人世帯主名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSMEI);
                        // 外国人世帯主カナ名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI);
                        // 世帯主併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSHEIKIMEI);
                        // 世帯主カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI);
                        // 世帯主通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI);
                        // 世帯主カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000046 2011/11/07 追加終了

                    // *履歴番号 000048 2014/04/28 追加開始
                    // 共通番号判定
                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                    {
                        // 空白除去した値を設定する。
                        csDataNewRow(ABMyNumberEntity.MYNUMBER) = csDataRow(ABMyNumberEntity.MYNUMBER).ToString.Trim;
                    }
                    else
                    {
                        // noop
                    }
                    // *履歴番号 000048 2014/04/28 追加終了

                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        // 世帯主氏名優先区分
                        csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csDataRow(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
                        // 氏名優先項目
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csDataRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB);
                        // 旧氏
                        csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = csDataRow(ABAtenaFZYEntity.RESERVE7);
                        // カナ旧氏
                        csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = csDataRow(ABAtenaFZYEntity.RESERVE8);
                        // 氏名フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = csDataRow(ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG);
                        // 旧氏フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = csDataRow(ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG);
                        // 通称フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = csDataRow(ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG);
                        // 生年月日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN);
                        // 不詳生年月日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = csDataRow(ABAtenaHyojunEntity.FUSHOUMAREBI);
                        // 記載事由
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD);
                        // 記載年月日
                        csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csDataRow(ABAtenaHyojunEntity.KISAIYMD);
                        // 消除事由
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD);

                        if ((csDataRow(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || csDataRow(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) && !(csDataRow(ABAtenaEntity.SHOJOJIYUCD).ToString.Trim == string.Empty))
                        {
                            if (csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = m_strShojoIdoWmdHenkan;
                            }
                            else
                            {
                                csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD);
                            }
                        }
                        else
                        {
                            // 消除異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD);
                        }
                        // 消除異動日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN);
                        // 不詳消除異動日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csDataRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI);

                        if (!(csDataRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI).ToString.Trim == string.Empty) && csDataRow(ABAtenaHyojunEntity.CKINIDOWMD).ToString.Trim == string.Empty)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = m_strCknIdoWmdHenkan;
                        }
                        else
                        {
                            // 直近異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csDataRow(ABAtenaHyojunEntity.CKINIDOWMD);
                        }
                        // 直近異動日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN);
                        // 不詳直近異動日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csDataRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI);
                        // 事実上の世帯主
                        csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csDataRow(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI);
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            // 住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.JUKISHIKUCHOSONCD);
                            // 住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.JUKIMACHIAZACD);
                            // 住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.JUKITODOFUKEN);
                            // 住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.JUKISHIKUCHOSON);
                            // 住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaHyojunEntity.JUKIMACHIAZA);
                        }
                        else
                        {
                            // 住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.SHIKUCHOSONCD);
                            // 住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.MACHIAZACD);
                            // 住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TODOFUKEN);
                            // 住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.SHIKUCHOSON);
                            // 住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaHyojunEntity.MACHIAZA);
                        }
                        if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                        {
                            // 本籍_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.HON_SHIKUCHOSONCD);
                            // 本籍_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.HON_MACHIAZACD);
                            // 本籍_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.HON_TODOFUKEN);
                            // 本籍_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = csDataRow(ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON);
                            // 本籍_町字
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.HON_MACHIAZA);
                        }
                        if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && (strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                        {
                            // 国籍コード
                            csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD);
                        }
                        if ((strGyomuMei ?? "") == NENKIN | (strGyomuMei ?? "") == NENKIN_2)
                        {
                            // 転入前住所_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                            // 転入前町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD);
                            // 転入前住所_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN);
                            // 転入前住所_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                            // 転入前住所_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA);
                            // 転入前住所_国名コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD);
                            // 転入前住所_国名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI);
                            // 転入前住所_国外住所
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                            // 転出確定_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                            // 転出確定町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                            // 転出確定_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                            // 転出確定_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                            // 転出確定_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                            // 転出予定_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                            // 転出予定町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                            // 転出予定_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                            // 転出予定_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                            // 転出予定_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                            // 転出予定_国名コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                            // 転出予定_国名等
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                            // 転出予定_国外住所
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                        }
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                        {
                            // 転入前住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                            // 転入前町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD);
                            // 転入前住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN);
                            // 転入前住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                            // 転入前住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA);
                            // 転入前住所_国名コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD);
                            // 転入前住所_国名
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI);
                            // 転入前住所_国外住所
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                            // 転出確定_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                            // 転出確定町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                            // 転出確定_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                            // 転出確定_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                            // 転出確定_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                            // 転出予定_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                            // 転出予定町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                            // 転出予定_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                            // 転出予定_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                            // 転出予定_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                            // 転出予定_国名コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                            // 転出予定_国名等
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                            // 転出予定_国外住所
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                        }
                        else
                        {
                        }
                        // 法第30条46又は47区分
                        csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csDataRow(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB);
                        // 在留カード等番号区分
                        csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csDataRow(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN);
                        // 住居地補正コード
                        csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csDataRow(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD);
                        // 直近届出通知区分
                        csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = csDataRow(ABAtenaEntity.CKINTDKDTUCIKB);
                        // 版番号
                        csDataNewRow(ABAtena1HyojunEntity.HANNO) = csDataRow(ABAtenaEntity.HANNO);
                        // 改製年月日
                        csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = csDataRow(ABAtenaEntity.KAISEIYMD);
                        // 異動区分
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = csDataRow(ABAtenaHyojunEntity.HYOJUNIDOKB);
                        // 入力場所コード
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = csDataRow(ABAtenaHyojunEntity.NYURYOKUBASHOCD);
                        // 入力場所表記
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = csDataRow(ABAtenaHyojunEntity.NYURYOKUBASHO);
                        if ((strGyomuMei ?? "") == KOBETSU & string.IsNullOrEmpty(strDainoKB.Trim()))
                        {
                            // 介護_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB);
                            // 国保_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB);
                            // 年金_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB);
                            // 年金_種別変更年月日
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD);
                            // 選挙_状態区分
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = csDataRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN);
                            if (m_strKobetsuShutokuKB == "1")
                            {
                                // 後期高齢_被保険者該当有無
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB);
                            }
                        }
                        // 連絡先区分（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = string.Empty;
                        // 連絡先名
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = string.Empty;
                        // 連絡先1（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = string.Empty;
                        // 連絡先2（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = string.Empty;
                        // 連絡先3（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = string.Empty;
                        // 連絡先種別1
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = string.Empty;
                        // 連絡先種別2
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = string.Empty;
                        // 連絡先種別3
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = string.Empty;
                        // * 履歴番号 000051 2023/10/19 修正開始
                        // If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                        if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2 && csDataRow.Table.Columns.Contains(ABFugenjuJohoEntity.FUGENJUKB))
                        {
                            // * 履歴番号 000051 2023/10/19 修正終了
                            // 不現住区分
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = csDataRow(ABFugenjuJohoEntity.FUGENJUKB);
                            // 不現住だった住所_郵便番号
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO);
                            // 不現住だった住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD);
                            // 不現住だった住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD);
                            // 不現住だった住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN);
                            // 不現住だった住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON);
                            // 不現住だった住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA);
                            // 不現住だった住所_番地号表記
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI);
                            // 不現住だった住所_方書
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI);
                            // 不現住だった住所_方書_フリガナ
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI);
                            // 不現住情報（対象者区分）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN);
                            // 不現住情報（対象者氏名）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI);
                            // 不現住情報（生年月日）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD);
                            // 不現住情報（性別）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU);
                            // 居住不明年月日
                            csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = csDataRow(ABFugenjuJohoEntity.KYOJUFUMEI_YMD);
                            // 不現住情報（備考）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_BIKO);
                        }
                        else
                        {
                        }
                        if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                        {
                            // 番号法更新区分
                            csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csDataRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB);
                        }
                        // * 履歴番号 000051 2023/10/19 修正開始
                        // If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1 && (strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2 && csDataRow.Table.Columns.Contains(ABDENSHISHOMEISHOMSTEntity.SERIALNO))
                        {
                            // * 履歴番号 000051 2023/10/19 修正終了
                            // シリアル番号
                            csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = csDataRow(ABDENSHISHOMEISHOMSTEntity.SERIALNO);
                        }
                        // 標準準拠異動事由コード
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNIDOJIYUCD);
                        if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                        {
                            // 連絡先区分（送付先）
                            csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = string.Empty;
                            // 送付先区分
                            csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = string.Empty;
                        }
                        else
                        {
                        }

                        strAtenaDataKB = Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)).Trim;
                        strAtenaDataSHU = Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATASHU)).Trim;
                        m_cABHyojunkaCdHenshuB.HenshuHyojunkaCd(strAtenaDataKB, strAtenaDataSHU);
                        // 住民区分
                        csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = m_cABHyojunkaCdHenshuB.p_strJuminKbn;
                        // 住民種別
                        csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = m_cABHyojunkaCdHenshuB.p_strJuminShubetsu;
                        // 住民状態
                        csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = m_cABHyojunkaCdHenshuB.p_strJuminJotai;
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            // 番地枝番数値
                            csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI);
                        }
                        else
                        {
                            // 番地枝番数値
                            csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI);
                        }
                    }
                    else
                    {
                        // noop
                    }

                    // *履歴番号 000026 2005/12/21 追加開始
                    csDataNewRow.EndEdit();
                    // *履歴番号 000026 2005/12/21 追加終了

                    // データレコードの追加
                    csDataTable.Rows.Add(csDataNewRow);
                    // *履歴番号 000013 2003/04/18 修正終了

                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            return csAtena1;

        }
        #endregion

        #region  年金宛名編集(NenkinAtenaHenshu) 
        // *履歴番号 000013 2003/04/18 追加開始
        // ************************************************************************************************
        // * メソッド名     年金宛名編集
        // * 
        // * 構文           Public Function NenkinAtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　年金編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABNenkinAtena)   : 取得した年金用宛名情報
        // ************************************************************************************************
        public DataSet NenkinAtenaHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", NENKIN);
        }
        // *履歴番号 000013 2003/04/18 追加終了
        // *履歴番号 000017 2003/10/09 追加開始
        // ************************************************************************************************
        // * メソッド名     年金履歴編集
        // * 
        // * 構文           Public Function NenkinRirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                                  ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　年金編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABNenkinAtena)   : 取得した年金用宛名情報
        // ************************************************************************************************
        public DataSet NenkinRirekiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return RirekiHenshu(cAtenaGetPara1, csAtenaEntity, string.Empty, string.Empty, string.Empty, NENKIN);
        }
        // *履歴番号 000017 2003/10/09 追加終了
        #endregion

        #region  年金宛名編集Ⅱ(NenkinAtenaHenshu2) 
        // *履歴番号 000027 2006/07/31 追加開始
        // ************************************************************************************************
        // * メソッド名     年金宛名編集Ⅱ
        // * 
        // * 構文           Public Function NenkinAtenaHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　年金編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABNenkinAtena)   : 取得した年金用宛名情報
        // ************************************************************************************************
        public DataSet NenkinAtenaHenshu2(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", NENKIN_2);
        }

        // ************************************************************************************************
        // * メソッド名     年金履歴編集Ⅱ
        // * 
        // * 構文           Public Function NenkinRirekiHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                                  ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　年金編集宛名データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABNenkinAtena)   : 取得した年金用宛名情報
        // ************************************************************************************************
        public DataSet NenkinRirekiHenshu2(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return RirekiHenshu(cAtenaGetPara1, csAtenaEntity, string.Empty, string.Empty, string.Empty, NENKIN_2);
        }
        // *履歴番号 000027 2006/07/31 追加終了
        #endregion

        #region  宛名個別編集(AtenaKobetsuHenshu) 
        // *履歴番号 000019 2003/11/19 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名個別編集
        // * 
        // * 構文           Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　宛名個別編集データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABAtena1Kobetsu)   : 取得した宛名個別編集
        // ************************************************************************************************
        internal DataSet AtenaKobetsuHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", KOBETSU);
        }
        // ************************************************************************************************
        // * メソッド名     宛名個別編集
        // * 
        // * 構文           Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                           ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　宛名個別編集データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // * 　　         csAtenaEntity       : 宛名データ
        // * 　　         strDainoKB          : 代納区分
        // * 　　         strGyomuCD          : 業務コード
        // * 　　         strGyomunaiSHU_CD   : 業務内種別コード
        // * 
        // * 戻り値       DataSet(ABAtena1Kobetsu)   : 取得した宛名個別編集
        // ************************************************************************************************
        internal DataSet AtenaKobetsuHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD)
        {
            return AtenaHenshu(cAtenaGetPara1, csAtenaEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, KOBETSU);
        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴個別編集
        // * 
        // * 構文           Friend Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                                  ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　宛名履歴個別編集データを作成する
        // * 
        // * 引数         cAtenaGetPara1      : 宛名取得パラメータ
        // *              csAtenaEntity       : 宛名データ
        // * 
        // * 戻り値       DataSet(ABAtena1Kobetsu)   : 取得した宛名履歴個別編集
        // ************************************************************************************************
        internal DataSet RirekiKobetsuHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaEntity)
        {
            return RirekiHenshu(cAtenaGetPara1, csAtenaEntity, string.Empty, string.Empty, string.Empty, KOBETSU);
        }
        // ************************************************************************************************
        // * メソッド名     宛名履歴個別編集
        // * 
        // * 構文           Friend Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
        // *                                                  ByVal csAtenaEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　　宛名履歴個別編集データを作成する
        // * 
        // * 引数          cAtenaGetPara1         : 宛名取得パラメータ
        // * 　　          csAtenaRirekiEntity    : 宛名履歴データ
        // * 　　          strDainoKB             : 代納区分
        // * 　　          strGyomuMei            : 業務名
        // * 
        // * 戻り値       DataSet(ABAtena1Kobetsu)   : 取得した宛名履歴個別編集
        // ************************************************************************************************
        internal DataSet RirekiKobetsuHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaRirekiEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD)
        {
            return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, KOBETSU);
        }

        // ************************************************************************************************
        // * メソッド名     送付先個別編集
        // * 
        // * 構文           Friend Function SofusakiKobetsuHenshu(ByVal csAtena1 As DataSet, _
        // *                                                      ByVal csSfskEntity As DataSet, _
        // *                                                      ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           csAtena1              : 宛名履歴データ
        // *               csSfskEntity           : 送付先データ
        // *               cAtenaGetPara1         : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena12)    : 取得した宛名情報
        // ************************************************************************************************
        internal DataSet SofusakiKobetsuHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtena1, DataSet csSfskEntity)
        {
            return SofusakiHenshu(cAtenaGetPara1, csAtena1, csSfskEntity, KOBETSU);
        }
        // *履歴番号 000019 2003/11/19 追加終了
        #endregion

        #region  履歴編集(RirekiHenshu) 
        // ************************************************************************************************
        // * メソッド名     履歴編集
        // * 
        // * 構文           Public Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1, _
        // *                                            ByVal csAtenaRirekiEntity As DataSet) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           cAtenaGetPara1         : 宛名取得パラメータ
        // *               csAtenaRirekiEntity    : 宛名履歴データ
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet RirekiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaRirekiEntity)
        {

            // *履歴番号 000017 2003/10/09 修正開始
            // Return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, "", "", "")
            return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, string.Empty, string.Empty, string.Empty);
            // *履歴番号 000017 2003/10/09 修正終了
        }

        // *履歴番号 000017 2003/10/09 追加開始
        // ************************************************************************************************
        // * メソッド名     履歴編集
        // * 
        // * 構文           Public Function RirekiHenshu(ByVal csAtenaRirekiEntity As DataSet, 
        // *                                            ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
        // *                                            ByVal strDainoKB As String,
        // *                                            ByVal strGyomuCD As String,
        // *                                            ByVal strGyomunaiSHU_CD As String) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           cAtenaGetPara1         : 宛名取得パラメータ
        // *               csAtenaRirekiEntity    : 宛名履歴データ
        // *               strDainoKB             : 代納区分
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataSet RirekiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaRirekiEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD)
        {
            return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, string.Empty);
        }
        // *履歴番号 000017 2003/10/09 追加終了

        // ************************************************************************************************
        // * メソッド名     履歴編集
        // * 
        // * 構文           Public Function RirekiHenshu(ByVal csAtenaRirekiEntity As DataSet, 
        // *                                            ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
        // *                                            ByVal strDainoKB As String,
        // *                                            ByVal strGyomuCD As String,
        // *                                            ByVal strGyomunaiSHU_CD As String, _
        // *                                            ByVal strGyomuMei As String) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           cAtenaGetPara1         : 宛名取得パラメータ
        // *               csAtenaRirekiEntity    : 宛名履歴データ
        // *               strDainoKB             : 代納区分
        // *               strGyomuMei            : 業務名
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        // *履歴番号 000017 2003/10/09 修正開始
        // Public Overloads Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, _
        // ByVal csAtenaRirekiEntity As DataSet, _
        // ByVal strDainoKB As String, _
        // ByVal strGyomuCD As String, _
        // ByVal strGyomunaiSHU_CD As String) As DataSet
        private DataSet RirekiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtenaRirekiEntity, string strDainoKB, string strGyomuCD, string strGyomunaiSHU_CD, string strGyomuMei)
        {
            // *履歴番号 000017 2003/10/09 修正終了
            const string THIS_METHOD_NAME = "RirekiHenshu";
            // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            DataTable csDataTable;
            DataSet csAtena1;                             // 宛名情報(ABAtena1)
            DataRow csDataNewRow;
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cuUSSCityInfo As USSCityInfoClass               '市町村情報管理クラス
            // Dim cABDainoKankeiB As ABDainoKankeiBClass          '代納関係クラス
            // Dim cABJuminShubetsuB As ABJuminShubetsuBClass      '住民種別クラス
            // Dim cABHojinMeishoB As ABHojinMeishoBClass          '法人名称クラス
            // Dim cABKjnhjnKBB As ABKjnhjnKBBClass                '個人法人クラス
            // Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '管内管外クラス
            // Dim cABUmareHenshuB As ABUmareHenshuBClass          '生年月日編集クラス
            // * 履歴番号 000023 2004/08/27 削除終了
            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
            // Dim csDainoKankeiCDMSTEntity As DataSet             '代納関係DataSet
            DataRow[] csDainoKankeiCDMSTEntity;             // 代納関係DataRow()
                                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）

            // * 履歴番号 000024 2005/01/25 削除開始（宮沢）
            // Dim strHenshuJusho As String                        '編集住所名
            // * 履歴番号 000024 2005/01/25 削除終了

            string strHenshuKanaMeisho;                   // 編集カナ名称
            string strHenshuKanjiShimei;                  // 編集漢字氏名
                                                          // *履歴番号 000008 2003/03/17 追加開始
                                                          // *履歴番号 000016 2003/08/22 削除開始
                                                          // Dim cURKanriJohoB As URKANRIJOHOBClass              '管理情報取得クラス
                                                          // *履歴番号 000016 2003/08/22 削除終了
            SofuJushoGyoseikuType cSofuJushoGyoseikuType;
            var strJushoHenshu3 = default(string);                       // 住所編集３
                                                                         // *履歴番号 000008 2003/03/17 追加終了
                                                                         // *履歴番号 000015 2003/04/30 追加開始
            var strJushoHenshu4 = default(string);                       // 住所編集４
                                                                         // *履歴番号 000015 2003/04/30 追加終了

            // *履歴番号 000021 2003/12/02 削除開始
            // '*履歴番号 000017 2003/10/09 追加開始
            // Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' 連絡先Ｂクラス
            // Dim csRenrakusakiEntity As DataSet                  ' 連絡先DataSet
            // Dim csRenrakusakiRow As DataRow                     ' 連絡先Row
            // '*履歴番号 000017 2003/10/09 追加終了
            // *履歴番号 000021 2003/12/02 削除終了
            // * corresponds to VS2008 Start 2010/04/16 000039
            // *履歴番号 000020 2003/12/01 追加開始
            // Dim strRenrakusakiGyomuCD As String                 ' 連絡先業務コード
            // *履歴番号 000020 2003/12/01 追加終了
            // * corresponds to VS2008 End 2010/04/16 000039

            // * 履歴番号 000026 2005/12/21 追加開始
            string strWork;
            // * 履歴番号 000026 2005/12/21 追加終了
            // *履歴番号 000042 2011/05/18 追加開始
            var strMeisho = new string[2];                          // 本名通称名優先制御用
                                                                    // *履歴番号 000042 2011/05/18 追加終了
            string strAtenaDataKB;
            string strAtenaDataSHU;


            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 'エラー処理クラスのインスタンス作成
                // '*履歴番号 000010  2003/03/27 修正開始
                // 'cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
                // cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // '*履歴番号 000010  2003/03/27 修正終了

                // *履歴番号 000017 2003/10/09 修正開始
                // 'カラム情報作成
                // csAtena1 = New DataSet()
                // csAtena1.Tables.Add(Me.CreateAtena1Columns())

                // *履歴番号 000019 2003/11/19 修正開始
                // 'カラム情報作成
                // If (strGyomuMei = NENKIN) Then
                // csDataTable = Me.CreateNenkinAtenaColumns()
                // Else
                // csDataTable = Me.CreateAtena1Columns()
                // End If

                // *履歴番号 000040 2010/05/14 追加開始
                // 本籍筆頭者区分パラメータに変数をセット
                m_strHonsekiHittoshKB_Param = cAtenaGetPara1.p_strHonsekiHittoshKB;

                // 処理停止区分パラメータに変数をセット
                m_strShoriteishiKB_Param = cAtenaGetPara1.p_strShoriTeishiKB;
                // *履歴番号 000040 2010/05/14 追加終了

                // *履歴番号 000041 2011/05/18 追加開始
                // 外国人在留情報取得区分パラメータに変数をセット
                m_strFrnZairyuJohoKB_Param = cAtenaGetPara1.p_strFrnZairyuJohoKB;
                // *履歴番号 000041 2011/05/18 追加終了
                // *履歴番号 000046 2011/11/07 追加開始
                // 住基法改正区分を変数にセット
                m_strJukiHokaiseiKB_Param = cAtenaGetPara1.p_strJukiHokaiseiKB;
                // *履歴番号 000046 2011/11/07 追加終了
                // *履歴番号 000048 2014/04/28 追加開始
                // 共通番号取得区分を変数にセット
                m_strMyNumberKB_Param = cAtenaGetPara1.p_strMyNumberKB;
                // *履歴番号 000048 2014/04/28 追加終了

                // カラム情報作成
                switch (strGyomuMei ?? "")
                {
                    // *履歴番号 000027 2006/07/31 修正開始
                    case NENKIN:
                    case NENKIN_2:    // 年金宛名情報
                        {
                            // *履歴番号 000040 2010/05/14 追加開始
                            m_blnNenKin = true;
                            // *履歴番号 000040 2010/05/14 追加終了

                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnKobetsu = false;
                            m_strKobetsuShutokuKB = string.Empty;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateNenkinAtenaHyojunColumns(strGyomuMei);
                            }
                            else
                            {
                                csDataTable = CreateNenkinAtenaColumns(strGyomuMei);
                            }

                            break;
                        }
                    // Case NENKIN     ' 年金宛名情報
                    // csDataTable = Me.CreateNenkinAtenaColumns()
                    // *履歴番号 000027 2006/07/31 修正終了
                    case KOBETSU:    // 宛名個別情報
                        {
                            // *履歴番号 000034 2008/01/15 追加開始
                            // 個別事項取得区分をメンバ変数にセット
                            m_strKobetsuShutokuKB = cAtenaGetPara1.p_strKobetsuShutokuKB.Trim;
                            // *履歴番号 000034 2008/01/15 追加終了

                            // *履歴番号 000040 2010/05/14 追加開始
                            m_blnKobetsu = true;
                            // *履歴番号 000040 2010/05/14 追加終了

                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnNenKin = false;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1KobetsuHyojunColumns();
                            }
                            else
                            {
                                csDataTable = CreateAtena1KobetsuColumns();
                            }       // 宛名情報

                            break;
                        }

                    default:
                        {
                            // *履歴番号 000047 2012/03/13 追加開始
                            m_blnKobetsu = false;
                            m_blnNenKin = false;
                            m_strKobetsuShutokuKB = string.Empty;
                            // *履歴番号 000047 2012/03/13 追加終了
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1HyojunColumns();
                            }
                            else
                            {
                                csDataTable = CreateAtena1Columns();
                            }

                            break;
                        }
                }
                // *履歴番号 000019 2003/11/19 修正終了

                csAtena1 = new DataSet();
                csAtena1.Tables.Add(csDataTable);
                // *履歴番号 000017 2003/10/09 修正終了

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // '市町村情報のインスタンス作成
                // 'cuUSSCityInfo = New USSCityInfoClass()

                // '代納関係のインスタンス作成
                // cABDainoKankeiB = New ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)

                // '住民種別のインスタンス作成
                // cABJuminShubetsuB = New ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '法人名称のインスタンス作成
                // cABHojinMeishoB = New ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '個人法人のインスタンス作成
                // cABKjnhjnKBB = New ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '管内管外のインスタンス作成
                // cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

                // '生年月日編集クラスのインスタンス化
                // cABUmareHenshuB = New ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // *履歴番号 000016 2003/08/22 削除開始
                // '管理情報取得Ｂのインスタンス作成
                // cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // *履歴番号 000016 2003/08/22 削除終了
                // *履歴番号 000008 2003/03/17 追加終了

                // *履歴番号 000021 2003/12/02 削除開始
                // '*履歴番号 000017 2003/10/09 追加開始
                // ' 連絡先Ｂクラスのインスタンス作成
                // cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // '*履歴番号 000017 2003/10/09 追加終了
                // *履歴番号 000021 2003/12/02 削除終了

                // *履歴番号 000007 2003/03/17 追加開始
                // パラメータのチェック
                CheckColumnValue(cAtenaGetPara1);
                // *履歴番号 000007 2003/03/17 追加終了

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // '住所編集１が"1"且つ住所編集２が"1"の場合
                // If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                // '直近の市町村名を取得する
                // 'm_cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
                // End If
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // 住所編集１が"1"且つ住所編集３が""の場合
                if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu3 == string.Empty)
                {
                    // *履歴番号 000016 2003/08/22 修正開始
                    // cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                    cSofuJushoGyoseikuType = GetSofuJushoGyoseikuType();
                    // *履歴番号 000016 2003/08/22 修正終了
                    switch (cSofuJushoGyoseikuType)
                    {
                        case var @case when @case == SofuJushoGyoseikuType.Jusho_Banchi:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case1 when case1 == SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "1";
                                break;
                            }
                        case var case2 when case2 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case3 when case3 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "1";
                                break;
                            }
                    }
                }
                else
                {
                    strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3;
                    strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4;
                }
                // *履歴番号 000008 2003/03/17 追加終了

                // 編集宛名データを作成する
                foreach (DataRow csDataRow in csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows)
                {
                    // *履歴番号 000017 2003/10/09 修正開始
                    // csDataNewRow = csAtena1.Tables(ABAtena1Entity.TABLE_NAME).NewRow
                    csDataNewRow = csDataTable.NewRow();
                    // *履歴番号 000017 2003/10/09 修正終了

                    // *履歴番号 000015 2003/04/30 追加開始
                    foreach (DataColumn csColumn in csDataNewRow.Table.Columns)
                        csDataNewRow[csColumn] = string.Empty;
                    // *履歴番号 000015 2003/04/30 追加終了

                    // *履歴番号 000021 2003/12/02 削除開始
                    // '*履歴番号 000017 2003/10/09 追加開始
                    // ' 業務コードが指定された場合
                    // If (strGyomuCD <> String.Empty) Then

                    // ' 連絡先データを取得する
                    // csRenrakusakiEntity = cRenrakusakiBClass.GetRenrakusakiBHoshu(CType(csDataRow(ABAtenaEntity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD)
                    // If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                    // csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                    // Else
                    // csRenrakusakiRow = Nothing
                    // End If
                    // Else
                    // csRenrakusakiRow = Nothing
                    // End If
                    // '*履歴番号 000017 2003/10/09 追加終了
                    // *履歴番号 000021 2003/12/02 削除終了

                    // 住民コード
                    csDataNewRow(ABAtena1Entity.JUMINCD) = csDataRow(ABAtenaRirekiEntity.JUMINCD);

                    // 代納区分
                    if (string.IsNullOrEmpty(strDainoKB))
                    {
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "00";
                    }
                    else
                    {
                        csDataNewRow(ABAtena1Entity.DAINOKB) = strDainoKB;
                    }

                    if (Conversions.ToString(csDataNewRow(ABAtena1Entity.DAINOKB)) == "00")
                    {
                        // 代納区分名称
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;
                        // 代納区分略式名称
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;
                    }
                    else
                    {
                        // 代納関係データを取得する

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                        // csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                        // '０件の場合、
                        // If csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count = 0 Then
                        // csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '代納区分名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '代納区分略式名称
                        // Else
                        // With csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0)

                        // '代納区分名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                        // '代納区分略式名称
                        // csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                        // End With

                        // End If
                        csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu2(Conversions.ToString(csDataNewRow(ABAtena1Entity.DAINOKB)));
                        if (csDainoKankeiCDMSTEntity.Length == 0)
                        {
                            csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;                   // 代納区分名称
                            csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;              // 代納区分略式名称
                        }
                        else
                        {

                            // 代納区分名称
                            csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = Conversions.ToString(csDainoKankeiCDMSTEntity[0].Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO));

                            // 代納区分略式名称
                            csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = Conversions.ToString(csDainoKankeiCDMSTEntity[0].Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI));

                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                    }

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {

                        // 代納区分指定なしの場合
                        if (string.IsNullOrEmpty(strGyomuCD))
                        {

                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = "00";

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = string.Empty;
                        }
                        else
                        {
                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = strGyomuCD;

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = strGyomunaiSHU_CD;
                        }

                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 旧市町村コード
                    csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csDataRow(ABAtenaRirekiEntity.KYUSHICHOSONCD);

                    // 世帯コード
                    csDataNewRow(ABAtena1Entity.STAICD) = csDataRow(ABAtenaRirekiEntity.STAICD);

                    // 宛名データ区分
                    csDataNewRow(ABAtena1Entity.ATENADATAKB) = csDataRow(ABAtenaRirekiEntity.ATENADATAKB);

                    // 宛名データ種別
                    csDataNewRow(ABAtena1Entity.ATENADATASHU) = csDataRow(ABAtenaRirekiEntity.ATENADATASHU);

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 編集種別
                        m_cABJuminShubetsuB.GetJuminshubetsu(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)));
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = m_cABJuminShubetsuB.p_strHenshuShubetsu;

                        // 編集種別略称
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = m_cABJuminShubetsuB.p_strHenshuShubetsuRyaku;

                        // 検索用カナ姓名
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANASEIMEI);

                        // 検索用カナ姓
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANASEI);
                        // 検索用カナ名

                        csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANAMEI);

                        // 検索用漢字名称
                        csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANJIMEISHO);
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // *履歴番号 000042 2011/05/18 追加開始
                    // 本名通称名切替対応 - カナ名称、漢字名称取得
                    switch (Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) ?? "")
                    {
                        case "11":
                        case "12":         // 住登内、住登外
                            {

                                if (m_strHonmyoTsushomeiYusenKB.Trim() == "1")
                                {
                                    // 管理情報：本名通称名優先制御 = "1" の場合
                                    strMeisho = MeishoHenshu(csDataRow);
                                }
                                else
                                {
                                    strMeisho[0] = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1));       // カナ名称１
                                    strMeisho[1] = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO1));
                                }      // 

                                break;
                            }
                        case "20":               // 法人
                            {
                                break;
                            }

                        case "30":               // 共有
                            {
                                strMeisho[0] = Conversions.ToString(csDataRow(ABAtenaEntity.KANAMEISHO1));
                                strMeisho[1] = Conversions.ToString(csDataRow(ABAtenaEntity.KANJIMEISHO1));
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                    // *履歴番号 000042 2011/05/18 追加終了

                    // 編集カナ名称
                    // 宛名区分="20"(法人)の場合
                    if (Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)) == "20")
                    {
                        // * 履歴番号 000033 2007/07/17 修正開始
                        // カナ名称２（支店名）が無い場合はカナ名称１（法人名）とカナ名称２（支店名）の結合は行わない
                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2)).Trim ?? new char[0]) != "")
                        {
                            strHenshuKanaMeisho = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1)).TrimEnd + " " + Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2)).TrimEnd;
                        }
                        else
                        {
                            strHenshuKanaMeisho = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1)).TrimEnd;
                        }
                        // strHenshuKanaMeisho = CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1), String).TrimEnd _
                        // + " " + CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2), String).TrimEnd
                        // * 履歴番号 000033 2007/07/17 修正終了
                        // * 履歴番号 000032 2007/07/09 修正開始
                        if (strHenshuKanaMeisho.RLength > 240)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.RSubstring(0, 240);
                        }
                        // If (strHenshuKanaMeisho.Length > 60) Then
                        // csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.Substring(0, 60)
                        // * 履歴番号 000032 2007/07/09 修正終了
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho;
                        }
                    }
                    else
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        strHenshuKanaMeisho = strMeisho[0];
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strHenshuKanaMeisho, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO);
                        // csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO1)
                        // *履歴番号 000042 2011/05/18 修正終了
                    }
                    // 編集カナ名称（フル）
                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = strHenshuKanaMeisho;
                    }
                    else
                    {
                    }

                    // 編集漢字名称
                    // 宛名区分="20"(法人)の場合
                    if (Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)) == "20")
                    {
                        m_cABHojinMeishoB.p_strKeitaiFuyoKB = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.HANYOKB1));
                        m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.HANYOKB2));
                        m_cABHojinMeishoB.p_strKanjiHjnKeitai = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANJIHJNKEITAI));
                        m_cABHojinMeishoB.p_strKanjiMeisho1 = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1));
                        m_cABHojinMeishoB.p_strKanjiMeisho2 = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2));
                        strHenshuKanjiShimei = m_cABHojinMeishoB.GetHojinMeisho();
                        // * 履歴番号 000032 2007/076/09 修正開始
                        if (strHenshuKanjiShimei.RLength > 240)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.RSubstring(0, 240);
                        }
                        // If (strHenshuKanjiShimei.Length > 80) Then
                        // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.Substring(0, 80)
                        // * 履歴番号 000032 2007/07/09 修正終了
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei;
                        }
                    }
                    // * 履歴開始 000035 2008/02/15 修正開始
                    // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)
                    else if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                        strHenshuKanjiShimei = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)), strMeisho[1]);
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO);
                    }
                    // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), _
                    // CStr(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)), _
                    // CStr(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)))
                    // *履歴番号 000042 2011/05/18 修正終了
                    else
                    {
                        // *履歴番号 000042 2011/05/18 修正開始
                        // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                        strHenshuKanjiShimei = strMeisho[1];
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO);
                        // csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)
                        // *履歴番号 000042 2011/05/18 修正終了
                        // * 履歴開始 000035 2008/02/15 修正終了
                    }
                    // 編集漢字名称（フル）
                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = strHenshuKanjiShimei;
                    }
                    else
                    {
                    }

                    if (csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)
                    {
                        if (csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString.Trim == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = m_strUmareYMDHenkanParam;
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdHenkan;
                        }
                        else if (CheckDate(csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString))
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaRirekiEntity.UMAREYMD);
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.UMAREYMD) = GetSeirekiLastDay(csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString);
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = GetWarekiLastDay(csDataRow(ABAtenaRirekiEntity.UMAREWMD).ToString, csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString);
                        }
                    }
                    else
                    {
                        // 生年月日
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaRirekiEntity.UMAREYMD);

                        // 生和暦年月日
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD);
                    }
                    // 生年月日編集
                    // csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD)

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        m_cABUmareHenshuB.p_strDataKB = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB));
                        m_cABUmareHenshuB.p_strJuminSHU = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU));
                        m_cABUmareHenshuB.p_strUmareYMD = Conversions.ToString(csDataNewRow(ABAtena1Entity.UMAREYMD));
                        m_cABUmareHenshuB.p_strUmareWMD = Conversions.ToString(csDataNewRow(ABAtena1Entity.UMAREWMD));
                        m_cABUmareHenshuB.HenshuUmare();
                        // 生表示年月日
                        csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = m_cABUmareHenshuB.p_strHyojiUmareYMD;

                        // 生証明年月日
                        csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = m_cABUmareHenshuB.p_strShomeiUmareYMD;

                        // 性別コード
                        csDataNewRow(ABAtena1Entity.SEIBETSUCD) = csDataRow(ABAtenaRirekiEntity.SEIBETSUCD);

                        // 性別
                        strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.SEIBETSU)).Trim;
                        csDataNewRow(ABAtena1Entity.SEIBETSU) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_SEIBETSU);
                        // 性別（フル）
                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.SEIBETSU_FULL) = csDataRow(ABAtenaRirekiEntity.SEIBETSU);
                        }
                        else
                        {
                        }

                        // 編集続柄コード
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(ABAtenaRirekiEntity.DAI2ZOKUGARACD, String) = String.Empty Then
                        // *履歴番号 000018 2003/10/14 修正開始
                        // If CType(ABAtenaRirekiEntity.DAI2ZOKUGARACD, String).Trim = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2ZOKUGARACD)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000018 2003/10/14 修正終了
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.ZOKUGARACD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.DAI2ZOKUGARACD);
                        }

                        // 編集続柄
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(ABAtenaRirekiEntity.DAI2ZOKUGARA, String) = String.Empty Then
                        // *履歴番号 000018 2003/10/14 修正開始
                        // If CType(ABAtenaRirekiEntity.DAI2ZOKUGARA, String).Trim = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaEntity.DAI2ZOKUGARA)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000018 2003/10/14 修正終了
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaRirekiEntity.ZOKUGARA);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaRirekiEntity.DAI2ZOKUGARA);
                        }

                        // * 履歴開始 000035 2008/02/15 修正開始
                        // 法人代表者名
                        // csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)
                        if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                            csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)));
                        }
                        else
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                            csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI);
                        }
                        // * 履歴開始 000035 2008/02/15 修正終了
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    // 個人法人区分
                    csDataNewRow(ABAtena1Entity.KJNHJNKB) = csDataRow(ABAtenaRirekiEntity.KJNHJNKB);

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {

                        // 個人法人区分名称
                        csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = m_cABKjnhjnKBB.GetKjnhjn(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KJNHJNKB)));

                        // 管内管外区分名称
                        csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB)));
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // 管内管外区分
                    csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB);

                    // 住基優先の場合
                    if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                    {

                        // 郵便番号
                        csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaRirekiEntity.JUKIYUBINNO);

                        // 住所コード
                        csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaRirekiEntity.JUKIJUSHOCD);

                        // 住所
                        csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaRirekiEntity.JUKIJUSHO);

                        // 編集住所名
                        if (cAtenaGetPara1.p_strJushoHenshu1 == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }

                        else if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // strHenshuJusho = String.Empty
                            m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength);
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (cAtenaGetPara1.p_strJushoHenshu2 == "1")
                            {

                                // 市町村名を頭に付加する（管内のみ）
                                if (Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB)) == "1")
                                {
                                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                    // strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                    m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0));
                                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                }
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // Select Case cAtenaGetPara1.p_strJushoHenshu3
                            switch (strJushoHenshu3 ?? "")
                            {
                                // *履歴番号 000008 2003/03/17 修正終了
                                // * 履歴番号 000028 2007/01/15 修正開始
                                case "1":
                                case "6":   // 住所＋番地
                                    {
                                        // Case "1"    '住所＋番地
                                        // * 履歴番号 000028 2007/01/15 修正終了
                                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        break;
                                    }
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                case "2":    // 行政区＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // 行政区＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 修正終了
                                case "3":    // 住所＋（行政区）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000004 2003/02/25 修正終了
                                case "4":    // 行政区＋（住所）＋番地
                                    {
                                        // *履歴番号 000004 2003/02/25 修正開始 
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                        // 住所が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加開始
                                        // 行政区名が存在しない場合
                                        else if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加終了
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）

                                        }

                                        break;
                                    }
                                // *履歴番号 000004 2003/02/25 修正終了
                                // *履歴番号 000009 2003/03/17 追加開始
                                case "5":    // 行政区＋△＋番地
                                    {
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // 行政区＋△＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                            // + "　" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("　");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                    // *履歴番号 000009 2003/03/17 追加終了
                            }

                            // *履歴番号 000008 2003/03/17 修正開始
                            // If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                            // * 履歴番号 000028 2007/01/15 修正開始
                            if (strJushoHenshu4 == "1" && new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)).Trim ?? new char[0]) != "")
                            {
                                // If strJushoHenshu4 = "1" Then
                                // * 履歴番号 000028 2007/01/15 修正終了
                                // *履歴番号 000008 2003/03/17 修正終了
                                // *履歴番号 000004 2003/02/25 修正開始
                                // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).TrimEnd

                                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                // strHenshuJusho += "　" + CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).TrimEnd
                                m_strHenshuJusho.Append("　");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)).TrimEnd);
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                // *履歴番号 000004 2003/02/25 修正終了
                            }
                            // * 履歴番号 000028 2007/01/15 追加開始
                            // 住所編集３パラメータが６、且つ行政区名があるときは、編集住所に（行政区）を追加する
                            if (strJushoHenshu3 == "6" && new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).Trim ?? new char[0]) != "")
                            {
                                m_strHenshuJusho.Append("（");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)).TrimEnd);
                                m_strHenshuJusho.Append("）");
                            }
                            // * 履歴番号 000028 2007/01/15 追加終了
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // If strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            // Else
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                            // End If
                            // * 履歴番号 000032 2007/07/09 修正開始
                            if (m_strHenshuJusho.RLength >= 160)
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160);
                            }
                            // If m_strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            // * 履歴番号 000032 2007/07/09 修正終了
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString();
                            }
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString();
                            }
                            else
                            {
                            }
                        }

                        // 番地コード１
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD1);

                        // 番地コード２
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD2);

                        // 番地コード３
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD3);
                        // 番地
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // 住所編集ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.BANCHI) = "";
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI);
                        }

                        // 方書フラグ
                        csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKIFG);

                        // 方書コード
                        csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKICD);

                        // 方書
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu4 == "1")
                        {
                            // 方書付加ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI);
                            }
                            else
                            {
                            }
                        }

                        // *履歴番号 000017 2003/10/09 修正開始
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)

                        // *履歴番号 000021 2003/12/02 修正開始
                        // ' 連絡先マスタが存在する場合は、連絡先マスタの連絡先を設定する
                        // If (csRenrakusakiRow Is Nothing) Then
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                        // '*履歴番号 000020 2003/12/01 追加開始
                        // '連絡先取得業務コード
                        // strRenrakusakiGyomuCD = String.Empty
                        // '*履歴番号 000020 2003/12/01 追加終了
                        // Else
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                        // '*履歴番号 000020 2003/12/01 修正開始
                        // ''連絡先取得業務コード
                        // 'csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD

                        // '連絡先取得業務コード
                        // strRenrakusakiGyomuCD = strGyomuCD
                        // '*履歴番号 000020 2003/12/01 修正終了
                        // End If
                        // '*履歴番号 000017 2003/10/09 修正終了

                        // 連絡先１
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1);
                        // 連絡先２
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2);
                        // *履歴番号 000021 2003/12/02 修正終了

                        // 行政区コード
                        csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUCD);

                        // 行政区名
                        csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI);

                        // 地区コード１
                        csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD1);

                        // 地区１
                        csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI1);

                        // 地区コード２
                        csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD2);

                        // 地区２
                        csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI2);

                        // 地区コード３
                        csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD3);

                        // 地区３
                        csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI3);

                        // 表示順（第２住民票表示順がある場合は、第２住民票表示順）
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN), String) = String.Empty Then
                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            if (Conversions.ToString(csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN)).Trim == "00")
                            {
                                // *履歴番号 000002 2003/02/20 修正終了
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN);
                            }
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN);
                            }
                        }
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    else
                    {
                        // 郵便番号
                        csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaRirekiEntity.YUBINNO);
                        // 住所コード
                        csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaRirekiEntity.JUSHOCD);
                        // 住所
                        csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaRirekiEntity.JUSHO);

                        // 編集住所名
                        if (cAtenaGetPara1.p_strJushoHenshu1 == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }

                        else if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // strHenshuJusho = String.Empty
                            m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength);
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (cAtenaGetPara1.p_strJushoHenshu2 == "1")
                            {

                                // 管内のみ市町村名を付加する
                                if (Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB)) == "1")
                                {
                                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                    // strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                    m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0));
                                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                }
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // Select Case cAtenaGetPara1.p_strJushoHenshu3
                            switch (strJushoHenshu3 ?? "")
                            {
                                // *履歴番号 000008 2003/03/17 修正終了
                                // * 履歴番号 000028 2007/01/15 修正開始
                                case "1":
                                case "6":   // 住所＋番地
                                    {
                                        // Case "1"    '住所＋番地
                                        // * 履歴番号 000028 2007/01/15 修正終了
                                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                        m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        break;
                                    }
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                case "2":    // 行政区＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 修正終了
                                case "3":    // 住所＋（行政区）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000004  2003/02/25 修正終了
                                case "4":    // 行政区＋（住所）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                        // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd

                                        // 住所が存在しない場合、行政区＋番地
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加開始
                                        else if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 行政区名が存在しない場合、住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        // *履歴番号 000009 2003/03/17 追加終了
                                        else
                                        {
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "（" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + "）" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd);
                                            m_strHenshuJusho.Append("（");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append("）");
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                // *履歴番号 000004 2003/02/25 修正終了
                                // *履歴番号 000009 2003/03/17 追加開始
                                case "5":    // 行政区＋△＋番地
                                    {
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUSHO)).TrimEnd);
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                        }
                                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        else
                                        {
                                            // 行政区＋△＋番地
                                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                            // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                            // + "　" _
                                            // + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).TrimEnd);
                                            // * 履歴番号 000028 2007/01/15 追加開始
                                            m_strHenshuJusho.Append("　");
                                            // * 履歴番号 000028 2007/01/15 追加終了
                                            m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.BANCHI)).TrimEnd);
                                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                        }

                                        break;
                                    }
                                    // *履歴番号 000009 2003/03/17 追加終了
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                            // * 履歴番号 000028 2007/01/15 修正開始
                            if (strJushoHenshu4 == "1" && new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KATAGAKI)).Trim ?? new char[0]) != "")
                            {
                                // If strJushoHenshu4 = "1" Then
                                // * 履歴番号 000028 2007/01/15 修正終了
                                // *履歴番号 000008 2003/03/17 修正終了
                                // *履歴番号 000004  2003/02/25 修正開始
                                // strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).TrimEnd

                                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                                // strHenshuJusho += "　" + CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).TrimEnd
                                m_strHenshuJusho.Append("　");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KATAGAKI)).TrimEnd);
                                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                                // *履歴番号 000004  2003/02/25 修正終了
                            }
                            // * 履歴番号 000028 2007/01/15 追加開始
                            // 住所編集３パラメータが６、且つ行政区名があるときは、編集住所に（行政区）を追加する
                            if (strJushoHenshu3 == "6" && new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)).Trim ?? new char[0]) != "")
                            {
                                m_strHenshuJusho.Append("（");
                                m_strHenshuJusho.Append(Conversions.ToString(csDataRow(ABAtenaEntity.GYOSEIKUMEI)).TrimEnd);
                                m_strHenshuJusho.Append("）");
                            }
                            // * 履歴番号 000028 2007/01/15 追加終了
                            // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                            // If strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            // Else
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                            // End If
                            // * 履歴番号 000032 2007/07/09 修正開始
                            if (m_strHenshuJusho.RLength >= 160)
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160);
                            }
                            // If m_strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            // * 履歴番号 000032 2007/07/09 修正終了
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString();
                            }
                            // * 履歴番号 000024 2005/01/25 更新終了（宮沢）
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString();
                            }
                            else
                            {
                            }
                        }

                        // 番地コード１
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaRirekiEntity.BANCHICD1);

                        // 番地コード２
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaRirekiEntity.BANCHICD2);

                        // 番地コード３
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaRirekiEntity.BANCHICD3);

                        // 番地
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // 住所編集ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.BANCHI) = "";
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI);
                        }

                        // 方書フラグ
                        csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaRirekiEntity.KATAGAKIFG);

                        // 方書コード
                        csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaRirekiEntity.KATAGAKICD);

                        // 方書
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu4 == "1")
                        {
                            // 方書付加ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = "";
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI);
                            }
                            else
                            {
                            }
                        }

                        // *履歴番号 000017 2003/10/09 修正開始
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)

                        // *履歴番号 000021 2003/12/02 修正開始
                        // ' 連絡先マスタが存在する場合は、連絡先マスタの連絡先を設定する
                        // If (csRenrakusakiRow Is Nothing) Then
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                        // '*履歴番号 000020 2003/12/01 追加開始
                        // '連絡先取得業務コード
                        // strRenrakusakiGyomuCD = String.Empty
                        // '*履歴番号 000020 2003/12/01 追加終了
                        // Else
                        // '連絡先１
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                        // '連絡先２
                        // csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                        // '*履歴番号 000020 2003/12/01 修正開始
                        // ''連絡先取得業務コード
                        // 'csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD

                        // '連絡先取得業務コード
                        // strRenrakusakiGyomuCD = strGyomuCD
                        // '*履歴番号 000020 2003/12/01 修正終了
                        // End If
                        // '*履歴番号 000017 2003/10/09 修正終了

                        // 連絡先１
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1);
                        // 連絡先２
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2);
                        // *履歴番号 000021 2003/12/02 修正終了

                        // 行政区コード
                        csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaRirekiEntity.GYOSEIKUCD);

                        // 行政区名
                        csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI);

                        // 地区コード１
                        csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaRirekiEntity.CHIKUCD1);

                        // 地区１
                        csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI1);

                        // 地区コード２
                        csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaRirekiEntity.CHIKUCD2);

                        // 地区２
                        csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI2);

                        // 地区コード３
                        csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaRirekiEntity.CHIKUCD3);

                        // 地区３
                        csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI3);

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            // * 履歴番号 000026 2005/12/21 修正開始
                            // '表示順
                            // csDataNewRow(ABAtena1Entity.HYOJIJUN) = String.Empty

                            // 表示順（第２住民票表示順がある場合は、第２住民票表示順）
                            if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                            {
                                strWork = Conversions.ToString(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN)).Trim;
                                if (strWork == "00")
                                {
                                    strWork = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN).ToString().Trim;
                                }
                                if (string.IsNullOrEmpty(strWork))
                                {
                                    strWork = "99";
                                }
                                csDataNewRow(ABAtena1Entity.HYOJIJUN) = strWork;
                            }
                            // * 履歴番号 000026 2005/12/21 修正終了
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    }
                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {

                        // 登録異動年月日
                        csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csDataRow(ABAtenaRirekiEntity.TOROKUIDOYMD);

                        // 登録事由コード
                        csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csDataRow(ABAtenaRirekiEntity.TOROKUJIYUCD);

                        // 登録事由
                        csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csDataRow(ABAtenaRirekiEntity.TOROKUJIYU);

                        if ((csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) && !(csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD).ToString.Trim == string.Empty))
                        {
                            if (csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = m_strShojoIdobiHenkanParam;
                            }
                            else
                            {
                                csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD);
                            }
                        }
                        else
                        {
                            // 消除異動年月日
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD);
                        }
                        // 消除事由コード
                        csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD);

                        // 消除事由名称
                        csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csDataRow(ABAtenaRirekiEntity.SHOJOJIYU);

                        // 編集世帯主住民コード
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD), String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaRirekiEntity.STAINUSJUMINCD);
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD);
                        }
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    // 編集カナ世帯主名
                    // *履歴番号 000002 2003/02/20 修正開始
                    // If CType(csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI), String) = String.Empty Then
                    if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI)).Trim ?? new char[0]) == "")
                    {
                        // *履歴番号 000002 2003/02/20 修正終了
                        csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaRirekiEntity.KANASTAINUSMEI);
                    }
                    else
                    {
                        csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI);
                    }

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 編集漢字世帯主名
                        // *履歴番号 000002 2003/02/20 修正開始
                        // If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI), String) = String.Empty Then
                        if (new string(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)).Trim ?? new char[0]) == "")
                        {
                            // *履歴番号 000002 2003/02/20 修正終了
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.STAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.STAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.STAINUSMEI);
                            }
                        }
                        // * 履歴開始 000035 2008/02/15 修正終了
                        // * 履歴開始 000035 2008/02/15 修正開始
                        // csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)
                        else if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)));
                        }
                        else
                        {
                            // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI);
                            // * 履歴開始 000035 2008/02/15 修正終了
                        }

                        // *履歴番号 000012 2003/04/18 追加開始
                        // 続柄コード
                        csDataNewRow(ABAtena1Entity.ZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.ZOKUGARACD);
                        // 続柄
                        csDataNewRow(ABAtena1Entity.ZOKUGARA) = csDataRow(ABAtenaRirekiEntity.ZOKUGARA);

                        // *履歴番号 000014 2003/04/30 修正開始
                        // ' カナ名称２
                        // csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO2)
                        // ' 漢字名称２
                        // csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)

                        // 宛名区分≠"20"(法人)の場合
                        if (!(Conversions.ToString(csDataRow(ABAtenaEntity.ATENADATAKB)) == "20"))
                        {
                            // カナ名称２
                            csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO2);
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 漢字名称２
                            // csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)), Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                        }
                        // *履歴番号 000014 2003/04/30 修正終了

                        // 籍番号
                        csDataNewRow(ABAtena1Entity.SEKINO) = csDataRow(ABAtenaRirekiEntity.SEKINO);
                        // *履歴番号 000012 2003/04/18 追加終了
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                    // *履歴番号 000040 2010/05/14 追加開始
                    // 本籍筆頭者情報出力判定
                    if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                    {
                        // パラメータ:本籍筆頭者取得区分が"1"かつ、管理情報:本籍取得区分(10･18)が"1"の場合のみセット
                        // 本籍住所
                        csDataNewRow(ABAtena1Entity.HON_JUSHO) = csDataRow(ABAtenaRirekiEntity.HON_JUSHO);
                        // 本籍番地
                        csDataNewRow(ABAtena1Entity.HONSEKIBANCHI) = csDataRow(ABAtenaRirekiEntity.HONSEKIBANCHI);
                        // 筆頭者
                        csDataNewRow(ABAtena1Entity.HITTOSH) = csDataRow(ABAtenaRirekiEntity.HITTOSH);
                    }
                    else
                    {
                    }

                    // 処理停止区分出力判定
                    if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                    {
                        // パラメータ:処理停止区分取得区分が"1"かつ、管理情報:処理停止区分取得区分(10･19)が"1"の場合のみセット
                        // 処理停止区分
                        csDataNewRow(ABAtena1Entity.SHORITEISHIKB) = csDataRow(ABAtenaRirekiEntity.SHORITEISHIKB);
                    }
                    else
                    {
                    }
                    // *履歴番号 000040 2010/05/14 追加終了

                    // *履歴番号 000041 2011/05/18 追加開始
                    if (m_strFrnZairyuJohoKB_Param == "1")
                    {
                        // パラメータ：外国人在留資格取得区分が"1"の場合
                        // 国籍
                        strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KOKUSEKI)).Trim;
                        csDataNewRow(ABAtena1Entity.KOKUSEKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KOKUSEKI);
                        // 国籍（フル）
                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.KOKUSEKI_FULL) = csDataRow(ABAtenaRirekiEntity.KOKUSEKI);
                        }
                        else
                        {
                        }
                        // 在留資格コード
                        csDataNewRow(ABAtena1Entity.ZAIRYUSKAKCD) = csDataRow(ABAtenaRirekiEntity.ZAIRYUSKAKCD);
                        // 在留資格
                        csDataNewRow(ABAtena1Entity.ZAIRYUSKAK) = csDataRow(ABAtenaRirekiEntity.ZAIRYUSKAK);
                        // 在留期間
                        csDataNewRow(ABAtena1Entity.ZAIRYUKIKAN) = csDataRow(ABAtenaRirekiEntity.ZAIRYUKIKAN);
                        // 在留開始年月日
                        csDataNewRow(ABAtena1Entity.ZAIRYU_ST_YMD) = csDataRow(ABAtenaRirekiEntity.ZAIRYU_ST_YMD);
                        // 在留終了年月日
                        csDataNewRow(ABAtena1Entity.ZAIRYU_ED_YMD) = csDataRow(ABAtenaRirekiEntity.ZAIRYU_ED_YMD);
                    }
                    else
                    {
                    }
                    // *履歴番号 000041 2011/05/18 追加終了

                    // *履歴番号 000017 2003/10/09 修正開始
                    // 'レコードの追加
                    // csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                    // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                    if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                    {
                        // 年金用データ作成
                        // *履歴番号 000027 2006/07/31 修正開始
                        if ((strGyomuMei ?? "") == NENKIN | (strGyomuMei ?? "") == NENKIN_2)
                        {
                            // If (strGyomuMei = NENKIN) Then
                            // *履歴番号 000027 2006/07/31 修正終了


                            // 旧姓
                            csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = csDataRow(ABAtenaRirekiEntity.KYUSEI);
                            // 住定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.JUTEIIDOYMD);
                            // 住定事由
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = csDataRow(ABAtenaRirekiEntity.JUTEIJIYU);
                            // 転入前住所郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO);
                            // 転入前住所全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD);
                            // 転入前住所住所
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_JUSHO);
                            // 転入前住所番地
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_BANCHI);
                            // 転入前住所方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出予定郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO);
                            // 転出予定全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD);
                            // 転出予定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD);
                            // 転出予定住所
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO);
                            // 転出予定番地
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI);
                            // 転出予定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出確定郵便番号
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO);
                            // 転出確定全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD);
                            // 転出確定異動年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD);
                            // 転出確定通知年月日
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD);
                            // 転出確定住所
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO);
                            // 転出確定番地
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI);
                            // 転出確定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)).Trim;
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);

                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転入前住所方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI);
                                // 転出予定方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI);
                                // 転出確定方書（フル）
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI);
                            }
                            else
                            {
                            }

                            // 住基優先の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                            {
                                // 編集前番地
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)).Trim;
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                // 編集前番地
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KATAGAKI)).Trim;
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI);
                                }
                                else
                                {
                                }
                            }

                            // 消除届出年月日
                            csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOTDKDYMD);
                            // 直近事由コード
                            csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = csDataRow(ABAtenaRirekiEntity.CKINJIYUCD);

                            // *履歴番号 000022 2003/12/04 追加開始
                            // 本籍全国住所コード
                            csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.HON_ZJUSHOCD);
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出予定世帯主名
                            // csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                            // 転出確定世帯主名
                            // csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                // 転出予定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)));
                                // 転出確定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                // 転出予定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI);
                                // 転出確定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                            // 国籍コード
                            csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD);
                            // *履歴番号 000022 2003/12/04 追加終了
                            // *履歴番号 000027 2006/07/31 追加開始
                            if ((strGyomuMei ?? "") == NENKIN_2)
                            {
                                // * 履歴開始 000035 2008/02/15 修正開始
                                // 転入前住所世帯主名
                                // csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)
                                if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                                {
                                    // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                    csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)));
                                }
                                else
                                {
                                    // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                    csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI);
                                }
                                // * 履歴開始 000035 2008/02/15 修正終了
                            }
                            // *履歴番号 000027 2006/07/31 追加終了
                        }

                        // *履歴番号 000030 2007/04/28 追加開始
                        // 介護用サブルーチン取得項目
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                        {
                            // 旧姓
                            csDataNewRow(ABAtena1Entity.KYUSEI) = csDataRow(ABAtenaRirekiEntity.KYUSEI);
                            // 住定異動年月日
                            csDataNewRow(ABAtena1Entity.JUTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.JUTEIIDOYMD);
                            // 住定事由
                            csDataNewRow(ABAtena1Entity.JUTEIJIYU) = csDataRow(ABAtenaRirekiEntity.JUTEIJIYU);
                            // 本籍全国住所コード
                            csDataNewRow(ABAtena1Entity.HON_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.HON_ZJUSHOCD);
                            // 転入前住所郵便番号
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO);
                            // 転入前住所全国住所コード
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD);
                            // 転入前住所住所
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_JUSHO);
                            // 転入前住所番地
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_BANCHI);
                            // 転入前住所方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            // 転出予定郵便番号
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO);
                            // 転出予定全国住所コード
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD);
                            // 転出予定異動年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD);
                            // 転出予定住所
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO);
                            // 転出予定番地
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI);
                            // 転出予定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転入前住所方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI);
                                // 転出予定方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI);
                            }
                            else
                            {
                            }
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出予定世帯主名
                            // csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了
                            // 転出確定郵便番号
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO);
                            // 転出確定全国住所コード
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD);
                            // 転出確定異動年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD);
                            // 転出確定通知年月日
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD);
                            // 転出確定住所
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO);
                            // 転出確定番地
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI);
                            // 転出確定方書
                            strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 転出確定方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI);
                            }
                            else
                            {
                            }
                            // * 履歴開始 000035 2008/02/15 修正開始
                            // 転出確定世帯主名
                            // csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                            if (cAtenaGetPara1.p_strFrnMeishoHenshuKB != "1")
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行う
                                csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(Conversions.ToString(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)));
                            }
                            else
                            {
                                // 外国人の漢字氏名に含まれる括弧で括られた文字列の除去を行わない
                                csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI);
                            }
                            // * 履歴開始 000035 2008/02/15 修正終了

                            // 住基優先の場合
                            if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                            {
                                // 編集前番地
                                csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)).Trim;
                                csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                // 編集前番地
                                csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI);
                                // 編集前方書
                                strWork = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.KATAGAKI)).Trim;
                                csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 編集前方書（フル）
                                    csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI);
                                }
                                else
                                {
                                }
                            }

                            // 消除届出年月日
                            csDataNewRow(ABAtena1Entity.SHOJOTDKDYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOTDKDYMD);
                            // 直近事由コード
                            csDataNewRow(ABAtena1Entity.CKINJIYUCD) = csDataRow(ABAtenaRirekiEntity.CKINJIYUCD);
                            // 国籍コード
                            csDataNewRow(ABAtena1Entity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD);
                            // 登録届出年月日
                            csDataNewRow(ABAtena1Entity.TOROKUTDKDYMD) = csDataRow(ABAtenaRirekiEntity.TOROKUTDKDYMD);
                            // 住定届出年月日
                            csDataNewRow(ABAtena1Entity.JUTEITDKDYMD) = csDataRow(ABAtenaRirekiEntity.JUTEITDKDYMD);
                            // 転出入理由
                            csDataNewRow(ABAtena1Entity.TENSHUTSUNYURIYU) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUNYURIYU);
                            // 市町村コード
                            csDataNewRow(ABAtena1Entity.SHICHOSONCD) = csDataRow(ABAtenaRirekiEntity.SHICHOSONCD);
                            if (!(csDataRow(ABAtenaRirekiEntity.CKINJIYUCD).ToString.Trim == string.Empty) && csDataRow(ABAtenaRirekiEntity.CKINIDOYMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1Entity.CKINIDOYMD) = m_strCknIdobiHenkanParam;
                            }
                            else
                            {
                                // 直近異動年月日
                                csDataNewRow(ABAtena1Entity.CKINIDOYMD) = csDataRow(ABAtenaRirekiEntity.CKINIDOYMD);
                            }
                            // 更新日時
                            csDataNewRow(ABAtena1Entity.KOSHINNICHIJI) = csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI);
                        }
                        // *履歴番号 000030 2007/04/28 追加終了

                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    // *履歴番号 000019 2003/11/19 追加開始
                    // 宛名個別情報用データ作成(本人レコードのみ設定)
                    if ((strGyomuMei ?? "") == KOBETSU & string.IsNullOrEmpty(strDainoKB.Trim()))
                    {

                        // 基礎年金番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KSNENKNNO) = csDataRow(ABAtena1KobetsuEntity.KSNENKNNO);
                        // 年金資格取得年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD);
                        // 年金資格取得種別	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU);
                        // 年金資格取得理由コード	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD);
                        // 年金資格喪失年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD);
                        // 年金資格喪失理由コード	
                        csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD);
                        // 受給年金記号１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1);
                        // 受給年金番号１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO1);
                        // 受給年金種別１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU1);
                        // 受給年金枝番１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1);
                        // 受給年金区分１	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB1);
                        // 受給年金記号２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2);
                        // 受給年金番号２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO2);
                        // 受給年金種別２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU2);
                        // 受給年金枝番２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2);
                        // 受給年金区分２	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB2);
                        // 受給年金記号３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3);
                        // 受給年金番号３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO3);
                        // 受給年金種別３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU3);
                        // 受給年金枝番３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3);
                        // 受給年金区分３	
                        csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB3);
                        // 国保番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHONO);
                        // 国保資格区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB);
                        // 国保資格区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO);
                        // 国保資格区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO);
                        // 国保学遠区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB);
                        // 国保学遠区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO);
                        // 国保学遠区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO);
                        // 国保取得年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD);
                        // 国保喪失年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD);
                        // 国保退職区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB);
                        // 国保退職区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO);
                        // 国保退職区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO);
                        // 国保退職本被区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB);
                        // 国保退職本被区分正式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO);
                        // 国保退職本被区分略式名称	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO);
                        // 国保退職該当年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD);
                        // 国保退職非該当年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD);
                        // 国保保険証記号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO);
                        // 国保保険証番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO);
                        // 印鑑番号	
                        csDataNewRow(ABAtena1KobetsuEntity.INKANNO) = csDataRow(ABAtena1KobetsuEntity.INKANNO);
                        // 印鑑登録区分	
                        csDataNewRow(ABAtena1KobetsuEntity.INKANTOROKUKB) = csDataRow(ABAtena1KobetsuEntity.INKANTOROKUKB);
                        // 選挙資格区分	
                        csDataNewRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB);
                        // 児手被用区分	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB);
                        // 児手開始年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATESTYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATESTYM);
                        // 児手終了年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM);
                        // 介護被保険者番号	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO);
                        // 介護資格取得日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD);
                        // 介護資格喪失日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD);
                        // 介護資格被保険者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB);
                        // 介護住所地特例者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB);
                        // 介護受給者区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB);
                        // 要介護状態区分コード	
                        csDataNewRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD) = csDataRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD);
                        // 要介護状態区分	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKKB);
                        // 介護認定有効開始日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD);
                        // 介護認定有効終了日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD);
                        // 介護受給認定年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD);
                        // 介護受給認定取消年月日	
                        csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD);

                        // *履歴番号 000034 2008/01/15 追加開始
                        if (m_strKobetsuShutokuKB == "1")
                        {
                            // 個別事項取得区分が"1"の場合は後期高齢項目を追加する
                            // 資格区分
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB);
                            // 被保険者番号
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO);
                            // 被保険者資格取得事由コード
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD);
                            // 被保険者資格取得事由名称
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI);
                            // 被保険者資格取得年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD);
                            // 被保険者資格喪失事由コード
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD);
                            // 被保険者資格喪失事由名称
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI);
                            // 被保険者資格喪失年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD);
                            // 保険者番号適用開始年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD);
                            // 保険者番号適用終了年月日
                            csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD);
                        }
                        else
                        {
                            // 個別事項取得区分が値なしの場合は後期高齢項目を追加しない
                        }
                        // *履歴番号 000034 2008/01/15 追加終了

                    }
                    // *履歴番号 000019 2003/11/19 追加終了

                    // *履歴番号 000046 2011/11/07 追加開始
                    // 住基法改正判定
                    if (m_strJukiHokaiseiKB_Param == "1")
                    {
                        // 住民票状態区分
                        csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csDataRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN);
                        // 住居地届出有無フラグ
                        csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csDataRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG);
                        // 本国名
                        csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csDataRow(ABAtenaFZYEntity.HONGOKUMEI);
                        // カナ本国名
                        csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csDataRow(ABAtenaFZYEntity.KANAHONGOKUMEI);
                        // 併記名
                        csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANJIHEIKIMEI);
                        // カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANAHEIKIMEI);
                        // 通称名
                        csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANJITSUSHOMEI);
                        // カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANATSUSHOMEI);
                        // カタカナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI);
                        // 生年月日不詳区分
                        csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csDataRow(ABAtenaFZYEntity.UMAREFUSHOKBN);
                        // 通称名登録（変更）年月日
                        csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csDataRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD);
                        // 在留期間コード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANCD);
                        // 在留期間名称
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO);
                        // 中長期在留者である旨等のコード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHACD);
                        // 中長期在留者である旨等
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO);
                        // 在留カード等番号
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csDataRow(ABAtenaFZYEntity.ZAIRYUCARDNO);
                        // 特別永住者証明書交付年月日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csDataRow(ABAtenaFZYEntity.KOFUYMD);
                        // 特別永住者証明書交付予定期間開始日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEISTYMD);
                        // 特定永住者証明書交付予定期間終了日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD);
                        // 住基対象者（第30条45非該当）消除異動年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
                        // 住基対象者（第30条45非該当）消除事由コード
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
                        // 住基対象者（第30条45非該当）消除事由
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU);
                        // 住基対象者（第30条45非該当）消除届出年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
                        // 住基対象者（第30条45非該当）消除届出通知区分
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
                        // 外国人世帯主名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSMEI);
                        // 外国人世帯主カナ名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI);
                        // 世帯主併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSHEIKIMEI);
                        // 世帯主カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI);
                        // 世帯主通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI);
                        // 世帯主カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000046 2011/11/07 追加終了

                    // *履歴番号 000048 2014/04/28 追加開始
                    // 共通番号判定
                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                    {
                        // 空白除去した値を設定する。
                        csDataNewRow(ABMyNumberEntity.MYNUMBER) = csDataRow(ABMyNumberEntity.MYNUMBER).ToString.Trim;
                    }
                    else
                    {
                        // noop
                    }
                    // *履歴番号 000048 2014/04/28 追加終了

                    if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        // 世帯主氏名優先区分
                        csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
                        // 氏名優先項目
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.SHIMEIYUSENKB);
                        // 旧氏
                        csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = csDataRow(ABAtenaRirekiFZYEntity.RESERVE7);
                        // カナ旧氏
                        csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = csDataRow(ABAtenaRirekiFZYEntity.RESERVE8);
                        // 氏名フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = csDataRow(ABAtenaRirekiHyojunEntity.SHIMEIKANAKAKUNINFG);
                        // 旧氏フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = csDataRow(ABAtenaRirekiHyojunEntity.KYUUJIKANAKAKUNINFG);
                        // 通称フリガナ確認フラグ
                        csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = csDataRow(ABAtenaRirekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG);
                        // 生年月日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.UMAREBIFUSHOPTN);
                        // 不詳生年月日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOUMAREBI);
                        // 記載事由
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNKISAIJIYUCD);
                        // 記載年月日
                        csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csDataRow(ABAtenaRirekiHyojunEntity.KISAIYMD);
                        // 消除事由
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNSHOJOJIYUCD);

                        if ((csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTONAI_KOJIN || csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) && !(csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD).ToString.Trim == string.Empty))
                        {
                            if (csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD).ToString.Trim == string.Empty)
                            {
                                csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = m_strShojoIdoWmdHenkan;
                            }
                            else
                            {
                                csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD);
                            }
                        }
                        else
                        {
                            // 消除異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD);
                        }
                        // 消除異動日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOBIFUSHOPTN);
                        // 不詳消除異動日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI);

                        if (!(csDataRow(ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI).ToString.Trim == string.Empty) && csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOWMD).ToString.Trim == string.Empty)
                        {
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = m_strCknIdoWmdHenkan;
                        }
                        else
                        {
                            // 直近異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOWMD);
                        }
                        // 直近異動日不詳パターン
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOBIFUSHOPTN);
                        // 不詳直近異動日
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI);
                        // 事実上の世帯主
                        csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csDataRow(ABAtenaRirekiHyojunEntity.JIJITSUSTAINUSMEI);
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            // 住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSONCD);
                            // 住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIMACHIAZACD);
                            // 住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.JUKITODOFUKEN);
                            // 住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSON);
                            // 住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIMACHIAZA);
                        }
                        else
                        {
                            // 住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.SHIKUCHOSONCD);
                            // 住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.MACHIAZACD);
                            // 住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TODOFUKEN);
                            // 住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.SHIKUCHOSON);
                            // 住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.MACHIAZA);
                        }
                        if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                        {
                            // 本籍_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.HON_SHIKUCHOSONCD);
                            // 本籍_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.HON_MACHIAZACD);
                            // 本籍_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.HON_TODOFUKEN);
                            // 本籍_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.HON_SHIKUGUNCHOSON);
                            // 本籍_町字
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.HON_MACHIAZA);
                        }
                        if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && (strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                        {
                            // 国籍コード
                            csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD);
                        }
                        if ((strGyomuMei ?? "") == NENKIN | (strGyomuMei ?? "") == NENKIN_2)
                        {
                            // 転入前住所_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                            // 転入前町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD);
                            // 転入前住所_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN);
                            // 転入前住所_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                            // 転入前住所_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA);
                            // 転入前住所_国名コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD);
                            // 転入前住所_国名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI);
                            // 転入前住所_国外住所
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                            // 転出確定_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                            // 転出確定町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                            // 転出確定_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                            // 転出確定_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                            // 転出確定_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                            // 転出予定_市区町村コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                            // 転出予定町字コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                            // 転出予定_都道府県
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                            // 転出予定_市区郡町村名
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                            // 転出予定_町字
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                            // 転出予定_国名コード
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                            // 転出予定_国名等
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                            // 転出予定_国外住所
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                        }
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                        {
                            // 転入前住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD);
                            // 転入前町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD);
                            // 転入前住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN);
                            // 転入前住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON);
                            // 転入前住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA);
                            // 転入前住所_国名コード
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD);
                            // 転入前住所_国名
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI);
                            // 転入前住所_国外住所
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO);
                            // 転出確定_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD);
                            // 転出確定町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD);
                            // 転出確定_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN);
                            // 転出確定_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON);
                            // 転出確定_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA);
                            // 転出予定_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD);
                            // 転出予定町字コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD);
                            // 転出予定_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN);
                            // 転出予定_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON);
                            // 転出予定_町字
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA);
                            // 転出予定_国名コード
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD);
                            // 転出予定_国名等
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI);
                            // 転出予定_国外住所
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO);
                        }
                        else
                        {
                        }
                        // 法第30条46又は47区分
                        csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.HODAI30JO46MATAHA47KB);
                        // 在留カード等番号区分
                        csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csDataRow(ABAtenaRirekiFZYHyojunEntity.ZAIRYUCARDNOKBN);
                        // 住居地補正コード
                        csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csDataRow(ABAtenaRirekiFZYHyojunEntity.JUKYOCHIHOSEICD);
                        // 直近届出通知区分
                        csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = csDataRow(ABAtenaRirekiEntity.CKINTDKDTUCIKB);
                        // 版番号
                        csDataNewRow(ABAtena1HyojunEntity.HANNO) = csDataRow(ABAtenaRirekiEntity.HANNO);
                        // 改製年月日
                        csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = csDataRow(ABAtenaRirekiEntity.KAISEIYMD);
                        // 異動区分
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNIDOKB);
                        // 入力場所コード
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = csDataRow(ABAtenaRirekiHyojunEntity.NYURYOKUBASHOCD);
                        // 入力場所表記
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = csDataRow(ABAtenaRirekiHyojunEntity.NYURYOKUBASHO);
                        if ((strGyomuMei ?? "") == KOBETSU & string.IsNullOrEmpty(strDainoKB.Trim()))
                        {
                            // 介護_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB);
                            // 国保_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB);
                            // 年金_被保険者該当有無
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB);
                            // 年金_種別変更年月日
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD);
                            // 選挙_状態区分
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = csDataRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN);
                            if (m_strKobetsuShutokuKB == "1")
                            {
                                // 後期高齢_被保険者該当有無
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB);
                            }
                        }
                        // 連絡先区分（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = string.Empty;
                        // 連絡先名
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = string.Empty;
                        // 連絡先1（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = string.Empty;
                        // 連絡先2（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = string.Empty;
                        // 連絡先3（連絡先）
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = string.Empty;
                        // 連絡先種別1
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = string.Empty;
                        // 連絡先種別2
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = string.Empty;
                        // 連絡先種別3
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = string.Empty;
                        // * 履歴番号 000051 2023/10/19 修正開始
                        // If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                        if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2 && csDataRow.Table.Columns.Contains(ABFugenjuJohoEntity.FUGENJUKB))
                        {
                            // * 履歴番号 000051 2023/10/19 修正終了
                            // 不現住区分
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = csDataRow(ABFugenjuJohoEntity.FUGENJUKB);
                            // 不現住だった住所_郵便番号
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO);
                            // 不現住だった住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD);
                            // 不現住だった住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD);
                            // 不現住だった住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN);
                            // 不現住だった住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON);
                            // 不現住だった住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA);
                            // 不現住だった住所_番地号表記
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI);
                            // 不現住だった住所_方書
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI);
                            // 不現住だった住所_方書_フリガナ
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI);
                            // 不現住情報（対象者区分）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN);
                            // 不現住情報（対象者氏名）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI);
                            // 不現住情報（生年月日）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD);
                            // 不現住情報（性別）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU);
                            // 居住不明年月日
                            csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = csDataRow(ABFugenjuJohoEntity.KYOJUFUMEI_YMD);
                            // 不現住情報（備考）
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_BIKO);
                        }
                        else
                        {
                        }
                        if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                        {
                            // 番号法更新区分
                            csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csDataRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB);
                        }
                        // * 履歴番号 000051 2023/10/19 修正開始
                        // If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                        if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1 && (strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2 && csDataRow.Table.Columns.Contains(ABDENSHISHOMEISHOMSTEntity.SERIALNO))
                        {
                            // * 履歴番号 000051 2023/10/19 修正終了
                            // シリアル番号
                            csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = csDataRow(ABDENSHISHOMEISHOMSTEntity.SERIALNO);
                        }
                        // 標準準拠異動事由コード
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNIDOJIYUCD);
                        if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                        {
                            // 連絡先区分（送付先）
                            csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = string.Empty;
                            // 送付先区分
                            csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = string.Empty;
                        }
                        else
                        {
                        }

                        strAtenaDataKB = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)).Trim;
                        strAtenaDataSHU = Conversions.ToString(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)).Trim;
                        m_cABHyojunkaCdHenshuB.HenshuHyojunkaCd(strAtenaDataKB, strAtenaDataSHU);
                        // 住民区分
                        csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = m_cABHyojunkaCdHenshuB.p_strJuminKbn;
                        // 住民種別
                        csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = m_cABHyojunkaCdHenshuB.p_strJuminShubetsu;
                        // 住民状態
                        csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = m_cABHyojunkaCdHenshuB.p_strJuminJotai;
                        if (cAtenaGetPara1.p_strJukiJutogaiKB == "1")
                        {
                            // 番地枝番数値
                            csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIBANCHIEDABANSUCHI);
                        }
                        else
                        {
                            // 番地枝番数値
                            csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaRirekiHyojunEntity.BANCHIEDABANSUCHI);
                        }
                    }
                    else
                    {
                        // noop
                    }

                    // データレコードの追加
                    csDataTable.Rows.Add(csDataNewRow);

                    // *履歴番号 000017 2003/10/09 修正終了

                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            return csAtena1;

        }
        #endregion

        #region  送付先編集(SofusakiHenshu) 
        // *履歴番号 000019 2003/11/19 追加開始
        // ************************************************************************************************
        // * メソッド名     送付先編集
        // * 
        // * 構文           Public Function SofusakiHenshu(ByVal csAtena1 As DataSet, _
        // *                                              ByVal csSfskEntity As DataSet, _
        // *                                              ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           csAtena1              : 宛名履歴データ
        // *               csSfskEntity           : 送付先データ
        // *               cAtenaGetPara1         : 宛名取得パラメータ
        // * 
        // * 戻り値         DataSet(ABAtena12)    : 取得した宛名情報
        // ************************************************************************************************
        public DataSet SofusakiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtena1, DataSet csSfskEntity)
        {
            return SofusakiHenshu(cAtenaGetPara1, csAtena1, csSfskEntity, string.Empty);
        }
        // *履歴番号 000019 2003/11/19 追加終了

        // ************************************************************************************************
        // * メソッド名     送付先編集
        // * 
        // * 構文           Public Function SofusakiHenshu(ByVal csAtena1 As DataSet, _
        // *                                              ByVal csSfskEntity As DataSet, _
        // *                                              ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
        // * 
        // * 機能　　    　 編集宛名データを作成する
        // * 
        // * 引数           csAtena1              : 宛名取得データ
        // *               csSfskEntity           : 送付先データ
        // *               cAtenaGetPara1         : 宛名取得パラメータ
        // *               strGyomuMei            : 業務名
        // * 
        // * 戻り値         DataSet(ABAtena12)    : 取得した宛名情報
        // ************************************************************************************************
        // *履歴番号 000019 2003/11/19 修正開始
        // Public Overloads Function SofusakiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, _
        // ByVal csAtena1 As DataSet, _
        // ByVal csSfskEntity As DataSet) As DataSet
        [SecuritySafeCritical]
        private DataSet SofusakiHenshu(ABAtenaGetPara1XClass cAtenaGetPara1, DataSet csAtena1, DataSet csSfskEntity, string strGyomuMei)
        {
            // *履歴番号 000019 2003/11/19 修正終了
            const string THIS_METHOD_NAME = "SofusakiHenshu";
            // Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cuUSSCityInfo As USSCityInfoClass               '市町村情報管理クラス
            // * 履歴番号 000023 2004/08/27 削除終了
            DataTable csDataTable;                          // 宛名情報入力Row
            DataSet csAtena12;                            // 宛名情報(ABAtena1)
            DataRow csDataNewRow;                         // 宛名情報出力Row
            DataRow csSfskRow;                            // 送付先DataRow
                                                          // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                                                          // Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '管内管外クラス
                                                          // * 履歴番号 000023 2004/08/27 削除終了
            string strHenshuJusho;                        // 編集住所
                                                          // *履歴番号 000008 2003/03/17 追加開始
                                                          // *履歴番号 000016 2003/08/22 削除開始
                                                          // Dim cURKanriJohoB As URKANRIJOHOBClass              '管理情報取得クラス
                                                          // *履歴番号 000016 2003/08/22 削除終了
            SofuJushoGyoseikuType cSofuJushoGyoseikuType;
            var strJushoHenshu3 = default(string);                       // 住所編集３
            var strJushoHenshu4 = default(string);                       // 住所編集４
                                                                         // *履歴番号 000008 2003/03/17 追加終了
                                                                         // *履歴番号 000019 2003/11/19 追加開始
            DataTable dsAtena1Table;                      // 宛名取得データTable
                                                          // *履歴番号 000019 2003/11/19 追加終了
                                                          // * 履歴番号 000029 2007/01/25 追加開始
            URBANCHICDMSTBClass crBanchiCdMstB;           // UR番地コードマスタクラス
            string[] strBanchiCD;                         // 番地コード取得用配列
            var strMotoBanchiCD = default(string[]);                     // 変更前番地コード
                                                                         // * 履歴番号 000029 2007/01/25 追加終了
                                                                         // *履歴番号 000037 2008/11/17 追加開始
            int intLoop;                              // ループカウンタ
                                                      // *履歴番号 000037 2008/11/17 追加終了
            string strWork;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 'エラー処理クラスのインスタンス作成
                // '*履歴番号 000010  2003/03/27 修正開始
                // 'cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
                // cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // '*履歴番号 000010  2003/03/27 修正終了

                // *履歴番号 000019 2003/11/19 修正開始
                // 'カラム情報作成
                // csAtena12 = New DataSet()
                // csAtena12.Tables.Add(Me.CreateAtena1Columns())

                // カラム情報作成
                switch (strGyomuMei ?? "")
                {
                    // *履歴番号 000027 2006/07/31 修正開始
                    case NENKIN:
                    case NENKIN_2:    // 年金宛名情報
                        {
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateNenkinAtenaHyojunColumns(strGyomuMei);
                                dsAtena1Table = csAtena1.Tables(ABNenkinAtenaHyojunEntity.TABLE_NAME);
                            }
                            else
                            {
                                csDataTable = CreateNenkinAtenaColumns(strGyomuMei);
                                // Case NENKIN     ' 年金宛名情報
                                // csDataTable = Me.CreateNenkinAtenaColumns()
                                // *履歴番号 000027 2006/07/31 修正終了
                                dsAtena1Table = csAtena1.Tables(ABNenkinAtenaEntity.TABLE_NAME);
                            }

                            break;
                        }
                    case KOBETSU:    // 宛名個別情報
                        {
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1KobetsuHyojunColumns();
                                dsAtena1Table = csAtena1.Tables(ABAtena1KobetsuHyojunEntity.TABLE_NAME);
                            }
                            else
                            {
                                csDataTable = CreateAtena1KobetsuColumns();
                                dsAtena1Table = csAtena1.Tables(ABAtena1KobetsuEntity.TABLE_NAME);
                            }       // 宛名情報

                            break;
                        }

                    default:
                        {
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                csDataTable = CreateAtena1HyojunColumns();
                                dsAtena1Table = csAtena1.Tables(ABAtena1HyojunEntity.TABLE_NAME);
                            }
                            else
                            {
                                csDataTable = CreateAtena1Columns();
                                dsAtena1Table = csAtena1.Tables(ABAtena1Entity.TABLE_NAME);
                            }

                            break;
                        }
                }
                csAtena12 = new DataSet();
                csAtena12.Tables.Add(csDataTable);
                // *履歴番号 000019 2003/11/19 修正終了

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // 市町村情報のインスタンス作成
                // cuUSSCityInfo = New USSCityInfoClass()

                // 管内管外のインスタンス作成
                // cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // *履歴番号 000016 2003/08/22 削除開始
                // '管理情報取得Ｂのインスタンス作成
                // cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // *履歴番号 000016 2003/08/22 削除終了
                // *履歴番号 000008 2003/03/17 追加終了

                // * 履歴番号 000029 2007/01/25 追加開始
                // UR番地コードマスタクラスのインスタンス生成
                crBanchiCdMstB = new URBANCHICDMSTBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass);
                // * 履歴番号 000029 2007/01/25 追加終了

                // *履歴番号 000007 2003/03/17 追加開始
                // パラメータのチェック
                CheckColumnValue(cAtenaGetPara1);
                // *履歴番号 000007 2003/03/17 追加終了

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // 住所編集１が"1"且つ住所編集２が"1"の場合
                // If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then
                // '直近の市町村名を取得する
                // 'm_cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
                // End If
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000008 2003/03/17 追加開始
                // 住所編集１が"1"且つ住所編集３が""の場合
                if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu3 == string.Empty)
                {
                    // *履歴番号 000016 2003/08/22 修正開始
                    // cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                    cSofuJushoGyoseikuType = GetSofuJushoGyoseikuType();
                    // *履歴番号 000016 2003/08/22 修正終了
                    switch (cSofuJushoGyoseikuType)
                    {
                        case var @case when @case == SofuJushoGyoseikuType.Jusho_Banchi:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case1 when case1 == SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "1";
                                strJushoHenshu4 = "1";
                                break;
                            }
                        case var case2 when case2 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "";
                                break;
                            }
                        case var case3 when case3 == SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki:
                            {
                                strJushoHenshu3 = "5";
                                strJushoHenshu4 = "1";
                                break;
                            }
                    }
                }
                else
                {
                    strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3;
                    strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4;
                }
                // *履歴番号 000008 2003/03/17 追加終了

                // 編集宛名データを作成する
                // *履歴番号 000017 2003/10/09 修正開始
                // For Each csAtena1Row In csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows
                // csDataNewRow = csAtena12.Tables(ABAtena1Entity.TABLE_NAME).NewRow

                foreach (DataRow csAtena1Row in dsAtena1Table.Rows)
                {
                    csDataNewRow = csDataTable.NewRow();
                    // *履歴番号 000019 2003/11/19 修正終了

                    // *履歴番号 000037 2008/11/17 追加開始
                    foreach (DataColumn csColumn in csDataNewRow.Table.Columns)
                        csDataNewRow[csColumn] = string.Empty;
                    // *履歴番号 000037 2008/11/17 修正終了

                    // 送付先データ検索
                    csSfskRow = null;
                    // *履歴番号 000002 2003/02/20 修正開始
                    // For Each csDataRow In csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows
                    // '*履歴番号 000001 2003/02/19 修正開始
                    // 'If CType(csAtena1Row(ABAtena1Entity.JUMINCD), String).Trim = CType(csDataRow(ABSfskEntity.JUMINCD), String).Trim _
                    // '        And CType(csAtena1Row(ABAtena1Entity.GYOMUCD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUCD), String).Trim _
                    // '        And CType(csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String).Trim Then
                    // If CType(csAtena1Row(ABAtena1Entity.JUMINCD), String).Trim = CType(csDataRow(ABSfskEntity.JUMINCD), String).Trim _
                    // And CType(csAtena1Row(ABAtena1Entity.GYOMUCD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUCD), String).Trim _
                    // And CType(csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String).Trim Then
                    // '*履歴番号 000001 2003/02/19 修正終了
                    // csSfskRow = csDataRow
                    // Exit For
                    // End If
                    // Next csDataRow

                    // 送付先データは0件又は1件来る
                    if (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 0)
                    {
                        csSfskRow = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0);
                    }
                    // *履歴番号 000002 2003/02/20 修正終了

                    // 送付先が存在しない場合
                    if (csSfskRow is null)
                    {

                        csDataNewRow.ItemArray = csAtena1Row.ItemArray;

                        // 住民コード
                        csDataNewRow(ABAtena1Entity.JUMINCD) = csAtena1Row(ABAtena1Entity.JUMINCD);

                        // 代納区分（本人マスタの代納区分が"00"の場合"40"、それ以外は"50"）
                        if (Conversions.ToString(csAtena1Row(ABAtena1Entity.DAINOKB)) == "00")
                        {
                            // 代納区分
                            csDataNewRow(ABAtena1Entity.DAINOKB) = "40";
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.DAINOKB) = "50";
                        }

                        // 代納区分名称
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;

                        // 代納区分略式名称
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {

                            // *履歴番号 000005  2003/02/25 修正開始
                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = string.Empty;

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = string.Empty;
                            // *履歴番号 000005  2003/02/25 修正終了
                        }
                    }
                    // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                    else
                    {

                        // 住民コード
                        csDataNewRow(ABAtena1Entity.JUMINCD) = csAtena1Row(ABAtena1Entity.JUMINCD);

                        // 代納区分（本人マスタの代納区分が"00"の場合"40"、それ以外は"50"）
                        if (Conversions.ToString(csAtena1Row(ABAtena1Entity.DAINOKB)) == "00")
                        {
                            // 代納区分
                            csDataNewRow(ABAtena1Entity.DAINOKB) = "40";
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.DAINOKB) = "50";
                        }

                        // 代納区分名称
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = string.Empty;

                        // 代納区分略式名称
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = string.Empty;

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {

                            // *履歴番号 000003 2003/02/21 修正開始
                            // '業務コード
                            // csDataNewRow(ABAtena1Entity.GYOMUCD) = csAtena1Row(ABAtena1Entity.GYOMUCD)
                            // '業務内種別コード
                            // csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD)

                            // 業務コード
                            csDataNewRow(ABAtena1Entity.GYOMUCD) = csSfskRow(ABSfskEntity.GYOMUCD);

                            // 業務内種別コード
                            csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = csSfskRow(ABSfskEntity.GYOMUNAISHU_CD);
                            // *履歴番号 000003 2003/02/21 修正終了

                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                        // 旧市町村コード
                        csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csAtena1Row(ABAtena1Entity.KYUSHICHOSONCD);

                        // 世帯コード
                        csDataNewRow(ABAtena1Entity.STAICD) = csAtena1Row(ABAtena1Entity.STAICD);

                        // 宛名データ区分
                        csDataNewRow(ABAtena1Entity.ATENADATAKB) = csSfskRow(ABSfskEntity.SFSKDATAKB);

                        // 宛名データ種別
                        csDataNewRow(ABAtena1Entity.ATENADATASHU) = string.Empty;

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {

                            // 編集種別
                            csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = string.Empty;

                            // 編集種別略称
                            csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = string.Empty;

                            // 検索用カナ姓名
                            csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = string.Empty;

                            // 検索用カナ姓
                            csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = string.Empty;

                            // 検索用カナ名
                            csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = string.Empty;

                            // 検索用漢字名称
                            csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = string.Empty;
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                        // 編集カナ名称
                        strWork = Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKANAMEISHO)).Trim;
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO);

                        // 編集漢字名称
                        strWork = Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKANJIMEISHO)).Trim;
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO);

                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            // 編集カナ名称（フル）
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = csSfskRow(ABSfskEntity.SFSKKANAMEISHO);

                            // 編集漢字名称（フル）
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = csSfskRow(ABSfskEntity.SFSKKANJIMEISHO);
                        }
                        else
                        {
                        }

                        // 生年月日
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = string.Empty;

                        // 生和暦年月日
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = string.Empty;

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            // 生表示年月日
                            csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = string.Empty;

                            // 生証明年月日
                            csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = string.Empty;

                            // 性別コード
                            csDataNewRow(ABAtena1Entity.SEIBETSUCD) = string.Empty;

                            // 性別
                            csDataNewRow(ABAtena1Entity.SEIBETSU) = string.Empty;

                            // 編集続柄コード
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = string.Empty;

                            // 編集続柄
                            csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = string.Empty;

                            // 法人代表者名
                            csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = string.Empty;
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                        // 個人法人区分
                        csDataNewRow(ABAtena1Entity.KJNHJNKB) = string.Empty;
                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {

                            // 個人法人区分名称
                            csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = string.Empty;

                            // 管内管外区分名称
                            csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB)));
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                        // 管内管外区分
                        csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB);

                        // 郵便番号
                        csDataNewRow(ABAtena1Entity.YUBINNO) = csSfskRow(ABSfskEntity.SFSKYUBINNO);

                        // 住所コード
                        csDataNewRow(ABAtena1Entity.JUSHOCD) = csSfskRow(ABSfskEntity.SFSKZJUSHOCD);

                        // 住所
                        csDataNewRow(ABAtena1Entity.JUSHO) = csSfskRow(ABSfskEntity.SFSKJUSHO);

                        // 編集住所名
                        if (cAtenaGetPara1.p_strJushoHenshu1 == string.Empty)
                        {
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }

                        else if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            strHenshuJusho = string.Empty;
                            if (cAtenaGetPara1.p_strJushoHenshu2 == "1")
                            {

                                // 管内のみ市町村名を付加する
                                if (Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB)) == "1")
                                {
                                    strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0);
                                }
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // Select Case cAtenaGetPara1.p_strJushoHenshu3
                            switch (strJushoHenshu3 ?? "")
                            {
                                // *履歴番号 000008 2003/03/17 修正終了
                                // * 履歴番号 000028 2007/01/15 修正開始
                                case "1":
                                case "6":   // 住所＋番地
                                    {
                                        // Case "1"    '住所＋番地
                                        // * 履歴番号 000028 2007/01/15 修正終了
                                        strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        break;
                                    }
                                case "2":    // 行政区＋番地
                                    {
                                        // *履歴番号 000009 2003/03/17 修正開始
                                        // strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }
                                        else
                                        {
                                            // 行政区＋番地
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }

                                        break;
                                    }
                                // *履歴番号 000009 2003/03/17 修正終了
                                case "3":    // 住所＋（行政区）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                        // + CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd

                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }
                                        else
                                        {
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + "（" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd + "）" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;



                                        }

                                        break;
                                    }
                                // *履歴番号 000004  2003/02/25 修正終了
                                case "4":    // 行政区＋（住所）＋番地
                                    {
                                        // *履歴番号 000004  2003/02/25 修正開始
                                        // strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                        // + CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                        // + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd

                                        // 住所が存在しない場合
                                        if (new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd ?? new char[0]) == "")
                                        {
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }
                                        // *履歴番号 000009 2003/03/17 追加開始
                                        // 行政区名が存在しない場合
                                        else if (new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }
                                        // *履歴番号 000009 2003/03/17 追加終了
                                        else
                                        {
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd + "（" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + "）" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;



                                        }

                                        break;
                                    }
                                // *履歴番号 000004 2003/02/25 修正終了
                                // *履歴番号 000009 2003/03/17 追加開始
                                case "5":    // 行政区＋△＋番地
                                    {
                                        // 行政区名が存在しない場合
                                        if (new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd ?? new char[0]) == "")
                                        {
                                            // 住所＋番地
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKJUSHO)).TrimEnd + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;
                                        }
                                        else
                                        {
                                            // 行政区＋△＋番地
                                            strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd + "　" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).TrimEnd;

                                        }

                                        break;
                                    }
                                    // *履歴番号 000009 2003/03/17 追加終了
                            }
                            // *履歴番号 000008 2003/03/17 修正開始
                            // If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                            // * 履歴番号 000028 2007/01/15 修正開始
                            if (strJushoHenshu4 == "1" && new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKATAGAKI)).Trim ?? new char[0]) != "")
                            {
                                // If strJushoHenshu4 = "1" Then
                                // * 履歴番号 000028 2007/01/15 修正終了
                                // *履歴番号 000008 2003/03/17 修正終了
                                // *履歴番号 000004 2003/02/25 修正開始
                                // strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKKATAGAKI), String).TrimEnd

                                strHenshuJusho += "　" + Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKATAGAKI)).TrimEnd;
                                // *履歴番号 000004 2003/02/25 修正終了
                            }
                            // * 履歴番号 000028 2007/01/15 追加開始
                            // 住所編集３パラメータが６、且つ行政区名があるときは、編集住所に（行政区）を追加する
                            if (strJushoHenshu3 == "6" && new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).Trim ?? new char[0]) != "")
                            {
                                strHenshuJusho += "（";
                                strHenshuJusho += Conversions.ToString(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)).TrimEnd;
                                strHenshuJusho += "）";
                            }
                            // * 履歴番号 000028 2007/01/15 追加終了
                            // * 履歴番号 000032 2007/07/09 修正開始
                            if (strHenshuJusho.RLength >= 160)
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.RSubstring(0, 160);
                            }
                            // If strHenshuJusho.Length >= 80 Then
                            // csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            // * 履歴番号 000032 2007/07/09 修正終了
                            else
                            {
                                csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho;
                            }
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 編集住所名（フル）
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = strHenshuJusho;
                            }
                            else
                            {
                            }
                        }

                        // * 履歴番号 000029 2007/01/25 修正開始
                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            // 番地コード１
                            csDataNewRow(ABAtena1Entity.BANCHICD1) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD1);

                            // 番地コード２
                            csDataNewRow(ABAtena1Entity.BANCHICD2) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD2);

                            // 番地コード３
                            csDataNewRow(ABAtena1Entity.BANCHICD3) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD3);
                        }
                        else if (csSfskRow(ABSfskEntity.SFSKBANCHI) == null == false && new string(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)).Trim ?? new char[0]) != "")
                        {
                            // 番地情報がある場合は、URのメソッドから番地を取得する
                            // 番地コード取得メソッドを呼び出す
                            strBanchiCD = crBanchiCdMstB.GetBanchiCd(Conversions.ToString(csSfskRow(ABSfskEntity.SFSKBANCHI)), strMotoBanchiCD, true);

                            // 取得した番地コード配列にNothingの項目がある場合はString.Emptyをセットする
                            var loopTo = strBanchiCD.Length - 1;
                            for (intLoop = 0; intLoop <= loopTo; intLoop++)
                            {
                                if (strBanchiCD[intLoop] == null)
                                {
                                    strBanchiCD[intLoop] = string.Empty;
                                }
                            }

                            // 番地コード１
                            csDataNewRow(ABAtena1Entity.BANCHICD1) = strBanchiCD[0];

                            // 番地コード２
                            csDataNewRow(ABAtena1Entity.BANCHICD2) = strBanchiCD[1];

                            // 番地コード３
                            csDataNewRow(ABAtena1Entity.BANCHICD3) = strBanchiCD[2];
                        }
                        else
                        {
                            // 番地コード１
                            csDataNewRow(ABAtena1Entity.BANCHICD1) = string.Empty;

                            // 番地コード２
                            csDataNewRow(ABAtena1Entity.BANCHICD2) = string.Empty;

                            // 番地コード３
                            csDataNewRow(ABAtena1Entity.BANCHICD3) = string.Empty;
                        }
                        // * 履歴番号 000029 2007/01/25 修正終了

                        // 番地
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1")
                        {
                            // 住所編集ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.BANCHI) = string.Empty;
                        }
                        else
                        {
                            csDataNewRow(ABAtena1Entity.BANCHI) = csSfskRow(ABSfskEntity.SFSKBANCHI);
                        }

                        // 方書フラグ
                        csDataNewRow(ABAtena1Entity.KATAGAKIFG) = string.Empty;

                        // 方書コード
                        csDataNewRow(ABAtena1Entity.KATAGAKICD) = string.Empty;

                        // 方書
                        if (cAtenaGetPara1.p_strJushoHenshu1 == "1" & cAtenaGetPara1.p_strJushoHenshu4 == "1")
                        {
                            // 方書付加ありの場合は、Null
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = string.Empty;
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = string.Empty;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            strWork = Conversions.ToString(csSfskRow(ABSfskEntity.SFSKKATAGAKI)).Trim;
                            csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI);
                            if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                            {
                                // 方書（フル）
                                csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csSfskRow(ABSfskEntity.SFSKKATAGAKI);
                            }
                            else
                            {
                            }
                        }

                        // 連絡先１
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csSfskRow(ABSfskEntity.SFSKRENRAKUSAKI1);

                        // 連絡先２
                        csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csSfskRow(ABSfskEntity.SFSKRENRAKUSAKI2);

                        // 行政区コード
                        csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csSfskRow(ABSfskEntity.SFSKGYOSEIKUCD);

                        // 行政区名
                        csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI);

                        // 地区コード１
                        csDataNewRow(ABAtena1Entity.CHIKUCD1) = csSfskRow(ABSfskEntity.SFSKCHIKUCD1);

                        // 地区１
                        csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI1);

                        // 地区コード２
                        csDataNewRow(ABAtena1Entity.CHIKUCD2) = csSfskRow(ABSfskEntity.SFSKCHIKUCD2);

                        // 地区２
                        csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI2);

                        // 地区コード３
                        csDataNewRow(ABAtena1Entity.CHIKUCD3) = csSfskRow(ABSfskEntity.SFSKCHIKUCD3);

                        // 地区３
                        csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI3);

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {

                            // 登録異動年月日
                            csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csAtena1Row(ABAtena1Entity.TOROKUIDOYMD);

                            // 登録事由コード
                            csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csAtena1Row(ABAtena1Entity.TOROKUJIYUCD);

                            // 登録事由
                            csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csAtena1Row(ABAtena1Entity.TOROKUJIYU);

                            // 消除異動年月日
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csAtena1Row(ABAtena1Entity.SHOJOIDOYMD);

                            // 消除事由コード
                            csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csAtena1Row(ABAtena1Entity.SHOJOJIYUCD);

                            // 消除事由名称
                            csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csAtena1Row(ABAtena1Entity.SHOJOJIYU);

                            // 編集世帯主住民コード
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csAtena1Row(ABAtena1Entity.HENSHUNUSHIJUMINCD);
                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                        // 編集カナ世帯主名
                        csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csAtena1Row(ABAtena1Entity.HENSHUKANANUSHIMEI);

                        // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                        if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                        {
                            // 編集漢字世帯主名
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csAtena1Row(ABAtena1Entity.HENSHUNUSHIMEI);

                            // 表示順（第２住民票表示順がある場合は、第２住民票表示順）
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = csAtena1Row(ABAtena1Entity.HYOJIJUN);

                            // *履歴番号 000012 2003/04/18 追加開始
                            // 続柄コード
                            csDataNewRow(ABAtena1Entity.ZOKUGARACD) = string.Empty;
                            // 続柄
                            csDataNewRow(ABAtena1Entity.ZOKUGARA) = string.Empty;

                            // カナ名称２
                            csDataNewRow(ABAtena1Entity.KANAMEISHO2) = string.Empty;
                            // 漢字名称２
                            csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = string.Empty;

                            // 籍番号
                            csDataNewRow(ABAtena1Entity.SEKINO) = string.Empty;
                            // *履歴番号 000012 2003/04/18 追加終了


                            // *履歴番号 000030 2007/04/28 追加開始
                            // 介護用サブルーチン取得項目
                            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                            {
                                // 連絡先業務コード
                                csDataNewRow(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD) = string.Empty;
                                // 旧姓
                                csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = string.Empty;
                                // 住定異動年月日
                                csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = string.Empty;
                                // 住定事由
                                csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = string.Empty;
                                // 本籍全国住所コード
                                csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = string.Empty;
                                // 転入前住所郵便番号
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = string.Empty;
                                // 転入前住所全国住所コード
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = string.Empty;
                                // 転入前住所住所
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = string.Empty;
                                // 転入前住所番地
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = string.Empty;
                                // 転入前住所方書
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = string.Empty;
                                // 転出予定郵便番号
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = string.Empty;
                                // 転出予定全国住所コード
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = string.Empty;
                                // 転出予定異動年月日
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = string.Empty;
                                // 転出予定住所
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = string.Empty;
                                // 転出予定番地
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = string.Empty;
                                // 転出予定方書
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = string.Empty;
                                // 転出予定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = string.Empty;
                                // 転出確定郵便番号
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = string.Empty;
                                // 転出確定全国住所コード
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = string.Empty;
                                // 転出確定異動年月日
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = string.Empty;
                                // 転出確定通知年月日
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = string.Empty;
                                // 転出確定住所
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = string.Empty;
                                // 転出確定番地
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = string.Empty;
                                // 転出確定方書
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = string.Empty;
                                // 転出確定世帯主名
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = string.Empty;
                                // 編集前番地
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = string.Empty;
                                // 編集前方書
                                csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = string.Empty;
                                // 消除届出年月日
                                csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = string.Empty;
                                // 直近事由コード
                                csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = string.Empty;
                                // 国籍コード
                                csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = string.Empty;
                                // 登録届出年月日
                                csDataNewRow(ABNenkinAtenaEntity.TOROKUTDKDYMD) = string.Empty;
                                // 住定届出年月日
                                csDataNewRow(ABNenkinAtenaEntity.JUTEITDKDYMD) = string.Empty;
                                // 転出入理由
                                csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUNYURIYU) = string.Empty;
                                // 市町村コード
                                csDataNewRow(ABNenkinAtenaEntity.SHICHOSONCD) = string.Empty;
                                // 直近異動年月日
                                csDataNewRow(ABNenkinAtenaEntity.CKINIDOYMD) = string.Empty;
                                // 更新日時
                                csDataNewRow(ABNenkinAtenaEntity.KOSHINNICHIJI) = csSfskRow(ABSfskEntity.KOSHINNICHIJI);
                                if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                                {
                                    // 転入前住所方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = string.Empty;
                                    // 転出予定方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = string.Empty;
                                    // 転出確定方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = string.Empty;
                                    // 編集前方書（フル）
                                    csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = string.Empty;
                                }
                                else
                                {
                                }
                            }
                            // *履歴番号 000030 2007/04/28 追加終了

                        }
                        // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む

                        if (m_intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                        {
                            // 世帯主氏名優先区分
                            csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csAtena1Row(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
                            // 氏名優先項目
                            csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csAtena1Row(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB);
                            // 旧氏
                            csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = string.Empty;
                            // カナ旧氏
                            csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = string.Empty;
                            // 氏名フリガナ確認フラグ
                            csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = string.Empty;
                            // 旧氏フリガナ確認フラグ
                            csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = string.Empty;
                            // 通称フリガナ確認フラグ
                            csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = string.Empty;
                            // 生年月日不詳パターン
                            csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = string.Empty;
                            // 不詳生年月日
                            csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = string.Empty;
                            // 記載事由
                            csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csAtena1Row(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD);
                            // 記載年月日
                            csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csAtena1Row(ABAtenaHyojunEntity.KISAIYMD);
                            // 消除事由
                            csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csAtena1Row(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD);
                            // 消除異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csAtena1Row(ABAtenaHyojunEntity.SHOJOIDOWMD);
                            // 消除異動日不詳パターン
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csAtena1Row(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN);
                            // 不詳消除異動日
                            csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csAtena1Row(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI);
                            // 直近異動和暦年月日
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csAtena1Row(ABAtenaHyojunEntity.CKINIDOWMD);
                            // 直近異動日不詳パターン
                            csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csAtena1Row(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN);
                            // 不詳直近異動日
                            csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csAtena1Row(ABAtenaHyojunEntity.FUSHOCKINIDOBI);
                            // 事実上の世帯主
                            csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csAtena1Row(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI);
                            // 住所_市区町村コード
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csSfskRow(ABSfskHyojunEntity.SFSKSHIKUCHOSONCD);
                            // 住所_町字コード
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csSfskRow(ABSfskHyojunEntity.SFSKMACHIAZACD);
                            // 住所_都道府県
                            csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csSfskRow(ABSfskHyojunEntity.SFSKTODOFUKEN);
                            // 住所_市区郡町村名
                            csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csSfskRow(ABSfskHyojunEntity.SFSKSHIKUCHOSON);
                            // 住所_町字
                            csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csSfskRow(ABSfskHyojunEntity.SFSKMACHIAZA);
                            if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                            {
                                // 本籍_市区町村コード
                                csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = string.Empty;
                                // 本籍_町字コード
                                csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = string.Empty;
                                // 本籍_都道府県
                                csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = string.Empty;
                                // 本籍_市区郡町村名
                                csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = string.Empty;
                                // 本籍_町字
                                csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = string.Empty;
                            }
                            if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && (strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                            {
                                // 国籍コード
                                csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = string.Empty;
                            }
                            if ((strGyomuMei ?? "") == NENKIN | (strGyomuMei ?? "") == NENKIN_2)
                            {
                                // 転入前住所_市区町村コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = string.Empty;
                                // 転入前町字コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = string.Empty;
                                // 転入前住所_都道府県
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = string.Empty;
                                // 転入前住所_市区郡町村名
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = string.Empty;
                                // 転入前住所_町字
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = string.Empty;
                                // 転入前住所_国名コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = string.Empty;
                                // 転入前住所_国名
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = string.Empty;
                                // 転入前住所_国外住所
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = string.Empty;
                                // 転出確定_市区町村コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = string.Empty;
                                // 転出確定町字コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = string.Empty;
                                // 転出確定_都道府県
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = string.Empty;
                                // 転出確定_市区郡町村名
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = string.Empty;
                                // 転出確定_町字
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = string.Empty;
                                // 転出予定_市区町村コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = string.Empty;
                                // 転出予定町字コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = string.Empty;
                                // 転出予定_都道府県
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = string.Empty;
                                // 転出予定_市区郡町村名
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = string.Empty;
                                // 転出予定_町字
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = string.Empty;
                                // 転出予定_国名コード
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = string.Empty;
                                // 転出予定_国名等
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = string.Empty;
                                // 転出予定_国外住所
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = string.Empty;
                            }
                            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                            {
                                // 転入前住所_市区町村コード
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = string.Empty;
                                // 転入前町字コード
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = string.Empty;
                                // 転入前住所_都道府県
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = string.Empty;
                                // 転入前住所_市区郡町村名
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = string.Empty;
                                // 転入前住所_町字
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = string.Empty;
                                // 転入前住所_国名コード
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = string.Empty;
                                // 転入前住所_国名
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = string.Empty;
                                // 転入前住所_国外住所
                                csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = string.Empty;
                                // 転出確定_市区町村コード
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = string.Empty;
                                // 転出確定町字コード
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = string.Empty;
                                // 転出確定_都道府県
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = string.Empty;
                                // 転出確定_市区郡町村名
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = string.Empty;
                                // 転出確定_町字
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = string.Empty;
                                // 転出予定_市区町村コード
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = string.Empty;
                                // 転出予定町字コード
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = string.Empty;
                                // 転出予定_都道府県
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = string.Empty;
                                // 転出予定_市区郡町村名
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = string.Empty;
                                // 転出予定_町字
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = string.Empty;
                                // 転出予定_国名コード
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = string.Empty;
                                // 転出予定_国名等
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = string.Empty;
                                // 転出予定_国外住所
                                csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = string.Empty;
                            }
                            else
                            {
                            }
                            // 法第30条46又は47区分
                            csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csAtena1Row(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB);
                            // 在留カード等番号区分
                            csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csAtena1Row(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN);
                            // 住居地補正コード
                            csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csAtena1Row(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD);
                            // 直近届出通知区分
                            csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = string.Empty;
                            // 版番号
                            csDataNewRow(ABAtena1HyojunEntity.HANNO) = string.Empty;
                            // 改製年月日
                            csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = string.Empty;
                            // 異動区分
                            csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = string.Empty;
                            // 入力場所コード
                            csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = string.Empty;
                            // 入力場所表記
                            csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = string.Empty;
                            if ((strGyomuMei ?? "") == KOBETSU)
                            {
                                // 介護_被保険者該当有無
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = string.Empty;
                                // 国保_被保険者該当有無
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = string.Empty;
                                // 年金_被保険者該当有無
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = string.Empty;
                                // 年金_種別変更年月日
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = string.Empty;
                                // 選挙_状態区分
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = string.Empty;
                                if (m_strKobetsuShutokuKB == "1")
                                {
                                    // 後期高齢_被保険者該当有無
                                    csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = string.Empty;
                                }
                            }
                            // 連絡先区分（連絡先）
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = string.Empty;
                            // 連絡先名
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = string.Empty;
                            // 連絡先1（連絡先）
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = string.Empty;
                            // 連絡先2（連絡先）
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = string.Empty;
                            // 連絡先3（連絡先）
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = string.Empty;
                            // 連絡先種別1
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = string.Empty;
                            // 連絡先種別2
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = string.Empty;
                            // 連絡先種別3
                            csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = string.Empty;
                            if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                            {
                                // 不現住区分
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = string.Empty;
                                // 不現住だった住所_郵便番号
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = string.Empty;
                                // 不現住だった住所_市区町村コード
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = string.Empty;
                                // 不現住だった住所_町字コード
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = string.Empty;
                                // 不現住だった住所_都道府県
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = string.Empty;
                                // 不現住だった住所_市区郡町村名
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = string.Empty;
                                // 不現住だった住所_町字
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = string.Empty;
                                // 不現住だった住所_番地号表記
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = string.Empty;
                                // 不現住だった住所_方書
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = string.Empty;
                                // 不現住だった住所_方書_フリガナ
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = string.Empty;
                                // 不現住情報（対象者区分）
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = string.Empty;
                                // 不現住情報（対象者氏名）
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = string.Empty;
                                // 不現住情報（生年月日）
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = string.Empty;
                                // 不現住情報（性別）
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = string.Empty;
                                // 居住不明年月日
                                csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = string.Empty;
                                // 不現住情報（備考）
                                csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = string.Empty;
                            }
                            else
                            {
                            }
                            if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                            {
                                // 番号法更新区分
                                csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csAtena1Row(ABMyNumberHyojunEntity.BANGOHOKOSHINKB);
                            }
                            if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1)
                            {
                                // シリアル番号
                                csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = string.Empty;
                            }
                            // 標準準拠異動事由コード
                            csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = string.Empty;
                            if ((strGyomuMei ?? "") != NENKIN && (strGyomuMei ?? "") != NENKIN_2)
                            {
                                // 連絡先区分（送付先）
                                csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = csSfskRow(ABSfskHyojunEntity.SFSKRENRAKUSAKIKB);
                                // 送付先区分
                                csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = csSfskRow(ABSfskHyojunEntity.SFSKKBN);
                            }
                            else
                            {
                            }
                            // 住民区分
                            csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = string.Empty;
                            // 住民種別
                            csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = string.Empty;
                            // 住民状態
                            csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = string.Empty;
                            // 番地枝番数値
                            csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = string.Empty;
                        }
                        else
                        {
                            // noop
                        }

                    }

                    // *履歴番号 000046 2011/11/07 追加開始
                    // 住基法改正判定
                    if (m_strJukiHokaiseiKB_Param == "1")
                    {
                        // 住民票状態区分
                        csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csAtena1Row(ABAtenaFZYEntity.JUMINHYOJOTAIKBN);
                        // 住居地届出有無フラグ
                        csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csAtena1Row(ABAtenaFZYEntity.JUKYOCHITODOKEFLG);
                        // 本国名
                        csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csAtena1Row(ABAtenaFZYEntity.HONGOKUMEI);
                        // カナ本国名
                        csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csAtena1Row(ABAtenaFZYEntity.KANAHONGOKUMEI);
                        // 併記名
                        csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KANJIHEIKIMEI);
                        // カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KANAHEIKIMEI);
                        // 通称名
                        csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.KANJITSUSHOMEI);
                        // カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.KANATSUSHOMEI);
                        // カタカナ併記名
                        csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KATAKANAHEIKIMEI);
                        // 生年月日不詳区分
                        csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csAtena1Row(ABAtenaFZYEntity.UMAREFUSHOKBN);
                        // 通称名登録（変更）年月日
                        csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csAtena1Row(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD);
                        // 在留期間コード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUKIKANCD);
                        // 在留期間名称
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO);
                        // 中長期在留者である旨等のコード
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUSHACD);
                        // 中長期在留者である旨等
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUSHAMEISHO);
                        // 在留カード等番号
                        csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUCARDNO);
                        // 特別永住者証明書交付年月日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYMD);
                        // 特別永住者証明書交付予定期間開始日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYOTEISTYMD);
                        // 特定永住者証明書交付予定期間終了日
                        csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYOTEIEDYMD);
                        // 住基対象者（第30条45非該当）消除異動年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
                        // 住基対象者（第30条45非該当）消除事由コード
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
                        // 住基対象者（第30条45非該当）消除事由
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU);
                        // 住基対象者（第30条45非該当）消除届出年月日
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
                        // 住基対象者（第30条45非該当）消除届出通知区分
                        csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
                        // 外国人世帯主名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csAtena1Row(ABAtenaFZYEntity.FRNSTAINUSMEI);
                        // 外国人世帯主カナ名
                        csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csAtena1Row(ABAtenaFZYEntity.FRNSTAINUSKANAMEI);
                        // 世帯主併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSHEIKIMEI);
                        // 世帯主カナ併記名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI);
                        // 世帯主通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSTSUSHOMEI);
                        // 世帯主カナ通称名
                        csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000046 2011/11/07 追加終了

                    // *履歴番号 000048 2014/04/28 追加開始
                    // 共通番号判定
                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                    {
                        // 空白除去した値を設定する。
                        csDataNewRow(ABMyNumberEntity.MYNUMBER) = csAtena1Row(ABMyNumberEntity.MYNUMBER).ToString.Trim;
                    }
                    else
                    {
                        // noop
                    }
                    // *履歴番号 000048 2014/04/28 追加終了

                    // *履歴番号 000019 2003/11/19 修正開始
                    // 'レコードの追加
                    // csAtena12.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                    // レコードの追加
                    csDataTable.Rows.Add(csDataNewRow);
                    // *履歴番号 000019 2003/11/19 修正終了


                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            return csAtena12;

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
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(ABAtenaGetPara1XClass cAtenaGetPara1)
        {

            const string THIS_METHOD_NAME = "CheckColumnValue";
            UFErrorClass cfErrorClass;                    // エラー処理クラス
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                                                          // Dim cABCommon As ABCommonClass                      '宛名共通クラス
                                                          // * 履歴番号 000023 2004/08/27 削除終了

            try
            {

                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 'エラー処理クラスのインスタンス作成
                // '*履歴番号 000010  2003/03/27 修正開始
                // 'cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
                // cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                // '*履歴番号 000010  2003/03/27 修正終了

                // 宛名共通クラスのインスタンス作成
                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // m_cABCommon = New ABCommonClass()
                // * 履歴番号 000023 2004/08/27 削除終了

                // *履歴番号 000007 2003/03/17 削除開始
                // '住基・住登外区分
                // If Not (cAtenaGetPara1.p_strJukiJutogaiKB = String.Empty) Then
                // If Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1") Then
                // 'エラー定義を取得
                // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUKIJUTOGAIKB)
                // '例外を生成
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                // End If
                // End If
                // *履歴番号 000007 2003/03/17 削除終了

                // 業務コード
                if (!(cAtenaGetPara1.p_strGyomuCD == string.Empty))
                {
                    if (!UFStringClass.CheckAlphabetNumber(cAtenaGetPara1.p_strGyomuCD))
                    {
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_GYOMUCD)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "業務コード", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // 業務内種別コード
                if (!(cAtenaGetPara1.p_strGyomunaiSHU_CD == string.Empty))
                {
                    if (!UFStringClass.CheckNumber(cAtenaGetPara1.p_strGyomunaiSHU_CD))
                    {
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_GYOMUNAISHU_CD)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "業務内種別コード", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // *履歴番号 000007 2003/03/17 削除開始
                // '送付先データ区分
                // If Not (cAtenaGetPara1.p_strSfskDataKB = String.Empty) Then
                // If (Not (cAtenaGetPara1.p_strSfskDataKB = "1")) Then
                // 'エラー定義を取得
                // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_SFSKDATAKB)
                // '例外を生成
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                // End If
                // End If

                // '世帯員編集
                // If Not (cAtenaGetPara1.p_strStaiinHenshu = String.Empty) Then
                // If (Not (cAtenaGetPara1.p_strStaiinHenshu = "1")) Then
                // 'エラー定義を取得
                // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_STAIINHENSHU)
                // '例外を生成
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                // End If
                // End If

                // 'データ区分
                // If Not (cAtenaGetPara1.p_strDataKB = String.Empty) Then
                // If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strDataKB)) Then
                // 'エラー定義を取得
                // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_DATAKB)
                // '例外を生成
                // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                // End If
                // End If
                // *履歴番号 000007 2003/03/17 削除終了

                // 住所編集１
                if (!(cAtenaGetPara1.p_strJushoHenshu1 == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strJushoHenshu1 == "1"))
                    {
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU1)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所編集１", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // 住所編集２
                if (!(cAtenaGetPara1.p_strJushoHenshu2 == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strJushoHenshu2 == "1"))
                    {
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU2)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所編集２", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // 住所編集３
                if (!(cAtenaGetPara1.p_strJushoHenshu3 == string.Empty))
                {
                    // * 履歴番号 000028 2007/01/15 修正開始
                    // * 履歴番号 000025 2005/07/14 修正開始
                    // If (Not (cAtenaGetPara1.p_strJushoHenshu3 >= "1" And cAtenaGetPara1.p_strJushoHenshu3 <= "4")) Then
                    // If (Not (cAtenaGetPara1.p_strJushoHenshu3 >= "1" And cAtenaGetPara1.p_strJushoHenshu3 <= "5")) Then
                    if (!(cAtenaGetPara1.p_strJushoHenshu3 >= "1" & cAtenaGetPara1.p_strJushoHenshu3 <= "6"))
                    {
                        // * 履歴番号 000025 2005/07/14 修正終了
                        // * 履歴番号 000028 2007/01/15 修正終了
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU3)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所編集３", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // 住所編集４
                if (!(cAtenaGetPara1.p_strJushoHenshu4 == string.Empty))
                {
                    if (!(cAtenaGetPara1.p_strJushoHenshu4 == "1"))
                    {
                        // *履歴番号 000009 2003/03/18 修正開始
                        // 'エラー定義を取得
                        // objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU4)
                        // '例外を生成
                        // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                        // エラー定義を取得
                        cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                        objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                        // 例外を生成
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住所編集４", objErrorStruct.m_strErrorCode);
                        // *履歴番号 000009 2003/03/18 修正終了
                    }
                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException ObjAppExp)
            {
                // ワーニングログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + ObjAppExp.Message + "】");

                // エラーをスローする()
                throw ObjAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");

                // エラーをそのままスローする
                throw objExp;
            }

        }
        #endregion

        #region  宛名情報カラム作成(CreateAtena1Columns) 
        // ************************************************************************************************
        // * メソッド名     宛名情報カラム作成
        // * 
        // * 構文           Private Function CreateAtena1Columns() As DataTable
        // * 
        // * 機能　　    　　宛名情報DataSetのカラムを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet(ABAtena1) : 取得した宛名情報
        // ************************************************************************************************
        public DataTable CreateAtena1Columns()
        {
            const string THIS_METHOD_NAME = "CreateAtena1Columns";
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            DataTable csDataTable;
            DataColumn csDataColumn;
            // *履歴番号 000011 2003/04/01 削除開始
            // Dim csDataPrimaryKey(4) As DataColumn               '主キー
            // *履歴番号 000011 2003/04/01 削除終了

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // *履歴番号 000047 2012/03/13 修正開始
                // '* 履歴番号 000024 2005/01/25 追加開始（宮沢）
                // If Not (m_csOrgAtena1 Is Nothing) Then
                // Return m_csOrgAtena1.Clone
                // End If
                // '* 履歴番号 000024 2005/01/25 追加終了（宮沢）

                if (!m_blnNenKin && !m_blnKobetsu)
                {
                    // 年金・個別以外の時は通常スキーマを見る
                    if (m_csOrgAtena1 is not null)
                    {
                        return m_csOrgAtena1.Clone();
                    }
                    else
                    {
                        // 何もしない
                    }
                }
                // 年金or個別の時は専用のスキーマを見る
                else if (m_csOrgNenkinKobetsu is not null)
                {
                    return m_csOrgNenkinKobetsu.Clone();
                }
                else
                {
                    // 何もしない
                }
                // *履歴番号 000047 2012/03/13 修正終了

                csDataTable = new DataTable();
                csDataTable.TableName = ABAtena1Entity.TABLE_NAME;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUMINCD, Type.GetType("System.String"));
                csDataColumn.AllowDBNull = false;
                csDataColumn.MaxLength = 15;
                // *履歴番号 000011 2003/04/01 削除開始
                // csDataPrimaryKey(0) = csDataColumn              '主キー①
                // *履歴番号 000011 2003/04/01 削除終了
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKB, Type.GetType("System.String"));
                csDataColumn.AllowDBNull = false;
                csDataColumn.MaxLength = 2;
                // *履歴番号 000011 2003/04/01 削除開始
                // csDataPrimaryKey(1) = csDataColumn              '主キー②
                // *履歴番号 000011 2003/04/01 削除終了

                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKBRYAKUMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;

                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOMUCD, Type.GetType("System.String"));
                    csDataColumn.AllowDBNull = false;
                    csDataColumn.MaxLength = 2;
                    // *履歴番号 000011 2003/04/01 削除開始
                    // csDataPrimaryKey(2) = csDataColumn              '主キー③
                    // *履歴番号 000011 2003/04/01 削除終了
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOMUNAISHU_CD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // *履歴番号 000011 2003/04/01 削除開始
                    // csDataPrimaryKey(3) = csDataColumn              '主キー④
                    // *履歴番号 000011 2003/04/01 削除終了
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KYUSHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.STAICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ATENADATAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ATENADATASHU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUSHUBETSU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUSHUBETSURYAKU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANASEIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;        // 40
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANASEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 72;         // 24
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANAMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;         // 16
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANJIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 40
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANASHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;        // 60
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANJISHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 920;        // 80
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREHYOJIWMD, Type.GetType("System.String"));
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMARESHOMEIWMD, Type.GetType("System.String"));
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEIBETSUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEIBETSU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUZOKUGARACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUZOKUGARA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 40;         // 15
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HOJINDAIHYOUSHA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KJNHJNKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KJNHJNKBMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANNAIKANGAIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.NAIGAIKBMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 640;        // 80
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKIFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;         // 30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOSEIKUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOSEIKUMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUJIYUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUJIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOJIYUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOJIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUNUSHIJUMINCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 15;
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANANUSHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 120;        // 40
                                                     // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUNUSHIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HYOJIJUN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 4;
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                // *履歴番号 000011 2003/04/01 削除開始
                // csDataTable.PrimaryKey = csDataPrimaryKey       '主キー
                // *履歴番号 000011 2003/04/01 削除終了
                // *履歴番号 000012 2003/04/18 追加開始
                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）IF文で囲む
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZOKUGARACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZOKUGARA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 40;         // 15
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANAMEISHO2, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;        // 60
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANJIMEISHO2, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 40
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEKINO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                }
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）IF文で囲む
                // *履歴番号 000012 2003/04/18 追加終了
                // *履歴番号 000017 2003/10/09 追加開始
                // *履歴番号 000020 2003/12/01 削除開始
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI_GYOMUCD, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 2
                // *履歴番号 000020 2003/12/01 削除終了
                // *履歴番号 000017 2003/10/09 追加終了

                // *履歴番号 000030 2007/04/28 追加開始
                // 介護用取得項目
                if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI_GYOMUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KYUSEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 60;         // 15
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEIIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEIJIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_ZJUSHOCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_YUBINNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_ZJUSHOCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_JUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_BANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 20
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_KATAGAKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 240;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIJUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIBANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 20
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 240;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIYUBINNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIJUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIBANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 20
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 240;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUMAEBANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;         // 20
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUMAEKATAGAKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 240;         // 30
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOTDKDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CKINJIYUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKICD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUTDKDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEITDKDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUNYURIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 30;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHICHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CKINIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOSHINNICHIJI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 17;
                }
                // *履歴番号 000030 2007/04/28 追加終了

                // *履歴番号 000037 2008/11/18 修正開始
                // *履歴番号 000036 2008/11/10 追加開始
                // If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso m_blnKobetsu = False AndAlso _
                // (m_strRiyoTdkdKB = "1" OrElse m_strRiyoTdkdKB = "2")) Then
                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly && m_blnKobetsu == false && m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo && (m_strRiyoTdkdKB == "1" || m_strRiyoTdkdKB == "2"))
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.NOZEIID, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 11;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RIYOSHAID, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 11;
                }
                else
                {
                }
                // *履歴番号 000036 2008/11/10 追加終了
                // *履歴番号 000037 2008/11/18 修正終了

                // *履歴番号 000040 2010/05/14 追加開始
                if (m_blnNenKin == false && m_blnKobetsu == false)
                {
                    // 通常、簡易宛名用、介護用のみ

                    // 本籍筆頭者情報出力判定
                    if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                    {
                        // パラメータ:本籍筆頭者取得区分が"1"かつ、管理情報:本籍取得区分(10･18)が"1"の場合のみ出力
                        // 本籍住所
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 200;        // 30
                                                             // 本籍番地
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 200;        // 20
                                                             // 筆頭者
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;        // 30
                    }
                    else
                    {
                    }

                    // 処理停止区分出力判定
                    if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                    {
                        // パラメータ:処理停止区分取得区分が"1"かつ、管理情報:処理停止区分取得区分(10･19)が"1"の場合のみ出力
                        // 処理停止区分
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 1;
                    }
                    else
                    {
                    }

                    // *履歴番号 000041 2011/05/18 追加開始
                    // 外国人在留情報出力判定
                    if (m_strFrnZairyuJohoKB_Param == "1")
                    {
                        // パラメータ:外国人在留情報取得区分が"1"の場合のみ
                        // 国籍
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 20;
                        // 在留資格コード
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 3;
                        // 在留資格
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 15;
                        // 在留期間
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 4;
                        // 在留開始年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 在留終了年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                    }
                    // *履歴番号 000041 2011/05/18 追加終了
                    // *履歴番号 000046 2011/11/07 追加開始
                    // 住基法改正判定
                    if (m_strJukiHokaiseiKB_Param == "1")
                    {
                        // 住民票状態区分
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 1;
                        // 住居地届出有無フラグ
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 1;
                        // 本国名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // カナ本国名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // 併記名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // カナ併記名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // 通称名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // カナ通称名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // カタカナ併記名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // 生年月日不詳区分
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 2;
                        // 通称名登録（変更）年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 在留期間コード
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 7;
                        // 在留期間名称
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 10;
                        // 中長期在留者である旨等のコード
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 2;
                        // 中長期在留者である旨等
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 20;
                        // 在留カード等番号
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 12;
                        // 特別永住者証明書交付年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 特別永住者証明書交付予定期間開始日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 特定永住者証明書交付予定期間終了日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 住基対象者（第30条45非該当）消除異動年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 住基対象者（第30条45非該当）消除事由コード
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 2;
                        // 住基対象者（第30条45非該当）消除事由
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 10;
                        // 住基対象者（第30条45非該当）消除届出年月日
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 8;
                        // 住基対象者（第30条45非該当）消除届出通知区分
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 1;
                        // 外国人世帯主名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // 外国人世帯主カナ名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // 世帯主併記名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // 世帯主カナ併記名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                        // 世帯主通称名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 480;
                        // 世帯主カナ通称名
                        csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 120;
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000046 2011/11/07 追加終了

                    // *履歴番号 000048 2014/04/28 追加開始
                    // 共通番号判定
                    if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                    {
                        // 共通番号
                        csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, Type.GetType("System.String"));
                        csDataColumn.MaxLength = 13;
                    }
                    else
                    {
                        // noop
                    }
                    // *履歴番号 000048 2014/04/28 追加終了

                    // *履歴番号 000047 2012/03/13 追加開始
                    // 通常スキーマに保存
                    m_csOrgAtena1 = csDataTable.Clone();
                }
                // *履歴番号 000047 2012/03/13 追加終了
                else
                {
                    // *履歴番号 000047 2012/03/13 追加開始
                    // 年金・個別スキーマに保存
                    m_csOrgNenkinKobetsu = csDataTable.Clone();
                    // *履歴番号 000047 2012/03/13 追加終了
                }
                // *履歴番号 000040 2010/05/14 追加終了

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            // *履歴番号 000047 2012/03/13 修正開始
            // '* 履歴番号 000024 2005/01/25 変更開始（宮沢）
            // 'Return csDataTable
            // m_csOrgAtena1 = csDataTable
            // Return m_csOrgAtena1.Clone
            // '* 履歴番号 000024 2005/01/25 変更終了（宮沢）

            return csDataTable;
            // *履歴番号 000047 2012/03/13 修正終了
        }
        #endregion

        #region  年金用宛名情報カラム作成(CreateNenkinAtenaColumns) 
        // *履歴番号 000013 2003/04/18 追加開始
        // ************************************************************************************************
        // * メソッド名     年金用宛名情報カラム作成
        // * 
        // * 構文           Private Function CreateNenkinAtenaColumns(ByVal strGyomuMei As String) As DataTable
        // * 
        // * 機能　　    　　年金用宛名情報DataSetのカラムを作成する
        // * 
        // * 引数           ByVal strGyomuMei As String
        // * 
        // * 戻り値         DataSet(ABNenkinAtena) : 作成した年金用宛名情報
        // ************************************************************************************************
        // *履歴番号 000027 2006/07/31 修正開始
        private DataTable CreateNenkinAtenaColumns(string strGyomuMei)
        {
            // Private Function CreateNenkinAtenaColumns() As DataTable
            // *履歴番号 000027 2006/07/31 修正終了
            const string THIS_METHOD_NAME = "CreateNenkinAtenaColumns";
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            DataTable csDataTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
                if (m_csOrgAtena1Nenkin is not null)
                {
                    return m_csOrgAtena1Nenkin.Clone();
                }
                // * 履歴番号 000024 2005/01/25 追加終了（宮沢）

                // 宛名情報より作成する
                csDataTable = CreateAtena1Columns();
                csDataTable.TableName = ABNenkinAtenaEntity.TABLE_NAME;

                // *履歴番号 000020 2003/12/01 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                // *履歴番号 000020 2003/12/01 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.KYUSEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 60;         // 15
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.JUTEIIDOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.JUTEIJIYU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                // *履歴番号 000022 2003/12/04 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HON_ZJUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                // *履歴番号 000022 2003/12/04 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                // *履歴番号 000017 2003/10/09 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                // *履歴番号 000017 2003/10/09 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_JUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_BANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 20
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;         // 30
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 20
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;         // 30
                                                      // *履歴番号 000022 2003/12/04 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;        // 30
                                                     // *履歴番号 000022 2003/12/04 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                // *履歴番号 000017 2003/10/09 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                // *履歴番号 000017 2003/10/09 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 30
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 20
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;         // 30
                                                      // *履歴番号 000022 2003/12/04 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;        // 30
                                                     // *履歴番号 000022 2003/12/04 追加終了
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HENSHUMAEBANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;         // 20
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 240;         // 30
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.SHOJOTDKDYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.CKINJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                // *履歴番号 000022 2003/12/04 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.KOKUSEKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                // *履歴番号 000022 2003/12/04 追加終了
                // *履歴番号 000027 2006/07/31 修正開始
                if (strGyomuMei == "NENKIN_2")
                {
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                }
                // *履歴番号 000027 2006/07/31 修正終了

                // *履歴番号 000044 2011/06/24 追加開始
                // 外国人在留情報出力判定
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // パラメータ:外国人在留情報取得区分が"1"の場合のみ
                    // 国籍
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 20;
                    // 在留資格コード
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    // 在留資格
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 15;
                    // 在留期間
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 4;
                    // 在留開始年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 在留終了年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                }
                // *履歴番号 000044 2011/06/24 追加終了

                // *履歴番号 000040 2010/05/14 追加開始
                // 本籍筆頭者情報出力判定
                if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                {
                    // パラメータ:本籍筆頭者取得区分が"1"かつ、管理情報:本籍取得区分(10･18)が"1"の場合のみ出力
                    // 本籍住所
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;        // 30
                                                         // 本籍番地
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;        // 20
                                                         // 筆頭者
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                }
                else
                {
                }

                // 処理停止区分出力判定
                if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                {
                    // パラメータ:処理停止区分取得区分が"1"かつ、管理情報:処理停止区分取得区分(10･19)が"1"の場合のみ出力
                    // 処理停止区分
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                else
                {
                }
                // *履歴番号 000040 2010/05/14 追加終了

                // *履歴番号 000044 2011/06/24 削除開始
                // '*履歴番号 000041 2011/05/18 追加開始
                // ' 外国人在留情報出力判定
                // If (m_strFrnZairyuJohoKB_Param = "1") Then
                // ' パラメータ:外国人在留情報取得区分が"1"の場合のみ
                // ' 国籍
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 20
                // ' 在留資格コード
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 3
                // ' 在留資格
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 15
                // ' 在留期間
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 4
                // ' 在留開始年月日
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 8
                // ' 在留終了年月日
                // csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
                // csDataColumn.MaxLength = 8
                // End If
                // '*履歴番号 000041 2011/05/18 追加終了
                // *履歴番号 000044 2011/06/24 削除終了

                // *履歴番号 000046 2011/11/07 追加開始
                // 住基法改正判定
                if (m_strJukiHokaiseiKB_Param == "1")
                {
                    // 住民票状態区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 住居地届出有無フラグ
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 本国名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ本国名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // カタカナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 生年月日不詳区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 通称名登録（変更）年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 在留期間コード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    // 在留期間名称
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    // 中長期在留者である旨等のコード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 中長期在留者である旨等
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 20;
                    // 在留カード等番号
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 12;
                    // 特別永住者証明書交付年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 特別永住者証明書交付予定期間開始日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 特定永住者証明書交付予定期間終了日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除異動年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除事由コード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 住基対象者（第30条45非該当）消除事由
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    // 住基対象者（第30条45非該当）消除届出年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除届出通知区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 外国人世帯主名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 外国人世帯主カナ名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 世帯主併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 世帯主カナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 世帯主通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 世帯主カナ通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                }
                else
                {
                    // 処理なし
                }
                // *履歴番号 000046 2011/11/07 追加終了

                // *履歴番号 000048 2014/04/28 追加開始
                // 共通番号判定
                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                {
                    // 共通番号
                    csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                }
                else
                {
                    // noop
                }
                // *履歴番号 000048 2014/04/28 追加終了

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            // * 履歴番号 000024 2005/01/25 変更開始（宮沢）
            // Return csDataTable
            m_csOrgAtena1Nenkin = csDataTable;
            return m_csOrgAtena1Nenkin.Clone();
            // * 履歴番号 000024 2005/01/25 変更終了（宮沢）


        }
        // *履歴番号 000013 2003/04/18 追加終了
        #endregion

        #region  宛名個別情報カラム作成(CreateAtena1KobetsuColumns) 
        // *履歴番号 000019 2003/11/19 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名個別情報カラム作成
        // * 
        // * 構文           Private Function CreateAtena1KobetsuColumns() As DataTable
        // * 
        // * 機能　　    　　宛名個別情報DataSetのカラムを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet(ABAtena1Kobetsu) : 作成した宛名個別情報
        // ************************************************************************************************
        private DataTable CreateAtena1KobetsuColumns()
        {
            // * corresponds to VS2008 Start 2010/04/16 000039
            // Dim csDataSet As DataSet
            // * corresponds to VS2008 End 2010/04/16 000039
            DataTable csDataTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                // * 履歴番号 000024 2005/01/25 追加開始（宮沢）
                if (m_csOrgAtena1Kobetsu is not null)
                {
                    return m_csOrgAtena1Kobetsu.Clone();
                }
                // * 履歴番号 000024 2005/01/25 追加終了（宮沢）
                // 宛名情報より作成する
                csDataTable = CreateAtena1Columns();
                csDataTable.TableName = ABAtena1KobetsuEntity.TABLE_NAME;

                // *履歴番号 000020 2003/12/01 追加開始
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                // *履歴番号 000020 2003/12/01 追加終了
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KSNENKNNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHONO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 14;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.INKANNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.INKANTOROKUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 9;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATESTYM, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATEEDYM, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGHIHKNSHANO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                // *履歴番号 000034 2008/01/15 追加開始
                if (m_strKobetsuShutokuKB == "1")
                {
                    // 個別事項取得区分が"1"の場合は後期高齢項目を追加する
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB, Type.GetType("System.String"));           // 資格区分
                    csDataColumn.MaxLength = 1;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO, Type.GetType("System.String"));          // 被保険者番号
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD, Type.GetType("System.String"));     // 被保険者資格取得事由コード
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI, Type.GetType("System.String"));    // 被保険者資格取得事由名称
                    csDataColumn.MaxLength = 10;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD, Type.GetType("System.String"));        // 被保険者資格取得年月日
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD, Type.GetType("System.String"));     // 被保険者資格喪失事由コード
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI, Type.GetType("System.String"));    // 被保険者資格喪失事由名称
                    csDataColumn.MaxLength = 10;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD, Type.GetType("System.String"));        // 被保険者資格喪失年月日
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD, Type.GetType("System.String"));     // 保険者番号適用開始年月日
                    csDataColumn.MaxLength = 8;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD, Type.GetType("System.String"));     // 保険者番号適用終了年月日
                    csDataColumn.MaxLength = 8;
                }
                else
                {
                    // 個別事項取得区分が値なしの場合は後期高齢項目を追加しない
                }

                // *履歴番号 000034 2008/01/15 追加終了

                // *履歴番号 000040 2010/05/14 追加開始
                // 本籍筆頭者情報出力判定
                if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                {
                    // パラメータ:本籍筆頭者取得区分が"1"かつ、管理情報:本籍取得区分(10･18)が"1"の場合のみ出力
                    // 本籍住所
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;        // 30
                                                         // 本籍番地
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;        // 20
                                                         // 筆頭者
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;        // 30
                }
                else
                {
                }

                // 処理停止区分出力判定
                if (m_strShoriteishiKB == "1" && m_strShoriteishiKB_Param == "1")
                {
                    // パラメータ:処理停止区分取得区分が"1"かつ、管理情報:処理停止区分取得区分(10･19)が"1"の場合のみ出力
                    // 処理停止区分
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                else
                {
                }
                // *履歴番号 000040 2010/05/14 追加終了

                // *履歴番号 000041 2011/05/18 追加開始
                // 外国人在留情報出力判定
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    // パラメータ:外国人在留情報取得区分が"1"の場合のみ
                    // 国籍
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 20;
                    // 在留資格コード
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    // 在留資格
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 15;
                    // 在留期間
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 4;
                    // 在留開始年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 在留終了年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                }
                // *履歴番号 000041 2011/05/18 追加終了
                // *履歴番号 000046 2011/11/07 追加開始
                // 住基法改正判定
                if (m_strJukiHokaiseiKB_Param == "1")
                {
                    // 住民票状態区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 住居地届出有無フラグ
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 本国名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ本国名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // カナ通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // カタカナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 生年月日不詳区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 通称名登録（変更）年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 在留期間コード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    // 在留期間名称
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    // 中長期在留者である旨等のコード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 中長期在留者である旨等
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 20;
                    // 在留カード等番号
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 12;
                    // 特別永住者証明書交付年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 特別永住者証明書交付予定期間開始日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 特定永住者証明書交付予定期間終了日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除異動年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除事由コード
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 2;
                    // 住基対象者（第30条45非該当）消除事由
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                    // 住基対象者（第30条45非該当）消除届出年月日
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 8;
                    // 住基対象者（第30条45非該当）消除届出通知区分
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                    // 外国人世帯主名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 外国人世帯主カナ名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 世帯主併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 世帯主カナ併記名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                    // 世帯主通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    // 世帯主カナ通称名
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 120;
                }
                else
                {
                    // 処理なし
                }
                // *履歴番号 000046 2011/11/07 追加終了

                // *履歴番号 000048 2014/04/28 追加開始
                // 共通番号判定
                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                {
                    // 共通番号
                    csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 13;
                }
                else
                {
                    // noop
                }
                // *履歴番号 000048 2014/04/28 追加終了

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            // * 履歴番号 000024 2005/01/25 変更開始（宮沢）
            // Return csDataTable
            m_csOrgAtena1Kobetsu = csDataTable;
            return m_csOrgAtena1Kobetsu.Clone();
            // * 履歴番号 000024 2005/01/25 変更終了（宮沢）

        }
        // *履歴番号 000019 2003/11/19 追加終了
        #endregion

        // *履歴番号 000050 2023/03/10 追加開始
        #region  宛名情報標準版カラム作成(CreateAtena1HyojunColumns) 
        // ************************************************************************************************
        // * メソッド名     宛名情報標準版カラム作成
        // * 
        // * 構文           Private Function CreateAtena1HyojunColumns() As DataTable
        // * 
        // * 機能　　    　　宛名情報標準版DataSetのカラムを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet(ABAtena1Hyojun) : 作成した宛名情報
        // ************************************************************************************************
        private DataTable CreateAtena1HyojunColumns()
        {
            DataTable csDataTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                if (m_csOrgAtena1Hyojun is not null)
                {
                    return m_csOrgAtena1Hyojun.Clone();
                }
                // 宛名情報より作成する
                csDataTable = CreateAtena1Columns();
                csDataTable.TableName = ABAtena1HyojunEntity.TABLE_NAME;

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL, Type.GetType("System.String"));

                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SEIBETSU_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                }
                else
                {
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUJUSHO_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1200;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1200;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1200;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1200;
                }
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 100;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANJIKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 80;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANAKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.UMAREBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOUMAREBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KISAIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOCKINIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_TODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                }
                if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKICD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                }
                if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_Kaigo)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 300;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 200;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 300;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUKYOCHIHOSEICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINTDKDTUCIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HANNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KAISEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 400;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 50;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 100;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYOJUFUMEI_YMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2000;
                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANGOHOKOSHINKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                if (m_blnMethodKB == ABEnumDefine.MethodKB.KB_AtenaGet1)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SERIALNO, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 40;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINSHUBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINJOTAI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANCHIEDABANSUCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            m_csOrgAtena1Hyojun = csDataTable;
            return m_csOrgAtena1Hyojun.Clone();

        }
        #endregion

        #region  年金用宛名情報標準版カラム作成(CreateNenkinAtenaHyojunColumns) 
        // ************************************************************************************************
        // * メソッド名     年金用宛名情報標準版カラム作成
        // * 
        // * 構文           Private Function CreateNenkinAtenaHyojunColumns(ByVal strGyomuMei As String) As DataTable
        // * 
        // * 機能　　    　　年金用宛名情報標準版DataSetのカラムを作成する
        // * 
        // * 引数           ByVal strGyomuMei As String
        // * 
        // * 戻り値         DataSet(Atena1NenkinHyojun) : 作成した年金用宛名情報
        // ************************************************************************************************
        private DataTable CreateNenkinAtenaHyojunColumns(string strGyomuMei)
        {
            DataTable csDataTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                if (m_csOrgAtena1NenkinHyojun is not null)
                {
                    return m_csOrgAtena1NenkinHyojun.Clone();
                }

                // 宛名情報より作成する
                csDataTable = CreateNenkinAtenaColumns(strGyomuMei);
                csDataTable.TableName = ABNenkinAtenaHyojunEntity.TABLE_NAME;

                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUKANASHIMEI_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUKANJISHIMEI_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SEIBETSU_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 10;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUJUSHO_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KOKUSEKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 100;
                }
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.STAINUSSHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KANJIKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 80;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KANAKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIMEIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KYUUJIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TSUSHOKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.UMAREBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOUMAREBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNKISAIJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KISAIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNSHOJOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHOJOIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOSHOJOIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOCKINIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JIJITSUSTAINUSMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_SHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_MACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_TODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_SHIKUGUNCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_MACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                }
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 200;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HODAI30JO46MATAHA47KB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.ZAIRYUCARDNOKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUKYOCHIHOSEICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINTDKDTUCIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HANNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KAISEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNIDOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.NYURYOKUBASHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.NYURYOKUBASHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 400;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                {
                    csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.BANGOHOKOSHINKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNIDOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINSHUBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINJOTAI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.BANCHIEDABANSUCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            m_csOrgAtena1Hyojun = csDataTable;
            return m_csOrgAtena1Hyojun.Clone();

        }
        #endregion

        #region  宛名個別情報標準版カラム作成(CreateAtena1KobetsuHyojunColumns) 
        // ************************************************************************************************
        // * メソッド名     宛名個別情報標準版カラム作成
        // * 
        // * 構文           Private Function CreateAtena1KobetsuHyojunColumns() As DataTable
        // * 
        // * 機能　　    　　宛名個別情報標準版DataSetのカラムを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet(Atena1KobetsuHyojun) : 作成した宛名個別情報
        // ************************************************************************************************
        private DataTable CreateAtena1KobetsuHyojunColumns()
        {
            DataTable csDataTable;
            DataColumn csDataColumn;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                if (m_csOrgAtena1KobetsuHyojun is not null)
                {
                    return m_csOrgAtena1KobetsuHyojun.Clone();
                }

                // 宛名情報より作成する
                csDataTable = CreateAtena1KobetsuColumns();
                csDataTable.TableName = ABAtena1KobetsuHyojunEntity.TABLE_NAME;

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL, Type.GetType("System.String"));

                if (m_blnSelectAll != ABEnumDefine.AtenaGetKB.KaniOnly)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SEIBETSU_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 10;
                }
                else
                {
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUJUSHO_FULL, Type.GetType("System.String"));

                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KATAGAKI_FULL, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1200;
                if (m_strFrnZairyuJohoKB_Param == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKI_FULL, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 100;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIYUSENKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANJIKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 80;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANAKYUUJI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.UMAREBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOUMAREBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KISAIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOCKINIDOBI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 72;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 16;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 48;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                if (m_strHonsekiHittoshKB_Param == "1" && m_strHonsekiHittoshKB == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 6;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZACD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 7;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_TODOFUKEN, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 16;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 48;
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZA, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 480;
                }
                if (m_blnMethodKB != ABEnumDefine.MethodKB.KB_Kaigo)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKICD, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 3;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUKYOCHIHOSEICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINTDKDTUCIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HANNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 5;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KAISEIYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHOCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                if (m_strKobetsuShutokuKB == "1")
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 400;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 254;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 7;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 4;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON, Type.GetType("System.String"));
                csDataColumn.MaxLength = 12;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA, Type.GetType("System.String"));
                csDataColumn.MaxLength = 480;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 50;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 300;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 100;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYOJUFUMEI_YMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 8;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2000;
                if (m_strMyNumberKB_Param == ABConstClass.MYNUMBER.MYNUMBERKB.ON)
                {
                    csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANGOHOKOSHINKB, Type.GetType("System.String"));
                    csDataColumn.MaxLength = 1;
                }
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SERIALNO, Type.GetType("System.String"));
                csDataColumn.MaxLength = 40;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINKBN, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINSHUBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINJOTAI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANCHIEDABANSUCHI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 20;

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            catch (UFAppException exException)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // スローする
                throw exException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + exException.Message + "】");

                // システムエラーをスローする
                throw exException;

            }

            m_csOrgAtena1Hyojun = csDataTable;
            return m_csOrgAtena1Hyojun.Clone();

        }
        #endregion
        // *履歴番号 000050 2023/03/10 追加終了

        #region  送付先住所行政区編集区分取得(GetSofuJushoGyoseikuType) 
        // *履歴番号 000016 2003/08/22 追加開始
        // ************************************************************************************************
        // * メソッド名     送付先住所行政区編集区分取得
        // * 
        // * 構文           Private Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
        // * 
        // * 機能　　    　　送付先住所行政区編集区分を取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         SofuJushoGyoseikuType
        // ************************************************************************************************
        [SecuritySafeCritical]
        protected virtual SofuJushoGyoseikuType GetSofuJushoGyoseikuType()
        {
            const string THIS_METHOD_NAME = "GetSofuJushoGyoseikuType";
            // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
            // Dim cURKanriJohoB As URKANRIJOHOCacheBClass         '管理情報取得クラス
            // * 履歴番号 000023 2004/08/27 削除終了
            SofuJushoGyoseikuType cSofuJushoGyoseikuType;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // * 履歴番号 000023 2004/08/27 削除開始（宮沢）
                // 管理情報取得Ｂのインスタンス作成
                // cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
                // * 履歴番号 000023 2004/08/27 削除終了

                // * 履歴番号 000024 2005/01/25 更新開始（宮沢）
                // cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
                if (m_bSofuJushoGyoseikuTypeFlg == false)
                {
                    m_cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param;
                    m_bSofuJushoGyoseikuTypeFlg = true;
                }
                cSofuJushoGyoseikuType = m_cSofuJushoGyoseikuType;
                // * 履歴番号 000024 2005/01/25 更新終了（宮沢）

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }

            return cSofuJushoGyoseikuType;

        }
        // *履歴番号 000016 2003/08/22 追加終了
        #endregion

        // *履歴番号 000042 2011/05/18 追加開始
        #region 名称編集処理(MeishoHenshu)
        // ************************************************************************************************
        // * メソッド名       名称編集処理
        // * 
        // * 構文             Private Function MeishoHenshu(ByVal csAtenaDataRow As DataRow) As String()
        // * 
        // * 機能　　    　   本名通称名用名称編集処理を行う
        // * 
        // * 引数             csAtenaDataRow  : DataRow(宛名データ)
        // * 
        // * 戻り値           String()        : 配列[カナ名称、漢字名称]
        // ************************************************************************************************
        private string[] MeishoHenshu(DataRow csAtenaDataRow)
        {
            const string THIS_METHOD_NAME = "MeishoHenshu";
            var strMeisho = new string[2];                          // 返却用名称配列[カナ名称、漢字名称]
            string strGroupID;                            // グループID
            DataSet csMeishoSeigyoDS;                     // 名称制御データ用データセット
            bool blnMeishoSeigyoFlg;                   // 名称制御フラグ
            string strRiyoFlg = string.Empty;             // 利用フラグ
                                                          // *履歴番号 000043 2011/06/23 追加開始
            USUrlPrmData cuUrlPrmData;                    // URLパラメータインターフェース
            const string DEFAULT_VALUE = "01";
            // *履歴番号 000043 2011/06/23 追加終了


            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 初期化処理
                strMeisho[0] = string.Empty;
                strMeisho[1] = string.Empty;

                // *履歴番号 000043 2011/06/23 修正開始
                // **
                // * 課情報取得処理
                // *
                // URLパラメータクラスのインスタンス化
                if (m_cuUSSUrlParm is null)
                {
                    m_cuUSSUrlParm = new USUrlParmClass();
                }

                // 課情報の取得
                cuUrlPrmData = m_cuUSSUrlParm.getURLPrm(m_cfUFControlData, USUrlParmClass.PrmType.ToshimaAtenaType, DEFAULT_VALUE);
                strGroupID = cuUrlPrmData.p_strPrm;

                // strGroupID = "01"
                // *履歴番号 000043 2011/06/23 修正終了

                // **
                // * 優先名称情報取得処理
                // *
                // 表示名称制御Ｂクラスのインスタンス作成
                if (m_cABMeishoSeigyoB is null)
                {
                    m_cABMeishoSeigyoB = new ABMeishoSeigyoBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass);
                }

                // 表示名称制御データ取得
                csMeishoSeigyoDS = m_cABMeishoSeigyoB.GetMeishoSeigyo(Conversions.ToString(csAtenaDataRow(ABAtenaEntity.JUMINCD)), strGroupID);

                if (csMeishoSeigyoDS is not null && csMeishoSeigyoDS.Tables(ABMeishoSeigyoEntity.TABLE_NAME).Rows.Count > 0)
                {
                    // 表示名称制御データが存在する場合
                    // 利用フラグ取得
                    strRiyoFlg = csMeishoSeigyoDS.Tables(ABMeishoSeigyoEntity.TABLE_NAME).Rows(0)(ABMeishoSeigyoEntity.RIYOFG).ToString;

                    blnMeishoSeigyoFlg = true;
                }
                else
                {
                    // 表示名称制御データが存在しない場合
                    strRiyoFlg = string.Empty;

                    blnMeishoSeigyoFlg = false;
                }

                // **
                // * 名称編集処理
                // *
                if (blnMeishoSeigyoFlg == true)
                {
                    switch (strRiyoFlg ?? "")
                    {
                        case "0":        // 本名
                            {
                                // *履歴番号 000045 2011/06/27 追加開始
                                if (csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString.Trim != string.Empty)
                                {
                                    // 漢字名称２が空白以外の場合、カナ名称２、漢字名称２をセット
                                    strMeisho[0] = csAtenaDataRow(ABAtenaEntity.KANAMEISHO2).ToString;
                                    strMeisho[1] = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString;
                                }
                                else
                                {
                                    // 漢字名称２が空白の場合、カナ名称１、漢字名称１をセット
                                    strMeisho[0] = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString;
                                    strMeisho[1] = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString;
                                }

                                break;
                            }
                        // strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO2).ToString
                        // strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString
                        // *履歴番号 000045 2011/06/27 追加終了

                        case "1":        // 通称名
                            {
                                strMeisho[0] = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString;
                                strMeisho[1] = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString;       // それ以外
                                break;
                            }

                        default:
                            {
                                strMeisho[0] = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString;
                                strMeisho[1] = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString;
                                break;
                            }

                    }
                }
                else
                {
                    strMeisho[0] = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString;
                    strMeisho[1] = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString;
                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return strMeisho;

        }
        #endregion
        // *履歴番号 000042 2011/05/18 追加終了

        #region 暦上日チェック 
        // ************************************************************************************************
        // * メソッド名     暦上日チェック
        // * 
        // * 構文           Private Function CheckDate(ByVal strDate As String) As Boolean
        // * 
        // * 機能　　    　　暦上日チェックを行なう
        // * 
        // * 引数           strDate As String
        // * 
        // * 戻り値         Boolean
        // ************************************************************************************************
        [SecuritySafeCritical]
        private bool CheckDate(string strDate)
        {
            const string THIS_METHOD_NAME = "CheckDate";
            bool blnResult;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                m_cfDate.p_strDateValue = strDate;
                blnResult = m_cfDate.CheckDate;

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }

            return blnResult;

        }

        #endregion

        #region 西暦末日算出 
        // ************************************************************************************************
        // * メソッド名     西暦末日算出
        // * 
        // * 構文           Private Function GetSeirekiLastDay(ByVal strDate As String) As String
        // * 
        // * 機能　　    　　西暦の末日算出を行なう
        // * 
        // * 引数           strDate As String
        // * 
        // * 戻り値         String
        // ************************************************************************************************
        [SecuritySafeCritical]
        private string GetSeirekiLastDay(string strDate)
        {
            const string THIS_METHOD_NAME = "GetSeirekiLastDay";
            string strResult;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                m_cfDate.p_strDateValue = strDate.RSubstring(0, 6) + "01";
                strResult = m_cfDate.GetLastDay();

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }

            return strResult;

        }

        #endregion

        #region 和暦末日算出 
        // ************************************************************************************************
        // * メソッド名     和暦末日算出
        // * 
        // * 構文           Private Function GetWarekiLastDay(ByVal strDate As String) As String
        // * 
        // * 機能　　    　　和暦の末日算出を行なう
        // * 
        // * 引数           String
        // * 
        // * 戻り値         Boolean
        // ************************************************************************************************
        [SecuritySafeCritical]
        private string GetWarekiLastDay(string strDate, string strSeireki)
        {
            const string THIS_METHOD_NAME = "GetWarekiLastDay";
            string strWork;
            string strResult;

            try
            {
                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strWork = GetSeirekiLastDay(strSeireki);
                strResult = strDate.RSubstring(0, 5) + strWork.RSubstring(6, 2);

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }

            return strResult;

        }

        #endregion

    }
}
