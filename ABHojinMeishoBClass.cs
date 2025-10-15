// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        法人名称(ABHojinMeishoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/18　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/09/11 000001     チューニング
// * 2015/04/23 000002     支店名の連結時に値有無判定を追加（石合）
// ************************************************************************************************
using System;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABHojinMeishoBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFControlData m_cfUFControlData;      // コントロールデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABHojinMeishoBClass";

        // パラメータのメンバ変数
        private string m_strKeitaiFuyoKB;             // 区分（1桁）
        private string m_strKeitaiSeiRyakuKB;         // 区分（1桁）
        private string m_strKanjiHjnKeitai;           // 形態（全角　Max１０文字）
        private string m_strKanjiMeisho1;             // 名称（全角　Max４０文字）
        private string m_strKanjiMeisho2;             // 名称（全角　Max４０文字）

        // 各メンバ変数のプロパティ定義
        public string p_strKeitaiFuyoKB
        {
            set
            {
                m_strKeitaiFuyoKB = value;
            }
        }
        public string p_strKeitaiSeiRyakuKB
        {
            set
            {
                m_strKeitaiSeiRyakuKB = value;
            }
        }
        public string p_strKanjiHjnKeitai
        {
            set
            {
                m_strKanjiHjnKeitai = value;
            }
        }
        public string p_strKanjiMeisho1
        {
            set
            {
                m_strKanjiMeisho1 = value;
            }
        }
        public string p_strKanjiMeisho2
        {
            set
            {
                m_strKanjiMeisho2 = value;
            }
        }

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABHojinMeishoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {
            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);
            // パラメータのメンバ変数
            m_strKeitaiFuyoKB = string.Empty;
            m_strKeitaiSeiRyakuKB = string.Empty;
            m_strKanjiHjnKeitai = string.Empty;
            m_strKanjiMeisho1 = string.Empty;
            m_strKanjiMeisho2 = string.Empty;
        }

        // ************************************************************************************************
        // * メソッド名      法人名称編集
        // * 
        // * 構文            Public Function GetHojinMeisho() As String
        // * 
        // * 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          編集名称（String）
        // ************************************************************************************************
        public string GetHojinMeisho()
        {
            // *履歴番号 000001 2003/09/11 修正開始
            // Dim strKanjiMeisho As String = String.Empty

            // Try
            // 'デバッグ開始ログ出力
            // m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")

            // '法人の名称の編集
            // Select Case m_strKeitaiFuyoKB
            // Case "1"
            // Select Case m_strKeitaiSeiRyakuKB
            // Case "1"
            // strKanjiMeisho = m_strKanjiHjnKeitai + m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
            // Case Else
            // strKanjiMeisho = m_strKanjiHjnKeitai + "　" + m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
            // End Select
            // Case "2"
            // Select Case m_strKeitaiSeiRyakuKB
            // Case "1"
            // strKanjiMeisho = m_strKanjiMeisho1 + m_strKanjiHjnKeitai + m_strKanjiMeisho2
            // Case Else
            // strKanjiMeisho = m_strKanjiMeisho1 + "　" + m_strKanjiHjnKeitai + "　" + m_strKanjiMeisho2
            // End Select
            // Case Else
            // strKanjiMeisho = m_strKanjiMeisho1 + "　" + m_strKanjiMeisho2
            // End Select

            // 'デバッグ終了ログ出力
            // m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")
            // Catch objExp As Exception
            // 'エラーログ出力
            // m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKjnhjn】【エラー内容:" + objExp.Message + "】")
            // 'エラーをそのままスローする
            // Throw objExp
            // End Try

            // Return strKanjiMeisho

            StringBuilder strKanjiMeisho;
            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                strKanjiMeisho = new StringBuilder();
                // 法人の名称の編集
                switch (m_strKeitaiFuyoKB ?? "")
                {
                    case "1":
                        {
                            switch (m_strKeitaiSeiRyakuKB ?? "")
                            {
                                case "1":
                                    {
                                        // *履歴番号 000002 2015/04/23 修正開始
                                        // strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                                        strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1);
                                        // *履歴番号 000002 2015/04/23 修正終了
                                        strKanjiMeisho = AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2);
                                        break;
                                    }

                                default:
                                    {
                                        // *履歴番号 000002 2015/04/23 修正開始
                                        // strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                                        strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho1);
                                        strKanjiMeisho = AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2);
                                        break;
                                    }
                                    // *履歴番号 000002 2015/04/23 修正終了
                            }

                            break;
                        }
                    case "2":
                        {
                            switch (m_strKeitaiSeiRyakuKB ?? "")
                            {
                                case "1":
                                    {
                                        strKanjiMeisho.Append(m_strKanjiMeisho1).Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho2);
                                        break;
                                    }

                                default:
                                    {
                                        // *履歴番号 000002 2015/04/23 修正開始
                                        // strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiHjnKeitai).Append("　").Append(m_strKanjiMeisho2)
                                        strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiHjnKeitai);
                                        strKanjiMeisho = AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2);
                                        break;
                                    }
                                    // *履歴番号 000002 2015/04/23 修正終了
                            }

                            break;
                        }

                    default:
                        {
                            // *履歴番号 000002 2015/04/23 修正開始
                            // strKanjiMeisho.Append(m_strKanjiMeisho1).Append("　").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiMeisho1);
                            strKanjiMeisho = AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2);
                            break;
                        }
                        // *履歴番号 000002 2015/04/23 修正終了
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【エラー内容:" + objExp.Message + "】");


                throw objExp;
            }


            return strKanjiMeisho.ToString();
            // *履歴番号 000001 2003/09/11 修正終了

        }

        // *履歴番号 000002 2015/04/23 追加開始
        /// <summary>
    /// 法人名（および法人形態）に支店名を連結して返信します。
    /// </summary>
    /// <param name="csHojinmei">法人名（および法人形態）</param>
    /// <param name="strShitenmei">支店名</param>
    /// <returns></returns>
    /// <remarks>値有無判定、および設定値の前後空白は除去しない。</remarks>
        private StringBuilder AppendShitenmei(StringBuilder csHojinmei, string strShitenmei)

        {

            try
            {


                // 支店名が存在する場合に、全角空白＋支店名を連結する。
                if (strShitenmei.RLength() > 0)
                {
                    csHojinmei.Append("　");
                    csHojinmei.Append(strShitenmei);
                }
                else
                {
                    // noop

                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csHojinmei;

        }
        // *履歴番号 000002 2015/04/23 追加終了

    }
}
