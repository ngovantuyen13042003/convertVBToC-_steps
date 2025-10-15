'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        共有更新Ｂ(ABKyoyuKoshinBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/06/06　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/05/17  000001      共有更新処理メソッドに異動日時を引数として追加
'* 2004/05/17  000002      直近異動年月日に異動日時を格納に修正
'* 2006/03/27  000003      ワークフロー連携メソッド追加
'* 2006/05/31  000004      累積更新時に異動前データも追加する
'* 2006/09/13  000005      更新方法を変更する
'*                         履歴データの開始～終了に引数の異動年月日が当てはまったデータ以降を
'*                         引数のcABJutogaiの内容で更新する。但し開始・終了・異動年月日は除く
'*                         当てはまったデータが直近の場合は通常通り分割する(マルゴ村山)
'* 2007/10/11  000006      宛名編集処理の未使用クラス(UR管理情報キャッシュクラス)を削除する（比嘉）
'* 2010/04/16  000007      VS2008対応（比嘉）
'* 2014/12/24  000008      【AB21080】中間サーバーＢＳ連携機能追加（石合）
'* 2015/01/08  000009      【AB21080】中間サーバーＢＳ連携機能削除（石合）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common

Public Class ABKyoyuKoshinBClass

    '**
    '* クラスID定義
    '* 
    Private Const THIS_CLASS_NAME As String = "ABKyoyuKoshinBClass"

    '**
    '* メンバ変数
    '*  
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigData As UFConfigDataClass             ' 環境情報データクラス
    Private m_cfLog As UFLogClass                           ' ログ出力クラス
    Private m_cfRdb As UFRdbClass                           ' RDBクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス
    Private m_cNyuryokuParaX As ABNyuryokuParaXClass        ' 入力画面パラメータ
    Private m_cCommonClass As New ABCommonClass()           ' Commonクラス

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass
    '* 　　                          ByVal csUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigDataClass
        m_cfRdb = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLog = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' 日付クラスのインスタンス化
        m_cfDateClass = New UFDateClass(m_cfConfigData)
    End Sub


    '************************************************************************************************
    '* メソッド名     共有更新処理
    '* 
    '* 構文           Public Function UpdateKyoyu(ByVal StrJuminCD As String, _
    '*                        　                  ByVal IntKoshinKB As Integer, _
    '*                                            ByVal cABJutogai As DataSet) As Integer
    '* 
    '* 機能　　       共有データの追加を行なう。
    '* 
    '* 引数           StrJuminCD As String           : 住民コード
    '*                IntKoshinKB As Intege          : 更新区分
    '* 　　　         cABJutogai As DataSet          : 住登外Entity
    '* 
    '* 戻り値         件数
    '************************************************************************************************
    Public Function UpdateKyoyu(ByVal StrJuminCD As String, _
                                ByVal IntKoshinKB As Integer, _
                                ByVal StrIdoYMD As String, _
                                ByVal cABJutogai As DataSet) As Integer

        Const THIS_METHOD_NAME As String = "UpdateKyoyu"    'メソッド名
        Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        Dim cJutogaiB As ABJutogaiBClass                    ' 住登外ＤＡ
        Dim csJutogaiEntity As DataSet                      ' 住登外DataSet
        Dim csJutogaiRow As DataRow                         ' 住登外Row
        Dim csJutogaiRowN As DataRow
        Dim cAtenaB As ABAtenaBClass                        ' 宛名ＤＡ
        Dim csAtenaEntity As DataSet                        ' 宛名Entity
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       ' 宛名検索キー
        Dim cAtenaRirekiB As ABAtenaRirekiBClass            ' 宛名履歴ＤＡ
        Dim csAtenaRirekiEntity As DataSet                  ' 宛名履歴Entity
        Dim cAtenaRuisekiB As ABAtenaRuisekiBClass          ' 宛名累積ＤＡ
        Dim csAtenaRuisekiEntity As DataSet                 ' 宛名累積Entity
        Dim intUpdataCount As Integer                       ' 更新件数
        Dim strSystemDate As String                         ' システム日付
        Dim csDataRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000007
        Dim cABEnumDefine As New ABEnumDefine
        'Dim csColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        '*履歴番号 000003 2006/03/27 追加開始
        Dim strKoshinKB As String                           '更新区分
        '*履歴番号 000003 2006/03/27 追加終了
        '*履歴番号 000005 2006/09/13 追加開始
        Dim csRirekiRows() As DataRow
        Dim csRirekiCkinRow As DataRow
        '*履歴番号 000005 2006/09/13 追加終了
        '* 履歴番号 000009 2015/01/08 削除開始
        ''*履歴番号 000008 2014/12/24 追加開始
        'Dim cABBSRenkeiB As ABBSRenkeiBClass                ' 中間サーバーＢＳ連携ビジネスクラス
        ''*履歴番号 000008 2014/12/24 追加終了
        '* 履歴番号 000009 2015/01/08 削除終了

        Try

            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 住登外ＤＡクラスのインスタンス化
            cJutogaiB = New ABJutogaiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' 宛名ＤＡクラスのインスタンス化
            cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' 宛名履歴ＤＡクラスのインスタンス化
            cAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' 宛名累積ＤＡクラスのインスタンス化
            cAtenaRuisekiB = New ABAtenaRuisekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

            ' システム日付の取得
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")


            ' 住登外マスタの追加を行なう
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                intUpdataCount = cJutogaiB.InsertJutogaiB(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0))
            Else
                csJutogaiEntity = cJutogaiB.GetJutogaiBHoshu(StrJuminCD)

                ' 住登外データが存在しない場合、エラーを発生する
                If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count = 0) Then
                    m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    ' エラー定義を取得（更新対象のデータが存在しません。：住登外）
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住登外", objErrorStruct.m_strErrorCode)
                End If

                csJutogaiRow = csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)
                csJutogaiRowN = cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)

                csJutogaiRow.BeginEdit()

                'For Each csColumn In csJutogaiRow.Table.Columns
                '    csJutogaiRow(csColumn.ColumnName) = csJutogaiRowN(csColumn)
                'Next csColumn
                'csJutogaiRow = csJutogaiRowN
                ' 住登外編集処理
                Me.EditJutogai(csJutogaiRow, csJutogaiRowN)

                csJutogaiRow.EndEdit()

                intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0))
                'intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiRow)
            End If

            ' 更新件数が１件以外の場合、エラーを発生させる
            If Not (intUpdataCount = 1) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得（既に同一データが存在します。：住登外）
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住登外", objErrorStruct.m_strErrorCode)
            End If

            '**
            '* 宛名マスタ更新処理
            '*
            cAtenaSearchKey.p_strJuminCD = StrJuminCD

            ' 宛名編集処理
            ' 新規作成の場合
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csAtenaEntity = m_cfRdb.GetTableSchema(ABAtenaEntity.TABLE_NAME)
            Else
                ' 宛名マスタを取得する
                ' 宛名ＤＡクラスのインスタンス化
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey)
            End If

            Me.EditAtenaJutogai(IntKoshinKB, StrIdoYMD, cABJutogai, csAtenaEntity)

            For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows

                If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                    ' 宛名マスタの追加を行なう
                    intUpdataCount = cAtenaB.InsertAtenaB(csDataRow)
                Else
                    ' 宛名マスタの更新を行なう
                    intUpdataCount = cAtenaB.UpdateAtenaB(csDataRow)
                End If

                ' 更新件数が１件以外の場合、エラーを発生させる
                If Not (intUpdataCount = 1) Then
                    m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    ' エラー定義を取得（既に同一データが存在します。：宛名）
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名", objErrorStruct.m_strErrorCode)
                End If
            Next csDataRow

            '**
            '* 宛名履歴マスタ更新処理
            '*

            ' 宛名履歴マスタを取得する
            '*履歴番号 000005 2006/09/13 修正開始
            ' 直近だけでなく全件取得する
            ''csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "99999999", True)
            csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "", True)

            ' 直近ロウを退避しておく
            csRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")
            If csRirekiRows.Length > 0 Then
                csRirekiCkinRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow()
                csRirekiCkinRow.ItemArray = csRirekiRows(0).ItemArray
            Else
                csRirekiCkinRow = Nothing
            End If
            '*履歴番号 000005 2006/09/13 修正終了

            ' 宛名履歴編集処理
            Me.EditAtenaRireki(StrIdoYMD, csAtenaEntity, csAtenaRirekiEntity)

            ' 宛名履歴マスタの追加を行なう
            For Each csDataRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows
                Select Case csDataRow.RowState
                    Case DataRowState.Added
                        intUpdataCount = cAtenaRirekiB.InsertAtenaRB(csDataRow)

                        ' 更新件数が１件以外の場合、エラーを発生させる
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' エラー定義を取得（既に同一データが存在します。：宛名履歴）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                        End If
                    Case DataRowState.Modified
                        intUpdataCount = cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                        ' 更新件数が１件以外の場合、エラーを発生させる
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' エラー定義を取得（更新対象のデータが存在しません。：宛名履歴）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名履歴", objErrorStruct.m_strErrorCode)
                        End If
                End Select
            Next csDataRow

            '**
            '* 宛名累積マスタ更新処理
            '*

            ' 宛名累積マスタを取得する
            csAtenaRuisekiEntity = m_cfRdb.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME)

            ' 宛名累積編集処理
            '*履歴番号 000005 2006/09/13 修正開始
            ' 退避しておいた操作前の直近レコードを引数に加える
            ''Me.EditAtenaRuiseki(csAtenaRirekiEntity, csAtenaRuisekiEntity)
            Me.EditAtenaRuiseki(csAtenaRirekiEntity, csAtenaRuisekiEntity, csRirekiCkinRow)
            '*履歴番号 000005 2006/09/13 修正終了

            ' 宛名累積マスタの追加を行なう
            For Each csDataRow In csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows
                Select Case csDataRow.RowState
                    Case DataRowState.Added
                        intUpdataCount = cAtenaRuisekiB.InsertAtenaRB(csDataRow)

                        ' 更新件数が１件以外の場合、エラーを発生させる
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' エラー定義を取得（既に同一データが存在します。：宛名累積）
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "宛名累積", objErrorStruct.m_strErrorCode)
                        End If
                End Select

            Next csDataRow

            '*履歴番号 000003 2006/03/27 追加開始
            '処理区分を資産税更新用からワークフロー連携用に修正する
            Select Case IntKoshinKB
                Case cABEnumDefine.KoshinKB.Insert
                    strKoshinKB = "1"
                Case cABEnumDefine.KoshinKB.Update
                    strKoshinKB = "2"
            End Select
            'ワークフロー連携処理の呼び出し
            AtenaDataReplicaKoshin(CStr(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0).Item(ABJutogaiEntity.JUMINCD)), _
                                      CStr(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0).Item(ABJutogaiEntity.STAICD)), CStr(IntKoshinKB))
            '*履歴番号 000003 2006/03/27 追加終了

            '* 履歴番号 000009 2015/01/08 削除開始
            ''*履歴番号 000008 2014/12/24 追加開始
            '' 中間サーバーＢＳ連携ビジネスクラスのインスタンス化
            'cABBSRenkeiB = New ABBSRenkeiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

            '' 中間サーバーＢＳ連携の実行
            'cABBSRenkeiB.ExecRenkei(StrJuminCD)
            ''*履歴番号 000008 2014/12/24 追加終了
            '* 履歴番号 000009 2015/01/08 削除終了

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbDeadLockExp As UFRdbDeadLockException   ' デッドロックをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objRdbDeadLockExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbDeadLockExp.Message, objRdbDeadLockExp.p_intErrorCode, objRdbDeadLockExp)

        Catch objUFRdbUniqueExp As UFRdbUniqueException     ' 一意制約違反をキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objUFRdbUniqueExp.Message, objUFRdbUniqueExp.p_intErrorCode, objUFRdbUniqueExp)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

        Return intUpdataCount

    End Function

    '************************************************************************************************
    '* メソッド名     住登外編集処理
    '* 
    '* 構文           Public Sub EditJutogai(ByVal cfControlData As UFControlData,
    '* 　　                               ByVal cNyuryokuParaX As ABNyuryokuParaXClass) As DataSet
    '* 
    '* 機能　　       入力画面データより住登外Entityを追加・編集する
    '* 
    '* 引数           csJutogaiEntity As DataSet              : 住登外Entity
    '* 　　           cNyuryokuParaX As ABNyuryokuParaXClass  : 個人入力データ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub EditJutogai(ByRef csJutogaiRow As DataRow, _
                            ByVal csJutogaiRowN As DataRow)
        Const THIS_METHOD_NAME As String = "EditJutogai"    'メソッド名
        Dim cABJutogaiIF As New ABJutogaiEntity()                   '住登外マスタコンストクラス

        Try
            '**
            '* 編集処理
            '*
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With cABJutogaiIF
                '市町村コード
                csJutogaiRow(.SHICHOSONCD) = csJutogaiRowN(.SHICHOSONCD)
                '旧市町村コード
                csJutogaiRow(.KYUSHICHOSONCD) = csJutogaiRowN(.KYUSHICHOSONCD)
                '世帯コード
                csJutogaiRow(.STAICD) = csJutogaiRowN(.STAICD)
                '宛名データ区分
                csJutogaiRow(.ATENADATAKB) = csJutogaiRowN(.ATENADATAKB)
                '宛名データ種別
                csJutogaiRow(.ATENADATASHU) = csJutogaiRowN(.ATENADATASHU)
                '検索用カナ姓名
                csJutogaiRow(.SEARCHKANASEIMEI) = csJutogaiRowN(.SEARCHKANASEIMEI)
                '検索用カナ姓
                csJutogaiRow(.SEARCHKANASEI) = csJutogaiRowN(.SEARCHKANASEI)
                '検索用カナ名
                csJutogaiRow(.SEARCHKANAMEI) = csJutogaiRowN(.SEARCHKANAMEI)
                'カナ名称1
                csJutogaiRow(.KANAMEISHO1) = csJutogaiRowN(.KANAMEISHO1)
                '漢字名称1
                csJutogaiRow(.KANJIMEISHO1) = csJutogaiRowN(.KANJIMEISHO1)
                'カナ名称2
                csJutogaiRow(.KANAMEISHO2) = csJutogaiRowN(.KANAMEISHO2)
                '漢字名称2
                csJutogaiRow(.KANJIMEISHO2) = csJutogaiRowN(.KANJIMEISHO2)
                '生年月日
                csJutogaiRow(.UMAREYMD) = csJutogaiRowN(.UMAREYMD)
                '生和暦年月日
                csJutogaiRow(.UMAREWMD) = csJutogaiRowN(.UMAREWMD)
                '性別コード
                csJutogaiRow(.SEIBETSUCD) = csJutogaiRowN(.SEIBETSUCD)
                '性別
                csJutogaiRow(.SEIBETSU) = csJutogaiRowN(.SEIBETSU)
                '続柄コード
                csJutogaiRow(.ZOKUGARACD) = csJutogaiRowN(.ZOKUGARACD)
                '続柄
                csJutogaiRow(.ZOKUGARA) = csJutogaiRowN(.ZOKUGARA)
                '第2続柄コード
                csJutogaiRow(.DAI2ZOKUGARACD) = csJutogaiRowN(.DAI2ZOKUGARACD)
                '第2続柄
                csJutogaiRow(.DAI2ZOKUGARA) = csJutogaiRowN(.DAI2ZOKUGARA)
                '漢字法人代表者氏名
                csJutogaiRow(.KANJIHJNDAIHYOSHSHIMEI) = csJutogaiRowN(.KANJIHJNDAIHYOSHSHIMEI)
                '汎用区分1
                csJutogaiRow(.HANYOKB1) = csJutogaiRowN(.HANYOKB1)
                '漢字法人形態
                csJutogaiRow(.KANJIHJNKEITAI) = csJutogaiRowN(.KANJIHJNKEITAI)
                '個人法人区分
                csJutogaiRow(.KJNHJNKB) = csJutogaiRowN(.KJNHJNKB)
                '汎用区分2
                csJutogaiRow(.HANYOKB2) = csJutogaiRowN(.HANYOKB2)
                '管内管外区分
                csJutogaiRow(.KANNAIKANGAIKB) = csJutogaiRowN(.KANNAIKANGAIKB)
                '家屋敷区分
                csJutogaiRow(.KAOKUSHIKIKB) = csJutogaiRowN(.KAOKUSHIKIKB)
                '備考税目
                csJutogaiRow(.BIKOZEIMOKU) = csJutogaiRowN(.BIKOZEIMOKU)
                '郵便番号
                csJutogaiRow(.YUBINNO) = csJutogaiRowN(.YUBINNO)
                '住所コード
                csJutogaiRow(.JUSHOCD) = csJutogaiRowN(.JUSHOCD)
                '住所
                csJutogaiRow(.JUSHO) = csJutogaiRowN(.JUSHO)
                '番地コード1
                csJutogaiRow(.BANCHICD1) = csJutogaiRowN(.BANCHICD1)
                '番地コード2
                csJutogaiRow(.BANCHICD2) = csJutogaiRowN(.BANCHICD2)
                '番地コード3
                csJutogaiRow(.BANCHICD3) = csJutogaiRowN(.BANCHICD3)
                '番地
                csJutogaiRow(.BANCHI) = csJutogaiRowN(.BANCHI)
                '肩書フラグ
                csJutogaiRow(.KATAGAKIFG) = csJutogaiRowN(.KATAGAKIFG)
                '肩書コード
                csJutogaiRow(.KATAGAKICD) = csJutogaiRowN(.KATAGAKICD)
                '肩書
                csJutogaiRow(.KATAGAKI) = csJutogaiRowN(.KATAGAKI)
                '連絡先1
                csJutogaiRow(.RENRAKUSAKI1) = csJutogaiRowN(.RENRAKUSAKI1)
                '連絡先2
                csJutogaiRow(.RENRAKUSAKI2) = csJutogaiRowN(.RENRAKUSAKI2)
                '行政区コード
                csJutogaiRow(.GYOSEIKUCD) = csJutogaiRowN(.GYOSEIKUCD)
                '行政区名
                csJutogaiRow(.GYOSEIKUMEI) = csJutogaiRowN(.GYOSEIKUMEI)
                '地区コード1
                csJutogaiRow(.CHIKUCD1) = csJutogaiRowN(.CHIKUCD1)
                '地区名1
                csJutogaiRow(.CHIKUMEI1) = csJutogaiRowN(.CHIKUMEI1)
                '地区コード2
                csJutogaiRow(.CHIKUCD2) = csJutogaiRowN(.CHIKUCD2)
                '地区名2
                csJutogaiRow(.CHIKUMEI2) = csJutogaiRowN(.CHIKUMEI2)
                '地区コード3
                csJutogaiRow(.CHIKUCD3) = csJutogaiRowN(.CHIKUCD3)
                '地区名3
                csJutogaiRow(.CHIKUMEI3) = csJutogaiRowN(.CHIKUMEI3)
                '登録異動年月日
                csJutogaiRow(.TOROKUIDOYMD) = csJutogaiRowN(.TOROKUIDOYMD)
                '登録事由コード
                csJutogaiRow(.TOROKUJIYUCD) = csJutogaiRowN(.TOROKUJIYUCD)
                '消除異動年月日
                csJutogaiRow(.SHOJOIDOYMD) = csJutogaiRowN(.SHOJOIDOYMD)
                '消除事由コード
                csJutogaiRow(.SHOJOJIYUCD) = csJutogaiRowN(.SHOJOJIYUCD)
                'リザーブ
                csJutogaiRow(.RESERVE) = csJutogaiRowN(.RESERVE)
            End With

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名     宛名編集処理
    '* 
    '* 構文           Public Sub EditAtenaJutogai(ByVal csJutogaiEntity As DataSet, _
    '* 　　                             ByVal csAtenaEntity As DataSet)
    '* 
    '* 機能　　       住登外Entityより宛名Entityを追加・編集する
    '* 
    '* 引数           csJutogaiEntity As DataSet  : 住登外(ABJutogaiEntity)
    '* 　　           csAtenaEntity   As DataSet  : 宛名(ABAtenaEntity)
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub EditAtenaJutogai(ByVal IntKoshinKB As Integer, _
                                 ByVal StrIdoYMD As String, _
                                 ByVal csJutogaiEntity As DataSet, _
                                 ByRef csAtenaEntity As DataSet)
        Const THIS_METHOD_NAME As String = "EditAtenaJutogai"
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                 ' エラー定義構造体
        'Dim cuCityInfo As USSCityInfoClass                  ' 市町村情報管理クラス
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataSet As DataSet
        'Dim csColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim strSystemDate As String                         ' システム日付
        Dim csJutogaiRow As DataRow                         ' 住登外DataRow
        Dim cIdoJiyuB As ABIdoJiyuBClass                    ' 異動事由Ｂクラス
        '* 履歴番号 000006 2007/10/11 削除開始
        '* 履歴番号 000002 2003/08/22 修正開始
        'Dim cuKanriJohoB As URKANRIJOHOBClass               ' 管理情報Ｂクラス
        'Dim cuKanriJohoB As URKANRIJOHOCacheBClass          ' 管理情報Ｂクラス(キャッシュ対応版)
        '* 履歴番号 000002 2003/08/22 修正終了
        'Dim emKensakShimei As FrnKensakuShimeiType          ' 外国人検索用氏名
        '* 履歴番号 000006 2007/10/11 削除終了
        '* corresponds to VS2008 Start 2010/04/16 000007
        Dim cABEnumDefine As New ABEnumDefine
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '宛名検索キー
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim cAtenaB As ABAtenaBClass                        '宛名ＤＡ
        '* corresponds to VS2008 End 2010/04/16 000007


        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 異動事由Ｂクラスのインスタンス化
            cIdoJiyuB = New ABIdoJiyuBClass(m_cfControlData, m_cfConfigData)

            ' ＵＲ管理情報Ｂクラスのインスタンス化
            '* 履歴番号 000006 2007/10/11 削除開始
            '* 履歴番号 000002 2003/08/22 修正開始
            'cuKanriJohoB = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            'cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            '* 履歴番号 000002 2003/08/22 修正終了
            '* 履歴番号 000006 2007/10/11 削除開始

            ' 日付クラスの必要な設定を行う
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None

            '* 履歴番号 000006 2007/10/11 削除開始
            '' ＵＲ外国人検索用氏名を取得する   保留
            'emKensakShimei = cuKanriJohoB.GetFrn_KensakuShimei_Param
            '* 履歴番号 000006 2007/10/11 削除開始

            ' 住登外データRow
            csJutogaiRow = csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)

            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow
                ' DataRowの初期化
                m_cCommonClass.InitColumnValue(csRow)
            Else
                ' 宛名マスタを取得する
                ' 宛名ＤＡクラスのインスタンス化
                csRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
            End If

            '**
            '* 編集処理
            '*
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")                                        'システム日付

            csRow(ABAtenaEntity.JUMINCD) = csJutogaiRow(ABJutogaiEntity.JUMINCD)                                ' 住民コード
            csRow(ABAtenaEntity.SHICHOSONCD) = csJutogaiRow(ABJutogaiEntity.SHICHOSONCD)                        ' 市町村コード
            csRow(ABAtenaEntity.KYUSHICHOSONCD) = csJutogaiRow(ABJutogaiEntity.KYUSHICHOSONCD)                  ' 旧市町村コード
            csRow(ABAtenaEntity.JUMINJUTOGAIKB) = "2"                                                           ' 住民住登外区分
            csRow(ABAtenaEntity.JUMINYUSENIKB) = "0"                                                            ' 住民優先区分
            csRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"                                                           ' 住登外優先区分
            csRow(ABAtenaEntity.ATENADATAKB) = csJutogaiRow(ABJutogaiEntity.ATENADATAKB)                        ' 宛名データ区分
            csRow(ABAtenaEntity.STAICD) = csJutogaiRow(ABJutogaiEntity.STAICD)                                  ' 世帯コード
            csRow(ABAtenaEntity.ATENADATASHU) = csJutogaiRow(ABJutogaiEntity.ATENADATASHU)                      ' 宛名データ種別
            csRow(ABAtenaEntity.HANYOKB1) = csJutogaiRow(ABJutogaiEntity.HANYOKB1)                              ' 汎用区分1
            csRow(ABAtenaEntity.KJNHJNKB) = csJutogaiRow(ABJutogaiEntity.KJNHJNKB)                              ' 個人法人区分
            csRow(ABAtenaEntity.HANYOKB2) = csJutogaiRow(ABJutogaiEntity.HANYOKB2)                              ' 汎用区分2
            csRow(ABAtenaEntity.KANNAIKANGAIKB) = csJutogaiRow(ABJutogaiEntity.KANNAIKANGAIKB)                  ' 管内管外区分
            csRow(ABAtenaEntity.KANAMEISHO1) = csJutogaiRow(ABJutogaiEntity.KANAMEISHO1)                        ' カナ名称1
            csRow(ABAtenaEntity.KANJIMEISHO1) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO1)                      ' 漢字名称1
            csRow(ABAtenaEntity.KANAMEISHO2) = csJutogaiRow(ABJutogaiEntity.KANAMEISHO2)                        ' カナ名称2
            csRow(ABAtenaEntity.KANJIMEISHO2) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO2)                      ' 漢字名称2
            csRow(ABAtenaEntity.KANJIHJNKEITAI) = csJutogaiRow(ABJutogaiEntity.KANJIHJNKEITAI)                  ' 漢字法人形態
            csRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJutogaiRow(ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI)  ' 漢字法人代表者氏名
            csRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO1)             ' 検索用漢字名称
            csRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANASEIMEI)              ' 検索用カナ姓名
            csRow(ABAtenaEntity.SEARCHKANASEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANASEI)                    ' 検索用カナ姓
            csRow(ABAtenaEntity.SEARCHKANAMEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANAMEI)                    ' 検索用カナ名
            csRow(ABAtenaEntity.RRKST_YMD) = StrIdoYMD                                                      ' 履歴開始年月日
            csRow(ABAtenaEntity.RRKED_YMD) = "99999999"                                                         ' 履歴終了年月日
            csRow(ABAtenaEntity.UMAREYMD) = csJutogaiRow(ABJutogaiEntity.UMAREYMD)                              ' 生年月日
            csRow(ABAtenaEntity.UMAREWMD) = csJutogaiRow(ABJutogaiEntity.UMAREWMD)                              ' 生和暦年月日
            csRow(ABAtenaEntity.SEIBETSUCD) = csJutogaiRow(ABJutogaiEntity.SEIBETSUCD)                          ' 性別コード
            csRow(ABAtenaEntity.SEIBETSU) = csJutogaiRow(ABJutogaiEntity.SEIBETSU)                              ' 性別
            csRow(ABAtenaEntity.ZOKUGARACD) = csJutogaiRow(ABJutogaiEntity.ZOKUGARACD)                          ' 続柄コード
            csRow(ABAtenaEntity.ZOKUGARA) = csJutogaiRow(ABJutogaiEntity.ZOKUGARA)                              ' 続柄
            csRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJutogaiRow(ABJutogaiEntity.DAI2ZOKUGARACD)                  ' 第2続柄コード
            csRow(ABAtenaEntity.DAI2ZOKUGARA) = csJutogaiRow(ABJutogaiEntity.DAI2ZOKUGARA)                      ' 第2続柄
            csRow(ABAtenaEntity.YUBINNO) = csJutogaiRow(ABJutogaiEntity.YUBINNO)                                ' 郵便番号
            csRow(ABAtenaEntity.JUSHOCD) = csJutogaiRow(ABJutogaiEntity.JUSHOCD)                                ' 住所コード
            csRow(ABAtenaEntity.JUSHO) = csJutogaiRow(ABJutogaiEntity.JUSHO)                                    ' 住所
            csRow(ABAtenaEntity.BANCHICD1) = csJutogaiRow(ABJutogaiEntity.BANCHICD1)                            ' 番地コード1
            csRow(ABAtenaEntity.BANCHICD2) = csJutogaiRow(ABJutogaiEntity.BANCHICD2)                            ' 番地コード2
            csRow(ABAtenaEntity.BANCHICD3) = csJutogaiRow(ABJutogaiEntity.BANCHICD3)                            ' 番地コード3
            csRow(ABAtenaEntity.BANCHI) = csJutogaiRow(ABJutogaiEntity.BANCHI)                                  ' 番地
            csRow(ABAtenaEntity.KATAGAKIFG) = csJutogaiRow(ABJutogaiEntity.KATAGAKIFG)                          ' 方書フラグ
            csRow(ABAtenaEntity.KATAGAKICD) = csJutogaiRow(ABJutogaiEntity.KATAGAKICD)                          ' 方書コード
            csRow(ABAtenaEntity.KATAGAKI) = csJutogaiRow(ABJutogaiEntity.KATAGAKI)                              ' 方書
            csRow(ABAtenaEntity.RENRAKUSAKI1) = csJutogaiRow(ABJutogaiEntity.RENRAKUSAKI1)                      ' 連絡先1
            csRow(ABAtenaEntity.RENRAKUSAKI2) = csJutogaiRow(ABJutogaiEntity.RENRAKUSAKI2)                      ' 連絡先2
            ' 直近異動年月日
            'm_cfDateClass.p_strDateValue = m_cNyuryokuParaX.p_strCkinIdoYMD
            '*履歴番号 000002 2004/05/17 修正開始
            'csRow(ABAtenaEntity.CKINIDOYMD) = strSystemDate                                                      ' 履歴開始年月日
            csRow(ABAtenaEntity.CKINIDOYMD) = StrIdoYMD
            '*履歴番号 000002 2004/05/17 修正終了
            ' 登録異動年月日
            csRow(ABAtenaEntity.TOROKUIDOYMD) = csJutogaiRow(ABJutogaiEntity.TOROKUIDOYMD)
            ' 登録異動和暦年月日
            m_cfDateClass.p_strDateValue = CType(csJutogaiRow(ABJutogaiEntity.TOROKUIDOYMD), String)
            csRow(ABAtenaEntity.TOROKUIDOWMD) = m_cfDateClass.p_strWarekiYMD
            csRow(ABAtenaEntity.TOROKUJIYUCD) = csJutogaiRow(ABJutogaiEntity.TOROKUJIYUCD)                      ' 登録事由コード
            csRow(ABAtenaEntity.TOROKUJIYU) = cIdoJiyuB.GetIdoJiyu(csJutogaiRow(ABJutogaiEntity.TOROKUJIYUCD).ToString)     ' 登録事由
            csRow(ABAtenaEntity.SHOJOIDOYMD) = csJutogaiRow(ABJutogaiEntity.SHOJOIDOYMD)                        ' 消除異動年月日
            csRow(ABAtenaEntity.SHOJOJIYUCD) = csJutogaiRow(ABJutogaiEntity.SHOJOJIYUCD)                        ' 消除事由コード
            csRow(ABAtenaEntity.SHOJOJIYU) = cIdoJiyuB.GetIdoJiyu(csJutogaiRow(ABJutogaiEntity.SHOJOJIYUCD).ToString)       ' 消除事由
            csRow(ABAtenaEntity.GYOSEIKUCD) = csJutogaiRow(ABJutogaiEntity.GYOSEIKUCD)                          ' 行政区コード
            csRow(ABAtenaEntity.GYOSEIKUMEI) = csJutogaiRow(ABJutogaiEntity.GYOSEIKUMEI)                        ' 行政区名
            csRow(ABAtenaEntity.CHIKUCD1) = csJutogaiRow(ABJutogaiEntity.CHIKUCD1)                              ' 地区コード1
            csRow(ABAtenaEntity.CHIKUMEI1) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI1)                            ' 地区名1
            csRow(ABAtenaEntity.CHIKUCD2) = csJutogaiRow(ABJutogaiEntity.CHIKUCD2)                              ' 地区コード2
            csRow(ABAtenaEntity.CHIKUMEI2) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI2)                            ' 地区名2
            csRow(ABAtenaEntity.CHIKUCD3) = csJutogaiRow(ABJutogaiEntity.CHIKUCD3)                              ' 地区コード3
            csRow(ABAtenaEntity.CHIKUMEI3) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI3)                            ' 地区名3
            csRow(ABAtenaEntity.KAOKUSHIKIKB) = csJutogaiRow(ABJutogaiEntity.KAOKUSHIKIKB)                      ' 家屋敷区分
            csRow(ABAtenaEntity.BIKOZEIMOKU) = csJutogaiRow(ABJutogaiEntity.BIKOZEIMOKU)                        ' 備考税目

            ' 新規作成の場合
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Add(csRow)
            End If

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名     宛名履歴編集処理
    '* 
    '* 構文           Public Sub EditAtenaRireki(ByVal csAtenaEntity As DataSet, _
    '* 　　                                  ByVal csAtenaRirekiEntity As DataSet)
    '* 
    '* 機能　　       宛名履歴の編集を行なう。
    '* 
    '* 引数           csAtenaEntity        As DataSet  : 宛名(ABAtenaEntity)
    '* 　　           csAtenaRirekiEntity  As DataSet  : 宛名履歴(ABAtenaRirekiEntity)
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Overloads Sub EditAtenaRireki(ByVal StrIdoYMD As String, _
                                          ByVal csAtenaEntity As DataSet, _
                                          ByRef csAtenaRirekiEntity As DataSet)
        Const THIS_METHOD_NAME As String = "EditAtenaRireki"                    'メソッド名
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                                     'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        Dim csRows As DataRow()
        Dim csColumn As DataColumn
        Dim strSearchKana(4) As String                                          '検索用カナ
        Dim csAtenaRow As DataRow                                               '宛名DataRow
        Dim csAtenaRows As DataRow()
        Dim strSystemDate As String                                             'システム日付
        '*履歴番号 000005 2006/09/13 追加開始
        Dim csRirekiRow As DataRow                ' 絞込み・ソートを施したレコードたち
        Dim strST_YMD As String                   ' 開始年月日
        Dim strED_YMD As String                   ' 終了年月日
        Dim blnHit As Boolean = False             ' 当てはまったかどうか
        Dim strRirekiNO As String
        '*履歴番号 000005 2006/09/13 追加終了

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 必要な設定を行う
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            m_cfDateClass.p_enEraType = UFEraType.Number

            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")        'システム日付

            ' 宛名Rowを取得する
            csAtenaRows = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Select(ABAtenaEntity.JUMINJUTOGAIKB + "='2'")
            csAtenaRow = csAtenaRows(0)

            ' 宛名履歴より新しいRowを取得する
            csRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
            ' DataRowの初期化
            m_cCommonClass.InitColumnValue(csRow)

            '**
            '* 編集処理
            '*

            If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                ' 履歴番号
                csRow(ABAtenaRirekiEntity.RIREKINO) = "0001"

                '*履歴番号 000005 2006/09/13 追加開始
                ' 宛名マスタを宛名履歴へそのまま編集する
                For Each csColumn In csAtenaRow.Table.Columns
                    csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                Next csColumn

                ' 宛名履歴へ追加する
                csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
                '*履歴番号 000005 2006/09/13 追加終了
            Else
                '*履歴番号 000005 2006/09/13 修正開始
                '* corresponds to VS2008 Start 2010/04/16 000007
                ''csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                '''' 履歴終了日にシステム日付の前日を設定する
                ''m_cfDateClass.p_strDateValue = StrIdoYMD
                ''csRows(0).BeginEdit()
                ''csRows(0).Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                ''csRows(0).EndEdit()

                '''' 履歴番号
                ''csRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                '* corresponds to VS2008 End 2010/04/16 000007

                ' 追加するレコード用に履歴番号を取得する
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                strRirekiNO = CType((CType(csRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).RPadLeft(4, "0"c)

                ' 住民住登外区分="2"で抽出し、履歴開始年月日昇順・履歴番号昇順にソートする
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2'", ABAtenaRirekiEntity.RRKST_YMD + " ASC , " + ABAtenaRirekiEntity.RIREKINO + " ASC")

                ' どのレコードの開始～終了に当てはまるかを調べる
                For Each csRirekiRow In csRows
                    ' 開始・終了年月日を取得
                    strST_YMD = CStr(csRirekiRow.Item(ABAtenaRirekiEntity.RRKST_YMD))
                    strED_YMD = CStr(csRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD))

                    If blnHit = False Then
                        ' まだ当てはまるレコードが見つかっていない
                        If strST_YMD > StrIdoYMD Then
                            ' 開始年月日＞StrIdoYMD

                            blnHit = True   ' フラグをTrueにして、これ以降のレコードの更新を行う

                        ElseIf (strST_YMD <= StrIdoYMD AndAlso StrIdoYMD <= strED_YMD) AndAlso _
                                strED_YMD <> "99999999" Then
                            ' 開始年月日≦StrIdoYMD≦終了年月日
                            ' かつ
                            ' 終了年月日が"99999999"でない

                            blnHit = True   ' フラグをTrueにして、これ以降のレコードの更新を行う

                        End If
                    End If

                    ' 当てはまるレコードが見つかった場合
                    If blnHit = True Then
                        ' 宛名マスタを宛名履歴へそのまま編集する
                        For Each csColumn In csAtenaRow.Table.Columns
                            If csColumn.ColumnName <> ABAtenaRirekiEntity.JUMINCD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RIREKINO AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RRKST_YMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RRKED_YMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.CKINIDOYMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.SAKUSEINICHIJI AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.SAKUSEIUSER Then
                                ' 住民CD・履歴番号・開始・終了・直近異動年月日・作成日時・作成ユーザ以外を上書きする

                                csRirekiRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                            End If
                        Next csColumn
                    End If

                Next csRirekiRow

                ' 当てはまるレコードが見つからなかった場合、直近で分割する
                If blnHit = False Then
                    ' 住民住登外区分="2"、履歴終了年月日="99999999"で抽出
                    csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")
                    If csRows.Length > 0 Then
                        m_cfDateClass.p_strDateValue = StrIdoYMD
                        ' 直近レコードの終了年月日をStrIdoYMDの一日前の値で更新する
                        csRows(0).BeginEdit()
                        csRows(0).Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        csRows(0).EndEdit()
                    End If

                    ' 宛名マスタを宛名履歴へそのまま編集する
                    For Each csColumn In csAtenaRow.Table.Columns
                        csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                    Next csColumn

                    ' 履歴番号を設定する
                    csRow(ABAtenaRirekiEntity.RIREKINO) = strRirekiNO

                    ' 宛名履歴へ追加する
                    csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
                End If
                '*履歴番号 000005 2006/09/13 修正終了
            End If

            '*履歴番号 000005 2006/09/13 削除開始
            '* corresponds to VS2008 Start 2010/04/16 000007
            '''' 宛名マスタを宛名履歴へそのまま編集する
            ''For Each csColumn In csAtenaRow.Table.Columns
            ''    csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
            ''Next csColumn

            '''' 宛名履歴へ追加する
            ''csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
            '*履歴番号 000005 2006/09/13 削除終了
            '* corresponds to VS2008 End 2010/04/16 000007

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名     宛名累積処理
    '* 
    '* 構文           Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
    '*                                             ByRef csAtenaRuisekiEntity As DataSet, _
    '*                                             ByVal csRirekiCkinRow As DataRow)
    '* 
    '* 機能　　       宛名履歴の編集を行なう。
    '* 
    '* 引数           csAtenaRirekiEntity   As DataSet  : 宛名履歴(ABAtenaRirekiEntity)
    '* 　　           csAtenaRuisekiEntity  As DataSet  : 宛名累積(ABAtenaRuisekiEntity)
    '* 　　           csRirekiCkinRow       As DataRow  : 手を加える前の履歴直近ロウ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    '*履歴番号 000005 2006/09/13 修正開始
    ''Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
    ''ByRef csAtenaRuisekiEntity As DataSet)
    Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
                                 ByRef csAtenaRuisekiEntity As DataSet, _
                                 ByVal csRirekiCkinRow As DataRow)
        '*履歴番号 000005 2006/09/13 修正終了
        Const THIS_METHOD_NAME As String = "EditAtenaRuiseki"                   'メソッド名
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                                     ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        Dim csRows As DataRow()
        Dim csColumn As DataColumn
        Dim strSearchKana(4) As String                                          '検索用カナ
        Dim csAtenaRirekiRow As DataRow                                         '宛名履歴DataRow
        Dim strSystemDate As String                                             'システム日付
        '*履歴番号 000005 2006/09/13 追加開始
        Dim blnAtoAdd As Boolean = False                                        '後のレコードを追加したかどうか
        '*履歴番号 000005 2006/09/13 追加終了

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '*履歴番号 000004 2006/05/31 追加開始
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")                   'システム日付
            '*履歴番号 000004 2006/05/31 追加終了

            '*履歴番号 000005 2006/09/13 追加開始
            ' 累積(前)を生成し追加する
            ' 累積(前)は操作前の履歴直近レコードとする
            If Not (csRirekiCkinRow Is Nothing) Then
                ' 宛名累積より新しいRowを取得する
                csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ' DataRowの初期化
                m_cCommonClass.InitColumnValue(csRow)

                ' 処理日時
                csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                ' 前後区分
                csRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                ' 履歴→累積
                For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                    csRow(csColumn.ColumnName) = csRirekiCkinRow(csColumn)
                Next csColumn

                ' 宛名累積へ追加する
                csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)
            End If
            '*履歴番号 000005 2006/09/13 追加終了

            For Each csAtenaRirekiRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows

                If csAtenaRirekiRow.RowState = DataRowState.Added Then
                    ' 宛名累積より新しいRowを取得する
                    csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                    ' DataRowの初期化
                    m_cCommonClass.InitColumnValue(csRow)

                    ' 処理日時
                    csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                    ' 前後区分
                    csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                    ' 宛名履歴マスタを宛名累積へそのまま編集する
                    For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                        csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                    Next csColumn

                    ' 宛名累積へ追加する
                    csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

                    blnAtoAdd = True
                    Exit For
                End If
                '* corresponds to VS2008 Start 2010/04/16 000007
                ''*履歴番号 000005 2006/09/13 削除開始
                ''''Select Case csAtenaRirekiRow.RowState
                ''''    Case DataRowState.Added

                ''''        ' 宛名累積より新しいRowを取得する
                ''''        csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ''''        ' DataRowの初期化
                ''''        m_cCommonClass.InitColumnValue(csRow)

                ''''        '**
                ''''        '* 編集処理
                ''''        '*

                ''''        ' 処理日時
                ''''        '*履歴番号 000004 2006/05/31 削除開始
                ''''        'strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")                   'システム日付
                ''''        '*履歴番号 000004 2006/05/31 削除終了
                ''''        csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate

                ''''        ' 前後区分
                ''''        csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                ''''        ' 宛名マスタを宛名履歴へそのまま編集する
                ''''        For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                ''''            csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                ''''        Next csColumn

                ''''        ' 宛名累積へ追加する
                ''''        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

                ''''        '*履歴番号 000004 2006/05/31 追加開始
                ''''    Case DataRowState.Modified
                ''''        ' 宛名累積より新しいRowを取得する
                ''''        csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ''''        ' DataRowの初期化
                ''''        m_cCommonClass.InitColumnValue(csRow)

                ''''        '**
                ''''        '* 編集処理
                ''''        '*

                ''''        ' 処理日時
                ''''        csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate

                ''''        ' 前後区分
                ''''        csRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                ''''        ' 宛名履歴データを宛名累積へそのまま編集する
                ''''        For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                ''''            csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                ''''        Next csColumn

                ''''        ' 宛名累積へ追加する
                ''''        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)
                ''''        '*履歴番号 000004 2006/05/31 追加終了
                ''''End Select
                ''*履歴番号 000005 2006/09/13 削除終了
                '* corresponds to VS2008 End 2010/04/16 000007
            Next csAtenaRirekiRow

            ' ここで累積(後)がまだ追加されていない場合(追加なしで更新しただけの場合)
            If blnAtoAdd = False Then
                ' 操作後の履歴直近レコードを取得する
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")

                ' 宛名累積より新しいRowを取得する
                csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ' DataRowの初期化
                m_cCommonClass.InitColumnValue(csRow)

                ' 処理日時
                csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                ' 前後区分
                csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                ' 宛名履歴マスタを宛名累積へそのまま編集する
                For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                    csRow(csColumn.ColumnName) = csRows(0)(csColumn)
                Next csColumn

                ' 宛名累積へ追加する
                csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

            End If

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

    End Sub


    '************************************************************************************************
    '* メソッド名     検索用カナ取得
    '* 
    '* 構文           Public Function GetSearchKana(ByVal strKanaMeisho As String) As String()
    '* 
    '* 機能　　       検索用カナ名称を編集する
    '* 
    '* 引数           strKanaMeisho As String     : カナ名称
    '* 
    '* 戻り値         String()        : [0]検索用カナ姓名
    '*                                  : [1]検索用カナ姓
    '*                                  : [2]検索用カナ名
    '*                                  : [3]カナ姓
    '*                                  : [4]カナ名
    '************************************************************************************************
    Private Function GetSearchKana(ByVal strKanaMeisho As String) As String()
        Const THIS_METHOD_NAME As String = "GetSearchKana"                      'メソッド名
        Dim strSearchKana(4) As String                      '検索用カナ
        Dim cuString As New USStringClass()                 '文字列編集
        Dim intIndex As Integer                             '先頭からの空白位置

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' カナ姓名（空白を詰める）
            strSearchKana(0) = cuString.ToKanaKey(strKanaMeisho).Replace(" ", String.Empty)

            ' 先頭からの空白位置を調べる
            intIndex = strKanaMeisho.RIndexOf(" ")

            ' 空白が存在しない場合
            If (intIndex = -1) Then
                ' カナ姓・名
                strSearchKana(1) = strSearchKana(0)
                strSearchKana(3) = strKanaMeisho
                strSearchKana(2) = String.Empty
                strSearchKana(4) = String.Empty
            Else
                ' カナ姓・名
                strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex))
                strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                ' 先頭からの空白位置が文字列長と以上場合
                If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1))
                    strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                End If
            End If

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

        Return strSearchKana

    End Function

    '*履歴番号 000003 2006/03/27 追加開始
    '************************************************************************************************
    '* メソッド名      宛名レプリカデータ更新
    '* 
    '* 構文            Public Sub AtenaDataReplicaKoshin(ByVal strJuminCD As String, _
    '*                                      ByVal strStaiCD As String, ByVal strKoshinKB As String)
    '* 
    '* 機能　　        宛名レプリカデータの更新処理を行なう
    '* 
    '* 引数           strJuminCD：住民コード
    '*                  strStaiCD：世帯コード
    '*                  strKoshinKB：更新区分（追加：1　修正：2）
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub AtenaDataReplicaKoshin(ByVal strJuminCD As String, ByVal strStaiCD As String, ByVal strKoshinKB As String)
        Const THIS_METHOD_NAME As String = "AtenaDataReplicaKoshin"
        Const WORK_FLOW_NAME As String = "宛名異動"             ' ワークフロー名
        Const DATA_NAME As String = "宛名"                      'データ名
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
        Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
        Dim csABToshoPrmEntity As New DataSet()             'レプリカ作成用パラメータデータセット
        Dim csABToshoPrmTable As DataTable                  'レプリカ作成用パラメータデータテーブル
        Dim csABToshoPrmRow As DataRow                      'レプリカ作成用パラメータデータテーブル
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass


        Try
            ' デバッグログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            '  宛名管理情報の種別04識別キー01のデータを全件取得する
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

            '管理情報のワークフローレコードが存在し、パラメータが"1"と"2"の時だけワークフロー処理を行う
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
                    (CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" OrElse _
                        CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "2") Then

                'データセット取得クラスのインスタンス化
                cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                ' テーブルセットの取得
                csABToshoPrmTable = cABAtenaCnvBClass.CreateColumnsToshoPrmData()
                csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME
                ' データセットにテーブルセットの追加
                csABToshoPrmEntity.Tables.Add(csABToshoPrmTable)

                '新規ロウの作成
                csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow()
                'レプリカデータ作成用パラメータにセット
                csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD                 '住民コード
                csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = strStaiCD                   '世帯コード
                csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = strKoshinKB               '更新区分（追加:1 修正:2 論理削除:9 削除データ回復:2 物理削除:D）
                'データセットにロウを追加する
                csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow)

                'ワークフロー送信処理呼び出し
                cABAtenaCnvBClass.WorkFlowExec(csABToshoPrmEntity, WORK_FLOW_NAME, DATA_NAME)

            End If

            ' デバッグログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

    End Sub
    '*履歴番号 000003 2006/03/27 追加終了

End Class
