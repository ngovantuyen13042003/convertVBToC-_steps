// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        法人個人(ABKjnhjnKBBClass)
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

namespace Densan.Reams.AB.AB000BB
{

    public class ABKjnhjnKBBClass
    {
        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFControlData m_cfUFControlData;      // コントロールデータ

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABKjnhjnKBBClass";

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
        public ABKjnhjnKBBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {
            // メンバ変数セット
            m_cfUFControlData = cfControlData;
            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);
        }

        // ************************************************************************************************
        // * メソッド名      個人法人取得
        // * 
        // * 構文            Public Function GetKjnhjn(strKjnhjnKB As String) As String
        // * 
        // * 機能　　        区分より管内管外名称を取得
        // * 
        // * 引数            strKjnhjnKB As String   :個人法人区分
        // * 
        // * 戻り値          個人法人名称
        // ************************************************************************************************
        public string GetKjnhjn(string strKjnhjnKB)
        {
            string strMeisho = string.Empty;
            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn");

                switch (strKjnhjnKB ?? "")
                {
                    case "1":
                        {
                            strMeisho = "個人";
                            break;
                        }
                    case "2":
                        {
                            strMeisho = "法人";
                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetKjnhjn】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return strMeisho;
        }

        // ************************************************************************************************
        // * メソッド名      個人法人編集
        // * 
        // * 構文            Public Function HenKangaiKangai() As DataSet
        // * 
        // * 機能　　        個人法人のコードと名称を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          個人法人名称（DataSet）
        // *                   構造：csKjnHjnData    インテリセンス：ABKjnHjnData
        // ************************************************************************************************
        public DataSet HenKangaiKangai()
        {
            var csKjnHjnData = new DataSet();
            DataTable csKjnHjnDataTbl;
            DataRow csKjnHjnDataRow;

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai");

                // テーブルを作成する
                csKjnHjnDataTbl = csKjnHjnData.Tables.Add(ABKjnHjnData.TABLE_NAME);

                // テーブル配下に必要フィールドを用意する
                csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKB, Type.GetType("System.String"));
                csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKBMEI, Type.GetType("System.String"));

                // 各フィールドにデータを格納する
                // 個人法人区分 = 1
                csKjnHjnDataRow = csKjnHjnDataTbl.NewRow();
                csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "1";
                csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "個人";
                // データの追加
                csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow);

                // 個人法人区分 = 2
                csKjnHjnDataRow = csKjnHjnDataTbl.NewRow();
                csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "2";
                csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "法人";
                // データの追加
                csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow);

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:HenKangaiKangai】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return csKjnHjnData;
        }

    }
}
