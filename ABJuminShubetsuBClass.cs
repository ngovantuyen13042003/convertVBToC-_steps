// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        住民種別(ABJuminShubetsuBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2002/12/13　山崎　敏生
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2011/03/31   000001     住民種別取得２メソッド(GetJuminshubetsu2)の追加（比嘉）
// ************************************************************************************************
using System;

namespace Densan.Reams.AB.AB000BB
{

    public class ABJuminShubetsuBClass
    {

        // メンバ変数の定義
        private UFLogClass m_cfUFLogClass;            // ログ出力クラス
        private UFControlData m_cfUFControlData;      // コントロールデータ

        // パラメータのメンバ変数
        private string m_strHenshuShubetsu;           // 種別（全角　Max８文字）
        private string m_strHenshuShubetsuRyaku;      // 略称（全角　Max３文字）

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABJuminShubetsuBClass";

        // 各メンバ変数のプロパティ定義
        public string p_strHenshuShubetsu
        {
            get
            {
                return m_strHenshuShubetsu;
            }
        }
        public string p_strHenshuShubetsuRyaku
        {
            get
            {
                return m_strHenshuShubetsuRyaku;
            }
        }

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文            Public Sub New(ByVal cfUFControlData As UFControlData, 
        // *                               ByVal cfUFConfigData As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
        // *                 cfUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABJuminShubetsuBClass(UFControlData cfControlData, UFConfigDataClass cfConfigData)
        {

            // メンバ変数セット
            m_cfUFControlData = cfControlData;

            // ログ出力クラスのインスタンス化
            m_cfUFLogClass = new UFLogClass(cfConfigData, cfControlData.m_strBusinessId);

            // パラメータのメンバ変数
            m_strHenshuShubetsu = string.Empty;
            m_strHenshuShubetsuRyaku = string.Empty;
        }

        // ************************************************************************************************
        // * メソッド名      住民種別取得
        // * 
        // * 構文            Public Sub GetJuminshubetsu(ByVal strAtenaDataKB As String,
        // *                                             ByVal strAtenaDataSHU As String)
        // * 
        // * 機能　　        宛名データ区分、宛名データ種別より名称を編集する
        // * 
        // * 引数            strAtenaDataKB As String   :宛名データ区分
        // *                 strAtenaDataSHU As String  :宛名データ種別
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetJuminshubetsu(string strAtenaDataKB, string strAtenaDataSHU)
        {
            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");

                switch (strAtenaDataKB ?? "")
                {
                    case "20":
                        {
                            m_strHenshuShubetsu = "法　人";
                            m_strHenshuShubetsuRyaku = "法　人";
                            break;
                        }
                    case "30":
                        {
                            m_strHenshuShubetsu = "共　有";
                            m_strHenshuShubetsuRyaku = "共　有";
                            break;
                        }

                    default:
                        {
                            switch (strAtenaDataSHU ?? "")
                            {
                                case "10":
                                    {
                                        m_strHenshuShubetsu = "日本人・住民";
                                        m_strHenshuShubetsuRyaku = "";
                                        break;
                                    }
                                case "13":
                                    {
                                        m_strHenshuShubetsu = "日本人（住登外）";
                                        m_strHenshuShubetsuRyaku = "住登外";
                                        break;
                                    }
                                case "14":
                                    {
                                        m_strHenshuShubetsu = "その他個人";
                                        m_strHenshuShubetsuRyaku = "その他";
                                        break;
                                    }
                                case "17":
                                    {
                                        m_strHenshuShubetsu = "日本人・消除者";
                                        m_strHenshuShubetsuRyaku = "消　除";
                                        break;
                                    }
                                case "18":
                                    {
                                        m_strHenshuShubetsu = "日本人・転出者";
                                        m_strHenshuShubetsuRyaku = "転　出";
                                        break;
                                    }
                                case "19":
                                    {
                                        m_strHenshuShubetsu = "日本人・死亡者";
                                        m_strHenshuShubetsuRyaku = "死　亡";
                                        break;
                                    }
                                case "20":
                                    {
                                        m_strHenshuShubetsu = "外国人：住民";
                                        m_strHenshuShubetsuRyaku = "外国人";
                                        break;
                                    }
                                case "23":
                                    {
                                        m_strHenshuShubetsu = "外国人（住登外）";
                                        m_strHenshuShubetsuRyaku = "住登外";
                                        break;
                                    }
                                case "27":
                                    {
                                        m_strHenshuShubetsu = "外国人：消除者";
                                        m_strHenshuShubetsuRyaku = "消　除";
                                        break;
                                    }
                                case "28":
                                    {
                                        m_strHenshuShubetsu = "外国人：転出者";
                                        m_strHenshuShubetsuRyaku = "転　出";
                                        break;
                                    }
                                case "29":
                                    {
                                        m_strHenshuShubetsu = "外国人：死亡者";
                                        m_strHenshuShubetsuRyaku = "死　亡";
                                        break;
                                    }

                                default:
                                    {
                                        m_strHenshuShubetsu = "＊＊＊＊＊＊＊＊";
                                        m_strHenshuShubetsuRyaku = "＊＊＊";
                                        break;
                                    }
                            }

                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetJuminshubetsu】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }
        }

        // *履歴番号 000001 2011/03/31 追加開始
        // ************************************************************************************************
        // * メソッド名      住民種別取得２
        // * 
        // * 構文            Public Sub GetJuminshubetsu2(ByVal strAtenaDataKB As String,
        // *                                              ByVal strAtenaDataSHU As String)
        // * 
        // * 機能　　        宛名データ区分、宛名データ種別より名称を編集する
        // *                 ※GetJuminshubetsuメソッドと外国人の表示方法が異なる
        // * 
        // * 引数            strAtenaDataKB As String   :宛名データ区分
        // *                 strAtenaDataSHU As String  :宛名データ種別
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public void GetJuminshubetsu2(string strAtenaDataKB, string strAtenaDataSHU)
        {
            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");

                switch (strAtenaDataKB ?? "")
                {
                    case "20":
                        {
                            m_strHenshuShubetsu = "法　人";
                            m_strHenshuShubetsuRyaku = "法　人";
                            break;
                        }
                    case "30":
                        {
                            m_strHenshuShubetsu = "共　有";
                            m_strHenshuShubetsuRyaku = "共　有";
                            break;
                        }

                    default:
                        {
                            switch (strAtenaDataSHU ?? "")
                            {
                                case "10":
                                    {
                                        m_strHenshuShubetsu = "住民";
                                        m_strHenshuShubetsuRyaku = "";
                                        break;
                                    }
                                case "13":
                                    {
                                        m_strHenshuShubetsu = "住登外";
                                        m_strHenshuShubetsuRyaku = "住登外";
                                        break;
                                    }
                                case "14":
                                    {
                                        m_strHenshuShubetsu = "その他個人";
                                        m_strHenshuShubetsuRyaku = "その他";
                                        break;
                                    }
                                case "17":
                                    {
                                        m_strHenshuShubetsu = "職権消除者";
                                        m_strHenshuShubetsuRyaku = "消　除";
                                        break;
                                    }
                                case "18":
                                    {
                                        m_strHenshuShubetsu = "転出者";
                                        m_strHenshuShubetsuRyaku = "転　出";
                                        break;
                                    }
                                case "19":
                                    {
                                        m_strHenshuShubetsu = "死亡者";
                                        m_strHenshuShubetsuRyaku = "死　亡";
                                        break;
                                    }
                                case "20":
                                    {
                                        m_strHenshuShubetsu = "外国人住民";
                                        m_strHenshuShubetsuRyaku = "外国人";
                                        break;
                                    }
                                case "23":
                                    {
                                        m_strHenshuShubetsu = "外国人住登外";
                                        m_strHenshuShubetsuRyaku = "住登外(外国人)";
                                        break;
                                    }
                                case "27":
                                    {
                                        m_strHenshuShubetsu = "外国人職権消除者";
                                        m_strHenshuShubetsuRyaku = "消除(外国人)";
                                        break;
                                    }
                                case "28":
                                    {
                                        m_strHenshuShubetsu = "外国人転出者";
                                        m_strHenshuShubetsuRyaku = "転出(外国人)";
                                        break;
                                    }
                                case "29":
                                    {
                                        m_strHenshuShubetsu = "外国人死亡者";
                                        m_strHenshuShubetsuRyaku = "死亡(外国人)";
                                        break;
                                    }

                                default:
                                    {
                                        m_strHenshuShubetsu = "＊＊＊＊＊＊＊＊";
                                        m_strHenshuShubetsuRyaku = "＊＊＊";
                                        break;
                                    }
                            }

                            break;
                        }
                }

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetJuminshubetsu】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }
        }
        // *履歴番号 000001 2011/03/31 追加終了

        // ************************************************************************************************
        // * メソッド名      住民種別編集
        // * 
        // * 構文            Public Function GetJuminshubetsu() As DataSet
        // * 
        // * 機能　　        宛名データ種別のコードと名称を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          種別データ（DataSet）
        // *                   構造：csShubetsuData    インテリセンス：ABShubetsuData
        // ************************************************************************************************
        public DataSet GetJuminshubetsu()
        {
            var csShubetsuData = new DataSet();
            DataTable csShubetsuDataTbl;
            DataRow csShubetsuDataRow;

            try
            {
                // デバッグ開始ログ出力
                m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");

                // テーブルを作成する
                csShubetsuDataTbl = csShubetsuData.Tables.Add(ABShubetsuData.TABLE_NAME);

                // テーブル配下に必要フィールドを用意する
                csShubetsuDataTbl.Columns.Add(ABShubetsuData.ATENADATASHU, Type.GetType("System.String"));
                csShubetsuDataTbl.Columns.Add(ABShubetsuData.HENSHUSHUBETSU, Type.GetType("System.String"));
                csShubetsuDataTbl.Columns.Add(ABShubetsuData.HENSHUSHUBETSURYAKU, Type.GetType("System.String"));

                // 各フィールドにデータを格納する
                // 宛名データ種別 = 10
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "10";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "日本人・住民";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 13
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "13";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "日本人（住登外）";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "住登外";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 14
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "14";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "その他個人";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "その他";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 17
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "17";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "日本人・消除者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "消　除";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 18
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "18";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "日本人・転出者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "転　出";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 19
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "19";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "日本人・死亡者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "死　亡";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 20
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "20";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "外国人：住民";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "外国人";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 23
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "23";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "外国人（住登外）";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "住登外";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 27
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "27";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "外国人：消除者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "消　除";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 28
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "28";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "外国人：転出者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "転　出";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // 宛名データ種別 = 29
                csShubetsuDataRow = csShubetsuDataTbl.NewRow();
                csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "29";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "外国人：死亡者";
                csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "死　亡";
                // データの追加
                csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow);

                // デバッグ終了ログ出力
                m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu");
            }
            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:GetJuminshubetsu】【エラー内容:" + objExp.Message + "】");
                // エラーをそのままスローする
                throw objExp;
            }

            return csShubetsuData;
        }

    }
}