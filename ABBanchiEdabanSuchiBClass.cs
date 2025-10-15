// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         番地コード編集Ｂクラス(ABBanchiEdabanSuchiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/08/14  早崎 雄矢
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Security;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABBanchiEdabanSuchiBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;                            // ログ出力クラス
        private UFControlData m_cfUFControlData;                      // コントロールデータ
        private UFConfigDataClass m_cfUFConfigDataClass;              // コンフィグデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABBanchiEdabanSuchiBClass";

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass) 
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        [SecuritySafeCritical]
        public ABBanchiEdabanSuchiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {

            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            m_cfUFConfigDataClass = cfConfigDataClass;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId);

        }
        #endregion

        #region メソッド
        // **********************************************************************************************************************
        // * メソッド名     番地コード編集
        // * 
        // * 構文           Public Function GetBanchiEdabanSuchi(ByVal strBanchiCD1 As String, ByVal strBanchiCD2 As String, _
        // *                                                     ByVal strBanchiCD3 As String) As String
        // * 
        // * 機能           番地から番地コード１～３を編集する
        // * 
        // * 引数           strBanchiCD1 As String : 番地コード１
        // *                strBanchiCD2 As String : 番地コード２
        // *                strBanchiCD3 As String : 番地コード３
        // *
        // * 戻り値         String      編集した番地コード
        // *
        // **********************************************************************************************************************
        [SecuritySafeCritical]
        public string GetBanchiEdabanSuchi(string strBanchiCD1, string strBanchiCD2, string strBanchiCD3)
        {
            string GetBanchiEdabanSuchiRet = default;
            string THIS_METHOD_NAME = "GetBanchiEdabanSuchi";
            string strAfterBanchiCD1;
            string strAfterBanchiCD2;
            string strAfterBanchiCD3;

            try
            {

                strAfterBanchiCD1 = GetBanchiCDChange(strBanchiCD1);
                strAfterBanchiCD2 = GetBanchiCDChange(strBanchiCD2);
                strAfterBanchiCD3 = GetBanchiCDChange(strBanchiCD3);

                // 連結して戻り値とする
                GetBanchiEdabanSuchiRet = strAfterBanchiCD1 + strAfterBanchiCD2 + strAfterBanchiCD3;
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfUFLogClass.WarningWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                throw;
            }
            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                throw;
            }

            return GetBanchiEdabanSuchiRet;

        }

        // **********************************************************************************************************************
        // * メソッド名     番地コード変換(5桁)
        // * 
        // * 構文           Public Function GetBanchiCDChange(ByVal strBanchiCD As String) As String
        // * 
        // * 機能           番地コードに数値以外が存在した場合、以降を０埋めする(5桁)
        // * 
        // * 引数           strBanchiCD As String : 番地コード
        // *
        // * 戻り値         String      編集した番地コード
        // *
        // **********************************************************************************************************************
        public string GetBanchiCDChange(string strBanchiCD)
        {
            string THIS_METHOD_NAME = "GetBanchiCDChange";
            string strBanchiCDAfter = string.Empty;

            // 番地コード≠空白の場合
            if (!ReferenceEquals(strBanchiCD.Trim(), string.Empty))
            {
                // 番地コードに数値以外が含まれる場合
                if (!Information.IsNumeric(strBanchiCD))
                {
                    // 一文字づつチェックを行い、数値以外が存在する場合、以降0埋めする(5桁)
                    foreach (string strBanchiData in strBanchiCD)
                    {
                        if (Information.IsNumeric(strBanchiData))
                        {
                            strBanchiCDAfter = strBanchiCDAfter + strBanchiData;
                        }

                        else if (strBanchiData == " ")
                        {
                            strBanchiCDAfter = strBanchiCDAfter + "0";
                        }

                        else
                        {
                            strBanchiCDAfter = strBanchiCDAfter.PadRight(5, '0');
                            break;
                        }
                    }
                }
                else if (strBanchiCD.Trim().Length < 5)
                {
                    // 数値のみ5桁以下の場合、前0で5桁埋める
                    strBanchiCDAfter = strBanchiCD.Trim().PadLeft(5, '0');
                }
                else if (strBanchiCD.Trim().Length == 5)
                {
                    // 数値のみ5桁の場合、そのまま返す
                    strBanchiCDAfter = strBanchiCD;
                }
            }
            else
            {
                strBanchiCDAfter = string.Empty.PadLeft(5, '0');
            }

            return strBanchiCDAfter;

        }
        #endregion

    }
}
