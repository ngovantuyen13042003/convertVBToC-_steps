// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        管内管外(ABKannaiKangaiKBBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/17　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************
using System;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABKannaiKangaiKBBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;                    // ログ出力クラス
        private UFControlData m_cfUFControlData;              // コントロールデータ
        private UFConfigDataClass m_cfUFConfigDataClass;      // コンフィグデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABJuminShubetsuBClass";

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfControlData AS UFControlData,
        // *         　　　　               ByVal cfConfigData  AS UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABKannaiKangaiKBBClass(UFControlData cfControlData, UFConfigDataClass cfUFConfigDataClass)
        {

            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            m_cfUFConfigDataClass = cfUFConfigDataClass;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfUFConfigDataClass, cfControlData.m_strBusinessId);
        }

        // ************************************************************************************************
        // * メソッド名      管内管外取得
        // * 
        // * 構文            Public Function GetKannaiKangai(strKannaiKangaiKB As String) As String
        // * 
        // * 機能　　        区分より管内管外名称を取得
        // * 
        // * 引数            strKannaiKangaiKB As String   :管内管外区分
        // * 
        // * 戻り値          管内管外名称
        // ************************************************************************************************
        public string GetKannaiKangai(string strKannaiKangaiKB)
        {
            string strMeisho = string.Empty;
            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai");

                switch (strKannaiKangaiKB ?? "")
                {
                    case "1":
                        {
                            strMeisho = "管内";
                            break;
                        }
                    case "2":
                        {
                            strMeisho = "管外";
                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai");
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKannaiKangai】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strMeisho;
        }

        // ************************************************************************************************
        // * メソッド名      管内管外編集
        // * 
        // * 構文            Public Function HenKannaiKangai() As DataSet
        // * 
        // * 機能　　        管内管外のコードと名称を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          管内管外名称（DataSet）
        // *                   構造：csKannaiKangaiData    インテリセンス：ABKannaiKangaiData
        // ************************************************************************************************
        public DataSet HenKannaiKangai()
        {
            var csKannaiKangaiData = new DataSet();
            DataTable csKannaiKangaiDataTbl;
            DataRow csKannaiKangaiDataRow;

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai");

                // テーブルを作成する
                csKannaiKangaiDataTbl = csKannaiKangaiData.Tables.Add(ABKannaiKangaiData.TABLE_NAME);

                // テーブル配下に必要フィールドを用意する
                csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKB, Type.GetType("System.String"));
                csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKBMEI, Type.GetType("System.String"));

                // 各フィールドにデータを格納する
                // 管内管外区分 = 1
                csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow();
                csKannaiKangaiDataRow[ABKannaiKangaiData.KANNAIKANGAIKB] = "1";
                csKannaiKangaiDataRow[ABKannaiKangaiData.KANNAIKANGAIKBMEI] = "管内";
                // データの追加
                csKannaiKangaiData.Tables[ABKannaiKangaiData.TABLE_NAME].Rows.Add(csKannaiKangaiDataRow);

                // 管内管外区分 = 2
                csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow();
                csKannaiKangaiDataRow[ABKannaiKangaiData.KANNAIKANGAIKB] = "2";
                csKannaiKangaiDataRow[ABKannaiKangaiData.KANNAIKANGAIKBMEI] = "管外";
                // データの追加
                csKannaiKangaiData.Tables[ABKannaiKangaiData.TABLE_NAME].Rows.Add(csKannaiKangaiDataRow);

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenKannaiKangai】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return csKannaiKangaiData;
        }

    }
}
