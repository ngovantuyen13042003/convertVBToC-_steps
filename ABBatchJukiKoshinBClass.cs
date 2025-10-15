// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ＡＢ宛名住基バッチ更新(ABBatchJukiKoshinBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け           2009/05/12　
// *
// * 作成者           比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 
// ************************************************************************************************

namespace Densan.Reams.AB.AB000BB
{

    public class ABBatchJukiKoshinBClass : ABJukiKoshinBClass           // 住基更新Ｂクラスを継承
    {

        // ************************************************************************************************
        // * メソッド名     コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass,
        // *                               ByVal cfRdbClass As UFRdbClass)
        // * 
        // * 機能　　       初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public ABBatchJukiKoshinBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass) : base(cfControlData, cfConfigDataClass, cfRdbClass)

        {
            m_blnBatch = true;

        }
    }
}