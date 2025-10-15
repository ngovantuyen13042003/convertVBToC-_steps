// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        ＡＢ宛名＿生年月日編集
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/24　芳沢　昇
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/06/27 000001     変換元の値がSteing.Emptyの場合エラーするバグを修正
// * 2023/03/10 000002     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// ************************************************************************************************
using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace Densan.Reams.AB.AB000BB
{

    public class ABUmareHenshuBClass
    {
        // ************************************************************************************************
        // *
        // * 生年月日編集に使用するパラメータクラス
        // *
        // ************************************************************************************************
        // パラメータのメンバ変数
        private UFLogClass m_cfUFLogClass;                // ログ出力クラス
        private UFControlData m_cfUFControlData;          // コントロールデータ
        private UFConfigDataClass m_cfUFConfigDataClass;  // コンフィグデータ

        private string m_strDataKB;                       // 区分(2桁)
        private string m_strJuminSHU;                     // 種別(2桁)
        private string m_strUmareYMD;                     // 生年月日
        private string m_strUmareWMD;                     // 生和暦年月日
        private string m_strHyojiUmareYMD;                // 表示用生年月日
        private string m_strShomeiUmareYMD;               // 証明用生年月日
        private UFDateClass m_cfDateClass;                // 日付編集 

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABUmareHenshuBClass";             // クラス名

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABUmareHenshuBClass(UFControlData cfUFControlData, UFConfigDataClass cfUFConfigDataClass)
        {
            // メンバ変数セット
            m_cfUFControlData = cfUFControlData;
            m_cfUFConfigDataClass = cfUFConfigDataClass;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfUFConfigDataClass, cfUFControlData.m_strBusinessId);

            // パラメータのメンバ変数初期化
            m_strDataKB = string.Empty;
            m_strJuminSHU = string.Empty;
            m_strUmareYMD = string.Empty;
            m_strUmareWMD = string.Empty;
            m_strHyojiUmareYMD = string.Empty;
            m_strShomeiUmareYMD = string.Empty;
            // 日付処理クラスインスタンス化
            m_cfDateClass = new UFDateClass(m_cfUFConfigDataClass);

        }

        // ************************************************************************************************
        // * メソッド名      生年月日編集
        // * 
        // * 構文           Public Sub HenshuUmare()
        // * 
        // * 機能　　       生年月日・生和暦年月日より表示用生年月日・証明用年月日を編集する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void HenshuUmare()
        {
            string strNengo = string.Empty;
            string strUmareYmd = string.Empty;

            do
            {
                try
                {


                    // 和暦１文字目を取得
                    strNengo = m_strUmareWMD.RSubstring(0, 1);
                    if (strNengo == "0" | strNengo == "8" | strNengo == "9")
                    {
                        if (string.IsNullOrEmpty(m_strUmareYMD.Trim()))
                        {
                            switch (strNengo ?? "")
                            {
                                case "0":
                                    {
                                        strUmareYmd = "20" + m_strUmareWMD.RSubstring(1);
                                        break;
                                    }
                                case "8":
                                    {
                                        strUmareYmd = "18" + m_strUmareWMD.RSubstring(1);
                                        break;
                                    }
                                case "9":
                                    {
                                        strUmareYmd = "19" + m_strUmareWMD.RSubstring(1);
                                        break;
                                    }

                                default:
                                    {
                                        strUmareYmd = "20" + m_strUmareWMD.RSubstring(1);
                                        break;
                                    }
                            }
                            m_cfDateClass.p_strDateValue = strUmareYmd;
                        }
                        else
                        {
                            m_cfDateClass.p_strDateValue = m_strUmareYMD;
                        }

                        if (!m_cfDateClass.CheckDate())
                        {
                            m_strHyojiUmareYMD = string.Empty;
                            m_strShomeiUmareYMD = string.Empty;
                            break;
                        }

                        // 生年月日より表示用日付の編集を行う
                        m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period;
                        m_cfDateClass.p_blnWideType = false;
                        m_cfDateClass.p_enDateFillType = UFDateFillType.Zero;
                        m_strHyojiUmareYMD = m_cfDateClass.p_strSeirekiYMD;

                        // 生年月日より証明用日付の編集を行う
                        m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese;
                        m_cfDateClass.p_blnWideType = true;
                        m_cfDateClass.p_enEraType = UFEraType.Kanji;
                        m_cfDateClass.p_enDateFillType = UFDateFillType.Blank;
                        m_strShomeiUmareYMD = m_cfDateClass.p_strSeirekiYMD;
                    }
                    else
                    {
                        // 生和暦年月日より表示用日付の編集を行う
                        m_cfDateClass.p_strDateValue = m_strUmareWMD;

                        if (!m_cfDateClass.CheckDate())
                        {
                            m_strHyojiUmareYMD = string.Empty;
                            m_strShomeiUmareYMD = string.Empty;
                            break;
                        }

                        m_cfDateClass.p_blnWideType = false;
                        m_cfDateClass.p_enEraType = UFEraType.KanjiRyaku;
                        m_cfDateClass.p_enDateFillType = UFDateFillType.Zero;
                        m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period;
                        m_strHyojiUmareYMD = m_cfDateClass.p_strWarekiYMD;

                        // 生和暦年月日より証明用日付の編集を行う
                        m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese;
                        m_cfDateClass.p_blnWideType = true;
                        m_cfDateClass.p_enEraType = UFEraType.Kanji;
                        m_cfDateClass.p_enDateFillType = UFDateFillType.Blank;
                        m_strShomeiUmareYMD = m_cfDateClass.p_strWarekiYMD;
                    }
                }
                catch (Exception objExp)
                {
                    // エラーログ出力
                    m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenshuUmare】【エラー内容:" + objExp.Message + "】");
                    // システムエラーをスローする
                    throw objExp;
                }
            }
            while (false);

        }

        // ************************************************************************************************
        // * 各メンバ変数のプロパティ定義
        // ************************************************************************************************
        public string p_strDataKB
        {
            set
            {
                m_strDataKB = value;
            }
        }
        public string p_strJuminSHU
        {
            set
            {
                m_strJuminSHU = value;
            }
        }
        public string p_strUmareYMD
        {
            set
            {
                // * 履歴番号 000001 2003/06/27 修正開始
                // m_strUmareYMD = Value
                m_strUmareYMD = value.RPadRight(8);
                // * 履歴番号 000001 2003/06/27 修正終了
            }
        }
        public string p_strUmareWMD
        {
            set
            {
                // * 履歴番号 000001 2003/06/27 修正開始
                // m_strUmareWMD = Value
                m_strUmareWMD = value.RPadRight(7);
                // * 履歴番号 000001 2003/06/27 修正終了
            }
        }
        public string p_strHyojiUmareYMD
        {
            get
            {
                return m_strHyojiUmareYMD;
            }
        }
        public string p_strShomeiUmareYMD
        {
            get
            {
                return m_strShomeiUmareYMD;
            }
        }

    }
}
