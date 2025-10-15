// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        ＡＢバッチ用宛名取得(ABBatchAtenaGetClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/08/21　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/08/27 000001     速度改善：（宮沢）
// * 2005/01/25 000002     速度改善２：（宮沢）
// *
// ************************************************************************************************
using System;
using System.Security;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABBatchAtenaGetBClass : ABAtenaGetBClass           // 宛名取得Ｂクラスを継承
    {

        // パラメータのメンバ変数
        protected new ABBatchAtenaHenshuBClass m_cABAtenaHenshuB;             // 宛名編集クラス(バッチ用)

        // コンスタント定義
        protected new const string THIS_CLASS_NAME = "ABBatchAtenaGetBClass"; // クラス名

        // * 履歴番号 000001 2004/08/27 追加開始（宮沢）
        private URAtenaKanriJohoBClass m_cfURAtenaKanriJoho;    // 宛名管理情報Ｂクラス
                                                                // * 履歴番号 000001 2004/08/27 追加終了


        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABBatchAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass) : base(cfControlData, cfConfigDataClass)
        {
            m_blnBatch = true;
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
            if (m_cABAtenaBRef is not null)
            {
                m_cABAtenaBRef.m_blnBatch = true;
            }
            if (m_cABAtenaRirekiBRef is not null)
            {
                m_cABAtenaRirekiBRef.m_blnBatch = true;
            }
            if (m_cABDainoBRef is not null)
            {
                m_cABDainoBRef.m_blnBatch = true;
            }
            if (m_cABSfskBRef is not null)
            {
                m_cABSfskBRef.m_blnBatch = true;
            }
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        }
        // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 　　                          ByVal blnSelectAll as boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABBatchAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, bool blnSelectAll) : base(cfControlData, cfConfigDataClass, blnSelectAll)
        {
            m_blnBatch = true;
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
            if (m_cABAtenaBRef is not null)
            {
                m_cABAtenaBRef.m_blnBatch = true;
            }
            if (m_cABAtenaRirekiBRef is not null)
            {
                m_cABAtenaRirekiBRef.m_blnBatch = true;
            }
            if (m_cABDainoBRef is not null)
            {
                m_cABDainoBRef.m_blnBatch = true;
            }
            if (m_cABSfskBRef is not null)
            {
                m_cABSfskBRef.m_blnBatch = true;
            }
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        }
        // * 履歴番号 000002 2005/01/25 追加終了（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABBatchAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass) : base(cfControlData, cfConfigDataClass, cfRdbClass)
        {
            m_blnBatch = true;
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
            if (m_cABAtenaBRef is not null)
            {
                m_cABAtenaBRef.m_blnBatch = true;
            }
            if (m_cABAtenaRirekiBRef is not null)
            {
                m_cABAtenaRirekiBRef.m_blnBatch = true;
            }
            if (m_cABDainoBRef is not null)
            {
                m_cABDainoBRef.m_blnBatch = true;
            }
            if (m_cABSfskBRef is not null)
            {
                m_cABSfskBRef.m_blnBatch = true;
            }
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        }

        // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
        // * 　　                          ByVal blnSelectAll as boolean)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABBatchAtenaGetBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass, bool blnSelectAll) : base(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll)
        {
            m_blnBatch = true;
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
            if (m_cABAtenaBRef is not null)
            {
                m_cABAtenaBRef.m_blnBatch = true;
            }
            if (m_cABAtenaRirekiBRef is not null)
            {
                m_cABAtenaRirekiBRef.m_blnBatch = true;
            }
            if (m_cABDainoBRef is not null)
            {
                m_cABDainoBRef.m_blnBatch = true;
            }
            if (m_cABSfskBRef is not null)
            {
                m_cABSfskBRef.m_blnBatch = true;
            }
            // * 履歴番号 000002 2005/01/25 追加開始（宮沢）
        }
        // * 履歴番号 000002 2005/01/25 追加終了（宮沢）

        // ************************************************************************************************
        // * メソッド名     管理情報取得（内部処理）
        // * 
        // * 構文           Private Function GetKanriJoho()
        // * 
        // * 機能　　    　　管理情報を取得する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        [SecuritySafeCritical]
        protected override void GetKanriJoho()
        {
            const string THIS_METHOD_NAME = "GetKanriJoho";
            // * 履歴番号 000001 2004/08/27 削除開始（宮沢）
            // Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '宛名管理情報Ｂクラス
            // * 履歴番号 000001 2004/08/27 削除終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                if (m_blnKanriJoho)
                {
                    return;
                }

                // 管理情報クラスのインスタンス作成
                // * 履歴番号 000001 2004/08/27 更新開始（宮沢）
                // cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                if (m_cfURAtenaKanriJoho is null)
                {
                    m_cfURAtenaKanriJoho = new URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);
                }
                // * 履歴番号 000001 2004/08/27 更新終了

                m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD;                // 住民コード表示桁数
                m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD;                 // 世帯コード表示桁数
                m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD;                // 住所コード表示桁数（管内のみ）
                m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD;          // 行政区コード表示桁数
                m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1;              // 地区コード１表示桁数
                m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2;              // 地区コード２表示桁数
                m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3;              // 地区コード３表示桁数
                m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho;          // 地区コード１表示名称
                m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho;          // 地区コード２表示名称
                m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho;          // 地区コード３表示名称
                m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho;  // 連絡先１表示名称
                m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho;  // 連絡先２表示名称

                // 管理情報取得済みフラグ設定
                m_blnKanriJoho = true;

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

    }
}
