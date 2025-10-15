// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ住登外マスタＤＡ(ABJutogaiBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/20　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/12 000001     有効桁数の対応
// * 2003/03/25 000002     郵便番号が追加になりました。
// * 2003/04/16 000003     生和暦年月日の日付チェックを数値チェックに変更
// *                       検索用カナの半角カナチェックをＡＮＫチェックに変更
// * 2003/05/21 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000005     RDBアクセスログの修正
// * 2003/09/11 000006     端末ＩＤ整合性チェックをANKにする
// * 2003/10/09 000007     作成ユーザー・更新ユーザーチェックの変更
// * 2003/10/30 000008     仕様変更、カタカナチェックをANKチェックに変更
// * 2004/05/13 000009     仕様変更、汎用区分をANKチェックに変更
// * 2005/01/15 000010     仕様変更、住所コードをANKチェックに変更
// * 2005/06/16 000011     SQL文をInsert,Update,論理Delete,物理Deleteの各メソッドが呼ばれた時に各自作成する(マルゴ村山)
// * 2005/12/26 000012     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
// * 2010/04/16 000013     VS2008対応（比嘉）
// * 2011/10/24 000014     【AB17010】＜住基法改正対応＞宛名付随マスタ追加   (小松)
// * 2023/08/14 000015    【AB-0820-1】住登外管理項目追加(早崎)
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
    // * 住登外マスタ取得時に使用するパラメータクラス
    // *
    // ************************************************************************************************
    public class ABJutogaiBClass
    {
        #region メンバ変数
        // パラメータのメンバ変数
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private string m_strInsertSQL;                        // INSERT用SQL
        private string m_strUpdateSQL;                        // UPDATE用SQL
        private string m_strDelRonriSQL;                      // 論理削除用SQL
        private string m_strDelButuriSQL;                     // 物理削除用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollection;       // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollection;       // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDelRonriUFParameterCollection;     // 論理削除用パラメータコレクション
        private UFParameterCollectionClass m_cfDelButuriUFParameterCollection;    // 物理削除用パラメータコレクション

        // *履歴番号 000014 2011/10/24 追加開始
        private ABSekoYMDHanteiBClass m_csSekoYMDHanteiB;             // 施行日判定Bｸﾗｽ
        private ABAtenaFZYBClass m_csAtenaFZYB;                       // 宛名付随マスタBｸﾗｽ
        private string m_strJukihoKaiseiKB;                           // 住基法改正区分
                                                                      // *履歴番号 000014 2011/10/24 追加終了
                                                                      // *履歴番号 000015 2023/08/14 追加開始
        private bool m_blnJukihoKaiseiFG = false;
        // *履歴番号 000015 2023/08/14 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABJutogaiBClass";                 // クラス名
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
        private const string JUKIHOKAISEIKB_ON = "1";

        #endregion

        #region プロパティ
        // *履歴番号 000014 2011/10/24 追加開始
        public string p_strJukihoKaiseiKB      // 住基法改正区分
        {
            set
            {
                m_strJukihoKaiseiKB = value;
            }
        }
        // *履歴番号 000014 2011/10/24 追加終了
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
        public ABJutogaiBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)
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
            m_cfInsertUFParameterCollection = null;
            m_cfUpdateUFParameterCollection = null;
            m_cfDelRonriUFParameterCollection = null;
            m_cfDelButuriUFParameterCollection = null;

            // *履歴番号 000014 2011/10/24 追加開始
            m_strJukihoKaiseiKB = string.Empty;
            // *履歴番号 000014 2011/10/24 追加終了
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     住登外マスタ抽出
        // * 
        // * 構文           Public Function GetJutogaiBHoshu() As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         DataSet : 取得した住登外マスタの該当データ
        // ************************************************************************************************
        public DataSet GetJutogaiBHoshu()
        {

            return GetJutogaiBHoshu(false);

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ抽出
        // * 
        // * 構文           Public Function GetJutogaiBHoshu(ByVal blnSakujoFG As Boolean) As DataSet
        // * 
        // * 機能           住登外マスタより全件データを取得する
        // * 
        // * 引数           blnSakujoFG   : 削除フラグ（省略可）
        // * 
        // * 戻り値         DataSet : 取得した住登外マスタの該当データ
        // ************************************************************************************************
        public DataSet GetJutogaiBHoshu(bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetJutogaiBHoshu";
            DataSet csJutogaiEntity;
            string strSQL;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                if (blnSakujoFG == true)
                {
                    strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME;
                }
                else
                {
                    strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " + ABJutogaiEntity.SAKUJOFG + " <> '1';";
                }

                // *履歴番号 000015 2023/08/14 追加開始
                // 施行日以降フラグを取得する
                m_csSekoYMDHanteiB = new ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD();

                // 住基法改正以降のとき、は宛名_標準、宛名付随_標準をLEFT OUTER JOINして取得する
                if (m_blnJukihoKaiseiFG)
                {
                    strSQL = "SELECT A.* FROM (" + strSQL + ") A";
                    strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaHyojunEntity.TABLE_NAME + " B ON A." + ABJutogaiEntity.JUMINCD + "  = B." + ABAtenaHyojunEntity.JUMINCD;
                    strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaFZYHyojunEntity.TABLE_NAME + " C ON A." + ABJutogaiEntity.JUMINCD + " = C." + ABAtenaFZYHyojunEntity.JUMINCD;
                }
                // *履歴番号 000015 2023/08/14 追加終了

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + THIS_CLASS_NAME + "】" +
                // "【メソッド名:" + THIS_METHOD_NAME + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + strSQL + "】")

                // SQLの実行 DataSetの取得
                csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL, ABJutogaiEntity.TABLE_NAME);


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

            return csJutogaiEntity;

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ抽出
        // * 
        // * 構文           Public Function GetJutogaiBHoshu(ByVal strJuminCD As String) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード（省略可）
        // * 
        // * 戻り値         DataSet : 取得した住登外マスタの該当データ
        // ************************************************************************************************
        public DataSet GetJutogaiBHoshu(string strJuminCD)
        {

            return GetJutogaiBHoshu(strJuminCD, false);

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ抽出
        // * 
        // * 構文           Public Function GetJutogaiBHoshu(Optional ByVal strJuminCD As String = "", _
        // *                                Optional ByVal blnSakujoFG As Boolean = False) As DataSet
        // * 
        // * 機能　　    　　住登外マスタより該当データを取得する
        // * 
        // * 引数           strJuminCD    : 住民コード
        // *                blnSakujoFG   : 削除フラグ
        // * 
        // * 戻り値         DataSet : 取得した住登外マスタの該当データ
        // ************************************************************************************************
        public DataSet GetJutogaiBHoshu(string strJuminCD, bool blnSakujoFG)
        {
            const string THIS_METHOD_NAME = "GetJutogaiBHoshu";
            DataSet csJutogaiEntity;
            var strSQL = new StringBuilder();
            UFParameterClass cfUFParameterClass;
            UFParameterCollectionClass cfUFParameterCollectionClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                // *履歴番号 000014 2011/10/24 修正開始
                // 住基法改正以降は宛名付随マスタを付加
                if ((m_strJukihoKaiseiKB ?? "") == JUKIHOKAISEIKB_ON)
                {
                    strSQL.AppendFormat("SELECT {0}.* ", ABJutogaiEntity.TABLE_NAME);
                    SetFZYEntity(ref strSQL);
                    strSQL.AppendFormat(" FROM {0} ", ABJutogaiEntity.TABLE_NAME);
                    SetFZYJoin(ref strSQL);
                    strSQL.AppendFormat(" WHERE {0}.{1}={2} ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD, ABJutogaiEntity.KEY_JUMINCD);
                    if (blnSakujoFG == false)
                    {
                        strSQL.AppendFormat(" AND {0}.{1} <> '1' ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.SAKUJOFG);
                    }
                }
                else
                {
                    strSQL.Append("SELECT * FROM ");
                    strSQL.Append(ABJutogaiEntity.TABLE_NAME);
                    strSQL.Append(" WHERE ");
                    strSQL.Append(ABJutogaiEntity.JUMINCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABJutogaiEntity.KEY_JUMINCD);
                    if (blnSakujoFG == false)
                    {
                        strSQL.Append(" AND ");
                        strSQL.Append(ABJutogaiEntity.SAKUJOFG);
                        strSQL.Append(" <> '1';");
                    }
                }
                // *履歴番号 000014 2011/10/24 修正終了

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaJiteEntity.KEY_JUMINCD;
                cfUFParameterClass.Value = strJuminCD;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:GetDataSet】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABJutogaiEntity.TABLE_NAME, cfUFParameterCollectionClass);


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

            return csJutogaiEntity;

        }

        // *履歴番号 000014 2011/10/24 追加開始
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
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TABLEINSERTKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.LINKNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINHYOJOTAIKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKYOCHITODOKEFLG);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KATAKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.UMAREFUSHOKBN);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHACD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHAMEISHO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUCARDNO);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEISTYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEIEDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSKANAMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANAHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANATSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE1);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE2);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE3);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE4);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE5);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE6);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE7);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE8);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE9);
            strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE10);
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
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYEntity.TABLE_NAME);
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD, ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD);
        }
        // *履歴番号 000014 2011/10/24 追加終了

        // ************************************************************************************************
        // * メソッド名     住登外マスタ追加
        // * 
        // * 構文           Public Function InsertJutogaiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　住登外マスタにデータを追加する
        // * 
        // * 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 追加したデータの件数
        // ************************************************************************************************
        public int InsertJutogaiB(DataRow csDataRow)
        {

            // * corresponds to VS2008 Start 2010/04/16 000013
            // Dim csInstRow As DataRow
            // * corresponds to VS2008 End 2010/04/16 000013
            const string THIS_METHOD_NAME = "InsertJutogaiB";
            int intInsCnt;        // 追加件数
            string strUpdateDateTime;


            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollection is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateInsertSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");  // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABJutogaiEntity.TANMATSUID] = m_cfControlData.m_strClientId;   // 端末ＩＤ
                csDataRow[ABJutogaiEntity.SAKUJOFG] = "0";                               // 削除フラグ
                csDataRow[ABJutogaiEntity.KOSHINCOUNTER] = decimal.Zero;                 // 更新カウンタ
                csDataRow[ABJutogaiEntity.SAKUSEINICHIJI] = strUpdateDateTime;           // 作成日時
                csDataRow[ABJutogaiEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;    // 作成ユーザー
                csDataRow[ABJutogaiEntity.KOSHINNICHIJI] = strUpdateDateTime;            // 更新日時
                csDataRow[ABJutogaiEntity.KOSHINUSER] = m_cfControlData.m_strUserId;     // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim());

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollection)
                    cfParam.Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollection) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollection);

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

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ更新
        // * 
        // * 構文           Public Function UpdateJutogaiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　住登外マスタのデータを更新する
        // * 
        // * 引数           csDataRow As DataRow : 更新するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 更新したデータの件数
        // ************************************************************************************************
        public int UpdateJutogaiB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateJutogaiB";                     // パラメータクラス
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000013
                                                                                  // Dim csDataColumn As DataColumn
                                                                                  // * corresponds to VS2008 End 2010/04/16 000013
            int intUpdCnt;                            // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollection is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateUpdateSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }

                // 共通項目の編集を行う
                csDataRow[ABJutogaiEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                   // 端末ＩＤ
                csDataRow[ABJutogaiEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABJutogaiEntity.KOSHINCOUNTER]) + 1m;           // 更新カウンタ
                csDataRow[ABJutogaiEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow[ABJutogaiEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                     // 更新ユーザー


                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollection)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength()) == ABJutogaiEntity.PREFIX_KEY)
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollection[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString().Trim());
                        this.m_cfUpdateUFParameterCollection[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollection) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollection);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan");
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

            return intUpdCnt;

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ削除
        // * 
        // * 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　　住登外マスタのデータを論理削除する
        // * 
        // * 引数           csDataRow As DataRow : 論理削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 論理削除したデータの件数
        // ************************************************************************************************
        public int DeleteJutogaiB(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateJutogaiB";                     // パラメータクラス
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000013
                                                                                  // Dim csDataColumn As DataColumn
                                                                                  // * corresponds to VS2008 End 2010/04/16 000013
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDelRonriSQL is null | string.IsNullOrEmpty(m_strDelRonriSQL) | m_cfDelRonriUFParameterCollection is null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateDeleteRonriSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }


                // 共通項目の編集を行う
                csDataRow[ABJutogaiEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                     // 端末ＩＤ
                csDataRow[ABJutogaiEntity.SAKUJOFG] = "1";                                                                   // 削除フラグ
                csDataRow[ABJutogaiEntity.KOSHINCOUNTER] = UFVBAPI.ToDecimal(csDataRow[ABJutogaiEntity.KOSHINCOUNTER]) + 1m;               // 更新カウンタ
                csDataRow[ABJutogaiEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");     // 更新日時
                csDataRow[ABJutogaiEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                       // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelRonriUFParameterCollection)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength()) == ABJutogaiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelRonriUFParameterCollection[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString().Trim());
                        this.m_cfDelRonriUFParameterCollection[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection);

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

            return intDelCnt;

        }

        // ************************************************************************************************
        // * メソッド名     住登外マスタ物理削除
        // * 
        // * 構文           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow, _
        // *                                               ByVal strSakujoKB As String) As Integer
        // * 
        // * 機能　　    　　住登外マスタのデータを物理削除する
        // * 
        // * 引数           csDataRow As DataRow : 削除するデータの含まれるDataRowオブジェクト
        // * 
        // * 戻り値         Integer : 削除したデータの件数
        // ************************************************************************************************
        public int DeleteJutogaiB(DataRow csDataRow, string strSakujoKB)
        {
            const string THIS_METHOD_NAME = "DeleteJutogaiB";
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
                                                          // パラメータクラス
                                                          // * corresponds to VS2008 Start 2010/04/16 000013
                                                          // Dim csDataColumn As DataColumn
                                                          // * corresponds to VS2008 End 2010/04/16 000013
            int intDelCnt;                            // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除区分のチェックを行う
                if (!(strSakujoKB == "D"))
                {
                    // エラー定義を取得
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                }

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDelButuriSQL is null | string.IsNullOrEmpty(m_strDelButuriSQL) | m_cfDelButuriUFParameterCollection == null)
                {
                    // * 履歴番号 000011 2005/06/16 追加開始
                    // Call CreateSQL(csDataRow)
                    CreateDeleteButsuriSQL(csDataRow);
                    // * 履歴番号 000011 2005/06/16 追加終了
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDelButuriUFParameterCollection)
                {

                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength()) == ABJutogaiEntity.PREFIX_KEY)
                    {
                        this.m_cfDelButuriUFParameterCollection[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                }


                // *履歴番号 000005 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // ' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
                // m_cfLogClass.RdbWrite(m_cfControlData,
                // "【クラス名:" + Me.GetType.Name + "】" +
                // "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                // "【実行メソッド名:ExecuteSQL】" +
                // "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection) + "】")
                // *履歴番号 000005 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection);

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

            return intDelCnt;

        }

        // * corresponds to VS2008 Start 2010/04/16 000013
        // '* 履歴番号 000011 2005/06/16 削除開始
        // ''''************************************************************************************************
        // ''''* メソッド名     SQL文の作成
        // ''''* 
        // ''''* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // ''''* 
        // ''''* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
        // ''''* 
        // ''''* 引数           csDataRow As DataRow : 更新対象の行
        // ''''* 
        // ''''* 戻り値         なし
        // ''''************************************************************************************************
        // '''Private Sub CreateSQL(ByVal csDataRow As DataRow)

        // '''    Const THIS_METHOD_NAME As String = "CreateSQL"
        // '''    Dim csDataColumn As DataColumn
        // '''    Dim strInsertColumn As String                       'INSERT用カラム
        // '''    Dim strInsertParam As String
        // '''    Dim cfUFParameterClass As UFParameterClass
        // '''    Dim strUpdateWhere As String
        // '''    Dim strUpdateParam As String
        // '''    Dim csDelRonriSQL As New StringBuilder()            '論理削除用SQL

        // '''    Try
        // '''        ' デバッグログ出力
        // '''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''        ' SELECT SQL文の作成
        // '''        m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " "
        // '''        strInsertColumn = ""
        // '''        strInsertParam = ""

        // '''        ' UPDATE SQL文の作成
        // '''        m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET "
        // '''        strUpdateParam = ""
        // '''        strUpdateWhere = ""

        // '''        ' 論理DELETE SQL文の作成
        // '''        csDelRonriSQL.Append("UPDATE ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME)
        // '''        csDelRonriSQL.Append(" SET ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID)
        // '''        csDelRonriSQL.Append(", ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG)
        // '''        csDelRonriSQL.Append(", ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER)
        // '''        csDelRonriSQL.Append(", ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI)
        // '''        csDelRonriSQL.Append(", ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER)
        // '''        csDelRonriSQL.Append(" WHERE ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
        // '''        csDelRonriSQL.Append(" AND ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
        // '''        csDelRonriSQL.Append(" = ")
        // '''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER)


        // '''        ' 物理DELETE SQL文の作成
        // '''        m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " + _
        // '''                         ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
        // '''                         ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

        // '''        ' SELECT パラメータコレクションクラスのインスタンス化
        // '''        m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

        // '''        ' UPDATE パラメータコレクションのインスタンス化
        // '''        m_cfUpdateUFParameterCollection = New UFParameterCollectionClass()

        // '''        ' 論理削除用パラメータコレクションのインスタンス化
        // '''        m_cfDelRonriUFParameterCollection = New UFParameterCollectionClass()

        // '''        ' 物理削除用パラメータコレクションのインスタンス化
        // '''        m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

        // '''        ' デバッグログ出力
        // '''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, "UFParameterCollectionClass End")


        // '''        ' パラメータコレクションの作成
        // '''        For Each csDataColumn In csDataRow.Table.Columns
        // '''            cfUFParameterClass = New UFParameterClass()

        // '''            ' INSERT SQL文の作成
        // '''            strInsertColumn += csDataColumn.ColumnName + ", "
        // '''            strInsertParam += ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // '''            ' UPDATE SQL文の作成
        // '''            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

        // '''            ' INSERT コレクションにパラメータを追加
        // '''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // '''            m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

        // '''            ' UPDATE コレクションにパラメータを追加
        // '''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
        // '''            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

        // '''        Next csDataColumn

        // '''        ' INSERT SQL文のトリミング
        // '''        strInsertColumn = strInsertColumn.Trim()
        // '''        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
        // '''        strInsertParam = strInsertParam.Trim()
        // '''        strInsertParam = strInsertParam.Trim(CType(",", Char))

        // '''        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

        // '''        ' UPDATE SQL文のトリミング
        // '''        m_strUpdateSQL = m_strUpdateSQL.Trim()
        // '''        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

        // '''        ' UPDATE SQL文にWHERE句の追加
        // '''        m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
        // '''                                      ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER


        // '''        ' UPDATE コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
        // '''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
        // '''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

        // '''        ' 論理削除用コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
        // '''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

        // '''        ' 物理削除用コレクションにパラメータを追加
        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
        // '''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

        // '''        cfUFParameterClass = New UFParameterClass()
        // '''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
        // '''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)


        // '''        'パラメータ変数へ格納
        // '''        m_strDelRonriSQL = csDelRonriSQL.ToString

        // '''        ' デバッグログ出力
        // '''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        // '''    Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
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
        // '''        ' システムエラーをスローする
        // '''        Throw objExp

        // '''    End Try

        // '''End Sub
        // '* 履歴番号 000011 2005/06/16 削除終了
        // * corresponds to VS2008 End 2010/04/16 000013
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
            StringBuilder csInsertColumn;                 // INSERT用カラム定義
            StringBuilder csInsertParam;                  // INSERT用パラメータ定義
            UFParameterClass cfUFParameterClass;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " ";
                csInsertColumn = new StringBuilder();
                csInsertParam = new StringBuilder();

                // INSERT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollection = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    csInsertColumn.Append(csDataColumn.ColumnName);
                    csInsertColumn.Append(", ");
                    csInsertParam.Append(ABJutogaiEntity.PARAM_PLACEHOLDER);
                    csInsertParam.Append(csDataColumn.ColumnName);
                    csInsertParam.Append(", ");

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollection.Add(cfUFParameterClass);

                }

                // 最後のカンマを取り除いてINSERT文を作成
                m_strInsertSQL += "(" + csInsertColumn.ToString().Trim().Trim(",") + ")" + " VALUES (" + csInsertParam.ToString().Trim().TrimEnd(",") + ")";

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
            StringBuilder csUpdateParam;                  // UPDATE用SQL定義

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET ";
                csUpdateParam = new StringBuilder();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollection = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    // 住民ＣＤ・作成日時・作成ユーザは更新しない
                    if (!(csDataColumn.ColumnName == ABJutogaiEntity.JUMINCD) && !(csDataColumn.ColumnName == ABJutogaiEntity.SAKUSEIUSER) && !(csDataColumn.ColumnName == ABJutogaiEntity.SAKUSEINICHIJI))
                    {

                        cfUFParameterClass = new UFParameterClass();

                        // UPDATE SQL文の作成
                        m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                        // UPDATE コレクションにパラメータを追加
                        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass);
                    }

                }

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.ToString().Trim();
                m_strUpdateSQL = m_strUpdateSQL.ToString().Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER;

                // UPDATE コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD;
                m_cfUpdateUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollection.Add(cfUFParameterClass);

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
            var csDelRonriSQL = new StringBuilder();            // 論理削除用SQL

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 論理DELETE SQL文の作成
                csDelRonriSQL.Append("UPDATE ");
                csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME);
                csDelRonriSQL.Append(" SET ");
                csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID);
                csDelRonriSQL.Append(", ");
                csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG);
                csDelRonriSQL.Append(", ");
                csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER);
                csDelRonriSQL.Append(", ");
                csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI);
                csDelRonriSQL.Append(", ");
                csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER);
                csDelRonriSQL.Append(" WHERE ");
                csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD);
                csDelRonriSQL.Append(" AND ");
                csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER);
                csDelRonriSQL.Append(" = ");
                csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER);

                // 論理削除用パラメータコレクションのインスタンス化
                m_cfDelRonriUFParameterCollection = new UFParameterCollectionClass();

                // 論理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER;
                m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass);

                // パラメータ変数へ格納
                m_strDelRonriSQL = csDelRonriSQL.ToString();

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

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 物理DELETE SQL文の作成
                m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER;

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDelButuriUFParameterCollection = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD;
                m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass);

                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER;
                m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass);

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
        // * 履歴番号 000011 2005/06/16 追加終了

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
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
            const string TABLENAME = "住登外．";
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

                    case var @case when @case == ABJutogaiEntity.JUMINCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUMINCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case1 when case1 == ABJutogaiEntity.SHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case2 when case2 == ABJutogaiEntity.KYUSHICHOSONCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case3 when case3 == ABJutogaiEntity.STAICD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_STAICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case4 when case4 == ABJutogaiEntity.ATENADATAKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATAKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case5 when case5 == ABJutogaiEntity.ATENADATASHU:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATASHU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case6 when case6 == ABJutogaiEntity.SEARCHKANASEIMEI:
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

                    case var case7 when case7 == ABJutogaiEntity.SEARCHKANASEI:
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

                    case var case8 when case8 == ABJutogaiEntity.SEARCHKANAMEI:
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

                    case var case9 when case9 == ABJutogaiEntity.KANAMEISHO1:
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case10 when case10 == ABJutogaiEntity.KANJIMEISHO1:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case11 when case11 == ABJutogaiEntity.KANAMEISHO2:
                        {
                            // *履歴番号 000008 2003/10/30 修正開始
                            // If (Not UFStringClass.CheckKataKana(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // *履歴番号 000008 2003/10/30 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case12 when case12 == ABJutogaiEntity.KANJIMEISHO2:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case13 when case13 == ABJutogaiEntity.UMAREYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_UMAREYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case14 when case14 == ABJutogaiEntity.UMAREWMD:               // 生和暦年月日
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得(数字項目入力の誤りです。：)
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case15 when case15 == ABJutogaiEntity.SEIBETSUCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case16 when case16 == ABJutogaiEntity.SEIBETSU:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case17 when case17 == ABJutogaiEntity.ZOKUGARACD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case18 when case18 == ABJutogaiEntity.ZOKUGARA:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case19 when case19 == ABJutogaiEntity.DAI2ZOKUGARACD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARACD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case20 when case20 == ABJutogaiEntity.DAI2ZOKUGARA:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARA);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case21 when case21 == ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case22 when case22 == ABJutogaiEntity.HANYOKB1:
                        {
                            // *履歴番号 000009 2004/05/13 修正開始
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // If (Not UFStringClass.CheckNumber(strValue)) Then
                                // *履歴番号 000009 2004/05/13 修正開始
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case23 when case23 == ABJutogaiEntity.KANJIHJNKEITAI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNKEITAI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case24 when case24 == ABJutogaiEntity.KJNHJNKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KJNHJNKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case25 when case25 == ABJutogaiEntity.HANYOKB2:
                        {
                            // *履歴番号 000009 2004/05/13 修正開始
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // If (Not UFStringClass.CheckNumber(strValue)) Then
                                // *履歴番号 000009 2004/05/13 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case26 when case26 == ABJutogaiEntity.KANNAIKANGAIKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANNAIKANGAIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case27 when case27 == ABJutogaiEntity.KAOKUSHIKIKB:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KAOKUSHIKIKB);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case28 when case28 == ABJutogaiEntity.BIKOZEIMOKU:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BIKOZEIMOKU);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case29 when case29 == ABJutogaiEntity.YUBINNO:                // 郵便番号
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "郵便番号", objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case30 when case30 == ABJutogaiEntity.JUSHOCD:
                        {
                            // *履歴番号 000010 2005/01/15 修正開始
                            // If (Not UFStringClass.CheckNumber(strValue.TrimStart())) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // *履歴番号 000010 2005/01/15 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHOCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case31 when case31 == ABJutogaiEntity.JUSHO:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case32 when case32 == ABJutogaiEntity.BANCHICD1:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case33 when case33 == ABJutogaiEntity.BANCHICD2:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case34 when case34 == ABJutogaiEntity.BANCHICD3:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case35 when case35 == ABJutogaiEntity.BANCHI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case36 when case36 == ABJutogaiEntity.KATAGAKIFG:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKIFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case37 when case37 == ABJutogaiEntity.KATAGAKICD:
                        {
                            if (!UFStringClass.CheckNumber(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKICD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case38 when case38 == ABJutogaiEntity.KATAGAKI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case39 when case39 == ABJutogaiEntity.RENRAKUSAKI1:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case40 when case40 == ABJutogaiEntity.RENRAKUSAKI2:
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case41 when case41 == ABJutogaiEntity.GYOSEIKUCD:
                        {
                            // * 履歴番号 000012 2005/12/26 修正開始
                            // 'If (Not UFStringClass.CheckNumber(strValue.TrimStart())) Then
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // * 履歴番号 000012 2005/12/26 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case42 when case42 == ABJutogaiEntity.GYOSEIKUMEI:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUMEI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case43 when case43 == ABJutogaiEntity.CHIKUCD1:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case44 when case44 == ABJutogaiEntity.CHIKUMEI1:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI1);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case45 when case45 == ABJutogaiEntity.CHIKUCD2:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case46 when case46 == ABJutogaiEntity.CHIKUMEI2:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI2);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case47 when case47 == ABJutogaiEntity.CHIKUCD3:
                        {
                            if (!UFStringClass.CheckANK(strValue.TrimStart()))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case48 when case48 == ABJutogaiEntity.CHIKUMEI3:
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI3);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case49 when case49 == ABJutogaiEntity.TOROKUIDOYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case50 when case50 == ABJutogaiEntity.TOROKUJIYUCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case51 when case51 == ABJutogaiEntity.SHOJOIDOYMD:
                        {
                            if (!(string.IsNullOrEmpty(strValue) | strValue == "00000000"))
                            {
                                m_cfDateClass.p_strDateValue = strValue;
                                if (!m_cfDateClass.CheckDate())
                                {
                                    // エラー定義を取得
                                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOIDOYMD);
                                    // 例外を生成
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                                }
                            }

                            break;
                        }

                    case var case52 when case52 == ABJutogaiEntity.SHOJOJIYUCD:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOJIYUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case53 when case53 == ABJutogaiEntity.RESERVE:
                        {
                            break;
                        }
                    // チェックなし

                    case var case54 when case54 == ABJutogaiEntity.TANMATSUID:
                        {
                            // * 履歴番号 000006 2003/09/11 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000006 2003/09/11 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case55 when case55 == ABJutogaiEntity.SAKUJOFG:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case56 when case56 == ABJutogaiEntity.KOSHINCOUNTER:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case57 when case57 == ABJutogaiEntity.SAKUSEINICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case58 when case58 == ABJutogaiEntity.SAKUSEIUSER:
                        {
                            // * 履歴番号 000007 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000007 2003/10/09 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case59 when case59 == ABJutogaiEntity.KOSHINNICHIJI:
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

                    case var case60 when case60 == ABJutogaiEntity.KOSHINUSER:
                        {
                            // * 履歴番号 000007 2003/10/09 修正開始
                            // If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                // * 履歴番号 000007 2003/10/09 修正終了
                                // エラー定義を取得
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }

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

        #endregion

    }
}
