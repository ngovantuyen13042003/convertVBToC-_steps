// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ共通ビジネスクラス(ABCommonBClass)
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

namespace Densan.Reams.AB.AB000BB
{

    /// <summary>
/// ＡＢ共通ビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABCommonBClass
    {

        #region メンバー変数

        // メンバー変数
        protected string m_strClassName = THIS_CLASS_NAME;                      // クラス名
        protected UFLogClass m_cfLogClass;                                      // ログ出力クラス
        protected UFControlData m_cfControlData;                                // コントロールデータ
        protected UFConfigDataClass m_cfConfigDataClass;                        // コンフィグデータ
        protected UFRdbClass m_cfRdbClass;                                      // ＲＤＢクラス

        protected URSekoYMDHanteiBClass m_crSekoYMDHanteiB;                     // 番号制度施行日判定ビジネスクラス
        protected URBangoMaskBClass m_crBangoMaskB;                             // 番号マスク化ビジネスクラス
        protected USLAccessLogKojinBangoClass m_cuAccessLog;                    // アクセスログクラス
        protected ABMyNumberCommonBClass m_cMyNumberCommonB;                    // 共通番号共通ビジネスクラス

        // *履歴番号 000001 2015/01/05 追加開始
        protected ABAtenaKanriJohoBClass m_cAtenaKanriJohoB;                    // 宛名管理情報ビジネスクラス
        protected bool m_blnIsAfterKojinBangoRiyoKaishiYMD;                  // 個人番号利用開始日以降判定結果
        protected bool m_blnIsAfterHojinBangoRiyoKaishiYMD;                  // 法人番号利用開始日以降判定結果
                                                                             // *履歴番号 000001 2015/01/05 追加終了

        // *履歴番号 000002 2015/01/09 追加開始
        protected UFUserInfoClass m_cfUserInfo;                                 // ユーザー情報クラス
                                                                                // *履歴番号 000002 2015/01/09 追加終了

        // コンスタント定義
        protected const string THIS_CLASS_NAME = "ABCommonBClass";              // クラス名

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="strClassName">クラス名</param>
    /// <remarks></remarks>
        protected ABCommonBClass(string strClassName)
        {
            m_strClassName = strClassName;
        }

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public ABCommonBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
                m_crSekoYMDHanteiB = new URSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_cfControlData.m_strBusinessId);
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

        #region メソッド

        #region 番号制度判定用システム日付取得

