// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        宛名更新エラーログＤＢ管理(ABErrLogBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2007/02/05　内山 堅太郎
// * 
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABErrLogBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFConfigDataClass m_cfConfigDataClass;                      // コンフィグデータ
        private UFControlData m_cfControlData;                              // コントロールデータ
        private UFLogClass m_cfLogClass;                                    // ログ出力クラス
        private UFParameterCollectionClass m_cfInsParamCollection;          // INSERT用パラメータコレクション
        private string m_strInsertSQL;                                      // INSERT用SQL
        private string m_strRsBusinId;                                      // ビジネスＩＤ保存用

        // コンスタント定義
        private const string TAISHOKBN_MIKAKUNIN = "0";                     // 未確認
        private const string TAISHOKBN_ZUMI = "1";                          // 確認済
        private const string JOKYOKBN_NORMAL = "0";                         // 正常終了
        private const string JOKYOKBN_ERR = "9";                            // 異常終了
        private const string SPACE = " ";                                   // SPACE

        private const string THIS_CLASS_NAME = "ABErrLogBClass";            // クラス名

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData,
        // * 　　                           ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // * 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABErrLogBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {

            // メンバ変数へセット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;

            // ログ出力クラスインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // 受け取ったビジネスIDをメンバへ保存
            m_strRsBusinId = m_cfControlData.m_strBusinessId;

            // メンバ変数の初期化
            m_strInsertSQL = string.Empty;
            m_cfInsParamCollection = (object)null;

        }

        #endregion

        #region エラーログ取得
        // ************************************************************************************************
        // * メソッド名      エラーログ取得
        // * 
        // * 構文            Public Function GetABErrLog() As String()
        // * 
        // * 機能            エラーログの取得を行なう
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          String()：エラー発生場所とエラーメッセージ
        // ************************************************************************************************
        public string[] GetABErrLog()
        {

            const string THIS_METHOD_NAME = "GetABErrLog";
            var cfRdb = default(UFRdbClass);
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csABErrLogEntity;
            int intCnt;
            string strGyomuMei;
            string strErrMSG;
            string[] strReturn;
            var strSQL = new StringBuilder();

            try
            {
                // 業務ＩＤを宛名(AB)に変更
                m_cfControlData.m_strBusinessId = "AB";

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");



                // RDBクラスのインスタンス作成
                cfRdb = new UFRdbClass(m_cfControlData.m_strBusinessId);

                // RDB接続
                cfRdb.Connect();

                // SelectSQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABErrLogEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABErrLogEntity.TAISHOKB);
                strSQL.Append(" = ");
                strSQL.Append(ABErrLogEntity.KEY_TAISHOKB);
                strSQL.Append(" AND ");
                strSQL.Append(ABErrLogEntity.JOKYOKB);
                strSQL.Append(" = ");
                strSQL.Append(ABErrLogEntity.KEY_JOKYOKB);
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABErrLogEntity.LOGNO);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB;          // 対処区分
                cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB;           // 状況区分
                cfUFParameterClass.Value = JOKYOKBN_ERR;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQL実行 DataSet取得
                csABErrLogEntity = cfRdb.GetDataSet(strSQL.ToString(), ABErrLogEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // 戻り値編集用配列初期化
                var strRet = new string[csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count];

                // 戻り値編集
                // For intCnt = 0 To csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count - 1
                // csDataRow = csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows(intCnt)
                // strGyomuMei = CType(csDataRow(ABErrLogEntity.MSG5), String).Trim
                // strErrMSG = CType(csDataRow(ABErrLogEntity.MSG7), String).Trim
                // strRet(intCnt) = strGyomuMei + "," + strErrMSG
                // Next intCnt

                intCnt = 0;
                foreach (DataRow csDataRow in csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows)
                {
                    strGyomuMei = ((string)csDataRow(ABErrLogEntity.MSG5)).Trim;          // エラー発生場所
                    strErrMSG = ((string)csDataRow(ABErrLogEntity.MSG7)).Trim;            // エラーメッセージ
                    strRet[intCnt] = strGyomuMei + "," + strErrMSG;
                    intCnt += 1;
                }

                // 戻り値セット
                strReturn = strRet;
            }

            catch (UFRdbException objRdbExp)                          // RdbExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objRdbExp.Message + "】");


                // ワーニングをスローする
                throw objRdbExp;
            }

            catch (UFRdbDeadLockException objRdbDeadLockExp)          // デッドロックをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbDeadLockExp.Message + "】");



                // ワーニングをスローする
                throw objRdbDeadLockExp;
            }

            catch (UFRdbUniqueException objUFRdbUniqueExp)            // 一意制約違反をキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】");



                // ワーニングをスローする
                throw objUFRdbUniqueExp;
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)            // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");



                // ワーニングをスローする
                throw objRdbTimeOutExp;
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException)                             // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;
            }
            finally
            {
                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");



                // RDB切断
                cfRdb.Disconnect();

                // 元のビジネスIDを入れる
                m_cfControlData.m_strBusinessId = m_strRsBusinId;

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

            }

            // 戻り値設定
            return strReturn;

        }

        #endregion

        #region エラーログ追加
        // ************************************************************************************************
        // * メソッド名      エラーログ追加
        // * 
        // * 構文            Public Function InsertABErrLog(ByVal cABErrLogXClass As ABErrLogXClass) As Integer
        // * 
        // * 機能            エラーログの追加を行なう
        // * 
        // * 引数            cABErrLogXClass As ABErrLogXClass : 追加データ
        // * 
        // * 戻り値          Integer ： 追加したデータの件数
        // ************************************************************************************************
        public int InsertABErrLog(ABErrLogXClass cABErrLogXClass)
        {

            const string THIS_METHOD_NAME = "InsertABErrLog";
            ABAkibanShutokuBClass cABAkibanShutokuBClass;          // エラーログ番号空番取得
            UFErrorClass cfErrorClass;                             // エラークラス
            UFErrorStruct cfErrorStruct;                           // エラー定義構造体
            var cfRdb = default(UFRdbClass);
            UFParameterClass cfUFParameterClass;
            int intCheckCnt;
            int intInsCnt;
            string strErrLogNo;
            string strSystemDateTime;
            string strSystemDate;
            string strSystemTime;

            try
            {
                // 業務ＩＤを宛名(AB)に変更
                m_cfControlData.m_strBusinessId = "AB";

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Connect】");



                // RDBクラスのインスタンス作成
                cfRdb = new UFRdbClass(m_cfControlData.m_strBusinessId);

                // RDB接続
                cfRdb.Connect();

                // 引数チェック
                // 空白チェック
                if (cABErrLogXClass.p_strShichosonCD.Trim == string.Empty)          // 市町村コード
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode);
                }

                // 文字数チェック
                if (cABErrLogXClass.p_strShichosonCD.RLength > 6)                   // 市町村コード
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode);
                }

                // 数値チェック
                var loopTo = Len(cABErrLogXClass.p_strShichosonCD);
                for (intCheckCnt = 1; intCheckCnt <= loopTo; intCheckCnt++)            // 市町村コード
                {
                    if (!LikeOperator.LikeString(Strings.Mid(cABErrLogXClass.p_strShichosonCD, intCheckCnt, 1), "[0-9]", CompareMethod.Binary))
                    {
                        cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                        cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001014);
                        // 例外を生成
                        throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode);
                    }
                }

                // 空白チェック
                if (cABErrLogXClass.p_strShoriID.Trim == string.Empty)              // 処理ＩＤ
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode);
                }

                // 文字数チェック
                if (cABErrLogXClass.p_strShoriID.RLength > 5)                       // 処理ＩＤ
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode);
                }

                // 半角チェック
                var loopTo1 = Len(cABErrLogXClass.p_strShoriID);
                for (intCheckCnt = 1; intCheckCnt <= loopTo1; intCheckCnt++)                // 処理ＩＤ
                {
                    if (!LikeOperator.LikeString(Strings.Mid(cABErrLogXClass.p_strShoriID, intCheckCnt, 1), "[0-9a-zA-Z]", CompareMethod.Binary))
                    {
                        cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                        cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080);
                        // 例外を生成
                        throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode);
                    }
                }

                // 空白チェック
                if (cABErrLogXClass.p_strShoriShu.Trim == string.Empty)             // 処理種別
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode);
                }

                // 文字数チェック
                if (cABErrLogXClass.p_strShoriShu.RLength > 4)                      // 処理種別
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode);
                }

                // 半角チェック
                var loopTo2 = Len(cABErrLogXClass.p_strShoriShu);
                for (intCheckCnt = 1; intCheckCnt <= loopTo2; intCheckCnt++)               // 処理種別
                {
                    if (!LikeOperator.LikeString(Strings.Mid(cABErrLogXClass.p_strShoriShu, intCheckCnt, 1), "[0-9a-zA-Z]", CompareMethod.Binary))
                    {
                        cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                        cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080);
                        // 例外を生成
                        throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode);
                    }
                }

                // 空白チェック
                if (cABErrLogXClass.p_strMsg5.Trim == string.Empty)                 // メッセージ５（エラー発生場所）
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "エラー発生場所】", cfErrorStruct.m_strErrorCode);
                }

                // 空白チェック
                if (cABErrLogXClass.p_strMsg6.Trim == string.Empty)                 // メッセージ６（住民コード）
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "住民コード】", cfErrorStruct.m_strErrorCode);
                }

                // 空白チェック
                if (cABErrLogXClass.p_strMsg7.Trim == string.Empty)                 // メッセージ７（エラーメッセージ）
                {
                    cfErrorClass = new UFErrorClass(URCommonXClass.GYOMUCD_REAMS);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(cABErrLogXClass.p_strMsg7.Trim + @"\n【" + cfErrorStruct.m_strErrorMessage + "エラーメッセージ】", cfErrorStruct.m_strErrorCode);
                }

                // InsertSQL文の雛形を作成
                CreateInsertSQL();

                // 空番取得クラスのインスタンス化
                cABAkibanShutokuBClass = new ABAkibanShutokuBClass(m_cfControlData, m_cfConfigDataClass);
                cABAkibanShutokuBClass.GetErrLogNo();

                // エラーログ番号を取得
                strErrLogNo = cABAkibanShutokuBClass.p_strBango;

                // ＤＢ日時の取得
                strSystemDateTime = cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // ＤＢ日時
                strSystemDate = cfRdb.GetSystemDate.ToString("yyyyMMdd");                         // ＤＢ日付
                strSystemTime = cfRdb.GetSystemDate.ToString("HHmmss");                           // ＤＢ時間

                // パラメータコレクションオブジェクトを作成
                m_cfInsParamCollection = new UFParameterCollectionClass();

                // 項目の編集
                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO;                   // ログ番号
                cfUFParameterClass.Value = strErrLogNo;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD;                  // 開始年月日
                cfUFParameterClass.Value = strSystemDate;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME;                 // 開始時間
                cfUFParameterClass.Value = strSystemTime;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID;                 // 処理ＩＤ
                cfUFParameterClass.Value = cABErrLogXClass.p_strShoriID.Trim;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU;                // 処理種別
                cfUFParameterClass.Value = cABErrLogXClass.p_strShoriShu.Trim;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB;                // 対処区分
                cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB;                 // 状況区分
                cfUFParameterClass.Value = JOKYOKBN_ERR;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU;          // 進捗率
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1;                    // ステータス１
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2;                    // ステータス２
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD;                  // 終了年月日
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME;                 // 終了時間
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1;                    // メッセージ１
                                                                                               // 文字数チェック
                if (cABErrLogXClass.p_strMsg1.RLength > 8)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.RSubstring(0, 8).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2;                    // メッセージ２
                if (cABErrLogXClass.p_strMsg2.RLength > 8)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.RSubstring(0, 8).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3;                    // メッセージ３
                if (cABErrLogXClass.p_strMsg3.RLength > 8)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.RSubstring(0, 8).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4;                    // メッセージ４
                if (cABErrLogXClass.p_strMsg4.RLength > 8)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.RSubstring(0, 8).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5;                    // メッセージ５
                if (cABErrLogXClass.p_strMsg5.RLength > 15)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.RSubstring(0, 15).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6;                    // メッセージ６
                if (cABErrLogXClass.p_strMsg6.RLength > 40)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.RSubstring(0, 40).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7;                    // メッセージ７
                if (cABErrLogXClass.p_strMsg7.RLength > 100)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.RSubstring(0, 100).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8;                    // メッセージ８
                if (cABErrLogXClass.p_strMsg8.RLength > 120)
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.RSubstring(0, 120).Trim;
                }
                else
                {
                    cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.Trim;
                }
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI;              // ログファイル名
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD;             // 市町村コード
                cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD;          // 旧市町村コード
                cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE;           // リザーブ
                cfUFParameterClass.Value = string.Empty;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID;              // 端末ＩＤ
                cfUFParameterClass.Value = m_cfControlData.m_strClientId;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG;                // 削除フラグ
                cfUFParameterClass.Value = "0";
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER;           // 更新カウンタ
                cfUFParameterClass.Value = decimal.Zero;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI;          // 作成日時
                cfUFParameterClass.Value = strSystemDateTime;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER;             // 作成ユーザー
                cfUFParameterClass.Value = m_cfControlData.m_strUserId;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI;           // 更新日時
                cfUFParameterClass.Value = strSystemDateTime;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // パラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER;              // 更新ユーザー
                cfUFParameterClass.Value = m_cfControlData.m_strUserId;
                // パラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:INSERT】" + "【SQL内容:" + cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsParamCollection) + "】");




                // SQL実行
                intInsCnt = cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsParamCollection);
            }

            catch (UFRdbException objRdbExp)                          // RdbExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objRdbExp.Message + "】");


                // ワーニングをスローする
                throw objRdbExp;
            }

            catch (UFRdbDeadLockException objRdbDeadLockExp)          // デッドロックをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbDeadLockExp.Message + "】");



                // ワーニングをスローする
                throw objRdbDeadLockExp;
            }

            catch (UFRdbUniqueException objUFRdbUniqueExp)            // 一意制約違反をキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】");



                // ワーニングをスローする
                throw objUFRdbUniqueExp;
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)            // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");



                // ワーニングをスローする
                throw objRdbTimeOutExp;
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException)                             // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;
            }
            finally
            {
                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:Disconnect】");



                // RDB切断
                cfRdb.Disconnect();

                // 元のビジネスIDを入れる
                m_cfControlData.m_strBusinessId = m_strRsBusinId;

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

            }

            // 戻り値設定
            return intInsCnt;

        }

        #endregion

        #region InsertSQL文の雛形を作成
        // ************************************************************************************************
        // * メソッド名      InsertSQL文の雛形を作成
        // * 
        // * 構文            Private Sub CreateInsertSQL()
        // * 
        // * 機能　　    　　InsertSQLの雛型とパラメータコレクションを作成する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void CreateInsertSQL()
        {

            const string THIS_METHOD_NAME = "CreateInsertSQL";
            UFParameterClass cfUFParameterClass;
            var strInsertColumn = new StringBuilder();
            var strInsertParam = new StringBuilder();
            var strInsertSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // InsertSQL文の作成
                strInsertSQL.Append("INSERT INTO ");
                strInsertSQL.Append(ABErrLogEntity.TABLE_NAME);
                strInsertSQL.Append(" ");

                // INSERTパラメータコレクションクラスのインスタンス化
                m_cfInsParamCollection = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                cfUFParameterClass = new UFParameterClass();

                // InsertSQL文の作成
                strInsertColumn.Append(ABErrLogEntity.LOGNO);                   // ログ番号
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.ST_YMD);                  // 開始年月日
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.ST_TIME);                 // 開始時間
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SHORIID);                 // 処理ＩＤ
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SHORISHU);                // 処理種別
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.TAISHOKB);                // 対処区分
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.JOKYOKB);                 // 状況区分
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SHINCHOKURITSU);          // 進捗率
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.STS1);                    // ステータス１
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.STS2);                    // ステータス２
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.ED_YMD);                  // 終了年月日
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.ED_TIME);                 // 終了時間
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG1);                    // メッセージ１
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG2);                    // メッセージ２
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG3);                    // メッセージ３
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG4);                    // メッセージ４
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG5);                    // メッセージ５
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG6);                    // メッセージ６
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG7);                    // メッセージ７
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.MSG8);                    // メッセージ８
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.LOGFILEMEI);              // ログファイル名
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SHICHOSONCD);             // 市町村コード
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.KYUSHICHOSONCD);          // 旧市町村コード
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.RESERVE30BYTE);           // リザーブ
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.TANMATSUID);              // 端末ＩＤ
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SAKUJOFG);                // 削除フラグ
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.KOSHINCOUNTER);           // 更新カウンタ
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SAKUSEINICHIJI);          // 作成日時
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.SAKUSEIUSER);             // 作成ユーザー
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.KOSHINNICHIJI);           // 更新日時
                strInsertColumn.Append(", ");
                strInsertColumn.Append(ABErrLogEntity.KOSHINUSER);              // 更新ユーザー

                strInsertParam.Append(ABErrLogEntity.KEY_LOGNO);                   // ログ番号
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_ST_YMD);                  // 開始年月日
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_ST_TIME);                 // 開始時間
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SHORIID);                 // 処理ＩＤ
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SHORISHU);                // 処理種別
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_TAISHOKB);                // 対処区分
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_JOKYOKB);                 // 状況区分
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SHINCHOKURITSU);          // 進捗率
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_STS1);                    // ステータス１
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_STS2);                    // ステータス２
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_ED_YMD);                  // 終了年月日
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_ED_TIME);                 // 終了時間
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG1);                    // メッセージ１
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG2);                    // メッセージ２
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG3);                    // メッセージ３
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG4);                    // メッセージ４
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG5);                    // メッセージ５
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG6);                    // メッセージ６
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG7);                    // メッセージ７
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_MSG8);                    // メッセージ８
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_LOGFILEMEI);              // ログファイル名
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SHICHOSONCD);             // 市町村コード
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_KYUSHICHOSONCD);          // 旧市町村コード
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_RESERVE30BYTE);           // リザーブ
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_TANMATSUID);              // 端末ＩＤ
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SAKUJOFG);                // 削除フラグ
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_KOSHINCOUNTER);           // 更新カウンタ
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEINICHIJI);          // 作成日時
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEIUSER);             // 作成ユーザー
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_KOSHINNICHIJI);           // 更新日時
                strInsertParam.Append(", ");
                strInsertParam.Append(ABErrLogEntity.KEY_KOSHINUSER);              // 更新ユーザー

                // INSERTコレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO;                   // ログ番号
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD;                  // 開始年月日
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME;                 // 開始時間
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID;                 // 処理ＩＤ
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU;                // 処理種別
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB;                // 対処区分
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB;                 // 状況区分
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU;          // 進捗率
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1;                    // ステータス１
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2;                    // ステータス２
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD;                  // 終了年月日
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME;                 // 終了時間
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1;                    // メッセージ１
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2;                    // メッセージ２
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3;                    // メッセージ３
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4;                    // メッセージ４
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5;                    // メッセージ５
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6;                    // メッセージ６
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7;                    // メッセージ７
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8;                    // メッセージ８
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI;              // ログファイル名
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD;             // 市町村コード
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD;          // 旧市町村コード
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE;           // リザーブ
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID;              // 端末ＩＤ
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG;                // 削除フラグ
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER;           // 更新カウンタ
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI;          // 作成日時
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER;             // 作成ユーザー
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI;           // 更新日時
                m_cfInsParamCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER;              // 更新ユーザー
                m_cfInsParamCollection.Add(cfUFParameterClass);

                // InsertSQL文の結合
                strInsertSQL.Append("(");
                strInsertSQL.Append(strInsertColumn);
                strInsertSQL.Append(")");
                strInsertSQL.Append(" VALUES (");
                strInsertSQL.Append(strInsertParam);
                strInsertSQL.Append(")");

                // String型に変換
                m_strInsertSQL = strInsertSQL.ToString();
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // エラーをそのままスローする
                throw exAppException;
            }

            catch (Exception exException)          // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // エラーをそのままスローする
                throw exException;
            }
            finally
            {
                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

            }

        }

        #endregion

    }
}
