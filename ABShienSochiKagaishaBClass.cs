// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ支援措置加害者マスタＤＡ(ABShienSochiKagaishaBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/10/13　下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
// * 2024/03/07  000001     【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABShienSochiKagaishaBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private UFParameterCollectionClass m_cfSelectUFParameterCollectionClass;      // SELECT用パラメータコレクション
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;      // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;      // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollectionClass;    // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollectionClass;   // 物理削除用パラメータコレクション
        private DataSet m_csDataSchma;   // スキーマ保管用データセット
        private string m_strUpdateDatetime;                   // 更新日時

        public bool m_blnBatch = false;               // バッチフラグ
                                                      // コンスタント定義
        private const string THIS_CLASS_NAME = "ABShienSochiKagaisha";                 // クラス名
        private const string THIS_BUSINESSID = "AB";                                   // 業務コード

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private const string ERR_SHIENSOCHIKANRINO = "支援措置管理番号";

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
        public ABShienSochiKagaishaBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_cfSelectUFParameterCollectionClass = null;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDelRonriUFParameterCollectionClass = null;
            m_cfDelButuriUFParameterCollectionClass = null;
        }
        #endregion

        #region メソッド
        #region 支援措置加害者抽出　[GetShienSochiKagaisha]
        // ************************************************************************************************
        // * メソッド名    支援措置加害者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiKagaisha As DataSet
        // * 
        // * 機能　　    　支援措置加害者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo: 支援措置管理番号 
        // * 
        // * 戻り値        DataSet : 取得した支援措置加害者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiKagaisha(string strShienSochiKanriNo)
        {

            return GetShienSochiKagaisha(strShienSochiKanriNo, false);

        }
        // ************************************************************************************************
        // * メソッド名    支援措置加害者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiKagaisha As DataSet
        // * 
        // * 機能　　    　支援措置加害者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo: 支援措置管理番号 
        // *               blnSakujoFG        : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置加害者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiKagaisha(string strShienSochiKanriNo, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochiKagaisha";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 支援措置管理番号が指定されていないときエラー
                if (strShienSochiKanriNo == null || strShienSochiKanriNo.Trim().RLength() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strShienSochiKanriNo, 0, blnSakujoFG));
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                strSQL.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RENBAN);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名    支援措置加害者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiKagaisha As DataSet
        // * 
        // * 機能　　    　支援措置加害者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo: 支援措置管理番号 
        // *               intRenban           : 連番
        // * 
        // * 戻り値        DataSet : 取得した支援措置加害者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiKagaisha(string strShienSochiKanriNo, int intRenban)
        {

            return GetShienSochiKagaisha(strShienSochiKanriNo, intRenban, false);

        }
        // ************************************************************************************************
        // * メソッド名    支援措置加害者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiKagaisha As DataSet
        // * 
        // * 機能　　    　支援措置加害者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo: 支援措置管理番号 
        // *               intRenban           : 連番
        // *               blnSakujoFG         : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置加害者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiKagaisha(string strShienSochiKanriNo, int intrenban, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochiKagaisha";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 支援措置管理番号が指定されていないときエラー
                if (strShienSochiKanriNo == null || strShienSochiKanriNo.Trim().RLength() == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strShienSochiKanriNo, intrenban, blnSakujoFG));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csAtenaEntity = m_csDataSchma.Clone();
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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
        // * メソッド名    支援措置加害者マスタ抽出
        // * 
        // * 構文          Public Function GetShienSochiKagaisha As DataSet
        // * 
        // * 機能　　    　支援措置加害者マスタより該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo: 支援措置管理番号の配列 
        // * 
        // * 戻り値        DataSet : 取得した支援措置加害者マスタの該当データ
        // ************************************************************************************************
        public DataSet GetShienSochiKagaisha(string[] strShienSochiKanriNo)
        {

            const string THIS_METHOD_NAME = "GetShienSochiKagaisha";
            DataSet csAtenaEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfParameter;
            string strParameterName;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                m_cfSelectUFParameterCollectionClass = new UFParameterCollectionClass();

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, false);
                }

                if (strShienSochiKanriNo.Length == 0)
                {
                    csAtenaEntity = m_csDataSchma.Clone();
                }
                else
                {
                    // WHERE句の作成
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                    strSQL.Append(" IN (");

                    for (int i = 0, loopTo = strShienSochiKanriNo.Length - 1; i <= loopTo; i++)
                    {
                        // 支援措置管理番号
                        strParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO + i.ToString();

                        if (i > 0)
                        {
                            strSQL.AppendFormat(", {0}", strParameterName);
                        }
                        else
                        {
                            strSQL.Append(strParameterName);
                        }

                        cfParameter = new UFParameterClass();
                        cfParameter.ParameterName = strParameterName;
                        cfParameter.Value = strShienSochiKanriNo[i];
                        m_cfSelectUFParameterCollectionClass.Add(cfParameter);
                        // -----------------------------------------------------------------------------
                    }

                    strSQL.Append(")");

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                    // SQLの実行 DataSetの取得
                    csAtenaEntity = m_csDataSchma.Clone();
                    csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);
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
                csSELECT.AppendFormat("SELECT {0}", ABShienSochiKagaishaEntity.SHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RENBAN);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUMINCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHIMEI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_UMAREYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SEIBETSU);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_YUBINNO);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KANNAIKANGAIKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUSHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_MACHIAZACD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_TODOFUKEN);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHICHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_MACHIAZA);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_BANCHI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KATAGAKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUSEKICD);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUSEKI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUGAIJUSHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SONOTA);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINUSER);

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
        // * メソッド名   WHERE文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能         WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数         strShienSochiKanriNo : 支援措置管理番号 
        // *              intRenban            : 連番
        // *              blnSakujoFG          : 削除フラグ
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private string CreateWhere(string strShienSochiKanriNo, int intRenban, bool blnSakujoFG)
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

                // 支援措置管理番号
                csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO, ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO;
                cfUFParameterClass.Value = strShienSochiKanriNo;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 連番
                if (!(intRenban == 0))
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiKagaishaEntity.RENBAN, ABShienSochiKagaishaEntity.KEY_RENBAN);
                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN;
                    cfUFParameterClass.Value = intRenban.ToString();
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
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiKagaishaEntity.SAKUJOFG, SAKUJOFG_ON);
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

        #region 支援措置加害者マスタ追加　[InsertShienSochiKagaisha]
        // ************************************************************************************************
        // * メソッド名     支援措置加害者マスタ追加
        // * 
        // * 構文           Public Function InsertShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置加害者マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertShienSochiKagaisha(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertShienSochiKagaisha";
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
                csDataRow[ABShienSochiKagaishaEntity.TANMATSUID] = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow[ABShienSochiKagaishaEntity.SAKUJOFG] = SAKUJOFG_OFF;                        // 削除フラグ
                csDataRow[ABShienSochiKagaishaEntity.KOSHINCOUNTER] = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow[ABShienSochiKagaishaEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow[ABShienSochiKagaishaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;       // 更新ユーザー

                // 作成日時、更新日時の設定
                var argcsDate = csDataRow[ABShienSochiKagaishaEntity.SAKUSEINICHIJI];
                this.SetUpdateDatetime(ref argcsDate);
                var argcsDate1 = csDataRow[ABShienSochiKagaishaEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate1);

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");

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
                    strParamName = string.Format("{0}{1}", ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABShienSochiKagaishaEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

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

        #region 支援措置加害者マスタ更新　[UpdateShienSochiKagaisha]
        // ************************************************************************************************
        // * メソッド名     支援措置加害者マスタ更新
        // * 
        // * 構文           Public Function UpdateShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 支援措置加害者マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateShienSochiKagaisha(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateShienSochiKagaisha";                     // パラメータクラス
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
                csDataRow[ABShienSochiKagaishaEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABShienSochiKagaishaEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABShienSochiKagaishaEntity.KOSHINCOUNTER] + 1m; // 更新カウンタ
                csDataRow[ABShienSochiKagaishaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                csDataRow[ABShienSochiKagaishaEntity.KOSHINNICHIJI] = m_strUpdateDatetime;

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()) == ABShienSochiKagaishaEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");

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
                m_strUpdateSQL = "UPDATE " + ABShienSochiKagaishaEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 支援措置管理番号・連番・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO) && !(csDataColumn.ColumnName == ABShienSochiKagaishaEntity.RENBAN) && !(csDataColumn.ColumnName == ABShienSochiKagaishaEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABShienSochiKagaishaEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER;
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

        #region 支援措置加害者マスタ削除　[DeleteShienSochiKagaisha]
        // ************************************************************************************************
        // * メソッド名     支援措置加害者マスタ削除
        // * 
        // * 構文           Public Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置加害者マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteShienSochiKagaisha(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteShienSochiKagaisha";  // パラメータクラス
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
                csDataRow[ABShienSochiKagaishaEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABShienSochiKagaishaEntity.SAKUJOFG] = SAKUJOFG_ON;                                                       // 削除フラグ
                csDataRow[ABShienSochiKagaishaEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABShienSochiKagaishaEntity.KOSHINCOUNTER] + 1m; // 更新カウンタ
                csDataRow[ABShienSochiKagaishaEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow[ABShienSochiKagaishaEntity.KOSHINNICHIJI];
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()) == ABShienSochiKagaishaEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】");
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
        // * メソッド名     支援措置加害者マスタ物理削除
        // * 
        // * 構文           Public Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　支援措置加害者マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteShienSochiKagaisha(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteShienSochiKagaisha";
            UFErrorStruct objErrorStruct; // エラー定義構造体
                                          // パラメータクラス
            int intDelCnt;            // 削除件数


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
                if (m_strDelButuriSQL is null || string.IsNullOrEmpty(m_strDelButuriSQL) || m_cfDelButuriUFParameterCollectionClass == null)
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
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()) == ABShienSochiKagaishaEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }

                    // キー項目以外の取得なし
                    else
                    {
                        // 処理なし
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "】");
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
                csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER);


                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER;
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
                csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.RENBAN);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABShienSochiKagaishaEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER;
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
