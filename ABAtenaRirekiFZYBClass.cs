// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ宛名履歴付随マスタＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2011/10/24　小松　知尚
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/12/07  000001     【AB-9000-1】住基更新連携標準化対応(下村)
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
    // * 宛名履歴付随マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABAtenaRirekiFZYBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private string m_strDelFromJuminCDSQL;                // 物理削除用SQL(１住民コード指定)
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelFromJuminCDPrmCollection;           // 物理削除用SQL(１住民コード指定)
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
        private string m_strUpdateDatetime;                   // 更新日時

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaRirekiFZYBClass";                 // クラス名
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
        public ABAtenaRirekiFZYBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_strDelButuriSQL = string.Empty;
            m_strDelFromJuminCDSQL = string.Empty;
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;
            m_cfDelButuriUFParameterCollectionClass = null;
            m_cfDelFromJuminCDPrmCollection = null;
        }
        #endregion

        #region メソッド
        #region 宛名履歴付随マスタ抽出　[GetAtenaFZYRBHoshu]
        // ************************************************************************************************
        // * メソッド名    宛名履歴付随マスタ抽出
        // * 
        // * 構文          Public Function GetAtenaFZYRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String) As DataSet
        // * 
        // * 機能　　    　住登外マスタより該当データを取得する
        // * 
        // * 引数          strJuminCD         : 住民コード 
        // *               strRrkNo           : 履歴番号
        // *               strJuminJutogaiKB  : 住民住登外区分
        // * 
        // * 戻り値        DataSet : 取得した宛名履歴付随マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaFZYRBHoshu(string strJuminCD, string strRrkNo, string strJuminJutogaiKB)

        {
            return GetAtenaFZYRBHoshu(strJuminCD, strRrkNo, strJuminJutogaiKB, false);
        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String, _
        // *                                                ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名履歴付随マスタより該当データを取得する
        // * 
        // * 引数          strJuminCD     : 住民コード 
        // *               strRrkNo       : 履歴番号
        // *               strJuminJutogaiKB  : 住民住登外区分
        // *               blnSakujoFG    : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴付随マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaFZYRBHoshu(string strJuminCD, string strRrkNo, string strJuminJutogaiKB, bool blnSakujoFG)


        {
            const string THIS_METHOD_NAME = "GetAtenaFZYRBHoshu";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRirekiEntity;                  // 宛名履歴データセット
            var strSQL = new StringBuilder();

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
                strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strJuminCD, strRrkNo, strJuminJutogaiKB, blnSakujoFG));

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaRirekiEntity = m_csDataSchma.Clone();
                csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csAtenaRirekiEntity;

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
                csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiFZYEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KYUSHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TABLEINSERTKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.LINKNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKYOCHITODOKEFLG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.HONGOKUMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHONGOKUMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJIHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJITSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.UMAREFUSHOKBN);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TSUSHOMEITOUROKUYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANMEISHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHACD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHAMEISHO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUCARDNO);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEISTYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEIEDYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYU);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSKANAMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE6);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE7);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE8);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE9);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE10);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINUSER);

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
        private string CreateWhere(string strJuminCD, string strRrkNo, string strJuminJutogaiKB, bool blnSakujoFG)


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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                if (!strRrkNo.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.RIREKINO, ABAtenaRirekiFZYEntity.KEY_RIREKINO);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO;
                    cfUFParameterClass.Value = strRrkNo;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 住民住登外区分
                if (!strJuminJutogaiKB.Trim().Equals(string.Empty))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB, ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB;
                    cfUFParameterClass.Value = strJuminJutogaiKB;
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                    // 処理なし
                }

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON);
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

        #region 宛名履歴付随マスタ抽出
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ抽出
        // * 
        // * 構文           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
        // *                                                ByVal cSearchKey As ABAtenaSearchKey, _
        // *                                                ByVal strKikanYMD As String, _
        // *                                                ByVal blnSakujoKB As Boolean) As DataSet
        // * 
        // * 機能　　    　　宛名履歴付随マスタより該当データを取得する
        // * 
        // * 引数          strJuminCD     : 住民コード 
        // *               strRrkNo       : 履歴番号
        // *               strJuminJutogaiKB  : 住民住登外区分
        // *               blnSakujoFG    : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した宛名履歴付随マスタの該当データ
        // ************************************************************************************************
        public DataSet GetAtenaRirekiFZYByLinkNo(string strJuminCD, string strLinkNo)
        {
            const string THIS_METHOD_NAME = "GetAtenaRirekiFZYByLinkNo";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaRirekiFZYEntity;               // 宛名履歴付随データセット
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;

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
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECTパラメータコレクションクラスのインスタンス化
                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                // 住民コード
                strSQL.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // リンク番号
                strSQL.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.LINKNO, ABAtenaRirekiFZYEntity.PARAM_LINKNO);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_LINKNO;
                cfUFParameterClass.Value = strLinkNo;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 住民住登外区分
                strSQL.AppendFormat(" AND {0} = '1'", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB);

                // 削除フラグ
                strSQL.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON);

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】")

                // SQLの実行 DataSetの取得
                csAtenaRirekiFZYEntity = m_csDataSchma.Clone();
                csAtenaRirekiFZYEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaRirekiFZYEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csAtenaRirekiFZYEntity;

        }
        #endregion

        #region 宛名履歴付随マスタ追加　[InsertAtenaFZYRB]
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ追加
        // * 
        // * 構文           Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴付随マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertAtenaFZYRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertAtenaRB";
            int intInsCnt;        // 追加件数

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null || string.IsNullOrEmpty(m_strInsertSQL) || m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateInsertSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaRirekiFZYEntity.TANMATSUID] = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow[ABAtenaRirekiFZYEntity.SAKUJOFG] = SAKUJOFG_OFF;                      // 削除フラグ
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;            // 更新カウンタ
                csDataRow[ABAtenaRirekiFZYEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;    // 作成ユーザー
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINUSER] = m_cfControlData.m_strUserId;     // 更新ユーザー

                // 作成日時、更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRirekiFZYEntity.SAKUSEINICHIJI];
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow[ABAtenaRirekiFZYEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

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
                    strParamName = string.Format("{0}{1}", ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABAtenaRirekiFZYEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));



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

        #region 宛名履歴付随マスタ更新　[UpdateAtenaFZYRB]
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ更新
        // * 
        // * 構文           Public Function UpdateAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴付随マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateAtenaFZYRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateAtenaRB";                     // パラメータクラス
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

                // 共通項目の編集を行う
                csDataRow[ABAtenaRirekiFZYEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                    // 端末ＩＤ
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRirekiFZYEntity.KOSHINCOUNTER]) + 1m;     // 更新カウンタ
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                      // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRirekiFZYEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiFZYEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

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
                m_strUpdateSQL = "UPDATE " + ABAtenaRirekiFZYEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・履歴番号・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABAtenaRirekiFZYEntity.JUMINCD) && !(csDataColumn.ColumnName == ABAtenaRirekiFZYEntity.RIREKINO) && !(csDataColumn.ColumnName == ABAtenaRirekiFZYEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABAtenaRirekiFZYEntity.SAKUSEINICHIJI))


                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER;
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

        #region 宛名履歴付随マスタ削除　[DeleteAtenaFZYRB]
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ削除
        // * 
        // * 構文           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　宛名履歴付随マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaFZYRB(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaFZYRB";                     // パラメータクラス
            int intDelCnt;                            // 削除件数


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
                csDataRow[ABAtenaRirekiFZYEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                    // 端末ＩＤ
                csDataRow[ABAtenaRirekiFZYEntity.SAKUJOFG] = SAKUJOFG_ON;                                                        // 削除フラグ
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABAtenaRirekiFZYEntity.KOSHINCOUNTER]) + 1m;     // 更新カウンタ
                csDataRow[ABAtenaRirekiFZYEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                      // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABAtenaRirekiFZYEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiFZYEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")

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
        // * メソッド名     宛名履歴付随マスタ物理削除
        // * 
        // * 構文           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow, _
        // *                                              ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　宛名履歴付随マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaFZYRB(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteAtenaFZYRB";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
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
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null | string.IsNullOrEmpty(m_strDelButuriSQL) | m_cfDelButuriUFParameterCollectionClass == null)
                {
                    CreateDeleteButsuriSQL(csDataRow);
                }
                else
                {
                    // 処理なし
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()) == ABAtenaRirekiFZYEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // 処理なし
                    }
                }

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】")

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
        // * 履歴番号 000022 2005/11/18 追加開始
        // ************************************************************************************************
        // * メソッド名     宛名履歴付随マスタ物理削除(１住民コード指定)
        // * 
        // * 構文           Public Overloads Function DeleteAtenaFZYRB(ByVal strJuminCD As String) As Integer
        // * 
        // * 機能　　    　　宛名履歴付随マスタのデータを物理削除する
        // * 
        // * 引数           strJuminCD As String : 削除する対象となる住民コード
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteAtenaFZYRB(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "DeleteAtenaFZYRB";
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelFromJuminCDSQL is null || string.IsNullOrEmpty(m_strDelFromJuminCDSQL) || m_cfDelFromJuminCDPrmCollection == null)
                {
                    CreateDelFromJuminCDSQL();
                }
                else
                {
                    // 処理なし
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                this.m_cfDelFromJuminCDPrmCollection[ABAtenaRirekiFZYEntity.KEY_JUMINCD].Value = strJuminCD;

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + Me.GetType.Name + "】" + _
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection) + "】")

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection);

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
                csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER);

                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();


                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER;
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
            const string THIS_METHOD_NAME = "CreateButsuriSQL";
            UFParameterClass cfUFParameterClass;
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

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
        // ************************************************************************************************
        // * メソッド名     物理削除用(１住民ＣＤ指定)SQL文の作成
        // * 
        // * 構文           Private Sub CreateDelFromJuminCDSQL()
        // * 
        // * 機能           住民ＣＤで該当全履歴データを物理削除するSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateDelFromJuminCDSQL()
        {
            const string THIS_METHOD_NAME = "CreateDelFromJuminCDSQL";
            StringBuilder csWhere;                        // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD);
                csWhere.Append(" = ");
                csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD);

                // 物理DELETE(１住民ＣＤ指定) SQL文の作成
                m_strDelFromJuminCDSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用コレクションにパラメータを追加
                m_cfDelFromJuminCDPrmCollection = new UFParameterCollectionClass();
                m_cfDelFromJuminCDPrmCollection.Add(ABAtenaRirekiFZYEntity.KEY_JUMINCD, DbType.String);

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
