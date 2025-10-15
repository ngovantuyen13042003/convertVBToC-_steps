// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        異動事由(ABIdoJiyuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/04/01　滝沢　欽也
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

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABIdoJiyuBClass
    {

        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABIdoJiyuBClass";

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData, 
        // *                                  ByVal cfConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // *                   cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABIdoJiyuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {

            // メンバ変数セット
            m_cfControlData = cfControlData;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

        }

        // ************************************************************************************************
        // * メソッド名      異動事由取得
        // * 
        // * 構文            Public Sub GetIdoJiyu(ByVal strAtenaDataKB As String,
        // *                                         ByVal strAtenaDataSHU As String)
        // * 
        // * 機能　　        宛名データ区分、宛名データ種別より名称を編集する
        // * 
        // * 引数            strIdoJiyuCD As String   : 異動事由コード
        // * 
        // * 戻り値          異動事由(String)
        // ************************************************************************************************
        public string GetIdoJiyu(string strIdoJiyuCD)
        {
            const string THIS_METHOD_NAME = "GetIdoJiyu";
            var strIdoJiyu = default(string);

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strIdoJiyuCD ?? "")
                {
                    case "001":
                    case "01":
                        {
                            strIdoJiyu = "削除";
                            break;
                        }
                    case "002":
                    case "02":
                        {
                            strIdoJiyu = "追加";
                            break;
                        }
                    case "010":
                    case "10":
                        {
                            strIdoJiyu = "転入";
                            break;
                        }
                    case "011":
                    case "11":
                        {
                            strIdoJiyu = "出生";
                            break;
                        }
                    case "012":
                    case "12":
                        {
                            strIdoJiyu = "職権記載";
                            break;
                        }
                    case "013":
                    case "13":
                        {
                            strIdoJiyu = "帰化";
                            break;
                        }
                    case "014":
                    case "14":
                        {
                            strIdoJiyu = "国籍取得";
                            break;
                        }
                    case "015":
                    case "15":
                        {
                            strIdoJiyu = "回復";
                            break;
                        }
                    case "020":
                    case "20":
                        {
                            strIdoJiyu = "転出";
                            break;
                        }
                    case "021":
                    case "21":
                        {
                            strIdoJiyu = "死亡";
                            break;
                        }
                    case "022":
                    case "22":
                        {
                            strIdoJiyu = "職権消除";
                            break;
                        }
                    case "023":
                    case "23":
                        {
                            strIdoJiyu = "国籍喪失";
                            break;
                        }
                    case "024":
                    case "24":
                        {
                            strIdoJiyu = "失踪";
                            break;
                        }

                    default:
                        {
                            strIdoJiyu = "";
                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



            }
            // エラーをそのままスローする

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;
            }

            return strIdoJiyu;

        }

    }
}
