// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ連絡先マスタ３ビジネスクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2018/05/22　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * 2018/05/22   000000      【AB24011】新規作成（石合）
// * 2024/01/11   000001      【AB-0860-1】連絡先管理項目追加
// * 2024/03/07   000002      【AB-0900-1】アドレス・ベース・レジストリ対応(下村)
// ************************************************************************************************

using System;
using System.Data;
using System.Linq;
using System.Text;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    /// <summary>
/// ＡＢ連絡先マスタ３ビジネスクラス
/// </summary>
/// <remarks></remarks>
    public class ABRenrakusaki3BClass
    {

        #region メンバー変数

        // メンバー変数
        private UFLogClass m_cfLogClass;                                              // ログ出力クラス
        private UFControlData m_cfControlData;                                        // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;                                // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                                              // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                                          // エラー処理クラス
        private UFErrorStruct m_cfErrorStruct;                                        // エラー情報構造体

        private string m_strSelectSQL;                                                // SELECT用SQL
        private UFParameterCollectionClass m_cfSelectParamCollection;                 // SELECT用パラメータコレクション

        private bool m_blnIsCreateSelectSQL;                                       // SELECT用SQL作成済みフラグ

        private DataSet m_csDataSchema;                                               // スキーマ保管用データセット

        private ABRenrakusakiFZYBClass m_cRenrakusakiFZYB;                            // ＡＢ連絡先付随マスタビジネスクラス

        // *履歴番号 000001 2024/01/11 追加開始
        private ABRenrakusakiFZYHyojunBClass m_cRenrakusakiFZYHyojunB;                // ＡＢ連絡先付随_標準マスタビジネスクラス
                                                                                      // *履歴番号 000001 2024/01/11 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABRenrakusaki3BClass";                // クラス名

        private static readonly string SQL_SAKUJOFG = string.Format("{0}.{1} = '0'", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUJOFG);

        public const string SUFFIX_JOIN = "_JOIN";                                    // 結合用サフィックス
        public const string SUFFIX_FZY = "_FZY";                                      // 付随用サフィックス
                                                                                      // *履歴番号 000001 2024/01/11 追加開始
        public const string SUFFIX_FZY_HYOJUN = "_FZY_HYOJUN";                        // 付随標準用サフィックス
                                                                                      // *履歴番号 000001 2024/01/11 追加終了

        #endregion

        #region プロパティー

        #endregion

        #region コンストラクター

        /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="cfControlData">コントロールデータ</param>
    /// <param name="cfConfigDataClass">コンフィグデータ</param>
    /// <param name="cfRdbClass">ＲＤＢクラス</param>
    /// <remarks></remarks>
        public ABRenrakusaki3BClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)


        {

            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // パラメーター変数の初期化
            m_strSelectSQL = string.Empty;
            m_cfSelectParamCollection = null;

            // SQL作成済みフラグの初期化
            m_blnIsCreateSelectSQL = false;

            // スキーマ保管用データセットの初期化
            m_csDataSchema = null;

            // ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
            m_cRenrakusakiFZYB = new ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

        }

        #endregion

        #region メソッド

        #region GetRenrakusakiTableSchema

        /// <summary>
    /// GetRenrakusakiTableSchema
    /// </summary>
    /// <returns>テーブルスキーマ</returns>
    /// <remarks></remarks>
        public DataSet GetRenrakusakiTableSchema()
        {

            DataSet csRenrakusakiEntity;

            try
            {

                // スキーマの取得
                csRenrakusakiEntity = m_cfRdbClass.GetTableSchemaNoRestriction(string.Format("SELECT * FROM {0}", ABRenrakusakiEntity.TABLE_NAME), ABRenrakusakiEntity.TABLE_NAME, false);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csRenrakusakiEntity;

        }

        #endregion

        #region GetRenrakusakiFZYTableSchema

        /// <summary>
    /// GetRenrakusakiFZYTableSchema
    /// </summary>
    /// <returns>テーブルスキーマ</returns>
    /// <remarks></remarks>
        public DataSet GetRenrakusakiFZYTableSchema()
        {

            DataSet csRenrakusakiFZYEntity;

            try
            {

                // スキーマの取得
                csRenrakusakiFZYEntity = m_cfRdbClass.GetTableSchemaNoRestriction(string.Format("SELECT * FROM {0}", ABRenrakusakiFZYEntity.TABLE_NAME), ABRenrakusakiFZYEntity.TABLE_NAME, false);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csRenrakusakiFZYEntity;

        }

        #endregion

        // *履歴番号 000001 2024/01/11 追加開始
        #region GetRenrakusakiFZYHyojunTableSchema

        /// <summary>
    /// GetRenrakusakiFZYHyojunTableSchema
    /// </summary>
    /// <returns>テーブルスキーマ</returns>
    /// <remarks></remarks>
        public DataSet GetRenrakusakiFZYHyojunTableSchema()
        {

            DataSet csRenrakusakiFZYHyojunEntity;

            try
            {

                // スキーマの取得
                csRenrakusakiFZYHyojunEntity = m_cfRdbClass.GetTableSchemaNoRestriction(string.Format("SELECT * FROM {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME), ABRenrakusakiFZYHyojunEntity.TABLE_NAME, false);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csRenrakusakiFZYHyojunEntity;

        }

        #endregion
        // *履歴番号 000001 2024/01/11 追加終了

        #region Select

        /// <summary>
    /// Select
    /// </summary>
    /// <param name="strWhere">SQL文</param>
    /// <param name="cfParamCollection">パラメーターコレクション</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>※ABRenrakusaki2BClassの動きに準拠し、削除フラグを考慮しない。</remarks>
        private DataSet Select(string strWhere, UFParameterCollectionClass cfParamCollection)

        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            string strSQL;
            DataSet csResultEntity;
            DataSet csRenrakusakiJoinEntity;
            DataSet csRenrakusakiEntity;
            DataSet csRenrakusakiFZYEntity;
            // *履歴番号 000001 2024/01/11 追加開始
            DataSet csRenrakusakiFZYHyojunEntity;
            // *履歴番号 000001 2024/01/11 追加終了

            DataRow csNewRow;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_blnIsCreateSelectSQL == false)
                {

                    CreateSelectSQL();

                    m_blnIsCreateSelectSQL = true;
                }

                else
                {
                    // noop
                }

                // WHERE区の作成
                if (strWhere.Trim().RLength() > 0)
                {
                    strSQL = string.Format(m_strSelectSQL, string.Concat(" WHERE ", strWhere));
                }
                else
                {
                    strSQL = string.Format(m_strSelectSQL, string.Empty);
                }

                // ＲＤＢアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL, cfParamCollection) + "】");




                // SQLの実行 DataSetの取得
                csRenrakusakiJoinEntity = m_csDataSchema.Clone();
                csRenrakusakiJoinEntity = m_cfRdbClass.GetDataSet(strSQL, csRenrakusakiJoinEntity, string.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN), cfParamCollection, false);

                // 取得結果を分割
                csResultEntity = new DataSet();
                csRenrakusakiEntity = GetRenrakusakiTableSchema();
                csResultEntity.Tables.Add(csRenrakusakiEntity.Tables[ABRenrakusakiEntity.TABLE_NAME].Clone());
                csRenrakusakiFZYEntity = GetRenrakusakiFZYTableSchema();
                csResultEntity.Tables.Add(csRenrakusakiFZYEntity.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Clone());
                // *履歴番号 000001 2024/01/11 追加開始
                csRenrakusakiFZYHyojunEntity = GetRenrakusakiFZYHyojunTableSchema();
                csResultEntity.Tables.Add(csRenrakusakiFZYHyojunEntity.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Clone());
                // *履歴番号 000001 2024/01/11 追加終了

                foreach (DataRow csDataRow in csRenrakusakiJoinEntity.Tables(string.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN)).Rows)
                {

                    csNewRow = csResultEntity.Tables[ABRenrakusakiEntity.TABLE_NAME].NewRow();
                    {
                        ref var withBlock = ref csNewRow;
                        withBlock.BeginEdit();
                        withBlock[ABRenrakusakiEntity.JUMINCD] = csDataRow[ABRenrakusakiEntity.JUMINCD];
                        withBlock[ABRenrakusakiEntity.SHICHOSONCD] = csDataRow[ABRenrakusakiEntity.SHICHOSONCD];
                        withBlock[ABRenrakusakiEntity.KYUSHICHOSONCD] = csDataRow[ABRenrakusakiEntity.KYUSHICHOSONCD];
                        withBlock[ABRenrakusakiEntity.GYOMUCD] = csDataRow[ABRenrakusakiEntity.GYOMUCD];
                        withBlock[ABRenrakusakiEntity.GYOMUNAISHU_CD] = csDataRow[ABRenrakusakiEntity.GYOMUNAISHU_CD];
                        // *履歴番号 000001 2024/01/11 追加開始
                        withBlock[ABRenrakusakiEntity.TOROKURENBAN] = csDataRow[ABRenrakusakiEntity.TOROKURENBAN];
                        // *履歴番号 000001 2024/01/11 追加終了
                        withBlock[ABRenrakusakiEntity.RENRAKUSAKIKB] = csDataRow[ABRenrakusakiEntity.RENRAKUSAKIKB];
                        withBlock[ABRenrakusakiEntity.RENRAKUSAKIMEI] = csDataRow[ABRenrakusakiEntity.RENRAKUSAKIMEI];
                        withBlock[ABRenrakusakiEntity.RENRAKUSAKI1] = csDataRow[ABRenrakusakiEntity.RENRAKUSAKI1];
                        withBlock[ABRenrakusakiEntity.RENRAKUSAKI2] = csDataRow[ABRenrakusakiEntity.RENRAKUSAKI2];
                        withBlock[ABRenrakusakiEntity.RENRAKUSAKI3] = csDataRow[ABRenrakusakiEntity.RENRAKUSAKI3];
                        withBlock[ABRenrakusakiEntity.RESERVE] = csDataRow[ABRenrakusakiEntity.RESERVE];
                        withBlock[ABRenrakusakiEntity.TANMATSUID] = csDataRow[ABRenrakusakiEntity.TANMATSUID];
                        withBlock[ABRenrakusakiEntity.SAKUJOFG] = csDataRow[ABRenrakusakiEntity.SAKUJOFG];
                        withBlock[ABRenrakusakiEntity.KOSHINCOUNTER] = csDataRow[ABRenrakusakiEntity.KOSHINCOUNTER];
                        withBlock[ABRenrakusakiEntity.SAKUSEINICHIJI] = csDataRow[ABRenrakusakiEntity.SAKUSEINICHIJI];
                        withBlock[ABRenrakusakiEntity.SAKUSEIUSER] = csDataRow[ABRenrakusakiEntity.SAKUSEIUSER];
                        withBlock[ABRenrakusakiEntity.KOSHINNICHIJI] = csDataRow[ABRenrakusakiEntity.KOSHINNICHIJI];
                        withBlock[ABRenrakusakiEntity.KOSHINUSER] = csDataRow[ABRenrakusakiEntity.KOSHINUSER];
                        withBlock.EndEdit();
                    }
                    csResultEntity.Tables[ABRenrakusakiEntity.TABLE_NAME].Rows.Add(csNewRow);

                    csNewRow = csResultEntity.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].NewRow();
                    {
                        ref var withBlock1 = ref csNewRow;
                        withBlock1.BeginEdit();
                        withBlock1[ABRenrakusakiFZYEntity.JUMINCD] = csDataRow[ABRenrakusakiEntity.JUMINCD];
                        withBlock1[ABRenrakusakiFZYEntity.SHICHOSONCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SHICHOSONCD, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.KYUSHICHOSONCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KYUSHICHOSONCD, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.GYOMUCD] = csDataRow[ABRenrakusakiEntity.GYOMUCD];
                        withBlock1[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD] = csDataRow[ABRenrakusakiEntity.GYOMUNAISHU_CD];
                        // *履歴番号 000001 2024/01/11 追加開始
                        withBlock1[ABRenrakusakiFZYEntity.TOROKURENBAN] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.TOROKURENBAN, SUFFIX_FZY));
                        // *履歴番号 000001 2024/01/11 追加終了
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI4] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI5] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI6] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.RESERVE] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RESERVE, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.TANMATSUID] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.TANMATSUID, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.SAKUJOFG] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUJOFG, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.KOSHINCOUNTER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINCOUNTER, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.SAKUSEINICHIJI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUSEINICHIJI, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.SAKUSEIUSER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUSEIUSER, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.KOSHINNICHIJI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINNICHIJI, SUFFIX_FZY));
                        withBlock1[ABRenrakusakiFZYEntity.KOSHINUSER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINUSER, SUFFIX_FZY));
                        withBlock1.EndEdit();
                    }
                    csResultEntity.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows.Add(csNewRow);

                    // *履歴番号 000001 2024/01/11 追加開始
                    csNewRow = csResultEntity.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].NewRow();
                    csNewRow.BeginEdit();
                    csNewRow[ABRenrakusakiFZYHyojunEntity.JUMINCD] = csDataRow[ABRenrakusakiFZYHyojunEntity.JUMINCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOMUCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUCD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.TOROKURENBAN] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.TOROKUYMD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TOROKUYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.BIKO] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.BIKO, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE1] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE1, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE2] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE2, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE3] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE3, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE4] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE4, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE5] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE5, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.TANMATSUID] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TANMATSUID, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUJOFG, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, SUFFIX_FZY_HYOJUN));
                    csNewRow[ABRenrakusakiFZYHyojunEntity.KOSHINUSER] = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINUSER, SUFFIX_FZY_HYOJUN));
                    csNewRow.EndEdit();
                    csResultEntity.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows.Add(csNewRow);
                    // *履歴番号 000001 2024/01/11 追加終了

                }

                csResultEntity.AcceptChanges();

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            // 抽出結果DataSetの返信
            return csResultEntity;

        }

        /// <summary>
    /// SelectByJuminCd
    /// </summary>
    /// <param name="strJuminCD">住民コード</param>
    /// <returns>抽出結果DataSet</returns>
    /// <remarks>※ABRenrakusaki2BClassの動きに準拠し、削除フラグを考慮しない。</remarks>
        public DataSet SelectByJuminCd(string strJuminCd)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;

            StringBuilder csSQL;
            UFParameterClass cfParam;
            DataSet csResultEntity;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // パラメーターコレクションクラスのインスタンス化
                m_cfSelectParamCollection = new UFParameterCollectionClass();


                // 住民コード
                csSQL.AppendFormat("{0}.{1} = {2} ", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiEntity.PARAM_JUMINCD);

                cfParam = new UFParameterClass();
                cfParam.ParameterName = ABRenrakusakiEntity.PARAM_JUMINCD;
                cfParam.Value = strJuminCd;

                m_cfSelectParamCollection.Add(cfParam);

                // 抽出処理を実行
                csResultEntity = Select(csSQL.ToString(), m_cfSelectParamCollection);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // エラーをそのままスローする
                throw;

            }

            // 抽出結果DataSetの返信
            return csResultEntity;

        }

        #endregion

        #region CreateSelectSQL

        /// <summary>
    /// CreateSelectSQL
    /// </summary>
    /// <remarks></remarks>
        private void CreateSelectSQL()
        {

            StringBuilder csSQL;

            try
            {

                // SQL文字列変数のインスタンス化
                csSQL = new StringBuilder(256);

                // SELECT区の生成
                csSQL.Append(CreateSelect());

                // FROM区の生成
                csSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME);
                // *履歴番号 000001 2024/01/11 修正開始
                // csSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME)
                csSQL.AppendFormat(" LEFT OUTER JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME);
                // *履歴番号 000001 2024/01/11 修正終了
                csSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD);
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD);
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD);
                // *履歴番号 000001 2024/01/11 追加開始
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN);
                csSQL.AppendFormat(" LEFT OUTER JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME);
                csSQL.AppendFormat(" ON {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD);
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                csSQL.AppendFormat(" AND {0}.{1} = {2}.{3}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN, ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);
                // *履歴番号 000001 2024/01/11 追加終了

                // スキーマの取得
                if (m_csDataSchema is null)
                {
                    m_csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), string.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN), false);
                }
                else
                {
                    // noop
                }

                // WHERE区の作成
                csSQL.Append("{0}");

                // ORDERBY区の生成
                csSQL.Append(" ORDER BY");
                csSQL.AppendFormat(" {0}.{1},", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD);
                csSQL.AppendFormat(" {0}.{1},", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD);
                csSQL.AppendFormat(" {0}.{1} ", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD);

                // メンバー変数に設定
                m_strSelectSQL = csSQL.ToString();
            }

            catch (Exception csExp)
            {
                throw;
            }

        }

        #endregion

        #region CreateSelect

        /// <summary>
    /// CreateSelect
    /// </summary>
    /// <returns>SELECT区</returns>
    /// <remarks></remarks>
        private string CreateSelect()
        {

            StringBuilder csSQL;

            try
            {

                csSQL = new StringBuilder();


                csSQL.Append("SELECT ");
                csSQL.AppendFormat("  {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SHICHOSONCD);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KYUSHICHOSONCD);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD);
                // *履歴番号 000001 2024/01/11 追加開始
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN);
                // *履歴番号 000001 2024/01/11 追加終了
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKIKB);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKIMEI);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI1);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI2);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RENRAKUSAKI3);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.RESERVE);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TANMATSUID);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUJOFG);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINCOUNTER);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUSEINICHIJI);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.SAKUSEIUSER);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINNICHIJI);
                csSQL.AppendFormat(", {0}.{1}", ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.KOSHINUSER);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SHICHOSONCD, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KYUSHICHOSONCD, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, SUFFIX_FZY);
                // *履歴番号 000001 2024/01/11 追加開始
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN, SUFFIX_FZY);
                // *履歴番号 000001 2024/01/11 追加終了
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RESERVE, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TANMATSUID, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUJOFG, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINCOUNTER, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUSEINICHIJI, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.SAKUSEIUSER, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINNICHIJI, SUFFIX_FZY);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.KOSHINUSER, SUFFIX_FZY);
                // *履歴番号 000001 2024/01/11 追加開始
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE1, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE2, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE3, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE4, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RESERVE5, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TANMATSUID, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUJOFG, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER, SUFFIX_FZY_HYOJUN);
                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, SUFFIX_FZY_HYOJUN);
                // *履歴番号 000001 2024/01/11 追加終了

                csSQL.AppendFormat(", {0}.{1} AS {1}{2}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.KOSHINUSER, SUFFIX_FZY_HYOJUN);
            }

            catch (Exception csExp)
            {
                throw;
            }

            return csSQL.ToString();

        }

        #endregion

        #region Update

        /// <summary>
    /// Update
    /// </summary>
    /// <param name="csRenrakusakiRow">連絡先マスタ</param>
    /// <param name="csRenrakusakiFZYRow">連絡先付随マスタ</param>
    /// <remarks>※更新時も削除フラグを考慮しない。</remarks>
        public void Update(DataRow csRenrakusakiRow, DataRow csRenrakusakiFZYRow)

        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;        // メソッド名
            DataSet csDataSet;
            DataRow csNewRow;
            int intKoshinCount;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
                if (m_cRenrakusakiFZYB is null)
                {
                    m_cRenrakusakiFZYB = new ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                else
                {
                    // noop
                }

                // キー情報で連絡先付随マスタを取得
                // *履歴番号 000001 2024/01/11 修正開始
                // csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.JUMINCD].ToString(), _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUCD].ToString(), _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD].ToString(), _
                // True)
                csDataSet = m_cRenrakusakiFZYB.SelectByKey(csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.JUMINCD].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUCD].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.TOROKURENBAN].ToString(), true);




                // *履歴番号 000001 2024/01/11 修正終了

                if (csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows)
                    {

                        // データ編集
                        csDataRow.BeginEdit();
                        csDataRow[ABRenrakusakiFZYEntity.SHICHOSONCD] = csRenrakusakiRow[ABRenrakusakiEntity.SHICHOSONCD];
                        csDataRow[ABRenrakusakiFZYEntity.KYUSHICHOSONCD] = csRenrakusakiRow[ABRenrakusakiEntity.KYUSHICHOSONCD];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO];
                        csDataRow[ABRenrakusakiFZYEntity.RESERVE] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RESERVE];
                        csDataRow[ABRenrakusakiFZYEntity.SAKUJOFG] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUJOFG];
                        csDataRow[ABRenrakusakiFZYEntity.KOSHINNICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.KOSHINNICHIJI];
                        csDataRow.EndEdit();

                        // 連絡先付随マスタの更新処理
                        intKoshinCount = m_cRenrakusakiFZYB.Update(csDataRow);

                        // 更新件数判定
                        if (intKoshinCount != 1)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(string.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随マスタ"), m_cfErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // noop
                        }

                    }
                }

                else
                {

                    // データ編集
                    csNewRow = csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].NewRow();
                    csNewRow.EndEdit();
                    csNewRow[ABRenrakusakiFZYEntity.JUMINCD] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.JUMINCD];
                    csNewRow[ABRenrakusakiFZYEntity.SHICHOSONCD] = csRenrakusakiRow[ABRenrakusakiEntity.SHICHOSONCD];
                    csNewRow[ABRenrakusakiFZYEntity.KYUSHICHOSONCD] = csRenrakusakiRow[ABRenrakusakiEntity.KYUSHICHOSONCD];
                    csNewRow[ABRenrakusakiFZYEntity.GYOMUCD] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUCD];
                    csNewRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD];
                    // *履歴番号 000001 2024/01/11 追加開始
                    csNewRow[ABRenrakusakiFZYEntity.TOROKURENBAN] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.TOROKURENBAN];
                    // *履歴番号 000001 2024/01/11 追加終了
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO];
                    csNewRow[ABRenrakusakiFZYEntity.RESERVE] = csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.RESERVE];
                    csNewRow[ABRenrakusakiFZYEntity.SAKUJOFG] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUJOFG];
                    csNewRow[ABRenrakusakiFZYEntity.SAKUSEINICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUSEINICHIJI];
                    csNewRow[ABRenrakusakiFZYEntity.KOSHINNICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.KOSHINNICHIJI];
                    csNewRow.EndEdit();
                    csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows.Add(csNewRow);

                    // 連絡先付随マスタの追加処理
                    m_cRenrakusakiFZYB.Insert(csNewRow);

                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbDeadLockException cfRdbDeadLockExp)   // デッドロックをキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp);
            }

            catch (UFRdbUniqueException cfUFRdbUniqueExp)     // 一意制約違反をキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp);
            }

            catch (UFRdbTimeOutException cfRdbTimeOutExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

        }

        #endregion

        // *履歴番号 000001 2024/01/11 追加開始
        #region UpdateFZYHyojun

        /// <summary>
    /// UpdateFZYHyojun
    /// </summary>
    /// <param name="csRenrakusakiRow">連絡先マスタ</param>
    /// <param name="csRenrakusakiFZYHyojunRow">連絡先付随標準マスタ</param>
    /// <remarks>※更新時も削除フラグを考慮しない。</remarks>
        public void UpdateFZYHyojun(DataRow csRenrakusakiRow, DataRow csRenrakusakiFZYHyojunRow)

        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;        // メソッド名
            DataSet csDataSet;
            DataRow csNewRow;
            int intKoshinCount;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＡＢ連絡先付随標準マスタビジネスクラスのインスタンス化
                if (m_cRenrakusakiFZYHyojunB is null)
                {
                    m_cRenrakusakiFZYHyojunB = new ABRenrakusakiFZYHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                else
                {
                    // noop
                }

                // キー情報で連絡先付随標準マスタを取得
                csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey(csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.JUMINCD].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUCD].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.TOROKURENBAN].ToString(), true);





                if (csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows)
                    {

                        // データ編集
                        csDataRow.BeginEdit();
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.BIKO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.BIKO];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RESERVE1] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE1];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RESERVE2] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE2];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RESERVE3] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE3];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RESERVE4] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE4];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.RESERVE5] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE5];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUJOFG];
                        csDataRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.KOSHINNICHIJI];

                        csDataRow.EndEdit();

                        // 連絡先付随標準マスタの更新処理
                        intKoshinCount = m_cRenrakusakiFZYHyojunB.Update(csDataRow);

                        // 更新件数判定
                        if (intKoshinCount != 1)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(string.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随標準マスタ"), m_cfErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // noop
                        }

                    }
                }

                else
                {

                    // データ編集
                    csNewRow = csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].NewRow();
                    csNewRow.EndEdit();
                    csNewRow[ABRenrakusakiFZYHyojunEntity.JUMINCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.JUMINCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOMUCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.TOROKURENBAN] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.TOROKURENBAN];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.TOROKUYMD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.TOROKUYMD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.BIKO] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.BIKO];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE1] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE1];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE2] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE2];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE3] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE3];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE4] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE4];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.RESERVE5] = csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.RESERVE5];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.SAKUJOFG] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUJOFG];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.SAKUSEINICHIJI];
                    csNewRow[ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI] = csRenrakusakiRow[ABRenrakusakiEntity.KOSHINNICHIJI];
                    csNewRow.EndEdit();
                    csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows.Add(csNewRow);

                    // 連絡先付随標準マスタの追加処理
                    m_cRenrakusakiFZYHyojunB.Insert(csNewRow);

                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbDeadLockException cfRdbDeadLockExp)   // デッドロックをキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp);
            }

            catch (UFRdbUniqueException cfUFRdbUniqueExp)     // 一意制約違反をキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp);
            }

            catch (UFRdbTimeOutException cfRdbTimeOutExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

        }

        #endregion
        // *履歴番号 000001 2024/01/11 追加終了

        #region Delete

        /// <summary>
    /// Delete
    /// </summary>
    /// <param name="csRenrakusakiFZYRow">連絡先付随マスタ</param>
    /// <remarks>※更新時も削除フラグを考慮しない。</remarks>
        public void Delete(DataRow csRenrakusakiFZYRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;        // メソッド名
            DataSet csDataSet;
            int intKoshinCount;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＡＢ連絡先付随マスタビジネスクラスのインスタンス化
                if (m_cRenrakusakiFZYB is null)
                {
                    m_cRenrakusakiFZYB = new ABRenrakusakiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                else
                {
                    // noop
                }

                // キー情報で連絡先付随マスタを取得
                // *履歴番号 000001 2024/01/11 修正開始
                // csDataSet = m_cRenrakusakiFZYB.SelectByKey( _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original].ToString(), _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original].ToString(), _
                // csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original].ToString(), _
                // True)
                csDataSet = m_cRenrakusakiFZYB.SelectByKey(csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original].ToString(), csRenrakusakiFZYRow[ABRenrakusakiFZYEntity.TOROKURENBAN, DataRowVersion.Original].ToString(), true);




                // *履歴番号 000001 2024/01/11 修正終了

                if (csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables[ABRenrakusakiFZYEntity.TABLE_NAME].Rows)
                    {

                        // 連絡先付随マスタの物理削除処理
                        intKoshinCount = m_cRenrakusakiFZYB.Delete(csDataRow);

                        // 更新件数判定
                        if (intKoshinCount != 1)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(string.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随マスタ"), m_cfErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // noop
                        }

                    }
                }

                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbDeadLockException cfRdbDeadLockExp)   // デッドロックをキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp);
            }

            catch (UFRdbUniqueException cfUFRdbUniqueExp)     // 一意制約違反をキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp);
            }

            catch (UFRdbTimeOutException cfRdbTimeOutExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

        }

        #endregion

        // *履歴番号 000001 2024/01/11 追加開始
        #region DeleteFzyHyojun

        /// <summary>
    /// DeleteFzyHyojun
    /// </summary>
    /// <param name="csRenrakusakiFZYHyojunRow">連絡先付随標準マスタ</param>
    /// <remarks>※更新時も削除フラグを考慮しない。</remarks>
        public void DeleteFzyHyojun(DataRow csRenrakusakiFZYHyojunRow)
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;        // メソッド名
            DataSet csDataSet;
            int intKoshinCount;

            try
            {

                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // ＡＢ連絡先付随標準マスタビジネスクラスのインスタンス化
                if (m_cRenrakusakiFZYHyojunB is null)
                {
                    m_cRenrakusakiFZYHyojunB = new ABRenrakusakiFZYHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                else
                {
                    // noop
                }

                // キー情報で連絡先付随標準マスタを取得
                csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey(csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.JUMINCD, DataRowVersion.Original].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUCD, DataRowVersion.Original].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, DataRowVersion.Original].ToString(), csRenrakusakiFZYHyojunRow[ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, DataRowVersion.Original].ToString(), true);





                if (csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables[ABRenrakusakiFZYHyojunEntity.TABLE_NAME].Rows)
                    {

                        // 連絡先付随標準マスタの物理削除処理
                        intKoshinCount = m_cRenrakusakiFZYHyojunB.Delete(csDataRow);

                        // 更新件数判定
                        if (intKoshinCount != 1)
                        {
                            m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                            m_cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047);
                            throw new UFAppException(string.Concat(m_cfErrorStruct.m_strErrorMessage, "連絡先付随標準マスタ"), m_cfErrorStruct.m_strErrorCode);
                        }
                        else
                        {
                            // noop
                        }

                    }
                }

                else
                {
                    // noop
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbDeadLockException cfRdbDeadLockExp)   // デッドロックをキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbDeadLockExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbDeadLockExp.Message, cfRdbDeadLockExp.p_intErrorCode, cfRdbDeadLockExp);
            }

            catch (UFRdbUniqueException cfUFRdbUniqueExp)     // 一意制約違反をキャッチ
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfUFRdbUniqueExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfUFRdbUniqueExp.Message, cfUFRdbUniqueExp.p_intErrorCode, cfUFRdbUniqueExp);
            }

            catch (UFRdbTimeOutException cfRdbTimeOutExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfRdbTimeOutExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(cfRdbTimeOutExp.Message, cfRdbTimeOutExp.p_intErrorCode, cfRdbTimeOutExp);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // エラーをそのままスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

        }

        #endregion
        // *履歴番号 000001 2024/01/11 追加終了

        #endregion

    }
}
