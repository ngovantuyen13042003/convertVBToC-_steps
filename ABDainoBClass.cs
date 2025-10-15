// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ代納マスタＤＡ(ABDainoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/06　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/02/25 000001     抽出条件から業務内種別コードをはずすとあるが、業務内種別コードを String.Emptyとして取得する
// * 2003/03/27 000002     エラー処理クラスの参照先を"AB"固定にする
// * 2003/04/21 000003     整合性チェック変更(業務内種別・開始年月・終了年月)
// * 2003/05/06 000004     整合性チェック変更
// * 2003/05/20 000005     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000006     RDBアクセスログの修正
// * 2003/09/11 000007     端末ＩＤ整合性チェックをANKにする
// * 2003/10/09 000008     作成ユーザー・更新ユーザーチェックの変更
// * 2004/08/27 000009     速度改善：（宮沢）
// * 2005/01/25 000010     速度改善２：（宮沢）
// * 2005/06/16 000011     SQL文をInsert,Update,Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
// * 2006/12/22 000012     本店情報取得メソッドを追加。
// * 2007/03/09 000013     代納情報取得SQLのソート順を変更(高原)
// * 2010/03/05 000014     代納マスタ抽出処理のオーバーロードを追加（比嘉）
// * 2010/04/16 000015     VS2008対応（比嘉）
// * 2023/03/10 000016     【AB-0970-1】宛名GET取得項目標準化対応（仲西）
// * 2023/04/20 000017     【AB-0970-1】宛名GET取得項目標準化対応_暫定対応（仲西）
// * 2023/10/19 000018     【AB-0840-1】送付先管理項目追加対応（見城）
// * 2023/12/05 000019     【AB-0840-1】送付先管理項目追加対応_追加修正（仲西）
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 代納マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABDainoBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfParameterCollectionClass;            // 読込用パラメータコレクション
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABDainoBClass";                       // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード

        // * 履歴番号 000009 2004/08/27 追加開始（宮沢）
        public bool m_blnBatch = false;               // バッチフラグ
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
                                         // * 履歴番号 000009 2004/08/27 追加終了
                                         // * 履歴番号 000018 2023/10/19 修正開始
        private const string ALL0_YMD = "00000000";            // 年月日オール０
        private const string ALL9_YMD = "99999999";            // 年月日オール９
                                                               // * 履歴番号 000018 2023/10/19 修正終了

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // *                               ByVal cfConfigDataClass As UFConfigDataClass, 
        // *                               ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABDainoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数初期化
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDelRonriSQL = string.Empty;
            m_strDelButuriSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;
            m_cfDelButuriUFParameterCollectionClass = (object)null;
            m_cfParameterCollectionClass = (object)null;
            // * 履歴番号 000009 2004/08/27 追加開始（宮沢）
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoEntity.TABLE_NAME, ABDainoEntity.TABLE_NAME, false);
            // * 履歴番号 000009 2004/08/27 追加終了
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     代納マスタ抽出
        // * 
        // * 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetDainoBHoshu(string strJuminCD)
        {
            return GetDainoBHoshu(strJuminCD, false);
        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ抽出
        // * 
        // * 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
        // *                                               ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // *                blnSakujoFG  : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetDainoBHoshu(string strJuminCD, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetDainoBHoshu";
            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000015
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csDataSet;                            // データセット
            var strSQL = new StringBuilder("");

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // なし

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成    
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_JUMINCD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABDainoEntity.GYOMUCD);
                strSQL.Append(" ASC, ");
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                strSQL.Append(" ASC");
                // *履歴番号 000013 2007/03/09 追加開始
                strSQL.Append(", ");
                strSQL.Append(ABDainoEntity.STYMD);
                strSQL.Append(" ASC");
                // *履歴番号 000013 2007/03/09 追加終了
                strSQL.Append(";");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                if (m_blnBatch == false)
                {
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csDataSet = m_csDataSchma.Clone();
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                // * 履歴番号 000009 2004/08/27 更新終了


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

            return csDataSet;

        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ抽出
        // * 
        // * 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
        // *                                               ByVal strGyomuCD As String,
        // *                                               ByVal strGyomunaiSHUCD As String,
        // *                                               ByVal strKikanYMD As String) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strGyomuCD        : 業務コード
        // *                strGyomunaiSHUCD  : 業務内種別コード
        // *                strKikanYM        : 期間年月日
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetDainoBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, string strKikanYMD)
        {
            return GetDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, false);
        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ抽出
        // * 
        // * 構文           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
        // *                                               ByVal strGyomuCD As String,
        // *                                               ByVal strGyomunaiSHUCD As String,
        // *                                               ByVal strKikanYMD As String,
        // *                                               ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strGyomuCD        : 業務コード
        // *                strGyomunaiSHUCD  : 業務内種別コード
        // *                strKikanYMD       : 期間年月日
        // *                blnSakujoFG       : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetDainoBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, string strKikanYMD, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetDainoBHoshu";
            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000015
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csDataSet;                            // データセット
            StringBuilder strSQL;
            UFDateClass cfDateClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // なし

                // * 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
                int intWkKensu;
                intWkKensu = m_cfRdbClass.p_intMaxRows();
                // * 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする

                // SQL文の作成    
                strSQL = new StringBuilder();
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_JUMINCD);
                if (!(strGyomuCD == "*1"))
                {
                    // * 履歴番号 000010 2005/01/25 更新開始（宮沢）共通代納も一度に読む
                    // strSQL.Append(" AND ")
                    // strSQL.Append(ABDainoEntity.GYOMUCD)
                    // strSQL.Append(" = ")
                    // strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.GYOMUCD);
                    strSQL.Append(" IN(");
                    strSQL.Append(ABDainoEntity.KEY_GYOMUCD);
                    strSQL.Append(",'00')");
                    // * 履歴番号 000010 2005/01/25 更新終了（宮沢）共通代納も一度に読む

                    // * 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にする
                    m_cfRdbClass.p_intMaxRows = 1;
                    // * 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にする
                }
                strSQL.Append(" AND ");

                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）種別無しも一度に読む
                // strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                if (!(strGyomuCD == "*1"))
                {
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                    strSQL.Append(" IN(");
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                    strSQL.Append(" ,'')");
                }
                else
                {
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                    strSQL.Append(" = ");
                    strSQL.Append("''");
                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）種別無しも一度に読む

                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.STYMD);
                strSQL.Append(" <= ");
                strSQL.Append(ABDainoEntity.KEY_STYMD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.EDYMD);
                strSQL.Append(" >= ");
                strSQL.Append(ABDainoEntity.KEY_EDYMD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }

                // * 履歴番号 000010 2005/01/25 追加開始（宮沢）一度で読んだものをソートして先頭の１件を対象にする
                if (!(strGyomuCD == "*1"))
                {
                    strSQL.Append(" ORDER BY ");
                    strSQL.Append(ABDainoEntity.GYOMUCD);
                    strSQL.Append(" DESC,");
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                    strSQL.Append(" DESC");
                }
                // * 履歴番号 000010 2005/01/25 追加終了（宮沢）一度で読んだものをソートして先頭の１件を対象にする

                strSQL.Append(";");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If文で囲む
                if (!(strGyomuCD == "*1"))
                {
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                    cfUFParameterClass.Value = strGyomuCD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If文で囲む

                // 検索条件のパラメータを作成
                if (!(strGyomuCD == "*1"))
                {
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                    cfUFParameterClass.Value = strGyomunaiSHUCD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                if (strKikanYMD.Trim().Length == 6)
                {
                    if (strKikanYMD.Trim() == "000000")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                    }
                    else if (strKikanYMD.Trim() == "999999")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                    }
                    else
                    {
                        cfDateClass = new UFDateClass(m_cfConfigDataClass);
                        cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                        // * 履歴番号 000018 2023/10/19 修正開始
                        // cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                        cfDateClass.p_strDateValue = strKikanYMD.Trim() + "00";
                        // * 履歴番号 000018 2023/10/19 修正終了
                        cfUFParameterClass.Value = cfDateClass.GetLastDay();
                    }
                }
                else
                {
                    cfUFParameterClass.Value = strKikanYMD;
                }
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                if (strKikanYMD.Trim().Length == 6)
                {
                    if (strKikanYMD.Trim() == "000000")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                    }
                    else if (strKikanYMD.Trim() == "999999")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                    }
                    else
                    {
                        // * 履歴番号 000018 2023/10/19 修正開始
                        // cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                        // * 履歴番号 000018 2023/10/19 修正終了
                    }
                }
                else
                {
                    cfUFParameterClass.Value = strKikanYMD;
                }
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                if (m_blnBatch == false)
                {
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csDataSet = m_csDataSchma.Clone();
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                // * 履歴番号 000009 2004/08/27 更新終了

                // * 履歴番号 000010 2005/01/25 追加開始（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する
                // 上の番号で一度作成したが、必要なくなったので削除
                // If (strGyomuCD = "*1") Then
                // If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count > 1) Then
                // Dim csDataRow As DataRow
                // Dim csDataTable As DataTable
                // Dim intRowCount As Integer
                // csDataTable = csDataSet.Tables(ABDainoEntity.TABLE_NAME)
                // csDataRow = csDataTable.Rows(0)
                // For intRowCount = csDataTable.Rows.Count - 1 To 1 Step -1
                // If (CType(csDataRow.Item(ABDainoEntity.GYOMUNAISHU_CD), String) <> CType(csDataTable.Rows(intRowCount).Item(ABDainoEntity.GYOMUNAISHU_CD), String)) Then
                // csDataTable.Rows(intRowCount).Delete()
                // End If
                // Next
                // csDataTable.AcceptChanges()
                // End If
                // End If
                // * 履歴番号 000010 2005/01/25 追加終了（宮沢）複数件返す場合は、先頭と同じ業務内種別以外のものは削除する

                // * 履歴番号 000010 2005/01/25 追加開始（宮沢）１件だけ読み込む様にしたものを元に戻す
                m_cfRdbClass.p_intMaxRows = intWkKensu;
                // * 履歴番号 000010 2005/01/25 追加終了（宮沢）１件だけ読み込む様にしたものを元に戻す

                // * 履歴番号 000010 2005/01/25 削除開始（宮沢）
                // ' データ件数チェック
                // If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                // ' 業務内種別が指定されていた場合
                // If Not (strGyomunaiSHUCD = String.Empty) Then

                // ' SQL文の作成
                // strSQL = Nothing
                // strSQL = New StringBuilder()
                // strSQL.Append("SELECT * FROM ")
                // strSQL.Append(ABDainoEntity.TABLE_NAME)
                // strSQL.Append(" WHERE ")
                // strSQL.Append(ABDainoEntity.JUMINCD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_JUMINCD)
                // If Not (strGyomuCD = "*1") Then
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.GYOMUCD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                // End If
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.STYM)
                // strSQL.Append(" <= ")
                // strSQL.Append(ABDainoEntity.KEY_STYM)
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.EDYM)
                // strSQL.Append(" >= ")
                // strSQL.Append(ABDainoEntity.KEY_EDYM)
                // If Not blnSakujoFG Then
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.SAKUJOFG)
                // strSQL.Append(" <> 1")
                // End If
                // strSQL.Append(";")

                // ' 検索条件のパラメータコレクションオブジェクトを作成
                // cfUFParameterCollectionClass = New UFParameterCollectionClass()

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
                // cfUFParameterClass.Value = strJuminCD
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // If Not (strGyomuCD = "*1") Then
                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                // cfUFParameterClass.Value = strGyomuCD
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                // cfUFParameterClass.Value = ""
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
                // cfUFParameterClass.Value = strKikanYM
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
                // cfUFParameterClass.Value = strKikanYM
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // '*履歴番号 000006 2003/08/28 修正開始
                // '' RDBアクセスログ出力
                // 'm_cfLogClass.RdbWrite(m_cfControlData, _
                // '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // '                    "【実行メソッド名:GetDataSet】" + _
                // '                    "【SQL内容:" + strSQL.ToString + "】")

                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
                // '*履歴番号 000006 2003/08/28 修正終了

                // ' SQLの実行 DataSetの取得
                // '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // 'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                // csDataSet = m_csDataSchma.Clone()
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                // '* 履歴番号 000009 2004/08/27 更新終了


                // End If

                // End If

                // ' データ件数チェック
                // If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                // ' 業務コード（”00”以外）が指定されていた場合
                // If Not (strGyomuCD = "00") Then

                // ' SQL文の作成
                // strSQL = Nothing
                // strSQL = New StringBuilder()
                // strSQL.Append("SELECT * FROM ")
                // strSQL.Append(ABDainoEntity.TABLE_NAME)
                // strSQL.Append(" WHERE ")
                // strSQL.Append(ABDainoEntity.JUMINCD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_JUMINCD)
                // If Not (strGyomuCD = "*1") Then
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.GYOMUCD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                // End If
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                // strSQL.Append(" = ")
                // strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.STYM)
                // strSQL.Append(" <= ")
                // strSQL.Append(ABDainoEntity.KEY_STYM)
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.EDYM)
                // strSQL.Append(" >= ")
                // strSQL.Append(ABDainoEntity.KEY_EDYM)
                // If Not blnSakujoFG Then
                // strSQL.Append(" AND ")
                // strSQL.Append(ABDainoEntity.SAKUJOFG)
                // strSQL.Append(" <> 1")
                // End If
                // strSQL.Append(";")

                // ' 検索条件のパラメータコレクションオブジェクトを作成
                // cfUFParameterCollectionClass = New UFParameterCollectionClass()

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
                // cfUFParameterClass.Value = strJuminCD
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // If Not (strGyomuCD = "*1") Then
                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                // cfUFParameterClass.Value = "00"
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)
                // End If

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                // cfUFParameterClass.Value = ""
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
                // cfUFParameterClass.Value = strKikanYM
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass()
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
                // cfUFParameterClass.Value = strKikanYM
                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // '*履歴番号 000006 2003/08/28 修正開始
                // '' RDBアクセスログ出力
                // 'm_cfLogClass.RdbWrite(m_cfControlData, _
                // '                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // '                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // '                    "【実行メソッド名:GetDataSet】" + _
                // '                    "【SQL内容:" + strSQL.ToString + "】")

                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
                // '*履歴番号 000006 2003/08/28 修正終了

                // ' SQLの実行 DataSetの取得
                // '* 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // 'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                // csDataSet = m_csDataSchma.Clone()
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                // '* 履歴番号 000009 2004/08/27 更新終了

                // End If

                // End If
                // * 履歴番号 000010 2005/01/25 削除終了（宮沢）

                // クラスの解放
                strSQL = null;

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

            return csDataSet;

        }


        // *履歴番号 000014 2010/03/05 追加開始
        // ************************************************************************************************
        // * メソッド名     代納マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetDainoBHoshu(ByVal cABDainoGetParaX As ABDainoGetParaXClass) As DataSet
        // * 
        // * 
        // * 機能　　    　 代納マスタより該当データを取得する
        // * 
        // * 引数           cABDainoGetParaX      :   代納情報パラメータクラス
        // *  
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetDainoBHoshu(ABDainoGetParaXClass cABDainoGetParaX)
        {
            const string THIS_METHOD_NAME = "GetDainoBHoshu";             // メソッド名
            DataSet csDainoEntity;                                    // 代納マスタデータ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnAndFg = false;                                 // AND判定フラグ
            string strWork;
            UFDateClass cfDateClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // インスタンス化
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // スキーマ取得処理
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoEntity.TABLE_NAME, false);
                }
                else
                {
                }

                // SQL文の作成
                // SELECT句
                strSQL.Append("SELECT * ");

                strSQL.Append(" FROM ").Append(ABDainoEntity.TABLE_NAME);

                // WHERE句
                strSQL.Append(" WHERE ");
                // ---------------------------------------------------------------------------------
                // 住民コード
                if (cABDainoGetParaX.p_strJuminCD.Trim != string.Empty)
                {
                    // 住民コードが設定されている場合

                    strSQL.Append(ABDainoEntity.JUMINCD).Append(" = ");
                    strSQL.Append(ABDainoEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = (string)cABDainoGetParaX.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 業務コード
                if (cABDainoGetParaX.p_strGyomuCD.Trim != string.Empty)
                {
                    // 業務コードが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABDainoEntity.GYOMUCD).Append(" = ");
                    strSQL.Append(ABDainoEntity.KEY_GYOMUCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 業務内種別コード
                if (cABDainoGetParaX.p_strGyomuneiSHU_CD.Trim != string.Empty)
                {
                    // 業務内種別コードが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD).Append(" = ");
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuneiSHU_CD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }

                // ---------------------------------------------------------------------------------
                // 期間
                // * 履歴番号 000018 2023/10/19 修正開始
                // If (cABDainoGetParaX.p_strKikanYM.Trim <> String.Empty) Then
                if (cABDainoGetParaX.p_strKikanYMD.Trim != string.Empty)
                {
                    // * 履歴番号 000018 2023/10/19 修正終了
                    // 期間が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append("(");
                    strSQL.Append(ABDainoEntity.STYMD);                    // 開始年月日
                    strSQL.Append(" <= ");
                    strSQL.Append(ABDainoEntity.KEY_STYMD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.EDYMD);                    // 終了年月日
                    strSQL.Append(" >= ");
                    strSQL.Append(ABDainoEntity.KEY_EDYMD);
                    strSQL.Append(")");

                    // 開始年月日
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                    // * 履歴番号 000018 2023/10/19 修正開始
                    // If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                    // If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                    // ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                    // Else
                    // cfDateClass = New UFDateClass(m_cfConfigDataClass)
                    // cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    // cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                    // cfUFParameterClass.Value = cfDateClass.GetLastDay()
                    // End If
                    // Else
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                    // End If

                    if (cABDainoGetParaX.p_strKikanYMD.Trim.Length == 6)
                    {
                        if (cABDainoGetParaX.p_strKikanYMD.Trim == ALL0_YMD)
                        {
                            cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00";
                        }
                        else if (cABDainoGetParaX.p_strKikanYMD.Trim == ALL9_YMD)
                        {
                            cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99";
                        }
                        else
                        {
                            cfDateClass = new UFDateClass(m_cfConfigDataClass);
                            cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                            cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYMD.Trim + "00";
                            cfUFParameterClass.Value = cfDateClass.GetLastDay();
                        }
                    }
                    else
                    {
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD;
                    }
                    // * 履歴番号 000018 2023/10/19 修正終了

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 終了年月日
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                    // * 履歴番号 000018 2023/10/19 修正開始
                    // If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                    // If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                    // ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                    // Else
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                    // End If
                    // Else
                    // cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                    // End If
                    if (cABDainoGetParaX.p_strKikanYMD.Trim.Length == 6)
                    {
                        if (cABDainoGetParaX.p_strKikanYMD.Trim == ALL0_YMD)
                        {
                            cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00";
                        }
                        else if (cABDainoGetParaX.p_strKikanYMD.Trim == ALL9_YMD)
                        {
                            cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99";
                        }
                        else
                        {
                            cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00";
                        }
                    }
                    else
                    {
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD;
                    }
                    // * 履歴番号 000018 2023/10/19 修正終了

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 削除フラグ
                if (cABDainoGetParaX.p_strSakujoFG.Trim == string.Empty)
                {
                    // 削除フラグ指定がない場合、削除データは抽出しない
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }
                    strSQL.Append(ABDainoEntity.SAKUJOFG).Append(" <> '1'");
                }

                // 削除フラグ指定がある場合、削除データも抽出する
                else if (blnAndFg == true)
                {
                }
                // AND判定フラグが"True"の場合、SQL文生成処理を終了
                else
                {
                    // AND判定フラグが"False"の場合、SQL文から｢WHERE｣を削除
                    // 削除したSQLを一時退避
                    strWork = strSQL.ToString().Replace("WHERE", string.Empty);

                    // strSQLをクリアし、退避したSQLをセット
                    strSQL.Length = 0;
                    strSQL.Append(strWork);
                }
                // ---------------------------------------------------------------------------------

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csDainoEntity = m_csDataSchma.Clone();
                csDainoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoEntity, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, false);


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

            return csDainoEntity;

        }
        // *履歴番号 000014 2010/03/05 追加終了


        // ************************************************************************************************
        // * メソッド名     被代納マスタ抽出
        // * 
        // * 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetHiDainoBHoshu(string strJuminCD)
        {
            return GetHiDainoBHoshu(strJuminCD, false);
        }

        // ************************************************************************************************
        // * メソッド名     被代納マスタ抽出
        // * 
        // * 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
        // *                                                 ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // *                blnSakujoFG   : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetHiDainoBHoshu(string strJuminCD, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetHiDainoBHoshu";
            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000015
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csDataSet;                            // データセット
            var strSQL = new StringBuilder("");

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // なし

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成    
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoEntity.DAINOJUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABDainoEntity.GYOMUCD);
                strSQL.Append(" ASC, ");
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                strSQL.Append(" ASC");
                // *履歴番号 000013 2007/03/09 追加開始
                strSQL.Append(", ");
                strSQL.Append(ABDainoEntity.STYMD);
                strSQL.Append(" ASC");
                // *履歴番号 000013 2007/03/09 追加終了
                strSQL.Append(";");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                if (m_blnBatch == false)
                {
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csDataSet = m_csDataSchma.Clone();
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                // * 履歴番号 000009 2004/08/27 更新終了


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

            return csDataSet;

        }

        // ************************************************************************************************
        // * メソッド名     被代納マスタ抽出
        // * 
        // * 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
        // *                                                 ByVal strGyomuCD As String,
        // *                                                 ByVal strGyomunaiSHUCD As String,
        // *                                                 ByVal strKikanYMD As String) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strGyomuCD        : 業務コード
        // *                strGyomunaiSHUCD  : 業務内種別コード
        // *                strKikanYM        : 期間年月日
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetHiDainoBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, string strKikanYMD)
        {
            return GetHiDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, false);
        }

        // ************************************************************************************************
        // * メソッド名     被代納マスタ抽出
        // * 
        // * 構文           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
        // *                                                 ByVal strGyomuCD As String,
        // *                                                 ByVal strGyomunaiSHUCD As String,
        // *                                                 ByVal strKikanYMD As String,
        // *                                                 ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strGyomuCD        : 業務コード
        // *                strGyomunaiSHUCD  : 業務内種別コード
        // *                strKikanYM        : 期間年月日
        // *                blnSakujoFG       : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetHiDainoBHoshu(string strJuminCD, string strGyomuCD, string strGyomunaiSHUCD, string strKikanYMD, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetHiDainoBHoshu";
            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000015
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csDataSet;                            // データセット
            StringBuilder strSQL;
            UFDateClass cfDateClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // なし

                // 宛名検索キーのチェック
                // なし

                // SQL文の作成    
                strSQL = new StringBuilder();
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoEntity.DAINOJUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                if (!(strGyomuCD == "*1"))
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.GYOMUCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABDainoEntity.KEY_GYOMUCD);
                }
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.STYMD);
                strSQL.Append(" <= ");
                strSQL.Append(ABDainoEntity.KEY_STYMD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.EDYMD);
                strSQL.Append(" >= ");
                strSQL.Append(ABDainoEntity.KEY_EDYMD);
                if (!blnSakujoFG)
                {
                    strSQL.Append(" AND ");
                    strSQL.Append(ABDainoEntity.SAKUJOFG);
                    strSQL.Append(" <> 1");
                }
                strSQL.Append(";");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                if (!(strGyomuCD == "*1"))
                {
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                    cfUFParameterClass.Value = strGyomuCD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = strGyomunaiSHUCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                if (strKikanYMD.Trim().Length == 6)
                {
                    if (strKikanYMD.Trim() == "000000")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                    }
                    else if (strKikanYMD.Trim() == "999999")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                    }
                    else
                    {
                        cfDateClass = new UFDateClass(m_cfConfigDataClass);
                        cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                        // * 履歴番号 000018 2023/10/19 修正開始
                        // cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                        cfDateClass.p_strDateValue = strKikanYMD.Trim() + "00";
                        // * 履歴番号 000018 2023/10/19 修正終了
                        cfUFParameterClass.Value = cfDateClass.GetLastDay();
                    }
                }
                else
                {
                    cfUFParameterClass.Value = strKikanYMD;
                }
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                if (strKikanYMD.Trim().Length == 6)
                {
                    if (strKikanYMD.Trim() == "000000")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                    }
                    else if (strKikanYMD.Trim() == "999999")
                    {
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                    }
                    else
                    {
                        // * 履歴番号 000018 2023/10/19 修正開始
                        // cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                        cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                        // * 履歴番号 000018 2023/10/19 修正終了
                    }
                }
                else
                {
                    cfUFParameterClass.Value = strKikanYMD;
                }
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                if (m_blnBatch == false)
                {
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                }
                // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                csDataSet = m_csDataSchma.Clone();
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                // * 履歴番号 000009 2004/08/27 更新終了

                // データ件数チェック
                if (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count == 0)
                {

                    // 業務内種別が指定されていた場合
                    if (!string.IsNullOrEmpty(strGyomunaiSHUCD))
                    {

                        // SQL文の作成
                        strSQL = null;
                        strSQL = new StringBuilder();
                        strSQL.Append("SELECT * FROM ");
                        strSQL.Append(ABDainoEntity.TABLE_NAME);
                        strSQL.Append(" WHERE ");
                        strSQL.Append(ABDainoEntity.DAINOJUMINCD);
                        strSQL.Append(" = ");
                        strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                        if (!(strGyomuCD == "*1"))
                        {
                            strSQL.Append(" AND ");
                            strSQL.Append(ABDainoEntity.GYOMUCD);
                            strSQL.Append(" = ");
                            strSQL.Append(ABDainoEntity.KEY_GYOMUCD);
                        }
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                        strSQL.Append(" = ");
                        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.STYMD);
                        strSQL.Append(" <= ");
                        strSQL.Append(ABDainoEntity.KEY_STYMD);
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.EDYMD);
                        strSQL.Append(" >= ");
                        strSQL.Append(ABDainoEntity.KEY_EDYMD);
                        if (!blnSakujoFG)
                        {
                            strSQL.Append(" AND ");
                            strSQL.Append(ABDainoEntity.SAKUJOFG);
                            strSQL.Append(" <> 1");
                        }
                        strSQL.Append(";");

                        // 検索条件のパラメータコレクションオブジェクトを作成
                        cfUFParameterCollectionClass = new UFParameterCollectionClass();

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                        cfUFParameterClass.Value = strJuminCD;
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        if (!(strGyomuCD == "*1"))
                        {
                            // 検索条件のパラメータを作成
                            cfUFParameterClass = new UFParameterClass();
                            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                            cfUFParameterClass.Value = strGyomuCD;
                            // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfUFParameterCollectionClass.Add(cfUFParameterClass);
                        }

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                        cfUFParameterClass.Value = "";
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                        if (strKikanYMD.Trim().Length == 6)
                        {
                            if (strKikanYMD.Trim() == "000000")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                            }
                            else if (strKikanYMD.Trim() == "999999")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                            }
                            else
                            {
                                cfDateClass = new UFDateClass(m_cfConfigDataClass);
                                cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                                // * 履歴番号 000018 2023/10/19 修正開始
                                // cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                                cfDateClass.p_strDateValue = strKikanYMD.Trim() + "00";
                                // * 履歴番号 000018 2023/10/19 修正終了
                                cfUFParameterClass.Value = cfDateClass.GetLastDay();
                            }
                        }
                        else
                        {
                            cfUFParameterClass.Value = strKikanYMD;
                        }
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                        if (strKikanYMD.Trim().Length == 6)
                        {
                            if (strKikanYMD.Trim() == "000000")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                            }
                            else if (strKikanYMD.Trim() == "999999")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                            }
                            else
                            {
                                // * 履歴番号 000018 2023/10/19 修正開始
                                // cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                                // * 履歴番号 000018 2023/10/19 修正終了
                            }
                        }
                        else
                        {
                            cfUFParameterClass.Value = strKikanYMD;
                        }
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // *履歴番号 000006 2003/08/28 修正開始
                        // ' RDBアクセスログ出力
                        // m_cfLogClass.RdbWrite(m_cfControlData, _
                        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                        // "【実行メソッド名:GetDataSet】" + _
                        // "【SQL内容:" + strSQL.ToString + "】")

                        // RDBアクセスログ出力
                        // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                        if (m_blnBatch == false)
                        {
                            m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                        }
                        // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                        // *履歴番号 000006 2003/08/28 修正終了

                        // SQLの実行 DataSetの取得
                        // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                        // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                        csDataSet = m_csDataSchma.Clone();
                        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                        // * 履歴番号 000009 2004/08/27 更新終了


                    }

                }

                // データ件数チェック
                if (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count == 0)
                {

                    // 業務コード（”00”以外）が指定されていた場合
                    if (!(strGyomuCD == "00"))
                    {

                        // SQL文の作成
                        strSQL = null;
                        strSQL = new StringBuilder();
                        strSQL.Append("SELECT * FROM ");
                        strSQL.Append(ABDainoEntity.TABLE_NAME);
                        strSQL.Append(" WHERE ");
                        strSQL.Append(ABDainoEntity.DAINOJUMINCD);
                        strSQL.Append(" = ");
                        strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                        if (!(strGyomuCD == "*1"))
                        {
                            strSQL.Append(" AND ");
                            strSQL.Append(ABDainoEntity.GYOMUCD);
                            strSQL.Append(" = ");
                            strSQL.Append(ABDainoEntity.KEY_GYOMUCD);
                        }
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                        strSQL.Append(" = ");
                        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.STYMD);
                        strSQL.Append(" <= ");
                        strSQL.Append(ABDainoEntity.KEY_STYMD);
                        strSQL.Append(" AND ");
                        strSQL.Append(ABDainoEntity.EDYMD);
                        strSQL.Append(" >= ");
                        strSQL.Append(ABDainoEntity.KEY_EDYMD);
                        if (!blnSakujoFG)
                        {
                            strSQL.Append(" AND ");
                            strSQL.Append(ABDainoEntity.SAKUJOFG);
                            strSQL.Append(" <> 1");
                        }
                        strSQL.Append(";");

                        // 検索条件のパラメータコレクションオブジェクトを作成
                        cfUFParameterCollectionClass = new UFParameterCollectionClass();

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                        cfUFParameterClass.Value = strJuminCD;
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        if (!(strGyomuCD == "*1"))
                        {
                            // 検索条件のパラメータを作成
                            cfUFParameterClass = new UFParameterClass();
                            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                            cfUFParameterClass.Value = "00";
                            // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                            cfUFParameterCollectionClass.Add(cfUFParameterClass);
                        }

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                        cfUFParameterClass.Value = "";
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                        if (strKikanYMD.Trim().Length == 6)
                        {
                            if (strKikanYMD.Trim() == "000000")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                            }
                            else if (strKikanYMD.Trim() == "999999")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                            }
                            else
                            {
                                cfDateClass = new UFDateClass(m_cfConfigDataClass);
                                cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                                // * 履歴番号 000018 2023/10/19 修正開始
                                // cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                                cfDateClass.p_strDateValue = strKikanYMD.Trim() + "00";
                                // * 履歴番号 000018 2023/10/19 修正終了
                                cfUFParameterClass.Value = cfDateClass.GetLastDay();
                            }
                        }
                        else
                        {
                            cfUFParameterClass.Value = strKikanYMD;
                        }
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                        if (strKikanYMD.Trim().Length == 6)
                        {
                            if (strKikanYMD.Trim() == "000000")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                            }
                            else if (strKikanYMD.Trim() == "999999")
                            {
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "99";
                            }
                            else
                            {
                                // * 履歴番号 000018 2023/10/19 修正開始
                                // cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                                cfUFParameterClass.Value = strKikanYMD.Trim() + "00";
                                // * 履歴番号 000018 2023/10/19 修正終了
                            }
                        }
                        else
                        {
                            cfUFParameterClass.Value = strKikanYMD;
                        }
                        // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                        cfUFParameterCollectionClass.Add(cfUFParameterClass);

                        // *履歴番号 000006 2003/08/28 修正開始
                        // ' RDBアクセスログ出力
                        // m_cfLogClass.RdbWrite(m_cfControlData, _
                        // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                        // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                        // "【実行メソッド名:GetDataSet】" + _
                        // "【SQL内容:" + strSQL.ToString + "】")

                        // RDBアクセスログ出力
                        // * 履歴番号 000010 2005/01/25 更新開始（宮沢）If 文で囲む
                        if (m_blnBatch == false)
                        {
                            m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");
                        }
                        // * 履歴番号 000010 2005/01/25 更新終了（宮沢）If 文で囲む
                        // *履歴番号 000006 2003/08/28 修正終了

                        // SQLの実行 DataSetの取得
                        // * 履歴番号 000009 2004/08/27 更新開始（宮沢）
                        // csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                        csDataSet = m_csDataSchma.Clone();
                        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);
                        // * 履歴番号 000009 2004/08/27 更新終了

                    }

                }

                // クラスの解放
                strSQL = null;

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

            return csDataSet;

        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ追加
        // * 
        // * 構文           Public Function InsertDainoB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　代納マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertDainoB(DataRow csDataRow)
        {

            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim csInstRow As DataRow
            // * corresponds to VS2008 End 2010/04/16 000015
            const string THIS_METHOD_NAME = "InsertDainoB";     // パラメータクラス
            int intInsCnt;            // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateInsertSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }


                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId; // 端末ＩＤ
                csDataRow(ABDainoEntity.SAKUJOFG) = "0";                             // 削除フラグ
                csDataRow(ABDainoEntity.KOSHINCOUNTER) = decimal.Zero;               // 更新カウンタ
                csDataRow(ABDainoEntity.SAKUSEINICHIJI) = strUpdateDateTime;         // 作成日時
                csDataRow(ABDainoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;  // 作成ユーザー
                csDataRow(ABDainoEntity.KOSHINNICHIJI) = strUpdateDateTime;          // 更新日時
                csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;   // 更新ユーザー


                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim());


                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength)).ToString();


                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");



                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

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

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ更新
        // * 
        // * 構文           Public Function UpdateDainoB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　代納マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateDainoB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateDainoB";                     // パラメータクラス
                                                                                // * corresponds to VS2008 Start 2010/04/16 000015
                                                                                // Dim csDataColumn As DataColumn
                                                                                // * corresponds to VS2008 End 2010/04/16 000015
            int intUpdCnt;                            // 更新件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateUpdateSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }

                // 共通項目の編集を行う
                csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABDainoEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1m;               // 更新カウンタ
                csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー
                                                                                                                       // * 履歴番号 000019 2023/12/05 削除開始
                                                                                                                       // '* 履歴番号 000018 2023/10/19 追加開始
                                                                                                                       // csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             '履歴番号
                                                                                                                       // '* 履歴番号 000018 2023/10/19 追加終了
                                                                                                                       // * 履歴番号 000019 2023/12/05 削除終了

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) == ABDainoEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim);
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");



                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

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

            return intUpdCnt;

        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ論理削除
        // * 
        // * 構文           Public Function DeleteDainoB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　代納マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteDainoB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteDainoB";                     // パラメータクラス
                                                                                // * corresponds to VS2008 Start 2010/04/16 000015
                                                                                // Dim csDataColumn As DataColumn
                                                                                // * corresponds to VS2008 End 2010/04/16 000015
            int intDelCnt;                            // 削除件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null | string.IsNullOrEmpty(m_strDelRonriSQL) | m_cfDelRonriUFParameterCollectionClass is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateDeleteRonriSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }


                // 共通項目の編集を行う
                csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABDainoEntity.SAKUJOFG) = "1";                                                               // 削除フラグ
                csDataRow(ABDainoEntity.KOSHINCOUNTER) = Conversions.ToDecimal(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1m;             // 更新カウンタ
                csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー
                                                                                                                       // * 履歴番号 000019 2023/12/05 削除開始
                                                                                                                       // '* 履歴番号 000018 2023/10/19 追加開始
                                                                                                                       // csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             ' 履歴番号
                                                                                                                       // '* 履歴番号 000018 2023/10/19 追加終了
                                                                                                                       // * 履歴番号 000019 2023/12/05 削除終了

                // *履歴番号 000006 2003/08/28 修正開始
                // ' 作成済みのパラメータへ更新行から値を設定する。
                // For Each cfParam In m_cfUpdateUFParameterCollectionClass
                // ' キー項目は更新前の値で設定
                // If (cfParam.ParameterName.Substring(0, ABDainoEntity.PREFIX_KEY.Length) = ABDainoEntity.PREFIX_KEY) Then
                // m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
                // csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PREFIX_KEY.Length), _
                // DataRowVersion.Original).ToString()
                // Else
                // 'データ整合性チェック
                // CheckColumnValue(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
                // m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
                // End If
                // Next cfParam

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) == ABDainoEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim);
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }
                // *履歴番号 000006 2003/08/28 修正終了


                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】");



                // *履歴番号 000006 2003/08/28 修正終了

                // *履歴番号 000006 2003/08/28 修正開始
                // ' SQLの実行
                // intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);
                // *履歴番号 000006 2003/08/28 修正終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan");
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

            return intDelCnt;

        }

        // ************************************************************************************************
        // * メソッド名     代納マスタ物理削除
        // * 
        // * 構文           Public Function DeleteDainoB(ByVal csDataRow As DataRow, _
        // *                                             ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　代納マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow  : 削除するデータの含まれるDataRowオブジェクト
        // *                strSakujoKB As String : 削除フラグ
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteDainoB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteDainoB";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
                                                          // * corresponds to VS2008 Start 2010/04/16 000015
                                                          // Dim csDataColumn As DataColumn
                                                          // * corresponds to VS2008 End 2010/04/16 000015
            int intDelCnt;                            // 削除件数


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除区分のチェックを行う
                if (!(strSakujoKB == "D"))
                {

                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);

                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null | string.IsNullOrEmpty(m_strDelButuriSQL) | m_cfDelButuriUFParameterCollectionClass == null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateDeleteButsuriSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollectionClass)
                {

                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) == ABDainoEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                }


                // *履歴番号 000006 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】");



                // *履歴番号 000006 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass);

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

            return intDelCnt;

        }

        // * corresponds to VS2008 Start 2010/04/16 000015
        // * 履歴番号 000011 2005/06/16 削除開始
        // '''************************************************************************************************
        // '''* メソッド名     SQL文の作成
        // '''* 
        // '''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // '''* 
        // '''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // '''* 
        // '''* 引数           csDataRow As DataRow : 更新対象の行
        // '''* 
        // '''* 戻り値         なし
        // '''************************************************************************************************
        // '''Private Sub CreateSQL(ByVal csDataRow As DataRow)

        // '''    Const THIS_METHOD_NAME As String = "CreateSQL"
        // '''    Dim cfUFParameterClass As UFParameterClass
        // '''    Dim csDataColumn As DataColumn
        // '''    Dim csInsertColumn As StringBuilder                 'INSERTカラム定義
        // '''    Dim csInsertParam As StringBuilder                  'INSERTパラメータ定義
        // '''    Dim csUpdateParam As StringBuilder                  'UPDATE用パラメータ
        // '''    Dim csWhere As StringBuilder                        'WHERE句
        // '''    Dim csDelRonriParam As StringBuilder                '論理削除パラメータ定義

        // '''    Try
        // '''        ' デバッグログ出力
        // '''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''        ' INSERT SQL文の作成
        // '''        m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " "
        // '''        csInsertColumn = New StringBuilder()
        // '''        csInsertParam = New StringBuilder()

        // '''        ' UPDATE SQL文の作成
        // '''        m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET "
        // '''        csUpdateParam = New StringBuilder()

        // '''        ' WHERE句の作成
        // '''        csWhere = New StringBuilder()
        // '''        csWhere.Append(" WHERE ")
        // '''        csWhere.Append(ABDainoEntity.JUMINCD)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_JUMINCD)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.GYOMUCD)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.DAINOJUMINCD)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.STYM)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_STYM)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.EDYM)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_EDYM)
        // '''        csWhere.Append(" AND ")
        // '''        csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
        // '''        csWhere.Append(" = ")
        // '''        csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

        // '''        ' 論理DELETE SQL文の作成
        // '''        csDelRonriParam = New StringBuilder()
        // '''        csDelRonriParam.Append("UPDATE ")
        // '''        csDelRonriParam.Append(ABDainoEntity.TABLE_NAME)
        // '''        csDelRonriParam.Append(" SET ")
        // '''        csDelRonriParam.Append(ABDainoEntity.TANMATSUID)
        // '''        csDelRonriParam.Append(" = ")
        // '''        csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID)
        // '''        csDelRonriParam.Append(", ")
        // '''        csDelRonriParam.Append(ABDainoEntity.SAKUJOFG)
        // '''        csDelRonriParam.Append(" = ")
        // '''        csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG)
        // '''        csDelRonriParam.Append(", ")
        // '''        csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER)
        // '''        csDelRonriParam.Append(" = ")
        // '''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER)
        // '''        csDelRonriParam.Append(", ")
        // '''        csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI)
        // '''        csDelRonriParam.Append(" = ")
        // '''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI)
        // '''        csDelRonriParam.Append(", ")
        // '''        csDelRonriParam.Append(ABDainoEntity.KOSHINUSER)
        // '''        csDelRonriParam.Append(" = ")
        // '''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER)
        // '''        csDelRonriParam.Append(csWhere)
        // '''        m_strDelRonriSQL = csDelRonriParam.ToString

        // '''        ' 物理DELETE SQL文の作成
        // '''        m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME _
        // '''                + csWhere.ToString

        // '''        ' INSERT パラメータコレクションクラスのインスタンス化
        // '''        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

        // '''        ' UPDATE パラメータコレクションのインスタンス化
        // '''        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

        // '''        ' 論理削除用パラメータコレクションのインスタンス化
        // '''        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

        // '''        ' 物理削除用パラメータコレクションのインスタンス化
        // '''        m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()



        // '''        ' パラメータコレクションの作成
        // '''        For Each csDataColumn In csDataRow.Table.Columns
        // '''            cfUFParameterClass = New UFParameterClass()

        // '''            ' INSERT SQL文の作成
        // '''            csInsertColumn.Append(csDataColumn.ColumnName)
        // '''            csInsertColumn.Append(", ")

        // '''            csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
        // '''            csInsertParam.Append(csDataColumn.ColumnName)
        // '''            csInsertParam.Append(", ")


        // '''            ' UPDATE SQL文の作成
        // '''            csUpdateParam.Append(csDataColumn.ColumnName)
        // '''            csUpdateParam.Append(" = ")
        // '''            csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
        // '''            csUpdateParam.Append(csDataColumn.ColumnName)
        // '''            csUpdateParam.Append(", ")

        // '''            ' INSERT コレクションにパラメータを追加
        // '''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // '''            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''            ' UPDATE コレクションにパラメータを追加
        // '''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // '''            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        Next csDataColumn


        // '''        '最後のカンマを取り除いてINSERT文を作成
        // '''        m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
        // '''                + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"



        // '''        '最後のカンマを取り除いてUPDATE文を作成
        // '''        m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString


        // '''        ' UPDATE コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
        // '''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)


        // '''        ' 論理削除用コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
        // '''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)


        // '''        ' 物理削除用コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
        // '''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

        // '''        ' デバッグログ出力
        // '''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''    Catch objAppExp As UFAppException
        // '''        ' ワーニングログ出力
        // '''        m_cfLogClass.WarningWrite(m_cfControlData, _
        // '''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '''                                    "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
        // '''                                    "【ワーニング内容:" + objAppExp.Message + "】")
        // '''        ' エラーをそのままスローする
        // '''        Throw objAppExp

        // '''    Catch objExp As Exception
        // '''        ' エラーログ出力
        // '''        m_cfLogClass.ErrorWrite(m_cfControlData, _
        // '''                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
        // '''                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
        // '''                                    "【エラー内容:" + objExp.Message + "】")
        // '''        ' エラーをそのままスローする
        // '''        Throw objExp

        // '''    End Try

        // '''End Sub
        // * 履歴番号 000011 2005/06/16 削除終了
        // * corresponds to VS2008 End 2010/04/16 000015

        // * 履歴番号 000011 2005/06/16 追加開始
        // ************************************************************************************************
        // * メソッド名     Insert用SQL文の作成
        // * 
        // * 構文           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           INSERT用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateInsertSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateInsertSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csInsertColumn;                 // INSERTカラム定義
            StringBuilder csInsertParam;                  // INSERTパラメータ定義

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " ";
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    csInsertColumn.Append(csDataColumn.ColumnName);
                    csInsertColumn.Append(", ");

                    csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + csInsertColumn.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" + " VALUES (" + csInsertParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")";

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
        // * メソッド名     Update用SQL文の作成
        // * 
        // * 構文           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           UPDATE用の各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateUpdateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateUpdateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csUpdateParam;                  // UPDATE用パラメータ
            StringBuilder csWhere;                        // WHERE句

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE句の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABDainoEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.DAINOJUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.STYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_STYM)
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.EDYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_EDYM)
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.TOROKURENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABDainoEntity.JUMINCD) && !(csDataColumn.ColumnName == ABDainoEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABDainoEntity.SAKUSEINICHIJI))

                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(", ");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }

                }

                // 最後のカンマを取り除いてUPDATE文を作成
                m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString();

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
                // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
                // m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * メソッド名     論理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           論理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteRonriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteRonriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE句
            StringBuilder csDelRonriParam;                // 論理削除パラメータ定義

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABDainoEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.DAINOJUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.STYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_STYM)
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.EDYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_EDYM)
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.TOROKURENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABDainoEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABDainoEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABDainoEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER);
                // * 履歴番号 000018 2023/10/19 追加開始
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABDainoEntity.RRKNO);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_RRKNO);
                // * 履歴番号 000018 2023/10/19 追加終了
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABDainoEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                // * 履歴番号 000018 2023/10/19 追加開始
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_RRKNO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);
                // * 履歴番号 000018 2023/10/19 追加終了

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
                // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
                // m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDeleteButsuriSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateDeleteButsuriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE句

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE句の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABDainoEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.DAINOJUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD);
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.STYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_STYM)
                // csWhere.Append(" AND ")
                // csWhere.Append(ABDainoEntity.EDYM)
                // csWhere.Append(" = ")
                // csWhere.Append(ABDainoEntity.KEY_EDYM)
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.TOROKURENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABDainoEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
                // m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
                // m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // * 履歴番号 000011 2005/06/16 削除終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue as String)
        // * 
        // * 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           strColumnName As String : 住登外マスタデータセットの項目名
        // *                strValue As String     : 項目に対応する値
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {

            const string THIS_METHOD_NAME = "CheckColumnValue";
            const string TABLENAME = "代納．";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'");

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                switch (strColumnName.ToUpper() ?? "")
                {

                    case var @case when @case == ABDainoEntity.JUMINCD:                  // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case1 when case1 == ABDainoEntity.SHICHOSONCD:              // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case2 when case2 == ABDainoEntity.KYUSHICHOSONCD:           // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case3 when case3 == ABDainoEntity.GYOMUCD:                  // 業務コード
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case4 when case4 == ABDainoEntity.GYOMUNAISHU_CD:           // 業務内種別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.Trim()))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUNAISHU_CD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case5 when case5 == ABDainoEntity.DAINOJUMINCD:             // 代納住民コード
                        {
                            if (!string.IsNullOrEmpty(strValue.Trim()))
                            {
                                if (!UFStringClass.CheckNumber(strValue))
                                {
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOJUMINCD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case6 when case6 == ABDainoEntity.STYMD:                     // 開始年月日
                        {
                            switch (strValue.Trim() ?? "")
                            {
                                // ＯＫ
                                case "00000000":
                                case var case7 when case7 == "":
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        m_cfDateClass.p_strDateValue = strValue;
                                        if (!m_cfDateClass.CheckDate())
                                        {
                                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                            // エラー定義を取得(日付項目入力の誤りです。：)
                                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019);
                                            // 例外を生成
                                            throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "開始年月日", objErrorStruct.m_strErrorCode);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }

                    case var case8 when case8 == ABDainoEntity.EDYMD:                     // 終了年月日
                        {
                            switch (strValue.Trim() ?? "")
                            {
                                // ＯＫ
                                case "00000000":
                                case "99999999":
                                case var case9 when case9 == "":
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        m_cfDateClass.p_strDateValue = strValue;
                                        if (!m_cfDateClass.CheckDate())
                                        {
                                            m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                            // エラー定義を取得(日付項目入力の誤りです。：)
                                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019);
                                            // 例外を生成
                                            throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "終了年月日", objErrorStruct.m_strErrorCode);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }

                    // * 履歴番号 000018 2023/10/19 追加開始
                    case var case10 when case10 == ABDainoEntity.TOROKURENBAN:             // 登録連番
                        {
                            if (!string.IsNullOrEmpty(strValue.Trim()))
                            {
                                if (!UFStringClass.CheckNumber(strValue))
                                {
                                    // * 履歴番号 000019 2023/12/05 修正開始
                                    // '例外を生成
                                    // Throw New UFAppException("数字項目入力エラー：ＡＢ代納　登録連番", UFAppException.ERR_EXCEPTION)
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TOROKURENBAN);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                    // * 履歴番号 000019 2023/12/05 修正終了
                                }
                            }

                            break;
                        }

                    case var case11 when case11 == ABDainoEntity.RRKNO:                     // 履歴番号
                        {
                            if (!string.IsNullOrEmpty(strValue.Trim()))
                            {
                                if (!UFStringClass.CheckNumber(strValue))
                                {
                                    // * 履歴番号 000019 2023/12/05 修正開始
                                    // '例外を生成
                                    // Throw New UFAppException("数字項目入力エラー：ＡＢ代納　履歴番号", UFAppException.ERR_EXCEPTION)
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    // エラー定義を取得
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_RRKNO);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                    // * 履歴番号 000019 2023/12/05 修正終了
                                }
                            }

                            break;
                        }
                    // * 履歴番号 000018 2023/10/19 追加終了

                    case var case12 when case12 == ABDainoEntity.DAINOKB:                  // 代納区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case13 when case13 == ABDainoEntity.RESERVE:                  // リザーブ
                        {
                            break;
                        }
                    // チェックなし

                    case var case14 when case14 == ABDainoEntity.TANMATSUID:               // 端末ＩＤ
                        {
                            // * 履歴番号 000007 2003/09/11 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000007 2003/09/11 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case15 when case15 == ABDainoEntity.SAKUJOFG:                 // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case16 when case16 == ABDainoEntity.KOSHINCOUNTER:            // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case17 when case17 == ABDainoEntity.SAKUSEINICHIJI:           // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case18 when case18 == ABDainoEntity.SAKUSEIUSER:              // 作成ユーザ
                        {
                            // * 履歴番号 000008 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000008 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case19 when case19 == ABDainoEntity.KOSHINNICHIJI:            // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case20 when case20 == ABDainoEntity.KOSHINUSER:               // 更新ユーザ
                        {
                            // * 履歴番号 000008 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000008 2003/10/09 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

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
        // * 履歴番号 000010 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名     代納マスタスキーマ取得
        // * 
        // * 構文           Public Function GetDainoSchemaBHoshu() As DataSet
        // * 
        // * 機能　　    　　代納マスタよりスキーマ取得
        // * 
        // * 
        // * 戻り値         DataSet : 取得した代納マスタのスキーマ
        // ************************************************************************************************
        public DataSet GetDainoSchemaBHoshu()
        {
            const string THIS_METHOD_NAME = "GetDainoSchemaBHoshu";              // このメソッド名

            try
            {
                return m_csDataSchma.Clone();
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
        // * 履歴番号 000010 2005/01/25 追加終了（宮沢）

        // * 履歴番号 000012 2006/12/22 追加開始
        // ************************************************************************************************
        // * メソッド名     本店情報抽出
        // * 
        // * 構文           Public Function GetHontenBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　代納マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // * 
        // * 戻り値         DataSet : 取得した代納マスタの該当データ
        // ************************************************************************************************
        public DataSet GetHontenBHoshu(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetHontenBHoshu";    // メソッド名
            const string HONTEN_GYOMUCD = "05";                   // 本店情報レコード業務コード
            const string HONTEN_GYOMUNAISHU_CD = "9";             // 本店情報レコード業務内種コード
            const string HONTEN_STYMD = "00000000";                  // 本店情報レコード開始年月日
            const string HONTEN_EDYMD = "99999999";                  // 本店情報レコード終了年月日
                                                                     // * corresponds to VS2008 Start 2010/04/16 000015
                                                                     // Dim objErrorStruct As UFErrorStruct                     ' エラー定義構造体
                                                                     // * corresponds to VS2008 End 2010/04/16 000015
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;
            DataSet csDataSet;                                // データセット
            var strSQL = new StringBuilder("");

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成    
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABDainoEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABDainoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.GYOMUCD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.STYMD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_STYMD);
                strSQL.Append(" AND ");
                strSQL.Append(ABDainoEntity.EDYMD);
                strSQL.Append(" = ");
                strSQL.Append(ABDainoEntity.KEY_EDYMD);

                strSQL.Append(";");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成（住民コード）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成（業務コード）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = HONTEN_GYOMUCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成（業務内種コード）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD;
                cfUFParameterClass.Value = HONTEN_GYOMUNAISHU_CD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成（開始年月日）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD;
                cfUFParameterClass.Value = HONTEN_STYMD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 検索条件のパラメータを作成（終了年月日）
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD;
                cfUFParameterClass.Value = HONTEN_EDYMD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csDataSet = m_csDataSchma.Clone();
                csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, true);

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

            return csDataSet;

        }
        // * 履歴番号 000012 2006/12/22 追加終了
        #endregion

    }
}