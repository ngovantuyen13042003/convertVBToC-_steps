// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        施行日判定Ｂ(ABSekoYMDHanteiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2011/10/18　後藤　洋輔
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　 履歴番号　　    修正内容
// ************************************************************************************************

using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABSekoYMDHanteiBClass
    {

        // **
        // * クラスID定義
        // * 
        private const string THIS_CLASS_NAME = "ABSekoYMDHanteiBCClass";

        #region メンバ変数定義
        private UFControlData m_cfControlData;                            // コントロールデータ
        private UFConfigDataClass m_cfConfigData;                         // コンフィグデータ
        private UFLogClass m_cfLog;                                       // ログクラス
        private UFRdbClass m_cfRdb;                                       // RDBクラス
        private UFDateClass m_cfDate;                                     // 日付クラス
        private ABAtenaKanriJohoBClass m_csAtenaKanriJohoB;               // 宛名管理情報Bクラス   
        #endregion

        #region コンストラクタ(New)
        public ABSekoYMDHanteiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)

        {

            // ●各メンバ変数のインスタンス化
            // *コントロールデータ
            m_cfControlData = cfControlData;

            // *コンフィグデータ
            m_cfConfigData = cfConfigData;

            // *ログクラス
            m_cfLog = new UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId);

            // *RDBクラス
            m_cfRdb = cfRdb;

            // *日付クラス
            m_cfDate = new UFDateClass(m_cfConfigData);

            // *宛名管理情報クラス
            m_csAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigData, m_cfRdb);

        }
        #endregion

        #region 施行日取得(呼び出し)[GetSekoYMD]
        /// <summary>
    /// 施行日を取得します
    /// </summary>
    /// <returns>施行日</returns>
    /// <remarks>施行日取得メソッドを実行し、施行日を取得します</remarks>
        public string GetSekoYMD()
        {

            const string THIS_METHOD_NAME = "GetSekoYMD";         // メソッド名
            string strRetSekoYMD = string.Empty;              // 施行日(返却用)

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ●管理情報から施行日の取得を行う
                strRetSekoYMD = GetSekoYMDFromKanriJoho();

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
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


                // エラーをそのままスローする
                throw objExp;

            }

            return strRetSekoYMD;

        }
        #endregion

        #region 施行日取得(GetSekoYMDFromKanriJoho)
        /// <summary>
    /// 施行日を取得します
    /// </summary>
    /// <returns>施行日</returns>
    /// <remarks>管理情報で保持する住基法改正施行日を取得します</remarks>
        private string GetSekoYMDFromKanriJoho()
        {

            const string THIS_METHOD_NAME = "GetSekoYMDFromKanriJoho";            // メソッド名
            const string CNS_SHUKEY25 = "25";                                     // 主キー"25"
            const string CNS_SHIKIBETSUKEY01 = "01";                              // 識別キー"01"
            string strRetSekoYMD = string.Empty;                              // 施行日(返却用)
            DataSet csKanriJoho = default;                                    // 管理情報

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ●管理情報から施行日を取得(引数"25","01")
                csKanriJoho = m_csAtenaKanriJohoB.GetKanriJohoHoshu(CNS_SHUKEY25, CNS_SHIKIBETSUKEY01);

                // *取得した管理情報からパラメータをワークに設定
                {
                    var withBlock = csKanriJoho.Tables[ABAtenaKanriJohoEntity.TABLE_NAME];

                    // ●施行日の設定
                    if (withBlock.Rows.Count > 0)
                    {
                        // **管理情報が取得された場合**

                        strRetSekoYMD = (string)withBlock.Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                        m_cfDate.p_strDateValue = strRetSekoYMD;

                        if (strRetSekoYMD.Trim().RLength() == 8 && m_cfDate.CheckDate())
                        {
                        }
                        // **0行目のパラメータが8桁 かつ 日付として正しい場合**
                        // 処理なし
                        else
                        {
                            // *戻り値に空白を設定
                            strRetSekoYMD = string.Empty;
                        }
                    }

                    else
                    {
                        // 処理なし
                    }

                }

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
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


                // エラーをそのままスローする
                throw objExp;
            }

            return strRetSekoYMD;

        }

        #endregion

        #region 施行日後チェック(CheckAfterSekoYMD)
        /// <summary>
    /// 施行日後チェックメソッド
    /// </summary>
    /// <returns>施行日後チェック結果</returns>
    /// <remarks>現在日が住基法改正施行日後かの判定をします</remarks>
        public bool CheckAfterSekoYMD()
        {

            const string THIS_METHOD_NAME = "CheckAfterSekoYMD";              // メソッド名
            bool blnCheckResult = false;                                    // 施行日チェックの結果
            string strSekoYMD = string.Empty;                             // 施行日

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ●施行日を管理情報より取得
                strSekoYMD = GetSekoYMDFromKanriJoho();

                // ●取得した施行日から判定をする
                if (strSekoYMD.Trim().RLength() > 0)
                {
                    // **施行日が空白でない場合**

                    if (strSekoYMD <= m_cfRdb.GetSystemDate().ToString("yyyyMMdd"))
                    {
                        // **施行日が現在日以前の場合**

                        blnCheckResult = true;
                    }
                    else
                    {
                        // 処理なし
                    }
                }
                else
                {
                    // 処理なし
                }

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
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


                // エラーをそのままスローする
                throw objExp;
            }

            return blnCheckResult;

        }
        #endregion

    }
}
