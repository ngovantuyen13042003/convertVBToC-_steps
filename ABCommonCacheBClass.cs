// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ共通キャッシュビジネスクラス(ABCommonCacheBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2014/07/14　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2015/01/05   000001      法人番号利用開始日対応（石合）
// * 2015/01/09   000002      権限管理機能実装（石合）
// ************************************************************************************************

using System;
using System.Security;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.reams.ur.publicmodule.library.businesscommon.ur010x;
using ndensan.reams.ur.publicmodule.library.business.ur010b;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    /// <summary>
/// ＡＢ共通キャッシュビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABCommonCacheBClass : ABCommonBClass
    {

        #region メンバー変数

        // コンスタント定義
        protected new const string THIS_CLASS_NAME = "ABCommonCacheBClass";              // クラス名

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        [SecuritySafeCritical]

        // 基底クラスのコンストラクター呼び出し
        public ABCommonCacheBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass) : base(THIS_CLASS_NAME)
        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // 施行日判定ビジネスクラスのインスタンス化
            try
            {
                m_crSekoYMDHanteiB = new URSekoYMDHanteiCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_cfControlData.m_strBusinessId);
            }
            catch (UFAppException cfAppExp)
            {
                // システムをダウンさせるため、ExceptionにてThrowする。
                throw new Exception(cfAppExp.Message, cfAppExp);
            }
            catch (Exception csExp)
            {
                throw;
            }

            // *履歴番号 000001 2015/01/05 追加開始
            // 宛名管理情報ビジネスクラスのインスタンス化
            m_cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
            // *履歴番号 000001 2015/01/05 追加終了

            // 番号マスク化ビジネスクラスのインスタンス化
            m_crBangoMaskB = new URBangoMaskBClass(m_cfControlData);

            // アクセスログクラスのインスタンス化
            m_cuAccessLog = new USLAccessLogKojinBangoClass(m_cfControlData, m_cfControlData.m_strBusinessId);

            // 共通番号共通ビジネスクラスのインスタンス化
            m_cMyNumberCommonB = new ABMyNumberCommonBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

            // *履歴番号 000002 2015/01/09 追加開始
            // ユーザー情報クラスのインスタンス化
            m_cfUserInfo = new UFUserInfoClass(m_cfControlData.m_strBusinessId);
            // *履歴番号 000002 2015/01/09 追加終了

            // *履歴番号 000001 2015/01/05 追加開始
            // 個人番号利用開始日以降判定結果を取得
            m_blnIsAfterKojinBangoRiyoKaishiYMD = CheckAfterBangoSeidoDai4SekoYMD();

            // 法人番号利用開始日以降判定結果を取得
            m_blnIsAfterHojinBangoRiyoKaishiYMD = CheckAfterHojinBangoRiyoKaishiYMD();
            // *履歴番号 000001 2015/01/05 追加終了

        }

        #endregion

    }
}
