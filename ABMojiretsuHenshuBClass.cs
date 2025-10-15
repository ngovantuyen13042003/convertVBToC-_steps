// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         文字列編集Ｂクラス(ABMojiretsuHenshuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2008/02/13  比嘉　計成
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

    public class ABMojiretsuHenshuBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                              // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                              // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                          // エラー処理クラス

        private ABAtenaKanriJohoBClass m_cABAtenaKanriJohoB;          // 管理情報Ｂクラス
        private string m_strShimeiKakkoKB_param;                      // 氏名括弧制御区分パラメータ
        private ABMojiretsuHenshuBClass m_cABMojiRetsuHenshuB;        // 文字列編集Ｂクラス 


        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMojiretsuHenshuBClass";
        private const string THIS_BUSINESSID = "AB";          // 業務コード

        private const string HIDARI_KAKKO = "（";
        private const string MIGI_KAKKO = "）";
        private const string STR_10 = "10";
        private const string STR_20 = "20";

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
        public ABMojiretsuHenshuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = new UFRdbClass(m_cfControlData.m_strBusinessId);

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // 管理情報取得を行う
            if (m_strShimeiKakkoKB_param is null)
            {
                // メンバに無い場合のみインスタンス化を行う
                if (m_cABAtenaKanriJohoB is null)
                {
                    m_cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                else
                {
                }
                // 管理情報より取得
                m_strShimeiKakkoKB_param = m_cABAtenaKanriJohoB.GetShimeiKakkoKB_Param();
            }
            else
            {
            }

        }
        #endregion

        #region メソッド
        // **********************************************************************************************************************
        // * メソッド名     氏名簡略文字編集
        // * 
        // * 構文           Public Overloads Function EditKanryakuMeisho(ByVal strMeisho As String) As String
        // * 
        // * 機能           氏名名称から括弧の削除処理を行うs
        // * 
        // * 引数           strMeisho  文字列
        // *
        // * 戻り値         String     編集した文字列
        // *
        // **********************************************************************************************************************
        public string EditKanryakuMeisho(string strMeisho)
        {

            return EditKanryakuMeisho(STR_10, STR_20, strMeisho);
        }

        #region EditKanryakuMeisho:氏名簡略文字編集
        // **********************************************************************************************************************
        // * メソッド名     氏名簡略文字編集
        // * 
        // * 構文           Public Overloads Function EditKanryakuMeisho(ByVal strDataKB As String, _
        // *                                                             ByVal strDataShu As String, _
        // *                                                             ByVal strMeisho As String) As String
        // * 
        // * 機能           氏名名称から括弧の削除処理を行う
        // * 
        // * 引数           strDataKB     データ区分
        // *                strDataShu    データ種別               
        // *                strMeisho     文字列
        // *
        // * 戻り値         String        編集した文字列
        // *
        // **********************************************************************************************************************
        public string EditKanryakuMeisho(string strDataKB, string strDataShu, string strMeisho)

        {
            string THIS_METHOD_NAME = "EditKanryakuMeisho";
            int intIndexFrom;
            int intIndexTo;
            string strWkMeisho;
            string strRet = string.Empty;

            do
            {
                try
                {
                    // ワークに名称をセット
                    strWkMeisho = strMeisho;

                    // 管理情報：氏名括弧編集制御[10,15]が"1"の場合、括弧を取り除く
                    if (m_strShimeiKakkoKB_param == "1")
                    {
                    }
                    else
                    {
                        // 編集はしない
                        strRet = strWkMeisho;
                        break;
                    }

                    // データ区分が個人(1%) かつ データ種別が外国人(2%)の場合、括弧を取り除く
                    if (strDataKB.RSubstring(0, 1) == "1" && strDataShu.RSubstring(0, 1) == "2")
                    {
                    }
                    else
                    {
                        // 編集はしない
                        strRet = strWkMeisho;
                        break;
                    }

                    // 空白または、頭１文字が数字もしくはアルファベットの場合は、行わない
                    if (!string.IsNullOrEmpty(strWkMeisho.TrimEnd()))
                    {
                        if (!UFStringClass.CheckAlphabetNumber(UFStringClass.ConvertWideToNarrow(strWkMeisho.RSubstring(0, 1))))
                        {
                            // 左括弧を捜す
                            intIndexFrom = strWkMeisho.RIndexOf(HIDARI_KAKKO);

                            while (intIndexFrom >= 0)
                            {

                                // 右括弧を捜す
                                intIndexTo = strWkMeisho.RSubstring(intIndexFrom + 1).RIndexOf(MIGI_KAKKO);

                                if (intIndexTo >= 0)
                                {
                                    // 括弧を削除する
                                    strRet = strRet + strWkMeisho.RSubstring(0, intIndexFrom);                // 左括弧の直前まで出力対象

                                    // ワーク文字列より括弧除去文字列が長い場合は空白をセット
                                    if (strWkMeisho.RLength > intIndexFrom + intIndexTo + 2)
                                    {
                                        strWkMeisho = strWkMeisho.RSubstring(intIndexFrom + intIndexTo + 2);      // 右括弧の次からワークにセット
                                    }
                                    else
                                    {
                                        strWkMeisho = string.Empty;
                                    }

                                    // 左括弧を捜す
                                    intIndexFrom = strWkMeisho.RIndexOf(HIDARI_KAKKO);
                                }

                                else
                                {
                                    break;
                                }

                            }

                            // ワークの値を戻り値に追加セット
                            strRet = strRet + strWkMeisho;
                        }
                        else
                        {
                            // 編集はしない
                            strRet = strWkMeisho;
                        }
                    }
                    else
                    {
                        // 編集はしない
                        strRet = strWkMeisho;
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
            }

            while (false);

            return strRet;
        }
        #endregion

        #endregion

    }
}
