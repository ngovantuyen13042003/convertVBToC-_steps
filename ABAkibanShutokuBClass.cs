// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        宛名空番取得(ABAkibanShutokuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/20　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/04/19  000001      住民コード取得(宛名法人用)・(宛名共有用)処理追加 
// * 2007/02/05  000002      宛名更新エラーログ番号取得処理追加（内山(堅)）
// * 2007/04/02  000003      コード取得時の存在チェック処理を修正（比嘉）
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{

    public class ABAkibanShutokuBClass
    {

        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFControlData m_cfUFControlData;      // コントロールデータ

        // パラメータのメンバ変数
        private string m_strBango;                    // 取得番号

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAkibanShutokuBClass";

        // 各メンバ変数のプロパティ定義
        public string p_strBango
        {
            get
            {
                return m_strBango;
            }
        }

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAkibanShutokuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {

            // メンバ変数セット
            m_cfUFControlData = cfControlData;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strBango = string.Empty;
        }

        // ************************************************************************************************
        // * メソッド名      住民コード取得
        // * 
        // * 構文            Public Sub GetJuminCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetJuminCD()
        {
            const string THIS_METHOD_NAME = "GetJuminCD";             // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0001", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                // コード存在チェック
                AtenaDBChecKCD(cuGetNum, "0");

                // '住民コードを１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // ************************************************************************************************
        // * メソッド名      住民コード取得（宛名用）
        // * 
        // * 構文            Public Sub GetAtenaJuminCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetAtenaJuminCD()
        {
            const string THIS_METHOD_NAME = "GetAtenaJuminCD";            // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0002", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "0");

                // '住民コード（宛名用）を１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // ************************************************************************************************
        // * メソッド名      世帯コード取得
        // * 
        // * 構文            Public Sub GetSetaiCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetSetaiCD()
        {
            const string THIS_METHOD_NAME = "GetSetaiCD";             // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0003", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "1");

                // '世帯コードを１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // ************************************************************************************************
        // * メソッド名      世帯コード取得（宛名用）
        // * 
        // * 構文            Public Sub GetAtenaSetaiCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetAtenaSetaiCD()
        {
            const string THIS_METHOD_NAME = "GetAtenaSetaiCD";        // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0004", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "1");

                // '世帯コード（宛名用）を１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // ************************************************************************************************
        // * メソッド名      共有者コード取得
        // * 
        // * 構文            Public Sub GetKyoyuCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetKyoyuCD()
        {
            const string THIS_METHOD_NAME = "GetKyoyuCD";             // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0005", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "0");

                // '共有者コードを１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // *履歴番号 000001 2004/04/19 追加開始
        // ************************************************************************************************
        // * メソッド名      住民コード取得（宛名法人用）
        // * 
        // * 構文            Public Sub GetAtenaHojinCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetAtenaHojinCD()
        {
            const string THIS_METHOD_NAME = "GetAtenaHojinCD";            // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0006", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "0");

                // '住民コード（宛名用）を１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }

        // ************************************************************************************************
        // * メソッド名      住民コード取得（宛名共有用）
        // * 
        // * 構文            Public Sub GetAtenaKyoyuCD()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetAtenaKyoyuCD()
        {
            const string THIS_METHOD_NAME = "GetAtenaKyoyuCD";            // このメソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "0007", "0000");

                // *履歴番号 000003 2007/04/02 修正開始
                AtenaDBChecKCD(cuGetNum, "0");

                // '住民コード（宛名用）を１件取得
                // cuGetNum.GetNum(m_cfUFControlData)

                // '取得番号をプロパティにセット
                // m_strBango = cuGetNum.p_strBango(0)
                // *履歴番号 000003 2007/04/02 修正終了

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }
        }
        // *履歴番号 000001 2004/04/19 追加終了

        // *履歴番号 000003 2007/04/02 追加開始
        // ************************************************************************************************
        // * メソッド名      コード取得時の存在チェック
        // * 
        // * 構文            Public Sub AtenaDBChecKCD(ByVal cuGetNum As USSnumgetClass, ByVal strChkCD As String)
        // * 
        // * 機能　　        取得したコードが宛名ＤＢ上に存在しないかチェックを行う。
        // * 
        // * 引数            cuGetNum As USSnumgetClass   :番号取得クラス 
        // *                 strChkCD As String           :コード取得判定フラグ
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void AtenaDBChecKCD(USSnumgetClass cuGetNum, string strChkCD)
        {
            const string THIS_METHOD_NAME = "AtenaDBChecKCD";     // メソッド名
            UFRdbClass cfRdb;                                 // RDBクラス
            bool blnChkCD = true;                          // コード存在チェックフラグ
            StringBuilder csSB;
            UFParameterCollectionClass cfParamCollection;     // パラメータコレクションクラス
            UFDataReaderClass cfDataReder;                    // データリーダークラス

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // インスタンス化
                csSB = new StringBuilder();
                cfParamCollection = new UFParameterCollectionClass();

                // SQL作成
                // * SELECT JUMINCD FROM ABATENA WHERE JUMINCD = @JUMINCD
                // * SELECT JUMINCD FROM ABATENA WHERE STAICD = @STAICD
                csSB.Append("SELECT ").Append(ABAtenaEntity.JUMINCD);
                csSB.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME);
                if (strChkCD == "0")
                {
                    // 住民コードの存在値チェック
                    csSB.Append(" WHERE ").Append(ABAtenaEntity.JUMINCD);
                    csSB.Append(" = ").Append(ABAtenaEntity.PARAM_JUMINCD);
                }
                else
                {
                    // 世帯コードの存在値チェック
                    csSB.Append(" WHERE ").Append(ABAtenaEntity.STAICD);
                    csSB.Append(" = ").Append(ABAtenaEntity.PARAM_STAICD);
                }

                // RDBクラスのインスタンス作成
                cfRdb = new UFRdbClass(m_cfUFControlData.m_strBusinessId);
                // RDB接続
                cfRdb.Connect();

                try
                {
                    // 空きコードが見つかるまで繰り返す
                    while (blnChkCD)
                    {
                        // 空番取得
                        cuGetNum.GetNum(m_cfUFControlData);

                        cfParamCollection.Clear();
                        // 住民コードか世帯コードか判断
                        if (strChkCD == "0")
                        {
                            // 住民コードの場合
                            cfParamCollection.Add(ABAtenaEntity.PARAM_JUMINCD, cuGetNum.p_strBango(0));
                        }
                        else
                        {
                            // 世帯コードの場合
                            cfParamCollection.Add(ABAtenaEntity.PARAM_STAICD, cuGetNum.p_strBango(0));
                        }

                        cfDataReder = cfRdb.GetDataReader(csSB.ToString(), cfParamCollection);
                        if (cfDataReder.Read == false)
                        {
                            // コードが存在しない場合
                            // チェックフラグをFalseにする
                            blnChkCD = false;
                        }
                        cfDataReder.Close();

                    }
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDBアクセスログ出力
                    m_cfUFLogClass.RdbWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");


                    // RDB切断
                    cfRdb.Disconnect();
                }

                // 取得番号をプロパティにセット
                m_strBango = cuGetNum.p_strBango(0);

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw;

            }
        }
        // *履歴番号 000003 2007/04/02 追加終了

        // *履歴番号 000002 2007/02/05 追加開始
        // ************************************************************************************************
        // * メソッド名      宛名更新エラーログ番号取得
        // * 
        // * 構文            Public Sub GetErrLogNo()
        // * 
        // * 機能　　        空番を取得する。
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetErrLogNo()
        {

            const string THIS_METHOD_NAME = "GetErrLogNo";          // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 番号取得クラスコンストラクタセット
                var cuGetNum = new USSnumgetClass("AB", "2001", "0000");

                // 宛名更新エラーログ番号を１件取得
                cuGetNum.GetNum(m_cfUFControlData);

                // 取得番号をプロパティにセット
                m_strBango = cuGetNum.p_strBango(0);

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;

            }

        }
        // *履歴番号 000002 2007/02/05 追加終了

    }
}