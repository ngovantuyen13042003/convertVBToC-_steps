// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        宛名管理情報キャッシュＤＡ(ABKANRIJOHOCacheBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2014/04/28　岩下 一美
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2014/04/28  000000      新規作成
// * 2014/06/11  000001      バッチ処理よりコールされた際のエラー修正（田中）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABKANRIJOHOCacheBClass : ABAtenaKanriJohoBClass
    {

        #region メンバ変数
        // **
        // * クラスID定義
        // * 
        private const string THIS_CLASS_NAME = "ABKANRIJOHOCacheBClass";

        // メンバ変数の定義
        private URLogXClass m_cfLog;                                     // ログ出力クラス

        // キャッシュクラス
        private const string ABKANRIJOHO = "ABKANRIJOHO";
        private class CacheDataClass
        {
            public string m_strUpdate;
            public DataSet m_csDS;
        }

        // 宛名管理情報　種別キー・識別キー
        private const string SHUBETSUKEY_KOJINJOHOSEIGYO = "20";         // 種別キー:20：個人情報制御機能
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // *                               ByVal cfConfigData As UFConfigDataClass, 
        // *                               ByVal cfRdb As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData    : コントロールデータオブジェクト
        // *                cfConfigData As UFConfigDataClass : コンフィグデータオブジェクト
        // *                cfRdb As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************

        public ABKANRIJOHOCacheBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb) : base(cfControlData, cfConfigData, cfRdb)

        {

            // ログ出力クラスインスタンス化
            m_cfLog = new URLogXClass(cfControlData, cfConfigData, GetType().Name);

        }
        #endregion

        #region メソッド
        #region 管理情報マスタ抽出
        // ************************************************************************************************
        // * メソッド名     管理情報マスタ抽出
        // * 
        // * 構文           Private Function GetKanriJohoHoshu() As DataSet
        // * 
        // * 機能           指定された管理情報マスタを条件により該当データを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet : 取得した管理情報マスタの該当データ
        // ************************************************************************************************
        public new DataSet GetKanriJohoHoshu()
        {
            return GetKanriJohoHoshu(string.Empty, string.Empty);
        }

        // ************************************************************************************************
        // * メソッド名     管理情報マスタ抽出
        // * 
        // * 構文           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String) As DataSet
        // * 
        // * 機能           指定された管理情報マスタを条件により該当データを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet : 取得した管理情報マスタの該当データ
        // ************************************************************************************************
        public new DataSet GetKanriJohoHoshu(string strShuKEY)
        {
            const string THIS_METHOD_NAME = "GetKanriJohoHoshu";     // メソッド名
            DataSet csRet;
            string strMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                m_cfLog.DebugStartWrite(strMethodName);

                // キャッシュからデータを取得
                csRet = GetKanriJohoHoshu(strShuKEY, string.Empty);

                m_cfLog.DebugEndWrite(strMethodName);

                return csRet;
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite("【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】", "【ワーニングコード:" + objAppExp.p_strErrorCode + "】", "【ワーニング内容:" + objAppExp.Message + "】");


                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite("【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】", "【エラー内容:" + objExp.Message + "】");

                throw objExp;
            }
        }

        // ************************************************************************************************
        // * メソッド名     管理情報マスタ抽出
        // * 
        // * 構文           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String, _
        // *                                                      ByVal strShikibetsuKEY As String) As DataSet
        // * 
        // * 機能           指定された管理情報マスタを条件により該当データを取得する
        // * 
        // * 引数           strShuKEY As String        : 種別キー（管理情報マスタ取得時のキー）
        // *                strShikibetsuKEY As String : 識別キー（管理情報マスタ取得時のキー）
        // * 
        // * 戻り値         DataSet : 取得した管理情報マスタの該当データ
        // ************************************************************************************************
        public new DataSet GetKanriJohoHoshu(string strShuKEY, string strShikibetsuKEY)
        {
            DataSet csDS;
            DataSet csRetDS;
            var csDRs = default(DataRow[]);
            int intI;
            DataTable csRetDT;
            var csSB = new StringBuilder();

            // キャッシュから管理情報の取得
            csDS = GetDataFromCache();

            // Filter条件の作成
            if (!string.IsNullOrEmpty(strShuKEY))
            {
                csSB.Append(ABAtenaKanriJohoEntity.SHUKEY).Append(" = '").Append(strShuKEY).Append("'");
                if (!string.IsNullOrEmpty(strShikibetsuKEY))
                {
                    csSB.Append(" AND ");
                }
            }
            if (!string.IsNullOrEmpty(strShikibetsuKEY))
            {
                csSB.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).Append(" = '").Append(strShikibetsuKEY).Append("'");
            }
            if (csSB.RLength > 0)
            {
                csDRs = csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Select(csSB.ToString());
            }

            csRetDS = csDS.Clone;
            csRetDT = csRetDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME);
            var loopTo = csDRs.Length - 1;
            for (intI = 0; intI <= loopTo; intI++)
                csRetDT.ImportRow(csDRs[intI]);
            return csRetDS;
        }

        // ************************************************************************************************
        // * メソッド名     管理情報マスタ取得
        // * 
        // * 構文           Private Function GetDataFromCache() As DataSet
        // * 
        // * 機能           管理情報マスタをキャッシュから取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet
        // ************************************************************************************************
        private DataSet GetDataFromCache()
        {
            const string THIS_METHOD_NAME = "GetDataFromCache";     // メソッド名
            var cCacheData = default(CacheDataClass);
            DataSet csRet;

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(THIS_METHOD_NAME);

                lock (typeof(HttpContext))
                {
                    try
                    {
                        cCacheData = (CacheDataClass)HttpContext.Current.Cache[ABKANRIJOHO];
                    }
                    catch
                    {
                    }
                    if (cCacheData is null)
                    {
                        // *履歴番号 000001 2014/06/11 修正開始
                        // m_cfLog.DebugWrite("キャッシュ作成(ABKANRIJOHO)")
                        // cCacheData = New CacheDataClass()
                        // cCacheData.m_csDS = MyBase.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO)
                        // cCacheData.m_strUpdate = String.Empty
                        // HttpContext.Current.Cache(ABKANRIJOHO) = cCacheData

                        csRet = base.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO);

                        if (HttpContext.Current is not null)
                        {
                            // HttpContext.CurrentがNothingでない場合
                            m_cfLog.DebugWrite("キャッシュ作成(ABKANRIJOHO)");
                            cCacheData = new CacheDataClass();
                            cCacheData.m_csDS = csRet;
                            cCacheData.m_strUpdate = string.Empty;
                            HttpContext.Current.Cache[ABKANRIJOHO] = cCacheData;
                        }
                        else
                        {
                            // それ以外の場合、処理なし
                        }
                    }
                    // *履歴番号 000001 2014/06/11 修正終了
                    else
                    {
                        m_cfLog.DebugWrite("キャッシュ中にデータ有");
                        // *履歴番号 000001 2014/06/11 追加開始
                        csRet = cCacheData.m_csDS;
                        // *履歴番号 000001 2014/06/11 追加終了
                    }
                    // *履歴番号 000001 2014/06/11 削除開始
                    // csRet = cCacheData.m_csDS
                    // *履歴番号 000001 2014/06/11 削除終了

                }

                m_cfLog.DebugEndWrite(THIS_METHOD_NAME);

                return csRet;
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite("【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】", "【ワーニングコード:" + objAppExp.p_strErrorCode + "】", "【ワーニング内容:" + objAppExp.Message + "】");


                throw objAppExp;
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite("【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】", "【エラー内容:" + objExp.Message + "】");

                throw objExp;
            }
        }
        #endregion
        #endregion

    }
}
