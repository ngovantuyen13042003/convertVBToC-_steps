// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        名称(ABMeishoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/07/25　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// *
// ************************************************************************************************
using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABMeishoBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFControlData m_cfUFControlData;      // コントロールデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMeishoBClass";

        // パラメータのメンバ変数
        private string m_strKeitaiFuyoKB;                     // 区分（1桁）
        private string m_strKeitaiSeiRyakuKB;                 // 区分（1桁）
        private string m_strKanjiHjnKeitai;                   // 形態（全角　Max１０文字）
        private string m_strKanjiMeisho1;                     // 名称（全角　Max４０文字）
        private string m_strKanjiMeisho2;                     // 名称（全角　Max４０文字）
        private string m_strAtenaDataKB;                      // 宛名データ区分
        private ABHojinMeishoBClass m_cHojinMeishoBClass;     // 法人名称クラス

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
        public string p_strAtenaDataKB
        {
            set
            {
                m_strAtenaDataKB = value;
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
        public ABMeishoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {

            // メンバ変数セット
            m_cfUFControlData = cfControlData;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

            // 法人名称クラスのインスタンス作成
            m_cHojinMeishoBClass = new ABHojinMeishoBClass(cfControlData, cfConfigData);

            // パラメータのメンバ変数
            m_strKeitaiFuyoKB = string.Empty;
            m_strKeitaiSeiRyakuKB = string.Empty;
            m_strKanjiHjnKeitai = string.Empty;
            m_strKanjiMeisho1 = string.Empty;
            m_strKanjiMeisho2 = string.Empty;
            m_strAtenaDataKB = string.Empty;
        }

        // ************************************************************************************************
        // * メソッド名      名称編集
        // * 
        // * 構文            Public Function GetMeisho() As String
        // * 
        // * 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
        // * 
        // * 引数            名称
        // * 
        // * 戻り値          編集名称（String）
        // ************************************************************************************************
        public string GetMeisho()
        {
            const string THIS_METHOD_NAME = "GetHojinMeisho";
            string strKanjiMeisho = string.Empty;

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (m_strAtenaDataKB ?? "")
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_HOJIN:
                        {
                            // 法人の名称の編集
                            m_cHojinMeishoBClass.p_strKeitaiFuyoKB = m_strKeitaiFuyoKB;
                            m_cHojinMeishoBClass.p_strKeitaiSeiRyakuKB = m_strKeitaiSeiRyakuKB;
                            m_cHojinMeishoBClass.p_strKanjiHjnKeitai = m_strKanjiHjnKeitai;
                            m_cHojinMeishoBClass.p_strKanjiMeisho1 = m_strKanjiMeisho1;
                            m_cHojinMeishoBClass.p_strKanjiMeisho2 = m_strKanjiMeisho2;
                            strKanjiMeisho = m_cHojinMeishoBClass.GetHojinMeisho();
                            break;
                        }

                    default:
                        {
                            strKanjiMeisho = m_strKanjiMeisho1;
                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {

                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:" + THIS_METHOD_NAME + "】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strKanjiMeisho;

        }

        // ************************************************************************************************
        // * メソッド名      名称編集
        // * 
        // * 構文            Public Function GetHojinMeisho(ByVal cABHojinMeishoParaX() As ABHojinMeishoParaXClass) As String()
        // * 
        // * 機能　　        法人形態付与区分、法人形態正式略称区分、法人形態、名称１、名称２より名称を編集する
        // * 
        // * 引数            名称パラメータクラス   : ABMeishoParaXClass[]
        // * 
        // * 戻り値          編集名称（String[]）
        // ************************************************************************************************
        public string[] GetMeisho(ABMeishoParaXClass[] cABMeishoParaX)
        {
            const string THIS_METHOD_NAME = "GetHojinMeisho";
            string[] strKanjiMeisho;
            int intIndex;

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strKanjiMeisho = new string[Information.UBound(cABMeishoParaX) + 1];
                var loopTo = Information.UBound(cABMeishoParaX);
                for (intIndex = 0; intIndex <= loopTo; intIndex++)
                {
                    {
                        ref var withBlock = ref cABMeishoParaX[intIndex];
                        m_strKeitaiFuyoKB = withBlock.p_strKeitaiFuyoKB;
                        m_strKeitaiSeiRyakuKB = withBlock.p_strKeitaiSeiRyakuKB;
                        m_strKanjiHjnKeitai = withBlock.p_strKanjiHjnKeitai;
                        m_strKanjiMeisho1 = withBlock.p_strKanjiMeisho1;
                        m_strKanjiMeisho2 = withBlock.p_strKanjiMeisho2;
                        m_strAtenaDataKB = withBlock.p_strAtenaDataKB;
                    }
                    strKanjiMeisho[intIndex] = GetMeisho();
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:" + THIS_METHOD_NAME + "】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strKanjiMeisho;

        }
    }
}
