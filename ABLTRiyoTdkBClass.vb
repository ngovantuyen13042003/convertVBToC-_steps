'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         ＡＢｅＬＴＡＸ利用届マスタＤＡ(ABLTRiyoTdkBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2008/11/10
'*
'* 作成者　　　     比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2008/11/18   000001     追加処理、更新処理メソッドを追加（比嘉）
'* 2008/11/27   000002     利用届データ取得新メソッドを追加（比嘉）
'* 2009/07/27   000003     利用届出連携機能追加に伴う改修（比嘉）
'* 2009/11/16   000004     検索条件:カナ氏名を検索カナ氏名に修正（比嘉）
'* 2010/02/22   000005     削除処理メソッドを追加（比嘉）
'* 2010/04/16   000006     VS2008対応（比嘉）
'* 2014/08/15   000007     【AB21010】個人番号制度対応 電子申告（岩下）
'* 2015/03/19   000008     【AB21010】個人番号制度対応 電子申告 SQL不具合修正（岩下）
'* 2020/11/06   000009     【AB00189】利用届出複数納税者ID対応（須江）
'* 2024/01/09   000010     【AB-0770-1】利用届出データ管理対応（原野）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text
'*履歴番号 000009 2020/11/06 追加開始
Imports System.Collections.Generic
'*履歴番号 000009 2020/11/06 追加終了

Public Class ABLTRiyoTdkBClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_csDataSchma As DataSet                        ' スキーマ保管用データセット:全項目用
    Private m_csDataSchma_Select As DataSet                 ' スキーマ保管用データセット:納税者ID,利用者ID

    '*履歴番号 000001 2008/11/17 追加開始
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    '*履歴番号 000001 2008/11/17 追加終了
    '*履歴番号 000005 2010/02/22 追加開始
    Private m_strDeleteSQL As String
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'Delete用パラメータコレクション
    '*履歴番号 000005 2010/02/22 追加終了

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABLTRiyoTdkBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* 　　                          ByVal cfRdbClass As UFRdbClass)
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
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' SQL文の作成
        ' 全項目抽出用スキーマ
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, False)
        ' 納税者ID、利用者ID用スキーマ
        m_csDataSchma_Select = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT NOZEIID,RIYOSHAID FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"

