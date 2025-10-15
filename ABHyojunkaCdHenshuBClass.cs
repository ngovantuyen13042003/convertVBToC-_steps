// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         標準化コード編集Ｂクラス(ABHyojunkaCdHenshuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/03/13  仲西　勝
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

    public class ABHyojunkaCdHenshuBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                // コンフィグデータ
        private UFLogClass m_cfLogClass;                              // ログ出力クラス

        // パラメータのメンバ変数
        private string m_strJuminKbn;                                 // 住民区分
        private string m_strJuminShubetsu;                            // 住民種別
        private string m_strJuminJotai;                               // 住民状態

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABHyojunkaCdHenshuBClass";

        // 各メンバ変数のプロパティ定義
        public string p_strJuminKbn
        {
            get
            {
                return m_strJuminKbn;
            }
        }
        public string p_strJuminShubetsu
        {
            get
            {
                return m_strJuminShubetsu;
            }
        }
        public string p_strJuminJotai
        {
            get
            {
                return m_strJuminJotai;
            }
        }

        #endregion

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
        public ABHyojunkaCdHenshuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strJuminKbn = string.Empty;
            m_strJuminShubetsu = string.Empty;
            m_strJuminJotai = string.Empty;

        }
        #endregion

        #region メソッド

        #region HenshuHyojunkaCd:標準化コード編集
        // **********************************************************************************************************************
        // * メソッド名     標準化コード編集
        // * 
        // * 構文           Public Sub HenshuHyojunkaCd(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
        // * 
        // * 機能           各コードを標準化準拠に準ずる体系に編集する
        // * 
        // * 引数           strAtenaDataKB     宛名データ区分
        // *                strAtenaDataSHU    宛名データ種別
        // *
        // * 戻り値         なし
        // *
        // **********************************************************************************************************************
        public void HenshuHyojunkaCd(string strAtenaDataKB, string strAtenaDataSHU)
        {
            string THIS_METHOD_NAME = "HenshuHyojunkaCd";

            try
            {
                m_strJuminKbn = GetJuminKbn(strAtenaDataKB);
                m_strJuminShubetsu = GetJuminShubetsu(strAtenaDataKB, strAtenaDataSHU);
                m_strJuminJotai = GetJuminJotai(strAtenaDataKB, strAtenaDataSHU);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                throw;

            }

        }
        #endregion

        #region GetJuminKbn:住民区分取得
        // **********************************************************************************************************************
        // * メソッド名     住民区分取得
        // * 
        // * 構文           Private Function GetJuminKbn(ByVal strAtenaDataKB As String) As String
        // * 
        // * 機能           標準化準拠のコード体系に準ずる住民区分を返却する
        // * 
        // * 引数           strAtenaDataKB     宛名データ区分
        // *
        // * 戻り値         String             住民区分
        // *
        // **********************************************************************************************************************
        private string GetJuminKbn(string strAtenaDataKB)
        {
            string THIS_METHOD_NAME = "GetJuminKbn";
            string strRet = string.Empty;

            try
            {
                switch (strAtenaDataKB ?? "")
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                        {
                            // 住民
                            strRet = "1";
                            break;
                        }
                    case var case1 when case1 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                        {
                            // 住登外
                            strRet = "2";
                            break;
                        }
                    case var case2 when case2 == ABConstClass.ATENADATAKB_HOJIN:
                        {
                            // 法人
                            strRet = "3";
                            break;
                        }

                    default:
                        {
                            // 以外の場合、空白を設定
                            strRet = string.Empty;
                            break;
                        }
                }
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                throw;

            }

            return strRet;
        }
        #endregion

        #region GetJuminShubetsu:住民種別取得
        // **********************************************************************************************************************
        // * メソッド名     住民種別取得
        // * 
        // * 構文           Private Function GetJuminShubetsu(ByVal strAtenaDataKB As String) As String
        // * 
        // * 機能           標準化準拠のコード体系に準ずる住民種別を返却する
        // * 
        // * 引数           strAtenaDataKB     宛名データ区分
        // *                strAtenaDataSHU    宛名データ種別
        // *
        // * 戻り値         String             住民種別
        // *
        // **********************************************************************************************************************
        private string GetJuminShubetsu(string strAtenaDataKB, string strAtenaDataSHU)
        {
            string THIS_METHOD_NAME = "GetJuminShubetsu";
            string strRet = string.Empty;

            try
            {
                switch (strAtenaDataKB ?? "")
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                    case var case1 when case1 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                        {
                            if (strAtenaDataSHU.Trim().RLength > 0 && strAtenaDataSHU.Trim().RSubstring(0, 1) == "1")
                            {
                                // 日本人
                                strRet = "1";
                            }
                            else if (strAtenaDataSHU.Trim().RLength > 0 && strAtenaDataSHU.Trim().RSubstring(0, 1) == "2")
                            {
                                // 外国人
                                strRet = "2";
                            }
                            else
                            {
                                // 以外の場合、空白を設定
                                strRet = string.Empty;
                            }

                            break;
                        }

                    default:
                        {
                            // 以外の場合、空白を設定
                            strRet = string.Empty;
                            break;
                        }
                }
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                throw;

            }

            return strRet;
        }
        #endregion

        #region GetJuminJotai:住民状態取得
        // **********************************************************************************************************************
        // * メソッド名     住民状態取得
        // * 
        // * 構文           Private Function GetJuminJotai(ByVal strAtenaDataKB As String) As String
        // * 
        // * 機能           標準化準拠のコード体系に準ずる住民状態を返却する
        // * 
        // * 引数           strAtenaDataKB     宛名データ区分
        // *                strAtenaDataSHU    宛名データ種別
        // *
        // * 戻り値         String             住民状態
        // *
        // **********************************************************************************************************************
        private string GetJuminJotai(string strAtenaDataKB, string strAtenaDataSHU)
        {
            string THIS_METHOD_NAME = "GetJuminJotai";
            string strRet = string.Empty;

            try
            {
                switch (strAtenaDataKB ?? "")
                {
                    case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                        {
                            switch (strAtenaDataSHU ?? "")
                            {
                                case var case1 when case1 == ABConstClass.JUMINSHU_NIHONJIN_JUMIN:
                                case var case2 when case2 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                    {
                                        // 住登者
                                        strRet = "1";
                                        break;
                                    }
                                case var case3 when case3 == ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU:
                                case var case4 when case4 == ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU:
                                    {
                                        // 転出者
                                        strRet = "2";
                                        break;
                                    }
                                case var case5 when case5 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                case var case6 when case6 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        // 死亡者
                                        strRet = "3";
                                        break;
                                    }

                                default:
                                    {
                                        // その他消除者
                                        strRet = "9";
                                        break;
                                    }
                            }

                            break;
                        }

                    case var case7 when case7 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                        {
                            switch (strAtenaDataSHU ?? "")
                            {
                                case var case8 when case8 == ABConstClass.JUMINSHU_NIHONJIN_JUMIN:
                                case var case9 when case9 == ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN:
                                    {
                                        // 住登外者
                                        strRet = "1";
                                        break;
                                    }
                                case var case10 when case10 == ABConstClass.JUMINSHU_NIHONJIN_SHIBOU:
                                case var case11 when case11 == ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU:
                                    {
                                        // 死亡者
                                        strRet = "2";
                                        break;
                                    }

                                default:
                                    {
                                        // その他消除者
                                        strRet = "9";
                                        break;
                                    }
                            }

                            break;
                        }

                    default:
                        {
                            // 以外の場合、空白を設定
                            strRet = string.Empty;
                            break;
                        }
                }
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                throw;

            }

            return strRet;
        }
        #endregion

        #endregion

    }
}
