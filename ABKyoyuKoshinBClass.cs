// ************************************************************************************************
// * 業務名          宛名システム
// * 
// * クラス名        共有更新Ｂ(ABKyoyuKoshinBClass)
// * 
// * バージョン情報   Ver 1.0
// * 
// * 日付け　作成者   2003/06/06　滝沢　欽也
// *
// * 著作権          （株）電算
// ************************************************************************************************
// * 修正履歴　　履歴番号　　修正内容
// * 2004/05/17  000001      共有更新処理メソッドに異動日時を引数として追加
// * 2004/05/17  000002      直近異動年月日に異動日時を格納に修正
// * 2006/03/27  000003      ワークフロー連携メソッド追加
// * 2006/05/31  000004      累積更新時に異動前データも追加する
// * 2006/09/13  000005      更新方法を変更する
// *                         履歴データの開始～終了に引数の異動年月日が当てはまったデータ以降を
// *                         引数のcABJutogaiの内容で更新する。但し開始・終了・異動年月日は除く
// *                         当てはまったデータが直近の場合は通常通り分割する(マルゴ村山)
// * 2007/10/11  000006      宛名編集処理の未使用クラス(UR管理情報キャッシュクラス)を削除する（比嘉）
// * 2010/04/16  000007      VS2008対応（比嘉）
// * 2014/12/24  000008      【AB21080】中間サーバーＢＳ連携機能追加（石合）
// * 2015/01/08  000009      【AB21080】中間サーバーＢＳ連携機能削除（石合）
// ************************************************************************************************
using System;
using System.Linq;
using System.Data;
using ndensan.framework.uf.publicmodule.library.businesscommon.ufcommon;
using ndensan.framework.uf.publicmodule.library.businesscommon.uftools;
using ndensan.framework.us.publicmodule.library.businesscommon.uscommon;

namespace ndensan.reams.ab.publicmodule.library.business.ab000b
{

    public class ABKyoyuKoshinBClass
    {

        // **
        // * クラスID定義
        // * 
        private const string THIS_CLASS_NAME = "ABKyoyuKoshinBClass";

        // **
        // * メンバ変数
        // *  
        private UFControlData m_cfControlData;                // コントロールデータ
        private UFConfigDataClass m_cfConfigData;             // 環境情報データクラス
        private UFLogClass m_cfLog;                           // ログ出力クラス
        private UFRdbClass m_cfRdb;                           // RDBクラス
        private UFErrorClass m_cfErrorClass;                  // エラー処理クラス
        private UFDateClass m_cfDateClass;                    // 日付クラス
        private ABNyuryokuParaXClass m_cNyuryokuParaX;        // 入力画面パラメータ
        private ABCommonClass m_cCommonClass = new ABCommonClass();           // Commonクラス

        // ************************************************************************************************
        // * メソッド名      コンストラクタ
        // * 
        // * 構文           Public Sub New(ByVal cfControlData As UFControlData, 
        // * 　　                          ByVal cfConfigDataClass As UFConfigDataClass
        // * 　　                          ByVal csUFRdbClass As UFRdbClass)
        // * 
        // * 機能　　        初期化処理
        // * 
        // * 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
        // * 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
        // * 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
        // * 
        // * 戻り値          なし
        // ************************************************************************************************
        public ABKyoyuKoshinBClass(UFControlData cfControlData, UFConfigDataClass cfConfigDataClass, UFRdbClass cfRdbClass)

        {
            // メンバ変数セット
            m_cfControlData = cfControlData;
            m_cfConfigData = cfConfigDataClass;
            m_cfRdb = cfRdbClass;

            // ログ出力クラスのインスタンス化
            m_cfLog = new UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId);

            // 日付クラスのインスタンス化
            m_cfDateClass = new UFDateClass(m_cfConfigData);
        }


        // ************************************************************************************************
        // * メソッド名     共有更新処理
        // * 
        // * 構文           Public Function UpdateKyoyu(ByVal StrJuminCD As String, _
        // *                        　                  ByVal IntKoshinKB As Integer, _
        // *                                            ByVal cABJutogai As DataSet) As Integer
        // * 
        // * 機能　　       共有データの追加を行なう。
        // * 
        // * 引数           StrJuminCD As String           : 住民コード
        // *                IntKoshinKB As Intege          : 更新区分
        // * 　　　         cABJutogai As DataSet          : 住登外Entity
        // * 
        // * 戻り値         件数
        // ************************************************************************************************
        public int UpdateKyoyu(string StrJuminCD, int IntKoshinKB, string StrIdoYMD, DataSet cABJutogai)


        {

            const string THIS_METHOD_NAME = "UpdateKyoyu";    // メソッド名
            UFErrorStruct objErrorStruct;                 // エラー定義構造体
            ABJutogaiBClass cJutogaiB;                    // 住登外ＤＡ
            DataSet csJutogaiEntity;                      // 住登外DataSet
            DataRow csJutogaiRow;                         // 住登外Row
            DataRow csJutogaiRowN;
            ABAtenaBClass cAtenaB;                        // 宛名ＤＡ
            DataSet csAtenaEntity;                        // 宛名Entity
            var cAtenaSearchKey = new ABAtenaSearchKey();       // 宛名検索キー
            ABAtenaRirekiBClass cAtenaRirekiB;            // 宛名履歴ＤＡ
            DataSet csAtenaRirekiEntity;                  // 宛名履歴Entity
            ABAtenaRuisekiBClass cAtenaRuisekiB;          // 宛名累積ＤＡ
            DataSet csAtenaRuisekiEntity;                 // 宛名累積Entity
            int intUpdataCount;                       // 更新件数
            string strSystemDate;                         // システム日付
            DataRow csDataRow;
            // * corresponds to VS2008 Start 2010/04/16 000007
            var cABEnumDefine = new ABEnumDefine();
            // Dim csColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000007
            // *履歴番号 000003 2006/03/27 追加開始
            string strKoshinKB;                           // 更新区分
                                                          // *履歴番号 000003 2006/03/27 追加終了
                                                          // *履歴番号 000005 2006/09/13 追加開始
            DataRow[] csRirekiRows;
            DataRow csRirekiCkinRow;
            // *履歴番号 000005 2006/09/13 追加終了
            // * 履歴番号 000009 2015/01/08 削除開始
            // '*履歴番号 000008 2014/12/24 追加開始
            // Dim cABBSRenkeiB As ABBSRenkeiBClass                ' 中間サーバーＢＳ連携ビジネスクラス
            // '*履歴番号 000008 2014/12/24 追加終了
            // * 履歴番号 000009 2015/01/08 削除終了

            try
            {

                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 住登外ＤＡクラスのインスタンス化
                cJutogaiB = new ABJutogaiBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                // 宛名ＤＡクラスのインスタンス化
                cAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                // 宛名履歴ＤＡクラスのインスタンス化
                cAtenaRirekiB = new ABAtenaRirekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                // 宛名累積ＤＡクラスのインスタンス化
                cAtenaRuisekiB = new ABAtenaRuisekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb);

                // システム日付の取得
                strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd");


                // 住登外マスタの追加を行なう
                if (IntKoshinKB == cABEnumDefine.KoshinKB.Insert)
                {
                    intUpdataCount = cJutogaiB.InsertJutogaiB(cABJutogai.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0]);
                }
                else
                {
                    csJutogaiEntity = cJutogaiB.GetJutogaiBHoshu(StrJuminCD);

                    // 住登外データが存在しない場合、エラーを発生する
                    if (csJutogaiEntity.Tables[ABJutogaiEntity.TABLE_NAME].Rows.Count == 0)
                    {
                        m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        // エラー定義を取得（更新対象のデータが存在しません。：住登外）
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "住登外", objErrorStruct.m_strErrorCode);
                    }

