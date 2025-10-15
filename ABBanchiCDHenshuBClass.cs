// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         番地コード編集Ｂクラス(ABBanchiCDHenshuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2009/04/07  工藤　美芙由
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Security;

namespace Densan.Reams.AB.AB000BB
{

    public class ABBanchiCDHenshuBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;                            // ログ出力クラス
        private UFControlData m_cfUFControlData;                      // コントロールデータ
        private UFConfigDataClass m_cfUFConfigDataClass;              // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                              // ＲＤＢクラス
        private URBANCHICDMSTBClass m_crBanchiCdMstB;                 // ＵＲ番地コードマスタクラス
        private UFErrorClass m_cfErrorClass;                          // エラー処理クラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABBanchiCDHenshuBClass";
        private const string THIS_BUSINESSID = "AB";


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
        [SecuritySafeCritical]
        public ABBanchiCDHenshuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            m_cfUFConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = new UFRdbClass(m_cfUFControlData.m_strBusinessId);

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId);

            // ＵＲ番地コードマスタクラスのインスタンス化
            if (m_crBanchiCdMstB is null)
            {
                m_crBanchiCdMstB = new URBANCHICDMSTBClass(cfControlData, cfConfigDataClass, m_cfRdbClass);
            }

        }
        #endregion

        #region メソッド

        #region CreateBanchiCD:番地コード編集
        // **********************************************************************************************************************
        // * メソッド名     番地コード編集
        // * 
        // * 構文           Public Function CreateBanchiCD(ByVal strBanchi As String) As String()
        // * 
        // * 機能           番地から番地コード１～３を編集する
        // * 
        // * 引数           strBanchi     番地
        // *
        // * 戻り値         String()      編集した番地コード配列
        // *
        // **********************************************************************************************************************
        [SecuritySafeCritical]
        public string[] CreateBanchiCD(string strBanchi)
        {
            string THIS_METHOD_NAME = "CreateBanchiCD";
            var strBanchiCD = new string[3];                        // 番地コード配列（取得用）
            var strRetBanchiCD = new string[3];                     // 番地コード配列（戻り値用）
            var strMotoBanchiCD = default(string[]);                     // 変更前番地コード
            int intLoop;                              // ループカウンタ

            try
            {

                // 番地コード取得
                strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(strBanchi, strMotoBanchiCD, true);

                var loopTo = strBanchiCD.Length - 1;
                for (intLoop = 0; intLoop <= loopTo; intLoop++)
                {
                    if (strBanchiCD[intLoop] == null)
                    {
                        // 取得した番地コード配列にNothingがある場合はString.Emptyをセット
                        strBanchiCD[intLoop] = string.Empty;
                    }

                    // 番地コードを右詰する（5桁に満たない場合は半角スペースを左詰）
                    strRetBanchiCD[intLoop] = strBanchiCD[intLoop].Trim().RPadLeft(5, ' ');
                }
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

            return strRetBanchiCD;

        }
        #endregion

        #endregion

    }
}