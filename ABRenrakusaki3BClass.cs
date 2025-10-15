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

namespace Densan.Reams.AB.AB000BB
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
            m_cfSelectParamCollection = (object)null;

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
                if (strWhere.Trim().RLength > 0)
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
                csResultEntity.Tables.Add(csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Clone);
                csRenrakusakiFZYEntity = GetRenrakusakiFZYTableSchema();
                csResultEntity.Tables.Add(csRenrakusakiFZYEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Clone);
                // *履歴番号 000001 2024/01/11 追加開始
                csRenrakusakiFZYHyojunEntity = GetRenrakusakiFZYHyojunTableSchema();
                csResultEntity.Tables.Add(csRenrakusakiFZYHyojunEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Clone);
                // *履歴番号 000001 2024/01/11 追加終了

                foreach (DataRow csDataRow in csRenrakusakiJoinEntity.Tables(string.Concat(ABRenrakusakiEntity.TABLE_NAME, SUFFIX_JOIN)).Rows)
                {

                    csNewRow = csResultEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).NewRow;
                    {
                        ref var withBlock = ref csNewRow;
                        withBlock.BeginEdit();
                        withBlock.Item(ABRenrakusakiEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiEntity.JUMINCD);
                        withBlock.Item(ABRenrakusakiEntity.SHICHOSONCD) = csDataRow.Item(ABRenrakusakiEntity.SHICHOSONCD);
                        withBlock.Item(ABRenrakusakiEntity.KYUSHICHOSONCD) = csDataRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD);
                        withBlock.Item(ABRenrakusakiEntity.GYOMUCD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUCD);
                        withBlock.Item(ABRenrakusakiEntity.GYOMUNAISHU_CD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUNAISHU_CD);
                        // *履歴番号 000001 2024/01/11 追加開始
                        withBlock.Item(ABRenrakusakiEntity.TOROKURENBAN) = csDataRow.Item(ABRenrakusakiEntity.TOROKURENBAN);
                        // *履歴番号 000001 2024/01/11 追加終了
                        withBlock.Item(ABRenrakusakiEntity.RENRAKUSAKIKB) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKIKB);
                        withBlock.Item(ABRenrakusakiEntity.RENRAKUSAKIMEI) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKIMEI);
                        withBlock.Item(ABRenrakusakiEntity.RENRAKUSAKI1) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI1);
                        withBlock.Item(ABRenrakusakiEntity.RENRAKUSAKI2) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI2);
                        withBlock.Item(ABRenrakusakiEntity.RENRAKUSAKI3) = csDataRow.Item(ABRenrakusakiEntity.RENRAKUSAKI3);
                        withBlock.Item(ABRenrakusakiEntity.RESERVE) = csDataRow.Item(ABRenrakusakiEntity.RESERVE);
                        withBlock.Item(ABRenrakusakiEntity.TANMATSUID) = csDataRow.Item(ABRenrakusakiEntity.TANMATSUID);
                        withBlock.Item(ABRenrakusakiEntity.SAKUJOFG) = csDataRow.Item(ABRenrakusakiEntity.SAKUJOFG);
                        withBlock.Item(ABRenrakusakiEntity.KOSHINCOUNTER) = csDataRow.Item(ABRenrakusakiEntity.KOSHINCOUNTER);
                        withBlock.Item(ABRenrakusakiEntity.SAKUSEINICHIJI) = csDataRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI);
                        withBlock.Item(ABRenrakusakiEntity.SAKUSEIUSER) = csDataRow.Item(ABRenrakusakiEntity.SAKUSEIUSER);
                        withBlock.Item(ABRenrakusakiEntity.KOSHINNICHIJI) = csDataRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI);
                        withBlock.Item(ABRenrakusakiEntity.KOSHINUSER) = csDataRow.Item(ABRenrakusakiEntity.KOSHINUSER);
                        withBlock.EndEdit();
                    }
                    csResultEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Add(csNewRow);

                    csNewRow = csResultEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).NewRow;
                    {
                        ref var withBlock1 = ref csNewRow;
                        withBlock1.BeginEdit();
                        withBlock1.Item(ABRenrakusakiFZYEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiEntity.JUMINCD);
                        withBlock1.Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SHICHOSONCD, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KYUSHICHOSONCD, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.GYOMUCD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUCD);
                        withBlock1.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD) = csDataRow.Item(ABRenrakusakiEntity.GYOMUNAISHU_CD);
                        // *履歴番号 000001 2024/01/11 追加開始
                        withBlock1.Item(ABRenrakusakiFZYEntity.TOROKURENBAN) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.TOROKURENBAN, SUFFIX_FZY));
                        // *履歴番号 000001 2024/01/11 追加終了
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.RESERVE) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.RESERVE, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.TANMATSUID) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.TANMATSUID, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUJOFG, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.KOSHINCOUNTER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINCOUNTER, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.SAKUSEINICHIJI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUSEINICHIJI, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.SAKUSEIUSER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.SAKUSEIUSER, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINNICHIJI, SUFFIX_FZY));
                        withBlock1.Item(ABRenrakusakiFZYEntity.KOSHINUSER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYEntity.KOSHINUSER, SUFFIX_FZY));
                        withBlock1.EndEdit();
                    }
                    csResultEntity.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Add(csNewRow);

                    // *履歴番号 000001 2024/01/11 追加開始
                    csNewRow = csResultEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).NewRow;
                    csNewRow.BeginEdit();
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD) = csDataRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUCD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TOROKUYMD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.BIKO, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE1, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE2, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE3, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE4, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.RESERVE5, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.TANMATSUID) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.TANMATSUID, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUJOFG, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINCOUNTER, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.SAKUSEIUSER, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI, SUFFIX_FZY_HYOJUN));
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.KOSHINUSER) = csDataRow.Item(string.Concat(ABRenrakusakiFZYHyojunEntity.KOSHINUSER, SUFFIX_FZY_HYOJUN));
                    csNewRow.EndEdit();
                    csResultEntity.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Add(csNewRow);
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
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD).ToString, _
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD).ToString, _
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD).ToString, _
                // True)
                csDataSet = m_cRenrakusakiFZYB.SelectByKey(csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN).ToString, true);




                // *履歴番号 000001 2024/01/11 修正終了

                if (csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows)
                    {

                        // データ編集
                        csDataRow.BeginEdit();
                        csDataRow.Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SHICHOSONCD);
                        csDataRow.Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO);
                        csDataRow.Item(ABRenrakusakiFZYEntity.RESERVE) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RESERVE);
                        csDataRow.Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG);
                        csDataRow.Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI);
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
                    csNewRow = csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).NewRow;
                    csNewRow.EndEdit();
                    csNewRow.Item(ABRenrakusakiFZYEntity.JUMINCD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD);
                    csNewRow.Item(ABRenrakusakiFZYEntity.SHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SHICHOSONCD);
                    csNewRow.Item(ABRenrakusakiFZYEntity.KYUSHICHOSONCD) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KYUSHICHOSONCD);
                    csNewRow.Item(ABRenrakusakiFZYEntity.GYOMUCD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD);
                    csNewRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD);
                    // *履歴番号 000001 2024/01/11 追加開始
                    csNewRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN);
                    // *履歴番号 000001 2024/01/11 追加終了
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO);
                    csNewRow.Item(ABRenrakusakiFZYEntity.RESERVE) = csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.RESERVE);
                    csNewRow.Item(ABRenrakusakiFZYEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG);
                    csNewRow.Item(ABRenrakusakiFZYEntity.SAKUSEINICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI);
                    csNewRow.Item(ABRenrakusakiFZYEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI);
                    csNewRow.EndEdit();
                    csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Add(csNewRow);

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
                csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey(csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN).ToString, true);





                if (csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows)
                    {

                        // データ編集
                        csDataRow.BeginEdit();
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG);
                        csDataRow.Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI);

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
                    csNewRow = csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).NewRow;
                    csNewRow.EndEdit();
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKUYMD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.BIKO);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE1);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE2);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE3);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE4);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5) = csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.RESERVE5);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUJOFG) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUJOFG);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.SAKUSEINICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.SAKUSEINICHIJI);
                    csNewRow.Item(ABRenrakusakiFZYHyojunEntity.KOSHINNICHIJI) = csRenrakusakiRow.Item(ABRenrakusakiEntity.KOSHINNICHIJI);
                    csNewRow.EndEdit();
                    csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Add(csNewRow);

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
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original).ToString, _
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original).ToString, _
                // csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, _
                // True)
                csDataSet = m_cRenrakusakiFZYB.SelectByKey(csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.JUMINCD, DataRowVersion.Original).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUCD, DataRowVersion.Original).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, csRenrakusakiFZYRow.Item(ABRenrakusakiFZYEntity.TOROKURENBAN, DataRowVersion.Original).ToString, true);




                // *履歴番号 000001 2024/01/11 修正終了

                if (csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABRenrakusakiFZYEntity.TABLE_NAME).Rows)
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
                csDataSet = m_cRenrakusakiFZYHyojunB.SelectByKey(csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.JUMINCD, DataRowVersion.Original).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUCD, DataRowVersion.Original).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD, DataRowVersion.Original).ToString, csRenrakusakiFZYHyojunRow.Item(ABRenrakusakiFZYHyojunEntity.TOROKURENBAN, DataRowVersion.Original).ToString, true);





                if (csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)
                {

                    foreach (DataRow csDataRow in csDataSet.Tables(ABRenrakusakiFZYHyojunEntity.TABLE_NAME).Rows)
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