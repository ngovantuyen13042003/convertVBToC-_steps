// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        編集検索氏名(ABHenshuSearchShimeiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2007/09/28　中沢　誠
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2007/10/10 000001     標準市町村の検索カナ項目がアルファベットの場合は大文字に変換（中沢）
// * 2023/08/14 000002    【AB-0820-1】住登外管理項目追加(早崎)
// ************************************************************************************************
using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABHenshuSearchShimeiBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLog;                       // ログ出力クラス
        private UFConfigDataClass m_cfConfigData;         // 環境情報データクラス
        private UFControlData m_cfControlData;            // コントロールデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABHenshuSearchShimeiBClass";
        // *履歴番号 000002 2023/08/14 追加開始
        private const int KANA_SEIMEI = 120;
        private const int KANA_SEI = 72;
        private const int KANA_MEI = 48;
        // *履歴番号 000002 2023/08/14 追加終了
        #endregion

        #region コンストラクタ
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
        public ABHenshuSearchShimeiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigData;

            // ログ出力クラスのインスタンス化
            m_cfLog = new UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId);

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     検索用カナ編集
        // * 
        // * 構文           Public Function GetSearchKana(ByVal strKanaMeisho As String, _
        // *                                              ByVal strKanaMeisho As String, _
        // *                                              ByVal enHommyKensakuKB As FrnHommyoKensakuType) As String()
        // * 
        // * 機能　　       検索用カナ名称を編集する
        // * 
        // * 引数           strKanaMeisho    As String                   : カナ名称１
        // *                strKanaMeisho2   As String                   : カナ名称２
        // *                enHommyKensakuKB As FrnHommyoKensakuType     : 本名優先検索区分
        // * 
        // * 戻り値         String()          : [0]検索用カナ姓名
        // *                                  : [1]検索用カナ姓
        // *                                  : [2]検索用カナ名
        // *                                  : [3]カナ姓
        // *                                  : [4]カナ名
        // ************************************************************************************************
        public string[] GetSearchKana(string strKanaMeisho, string strKanaMeisho2, FrnHommyoKensakuType enHommyKensakuKB)

        {
            const string THIS_METHOD_NAME = "GetSearchKana";                      // メソッド名
            var strSearchKana = new string[5];                      // 検索用カナ
            var cuString = new USStringClass();                   // 文字列編集
            int intIndex;                             // 先頭からの空白位置

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 標準（Tsusho：標準　Tsusho_Seishiki：本名と通称名で検索可能なDB）
                if (enHommyKensakuKB == FrnHommyoKensakuType.Tsusho)
                {

                    // カナ姓名 空白を詰めてから清音化する
                    // * 履歴番号 0000001 2007/10/10 修正開始
                    strSearchKana[0] = cuString.ToKanaKey(strKanaMeisho.Replace(" ", string.Empty)).ToUpper();
                    // strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty))
                    // * 履歴番号 0000001 2007/10/10 修正終了

                    // 先頭からの空白位置を調べる
                    intIndex = strKanaMeisho.RIndexOf(" ");

                    // 空白が存在しない場合
                    if (intIndex == -1)
                    {
                        // カナ姓・名
                        strSearchKana[1] = strSearchKana[0];
                        strSearchKana[3] = strKanaMeisho;
                        strSearchKana[2] = string.Empty;
                        strSearchKana[4] = string.Empty;
                    }
                    else
                    {
                        // カナ姓・名
                        // * 履歴番号 0000001 2007/10/10 修正開始
                        strSearchKana[1] = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex)).ToUpper();
                        // strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.Substring(0, intIndex))
                        // * 履歴番号 0000001 2007/10/10 修正終了
                        strSearchKana[3] = strKanaMeisho.RSubstring(0, intIndex);

                        // 先頭からの空白位置が文字列長と以上場合
                        if (intIndex + 1 >= strKanaMeisho.RLength())
                        {
                            strSearchKana[2] = string.Empty;
                            strSearchKana[4] = string.Empty;
                        }
                        else
                        {
                            // * 履歴番号 0000001 2007/10/10 修正開始
                            strSearchKana[2] = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper();
                            // strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.Substring(intIndex + 1))
                            // * 履歴番号 0000001 2007/10/10 修正終了
                            strSearchKana[4] = strKanaMeisho.RSubstring(intIndex + 1);
                        }
                    }
                }
                else
                {
                    // 本名と通称名で検索可能なDB

                    // カナ姓名 空白を詰めてから清音化する
                    strSearchKana[0] = cuString.ToKanaKey(strKanaMeisho.Replace(" ", string.Empty)).ToUpper();

                    // 先頭からの空白位置を調べる
                    intIndex = strKanaMeisho.RIndexOf(" ");

                    // 空白が存在しない場合カナ姓のみをセット
                    if (intIndex == -1)
                    {
                        // カナ姓
                        strSearchKana[1] = string.Empty;
                        strSearchKana[3] = strKanaMeisho;
                        strSearchKana[2] = string.Empty;
                        strSearchKana[4] = string.Empty;
                    }
                    else
                    {
                        // カナ姓（法人のみ使用）
                        strSearchKana[3] = strKanaMeisho.RSubstring(0, intIndex);

                        // 先頭からの空白位置が文字列長以上の場合
                        if (intIndex + 1 >= strKanaMeisho.RLength())
                        {
                            strSearchKana[2] = string.Empty;
                            strSearchKana[4] = string.Empty;
                        }
                        else
                        {
                            strSearchKana[2] = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper();
                            // カナ名（法人のみ使用）
                            strSearchKana[4] = strKanaMeisho.RSubstring(intIndex + 1);
                        }
                    }

                    // 本名カナ姓名
                    if (strKanaMeisho2.RLength() > 0)
                    {
                        strSearchKana[1] = cuString.ToKanaKey(strKanaMeisho2.Replace(" ", string.Empty)).ToUpper();
                    }
                    else
                    {
                        strSearchKana[1] = string.Empty;
                    }

                }

                // *履歴番号 000002 2023/08/14 修正開始
                // '検索カナ姓名の桁チェック
                // If strSearchKana(0).RLength() > 40 Then
                // strSearchKana(0) = strSearchKana(0).RSubstring(0, 40)
                // End If
                if (strSearchKana[0].RLength() > KANA_SEIMEI)
                {
                    strSearchKana[0] = strSearchKana[0].RSubstring(0, KANA_SEIMEI);
                }
                // *履歴番号 000002 2023/08/14 修正終了

                // *履歴番号 000002 2023/08/14 修正開始
                // '検索カナ姓の桁チェック
                // If strSearchKana(1).RLength() > 24 Then
                // strSearchKana(1) = strSearchKana(1).RSubstring(0, 24)
                // End If
                if (strSearchKana[1].RLength() > KANA_SEI)
                {
                    strSearchKana[1] = strSearchKana[1].RSubstring(0, KANA_SEI);
                }
                // *履歴番号 000002 2023/08/14 修正終了

                // *履歴番号 000002 2023/08/14 修正開始
                // '検索カナ名の桁チェック
                // If strSearchKana(2).RLength() > 16 Then
                // strSearchKana(2) = strSearchKana(2).RSubstring(0, 16)
                // End If
                if (strSearchKana[2].RLength() > KANA_MEI)
                {
                    strSearchKana[2] = strSearchKana[2].RSubstring(0, KANA_MEI);
                }
                // *履歴番号 000002 2023/08/14 修正終了

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

            return strSearchKana;

        }
        #endregion
    }
}
