// ************************************************************************************************
// * 業務名　　　　   宛名管理システム
// * 
// * クラス名　　　   ABKojinSeigyoKanriBClass：宛名個人情報管理Bクラス
// * 
// * バージョン情報   Ver 1.0
// * 
// * 作成日付　　     2012/07/19
// *
// * 作成者　　　　   2906 中嶋　秀文
// * 
// * 著作権　　　　   （株）電算
// * 
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// ************************************************************************************************
using System;
using System.Text;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.reams.ab.publicmodule.library.businesscommon.ab001x;

#region 参照名前空間

namespace Densan.Reams.AB.AB000BB
{
    #endregion

    public class ABKojinSeigyoKanriBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigData;             // コンフィグデータ
        private UFRdbClass m_cfRdb;                           // ＲＤＢクラス
        private UFErrorClass m_cfError;                       // エラー処理クラス
        private ABLogXClass m_cABLogX;                        // ABログ出力Xクラス

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKojinSeigyoKanriBClass";
        #endregion


        #region メソッド

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
        // * 　　                          ByVal cfRdb As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdb as UFRdb                          : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABKojinSeigyoKanriBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData, UFRdbClass cfRdb)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigData;
            m_cfRdb = cfRdb;

            // ABログ出力クラスのインスタンス化
            m_cABLogX = new ABLogXClass(m_cfControlData, m_cfConfigData, THIS_CLASS_NAME);

        }
        #endregion

        #region 宛名個人制御管理マスタデータ取得
        // ************************************************************************************************
        // * メソッド名     宛名個人制御管理マスタデータ取得
        // * 
        // * 構文           Public Function GetABKojinSeigyoKanri(ByVal strGyomuCD As String, ByVal strGroupID As String, ByVal enKinoBunrui As ABKinoBunruiType) As DataSet
        // * 
        // * 機能　　    　 宛名個人制御管理マスタから引数条件でデータを取得する
        // * 
        // * 引数           strGyomuCD：業務コード strGruopID：グループID enKinoBunrui：機能分類
        // * 
        // * 戻り値         取得結果：DataSet
        // ************************************************************************************************
        public DataSet GetABKojinSeigyoKanri(string strGyomuCD, string[] a_strGroupID, ABKinoBunruiType enKinoBunrui)
        {
            const string THIS_METHOD_NAME = "GetABKojinSeigyo";           // メソッド名
            var csSQL = new StringBuilder();
            UFParameterCollectionClass cfParameterCollection;         // パラメータクラス
            DataSet csReturn;
            try
            {
                // デバッグログ出力
                m_cABLogX.DebugStartWrite(THIS_METHOD_NAME);

                cfParameterCollection = new UFParameterCollectionClass();

                csSQL.AppendFormat("SELECT * FROM {0}", ABKojinSeigyoKanriMstEntity.TABLE_NAME);
                csSQL.Append(" WHERE");
                if (strGyomuCD.Trim().RLength > 0)
                {
                    csSQL.AppendFormat(" {0} = {1} AND", ABKojinSeigyoKanriMstEntity.GYOMUCD, ABKojinSeigyoKanriMstEntity.KEY_GYOMUCD);
                    cfParameterCollection.Add(ABKojinSeigyoKanriMstEntity.KEY_GYOMUCD, strGyomuCD);
                }
                else
                {
                    // そのまま
                }
                if (a_strGroupID is not null && a_strGroupID.Length > 0)
                {
                    csSQL.AppendFormat(" {0} IN (", ABKojinSeigyoKanriMstEntity.GROUPID);
                    // 引数のグループID分作成
                    for (int intIdx = 0, loopTo = a_strGroupID.Length - 1; intIdx <= loopTo; intIdx++)
                    {
                        csSQL.AppendFormat(" {0}_{1},", ABKojinSeigyoKanriMstEntity.KEY_GROUPID, intIdx.ToString());
                        cfParameterCollection.Add(string.Format("{0}_{1}", ABKojinSeigyoKanriMstEntity.KEY_GROUPID, intIdx.ToString()), a_strGroupID[intIdx]);
                    }
                    // 最後のカンマを取る
                    csSQL.RRemove(csSQL.RLength - 1, 1);
                    csSQL.Append(" ) AND");
                }
                else
                {
                    // そのまま
                }
                // 機能分類は指定なしには出来ないので必ず付ける
                csSQL.AppendFormat(" {0} = {1} AND", ABKojinSeigyoKanriMstEntity.KINOBUNRUI, ABKojinSeigyoKanriMstEntity.KEY_KINOBUNRUI);
                cfParameterCollection.Add(ABKojinSeigyoKanriMstEntity.KEY_KINOBUNRUI, Convert.ToInt32(enKinoBunrui).ToString);

                csSQL.AppendFormat(" {0} <> '1'", ABKojinSeigyoKanriMstEntity.SAKUJOFG);

                csSQL.AppendFormat(" ORDER BY {0},{1},{2}", ABKojinSeigyoKanriMstEntity.GYOMUCD, ABKojinSeigyoKanriMstEntity.GROUPID, ABKojinSeigyoKanriMstEntity.KINOBUNRUI);



                // RDBアクセスログ出力
                m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod().Name, m_cfRdb.GetDevelopmentSQLString(csSQL.ToString(), cfParameterCollection));

                csReturn = m_cfRdb.GetDataSet(csSQL.ToString(), cfParameterCollection);

                // デバッグログ出力
                m_cABLogX.DebugEndWrite(THIS_METHOD_NAME);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message);
                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException exAppException)
            {
                // ワーニングログ出力
                m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message);
                // ワーニングをスローする
                throw exAppException;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message);
                // システムエラーをスローする
                throw exException;

            }

            return csReturn;

        }
        #endregion
        #endregion

    }
}
