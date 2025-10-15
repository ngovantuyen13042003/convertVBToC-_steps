// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        検索氏名編集(ABKensakuShimeiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/18　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/11 000001     区切り文字の変更
// * 2005/04/04 000002     全角でのあいまい検索を可能にする(マルゴ村山)
// * 2007/09/03 000003     多摩市用編集サブルーチンのオーバーロード（中沢）
// * 2007/10/10 000004     標準の仕様でも氏名がアルファベットの場合は大文字に変換する（中沢）
// * 2007/11/06 000005     検索カナ姓名編集パターンの修正、検索カナ項目メンバ変数を初期化（中沢）
// * 2011/09/26 000006     全角アルファベット検索時の清音化判定処理を追加（比嘉）
// * 2012/01/20 000007     【AB17051】アルファベット氏名検索機能の改善(北村)
// * 2020/01/10 000008     【AB32001】アルファベット検索（石合）
// * 2023/12/04 000009     【AB-1600-1】検索機能対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Security;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace Densan.Reams.AB.AB000BB
{

    public class ABKensakuShimeiBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFConfigDataClass m_cfConfigData;     // 環境情報データクラス
        private UFControlData m_cfUFControlData;      // コントロールデータ
        private USRuijiClass m_cRuijiClass;       // 類似文字クラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKensakuShimeiBClass";
        private const string BUBUNITCHI = "2";

        // パラメータのメンバ変数
        private string m_strSearchkanjimei;           // 検索用漢字名称（全角漢字　Max４０文字）
        private string m_strSearchKanaseimei;         // 検索用カナ姓名（半角カナ　Max４０文字）
        private string m_strSearchKanasei;            // 検索用カナ姓　（半角カナ　Max２４文字）
        private string m_strSearchKanamei;            // 検索用カナ名　（半角カナ　Max１６文字）

        // 各メンバ変数のプロパティ定義
        public string p_strSearchkanjimei
        {
            get
            {
                return m_strSearchkanjimei;
            }
        }
        public string p_strSearchKanaseimei
        {
            get
            {
                return m_strSearchKanaseimei;
            }
        }
        public string p_strSearchKanasei
        {
            get
            {
                return m_strSearchKanasei;
            }
        }
        public string p_strSearchKanamei
        {
            get
            {
                return m_strSearchKanamei;
            }
        }

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal csUFControlData As UFControlData, 
        // *                               ByVal csUFConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            csUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 csUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABKensakuShimeiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {
            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            m_cfConfigData = cfConfigData;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strSearchkanjimei = string.Empty;
            m_strSearchKanaseimei = string.Empty;
            m_strSearchKanasei = string.Empty;
            m_strSearchKanamei = string.Empty;
        }

        // ************************************************************************************************
        // * メソッド名      検索氏名取得
        // * 
        // * 構文            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
        // * 
        // * 機能　　        氏名を検索キーとして編集する
        // * 
        // * 引数            strAimai As String        :前方一致
        // *                 strShimei As String      ：氏名
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        // *履歴番号 000003 2007/09/03 修正開始
        public void GetKensakuShimei(string strAimai, string strShimei)
        {
            // 'Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
            // Const THIS_METHOD_NAME As String = "GetKensakuShimei"                   'メソッド名
            // Dim cuString As New USStringClass
            // Dim strHenshu As String = String.Empty              '引数の編集名称を格納
            // Dim strHenshuSei As String = String.Empty           '編集名称姓
            // Dim strHenshuMei As String = String.Empty           '編集名称名
            // Dim intIchi As Integer = 0                          '桁位置
            // '04/02/28 追加開始
            // Dim strChkHenshu As String = String.Empty           'ひらがらチェック
            // '04/02/28 追加終了

            // Try
            // 'デバッグ開始ログ出力
            // m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            // '04/02/28 追加開始
            // If cuString.ToHankaku(strShimei, strChkHenshu) Then
            // strShimei = strChkHenshu
            // End If
            // '04/02/28 追加終了

            // strHenshu = strShimei

            // '* 履歴番号 000002 2005/04/04 修正開始
            // 'If (UFStringClass.CheckKanjiCode(strHenshu, m_cfConfigData)) Then
            // If (UFStringClass.CheckKanjiCode(strHenshu.Trim("%"c).Trim("％"c), m_cfConfigData)) Then
            // '* 履歴番号 000002 2005/04/04 修正終了
            // '全角
            // '* 履歴番号 000001 2003/03/11 修正開始
            // 'intIchi = InStr(strHenshu, "：")
            // intIchi = InStr(strHenshu, "＊")
            // '* 履歴番号 000001 2003/03/11 修正終了
            // If (intIchi > 0) Then
            // Mid(strHenshu, intIchi, 1) = "　"
            // End If
            // '* 履歴番号 000002 2005/04/04 追加開始
            // intIchi = InStr(strHenshu, "％")
            // If (intIchi > 0) Then
            // Mid(strHenshu, intIchi, 1) = "%"
            // End If
            // '* 履歴番号 000002 2005/04/04 追加終了
            // If (strAimai = "1") Then
            // strHenshu = strHenshu + "%"
            // End If
            // m_strSearchkanjimei = strHenshu
            // Else
            // '半角
            // '* 履歴番号 000002 2005/04/04 追加開始
            // intIchi = InStr(strShimei, "％")
            // If (intIchi > 0) Then
            // Mid(strHenshu, intIchi, 1) = "%"
            // End If
            // '* 履歴番号 000002 2005/04/04 追加終了
            // '* 履歴番号 000001 2003/03/11 修正開始
            // 'intIchi = InStr(strShimei, ":")
            // intIchi = InStr(strShimei, "*")
            // '* 履歴番号 000001 2003/03/11 修正終了
            // If (intIchi = 0) Then
            // intIchi = InStr(strShimei, " ")
            // End If
            // If (intIchi <> 0) Then
            // '分割
            // '姓
            // strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
            // If (strAimai = "1") Then
            // strHenshuSei = strHenshuSei + "%"
            // End If
            // m_strSearchKanasei = strHenshuSei
            // '名
            // strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
            // If (strAimai = "1") Then
            // strHenshuMei = strHenshuMei + "%"
            // End If
            // m_strSearchKanamei = strHenshuMei
            // Else
            // '分割なし
            // strHenshu = cuString.ToKanaKey(strHenshu)
            // If (strAimai = "1") Then
            // strHenshu = strHenshu + "%"
            // End If
            // m_strSearchKanaseimei = strHenshu
            // End If
            // End If

            // 'デバッグ終了ログ出力
            // m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            // Catch objExp As Exception
            // 'エラーログ出力
            // m_cfUFLogClass.ErrorWrite(m_cfUFControlData, _
            // "【クラス名:" + THIS_CLASS_NAME + "】" _
            // + "【メソッド名:" + THIS_METHOD_NAME + "】" _
            // + "【エラー内容:" + objExp.Message + "】")
            // 'エラーをそのままスローする
            // Throw objExp
            // End Try

            GetKensakuShimei(strAimai, strShimei, 0);
            // *履歴番号 000003 2007/09/03 修正終了
        }

        // *履歴番号 000003 2007/09/03 追加開始
        // ************************************************************************************************
        // * メソッド名      検索氏名取得（オーバーロード）
        // * 
        // * 構文            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String, 
        // *                                                                  ByVal intHommyoYusen As Integer)
        // * 
        // * 機能　　        氏名を検索キーとして編集する
        // * 
        // * 引数            strAimai As String        :前方一致
        // *                 strShimei As String      ：氏名
        // *                 intHommyoYusen As Integer：標準(0)，本名(1)，通称名(2)
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        [SecuritySafeCritical]
        public void GetKensakuShimei(string strAimai, string strShimei, int intHommyoYusen)
        {
            const string THIS_METHOD_NAME = "GetKensakuShimei";                   // メソッド名
            var cuString = new USStringClass();
            string strHenshu = string.Empty;              // 引数の編集名称を格納
            string strHenshuSei = string.Empty;           // 編集名称姓
            string strHenshuMei = string.Empty;           // 編集名称名
            int intIchi = 0;                          // 桁位置
            string strChkHenshu = string.Empty;           // ひらがなチェック
            UFRdbClass cfRdb;                             // RDBクラス
            URKANRIJOHOCacheBClass crKanriJohoB;          // 管理情報Ｂクラス
            FrnHommyoKensakuType enGaikokujinKensakuKB;   // 外国人本名検索区分
                                                          // *履歴番号 000006 2011/09/26 追加開始
            ABAtenaKanriJohoBClass cABKanriJohoB;         // 宛名管理情報クラス
            DataSet csABKanriJohoDS;
            string strZenAlphabetKB;
            // *履歴番号 000006 2011/09/26 追加終了

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // RDBクラスのインスタンス作成
                cfRdb = new UFRdbClass(m_cfUFControlData.m_strBusinessId);

                // *履歴番号 000005 2007/11/06 追加開始
                // 検索用メンバ変数初期化
                m_strSearchkanjimei = string.Empty;
                m_strSearchKanaseimei = string.Empty;
                m_strSearchKanasei = string.Empty;
                m_strSearchKanamei = string.Empty;
                // *履歴番号 000005 2007/11/06 追加終了

                // 宛名取得ビジネスクラスのインスタンス作成
                crKanriJohoB = new URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfConfigData, cfRdb);
                // 管理情報取得メソッド実行
                enGaikokujinKensakuKB = crKanriJohoB.GetFrn_HommyoKensaku_Param();

                // *履歴番号 000006 2011/09/26 追加開始
                // 宛名管理情報クラスのインスタンス化
                cABKanriJohoB = new ABAtenaKanriJohoBClass(m_cfUFControlData, m_cfConfigData, cfRdb);
                // 管理情報取得メソッド実行(検索画面(03)、全角アルファベット検索制御(14))
                csABKanriJohoDS = cABKanriJohoB.GetKanriJohoHoshu("03", "14");

                // 管理情報チェック
                if (csABKanriJohoDS is not null && csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0)
                {
                    strZenAlphabetKB = csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString;
                }
                else
                {
                    strZenAlphabetKB = "0";
                }
                // *履歴番号 000006 2011/09/26 追加終了

                if (m_cRuijiClass is null)
                {
                    m_cRuijiClass = new USRuijiClass();
                }

                if (cuString.ToHankaku(strShimei, strChkHenshu))
                {
                    // *履歴番号 000006 2011/09/26 追加開始
                    if (strZenAlphabetKB == "1")
                    {
                        // 管理情報：検索画面・全角アルファベット検索制御(03・14) = "1" の場合
                        if (UFStringClass.CheckAlphabetNumber(strChkHenshu.Replace(" ", "").Trim('%').Trim('*').Trim('.').Trim('･')))
                        {
                            if ((strShimei ?? "") == (strChkHenshu ?? ""))
                            {
                                // 入力が半角アルファベットということになるため半角で検索させる
                                strShimei = strChkHenshu;
                            }
                            // *履歴番号 000007 2012/01/20 修正開始
                            else if (strChkHenshu == "*")
                            {
                                // 半角変換後の値が'*'の場合、'*'で検索させる
                                strShimei = strChkHenshu;
                            }
                            // *履歴番号 000007 2012/01/20 修正終了
                            else
                            {
                                // *履歴番号 000008 2020/01/10 修正開始
                                // '入力が全角アルファベットということだから全角で検索させる
                                // 入力が全角アルファベットということだから全角半角両方で検索させる
                                SetSearchKanjiShimei(strShimei, strAimai);
                                strShimei = strChkHenshu;
                                // *履歴番号 000008 2020/01/10 修正終了
                            }
                        }
                        else
                        {
                            // アルファベットではないので通常通り半角での検索
                            strShimei = strChkHenshu;
                        }
                    }
                    else
                    {
                        strShimei = strChkHenshu;
                    }
                    // strShimei = strChkHenshu
                    // *履歴番号 000006 2011/09/26 追加終了
                }

                strHenshu = strShimei;

                if (UFStringClass.CheckKanjiCode(strHenshu.Trim('%').Trim('％'), m_cfConfigData))
                {
                    // 全角
                    intIchi = Strings.InStr(strHenshu, "＊");
                    if (intIchi > 0)
                    {
                        StringType.MidStmtStr(ref strHenshu, intIchi, 1, "　");
                    }
                    strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("　", string.Empty)).ToUpper;
                    intIchi = Strings.InStr(strHenshu, "％");
                    if (intIchi > 0)
                    {
                        StringType.MidStmtStr(ref strHenshu, intIchi, 1, "%");
                    }
                    if (strAimai == "1")
                    {
                        strHenshu = strHenshu + "%";
                    }
                    else if ((strAimai ?? "") == BUBUNITCHI)
                    {
                        strHenshu = "%" + strHenshu + "%";
                    }
                    m_strSearchkanjimei = strHenshu;
                }
                else
                {
                    // 半角
                    intIchi = Strings.InStr(strShimei, "％");
                    if (intIchi > 0)
                    {
                        StringType.MidStmtStr(ref strHenshu, intIchi, 1, "%");
                    }
                    intIchi = Strings.InStr(strShimei, "*");
                    if (intIchi == 0)
                    {
                        intIchi = Strings.InStr(strShimei, " ");
                    }

                    // 本名優先検索パラメータが１，２以外のときはAtenaGetのインターフェース用に検索カナ用変数を設定
                    // 外国人本名検索機能区分が標準のときはAtenaGetのインターフェース用に検索カナ用変数を設定
                    // *履歴番号 000003 2007/09/03以前からGetKensakuShimeiを使用している業務には影響なし。
                    if (enGaikokujinKensakuKB == FrnHommyoKensakuType.Tsusho || intHommyoYusen != 1 && intHommyoYusen != 2)
                    {
                        // 標準仕様
                        if (intIchi != 0)
                        {
                            // 分割
                            // 姓
                            // * 履歴番号 000004 2007/10/10 修正開始
                            strHenshuSei = cuString.ToKanaKey(Strings.Left(strHenshu, intIchi - 1)).ToUpper();
                            // strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
                            // * 履歴番号 000004 2007/10/10 修正終了
                            if (strAimai == "1")
                            {
                                strHenshuSei = strHenshuSei + "%";
                            }
                            m_strSearchKanasei = strHenshuSei;
                            // 名
                            // * 履歴番号 000004 2007/10/10 修正開始
                            strHenshuMei = cuString.ToKanaKey(Strings.Mid(strHenshu, intIchi + 1)).ToUpper();
                            // strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
                            // * 履歴番号 000004 2007/10/10 修正終了
                            if (strAimai == "1")
                            {
                                strHenshuMei = strHenshuMei + "%";
                            }
                            m_strSearchKanamei = strHenshuMei;
                            if ((strAimai ?? "") == BUBUNITCHI)
                            {
                                m_strSearchKanasei = string.Empty;
                                m_strSearchKanamei = string.Empty;
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", string.Empty).Replace("*", string.Empty)).ToUpper();
                                strHenshu = "%" + strHenshu + "%";
                                m_strSearchKanaseimei = strHenshu;
                            }
                        }
                        else
                        {
                            // 分割なし
                            // * 履歴番号 000004 2007/10/10 修正開始
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper();
                            // strHenshu = cuString.ToKanaKey(strHenshu)
                            // * 履歴番号 000004 2007/10/10 修正終了
                            if (strAimai == "1")
                            {
                                strHenshu = strHenshu + "%";
                            }
                            else if ((strAimai ?? "") == BUBUNITCHI)
                            {
                                strHenshu = "%" + strHenshu + "%";
                            }
                            m_strSearchKanaseimei = strHenshu;
                        }
                    }
                    // 本名と通称名の両方で検索可能なＤＢ
                    // アルファベットは全て大文字でセットする
                    else if (intHommyoYusen == 2)
                    {
                        // 本名優先検索以外
                        // 検索カナ姓名　検索カナ名に検索文字列がセットされる
                        // カナ通称名の場合
                        if (intIchi != 0)
                        {
                            // *履歴番号 000005 2007/11/06 修正開始
                            // 分割あり カナ姓カナ名を抽出
                            strHenshuSei = cuString.ToKanaKey(Strings.Left(strHenshu, intIchi - 1)).ToUpper();
                            strHenshuMei = cuString.ToKanaKey(Strings.Mid(strHenshu, intIchi + 1)).ToUpper();
                            if (strAimai == "1")    // 曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                            {
                                if (!string.IsNullOrEmpty(strHenshuSei.Trim()))
                                {
                                    m_strSearchKanaseimei = strHenshuSei + "%";  // 検索カナ姓
                                }
                                m_strSearchKanamei = strHenshuMei + "%";     // 検索カナ名
                            }
                            else if ((strAimai ?? "") == BUBUNITCHI)
                            {
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", string.Empty)).ToUpper();
                                strHenshu = "%" + strHenshu + "%";
                                m_strSearchKanaseimei = strHenshu;
                            }
                            // 完全一致
                            // 検索カナ姓名
                            else if (!string.IsNullOrEmpty(strHenshuSei.Trim()))
                            {
                                m_strSearchKanaseimei = cuString.ToKanaKey(strHenshu.Replace(" ", string.Empty)).ToUpper();
                            }
                            else
                            {
                                m_strSearchKanamei = strHenshuMei;
                            }
                        }
                        // '分割あり カナ姓カナ名を抽出
                        // strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                        // strHenshuMei = cuString.ToKanaKey((Mid(strHenshu, intIchi + 1))).ToUpper()
                        // If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                        // strHenshuMei = strHenshuMei + "%"
                        // End If
                        // m_strSearchKanaseimei = strHenshuSei + "%"  '検索カナ姓（曖昧の有無にかかわらず％が付加される）
                        // m_strSearchKanamei = strHenshuMei           '検索カナ名
                        // *履歴番号 000005 2007/11/06 修正終了
                        else
                        {
                            // 分割なし
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper();
                            if (strAimai == "1")
                            {
                                strHenshu = strHenshu + "%";
                            }
                            else if ((strAimai ?? "") == BUBUNITCHI)
                            {
                                strHenshu = "%" + strHenshu + "%";
                            }
                            m_strSearchKanaseimei = strHenshu;
                        }           // 検索カナ姓名
                    }
                    // 本名優先検索
                    // カナ本名の場合（検索カナ姓のみで検索可能にする変数を生成）
                    // 検索カナ姓に検索文字列がセットされる
                    else if (intIchi != 0)
                    {
                        // *履歴番号 000005 2007/11/06 修正開始
                        // 分割ありの場合姓名分割
                        strHenshuSei = cuString.ToKanaKey(Strings.Left(strHenshu, intIchi - 1)).ToUpper();
                        strHenshuMei = cuString.ToKanaKey(Strings.Mid(strHenshu, intIchi + 1)).ToUpper();
                        if (strAimai == "1")    // 曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                        {
                            strHenshuSei = strHenshuSei + "%";
                            strHenshuMei = strHenshuMei + "%";
                            // 本名カナ名称は検索用カナ姓名で返される（検索カナ姓と検索カナ名を結合）
                            m_strSearchKanasei = strHenshuSei + strHenshuMei;
                        }
                        else if ((strAimai ?? "") == BUBUNITCHI)
                        {
                            strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", string.Empty)).ToUpper();
                            strHenshu = "%" + strHenshu + "%";
                            m_strSearchKanaseimei = strHenshu;
                        }
                        // 完全一致の場合
                        else if (string.IsNullOrEmpty(strHenshuSei.Trim()))
                        {
                            m_strSearchKanasei = "%" + strHenshuMei;
                        }
                        else
                        {
                            m_strSearchKanasei = cuString.ToKanaKey(strHenshu.Replace(" ", string.Empty)).ToUpper();
                        }
                    }
                    // '分割ありの場合姓名分割
                    // strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                    // strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                    // If (strAimai = "1") Then    '曖昧検索（前方一致チェックがTrue）のとき"%"を付加
                    // strHenshuSei = strHenshuSei + "%"
                    // strHenshuMei = strHenshuMei + "%"
                    // End If
                    // '本名カナ名称は検索用カナ姓名で返される（検索カナ姓と検索カナ名を結合）
                    // m_strSearchKanasei = strHenshuSei + strHenshuMei
                    // *履歴番号 000005 2007/11/06 修正終了
                    else
                    {
                        // 分割なしの場合そのまま曖昧検索を付加
                        strHenshu = cuString.ToKanaKey(strHenshu).ToUpper();
                        if (strAimai == "1")
                        {
                            strHenshu = strHenshu + "%";
                        }
                        else if ((strAimai ?? "") == BUBUNITCHI)
                        {
                            strHenshu = "%" + strHenshu + "%";
                        }
                        // 本名カナ名称は検索用カナ姓名で返される
                        m_strSearchKanasei = strHenshu;
                    }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");

                // エラーをそのままスローする
                throw objExp;
            }

        }
        // *履歴番号 000003 2007/09/03 追加終了

        // *履歴番号 000008 2020/01/10 追加開始
        /// <summary>
    /// 検索用漢字氏名設定
    /// </summary>
    /// <param name="strShimei">対象文字列</param>
    /// <param name="strAimai">あいまい検索</param>
    /// <remarks></remarks>
        private void SetSearchKanjiShimei(string strShimei, string strAimai)
        {

            string strHenshu;
            int intIchi;

            try
            {

                strHenshu = strShimei;
                intIchi = Strings.InStr(strHenshu, "＊");
                if (intIchi > 0)
                {
                    StringType.MidStmtStr(ref strHenshu, intIchi, 1, "　");
                }
                strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("　", string.Empty)).ToUpper;
                intIchi = Strings.InStr(strHenshu, "％");
                if (intIchi > 0)
                {
                    StringType.MidStmtStr(ref strHenshu, intIchi, 1, "%");
                }
                if (strAimai == "1")
                {
                    strHenshu = strHenshu + "%";
                }
                else if ((strAimai ?? "") == BUBUNITCHI)
                {
                    strHenshu = "%" + strHenshu + "%";
                }
                m_strSearchkanjimei = strHenshu;
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        /// <summary>
    /// 氏名検索条件生成
    /// </summary>
    /// <param name="cSearchKey">宛名検索キー</param>
    /// <param name="strTableName">テーブル名</param>
    /// <param name="csWhere">作成中条件</param>
    /// <param name="cfParamCollection">パラメーターコレクション</param>
    /// <remarks></remarks>
        public void CreateWhereForShimei(ABAtenaSearchKey cSearchKey, string strTableName, ref StringBuilder csWhere, ref UFParameterCollectionClass cfParamCollection)
        {

            StringBuilder csWhereForKanaShimei;
            StringBuilder csWhereForKanjiShimei;
            UFParameterClass cfParam;

            try
            {

                // カナ検索部、漢字検索部に１つでも値が存在する場合に検索条件を追加する
                if (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 || cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 || cSearchKey.p_enGaikokuHommyoKensaku == FrnHommyoKensakuType.Tsusho_Seishiki && cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)





                {

                    if (csWhere.RLength > 0)
                    {
                        csWhere.Append(" AND ");
                    }
                    else
                    {
                        // noop
                    }

                    // ---------------------------------------------------------------------------------
                    // カナ検索部編集
                    csWhereForKanaShimei = new StringBuilder();

                    // -----------------------------------------------------------------------------
                    // 検索用カナ姓名
                    if (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0)
                    {

                        if (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock = ref cfParam;
                                withBlock.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI;
                                withBlock.Value = cSearchKey.p_strSearchKanaSeiMei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock1 = ref cfParam;
                                withBlock1.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI;
                                withBlock1.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 検索用カナ姓
                    if (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0)
                    {

                        if (csWhereForKanaShimei.RLength > 0)
                        {
                            csWhereForKanaShimei.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0)
                        {
                            csWhereForKanaShimei.Append("(");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock2 = ref cfParam;
                                withBlock2.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI;
                                withBlock2.Value = cSearchKey.p_strSearchKanaSei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock3 = ref cfParam;
                                withBlock3.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI;
                                withBlock3.Value = cSearchKey.p_strSearchKanaSei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 検索カナ姓２
                    if (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0)
                    {

                        csWhereForKanaShimei.Append(" OR ");

                        if (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock4 = ref cfParam;
                                withBlock4.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2;
                                withBlock4.Value = cSearchKey.p_strSearchKanaSei2;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock5 = ref cfParam;
                                withBlock5.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2;
                                withBlock5.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);

                        csWhereForKanaShimei.Append(")");
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 検索用カナ名
                    if (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0)
                    {

                        if (csWhereForKanaShimei.RLength > 0)
                        {
                            csWhereForKanaShimei.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock6 = ref cfParam;
                                withBlock6.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI;
                                withBlock6.Value = cSearchKey.p_strSearchKanaMei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock7 = ref cfParam;
                                withBlock7.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI;
                                withBlock7.Value = cSearchKey.p_strSearchKanaMei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                        // -----------------------------------------------------------------------------

                    }
                    // ---------------------------------------------------------------------------------

                    // ---------------------------------------------------------------------------------
                    // 漢字検索部編集
                    csWhereForKanjiShimei = new StringBuilder();

                    // -----------------------------------------------------------------------------
                    // 検索用漢字名称
                    if (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0)
                    {

                        if (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0)
                        {

                            csWhereForKanjiShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock8 = ref cfParam;
                                withBlock8.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO;
                                withBlock8.Value = cSearchKey.p_strSearchKanjiMeisho;
                            }
                        }

                        else
                        {

                            csWhereForKanjiShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock9 = ref cfParam;
                                withBlock9.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO;
                                withBlock9.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 漢字氏名２
                    if (cSearchKey.p_enGaikokuHommyoKensaku == FrnHommyoKensakuType.Tsusho_Seishiki)
                    {

                        if (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0)
                        {

                            if (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0)
                            {

                                csWhereForKanjiShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2);

                                // 検索条件のパラメータを作成
                                cfParam = new UFParameterClass();
                                {
                                    ref var withBlock10 = ref cfParam;
                                    withBlock10.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2;
                                    withBlock10.Value = cSearchKey.p_strKanjiMeisho2;
                                }
                            }

                            else
                            {

                                csWhereForKanjiShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2);

                                // 検索条件のパラメータを作成
                                cfParam = new UFParameterClass();
                                cfParam.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2;
                                cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd;

                            }

                            // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfParamCollection.Add(cfParam);
                        }

                        else
                        {
                            // noop
                        }
                    }

                    else
                    {
                        // noop
                        // -----------------------------------------------------------------------------

                    }
                    // ---------------------------------------------------------------------------------

                    // ---------------------------------------------------------------------------------
                    // カナ検索部と漢字検索部が両方設定されている場合、ＯＲ条件で連結する
                    if (csWhereForKanaShimei.RLength > 0)
                    {
                        if (csWhereForKanjiShimei.RLength > 0)
                        {
                            csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString(), csWhereForKanjiShimei.ToString());
                        }
                        else
                        {
                            csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString());
                        }
                    }
                    else
                    {
                        csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString());
                    }
                }
                // ---------------------------------------------------------------------------------

                else
                {
                    // noop
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

        }
        // *履歴番号 000008 2020/01/10 追加終了

        /// <summary>
    /// 氏名検索条件生成(オーバーロード)
    /// </summary>
    /// <param name="cSearchKey">宛名検索キー</param>
    /// <param name="strTableName">テーブル名</param>
    /// <param name="csWhere">作成中条件</param>
    /// <param name="cfParamCollection">パラメーターコレクション</param>
    /// <param name="strFZYHyojunTableName">宛名付随標準テーブル名</param>
    /// <param name="blnFromAtenaRireki">宛名履歴判定フラグ:Optional-False</param>
    /// <param name="intHyojunKB">標準化版判定:Optional通常</param>
    /// <remarks></remarks>
        public void CreateWhereForShimei(ABAtenaSearchKey cSearchKey, string strTableName, ref StringBuilder csWhere, ref UFParameterCollectionClass cfParamCollection, string strFZYHyojunTableName, bool blnFromAtenaRireki = false, ABEnumDefine.HyojunKB intHyojunKB = ABEnumDefine.HyojunKB.KB_Tsujo)
        {

            StringBuilder csWhereForKanaShimei;
            StringBuilder csWhereForKanjiShimei;
            UFParameterClass cfParam;
            string strWhereFZYHyojunKana;
            string strWhereFzyHyojunKanji;

            try
            {

                // カナ検索部、漢字検索部に１つでも値が存在する場合に検索条件を追加する
                if (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 || cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 || cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 || cSearchKey.p_enGaikokuHommyoKensaku == FrnHommyoKensakuType.Tsusho_Seishiki && cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)





                {

                    if (csWhere.RLength > 0)
                    {
                        csWhere.Append(" AND ");
                    }
                    else
                    {
                        // noop
                    }

                    // ---------------------------------------------------------------------------------
                    // カナ検索部編集
                    csWhereForKanaShimei = new StringBuilder();

                    // -----------------------------------------------------------------------------
                    // 検索用カナ姓名
                    if (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0)
                    {
                        strWhereFZYHyojunKana = CreateWhereFZYHyojunKana(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB);
                        if (strWhereFZYHyojunKana.RLength > 0)
                        {
                            csWhereForKanaShimei.Append("(");
                        }
                        if (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0)
                        {
                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock = ref cfParam;
                                withBlock.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI;
                                withBlock.Value = cSearchKey.p_strSearchKanaSeiMei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock1 = ref cfParam;
                                withBlock1.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI;
                                withBlock1.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                        if (strWhereFZYHyojunKana.RLength > 0)
                        {
                            if (blnFromAtenaRireki)
                            {
                                csWhereForKanaShimei.Append(strWhereFZYHyojunKana);
                            }
                            else
                            {
                                csWhereForKanaShimei.Append(" OR ");
                                csWhereForKanaShimei.AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD);
                                csWhereForKanaShimei.AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD);
                                csWhereForKanaShimei.AppendFormat(" WHERE {0}", strWhereFZYHyojunKana);
                                csWhereForKanaShimei.Append("))");
                            }
                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd;
                            cfParamCollection.Add(cfParam);
                        }
                    }
                    else
                    {
                        // noop
                    }

                    // -----------------------------------------------------------------------------
                    // 検索用カナ姓
                    if (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0)
                    {

                        if (csWhereForKanaShimei.RLength > 0)
                        {
                            csWhereForKanaShimei.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0)
                        {
                            csWhereForKanaShimei.Append("(");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock2 = ref cfParam;
                                withBlock2.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI;
                                withBlock2.Value = cSearchKey.p_strSearchKanaSei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock3 = ref cfParam;
                                withBlock3.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI;
                                withBlock3.Value = cSearchKey.p_strSearchKanaSei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 検索カナ姓２
                    if (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0)
                    {

                        csWhereForKanaShimei.Append(" OR ");

                        if (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock4 = ref cfParam;
                                withBlock4.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2;
                                withBlock4.Value = cSearchKey.p_strSearchKanaSei2;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock5 = ref cfParam;
                                withBlock5.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2;
                                withBlock5.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);

                        csWhereForKanaShimei.Append(")");
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 検索用カナ名
                    if (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0)
                    {

                        if (csWhereForKanaShimei.RLength > 0)
                        {
                            csWhereForKanaShimei.Append(" AND ");
                        }
                        else
                        {
                            // noop
                        }

                        if (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0)
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock6 = ref cfParam;
                                withBlock6.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI;
                                withBlock6.Value = cSearchKey.p_strSearchKanaMei;
                            }
                        }

                        else
                        {

                            csWhereForKanaShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock7 = ref cfParam;
                                withBlock7.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI;
                                withBlock7.Value = cSearchKey.p_strSearchKanaMei.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                        // -----------------------------------------------------------------------------

                    }
                    // ---------------------------------------------------------------------------------

                    // ---------------------------------------------------------------------------------
                    // 漢字検索部編集
                    csWhereForKanjiShimei = new StringBuilder();

                    if (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0 || cSearchKey.p_enGaikokuHommyoKensaku == 2 & cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0)
                    {
                        strWhereFzyHyojunKanji = CreateWhereFZYHyojunKanji(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB);
                    }
                    else
                    {
                        strWhereFzyHyojunKanji = string.Empty;
                    }
                    if (strWhereFzyHyojunKanji.RLength > 0)
                    {
                        csWhereForKanjiShimei.Append("(");
                    }
                    // -----------------------------------------------------------------------------
                    // 検索用漢字名称
                    if (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0)
                    {

                        if (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0)
                        {

                            csWhereForKanjiShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock8 = ref cfParam;
                                withBlock8.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO;
                                withBlock8.Value = cSearchKey.p_strSearchKanjiMeisho;
                            }
                        }

                        else
                        {

                            csWhereForKanjiShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO);

                            // 検索条件のパラメータを作成
                            cfParam = new UFParameterClass();
                            {
                                ref var withBlock9 = ref cfParam;
                                withBlock9.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO;
                                withBlock9.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd;
                            }

                        }

                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfParamCollection.Add(cfParam);
                    }

                    else
                    {
                        // noop
                    }
                    // -----------------------------------------------------------------------------
                    // 漢字氏名２
                    if (cSearchKey.p_enGaikokuHommyoKensaku == FrnHommyoKensakuType.Tsusho_Seishiki)
                    {

                        if (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0)
                        {

                            if (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0)
                            {

                                csWhereForKanjiShimei.AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2);

                                // 検索条件のパラメータを作成
                                cfParam = new UFParameterClass();
                                {
                                    ref var withBlock10 = ref cfParam;
                                    withBlock10.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2;
                                    withBlock10.Value = cSearchKey.p_strKanjiMeisho2;
                                }
                            }

                            else
                            {

                                csWhereForKanjiShimei.AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2);

                                // 検索条件のパラメータを作成
                                cfParam = new UFParameterClass();
                                {
                                    ref var withBlock11 = ref cfParam;
                                    withBlock11.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2;
                                    withBlock11.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd;
                                }

                            }

                            // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfParamCollection.Add(cfParam);
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
                    // -----------------------------------------------------------------------------
                    if (strWhereFzyHyojunKanji.RLength > 0)
                    {
                        if (blnFromAtenaRireki)
                        {
                            csWhereForKanjiShimei.Append(strWhereFzyHyojunKanji);
                        }
                        else
                        {
                            csWhereForKanjiShimei.Append(" OR ");
                            csWhereForKanjiShimei.AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD);
                            csWhereForKanjiShimei.AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD);
                            csWhereForKanjiShimei.AppendFormat(" WHERE {0}", strWhereFzyHyojunKanji);
                            csWhereForKanjiShimei.Append("))");
                        }
                        if (cSearchKey.p_strSearchKanjiMeisho.RLength > 0)
                        {
                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI;
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd;
                            cfParamCollection.Add(cfParam);
                        }
                        else
                        {
                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI;
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI;
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd;
                            cfParamCollection.Add(cfParam);

                            cfParam = new UFParameterClass();
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI;
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd;
                            cfParamCollection.Add(cfParam);
                        }

                    }
                    // ---------------------------------------------------------------------------------

                    // ---------------------------------------------------------------------------------
                    // カナ検索部と漢字検索部が両方設定されている場合、ＯＲ条件で連結する
                    if (csWhereForKanaShimei.RLength > 0)
                    {
                        if (csWhereForKanjiShimei.RLength > 0)
                        {
                            csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString(), csWhereForKanjiShimei.ToString());
                        }
                        else
                        {
                            csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString());
                        }
                    }
                    else
                    {
                        csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString());
                    }
                }
                // ---------------------------------------------------------------------------------

                else
                {
                    // noop
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        /// <summary>
    /// 抽出条件文字列の生成（宛名付随標準・カナ姓名用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <param name="strTable">テーブル名</param>
    /// <param name="blnRireki">履歴区分</param>
    /// <param name="intHyojunKB">標準化区分</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        public string CreateWhereFZYHyojunKana(ABAtenaSearchKey cSearchKey, string strTable, bool blnRireki, ABEnumDefine.HyojunKB intHyojunKB)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;

            try
            {

                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                if (blnRireki)
                {
                    if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        if (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") == -1)
                        {
                            csWHERE.Append("(");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI);
                            csWHERE.Append(")");
                        }
                        else
                        {
                            csWHERE.Append("(");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI);
                            csWHERE.Append(")");
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") == -1)
                {
                    csWHERE.Append("(");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI);
                    csWHERE.Append(")");
                }
                else
                {
                    csWHERE.Append("(");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI);
                    csWHERE.Append(")");
                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw cfAppExp;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                throw csExp;

            }

            return csWHERE.ToString();

        }

        /// <summary>
    /// 抽出条件文字列の生成（宛名付随標準・漢字姓名用）
    /// </summary>
    /// <param name="cSearchKey">検索キー</param>
    /// <param name="strTable">テーブル名</param>
    /// <param name="blnRireki">履歴区分</param>
    /// <param name="intHyojunKB">標準化区分</param>
    /// <returns>抽出条件文字列</returns>
    /// <remarks></remarks>
        public string CreateWhereFZYHyojunKanji(ABAtenaSearchKey cSearchKey, string strTable, bool blnRireki, ABEnumDefine.HyojunKB intHyojunKB)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            StringBuilder csWHERE;

            try
            {

                // デバッグログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                if (blnRireki)
                {
                    if (intHyojunKB == ABEnumDefine.HyojunKB.KB_Hyojun)
                    {
                        if (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") == -1)
                        {
                            csWHERE.Append("(");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
                            csWHERE.Append(" = ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI);
                            csWHERE.Append(")");
                        }
                        else
                        {
                            csWHERE.Append("(");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI);
                            csWHERE.Append(" OR ");
                            csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
                            csWHERE.Append(" LIKE ");
                            csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI);
                            csWHERE.Append(")");
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") == -1)
                {
                    csWHERE.Append("(");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
                    csWHERE.Append(" = ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI);
                    csWHERE.Append(")");
                }
                else
                {
                    csWHERE.Append("(");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI);
                    csWHERE.Append(" OR ");
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
                    csWHERE.Append(" LIKE ");
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI);
                    csWHERE.Append(")");
                }

                // デバッグログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");
                throw cfAppExp;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");

                throw csExp;

            }

            return csWHERE.ToString();

        }
    }
}
