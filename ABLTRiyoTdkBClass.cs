// ************************************************************************************************
// * 業務名           宛名管理システム
// * 
// * クラス名         ＡＢｅＬＴＡＸ利用届マスタＤＡ(ABLTRiyoTdkBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け           2008/11/10
// *
// * 作成者　　　     比嘉　計成
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2008/11/18   000001     追加処理、更新処理メソッドを追加（比嘉）
// * 2008/11/27   000002     利用届データ取得新メソッドを追加（比嘉）
// * 2009/07/27   000003     利用届出連携機能追加に伴う改修（比嘉）
// * 2009/11/16   000004     検索条件:カナ氏名を検索カナ氏名に修正（比嘉）
// * 2010/02/22   000005     削除処理メソッドを追加（比嘉）
// * 2010/04/16   000006     VS2008対応（比嘉）
// * 2014/08/15   000007     【AB21010】個人番号制度対応 電子申告（岩下）
// * 2015/03/19   000008     【AB21010】個人番号制度対応 電子申告 SQL不具合修正（岩下）
// * 2020/11/06   000009     【AB00189】利用届出複数納税者ID対応（須江）
// * 2024/01/09   000010     【AB-0770-1】利用届出データ管理対応（原野）
// ************************************************************************************************
using System;
// *履歴番号 000009 2020/11/06 追加開始
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Densan.Reams.AB.AB000BB
{
    // *履歴番号 000009 2020/11/06 追加終了

    public class ABLTRiyoTdkBClass
    {

        #region メンバ変数
        // メンバ変数の定義
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigDataClass;        // コンフィグデータ
        private UFRdbClass m_cfRdbClass;                      // ＲＤＢクラス
        private UFLogClass m_cfLogClass;                      // ログ出力クラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private DataSet m_csDataSchma;                        // スキーマ保管用データセット:全項目用
        private DataSet m_csDataSchma_Select;                 // スキーマ保管用データセット:納税者ID,利用者ID

        // *履歴番号 000001 2008/11/17 追加開始
        private string m_strInsertSQL;
        private string m_strUpDateSQL;
        private UFParameterCollectionClass m_cfInsertUFParameterCollectionClass;  // INSERT用パラメータコレクション
        private UFParameterCollectionClass m_cfUpdateUFParameterCollectionClass;  // UPDATE用パラメータコレクション
                                                                                  // *履歴番号 000001 2008/11/17 追加終了
                                                                                  // *履歴番号 000005 2010/02/22 追加開始
        private string m_strDeleteSQL;
        private UFParameterCollectionClass m_cfDeleteUFParameterCollectionClass;  // Delete用パラメータコレクション
                                                                                  // *履歴番号 000005 2010/02/22 追加終了

        // コンスタント定義
        private const string THIS_CLASS_NAME = "ABLTRiyoTdkBClass";
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
        public ABLTRiyoTdkBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigDataClass = cfConfigDataClass;
            m_cfRdbClass = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLogClass = new UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId);

            // SQL文の作成
            // 全項目抽出用スキーマ
            m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, false);
            // 納税者ID、利用者ID用スキーマ
            m_csDataSchma_Select = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT NOZEIID,RIYOSHAID FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, false);

        }
        #endregion

        #region メソッド

        #region eLTAX利用届データ取得メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ取得メソッド
        // * 
        // * 構文         Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
        // * 
        // * 機能　　     利用届出マスタより該当データを取得する。
        // * 
        // * 引数         csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass   : 利用届出パラメータクラス
        // * 
        // * 戻り値       取得した利用届出マスタの該当データ（DataSet）
        // *                 構造：csLtRiyoTdkEntity    
        // ************************************************************************************************
        // *履歴番号 000002 2008/11/27 修正開始
        // Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
        public DataSet GetLTRiyoTdkData(ABLTRiyoTdkParaXClass csABLTRiyoTdkParaX)
        {
            // *履歴番号 000002 2008/11/27 修正終了
            const string THIS_METHOD_NAME = "GetLTRiyoTdkData";
            UFErrorStruct objErrorStruct;                             // エラー定義構造体
            DataSet csLtRiyoTdkEntity;                                // 利用届出マスタデータ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnAndFg = false;                                 // AND判定フラグ

            // 履歴番号 000009 2020/11/06 追加開始
            DataSet csRetLtRiyoTdkEntity;
            DataRow[] csLtRiyoTdkRow;
            var strFilter = default(string);
            var strSort = default(string);
            ABAtenaKanriJohoBClass cABAtenaKanriJohoB;              // 管理情報ビジネスクラス
            string strKanriJoho;
            List<string> csHenkyakuFuyoGyomuCDList;              // 返却不要業務CDリスト
            string strBreakKey;
            DataRow NewDataRow;
            // 履歴番号 000009 2020/11/06 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // パラメータチェック
                if (csABLTRiyoTdkParaX.p_strJuminCD.Trim == string.Empty && csABLTRiyoTdkParaX.p_strZeimokuCD == ABEnumDefine.ZeimokuCDType.Empty)
                {
                    // パラメータ:住民CD、税目CDが設定されていない場合は引数エラー
                    // メッセージ『必須項目が入力されていません。：住民コード､税目コードのいずれかを設定してください。』
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "住民コード､税目コードのいずれかを設定してください。", objErrorStruct.m_strErrorCode);
                }
                else
                {
                }

                // *履歴番号 000009 2020/11/06 追加開始
                if (!(csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "" || csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "1" || csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "2" || csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "3" || csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "4"))



                {
                    // パラメータ:利用区分が未設定、又は"1"～"4"のいずれでもない場合は引数エラー
                    // メッセージ『利用届出利用区分』
                    m_cfErrorClass = new UFErrorClass(THIS_BUSINESSID);
                    // エラー定義を取得
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002);
                    // 例外を生成
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "利用届出利用区分", objErrorStruct.m_strErrorCode);
                }
                // *履歴番号 000009 2020/11/06 追加終了

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                // SELECT句
                // *履歴番号 000009 2020/11/06 修正開始
                // If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                // ' 出力区分が"1"の場合、『納税者ID､利用者ID』を抽出
                // strSQL.Append("SELECT ")
                // strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(", ")
                // strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID)
                // Else
                // ' 出力区分が"1"以外の場合、全項目抽出
                // strSQL.Append("SELECT * ")
                // End If
                // 出力区分が"1"以外の場合、全項目抽出
                strSQL.Append("SELECT * ");
                // *履歴番号 000009 2020/11/06 修正終了

                strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME);

                // WHERE句
                strSQL.Append(" WHERE ");

                // 住民コード
                if (csABLTRiyoTdkParaX.p_strJuminCD.Trim != string.Empty)
                {
                    // 住民コードが設定されている場合
                    strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = csABLTRiyoTdkParaX.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                    // 住民コードが設定されていない場合、何もセットしない
                }

                // 税目コード
                if (csABLTRiyoTdkParaX.p_strZeimokuCD != ABEnumDefine.ZeimokuCDType.Empty)
                {
                    // 税目コードが設定されている場合、抽出条件にする
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)csABLTRiyoTdkParaX.p_strZeimokuCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }

                // 廃止フラグ
                if (blnAndFg == true)
                {
                    // AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ");
                }

                if (csABLTRiyoTdkParaX.p_blnHaishiFG == false)
                {
                    // 廃止区分が"False"の場合、廃止区分が廃止でないものを取得する
                    // * AND (HAISHIFG <> '1' OR HAISHIFG <> '2') AND SAKUJOFG <> '1'
                    strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ");
                    strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2' AND ");
                    strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                }
                else
                {
                    // * AND SAKUJOFG <> '1'
                    strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                // *履歴番号 000009 2020/11/06 修正開始
                // If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                // csLtRiyoTdkEntity = m_csDataSchma_Select.Clone()
                // csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
                // Else
                // csLtRiyoTdkEntity = m_csDataSchma.Clone()
                // csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
                // End If
                // この時点ではcsLtRiyoTdkEntityは全項目とする
                csLtRiyoTdkEntity = m_csDataSchma.Clone();
                csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, false);
                // *履歴番号 000009 2020/11/06 修正終了

                // *履歴番号 000009 2020/11/06 追加開始

                // 管理情報ビジネスクラスのインスタンス化
                cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 管理情報（10-46）を取得
                strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param().Trim();
                csHenkyakuFuyoGyomuCDList = new List<string>(strKanriJoho.Split(','));

                // 一旦優先順位を付けてソートさせてから取捨選択する事からクローンcsRetLtRiyoTdkEntityを作成
                // csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

                if (csABLTRiyoTdkParaX.p_strOutKB == "1")
                {
                    // 出力区分'1'の場合は納税者IDと利用者IDのみ返却するため、2項目のみとする
                    csRetLtRiyoTdkEntity = m_csDataSchma_Select.Clone();
                }
                else
                {
                    csRetLtRiyoTdkEntity = m_csDataSchma.Clone();
                }

                // 管理情報（10-46）に該当する業務CDが設定されているか否かで制御を行う
                if (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) == true)
                {
                    // 該当する業務CDが設定されていた場合（共通納税は返却不要となる）

                    switch (csABLTRiyoTdkParaX.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Empty;
                                break;
                            }

                    }
                }

                else
                {
                    // 該当する業務CDが設定されていない場合

                    switch (csABLTRiyoTdkParaX.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順
                                strFilter = string.Empty;
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし
                                strFilter = string.Empty;
                                strSort = string.Empty;
                                break;
                            }

                    }

                }

                csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort);

                // csRetLtRiyoTdkEntityへのセット
                if (csLtRiyoTdkRow.Length > 0)
                {
                    // 取得件数が0件以上の場合

                    if (csABLTRiyoTdkParaX.p_strOutKB == "1")
                    {
                        // 出力区分'1'の場合は納税者IDと利用者IDのみ返却するため、csRetLtRiyoTdkEntityはその2項目のみセットする

                        if (csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "4")
                        {
                            // 引数：利用区分＝"4"の場合は全件返却する。
                            for (int i = 0, loopTo = csLtRiyoTdkRow.Length - 1; i <= loopTo; i++)
                            {
                                NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow();                     // 追加するデータテーブルの新規行とする
                                NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.NOZEIID);      // 納税者ID
                                NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.RIYOSHAID);  // 利用者ID
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow);                      // 返却用データテーブルに行追加
                            }
                        }
                        else
                        {
                            // 引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                            strBreakKey = "";

                            for (int i = 0, loopTo1 = csLtRiyoTdkRow.Length - 1; i <= loopTo1; i++)
                            {
                                if (strBreakKey != csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString)
                                {
                                    NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow();                     // 追加するデータテーブルの新規行とする
                                    NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.NOZEIID);      // 納税者ID
                                    NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.RIYOSHAID);  // 利用者ID
                                    csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow);                      // 返却用データテーブルに行追加
                                    strBreakKey = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString;
                                }
                            }

                        }
                    }
                    // 出力区分'1'以外の場合はそのままIMPORTする。

                    else if (csABLTRiyoTdkParaX.p_strRiyoKB.Trim == "4")
                    {
                        // 引数：利用区分＝"4"の場合は全件返却する。
                        for (int i = 0, loopTo2 = csLtRiyoTdkRow.Length - 1; i <= loopTo2; i++)
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                    }
                    else
                    {
                        // 引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                        strBreakKey = "";
                        for (int i = 0, loopTo3 = csLtRiyoTdkRow.Length - 1; i <= loopTo3; i++)
                        {
                            if (strBreakKey != csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString)
                            {
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                                strBreakKey = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString;
                            }
                        }
                    }

                }
                // *履歴番号 000009 2020/11/06 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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

            // *履歴番号 000009 2020/11/06 追加開始
            // Return csLtRiyoTdkEntity
            return csRetLtRiyoTdkEntity;
            // *履歴番号 000009 2020/11/06 追加終了

        }
        #endregion

        // *履歴番号 000002 2008/11/27 追加開始
        #region eLTAX利用届データ取得メソッド２
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ取得メソッド２
        // * 
        // * 構文         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass) As DataSet
        // * 
        // * 機能　　     利用届出マスタより該当データを取得する。
        // * 
        // * 引数         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass   : 利用届出パラメータ２クラス
        // * 
        // * 戻り値       取得した利用届出マスタの該当データ（DataSet）
        // *                 構造：csLtRiyoTdkEntity    
        // ************************************************************************************************
        public DataSet GetLTRiyoTdkData(ABLTRiyoTdkPara2XClass cABLTRiyoTdkPara2X)
        {
            const string THIS_METHOD_NAME = "GetLTRiyoTdkData";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000006
            DataSet csLtRiyoTdkEntity;                                // 利用届出マスタデータ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnAndFg = false;                                 // AND判定フラグ

            // 履歴番号 000009 2020/11/06 追加開始
            DataSet csRetLtRiyoTdkEntity;
            DataRow[] csLtRiyoTdkRow;
            var strFilter = default(string);
            var strSort = default(string);
            ABAtenaKanriJohoBClass cABAtenaKanriJohoB;              // 管理情報ビジネスクラス
            string strKanriJoho;
            List<string> csHenkyakuFuyoGyomuCDList;              // 返却不要業務CDリスト
            string strBreakKey;
            // 履歴番号 000009 2020/11/06 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                // SELECT句
                // *履歴番号 000010 2024/01/09 修正開始
                // strSQL.Append("SELECT * ")
                strSQL.Append("SELECT ");
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*");
                // *履歴番号 000010 2024/01/09 修正終了

                strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME);

                // *履歴番号 000010 2024/01/09 追加開始
                if (cABLTRiyoTdkPara2X.p_strMyNumber.Trim != string.Empty)
                {
                    strSQL.Append(" INNER JOIN ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME);
                    strSQL.Append(" ON ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER);
                    strSQL.Append(" = ");
                    strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB);
                    strSQL.Append(" = ");
                    strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG);
                    strSQL.Append(" <> '1'");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strMyNumber;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }
                // *履歴番号 000010 2024/01/09 追加終了

                // WHERE句
                strSQL.Append(" WHERE ");
                // ---------------------------------------------------------------------------------
                // 税目区分
                if (cABLTRiyoTdkPara2X.p_strTaxKB.Trim != string.Empty)
                {
                    // 税目区分が設定されている場合

                    strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strTaxKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 納税者ID
                if (cABLTRiyoTdkPara2X.p_strNozeiID.Trim != string.Empty)
                {
                    // 納税者IDが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_NOZEIID);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 利用者ID
                if (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim != string.Empty)
                {
                    // 利用者IDが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // *履歴番号 000010 2024/01/09 削除開始
                // '*履歴番号 000007 2014/08/15 追加開始
                // '---------------------------------------------------------------------------------
                // ' 個人番号
                // 'If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                // '    住民コードが設定されている場合
                // '    If (blnAndFg = True) Then
                // '        AND判定フラグが"True"の場合、AND句をセット
                // '        strSQL.Append(" AND ")
                // '    End If

                // '    strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
                // '    strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)

                // '    検索条件のパラメータを作成
                // '    cfUFParameterClass = New UFParameterClass
                // '    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
                // '    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

                // '    検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // '    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // '    AND判定フラグをセット
                // '    blnAndFg = True
                // 'Else
                // 'End If
                // '*履歴番号 000007 2014/08/15 追加終了
                // *履歴番号 000010 2024/01/09 削除終了
                // ---------------------------------------------------------------------------------
                // 住民コード
                if (cABLTRiyoTdkPara2X.p_strJuminCD.Trim != string.Empty)
                {
                    // 住民コードが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 受付番号
                if (cABLTRiyoTdkPara2X.p_strRcptNO.Trim != string.Empty)
                {
                    // 受付番号が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTNO);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strRcptNO;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 受付日
                if (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim != string.Empty && cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim != string.Empty)
                {
                    // 受付日が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1");

                    strSQL.Append(" AND ");

                    strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, '0');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, '9');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // *履歴番号 000003 2009/07/27 追加開始
                // 処理日
                if (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim != string.Empty && cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim != string.Empty)
                {
                    // 処理日が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1");

                    strSQL.Append(" AND ");

                    strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ");
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, '0');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, '9');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // カナ・漢字名称
                // カナ名称
                if (!(cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim == string.Empty))
                {
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    if (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") == -1)
                    {
                        // *履歴番号 000004 2009/11/16 修正開始
                        strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO);
                        // strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                        // *履歴番号 000004 2009/11/16 修正終了
                        strSQL.Append(" = ");
                        strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho;
                    }
                    else
                    {
                        // *履歴番号 000004 2009/11/16 修正開始
                        strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO);
                        // strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                        // *履歴番号 000004 2009/11/16 修正終了
                        strSQL.Append(" LIKE ");
                        strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd;
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }

                // 検索用漢字名称
                if (!(cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim == string.Empty))
                {
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    if (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") == -1)
                    {
                        strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO);
                        strSQL.Append(" = ");
                        strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho;
                    }
                    else
                    {
                        strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO);
                        strSQL.Append(" LIKE ");
                        strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd;

                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                // *履歴番号 000003 2009/07/27 追加終了
                // ---------------------------------------------------------------------------------
                // 廃止フラグ
                if (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim != string.Empty)
                {
                    // 廃止フラグが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strSQL.Append(" AND ");
                    }

                    switch (cABLTRiyoTdkPara2X.p_strHaishiFG)
                    {
                        case "0":    // 有効のみ
                            {
                                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ");
                                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'");
                                break;
                            }

                        case "1":    // 廃止のみ
                            {
                                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'");
                                break;
                            }

                        case "2":    // 税目削除のみ
                            {
                                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'");
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 削除フラグ
                if (blnAndFg == true)
                {
                    // AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ");
                    // *履歴番号 000010 2024/01/09 修正開始
                    // strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                    strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                }
                // *履歴番号 000010 2024/01/09 修正終了

                else
                {
                    // *履歴番号 000010 2024/01/09 修正開始
                    // strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                    strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                    // *履歴番号 000010 2024/01/09 修正終了
                }
                // ---------------------------------------------------------------------------------
                // 最大取得件数
                if (cABLTRiyoTdkPara2X.p_intGetCountMax != 0)
                {
                    m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax;
                }
                else
                {
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csLtRiyoTdkEntity = m_csDataSchma.Clone();
                csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // *履歴番号 000009 2020/11/06 追加開始

                // 管理情報ビジネスクラスのインスタンス化
                cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 管理情報（10-46）を取得
                strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param().Trim();
                csHenkyakuFuyoGyomuCDList = new List<string>(strKanriJoho.Split(','));

                csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone();

                if (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) == true)
                {
                    // 管理情報（10-46）に該当する業務CDが設定されていた場合は共通納税は不要

                    switch (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Empty;
                                break;
                            }

                    }
                }

                else
                {
                    // 管理情報（10-46）に該当する業務CDが設定されていない場合

                    switch (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順
                                strFilter = string.Empty;
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし
                                strFilter = string.Empty;
                                strSort = string.Empty;
                                break;
                            }

                    }

                }

                csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort);

                if (csLtRiyoTdkRow.Length > 0)
                {
                    // 取得件数が0件以上の場合
                    if (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim == "4")
                    {
                        // 引数：利用区分＝"4"の場合は全件返却する。
                        for (int i = 0, loopTo = csLtRiyoTdkRow.Length - 1; i <= loopTo; i++)
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                    }
                    else
                    {
                        // csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                        // 引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                        strBreakKey = "";
                        for (int i = 0, loopTo1 = csLtRiyoTdkRow.Length - 1; i <= loopTo1; i++)
                        {
                            if (strBreakKey != csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString)
                            {
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                                strBreakKey = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString;
                            }
                        }
                    }
                }
                // *履歴番号 000009 2020/11/06 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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

            // *履歴番号 000009 2020/11/06 追加開始
            // Return csLtRiyoTdkEntity
            return csRetLtRiyoTdkEntity;
            // *履歴番号 000009 2020/11/06 追加終了

        }
        #endregion
        // *履歴番号 000002 2008/11/27 追加終了

        // *履歴番号 000003 2009/07/27 追加開始
        #region eLTAX利用届データ取得メソッド３
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ取得メソッド３
        // * 
        // * 構文         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass, _
        // *                                                         ByRef intAllCount As Integer) As DataSet
        // * 
        // * 機能　　     利用届出マスタより該当データを取得する。
        // * 
        // * 引数         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass    : 利用届出パラメータ２クラス
        // *              intAllCount As Integer                          : 全データ件数
        // * 
        // * 戻り値       取得した利用届出マスタの該当データ（DataSet）
        // *                 構造：csLtRiyoTdkEntity    
        // ************************************************************************************************
        public DataSet GetLTRiyoTdkData(ABLTRiyoTdkPara2XClass cABLTRiyoTdkPara2X, ref int intAllCount)
        {
            const string THIS_METHOD_NAME = "GetLTRiyoTdkData";
            const string COL_COUNT = "COUNT";
            // * corresponds to VS2008 Start 2010/04/16 000006
            // Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
            // * corresponds to VS2008 End 2010/04/16 000006
            DataSet csLtRiyoTdkEntity;                                // 利用届出マスタデータ
            DataSet csLtRiyoTdk_AllCount;                             // 利用届出マスタ全件取得データ
            var strSQL = new StringBuilder();                                 // SQL文文字列
            var strSQL_Conut = new StringBuilder();                           // 全件抽出
            var strWhere = new StringBuilder();                               // WHERE文文字列
            UFParameterClass cfUFParameterClass;                      // パラメータクラス
            UFParameterCollectionClass cfUFParameterCollectionClass;  // パラメータコレクションクラス
            bool blnAndFg = false;                                 // AND判定フラグ
                                                                   // *履歴番号 000007 2014/08/15 追加開始
            var strSQLMyNumber = new StringBuilder();                         // 共通番号SQL
                                                                              // *履歴番号 000007 2014/08/15 追加終了

            // 履歴番号 000009 2020/11/06 追加開始
            DataSet csRetLtRiyoTdkEntity;
            DataRow[] csLtRiyoTdkRow;
            var strFilter = default(string);
            var strSort = default(string);
            ABAtenaKanriJohoBClass cABAtenaKanriJohoB;              // 管理情報ビジネスクラス
            string strKanriJoho;
            List<string> csHenkyakuFuyoGyomuCDList;              // 返却不要業務CDリスト
            string strBreakKey;
            // 履歴番号 000009 2020/11/06 追加終了

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 検索条件のパラメータコレクションオブジェクトを作成
                cfUFParameterCollectionClass = new UFParameterCollectionClass();

                // SQL文の作成
                // SELECT句
                // *履歴番号 000010 2024/01/09 修正開始
                // strSQL.Append("SELECT * ")
                strSQL.Append("SELECT ");
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*");
                // *履歴番号 000010 2024/01/09 修正終了
                strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME);

                strSQL_Conut.Append("SELECT COUNT(*) AS ").Append(COL_COUNT);
                strSQL_Conut.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME);

                // *履歴番号 000010 2024/01/09 追加開始
                if (cABLTRiyoTdkPara2X.p_strMyNumber.Trim != string.Empty)
                {
                    strSQL.Append(" INNER JOIN ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME);
                    strSQL.Append(" ON ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD);
                    strSQL.Append(" = ");
                    strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER);
                    strSQL.Append(" = ");
                    strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER);
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB);
                    strSQL.Append(" = ");
                    strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'");
                    strSQL.Append(" AND ");
                    strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG);
                    strSQL.Append(" <> '1' ");

                    strSQL_Conut.Append(" INNER JOIN ");
                    strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME);
                    strSQL_Conut.Append(" ON ");
                    strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD);
                    strSQL_Conut.Append(" = ");
                    strSQL_Conut.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD);
                    strSQL_Conut.Append(" AND ");
                    strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER);
                    strSQL_Conut.Append(" = ");
                    strSQL_Conut.Append(ABMyNumberEntity.PARAM_MYNUMBER);
                    strSQL_Conut.Append(" AND ");
                    strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB);
                    strSQL_Conut.Append(" = ");
                    strSQL_Conut.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'");
                    strSQL_Conut.Append(" AND ");
                    strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG);
                    strSQL_Conut.Append(" <> '1' ");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strMyNumber;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);
                }
                else
                {
                }
                // *履歴番号 000010 2024/01/09 追加終了

                // WHERE句
                strWhere.Append(" WHERE ");
                // ---------------------------------------------------------------------------------
                // 税目区分
                if (cABLTRiyoTdkPara2X.p_strTaxKB.Trim != string.Empty)
                {
                    // 税目区分が設定されている場合

                    strWhere.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_TAXKB);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strTaxKB;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 納税者ID
                if (cABLTRiyoTdkPara2X.p_strNozeiID.Trim != string.Empty)
                {
                    // 納税者IDが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_NOZEIID);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 利用者ID
                if (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim != string.Empty)
                {
                    // 利用者IDが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // *履歴番号 000010 2024/01/09 削除開始
                // '*履歴番号 000007 2014/08/15 追加開始
                // '---------------------------------------------------------------------------------
                // ' 個人番号
                // If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                // '*履歴番号 000007 2014/08/15 修正開始
                // '' 住民コードが設定されている場合
                // 'If (blnAndFg = True) Then
                // '    ' AND判定フラグが"True"の場合、AND句をセット
                // '    strSQL.Append(" AND ")
                // 'End If

                // 'strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
                // 'strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
                // ' 個人番号が設定されている場合
                // If (blnAndFg = True) Then
                // ' AND判定フラグが"True"の場合、AND句をセット
                // strWhere.Append(" AND ")
                // End If

                // strWhere.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
                // strWhere.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
                // '*履歴番号 000007 2014/08/15 修正終了

                // ' 検索条件のパラメータを作成
                // cfUFParameterClass = New UFParameterClass
                // cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
                // cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

                // ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                // cfUFParameterCollectionClass.Add(cfUFParameterClass)

                // ' AND判定フラグをセット
                // blnAndFg = True
                // Else
                // End If
                // '*履歴番号 000007 2014/08/15 追加終了
                // *履歴番号 000010 2024/01/09 削除終了
                // ---------------------------------------------------------------------------------
                // 住民コード
                if (cABLTRiyoTdkPara2X.p_strJuminCD.Trim != string.Empty)
                {
                    // 住民コードが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_JUMINCD);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD;
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 受付番号
                if (cABLTRiyoTdkPara2X.p_strRcptNO.Trim != string.Empty)
                {
                    // 受付番号が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTNO);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO;
                    cfUFParameterClass.Value = (string)cABLTRiyoTdkPara2X.p_strRcptNO;

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 受付日
                if (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim != string.Empty && cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim != string.Empty)
                {
                    // 受付日が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1");

                    strWhere.Append(" AND ");

                    strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, '0');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, '9');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // *履歴番号 000003 2009/07/27 追加開始
                // 処理日
                if (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim != string.Empty && cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim != string.Empty)
                {
                    // 処理日が設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1");

                    strWhere.Append(" AND ");

                    strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ");
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2");

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, '0');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // 検索条件のパラメータを作成
                    cfUFParameterClass = new UFParameterClass();
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2";
                    cfUFParameterClass.Value = ((string)cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, '9');

                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // カナ・漢字名称
                // カナ名称
                if (!(cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim == string.Empty))
                {
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    if (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") == -1)
                    {
                        // *履歴番号 000004 2009/11/16 修正開始
                        strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO);
                        // strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                        // *履歴番号 000004 2009/11/16 修正終了
                        strWhere.Append(" = ");
                        strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho;
                    }
                    else
                    {
                        // *履歴番号 000004 2009/11/16 修正開始
                        strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO);
                        // strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                        // *履歴番号 000004 2009/11/16 修正終了
                        strWhere.Append(" LIKE ");
                        strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd;
                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }

                // 検索用漢字名称
                if (!(cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim == string.Empty))
                {
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    if (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") == -1)
                    {
                        strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO);
                        strWhere.Append(" = ");
                        strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho;
                    }
                    else
                    {
                        strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO);
                        strWhere.Append(" LIKE ");
                        strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO);

                        // 検索条件のパラメータを作成
                        cfUFParameterClass = new UFParameterClass();
                        cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO;
                        cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd;

                    }
                    // 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                    cfUFParameterCollectionClass.Add(cfUFParameterClass);

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                // *履歴番号 000003 2009/07/27 追加終了
                // ---------------------------------------------------------------------------------
                // 廃止フラグ
                if (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim != string.Empty)
                {
                    // 廃止フラグが設定されている場合
                    if (blnAndFg == true)
                    {
                        // AND判定フラグが"True"の場合、AND句をセット
                        strWhere.Append(" AND ");
                    }

                    switch (cABLTRiyoTdkPara2X.p_strHaishiFG)
                    {
                        case "0":    // 有効のみ
                            {
                                strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ");
                                strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'");
                                break;
                            }

                        case "1":    // 廃止のみ
                            {
                                strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'");
                                break;
                            }

                        case "2":    // 税目削除のみ
                            {
                                strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'");
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                    // AND判定フラグをセット
                    blnAndFg = true;
                }
                else
                {
                }
                // ---------------------------------------------------------------------------------
                // 削除フラグ
                if (blnAndFg == true)
                {
                    // AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ");
                    // *履歴番号 000010 2024/01/09 修正開始
                    // strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                    strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                }
                // *履歴番号 000010 2024/01/09 修正終了
                else
                {
                    // *履歴番号 000010 2024/01/09 修正開始
                    // strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                    strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'");
                    // *履歴番号 000010 2024/01/09 修正終了
                }
                // ---------------------------------------------------------------------------------
                // 最大取得件数
                if (cABLTRiyoTdkPara2X.p_intGetCountMax != 0)
                {
                    m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax;
                }
                else
                {
                }

                // SQL文結合処理
                strSQL.Append(strWhere.ToString());
                strSQL_Conut.Append(strWhere.ToString());

                // 全件取得処理
                csLtRiyoTdk_AllCount = m_cfRdbClass.GetDataSet(strSQL_Conut.ToString(), cfUFParameterCollectionClass);

                intAllCount = (int)csLtRiyoTdk_AllCount.Tables(0).Rows(0)(COL_COUNT);

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【実行メソッド名:GetDataSet】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), cfUFParameterCollectionClass) + "】");




                // SQLの実行 DataSetの取得
                csLtRiyoTdkEntity = m_csDataSchma.Clone();
                csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, false);

                // *履歴番号 000009 2020/11/06 追加開始

                // 管理情報ビジネスクラスのインスタンス化
                cABAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass);

                // 管理情報（10-46）を取得
                strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param().Trim();
                csHenkyakuFuyoGyomuCDList = new List<string>(strKanriJoho.Split(','));

                csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone();

                if (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) == true)
                {
                    // 管理情報（10-46）に該当する業務CDが設定されていた場合は共通納税は不要

                    switch (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし（ただし、共通納税は除外する）
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Empty;
                                break;
                            }

                    }
                }

                else
                {
                    // 管理情報（10-46）に該当する業務CDが設定されていない場合

                    switch (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim)
                    {

                        case "":
                        case "1":
                            {
                                // 共通＞申告＞共通納税の優先順
                                strFilter = string.Empty;
                                strSort = string.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "2":
                            {
                                // 申告＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "3":
                            {
                                // 共通納税＞共通の優先順
                                strFilter = string.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2");
                                strSort = string.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim);
                                break;
                            }

                        case "4":
                            {
                                // 絞り込みなし
                                strFilter = string.Empty;
                                strSort = string.Empty;
                                break;
                            }

                    }

                }

                csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort);

                if (csLtRiyoTdkRow.Length > 0)
                {
                    // 取得件数が0件以上の場合
                    if (cABLTRiyoTdkPara2X.p_strRiyoKB.Trim == "4")
                    {
                        // 引数：利用区分＝"4"の場合は全件返却する。
                        for (int i = 0, loopTo = csLtRiyoTdkRow.Length - 1; i <= loopTo; i++)
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                    }
                    else
                    {
                        // csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                        // 引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                        strBreakKey = "";
                        for (int i = 0, loopTo1 = csLtRiyoTdkRow.Length - 1; i <= loopTo1; i++)
                        {
                            if (strBreakKey != csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString)
                            {
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow[i]);
                                strBreakKey = csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.JUMINCD).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.TAXKB).ToString + csLtRiyoTdkRow[i].Item(ABLtRiyoTdkEntity.HAISHIFG).ToString;
                            }
                        }
                    }
                }
                // *履歴番号 000009 2020/11/06 追加終了

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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

            // *履歴番号 000009 2020/11/06 追加開始
            // Return csLtRiyoTdkEntity
            return csRetLtRiyoTdkEntity;
            // *履歴番号 000009 2020/11/06 追加終了

        }
        #endregion
        // *履歴番号 000003 2009/07/27 追加終了

        // *履歴番号 000001 2008/11/18 追加開始
        #region eLTAX利用届データ追加メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ追加メソッド
        // * 
        // * 構文         Public Function InsertLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     利用届出マスタに新規データを追加する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       追加件数(Integer)
        // ************************************************************************************************
        public int InsertLTRiyoTdk(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "InsertLTRiyoTdk";                                 // パラメータクラス
                                                                                               // * corresponds to VS2008 Start 2010/04/16 000006
                                                                                               // Dim csDataColumn As DataColumn                                  ' データカラム
                                                                                               // * corresponds to VS2008 End 2010/04/16 000006
            int intInsCnt;                                        // 追加件数
            string strUpdateDateTime;                                 // システム日付

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strInsertSQL is null | string.IsNullOrEmpty(m_strInsertSQL) | m_cfInsertUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 更新日時の取得
                strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");        // 作成日時

                // 共通項目の編集を行う
                csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId;             // 端末ＩＤ
                csDataRow(ABLtRiyoTdkEntity.SAKUJOFG) = "0";                                         // 削除フラグ
                csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = decimal.Zero;                           // 更新カウンタ
                csDataRow(ABLtRiyoTdkEntity.SAKUSEINICHIJI) = strUpdateDateTime;                     // 作成日時
                csDataRow(ABLtRiyoTdkEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId;              // 作成ユーザー
                csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = strUpdateDateTime;                      // 更新日時
                csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId;               // 更新ユーザー


                // パラメータコレクションへ値の設定
                foreach (UFParameterClass cfParam in m_cfInsertUFParameterCollectionClass)
                    this.m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength)).ToString();

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】");




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
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return intInsCnt;

        }
        #endregion

        #region eLTAX利用届データ更新メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ更新メソッド
        // * 
        // * 構文         Public Function UpdateLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     利用届出マスタのデータを更新する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       更新件数(Integer)
        // ************************************************************************************************
        public int UpdateLTRiyoTdk(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "UpdateLTRiyoTdk";                         // パラメータクラス
                                                                                       // * corresponds to VS2008 Start 2010/04/16 000006
                                                                                       // Dim csDataColumn As DataColumn                          ' データカラム
                                                                                       // * corresponds to VS2008 End 2010/04/16 000006
            int intUpdCnt;                                // 更新件数

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // SQLが作成されていなければ作成
                if (m_strUpDateSQL is null | string.IsNullOrEmpty(m_strUpDateSQL) | m_cfUpdateUFParameterCollectionClass is null)
                {
                    CreateSQL(csDataRow);
                }
                else
                {
                }

                // 共通項目の編集を行う
                csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId;                                 // 端末ＩＤ
                csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = (decimal)csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) + 1m;       // 更新カウンタ
                csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff");   // 更新日時
                csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId;                                   // 更新ユーザー

                // 作成済みのパラメータへ更新行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfUpdateUFParameterCollectionClass)
                {
                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) == ABLtRiyoTdkEntity.PREFIX_KEY)
                    {
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                    else
                    {
                        // パラメータコレクションへ値の設定
                        this.m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString();
                    }
                }

                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】");




                // SQLの実行
                intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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

            return intUpdCnt;

        }
        #endregion

        #region  SQL文の作成
        // ************************************************************************************************
        // * メソッド名   SQL文の作成
        // * 
        // * 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
        // * 
        // * 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数         csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値       なし
        // ************************************************************************************************
        private void CreateSQL(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL";
            UFParameterClass cfUFParameterClass;                  // パラメータクラス
            string strInsertColumn;                               // 追加SQL文項目文字列
            string strInsertParam;                                // 追加SQL文パラメータ文字列
            var strWhere = new StringBuilder();                           // 更新削除SQL文Where文文字列

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // INSERT SQL文の作成
                m_strInsertSQL = "INSERT INTO " + ABLtRiyoTdkEntity.TABLE_NAME + " ";
                strInsertColumn = "";
                strInsertParam = "";

                // UPDATE SQL文の作成
                m_strUpDateSQL = "UPDATE " + ABLtRiyoTdkEntity.TABLE_NAME + " SET ";

                // UPDATE Where文作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABLtRiyoTdkEntity.NOZEIID);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.TAXKB);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER);

                // SELECT パラメータコレクションクラスのインスタンス化
                m_cfInsertUFParameterCollectionClass = new UFParameterCollectionClass();

                // UPDATE パラメータコレクションのインスタンス化
                m_cfUpdateUFParameterCollectionClass = new UFParameterCollectionClass();

                // パラメータコレクションの作成
                foreach (DataColumn csDataColumn in csDataRow.Table.Columns)
                {
                    cfUFParameterClass = new UFParameterClass();

                    // INSERT SQL文の作成
                    strInsertColumn += csDataColumn.ColumnName + ", ";
                    strInsertParam += ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // UPDATE SQL文の作成
                    m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", ";

                    // INSERT コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass);

                    // UPDATE コレクションにパラメータを追加
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName;
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                }

                // INSERT SQL文のトリミング
                strInsertColumn = strInsertColumn.Trim();
                strInsertColumn = strInsertColumn.Trim(",");
                strInsertParam = strInsertParam.Trim();
                strInsertParam = strInsertParam.Trim(",");
                m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")";

                // UPDATE SQL文のトリミング
                m_strUpDateSQL = m_strUpDateSQL.Trim();
                m_strUpDateSQL = m_strUpDateSQL.Trim(",");

                // UPDATE SQL文にWHERE句の追加
                m_strUpDateSQL += strWhere.ToString();

                // UPDATE コレクションにキー情報を追加
                // 納税者ID
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付市町村ｺｰﾄﾞ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 税目区分
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER;
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
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
        // *履歴番号 000001 2008/11/18 追加終了

        // *履歴番号 000005 2010/02/22 追加開始
        #region eLTAX利用届データ削除(物理)メソッド
        // ************************************************************************************************
        // * メソッド名   eLTAX利用届データ削除(物理)メソッド
        // * 
        // * 構文         Public Function DeleteLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        // * 
        // * 機能　　     利用届出マスタから該当データを物理削除する。
        // * 
        // * 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
        // * 
        // * 戻り値       削除件数(Integer)
        // ************************************************************************************************
        public int DeleteLTRiyoTdk(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "DeleteLTRiyoTdk";                                 // パラメータクラス
                                                                                               // * corresponds to VS2008 Start 2010/04/16 000006
                                                                                               // Dim csDataColumn As DataColumn                                  ' データカラム
                                                                                               // * corresponds to VS2008 End 2010/04/16 000006
            int intDelCnt;                                        // 削除件数
                                                                  // * corresponds to VS2008 Start 2010/04/16 000006
                                                                  // Dim strUpdateDateTime As String                                 ' システム日付
                                                                  // * corresponds to VS2008 End 2010/04/16 000006

            try
            {
                // デバッグログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
                if (m_strDeleteSQL is null || string.IsNullOrEmpty(m_strDeleteSQL) || m_cfDeleteUFParameterCollectionClass == null)
                {
                    CreateSQL_Delete(csDataRow);
                }
                else
                {
                }

                // 作成済みのパラメータへ削除行から値を設定する。
                foreach (UFParameterClass cfParam in m_cfDeleteUFParameterCollectionClass)
                {

                    // キー項目は更新前の値で設定
                    if (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) == ABLtRiyoTdkEntity.PREFIX_KEY)
                    {
                        this.m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString();
                    }
                }


                // RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, "【クラス名:" + GetType().Name + "】" + "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "】" + "【実行メソッド名:ExecuteSQL】" + "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】");




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
                throw;
            }

            catch (Exception exException) // システムエラーをキャッチ
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + exException.Message + "】");


                // システムエラーをスローする
                throw;

            }

            return intDelCnt;

        }
        #endregion

        #region SQL文作成(物理削除)
        // ************************************************************************************************
        // * メソッド名     物理削除用SQL文の作成
        // * 
        // * 構文           Private Sub CreateSQL_Delete(ByVal csDataRow As DataRow)
        // * 
        // * 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
        // * 
        // * 引数           csDataRow As DataRow : 更新対象の行
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void CreateSQL_Delete(DataRow csDataRow)
        {
            const string THIS_METHOD_NAME = "CreateSQL_Delete";
            UFParameterClass cfUFParameterClass;              // パラメータクラス
            var strWhere = new StringBuilder();                       // WHERE定義

            try
            {
                // デバッグ開始ログ出力
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // WHERE文の作成
                strWhere.Append(" WHERE ");
                strWhere.Append(ABLtRiyoTdkEntity.NOZEIID);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.TAXKB);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB);
                strWhere.Append(" AND ");
                strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER);
                strWhere.Append(" = ");
                strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER);

                // 物理DELETE SQL文の作成
                m_strDeleteSQL = "DELETE FROM " + ABLtRiyoTdkEntity.TABLE_NAME + strWhere.ToString();

                // 物理削除用パラメータコレクションのインスタンス化
                m_cfDeleteUFParameterCollectionClass = new UFParameterCollectionClass();

                // 物理削除用コレクションにパラメータを追加
                // 納税者ID
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 受付市町村ｺｰﾄﾞ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 税目区分
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);
                // 更新カウンタ
                cfUFParameterClass = new UFParameterClass();
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER;
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass);

                // デバッグ終了ログ出力
                m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw;
            }

        }
        #endregion
        // *履歴番号 000005 2010/02/22 追加終了

        #endregion

    }
}