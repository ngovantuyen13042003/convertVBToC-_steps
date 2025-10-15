// ************************************************************************************************
// * 業務名          宛名管理システム
// * 
// * クラス名        窓あき封筒用宛名編集クラス（ABMadoakiAtenaEditBClass.vb）
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2023/04/24　張　一帆
// * 
// * 著作権          （株）電算
// ************************************************************************************************
#region 修正履歴
// * 修正履歴　　履歴番号　　修正内容
// * 2023/04/24  AB-0590-1   窓あき封筒用宛名編集機能 新規作成
// *
#endregion
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    #region 窓あき封筒用宛名編集クラス

    // **
    // * 窓あき封筒用宛名編集クラス
    // *
    // * @version 1.0    2023/04/24
    // * @author 張　一帆
    // *
    public class ABMadoakiAtenaEditBClass
    {

        #region コンスタント定義
        // クラス名
        public const string THIS_CLASS_NAME = "ABMadoakiAtenaEditBClass";

        // オーバーフロー時編集パターン
        public enum WhenOverflow : short
        {
            Edit = 0,                        // 編集
            ReplaceOverflowChar = 1,         // オーバーフロー文字置き換え
            Empty = 2                       // 空白
        }

        // メンバ変数
        private Tools.UFLogClass m_cfLogClass;                                            // ログ出力クラス
        private UFControlData m_cfControlData;                                            // コントロールデータ
        private UFConfigDataClass m_cfConfigData;                                         // コンフィグデータ
        private ABMadoakiAtenaEditParamXClass m_cMadoakiAtenaEditParamXClass;             // 窓あき宛名編集パラメータ
        private ABMadoakiAtenaLengthParamXClass m_cMadoakiAtenaLengthParamXClass;         // 窓あき宛名文字数・行数指示パラメータ
        private short m_shtEditPaturnWhenOverflow;                                        // オーバーフロー時編集方法
        private string m_strOverflowChar;                                                 // オーバーフロー文字
        private bool m_blnOverflowFG;                                                  // オーバーフローフラグ
        private string m_strYubinNO;                                                      // 郵便番号
        private string m_strShichosonMeisho;                                              // 市町村名名称
        private string m_strJuSho;                                                        // 住所
        private string m_strKatagaki;                                                     // 方書
        private string m_strSofuGyoseiku;                                                 // 送付用行政区
        private bool m_blnSofuGyoseikuOverFlowFG;                                      // 送付用行政区オーバーフローフラグ
        private string[] m_strDaino_Or_SofuShimei_Array;                                  // 代納人/送付先氏名配列
        private bool m_blnDaino_Or_SofuShimeiOverflowFG;                               // 代納人/送付先氏名オーバーフローフラグ
        private short m_shtDaino_Or_SofuShimeiFont;                                       // 代納人/送付先氏名フォント
        private string[] m_strHonninShimei_Array;                                         // 本人氏名配列
        private bool m_blnHonninShimeiOverFlowFG;                                      // 本人氏名オーバーフローフラグ
        private short m_shtHonninShimeiFont;                                              // 本人氏名フォント
        private bool m_blnKatagakiran_StaiNusMei_EditFG;                               // 方書欄世帯主編集フラグ
        private string m_strSamakata;                                                     // 様方
        private string[] m_strJusho_Array;                                                // 住所配列
        private bool m_blnJushoOverFlowFG;                                             // 住所オーバーフローフラグ
        private string[] m_strKatagaki_Array;                                             // 方書配列
        private bool m_blnkatagakiOverFlowFG;                                          // 方書オーバーフローフラグ

        public short p_shtEditPaturnWhenOverflow
        {
            get
            {
                return m_shtEditPaturnWhenOverflow;
            }
            set
            {
                m_shtEditPaturnWhenOverflow = value;
            }
        }

        public string p_strOverflowChar
        {
            get
            {
                return m_strOverflowChar;
            }
            set
            {
                m_strOverflowChar = value;
            }
        }

        public bool p_blnOverflowFG
        {
            get
            {
                return m_blnOverflowFG;
            }
            set
            {
                m_blnOverflowFG = value;
            }
        }
        #endregion

        #region コンストラクタ
        // ************************************************************************************************
        // * メソッド名     コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // *                               ByVal cfConfigDataClass As UFConfigDataClass)
        // * 
        // * 機能　　       初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // *                cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public ABMadoakiAtenaEditBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass)
        {
            // 引数をメンバ変数にセットする
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigDataClass;

            // メンバ変数・オーバーフロー時編集パターンに0（編集）をセットする
            m_shtEditPaturnWhenOverflow = 0;

            // メンバ変数・オーバーフロー文字に全角アスタリスクをセットする
            m_strOverflowChar = "＊";

        }
        #endregion

        #region 窓あき宛名編集
        // ************************************************************************************************
        // * メソッド名     窓あき宛名編集
        // * 
        // * 構文           Public Function EditMadoakiAtena(ByVal cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass,
        // *                                ByVal cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass) As ABMadoakiAtenaReturnXClass
        // *
        // * 
        // * 機能　　       窓あき宛名編集
        // * 
        // * 引数           cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass      : 窓あき宛名編集パラメータ
        // *                cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass  : 窓あき宛名文字数・行数指示パラメータ
        // * 
        // * 戻り値         窓あき宛名編集結果パラメータ
        // ************************************************************************************************
        public ABMadoakiAtenaReturnXClass EditMadoakiAtena(ABMadoakiAtenaEditParamXClass cMadoakiAtenaEditParamXClass, ABMadoakiAtenaLengthParamXClass cMadoakiAtenaLengthParamXClass)
        {

            const string THIS_METHOD_NAME = "EditMadoakiAtena";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass = new Tools.UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId);
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 1 窓あき宛名編集結果パラメータのインスタンスを生成する
                var cMadoakiAtenaReturnXClass = new ABMadoakiAtenaReturnXClass();

                // 2 引数をメンバ変数へセットする
                m_cMadoakiAtenaEditParamXClass = cMadoakiAtenaEditParamXClass;                  // 窓あき宛名編集パラメータ
                m_cMadoakiAtenaLengthParamXClass = cMadoakiAtenaLengthParamXClass;              // 窓あき宛名文字数・行数指示パラメータ

                // 3 窓あき宛名編集パラメータの空白削除（TrimMadoakiAtenaEditParam）を呼び出す
                TrimMadoakiAtenaEditParam();

                // 4 窓あき宛名編集パラメータのチェック（CheckMadoakiAtenaEditParam）を呼び出す
                CheckMadoakiAtenaEditParam();

                // 5 窓あき宛名文字数・行数指示パラメータのチェック（CheckMadoakiAtenaLengthParam）を呼び出す
                CheckMadoakiAtenaLengthParam();

                // 6 方書欄世帯主名編集（EditKatagakiSetainushi)を呼び出す
                EditKatagakiSetainushi();

                // 7 郵便番号編集（GetYubinHenshu）を呼びだす
                GetYubinHenshu();

                // 8 市町村名編集（EditShichosonMeisho）を呼び出す
                EditShichosonMeisho();

                // 9 住所編集（EditJusho）を呼び出す
                EditJusho();

                // 10 連結住所編集(EditJoinJusho)を呼び出す
                EditJoinJusho();

                // 11 方書編集（EditKatagaki）を呼び出す
                EditKatagaki();

                // 12 送付用行政区の編集（EditSofuGyoseiku）を呼び出す
                EditSofuGyoseiku();

                // 13 代納人/送付先氏名の編集（EditDainoShimei）を呼び出す
                EditDainoShimei();

                // 14 本人氏名の編集を行う
                if (!string.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei) && !string.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei) && m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei != m_cMadoakiAtenaEditParamXClass.p_strHonninShimei)
                {
                    // 本人氏名編集（EditHonninShimei）を呼び出す
                    EditHonninShimei();
                }
                else
                {
                    m_blnHonninShimeiOverFlowFG = false;
                    m_shtHonninShimeiFont = 0;
                    // 窓あき宛名文字数・行数指示パラメータ・氏名行数＝1の場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 1)
                    {
                        // メンバ変数・本人氏名配列を最大インデックス0で再定義し、空白をセットする
                        m_strHonninShimei_Array = new string[1];
                        m_strHonninShimei_Array[0] = "";
                    }
                    else
                    {
                        // メンバ変数・本人氏名配列を最大インデックス1で再定義し、各配列に空白をセットする
                        m_strHonninShimei_Array = new string[2];
                        m_strHonninShimei_Array[0] = "";
                        m_strHonninShimei_Array[1] = "";
                    }
                }

                // 15 窓あき宛名編集結果編集（EditMadoakiAtenaReturn）を呼び出す																			
                cMadoakiAtenaReturnXClass = EditMadoakiAtenaReturn(cMadoakiAtenaReturnXClass);

                // 16 窓あき宛名編集結果パラメータを返却する
                return cMadoakiAtenaReturnXClass;

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 窓あき宛名編集パラメータの空白カット
        // ************************************************************************************************
        // * メソッド名      窓あき宛名編集パラメータの空白カット
        // * 
        // * 構文           Private Sub TrimMadoakiAtenaEditParam()
        // * 
        // * 機能　　        メンバ変数の「窓あき宛名編集パラメータ」のString項目について後ろ空白を削除する
        // * 
        // * 引数           なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void TrimMadoakiAtenaEditParam()
        {

            const string THIS_METHOD_NAME = "TrimMadoakiAtenaEditParam";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    withBlock.p_strYubinNo = withBlock.p_strYubinNo.TrimEnd();                                 // 郵便番号
                    withBlock.p_strKenmei = withBlock.p_strKenmei.TrimEnd();                                   // 県名
                    withBlock.p_strGunmei = withBlock.p_strGunmei.TrimEnd();                                   // 郡名
                    withBlock.p_strShichosonMei = withBlock.p_strShichosonMei.TrimEnd();                       // 市町村名
                    withBlock.p_strJusho = withBlock.p_strJusho.TrimEnd();                                     // 住所
                    withBlock.p_strBanchi = withBlock.p_strBanchi.TrimEnd();                                   // 番地
                    withBlock.p_strKatagaki = withBlock.p_strKatagaki.TrimEnd();                               // 方書
                    withBlock.p_strGyoseikumei = withBlock.p_strGyoseikumei.TrimEnd();                         // 行政区名
                    withBlock.p_strKannaiKangaiKB = withBlock.p_strKannaiKangaiKB.TrimEnd();                   // 管内・管外区分
                    withBlock.p_strJushoEditPaturn_KakkoL = withBlock.p_strJushoEditPaturn_KakkoL.TrimEnd();   // 住所編集方法の括弧(左)
                    withBlock.p_strJushoEditPaturn_KakkoR = withBlock.p_strJushoEditPaturn_KakkoR.TrimEnd();   // 住所編集方法の括弧(右)
                    withBlock.p_strCNS_Samakata = withBlock.p_strCNS_Samakata.TrimEnd();                       // 様方コンスタント
                    withBlock.p_strDaino_Or_Sofushimei = withBlock.p_strDaino_Or_Sofushimei.TrimEnd();         // 代納人/送付先氏名
                    withBlock.p_strHonninShimei = withBlock.p_strHonninShimei.TrimEnd();                       // 本人氏名
                    withBlock.p_strCNS_Sama = withBlock.p_strCNS_Sama.TrimEnd();                               // 様コンスタント
                    withBlock.p_strHonninShimei_KakkoL = withBlock.p_strHonninShimei_KakkoL.TrimEnd();         // 本人氏名の括弧(左)
                    withBlock.p_strHonninShimei_KakkoR = withBlock.p_strHonninShimei_KakkoR.TrimEnd();         // 本人氏名の括弧(右)
                    withBlock.p_strCNS_Samabun = withBlock.p_strCNS_Samabun.TrimEnd();                         // 様分コンスタント
                    withBlock.p_strDainoKBMeisho = withBlock.p_strDainoKBMeisho.TrimEnd();                     // 代納区分名称
                    withBlock.p_strDainoKBMeisho_KakkoL = withBlock.p_strDainoKBMeisho_KakkoL.TrimEnd();       // 代納区分名称の括弧(左)
                    withBlock.p_strDainoKBMeisho_KakkoR = withBlock.p_strDainoKBMeisho_KakkoR.TrimEnd();       // 代納区分名称の括弧(右)
                    withBlock.p_strSofugyoseiku_KakkoL = withBlock.p_strSofugyoseiku_KakkoL.TrimEnd();         // 送付用行政区の括弧(左)
                    withBlock.p_strSofugyoseiku_KakkoR = withBlock.p_strSofugyoseiku_KakkoR.TrimEnd();         // 送付用行政区の括弧(右)
                    withBlock.p_strGyoseikuCD = withBlock.p_strGyoseikuCD.TrimEnd();                           // 行政区ｺｰﾄﾞ
                    withBlock.p_strStaiNusmei = withBlock.p_strStaiNusmei.TrimEnd();                           // 世帯主名
                    withBlock.p_strJusho_Honnin = withBlock.p_strJusho_Honnin.TrimEnd();                       // 本人・住所
                    withBlock.p_strBanchi_Honnin = withBlock.p_strBanchi_Honnin.TrimEnd();                     // 本人・番地
                    withBlock.p_strGyoseikuMei_Honnin = withBlock.p_strGyoseikuMei_Honnin.TrimEnd();           // 本人・行政区名
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 窓あき宛名編集パラメータのチェック
        // ************************************************************************************************
        // * メソッド名      窓あき宛名編集パラメータのチェック
        // * 
        // * 構文             Private Sub CheckMadoakiAtenaEditParam()
        // * 
        // * 機能　　         メンバ変数の窓あき宛名編集パラメータの内容をチェックする
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void CheckMadoakiAtenaEditParam()
        {

            const string THIS_METHOD_NAME = "CheckMadoakiAtenaEditParam";         // メソッド名

            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFErrorClass cfErrorClass;                  // エラー処理クラス

            try
            {
                cfErrorClass = new UFErrorClass();
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータ・管内・管外区分＝’1’（管内）　OR　’2’（管外）でない場合
                if (!(m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB == ABConstClass.KANNAIKB || m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB == ABConstClass.KANGAIKB))
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：管内管外）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：管内管外", objErrorStruct.m_strErrorCode);

                }

                // パラメータ・市町村名称編集方法＜0（空白）　OR　パラメータ・市町村名称編集方法＞3（市町村名）
                if (m_cMadoakiAtenaEditParamXClass.p_shtShichosonMeishoEditPaturn < 0 || m_cMadoakiAtenaEditParamXClass.p_shtShichosonMeishoEditPaturn > 3)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：市町村名称編集）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：市町村名称編集", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・住所編集方法＜1（住所）　OR　パラメータ・住所編集方法＞6（番地のみ）の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn < 1 || m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn > 6)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：住所編集）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：住所編集", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・様方編集方法＜0（空白）　or　パラメータ・様方編集方法＞2（末尾）の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn < 0 || m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn > 2)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：様方編集）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：様方編集", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・様方編集方法≠0（空白）　AND　パラメータ・様方コンスタント＝空白の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn != 0 && m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata == string.Empty)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：様方コンスタント）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：様方コンスタント", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・様編集方法≠0（空白）　AND　パラメータ・様コンスタント＝空白の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn != 0 && m_cMadoakiAtenaEditParamXClass.p_strCNS_Sama == string.Empty)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：様コンスタント）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：様コンスタント", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・様編集方法＜0（空白）　or　パラメータ・様編集方法＞2（末尾）の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn < 0 || m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn > 2)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：様編集）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：様編集", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・送付用行政区編集方法＜0（空白）　OR　パラメータ・送付用行政区編集方法＞3（行政区コード括弧）の場合
                if (m_cMadoakiAtenaEditParamXClass.p_shtSofuGyoseikuEditPaturn < 0 || m_cMadoakiAtenaEditParamXClass.p_shtSofuGyoseikuEditPaturn > 3)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：送付用行政区編集方法）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：送付用行政区編集方法", objErrorStruct.m_strErrorCode);
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

        #region 窓あき宛名文字数・行数指示パラメータのチェック
        // ************************************************************************************************
        // * メソッド名      窓あき宛名文字数・行数指示パラメータのチェック
        // * 
        // * 構文             Private Sub CheckMadoakiAtenaLengthParam()
        // * 
        // * 機能　　         メンバ変数の窓あき宛名文字数・行数指示パラメータの内容をチェックする
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void CheckMadoakiAtenaLengthParam()
        {

            const string THIS_METHOD_NAME = "CheckMadoakiAtenaLengthParam";         // メソッド名

            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            UFErrorClass cfErrorClass;                  // エラー処理クラス

            try
            {
                cfErrorClass = new UFErrorClass();

                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータ・住所行数＜1　OR　パラメータ・住所行数＞3　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount > 3)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：住所行数）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：住所行数", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・住所1行あたりの文字数　<　1　Or　パラメータ住所1行あたりの文字数>1000の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine > 1000)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：住所文字数）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：住所文字数", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・方書行数＜1　OR　パラメータ・方書行数＞2　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount > 2)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：方書行数）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：方書行数", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・方書1行あたりの文字数＜1　OR　パラメータ・方書1行あたりの文字数＞1000　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline > 1000)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：方書文字数）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：方書文字数", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・氏名行数<1　Or　パラメータ・氏名行数>2　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount > 2)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：氏名行数）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：氏名行数", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・氏名1行あたりの文字数大フォント　<　1　Or　パラメータ・氏名1行当たりの文字数大フォント　>　80　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont > 80)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：氏名文字数大）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数大", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・氏名1行あたりの文字数小フォント　<　1　Or　パラメータ・氏名1行当たりの文字数小フォント　>　120　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont < 1 || m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont > 120)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：氏名文字数小）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数小", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・氏名1行あたりの文字数小フォント　<　パラメータ・氏名1行当たりの文字数大フォント　の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont < m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：氏名文字数大小逆転）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数大小逆転", objErrorStruct.m_strErrorCode);
                }

                // パラメータ・送付用行政区文字数<0　Or　パラメータ・送付用行政区文字数>30の場合
                if (m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength < 0 || m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength > 30)
                {
                    // エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438);
                    // 例外を生成（ABE003438　パラメータエラー：氏名文字数小）
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "：送付用行政区文字数", objErrorStruct.m_strErrorCode);
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

        #region 方書欄世帯主名編集
        // ************************************************************************************************
        // * メソッド名       方書欄世帯主名編集
        // * 
        // * 構文             Private Sub EditKatagakiSetainushi()
        // * 
        // * 機能　　         世帯主名方書欄編集が指示された場合、方書欄に世帯主名を設定する
        // * 
        // * 引数             なし
        // * 
        // * 戻り値           なし
        // ************************************************************************************************
        private void EditKatagakiSetainushi()
        {

            const string THIS_METHOD_NAME = "EditKatagakiSetainushi";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // メンバ変数・方書欄世帯主編集フラグにFalseをセットする
                m_blnKatagakiran_StaiNusMei_EditFG = false;

                // メンバ変数・様方に空白をセットする
                m_strSamakata = string.Empty;

                // 窓あき宛名編集パラメータ・方書編集フラグ＝True　AND	
                // 窓あき宛名編集パラメータ・世帯主名方書欄編集フラグ＝True　AND	
                // 窓あき宛名編集パラメータ・方書＝空白　AND	
                // （窓あき宛名編集パラメータ・代納人/送付先氏名＝本人氏名　OR	
                // 窓あき宛名編集パラメータ・代納人/送付先氏名＝空白）　AND	
                // 窓あき宛名編集パラメータ・住所＝本人・住所　AND	
                // 窓あき宛名編集パラメータ・番地＝本人・番地　AND	
                // 窓あき宛名編集パラメータ・本人氏名≠世帯主名　AND	
                // 窓あき宛名編集パラメータ・世帯主名≠空白　の場合	
                if (m_cMadoakiAtenaEditParamXClass.p_blnKatagakiFG && m_cMadoakiAtenaEditParamXClass.p_blnKatagakiran_StaiNusmei_EditFG && string.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strKatagaki) && m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei == m_cMadoakiAtenaEditParamXClass.p_strHonninShimei | string.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei) && m_cMadoakiAtenaEditParamXClass.p_strJusho == m_cMadoakiAtenaEditParamXClass.p_strJusho_Honnin && m_cMadoakiAtenaEditParamXClass.p_strBanchi == m_cMadoakiAtenaEditParamXClass.p_strBanchi_Honnin && m_cMadoakiAtenaEditParamXClass.p_strHonninShimei != m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei && !string.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei))
                {
                    // 窓あき宛名編集パラメータ・管内・管外区分＝1（管内）　AND
                    // 窓あき宛名編集パラメータ・住所編集方法≧2（行政区）　AND
                    // 窓あき宛名編集パラメータ・住所編集方法≦5（行政区1空白）
                    if (m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB == ABConstClass.KANNAIKB && m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn >= 2 && m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn <= 5)
                    {
                        // 窓あき宛名編集パラメータ・行政区名＝窓あき宛名編集パラメータ・本人・行政区名の場合
                        if (m_cMadoakiAtenaEditParamXClass.p_strGyoseikumei == m_cMadoakiAtenaEditParamXClass.p_strGyoseikuMei_Honnin)
                        {
                            m_blnKatagakiran_StaiNusMei_EditFG = true;
                            m_strSamakata = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata;
                            m_cMadoakiAtenaEditParamXClass.p_strKatagaki = m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei;
                        }
                    }
                    // 上記以外
                    else
                    {
                        m_blnKatagakiran_StaiNusMei_EditFG = true;
                        m_strSamakata = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata;
                        m_cMadoakiAtenaEditParamXClass.p_strKatagaki = m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei;
                    }
                }
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 郵便番号編集
        // ************************************************************************************************
        // * メソッド名      郵便番号編集
        // * 
        // * 構文            Private Sub GetYubinHenshu()
        // * 
        // * 機能　　        郵便番号を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void GetYubinHenshu()
        {

            const string THIS_METHOD_NAME = "GetYubinHenshu";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 窓あき宛名編集パラメータ・郵便番号の文字列長　≦　3の場合
                if (m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim().RLength() <= 3)
                {
                    // 窓あき宛名編集パラメータ・郵便番号をメンバ変数・郵便番号へセットする
                    m_strYubinNO = m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim();
                }
                else
                {
                    // 窓あき宛名編集パラメータ・郵便番号の先頭3桁+「-」+窓あき宛名編集パラメータ・郵便番号の4桁目以降をメンバ変数・郵便番号にセットする
                    m_strYubinNO = m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim().RSubstring(0, 3) + "-" + m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim().RSubstring(3);
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 市町村名編集
        // ************************************************************************************************
        // * メソッド名      市町村名編集
        // * 
        // * 構文            Private Sub EditShichosonMeisho()
        // * 
        // * 機能　　        市町村名編集方法の指示に従い、県郡市町村名を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditShichosonMeisho()
        {

            const string THIS_METHOD_NAME = "EditShichosonMeisho";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    // 窓あき宛名編集パラメータ・管内管外＝’1’（管内）の場合
                    if (withBlock.p_strKannaiKangaiKB == ABConstClass.KANNAIKB)
                    {
                        // 窓あき宛名編集パラメータ・市町村名称編集方法
                        switch (withBlock.p_shtShichosonMeishoEditPaturn)
                        {
                            case var @case when @case == withBlock.ShichosonMeishoEditPaturn.Empty:               // 0（空白）の場合
                                {
                                    // メンバ変数・市町村名称は空白とする
                                    m_strShichosonMeisho = string.Empty;
                                    break;
                                }
                            case var case1 when case1 == withBlock.ShichosonMeishoEditPaturn.Kenmei:              // 1（県名）の場合
                                {
                                    // メンバ変数・市町村名称は窓あき宛名編集パラメータ・県名+郡名+市町村名をセットする
                                    m_strShichosonMeisho = withBlock.p_strKenmei + withBlock.p_strGunmei + withBlock.p_strShichosonMei;
                                    break;
                                }
                            case var case2 when case2 == withBlock.ShichosonMeishoEditPaturn.Gunmei:              // 2（郡名）の場合
                                {
                                    // メンバ変数・市町村名称は窓あき宛名編集パラメータ・郡名+市町村名をセットする
                                    m_strShichosonMeisho = withBlock.p_strGunmei + withBlock.p_strShichosonMei;
                                    break;
                                }
                            case var case3 when case3 == withBlock.ShichosonMeishoEditPaturn.ShichosonMei:        // 3（市町村名）の場合
                                {
                                    // メンバ変数・市町村名称は窓あき宛名編集パラメータ市町村名をセットする
                                    m_strShichosonMeisho = withBlock.p_strShichosonMei;
                                    break;
                                }
                        }
                    }
                    else                                                         // 上記以外の場合
                    {
                        // メンバ変数・市町村名称は空白とする
                        m_strShichosonMeisho = string.Empty;
                    }
                    // 窓あき宛名編集パラメータ・郵便番号付加有無フラグ＝true　AND　メンバ変数・市町村名称の文字列長＞0の場合
                    if (withBlock.p_blnYubinNoFG && m_strShichosonMeisho.RLength() > 0)
                    {
                        // メンバ変数・市町村名はメンバ変数・郵便番号+全角空白+メンバ変数・市町村名をセットする
                        m_strShichosonMeisho = m_strYubinNO + "　" + m_strShichosonMeisho;
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 住所編集
        // ************************************************************************************************
        // * メソッド名      住所編集
        // * 
        // * 構文            Private Sub EditJusho()
        // * 
        // * 機能　　        住所編集方法の指示に従い、住所を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditJusho()
        {

            const string THIS_METHOD_NAME = "EditJusho";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    // 窓あき宛名編集パラメータ・管内管外='1’（管内）の場合
                    if (withBlock.p_strKannaiKangaiKB == ABConstClass.KANNAIKB)
                    {
                        switch (withBlock.p_shtJushoEditPaturn)                     // 窓あき宛名編集パラメータ・住所編集方法
                        {
                            case var @case when @case == withBlock.JushoEditPaturn.Jusho:                     // 1（住所）の場合
                                {
                                    m_strJuSho = withBlock.p_strJusho;
                                    break;
                                }
                            case var case1 when case1 == withBlock.JushoEditPaturn.Gyoseiku:                  // 2（行政区）
                                {
                                    // 窓あき宛名編集パラメータ・行政区名≠空白の場合
                                    if (withBlock.p_strGyoseikumei != string.Empty)
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・行政区名をセットする
                                        m_strJuSho = withBlock.p_strGyoseikumei;
                                    }
                                    else
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                        m_strJuSho = withBlock.p_strJusho;
                                    }

                                    break;
                                }
                            case var case2 when case2 == withBlock.JushoEditPaturn.JushoKakkoGyoseiku:        // 3（住所（行政区））
                                {
                                    // 窓あき宛名編集パラメータ・行政区名≠空白　AND 窓あき宛名編集パラメータ・住所≠空白の場合
                                    if (withBlock.p_strGyoseikumei.Trim() != string.Empty && withBlock.p_strJusho.Trim() != string.Empty)
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所+住所編集方法の括弧(左)+行政区名+住所編集方法の括弧(右)をセットする
                                        m_strJuSho = withBlock.p_strJusho + withBlock.p_strJushoEditPaturn_KakkoL + withBlock.p_strGyoseikumei + withBlock.p_strJushoEditPaturn_KakkoR;
                                    }
                                    else
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                        m_strJuSho = withBlock.p_strJusho;
                                    }

                                    break;
                                }
                            case var case3 when case3 == withBlock.JushoEditPaturn.GyoseikuKakkoJusho:        // 4（行政区（住所））の場合  
                                {
                                    // 窓あき宛名編集パラメータ・行政区名≠空白　AND 窓あき宛名編集パラメータ・住所≠空白の場合
                                    if (withBlock.p_strGyoseikumei.Trim() != string.Empty && withBlock.p_strJusho.Trim() != string.Empty)
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・行政区名+住所編集方法の括弧(左)+住所+住所編集方法の括弧(右)をセットする
                                        m_strJuSho = withBlock.p_strGyoseikumei + withBlock.p_strJushoEditPaturn_KakkoL + withBlock.p_strJusho + withBlock.p_strJushoEditPaturn_KakkoR;
                                    }
                                    else if (withBlock.p_strJusho == string.Empty)
                                    {
                                        // 窓あき宛名編集パラメータ・住所＝空白　の場合
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・行政区名をセットする
                                        m_strJuSho = withBlock.p_strGyoseikumei;
                                    }
                                    else
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                        m_strJuSho = withBlock.p_strJusho;
                                    }

                                    break;
                                }
                            case var case4 when case4 == withBlock.JushoEditPaturn.GyoseikuOneBlanK:          // 5（行政区1空白）の場合
                                {
                                    // 窓あき宛名編集パラメータ・行政区名≠空白の場合
                                    if (withBlock.p_strGyoseikumei != string.Empty)
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・行政区名+全角空白をセットする
                                        m_strJuSho = withBlock.p_strGyoseikumei + "　";
                                    }
                                    else
                                    {
                                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                        m_strJuSho = withBlock.p_strJusho;
                                    }

                                    break;
                                }
                            case var case5 when case5 == withBlock.JushoEditPaturn.BanchiOnly:                // 6（番地のみ）の場合
                                {
                                    // メンバ変数・住所は空白をセットする
                                    m_strJuSho = string.Empty;
                                    break;
                                }
                        }
                    }
                    else                                                    // 上記以外の場合
                    {
                        // メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                        m_strJuSho = withBlock.p_strJusho;
                    }

                    // 窓あき宛名編集パラメータ・郵便番号付加有無フラグ＝true　AND　メンバ変数・市町村名称の文字列長＝0の場合
                    if (withBlock.p_blnYubinNoFG && m_strShichosonMeisho.RLength() == 0)
                    {
                        // メンバ変数・住所はメンバ変数・郵便番号+全角空白+メンバ変数・住所をセットする
                        m_strJuSho = m_strYubinNO + "　" + m_strJuSho;
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 連結住所編集
        // ************************************************************************************************
        // * メソッド名      連結住所編集
        // * 
        // * 構文            Private Sub EditJoinJusho()
        // * 
        // * 機能　　        市町村名称、住所、番地より戻り値・住所を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditJoinJusho()
        {

            const string THIS_METHOD_NAME = "EditJoinJusho";         // メソッド名

            string strShichosonMeishoJusho;       // 変数・市町村名住所
            string strJushoZentai;                // 変数・住所全体

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // メンバ変数・オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = false;

                // 変数・市町村名住所にメンバ変数・市町村名+メンバ変数・住所をセットする
                strShichosonMeishoJusho = m_strShichosonMeisho + m_strJuSho;

                // 変数・住所全体に変数・市町村名住所+窓あき宛名編集パラメータ・番地をセットする
                strJushoZentai = strShichosonMeishoJusho + m_cMadoakiAtenaEditParamXClass.p_strBanchi;

                switch (m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount)                      // 窓あき宛名文字数・行数指示パラメータ・住所行数
                {
                    case 1:
                        {
                            // 文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・住所配列(0)にセットする
                            m_strJusho_Array = this.CheckOverflow(strJushoZentai, m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine, m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount);
                            m_strJusho_Array[0] = m_strJusho_Array[0].TrimStart();
                            break;
                        }

                    case 2:
                        {
                            // メンバ変数・住所配列を最大インデックス1で再定義する
                            m_strJusho_Array = new string[2];

                            {
                                ref var withBlock = ref m_cMadoakiAtenaLengthParamXClass;
                                // 変数・住所全体の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                if (strJushoZentai.RLength() <= withBlock.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)に空白を設定する
                                    m_strJusho_Array[0] = string.Empty;

                                    // メンバ変数・住所配列(1)に変数・住所全体を設定する
                                    m_strJusho_Array[1] = strJushoZentai;
                                }
                                // 変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　And
                                // 窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                else if (strJushoZentai.RLength() > withBlock.p_shtJushoLengthEveryLine && strShichosonMeishoJusho.RLength() <= withBlock.p_shtJushoLengthEveryLine && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= withBlock.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)に変数・市町村名住所を設定する
                                    m_strJusho_Array[0] = strShichosonMeishoJusho;

                                    // メンバ変数・住所配列(1)に窓あき宛名編集パラメータ・番地を設定する
                                    m_strJusho_Array[1] = m_cMadoakiAtenaEditParamXClass.p_strBanchi;
                                }
                                else
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・住所配列にセットする
                                    m_strJusho_Array = this.CheckOverflow(strJushoZentai, withBlock.p_shtJushoLengthEveryLine, withBlock.p_shtJushoLineCount);
                                }
                            }

                            // メンバ変数・住所配列の各配列の先頭空白を削除する
                            m_strJusho_Array[0] = m_strJusho_Array[0].TrimStart();
                            m_strJusho_Array[1] = m_strJusho_Array[1].TrimStart();
                            break;
                        }

                    case 3:
                        {
                            // メンバ変数・住所配列を最大インデックス2で再定義する
                            m_strJusho_Array = new string[3];

                            {
                                ref var withBlock1 = ref m_cMadoakiAtenaLengthParamXClass;
                                // 変数・住所全体の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                if (strJushoZentai.RLength() <= withBlock1.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)に空白を設定する
                                    m_strJusho_Array[0] = string.Empty;

                                    // メンバ変数・住所配列(1)に変数・住所全体を設定する
                                    m_strJusho_Array[1] = strJushoZentai;

                                    // メンバ変数・住所配列(2)に空白を設定する
                                    m_strJusho_Array[2] = string.Empty;
                                }
                                // 変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　And
                                // 窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                else if (strJushoZentai.RLength() > withBlock1.p_shtJushoLengthEveryLine && strShichosonMeishoJusho.RLength() <= withBlock1.p_shtJushoLengthEveryLine && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= withBlock1.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)に空白を設定する
                                    m_strJusho_Array[0] = string.Empty;

                                    // メンバ変数・住所配列(1)に変数・市町村名住所を設定する
                                    m_strJusho_Array[1] = strShichosonMeishoJusho;

                                    // メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                    m_strJusho_Array[2] = m_cMadoakiAtenaEditParamXClass.p_strBanchi;
                                }
                                // 変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 変数・市町村名住所の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // メンバ変数・市町村名の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // メンバ変数・住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                else if (strJushoZentai.RLength() > withBlock1.p_shtJushoLengthEveryLine && strShichosonMeishoJusho.RLength() > withBlock1.p_shtJushoLengthEveryLine && m_strShichosonMeisho.RLength() <= withBlock1.p_shtJushoLengthEveryLine && m_strJuSho.RLength() <= withBlock1.p_shtJushoLengthEveryLine && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= withBlock1.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)にメンバ変数・市町村名を設定する
                                    m_strJusho_Array[0] = m_strShichosonMeisho;

                                    // メンバ変数・住所配列(1)にメンバ変数・住所を設定する
                                    m_strJusho_Array[1] = m_strJuSho;

                                    // メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                    m_strJusho_Array[2] = m_cMadoakiAtenaEditParamXClass.p_strBanchi;
                                }
                                // 変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数×2　AND
                                // （メンバ変数・市町村名の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　OR
                                // メンバ変数・住所の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数）　AND
                                // 窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                else if (strJushoZentai.RLength() > withBlock1.p_shtJushoLengthEveryLine && strShichosonMeishoJusho.RLength() <= withBlock1.p_shtJushoLengthEveryLine * 2 && (m_strShichosonMeisho.RLength() > withBlock1.p_shtJushoLengthEveryLine || m_strJuSho.RLength() > withBlock1.p_shtJushoLengthEveryLine) && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= withBlock1.p_shtJushoLengthEveryLine)
                                {
                                    // メンバ変数・住所配列(0)に変数・市町村名住所の先頭から住所1行当たりの文字数分を設定する
                                    m_strJusho_Array[0] = strShichosonMeishoJusho.RSubstring(0, withBlock1.p_shtJushoLengthEveryLine);

                                    // メンバ変数・住所配列(1)に変数・市町村名住所の住所1行当たりの文字数以降を設定する
                                    m_strJusho_Array[1] = strShichosonMeishoJusho.RSubstring(withBlock1.p_shtJushoLengthEveryLine);

                                    // メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                    m_strJusho_Array[2] = m_cMadoakiAtenaEditParamXClass.p_strBanchi;
                                }
                                // 変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 窓あき宛名編集パラメータ・番地の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                // 窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数×2　の場合
                                else if (strJushoZentai.RLength() > withBlock1.p_shtJushoLengthEveryLine && strShichosonMeishoJusho.RLength() <= withBlock1.p_shtJushoLengthEveryLine && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() > withBlock1.p_shtJushoLengthEveryLine && m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= withBlock1.p_shtJushoLengthEveryLine * 2)
                                {
                                    // メンバ変数・住所配列(0)に変数・市町村名住所を設定する
                                    m_strJusho_Array[0] = strShichosonMeishoJusho;

                                    // メンバ変数・住所配列(1)に窓あき宛名編集パラメータ・番地の先頭から住所1行当たりの文字数分を設定する
                                    m_strJusho_Array[1] = m_cMadoakiAtenaEditParamXClass.p_strBanchi.RSubstring(0, withBlock1.p_shtJushoLengthEveryLine);

                                    // メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地の住所1行当たりの文字数分以降を設定する
                                    m_strJusho_Array[2] = m_cMadoakiAtenaEditParamXClass.p_strBanchi.RSubstring(withBlock1.p_shtJushoLengthEveryLine);
                                }
                                else
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・住所配列にセットする
                                    m_strJusho_Array = this.CheckOverflow(strJushoZentai, withBlock1.p_shtJushoLengthEveryLine, withBlock1.p_shtJushoLineCount);
                                }
                            }

                            // メンバ変数・住所配列の各配列の先頭空白を削除する
                            m_strJusho_Array[0] = m_strJusho_Array[0].TrimStart();
                            m_strJusho_Array[1] = m_strJusho_Array[1].TrimStart();
                            m_strJusho_Array[2] = m_strJusho_Array[2].TrimStart();
                            break;
                        }

                }

                // メンバ変数・住所オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                m_blnJushoOverFlowFG = m_blnOverflowFG;

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 方書編集
        // ************************************************************************************************
        // * メソッド名      方書編集
        // * 
        // * 構文            Private Sub EditKatagaki()
        // * 
        // * 機能　　        方書編集有無フラグの指示に従い、方書を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditKatagaki()
        {

            const string THIS_METHOD_NAME = "EditKatagaki";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    // 窓あき宛名編集パラメータ・方書編集有無フラグ＝Falseの場合
                    if (withBlock.p_blnKatagakiFG == false)
                    {
                        // 変数・方書は空白をセットする
                        m_strKatagaki = string.Empty;
                    }
                    else
                    {
                        // 変数・方書は窓あき宛名編集パラメータ・方書をセットする
                        m_strKatagaki = withBlock.p_strKatagaki;
                    }

                    // メンバ変数・オーバーフローフラグにFalseをセットする
                    m_blnOverflowFG = false;

                    // 窓あき宛名文字数・行数指示パラメータ・方書行数＝1の場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount == 1)
                    {

                        // メンバ変数・方書配列を最大インデックス0で再定義する
                        m_strKatagaki_Array = new string[1];

                        switch (withBlock.p_shtSamakataEditPaturn)                  // 窓あき宛名編集パラメータ・様方編集方法
                        {

                            case var @case when @case == withBlock.SamakataEditPaturn.Empty:                  // 0（空白）の場合
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                    m_strKatagaki_Array[0] = this.CheckOverflow(m_strKatagaki, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart();
                                    break;
                                }
                            case var case1 when case1 == withBlock.SamakataEditPaturn.OneBlank:               // 1（1空白）の場合
                                {
                                    // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                    m_strKatagaki_Array[0] = this.PadCharOneBlank(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart();                                       // 上記以外
                                    break;
                                }

                            default:
                                {
                                    // 最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(0)にセットする
                                    m_strKatagaki_Array[0] = this.PadCharLast(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart();
                                    break;
                                }
                        }

                        // メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnkatagakiOverFlowFG = m_blnOverflowFG;
                    }

                    // 窓あき宛名文字数・行数指示パラメータ・方書行数＝2の場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount == 2)
                    {
                        // メンバ変数・方書配列を最大インデックス1で再定義し、各配列に空白をセットする
                        m_strKatagaki_Array = new string[2];
                        m_strKatagaki_Array[0] = string.Empty;
                        m_strKatagaki_Array[1] = string.Empty;

                        switch (withBlock.p_shtSamakataEditPaturn)                      // 窓あき宛名編集パラメータ・様方編集方法
                        {
                            case var case2 when case2 == withBlock.SamakataEditPaturn.Empty:                      // 0（空白）の場合
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列にセットする
                                    m_strKatagaki_Array = this.CheckOverflow(m_strKatagaki, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount);
                                    m_strKatagaki_Array[0] = m_strKatagaki_Array[0].TrimStart();
                                    m_strKatagaki_Array[1] = m_strKatagaki_Array[1].TrimStart();
                                    if (m_blnOverflowFG == false)
                                    {
                                        // メンバ変数・方書配列(0)をメンバ変数・方書配列(1)へセットする
                                        m_strKatagaki_Array[1] = m_strKatagaki_Array[0];
                                        // メンバ変数・方書配列(0)をクリアする
                                        m_strKatagaki_Array[0] = string.Empty;
                                    }

                                    break;
                                }

                            case var case3 when case3 == withBlock.SamakataEditPaturn.OneBlank:                   // 1（1空白）の場合
                                {
                                    // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(1)にセットする
                                    m_strKatagaki_Array[1] = this.PadCharOneBlank(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart();                                           // 上記以外
                                    break;
                                }

                            default:
                                {
                                    // 最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(1)にセットする
                                    m_strKatagaki_Array[1] = this.PadCharLast(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart();
                                    break;
                                }
                        }

                        // メンバ変数・オーバーフローフラグ＝falseの場合
                        if (m_blnOverflowFG == false)
                        {
                            // メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnkatagakiOverFlowFG = m_blnOverflowFG;
                        }

                        else
                        {
                            // メンバ変数・オーバーフローフラグにFalseをセットする
                            m_blnOverflowFG = false;

                            // メンバ変数・方書配列に空白をセットする
                            m_strKatagaki_Array[0] = string.Empty;
                            m_strKatagaki_Array[1] = string.Empty;

                            switch (withBlock.p_shtSamakataEditPaturn)                      // 窓あき宛名編集パラメータ・様方編集方法
                            {
                                case var case4 when case4 == withBlock.SamakataEditPaturn.Empty:                      // 0（空白）の場合
                                    {
                                        // 文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                        m_strKatagaki_Array[0] = this.CheckOverflow(m_strKatagaki, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * (short)2).TrimStart();
                                        break;
                                    }
                                case var case5 when case5 == withBlock.SamakataEditPaturn.OneBlank:                   // 1（1空白）の場合
                                    {
                                        // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                        m_strKatagaki_Array[0] = this.PadCharOneBlank(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * (short)2).TrimStart();                                           // 上記以外
                                        break;
                                    }

                                default:
                                    {
                                        // 最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(0)にセットする
                                        m_strKatagaki_Array[0] = this.PadCharLast(m_strKatagaki, withBlock.p_strCNS_Samakata, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * (short)2).TrimStart();
                                        break;
                                    }
                            }

                            // メンバ変数・方書配列(0)の文字列長＞0　AND
                            // 窓あき宛名文字数・行数指示パラメータ・方書1行あたりの文字数>0　の場合
                            if (m_strKatagaki_Array[0].RLength() > 0 & m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline > 0)
                            {
                                // メンバ変数・方書配列(1)にメンバ変数・方書配列(0)の窓あき宛名文字数・桁数指示パラメータ・方書1行あたりの文字数以降をセットする
                                m_strKatagaki_Array[1] = m_strKatagaki_Array[0].RSubstring(m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline);

                                // メンバ変数・方書配列(0)にメンバ変数・方書配列(0)の先頭から窓あき宛名文字数・桁数指示パラメータ・方書1行あたりの文字数分をセットする
                                m_strKatagaki_Array[0] = m_strKatagaki_Array[0].RSubstring(0, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline);
                            }
                        }

                        // メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnkatagakiOverFlowFG = m_blnOverflowFG;

                        // メンバ変数・方書配列の先頭空白を削除する
                        m_strKatagaki_Array[0] = m_strKatagaki_Array[0].TrimStart();
                        m_strKatagaki_Array[1] = m_strKatagaki_Array[1].TrimStart();
                    }
                }
                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 送付用行政区編集
        // ************************************************************************************************
        // * メソッド名      送付用行政区編集
        // * 
        // * 構文            Private Sub EditSofuGyoseiku()
        // * 
        // * 機能　　        送付用行政区編集方法の指示に従い、送付用行政区を編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditSofuGyoseiku()
        {

            const string THIS_METHOD_NAME = "EditSofuGyoseiku";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // メンバ変数・送付用行政区オーバーフローフラグ、オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = false;
                m_blnSofuGyoseikuOverFlowFG = false;

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    switch (withBlock.p_shtSofuGyoseikuEditPaturn)            // 窓あき宛名編集パラメータ・送付用行政区編集方法
                    {
                        case var @case when @case == withBlock.SofuGyoseikuEditPaturn.Empty:              // 0（空白）の場合
                            {
                                // メンバ変数・送付用行政区は空白とする
                                m_strSofuGyoseiku = string.Empty;
                                break;
                            }
                        case var case1 when case1 == withBlock.SofuGyoseikuEditPaturn.Gyoseiku:           // 1（行政区）の場合
                            {
                                // メンバ変数・送付用行政区は窓あき宛名編集パラメータ・行政区名をセットする
                                m_strSofuGyoseiku = withBlock.p_strGyoseikumei;
                                break;
                            }
                        case var case2 when case2 == withBlock.SofuGyoseikuEditPaturn.GyoseikuKakko:      // 2（行政区括弧）の場合
                            {
                                // 窓あき宛名編集パラメータ・行政区名≠空白の場合
                                if (withBlock.p_strGyoseikumei != string.Empty)
                                {
                                    // メンバ変数・送付用行政区は窓あき宛名編集パラメータ・送付用行政区の括弧(左)+行政区名+送付用行政区の括弧(右)をセットする
                                    m_strSofuGyoseiku = withBlock.p_strSofugyoseiku_KakkoL + withBlock.p_strGyoseikumei + withBlock.p_strSofugyoseiku_KakkoR;
                                }
                                else
                                {
                                    // メンバ変数・送付用行政区は空白とする
                                    m_strSofuGyoseiku = string.Empty;
                                }

                                break;
                            }
                        case var case3 when case3 == withBlock.SofuGyoseikuEditPaturn.GyoseikuCDKakko:    // 3（行政区コード括弧）の場合
                            {
                                // 窓あき宛名編集パラメータ・行政区コード≠空白の場合
                                if (withBlock.p_strGyoseikuCD != string.Empty)
                                {
                                    // メンバ変数・送付用行政区は窓あき宛名編集パラメータ・送付用行政区の括弧(左)+行政区コード+送付用行政区の括弧(右)をセットする
                                    m_strSofuGyoseiku = withBlock.p_strSofugyoseiku_KakkoL + withBlock.p_strGyoseikuCD + withBlock.p_strSofugyoseiku_KakkoR;
                                }
                                else
                                {
                                    // メンバ変数・送付用行政区は空白とする
                                    m_strSofuGyoseiku = string.Empty;
                                }

                                break;
                            }
                    }
                }

                // メンバ変数・送付用行政区の文字列長＞0の場合
                if (m_strSofuGyoseiku.RLength() > 0)
                {
                    // メンバ変数・送付用行政区に文字切れチェック結果(CheckOverflow)の呼出結果をセットする
                    m_strSofuGyoseiku = this.CheckOverflow(m_strSofuGyoseiku, m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength);
                    // メンバ変数・送付用行政区オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                    m_blnSofuGyoseikuOverFlowFG = m_blnOverflowFG;
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 代納人/送付先氏名の編集
        // ************************************************************************************************
        // * メソッド名      代納人/送付先氏名の編集
        // * 
        // * 構文            Private Sub EditDainoShimei()
        // * 
        // * 機能　　        代納人/送付先氏名を指示された行数・文字数に編集し、様編集方法の指示に従い敬称を付与する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditDainoShimei()
        {

            const string THIS_METHOD_NAME = "EditDainoShimei";         // メソッド名

            string strDainoShimei;        // 代納氏名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 窓あき宛名編集パラメータ・代納人/送付先氏名＝空白の場合
                if (m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei == string.Empty)
                {
                    // 変数・代納氏名に窓あき宛名編集パラメータ・本人氏名をセットする
                    strDainoShimei = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei;
                }
                else
                {
                    // 変数・代納氏名に窓あき宛名編集パラメータ・代納人/送付先氏名をセットする
                    strDainoShimei = m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei;
                }

                // メンバ変数・オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = false;

                {
                    ref var withBlock = ref m_cMadoakiAtenaEditParamXClass;
                    // 窓あき宛名文字数・行数指示パラメータ・氏名行数＝1の場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 1)
                    {
                        // メンバ変数・代納人/送付先氏名配列を最大インデックス0で再定義する
                        m_strDaino_Or_SofuShimei_Array = new string[1];

                        // メンバ変数・代納人/送付先氏名フォントに2（大）をセットする
                        m_shtDaino_Or_SofuShimeiFont = 2;

                        switch (withBlock.p_shtSamaEditPaturn)                // 窓あき宛名編集パラメータ・様編集方法
                        {
                            case var @case when @case == withBlock.SamaEditPaturn.Empty:                  // 0（空白）の場合
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[0] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);
                                    break;
                                }
                            case var case1 when case1 == withBlock.SamaEditPaturn.OneBlank:               // 1（1空白）の場合
                                {
                                    // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[0] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);
                                    break;
                                }
                            case var case2 when case2 == withBlock.SamaEditPaturn.Last:                   // 2（末尾）の場合
                                {
                                    // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[0] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);
                                    break;
                                }
                        }

                        // メンバ変数オーバーフローフラグ＝True　AND　
                        // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                        if (m_blnOverflowFG && m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont < m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                        {
                            // メンバ変数・オーバーフローフラグにFalseをセットする
                            m_blnOverflowFG = false;

                            // メンバ変数・代納人/送付先氏名フォントに1（小）をセットする
                            m_shtDaino_Or_SofuShimeiFont = 1;

                            switch (withBlock.p_shtSamaEditPaturn)                // 窓あき宛名編集パラメータ・様編集方法
                            {
                                case var case3 when case3 == withBlock.SamaEditPaturn.Empty:                  // 0（空白）の場合
                                    {
                                        // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                        m_strDaino_Or_SofuShimei_Array[0] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);
                                        break;
                                    }
                                case var case4 when case4 == withBlock.SamaEditPaturn.OneBlank:               // 1（1空白）の場合
                                    {
                                        // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                        m_strDaino_Or_SofuShimei_Array[0] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);
                                        break;
                                    }
                                case var case5 when case5 == withBlock.SamaEditPaturn.Last:                   // 2（末尾）の場合
                                    {
                                        // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                        m_strDaino_Or_SofuShimei_Array[0] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);
                                        break;
                                    }
                            }
                        }

                        // メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG;
                    }

                    // 窓あき宛名文字数・行数指示パラメータ・氏名行数＝2の場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 2)
                    {
                        // メンバ変数・代納人/送付先氏名配列を最大インデックス1で再定義し、空白をセットする
                        m_strDaino_Or_SofuShimei_Array = new string[2];
                        m_strDaino_Or_SofuShimei_Array[0] = string.Empty;
                        m_strDaino_Or_SofuShimei_Array[1] = string.Empty;

                        // メンバ変数・代納人/送付先氏名フォントに2（大）をセットする
                        m_shtDaino_Or_SofuShimeiFont = 2;


                        // フォント大で1段編集が可能か判定を行う
                        switch (withBlock.p_shtSamaEditPaturn)                // 窓あき宛名編集パラメータ・様編集方法
                        {
                            case var case6 when case6 == withBlock.SamaEditPaturn.Empty:                  // 0（空白）の場合
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);
                                    break;
                                }
                            case var case7 when case7 == withBlock.SamaEditPaturn.OneBlank:               // 1（1空白）の場合
                                {
                                    // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);                                   // 2（末尾）の場合
                                    break;
                                }

                            default:
                                {
                                    // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont);
                                    break;
                                }
                        }

                        // メンバ変数・オーバーフローフラグ＝false　の場合
                        if (!m_blnOverflowFG)
                        {
                            // メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG;
                        }
                        else
                        {
                            // メンバ変数・オーバーフローフラグにFalseをセットする
                            m_blnOverflowFG = false;

                            // フォント大で2段編集が可能か判定を行う
                            switch (withBlock.p_shtSamaEditPaturn)                // 窓あき宛名編集パラメータ・様編集方法
                            {
                                case var case8 when case8 == withBlock.SamaEditPaturn.Empty:                  // 0（空白）の場合
                                    {
                                        // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * (short)2);
                                        break;
                                    }
                                case var case9 when case9 == withBlock.SamaEditPaturn.OneBlank:               // 1（1空白）の場合
                                    {
                                        // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * (short)2);                                   // 2（末尾）の場合
                                        break;
                                    }

                                default:
                                    {
                                        // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * (short)2);
                                        break;
                                    }
                            }

                            // メンバ変数・オーバーフローフラグ＝True　AND
                            // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                            if (m_blnOverflowFG && m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont < m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                            {
                            }
                            // 処理なし
                            else
                            {
                                // メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                                m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG;

                                // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                // 引数:①メンバ変数・代納人/送付先氏名配列(1)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント、③氏名行数
                                m_strDaino_Or_SofuShimei_Array = this.CheckOverflow(m_strDaino_Or_SofuShimei_Array[1], m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount);
                            }
                        }
                    }

                    // 窓あき宛名文字数・行数指示パラメータ・氏名行数＝2　AND　メンバ変数・オーバーフローフラグ＝True　AND
                    // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                    if (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 2 && m_blnOverflowFG && m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont < m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                    {
                        // メンバ変数・代納人/送付先氏名フォントに1（小）をセットする
                        m_shtDaino_Or_SofuShimeiFont = 1;

                        // メンバ変数・代納人/送付先氏名配列を各配列に空白をセットする
                        m_strDaino_Or_SofuShimei_Array[0] = string.Empty;
                        m_strDaino_Or_SofuShimei_Array[1] = string.Empty;

                        // フォント小で1段編集が可能か判定を行う
                        switch (withBlock.p_shtSamaEditPaturn)                    // 窓あき宛名編集パラメータ・様編集方法
                        {
                            case var case10 when case10 == withBlock.SamaEditPaturn.Empty:                      // 0（空白）の場合
                                {
                                    // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);
                                    break;
                                }
                            case var case11 when case11 == withBlock.SamaEditPaturn.OneBlank:                   // 1（1空白）の場合
                                {
                                    // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント
                                    // 、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);                                       // 2（末尾）の場合
                                    break;
                                }

                            default:
                                {
                                    // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                    // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント'
                                    // 、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                    m_strDaino_Or_SofuShimei_Array[1] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont);
                                    break;
                                }
                        }

                        // メンバ変数・オーバーフローフラグ＝false　の場合
                        if (!m_blnOverflowFG)
                        {
                            // メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG;
                        }
                        else
                        {
                            // メンバ変数・オーバーフローフラグにFalseをセットする
                            m_blnOverflowFG = false;
                            // フォント小で2段編集が可能か判定を行う
                            switch (withBlock.p_shtSamaEditPaturn)                    // 窓あき宛名編集パラメータ・様編集方法
                            {
                                case var case12 when case12 == withBlock.SamaEditPaturn.Empty:                      // 0（空白）の場合
                                    {
                                        // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * (short)2);
                                        break;
                                    }
                                case var case13 when case13 == withBlock.SamaEditPaturn.OneBlank:                   // 1（1空白）の場合
                                    {
                                        // 全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.PadCharOneBlank(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * (short)2);                                       // 2（末尾）の場合
                                        break;
                                    }

                                default:
                                    {
                                        // 最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                        // 引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                        // ③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                        m_strDaino_Or_SofuShimei_Array[1] = this.PadCharLast(strDainoShimei, withBlock.p_strCNS_Sama, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * (short)2);
                                        break;
                                    }
                            }

                            // メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG;

                            // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                            // 引数:①メンバ変数・代納人/送付先氏名配列(1)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント、③氏名行数
                            m_strDaino_Or_SofuShimei_Array = this.CheckOverflow(m_strDaino_Or_SofuShimei_Array[1], m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount);
                        }
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 本人氏名編集
        // ************************************************************************************************
        // * メソッド名      本人氏名編集
        // * 
        // * 構文            Private Sub EditHonninShimei()
        // * 
        // * 機能　　        本人氏名を指示された行数・文字数に編集する
        // * 
        // * 引数            なし
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        private void EditHonninShimei()
        {

            const string THIS_METHOD_NAME = "EditHonninShimei";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // メンバ変数・オーバーフローフラグにfalseをセットする
                m_blnOverflowFG = false;

                {
                    ref var withBlock = ref m_cMadoakiAtenaLengthParamXClass;
                    // 窓あき宛名文字数・行数指示パラメータの氏名行数が１の場合
                    if (withBlock.p_shtShimeiLineCount == 1)
                    {
                        // メンバ変数・本人氏名配列を最大インデックス0で再定義する
                        m_strHonninShimei_Array = new string[1];

                        // メンバ変数・本人氏名フォントに2（大）をセットする
                        m_shtHonninShimeiFont = 2;

                        // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                        // 引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                        m_strHonninShimei_Array[0] = this.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, withBlock.p_shtShimeiLengthEveryLine_LargeFont);

                        // メンバ変数・オーバーフローフラグ＝True　AND
                        // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                        if (m_blnOverflowFG && withBlock.p_shtShimeiLengthEveryLine_LargeFont < withBlock.p_shtShimeiLengthEveryLine_SmallFont)
                        {
                            // メンバ変数・オーバーフローフラグにfalseをセットする
                            m_blnOverflowFG = false;
                            // メンバ変数・本人氏名フォントに1（小）をセットする
                            m_shtHonninShimeiFont = 1;
                            // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                            // 引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                            m_strHonninShimei_Array[0] = this.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, withBlock.p_shtShimeiLengthEveryLine_SmallFont);
                        }

                        // メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnHonninShimeiOverFlowFG = m_blnOverflowFG;
                    }

                    // 窓あき宛名文字数・行数指示パラメータの氏名行数が2の場合
                    if (withBlock.p_shtShimeiLineCount == 2)
                    {
                        // メンバ変数・本人氏名配列を最大インデックス1で再定義する
                        m_strHonninShimei_Array = new string[2];

                        // メンバ変数・本人氏名フォントに2（大）をセットする
                        m_shtHonninShimeiFont = 2;

                        // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列(0)にセットする
                        // 引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×氏名行数
                        m_strHonninShimei_Array[0] = this.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, withBlock.p_shtShimeiLengthEveryLine_LargeFont * withBlock.p_shtShimeiLineCount);

                        // メンバ変数・オーバーフローフラグ＝False　OR
                        // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント=氏名1行あたりの文字数小フォントの場合
                        if (m_blnOverflowFG == false || withBlock.p_shtShimeiLengthEveryLine_LargeFont == withBlock.p_shtShimeiLengthEveryLine_SmallFont)
                        {
                            // メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnHonninShimeiOverFlowFG = m_blnOverflowFG;

                            // メンバ変数・本人氏名配列(0)の文字列長≦窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォントの場合
                            if (m_strHonninShimei_Array[0].RLength() <= withBlock.p_shtShimeiLengthEveryLine_LargeFont)
                            {
                                // メンバ変数・本人氏名配列(1)にメンバ変数・本人氏名配列(0)をセットする
                                m_strHonninShimei_Array[1] = m_strHonninShimei_Array[0];

                                // メンバ変数・本人氏名配列(0)に空白をセットする
                                m_strHonninShimei_Array[0] = string.Empty;
                            }
                            else
                            {
                                // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                                // 引数:①メンバ変数・本人氏名配列(0)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント、③氏名行数
                                m_strHonninShimei_Array = this.CheckOverflow(m_strHonninShimei_Array[0], withBlock.p_shtShimeiLengthEveryLine_LargeFont, withBlock.p_shtShimeiLineCount);
                            }
                        }
                        else
                        {
                            // メンバ変数・オーバーフローフラグにfalseをセットする
                            m_blnOverflowFG = false;

                            // メンバ変数・本人氏名フォントに1（小）をセットする
                            m_shtHonninShimeiFont = 1;

                            // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列(0)にセットする
                            // 引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×氏名行数
                            m_strHonninShimei_Array[0] = this.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, withBlock.p_shtShimeiLengthEveryLine_SmallFont * withBlock.p_shtShimeiLineCount);

                            // メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnHonninShimeiOverFlowFG = m_blnOverflowFG;

                            // メンバ変数・本人氏名配列(0)の文字列長≦窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォントの場合
                            if (m_strHonninShimei_Array[0].RLength() <= withBlock.p_shtShimeiLengthEveryLine_SmallFont)
                            {
                                // メンバ変数・本人氏名配列(1)にメンバ変数・本人氏名配列(0)をセットする
                                m_strHonninShimei_Array[1] = m_strHonninShimei_Array[0];

                                // メンバ変数・本人氏名配列(0)に空白をセットする
                                m_strHonninShimei_Array[0] = string.Empty;
                            }
                            else
                            {
                                // 文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                                // 引数:①メンバ変数・本人氏名配列(0)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント、③氏名行数
                                m_strHonninShimei_Array = this.CheckOverflow(m_strHonninShimei_Array[0], withBlock.p_shtShimeiLengthEveryLine_SmallFont, withBlock.p_shtShimeiLineCount);
                            }
                        }
                    }


                    // メンバ変数・本人氏名フォント＝2（大）　AND
                    // 窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント=氏名1行あたりの文字数小フォントの場合
                    if (m_shtHonninShimeiFont == 2 && withBlock.p_shtShimeiLengthEveryLine_LargeFont == withBlock.p_shtShimeiLengthEveryLine_SmallFont)
                    {
                        // メンバ変数・本人氏名フォントに1（小）をセットする
                        m_shtHonninShimeiFont = 1;
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

        #region 窓あき宛名編集結果編集
        // ************************************************************************************************
        // * メソッド名      窓あき宛名編集結果編集
        // * 
        // * 構文            Private Sub EditMadoakiAtenaReturn(ByVal cMadoakiAtenaReturnXClass As ABMadoakiAtenaReturnXClass) As ABMadoakiAtenaReturnXClass
        // * 
        // * 機能　　        メンバ変数を窓あき宛名編集結果パラメータにセットする
        // * 
        // * 引数            窓あき宛名編集結果パラメータ
        // * 
        // * 戻り値          窓あき宛名編集結果パラメータ
        // ************************************************************************************************
        private ABMadoakiAtenaReturnXClass EditMadoakiAtenaReturn(ABMadoakiAtenaReturnXClass cMadoakiAtenaReturnXClass)
        {

            const string THIS_METHOD_NAME = "EditMadoakiAtenaReturn";         // メソッド名

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 窓あき宛名編集結果パラメータに編集元メンバ変数をセットする
                cMadoakiAtenaReturnXClass.p_strYubinNo = m_strYubinNO;                                                // 郵便番号
                cMadoakiAtenaReturnXClass.p_strJusho_Array = m_strJusho_Array;                                        // 住所配列
                cMadoakiAtenaReturnXClass.p_strKatagaki_Array = m_strKatagaki_Array;                                  // 方書配列
                cMadoakiAtenaReturnXClass.p_blnJushoOverFlowFG = m_blnJushoOverFlowFG;                                // 住所オーバーフローフラグ
                cMadoakiAtenaReturnXClass.p_blnKatagakiOverFlowFG = m_blnkatagakiOverFlowFG;                          // 方書オーバーフローフラグ
                cMadoakiAtenaReturnXClass.p_strSofuGyoseiku = m_strSofuGyoseiku;                                      // 送付行政区
                cMadoakiAtenaReturnXClass.p_blnSofuGyoseikuOverflowFG = m_blnSofuGyoseikuOverFlowFG;                  // 送付用行政区オーバーフローフラグ
                cMadoakiAtenaReturnXClass.p_strDaino_Or_SofuShimei_Array = m_strDaino_Or_SofuShimei_Array;            // 代納人/送付先氏名配列
                cMadoakiAtenaReturnXClass.p_blnDaino_Or_SofuShimeiOverFlowFG = m_blnDaino_Or_SofuShimeiOverflowFG;    // 代納人/送付先氏名オーバーフローフラグ
                cMadoakiAtenaReturnXClass.p_shtDaino_Or_SofuShimeiFont = m_shtDaino_Or_SofuShimeiFont;                // 代納人/送付先氏名フォント
                cMadoakiAtenaReturnXClass.p_strHonninShimei_Array = m_strHonninShimei_Array;                          // 本人氏名配列
                cMadoakiAtenaReturnXClass.p_blnHonninShimeiOverflowFG = m_blnHonninShimeiOverFlowFG;                  // 本人氏名オーバーフローフラグ
                cMadoakiAtenaReturnXClass.p_shtHonninShimeiFont = m_shtHonninShimeiFont;                              // 本人氏名フォント
                cMadoakiAtenaReturnXClass.p_blnKatagakiran_StaiNusmei_EditFG = m_blnKatagakiran_StaiNusMei_EditFG;    // 方書欄世帯主編集フラグ
                cMadoakiAtenaReturnXClass.p_strSamakata = m_strSamakata;                                              // 様方


                // 代納区分名称
                // メンバ変数・本人氏名フォント＝0（空白）　OR
                // 窓あき宛名編集パラメータ・代納区分名称=空白の場合
                if (m_shtHonninShimeiFont == 0 || m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho == string.Empty)
                {
                    // 窓あき宛名編集結果パラメータ・代納区分名称に空白をセットする
                    cMadoakiAtenaReturnXClass.p_strDainoKBMeisho = string.Empty;
                }
                else
                {
                    // 窓あき宛名編集結果パラメータ・代納区分名称に窓あき宛名編集パラメータ・代納区分名称の括弧(左)　+　
                    // 代納区分名称　+代納区分名称の括弧(右)をセットする
                    cMadoakiAtenaReturnXClass.p_strDainoKBMeisho = m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho_KakkoL + m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho + m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho_KakkoR;
                }

                // 本人氏名括弧上段
                // メンバ変数・本人氏名フォント≠0（空白）　AND
                // 窓あき宛名文字数・行数指示パラメータ・氏名行数=2　And
                // メンバ変数・本人氏名配列(0)の文字列長>0　And
                // メンバ変数・本人氏名配列(1)の文字列長>0　
                if (m_shtHonninShimeiFont != 0 && m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 2 && m_strHonninShimei_Array[0].RLength() > 0 && m_strHonninShimei_Array[1].RLength() > 0)
                {
                    // 窓あき宛名編集結果パラメータ・本人氏名括弧上段に窓あき宛名編集パラメータ・本人氏名の括弧(左)をセットする
                    cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoHigh = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoL;
                }
                else
                {
                    // 窓あき宛名編集結果パラメータ・本人氏名括弧上段に空白をセットする
                    cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoHigh = string.Empty;
                }

                // 本人氏名括弧下段
                // メンバ変数・本人氏名フォント≠0（空白）　AND
                // （窓あき宛名文字数・行数指示パラメータ・氏名行数＝2　AND
                // メンバ変数・本人氏名配列(0)の文字列長＝0　AND
                // メンバ変数・本人氏名配列(1)の文字列長＞0　）　OR
                // （窓あき宛名文字数・行数指示パラメータ・氏名行数＝1　AND
                // メンバ変数・本人氏名配列(0)の文字列長＞0）
                if (m_shtHonninShimeiFont != 0 && m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 2 && m_strHonninShimei_Array[0].RLength() == 0 && m_strHonninShimei_Array[1].RLength() > 0 || m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount == 1 && m_strHonninShimei_Array[0].RLength() > 0)
                {
                    // 窓あき宛名編集結果パラメータ・本人氏名括弧下段に窓あき宛名編集パラメータ・本人氏名の括弧(左)をセットする
                    cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoLow = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoL;
                }
                else
                {
                    // 窓あき宛名編集結果パラメータ・本人氏名括弧下段に空白をセットする
                    cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoLow = string.Empty;
                }

                // 様分
                // 窓あき宛名編集結果パラメータ・本人氏名括弧上段の文字列長＞0　OR
                // 窓あき宛名編集結果パラメータ・本人氏名括弧下段の文字列長>0　
                if (cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoHigh.RLength() > 0 || cMadoakiAtenaReturnXClass.p_strHonninShimei_KakkoLow.RLength() > 0)
                {
                    // 窓あき宛名編集結果パラメータ・様分に窓あき宛名編集パラメータ・様分コンスタント　+　本人氏名の括弧(右)をセットする
                    cMadoakiAtenaReturnXClass.p_strSamabun = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samabun + m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoR;
                }
                else
                {
                    // 窓あき宛名編集結果パラメータ・様分に空白ををセットする
                    cMadoakiAtenaReturnXClass.p_strSamabun = string.Empty;
                }

                // メンバ変数・送付用行政区オーバーフローフラグ　＝True　OR
                // メンバ変数・代納人/送付先氏名オーバーフローフラグ　=True　Or
                // メンバ変数・本人氏名オーバーフローフラグ　=True　Or
                // メンバ変数・住所オーバーフローフラグ　=True　Or
                // メンバ変数・方書オーバーフローフラグ　=True　　の場合
                if (m_blnSofuGyoseikuOverFlowFG || m_blnDaino_Or_SofuShimeiOverflowFG || m_blnHonninShimeiOverFlowFG || m_blnJushoOverFlowFG || m_blnkatagakiOverFlowFG)
                {
                    // メンバ変数・オーバーフローフラグにTrueをセットする
                    m_blnOverflowFG = true;
                }
                else
                {
                    // メンバ変数・オーバーフローフラグにFalseをセットする
                    m_blnOverflowFG = false;
                }

                // 窓あき宛名編集結果パラメータ・オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする

                cMadoakiAtenaReturnXClass.p_blnOverflowFG = m_blnOverflowFG;
                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

            return cMadoakiAtenaReturnXClass;

        }

        #endregion

        #region 全角空白＆文字列付加
        // ************************************************************************************************
        // * メソッド名      全角空白＆文字列付加
        // * 
        // * 構文                Private Function PadCharOneBlank(ByVal strInputChar As String,
        // *                                                      ByVal strPadChar As String,
        // *                                                      ByVal shtMaxLength As Short) As String
        // * 
        // * 機能　　        入力文字列の後ろに全角空白+付加文字を付加する
        // *                  例）入力文字列電算■太郎　、付加文字：様、文字数：10の場合、電算■太郎■様となる（■は全角空白）
        // * 
        // * 引数            入力文字列（String）、付加文字（String）、文字数（Short）
        // * 
        // * 戻り値          編集後文字列(String)
        // ************************************************************************************************
        private string PadCharOneBlank(string strInputChar, string strPadChar, short shtMaxLength)
        {

            const string THIS_METHOD_NAME = "PadCharOneBlank";         // メソッド名

            string strEditInput;          // 入力文字列
            string strFukaMoji;           // 付加文字
            string strOutChar;            // 戻り値

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数・入力文字列、引数・付加文字の後ろ空白を削除し、変数にセットする
                strEditInput = strInputChar.TrimEnd();
                strFukaMoji = strPadChar.TrimEnd();

                // 変数・戻り値に空白をセットする
                strOutChar = string.Empty;

                // メンバ変数・オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = false;

                // 変数・入力文字列の文字列長＞0の場合
                if (strEditInput.RLength() > 0)
                {

                    // 文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                    strOutChar = CheckOverflow(strEditInput, shtMaxLength);

                    // メンバ変数・オーバーフローフラグ＝False　AND　変数・付加文字の文字列長＞0の場合
                    if (m_blnOverflowFG == false && strFukaMoji.RLength() > 0)
                    {

                        // 文字切れチェック（CheckOverflow）を呼び出す
                        CheckOverflow(strEditInput + strFukaMoji, shtMaxLength);

                        // メンバ変数・オーバーフローフラグ＝Trueの場合
                        if (m_blnOverflowFG)
                        {

                            // 文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                            strOutChar = CheckOverflow(strEditInput + "＊", (short)strEditInput.RLength());
                        }
                        // 変数・入力文字列+変数・付加文字の文字列長＝変数・文字数の場合
                        else if ((short)(strEditInput.RLength() + strFukaMoji.RLength()) == shtMaxLength)
                        {
                            // 変数・戻り値に変数・入力文字列+変数・付加文字をセットする
                            strOutChar = strEditInput + strFukaMoji;
                        }
                        else
                        {
                            // 変数・戻り値に変数・入力文字列+全角空白+変数・付加文字をセットする
                            strOutChar = strEditInput + "　" + strFukaMoji;
                        }
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

            return strOutChar;

        }

        #endregion

        #region 最後尾文字列付加
        // ************************************************************************************************
        // * メソッド名      最後尾文字列付加
        // * 
        // * 構文                Private Function PadCharLast(ByVal strInputChar As String,
        // *                                                      ByVal strPadChar As String,
        // *                                                      ByVal shtMaxLength As Short) As String
        // * 
        // * 機能　　        文字数の位置に付加文字を付加して返却する		
        // *                 例）		入力文字列：電算■太郎　、付加文字：様、文字数：10の場合、電算■太郎■■■■様となる（■は全角空白）
        // * 
        // * 引数            入力文字列（String）、付加文字（String）、文字数（Short）
        // * 
        // * 戻り値          編集後文字列(String)
        // ************************************************************************************************
        private string PadCharLast(string strInputChar, string strPadChar, short shtMaxLength)
        {

            const string THIS_METHOD_NAME = "PadCharLast";         // メソッド名

            string strEditInput;          // 入力文字列
            string strFukaMoji;           // 付加文字
            string strOutChar;            // 戻り値

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数・入力文字列、引数・付加文字の後ろ空白を削除し、変数にセットする
                strEditInput = strInputChar.TrimEnd();
                strFukaMoji = strPadChar.TrimEnd();

                // 変数・戻り値に空白をセットする
                strOutChar = string.Empty;

                // メンバ変数・オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = false;

                // 変数・入力文字列の文字列長＞0の場合
                if (strEditInput.RLength() > 0)
                {
                    // 文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                    strOutChar = CheckOverflow(strEditInput, shtMaxLength);

                    // メンバ変数・オーバーフローフラグ＝False　AND　変数・付加文字の文字列長＞0の場合
                    if (m_blnOverflowFG == false && strFukaMoji.RLength() > 0)
                    {
                        // 文字切れチェック（CheckOverflow）を呼び出す
                        CheckOverflow(strEditInput + strFukaMoji, shtMaxLength);

                        // メンバ変数・オーバーフローフラグ＝Trueの場合
                        if (m_blnOverflowFG)
                        {
                            // 文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                            strOutChar = CheckOverflow(strEditInput + "＊", (short)strEditInput.RLength());
                        }
                        else
                        {
                            // 変数・入力文字列の右を引数・文字数分、全角空白埋めを行い、変数・戻り値にセットする
                            // 変数・戻り値の末尾を変数・付加文字に置き換え（変数・戻り値の文字列長-変数・付加文字の文字列長の位置に付加文字を挿入）後ろ空白を削除する
                            strOutChar = strEditInput.RPadRight(shtMaxLength, "　");
                            strOutChar = strOutChar.RInsert(strOutChar.RLength() - strFukaMoji.RLength(), strFukaMoji).TrimEnd();
                        }
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

            return strOutChar;

        }

        #endregion

        #region 文字切れチェック
        // ************************************************************************************************
        // * メソッド名      文字切れチェック
        // * 
        // * 構文            Private Function CheckOverflow(ByVal strInputChar As String,
        // *                                               ByVal shtMaxLength As Short) As String
        // * 
        // * 機能　　        行数１を補完して、文字切れチェックを呼び出す
        // * 
        // * 引数            入力文字列（String）、文字数（Short)
        // * 
        // * 戻り値          文字列（String）
        // ************************************************************************************************
        private string CheckOverflow(string strInputChar, short shtMaxLength)
        {

            const string THIS_METHOD_NAME = "CheckOverflow";         // メソッド名

            string[] strOutChar_Array;
            string strOutChar;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 文字切れチェック（CheckOverflow）を呼び出す。
                // 引数は①引数・入力文字列、②引数・文字数、③1固定
                strOutChar_Array = CheckOverflow(strInputChar, shtMaxLength, 1);

                // 文字切れチェックの戻り値配列の先頭を返却する
                strOutChar = strOutChar_Array[0];

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

            return strOutChar;
        }

        #endregion

        #region 文字切れチェック
        // ************************************************************************************************
        // * メソッド名      文字切れチェック
        // * 
        // * 構文                Private Function CheckOverflow(ByVal strInputChar As String,
        // *                             ByVal shtLengthEveryLine As Short,
        // *                             ByVal shtLineCount As Short) As String()
        // * 
        // * 機能　　        引数・入力文字列を引数・行数分、引数・文字数で分割してセットする
        // * 
        // * 引数            入力文字列（String）、文字数（Short)、行数（Short)
        // * 
        // * 戻り値          文字列配列（String()）
        // ************************************************************************************************
        private string[] CheckOverflow(string strInputChar, short shtLengthEveryLine, short shtLineCount)
        {

            const string THIS_METHOD_NAME = "CheckOverflow";         // メソッド名

            string strEditInput;
            string[] strOutChar_Array;
            int intLine;
            int intStartIndex;
            int intEditLengthCurrentLine;
            int intMaxLength;

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 引数・入力文字列の後ろ空白を削除し、変数へセットする
                strEditInput = strInputChar.TrimEnd();

                // メンバ変数・オーバーフローフラグをFalseにする
                m_blnOverflowFG = false;

                // 引数・行数＜1　OR　引数・文字数＜1　の場合
                if (shtLengthEveryLine < 1 || shtLineCount < 1)
                {
                    // メンバ変数・オーバーフローフラグをTrueにする
                    m_blnOverflowFG = true;

                    // 戻り値配列を最大インデックス0で再定義し、空白をセットする
                    strOutChar_Array = new string[1];
                    strOutChar_Array[0] = string.Empty;
                }
                else
                {
                    // 変数・最大文字数　＝　引数・文字数　×　引数・行数
                    intMaxLength = shtLengthEveryLine * shtLineCount;

                    // 変数・入力文字列の文字列長　＞　変数・最大文字数　の場合
                    if (strEditInput.RLength() > intMaxLength)
                    {
                        // メンバ変数・オーバーフローフラグをTrueにする
                        m_blnOverflowFG = true;
                    }

                    // 戻り値配列を最大インデックス（引数・行数-1）で再定義し、各配列に空白をセットする
                    strOutChar_Array = new string[shtLineCount];
                    var loopTo = strOutChar_Array.Length - 1;
                    for (intLine = 0; intLine <= loopTo; intLine++)
                        strOutChar_Array[intLine] = string.Empty;

                    // メンバ変数・オーバーフローフラグ=true　AND　メンバ変数・オーバーフロー時編集パターン＝1(オーバーバフロー文字置き換え）
                    // And　メンバ変数・オーバーフロー文字の文字列長>0の場合
                    if (m_blnOverflowFG && m_shtEditPaturnWhenOverflow == (short)WhenOverflow.ReplaceOverflowChar && m_strOverflowChar.RLength() > 0)
                    {
                        // 変数・入力文字列の（変数・最大文字数-1）の位置をメンバ変数・オーバーフロー文字の1文字目で置き換える
                        strEditInput = strEditInput.RInsert(intMaxLength - 1, m_strOverflowChar.RSubstring(0, 1));
                        strEditInput = strEditInput.RRemove(strEditInput.RLength() - 1, 1);
                    }

                    // メンバ変数・オーバーフローフラグ=true　AND　メンバ変数・オーバーフロー時編集パターン＝2（空白）の場合
                    if (m_blnOverflowFG && m_shtEditPaturnWhenOverflow == (short)WhenOverflow.Empty)
                    {
                    }
                    // 処理なし
                    else
                    {
                        // 引数・行数分、戻り値配列に変数入力文字列を分割してセットする
                        var loopTo1 = shtLineCount - 1;
                        for (intLine = 0; intLine <= loopTo1; intLine++)
                        {
                            // 開始位置　＝　行カウント　×　文字数
                            intStartIndex = intLine * shtLengthEveryLine;

                            // 編集文字列長　＝　変数・入力文字列の文字列長　-　開始位置
                            intEditLengthCurrentLine = strEditInput.RLength() - intStartIndex;

                            // 編集文字列長　＞　引数・文字数の場合
                            if (intEditLengthCurrentLine > shtLengthEveryLine)
                            {
                                // 編集文字列長に引数・文字数をセットする
                                intEditLengthCurrentLine = shtLengthEveryLine;
                            }

                            // 編集文字列長　＜　1の場合
                            if (intEditLengthCurrentLine < 1)
                            {
                                // 処理終了
                                break;
                            }

                            // 戻り値配列の該当行インデックスに　変数・入力文字列の開始位置から編集文字列長分substringしてセットする
                            strOutChar_Array[intLine] = strEditInput.RSubstring(intStartIndex, intEditLengthCurrentLine);
                        }
                    }
                }

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニング内容:" + objAppExp.Message + "】");
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

            return strOutChar_Array;
        }

        #endregion

    }
}
#endregion
