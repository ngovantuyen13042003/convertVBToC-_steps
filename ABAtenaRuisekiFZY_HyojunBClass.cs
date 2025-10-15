// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        AB宛名累積付随_標準マスタＤＡ(ABAtenaRuisekiFZY_HyojunBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/08/14 早崎  雄矢
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// *
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    // ************************************************************************************************
    // *
    // * 宛名累積付随_標準マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRuisekiFZY_HyojunBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                                          // エラー処理クラス
        private string m_strInsertSQL;                                                // INSERT用SQL
        private string m_strUpdateSQL;                                                // UPDATE用SQL
        private string m_strDelRonriSQL;                                              // 論理削除用SQL
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private DataSet m_csDataSchma;                                                // スキーマ保管用データセット
        private string m_strUpdateDatetime;                                           // 更新日時

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRuisekiFZY_HyojunBClass";      // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード
        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;
        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";
        private const string ERR_JUMINCD = "住民コード";

        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名     コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　       初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public ABAtenaRuisekiFZY_HyojunBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDelRonriSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名累積付随_標準マスタ抽出
        // * 
        // * 構文           Public Function GetABAtenaRuisekiFZYHyojunBClassBHoshu(ByVal strJuminCD As String, _
        // *                                                                       ByVal strRirekiNO As String, _
        // *                                                                       ByVal strShoriNichiji As String, _
        // *                                                                       ByVal strZengoKB As String) As DataSet
        // * 
        // * 機能　　    　 宛名累積付随_標準マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                strShoriNichiji    : 処理日時
        // *                strZengoKB         : 前後区分
        // * 
        // * 戻り値         DataSet : 取得した宛名_標準マスタの該当データ
        // ************************************************************************************************
        public DataSet GetABAtenaRuisekiFZYHyojunBClassBHoshu(string strJuminCD, string strRirekiNO, string strShoriNichiji, string strZengoKB)
        {

            const string THIS_METHOD_NAME = "GetABAtenaRuisekiFZYHyojunBClassBHoshu";
            UFErrorStruct cfErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var csSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 住民コードが指定されていないときエラー
                if (strJuminCD == null || strJuminCD.Trim().RLength() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(cfErrorStruct.m_strErrorMessage + ERR_JUMINCD, cfErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                csSQL.Append(CreateSelect());
                // FROM句の生成
                csSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME);
                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                csSQL.Append(CreateWhere(strJuminCD, strRirekiNO, strShoriNichiji, strZengoKB));

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity, ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csAtenaEntity;

        }

        // ************************************************************************************************
        // * メソッド名     SELECT句の作成
        // * 
        // * 構文           Private Sub CreateSelect() As String
        // * 
        // * 機能　　    　 SELECT句を生成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String    :   SELECT句
        // ************************************************************************************************
        private string CreateSelect()
        {
            const string THIS_METHOD_NAME = "CreateSelect";
            var csSELECT = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の作成
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiFZYHyojunEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.ZENGOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHFRNMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHKANAFRNMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SHIMEIYUSENKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHKANJIHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.ZAIRYUCARDNOKBN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.JUKYOCHIHOSEICD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.HODAI30JO46MATAHA47KB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYHyojunEntity.KOSHINUSER);

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

            return csSELECT.ToString();

        }

        // ************************************************************************************************
        // * メソッド名     WHERE文の作成
        // * 
        // * 構文           Private Function CreateWhere(ByVal strJuminCD As String, _
        // *                                             ByVal strRirekiNO As String, _
        // *                                             ByVal strShoriNichiji As String, _
        // *                                             ByVal strZengoKB As String) As String
        // * 
        // * 機能　　    　 WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           strJuminCD         : 住民コード 
        // *                strRirekiNO        : 履歴番号
        // *                strShoriNichiji    : 処理日時
        // *                strZengoKB         : 前後区分
        // *
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strRirekiNO, string strShoriNichiji, string strZengoKB)
        {

            const string THIS_METHOD_NAME = "CreateWhere";
            StringBuilder csWHERE;
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // WHERE句の作成
                csWHERE = new StringBuilder(256);

                // 住民コード
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiFZYHyojunEntity.JUMINCD, ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;

                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                if (!strRirekiNO.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYHyojunEntity.RIREKINO, ABAtenaRuisekiFZYHyojunEntity.KEY_RIREKINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_RIREKINO;
                    cfUFParameterClass.Value = strRirekiNO;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 処理日時
                if (!strShoriNichiji.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI, ABAtenaRuisekiFZYHyojunEntity.KEY_SHORINICHIJI);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_SHORINICHIJI;
                    cfUFParameterClass.Value = strShoriNichiji;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 前後区分
                if (!strZengoKB.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYHyojunEntity.ZENGOKB, ABAtenaRuisekiFZYHyojunEntity.KEY_ZENGOKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_ZENGOKB;
                    cfUFParameterClass.Value = strZengoKB;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
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

            return csWHERE.ToString();

        }

        #region 宛名累積付随_標準マスタ追加
        // ************************************************************************************************
        // * メソッド名     宛名累積付随_標準マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRuisekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積付随_標準マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRuisekiFZYHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRuisekiFZYHyojunB";
            int intInsCnt;                            // 追加件数

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL) || m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateInsertSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);

                // 共通項目の編集を行う
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.SAKUJOFG] = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow[ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI];
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

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
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義
            UFParameterClass cfUFParameterClass;
            string strParamName;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();
                    strParamName = string.Format("{0}{1}", ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

                // デバッグ終了ログ出力
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
        #endregion

        #region 宛名累積付随_標準マスタ更新
        // ************************************************************************************************
        // * メソッド名     宛名累積付随_標準マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaRuisekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積付随_標準マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaRuisekiFZYHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaRuisekiFZYHyojunB";                     // パラメータクラス
            int intUpdCnt;                            // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null || string.IsNullOrEmpty(m_strUpdateSQL) || m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateUpdateSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER]) + 1m;                  // 更新カウンタ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY.RLength()) == ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY)
                    {

                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

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
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csUpdateParam;                  // UPDATE用SQL定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_SHORINICHIJI);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.ZENGOKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_ZENGOKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・履歴番号・処理日時・前後区分・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.RIREKINO) && !(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI) && !(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.ZENGOKB) && !(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL += csUpdateParam.ToString().TrimEnd(",".ToCharArray());

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += csWhere.ToString();

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
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
        #endregion

        #region 宛名累積付随_標準マスタ削除
        // ************************************************************************************************
        // * メソッド名     宛名累積付随_標準マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaRuisekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積付随_標準マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaRuisekiFZYHyojunB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaRuisekiFZYHyojunB";  // パラメータクラス
            int intDelCnt;        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null || string.IsNullOrEmpty(m_strDelRonriSQL) || m_cfDelRonriUFParameterCollectionClass is null)
                {
                    CreateDeleteRonriSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.TANMATSUID] = m_cfControlData.m_strClientId;      // 端末ＩＤ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.SAKUJOFG] = SAKUJOFG_ON;                          // 削除フラグ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER]) + 1m;                     // 更新カウンタ
                csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINUSER] = m_cfControlData.m_strUserId;        // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY.RLength()) == ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY)
                    {

                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYHyojunEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(
                // m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass);

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
            StringBuilder csWhere;                        // WHERE定義
            StringBuilder csDelRonriParam;                // 論理削除パラメータ定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_SHORINICHIJI);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.ZENGOKB);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_ZENGOKB);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRuisekiFZYHyojunEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_JUMINJUTOGAIKB;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYHyojunEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
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
        #endregion

        #region 更新日時設定
        // ************************************************************************************************
        // * メソッド名     更新日時設定
        // * 
        // * 構文           Private Sub SetUpdateDatetime(ByRef csDate As Object)
        // * 
        // * 機能           未設定のとき更新日時を設定する
        // * 
        // * 引数           csDate As Object : 更新日時の項目
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetUpdateDatetime(ref object csDate)
        {
            try
            {
                // 未設定のとき
                if (csDate is DBNull || UFVBAPI.ToString(csDate).Trim().Equals(string.Empty))
                {
                    csDate = m_strUpdateDatetime;
                }
                else
                {
                    // 処理なし
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion

    }
}