#Region "eLTAX利用届データ取得メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ取得メソッド
    '* 
    '* 構文         Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
    '* 
    '* 機能　　     利用届出マスタより該当データを取得する。
    '* 
    '* 引数         csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass   : 利用届出パラメータクラス
    '* 
    '* 戻り値       取得した利用届出マスタの該当データ（DataSet）
    '*                 構造：csLtRiyoTdkEntity    
    '************************************************************************************************
    '*履歴番号 000002 2008/11/27 修正開始
    'Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
    Public Overloads Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
        '*履歴番号 000002 2008/11/27 修正終了
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        Dim csLtRiyoTdkEntity As DataSet                                ' 利用届出マスタデータ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim blnAndFg As Boolean = False                                 ' AND判定フラグ

        '履歴番号 000009 2020/11/06 追加開始
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' 管理情報ビジネスクラス
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' 返却不要業務CDリスト
        Dim strBreakKey As String
        Dim NewDataRow As DataRow
        '履歴番号 000009 2020/11/06 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータチェック
            If (csABLTRiyoTdkParaX.p_strJuminCD.Trim = String.Empty AndAlso _
                csABLTRiyoTdkParaX.p_strZeimokuCD = ABEnumDefine.ZeimokuCDType.Empty) Then
                ' パラメータ:住民CD、税目CDが設定されていない場合は引数エラー
                ' メッセージ『必須項目が入力されていません。：住民コード､税目コードのいずれかを設定してください。』
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "住民コード､税目コードのいずれかを設定してください。", objErrorStruct.m_strErrorCode)
            Else
            End If

            '*履歴番号 000009 2020/11/06 追加開始
            If Not (csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "1" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "2" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "3" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4") Then
                ' パラメータ:利用区分が未設定、又は"1"～"4"のいずれでもない場合は引数エラー
                ' メッセージ『利用届出利用区分』
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                'エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "利用届出利用区分", objErrorStruct.m_strErrorCode)
            End If
            '*履歴番号 000009 2020/11/06 追加終了

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            ' SELECT句
            '*履歴番号 000009 2020/11/06 修正開始
            'If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
            '    ' 出力区分が"1"の場合、『納税者ID､利用者ID』を抽出
            '    strSQL.Append("SELECT ")
            '    strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(", ")
            '    strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID)
            'Else
            '    ' 出力区分が"1"以外の場合、全項目抽出
            '    strSQL.Append("SELECT * ")
            'End If
            ' 出力区分が"1"以外の場合、全項目抽出
            strSQL.Append("SELECT * ")
            '*履歴番号 000009 2020/11/06 修正終了

            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")

            ' 住民コード
            If (csABLTRiyoTdkParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' 住民コードが設定されている場合
                strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = csABLTRiyoTdkParaX.p_strJuminCD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
                ' 住民コードが設定されていない場合、何もセットしない
            End If

            ' 税目コード
            If (csABLTRiyoTdkParaX.p_strZeimokuCD <> ABEnumDefine.ZeimokuCDType.Empty) Then
                ' 税目コードが設定されている場合、抽出条件にする
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTRiyoTdkParaX.p_strZeimokuCD)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If

            ' 廃止フラグ
            If (blnAndFg = True) Then
                ' AND判定フラグが"True"の場合、AND句をセット
                strSQL.Append(" AND ")
            End If

            If (csABLTRiyoTdkParaX.p_blnHaishiFG = False) Then
                ' 廃止区分が"False"の場合、廃止区分が廃止でないものを取得する
                '* AND (HAISHIFG <> '1' OR HAISHIFG <> '2') AND SAKUJOFG <> '1'
                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2' AND ")
                strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
            Else
                '* AND SAKUJOFG <> '1'
                strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
            End If

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            '*履歴番号 000009 2020/11/06 修正開始
            'If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
            '    csLtRiyoTdkEntity = m_csDataSchma_Select.Clone()
            '    csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            'Else
            '    csLtRiyoTdkEntity = m_csDataSchma.Clone()
            '    csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            'End If
            ' この時点ではcsLtRiyoTdkEntityは全項目とする
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '*履歴番号 000009 2020/11/06 修正終了

            '*履歴番号 000009 2020/11/06 追加開始

            ' 管理情報ビジネスクラスのインスタンス化
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '管理情報（10-46）を取得
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            ' 一旦優先順位を付けてソートさせてから取捨選択する事からクローンcsRetLtRiyoTdkEntityを作成
            'csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                ' 出力区分'1'の場合は納税者IDと利用者IDのみ返却するため、2項目のみとする
                csRetLtRiyoTdkEntity = m_csDataSchma_Select.Clone()
            Else
                csRetLtRiyoTdkEntity = m_csDataSchma.Clone()
            End If

            '管理情報（10-46）に該当する業務CDが設定されているか否かで制御を行う
            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '該当する業務CDが設定されていた場合（共通納税は返却不要となる）

                Select Case csABLTRiyoTdkParaX.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '該当する業務CDが設定されていない場合

                Select Case csABLTRiyoTdkParaX.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            ' csRetLtRiyoTdkEntityへのセット
            If (csLtRiyoTdkRow.Length > 0) Then
                '取得件数が0件以上の場合

                If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                    ' 出力区分'1'の場合は納税者IDと利用者IDのみ返却するため、csRetLtRiyoTdkEntityはその2項目のみセットする

                    If csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4" Then
                        '引数：利用区分＝"4"の場合は全件返却する。
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow()                     ' 追加するデータテーブルの新規行とする
                            NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.NOZEIID)      ' 納税者ID
                            NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.RIYOSHAID)  ' 利用者ID
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow)                      ' 返却用データテーブルに行追加
                        Next
                    Else
                        '引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                        strBreakKey = ""

                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                                NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow()                     ' 追加するデータテーブルの新規行とする
                                NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.NOZEIID)      ' 納税者ID
                                NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.RIYOSHAID)  ' 利用者ID
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow)                      ' 返却用データテーブルに行追加
                                strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                            End If
                        Next

                    End If
                Else
                    ' 出力区分'1'以外の場合はそのままIMPORTする。

                    If csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4" Then
                        '引数：利用区分＝"4"の場合は全件返却する。
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                        Next
                    Else
                        '引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                        strBreakKey = ""
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                                strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                            End If
                        Next
                    End If
                End If

            End If
            '*履歴番号 000009 2020/11/06 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        '*履歴番号 000009 2020/11/06 追加開始
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*履歴番号 000009 2020/11/06 追加終了

    End Function