                    csJutogaiRow = csJutogaiEntity.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0];
                    csJutogaiRowN = cABJutogai.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0];

                    csJutogaiRow.BeginEdit();

                    // For Each csColumn In csJutogaiRow.Table.Columns
                    // csJutogaiRow[csColumn.ColumnName] = csJutogaiRowN[csColumn]
                    // Next csColumn
                    // csJutogaiRow = csJutogaiRowN
                    // 住登外編集処理
                    EditJutogai(ref csJutogaiRow, csJutogaiRowN);

                    csJutogaiRow.EndEdit();

                    intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiEntity.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0]);
                    // intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiRow)
                }

                // 更新件数が１件以外の場合、エラーを発生させる
                if (!(intUpdataCount == 1))
                {
                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                    // エラー定義を取得（既に同一データが存在します。：住登外）
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "住登外", objErrorStruct.m_strErrorCode);
                }

                // **
                // * 宛名マスタ更新処理
                // *
                cAtenaSearchKey.p_strJuminCD = StrJuminCD;

                // 宛名編集処理
                // 新規作成の場合
                if (IntKoshinKB == cABEnumDefine.KoshinKB.Insert)
                {
                    csAtenaEntity = m_cfRdb.GetTableSchema(ABAtenaEntity.TABLE_NAME);
                }
                else
                {
                    // 宛名マスタを取得する
                    // 宛名ＤＡクラスのインスタンス化
                    cAtenaB = new ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                    csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey);
                }

                EditAtenaJutogai(IntKoshinKB, StrIdoYMD, cABJutogai, ref csAtenaEntity);

                foreach (DataRow currentCsDataRow in csAtenaEntity.Tables[ABAtenaEntity.TABLE_NAME].Rows)
                {
                    csDataRow = currentCsDataRow;

                    if (IntKoshinKB == cABEnumDefine.KoshinKB.Insert)
                    {
                        // 宛名マスタの追加を行なう
                        intUpdataCount = cAtenaB.InsertAtenaB(csDataRow);
                    }
                    else
                    {
                        // 宛名マスタの更新を行なう
                        intUpdataCount = cAtenaB.UpdateAtenaB(csDataRow);
                    }

                    // 更新件数が１件以外の場合、エラーを発生させる
                    if (!(intUpdataCount == 1))
                    {
                        m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                        // エラー定義を取得（既に同一データが存在します。：宛名）
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                        throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode);
                    }
                }

                // **
                // * 宛名履歴マスタ更新処理
                // *

                // 宛名履歴マスタを取得する
                // *履歴番号 000005 2006/09/13 修正開始
                // 直近だけでなく全件取得する
                // 'csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "99999999", True)
                csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "", true);

                // 直近ロウを退避しておく
                csRirekiRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'");
                if (csRirekiRows.Length > 0)
                {
                    csRirekiCkinRow = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].NewRow();
                    csRirekiCkinRow.ItemArray = csRirekiRows[0].ItemArray;
                }
                else
                {
                    csRirekiCkinRow = default;
                }
                // *履歴番号 000005 2006/09/13 修正終了

                // 宛名履歴編集処理
                EditAtenaRireki(StrIdoYMD, csAtenaEntity, ref csAtenaRirekiEntity);

                // 宛名履歴マスタの追加を行なう
                foreach (DataRow currentCsDataRow1 in csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows)
                {
                    csDataRow = currentCsDataRow1;
                    switch (csDataRow.RowState)
                    {
                        case var @case when @case == DataRowState.Added:
                            {
                                intUpdataCount = cAtenaRirekiB.InsertAtenaRB(csDataRow);

                                // 更新件数が１件以外の場合、エラーを発生させる
                                if (!(intUpdataCount == 1))
                                {
                                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                    // エラー定義を取得（既に同一データが存在します。：宛名履歴）
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                }

                                break;
                            }
                        case var case1 when case1 == DataRowState.Modified:
                            {
                                intUpdataCount = cAtenaRirekiB.UpdateAtenaRB(csDataRow);
                                // 更新件数が１件以外の場合、エラーを発生させる
                                if (!(intUpdataCount == 1))
                                {
                                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                    // エラー定義を取得（更新対象のデータが存在しません。：宛名履歴）
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode);
                                }

                                break;
                            }
                    }
                }

                // **
                // * 宛名累積マスタ更新処理
                // *

                // 宛名累積マスタを取得する
                csAtenaRuisekiEntity = m_cfRdb.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME);

                // 宛名累積編集処理
                // *履歴番号 000005 2006/09/13 修正開始
                // 退避しておいた操作前の直近レコードを引数に加える
                // 'Me.EditAtenaRuiseki(csAtenaRirekiEntity, csAtenaRuisekiEntity)
                EditAtenaRuiseki(csAtenaRirekiEntity, ref csAtenaRuisekiEntity, csRirekiCkinRow);
                // *履歴番号 000005 2006/09/13 修正終了

                // 宛名累積マスタの追加を行なう
                foreach (DataRow currentCsDataRow2 in csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows)
                {
                    csDataRow = currentCsDataRow2;
                    switch (csDataRow.RowState)
                    {
                        case var case2 when case2 == DataRowState.Added:
                            {
                                intUpdataCount = cAtenaRuisekiB.InsertAtenaRB(csDataRow);

                                // 更新件数が１件以外の場合、エラーを発生させる
                                if (!(intUpdataCount == 1))
                                {
                                    m_cfErrorClass = new UFErrorClass(ABConstClass.THIS_BUSINESSID);
                                    // エラー定義を取得（既に同一データが存在します。：宛名累積）
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044);
                                    throw new UFAppException(objErrorStruct.m_strErrorMessage + "宛名累積", objErrorStruct.m_strErrorCode);
                                }

                                break;
                            }
                    }

                }

                // *履歴番号 000003 2006/03/27 追加開始
                // 処理区分を資産税更新用からワークフロー連携用に修正する
                switch (IntKoshinKB)
                {
                    case var case3 when case3 == cABEnumDefine.KoshinKB.Insert:
                        {
                            strKoshinKB = "1";
                            break;
                        }
                    case var case4 when case4 == cABEnumDefine.KoshinKB.Update:
                        {
                            strKoshinKB = "2";
                            break;
                        }
                }
                // ワークフロー連携処理の呼び出し
                AtenaDataReplicaKoshin((string)cABJutogai.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0][ABJutogaiEntity.JUMINCD], (string)cABJutogai.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0][ABJutogaiEntity.STAICD], IntKoshinKB.ToString());
                // *履歴番号 000003 2006/03/27 追加終了

                // * 履歴番号 000009 2015/01/08 削除開始
                // '*履歴番号 000008 2014/12/24 追加開始
                // ' 中間サーバーＢＳ連携ビジネスクラスのインスタンス化
                // cABBSRenkeiB = New ABBSRenkeiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

                // ' 中間サーバーＢＳ連携の実行
                // cABBSRenkeiB.ExecRenkei(StrJuminCD)
                // '*履歴番号 000008 2014/12/24 追加終了
                // * 履歴番号 000009 2015/01/08 削除終了

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFRdbDeadLockException objRdbDeadLockExp)   // デッドロックをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbDeadLockExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(objRdbDeadLockExp.Message, objRdbDeadLockExp.p_intErrorCode, objRdbDeadLockExp);
            }

            catch (UFRdbUniqueException objUFRdbUniqueExp)     // 一意制約違反をキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(objUFRdbUniqueExp.Message, objUFRdbUniqueExp.p_intErrorCode, objUFRdbUniqueExp);
            }

            catch (UFRdbTimeOutException objRdbTimeOutExp)    // UFRdbTimeOutExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + "【ワーニング内容:" + objRdbTimeOutExp.Message + "】");



                // UFAppExceptionをスローする
                throw new UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp);
            }

            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

            return intUpdataCount;

        }

        // ************************************************************************************************
        // * メソッド名     住登外編集処理
        // * 
        // * 構文           Public Sub EditJutogai(ByVal cfControlData As UFControlData,
        // * 　　                               ByVal cNyuryokuParaX As ABNyuryokuParaXClass) As DataSet
        // * 
        // * 機能　　       入力画面データより住登外Entityを追加・編集する
        // * 
        // * 引数           csJutogaiEntity As DataSet              : 住登外Entity
        // * 　　           cNyuryokuParaX As ABNyuryokuParaXClass  : 個人入力データ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void EditJutogai(ref DataRow csJutogaiRow, DataRow csJutogaiRowN)
        {
            const string THIS_METHOD_NAME = "EditJutogai";    // メソッド名
            var cABJutogaiIF = new ABJutogaiEntity();                   // 住登外マスタコンストクラス

            try
            {
                // **
                // * 編集処理
                // *
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 市町村コード
                csJutogaiRow[cABJutogaiIF.SHICHOSONCD] = csJutogaiRowN[cABJutogaiIF.SHICHOSONCD];
                // 旧市町村コード
                csJutogaiRow[cABJutogaiIF.KYUSHICHOSONCD] = csJutogaiRowN[cABJutogaiIF.KYUSHICHOSONCD];
                // 世帯コード
                csJutogaiRow[cABJutogaiIF.STAICD] = csJutogaiRowN[cABJutogaiIF.STAICD];
                // 宛名データ区分
                csJutogaiRow[cABJutogaiIF.ATENADATAKB] = csJutogaiRowN[cABJutogaiIF.ATENADATAKB];
                // 宛名データ種別
                csJutogaiRow[cABJutogaiIF.ATENADATASHU] = csJutogaiRowN[cABJutogaiIF.ATENADATASHU];
                // 検索用カナ姓名
                csJutogaiRow[cABJutogaiIF.SEARCHKANASEIMEI] = csJutogaiRowN[cABJutogaiIF.SEARCHKANASEIMEI];
                // 検索用カナ姓
                csJutogaiRow[cABJutogaiIF.SEARCHKANASEI] = csJutogaiRowN[cABJutogaiIF.SEARCHKANASEI];
                // 検索用カナ名
                csJutogaiRow[cABJutogaiIF.SEARCHKANAMEI] = csJutogaiRowN[cABJutogaiIF.SEARCHKANAMEI];
                // カナ名称1
                csJutogaiRow[cABJutogaiIF.KANAMEISHO1] = csJutogaiRowN[cABJutogaiIF.KANAMEISHO1];
                // 漢字名称1
                csJutogaiRow[cABJutogaiIF.KANJIMEISHO1] = csJutogaiRowN[cABJutogaiIF.KANJIMEISHO1];
                // カナ名称2
                csJutogaiRow[cABJutogaiIF.KANAMEISHO2] = csJutogaiRowN[cABJutogaiIF.KANAMEISHO2];
                // 漢字名称2
                csJutogaiRow[cABJutogaiIF.KANJIMEISHO2] = csJutogaiRowN[cABJutogaiIF.KANJIMEISHO2];
                // 生年月日
                csJutogaiRow[cABJutogaiIF.UMAREYMD] = csJutogaiRowN[cABJutogaiIF.UMAREYMD];
                // 生和暦年月日
                csJutogaiRow[cABJutogaiIF.UMAREWMD] = csJutogaiRowN[cABJutogaiIF.UMAREWMD];
                // 性別コード
                csJutogaiRow[cABJutogaiIF.SEIBETSUCD] = csJutogaiRowN[cABJutogaiIF.SEIBETSUCD];
                // 性別
                csJutogaiRow[cABJutogaiIF.SEIBETSU] = csJutogaiRowN[cABJutogaiIF.SEIBETSU];
                // 続柄コード
                csJutogaiRow[cABJutogaiIF.ZOKUGARACD] = csJutogaiRowN[cABJutogaiIF.ZOKUGARACD];
                // 続柄
                csJutogaiRow[cABJutogaiIF.ZOKUGARA] = csJutogaiRowN[cABJutogaiIF.ZOKUGARA];
                // 第2続柄コード
                csJutogaiRow[cABJutogaiIF.DAI2ZOKUGARACD] = csJutogaiRowN[cABJutogaiIF.DAI2ZOKUGARACD];
                // 第2続柄
                csJutogaiRow[cABJutogaiIF.DAI2ZOKUGARA] = csJutogaiRowN[cABJutogaiIF.DAI2ZOKUGARA];
                // 漢字法人代表者氏名
                csJutogaiRow[cABJutogaiIF.KANJIHJNDAIHYOSHSHIMEI] = csJutogaiRowN[cABJutogaiIF.KANJIHJNDAIHYOSHSHIMEI];
                // 汎用区分1
                csJutogaiRow[cABJutogaiIF.HANYOKB1] = csJutogaiRowN[cABJutogaiIF.HANYOKB1];
                // 漢字法人形態
                csJutogaiRow[cABJutogaiIF.KANJIHJNKEITAI] = csJutogaiRowN[cABJutogaiIF.KANJIHJNKEITAI];
                // 個人法人区分
                csJutogaiRow[cABJutogaiIF.KJNHJNKB] = csJutogaiRowN[cABJutogaiIF.KJNHJNKB];
                // 汎用区分2
                csJutogaiRow[cABJutogaiIF.HANYOKB2] = csJutogaiRowN[cABJutogaiIF.HANYOKB2];
                // 管内管外区分
                csJutogaiRow[cABJutogaiIF.KANNAIKANGAIKB] = csJutogaiRowN[cABJutogaiIF.KANNAIKANGAIKB];
                // 家屋敷区分
                csJutogaiRow[cABJutogaiIF.KAOKUSHIKIKB] = csJutogaiRowN[cABJutogaiIF.KAOKUSHIKIKB];
                // 備考税目
                csJutogaiRow[cABJutogaiIF.BIKOZEIMOKU] = csJutogaiRowN[cABJutogaiIF.BIKOZEIMOKU];
                // 郵便番号
                csJutogaiRow[cABJutogaiIF.YUBINNO] = csJutogaiRowN[cABJutogaiIF.YUBINNO];
                // 住所コード
                csJutogaiRow[cABJutogaiIF.JUSHOCD] = csJutogaiRowN[cABJutogaiIF.JUSHOCD];
                // 住所
                csJutogaiRow[cABJutogaiIF.JUSHO] = csJutogaiRowN[cABJutogaiIF.JUSHO];
                // 番地コード1
                csJutogaiRow[cABJutogaiIF.BANCHICD1] = csJutogaiRowN[cABJutogaiIF.BANCHICD1];
                // 番地コード2
                csJutogaiRow[cABJutogaiIF.BANCHICD2] = csJutogaiRowN[cABJutogaiIF.BANCHICD2];
                // 番地コード3
                csJutogaiRow[cABJutogaiIF.BANCHICD3] = csJutogaiRowN[cABJutogaiIF.BANCHICD3];
                // 番地
                csJutogaiRow[cABJutogaiIF.BANCHI] = csJutogaiRowN[cABJutogaiIF.BANCHI];
                // 肩書フラグ
                csJutogaiRow[cABJutogaiIF.KATAGAKIFG] = csJutogaiRowN[cABJutogaiIF.KATAGAKIFG];
                // 肩書コード
                csJutogaiRow[cABJutogaiIF.KATAGAKICD] = csJutogaiRowN[cABJutogaiIF.KATAGAKICD];
                // 肩書
                csJutogaiRow[cABJutogaiIF.KATAGAKI] = csJutogaiRowN[cABJutogaiIF.KATAGAKI];
                // 連絡先1
                csJutogaiRow[cABJutogaiIF.RENRAKUSAKI1] = csJutogaiRowN[cABJutogaiIF.RENRAKUSAKI1];
                // 連絡先2
                csJutogaiRow[cABJutogaiIF.RENRAKUSAKI2] = csJutogaiRowN[cABJutogaiIF.RENRAKUSAKI2];
                // 行政区コード
                csJutogaiRow[cABJutogaiIF.GYOSEIKUCD] = csJutogaiRowN[cABJutogaiIF.GYOSEIKUCD];
                // 行政区名
                csJutogaiRow[cABJutogaiIF.GYOSEIKUMEI] = csJutogaiRowN[cABJutogaiIF.GYOSEIKUMEI];
                // 地区コード1
                csJutogaiRow[cABJutogaiIF.CHIKUCD1] = csJutogaiRowN[cABJutogaiIF.CHIKUCD1];
                // 地区名1
                csJutogaiRow[cABJutogaiIF.CHIKUMEI1] = csJutogaiRowN[cABJutogaiIF.CHIKUMEI1];
                // 地区コード2
                csJutogaiRow[cABJutogaiIF.CHIKUCD2] = csJutogaiRowN[cABJutogaiIF.CHIKUCD2];
                // 地区名2
                csJutogaiRow[cABJutogaiIF.CHIKUMEI2] = csJutogaiRowN[cABJutogaiIF.CHIKUMEI2];
                // 地区コード3
                csJutogaiRow[cABJutogaiIF.CHIKUCD3] = csJutogaiRowN[cABJutogaiIF.CHIKUCD3];
                // 地区名3
                csJutogaiRow[cABJutogaiIF.CHIKUMEI3] = csJutogaiRowN[cABJutogaiIF.CHIKUMEI3];
                // 登録異動年月日
                csJutogaiRow[cABJutogaiIF.TOROKUIDOYMD] = csJutogaiRowN[cABJutogaiIF.TOROKUIDOYMD];
                // 登録事由コード
                csJutogaiRow[cABJutogaiIF.TOROKUJIYUCD] = csJutogaiRowN[cABJutogaiIF.TOROKUJIYUCD];
                // 消除異動年月日
                csJutogaiRow[cABJutogaiIF.SHOJOIDOYMD] = csJutogaiRowN[cABJutogaiIF.SHOJOIDOYMD];
                // 消除事由コード
                csJutogaiRow[cABJutogaiIF.SHOJOJIYUCD] = csJutogaiRowN[cABJutogaiIF.SHOJOJIYUCD];
                // リザーブ
                csJutogaiRow[cABJutogaiIF.RESERVE] = csJutogaiRowN[cABJutogaiIF.RESERVE];

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     宛名編集処理
        // * 
        // * 構文           Public Sub EditAtenaJutogai(ByVal csJutogaiEntity As DataSet, _
        // * 　　                             ByVal csAtenaEntity As DataSet)
        // * 
        // * 機能　　       住登外Entityより宛名Entityを追加・編集する
        // * 
        // * 引数           csJutogaiEntity As DataSet  : 住登外(ABJutogaiEntity)
        // * 　　           csAtenaEntity   As DataSet  : 宛名(ABAtenaEntity)
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void EditAtenaJutogai(int IntKoshinKB, string StrIdoYMD, DataSet csJutogaiEntity, ref DataSet csAtenaEntity)


        {
            const string THIS_METHOD_NAME = "EditAtenaJutogai";
            // * corresponds to VS2008 Start 2010/04/16 000007
            // Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
            // Dim cuCityInfo As USSCityInfoClass                  ' 市町村情報管理クラス
            // * corresponds to VS2008 End 2010/04/16 000007
            DataRow csRow;
            // * corresponds to VS2008 Start 2010/04/16 000007
            // Dim csDataSet As DataSet
            // Dim csColumn As DataColumn
            // * corresponds to VS2008 End 2010/04/16 000007
            string strSystemDate;                         // システム日付
            DataRow csJutogaiRow;                         // 住登外DataRow
            ABIdoJiyuBClass cIdoJiyuB;                    // 異動事由Ｂクラス
                                                          // * 履歴番号 000006 2007/10/11 削除開始
                                                          // * 履歴番号 000002 2003/08/22 修正開始
                                                          // Dim cuKanriJohoB As URKANRIJOHOBClass               ' 管理情報Ｂクラス
                                                          // Dim cuKanriJohoB As URKANRIJOHOCacheBClass          ' 管理情報Ｂクラス(キャッシュ対応版)
                                                          // * 履歴番号 000002 2003/08/22 修正終了
                                                          // Dim emKensakShimei As FrnKensakuShimeiType          ' 外国人検索用氏名
                                                          // * 履歴番号 000006 2007/10/11 削除終了
                                                          // * corresponds to VS2008 Start 2010/04/16 000007
            var cABEnumDefine = new ABEnumDefine();
            // * corresponds to VS2008 End 2010/04/16 000007
            var cAtenaSearchKey = new ABAtenaSearchKey();       // 宛名検索キー
                                                                // * corresponds to VS2008 Start 2010/04/16 000007
                                                                // Dim cAtenaB As ABAtenaBClass                        '宛名ＤＡ
                                                                // * corresponds to VS2008 End 2010/04/16 000007


            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 異動事由Ｂクラスのインスタンス化
                cIdoJiyuB = new ABIdoJiyuBClass(m_cfControlData, m_cfConfigData);

                // ＵＲ管理情報Ｂクラスのインスタンス化
                // * 履歴番号 000006 2007/10/11 削除開始
                // * 履歴番号 000002 2003/08/22 修正開始
                // cuKanriJohoB = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                // cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                // * 履歴番号 000002 2003/08/22 修正終了
                // * 履歴番号 000006 2007/10/11 削除開始

                // 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;

                // * 履歴番号 000006 2007/10/11 削除開始
                // ' ＵＲ外国人検索用氏名を取得する   保留
                // emKensakShimei = cuKanriJohoB.GetFrn_KensakuShimei_Param
                // * 履歴番号 000006 2007/10/11 削除開始

                // 住登外データRow
                csJutogaiRow = csJutogaiEntity.Tables[ABJutogaiEntity.TABLE_NAME].Rows[0];

                if (IntKoshinKB == cABEnumDefine.KoshinKB.Insert)
                {
                    csRow = csAtenaEntity.Tables[ABAtenaEntity.TABLE_NAME].NewRow();
                    // DataRowの初期化
                    m_cCommonClass.InitColumnValue(csRow);
                }
                else
                {
                    // 宛名マスタを取得する
                    // 宛名ＤＡクラスのインスタンス化
                    csRow = csAtenaEntity.Tables[ABAtenaEntity.TABLE_NAME].Rows[0];
                }

                // **
                // * 編集処理
                // *
                strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd");                                        // システム日付

                csRow[ABAtenaEntity.JUMINCD] = csJutogaiRow[ABJutogaiEntity.JUMINCD];                                // 住民コード
                csRow[ABAtenaEntity.SHICHOSONCD] = csJutogaiRow[ABJutogaiEntity.SHICHOSONCD];                        // 市町村コード
                csRow[ABAtenaEntity.KYUSHICHOSONCD] = csJutogaiRow[ABJutogaiEntity.KYUSHICHOSONCD];                  // 旧市町村コード
                csRow[ABAtenaEntity.JUMINJUTOGAIKB] = "2";                                                           // 住民住登外区分
                csRow[ABAtenaEntity.JUMINYUSENIKB] = "0";                                                            // 住民優先区分
                csRow[ABAtenaEntity.JUTOGAIYUSENKB] = "1";                                                           // 住登外優先区分
                csRow[ABAtenaEntity.ATENADATAKB] = csJutogaiRow[ABJutogaiEntity.ATENADATAKB];                        // 宛名データ区分
                csRow[ABAtenaEntity.STAICD] = csJutogaiRow[ABJutogaiEntity.STAICD];                                  // 世帯コード
                csRow[ABAtenaEntity.ATENADATASHU] = csJutogaiRow[ABJutogaiEntity.ATENADATASHU];                      // 宛名データ種別
                csRow[ABAtenaEntity.HANYOKB1] = csJutogaiRow[ABJutogaiEntity.HANYOKB1];                              // 汎用区分1
                csRow[ABAtenaEntity.KJNHJNKB] = csJutogaiRow[ABJutogaiEntity.KJNHJNKB];                              // 個人法人区分
                csRow[ABAtenaEntity.HANYOKB2] = csJutogaiRow[ABJutogaiEntity.HANYOKB2];                              // 汎用区分2
                csRow[ABAtenaEntity.KANNAIKANGAIKB] = csJutogaiRow[ABJutogaiEntity.KANNAIKANGAIKB];                  // 管内管外区分
                csRow[ABAtenaEntity.KANAMEISHO1] = csJutogaiRow[ABJutogaiEntity.KANAMEISHO1];                        // カナ名称1
                csRow[ABAtenaEntity.KANJIMEISHO1] = csJutogaiRow[ABJutogaiEntity.KANJIMEISHO1];                      // 漢字名称1
                csRow[ABAtenaEntity.KANAMEISHO2] = csJutogaiRow[ABJutogaiEntity.KANAMEISHO2];                        // カナ名称2
                csRow[ABAtenaEntity.KANJIMEISHO2] = csJutogaiRow[ABJutogaiEntity.KANJIMEISHO2];                      // 漢字名称2
                csRow[ABAtenaEntity.KANJIHJNKEITAI] = csJutogaiRow[ABJutogaiEntity.KANJIHJNKEITAI];                  // 漢字法人形態
                csRow[ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI] = csJutogaiRow[ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI];  // 漢字法人代表者氏名
                csRow[ABAtenaEntity.SEARCHKANJIMEISHO] = csJutogaiRow[ABJutogaiEntity.KANJIMEISHO1];             // 検索用漢字名称
                csRow[ABAtenaEntity.SEARCHKANASEIMEI] = csJutogaiRow[ABJutogaiEntity.SEARCHKANASEIMEI];              // 検索用カナ姓名
                csRow[ABAtenaEntity.SEARCHKANASEI] = csJutogaiRow[ABJutogaiEntity.SEARCHKANASEI];                    // 検索用カナ姓
                csRow[ABAtenaEntity.SEARCHKANAMEI] = csJutogaiRow[ABJutogaiEntity.SEARCHKANAMEI];                    // 検索用カナ名
                csRow[ABAtenaEntity.RRKST_YMD] = StrIdoYMD;                                                      // 履歴開始年月日
                csRow[ABAtenaEntity.RRKED_YMD] = "99999999";                                                         // 履歴終了年月日
                csRow[ABAtenaEntity.UMAREYMD] = csJutogaiRow[ABJutogaiEntity.UMAREYMD];                              // 生年月日
                csRow[ABAtenaEntity.UMAREWMD] = csJutogaiRow[ABJutogaiEntity.UMAREWMD];                              // 生和暦年月日
                csRow[ABAtenaEntity.SEIBETSUCD] = csJutogaiRow[ABJutogaiEntity.SEIBETSUCD];                          // 性別コード
                csRow[ABAtenaEntity.SEIBETSU] = csJutogaiRow[ABJutogaiEntity.SEIBETSU];                              // 性別
                csRow[ABAtenaEntity.ZOKUGARACD] = csJutogaiRow[ABJutogaiEntity.ZOKUGARACD];                          // 続柄コード
                csRow[ABAtenaEntity.ZOKUGARA] = csJutogaiRow[ABJutogaiEntity.ZOKUGARA];                              // 続柄
                csRow[ABAtenaEntity.DAI2ZOKUGARACD] = csJutogaiRow[ABJutogaiEntity.DAI2ZOKUGARACD];                  // 第2続柄コード
                csRow[ABAtenaEntity.DAI2ZOKUGARA] = csJutogaiRow[ABJutogaiEntity.DAI2ZOKUGARA];                      // 第2続柄
                csRow[ABAtenaEntity.YUBINNO] = csJutogaiRow[ABJutogaiEntity.YUBINNO];                                // 郵便番号
                csRow[ABAtenaEntity.JUSHOCD] = csJutogaiRow[ABJutogaiEntity.JUSHOCD];                                // 住所コード
                csRow[ABAtenaEntity.JUSHO] = csJutogaiRow[ABJutogaiEntity.JUSHO];                                    // 住所
                csRow[ABAtenaEntity.BANCHICD1] = csJutogaiRow[ABJutogaiEntity.BANCHICD1];                            // 番地コード1
                csRow[ABAtenaEntity.BANCHICD2] = csJutogaiRow[ABJutogaiEntity.BANCHICD2];                            // 番地コード2
                csRow[ABAtenaEntity.BANCHICD3] = csJutogaiRow[ABJutogaiEntity.BANCHICD3];                            // 番地コード3
                csRow[ABAtenaEntity.BANCHI] = csJutogaiRow[ABJutogaiEntity.BANCHI];                                  // 番地
                csRow[ABAtenaEntity.KATAGAKIFG] = csJutogaiRow[ABJutogaiEntity.KATAGAKIFG];                          // 方書フラグ
                csRow[ABAtenaEntity.KATAGAKICD] = csJutogaiRow[ABJutogaiEntity.KATAGAKICD];                          // 方書コード
                csRow[ABAtenaEntity.KATAGAKI] = csJutogaiRow[ABJutogaiEntity.KATAGAKI];                              // 方書
                csRow[ABAtenaEntity.RENRAKUSAKI1] = csJutogaiRow[ABJutogaiEntity.RENRAKUSAKI1];                      // 連絡先1
                csRow[ABAtenaEntity.RENRAKUSAKI2] = csJutogaiRow[ABJutogaiEntity.RENRAKUSAKI2];                      // 連絡先2
                                                                                                                     // 直近異動年月日
                                                                                                                     // m_cfDateClass.p_strDateValue = m_cNyuryokuParaX.p_strCkinIdoYMD
                                                                                                                     // *履歴番号 000002 2004/05/17 修正開始
                                                                                                                     // csRow[ABAtenaEntity.CKINIDOYMD] = strSystemDate                                                      ' 履歴開始年月日
                csRow[ABAtenaEntity.CKINIDOYMD] = StrIdoYMD;
                // *履歴番号 000002 2004/05/17 修正終了
                // 登録異動年月日
                csRow[ABAtenaEntity.TOROKUIDOYMD] = csJutogaiRow[ABJutogaiEntity.TOROKUIDOYMD];
                // 登録異動和暦年月日
                m_cfDateClass.p_strDateValue = Convert.ToString(csJutogaiRow[ABJutogaiEntity.TOROKUIDOYMD]);
                csRow[ABAtenaEntity.TOROKUIDOWMD] = m_cfDateClass.p_strWarekiYMD;
                csRow[ABAtenaEntity.TOROKUJIYUCD] = csJutogaiRow[ABJutogaiEntity.TOROKUJIYUCD];                      // 登録事由コード
                csRow[ABAtenaEntity.TOROKUJIYU] = cIdoJiyuB.GetIdoJiyu(csJutogaiRow[ABJutogaiEntity.TOROKUJIYUCD].ToString());     // 登録事由
                csRow[ABAtenaEntity.SHOJOIDOYMD] = csJutogaiRow[ABJutogaiEntity.SHOJOIDOYMD];                        // 消除異動年月日
                csRow[ABAtenaEntity.SHOJOJIYUCD] = csJutogaiRow[ABJutogaiEntity.SHOJOJIYUCD];                        // 消除事由コード
                csRow[ABAtenaEntity.SHOJOJIYU] = cIdoJiyuB.GetIdoJiyu(csJutogaiRow[ABJutogaiEntity.SHOJOJIYUCD].ToString());       // 消除事由
                csRow[ABAtenaEntity.GYOSEIKUCD] = csJutogaiRow[ABJutogaiEntity.GYOSEIKUCD];                          // 行政区コード
                csRow[ABAtenaEntity.GYOSEIKUMEI] = csJutogaiRow[ABJutogaiEntity.GYOSEIKUMEI];                        // 行政区名
                csRow[ABAtenaEntity.CHIKUCD1] = csJutogaiRow[ABJutogaiEntity.CHIKUCD1];                              // 地区コード1
                csRow[ABAtenaEntity.CHIKUMEI1] = csJutogaiRow[ABJutogaiEntity.CHIKUMEI1];                            // 地区名1
                csRow[ABAtenaEntity.CHIKUCD2] = csJutogaiRow[ABJutogaiEntity.CHIKUCD2];                              // 地区コード2
                csRow[ABAtenaEntity.CHIKUMEI2] = csJutogaiRow[ABJutogaiEntity.CHIKUMEI2];                            // 地区名2
                csRow[ABAtenaEntity.CHIKUCD3] = csJutogaiRow[ABJutogaiEntity.CHIKUCD3];                              // 地区コード3
                csRow[ABAtenaEntity.CHIKUMEI3] = csJutogaiRow[ABJutogaiEntity.CHIKUMEI3];                            // 地区名3
                csRow[ABAtenaEntity.KAOKUSHIKIKB] = csJutogaiRow[ABJutogaiEntity.KAOKUSHIKIKB];                      // 家屋敷区分
                csRow[ABAtenaEntity.BIKOZEIMOKU] = csJutogaiRow[ABJutogaiEntity.BIKOZEIMOKU];                        // 備考税目

                // 新規作成の場合
                if (IntKoshinKB == cABEnumDefine.KoshinKB.Insert)
                {
                    csAtenaEntity.Tables[ABAtenaEntity.TABLE_NAME].Rows.Add(csRow);
                }

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     宛名履歴編集処理
        // * 
        // * 構文           Public Sub EditAtenaRireki(ByVal csAtenaEntity As DataSet, _
        // * 　　                                  ByVal csAtenaRirekiEntity As DataSet)
        // * 
        // * 機能　　       宛名履歴の編集を行なう。
        // * 
        // * 引数           csAtenaEntity        As DataSet  : 宛名(ABAtenaEntity)
        // * 　　           csAtenaRirekiEntity  As DataSet  : 宛名履歴(ABAtenaRirekiEntity)
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        private void EditAtenaRireki(string StrIdoYMD, DataSet csAtenaEntity, ref DataSet csAtenaRirekiEntity)

        {
            const string THIS_METHOD_NAME = "EditAtenaRireki";                    // メソッド名
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000007
                                                                                  // Dim objErrorStruct As UFErrorStruct                                     'エラー定義構造体
                                                                                  // * corresponds to VS2008 End 2010/04/16 000007
            DataRow csRow;
            DataRow[] csRows;
            DataColumn csColumn;
            var strSearchKana = new string[5];                                          // 検索用カナ
            DataRow csAtenaRow;                                               // 宛名DataRow
            DataRow[] csAtenaRows;
            // *履歴番号 000005 2006/09/13 追加開始
            string strSystemDate;                                             // システム日付
                                                                              // 絞込み・ソートを施したレコードたち
            string strST_YMD;                   // 開始年月日
            string strED_YMD;                   // 終了年月日
            bool blnHit = false;             // 当てはまったかどうか
            string strRirekiNO;
            // *履歴番号 000005 2006/09/13 追加終了

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None;
                m_cfDateClass.p_enEraType = UFEraType.Number;

                strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd");        // システム日付

                // 宛名Rowを取得する
                csAtenaRows = csAtenaEntity.Tables[ABAtenaEntity.TABLE_NAME].Select(ABAtenaEntity.JUMINJUTOGAIKB + "='2'");
                csAtenaRow = csAtenaRows[0];

                // 宛名履歴より新しいRowを取得する
                csRow = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].NewRow();
                // DataRowの初期化
                m_cCommonClass.InitColumnValue(csRow);

                // **
                // * 編集処理
                // *

                if (csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows.Count == 0)
                {
                    // 履歴番号
                    csRow[ABAtenaRirekiEntity.RIREKINO] = "0001";

                    // *履歴番号 000005 2006/09/13 追加開始
                    // 宛名マスタを宛名履歴へそのまま編集する
                    foreach (DataColumn currentCsColumn in csAtenaRow.Table.Columns)
                    {
                        csColumn = currentCsColumn;
                        csRow[csColumn.ColumnName] = csAtenaRow[csColumn];
                    }

                    // 宛名履歴へ追加する
                    csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows.Add(csRow);
                }
                // *履歴番号 000005 2006/09/13 追加終了
                else
                {
                    // *履歴番号 000005 2006/09/13 修正開始
                    // * corresponds to VS2008 Start 2010/04/16 000007
                    // 'csRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                    // ''' 履歴終了日にシステム日付の前日を設定する
                    // 'm_cfDateClass.p_strDateValue = StrIdoYMD
                    // 'csRows[0].BeginEdit()
                    // 'csRows[0][ABAtenaRirekiEntity.RRKED_YMD] = m_cfDateClass.AddDay(-1)
                    // 'csRows[0].EndEdit()

                    // ''' 履歴番号
                    // 'csRow[ABAtenaRirekiEntity.RIREKINO] = CType((CType(csRows[0][ABAtenaRirekiEntity.RIREKINO], Integer) + 1), String).PadLeft(4, "0"c)
                    // * corresponds to VS2008 End 2010/04/16 000007

                    // 追加するレコード用に履歴番号を取得する
                    csRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select("", ABAtenaRirekiEntity.RIREKINO + " DESC");
                    strRirekiNO = ((int)csRows[0][ABAtenaRirekiEntity.RIREKINO] + 1).ToString().RPadLeft(4, '0');

                    // 住民住登外区分="2"で抽出し、履歴開始年月日昇順・履歴番号昇順にソートする
                    csRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2'", ABAtenaRirekiEntity.RRKST_YMD + " ASC , " + ABAtenaRirekiEntity.RIREKINO + " ASC");

                    // どのレコードの開始～終了に当てはまるかを調べる
                    foreach (var csRirekiRow in csRows)
                    {
                        // 開始・終了年月日を取得
                        strST_YMD = Convert.ToString(csRirekiRow[ABAtenaRirekiEntity.RRKST_YMD]);
                        strED_YMD = Convert.ToString(csRirekiRow[ABAtenaRirekiEntity.RRKED_YMD]);

                        if (blnHit == false)
                        {
                            // まだ当てはまるレコードが見つかっていない
                            if (UFVBAPI.CompareString(strST_YMD, StrIdoYMD, false) > 0)
                            {
                                // 開始年月日＞StrIdoYMD

                                blnHit = true;   // フラグをTrueにして、これ以降のレコードの更新を行う
                            }

                            else if (UFVBAPI.CompareString(strST_YMD, StrIdoYMD, false) <= 0 && UFVBAPI.CompareString(StrIdoYMD, strED_YMD, false) <= 0 && strED_YMD != "99999999")
                            {
                                // 開始年月日≦StrIdoYMD≦終了年月日
                                // かつ
                                // 終了年月日が"99999999"でない

                                blnHit = true;   // フラグをTrueにして、これ以降のレコードの更新を行う

                            }
                        }

                        // 当てはまるレコードが見つかった場合
                        if (blnHit == true)
                        {
                            // 宛名マスタを宛名履歴へそのまま編集する
                            foreach (DataColumn currentCsColumn1 in csAtenaRow.Table.Columns)
                            {
                                csColumn = currentCsColumn1;
                                if (csColumn.ColumnName != ABAtenaRirekiEntity.JUMINCD && csColumn.ColumnName != ABAtenaRirekiEntity.RIREKINO && csColumn.ColumnName != ABAtenaRirekiEntity.RRKST_YMD && csColumn.ColumnName != ABAtenaRirekiEntity.RRKED_YMD && csColumn.ColumnName != ABAtenaRirekiEntity.CKINIDOYMD && csColumn.ColumnName != ABAtenaRirekiEntity.SAKUSEINICHIJI && csColumn.ColumnName != ABAtenaRirekiEntity.SAKUSEIUSER)





                                {
                                    // 住民CD・履歴番号・開始・終了・直近異動年月日・作成日時・作成ユーザ以外を上書きする

                                    csRirekiRow[csColumn.ColumnName] = csAtenaRow[csColumn];
                                }
                            }
                        }

                    }

                    // 当てはまるレコードが見つからなかった場合、直近で分割する
                    if (blnHit == false)
                    {
                        // 住民住登外区分="2"、履歴終了年月日="99999999"で抽出
                        csRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'");
                        if (csRows.Length > 0)
                        {
                            m_cfDateClass.p_strDateValue = StrIdoYMD;
                            // 直近レコードの終了年月日をStrIdoYMDの一日前の値で更新する
                            csRows[0].BeginEdit();
                            csRows[0][ABAtenaRirekiEntity.RRKED_YMD] = m_cfDateClass.AddDay(-1);
                            csRows[0].EndEdit();
                        }

                        // 宛名マスタを宛名履歴へそのまま編集する
                        foreach (DataColumn currentCsColumn2 in csAtenaRow.Table.Columns)
                        {
                            csColumn = currentCsColumn2;
                            csRow[csColumn.ColumnName] = csAtenaRow[csColumn];
                        }

                        // 履歴番号を設定する
                        csRow[ABAtenaRirekiEntity.RIREKINO] = strRirekiNO;

                        // 宛名履歴へ追加する
                        csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows.Add(csRow);
                    }
                    // *履歴番号 000005 2006/09/13 修正終了
                }

                // *履歴番号 000005 2006/09/13 削除開始
                // * corresponds to VS2008 Start 2010/04/16 000007
                // ''' 宛名マスタを宛名履歴へそのまま編集する
                // 'For Each csColumn In csAtenaRow.Table.Columns
                // '    csRow[csColumn.ColumnName] = csAtenaRow[csColumn]
                // 'Next csColumn

                // ''' 宛名履歴へ追加する
                // 'csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows.Add(csRow)
                // *履歴番号 000005 2006/09/13 削除終了
                // * corresponds to VS2008 End 2010/04/16 000007

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

        }

        // ************************************************************************************************
        // * メソッド名     宛名累積処理
        // * 
        // * 構文           Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
        // *                                             ByRef csAtenaRuisekiEntity As DataSet, _
        // *                                             ByVal csRirekiCkinRow As DataRow)
        // * 
        // * 機能　　       宛名履歴の編集を行なう。
        // * 
        // * 引数           csAtenaRirekiEntity   As DataSet  : 宛名履歴(ABAtenaRirekiEntity)
        // * 　　           csAtenaRuisekiEntity  As DataSet  : 宛名累積(ABAtenaRuisekiEntity)
        // * 　　           csRirekiCkinRow       As DataRow  : 手を加える前の履歴直近ロウ
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        // *履歴番号 000005 2006/09/13 修正開始
        // 'Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
        // 'ByRef csAtenaRuisekiEntity As DataSet)
        private void EditAtenaRuiseki(DataSet csAtenaRirekiEntity, ref DataSet csAtenaRuisekiEntity, DataRow csRirekiCkinRow)

        {
            // *履歴番号 000005 2006/09/13 修正終了
            const string THIS_METHOD_NAME = "EditAtenaRuiseki";                   // メソッド名
                                                                                  // * corresponds to VS2008 Start 2010/04/16 000007
                                                                                  // Dim objErrorStruct As UFErrorStruct                                     ' エラー定義構造体
                                                                                  // * corresponds to VS2008 End 2010/04/16 000007
            DataRow csRow;
            DataRow[] csRows;
            DataColumn csColumn;
            var strSearchKana = new string[5];                                          // 検索用カナ
                                                                                        // 宛名履歴DataRow
            string strSystemDate;                                             // システム日付
                                                                              // *履歴番号 000005 2006/09/13 追加開始
            bool blnAtoAdd = false;                                        // 後のレコードを追加したかどうか
                                                                           // *履歴番号 000005 2006/09/13 追加終了

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // *履歴番号 000004 2006/05/31 追加開始
                strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff");                   // システム日付
                                                                                                         // *履歴番号 000004 2006/05/31 追加終了

                // *履歴番号 000005 2006/09/13 追加開始
                // 累積(前)を生成し追加する
                // 累積(前)は操作前の履歴直近レコードとする
                if (csRirekiCkinRow is not null)
                {
                    // 宛名累積より新しいRowを取得する
                    csRow = csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].NewRow();
                    // DataRowの初期化
                    m_cCommonClass.InitColumnValue(csRow);

                    // 処理日時
                    csRow[ABAtenaRuisekiEntity.SHORINICHIJI] = strSystemDate;
                    // 前後区分
                    csRow[ABAtenaRuisekiEntity.ZENGOKB] = "1";

                    // 履歴→累積
                    foreach (DataColumn currentCsColumn in csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Columns)
                    {
                        csColumn = currentCsColumn;
                        csRow[csColumn.ColumnName] = csRirekiCkinRow[csColumn];
                    }

                    // 宛名累積へ追加する
                    csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows.Add(csRow);
                }
                // *履歴番号 000005 2006/09/13 追加終了

                foreach (DataRow csAtenaRirekiRow in csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Rows)
                {

                    if (csAtenaRirekiRow.RowState == DataRowState.Added)
                    {
                        // 宛名累積より新しいRowを取得する
                        csRow = csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].NewRow();
                        // DataRowの初期化
                        m_cCommonClass.InitColumnValue(csRow);

                        // 処理日時
                        csRow[ABAtenaRuisekiEntity.SHORINICHIJI] = strSystemDate;
                        // 前後区分
                        csRow[ABAtenaRuisekiEntity.ZENGOKB] = "2";

                        // 宛名履歴マスタを宛名累積へそのまま編集する
                        foreach (DataColumn currentCsColumn1 in csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Columns)
                        {
                            csColumn = currentCsColumn1;
                            csRow[csColumn.ColumnName] = csAtenaRirekiRow[csColumn];
                        }

                        // 宛名累積へ追加する
                        csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows.Add(csRow);

                        blnAtoAdd = true;
                        break;
                    }
                    // * corresponds to VS2008 Start 2010/04/16 000007
                    // '*履歴番号 000005 2006/09/13 削除開始
                    // '''Select Case csAtenaRirekiRow.RowState
                    // '''    Case DataRowState.Added

                    // '''        ' 宛名累積より新しいRowを取得する
                    // '''        csRow = csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].NewRow
                    // '''        ' DataRowの初期化
                    // '''        m_cCommonClass.InitColumnValue(csRow)

                    // '''        '**
                    // '''        '* 編集処理
                    // '''        '*

                    // '''        ' 処理日時
                    // '''        '*履歴番号 000004 2006/05/31 削除開始
                    // '''        'strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")                   'システム日付
                    // '''        '*履歴番号 000004 2006/05/31 削除終了
                    // '''        csRow[ABAtenaRuisekiEntity.SHORINICHIJI] = strSystemDate

                    // '''        ' 前後区分
                    // '''        csRow[ABAtenaRuisekiEntity.ZENGOKB] = "2"

                    // '''        ' 宛名マスタを宛名履歴へそのまま編集する
                    // '''        For Each csColumn In csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Columns
                    // '''            csRow[csColumn.ColumnName] = csAtenaRirekiRow[csColumn]
                    // '''        Next csColumn

                    // '''        ' 宛名累積へ追加する
                    // '''        csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows.Add(csRow)

                    // '''        '*履歴番号 000004 2006/05/31 追加開始
                    // '''    Case DataRowState.Modified
                    // '''        ' 宛名累積より新しいRowを取得する
                    // '''        csRow = csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].NewRow
                    // '''        ' DataRowの初期化
                    // '''        m_cCommonClass.InitColumnValue(csRow)

                    // '''        '**
                    // '''        '* 編集処理
                    // '''        '*

                    // '''        ' 処理日時
                    // '''        csRow[ABAtenaRuisekiEntity.SHORINICHIJI] = strSystemDate

                    // '''        ' 前後区分
                    // '''        csRow[ABAtenaRuisekiEntity.ZENGOKB] = "1"

                    // '''        ' 宛名履歴データを宛名累積へそのまま編集する
                    // '''        For Each csColumn In csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Columns
                    // '''            csRow[csColumn.ColumnName] = csAtenaRirekiRow[csColumn]
                    // '''        Next csColumn

                    // '''        ' 宛名累積へ追加する
                    // '''        csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows.Add(csRow)
                    // '''        '*履歴番号 000004 2006/05/31 追加終了
                    // '''End Select
                    // '*履歴番号 000005 2006/09/13 削除終了
                    // * corresponds to VS2008 End 2010/04/16 000007
                }

                // ここで累積(後)がまだ追加されていない場合(追加なしで更新しただけの場合)
                if (blnAtoAdd == false)
                {
                    // 操作後の履歴直近レコードを取得する
                    csRows = csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'");

                    // 宛名累積より新しいRowを取得する
                    csRow = csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].NewRow();
                    // DataRowの初期化
                    m_cCommonClass.InitColumnValue(csRow);

                    // 処理日時
                    csRow[ABAtenaRuisekiEntity.SHORINICHIJI] = strSystemDate;
                    // 前後区分
                    csRow[ABAtenaRuisekiEntity.ZENGOKB] = "2";

                    // 宛名履歴マスタを宛名累積へそのまま編集する
                    foreach (DataColumn currentCsColumn2 in csAtenaRirekiEntity.Tables[ABAtenaRirekiEntity.TABLE_NAME].Columns)
                    {
                        csColumn = currentCsColumn2;
                        csRow[csColumn.ColumnName] = csRows[0](csColumn);
                    }

                    // 宛名累積へ追加する
                    csAtenaRuisekiEntity.Tables[ABAtenaRuisekiEntity.TABLE_NAME].Rows.Add(csRow);

                }

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

        }


        // ************************************************************************************************
        // * メソッド名     検索用カナ取得
        // * 
        // * 構文           Public Function GetSearchKana(ByVal strKanaMeisho As String) As String()
        // * 
        // * 機能　　       検索用カナ名称を編集する
        // * 
        // * 引数           strKanaMeisho As String     : カナ名称
        // * 
        // * 戻り値         String()        : [0]検索用カナ姓名
        // *                                  : [1]検索用カナ姓
        // *                                  : [2]検索用カナ名
        // *                                  : [3]カナ姓
        // *                                  : [4]カナ名
        // ************************************************************************************************
        private string[] GetSearchKana(string strKanaMeisho)
        {
            const string THIS_METHOD_NAME = "GetSearchKana";                      // メソッド名
            var strSearchKana = new string[5];                      // 検索用カナ
            var cuString = new USStringClass();                 // 文字列編集
            int intIndex;                             // 先頭からの空白位置

            try
            {
                // デバッグ開始ログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // カナ姓名（空白を詰める）
                strSearchKana[0] = cuString.ToKanaKey(strKanaMeisho).Replace(" ", string.Empty);

                // 先頭からの空白位置を調べる
                intIndex = strKanaMeisho.RIndexOf(" ");

                // 空白が存在しない場合
                if (intIndex == -1)
                {
                    // カナ姓・名
                    strSearchKana[1] = strSearchKana[0];
                    strSearchKana[3] = strKanaMeisho;
                    strSearchKana[2] = string.Empty;
                    strSearchKana[4] = string.Empty;
                }
                else
                {
                    // カナ姓・名
                    strSearchKana[1] = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex));
                    strSearchKana[3] = strKanaMeisho.RSubstring(0, intIndex);

                    // 先頭からの空白位置が文字列長と以上場合
                    if (intIndex + 1 >= strKanaMeisho.RLength())
                    {
                        strSearchKana[2] = string.Empty;
                        strSearchKana[4] = string.Empty;
                    }
                    else
                    {
                        strSearchKana[2] = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1));
                        strSearchKana[4] = strKanaMeisho.RSubstring(intIndex + 1);
                    }
                }

                // デバッグ終了ログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }


            catch (UFAppException objAppExp)    // UFAppExceptionをキャッチ
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // システムエラーをスローする
                throw objExp;
            }

            return strSearchKana;

        }

        // *履歴番号 000003 2006/03/27 追加開始
        // ************************************************************************************************
        // * メソッド名      宛名レプリカデータ更新
        // * 
        // * 構文            Public Sub AtenaDataReplicaKoshin(ByVal strJuminCD As String, _
        // *                                      ByVal strStaiCD As String, ByVal strKoshinKB As String)
        // * 
        // * 機能　　        宛名レプリカデータの更新処理を行なう
        // * 
        // * 引数           strJuminCD：住民コード
        // *                  strStaiCD：世帯コード
        // *                  strKoshinKB：更新区分（追加：1　修正：2）
        // * 
        // * 戻り値         なし
        // ************************************************************************************************
        public void AtenaDataReplicaKoshin(string strJuminCD, string strStaiCD, string strKoshinKB)
        {
            const string THIS_METHOD_NAME = "AtenaDataReplicaKoshin";
            const string WORK_FLOW_NAME = "宛名異動";             // ワークフロー名
            const string DATA_NAME = "宛名";                      // データ名
            ABAtenaKanriJohoBClass cAtenaKanriJohoB;      // 宛名管理情報ＤＡビジネスクラス
            DataSet csAtenaKanriEntity;                   // 宛名管理情報データセット
            var csABToshoPrmEntity = new DataSet();             // レプリカ作成用パラメータデータセット
            DataTable csABToshoPrmTable;                  // レプリカ作成用パラメータデータテーブル
            DataRow csABToshoPrmRow;                      // レプリカ作成用パラメータデータテーブル
            ABAtenaCnvBClass cABAtenaCnvBClass;


            try
            {
                // デバッグログ出力
                m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);

                // 宛名管理情報Ｂクラスのインスタンス作成
                cAtenaKanriJohoB = new ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                // 宛名管理情報の種別04識別キー01のデータを全件取得する
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01");

                // 管理情報のワークフローレコードが存在し、パラメータが"1"と"2"の時だけワークフロー処理を行う
                if (!(csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows.Count == 0) && ((string)csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == "1" || (string)csAtenaKanriEntity.Tables[ABAtenaKanriJohoEntity.TABLE_NAME].Rows[0][ABAtenaKanriJohoEntity.PARAMETER] == "2"))

                {

                    // データセット取得クラスのインスタンス化
                    cABAtenaCnvBClass = new ABAtenaCnvBClass(m_cfControlData, m_cfConfigData, m_cfRdb);
                    // テーブルセットの取得
                    csABToshoPrmTable = cABAtenaCnvBClass.CreateColumnsToshoPrmData();
                    csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME;
                    // データセットにテーブルセットの追加
                    csABToshoPrmEntity.Tables.Add(csABToshoPrmTable);

                    // 新規ロウの作成
                    csABToshoPrmRow = csABToshoPrmEntity.Tables[ABToshoPrmEntity.TABLE_NAME].NewRow();
                    // レプリカデータ作成用パラメータにセット
                    csABToshoPrmRow[ABToshoPrmEntity.JUMINCD] = strJuminCD;                 // 住民コード
                    csABToshoPrmRow[ABToshoPrmEntity.STAICD] = strStaiCD;                   // 世帯コード
                    csABToshoPrmRow[ABToshoPrmEntity.KOSHINKB] = strKoshinKB;               // 更新区分（追加:1 修正:2 論理削除:9 削除データ回復:2 物理削除:D）
                                                                                                 // データセットにロウを追加する
                    csABToshoPrmEntity.Tables[ABToshoPrmEntity.TABLE_NAME].Rows.Add(csABToshoPrmRow);

                    // ワークフロー送信処理呼び出し
                    cABAtenaCnvBClass.WorkFlowExec(csABToshoPrmEntity, WORK_FLOW_NAME, DATA_NAME);

                }

                // デバッグログ出力
                m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME);
            }

            catch (UFAppException objAppExp)
            {
                // ワーニングログ出力
                m_cfLog.WarningWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + "【ワーニング内容:" + objAppExp.Message + "】");



                // エラーをそのままスローする
                throw objAppExp;
            }

            catch (Exception objExp)
            {
                // エラーログ出力
                m_cfLog.ErrorWrite(m_cfControlData, "【クラス名:" + THIS_CLASS_NAME + "】" + "【メソッド名:" + THIS_METHOD_NAME + "】" + "【エラー内容:" + objExp.Message + "】");


                // エラーをそのままスローする
                throw objExp;
            }

        }
        // *履歴番号 000003 2006/03/27 追加終了

    }
}
