// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        コード存在チェックＢ(ABCodeUmuCheckBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/04/21　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/05/22 000001     RDBのConnectはﾒｿｯﾄﾞの先頭に変更(仕様変更)
// * 2010/04/16  000002      VS2008対応（比嘉）
// ************************************************************************************************
using System;
using System.Linq;

namespace Densan.Reams.AB.AB000BB
{

    public class ABCodeUmuCheckBClass
    {

        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private bool m_blnCodeUmu;                         // コード有無

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABCodeUmuCheckBClass";            // クラス名
        private const string THIS_BUSINESSID = "AB";                              // 業務コード

        // ************************************************************************************************
        // * 各メンバ変数のプロパティ定義
        // ************************************************************************************************

        public bool p_blnCodeUmu
        {
            get
            {
                return m_blnCodeUmu;
            }
            set
            {
                m_blnCodeUmu = value;
            }
        }

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControldata As UFControlData, 
        // *                                 ByVal cfConfigData As UFConfigDataClass,
        // *                                 ByVal cfRdb As UFRdbClass)
        // * 
        // * 機能           初期化処理
        // * 
        // * 引数           cfControlData As UFControlData        : コントロールデータオブジェクト
        // *                  cfConfigData As UFConfigDataClass     : コンフィグデータオブジェクト
        // *                  cfRdb As UFRdbClass                   : ＲＤＢオブジェクト
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public ABCodeUmuCheckBClass(UFControlData cfControldata, UFConfigDataClass cfConfigData)
        {
            // * corresponds to VS2008 Start 2010/04/16 000002
            // Const THIS_METHOD_NAME As String = "New"            'メソッド名
            // * corresponds to VS2008 End 2010/04/16 000002

            // メンバ変数セット
            m_cfControlData = cfControldata;
            m_cfConfigDataClass = cfConfigData;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // RDBクラスのインスタンス作成
            m_cfRdbClass = new UFRdbClass(THIS_BUSINESSID);

            // メンバ変数の初期化
            m_blnCodeUmu = false;
        }

        // ************************************************************************************************
        // * メソッド名      住民コード有無チェック
        // * 
        // * 構文           Public Sub JuminCDUmuCheck(ByVal strJuminCD As String)
        // * 
        // * 機能　　        住民コードが存在するかチェックする。
        // * 
        // * 引数           strJuminCD As String          : 住民コード
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void JuminCDUmuCheck(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "JuminCDUmuCheck";
            // * corresponds to VS2008 Start 2010/04/16 000002
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000002
            ABAtenaBClass cAtenaB;                        // 宛名ＤＡクラス
            var cAtenaSearchKey = new ABAtenaSearchKey();       // 宛名検索キー
            DataSet csAtenaEntity;                        // 宛名Entity
            int intDataCount;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");


                // RDB接続
                m_cfRdbClass.Connect();

                try
                {
                    // 宛名取得インスタンス化
                    cAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                    cAtenaSearchKey.p_strJuminCD = strJuminCD;

                    // 宛名ＤＡクラスの宛名取得メゾットを実行
                    csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, true);

                    intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count();

                    // データが０件のときは
                    if (intDataCount == 0)
                    {
                        m_blnCodeUmu = false;
                    }
                    else
                    {
                        m_blnCodeUmu = true;
                    }
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");



                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");


                    // RDB切断
                    m_cfRdbClass.Disconnect();
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;

            }

        }

        // ************************************************************************************************
        // * メソッド名      世帯コード有無チェック
        // * 
        // * 構文           Public Sub StaiCDUmuCheck(ByVal strStaiCD As String)
        // * 
        // * 機能　　        世帯コードが存在するかチェックする。
        // * 
        // * 引数           strStaiCD As String          : 世帯コード
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void StaiCDUmuCheck(string strStaiCD)
        {
            const string THIS_METHOD_NAME = "StaiCDUmuCheck";
            // * corresponds to VS2008 Start 2010/04/16 000002
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000002
            ABAtenaBClass cAtenaB;                        // 宛名ＤＡクラス
            var cAtenaSearchKey = new ABAtenaSearchKey();       // 宛名検索キー
            DataSet csAtenaEntity;                        // 宛名Entity
            int intDataCount;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");


                // RDB接続
                m_cfRdbClass.Connect();

                try
                {
                    // 宛名取得インスタンス化
                    cAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                    cAtenaSearchKey.p_strStaiCD = strStaiCD;

                    // 宛名ＤＡクラスの宛名取得メゾットを実行
                    csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, true);

                    intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count();

                    // データが０件のときは
                    if (intDataCount == 0)
                    {
                        m_blnCodeUmu = false;
                    }
                    else
                    {
                        m_blnCodeUmu = true;
                    }
                }

                catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");



                    // UFAppExceptionをスローする
                    throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
                }
                catch
                {
                    // エラーをそのままスロー
                    throw;
                }
                finally
                {
                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");


                    // RDB切断
                    m_cfRdbClass.Disconnect();
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;

            }

        }

    }
}