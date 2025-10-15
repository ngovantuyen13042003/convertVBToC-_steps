// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        合併同一人ＤＡ(ABGappeiDoitsuninBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/15　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/17 000001     追加時、共通項目を設定する
// * 2003/04/25 000002     合併同一人グループ抽出メソッドを追加
// * 2003/05/13 000003     ＤＢよりスキーマを取得してSQLを作成
// * 2003/05/21 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/21 000005     合併同一人抽出(GetDoitsunin)メソッドを追加
// * 2007/07/27 000006     同一人代表者取得機能の追加 (吉澤)
// * 2010/04/16 000007     VS2008対応（比嘉）
// * 2016/01/07 000008     【AB00163】個人制御の同一人対応（石合）
// * 2018/05/01 000009     【AB27001】該当者一覧への同一人区分表示（石合）
// ************************************************************************************************
using System;
using System.Collections;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
// *履歴番号 000009 2018/05/01 追加開始
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{
    // *履歴番号 000009 2018/05/01 追加終了

    public class ABGappeiDoitsuninBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDeleteSQL;                        // DELETE用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;      // DELETE用パラメータコレクション

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABGappeiDoitsuninBClass";

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData As UFControlData,
        // * 　　                           ByVal cfConfigDataClass As UFConfigDataClass,
        // * 　　                           ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
        // * 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // * 　　            cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABGappeiDoitsuninBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // メンバ変数の初期化
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDeleteSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDeleteUFParameterCollectionClass = (object)null;
        }

        // ************************************************************************************************
        // * メソッド名     合併同一人全員抽出
        // * 
        // * 構文           Public Function GetDoitsuninAll(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　合併同一人より該当データを全件取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（DataSet）
        // *                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
        // ************************************************************************************************
        public DataSet GetDoitsuninAll(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninAll";            // このメソッド名
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            UFErrorStruct objErrorStruct;                             // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // 取得件数が０件の時
                if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 取得件数が１件の時
                if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 1)
                {
                    // SQL文の作成
                    strSQL = new StringBuilder();
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    // WHERE文結合
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");

                    // 検索条件のパラメータコレクションオブジェクトを作成
                    cfUFParameterCollectionClass = new UFParameterCollectionClass();
                    // 検索条件のパラメータを作成
                    // 同一人識別コード
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD;
                    cfUFParameterClass.Value = (string)csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                    // SQLの実行 DataSetの取得
                    csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csGappeiDoitsuninEntity;

        }

        // * 履歴番号 000005 2003/08/21 追加開始
        // ************************************************************************************************
        // * メソッド名     合併同一人抽出
        // * 
        // * 構文           Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　合併同一人より該当データを全件取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（DataSet）
        // *                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
        // ************************************************************************************************
        public DataSet GetDoitsunin(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsunin";               // このメソッド名
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
                                                                      // * corresponds to VS2008 Start 2010/04/16 000007
                                                                      // Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
                                                                      // * corresponds to VS2008 End 2010/04/16 000007

            do
            {
                try
                {
                    // デバッグログ出力
                    m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                    // SQL文の作成
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    // WHERE文結合
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");

                    // 検索条件のパラメータコレクションオブジェクトを作成
                    cfUFParameterCollectionClass = new UFParameterCollectionClass();
                    // 検索条件のパラメータを作成
                    // 住民コード
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = strJuminCD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                    // SQLの実行 DataSetの取得
                    csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);

                    // 取得件数が０件の時
                    if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 0)
                    {
                        break;
                    }

                    // 取得件数が１件の時
                    if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 1)
                    {
                        // SQL文の作成
                        strSQL = new StringBuilder();
                        strSQL.Append("SELECT * FROM ");
                        strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                        // WHERE文結合
                        strSQL.Append(" WHERE ");
                        strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                        strSQL.Append(" = ");
                        strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD);
                        strSQL.Append(" AND ");
                        strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                        strSQL.Append(" <> 1");

                        // 検索条件のパラメータコレクションオブジェクトを作成
                        cfUFParameterCollectionClass = new UFParameterCollectionClass();
                        // 検索条件のパラメータを作成
                        // 同一人識別コード
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD;
                        cfUFParameterClass.Value = (string)csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString;
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // RDBアクセスログ出力
                        m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                        // SQLの実行 DataSetの取得
                        csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);
                    }
                }

                catch (UFAppException exAppException)
                {
                    // ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                    // ワーニングをスローする
                    throw exAppException;
                }

                catch (Exception exException) // システムエラーをキャッチ
                {
                    // エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                    // システムエラーをスローする
                    throw exException;
                }
                finally
                {
                    // デバッグログ出力
                    m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
                }
            }
            while (false);

            return csGappeiDoitsuninEntity;

        }
        // * 履歴番号 000005 2003/08/21 追加終了

        // *履歴番号 000008 2016/01/07 追加開始
        /// <summary>
    /// 同一人データ取得
    /// </summary>
    /// <param name="a_strJuminCD">住民コード文字列配列</param>
    /// <returns>同一人データ</returns>
    /// <remarks></remarks>
        public DataSet GetDoitsunin(string[] a_strJuminCD)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            StringBuilder csSQL;
            UFParameterClass cfParameter;
            UFParameterCollectionClass cfParameterCollection;
            string strParameterName;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                csSQL = new StringBuilder();
                cfParameterCollection = new UFParameterCollectionClass();

                {
                    ref var withBlock = ref csSQL;

                    withBlock.Append("SELECT * FROM ");
                    withBlock.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    withBlock.Append(" WHERE ");
                    withBlock.Append(ABGappeiDoitsuninEntity.JUMINCD);
                    withBlock.Append(" IN (");

                    for (int i = 0, loopTo = a_strJuminCD.Length - 1; i <= loopTo; i++)
                    {

                        // -----------------------------------------------------------------------------
                        // 住民コード
                        strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString();

                        if (i > 0)
                        {
                            withBlock.AppendFormat(", {0}", strParameterName);
                        }
                        else
                        {
                            withBlock.Append(strParameterName);
                        }

                        cfParameter = new UFParameterClass();
                        cfParameter.ParameterName = strParameterName;
                        cfParameter.Value = a_strJuminCD[i];
                        cfParameterCollection.Add(cfParameter);
                        // -----------------------------------------------------------------------------

                    }

                    withBlock.Append(")");
                    withBlock.Append(" AND ");
                    withBlock.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    withBlock.Append(" <> '1'");

                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + csSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection);

                // 取得件数が１件以上の時
                if (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0)
                {

                    // SQL文の作成
                    csSQL = new StringBuilder();
                    cfParameterCollection = new UFParameterCollectionClass();


                    csSQL.Append("SELECT * FROM ");
                    csSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    csSQL.Append(" WHERE ");
                    csSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    csSQL.Append(" IN (");

                    for (int i = 0, loopTo1 = csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count - 1; i <= loopTo1; i++)
                    {

                        // -----------------------------------------------------------------------------
                        // 同一人識別コード
                        strParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD + i.ToString();

                        if (i > 0)
                        {
                            csSQL.AppendFormat(", {0}", strParameterName);
                        }
                        else
                        {
                            csSQL.Append(strParameterName);
                        }

                        cfParameter = new UFParameterClass();
                        cfParameter.ParameterName = strParameterName;
                        cfParameter.Value = csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(i).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString;
                        cfParameterCollection.Add(cfParameter);
                        // -----------------------------------------------------------------------------

                    }

                    csSQL.Append(")");
                    csSQL.Append(" AND ");
                    csSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    csSQL.Append(" <> '1'");
                    csSQL.Append(" ORDER BY ");
                    csSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    csSQL.Append(", ");
                    csSQL.Append(ABGappeiDoitsuninEntity.HONNINKB);
                    csSQL.Append(", ");

                    csSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + csSQL.ToString() + "】");




                    // SQLの実行 DataSetの取得
                    csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection);
                }

                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException csAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + csAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return csDataSet;

        }
        // *履歴番号 000008 2016/01/07 追加終了

        // *履歴番号 000009 2018/05/01 追加開始
        /// <summary>
    /// 同一人区分名称取得
    /// </summary>
    /// <param name="csJuminCDList">住民コードリスト</param>
    /// <returns>同一人区分名称</returns>
    /// <remarks></remarks>
        public Hashtable GetDoitsuninMeisho(List<string> csJuminCDList)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Hashtable csResult;
            DataSet csDataSet;
            StringBuilder csSQL;
            UFParameterClass cfParameter;
            UFParameterCollectionClass cfParameterCollection;
            string strParameterName;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 返信オブジェクトのインスタンス化
                csResult = new Hashtable();

                // SQL文の作成
                csSQL = new StringBuilder();
                cfParameterCollection = new UFParameterCollectionClass();


                csSQL.Append("SELECT * FROM ");
                csSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                csSQL.Append(" WHERE ");
                csSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                csSQL.Append(" IN (");

                for (int i = 0, loopTo = csJuminCDList.Count - 1; i <= loopTo; i++)
                {

                    // -----------------------------------------------------------------------------
                    // 住民コード
                    strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString();

                    if (i > 0)
                    {
                        csSQL.AppendFormat(", {0}", strParameterName);
                    }
                    else
                    {
                        csSQL.Append(strParameterName);
                    }

                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = strParameterName;
                    cfParameter.Value = csJuminCDList[i];
                    cfParameterCollection.Add(cfParameter);
                    // -----------------------------------------------------------------------------

                }

                csSQL.Append(")");
                csSQL.Append(" AND ");
                csSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);

                csSQL.Append(" <> '1'");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + csSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection);

                // 取得件数が１件以上の時
                if (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows)
                    {

                        // -----------------------------------------------------------------------------
                        // 同一人区分名称編集
                        switch (csDataRow.Item(ABGappeiDoitsuninEntity.HONNINKB).ToString.Trim)
                        {
                            case var @case when @case == ABConstClass.HONNINKB.CODE.DAIHYO:
                                {
                                    csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DAIHYO);
                                    break;
                                }
                            case var case1 when case1 == ABConstClass.HONNINKB.CODE.DOITSUNIN:
                                {
                                    csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DOITSUNIN);
                                    break;
                                }
                            case var case2 when case2 == ABConstClass.HONNINKB.CODE.HAISHI:
                                {
                                    csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.HAISHI);
                                    break;
                                }

                            default:
                                {
                                    csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, string.Empty);
                                    break;
                                }
                        }
                        // -----------------------------------------------------------------------------

                    }
                }

                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException csAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + csAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + csAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return csResult;

        }
        // *履歴番号 000009 2018/05/01 追加終了

        // *履歴番号 000006 2007/07/27 追加開始
        // ************************************************************************************************
        // * メソッド名     合併同一人代表住民コード取得
        // * 
        // * 構文           Public Function GetDoitsuninDaihyoJuminCD(ByVal strJuminCD As String) As String
        // * 
        // * 機能　　    　 合併同一人代表の住民コードを取得する
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（String）
        // ************************************************************************************************
        public string GetDoitsuninDaihyoJuminCD(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninDaihyoJuminCD";         // このメソッド名
            string strDaihyoJuminCD;                      // 住民コード（代表者）
            DataSet csDaihyosyaEntity;              // 同一人代表者データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 代表者情報の取得を行う
                // SQL文の作成
                strSQL.Append("SELECT A.");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(" A ");
                // JOIN文結合
                strSQL.Append("JOIN (SELECT ");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strSQL.Append(" FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB);
                strSQL.Append(" IN ('0','1')");
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> 1) B ON A.");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strSQL.Append(" = B.");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                // WHERE文結合
                strSQL.Append(" WHERE A.");
                strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB);
                strSQL.Append(" = '0'");


                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csDaihyosyaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // 取得件数が０件の時
                if (csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 0)
                {
                    // 同一人管理されていない場合は、指定された住民コードを返却する
                    strDaihyoJuminCD = strJuminCD;
                }
                else
                {
                    // 同一人管理されている場合は、同一人代表者の住民コードを返却する
                    strDaihyoJuminCD = (string)csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.JUMINCD);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return strDaihyoJuminCD;

        }
        // *履歴番号 000006 2007/07/27 追加終了

        // ************************************************************************************************
        // * メソッド名     合併同一人本人抽出
        // * 
        // * 構文           Public Function GetDoitsuninHonnin(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　合併同一人より該当データを取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（DataSet）
        // *                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
        // ************************************************************************************************
        public DataSet GetDoitsuninHonnin(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninHonnin";         // このメソッド名
            UFErrorStruct objErrorStruct;                             // エラー定義構造体
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // 取得件数が０件の時
                if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 取得件数が１件の時
                if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 1)
                {
                    // SQL文の作成
                    strSQL = new StringBuilder();
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                    // WHERE文結合
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB);
                    strSQL.Append(" = '0'");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");

                    // 検索条件のパラメータコレクションオブジェクトを作成
                    cfUFParameterCollectionClass = new UFParameterCollectionClass();
                    // 検索条件のパラメータを作成
                    // 同一人識別コード
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD;
                    cfUFParameterClass.Value = (string)csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                    // SQLの実行 DataSetの取得
                    csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csGappeiDoitsuninEntity;

        }

        // ************************************************************************************************
        // * メソッド名     合併同一人グループ抽出
        // * 
        // * 構文           Public Function GetDoitsuninGroup(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　合併同一人より該当データを全件取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         識別コード(String)         
        // ************************************************************************************************
        public string GetDoitsuninGroup(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninGroup";          // このメソッド名
            UFErrorStruct objErrorStruct;                             // エラー定義構造体
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            string strShikibetsuCD;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // 取得件数が０件の時
                if (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                strShikibetsuCD = (string)csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);


                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return strShikibetsuCD;

        }

        // ************************************************************************************************
        // * メソッド名     合併同一人追加
        // * 
        // * 構文           Public Function InsertDoitsunin(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  合併同一人にデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertDoitsunin(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertDoitsunin";            // このメソッド名
                                                                          // パラメータクラス
            int intInsCnt;                            // 追加件数
            string strUpdateDateTime;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId;           // 端末ＩＤ
                csDataRow(ABGappeiDoitsuninEntity.SAKUJOFG) = "0";                                       // 削除フラグ
                csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = decimal.Zero;                         // 更新カウンタ
                csDataRow(ABGappeiDoitsuninEntity.SAKUSEINICHIJI) = strUpdateDateTime;                   // 作成日時
                csDataRow(ABGappeiDoitsuninEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;            // 作成ユーザー
                csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = strUpdateDateTime;                    // 更新日時
                csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId;             // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_strInsertSQL + "】");




                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     合併同一人更新
        // * 
        // * 構文           Public Function UpdateDoitsunin(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  合併同一人のデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateDoitsunin(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateDoitsunin";            // このメソッド名
                                                                          // パラメータクラス
                                                                          // * corresponds to VS2008 Start 2010/04/16 000007
                                                                          // Dim csDataColumn As DataColumn
                                                                          // * corresponds to VS2008 End 2010/04/16 000007
            int intUpdCnt;                                        // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) + 1m;   // 更新カウンタ
                csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) == ABGappeiDoitsuninEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim);
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_strUpdateSQL + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw exException;

            }

            return intUpdCnt;

        }

        // ************************************************************************************************
        // * メソッド名     合併同一人削除（物理）
        // * 
        // * 構文           Public Function DeleteDoitsunin(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  合併同一人のデータを削除（物理）する。
        // * 
        // * 引数           csDataRow As DataRow      :削除データ
        // * 
        // * 戻り値         削除（物理）件数(Integer)
        // ************************************************************************************************
        public int DeleteDoitsunin(DataRow csDataRow)
        {
            // * corresponds to VS2008 Start 2010/04/16 000007
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000007
            const string THIS_METHOD_NAME = "DeleteDoitsunin（物理）";                     // パラメータクラス
                                                                                       // * corresponds to VS2008 Start 2010/04/16 000007
                                                                                       // Dim csDataColumn As DataColumn
                                                                                       // * corresponds to VS2008 End 2010/04/16 000007
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null | string.IsNullOrEmpty(m_strDeleteSQL) | m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) == ABGappeiDoitsuninEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_strDeleteSQL + "】");




                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw exException;

            }

            return intDelCnt;

        }

        // ************************************************************************************************
        // * メソッド名     SQL文の作成
        // * 
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;          // パラメータクラス
            string strInsertColumn;                       // 追加SQL文項目文字列
            string strInsertParam;                        // 追加SQL文パラメータ文字列
            var strDeleteSQL = new StringBuilder();               // 削除SQL文文字列
            var strWhere = new StringBuilder();                   // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABGappeiDoitsuninEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABGappeiDoitsuninEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABGappeiDoitsuninEntity.TABLE_NAME + " SET ";

                // DELETE（物理） SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strDeleteSQL.Append(strWhere.ToString());
                m_strDeleteSQL = strDeleteSQL.ToString();

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（物理） パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // ' カラムが存在する場合
                    // If (m_csSchema.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Columns.Contains(csDataColumn.ColumnName)) Then

                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    // End If

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");
                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += strWhere.ToString();

                // UPDATE,DELETE(物理) コレクションにキー情報を追加
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 同一人識別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }
        }

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       合併同一のデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String
        // *                strValue As String
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABGappeiDoitsuninEntity.JUMINCD:                    // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABGappeiDoitsuninEntity.SHICHOSONCD:                // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABGappeiDoitsuninEntity.KYUSHICHOSONCD:             // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD:      // 同一人識別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_DOITSUNINSHIKIBETSUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABGappeiDoitsuninEntity.HONNINKB:                   // 本人区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HONNINKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABGappeiDoitsuninEntity.HANYOKB1:                   // 汎用区分1
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABGappeiDoitsuninEntity.HANYOKB2:                   // 汎用区分2
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABGappeiDoitsuninEntity.BIKO:                       // 備考
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_BIKO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABGappeiDoitsuninEntity.RESERVE:                    // リザーブ
                        {
                            break;
                        }
                    // 何もしない
                    case var case9 when case9 == ABGappeiDoitsuninEntity.TANMATSUID:                 // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABGappeiDoitsuninEntity.SAKUJOFG:                   // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case11 when case11 == ABGappeiDoitsuninEntity.KOSHINCOUNTER:              // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case12 when case12 == ABGappeiDoitsuninEntity.SAKUSEINICHIJI:             // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABGappeiDoitsuninEntity.SAKUSEIUSER:                // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABGappeiDoitsuninEntity.KOSHINNICHIJI:              // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case15 when case15 == ABGappeiDoitsuninEntity.KOSHINUSER:                 // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }
            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;
            }

        }

    }
}
