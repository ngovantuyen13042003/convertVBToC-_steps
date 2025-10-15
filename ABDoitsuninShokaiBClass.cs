// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        同一人照会ＤＡ(ABDoitsuninShokaiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/05/01　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/08/28 000001     RDBアクセスログの修正
// * 2004/01/19 000002     旧市町村コードの追加に伴う修正     
// * 2007/05/22 000003     宛名データ種別の追加に伴う修正(生年月日統一化の判定用に追加)
// * 2007/07/10 000004     DB文字数拡張対応，文字数を拡張したDBに対応するためにカラム作成時のMaxLength値修正（中沢）
// * 2014/09/01 000005     【AB21010】個人番号制度対応（岩下）
// * 2022/12/16 000006    【AB-8010】住民コード世帯コード15桁対応(下村)
// * 2023/12/18 000007    【AB-7010-1】同一人設定情報取得対応(下村)
// ************************************************************************************************
using System;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace Densan.Reams.AB.AB000BB
{

    public class ABDoitsuninShokaiBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLog;                           // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdb;                           // ＲＤＢクラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABDoitsuninShokaiBClass";
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfUFControlData As UFControlData,
        // *                                ByVal cfUFConfigDataClass As UFConfigDataClass,
        // *                                ByVal cfUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // *                 cfUFRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABDoitsuninShokaiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)
        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigData;
            m_cfRdb = cfRdb;

            // ログ出力クラスのインスタンス化
            m_cfLog = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     同一人グループ宛名抽出
        // * 
        // * 構文           Public Function GetDoitsuninAtena(ByVal strDoitsuninShikibetsuCD As String) As DataSet
        // * 
        // * 機能　　    　　合併同一人より該当データを全件取得する。
        // * 
        // * 引数           strDoitsuninShikibetsuCD As String      :同一人識別コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（DataSet）
        // *                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
        // ************************************************************************************************
        public DataSet GetDoitsuninAtena(string strDoitsuninShikibetsuCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninAtena";          // このメソッド名
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfParameter;                             // パラメータクラス
            UFParameterCollectionClass cfParameterCollection;         // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(".*,");
                strSQL.Append(ABAtenaEntity.TABLE_NAME);
                strSQL.Append(".* FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(" LEFT OUTER JOIN ");
                strSQL.Append(ABAtenaEntity.TABLE_NAME);
                strSQL.Append(" ON ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(".");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append("=");
                strSQL.Append(ABAtenaEntity.TABLE_NAME);
                strSQL.Append(".");
                strSQL.Append(ABAtenaEntity.JUMINCD);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(".");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strSQL.Append("=");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaEntity.TABLE_NAME);
                strSQL.Append(".");
                strSQL.Append(ABAtenaEntity.JUTOGAIYUSENKB);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(".");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfParameterCollection = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成

                // 同一人識別コード
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD;
                cfParameter.Value = strDoitsuninShikibetsuCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // 住登外優先区分
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB;
                cfParameter.Value = "1";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // *履歴番号 000001 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLog.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString + "】")

                // RDBアクセスログ出力
                m_cfLog.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfParameterCollection) + "】");



                // *履歴番号 000001 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection);


                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csGappeiDoitsuninEntity;

        }

        // ************************************************************************************************
        // * メソッド名     同一人データスキーマ作成
        // * 
        // * 構文           Public Function GetSchemaDoitsuninData() As DataSet
        // * 
        // * 機能　　       同一人データのスキーマを作成する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         ABDoitsuninDataEntity(DataSet) : 同一人データ
        // ************************************************************************************************
        public DataSet GetSchemaDoitsuninData()
        {
            const string THIS_METHOD_NAME = "GetSchemaDoitsuninData";
            DataSet csDoitsuninDataEntity;                // 同一人データセット
            DataTable csDoitsuninDataTable;               // 同一人データテーブル
            DataColumn csDataColumn;                      // データカラム

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 合併同一人のテーブルスキーマを取得する
                csDoitsuninDataEntity = m_cfRdb.GetTableSchema(ABGappeiDoitsuninEntity.TABLE_NAME);

                // テーブル名を変更する
                csDoitsuninDataTable = csDoitsuninDataEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME);
                csDoitsuninDataTable.TableName = ABDoitsuninDataEntity.TABLE_NAME;

                // **
                // * 表示用カラムを追加する
                // *
                // 表示用種別(住民種別)
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_HENSHUSHUBETSURYOKU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用生年月日
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_UMAREHYOJIWMD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 11;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用性別
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_SEIBETSU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 1;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用氏名（名称）
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_HENSHUKANJISHIMEI, Type.GetType("System.String"));
                // * 履歴番号 000004 2007/07/10 修正開始
                csDataColumn.MaxLength = 240;
                // csDataColumn.MaxLength = 40
                // * 履歴番号 000004 2007/07/10 修正終了
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用住所
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_HENSHUJUSHO, Type.GetType("System.String"));
                // * 履歴番号 000004 2007/07/10 修正開始
                csDataColumn.MaxLength = 160;
                // csDataColumn.MaxLength = 60
                // * 履歴番号 000004 2007/07/10 修正終了
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用行政区
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_GYOSEIKUMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 30;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 表示用世帯コード
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_STAICD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 15;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // *履歴番号 000002 2003/08/28 修正開始
                // 表示用世帯コード
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_KYUSHICHOSONCD, Type.GetType("System.String"));
                csDataColumn.MaxLength = 6;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // *履歴番号 000002 2003/08/28 修正終了
                // 表示用本人区分
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.DISP_HONNINKBMEI, Type.GetType("System.String"));
                csDataColumn.MaxLength = 3;
                csDoitsuninDataTable.Columns.Add(csDataColumn);

                // 履歴番号 000003 2007/05/22 追加開始
                // 宛名データ種別
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.ATENADATASHU, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 履歴番号 000003 2007/05/22 追加終了

                // 履歴番号 000005 2014/09/01 追加開始
                // 個人番号
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.MYNUMBER, Type.GetType("System.String"));
                csDataColumn.MaxLength = 13;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 宛名データ区分
                csDataColumn = new DataColumn(ABDoitsuninDataEntity.ATENADATAKB, Type.GetType("System.String"));
                csDataColumn.MaxLength = 2;
                csDoitsuninDataTable.Columns.Add(csDataColumn);
                // 履歴番号 000005 2014/09/01 追加終了

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // システムエラーをスローする
                throw objExp;
            }

            return csDoitsuninDataEntity;

        }

        #region 同一人取得
        // ************************************************************************************************
        // * メソッド名     同一人取得
        // * 
        // * 構文           Public Function GetDoitsuninData_JuminCD(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　住民コード指定で同一人を取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した合併同一人の該当データ（DataSet）
        // *                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
        // ************************************************************************************************
        public DataSet GetDoitsuninData_JuminCD(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninData_JuminCD";          // このメソッド名
            DataSet csGappeiDoitsuninEntity;                          // 合併同一人データ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfParameter;                             // パラメータクラス
            UFParameterCollectionClass cfParameterCollection;         // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);

                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strSQL.Append(" = (SELECT ");
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD);
                strSQL.Append(" FROM ");
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME);
                strSQL.Append(" WHERE ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);
                strSQL.Append(" = ");
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> '1')");
                strSQL.Append(" AND ");
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG);
                strSQL.Append(" <> '1'");
                strSQL.Append(" ORDER BY ");
                strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfParameterCollection = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成

                // 同一人識別コード
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD;
                cfParameter.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // RDBアクセスログ出力
                m_cfLog.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfParameterCollection) + "】");

                // SQLの実行 DataSetの取得
                csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString(), ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection);


                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                // システムエラーをスローする
                throw exException;

            }

            return csGappeiDoitsuninEntity;

        }
        #endregion

        #region 同一人候補者取得
        // ************************************************************************************************
        // * メソッド名     同一人候補者取得
        // * 
        // * 構文           Public Function GetDoitsuninKohoshaData(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　住民コード指定で同一人候補者を取得する。
        // * 
        // * 引数           strJuminCD As String      :住民コード
        // * 
        // * 戻り値         取得した同一人候補者のデータ（DataSet）
        // *                   構造：csResultDS    インテリセンス：ABDoitsuninKohoshaEntity
        // ************************************************************************************************
        public DataSet GetDoitsuninKohoshaData(string strJuminCD)
        {
            const string THIS_METHOD_NAME = "GetDoitsuninKohoshaData";          // このメソッド名
            var csResultDS = default(DataSet);                                       // 同一人候補者データ
            var strSQL = new StringBuilder();                               // SQL文文字列
            UFParameterClass cfParameter;                             // パラメータクラス
            UFParameterCollectionClass cfParameterCollection;         // パラメータコレクションクラス
            ABAtenaSearchKey cSearchKey;
            ABAtenaBClass cABAtenaB;
            DataSet csDataSet;
            DataRow csRow;
            string strUmareYMD;
            string strSearchKanaShimei1;
            string strSearchKanaShimei2;
            string strSearchKanaShimei3;
            string strSearchKanaShimei4;
            string strSearchKanaShimei5;
            string strSeibetsuCd;
            int intI = 0;

            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 対象者の検索
                cSearchKey = new ABAtenaSearchKey();
                cSearchKey.p_strJuminCD = strJuminCD;
                cSearchKey.p_strJutogaiYusenKB = "1";                                // 住登外優先
                cABAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdb, ABEnumDefine.AtenaGetKB.SelectAll, true);
                cABAtenaB.m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun;           // 標準化対応
                csDataSet = cABAtenaB.GetAtenaBHoshu(1, cSearchKey);

                if (csDataSet is null)
                {
                    return csResultDS;
                }
                else if (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0)
                {
                    csRow = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0);
                    strUmareYMD = csRow.Item(ABAtenaEntity.UMAREYMD).ToString;
                    strSearchKanaShimei1 = csRow.Item(ABAtenaEntity.SEARCHKANASEIMEI).ToString;
                    if (csRow.Item(ABAtenaEntity.ATENADATAKB).ToString == ABConstClass.ATENADATAKB_HOJIN)
                    {
                        strSearchKanaShimei2 = csRow.Item(ABAtenaEntity.SEARCHKANASEI).ToString;
                    }
                    else
                    {
                        strSearchKanaShimei2 = string.Empty;
                    }
                    strSearchKanaShimei3 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI).ToString;
                    strSearchKanaShimei4 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI).ToString;
                    strSearchKanaShimei5 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI).ToString;
                    strSeibetsuCd = csRow.Item(ABAtenaEntity.SEIBETSUCD).ToString;
                }
                else
                {
                    return csResultDS;
                }

                // SQL文の作成
                strSQL.Append(CreateSelect());
                strSQL.Append(" FROM ");
                strSQL.Append(ABAtenaEntity.TABLE_NAME);
                strSQL.Append(" LEFT JOIN ");
                strSQL.Append(ABAtenaFZYEntity.TABLE_NAME);
                strSQL.AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                strSQL.AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB);
                strSQL.AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINJUTOGAIKB);
                strSQL.Append(" LEFT JOIN ");
                strSQL.Append(ABAtenaFZYHyojunEntity.TABLE_NAME);
                strSQL.AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                strSQL.AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB);
                strSQL.AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB);

                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.AppendFormat("{0}.{1} = '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUTOGAIYUSENKB);
                strSQL.AppendFormat(" AND {0}.{1} <> '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SAKUJOFG);
                strSQL.AppendFormat(" AND {0}.{1} <> ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                strSQL.Append(ABAtenaEntity.KEY_JUMINCD);
                strSQL.AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD);
                strSQL.Append(ABAtenaEntity.PARAM_UMAREYMD);
                strSQL.AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD);
                strSQL.Append(ABAtenaEntity.PARAM_SEIBETSUCD);
                // 検索カナ姓名
                strSQL.AppendFormat(" AND (( {0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI);
                strSQL.AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI);
                intI = 1;
                strSQL.AppendFormat("{0},", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString());
                // 検索カナ姓
                strSQL.AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI);
                strSQL.AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI);
                intI = 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString());
                // 検索カナ外国人名
                strSQL.AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                strSQL.AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI);
                intI = 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString());
                // 検索カナ通称名
                strSQL.AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                strSQL.AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI);
                intI = 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString());
                // 検索カナ併記名
                strSQL.AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                strSQL.AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI);
                intI = 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString());
                intI += 1;
                strSQL.AppendFormat("{0})))", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString());

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfParameterCollection = new UFParameterCollectionClass();
                // 検索条件のパラメータを作成

                // 住民コード
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABAtenaEntity.KEY_JUMINCD;
                cfParameter.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // 生年月日
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABAtenaEntity.PARAM_UMAREYMD;
                cfParameter.Value = strUmareYMD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // 性別コード
                cfParameter = new UFParameterClass();
                cfParameter.ParameterName = ABAtenaEntity.PARAM_SEIBETSUCD;
                cfParameter.Value = strSeibetsuCd;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter);

                // 検索カナ姓名
                for (intI = 1; intI <= 5; intI++)
                {
                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString();
                    switch (intI)
                    {
                        case 1:
                            {
                                cfParameter.Value = strSearchKanaShimei1;
                                break;
                            }
                        case 2:
                            {
                                cfParameter.Value = strSearchKanaShimei2;
                                break;
                            }
                        case 3:
                            {
                                cfParameter.Value = strSearchKanaShimei3;
                                break;
                            }
                        case 4:
                            {
                                cfParameter.Value = strSearchKanaShimei4;
                                break;
                            }
                        case 5:
                            {
                                cfParameter.Value = strSearchKanaShimei5;
                                break;
                            }
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfParameterCollection.Add(cfParameter);
                }

                // 検索カナ姓
                for (intI = 1; intI <= 5; intI++)
                {
                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString();
                    switch (intI)
                    {
                        case 1:
                            {
                                cfParameter.Value = strSearchKanaShimei1;
                                break;
                            }
                        case 2:
                            {
                                cfParameter.Value = strSearchKanaShimei2;
                                break;
                            }
                        case 3:
                            {
                                cfParameter.Value = strSearchKanaShimei3;
                                break;
                            }
                        case 4:
                            {
                                cfParameter.Value = strSearchKanaShimei4;
                                break;
                            }
                        case 5:
                            {
                                cfParameter.Value = strSearchKanaShimei5;
                                break;
                            }
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfParameterCollection.Add(cfParameter);
                }

                // 検索カナ外国人名
                for (intI = 1; intI <= 5; intI++)
                {
                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString();
                    switch (intI)
                    {
                        case 1:
                            {
                                cfParameter.Value = strSearchKanaShimei1;
                                break;
                            }
                        case 2:
                            {
                                cfParameter.Value = strSearchKanaShimei2;
                                break;
                            }
                        case 3:
                            {
                                cfParameter.Value = strSearchKanaShimei3;
                                break;
                            }
                        case 4:
                            {
                                cfParameter.Value = strSearchKanaShimei4;
                                break;
                            }
                        case 5:
                            {
                                cfParameter.Value = strSearchKanaShimei5;
                                break;
                            }
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfParameterCollection.Add(cfParameter);
                }

                // 検索カナ通称名
                for (intI = 1; intI <= 5; intI++)
                {
                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString();
                    switch (intI)
                    {
                        case 1:
                            {
                                cfParameter.Value = strSearchKanaShimei1;
                                break;
                            }
                        case 2:
                            {
                                cfParameter.Value = strSearchKanaShimei2;
                                break;
                            }
                        case 3:
                            {
                                cfParameter.Value = strSearchKanaShimei3;
                                break;
                            }
                        case 4:
                            {
                                cfParameter.Value = strSearchKanaShimei4;
                                break;
                            }
                        case 5:
                            {
                                cfParameter.Value = strSearchKanaShimei5;
                                break;
                            }
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfParameterCollection.Add(cfParameter);
                }

                // 検索カナ併記名
                for (intI = 1; intI <= 5; intI++)
                {
                    cfParameter = new UFParameterClass();
                    cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString();
                    switch (intI)
                    {
                        case 1:
                            {
                                cfParameter.Value = strSearchKanaShimei1;
                                break;
                            }
                        case 2:
                            {
                                cfParameter.Value = strSearchKanaShimei2;
                                break;
                            }
                        case 3:
                            {
                                cfParameter.Value = strSearchKanaShimei3;
                                break;
                            }
                        case 4:
                            {
                                cfParameter.Value = strSearchKanaShimei4;
                                break;
                            }
                        case 5:
                            {
                                cfParameter.Value = strSearchKanaShimei5;
                                break;
                            }
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfParameterCollection.Add(cfParameter);
                }

                // RDBアクセスログ出力
                m_cfLog.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString(), cfParameterCollection) + "】");

                // SQLの実行 DataSetの取得
                csResultDS = m_cfRdb.GetDataSet(strSQL.ToString(), ABDoitsuninKohoshaEntity.TABLE_NAME, cfParameterCollection);

                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");
                // システムエラーをスローする
                throw exException;

            }

            return csResultDS;

        }

        #endregion

        #region SELECT句作成
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
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT句の作成
                csSELECT.AppendFormat("SELECT {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATAKB);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATASHU);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO1);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO1);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO2);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO2);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SHIMEIYUSENKB);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSU);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHO);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKI);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.YUBINNO);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHOCD);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD1);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD2);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD3);
                csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKICD);

                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return csSELECT.ToString();

        }
        #endregion

        #endregion

    }
}
