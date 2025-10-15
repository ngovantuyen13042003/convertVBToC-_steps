// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名累積マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/15　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/10 000001     住所ＣＤ等の整合性チェックに誤り
// * 2003/03/31 000002     整合性チェックをTrimした値でチェックする
// * 2003/04/16 000003     生和暦年月日の日付チェックを数値チェックに変更
// *                       検索用カナの半角カナチェックをＡＮＫチェックに変更
// * 2003/05/20 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000005     RDBアクセスログの修正
// * 2003/09/11 000006     端末ＩＤ整合性チェックをANKにする
// * 2003/10/09 000007     作成ユーザー・更新ユーザーチェックの変更
// * 2003/10/30 000008     仕様変更、カタカナチェックをANKチェックに変更
// * 2003/11/18 000009     仕様変更：項目追加
// * 2003/12/01 000010     仕様変更：項目名の変更(SYORINICHIJI->SHORINICHIJI)
// *                       仕様変更：項目名の変更(KOKUHOTIAHKHONHIKBMEISHO->KOKUHOTISHKHONHIKBMEISHO)
// * 2004/03/06 000011     仕様変更：国保保険証番号のチェックなしに変更
// * 2004/08/13 000012     仕様変更、地区コードチェックをANKチェックに変更
// * 2004/11/12 000013     データチェックを行なわない
// * 2005/12/26 000014     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
// * 2010/04/16 000015     VS2008対応（比嘉）
// * 2011/10/24 000016     【AB17010】＜住基法改正対応＞宛名累積付随マスタ追加   (小松)
// * 2023/08/14 000017    【AB-0820-1】住登外管理項目追加(早崎)
// ************************************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 宛名累積マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRuisekiBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス

        // *履歴番号 000016 2011/10/24 追加開始
        private ABSekoYMDHanteiBClass m_csSekoYMDHanteiB;             // 施行日判定Bｸﾗｽ
        private ABAtenaRuisekiFZYBClass m_csAtenaRuisekiFZYB;         // 宛名累積付随マスタBｸﾗｽ
        private bool m_blnJukihoKaiseiFG = false;
        private string m_strJukihoKaiseiKB;                           // 住基法改正区分
                                                                      // *履歴番号 000016 2011/10/24 追加終了

        // *履歴番号 000017 2023/08/14 追加開始
        private ABAtenaRuiseki_HyojunBClass m_csAtenaRuisekiHyojunB;            // 宛名累積_標準マスタBｸﾗｽ
        private ABAtenaRuisekiFZY_HyojunBClass m_csAtenaRuisekiFZYHyojunB;      // 宛名累積付随_標準マスタBｸﾗｽ
                                                                                // *履歴番号 000017 2023/08/14 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRuisekiBClass";                // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード

        private const string JUKIHOKAISEIKB_ON = "1";

        #endregion

        #region プロパティ
        // *履歴番号 000016 2011/10/24 追加開始
        public string p_strJukihoKaiseiKB      // 住基法改正区分
        {
            set
            {
                m_strJukihoKaiseiKB = value;
            }
        }
        // *履歴番号 000016 2011/10/24 追加終了
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
        // *                               ByVal cfUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // *                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABAtenaRuisekiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strInsertSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;

            // *履歴番号 000016 2011/10/24 追加開始
            m_strJukihoKaiseiKB = string.Empty;

            // 住基法改正ﾌﾗｸﾞ取得
            GetJukihoKaiseiFG();
            // *履歴番号 000016 2011/10/24 追加終了
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名累積マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
        // *                                                          ByVal strYusenKB As String) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // *                strYusenKB    : 優先区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRuiseki(string strJuminCD, string strYusenKB)
        {
            return GetAtenaRuiseki(strJuminCD, "", "", strYusenKB);
        }

        // ************************************************************************************************
        // * メソッド名     宛名累積マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetAtenaRuiseki(ByVal strKaishiNichiji As String, _
        // *                                                          ByVal strSyuryoNichiji As String, _
        // *                                                          ByVal strYusenKB As String) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           strKaishiNichiji  : 開始日時
        // *                strSyuryoNichiji  : 終了日時
        // *                strYusenKB        : 優先区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRuiseki(string strKaishiNichiji, string strSyuryoNichiji, string strYusenKB)

        {
            return GetAtenaRuiseki("", strKaishiNichiji, strSyuryoNichiji, strYusenKB);
        }

        // ************************************************************************************************
        // * メソッド名     宛名累積マスタ抽出
        // * 
        // * 構文           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
        // *                                                          ByVal strKaishiNichiji As String, _
        // *                                                          ByVal strSyuryoNichiji As String, _
        // *                                                          ByVal strYusenKB As String) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strKaishiNichiji  : 開始日時
        // *                strSyuryoNichiji  : 終了日時
        // *                strYusenKB        : 優先区分
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRuiseki(string strJuminCD, string strKaishiNichiji, string strSyuryoNichiji, string strYusenKB)


        {
            const string THIS_METHOD_NAME = "GetAtenaRuiseki";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFParameterClass cfUFParameterClass;          // パラメータクラス
            DataSet csAtenaRuisekiEntity;                 // 宛名累積DataSet
            string strKaishiNichiji2;                     // 開始日時
            string strSyuryoNichiji2;                     // 終了日時
            StringBuilder strSQL;
            StringBuilder strWHERE;
            DataSet csDataSchema;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータコレクションのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータチェック
                // 開始日時チェック
                if (strKaishiNichiji.RLength == 17)
                {
                    strKaishiNichiji2 = strKaishiNichiji;
                }

                else if (strKaishiNichiji.RLength == 8)
                {
                    strKaishiNichiji2 = strKaishiNichiji + "000000000";
                }

                else if (string.IsNullOrEmpty(strKaishiNichiji) & string.IsNullOrEmpty(strSyuryoNichiji))
                {
                    strKaishiNichiji2 = string.Empty;
                }
                else
                {
                    // エラー定義を取得
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KAISHINICHIJI);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 終了日時チェック
                if (strSyuryoNichiji.RLength == 17)
                {
                    strSyuryoNichiji2 = strSyuryoNichiji;
                }

                else if (strSyuryoNichiji.RLength == 8)
                {
                    strSyuryoNichiji2 = strSyuryoNichiji + "000000000";
                }

                else if (string.IsNullOrEmpty(strSyuryoNichiji))
                {
                    strSyuryoNichiji2 = string.Empty;
                }
                else
                {
                    // エラー定義を取得
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_SYURYONICHIJI);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 優先区分
                if (!(strYusenKB == "1" | strYusenKB == "2"))
                {
                    // エラー定義を取得
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_YUSENKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }


                // SQL文の作成
                strSQL = new StringBuilder();
                // *履歴番号 000016 2011/10/24 修正開始
                // strSQL.Append("SELECT * FROM ")
                // strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME)
                // 住基法改正以降は宛名累積付随マスタを付加
                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                {
                    strSQL.AppendFormat("SELECT {0}.*", ABAtenaRuisekiEntity.TABLE_NAME);
                    SetFZYEntity(ref strSQL);
                    strSQL.AppendFormat(" FROM {0}", ABAtenaRuisekiEntity.TABLE_NAME);
                    SetFZYJoin(ref strSQL);
                }
                else
                {
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME);
                }
                // *履歴番号 000016 2011/10/24 修正終了

                // *履歴番号 000016 2011/10/24 追加開始
                csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRuisekiEntity.TABLE_NAME, false);
                // *履歴番号 000016 2011/10/24 追加終了


                strSQL.Append(" WHERE ");

                // WHERE句の作成
                strWHERE = new StringBuilder();
                // 住民コード
                if (!string.IsNullOrEmpty(strJuminCD))
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    // *履歴番号 000016 2011/10/24 追加開始
                    // 住基法改正以降は宛名累積付随マスタを付加
                    if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                    {
                        strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000016 2011/10/24 追加終了
                    strWHERE.Append(ABAtenaRuisekiEntity.JUMINCD);
                    strWHERE.Append(" = ");
                    strWHERE.Append(ABAtenaRuisekiEntity.KEY_JUMINCD);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = strJuminCD;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                // 開始日時
                if (!string.IsNullOrEmpty(strKaishiNichiji2))
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    // *履歴番号 000016 2011/10/24 追加開始
                    // 住基法改正以降は宛名累積付随マスタを付加
                    if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                    {
                        strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000016 2011/10/24 追加終了
                    // *履歴番号 000010 2003/12/01 修正開始
                    // strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                    strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI);
                    // *履歴番号 000010 2003/12/01 修正終了
                    strWHERE.Append(" >= ");
                    strWHERE.Append(ABAtenaRuisekiEntity.KEY_SYORINICHIJI);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_SYORINICHIJI;
                    cfUFParameterClass.Value = strKaishiNichiji2;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                // 終了日時
                if (!string.IsNullOrEmpty(strSyuryoNichiji2))
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    // *履歴番号 000016 2011/10/24 追加開始
                    // 住基法改正以降は宛名累積付随マスタを付加
                    if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                    {
                        strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME);
                    }
                    else
                    {
                        // 処理なし
                    }
                    // *履歴番号 000016 2011/10/24 追加終了
                    // *履歴番号 000010 2003/12/01 修正開始
                    // strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                    strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI);
                    // *履歴番号 000010 2003/12/01 修正終了
                    strWHERE.Append(" <= ");
                    strWHERE.Append(ABAtenaRuisekiEntity.PARAM_SYORINICHIJI);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_SYORINICHIJI;
                    cfUFParameterClass.Value = strSyuryoNichiji2;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                // 優先区分
                if (strYusenKB == "1")
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    strWHERE.Append(ABAtenaRuisekiEntity.JUTOGAIYUSENKB);
                    strWHERE.Append(" = '1'");
                }
                if (strYusenKB == "2")
                {
                    if (!(strWHERE.RLength == 0))
                    {
                        strWHERE.Append(" AND ");
                    }
                    strWHERE.Append(ABAtenaRuisekiEntity.JUMINYUSENIKB);
                    strWHERE.Append(" = '1'");
                }


                // ORDER句を結合
                if (strWHERE.RLength != 0)
                {
                    strSQL.Append(strWHERE);
                }


                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                // *履歴番号 000016 2011/10/24 修正開始
                // csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
                csAtenaRuisekiEntity = csDataSchema.Clone();
                csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRuisekiEntity, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
                // *履歴番号 000016 2011/10/24 修正終了


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

            return csAtenaRuisekiEntity;

        }


        // ************************************************************************************************
        // * メソッド名     宛名履歴マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRB";
            // * corresponds to VS2008 Start 2010/04/16 000015
            // Dim csInstRow As DataRow
            // Dim csDataColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000015
            int intInsCnt;                            // 追加件数
            string strUpdateDateTime;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABAtenaRuisekiEntity.TANMATSUID) = m_cfControlData.m_strClientId;  // 端末ＩＤ
                csDataRow(ABAtenaRuisekiEntity.SAKUJOFG) = "0";                              // 削除フラグ
                csDataRow(ABAtenaRuisekiEntity.KOSHINCOUNTER) = decimal.Zero;                // 更新カウンタ
                csDataRow(ABAtenaRuisekiEntity.SAKUSEINICHIJI) = strUpdateDateTime;          // 作成日時
                csDataRow(ABAtenaRuisekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;   // 作成ユーザー
                csDataRow(ABAtenaRuisekiEntity.KOSHINNICHIJI) = strUpdateDateTime;           // 更新日時
                csDataRow(ABAtenaRuisekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;    // 更新ユーザー

                // *履歴番号 000013 2004/11/12 修正開始
                // 当クラスのデータ整合性チェックを行う
                // For Each csDataColumn In csDataRow.Table.Columns
                // CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
                // Next csDataColumn
                // *履歴番号 000016 2004/11/12 修正終了

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

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

            return intInsCnt;

        }
        // *履歴番号 000016 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名累積マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaB() As Integer
        // * 
        // * 機能　　    　 宛名累積マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名累積）
        // * 　　           csAtenaFZYDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csAtenaDr, DataRow csAtenaFZYDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名累積マスタ追加を実行
                intCnt = InsertAtenaRB(csAtenaDr);

                // 住基法改正以降のとき
                if (!(csAtenaFZYDr == null) && m_blnJukihoKaiseiFG)
                {
                    // 宛名累積付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRuisekiFZYB == null)
                    {
                        m_csAtenaRuisekiFZYB = new ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 作成日時、更新日時の同期
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI);
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI);

                    // 宛名累積付随マスタ追加を実行
                    intCnt2 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr);
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

            return intCnt;

        }

        // *履歴番号 000017 2023/08/14 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名累積マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
        // *                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        // * 
        // * 機能　　    　 宛名累積マスタにデータを追加する
        // * 
        // * 引数           csAtenaDr As DataRow           : 追加するデータの含まれるDataRowオブジェクト（宛名累積）
        // *                csAtenaHyojunDr As DataRow     : 追加するデータの含まれるDataRowオブジェクト（宛名累積_標準）
        // * 　　           csAtenaFZYDr As DataRow        : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随）
        // *                csAtenaFZYHyojunDr As DataRow  : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随_標準）
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaRB(DataRow csAtenaDr, DataRow csAtenaHyojunDr, DataRow csAtenaFZYDr, DataRow csAtenaFZYHyojunDr)
        {
            int intCnt = 0;
            int intCnt2 = 0;
            int intCnt3 = 0;
            int intCnt4 = 0;

            const string THIS_METHOD_NAME = "InsertAtenaRB";

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名累積マスタ追加を実行
                intCnt = InsertAtenaRB(csAtenaDr);

                if (!(csAtenaHyojunDr == null))
                {

                    // 宛名累積_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    if (m_csAtenaRuisekiHyojunB == null)
                    {
                        m_csAtenaRuisekiHyojunB = new ABAtenaRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    }
                    else
                    {
                        // 処理なし
                    }

                    // 宛名累積標準の作成日時と更新日時に宛名累積Rowの作成日時と更新日時をセットする
                    csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI);
                    csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI);

                    // 宛名累積_標準マスタ追加を実行
                    intCnt2 = m_csAtenaRuisekiHyojunB.InsertAtenaRuisekiHyojunB(csAtenaHyojunDr);

                }
                // 住基法改正以降のとき
                if (m_blnJukihoKaiseiFG)
                {

                    // 宛名累積付随Rowが存在する場合
                    if (csAtenaFZYDr is not null)
                    {

                        // 宛名累積付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRuisekiFZYB == null)
                        {
                            m_csAtenaRuisekiFZYB = new ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI);
                        csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI);

                        // 宛名累積付随マスタ追加を実行
                        intCnt3 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr);

                    }

                    // 宛名累積付随_標準Rowが存在する場合
                    if (csAtenaFZYHyojunDr is not null)
                    {

                        // 宛名累積付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                        if (m_csAtenaRuisekiFZYHyojunB == null)
                        {
                            m_csAtenaRuisekiFZYHyojunB = new ABAtenaRuisekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                        }
                        else
                        {
                            // 処理なし
                        }

                        // 作成日時、更新日時の同期
                        csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI);
                        csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI);

                        // 宛名累積付随_標準マスタ追加を実行
                        intCnt4 = m_csAtenaRuisekiFZYHyojunB.InsertAtenaRuisekiFZYHyojunB(csAtenaFZYHyojunDr);

                    }
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

            return intCnt;

        }
        // *履歴番号 000017 2023/08/14 追加終了

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
            UFParameterClass cfUFParameterClass;
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義


            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABAtenaRuisekiEntity.TABLE_NAME + " ";
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

                    csInsertParam.Append(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);


                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + csInsertColumn.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")" + " VALUES (" + csInsertParam.ToString().TrimEnd().TrimEnd(",".ToCharArray()) + ")";

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

        // *履歴番号 000016 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名付随データ項目編集
        // * 
        // * 構文           Private SetFZYEntity()
        // * 
        // * 機能           宛名付随データの項目編集をします。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYEntity(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TABLEINSERTKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.LINKNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINHYOJOTAIKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKYOCHITODOKEFLG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.HONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJITSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KATAKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.UMAREFUSHOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TSUSHOMEITOUROKUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHAMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUCARDNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEISTYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEIEDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSKANAMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE10);

        }
        // ************************************************************************************************
        // * メソッド名     宛名付随テーブルJOIN句作成
        // * 
        // * 構文           Private SetFZYJoin()
        // * 
        // * 機能           宛名付随テーブルのJOIN句を作成します。
        // * 
        // * 引数           strAtenaSQLsb　：　宛名取得用SQL  
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void SetFZYJoin(ref StringBuilder strAtenaSQLsb)
        {
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRuisekiFZYEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.JUMINCD, ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINCD);

            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.RIREKINO, ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RIREKINO);

            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.SHORINICHIJI, ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.SHORINICHIJI);

            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.ZENGOKB, ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZENGOKB);

        }
        // *履歴番号 000016 2011/10/24 追加終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
        // * 
        // * 機能           更新データの整合性をチェックする。
        // * 
        // * 引数           strColumnName As String : 宛名履歴マスタデータセットの項目名
        // *                strValue As String     : 項目に対応する値
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {

            const string THIS_METHOD_NAME = "CheckColumnValue";
            const string TABLENAME = "宛名累積．";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体

            try
            {
                // デバッグ開始ログ出力
                // m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

                // 日付クラスのインスタンス化
                if (m_cfDateClass == null)
                {
                    m_cfDateClass = new UFDateClass(m_cfConfigDataClass);
                    // 日付クラスの必要な設定を行う
                    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                }

                switch (strColumnName.ToUpper() ?? "")
                {

                    case var @case when @case == ABAtenaRuisekiEntity.JUMINCD:            // 住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case1 when case1 == ABAtenaRuisekiEntity.SHICHOSONCD:        // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case2 when case2 == ABAtenaRuisekiEntity.KYUSHICHOSONCD:     // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case3 when case3 == ABAtenaRuisekiEntity.RIREKINO:           // 履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RIREKINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // *履歴番号 000010 2003/12/01 修正開始
                    // Case ABAtenaRuisekiEntity.SYORINICHIJI      '処理日時
                    case var case4 when case4 == ABAtenaRuisekiEntity.SHORINICHIJI:      // 処理日時
                        {
                            // *履歴番号 000010 2003/12/01 修正終了
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SYORINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABAtenaRuisekiEntity.ZENGOKB:           // 前後区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZENGOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case6 when case6 == ABAtenaRuisekiEntity.RRKST_YMD:          // 履歴開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case7 when case7 == ABAtenaRuisekiEntity.RRKED_YMD:          // 履歴終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000" | strValue == "99999999"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case8 when case8 == ABAtenaRuisekiEntity.JUMINJUTOGAIKB:     // 住民住登外区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINJUTOGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case9 when case9 == ABAtenaRuisekiEntity.JUMINYUSENIKB:      // 住民優先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINYUSENIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case10 when case10 == ABAtenaRuisekiEntity.JUTOGAIYUSENKB:     // 住登外優先区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTOGAIYUSENKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case11 when case11 == ABAtenaRuisekiEntity.ATENADATAKB:        // 宛名データ区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATAKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case12 when case12 == ABAtenaRuisekiEntity.STAICD:             // 世帯コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case13 when case13 == ABAtenaRuisekiEntity.JUMINHYOCD:         // 住民票コード
                        {
                            break;
                        }
                    // チェックなし

                    case var case14 when case14 == ABAtenaRuisekiEntity.SEIRINO:            // 整理番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIRINO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case15 when case15 == ABAtenaRuisekiEntity.ATENADATASHU:       // 宛名データ種別
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATASHU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case16 when case16 == ABAtenaRuisekiEntity.HANYOKB1:           // 汎用区分1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case17 when case17 == ABAtenaRuisekiEntity.KJNHJNKB:           // 個人法人区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KJNHJNKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case18 when case18 == ABAtenaRuisekiEntity.HANYOKB2:           // 汎用区分2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case19 when case19 == ABAtenaRuisekiEntity.KANNAIKANGAIKB:     // 管内管外区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANNAIKANGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case20 when case20 == ABAtenaRuisekiEntity.KANAMEISHO1:        // カナ名称1
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case21 when case21 == ABAtenaRuisekiEntity.KANJIMEISHO1:       // 漢字名称1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case22 when case22 == ABAtenaRuisekiEntity.KANAMEISHO2:        // カナ名称2
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case23 when case23 == ABAtenaRuisekiEntity.KANJIMEISHO2:       // 漢字名称2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case24 when case24 == ABAtenaRuisekiEntity.KANJIHJNKEITAI:     // 漢字法人形態
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNKEITAI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case25 when case25 == ABAtenaRuisekiEntity.KANJIHJNDAIHYOSHSHIMEI:   // 漢字法人代表者氏名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case26 when case26 == ABAtenaRuisekiEntity.SEARCHKANJIMEISHO:  // 検索用漢字名称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEARCHKANJIMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case27 when case27 == ABAtenaRuisekiEntity.KYUSEI:             // 旧姓
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case28 when case28 == ABAtenaRuisekiEntity.SEARCHKANASEIMEI:   // 検索用カナ姓名
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓名", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case29 when case29 == ABAtenaRuisekiEntity.SEARCHKANASEI:      // 検索用カナ姓
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case30 when case30 == ABAtenaRuisekiEntity.SEARCHKANAMEI:      // 検索用カナ名
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ名", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case31 when case31 == ABAtenaRuisekiEntity.JUKIRRKNO:          // 住基履歴番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIRRKNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // Case ABAtenaRuisekiEntity.UMAREYMD           '生年月日
                    // If Not (strValue = String.Empty Or strValue = "00000000") Then
                    // m_cfDateClass.p_strDateValue = strValue
                    // If (Not m_cfDateClass.CheckDate()) Then
                    // 'エラー定義を取得
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_UMAREYMD)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // End If
                    // End If

                    // Case ABAtenaRuisekiEntity.UMAREWMD           '生和暦年月日
                    // If (Not UFStringClass.CheckNumber(strValue)) Then
                    // 'エラー定義を取得(数字項目入力の誤りです。：)
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode)
                    // End If

                    case var case32 when case32 == ABAtenaRuisekiEntity.SEIBETSUCD:         // 性別コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case33 when case33 == ABAtenaRuisekiEntity.SEIBETSU:           // 性別
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case34 when case34 == ABAtenaRuisekiEntity.SEKINO:             // 籍番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEKINO);
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case35 when case35 == ABAtenaRuisekiEntity.JUMINHYOHYOJIJUN:   // 住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case36 when case36 == ABAtenaRuisekiEntity.ZOKUGARACD:         // 続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case37 when case37 == ABAtenaRuisekiEntity.ZOKUGARA:           // 続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case38 when case38 == ABAtenaRuisekiEntity.DAI2JUMINHYOHYOJIJUN:     // 第２住民票表示順
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case39 when case39 == ABAtenaRuisekiEntity.DAI2ZOKUGARACD:           // 第２続柄コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimEnd()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case40 when case40 == ABAtenaRuisekiEntity.DAI2ZOKUGARA:             // 第２続柄
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case41 when case41 == ABAtenaRuisekiEntity.STAINUSJUMINCD:     // 世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case42 when case42 == ABAtenaRuisekiEntity.STAINUSMEI:         // 世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case43 when case43 == ABAtenaRuisekiEntity.KANASTAINUSMEI:     // カナ世帯主名
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANASTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case44 when case44 == ABAtenaRuisekiEntity.DAI2STAINUSJUMINCD:       // 第２世帯主住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case45 when case45 == ABAtenaRuisekiEntity.DAI2STAINUSMEI:           // 第２世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case46 when case46 == ABAtenaRuisekiEntity.KANADAI2STAINUSMEI:       // 第２カナ世帯主名
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANADAI2STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case47 when case47 == ABAtenaRuisekiEntity.YUBINNO:            // 郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case48 when case48 == ABAtenaRuisekiEntity.JUSHOCD:            // 住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case49 when case49 == ABAtenaRuisekiEntity.JUSHO:              // 住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case50 when case50 == ABAtenaRuisekiEntity.BANCHICD1:          // 番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case51 when case51 == ABAtenaRuisekiEntity.BANCHICD2:          // 番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case52 when case52 == ABAtenaRuisekiEntity.BANCHICD3:          // 番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case53 when case53 == ABAtenaRuisekiEntity.BANCHI:             // 番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case54 when case54 == ABAtenaRuisekiEntity.KATAGAKIFG:         // 方書フラグ
                        {
                            if (!string.IsNullOrEmpty(strValue.Trim()))
                            {
                                if (!UFStringClass.CheckNumber(strValue))
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKIFG);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case55 when case55 == ABAtenaRuisekiEntity.KATAGAKICD:         // 方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case56 when case56 == ABAtenaRuisekiEntity.KATAGAKI:           // 方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case57 when case57 == ABAtenaRuisekiEntity.RENRAKUSAKI1:       // 連絡先1
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case58 when case58 == ABAtenaRuisekiEntity.RENRAKUSAKI2:       // 連絡先2
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case59 when case59 == ABAtenaRuisekiEntity.HON_ZJUSHOCD:       // 本籍全国住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case60 when case60 == ABAtenaRuisekiEntity.HON_JUSHO:          // 本籍住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case61 when case61 == ABAtenaRuisekiEntity.HONSEKIBANCHI:      // 本籍番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HONSEKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case62 when case62 == ABAtenaRuisekiEntity.HITTOSH:            // 筆頭者
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HITTOSH);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case63 when case63 == ABAtenaRuisekiEntity.CKINIDOYMD:         // 直近異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case64 when case64 == ABAtenaRuisekiEntity.CKINJIYUCD:         // 直近事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case65 when case65 == ABAtenaRuisekiEntity.CKINJIYU:           // 直近事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case66 when case66 == ABAtenaRuisekiEntity.CKINTDKDYMD:        // 直近届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case67 when case67 == ABAtenaRuisekiEntity.CKINTDKDTUCIKB:     // 直近届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case68 when case68 == ABAtenaRuisekiEntity.TOROKUIDOYMD:       // 登録異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case69 when case69 == ABAtenaRuisekiEntity.TOROKUIDOWMD:       // 登録異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case70 when case70 == ABAtenaRuisekiEntity.TOROKUJIYUCD:       // 登録事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case71 when case71 == ABAtenaRuisekiEntity.TOROKUJIYU:         // 登録事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case72 when case72 == ABAtenaRuisekiEntity.TOROKUTDKDYMD:      // 登録届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case73 when case73 == ABAtenaRuisekiEntity.TOROKUTDKDWMD:      // 登録届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case74 when case74 == ABAtenaRuisekiEntity.TOROKUTDKDTUCIKB:   // 登録届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case75 when case75 == ABAtenaRuisekiEntity.JUTEIIDOYMD:        // 住定異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case76 when case76 == ABAtenaRuisekiEntity.JUTEIIDOWMD:        // 住定異動和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case77 when case77 == ABAtenaRuisekiEntity.JUTEIJIYUCD:        // 住定事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case78 when case78 == ABAtenaRuisekiEntity.JUTEIJIYU:          // 住定事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case79 when case79 == ABAtenaRuisekiEntity.JUTEITDKDYMD:       // 住定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case80 when case80 == ABAtenaRuisekiEntity.JUTEITDKDWMD:       // 住定届出和暦年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "0000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDWMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case81 when case81 == ABAtenaRuisekiEntity.JUTEITDKDTUCIKB:    // 住定届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case82 when case82 == ABAtenaRuisekiEntity.SHOJOIDOYMD:        // 消除異動年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case83 when case83 == ABAtenaRuisekiEntity.SHOJOJIYUCD:        // 消除事由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case84 when case84 == ABAtenaRuisekiEntity.SHOJOJIYU:          // 消除事由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case85 when case85 == ABAtenaRuisekiEntity.SHOJOTDKDYMD:       // 消除届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case86 when case86 == ABAtenaRuisekiEntity.SHOJOTDKDTUCIKB:    // 消除届出通知区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDTUCIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case87 when case87 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIIDOYMD:     // 転出予定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case88 when case88 == ABAtenaRuisekiEntity.TENSHUTSUKKTIIDOYMD:      // 転出確定届出年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case89 when case89 == ABAtenaRuisekiEntity.TENSHUTSUKKTITSUCHIYMD:   // 転出確定通知年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case90 when case90 == ABAtenaRuisekiEntity.TENSHUTSUNYURIYUCD:       // 転出入理由コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case91 when case91 == ABAtenaRuisekiEntity.TENSHUTSUNYURIYU:         // 転出入理由
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case92 when case92 == ABAtenaRuisekiEntity.TENUMAEJ_YUBINNO:         // 転入前住所郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_YUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case93 when case93 == ABAtenaRuisekiEntity.TENUMAEJ_ZJUSHOCD:        // 転入前住所全国住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case94 when case94 == ABAtenaRuisekiEntity.TENUMAEJ_JUSHO:           // 転入前住所住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case95 when case95 == ABAtenaRuisekiEntity.TENUMAEJ_BANCHI:          // 転入前住所番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case96 when case96 == ABAtenaRuisekiEntity.TENUMAEJ_KATAGAKI:        // 転入前住所方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case97 when case97 == ABAtenaRuisekiEntity.TENUMAEJ_STAINUSMEI:      // 転入前住所世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_STAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case98 when case98 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIYUBINNO:    // 転出予定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case99 when case99 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIZJUSHOCD:   // 転出予定全国住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case100 when case100 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIJUSHO:      // 転出予定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case101 when case101 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIBANCHI:     // 転出予定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case102 when case102 == ABAtenaRuisekiEntity.TENSHUTSUYOTEIKATAGAKI:   // 転出予定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case103 when case103 == ABAtenaRuisekiEntity.TENSHUTSUYOTEISTAINUSMEI: // 転出予定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case104 when case104 == ABAtenaRuisekiEntity.TENSHUTSUKKTIYUBINNO:     // 転出確定郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case105 when case105 == ABAtenaRuisekiEntity.TENSHUTSUKKTIZJUSHOCD:    // 転出確定全国住所コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case106 when case106 == ABAtenaRuisekiEntity.TENSHUTSUKKTIJUSHO:     // 転出確定住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case107 when case107 == ABAtenaRuisekiEntity.TENSHUTSUKKTIBANCHI:      // 転出確定番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case108 when case108 == ABAtenaRuisekiEntity.TENSHUTSUKKTIKATAGAKI:    // 転出確定方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case109 when case109 == ABAtenaRuisekiEntity.TENSHUTSUKKTISTAINUSMEI:  // 転出確定世帯主名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case110 when case110 == ABAtenaRuisekiEntity.TENSHUTSUKKTIMITDKFG:     // 転出確定未届フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case111 when case111 == ABAtenaRuisekiEntity.BIKOYMD:                  // 備考年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case112 when case112 == ABAtenaRuisekiEntity.BIKO:                     // 備考
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case113 when case113 == ABAtenaRuisekiEntity.BIKOTENSHUTSUKKTIJUSHOFG: // 備考転出確定住所フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case114 when case114 == ABAtenaRuisekiEntity.HANNO:                    // 版番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case115 when case115 == ABAtenaRuisekiEntity.KAISEIATOFG:              // 改製後フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIATOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case116 when case116 == ABAtenaRuisekiEntity.KAISEIMAEFG:             // 改製前フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIMAEFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case117 when case117 == ABAtenaRuisekiEntity.KAISEIYMD:                // 改製年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case118 when case118 == ABAtenaRuisekiEntity.GYOSEIKUCD:               // 行政区コード
                        {
                            // * 履歴番号 000014 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000014 2005/12/26 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case119 when case119 == ABAtenaRuisekiEntity.GYOSEIKUMEI:              // 行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case120 when case120 == ABAtenaRuisekiEntity.CHIKUCD1:                 // 地区コード1
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case121 when case121 == ABAtenaRuisekiEntity.CHIKUMEI1:                // 地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case122 when case122 == ABAtenaRuisekiEntity.CHIKUCD2:                 // 地区コード2
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case123 when case123 == ABAtenaRuisekiEntity.CHIKUMEI2:                // 地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case124 when case124 == ABAtenaRuisekiEntity.CHIKUCD3:                 // 地区コード3
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case125 when case125 == ABAtenaRuisekiEntity.CHIKUMEI3:                // 地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case126 when case126 == ABAtenaRuisekiEntity.TOHYOKUCD:                // 投票区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOHYOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case127 when case127 == ABAtenaRuisekiEntity.SHOGAKKOKUCD:             // 小学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case128 when case128 == ABAtenaRuisekiEntity.CHUGAKKOKUCD:             // 中学校区コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHUGAKKOKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case129 when case129 == ABAtenaRuisekiEntity.HOGOSHAJUMINCD:           // 保護者住民コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HOGOSHAJUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case130 when case130 == ABAtenaRuisekiEntity.KANJIHOGOSHAMEI:          // 漢字保護者名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case131 when case131 == ABAtenaRuisekiEntity.KANAHOGOSHAMEI:           // カナ保護者名
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAHOGOSHAMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case132 when case132 == ABAtenaRuisekiEntity.KIKAYMD:                  // 帰化年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KIKAYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case133 when case133 == ABAtenaRuisekiEntity.KARIIDOKB:                // 仮異動区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KARIIDOKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case134 when case134 == ABAtenaRuisekiEntity.SHORITEISHIKB:            // 処理停止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORITEISHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case135 when case135 == ABAtenaRuisekiEntity.JUKIYUBINNO:              // 住基郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIYUBINNO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case136 when case136 == ABAtenaRuisekiEntity.SHORIYOKUSHIKB:           // 処理抑止区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORIYOKUSHIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case137 when case137 == ABAtenaRuisekiEntity.JUKIJUSHOCD:              // 住基住所コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case138 when case138 == ABAtenaRuisekiEntity.JUKIJUSHO:                // 住基住所
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case139 when case139 == ABAtenaRuisekiEntity.JUKIBANCHICD1:            // 住基番地コード1
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case140 when case140 == ABAtenaRuisekiEntity.JUKIBANCHICD2:            // 住基番地コード2
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case141 when case141 == ABAtenaRuisekiEntity.JUKIBANCHICD3:            // 住基番地コード3
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case142 when case142 == ABAtenaRuisekiEntity.JUKIBANCHI:               // 住基番地
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case143 when case143 == ABAtenaRuisekiEntity.JUKIKATAGAKIFG:           // 住基方書フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKIFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case144 when case144 == ABAtenaRuisekiEntity.JUKIKATAGAKICD:           // 住基方書コード
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case145 when case145 == ABAtenaRuisekiEntity.JUKIKATAGAKI:             // 住基方書
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case146 when case146 == ABAtenaRuisekiEntity.JUKIGYOSEIKUCD:           // 住基行政区コード
                        {
                            // * 履歴番号 000014 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000014 2005/12/26 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case147 when case147 == ABAtenaRuisekiEntity.JUKIGYOSEIKUMEI:          // 住基行政区名
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case148 when case148 == ABAtenaRuisekiEntity.JUKICHIKUCD1:                    // 住基地区コード1
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case149 when case149 == ABAtenaRuisekiEntity.JUKICHIKUMEI1:            // 住基地区名1
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case150 when case150 == ABAtenaRuisekiEntity.JUKICHIKUCD2:             // 住基地区コード2
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case151 when case151 == ABAtenaRuisekiEntity.JUKICHIKUMEI2:            // 住基地区名2
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case152 when case152 == ABAtenaRuisekiEntity.JUKICHIKUCD3:             // 住基地区コード3
                        {
                            // *履歴番号 00012 2004/08/13 修正開始
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 00012 2004/08/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case153 when case153 == ABAtenaRuisekiEntity.JUKICHIKUMEI3:            // 住基地区名3
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case154 when case154 == ABAtenaRuisekiEntity.KAOKUSHIKIKB:             // 家屋敷区分
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAOKUSHIKIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case155 when case155 == ABAtenaRuisekiEntity.BIKOZEIMOKU:              // 備考税目
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOZEIMOKU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case156 when case156 == ABAtenaRuisekiEntity.KOKUSEKICD:               // 国籍コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case157 when case157 == ABAtenaRuisekiEntity.KOKUSEKI:                 // 国籍
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case158 when case158 == ABAtenaRuisekiEntity.ZAIRYUSKAKCD:             // 在留資格コード
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAKCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case159 when case159 == ABAtenaRuisekiEntity.ZAIRYUSKAK:               // 在留資格
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAK);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case160 when case160 == ABAtenaRuisekiEntity.ZAIRYUKIKAN:              // 在留期間
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUKIKAN);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case161 when case161 == ABAtenaRuisekiEntity.ZAIRYU_ST_YMD:            // 在留開始年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ST_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case162 when case162 == ABAtenaRuisekiEntity.ZAIRYU_ED_YMD:            // 在留終了年月日
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ED_YMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    // *履歴番号 000009 2003/11/18 追加開始
                    case var case163 when case163 == ABAtenaRuisekiEntity.KSNENKNNO:
                    case var case164 when case164 == ABAtenaRuisekiEntity.JKYNENKNKIGO1:
                    case var case165 when case165 == ABAtenaRuisekiEntity.JKYNENKNNO1:
                    case var case166 when case166 == ABAtenaRuisekiEntity.JKYNENKNEDABAN1:
                    case var case167 when case167 == ABAtenaRuisekiEntity.JKYNENKNKB1:
                    case var case168 when case168 == ABAtenaRuisekiEntity.JKYNENKNKIGO2:
                    case var case169 when case169 == ABAtenaRuisekiEntity.JKYNENKNNO2:
                    case var case170 when case170 == ABAtenaRuisekiEntity.JKYNENKNEDABAN2:
                    case var case171 when case171 == ABAtenaRuisekiEntity.JKYNENKNKB2:
                    case var case172 when case172 == ABAtenaRuisekiEntity.JKYNENKNKIGO3:
                    case var case173 when case173 == ABAtenaRuisekiEntity.JKYNENKNNO3:
                    case var case174 when case174 == ABAtenaRuisekiEntity.JKYNENKNEDABAN3:
                    case var case175 when case175 == ABAtenaRuisekiEntity.JKYNENKNKB3:
                    case var case176 when case176 == ABAtenaRuisekiEntity.KOKUHOSHIKAKUKB:
                        {
                            // 基礎年金番号
                            // 受給年金記号１
                            // 受給年金番号１
                            // 受給年金枝番１
                            // 受給年金区分１
                            // 受給年金記号２
                            // 受給年金番号２
                            // 受給年金枝番２
                            // 受給年金区分２
                            // 受給年金記号３
                            // 受給年金番号３
                            // 受給年金枝番３
                            // 受給年金区分３
                            // 国保資格区分
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case177 when case177 == ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD:
                    case var case178 when case178 == ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD:
                    case var case179 when case179 == ABAtenaRuisekiEntity.KOKUHOSHUTOKUYMD:
                    case var case180 when case180 == ABAtenaRuisekiEntity.KOKUHOSOSHITSUYMD:
                    case var case181 when case181 == ABAtenaRuisekiEntity.KOKUHOTISHKGAITOYMD:
                    case var case182 when case182 == ABAtenaRuisekiEntity.KOKUHOTISHKHIGAITOYMD:
                        {
                            // 年金資格取得年月日
                            // 年金資格喪失年月日
                            // 国保取得年月日
                            // 国保喪失年月日
                            // 国保退職該当年月日
                            // 国保退職非該当年月日
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case183 when case183 == ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU:
                    case var case184 when case184 == ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD:
                    case var case185 when case185 == ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD:
                    case var case186 when case186 == ABAtenaRuisekiEntity.JKYNENKNSHU1:
                    case var case187 when case187 == ABAtenaRuisekiEntity.JKYNENKNSHU2:
                    case var case188 when case188 == ABAtenaRuisekiEntity.JKYNENKNSHU3:
                    case var case189 when case189 == ABAtenaRuisekiEntity.KOKUHONO:
                    case var case190 when case190 == ABAtenaRuisekiEntity.KOKUHOGAKUENKB:
                    case var case191 when case191 == ABAtenaRuisekiEntity.KOKUHOTISHKKB:
                    case var case192 when case192 == ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKB:
                        {
                            // 年金資格取得種別
                            // 年金資格取得理由コード
                            // 年金資格喪失理由コード
                            // 受給年金種別１
                            // 受給年金種別２
                            // 受給年金種別３
                            // 国保番号
                            // 国保学遠区分
                            // 国保退職区分
                            // 国保退職本被区分
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // *履歴番号 000010 2003/12/01 修正開始
                    // Case ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO, _
                    // ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO, _
                    // ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO, _
                    // ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO, _
                    // ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO, _
                    // ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO, _
                    // ABAtenaRuisekiEntity.KOKUHOTIAHKHONHIKBMEISHO, _
                    // ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO, _
                    // ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO, _
                    // ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                    case var case193 when case193 == ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO:
                    case var case194 when case194 == ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO:
                    case var case195 when case195 == ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO:
                    case var case196 when case196 == ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO:
                    case var case197 when case197 == ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO:
                    case var case198 when case198 == ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO:
                    case var case199 when case199 == ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBMEISHO:
                    case var case200 when case200 == ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO:
                    case var case201 when case201 == ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO: // , _
                        {
                            // *履歴番号 000011 2004/03/06 修正開始
                            // ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                            // *履歴番号 000011 2004/03/06 修正開始
                            // *履歴番号 000010 2003/12/01 修正終了
                            // 国保資格区分正式名称
                            // 国保資格区分略式名称
                            // 国保学遠区分正式名称
                            // 国保学遠区分略式名称
                            // 国保退職区分正式名称
                            // 国保退職区分略式名称
                            // 国保退職本被区分正式名称
                            // 国保退職本被区分略式名称
                            // 国保保険証記号
                            // 国保保険証番号
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    // *履歴番号 000009 2003/11/18 追加終了

                    case var case202 when case202 == ABAtenaRuisekiEntity.RESERCE:                  // リザーブ
                        {
                            break;
                        }
                    // チェックなし

                    case var case203 when case203 == ABAtenaRuisekiEntity.TANMATSUID:               // 端末ＩＤ
                        {
                            // * 履歴番号 000006 2003/09/11 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000006 2003/09/11 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case204 when case204 == ABAtenaRuisekiEntity.SAKUJOFG:                 // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case205 when case205 == ABAtenaRuisekiEntity.KOSHINCOUNTER:            // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case206 when case206 == ABAtenaRuisekiEntity.SAKUSEINICHIJI:           // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case207 when case207 == ABAtenaRuisekiEntity.SAKUSEIUSER:              // 作成ユーザ
                        {
                            // * 履歴番号 000007 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000007 2003/10/09 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case208 when case208 == ABAtenaRuisekiEntity.KOSHINNICHIJI:            // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case209 when case209 == ABAtenaRuisekiEntity.KOSHINUSER:               // 更新ユーザ
                        {
                            // * 履歴番号 000007 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000007 2003/10/09 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                }
            }

            // デバッグ終了ログ出力
            // m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをスローする
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

        // *履歴番号 000016 2011/10/24 追加開始
        // ************************************************************************************************
        // * メソッド名       住基法改正ﾌﾗｸﾞ取得
        // * 
        // * 構文             Private Function GetJukihoKaiseiFG()
        // * 
        // * 機能　　    　   管理情報を取得する
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void GetJukihoKaiseiFG()
        {
            const string THIS_METHOD_NAME = "GetJukihoKaiseiFG";
            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (m_csSekoYMDHanteiB == null)
                {
                    // 施行日判定Ｂｸﾗｽのｲﾝｽﾀﾝｽ化
                    m_csSekoYMDHanteiB = new ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                    // 住基法改正ﾌﾗｸﾞ＝施行日判定結果
                    m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD();
                }
                else
                {
                    // 処理なし
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
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


                // システムエラーをスローする
                throw objExp;

            }
        }
        // *履歴番号 000016 2011/10/24 追加終了

        #endregion

    }
}