        /// <summary>
    /// 番号制度判定用システム日付取得
    /// </summary>
    /// <returns>番号制度判定用システム日付</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string GetBangoSeidoHanteiyouSystemDate()
        {
            try
            {
                return m_crSekoYMDHanteiB.GetBangoSeidoHanteiyouSystemDate();
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
        }

        #endregion

        #region 番号制度施行日取得

        /// <summary>
    /// 番号制度施行日取得
    /// </summary>
    /// <returns>番号制度施行日</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string GetBangoSeidoSekoYMD()
        {
            try
            {
                return m_crSekoYMDHanteiB.GetBangoSeidoSekoYMD();
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
        }

        #endregion

        #region 番号制度第４施行日取得

        /// <summary>
    /// 番号制度第４施行日取得
    /// </summary>
    /// <returns>番号制度第４施行日</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string GetBangoSeidoDai4SekoYMD()
        {
            try
            {
                return m_crSekoYMDHanteiB.GetBangoSeidoDai4SekoYMD();
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
        }

        #endregion

        #region 番号制度施行日以降判定

        /// <summary>
    /// 番号制度施行日以降判定
    /// </summary>
    /// <returns>番号制度施行日以降判定結果</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public bool CheckAfterBangoSeidoSekoYMD()
        {
            try
            {
                return m_crSekoYMDHanteiB.CheckAfterBangoSeidoSekoYMD();
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
        }

        #endregion

        #region 番号制度第４施行日以降判定

        /// <summary>
    /// 番号制度第４施行日以降判定
    /// </summary>
    /// <returns>番号制度第４施行日以降判定結果</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public bool CheckAfterBangoSeidoDai4SekoYMD()
        {
            try
            {
                return m_crSekoYMDHanteiB.CheckAfterBangoSeidoDai4SekoYMD();
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
        }

        #endregion

        // *履歴番号 000001 2015/01/05 追加開始
        #region 法人番号利用開始日取得

        /// <summary>
    /// 法人番号利用開始日取得
    /// </summary>
    /// <returns>法人番号利用開始日</returns>
    /// <remarks></remarks>
        public string GetHojinBangoRiyoKaishiYMD()
        {
            try
            {
                return m_cAtenaKanriJohoB.GetHojinBangoRiyoKaishiYMD_Param();
            }
            catch (Exception csExp)
            {
                throw;
            }
        }

        #endregion

        #region 法人番号利用開始日以降判定

        /// <summary>
    /// 法人番号利用開始日以降判定
    /// </summary>
    /// <returns>法人番号利用開始日以降判定結果</returns>
    /// <remarks></remarks>
        public bool CheckAfterHojinBangoRiyoKaishiYMD()
        {

            bool blnResult;

            try
            {

                if (m_cfRdbClass.GetSystemDate.ToString("yyyyMMdd") < GetHojinBangoRiyoKaishiYMD())
                {
                    blnResult = false;
                }
                else
                {
                    blnResult = true;
                }
            }

            catch (Exception csExp)
            {
                throw;
            }

            return blnResult;

        }

        #endregion
        // *履歴番号 000001 2015/01/05 追加終了

        #region 番号マスク編集後の文字列取得

        /// <summary>
    /// 番号マスク編集後の文字列取得
    /// </summary>
    /// <param name="crBangoMaskPrm">番号編集パラメーター</param>
    /// <returns>番号マスク編集後の文字列</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string URBangoMask(URBangoMaskPrmClass crBangoMaskPrm)
        {
            return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm);
        }

        #endregion

        #region 番号マスク編集後の文字列取得

        /// <summary>
    /// 番号マスク編集後の文字列取得
    /// </summary>
    /// <param name="crBangoMaskPrm">番号編集パラメーター</param>
    /// <param name="blnWriteAccessLog">アクセスログ出力有無</param>
    /// <returns>番号マスク編集後の文字列</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string URBangoMask(URBangoMaskPrmClass crBangoMaskPrm, bool blnWriteAccessLog)
        {
            return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm, blnWriteAccessLog);
        }

        #endregion

        #region 共通番号（表示用）取得

        /// <summary>
    /// 共通番号（表示用）取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strJuminCD">住民コード</param>
    /// <param name="strAtenaDataKB">宛名データ区分</param>
    /// <returns>共通番号（表示用）</returns>
    /// <remarks>宛名データ区分を番号タイプに変換してマスク化を行います。</remarks>
        [SecuritySafeCritical]
        public string GetDispMyNumber(string strMyNumber, string strJuminCD, string strAtenaDataKB)
        {
            return GetDispMyNumber(strMyNumber, strJuminCD, (URBangoMaskPrmClass.URKojinBangoType)GetBangoTypeWithAtenaDataKB(strAtenaDataKB));
        }

        #endregion

        #region 共通番号（表示用）取得

        /// <summary>
    /// 共通番号（表示用）取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strJuminCD">住民コード</param>
    /// <param name="strUserKB">ユーザー区分</param>
    /// <returns>共通番号（表示用）</returns>
    /// <remarks>ユーザー区分を番号タイプに変換してマスク化を行います。</remarks>
        public string GetDispMyNumberWithUserKB(string strMyNumber, string strJuminCD, string strUserKB)
        {
            return GetDispMyNumber(strMyNumber, strJuminCD, (URBangoMaskPrmClass.URKojinBangoType)GetBangoTypeWithUserKB(strUserKB));
        }

        #endregion

        #region 共通番号（表示用）取得

        /// <summary>
    /// 共通番号（表示用）取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strJuminCD">住民コード</param>
    /// <param name="enBangoType">番号タイプ</param>
    /// <returns>共通番号（表示用）</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string GetDispMyNumber(string strMyNumber, string strJuminCD, URBangoMaskPrmClass.URKojinBangoType enBangoType)
        {

            URBangoMaskPrmClass crBangoMaskPrm;

            crBangoMaskPrm = new URBangoMaskPrmClass();
            crBangoMaskPrm.p_strGyomuCD = ABConstClass.THIS_BUSINESSID;
            crBangoMaskPrm.p_enBangoType = enBangoType;
            crBangoMaskPrm.p_strMaskId = string.Empty;
            crBangoMaskPrm.p_strMaskBango = strMyNumber;
            crBangoMaskPrm.p_strJuminCd = strJuminCD;
            return GetDispMyNumber(crBangoMaskPrm);

        }

        #endregion

        #region 共通番号（表示用）取得

        /// <summary>
    /// 共通番号（表示用）取得
    /// </summary>
    /// <param name="crBangoMaskPrm">番号編集パラメーター</param>
    /// <returns>共通番号（表示用）</returns>
    /// <remarks></remarks>
        [SecuritySafeCritical]
        public string GetDispMyNumber(URBangoMaskPrmClass crBangoMaskPrm)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool blnWriteAccessLog;
            USLPersonalDataKojinBango cuPersonalData;
            // *履歴番号 000001 2015/01/05 追加開始
            var enAuthLevel = default(UFAuthLevel);
            // *履歴番号 000001 2015/01/05 追加終了

            // 住民コードに値が存在しない場合、アクセスログを出力しない。
            // ※住民コードと紐付けがされていない共通番号を処理する場合、アクセスログの出力は行わない方針。
            if (crBangoMaskPrm.p_strJuminCd is not null && crBangoMaskPrm.p_strJuminCd.Trim.RLength > 0)
            {
                blnWriteAccessLog = true;
            }
            else
            {
                blnWriteAccessLog = false;
            }

            // 共通番号を事前に整備する。
            // マスク化桁数分の空白が存在する場合にエラーするので事前に空白を除去する。
            // ※値なしと桁数分の空白を同様に扱うため。（業共側のエラー回避）
            if (crBangoMaskPrm.p_strMaskBango is not null)
            {
                crBangoMaskPrm.p_strMaskBango = crBangoMaskPrm.p_strMaskBango.Trim;
            }
            else
            {
                crBangoMaskPrm.p_strMaskBango = string.Empty;
            }

            // 個人・法人区分を整備する（桁数による判定）
            crBangoMaskPrm.p_enBangoType = (URBangoMaskPrmClass.URKojinBangoType)GetBangoType(crBangoMaskPrm.p_strMaskBango, crBangoMaskPrm.p_enBangoType);

            // *履歴番号 000001 2015/01/05 追加開始
            // 個人・法人区分判定
            switch (crBangoMaskPrm.p_enBangoType)
            {

                case var @case when @case == URBangoMaskPrmClass.URKojinBangoType.KOJIN:
                    {

                        if (m_blnIsAfterKojinBangoRiyoKaishiYMD == false)
                        {
                            // 個人番号利用開始日前の場合には、空文字を返信する。
                            return string.Empty;
                        }
                        else
                        {
                            // 個人番号の場合、権限判定を行う。
                            enAuthLevel = GetAuthLevel();
                        }

                        break;
                    }

                case var case1 when case1 == URBangoMaskPrmClass.URKojinBangoType.HOJIN:
                    {

                        if (m_blnIsAfterHojinBangoRiyoKaishiYMD == false)
                        {
                            // 法人番号利用開始日前の場合には、空文字を返信する。
                            return string.Empty;
                        }
                        else
                        {
                            // 法人番号の場合、権限判定を行わない。
                            enAuthLevel = UFAuthLevel.W;
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
                    // noop
            }
            // *履歴番号 000001 2015/01/05 追加終了

            // *履歴番号 000001 2015/01/05 修正開始
            // Select Case Me.GetAuthLevel()
            switch (enAuthLevel)
            {
                // *履歴番号 000001 2015/01/05 修正終了
                case var case2 when case2 == UFAuthLevel.P:
                    {
                        // P（プロテクト表示）の場合、業共側でアクセスログを出力する。
                        // ※ただしバッチから処理された場合、基盤の仕組みによりアクセスログは出力されない。
                        return m_crBangoMaskB.URBangoMaskNoSession(crBangoMaskPrm, blnWriteAccessLog);
                    }
                case var case3 when case3 == UFAuthLevel.H:
                    {
                        // H（非表示）の場合、アクセスログを出力しない。
                        return string.Empty;
                    }

                default:
                    {

                        // 上記以外の場合、業務側でアクセスログを出力する。
                        // ※ただしバッチから処理された場合、基盤の仕組みによりアクセスログは出力されない。
                        if (blnWriteAccessLog == true)
                        {

                            if (crBangoMaskPrm.p_strMaskBango.RLength > 0)
                            {

                                cuPersonalData = new USLPersonalDataKojinBango();
                                cuPersonalData.p_strJuminCD = crBangoMaskPrm.p_strJuminCd;
                                cuPersonalData.p_strKojinBango = crBangoMaskPrm.p_strMaskBango;
                                if (crBangoMaskPrm.p_enBangoType == URBangoMaskPrmClass.URKojinBangoType.HOJIN)
                                {
                                    cuPersonalData.p_enKojinBangoType = USLPersonalDataKojinBango.USLKojinBangoTypeEnum.HojinBango;
                                }
                                else
                                {
                                    cuPersonalData.p_enKojinBangoType = USLPersonalDataKojinBango.USLKojinBangoTypeEnum.KojinBango;
                                }
                                m_cuAccessLog.ShokaiWrite(m_strClassName, THIS_METHOD_NAME, string.Empty, USLShokaiSubShubetsuEnum.SHOKAI, cuPersonalData);
                            }

                            else
                            {
                                // noop
                            }
                        }

                        else
                        {
                            // noop
                        }

                        return crBangoMaskPrm.p_strMaskBango;
                    }

            }

        }

        #endregion

        #region 住民コード⇒共通番号変換

        /// <summary>
    /// 住民コード⇒共通番号変換
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <returns>共通番号</returns>
    /// <remarks></remarks>
        public string GetMyNumber(string strJuminCd)
        {
            return m_cMyNumberCommonB.GetMyNumber(strJuminCd);
        }

        #endregion

        #region 共通番号⇒住民コード変換

        /// <summary>
    /// 共通番号⇒住民コード変換
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>住民コード配列</returns>
    /// <remarks></remarks>
        public string[] GetJuminCd(string strMyNumber)
        {
            return m_cMyNumberCommonB.GetJuminCd(strMyNumber);
        }

        /// <summary>
    /// 共通番号⇒住民コード変換
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="blnCkinFG">直近フラグ</param>
    /// <returns>住民コード配列</returns>
    /// <remarks></remarks>
        public string[] GetJuminCd(string strMyNumber, bool blnCkinFG)
        {
            return m_cMyNumberCommonB.GetJuminCd(strMyNumber, blnCkinFG);
        }

        #endregion

        #region ユーザー権限取得

        /// <summary>
    /// ユーザー権限取得
    /// </summary>
    /// <returns>ユーザー権限</returns>
    /// <remarks></remarks>
        public UFAuthLevel GetAuthLevel()
        {
            // *履歴番号 000002 2015/01/09 追加開始
            try
            {
                return m_cfUserInfo.GetBangoAuth(m_cfControlData.m_strUserId, m_cfControlData.m_strBusinessId);
            }
            catch (Exception csExp)
            {
                throw;
            }
            // *履歴番号 000002 2015/01/09 追加終了
        }

        #endregion

        #region 番号タイプ取得


        /// <summary>
    /// 番号タイプ取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strAtenaDataKB">宛名データ区分</param>
    /// <returns>番号タイプ</returns>
    /// <remarks>桁数により番号タイプを判定し、返信します。</remarks>
        public int GetBangoTypeWithAtenaDataKB(string strMyNumber, string strAtenaDataKB)
        {
            return GetBangoType(strMyNumber, GetBangoTypeWithAtenaDataKB(strAtenaDataKB));
        }

        #endregion

        #region 番号タイプ取得

        /// <summary>
    /// 番号タイプ取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="strUserKB">ユーザー区分</param>
    /// <returns>番号タイプ</returns>
    /// <remarks>桁数により番号タイプを判定し、返信します。</remarks>
        public int GetBangoTypeWithUserKB(string strMyNumber, string strUserKB)
        {
            return GetBangoType(strMyNumber, GetBangoTypeWithUserKB(strUserKB));
        }

        #endregion

        #region 番号タイプ取得

        /// <summary>
    /// 番号タイプ取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="intBangoType">番号タイプ</param>
    /// <returns>番号タイプ</returns>
    /// <remarks>桁数により番号タイプを判定し、返信します。</remarks>
        public int GetBangoType(string strMyNumber, int intBangoType)
        {

            int intResult;
            string strMyNumberWork;

            if (strMyNumber is null)
            {
                strMyNumberWork = string.Empty;
            }
            else
            {
                strMyNumberWork = strMyNumber.Trim();
            }

            switch (strMyNumberWork.RLength)
            {
                case var @case when @case == ABConstClass.MYNUMBER.LENGTH.KOJIN:
                    {
                        // 12桁の場合、個人番号と判定
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN;
                        break;
                    }
                case var case1 when case1 == ABConstClass.MYNUMBER.LENGTH.HOJIN:
                    {
                        // 13桁の場合、法人番号と判定
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN;
                        break;
                    }

                default:
                    {
                        // 上記以外の場合、指定された番号タイプに従う
                        intResult = intBangoType;
                        break;
                    }
            }

            return intResult;

        }

        #endregion

        #region 番号タイプ取得

        /// <summary>
    /// 番号タイプ取得
    /// </summary>
    /// <param name="strAtenaDataKB">宛名データ区分</param>
    /// <returns>番号タイプ</returns>
    /// <remarks>宛名データ区分により番号タイプを判定し、返信します。</remarks>
        private int GetBangoTypeWithAtenaDataKB(string strAtenaDataKB)
        {

            int intResult;
            string strAtenaDataKBWork;

            if (strAtenaDataKB is null)
            {
                strAtenaDataKBWork = string.Empty;
            }
            else
            {
                strAtenaDataKBWork = strAtenaDataKB.Trim();
            }

            // "11"（住登内個人）　→　個人
            // "12"（住登外個人）　→　個人
            // "20"（法人）　　　　→　法人
            // "30"（共有）      　→　個人（共有は個人扱い ※既存に準拠）
            // 上記以外（不明）    →　法人（宛名データ区分が不明の場合、法人として扱う ※桁数が法人の方が多い為）
            switch (strAtenaDataKBWork ?? "")
            {
                case var @case when @case == ABConstClass.ATENADATAKB_JUTONAI_KOJIN:
                case var case1 when case1 == ABConstClass.ATENADATAKB_JUTOGAI_KOJIN:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN;
                        break;
                    }
                case var case2 when case2 == ABConstClass.ATENADATAKB_HOJIN:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN;
                        break;
                    }
                case var case3 when case3 == ABConstClass.ATENADATAKB_KYOYU:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN;
                        break;
                    }

                default:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN;
                        break;
                    }
            }

            return intResult;

        }

