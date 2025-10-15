// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        共通番号マスタ共通処理ビジネスクラス(ABMyNumberCommonBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2014/04/30　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2016/01/21   000001      公表の同意取得対応（岩下）
// ************************************************************************************************

using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
// *履歴番号 000001 2016/01/21 追加開始
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{
    // *履歴番号 000001 2016/01/21 追加終了

    /// <summary>
/// 共通番号マスタ共通処理ビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABMyNumberCommonBClass
    {

        #region メンバー変数

        private UFControlData m_cfControlData;                    // コントロールデータ
        private UFConfigClass m_cfConfig;                         // コンフィグクラス
        private UFConfigDataClass m_cfConfigDataClass;            // コンフィグデータクラス
        private UFLogClass m_cfLogClass;                          // ログ出力クラス
        private UFRdbClass m_cfRdbClass;                          // ＲＤＢクラス
                                                                  // *履歴番号 000001 2016/01/21 追加開始
        private string m_strSelectSQL;
        // *履歴番号 000001 2016/01/21 追加開始

        private ABMyNumberBClass m_cABMyNumberB;                  // 共通番号ビジネスクラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABMyNumberCommonBClass";

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        public ABMyNumberCommonBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // 共通番号ビジネスクラスのインスタンス化
            m_cABMyNumberB = new ABMyNumberBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

        }

        #endregion

        #region メソッド

        #region GetMyNumber

        /// <summary>
    /// 共通番号取得
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <returns>共通番号</returns>
    /// <remarks>
    /// 引数の住民コードに対応する直近の共通番号を取得し返信します。
    /// </remarks>
        public string GetMyNumber(string strJuminCd)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            DataSet csDataSet;
            string strMyNumber = string.Empty;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 共通番号マスタ取得
                csDataSet = m_cABMyNumberB.SelectByJuminCd(strJuminCd);

                // 返信オブジェクトの整備
                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0 && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength > 0)

                {
                    strMyNumber = csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.MYNUMBER).ToString.Trim;
                }
                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            return strMyNumber;

        }

        #endregion

        #region GetJuminCd

        /// <summary>
    /// 住民コード取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <returns>共通番号文字列配列</returns>
    /// <remarks>
    /// 引数の共通番号に対応する住民コードを取得し返信します。
    /// 共通番号を履歴を含めて検索します。
    /// </remarks>
        public string[] GetJuminCd(string strMyNumber)
        {
            return GetJuminCd(strMyNumber, false);
        }

        /// <summary>
    /// 住民コード取得
    /// </summary>
    /// <param name="strMyNumber">共通番号</param>
    /// <param name="blnCkinFG">直近フラグ（True：直近のみ検索、False：履歴を含めて検索）</param>
    /// <returns>住民コード文字列配列</returns>
    /// <remarks>
    /// 引数の共通番号に対応する住民コードを取得し返信します。
    /// 直近のみ検索、履歴を含めて検索の指定が可能です。
    /// </remarks>
        public string[] GetJuminCd(string strMyNumber, bool blnCkinFG)

        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            DataSet csDataSet;
            string[] a_strJuminCd = null;
            int intIndex;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 共通番号マスタ取得
                if (blnCkinFG == true)
                {
                    csDataSet = m_cABMyNumberB.SelectByMyNumber(strMyNumber, ABMyNumberEntity.DEFAULT.CKINKB.CKIN);
                }
                else
                {
                    csDataSet = m_cABMyNumberB.SelectByMyNumber(strMyNumber, string.Empty);
                }

                // 返信オブジェクトの整備
                if (csDataSet is not null && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0)
                {
                    a_strJuminCd = new string[csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count];
                    intIndex = 0;
                    foreach (DataRow csDataRow in csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows)
                    {
                        a_strJuminCd[intIndex] = csDataRow.Item(ABMyNumberEntity.JUMINCD).ToString;
                        intIndex += 1;
                    }
                }
                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            return a_strJuminCd;

        }

        #endregion

        // *履歴番号 000001 2016/01/21 追加開始
        #region GetConsent
        /// <summary>
    /// 公表の同意取得
    /// </summary>
    /// <param name="strJuminCd">住民コード</param>
    /// <returns>公表の同意</returns>
    /// <remarks>
    /// 対象の住民コードの直近個人番号が法人番号を持っている場合、公表の同意を取得し返信します。
    /// それ以外はNothinggを返却します。
    /// 取得した公表の同意が不正な値の場合もNothingを返却します。
    /// </remarks>
        public string GetConsent(string strJuminCd)
        {
            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string strKohyounoDoui = null;
            List<string> strJuminCdLst;
            Dictionary<string, string> dicReturn;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strJuminCdLst = new List<string>();
                strJuminCdLst.Add(strJuminCd);

                // 公表の同意リスト取得メソッドを呼出
                dicReturn = GetConsent(strJuminCdLst);

                if (dicReturn is not null && dicReturn.Count > 0)
                {
                    strKohyounoDoui = dicReturn[strJuminCd];
                }
                else
                {
                    strKohyounoDoui = null;
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            return strKohyounoDoui;

        }

        /// <summary>
    /// 公表の同意取得
    /// </summary>
    /// <param name="strJuminCdLst">住民コードリスト</param>
    /// <returns>Dictionary(住民コード, 公表の同意)</returns>
    /// <remarks>
    /// 対象の住民コードが法人番号を持っている場合、公表の同意を取得し返信します。その他はNothingを返却します。
    /// それ以外はNothinggを返却します。
    /// 取得した公表の同意が不正な値の場合もNothingを返却します。
    /// </remarks>
        public Dictionary<string, string> GetConsent(List<string> strJuminCdLst)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            DataSet csDataSet;
            string strKohyounoDoui = null;
            string strR;
            Dictionary<string, string> dicReturn;
            List<string> lstSortedJuminCD;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数リストの整理
                lstSortedJuminCD = new List<string>();
                if (strJuminCdLst is not null)
                {
                    lstSortedJuminCD = strJuminCdLst.Distinct().ToList();
                }
                else
                {
                    // 住民コードリストが存在しない
                    return null;
                }

                // Dictionaryのインスタンス化
                dicReturn = new Dictionary<string, string>();

                foreach (var strJuminCD in lstSortedJuminCD)
                {
                    // 共通番号マスタ取得(削除データ除く)
                    csDataSet = m_cABMyNumberB.SelectConsentByJuminCd(strJuminCD, false);

                    // 返信オブジェクトの整備
                    if (csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0 && csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength == 13)
                    {
                        strR = csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)(ABMyNumberEntity.RESERVE).ToString.Trim;

                        switch (strR ?? "")
                        {
                            case var @case when @case == ABConstClass.KOHYONODOUI.KOHYOZUMI_CODE:
                            case var case1 when case1 == ABConstClass.KOHYONODOUI.ARI_CODE:
                            case var case2 when case2 == ABConstClass.KOHYONODOUI.NASHI_CODE:
                            case var case3 when case3 == ABConstClass.KOHYONODOUI.HUYO_CODE:
                                {

                                    strKohyounoDoui = strR;
                                    break;
                                }

                            default:
                                {
                                    strKohyounoDoui = null;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        // noop
                        strKohyounoDoui = null;
                    }

                    dicReturn.Add(strJuminCD, strKohyounoDoui);
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            return dicReturn;

        }

        #endregion
        // *履歴番号 000001 2016/01/21 追加終了

        #endregion

    }
}
