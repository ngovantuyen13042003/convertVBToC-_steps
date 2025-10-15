// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        連絡先マスタＤＡ(ABRenrakusakiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/14　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/17 000001     追加時、共通項目を設定する
// * 2003/05/21 000002     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000003     RDBアクセスログの修正
// * 2004/08/27 000004     速度改善：（宮沢）
// * 2010/04/16 000005     VS2008対応（比嘉）
// * 2023/07/13 000006     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2024/02/06 000007     【AB-0860-1】連絡先管理項目追加(下村)
// * 2024/03/07 000008     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABRenrakusakiBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDeleteSQL;                                                // DELETE用SQL（物理）
        private string m_strDelRonriSQL;                                              // DELETE用SQL（論理）
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;      // DELETE用パラメータコレクション（物理）
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // DELETE用パラメータコレクション（論理）
                                                                                      // * 履歴番号 000004 2004/08/27 追加開始（宮沢）
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
                                         // * 履歴番号 000004 2004/08/27 追加終了
                                         // *履歴番号 000006 2023/07/13 追加開始
        private DataSet m_csDataSchma_Hyojun;   // 標準化版スキーマ保管用データセット
                                                // *履歴番号 000006 2023/07/13 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABRenrakusakiBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABRenrakusakiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_strDelRonriSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDeleteUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;

            // SQL文の作成
            // * 履歴番号 000004 2004/08/27 追加開始（宮沢）
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TABLE_NAME, false);
            // * 履歴番号 000004 2004/08/27 追加終了
            // *履歴番号 000006 2023/07/13 追加開始
            m_csDataSchma_Hyojun = GetRenrakusakiSchemaBHoshu_Hyojun();
            // *履歴番号 000006 2023/07/13 追加終了

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     連絡先マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　連絡先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String  :住民コード
        // * 
        // * 戻り値         取得した連絡先マスタの該当データ（DataSet）
        // *                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
        // ************************************************************************************************
        public DataSet GetRenrakusakiBHoshu(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiBHoshu";       // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD);
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUCD);
                strSQL.Append(" ASC, ");
                strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN);
                strSQL.Append(" ASC");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + strSQL.ToString() + "】");




                // SQLの実行 DataSetの取得
                // * 履歴番号 000004 2004/08/27 更新開始（宮沢）
                // csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csRenrakusakiEntity = m_csDataSchma.Clone();
                csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // * 履歴番号 000004 2004/08/27 更新終了
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

            return csRenrakusakiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
        // *                                                        ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　連絡先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String    :住民コード
        // *                blnSakujoFG As Boolean  :削除フラグ
        // * 
        // * 戻り値         取得した連絡先マスタの該当データ（DataSet）
        // *                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
        // ************************************************************************************************
        public DataSet GetRenrakusakiBHoshu(string strJuminCD, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiBHoshu";       // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABRenrakusakiEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                // ORDER文結合
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUCD);
                strSQL.Append(" ASC, ");
                strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN);
                strSQL.Append(" ASC");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行 DataSetの取得

                // * 履歴番号 000004 2004/08/27 更新開始（宮沢）
                // csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csRenrakusakiEntity = m_csDataSchma.Clone();
                csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // * 履歴番号 000004 2004/08/27 更新終了

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

            return csRenrakusakiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
        // *                                                        ByVal strGyomuCD As String, 
        // *                                                        ByVal strGyomunaiSHUCD As String) As DataSet
        // * 
        // * 機能　　    　　連絡先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String          :住民コード
        // *                strGyomuCD As String          :業務コード
        // *                strGyomunaiSHUCD As String    :業務内種別コード
        // * 
        // * 戻り値         取得した連絡先マスタの該当データ（DataSet）
        // *                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
        // ************************************************************************************************
        public DataSet GetRenrakusakiBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiBHoshu";       // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnSakujo = true;                                 // 削除データ読み込み

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiEntity.JUMINCD);          // 住民コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUCD);          // 業務コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD);   // 業務内種別コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = strGyomuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = strGyomunaiSHUCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000004 2004/08/27 更新開始（宮沢）
                // csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csRenrakusakiEntity = m_csDataSchma.Clone();
                csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // * 履歴番号 000004 2004/08/27 更新終了

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

            return csRenrakusakiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
        // *                                                        ByVal strGyomuCD As String, 
        // *                                                        ByVal strGyomunaiSHUCD As String, 
        // *                                                        ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　連絡先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String          :住民コード
        // *                strGyomuCD As String          :業務コード
        // *                strGyomunaiSHUCD As String    :業務内種別コード
        // *                blnSakujoFG As Boolean        :削除フラグ
        // * 
        // * 戻り値         取得した連絡先マスタの該当データ（DataSet）
        // *                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
        // ************************************************************************************************
        public DataSet GetRenrakusakiBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiBHoshu";       // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnSakujo = true;                                 // 削除データ読み込み

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABRenrakusakiEntity.JUMINCD);          // 住民コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUCD);          // 業務コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD);   // 業務内種別コード
                strSQL.Append(" = ");
                strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABRenrakusakiEntity.SAKUJOFG);     // 削除フラグ
                    strSQL.Append(" <> 1");
                }

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = strGyomuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = strGyomunaiSHUCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000004 2004/08/27 更新開始（宮沢）
                // csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csRenrakusakiEntity = m_csDataSchma.Clone();
                csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // * 履歴番号 000004 2004/08/27 更新終了

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

            return csRenrakusakiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先マスタ追加
        // * 
        // * 構文           Public Function InsertRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  連絡先マスタにデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertRenrakusakiB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertRenrakusakiB";         // このメソッド名
                                                                          // パラメータクラス
            int intInsCnt;                                        // 追加件数
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
                csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId;               // 端末ＩＤ
                csDataRow(ABRenrakusakiEntity.SAKUJOFG) = "0";                                           // 削除フラグ
                csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = decimal.Zero;                             // 更新カウンタ
                csDataRow(ABRenrakusakiEntity.SAKUSEINICHIJI) = strUpdateDateTime;                       // 作成日時
                csDataRow(ABRenrakusakiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;                // 作成ユーザー
                csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = strUpdateDateTime;                        // 更新日時
                csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                 // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

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
        // * メソッド名     連絡先マスタ更新
        // * 
        // * 構文           Public Function UpdateRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  連絡先マスタのデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateRenrakusakiB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateRenrakusakiB";         // このメソッド名
                                                                          // パラメータクラス
                                                                          // * corresponds to VS2008 Start 2010/04/16 000005
                                                                          // Dim csDataColumn As DataColumn
                                                                          // * corresponds to VS2008 End 2010/04/16 000005
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
                csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId; // 端末ＩＤ
                csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) + 1m;   // 更新カウンタ
                csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;   // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) == ABRenrakusakiEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim);
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

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
        // * メソッド名     連絡先マスタ削除（論理）
        // * 
        // * 構文           Public Function DeleteRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  連絡先マスタのデータを削除（論理）する。
        // * 
        // * 引数           csDataRow As DataRow  :削除データ
        // * 
        // * 戻り値         削除（論理）件数(Integer)
        // ************************************************************************************************
        public int DeleteRenrakusakiB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteRenrakusakiB（論理）";  // このメソッド名
                                                                       // パラメータクラス
                                                                       // * corresponds to VS2008 Start 2010/04/16 000005
                                                                       // Dim csDataColumn As DataColumn
                                                                       // * corresponds to VS2008 End 2010/04/16 000005
            int intDelCnt;                                        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null | string.IsNullOrEmpty(m_strDelRonriSQL) | m_cfDelRonriUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId; // 端末ＩＤ
                csDataRow(ABRenrakusakiEntity.SAKUJOFG) = 1;                                 // 削除フラグ
                csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) + 1m;   // 更新カウンタ
                csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;   // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) == ABRenrakusakiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strDelRonriSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);

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
        // * メソッド名     連絡先マスタ削除（物理）
        // * 
        // * 構文           Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow, 
        // *                                                      ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　  連絡先マスタのデータを削除（物理）する。
        // * 
        // * 引数           csDataRow As DataRow      :削除データ
        // *                strSakujoKB As String     :削除フラグ
        // * 
        // * 戻り値         削除（物理）件数(Integer)
        // ************************************************************************************************
        public int DeleteRenrakusakiB(DataRow csDataRow, string strSakujoKB)
        {
            const string THIS_METHOD_NAME = "DeleteRenrakusakiB（物理）";  // このメソッド名
            UFErrorStruct objErrorStruct;                             // エラー定義構造体
                                                                      // パラメータクラス
                                                                      // * corresponds to VS2008 Start 2010/04/16 000005
                                                                      // Dim csDataColumn As DataColumn
                                                                      // * corresponds to VS2008 End 2010/04/16 000005
            int intDelCnt;                                        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数の削除区分をチェック
                if (strSakujoKB != "D")
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null | string.IsNullOrEmpty(m_strDeleteSQL) | m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) == ABRenrakusakiEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000003 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strDeleteSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");



                // *履歴番号 000003 2003/08/28 修正終了

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
            const string THIS_METHOD_NAME = "CreateSQL";              // このメソッド名
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            string strInsertColumn;                               // 追加SQL文項目文字列
            string strInsertParam;                                // 追加SQL文パラメータ文字列
            var strDelRonriSQL = new StringBuilder();                   // 論理削除SQL文文字列
            var strDeleteSQL = new StringBuilder();                     // 物理削除SQL文文字列
            var strWhere = new StringBuilder();                         // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABRenrakusakiEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABRenrakusakiEntity.JUMINCD);
                strWhere.Append(" = ");
                strWhere.Append(ABRenrakusakiEntity.KEY_JUMINCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABRenrakusakiEntity.GYOMUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABRenrakusakiEntity.KEY_GYOMUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD);
                strWhere.Append(" = ");
                strWhere.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD);
                strWhere.Append(" AND ");
                strWhere.Append(ABRenrakusakiEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABRenrakusakiEntity.KEY_KOSHINCOUNTER);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABRenrakusakiEntity.TABLE_NAME + " SET ";

                // DELETE（論理） SQL文の作成
                strDelRonriSQL.Append("UPDATE ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                strDelRonriSQL.Append(" SET ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.TANMATSUID);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_TANMATSUID);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.SAKUJOFG);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_SAKUJOFG);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINCOUNTER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINCOUNTER);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINNICHIJI);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINNICHIJI);
                strDelRonriSQL.Append(", ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINUSER);
                strDelRonriSQL.Append(" = ");
                strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINUSER);
                strDelRonriSQL.Append(strWhere.ToString());
                m_strDelRonriSQL = strDelRonriSQL.ToString();

                // DELETE（物理） SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABRenrakusakiEntity.TABLE_NAME);
                strDeleteSQL.Append(strWhere.ToString());
                m_strDeleteSQL = strDeleteSQL.ToString();

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（論理） パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（物理） パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

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
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // DELETE（論理） コレクションにパラメータを追加
                // 端末ＩＤ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 削除フラグ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新日時
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新ユーザ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * 機能　　       連絡先マスタのデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String
        // *                strValue As String
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";       // このメソッド名
            UFErrorStruct objErrorStruct;                         // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABRenrakusakiEntity.JUMINCD:                        // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABRenrakusakiEntity.SHICHOSONCD:                    // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABRenrakusakiEntity.KYUSHICHOSONCD:                 // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABRenrakusakiEntity.GYOMUCD:                        // 業務コード
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABRenrakusakiEntity.GYOMUNAISHU_CD:                 // 業務内種別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUNAISHU_CD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABRenrakusakiEntity.RENRAKUSAKI1:                   // 連絡先1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABRenrakusakiEntity.RENRAKUSAKI2:                   // 連絡先2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABRenrakusakiEntity.RESERVE:                        // リザーブ
                        {
                            break;
                        }
                    // 何もしない
                    case var case8 when case8 == ABRenrakusakiEntity.TANMATSUID:                     // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case9 when case9 == ABRenrakusakiEntity.SAKUJOFG:                       // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case10 when case10 == ABRenrakusakiEntity.KOSHINCOUNTER:                  // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case11 when case11 == ABRenrakusakiEntity.SAKUSEINICHIJI:                 // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case12 when case12 == ABRenrakusakiEntity.SAKUSEIUSER:                    // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABRenrakusakiEntity.KOSHINNICHIJI:                  // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABRenrakusakiEntity.KOSHINUSER:                     // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINUSER);
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

        // *履歴番号 000006 2023/07/13 追加開始
        // ************************************************************************************************
        // * メソッド名     連絡先マスタ抽出_標準版
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiBHoshu_Hyojun(ByVal strJuminCD As String, 
        // *                                                        ByVal strGyomuCD As String, 
        // *                                                        ByVal strGyomunaiSHUCD As String,
        // *                                                        ByVal strKikanYMD As String) As DataSet
        // * 
        // * 機能　　    　　連絡先マスタより該当データを取得する。
        // * 
        // * 引数           strJuminCD As String          :住民コード
        // *                strGyomuCD As String          :業務コード
        // *                strGyomunaiSHUCD As String    :業務内種別コード
        // *                strKikanYMD As String         :期間年月日
        // * 
        // * 戻り値         取得した連絡先マスタの該当データ（DataSet）
        // *                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
        // ************************************************************************************************
        public DataSet GetRenrakusakiBHoshu_Hyojun(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, string strKikanYMD)
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiBHoshu_Hyojun"; // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnSakujo = true;                                 // 削除データ読み込み

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.AppendFormat("SELECT {0}.* ", ABRenrakusakiEntity.TABLE_NAME);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN);
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO);
                strSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME);

                // JOIN文結合
                strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);
                strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME);
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN);

                // WHERE文結合
                strSQL.AppendFormat(" WHERE {0}.{1} = {2}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD);
                strSQL.AppendFormat(" AND {0}.{1} = {2}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD);
                strSQL.AppendFormat(" AND {0}.{1} <= {2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_STYMD);
                strSQL.AppendFormat(" AND {0}.{1} >= {2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_EDYMD);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = strGyomuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務内種別コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = strGyomunaiSHUCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 連絡先開始日
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_STYMD;
                cfUFParameterClass.Value = strKikanYMD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 連絡先終了日
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_EDYMD;
                cfUFParameterClass.Value = strKikanYMD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");

                csRenrakusakiEntity = m_csDataSchma_Hyojun.Clone();
                csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

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

            return csRenrakusakiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     連絡先マスタスキーマ取得_標準版
        // * 
        // * 構文           Public Overloads Function GetRenrakusakiSchemaBHoshu_Hyojun() As DataSet
        // * 
        // * 機能　　    　 連絡先マスタよりスキーマを取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet : 取得した送付先マスタのスキーマ
        // ************************************************************************************************
        public DataSet GetRenrakusakiSchemaBHoshu_Hyojun()
        {
            const string THIS_METHOD_NAME = "GetRenrakusakiSchemaBHoshu_Hyojun"; // このメソッド名
            DataSet csRenrakusakiEntity;                              // 連絡先マスタデータ
            var strSQL = new StringBuilder();                               // SQL文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (m_csDataSchma_Hyojun is null)
                {
                    // SQL文の作成
                    strSQL.AppendFormat("SELECT {0}.* ", ABRenrakusakiEntity.TABLE_NAME);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN);
                    strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO);
                    strSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME);

                    // JOIN文結合
                    strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);
                    strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);
                    strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME);
                    strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD);
                    strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN);

                    csRenrakusakiEntity = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABRenrakusakiEntity.TABLE_NAME, false);
                }
                else
                {
                    csRenrakusakiEntity = m_csDataSchma_Hyojun.Clone;
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

            return csRenrakusakiEntity;

        }
        // *履歴番号 000006 2023/07/13 追加終了
        #endregion

    }
}