        #endregion

        #region 番号タイプ取得

        /// <summary>
    /// 番号タイプ取得
    /// </summary>
    /// <param name="strUserKB">ユーザー区分</param>
    /// <returns>番号タイプ</returns>
    /// <remarks>ユーザー区分により番号タイプを判定し、返信します。</remarks>
        private int GetBangoTypeWithUserKB(string strUserKB)
        {

            int intResult;
            string strUserKBWork;

            if (strUserKB is null)
            {
                strUserKBWork = string.Empty;
            }
            else
            {
                strUserKBWork = strUserKB.Trim();
            }

            // "2"（個人）　　　→　個人
            // "1"（法人）　　　→　法人
            // 上記以外（不明） →　法人（ユーザー区分が不明の場合、法人として扱う ※桁数が法人の方が多い為）
            switch (strUserKBWork ?? "")
            {
                case var @case when @case == ABConstClass.eLTAX.USERKB.KOJIN:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.KOJIN;
                        break;
                    }
                case var case1 when case1 == ABConstClass.eLTAX.USERKB.HOJIN:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN;
                        break;
                    }

                default:
                    {
                        intResult = ABConstClass.MYNUMBER.BANGOTYPE.HOJIN;
                        break;
                    }
            }

            return intResult;

        }

        #endregion

        #endregion

    }
}
