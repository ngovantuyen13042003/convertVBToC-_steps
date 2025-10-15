// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ支援措置ＤＡ
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/10/13　下村　美江
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2023/10/13             【AB-0880-1】個人制御情報詳細管理項目追加
// *
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace Densan.Reams.AB.AB000BB
{

    public class ABShienSochiBClass
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
        private const string THIS_CLASS_NAME = "ABShienSochiBClass";                   // クラス名
        private const string THIS_BUSINESSID = "AB";                                   // 業務コード

        private const string SAKUJOFG_OFF = "0";
        private const string SAKUJOFG_ON = "1";
        private const string SAISHINFG_ON = "1";
        private const string SAISHINFG_OFF = "0";
        private const string KARISHIENSOCHI = "2";
        private const decimal KOSHINCOUNTER_DEF = decimal.Zero;

        private const string FORMAT_UPDATETIME = "yyyyMMddHHmmssfff";

        private const string ERR_SHIENSOCHI = "支援措置管理番号";

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
        public ABShienSochiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

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
            m_cfSelectUFParameterCollectionClass = (object)null;
            m_cfInsertUFParameterCollectionClass = (object)null;
            m_cfUpdateUFParameterCollectionClass = (object)null;
            m_cfDelRonriUFParameterCollectionClass = (object)null;
            m_cfDelButuriUFParameterCollectionClass = (object)null;
        }
        #endregion

        #region メソッド
        #region 支援措置抽出　[GetShienSochi]
        // ************************************************************************************************
        // * メソッド名    支援措置抽出
        // * 
        // * 構文          Public Function GetShienSochi As DataSet
        // * 
        // * 機能　　    　支援措置より該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号 
        // * 
        // * 戻り値        DataSet : 取得した支援措置の該当データ
        // ************************************************************************************************
        public DataSet GetShienSochi(string strShienSochiKanriNo)
        {

            return GetShienSochi(strShienSochiKanriNo, true, false);

        }

        // ************************************************************************************************
        // * メソッド名    支援措置抽出
        // * 
        // * 構文          Public Function GetShienSochi As DataSet
        // * 
        // * 機能　　    　支援措置より該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号  
        // *               blnSaishin           : 最新フラグ
        // *               blnSakujoFG          : 削除フラグ
        // * 
        // * 戻り値        DataSet : 取得した支援措置の該当データ
        // ************************************************************************************************
        public DataSet GetShienSochi(string strShienSochiKanriNo, bool blnSaishin, bool blnSakujoFG)
        {

            const string THIS_METHOD_NAME = "GetShienSochi";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            DataSet csShienSochiEntity;
            var strSQL = new StringBuilder();

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                // 支援措置管理番号が指定されていないときエラー
                if (strShienSochiKanriNo == null || strShienSochiKanriNo.Trim().RLength == 0)
                {
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHI, objErrorStruct.m_strErrorCode);
                }
                else
                {
                    // 処理なし
                }

                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                strSQL.Append(CreateWhere(strShienSochiKanriNo, blnSaishin, blnSakujoFG));

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csShienSochiEntity = m_csDataSchma.Clone();
                csShienSochiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochiEntity, ABShienSochiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csShienSochiEntity;

        }

        // ************************************************************************************************
        // * メソッド名    支援措置抽出
        // * 
        // * 構文          Public Function GetShienSochi As DataSet
        // * 
        // * 機能　　    　支援措置より該当データを取得する
        // * 
        // * 引数          strShienSochiKanriNo : 支援措置管理番号の配列       
        // * 
        // * 戻り値        DataSet : 取得した支援措置の該当データ
        // ************************************************************************************************
        public DataSet GetShienSochi(string[] strShienSochiKanriNo)
        {

            const string THIS_METHOD_NAME = "GetShienSochi";
            DataSet csShienSochiEntity;
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
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, false);
                }

                // WHERE句の作成
                if (strShienSochiKanriNo.Length == 0)
                {
                    csShienSochiEntity = m_csDataSchma.Clone();
                }
                else
                {
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABShienSochiEntity.SHIENSOCHIKANRINO);
                    strSQL.Append(" IN (");

                    for (int i = 0, loopTo = strShienSochiKanriNo.Length - 1; i <= loopTo; i++)
                    {
                        // -----------------------------------------------------------------------------
                        // 支援措置管理番号
                        strParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO + i.ToString();

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
                    strSQL.Append(" AND ");
                    strSQL.Append(ABShienSochiEntity.SAISHINFG);

                    strSQL.Append(" = '1'");

                    // RDBアクセスログ出力
                    m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "】");

                    // SQLの実行 DataSetの取得
                    csShienSochiEntity = m_csDataSchma.Clone();
                    csShienSochiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochiEntity, ABShienSochiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, false);

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

            return csShienSochiEntity;

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
                csSELECT.AppendFormat("SELECT {0}", ABShienSochiEntity.SHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIKANRINO);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RIREKINO);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAISHINFG);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.UKETSUKEKBN);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.UKETSUKEYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIKBN);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHIUMU);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHISTYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHIEDYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHISTYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIEDYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENFUYOKAKUNINRENRAKUYMD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TOSHOUKETSUKESHICHOSONCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TOSHOUKETSUKESHICHOSON);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.GYOMUCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.BIKO);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD1);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON1);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD1);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD2);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON2);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD2);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD3);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON3);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD3);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD4);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON4);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD4);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD5);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON5);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD5);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD6);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON6);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD6);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD7);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON7);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD7);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD8);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON8);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD8);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD9);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON9);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD9);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD10);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON10);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD10);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD11);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON11);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD11);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD12);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON12);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD12);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD13);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON13);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD13);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD14);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON14);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD14);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD15);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON15);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD15);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD16);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON16);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD16);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD17);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON17);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD17);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD18);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON18);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD18);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD19);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON19);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD19);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD20);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON20);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD20);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KANRIKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SETAIYOKUSHIKB);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE1);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE2);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE3);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE4);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE5);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.NYURYOKUBASHOCD);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.NYURYOKUBASHO);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TANMATSUID);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUJOFG);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINCOUNTER);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUSEINICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUSEIUSER);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINNICHIJI);
                csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINUSER);

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
        // * 構文         Private Sub CreateWhere
        // * 
        // * 機能         WHERE分を作成、パラメータコレクションを作成する
        // * 
        // * 引数         strShienSochiKanriNo: 支援措置管理番号 
        // *              blnSaishin          : 最新フラグ
        // *              blnSakujoFG         : 削除フラグ
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private string CreateWhere(string strShienSochiKanriNo, bool blnSaishin, bool blnSakujoFG)
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
                csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiEntity.SHIENSOCHIKANRINO, ABShienSochiEntity.KEY_SHIENSOCHIKANRINO);
                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO;
                cfUFParameterClass.Value = strShienSochiKanriNo;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass);

                // 最新フラグ
                if (blnSaishin)
                {
                    csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiEntity.SAISHINFG, SAISHINFG_ON);
                }
                else
                {
                    // 処理なし
                }

                // 削除フラグ
                if (blnSakujoFG == false)
                {
                    csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON);
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

        #region 支援措置追加　[InsertShienSochi]
        // ************************************************************************************************
        // * メソッド名     支援措置追加
        // * 
        // * 構文           Public Function InsertShienSochi((ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置にデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertShienSochi(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "InsertShienSochi";
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
                csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId;     // 端末ＩＤ
                csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF;              // 更新カウンタ
                csDataRow(ABShienSochiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;      // 作成ユーザー
                csDataRow(ABShienSochiEntity.SAKUSEINICHIJI) = m_strUpdateDatetime;           // 作成日時
                csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;       // 更新ユーザー
                csDataRow(ABShienSochiEntity.KOSHINNICHIJI) = m_strUpdateDatetime;             // 更新日時

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength)).ToString();

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
                    strParamName = string.Format("{0}{1}", ABShienSochiEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName);

                    // INSERT SQL文の作成
                    csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName);
                    csInsertParam.AppendFormat("{0},", strParamName);

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = strParamName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL = string.Format("INSERT INTO {0}({1}) VALUES ({2})", ABShienSochiEntity.TABLE_NAME, csInsertColumn.ToString().TrimEnd(",".ToCharArray()), csInsertParam.ToString().TrimEnd(",".ToCharArray()));

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

        #region 支援措置更新　[UpdateShienSochi]
        // ************************************************************************************************
        // * メソッド名     支援措置更新
        // * 
        // * 構文           Public Function UpdateShienSochi(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　 支援措置のデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateShienSochi(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "UpdateShienSochi";                     // パラメータクラス
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
                csDataRow(ABShienSochiEntity.SAISHINFG) = SAISHINFG_OFF;                                                    // 最新フラグ
                csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABShienSochiEntity.KOSHINCOUNTER) + 1m;        // 更新カウンタ
                csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                csDataRow(ABShienSochiEntity.KOSHINNICHIJI) = m_strUpdateDatetime;

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) == ABShienSochiEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }

                    // キー項目以外は編集内容取得
                    else
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
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
                m_strUpdateSQL = "UPDATE " + ABShienSochiEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // WHERE文の作成
                csWhere = new StringBuilder();
                csWhere.Append(" WHERE ");
                csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER);

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 支援措置管理番号・履歴番号・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABShienSochiEntity.SHIENSOCHIKANRINO) && !(csDataColumn.ColumnName == ABShienSochiEntity.RIREKINO) && !(csDataColumn.ColumnName == ABShienSochiEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABShienSochiEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(" = ");
                        csUpdateParam.Append(ABShienSochiEntity.PARAM_PLACEHOLDER);
                        csUpdateParam.Append(csDataColumn.ColumnName);
                        csUpdateParam.Append(",");

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
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
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER;
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

        #region 支援措置削除　[DeleteShienSochi]
        // ************************************************************************************************
        // * メソッド名     支援措置削除
        // * 
        // * 構文           Public Function DeleteShienSochi(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　支援措置のデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteShienSochi(DataRow csDataRow)
        {

            const string THIS_METHOD_NAME = "DeleteShienSochi";  // パラメータクラス
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
                csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow(ABShienSochiEntity.SAKUJOFG) = SAKUJOFG_ON;                                                       // 削除フラグ
                csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABShienSochiEntity.KOSHINCOUNTER) + 1m;        // 更新カウンタ
                csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                     // 更新ユーザー

                // 更新日時の設定
                m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME);
                var argcsDate = csDataRow(ABShienSochiEntity.KOSHINNICHIJI);
                this.SetUpdateDatetime(ref argcsDate);

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) == ABShienSochiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    // キー項目以外は編集内容を設定
                    else
                    {
                        this.m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
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
        // * メソッド名     支援措置物理削除
        // * 
        // * 構文           Public Function DeleteShienSochi(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　支援措置のデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteShienSochi(DataRow csDataRow, string strSakujoKB)
        {

            const string THIS_METHOD_NAME = "DeleteShienSochi";
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
                    if (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) == ABShienSochiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
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
                csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER);


                // 論理DELETE SQL文の作成
                csDelRonriParam = new StringBuilder();
                csDelRonriParam.Append("UPDATE ");
                csDelRonriParam.Append(ABShienSochiEntity.TABLE_NAME);
                csDelRonriParam.Append(" SET ");
                csDelRonriParam.Append(ABShienSochiEntity.NYURYOKUBASHOCD);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_NYURYOKUBASHOCD);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.NYURYOKUBASHO);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_NYURYOKUBASHO);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.TANMATSUID);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_TANMATSUID);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.SAKUJOFG);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_SAKUJOFG);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.KOSHINCOUNTER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINCOUNTER);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.KOSHINNICHIJI);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINNICHIJI);
                csDelRonriParam.Append(", ");
                csDelRonriParam.Append(ABShienSochiEntity.KOSHINUSER);
                csDelRonriParam.Append(" = ");
                csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINUSER);
                csDelRonriParam.Append(csWhere);
                // Where文の追加
                m_strDelRonriSQL = csDelRonriParam.ToString();

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_NYURYOKUBASHOCD;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_NYURYOKUBASHO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO;
                m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER;
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
                csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.RIREKINO);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_RIREKINO);
                csWhere.Append(" AND ");
                csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER);
                csWhere.Append(" = ");
                csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABShienSochiEntity.TABLE_NAME + csWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO;
                m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER;
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

        #region 支援措置情報取得
        // ************************************************************************************************
        // * メソッド名     支援措置情報取得
        // * 
        // * 構文           Public Function GetShienSochiJoho(ByVal strJumincd As String) As DataSet
        // * 
        // * 機能　　    　 支援措置と支援措置対象からデータを取得
        // * 
        // * 引数           strJumincd：住民コード
        // * 
        // * 戻り値         取得したデータ：DataSet
        // ************************************************************************************************
        public DataSet GetShienSochiJoho(string strJumincd)
        {
            const string THIS_METHOD_NAME = "GetShienSochiJoho";
            DataSet csShienSochiDS;                                        // 支援措置データ
            StringBuilder strSQL;                                         // SQL文SELECT句
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strSQL = new StringBuilder();

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL.Append("SELECT ");
                strSQL.Append(ABShienSochiEntity.TABLE_NAME);
                strSQL.Append(".* ");
                strSQL.Append(" FROM ");
                strSQL.Append(ABShienSochiEntity.TABLE_NAME);
                strSQL.Append(" INNER JOIN ");
                strSQL.Append(ABShienSochiTaishoEntity.TABLE_NAME);
                strSQL.Append(" ON ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO);
                strSQL.Append(" = ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);
                strSQL.Append(" AND ");
                strSQL.Append(ABShienSochiTaishoEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABShienSochiTaishoEntity.PARAM_JUMINCD);

                // 検索条件のパラメータを作成
                // 住民コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD;
                cfUFParameterClass.Value = strJumincd;
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // WHERE句の作成
                strSQL.Append(" WHERE ");
                strSQL.AppendFormat("{0} = '{1}'", ABShienSochiEntity.SAISHINFG, SAISHINFG_ON);
                strSQL.AppendFormat(" AND {0}.{1} <> '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON);
                strSQL.Append(" ORDER BY ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO);
                strSQL.Append(" DESC");

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csShienSochiDS = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return csShienSochiDS;

        }

        // ************************************************************************************************
        // * メソッド名     仮支援措置チェック
        // * 
        // * 構文           Public Function CheckKariShienSochi(ByVal strSystemYMD As String) As DataSet
        // * 
        // * 機能　　    　 支援措置と支援措置対象からデータを取得
        // * 
        // * 引数           strSystemYMD：システム日付
        // * 
        // * 戻り値         取得したデータ：DataSet
        // ************************************************************************************************
        public DataSet CheckKariShienSochi(string strSystemYMD)
        {
            const string THIS_METHOD_NAME = "CheckKariShienSochi";
            ABAtenaKanriJohoBClass cABKanriJohoB;         // 宛名管理情報クラス
            DataSet csABKanriJohoDS;
            int intNisu;
            DataSet csShienSochiDS;                                        // 支援措置データ
            StringBuilder strSQL;                                         // SQL文SELECT句
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス
            UFDateClass cfDate;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名管理情報クラスのインスタンス化
                cABKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                // 管理情報取得メソッド実行(個人制御機能(20)、仮支援警告日数(84))
                csABKanriJohoDS = cABKanriJohoB.GetKanriJohoHoshu("20", "84");

                // 管理情報チェック
                if (csABKanriJohoDS is not null && csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0 && UFStringClass.CheckNumber(csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString.Trim))
                {
                    intNisu = (int)csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString;
                }
                else
                {
                    intNisu = 30;
                }
                cfDate = new UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero);
                intNisu = intNisu * -1;
                cfDate.p_strDateValue = strSystemYMD;
                cfDate.p_strDateValue = cfDate.AddDay(intNisu);

                strSQL = new StringBuilder();

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                strSQL.Append("SELECT DISTINCT ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.JUMINCD);
                strSQL.Append(" FROM ");
                strSQL.Append(ABShienSochiEntity.TABLE_NAME);
                strSQL.Append(" INNER JOIN ");
                strSQL.Append(ABShienSochiTaishoEntity.TABLE_NAME);
                strSQL.Append(" ON ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO);
                strSQL.Append(" = ");
                strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.SHIENSOCHIKANRINO);

                // WHERE句の作成
                strSQL.Append(" WHERE ");
                strSQL.AppendFormat("{0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKBN, KARISHIENSOCHI);
                strSQL.AppendFormat(" AND {0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAISHINFG, SAISHINFG_ON);
                strSQL.AppendFormat(" AND {0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.KARISHIENSOCHIEDYMD, "99999999");
                strSQL.AppendFormat(" AND {0}.{1} < {2}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.KARISHIENSOCHISTYMD, ABShienSochiEntity.PARAM_KARISHIENSOCHISTYMD);
                strSQL.AppendFormat(" AND {0}.{1} <> '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON);

                // 検索条件のパラメータを作成
                // システム日付
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KARISHIENSOCHISTYMD;
                cfUFParameterClass.Value = cfDate.p_strSeirekiYMD;
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csShienSochiDS = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return csShienSochiDS;

        }
        #endregion

        #region 支援措置情報取得
        // ************************************************************************************************
        // * メソッド名     支援措置情報取得
        // * 
        // * 構文           Public Function GetShienSochi(ByVal strShienSochiNo As String, ByVal strRirekiNo As String) As DataSet
        // * 
        // * 機能　　    　 支援措置番号と履歴番号からデータを取得
        // * 
        // * 引数           strShienSochiNo：住民コード
        // *                strRirekiNO    :履歴番号
        // * 
        // * 戻り値         取得したデータ：DataSet
        // ************************************************************************************************
        public DataSet GetShienSochi(string strShienSochiNo, string strRirekiNo)
        {
            const string THIS_METHOD_NAME = "GetShienSochi";
            DataSet csShienSochiDS;                                        // 支援措置データ
            StringBuilder strSQL;                                         // SQL文SELECT句
            UFParameterClass cfUFParameterClass;                          // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;      // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                strSQL = new StringBuilder();

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                // SELECT句の生成
                strSQL.Append(CreateSelect());
                // FROM句の生成
                strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME);

                // ﾃﾞｰﾀｽｷｰﾏの取得
                if (m_csDataSchma is null)
                {
                    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, false);
                }

                strSQL.AppendFormat("WHERE {0} = {1}", ABShienSochiEntity.SHIENSOCHIKANRINO, ABShienSochiEntity.KEY_SHIENSOCHIKANRINO);
                strSQL.AppendFormat(" AND {0} = {1}", ABShienSochiEntity.RIREKINO, ABShienSochiEntity.KEY_RIREKINO);

                // 検索条件のパラメータを作成
                // 支援措置管理番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO;
                cfUFParameterClass.Value = strShienSochiNo;
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // 履歴番号
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO;
                cfUFParameterClass.Value = strRirekiNo;
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");

                // SQLの実行 DataSetの取得
                csShienSochiDS = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                // ワーニングをスローする
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                // システムエラーをスローする
                throw;

            }

            return csShienSochiDS;

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
