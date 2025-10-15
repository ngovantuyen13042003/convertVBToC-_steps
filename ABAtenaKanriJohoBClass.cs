// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        宛名管理情報ＤＡ(ABAtenaKanriJohoBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/01/14　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2003/03/17 000001     追加時、共通項目を設定する
// * 2003/04/14 000002     種別をキーに取得するメソッドを追加
// * 2003/05/21 000003     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
// * 2003/08/28 000004     RDBアクセスログの修正
// * 2005/01/17 000005     宛名管理情報の識別キーのデータ整合性チェックを修正(数字→英数字)
// * 2007/07/27 000006     同一人代表者取得メソッド追加(吉澤)
// * 2007/10/03 000007     更新時に「備考」は何もチェックしないように変更(吉澤)
// * 2008/02/13 000008     氏名括弧編集制御取得メソッド追加（比嘉）
// * 2010/04/16 000009     VS2008対応（比嘉）
// * 2010/05/12 000010     本籍筆頭者取得区分取得メソッド、外字フラグ取得区分取得メソッド追加（比嘉）
// * 2011/05/18 000011     本名・通称名優先設定制御パラメータ取得メソッドを追加（比嘉）
// * 2014/12/18 000012     【AB21040】番号制度　宛名取得　直近検索区分パラメーター取得メソッドを追加（石合）
// * 2015/01/05 000013     【AB21034】番号制度　法人番号利用開始日パラメーター取得メソッドを追加（石合）
// * 2015/03/05 000014     【AB21034】番号制度　法人番号利用開始日のエラーメッセージを変更（石合）
// * 2018/05/07 000015     【AB27002】備考管理（石合）
// * 2018/05/22 000016     【AB24011】連絡先管理項目追加（石合）
// * 2020/08/03 000017     【AB32008】代納・送付先備考管理（石合）
// * 2020/08/21 000018     【AB32006】代納・送付先メンテナンス（石合）
// * 2020/11/10 000019     【AB00189】利用届出複数納税者ID対応（須江）
// * 2023/12/22 000020     【AB-0970-1_2】宛名GET日付項目設定対応(下村)
// ************************************************************************************************
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABAtenaKanriJohoBClass
    {
        #region メンバ変数
        // メンバ変数の定義
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private string m_strInsertSQL;                                            // INSERT用SQL
        private string m_strUpdateSQL;                                            // UPDATE用SQL
        private string m_strDeleteSQL;                                            // DELETE用SQL
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;  // DELETE用パラメータコレクション

        // *履歴番号 000006 2007/07/27 追加開始
        private string[] m_strDoitsuHantei_Param = new[] { "10", "07" };             // 同一人代表者の取得判定
                                                                                     // *履歴番号 000006 2007/07/27 追加終了
                                                                                     // *履歴番号 000008 2008/02/13 追加開始
        private string[] m_strShimeiKakkoKB_Param = new[] { "10", "15" };            // 氏名括弧編集制御
                                                                                     // *履歴番号 000008 2008/02/13 追加終了
                                                                                     // *履歴番号 000010 2010/05/12 追加開始
        private string[] m_strHonsekiKB_Param = new[] { "10", "18" };                // 本籍取得区分
        private string[] m_strShoriTeishiKB_Param = new[] { "10", "19" };            // 処理停止区分取得区分
                                                                                     // *履歴番号 000010 2010/05/12 追加終了
                                                                                     // *履歴番号 000011 2011/05/18 追加開始
        private string[] m_strHonmyoTsushomeiYusenKB_Param = new[] { "10", "23" };   // 本名通称名優先区分取得区分
                                                                                     // *履歴番号 000011 2011/05/18 追加終了
                                                                                     // *履歴番号 000019 2020/11/10 追加開始
        private string[] m_strHenkyakuFuyoGyomuCD_Param = new[] { "10", "46" };      // 独自処理　利用届出共通納税返却不要業務
                                                                                     // *履歴番号 000019 2020/11/10 追加終了
                                                                                     // *履歴番号 000012 2014/12/18 追加開始
        private string[] m_strMyNumberChokkinSearchKB_Param = new string[] { "35", "29" };   // 番号制度　宛名取得　直近検索区分
                                                                                             // *履歴番号 000012 2014/12/18 追加終了
                                                                                             // *履歴番号 000013 2015/01/05 追加開始
        private string[] m_strHojinBangoRiyoKaishiYMD_Param = new string[] { "35", "30" };   // 番号制度　法人番号利用開始日
                                                                                             // *履歴番号 000013 2015/01/05 追加終了
                                                                                             // *履歴番号 000015 2018/05/07 追加開始
        private string[] m_strJutogaiBikoUmu_Param = new string[] { "40", "07" };            // 次期Ｒｅａｍｓ　住登外備考有無
                                                                                             // *履歴番号 000015 2018/05/07 追加終了
                                                                                             // *履歴番号 000016 2018/05/22 追加開始
        private string[] m_strRenrakusakiKakuchoUmu_Param = new string[] { "40", "08" };     // 次期Ｒｅａｍｓ　連絡先拡張有無
                                                                                             // *履歴番号 000016 2018/05/22 追加終了
                                                                                             // *履歴番号 000017 2020/08/03 追加開始
        private string[] m_strDainoSfskBikoUmu_Param = new string[] { "40", "15" };          // 代納・送付先備考有無
                                                                                             // *履歴番号 000017 2020/08/03 追加終了
                                                                                             // *履歴番号 000018 2020/08/21 追加開始
        private string[] m_strZeimokuCDConvertTable_Param = new string[] { "10", "40" };     // 税目コード変換テーブル
        private string[] m_strDainoSfskMainteShiyoUmu_Param = new string[] { "12", "25" };   // 代納・送付先メンテナンス使用有無
                                                                                             // *履歴番号 000018 2020/08/21 追加終了
        private string[] m_strUmareYMDHenkan_Param = new string[] { "51", "01" };            // 標準準拠対応宛名GET 歴上日変換日付（生年月日）
        private string[] m_strShojoIdobiHenkan_Param = new string[] { "51", "02" };          // 標準準拠対応宛名GET 歴上日変換日付（消除異動日）
        private string[] m_strCknIdobiHenkan_Param = new string[] { "51", "03" };            // 標準準拠対応宛名GET 歴上日変換日付（直近異動日）

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABAtenaKanriJohoBClass";
        private const string THIS_BUSINESSID = "AB";                              // 業務コード
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
        public ABAtenaKanriJohoBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // メンバ変数の初期化
            m_strInsertSQL = string.Empty;
            m_strUpdateSQL = string.Empty;
            m_strDeleteSQL = string.Empty;
            m_cfInsertUFParameterCollectionClass = null;
            m_cfUpdateUFParameterCollectionClass = null;
            m_cfDeleteUFParameterCollectionClass = null;
        }
        #endregion

        #region メソッド
        // ************************************************************************************************
        // * メソッド名     宛名管理情報抽出
        // * 
        // * 構文           Public Overloads Function GetKanriJohoHoshu() As DataSet
        // * 
        // * 機能　　    　　宛名管理情報より該当データを全件取得する。
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         取得した宛名管理情報の該当データ（DataSet）
        // *                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
        // ************************************************************************************************
        public DataSet GetKanriJohoHoshu()
        {
            const string THIS_METHOD_NAME = "GetKanriJohoHoshu";          // このメソッド名
            DataSet csAtenaKanriJohoEntity;                           // 宛名管理情報データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 管理年度
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO;
                cfUFParameterClass.Value = "0000";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = "AB";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csAtenaKanriJohoEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名管理情報抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String, 
        // *                                                            ByVal strShikibetsuKey As String) As DataSet
        // * 
        // * 機能　　    　　宛名管理情報より該当データを全件取得する。
        // * 
        // * 引数           strSHUKey As String           :種別キー
        // *                strShikibetsuKey As String    :識別キー
        // * 
        // * 戻り値         取得した宛名管理情報の該当データ（DataSet）
        // *                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
        // ************************************************************************************************
        public DataSet GetKanriJohoHoshu(string strSHUKey, string strShikibetsuKey)
        {
            const string THIS_METHOD_NAME = "GetKanriJohoHoshu(Overloads)";          // このメソッド名
            DataSet csAtenaKanriJohoEntity;                           // 宛名管理情報データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 管理年度
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO;
                cfUFParameterClass.Value = "0000";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = "AB";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 種別キー
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY;
                cfUFParameterClass.Value = strSHUKey;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 識別キー
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY;
                cfUFParameterClass.Value = strShikibetsuKey;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csAtenaKanriJohoEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名管理情報抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
        // * 
        // * 構文           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String) As DataSet
        // * 
        // * 機能　　    　　宛名管理情報より該当データを全件取得する。
        // * 
        // * 引数           strSHUKey As String           :種別キー
        // * 
        // * 戻り値         取得した宛名管理情報の該当データ（DataSet）
        // *                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
        // ************************************************************************************************
        public DataSet GetKanriJohoHoshu(string strSHUKey)
        {
            const string THIS_METHOD_NAME = "GetKanriJohoHoshu(Overloads)";          // このメソッド名
            DataSet csAtenaKanriJohoEntity;                           // 宛名管理情報データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQL文の作成
                strSQL.Append("SELECT * FROM ");
                strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME);
                // WHERE文結合
                strSQL.Append(" WHERE ");
                strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY);
                strSQL.Append(" = ");
                strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY);
                strSQL.Append(" AND ");
                strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG);
                strSQL.Append(" <> 1");

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // 検索条件のパラメータを作成
                // 管理年度
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO;
                cfUFParameterClass.Value = "0000";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD;
                cfUFParameterClass.Value = "AB";
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);
                // 種別キー
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY;
                cfUFParameterClass.Value = strSHUKey;
                // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass);

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:GetDataSet】" + _
                // "【SQL内容:" + strSQL.ToString() + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行 DataSetの取得
                csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return csAtenaKanriJohoEntity;

        }

        // ************************************************************************************************
        // * メソッド名     宛名管理情報追加
        // * 
        // * 構文           Public Function InsertKanriJoho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名管理情報にデータを追加する。
        // * 
        // * 引数           csDataRow As DataRow  :追加データ
        // * 
        // * 戻り値         追加件数(Integer)
        // ************************************************************************************************
        public int InsertKanriJoho(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertKanriJoho";            // このメソッド名
                                                                          // パラメータクラス
            int intInsCnt;                                        // 追加件数
            string strUpdateDateTime;

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");          // 作成日時

                // 共通項目の編集を行う
                csDataRow[ABAtenaKanriJohoEntity.TANMATSUID] = m_cfControlData.m_strClientId;            // 端末ＩＤ
                csDataRow[ABAtenaKanriJohoEntity.SAKUJOFG] = "0";                                        // 削除フラグ
                csDataRow[ABAtenaKanriJohoEntity.KOSHINCOUNTER] = decimal.Zero;                          // 更新カウンタ
                csDataRow[ABAtenaKanriJohoEntity.SAKUSEINICHIJI] = strUpdateDateTime;                    // 作成日時
                csDataRow[ABAtenaKanriJohoEntity.SAKUSEIUSER] = m_cfControlData.m_strUserId;             // 作成ユーザー
                csDataRow[ABAtenaKanriJohoEntity.KOSHINNICHIJI] = strUpdateDateTime;                     // 更新日時
                csDataRow[ABAtenaKanriJohoEntity.KOSHINUSER] = m_cfControlData.m_strUserId;              // 更新ユーザー

                // 当クラスのデータ整合性チェックを行う
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                    // データ整合性チェック
                    CheckColumnValue(csDataColumn.ColumnName, csDataRow[csDataColumn.ColumnName].ToString().Trim());

                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength())].ToString();

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strInsertSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行
                intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }

            return intInsCnt;

        }

        // ************************************************************************************************
        // * メソッド名     宛名管理情報更新
        // * 
        // * 構文           Public Function UpdateKanriJoho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名管理情報のデータを更新する。
        // * 
        // * 引数           csDataRow As DataRow  :更新データ
        // * 
        // * 戻り値         更新件数(Integer)
        // ************************************************************************************************
        public int UpdateKanriJoho(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateKanriJoho";         // このメソッド名
                                                                       // パラメータクラス
                                                                       // * corresponds to VS2008 Start 2010/04/16 000009
                                                                       // Dim csDataColumn As DataColumn
                                                                       // * corresponds to VS2008 End 2010/04/16 000009
            int intUpdCnt;                            // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpdateSQL is null | string.IsNullOrEmpty(m_strUpdateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 共通項目の編集を行う
                csDataRow[ABAtenaKanriJohoEntity.TANMATSUID] = m_cfControlData.m_strClientId;                                    // 端末ＩＤ
                csDataRow[ABAtenaKanriJohoEntity.KOSHINCOUNTER] = (decimal)csDataRow[ABAtenaKanriJohoEntity.KOSHINCOUNTER] + 1m;     // 更新カウンタ
                csDataRow[ABAtenaKanriJohoEntity.KOSHINNICHIJI] = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff");    // 更新日時
                csDataRow[ABAtenaKanriJohoEntity.KOSHINUSER] = m_cfControlData.m_strUserId;                                      // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength()) == ABAtenaKanriJohoEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // データ整合性チェック
                        CheckColumnValue(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength()), csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString().Trim());
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strUpdateSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw exException;

            }

            return intUpdCnt;

        }

        // ************************************************************************************************
        // * メソッド名     宛名管理情報削除（物理）
        // * 
        // * 構文           Public Overloads Function DeleteKanriJoho(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　    　  宛名管理情報のデータを削除（物理）する。
        // * 
        // * 引数           csDataRow As DataRow      :削除データ
        // * 
        // * 戻り値         削除（物理）件数(Integer)
        // ************************************************************************************************
        public int DeleteKanriJoho(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteKanriJoho（物理）";                                 // パラメータクラス
                                                                                                   // * corresponds to VS2008 Start 2010/04/16 000009
                                                                                                   // Dim csDataColumn As DataColumn
                                                                                                   // Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
                                                                                                   // * corresponds to VS2008 End 2010/04/16 000009
            int intDelCnt;                                        // 削除件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strDeleteSQL is null | string.IsNullOrEmpty(m_strDeleteSQL) | m_cfDeleteUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength()) == ABAtenaKanriJohoEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength()), DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfDeleteUFParameterCollectionClass[cfParam.ParameterName].Value = csDataRow[cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength()), DataRowVersion.Current].ToString();
                    }
                }

                // *履歴番号 000004 2003/08/28 修正開始
                // ' RDBアクセスログ出力
                // m_cfLogClass.RdbWrite(m_cfControlData, _
                // "【クラス名:" + THIS_CLASS_NAME + "】" + _
                // "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                // "【実行メソッド名:ExecuteSQL】" + _
                // "【SQL内容:" + m_strDeleteSQL + "】")

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");



                // *履歴番号 000004 2003/08/28 修正終了

                // SQLの実行
                intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");



                // システムエラーをスローする
                throw exException;

            }

            return intDelCnt;

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
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";              // このメソッド名
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            string strInsertColumn;                               // 追加SQL文項目文字列
            string strInsertParam;                                // 追加SQL文パラメータ文字列
            var strDeleteSQL = new StringBuilder();                       // 削除SQL文文字列
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SELECT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABAtenaKanriJohoEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // 更新削除Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABAtenaKanriJohoEntity.KANRINENDO);
                strWhere.Append(" = ");
                strWhere.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO);
                strWhere.Append(" AND ");
                strWhere.Append(ABAtenaKanriJohoEntity.GYOMUCD);
                strWhere.Append(" = ");
                strWhere.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABAtenaKanriJohoEntity.SHUKEY);
                strWhere.Append(" = ");
                strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY);
                strWhere.Append(" AND ");
                strWhere.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY);
                strWhere.Append(" = ");
                strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY);
                strWhere.Append(" AND ");
                strWhere.Append(ABAtenaKanriJohoEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER);

                // UPDATE SQL文の作成
                m_strUpdateSQL = "UPDATE " + ABAtenaKanriJohoEntity.TABLE_NAME + " SET ";

                // DELETE（物理） SQL文の作成
                strDeleteSQL.Append("DELETE FROM ");
                strDeleteSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME);
                strDeleteSQL.Append(strWhere.ToString());
                m_strDeleteSQL = strDeleteSQL.ToString();

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // DELETE（物理） パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // SQL文の作成
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");
                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // UPDATE SQL文のトリミング
                m_strUpdateSQL = m_strUpdateSQL.Trim();
                m_strUpdateSQL = m_strUpdateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpdateSQL += strWhere.ToString();

                // UPDATE,DELETE(物理) コレクションにキー情報を追加
                // 管理年度
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 業務コード
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 種別キー
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 識別キー
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }
        }

        // ************************************************************************************************
        // * メソッド名     データ整合性チェック
        // * 
        // * 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
        // *                                             ByVal strValue As String)
        // * 
        // * 機能　　       宛名管理情報のデータ整合性チェックを行います。
        // * 
        // * 引数           strColumnName As String
        // *                strValue As String
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CheckColumnValue(string strColumnName, string strValue)
        {
            const string THIS_METHOD_NAME = "CheckColumnValue";       // このメソッド名
            UFErrorStruct objErrorStruct;                         // エラー定義構造体

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                switch (strColumnName.ToUpper() ?? "")
                {
                    case var @case when @case == ABAtenaKanriJohoEntity.SHICHOSONCD:                 // 市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case1 when case1 == ABAtenaKanriJohoEntity.KYUSHICHOSONCD:              // 旧市町村コード
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KYUSHICHOSONCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case2 when case2 == ABAtenaKanriJohoEntity.KANRINENDO:                  // 管理年度
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KANRINENDO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case3 when case3 == ABAtenaKanriJohoEntity.GYOMUCD:                     // 業務コード
                        {
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_GYOMUCD);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case4 when case4 == ABAtenaKanriJohoEntity.SHUKEY:                      // 種別キー
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEY);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case5 when case5 == ABAtenaKanriJohoEntity.SHIKIBETSUKEY:               // 識別キー
                        {
                            // *履歴番号 000005 2006/01/17 修正開始
                            if (!UFStringClass.CheckAlphabetNumber(strValue))
                            {
                                // If (Not UFStringClass.CheckNumber(strValue)) Then
                                // *履歴番号 000005 2006/01/17 修正終了
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEY);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case6 when case6 == ABAtenaKanriJohoEntity.SHUKEYMEISHO:                // 種別キー名称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEYMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case7 when case7 == ABAtenaKanriJohoEntity.SHIKIBETSUKEYMEISHO:         // 識別キー名称
                        {
                            if (!UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEYMEISHO);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case8 when case8 == ABAtenaKanriJohoEntity.PARAMETER:                   // パラメータ
                        {
                            break;
                        }
                    // 何もしない
                    case var case9 when case9 == ABAtenaKanriJohoEntity.BIKO:                        // 備考
                        {
                            break;
                        }
                    // *履歴番号 000007 2007/10/01 削除開始
                    // If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    // m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    // 'エラー定義を取得
                    // objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_BIKO)
                    // '例外を生成
                    // Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    // End If
                    // *履歴番号 000007 2007/10/01 削除終了
                    case var case10 when case10 == ABAtenaKanriJohoEntity.RESERVE:                     // リザーブ
                        {
                            break;
                        }
                    // 何もしない
                    case var case11 when case11 == ABAtenaKanriJohoEntity.TANMATSUID:                  // 端末ID
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_TANMATSUID);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case12 when case12 == ABAtenaKanriJohoEntity.SAKUJOFG:                    // 削除フラグ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUJOFG);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case13 when case13 == ABAtenaKanriJohoEntity.KOSHINCOUNTER:               // 更新カウンタ
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINCOUNTER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case14 when case14 == ABAtenaKanriJohoEntity.SAKUSEINICHIJI:              // 作成日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEINICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case15 when case15 == ABAtenaKanriJohoEntity.SAKUSEIUSER:                 // 作成ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEIUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case16 when case16 == ABAtenaKanriJohoEntity.KOSHINNICHIJI:               // 更新日時
                        {
                            if (!UFStringClass.CheckNumber(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINNICHIJI);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                    case var case17 when case17 == ABAtenaKanriJohoEntity.KOSHINUSER:                  // 更新ユーザ
                        {
                            if (!UFStringClass.CheckANK(strValue))
                            {
                                m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                                // エラー定義を取得
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINUSER);
                                // 例外を生成
                                throw new UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode);
                            }

                            break;
                        }
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }
            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;
            }
        }

        // *履歴番号 000006 2007/07/27 追加開始
        // ************************************************************************************************
        // * メソッド名     同一人代表者取得の判定パラメータ取得
        // * 
        // * 構文           Public Function GetDoitsuHantei_Param() As DataSet
        // * 
        // * 機能           同一人代表者取得の判定パラメータを取得する
        // * 
        // * 引数           strShichosonCD As String : 市町村コード
        // * 
        // * 戻り値         String : 
        // ************************************************************************************************
        public string GetDoitsuHantei_Param()
        {
            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetDoitsuHantei_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strDoitsuHantei_Param[0], m_strDoitsuHantei_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、本人情報の取得とする
                    strRet = ABConstClass.PRM_HONNIN;
                }
                else if ((string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == ABConstClass.PRM_DAIHYO)
                {
                    // パラメータが同一人代表者取得の場合は、同一人代表者の取得とする
                    strRet = ABConstClass.PRM_DAIHYO;
                }
                else
                {
                    // 上記以外は、本人情報の取得とする
                    strRet = ABConstClass.PRM_HONNIN;
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + "【ワーニング内容:" + exAppException.Message + "】");



                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw exException;

            }
        }
        // *履歴番号 000006 2007/07/27 追加終了
        // *履歴番号 000008 2008/02/13 追加開始
        // ************************************************************************************************
        // * メソッド名     氏名括弧編集制御パラメータ取得
        // * 
        // * 構文           Public Function GetShimeiKakkoKB_Param() As DataSet
        // * 
        // * 機能           氏名括弧編集制御の判定パラメータを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String : 
        // ************************************************************************************************
        public string GetShimeiKakkoKB_Param()
        {
            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetShimeiKakkoKB_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strShimeiKakkoKB_Param[0], m_strShimeiKakkoKB_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、標準とする
                    strRet = "0";
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }
        // *履歴番号 000008 2008/02/13 追加終了
        // *履歴番号 000010 2010/05/12 追加開始
        // ************************************************************************************************
        // * メソッド名     本籍取得区分パラメータ取得
        // * 
        // * 構文           Public Function GetHonsekiKB_Param() As DataSet
        // * 
        // * 機能           本籍取得区分パラメータを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String 
        // ************************************************************************************************
        public string GetHonsekiKB_Param()
        {
            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetHonsekiKB_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strHonsekiKB_Param[0], m_strHonsekiKB_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = "0";
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }
        // ************************************************************************************************
        // * メソッド名     処理停止区分取得区分パラメータ取得
        // * 
        // * 構文           Public Function GetShoriteishiKB_Param() As DataSet
        // * 
        // * 機能           処理停止区分取得区分パラメータを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String 
        // ************************************************************************************************
        public string GetShoriteishiKB_Param()
        {
            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetShoriteishiKB_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strShoriTeishiKB_Param[0], m_strShoriTeishiKB_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = "0";
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }
        // *履歴番号 000010 2010/05/12 追加終了

        // *履歴番号 000011 2011/05/18 追加開始
        // ************************************************************************************************
        // * メソッド名     本名・通称名優先制御区分パラメータ取得
        // * 
        // * 構文           Public Function GetHonmyoTsushomeiYusenKB_Param() As String
        // * 
        // * 機能           本名・通称名優先制御区分パラメータを取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         String 
        // ************************************************************************************************
        public string GetHonmyoTsushomeiYusenKB_Param()
        {
            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetHonmyoTsushomeiYusenKB_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strHonmyoTsushomeiYusenKB_Param[0], m_strHonmyoTsushomeiYusenKB_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = "0";
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }
        // *履歴番号 000011 2011/05/18 追加終了

        // *履歴番号 000012 2014/12/18 追加開始
        #region 番号制度　宛名取得　直近検索区分　パラメーター取得

        /// <summary>
    /// 番号制度　宛名取得　直近検索区分　パラメーター取得
    /// </summary>
    /// <returns>番号制度　宛名取得　直近検索区分</returns>
    /// <remarks></remarks>
        public string GetMyNumberChokkinSearchKB_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            string strResult;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strMyNumberChokkinSearchKB_Param[0], m_strMyNumberChokkinSearchKB_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    strResult = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    switch (strResult ?? "")
                    {

                        case var @case when @case == ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode().ToString:
                        // noop
                        case var case1 when case1 == ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode().ToString:
                            {
                                break;
                            }

                        default:
                            {

                                // 規定値以外（値なしを含む）の場合は、"2"（履歴を含めて検索）を設定する。
                                strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode().ToString();
                                break;
                            }

                    }
                }

                else
                {

                    // レコードが存在しない場合は、"2"（履歴を含めて検索）を設定する。
                    strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode().ToString();

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return strResult;

        }

        #endregion
        // *履歴番号 000012 2014/12/18 追加終了

        // *履歴番号 000013 2015/01/05 追加開始
        #region 番号制度　法人番号利用開始日　パラメーター取得

        /// <summary>
    /// 番号制度　法人番号利用開始日　パラメーター取得
    /// </summary>
    /// <returns>番号制度　法人番号利用開始日</returns>
    /// <remarks></remarks>
        public string GetHojinBangoRiyoKaishiYMD_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            DataSet csDataSet;
            string strResult;
            UFDateClass cfDate;                           // 日付クラス
            UFErrorClass cfErrorClass;                    // エラークラス
            UFErrorStruct cfErrorStruct;                  // エラー定義構造体

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strHojinBangoRiyoKaishiYMD_Param[0], m_strHojinBangoRiyoKaishiYMD_Param[1]);

                // パラメーター値の取り出し
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {
                    strResult = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();
                }
                else
                {
                    strResult = string.Empty;
                }

                // 取得データのチェック
                cfDate = new UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero, UFEraType.Number, false, false);
                cfDate.p_strDateValue = strResult;
                if (cfDate.CheckDate() == true)
                {
                    strResult = cfDate.p_strSeirekiYMD;
                }
                else
                {

                    // 実在日以外の場合は、エラーとする。（業共の動きに準拠させる。）
                    // *履歴番号 000014 2015/03/05 修正開始
                    // cfErrorClass = New UFErrorClass
                    // cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001053)
                    // Throw New Exception(cfErrorStruct.m_strErrorMessage)
                    cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003144);
                    throw new Exception(string.Format("{0} 宛名管理情報 ： 種別キー【{1}】、識別キー【{2}】", cfErrorStruct.m_strErrorMessage, m_strHojinBangoRiyoKaishiYMD_Param[0], m_strHojinBangoRiyoKaishiYMD_Param[1]));


                    // *履歴番号 000014 2015/03/05 修正終了

                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return strResult;

        }

        #endregion
        // *履歴番号 000013 2015/01/05 追加終了

        // *履歴番号 000015 2018/05/07 追加開始
        #region 次期Ｒｅａｍｓ　住登外備考有無　パラメーター取得

        /// <summary>
    /// 次期Ｒｅａｍｓ　住登外備考有無　パラメーター取得
    /// </summary>
    /// <returns>次期Ｒｅａｍｓ　住登外備考有無</returns>
    /// <remarks></remarks>
        public bool GetJutogaiBikoUmu_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool blnResult = false;
            DataSet csDataSet;
            string strParameter;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strJutogaiBikoUmu_Param[0], m_strJutogaiBikoUmu_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    strParameter = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    if (strParameter.Trim() == "1")
                    {
                        blnResult = true;
                    }
                    else
                    {
                        blnResult = false;
                    }
                }

                else
                {
                    blnResult = false;
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return blnResult;

        }

        #endregion
        // *履歴番号 000015 2018/05/07 追加終了

        // *履歴番号 000016 2018/05/22 追加開始
        #region 次期Ｒｅａｍｓ　連絡先拡張有無　パラメーター取得

        /// <summary>
    /// 次期Ｒｅａｍｓ　連絡先拡張有無　パラメーター取得
    /// </summary>
    /// <returns>次期Ｒｅａｍｓ　連絡先拡張有無</returns>
    /// <remarks></remarks>
        public bool GetRenrakusakiKakuchoUmu_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool blnResult = false;
            DataSet csDataSet;
            string strParameter;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strRenrakusakiKakuchoUmu_Param[0], m_strRenrakusakiKakuchoUmu_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    strParameter = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    if (strParameter.Trim() == "1")
                    {
                        blnResult = true;
                    }
                    else
                    {
                        blnResult = false;
                    }
                }

                else
                {
                    blnResult = false;
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return blnResult;

        }

        #endregion
        // *履歴番号 000016 2018/05/22 追加終了

        // *履歴番号 000017 2020/08/03 追加開始
        #region 代納・送付先備考有無　パラメーター取得

        /// <summary>
    /// 代納・送付先備考有無　パラメーター取得
    /// </summary>
    /// <returns>代納・送付先備考有無</returns>
    /// <remarks></remarks>
        public bool GetDainoSfskBikoUmu_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool blnResult = false;
            DataSet csDataSet;
            string strParameter;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strDainoSfskBikoUmu_Param[0], m_strDainoSfskBikoUmu_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    strParameter = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    if (strParameter.Trim() == "1")
                    {
                        blnResult = true;
                    }
                    else
                    {
                        blnResult = false;
                    }
                }

                else
                {
                    blnResult = false;
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");



                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return blnResult;

        }

        #endregion
        // *履歴番号 000017 2020/08/03 追加終了

        // *履歴番号 000018 2020/08/21 追加開始
        #region 税目コード変換テーブル　パラメーター取得

        /// <summary>
    /// 税目コード変換テーブル　パラメーター取得
    /// </summary>
    /// <returns>税目コード変換テーブル</returns>
    /// <remarks></remarks>
        public Hashtable GetZeikokuCDConvertTable_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Hashtable csResult;
            DataSet csDataSet;
            string strParameter;
            string[] a_strParameter;
            string[] a_strValue;

            const char SEPARATOR_SLASH = '/';
            const char SEPARATOR_COMMA = ',';

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 返信オブジェクトのインスタンス化
                csResult = new Hashtable();

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strZeimokuCDConvertTable_Param[0], m_strZeimokuCDConvertTable_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    // パラメーターを取得
                    strParameter = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    // スラッシュで区切る
                    a_strParameter = strParameter.Split(SEPARATOR_SLASH);

                    // 業務数分ループ
                    foreach (string strValue in a_strParameter)
                    {

                        // カンマで区切る
                        a_strValue = strValue.Split(SEPARATOR_COMMA);

                        // 項目数分ループ
                        if (a_strValue.Count() > 1)
                        {

                            // 重複チェックを行いながら、ハッシュへ追加する
                            if (csResult.ContainsKey(a_strValue[0]) == true)
                            {
                            }
                            // noop
                            else
                            {
                                csResult.Add(a_strValue[0], a_strValue[1]);
                            }
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

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

            return csResult;

        }

        #endregion

        #region 代納・送付先メンテナンス使用有無　パラメーター取得

        /// <summary>
    /// 代納・送付先メンテナンス使用有無　パラメーター取得
    /// </summary>
    /// <returns>代納・送付先メンテナンス使用有無</returns>
    /// <remarks></remarks>
        public bool GetDainoSfskMainteShiyoUmu_Param()
        {

            string THIS_METHOD_NAME = System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool blnResult;
            DataSet csDataSet;
            string strParameter;

            try
            {

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 返信オブジェクトの初期化
                blnResult = false;

                // 管理情報からデータを取得
                csDataSet = GetKanriJohoHoshu(m_strDainoSfskMainteShiyoUmu_Param[0], m_strDainoSfskMainteShiyoUmu_Param[1]);

                // 取得データのチェック
                if (csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count > 0)
                {

                    // パラメーターを取得
                    strParameter = csDataSet.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER].ToString();

                    // 取得結果を判定
                    if (strParameter.Trim() == "1")
                    {
                        blnResult = true;
                    }
                    else
                    {
                        blnResult = false;
                    }
                }

                else
                {
                    // noop
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException cfAppExp)
            {

                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + cfAppExp.Message + "】");




                // ワーニングをスローする
                throw;
            }

            catch (Exception csExp)
            {

                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + csExp.Message + "】");



                // システムエラーをスローする
                throw;

            }

            return blnResult;

        }

        #endregion
        // *履歴番号 000018 2020/08/21 追加終了

        // *履歴番号 000019 2020/11/10 追加開始
        #region 独自処理　利用届出共通納税返却不要業務　パラメーター取得

        /// <summary>
    /// 独自処理　利用届出共通納税返却不要業務　パラメーター取得
    /// </summary>
    /// <returns>独自処理　利用届出共通納税返却不要業務</returns>
    /// <remarks></remarks>
        public string GetHenkyakuFuyoGyomuCD_Param()
        {

            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetHenkyakuFuyoGyomuCD_Param";


            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strHenkyakuFuyoGyomuCD_Param[0], m_strHenkyakuFuyoGyomuCD_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = "";
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }

        #endregion
        // *履歴番号 000019 2020/11/10 追加終了

        #region 標準準拠対応宛名GET　歴上日変換日付（生年月日）　パラメーター取得

        /// <summary>
    /// 標準準拠対応宛名GET　歴上日変換日付（生年月日）　パラメーター取得
    /// </summary>
    /// <returns>標準準拠対応宛名GET　歴上日変換日付（生年月日）</returns>
    /// <remarks></remarks>
        public string GetUmareYMDHenkanHizuke_Param()
        {

            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetUmareYMDHenkanHizuke_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strUmareYMDHenkan_Param[0], m_strUmareYMDHenkan_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = string.Empty;
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }

        #endregion

        #region 標準準拠対応宛名GET　歴上日変換日付（消除異動日）　パラメーター取得

        /// <summary>
    /// 標準準拠対応宛名GET　歴上日変換日付（消除異動日）　パラメーター取得
    /// </summary>
    /// <returns>標準準拠対応宛名GET　歴上日変換日付（消除異動日）</returns>
    /// <remarks></remarks>
        public string GetShojoIdobiHenkanHizuke_Param()
        {

            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetShojoIdobiHenkanHizuke_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strShojoIdobiHenkan_Param[0], m_strShojoIdobiHenkan_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = string.Empty;
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }

        #endregion

        #region 標準準拠対応宛名GET　歴上日変換日付（直近異動日）　パラメーター取得

        /// <summary>
    /// 標準準拠対応宛名GET　歴上日変換日付（直近異動日）　パラメーター取得
    /// </summary>
    /// <returns>標準準拠対応宛名GET　歴上日変換日付（直近異動日）</returns>
    /// <remarks></remarks>
        public string GetCknIdobiHenkanHizuke_Param()
        {

            DataSet csDS;
            string strRet;
            const string THIS_METHOD_NAME = "GetCknIdobiHenkanHizuke_Param";

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 管理情報からデータを取得
                csDS = GetKanriJohoHoshu(m_strCknIdobiHenkan_Param[0], m_strCknIdobiHenkan_Param[1]);

                // 取得データのチェック
                if (csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // レコードが存在しない場合は、空白とする
                    strRet = string.Empty;
                }
                else
                {
                    // レコードが存在する場合は、管理情報をセットする
                    strRet = (string)csDS.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER];
                }

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                return strRet;
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
        }

        #endregion
        #endregion

    }
}
