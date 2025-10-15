// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名累積付随マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2011/10/24　小松　知尚
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// ************************************************************************************************
using System;
using System.Data;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    // ************************************************************************************************
    // *
    // * 宛名累積付随マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRuisekiFZYBClass
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
        private DataSet m_csDataSchma;                        // スキーマ保管用データセット
        private string m_strUpdateDatetime;                   // 更新日時

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRuisekiFZYBClass";                // クラス名
        private const string THIS_BUSINESSID = "AB";                                  // 業務コード

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private const string ERR_JUMINCD = "住民コード";

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
        public ABAtenaRuisekiFZYBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
        }
        #endregion

        #region メソッド
        #region 宛名累積付随マスタ抽出
        // ************************************************************************************************
        // * メソッド名    宛名累積付随マスタ抽出
        // * 
        // * 構文          Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String, _
        // *                                                ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　宛名累積付随マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD        : 住民コード
        // *                strRrkNo          : 履歴番号
        // *                strShoriYMD       : 処理日時
        // *                strZengoKB        : 前後区分
        // * 
        // * 戻り値         DataSet : 取得した宛名累積付随マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaFZYRBHoshu(string strJuminCD, string strRrkNo, string strShoriYMD, string strZengoKB)


        {
            const string THIS_METHOD_NAME = "GetAtenaFZYRBHoshu";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRuisekiEntity;                  // 宛名累積データセット
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 住民コードが指定されていないときエラー
                if (strJuminCD == null || strJuminCD.Trim().RLength == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiFZYEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRuisekiFZYEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strRrkNo, strShoriYMD, strZengoKB));

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
                // SQLの実行 DataSetの取得
                csAtenaRuisekiEntity = m_csDataSchma.Clone();
                csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRuisekiEntity, ABAtenaRuisekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csAtenaRuisekiEntity;

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
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiFZYEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KYUSHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SHORINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZENGOKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TABLEINSERTKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.LINKNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUMINHYOJOTAIKBN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKYOCHITODOKEFLG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.HONGOKUMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANAHONGOKUMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANJIHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANJITSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KATAKANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.UMAREFUSHOKBN);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TSUSHOMEITOUROKUYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUKIKANCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUKIKANMEISHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUSHACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUSHAMEISHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.ZAIRYUCARDNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYOTEISTYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOFUYOTEIEDYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYU);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.FRNSTAINUSMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.FRNSTAINUSKANAMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSKANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.STAINUSKANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE6);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE7);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE8);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE9);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.RESERVE10);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRuisekiFZYEntity.KOSHINUSER);

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
        // * 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　    　　WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private string CreateWhere(string strJuminCD, string strRrkNo, string strShoriYMD, string strZengoKB)


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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiFZYEntity.JUMINCD, ABAtenaRuisekiFZYEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                if (!strRrkNo.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.RIREKINO, ABAtenaRuisekiFZYEntity.KEY_RIREKINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_RIREKINO;
                    cfUFParameterClass.Value = strRrkNo;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 処理日時
                if (!strShoriYMD.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.SHORINICHIJI, ABAtenaRuisekiFZYEntity.KEY_SHORINICHIJI);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_SHORINICHIJI;
                    cfUFParameterClass.Value = strShoriYMD;
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
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiFZYEntity.ZENGOKB, ABAtenaRuisekiFZYEntity.KEY_ZENGOKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiFZYEntity.KEY_ZENGOKB;
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

        #endregion

        #region 宛名累積付随マスタ追加　[InsertAtenaFZYRB]
        // ************************************************************************************************
        // * メソッド名     宛名累積付随マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名累積付随マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaFZYRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRB";
            int intInsCnt;                            // 追加件数

            try
            {

                // デバッグ開始ログ出力
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

                // 共通項目の編集を行う
                csDataRow(ABAtenaRuisekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId;  // 端末ＩＤ
                csDataRow(ABAtenaRuisekiFZYEntity.SAKUJOFG) = SAKUJOFG_OFF;                     // 削除フラグ
                csDataRow(ABAtenaRuisekiFZYEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;           // 更新カウンタ
                csDataRow(ABAtenaRuisekiFZYEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;   // 作成ユーザー
                csDataRow(ABAtenaRuisekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId;    // 更新ユーザー

                // 作成日時、更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABAtenaFZYEntity.SAKUSEINICHIJI);
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow(ABAtenaFZYEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiFZYEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

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
        private void CreateInsertSQL(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義
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
                    strParamName = string.Format("{0}{1}", ABAtenaRuisekiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaRuisekiFZYEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));



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

        #region その他
        // ************************************************************************************************
        // * メソッド名     更新日時設定
        // * 
        // * 構文           Private Sub SetUpdateDatetime()
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
                if (csDate is DBNull || Conversions.ToString(csDate).Trim().Equals(string.Empty))
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