#End Region

    '*履歴番号 000002 2008/11/27 追加開始
#Region "eLTAX利用届データ取得メソッド２"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ取得メソッド２
    '* 
    '* 構文         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass) As DataSet
    '* 
    '* 機能　　     利用届出マスタより該当データを取得する。
    '* 
    '* 引数         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass   : 利用届出パラメータ２クラス
    '* 
    '* 戻り値       取得した利用届出マスタの該当データ（DataSet）
    '*                 構造：csLtRiyoTdkEntity    
    '************************************************************************************************
    Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim csLtRiyoTdkEntity As DataSet                                ' 利用届出マスタデータ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim blnAndFg As Boolean = False                                 ' AND判定フラグ

        '履歴番号 000009 2020/11/06 追加開始
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' 管理情報ビジネスクラス
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' 返却不要業務CDリスト
        Dim strBreakKey As String
        '履歴番号 000009 2020/11/06 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            ' SELECT句
            '*履歴番号 000010 2024/01/09 修正開始
            'strSQL.Append("SELECT * ")
            strSQL.Append("SELECT ")
            strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*")
            '*履歴番号 000010 2024/01/09 修正終了

            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            '*履歴番号 000010 2024/01/09 追加開始
            If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                strSQL.Append(" INNER JOIN ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL.Append(" ON ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL.Append(" = ")
                strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL.Append(" = ")
                strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strMyNumber)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*履歴番号 000010 2024/01/09 追加終了

            ' WHERE句
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' 税目区分
            If (cABLTRiyoTdkPara2X.p_strTaxKB.Trim <> String.Empty) Then
                ' 税目区分が設定されている場合

                strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strTaxKB)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 納税者ID
            If (cABLTRiyoTdkPara2X.p_strNozeiID.Trim <> String.Empty) Then
                ' 納税者IDが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_NOZEIID)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 利用者ID
            If (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim <> String.Empty) Then
                ' 利用者IDが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '*履歴番号 000010 2024/01/09 削除開始
            ''*履歴番号 000007 2014/08/15 追加開始
            ''---------------------------------------------------------------------------------
            '' 個人番号
            ''If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
            ''    住民コードが設定されている場合
            ''    If (blnAndFg = True) Then
            ''        AND判定フラグが"True"の場合、AND句をセット
            ''        strSQL.Append(" AND ")
            ''    End If

            ''    strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            ''    strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)

            ''    検索条件のパラメータを作成
            ''    cfUFParameterClass = New UFParameterClass
            ''    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
            ''    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

            ''    検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            ''    cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ''    AND判定フラグをセット
            ''    blnAndFg = True
            ''Else
            ''End If
            ''*履歴番号 000007 2014/08/15 追加終了
            '*履歴番号 000010 2024/01/09 削除終了
            '---------------------------------------------------------------------------------
            ' 住民コード
            If (cABLTRiyoTdkPara2X.p_strJuminCD.Trim <> String.Empty) Then
                ' 住民コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 受付番号
            If (cABLTRiyoTdkPara2X.p_strRcptNO.Trim <> String.Empty) Then
                ' 受付番号が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTNO)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptNO)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 受付日
            If (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim <> String.Empty) Then
                ' 受付日が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1")

                strSQL.Append(" AND ")

                strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, "0"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, "9"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            '*履歴番号 000003 2009/07/27 追加開始
            ' 処理日
            If (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim <> String.Empty) Then
                ' 処理日が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1")

                strSQL.Append(" AND ")

                strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, "0"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, "9"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' カナ・漢字名称
            ' カナ名称
            If Not (cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") = -1) Then
                    '*履歴番号 000004 2009/11/16 修正開始
                    strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*履歴番号 000004 2009/11/16 修正終了
                    strSQL.Append(" = ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho
                Else
                    '*履歴番号 000004 2009/11/16 修正開始
                    strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*履歴番号 000004 2009/11/16 修正終了
                    strSQL.Append(" LIKE ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd
                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            End If

            '検索用漢字名称
            If Not (cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") = -1) Then
                    strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strSQL.Append(" = ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho
                Else
                    strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strSQL.Append(" LIKE ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd

                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            End If
            '*履歴番号 000003 2009/07/27 追加終了
            '---------------------------------------------------------------------------------
            ' 廃止フラグ
            If (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim <> String.Empty) Then
                ' 廃止フラグが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strSQL.Append(" AND ")
                End If

                Select Case cABLTRiyoTdkPara2X.p_strHaishiFG
                    Case "0"    ' 有効のみ
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'")

                    Case "1"    ' 廃止のみ
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'")

                    Case "2"    ' 税目削除のみ
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'")
                    Case Else
                End Select

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 削除フラグ
            If (blnAndFg = True) Then
                ' AND判定フラグが"True"の場合、AND句をセット
                strSQL.Append(" AND ")
                '*履歴番号 000010 2024/01/09 修正開始
                'strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*履歴番号 000010 2024/01/09 修正終了

            Else
                '*履歴番号 000010 2024/01/09 修正開始
                'strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*履歴番号 000010 2024/01/09 修正終了
            End If
            '---------------------------------------------------------------------------------
            ' 最大取得件数
            If (cABLTRiyoTdkPara2X.p_intGetCountMax <> 0) Then
                m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax
            Else
            End If

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            '*履歴番号 000009 2020/11/06 追加開始

            ' 管理情報ビジネスクラスのインスタンス化
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '管理情報（10-46）を取得
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '管理情報（10-46）に該当する業務CDが設定されていた場合は共通納税は不要

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '管理情報（10-46）に該当する業務CDが設定されていない場合

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            If (csLtRiyoTdkRow.Length > 0) Then
                '取得件数が0件以上の場合
                If cABLTRiyoTdkPara2X.p_strRiyoKB.Trim = "4" Then
                    '引数：利用区分＝"4"の場合は全件返却する。
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                    Next
                Else
                    'csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                    '引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                    strBreakKey = ""
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                            strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                        End If
                    Next
                End If
            End If
            '*履歴番号 000009 2020/11/06 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        '*履歴番号 000009 2020/11/06 追加開始
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*履歴番号 000009 2020/11/06 追加終了

    End Function
#End Region
    '*履歴番号 000002 2008/11/27 追加終了

    '*履歴番号 000003 2009/07/27 追加開始
#Region "eLTAX利用届データ取得メソッド３"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ取得メソッド３
    '* 
    '* 構文         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass, _
    '*                                                         ByRef intAllCount As Integer) As DataSet
    '* 
    '* 機能　　     利用届出マスタより該当データを取得する。
    '* 
    '* 引数         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass    : 利用届出パラメータ２クラス
    '*              intAllCount As Integer                          : 全データ件数
    '* 
    '* 戻り値       取得した利用届出マスタの該当データ（DataSet）
    '*                 構造：csLtRiyoTdkEntity    
    '************************************************************************************************
    Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass, _
                                               ByRef intAllCount As Integer) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        Const COL_COUNT As String = "COUNT"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim csLtRiyoTdkEntity As DataSet                                ' 利用届出マスタデータ
        Dim csLtRiyoTdk_AllCount As DataSet                             ' 利用届出マスタ全件取得データ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim strSQL_Conut As New StringBuilder                           ' 全件抽出
        Dim strWhere As New StringBuilder                               ' WHERE文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim blnAndFg As Boolean = False                                 ' AND判定フラグ
        '*履歴番号 000007 2014/08/15 追加開始
        Dim strSQLMyNumber As New StringBuilder                         ' 共通番号SQL
        '*履歴番号 000007 2014/08/15 追加終了

        '履歴番号 000009 2020/11/06 追加開始
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' 管理情報ビジネスクラス
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' 返却不要業務CDリスト
        Dim strBreakKey As String
        '履歴番号 000009 2020/11/06 追加終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            ' SELECT句
            '*履歴番号 000010 2024/01/09 修正開始
            'strSQL.Append("SELECT * ")
            strSQL.Append("SELECT ")
            strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*")
            '*履歴番号 000010 2024/01/09 修正終了
            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            strSQL_Conut.Append("SELECT COUNT(*) AS ").Append(COL_COUNT)
            strSQL_Conut.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            '*履歴番号 000010 2024/01/09 追加開始
            If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                strSQL.Append(" INNER JOIN ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL.Append(" ON ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL.Append(" = ")
                strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL.Append(" = ")
                strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL.Append(" <> '1' ")

                strSQL_Conut.Append(" INNER JOIN ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL_Conut.Append(" ON ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL_Conut.Append(" <> '1' ")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strMyNumber)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*履歴番号 000010 2024/01/09 追加終了

            ' WHERE句
            strWhere.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' 税目区分
            If (cABLTRiyoTdkPara2X.p_strTaxKB.Trim <> String.Empty) Then
                ' 税目区分が設定されている場合

                strWhere.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strTaxKB)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 納税者ID
            If (cABLTRiyoTdkPara2X.p_strNozeiID.Trim <> String.Empty) Then
                ' 納税者IDが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_NOZEIID)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 利用者ID
            If (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim <> String.Empty) Then
                ' 利用者IDが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '*履歴番号 000010 2024/01/09 削除開始
            ''*履歴番号 000007 2014/08/15 追加開始
            ''---------------------------------------------------------------------------------
            '' 個人番号
            'If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
            '    '*履歴番号 000007 2014/08/15 修正開始
            '    '' 住民コードが設定されている場合
            '    'If (blnAndFg = True) Then
            '    '    ' AND判定フラグが"True"の場合、AND句をセット
            '    '    strSQL.Append(" AND ")
            '    'End If

            '    'strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            '    'strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
            '    ' 個人番号が設定されている場合
            '    If (blnAndFg = True) Then
            '        ' AND判定フラグが"True"の場合、AND句をセット
            '        strWhere.Append(" AND ")
            '    End If

            '    strWhere.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            '    strWhere.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
            '    '*履歴番号 000007 2014/08/15 修正終了

            '    ' 検索条件のパラメータを作成
            '    cfUFParameterClass = New UFParameterClass
            '    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
            '    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

            '    ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            '    cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '    ' AND判定フラグをセット
            '    blnAndFg = True
            'Else
            'End If
            ''*履歴番号 000007 2014/08/15 追加終了
            '*履歴番号 000010 2024/01/09 削除終了
            '---------------------------------------------------------------------------------
            ' 住民コード
            If (cABLTRiyoTdkPara2X.p_strJuminCD.Trim <> String.Empty) Then
                ' 住民コードが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 受付番号
            If (cABLTRiyoTdkPara2X.p_strRcptNO.Trim <> String.Empty) Then
                ' 受付番号が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTNO)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptNO)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 受付日
            If (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim <> String.Empty) Then
                ' 受付日が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1")

                strWhere.Append(" AND ")

                strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, "0"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, "9"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            '*履歴番号 000003 2009/07/27 追加開始
            ' 処理日
            If (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim <> String.Empty) Then
                ' 処理日が設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1")

                strWhere.Append(" AND ")

                strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2")

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, "0"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, "9"c)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' カナ・漢字名称
            ' カナ名称
            If Not (cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") = -1) Then
                    '*履歴番号 000004 2009/11/16 修正開始
                    strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*履歴番号 000004 2009/11/16 修正終了
                    strWhere.Append(" = ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho
                Else
                    '*履歴番号 000004 2009/11/16 修正開始
                    strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*履歴番号 000004 2009/11/16 修正終了
                    strWhere.Append(" LIKE ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd
                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            End If

            '検索用漢字名称
            If Not (cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") = -1) Then
                    strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strWhere.Append(" = ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho
                Else
                    strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strWhere.Append(" LIKE ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' 検索条件のパラメータを作成
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd

                End If
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND判定フラグをセット
                blnAndFg = True
            End If
            '*履歴番号 000003 2009/07/27 追加終了
            '---------------------------------------------------------------------------------
            ' 廃止フラグ
            If (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim <> String.Empty) Then
                ' 廃止フラグが設定されている場合
                If (blnAndFg = True) Then
                    ' AND判定フラグが"True"の場合、AND句をセット
                    strWhere.Append(" AND ")
                End If

                Select Case cABLTRiyoTdkPara2X.p_strHaishiFG
                    Case "0"    ' 有効のみ
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'")

                    Case "1"    ' 廃止のみ
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'")

                    Case "2"    ' 税目削除のみ
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'")
                    Case Else
                End Select

                ' AND判定フラグをセット
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' 削除フラグ
            If (blnAndFg = True) Then
                ' AND判定フラグが"True"の場合、AND句をセット
                strWhere.Append(" AND ")
                '*履歴番号 000010 2024/01/09 修正開始
                'strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*履歴番号 000010 2024/01/09 修正終了
            Else
                '*履歴番号 000010 2024/01/09 修正開始
                'strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*履歴番号 000010 2024/01/09 修正終了
            End If
            '---------------------------------------------------------------------------------
            ' 最大取得件数
            If (cABLTRiyoTdkPara2X.p_intGetCountMax <> 0) Then
                m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax
            Else
            End If

            ' SQL文結合処理
            strSQL.Append(strWhere.ToString)
            strSQL_Conut.Append(strWhere.ToString)

            ' 全件取得処理
            csLtRiyoTdk_AllCount = m_cfRdbClass.GetDataSet(strSQL_Conut.ToString, cfUFParameterCollectionClass)

            intAllCount = CInt(csLtRiyoTdk_AllCount.Tables(0).Rows(0)(COL_COUNT))

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            '*履歴番号 000009 2020/11/06 追加開始

            ' 管理情報ビジネスクラスのインスタンス化
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '管理情報（10-46）を取得
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '管理情報（10-46）に該当する業務CDが設定されていた場合は共通納税は不要

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし（ただし、共通納税は除外する）
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '管理情報（10-46）に該当する業務CDが設定されていない場合

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '共通＞申告＞共通納税の優先順
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '申告＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '共通納税＞共通の優先順
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '絞り込みなし
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            If (csLtRiyoTdkRow.Length > 0) Then
                '取得件数が0件以上の場合
                If cABLTRiyoTdkPara2X.p_strRiyoKB.Trim = "4" Then
                    '引数：利用区分＝"4"の場合は全件返却する。
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                    Next
                Else
                    'csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                    '引数：利用区分≠"4"の場合は、住民コード、税目区分、廃止フラグのブレイク時に1件返却する。
                    strBreakKey = ""
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                            strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                        End If
                    Next
                End If
            End If
            '*履歴番号 000009 2020/11/06 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        '*履歴番号 000009 2020/11/06 追加開始
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*履歴番号 000009 2020/11/06 追加終了

    End Function
#End Region
    '*履歴番号 000003 2009/07/27 追加終了

    '*履歴番号 000001 2008/11/18 追加開始
#Region "eLTAX利用届データ追加メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ追加メソッド
    '* 
    '* 構文         Public Function InsertLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     利用届出マスタに新規データを追加する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       追加件数(Integer)
    '************************************************************************************************
    Public Function InsertLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertLTRiyoTdk"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                                  ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intInsCnt As Integer                                        ' 追加件数
        Dim strUpdateDateTime As String                                 ' システム日付

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")        ' 作成日時

            ' 共通項目の編集を行う
            csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId             ' 端末ＩＤ
            csDataRow(ABLtRiyoTdkEntity.SAKUJOFG) = "0"                                         ' 削除フラグ
            csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = Decimal.Zero                           ' 更新カウンタ
            csDataRow(ABLtRiyoTdkEntity.SAKUSEINICHIJI) = strUpdateDateTime                     ' 作成日時
            csDataRow(ABLtRiyoTdkEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId              ' 作成ユーザー
            csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = strUpdateDateTime                      ' 更新日時
            csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId               ' 更新ユーザー


            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return intInsCnt

    End Function
#End Region

#Region "eLTAX利用届データ更新メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ更新メソッド
    '* 
    '* 構文         Public Function UpdateLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     利用届出マスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTRiyoTdk"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                          ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL Is Nothing Or m_strUpDateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId                                 ' 端末ＩＤ
            csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER)) + 1       ' 更新カウンタ
            csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")   ' 更新日時
            csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                   ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) = ABLtRiyoTdkEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return intUpdCnt

    End Function
#End Region

#Region " SQL文の作成"
    '************************************************************************************************
    '* メソッド名   SQL文の作成
    '* 
    '* 構文         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　     INSERT, UPDATEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数         csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  ' パラメータクラス
        Dim strInsertColumn As String                               ' 追加SQL文項目文字列
        Dim strInsertParam As String                                ' 追加SQL文パラメータ文字列
        Dim strWhere As New StringBuilder                           ' 更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABLtRiyoTdkEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL文の作成
            m_strUpDateSQL = "UPDATE " + ABLtRiyoTdkEntity.TABLE_NAME + " SET "

            ' UPDATE Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER)

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL文の作成
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL文のトリミング
            m_strUpDateSQL = m_strUpDateSQL.Trim()
            m_strUpDateSQL = m_strUpDateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpDateSQL += strWhere.ToString

            ' UPDATE コレクションにキー情報を追加
            ' 納税者ID
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付市町村ｺｰﾄﾞ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 税目区分
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try
    End Sub
#End Region
    '*履歴番号 000001 2008/11/18 追加終了

    '*履歴番号 000005 2010/02/22 追加開始
#Region "eLTAX利用届データ削除(物理)メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX利用届データ削除(物理)メソッド
    '* 
    '* 構文         Public Function DeleteLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     利用届出マスタから該当データを物理削除する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       削除件数(Integer)
    '************************************************************************************************
    Public Function DeleteLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTRiyoTdk"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                                  ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intDelCnt As Integer                                        ' 削除件数
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim strUpdateDateTime As String                                 ' システム日付
        '* corresponds to VS2008 End 2010/04/16 000006

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 削除用のパラメータ付DELETE文文字列とパラメータコレクションを作成する
            If ((m_strDeleteSQL Is Nothing) OrElse (m_strDeleteSQL = String.Empty) OrElse _
                (IsNothing(m_cfDeleteUFParameterCollectionClass))) Then
                Call CreateSQL_Delete(csDataRow)
            Else
            End If

            ' 作成済みのパラメータへ削除行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass

                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) = ABLtRiyoTdkEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                  "【クラス名:" + Me.GetType.Name + "】" + _
                                  "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                  "【実行メソッド名:ExecuteSQL】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)


            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return intDelCnt

    End Function
#End Region

#Region "SQL文作成(物理削除)"
    '************************************************************************************************
    '* メソッド名     物理削除用SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL_Delete(ByVal csDataRow As DataRow)
    '* 
    '* 機能           物理DELETE用のSQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL_Delete(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL_Delete"
        Dim cfUFParameterClass As UFParameterClass              ' パラメータクラス
        Dim strWhere As New StringBuilder                       ' WHERE定義

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE文の作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER)

            ' 物理DELETE SQL文の作成
            m_strDeleteSQL = "DELETE FROM " + ABLtRiyoTdkEntity.TABLE_NAME + strWhere.ToString

            ' 物理削除用パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' 物理削除用コレクションにパラメータを追加
            ' 納税者ID
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付市町村ｺｰﾄﾞ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 税目区分
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw
        End Try

    End Sub
#End Region
    '*履歴番号 000005 2010/02/22 追加終了

#End Region

End Class
