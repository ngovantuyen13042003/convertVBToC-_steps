// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢ更新系バッチ排他クラス(ABBatchHourClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2015/07/02　石合　亮
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴     履歴番号    修正内容
// * yyyy/MM/dd   000000      ＮＮＮＮＮ
// ************************************************************************************************


namespace Densan.Reams.AB.AB000BB
{

    /// <summary>
/// ＡＢ更新系バッチ排他クラス
/// </summary>
/// <remarks></remarks>
    public class ABBatchHourClass : USBBatchHourClass
    {

        /// <summary>
    /// 共通クラスの戻り値定義
    /// </summary>
    /// <remarks></remarks>
        public class AB_RESULT
        {
            public const int NIGHT = 1;         // 夜間バッチエラー
            public const int UPDATE = 2;        // 更新系バッチエラー
        }

        /// <summary>
    /// 排他キー定義
    /// </summary>
    /// <remarks></remarks>
        public class AB_HAITAKEY
        {
            public const string AB = "AB";
        }

        /// <summary>
    /// 更新系バッチ排他チェック
    /// </summary>
    /// <param name="strKey">排他キー</param>
    /// <remarks></remarks>
        public void CheckBatchHourForAB(string strKey)
        {
            CheckBatchHourForAB(new string[] { strKey });
        }

        /// <summary>
    /// 更新系バッチ排他チェック
    /// </summary>
    /// <param name="a_strKey">排他キー配列</param>
    /// <remarks></remarks>
        public void CheckBatchHourForAB(string[] a_strKey)
        {
            int intResult;
            foreach (string strKey in a_strKey)
            {
                intResult = base.ChkBatchHour(ABConstClass.THIS_BUSINESSID, strKey);
                switch (intResult)
                {
                    case AB_RESULT.NIGHT:
                    case AB_RESULT.UPDATE:
                        {
                            throw new UFAppException(this.p_strErrMsg, string.Empty);
                        }

                    default:
                        {
                            break;
                        }
                        // noop
                }
            }
        }

    }
}